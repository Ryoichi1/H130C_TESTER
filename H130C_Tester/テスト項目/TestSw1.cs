using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace H130C_Tester
{
    public static class TestSw1
    {
        public enum NAME_SW1 { SW1_1, SW1_2, SW1_3, SW1_4 }

        public static List<SW1Spec> ListSw1Specs;

        public class SW1Spec
        {
            public NAME_SW1 name;
            public bool inPut;
            public bool Exp;
        }


        private static void ResetViewModel()
        {

            State.VmTestResults.ColSw1_1Exp = General.OffBrush;
            State.VmTestResults.ColSw1_2Exp = General.OffBrush;
            State.VmTestResults.ColSw1_3Exp = General.OffBrush;
            State.VmTestResults.ColSw1_4Exp = General.OffBrush;

            State.VmTestResults.ColSw1_1 = General.OffBrush;
            State.VmTestResults.ColSw1_2 = General.OffBrush;
            State.VmTestResults.ColSw1_3 = General.OffBrush;
            State.VmTestResults.ColSw1_4 = General.OffBrush;
        }

        private static void InitListS1()
        {
            ListSw1Specs = new List<SW1Spec>();
            foreach (var n in Enum.GetValues(typeof(NAME_SW1)))
            {
                ListSw1Specs.Add(new SW1Spec { name = (NAME_SW1)n });
            }
        }


        private enum SW1EXP { ALL_OFF, ALL_ON, }

        private static void AnalysisDataS1(string data, SW1EXP exp)
        {
            //ASCII エンコード 文字列をバイト配列に変換する
            var StateSw1 = Convert.ToInt16(data, 16);

            //DIPスイッチの状態を解析
            var sw1_4 = (StateSw1 & 0b0000_0001) == 0b0000_0000;
            var sw1_3 = (StateSw1 & 0b0000_0010) == 0b0000_0000;
            var sw1_2 = (StateSw1 & 0b0000_0100) == 0b0000_0000;
            var sw1_1 = (StateSw1 & 0b0000_1000) == 0b0000_0000;

            //ビューモデルの更新
            //期待値の設定
            switch (exp)
            {
                case SW1EXP.ALL_OFF:
                    State.VmTestResults.ColSw1_1Exp = General.OffBrush;
                    State.VmTestResults.ColSw1_2Exp = General.OffBrush;
                    State.VmTestResults.ColSw1_3Exp = General.OffBrush;
                    State.VmTestResults.ColSw1_4Exp = General.OffBrush;
                    break;

                case SW1EXP.ALL_ON:
                    State.VmTestResults.ColSw1_1Exp = General.OnBrush;
                    State.VmTestResults.ColSw1_2Exp = General.OnBrush;
                    State.VmTestResults.ColSw1_3Exp = General.OnBrush;
                    State.VmTestResults.ColSw1_4Exp = General.OnBrush;
                    break;

            }

            //取り込み値の設定
            State.VmTestResults.ColSw1_1 = sw1_1 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColSw1_2 = sw1_2 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColSw1_3 = sw1_3 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColSw1_4 = sw1_4 ? General.OnBrush : General.OffBrush;

            //スペックリストの更新
            ListSw1Specs.FirstOrDefault(L => L.name == NAME_SW1.SW1_1).inPut = sw1_1;
            ListSw1Specs.FirstOrDefault(L => L.name == NAME_SW1.SW1_2).inPut = sw1_2;
            ListSw1Specs.FirstOrDefault(L => L.name == NAME_SW1.SW1_3).inPut = sw1_3;
            ListSw1Specs.FirstOrDefault(L => L.name == NAME_SW1.SW1_4).inPut = sw1_4;
        }


        public static async Task<bool> Check()
        {
            bool resultOn = false;
            bool resultOff = false;

            try
            {
                return await Task<bool>.Run(() =>
                {
                    Flags.AddDecision = false;

                    try
                    {
                        resultOn = false;
                        resultOff = false;
                        ResetViewModel();

                        //すべてONの検査
                        //テストログの更新
                        State.VmTestStatus.TestLog += "\r\nALL SW1 ALL_ON";

                        General.PlaySound(General.soundNotice);
                        State.VmTestStatus.Message = "プレスを開けて、S1をすべてON ⇧ してください";
                        InitListS1();//テストスペック毎回初期化

                        while (true)
                        {
                            if (Flags.ClickStopButton) return false;
                            if (!General.CheckPress())
                                break;
                            Sleep(250);
                        }

                        while (true)
                        {
                            if (Flags.ClickStopButton) return false;
                            if (General.CheckPress())
                                break;
                            Sleep(250);
                        }

                        General.PowSupply(true);
                        Sleep(1000);
                        if (!General.MainIo.SendDataTarget("RSW100"))
                            return false;
                        var reOn = General.MainIo.RecieveData;

                        AnalysisDataS1(reOn, SW1EXP.ALL_ON);
                        resultOn = ListSw1Specs.All(l => l.inPut);
                        if (resultOn)
                        {
                            State.VmTestStatus.TestLog += "---PASS\r\n";
                        }
                        else
                        {
                            State.VmTestStatus.TestLog += "---FAIL\r\n";
                            return false;
                        }

                        //すべてOFFの検査
                        //テストログの更新
                        General.PowSupply(false);
                        State.VmTestStatus.TestLog += "ALL SW1 ALL_OFF";

                        General.PlaySound(General.soundNotice);
                        State.VmTestStatus.Message = "プレスを開けて、S1をすべてOFF ⇩ してください";
                        InitListS1();//テストスペック毎回初期化

                        while (true)
                        {
                            if (Flags.ClickStopButton) return false;
                            if (!General.CheckPress())
                                break;
                            Sleep(250);
                        }

                        while (true)
                        {
                            if (Flags.ClickStopButton) return false;
                            if (General.CheckPress())
                                break;
                            Sleep(250);
                        }


                        General.PowSupply(true);
                        Sleep(1000);
                        if (!General.MainIo.SendDataTarget("RSW100"))
                            return false;
                        var reOff = General.MainIo.RecieveData;

                        AnalysisDataS1(reOff, SW1EXP.ALL_OFF);
                        resultOff = ListSw1Specs.All(l => !l.inPut);
                        if (resultOff)
                        {
                            State.VmTestStatus.TestLog += "---PASS\r\n";
                            return true;
                        }
                        else
                        {
                            State.VmTestStatus.TestLog += "---FAIL\r\n";
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
                State.VmTestStatus.Message = Constants.MessWait;
                Sleep(200);

                if (!resultOn || !resultOff)
                {
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }

        }

    }
}
