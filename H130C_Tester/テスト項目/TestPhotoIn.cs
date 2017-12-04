using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace H130C_Tester
{
    public static class TestPhotoIn
    {
        public enum NAME
        {
            PH1_1, PH1_2, PH1_3, PH1_4,
            PH2_1, PH2_2, PH2_3, PH2_4,
        }

        public static List<Spec> ListSpecs;

        public class Spec
        {
            public NAME name;
            public bool input;
            public double time;
            public bool result;
        }

        private static void ResetViewModelTime()
        {
            State.VmTestResults.Ph1_1InTime = "";
            State.VmTestResults.Ph1_2InTime = "";
            State.VmTestResults.Ph1_3InTime = "";
            State.VmTestResults.Ph1_4InTime = "";
            State.VmTestResults.Ph2_1InTime = "";
            State.VmTestResults.Ph2_2InTime = "";
            State.VmTestResults.Ph2_3InTime = "";
            State.VmTestResults.Ph2_4InTime = "";

            State.VmTestResults.ColPh1_1InTime = General.OffBrush;
            State.VmTestResults.ColPh1_2InTime = General.OffBrush;
            State.VmTestResults.ColPh1_3InTime = General.OffBrush;
            State.VmTestResults.ColPh1_4InTime = General.OffBrush;
            State.VmTestResults.ColPh2_1InTime = General.OffBrush;
            State.VmTestResults.ColPh2_2InTime = General.OffBrush;
            State.VmTestResults.ColPh2_3InTime = General.OffBrush;
            State.VmTestResults.ColPh2_4InTime = General.OffBrush;
        }

        private static void InitList()
        {
            ListSpecs = new List<Spec>();
            foreach (var n in Enum.GetValues(typeof(NAME)))
            {
                ListSpecs.Add(new Spec { name = (NAME)n });
            }
        }

        /// <summary>
        /// 引数true：開放  false:L（GND）
        /// </summary>
        /// <param name="sw"></param>
        private static void SetPulseInput(bool sw)
        {
            //PH1側への入力
            General.MainIo.SendDataTarget("P,P21," + (sw ? "0.5" : "0.0"));
            General.MainIo.SendDataTarget("P,P22," + (sw ? "0.5" : "0.0"));
            General.MainIo.SendDataTarget("P,P23," + (sw ? "0.5" : "0.0"));
            General.MainIo.SendDataTarget("P,P24," + (sw ? "0.5" : "0.0"));

            //PH2側への入力
            General.SubIo.SendDataTarget("P,P21," + (sw ? "0.5" : "0.0"));
            General.SubIo.SendDataTarget("P,P22," + (sw ? "0.5" : "0.0"));
            General.SubIo.SendDataTarget("P,P23," + (sw ? "0.5" : "0.0"));
            General.SubIo.SendDataTarget("P,P24," + (sw ? "0.5" : "0.0"));

        }

        public static async Task<bool> CheckInputPulse()
        {
            bool result = false;
            double max = State.TestSpec.PhotoInTimeMilliSecMax;
            double min = State.TestSpec.PhotoInTimeMilliSecMin;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    InitList();//テストスペック毎回初期化

                    try
                    {
                        ResetViewModelTime();

                        SetPulseInput(true);
                        Sleep(1500);

                        ListSpecs.ForEach(l =>
                        {
                            var re = false;
                            switch (l.name)
                            {
                                case NAME.PH1_1:
                                    re = General.MainIo.SendDataTarget("RP2001") && General.MainIo.RecieveData != "NG";
                                    break;
                                case NAME.PH1_2:
                                    re = General.MainIo.SendDataTarget("RP2101") && General.MainIo.RecieveData != "NG";
                                    break;
                                case NAME.PH1_3:
                                    re = General.MainIo.SendDataTarget("RP2201") && General.MainIo.RecieveData != "NG";
                                    break;
                                case NAME.PH1_4:
                                    re = General.MainIo.SendDataTarget("RP2301") && General.MainIo.RecieveData != "NG";
                                    break;
                                case NAME.PH2_1:
                                    re = General.MainIo.SendDataTarget("RP2401") && General.MainIo.RecieveData != "NG";
                                    break;
                                case NAME.PH2_2:
                                    re = General.MainIo.SendDataTarget("RP2501") && General.MainIo.RecieveData != "NG";
                                    break;
                                case NAME.PH2_3:
                                    re = General.MainIo.SendDataTarget("RP2601") && General.MainIo.RecieveData != "NG";
                                    break;
                                case NAME.PH2_4:
                                    re = General.MainIo.SendDataTarget("RP2701") && General.MainIo.RecieveData != "NG";
                                    break;
                            }

                            if (re)
                            {
                                l.time = double.Parse(General.MainIo.RecieveData) * 1000.0;
                                l.result = l.time >= min && l.time <= max;
                            }
                            else
                            {
                                l.time = 0;
                                l.result = false;
                            }

                            //ビューモデルの更新
                            switch (l.name)
                            {
                                case NAME.PH1_1:
                                    State.VmTestResults.Ph1_1InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh1_1InTime = General.NgBrush;
                                    break;
                                case NAME.PH1_2:
                                    State.VmTestResults.Ph1_2InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh1_2InTime = General.NgBrush;
                                    break;
                                case NAME.PH1_3:
                                    State.VmTestResults.Ph1_3InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh1_3InTime = General.NgBrush;
                                    break;
                                case NAME.PH1_4:
                                    State.VmTestResults.Ph1_4InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh1_4InTime = General.NgBrush;
                                    break;
                                case NAME.PH2_1:
                                    State.VmTestResults.Ph2_1InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh2_1InTime = General.NgBrush;
                                    break;
                                case NAME.PH2_2:
                                    State.VmTestResults.Ph2_2InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh2_2InTime = General.NgBrush;
                                    break;
                                case NAME.PH2_3:
                                    State.VmTestResults.Ph2_3InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh2_3InTime = General.NgBrush;
                                    break;
                                case NAME.PH2_4:
                                    State.VmTestResults.Ph2_4InTime = $"{l.time.ToString("F1")}mS";
                                    if (!l.result) State.VmTestResults.ColPh2_4InTime = General.NgBrush;
                                    break;
                            }

                            Sleep(400);
                        });

                        return ListSpecs.All(l => l.result);
                    }
                    catch
                    {
                        return false;
                    }
                });

            }
            finally
            {
                SetPulseInput(false);
                if (!result)
                {
                    State.VmTestStatus.Spec = $"規格値 : {min}mS ～ {max}mS";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }

        }



        /////////////////////////////////////////////////////////////////

        private static void ResetViewModelIn()
        {

            State.VmTestResults.ColPh1_1Exp = General.OffBrush;
            State.VmTestResults.ColPh1_2Exp = General.OffBrush;
            State.VmTestResults.ColPh1_3Exp = General.OffBrush;
            State.VmTestResults.ColPh1_4Exp = General.OffBrush;
            State.VmTestResults.ColPh2_1Exp = General.OffBrush;
            State.VmTestResults.ColPh2_2Exp = General.OffBrush;
            State.VmTestResults.ColPh2_3Exp = General.OffBrush;
            State.VmTestResults.ColPh2_4Exp = General.OffBrush;

            State.VmTestResults.ColPh1_1In = General.OffBrush;
            State.VmTestResults.ColPh1_2In = General.OffBrush;
            State.VmTestResults.ColPh1_3In = General.OffBrush;
            State.VmTestResults.ColPh1_4In = General.OffBrush;
            State.VmTestResults.ColPh2_1In = General.OffBrush;
            State.VmTestResults.ColPh2_2In = General.OffBrush;
            State.VmTestResults.ColPh2_3In = General.OffBrush;
            State.VmTestResults.ColPh2_4In = General.OffBrush;
        }


        /// <summary>
        /// 引数true：開放  false:L（GND）
        /// </summary>
        /// <param name="sw"></param>
        private static bool SetInput(NAME onName, bool sw)
        {
            try
            {
                switch (onName)
                {
                    case NAME.PH1_1:
                        General.SubIo.SendData1768("W,P14," + (sw ? "1" : "0"));
                        break;
                    case NAME.PH1_2:
                        General.SubIo.SendData1768("W,P15," + (sw ? "1" : "0"));
                        break;
                    case NAME.PH1_3:
                        General.SubIo.SendData1768("W,P16," + (sw ? "1" : "0"));
                        break;
                    case NAME.PH1_4:
                        General.SubIo.SendData1768("W,P17," + (sw ? "1" : "0"));
                        break;
                    case NAME.PH2_1:
                        General.SubIo.SendData1768("W,P18," + (sw ? "1" : "0"));
                        break;
                    case NAME.PH2_2:
                        General.SubIo.SendData1768("W,P19," + (sw ? "1" : "0"));
                        break;
                    case NAME.PH2_3:
                        General.SubIo.SendData1768("W,P20," + (sw ? "1" : "0"));
                        break;
                    case NAME.PH2_4:
                        General.SubIo.SendData1768("W,P25," + (sw ? "1" : "0"));
                        break;
                    default:
                        break;
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
        private static void AnalysisData(NAME onName, bool expAllOff = false)
        {
            General.MainIo.SendDataTarget("RP2*00");
            var hexData = General.MainIo.RecieveData;

            //ASCII エンコード 文字列をバイトに変換する
            var StateP2 = Convert.ToInt16(hexData, 16);

            //P2の状態を解析 認識値がHならtrueを代入する
            var p20 = (StateP2 & 0b0000_0001) == 0b0000_0000;
            var p21 = (StateP2 & 0b0000_0010) == 0b0000_0000;
            var p22 = (StateP2 & 0b0000_0100) == 0b0000_0000;
            var p23 = (StateP2 & 0b0000_1000) == 0b0000_0000;
            var p24 = (StateP2 & 0b0001_0000) == 0b0000_0000;
            var p25 = (StateP2 & 0b0010_0000) == 0b0000_0000;
            var p26 = (StateP2 & 0b0100_0000) == 0b0000_0000;
            var p27 = (StateP2 & 0b1000_0000) == 0b0000_0000;

            ListSpecs.Find(l => l.name == NAME.PH1_1).input = p20;
            ListSpecs.Find(l => l.name == NAME.PH1_2).input = p21;
            ListSpecs.Find(l => l.name == NAME.PH1_3).input = p22;
            ListSpecs.Find(l => l.name == NAME.PH1_4).input = p23;
            ListSpecs.Find(l => l.name == NAME.PH2_1).input = p24;
            ListSpecs.Find(l => l.name == NAME.PH2_2).input = p25;
            ListSpecs.Find(l => l.name == NAME.PH2_3).input = p26;
            ListSpecs.Find(l => l.name == NAME.PH2_4).input = p27;

            //ビューモデルの更新
            //期待値の設定
            if (expAllOff)
            {
                State.VmTestResults.ColPh1_1Exp = General.OffBrush;
                State.VmTestResults.ColPh1_2Exp = General.OffBrush;
                State.VmTestResults.ColPh1_3Exp = General.OffBrush;
                State.VmTestResults.ColPh1_4Exp = General.OffBrush;
                State.VmTestResults.ColPh2_1Exp = General.OffBrush;
                State.VmTestResults.ColPh2_2Exp = General.OffBrush;
                State.VmTestResults.ColPh2_3Exp = General.OffBrush;
                State.VmTestResults.ColPh2_4Exp = General.OffBrush;
            }
            else
            {
                State.VmTestResults.ColPh1_1Exp = onName == NAME.PH1_1 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColPh1_2Exp = onName == NAME.PH1_2 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColPh1_3Exp = onName == NAME.PH1_3 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColPh1_4Exp = onName == NAME.PH1_4 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColPh2_1Exp = onName == NAME.PH2_1 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColPh2_2Exp = onName == NAME.PH2_2 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColPh2_3Exp = onName == NAME.PH2_3 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColPh2_4Exp = onName == NAME.PH2_4 ? General.OnBrush : General.OffBrush;
            }

            //取り込み値の設定
            State.VmTestResults.ColPh1_1In = ListSpecs.Find(l => l.name == NAME.PH1_1).input ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColPh1_2In = ListSpecs.Find(l => l.name == NAME.PH1_2).input ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColPh1_3In = ListSpecs.Find(l => l.name == NAME.PH1_3).input ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColPh1_4In = ListSpecs.Find(l => l.name == NAME.PH1_4).input ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColPh2_1In = ListSpecs.Find(l => l.name == NAME.PH2_1).input ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColPh2_2In = ListSpecs.Find(l => l.name == NAME.PH2_2).input ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColPh2_3In = ListSpecs.Find(l => l.name == NAME.PH2_3).input ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColPh2_4In = ListSpecs.Find(l => l.name == NAME.PH2_4).input ? General.OnBrush : General.OffBrush;
        }


        public static async Task<bool> CheckInput()
        {
            bool result = false;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    bool resultOn = false;
                    bool resultOff = false;
                    InitList();//テストスペック毎回初期化
                    Flags.AddDecision = false;

                    try
                    {
                        resultOn = false;
                        resultOff = false;
                        ResetViewModelIn();

                        return ListSpecs.All(L =>
                        {

                            //接点入力ON(製品がL認識することの確認) 検査
                            //テストログの更新
                            State.VmTestStatus.TestLog += $"\r\n{L.name} ON確認";
                            SetInput(L.name, true);
                            Sleep(200);
                            AnalysisData(L.name);

                            var reH = ListSpecs.Find(l => l.name == L.name).input;
                            var reL = ListSpecs.Where(l => l.input).Count() == 1;
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

                            //接点入力OFF(製品がH認識することの確認) 検査
                            //テストログの更新
                            State.VmTestStatus.TestLog += $"\r\n{L.name} OFF確認";
                            SetInput(L.name, false);
                            Sleep(200);
                            AnalysisData(L.name, true);

                            resultOff = ListSpecs.All(l => !l.result);
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

                            Sleep(150);

                        });

                    }
                    catch
                    {
                        return false;
                    }
                });

            }
            finally
            {
                ListSpecs.ForEach(l => SetInput(l.name, false));

                if (!result)
                {
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }

        }




    }
}
