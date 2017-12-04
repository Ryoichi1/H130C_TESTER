using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Threading.Thread;

namespace H130C_Tester
{
    public static class TestRelayOut
    {
        public enum NAME { RY1, RY2, RY3, RY4, RY5, RY6, RY7, RY8 }

        public static List<PortSpec> ListSpecs;

        public class PortSpec
        {
            public NAME name;
            public bool output;
            public double time;
            public bool result;
        }

        private static void ResetViewModelOut()
        {
            State.VmTestResults.ColRy1Exp = General.OffBrush;
            State.VmTestResults.ColRy2Exp = General.OffBrush;
            State.VmTestResults.ColRy3Exp = General.OffBrush;
            State.VmTestResults.ColRy4Exp = General.OffBrush;
            State.VmTestResults.ColRy5Exp = General.OffBrush;
            State.VmTestResults.ColRy6Exp = General.OffBrush;
            State.VmTestResults.ColRy7Exp = General.OffBrush;
            State.VmTestResults.ColRy8Exp = General.OffBrush;

            State.VmTestResults.ColRy1Out = General.OffBrush;
            State.VmTestResults.ColRy2Out = General.OffBrush;
            State.VmTestResults.ColRy3Out = General.OffBrush;
            State.VmTestResults.ColRy4Out = General.OffBrush;
            State.VmTestResults.ColRy5Out = General.OffBrush;
            State.VmTestResults.ColRy6Out = General.OffBrush;
            State.VmTestResults.ColRy7Out = General.OffBrush;
            State.VmTestResults.ColRy8Out = General.OffBrush;
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
                    case NAME.RY1:
                        return General.MainIo.SendDataTarget("WP10" + (sw ? "02" : "00"));
                    case NAME.RY2:
                        return General.MainIo.SendDataTarget("WP11" + (sw ? "02" : "00"));
                    case NAME.RY3:
                        return General.MainIo.SendDataTarget("WP12" + (sw ? "02" : "00"));
                    case NAME.RY4:
                        return General.MainIo.SendDataTarget("WP13" + (sw ? "02" : "00"));
                    case NAME.RY5:
                        return General.MainIo.SendDataTarget("WP14" + (sw ? "02" : "00"));
                    case NAME.RY6:
                        return General.MainIo.SendDataTarget("WP15" + (sw ? "02" : "00"));
                    case NAME.RY7:
                        return General.MainIo.SendDataTarget("WP16" + (sw ? "02" : "00"));
                    case NAME.RY8:
                        return General.MainIo.SendDataTarget("WP17" + (sw ? "02" : "00"));
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                Sleep(50);
            }
        }

