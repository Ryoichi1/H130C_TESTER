using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace H130C_Tester
{
    public static class TestIoPortOut
    {
        public enum NAME { P00, P01, P02, P03 }

        public static List<PortSpec> ListSpecs;

        public class PortSpec
        {
            public NAME name;
            public bool output;
        }

        private static void ResetViewModel()
        {

            State.VmTestResults.ColP00OutExp = General.OffBrush;
            State.VmTestResults.ColP01OutExp = General.OffBrush;
            State.VmTestResults.ColP02OutExp = General.OffBrush;
            State.VmTestResults.ColP03OutExp = General.OffBrush;

            State.VmTestResults.ColP00Out = General.OffBrush;
            State.VmTestResults.ColP01Out = General.OffBrush;
            State.VmTestResults.ColP02Out = General.OffBrush;
            State.VmTestResults.ColP03Out = General.OffBrush;
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
        /// 引数true 5V出力  false: 0V出力
        /// </summary>
        /// <param name="sw"></param>
        private static bool SetOutput(NAME name, bool sw)
        {
            try
            {
                switch (name)
                {
                    case NAME.P00:
                        return General.MainIo.SendDataTarget("WP00" + (sw ? "01" : "00"));
                    case NAME.P01:
                        return General.MainIo.SendDataTarget("WP01" + (sw ? "01" : "00"));
                    case NAME.P02:
                        return General.MainIo.SendDataTarget("WP02" + (sw ? "01" : "00"));
                    case NAME.P03:
                        return General.MainIo.SendDataTarget("WP03" + (sw ? "01" : "00"));
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 引数onName：H出力を指定したポート名  expAllOff: すべての出力ポートがL指定のときtrueを渡す
        /// </summary>
        /// <param name="exp"></param>
        private static void AnalysisData(NAME onName, bool expAllOff = false)
        {
            General.MainIo.SendData1768("R,P25");
            var M_P25 = General.MainIo.RecieveData;
            Sleep(100);
            General.MainIo.SendData1768("R,P26");
            var M_P26 = General.MainIo.RecieveData;
            Sleep(100);
            General.MainIo.SendData1768("R,P27");
            var M_P27 = General.MainIo.RecieveData;
            Sleep(100);
            General.MainIo.SendData1768("R,P28");
            var M_P28 = General.MainIo.RecieveData;


            var p00 = M_P25 == "L" ? true : false;
            var p01 = M_P26 == "L" ? true : false;
            var p02 = M_P27 == "L" ? true : false;
            var p03 = M_P28 == "L" ? true : false;

            ListSpecs.Find(l => l.name == NAME.P00).output = p00;
            ListSpecs.Find(l => l.name == NAME.P01).output = p01;
            ListSpecs.Find(l => l.name == NAME.P02).output = p02;
            ListSpecs.Find(l => l.name == NAME.P03).output = p03;

            //ビューモデルの更新
            //期待値の設定
            if (expAllOff)
            {
                State.VmTestResults.ColP00OutExp = General.OffBrush;
                State.VmTestResults.ColP01OutExp = General.OffBrush;
                State.VmTestResults.ColP02OutExp = General.OffBrush;
                State.VmTestResults.ColP03OutExp = General.OffBrush;
            }
            else
            {
                State.VmTestResults.ColP00OutExp = onName == NAME.P00 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColP01OutExp = onName == NAME.P01 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColP02OutExp = onName == NAME.P02 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColP03OutExp = onName == NAME.P03 ? General.OnBrush : General.OffBrush;

            }

            //取り込み値の設定
            State.VmTestResults.ColP00Out = p00 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColP01Out = p01 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColP02Out = p02 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColP03Out = p03 ? General.OnBrush : General.OffBrush;

        }


        public static async Task<bool> Check()
        {
            bool result = false;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    bool resultOn = false;
                    bool resultOff = false;
                    
                    InitList();//テストスペック毎回初期化
                    Flags.AddDecision = false;

                    try
                    {
                        ResetViewModel();

                        General.SetK8_9(false);
                        Sleep(150);
                        General.SetK4_5(true);
                        Sleep(150);

                        //P00～03を出力ポートに設定する
                        if (!General.MainIo.SendDataTarget("WP0*01")) return false;
                        Sleep(400);

                        return ListSpecs.All(L =>
                        {
                            resultOn = false;
                            resultOff = false;

                            if (Flags.ClickStopButton) return false;

                            //テストログの更新
                            State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " ONチェック";

                            //ONチェック
                            if (!SetOutput(L.name, true)) return false;
                            Sleep(150);
                            AnalysisData(L.name);

                            //ON指定したポートがH出力しているかの判定
                            var reH = ListSpecs.Find(l => l.name == L.name).output;
                            //ON指定したポート以外がL出力しているかの判定
                            var reL = ListSpecs.Where(l => l.output).Count() == 1;


                            resultOn = reH && reL;

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

                            Sleep(150);

                            //テストログの更新
                            State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " OFFチェック";

                            //OFFチェック
                            if (!SetOutput(L.name, false)) return false;
                            Sleep(150);
                            AnalysisData(L.name, true);

                            //ON指定したポート以外がL出力しているかの判定
                            resultOff = ListSpecs.All(l => !l.output);

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
                        });

                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        General.SetK4_5(false);
                        //P00～03を入力ポートに設定する
                        General.MainIo.SendDataTarget("WP0*00");
                    }
                });

            }
            finally
            {
                if (!result)
                {
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }

        }

    }
}
