using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace H130C_Tester
{
    public static class TestIoPortIn
    {
        public enum NAME { P00, P01, P02, P03 }

        public static List<PortSpec> ListSpecs;

        public class PortSpec
        {
            public NAME name;
            public bool input;
        }

        private static void ResetViewModel()
        {

            State.VmTestResults.ColP00InExp = General.OffBrush;
            State.VmTestResults.ColP01InExp = General.OffBrush;
            State.VmTestResults.ColP02InExp = General.OffBrush;
            State.VmTestResults.ColP03InExp = General.OffBrush;

            State.VmTestResults.ColP00In = General.OffBrush;
            State.VmTestResults.ColP01In = General.OffBrush;
            State.VmTestResults.ColP02In = General.OffBrush;
            State.VmTestResults.ColP03In = General.OffBrush;
        }

        private static void InitList()
        {
            ListSpecs = new List<PortSpec>();
            foreach (var n in Enum.GetValues(typeof(NAME)))
            {
                ListSpecs.Add(new PortSpec { name = (NAME)n });
            }
        }


        /// <summary>
        /// 引数true：開放  false:L（GND）
        /// </summary>
        /// <param name="sw"></param>
        private static bool SetInput(bool sw)
        {
            try
            {
                //製品側は内部でプルアップされているので、開放入力はHと認識する
                General.SetK4_5(false);
                if (sw)
                {
                    General.SetK8_9(false);
                }
                else
                {
                    General.SetK8_9(true);
                }
                return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 引数exp true：期待値 製品がH認識すること  false：期待値 製品がL認識すること
        /// </summary>
        /// <param name="exp"></param>
        private static void AnalysisData(bool exp)
        {
            General.MainIo.SendDataTarget("RP0*00");
            var hexData = General.MainIo.RecieveData;

            //ASCII エンコード 文字列をバイトに変換する
            var StateP0 = Convert.ToInt16(hexData, 16);

            //DIPスイッチの状態を解析 認識値がHならtrueを代入する
            var p00 = (StateP0 & 0b0000_0001) == 0b0000_0001;
            var p01 = (StateP0 & 0b0000_0010) == 0b0000_0010;
            var p02 = (StateP0 & 0b0000_0100) == 0b0000_0100;
            var p03 = (StateP0 & 0b0000_1000) == 0b0000_1000;

            ListSpecs.Find(l => l.name == NAME.P00).input = p00;
            ListSpecs.Find(l => l.name == NAME.P01).input = p01;
            ListSpecs.Find(l => l.name == NAME.P02).input = p02;
            ListSpecs.Find(l => l.name == NAME.P03).input = p03;

            //ビューモデルの更新
            //期待値の設定
            if (exp)
            {
                State.VmTestResults.ColP00InExp = General.OnBrush;
                State.VmTestResults.ColP01InExp = General.OnBrush;
                State.VmTestResults.ColP02InExp = General.OnBrush;
                State.VmTestResults.ColP03InExp = General.OnBrush;
            }
            else
            {
                State.VmTestResults.ColP00InExp = General.OffBrush;
                State.VmTestResults.ColP01InExp = General.OffBrush;
                State.VmTestResults.ColP02InExp = General.OffBrush;
                State.VmTestResults.ColP03InExp = General.OffBrush;

            }

            //取り込み値の設定
            State.VmTestResults.ColP00In = p00 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColP01In = p01 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColP02In = p02 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColP03In = p03 ? General.OnBrush : General.OffBrush;

        }


        public static async Task<bool> Check()
        {
            bool resultOn = false;
            bool resultOff = false;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    InitList();//テストスペック毎回初期化
                    Flags.AddDecision = false;

                    try
                    {
                        resultOn = false;
                        resultOff = false;
                        ResetViewModel();

                        //P00～03を入力ポートに設定する
                        if (!General.MainIo.SendDataTarget("WP0*00")) return false;
                        Sleep(400);


                        //開放入力(製品がH認識することの確認) 検査
                        //テストログの更新
                        State.VmTestStatus.TestLog += "\r\nALL 開放入力";

                        SetInput(true);
                        Sleep(100);

                        AnalysisData(true);

                        resultOn = ListSpecs.All(l => l.input);
                        if (resultOn)
                        {
                            //テストログの更新
                            State.VmTestStatus.TestLog += "---PASS";
                        }
                        else
                        {
                            //テストログの更新
                            State.VmTestStatus.TestLog += "---FAIL";
                            return false;
                        }

                        Sleep(1500);
                        //L(GND)入力(製品がL認識することの確認) 検査
                        //テストログの更新
                        State.VmTestStatus.TestLog += "\r\nALL L入力";

                        SetInput(false);
                        Sleep(100);

                        AnalysisData(false);

                        resultOff = ListSpecs.All(l => !l.input);
                        if (resultOff)
                        {
                            //テストログの更新
                            State.VmTestStatus.TestLog += "---PASS";
                            return true;
                        }
                        else
                        {
                            //テストログの更新
                            State.VmTestStatus.TestLog += "---FAIL";
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                });

            }
            finally
            {
                General.SetK8_9(false);

                if (!resultOn || !resultOff)
                {
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }

        }

    }
}