        /// <summary>
        /// 引数onName：H出力を指定したポート名  expAllOff: すべての出力ポートがL指定のときtrueを渡す
        /// </summary>
        /// <param name="exp"></param>
        private static void AnalysisData(NAME onName, bool expAllOff = false)
        {
            General.SetK6_7(false);
            Sleep(50);
            General.MainIo.SendData1768("R,P5");
            var M_P5 = General.MainIo.RecieveData;
            General.MainIo.SendData1768("R,P6");
            var M_P6 = General.MainIo.RecieveData;
            General.MainIo.SendData1768("R,P7");
            var M_P7 = General.MainIo.RecieveData;
            General.MainIo.SendData1768("R,P8");
            var M_P8 = General.MainIo.RecieveData;


            var ry1 = M_P5 == "L" ? true : false;
            var ry2 = M_P6 == "L" ? true : false;
            var ry3 = M_P7 == "L" ? true : false;
            var ry4 = M_P8 == "L" ? true : false;

            General.SetK6_7(true);
            Sleep(50);
            General.MainIo.SendData1768("R,P5");
            M_P5 = General.MainIo.RecieveData;
            General.MainIo.SendData1768("R,P6");
            M_P6 = General.MainIo.RecieveData;
            General.MainIo.SendData1768("R,P7");
            M_P7 = General.MainIo.RecieveData;
            General.MainIo.SendData1768("R,P8");
            M_P8 = General.MainIo.RecieveData;


            var ry5 = M_P5 == "L" ? true : false;
            var ry6 = M_P6 == "L" ? true : false;
            var ry7 = M_P7 == "L" ? true : false;
            var ry8 = M_P8 == "L" ? true : false;

            ListSpecs.Find(l => l.name == NAME.RY1).output = ry1;
            ListSpecs.Find(l => l.name == NAME.RY2).output = ry2;
            ListSpecs.Find(l => l.name == NAME.RY3).output = ry3;
            ListSpecs.Find(l => l.name == NAME.RY4).output = ry4;
            ListSpecs.Find(l => l.name == NAME.RY5).output = ry5;
            ListSpecs.Find(l => l.name == NAME.RY6).output = ry6;
            ListSpecs.Find(l => l.name == NAME.RY7).output = ry7;
            ListSpecs.Find(l => l.name == NAME.RY8).output = ry8;

            //ビューモデルの更新
            //期待値の設定
            if (expAllOff)
            {
                State.VmTestResults.ColRy1Exp = General.OffBrush;
                State.VmTestResults.ColRy2Exp = General.OffBrush;
                State.VmTestResults.ColRy3Exp = General.OffBrush;
                State.VmTestResults.ColRy4Exp = General.OffBrush;
                State.VmTestResults.ColRy5Exp = General.OffBrush;
                State.VmTestResults.ColRy6Exp = General.OffBrush;
                State.VmTestResults.ColRy7Exp = General.OffBrush;
                State.VmTestResults.ColRy8Exp = General.OffBrush;
            }
            else
            {
                State.VmTestResults.ColRy1Exp = onName == NAME.RY1 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColRy2Exp = onName == NAME.RY2 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColRy3Exp = onName == NAME.RY3 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColRy4Exp = onName == NAME.RY4 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColRy5Exp = onName == NAME.RY5 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColRy6Exp = onName == NAME.RY6 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColRy7Exp = onName == NAME.RY7 ? General.OnBrush : General.OffBrush;
                State.VmTestResults.ColRy8Exp = onName == NAME.RY8 ? General.OnBrush : General.OffBrush;

            }

            //取り込み値の設定
            State.VmTestResults.ColRy1Out = ry1 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColRy2Out = ry2 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColRy3Out = ry3 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColRy4Out = ry4 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColRy5Out = ry5 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColRy6Out = ry6 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColRy7Out = ry7 ? General.OnBrush : General.OffBrush;
            State.VmTestResults.ColRy8Out = ry8 ? General.OnBrush : General.OffBrush;

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
                        ResetViewModelOut();

                        General.SetRl2(false);
                        Sleep(150);

                        return ListSpecs.All(L =>
                        {
                            resultOn = false;
                            resultOff = false;

                            if (Flags.ClickStopButton) return false;

                            //テストログの更新
                            State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " ONチェック";

                            //ONチェック
                            if (!SetOutput(L.name, true)) return false;
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


                            //テストログの更新
                            State.VmTestStatus.TestLog += "\r\n" + L.name.ToString() + " OFFチェック";

                            //OFFチェック
                            if (!SetOutput(L.name, false)) return false;
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
                        General.MainIo.SendDataTarget("C");
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


        ///////////////////////////////////////////////////////////////////

        private static void ResetViewModelTime()
        {
            State.VmTestResults.Ry1OnTime = "";
            State.VmTestResults.Ry2OnTime = "";
            State.VmTestResults.Ry3OnTime = "";
            State.VmTestResults.Ry4OnTime = "";
            State.VmTestResults.Ry5OnTime = "";
            State.VmTestResults.Ry6OnTime = "";
            State.VmTestResults.Ry7OnTime = "";
            State.VmTestResults.Ry8OnTime = "";

            State.VmTestResults.ColRy1OnTime = General.OffBrush;
            State.VmTestResults.ColRy2OnTime = General.OffBrush;
            State.VmTestResults.ColRy3OnTime = General.OffBrush;
            State.VmTestResults.ColRy4OnTime = General.OffBrush;
            State.VmTestResults.ColRy5OnTime = General.OffBrush;
            State.VmTestResults.ColRy6OnTime = General.OffBrush;
            State.VmTestResults.ColRy7OnTime = General.OffBrush;
            State.VmTestResults.ColRy8OnTime = General.OffBrush;

        }

        public static async Task<bool> CheckPulseOut()
        {
            bool result = false;
            var max = State.TestSpec.RelayOnTimeMilliSecMax;
            var min = State.TestSpec.RelayOnTimeMilliSecMin;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    try
                    {
                        InitList();
                        ResetViewModelTime();
                        General.SetRl2(true);
                        Sleep(150);

                        ListSpecs.ForEach(l =>
                        {
                            switch (l.name)
                            {
                                case NAME.RY1:
                                    General.SetK6_7(false);
                                    Sleep(100);
                                    General.MainIo.SendData1768("MeasRy1");
                                    break;
                                case NAME.RY2:
                                    General.MainIo.SendData1768("MeasRy2");
                                    break;
                                case NAME.RY3:
                                    General.MainIo.SendData1768("MeasRy3");
                                    break;
                                case NAME.RY4:
                                    General.MainIo.SendData1768("MeasRy4");
                                    break;
                                case NAME.RY5:
                                    General.SetK6_7(true);
                                    Sleep(100);
                                    General.MainIo.SendData1768("MeasRy5");
                                    break;
                                case NAME.RY6:
                                    General.MainIo.SendData1768("MeasRy6");
                                    break;
                                case NAME.RY7:
                                    General.MainIo.SendData1768("MeasRy7");
                                    break;
                                case NAME.RY8:
                                    General.MainIo.SendData1768("MeasRy8");
                                    break;
                            }
                            Sleep(400);
                            General.MainIo.SendData1768("meas?");
                            var re = General.MainIo.RecieveData;
                            l.time = GetMicroTime(re);
                            l.result = l.time >= min && l.time <= max;

                            //ビューモデルの更新
                            switch (l.name)
                            {
                                case NAME.RY1:
                                    State.VmTestResults.Ry1OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy1OnTime = General.NgBrush;
                                    break;
                                case NAME.RY2:
                                    State.VmTestResults.Ry2OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy2OnTime = General.NgBrush;
                                    break;
                                case NAME.RY3:
                                    State.VmTestResults.Ry3OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy3OnTime = General.NgBrush;
                                    break;
                                case NAME.RY4:
                                    State.VmTestResults.Ry4OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy4OnTime = General.NgBrush;
                                    break;
                                case NAME.RY5:
                                    State.VmTestResults.Ry5OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy5OnTime = General.NgBrush;
                                    break;
                                case NAME.RY6:
                                    State.VmTestResults.Ry6OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy6OnTime = General.NgBrush;
                                    break;
                                case NAME.RY7:
                                    State.VmTestResults.Ry7OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy7OnTime = General.NgBrush;
                                    break;
                                case NAME.RY8:
                                    State.VmTestResults.Ry8OnTime = l.time.ToString("F1") + "mS";
                                    if (!l.result)
                                        State.VmTestResults.ColRy8OnTime = General.NgBrush;
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
                General.SetK6_7(false);
                if (!result)
                {
                    State.VmTestStatus.Spec = $"規格値 : {min}mS ～ {max}mS";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }
        }

        private static double GetMicroTime(string data)
        {
            try
            {
                var list = data.Split(',').ToList();//カンマ区切りなので、リスト化する
                var indexFall = list.FindIndex(l => l == "L");//立ち下がりを検出
                list = list.Skip(indexFall).ToList();//立ち下がり前のデータを削除
                var indexRise = list.FindIndex(l => l == "H");//立ち上がりを検出
                list = list.Take(indexRise).ToList();//立ち上がり後のデータを削除
                var Ontime = list.Count() * 10.0;//mbedが10uSec毎に取り込んでいる
                return Ontime;
            }
            catch
            {
                return 0.0;
            }

        }

    }
}
