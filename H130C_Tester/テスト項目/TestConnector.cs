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
                State.SetCamPropForCn();
                Sleep(500);
                //画像検査なので照明点灯
                General.SetLight(true);
                Sleep(1000);

                //cam1の画像を取得する処理
                General.cam.FlagTestPic = true;
                while (General.cam.FlagTestPic) ;
                src = General.cam.imageForTest.Clone();

                int x = 0, y = 0, w = 0, h = 0;
                int marjin = State.CnProp.Margin;

                ListCnSpecs2.ForEach(l =>
                {
                    switch (l.name)
                    {
                        case NAME2.CN220:
                            x = State.CnProp.X_Cn220;
                            y = State.CnProp.Y_Cn220;
                            w = State.CnProp.W_Cn220;
                            h = State.CnProp.H_Cn220;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn220);
                            break;
                        case NAME2.CN223:
                            x = State.CnProp.X_Cn223;
                            y = State.CnProp.Y_Cn223;
                            w = State.CnProp.W_Cn223;
                            h = State.CnProp.H_Cn223;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn223);
                            break;
                        case NAME2.CN224:
                            x = State.CnProp.X_Cn224;
                            y = State.CnProp.Y_Cn224;
                            w = State.CnProp.W_Cn224;
                            h = State.CnProp.H_Cn224;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn224);
                            break;
                        case NAME2.CN225:
                            x = State.CnProp.X_Cn225;
                            y = State.CnProp.Y_Cn225;
                            w = State.CnProp.W_Cn225;
                            h = State.CnProp.H_Cn225;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn225);
                            break;
                        case NAME2.CN226:
                            x = State.CnProp.X_Cn226;
                            y = State.CnProp.Y_Cn226;
                            w = State.CnProp.W_Cn226;
                            h = State.CnProp.H_Cn226;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateCn226);
                            break;
                        case NAME2.JP1:
                            x = State.CnProp.X_Jp1;
                            y = State.CnProp.Y_Jp1;
                            w = State.CnProp.W_Jp1;
                            h = State.CnProp.H_Jp1;
                            tmp = Cv.LoadImage(Constants.filePath_TemplateJp1);
                            break;
                    }

                    //
                    x -= marjin;
                    y -= marjin;
                    w += 2 * marjin;
                    h += 2 * marjin;

                    var _livePic = General.trimming(src, x, y, w, h);

                    double max;
                    double min;

                    var Angles = MakeTheta().ToList();
                    //ローカル関数
                    IEnumerable<double> MakeTheta()
                    {
                        var Val = -10.0;
                        while (true)
                        {
                            yield return Val;
                            Val += 0.1;
                            if (Val > 10.0) yield break;
                        }
                    }


                    var AllData = new List<(double 角度, double 一致率, CvPoint 座標)>();

                    Angles.ForEach(t =>
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
                General.SetLight(false);
                src.Dispose();
                tmp.Dispose();

            }
        }
    }
}
