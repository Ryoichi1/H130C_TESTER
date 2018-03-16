using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static System.Threading.Thread;
using OpenCvSharp;

namespace H130C_Tester
{
    public static class TestConnector
    {
        public enum NAME1 { CN221, CN222, CN227 }//スイッチプローブで逆検知
        public enum NAME2 { CN220, CN223, CN224, CN225, CN226, JP1 }//画像検査で逆検知

        public class CnSpec1
        {
            public NAME1 name;
            public bool result;
        }

        public class CnSpec2
        {
            public NAME2 name;
            public double 角度;
            public double 一致率;
            public bool result;
        }

        public static List<CnSpec1> ListCnSpecs1;
        public static List<CnSpec2> ListCnSpecs2;// CameraConfクラスからも使用します（カメラ調整のため）

        private static void InitList()
        {
            ListCnSpecs1 = new List<CnSpec1>();
            foreach (var n in Enum.GetValues(typeof(NAME1)))
            {
                ListCnSpecs1.Add(new CnSpec1 { name = (NAME1)n });
            }

            ListCnSpecs2 = new List<CnSpec2>();
            foreach (var n in Enum.GetValues(typeof(NAME2)))
            {
                ListCnSpecs2.Add(new CnSpec2 { name = (NAME2)n });
            }
        }

        public static async Task<bool> CheckCn()
        {
            var result = false;
            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    InitList();
                    CheckBySwProbe();
                    CheckCnByCamera();
                    return ListCnSpecs1.All(l => l.result) && ListCnSpecs2.All(l => l.result);
                });
            }
            finally
            {
                if (!result)
                {
                    State.uriOtherInfoPage = new Uri("Page/ErrInfo/ErrInfoコネクタチェック.xaml", UriKind.Relative);
                    State.VmTestStatus.EnableButtonErrInfo = System.Windows.Visibility.Visible;
                }
            }
        }

        private static void CheckBySwProbe()
        {
            try
            {
                var command = "";
                ListCnSpecs1.ForEach(l =>
                {
                    switch (l.name)
                    {
                        case NAME1.CN221:
                            command = "R,P09";
                            break;
                        case NAME1.CN222:
                            command = "R,P10";
                            break;
                        case NAME1.CN227:
                            command = "R,P11";
                            break;
                    }

                    //正常に実装されていればスイッチプローブがOnするので"L"が返ってきます
                    if (General.MainIo.SendData1768(command))
                    {
                        l.result = General.MainIo.RecieveData == "L" ? true : false;
                    }
                    else
                    {
                        l.result = false;
                    }
                });
            }
            catch
            {
                Flags.ThrowException = true;
            }
        }

        /// <summary>
        /// CameraConfクラスからも使用します（カメラ調整のため）
        /// </summary>
        public static void CheckCnByCamera()
        {
            IplImage src = null, tmp = null;

            try
            {
                int x = 0, y = 0, w = 0, h = 0, m = 0;

                ListCnSpecs2.ForEach(l =>
                {
                    switch (l.name)
                    {
                        case NAME2.CN220:
                            x = State.CamPropCn220.X;
                            y = State.CamPropCn220.Y;
                            w = State.CamPropCn220.W;
                            h = State.CamPropCn220.H;
                            m = State.CamPropCn220.Margin;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn220);
                            State.SetCamPropForCn(State.CN_NAME.CN220);
                            SetLight(NAME2.CN220);
                            break;
                        case NAME2.CN223:
                            x = State.CamPropCn223.X;
                            y = State.CamPropCn223.Y;
                            w = State.CamPropCn223.W;
                            h = State.CamPropCn223.H;
                            m = State.CamPropCn223.Margin;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn223);
                            State.SetCamPropForCn(State.CN_NAME.CN223);
                            SetLight(NAME2.CN223);
                            break;
                        case NAME2.CN224:
                            x = State.CamPropCn224.X;
                            y = State.CamPropCn224.Y;
                            w = State.CamPropCn224.W;
                            h = State.CamPropCn224.H;
                            m = State.CamPropCn224.Margin;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn224);
                            State.SetCamPropForCn(State.CN_NAME.CN224);
                            SetLight(NAME2.CN224);
                            break;
                        case NAME2.CN225:
                            x = State.CamPropCn225.X;
                            y = State.CamPropCn225.Y;
                            w = State.CamPropCn225.W;
                            h = State.CamPropCn225.H;
                            m = State.CamPropCn225.Margin;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn225);
                            State.SetCamPropForCn(State.CN_NAME.CN225);
                            SetLight(NAME2.CN225);
                            break;
                        case NAME2.CN226:
                            x = State.CamPropCn226.X;
                            y = State.CamPropCn226.Y;
                            w = State.CamPropCn226.W;
                            h = State.CamPropCn226.H;
                            m = State.CamPropCn226.Margin;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn226);
                            State.SetCamPropForCn(State.CN_NAME.CN226);
                            SetLight(NAME2.CN226);
                            break;
                        case NAME2.JP1:
                            x = State.CamPropJp1.X;
                            y = State.CamPropJp1.Y;
                            w = State.CamPropJp1.W;
                            h = State.CamPropJp1.H;
                            m = State.CamPropJp1.Margin;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateJp1);
                            State.SetCamPropForCn(State.CN_NAME.JP1);
                            SetLight(NAME2.JP1);
                            break;

                    }

                    Sleep(600);
                    //画像検査なので照明点灯

                    //cam1の画像を取得する処理
                    General.cam.FlagTestPic = true;
                    while (General.cam.FlagTestPic) ;
                    src = General.cam.imageForTest.Clone();
                    //
                    x -= m;
                    y -= m;
                    w += 2 * m;
                    h += 2 * m;

                    var _livePic = General.trimming(src, x, y, w, h);

                    double max;
                    double min;

                    var Angles = MakeTheta().ToList();
                    //ローカル関数
                    IEnumerable<double> MakeTheta()
                    {
                        var Val = -20.0;
                        while (true)
                        {
                            yield return Val;
                            Val += 0.1;
                            if (Val > 20.0) yield break;
                        }
                    }


                    var AllData = new List<(double 角度, double 一致率, CvPoint 座標)>();

                    //Angles.ForEach(t =>
                    Angles.AsParallel().ForAll(t =>
                    {
                        var livePic = _livePic.Clone();
                        //傾き補正
                        CvPoint2D32f center = new CvPoint2D32f(w / 2, h / 2);
                        CvMat affineMatrix = Cv.GetRotationMatrix2D(center, t, 1.0);
                        Cv.WarpAffine(livePic, livePic, affineMatrix);

                        CvMat result = new CvMat(h - tmp.Height + 1, w - tmp.Width + 1, MatrixType.F32C1);
                        Cv.MatchTemplate(livePic, tmp, result, MatchTemplateMethod.CCoeffNormed);

                        Cv.MinMaxLoc(result, out min, out max);

                        CvPoint minPoint = new CvPoint();
                        CvPoint maxPoint = new CvPoint();
                        Cv.MinMaxLoc(result, out minPoint, out maxPoint);

                        AllData.Add((t, max, maxPoint));
                    });


                    //////////
                    var re = AllData.OrderByDescending(d => d.一致率).ToList()[0];//相関係数の一番高いものを抽出する
                    l.一致率 = re.一致率;
                    l.角度 = re.角度;
                    l.result = re.一致率 >= State.TestSpec.MatchMin;
                    //////////
                });
            }
            catch
            {
                Flags.ThrowException = true;
            }
            finally
            {
                General.cam.ResetFlag();//これ忘れると無限ループにハマる
                General.SetLight1(false);
                General.SetLight2(false);
                General.SetLight3(false);
                src.Dispose();
                tmp.Dispose();

            }
        }

        private static void SetLight(NAME2 name)
        {
            switch (name)
            {
                case NAME2.CN220:
                    General.SetLight1(State.CamPropCn220.Light1On);
                    General.SetLight2(State.CamPropCn220.Light2On);
                    General.SetLight3(State.CamPropCn220.Light3On);
                    break;
                case NAME2.CN223:
                    General.SetLight1(State.CamPropCn223.Light1On);
                    General.SetLight2(State.CamPropCn223.Light2On);
                    General.SetLight3(State.CamPropCn223.Light3On);
                    break;
                case NAME2.CN224:
                    General.SetLight1(State.CamPropCn224.Light1On);
                    General.SetLight2(State.CamPropCn224.Light2On);
                    General.SetLight3(State.CamPropCn224.Light3On);
                    break;
                case NAME2.CN225:
                    General.SetLight1(State.CamPropCn225.Light1On);
                    General.SetLight2(State.CamPropCn225.Light2On);
                    General.SetLight3(State.CamPropCn225.Light3On);
                    break;
                case NAME2.CN226:
                    General.SetLight1(State.CamPropCn226.Light1On);
                    General.SetLight2(State.CamPropCn226.Light2On);
                    General.SetLight3(State.CamPropCn226.Light3On);
                    break;
                case NAME2.JP1:
                    General.SetLight1(State.CamPropJp1.Light1On);
                    General.SetLight2(State.CamPropJp1.Light2On);
                    General.SetLight3(State.CamPropJp1.Light3On);
                    break;
            }

        }

    }
}
