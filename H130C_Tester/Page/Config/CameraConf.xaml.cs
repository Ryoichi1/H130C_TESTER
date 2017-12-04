using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Linq;
using System;
using OpenCvSharp;
using System.Collections.Generic;
using static System.Threading.Thread;

namespace H130C_Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class CameraConf
    {
        private enum RB_CN { NON, CN220, CN223, CN224, CN225, CN226, JP1 }

        bool CanChangeCnPoint;

        public CameraConf()
        {
            InitializeComponent();
            this.DataContext = General.cam;
            canvasCnPoint.DataContext = State.VmCnPoint;
            canvasLdPoint.DataContext = State.VmLedPoint;
            toggleSw.IsChecked = General.cam.Opening;
            RingCnTesting.IsActive = false;
        }

        //フォームイベントいろいろ
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LedOn = false;
            State.VmMainWindow.MainWinEnable = false;
            await Task.Delay(1200);
            State.VmMainWindow.MainWinEnable = true;
            State.SetCamPoint();
            General.cam.Start();
            tbPoint.Visibility = System.Windows.Visibility.Hidden;
            tbHsv.Visibility = System.Windows.Visibility.Hidden;
            rbNon.IsChecked = true;
            CanChangeCnPoint = true;

            lightOn = true;
            General.SetLight(true);
            State.SetCamPropForCn();//デフォルトはコネクタチェック用の競ってとする
        }

        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            CanChangeCnPoint = false;
            rbNon.IsChecked = true;

            await General.cam.Stop();
            buttonLedOn.Background = General.OffBrush;
            buttonLabeling.Background = General.OffBrush;
            buttonBin.Background = General.OffBrush;
            resetView();

            FlagLabeling = false;
            BinSw = false;

            buttonLabeling.IsEnabled = true;
            buttonBin.IsEnabled = true;
            canvasLdPoint.IsEnabled = true;

            //TODO:
            //LEDを全消灯させる処理
            General.ResetIo();
            State.SetCamPoint();
            await Task.Delay(500);
        }

        private void im_MouseLeave(object sender, MouseEventArgs e)
        {
            tbPoint.Visibility = System.Windows.Visibility.Hidden;
            tbHsv.Visibility = System.Windows.Visibility.Hidden;
            General.cam.FlagHsv = false;
        }

        private void im_MouseEnter(object sender, MouseEventArgs e)
        {
            General.cam.FlagHsv = true;
            tbHsv.Visibility = System.Windows.Visibility.Visible;
        }

        private void im_MouseMove(object sender, MouseEventArgs e)
        {
            tbPoint.Visibility = System.Windows.Visibility.Visible;
            Point point = e.GetPosition(im);
            tbPoint.Text = "XY=" + ((int)(point.X)).ToString() + "/" + ((int)(point.Y)).ToString();

            General.cam.PointX = (int)point.X;
            General.cam.PointY = (int)point.Y;

            tbHsv.Text = "HSV=" + General.cam.Hdata.ToString() + "," + General.cam.Sdata.ToString() + "," + General.cam.Vdata.ToString();
        }


        //データ保存いろいろ
        private void resetView()
        {
            State.VmLedPoint.LED1 = "";
            State.VmLedPoint.LED2 = "";
            State.VmLedPoint.LED3 = "";
            State.VmLedPoint.LED4 = "";
            State.VmLedPoint.LED5 = "";
            State.VmLedPoint.LED6 = "";
            State.VmLedPoint.LED7 = "";
            State.VmLedPoint.LED8 = "";
            State.VmLedPoint.LED9 = "";
            State.VmLedPoint.LED10 = "";
            State.VmLedPoint.LED11 = "";
            State.VmLedPoint.LED12 = "";
            State.VmLedPoint.LED13 = "";
            State.VmLedPoint.LED14 = "";
            State.VmLedPoint.LED15 = "";
            State.VmLedPoint.LED16 = "";

            State.VmLedPoint.LED1Lum = "";
            State.VmLedPoint.LED2Lum = "";
            State.VmLedPoint.LED3Lum = "";
            State.VmLedPoint.LED4Lum = "";
            State.VmLedPoint.LED5Lum = "";
            State.VmLedPoint.LED6Lum = "";
            State.VmLedPoint.LED7Lum = "";
            State.VmLedPoint.LED8Lum = "";
            State.VmLedPoint.LED9Lum = "";
            State.VmLedPoint.LED10Lum = "";
            State.VmLedPoint.LED11Lum = "";
            State.VmLedPoint.LED12Lum = "";
            State.VmLedPoint.LED13Lum = "";
            State.VmLedPoint.LED14Lum = "";
            State.VmLedPoint.LED15Lum = "";
            State.VmLedPoint.LED16Lum = "";

            State.VmLedPoint.LED1Hue = "";
            State.VmLedPoint.LED2Hue = "";
            State.VmLedPoint.LED3Hue = "";
            State.VmLedPoint.LED4Hue = "";
            State.VmLedPoint.LED5Hue = "";
            State.VmLedPoint.LED6Hue = "";
            State.VmLedPoint.LED7Hue = "";
            State.VmLedPoint.LED8Hue = "";
            State.VmLedPoint.LED9Hue = "";
            State.VmLedPoint.LED10Hue = "";
            State.VmLedPoint.LED11Hue = "";
            State.VmLedPoint.LED12Hue = "";
            State.VmLedPoint.LED13Hue = "";
            State.VmLedPoint.LED14Hue = "";
            State.VmLedPoint.LED15Hue = "";
            State.VmLedPoint.LED16Hue = "";

            State.VmLedPoint.ColLED1Hue = General.OffBrush;
            State.VmLedPoint.ColLED2Hue = General.OffBrush;
            State.VmLedPoint.ColLED3Hue = General.OffBrush;
            State.VmLedPoint.ColLED4Hue = General.OffBrush;
            State.VmLedPoint.ColLED5Hue = General.OffBrush;
            State.VmLedPoint.ColLED6Hue = General.OffBrush;
            State.VmLedPoint.ColLED7Hue = General.OffBrush;
            State.VmLedPoint.ColLED8Hue = General.OffBrush;
            State.VmLedPoint.ColLED9Hue = General.OffBrush;
            State.VmLedPoint.ColLED10Hue = General.OffBrush;
            State.VmLedPoint.ColLED11Hue = General.OffBrush;
            State.VmLedPoint.ColLED12Hue = General.OffBrush;
            State.VmLedPoint.ColLED13Hue = General.OffBrush;
            State.VmLedPoint.ColLED14Hue = General.OffBrush;
            State.VmLedPoint.ColLED15Hue = General.OffBrush;
            State.VmLedPoint.ColLED16Hue = General.OffBrush;
        }

        private void SaveCnPoint()
        {
            State.CnProp.X_Cn220 = State.VmCnPoint.X_Cn220;
            State.CnProp.Y_Cn220 = State.VmCnPoint.Y_Cn220;
            State.CnProp.W_Cn220 = State.VmCnPoint.W_Cn220;
            State.CnProp.H_Cn220 = State.VmCnPoint.H_Cn220;

            State.CnProp.X_Cn223 = State.VmCnPoint.X_Cn223;
            State.CnProp.Y_Cn223 = State.VmCnPoint.Y_Cn223;
            State.CnProp.W_Cn223 = State.VmCnPoint.W_Cn223;
            State.CnProp.H_Cn223 = State.VmCnPoint.H_Cn223;

            State.CnProp.X_Cn224 = State.VmCnPoint.X_Cn224;
            State.CnProp.Y_Cn224 = State.VmCnPoint.Y_Cn224;
            State.CnProp.W_Cn224 = State.VmCnPoint.W_Cn224;
            State.CnProp.H_Cn224 = State.VmCnPoint.H_Cn224;

            State.CnProp.X_Cn225 = State.VmCnPoint.X_Cn225;
            State.CnProp.Y_Cn225 = State.VmCnPoint.Y_Cn225;
            State.CnProp.W_Cn225 = State.VmCnPoint.W_Cn225;
            State.CnProp.H_Cn225 = State.VmCnPoint.H_Cn225;

            State.CnProp.X_Cn226 = State.VmCnPoint.X_Cn226;
            State.CnProp.Y_Cn226 = State.VmCnPoint.Y_Cn226;
            State.CnProp.W_Cn226 = State.VmCnPoint.W_Cn226;
            State.CnProp.H_Cn226 = State.VmCnPoint.H_Cn226;

            State.CnProp.X_Jp1 = State.VmCnPoint.X_Jp1;
            State.CnProp.Y_Jp1 = State.VmCnPoint.Y_Jp1;
            State.CnProp.W_Jp1 = State.VmCnPoint.W_Jp1;
            State.CnProp.H_Jp1 = State.VmCnPoint.H_Jp1;
        }

        private void SaveCameraCommonPropForCn()
        {
            State.CamPropCn.Brightness = General.cam.Brightness;
            State.CamPropCn.Contrast = General.cam.Contrast;
            State.CamPropCn.Hue = General.cam.Hue;
            State.CamPropCn.Saturation = General.cam.Saturation;
            State.CamPropCn.Sharpness = General.cam.Sharpness;
            State.CamPropCn.Gamma = General.cam.Gamma;
            State.CamPropCn.Gain = General.cam.Gain;
            State.CamPropCn.Exposure = General.cam.Exposure;
            State.CamPropCn.Whitebalance = General.cam.Wb;
            State.CamPropCn.Theta = General.cam.Theta;
            State.CamPropCn.BinLevel = General.cam.BinLevel;

            State.CamPropCn.Opening = General.cam.Opening;
            State.CamPropCn.OpenCnt = General.cam.openCnt;
            State.CamPropCn.CloseCnt = General.cam.closeCnt;
        }

        private void SaveCameraCommonPropForLed()
        {
            State.CamPropLed.Brightness = General.cam.Brightness;
            State.CamPropLed.Contrast = General.cam.Contrast;
            State.CamPropLed.Hue = General.cam.Hue;
            State.CamPropLed.Saturation = General.cam.Saturation;
            State.CamPropLed.Sharpness = General.cam.Sharpness;
            State.CamPropLed.Gamma = General.cam.Gamma;
            State.CamPropLed.Gain = General.cam.Gain;
            State.CamPropLed.Exposure = General.cam.Exposure;
            State.CamPropLed.Whitebalance = General.cam.Wb;
            State.CamPropLed.Theta = General.cam.Theta;
            State.CamPropLed.BinLevel = General.cam.BinLevel;

            State.CamPropLed.Opening = General.cam.Opening;
            State.CamPropLed.OpenCnt = General.cam.openCnt;
            State.CamPropLed.CloseCnt = General.cam.closeCnt;
        }

        private void SaveLedPoint()
        {
            State.LedProp.Led1 = State.VmLedPoint.LED1;
            State.LedProp.Led2 = State.VmLedPoint.LED2;
            State.LedProp.Led3 = State.VmLedPoint.LED3;
            State.LedProp.Led4 = State.VmLedPoint.LED4;
            State.LedProp.Led5 = State.VmLedPoint.LED5;
            State.LedProp.Led6 = State.VmLedPoint.LED6;
            State.LedProp.Led7 = State.VmLedPoint.LED7;
            State.LedProp.Led8 = State.VmLedPoint.LED8;
            State.LedProp.Led9 = State.VmLedPoint.LED9;
            State.LedProp.Led10 = State.VmLedPoint.LED10;
            State.LedProp.Led11 = State.VmLedPoint.LED11;
            State.LedProp.Led12 = State.VmLedPoint.LED12;
            State.LedProp.Led13 = State.VmLedPoint.LED13;
            State.LedProp.Led14 = State.VmLedPoint.LED14;
            State.LedProp.Led15 = State.VmLedPoint.LED15;
            State.LedProp.Led16 = State.VmLedPoint.LED16;
        }

        private void SaveLedLum()
        {
            State.LedProp.LumLed1 = Double.Parse(State.VmLedPoint.LED1Lum);
            State.LedProp.LumLed2 = Double.Parse(State.VmLedPoint.LED2Lum);
            State.LedProp.LumLed3 = Double.Parse(State.VmLedPoint.LED3Lum);
            State.LedProp.LumLed4 = Double.Parse(State.VmLedPoint.LED4Lum);
            State.LedProp.LumLed5 = Double.Parse(State.VmLedPoint.LED5Lum);
            State.LedProp.LumLed6 = Double.Parse(State.VmLedPoint.LED6Lum);
            State.LedProp.LumLed7 = Double.Parse(State.VmLedPoint.LED7Lum);
            State.LedProp.LumLed8 = Double.Parse(State.VmLedPoint.LED8Lum);
            State.LedProp.LumLed9 = Double.Parse(State.VmLedPoint.LED9Lum);
            State.LedProp.LumLed10 = Double.Parse(State.VmLedPoint.LED10Lum);
            State.LedProp.LumLed11 = Double.Parse(State.VmLedPoint.LED11Lum);
            State.LedProp.LumLed12 = Double.Parse(State.VmLedPoint.LED12Lum);
            State.LedProp.LumLed13 = Double.Parse(State.VmLedPoint.LED13Lum);
            State.LedProp.LumLed14 = Double.Parse(State.VmLedPoint.LED14Lum);
            State.LedProp.LumLed15 = Double.Parse(State.VmLedPoint.LED15Lum);
            State.LedProp.LumLed16 = Double.Parse(State.VmLedPoint.LED16Lum);
        }


        //LED調整用
        bool BinSw = false;
        private void buttonBin_Click(object sender, RoutedEventArgs e)
        {
            General.cam.ResetFlag();
            BinSw = !BinSw;
            General.cam.FlagBin = BinSw;
            buttonBin.Background = BinSw ? Brushes.DodgerBlue : Brushes.Transparent;

            buttonLabeling.IsEnabled = !BinSw;

        }


        private void labeling()
        {
            Task.Run(() =>
            {
                resetView();
                while (FlagLabeling)
                {
                    if (General.cam.blobs == null) continue;
                    var blobInfo = General.cam.blobs.Clone();

                    ////正方形のブロブだけ抽出（dpだけ抽出）
                    //var rectBlobs = blobInfo.Where(pair =>
                    //{
                    //    CvRect rect = pair.Value.Rect;
                    //    return Math.Abs(rect.Height - rect.Width) < 10;
                    //});


                    //LED1～8の座標を抽出する（画面半分より上側のブロブを抽出する）
                    var blobLed1_8 = blobInfo.Where(b => b.Value.Centroid.Y < 180).OrderByDescending(b => b.Value.Centroid.X).ToList();

                    //LED9～16の座標を抽出する（画面半分より下側のブロブを抽出する）
                    var blobLed9_16 = blobInfo.Where(b => b.Value.Centroid.Y > 180).OrderByDescending(b => b.Value.Centroid.X).ToList();

                    if (blobLed1_8.Count() != 8 || blobLed9_16.Count() != 8)
                    {
                        resetView();
                        continue;
                    }

                    //ビューモデルの更新
                    State.VmLedPoint.LED1 = blobLed1_8[0].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[0].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED1Lum = blobLed1_8[0].Value.Area.ToString();

                    State.VmLedPoint.LED2 = blobLed1_8[1].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[1].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED2Lum = blobLed1_8[1].Value.Area.ToString();

                    State.VmLedPoint.LED3 = blobLed1_8[2].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[2].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED3Lum = blobLed1_8[2].Value.Area.ToString();

                    State.VmLedPoint.LED4 = blobLed1_8[3].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[3].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED4Lum = blobLed1_8[3].Value.Area.ToString();

                    State.VmLedPoint.LED5 = blobLed1_8[4].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[4].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED5Lum = blobLed1_8[4].Value.Area.ToString();

                    State.VmLedPoint.LED6 = blobLed1_8[5].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[5].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED6Lum = blobLed1_8[5].Value.Area.ToString();

                    State.VmLedPoint.LED7 = blobLed1_8[6].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[6].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED7Lum = blobLed1_8[6].Value.Area.ToString();

                    State.VmLedPoint.LED8 = blobLed1_8[7].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[7].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED8Lum = blobLed1_8[7].Value.Area.ToString();

                    State.VmLedPoint.LED9 = blobLed9_16[0].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[0].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED9Lum = blobLed9_16[0].Value.Area.ToString();

                    State.VmLedPoint.LED10 = blobLed9_16[1].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[1].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED10Lum = blobLed9_16[1].Value.Area.ToString();

                    State.VmLedPoint.LED11 = blobLed9_16[2].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[2].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED11Lum = blobLed9_16[2].Value.Area.ToString();

                    State.VmLedPoint.LED12 = blobLed9_16[3].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[3].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED12Lum = blobLed9_16[3].Value.Area.ToString();

                    State.VmLedPoint.LED13 = blobLed9_16[4].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[4].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED13Lum = blobLed9_16[4].Value.Area.ToString();

                    State.VmLedPoint.LED14 = blobLed9_16[5].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[5].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED14Lum = blobLed9_16[5].Value.Area.ToString();

                    State.VmLedPoint.LED15 = blobLed9_16[6].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[6].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED15Lum = blobLed9_16[6].Value.Area.ToString();

                    State.VmLedPoint.LED16 = blobLed9_16[7].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[7].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED16Lum = blobLed9_16[7].Value.Area.ToString();

                }

            });
        }

        bool FlagLabeling;
        private void buttonLabeling_Click(object sender, RoutedEventArgs e)
        {
            FlagLabeling = !FlagLabeling;

            buttonBin.IsEnabled = !FlagLabeling;
            buttonHue.IsEnabled = !FlagLabeling;

            buttonLabeling.Background = FlagLabeling ? General.OnBrush : General.OffBrush;

            if (FlagLabeling)
            {
                General.cam.ResetFlag();
                General.cam.FlagLabeling = true;

                labeling();
            }
            else
            {
                resetView();
                General.cam.ResetFlag();
            }

        }

        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (!LedOn)
                return;
            buttonSaveCnPoint.Background = Brushes.DodgerBlue;

            SaveLedPoint();
            SaveLedLum();
            SaveCameraCommonPropForLed();
            General.PlaySound(General.soundSuccess);
            await Task.Delay(150);
            buttonSaveCnPoint.Background = Brushes.Transparent;
        }

        bool LedOn;
        private async void buttonLedOn_Click(object sender, RoutedEventArgs e)
        {
            lightOn = false;
            General.SetLight(false);
            //TODO: 全点灯させる処理
            State.SetCamPropForLed();//LEDチェック用にカメラプロパティを切り替える

            LedOn = !LedOn;

            if (LedOn)
            {
                buttonLedOn.Background = General.OnBrush;
                General.PowSupply(true);
                await General.LedAllOn();
            }
            else
            {
                buttonLedOn.Background = General.OffBrush;
                General.ResetIo();
            }

        }


        //カメラプロパティ調整いろいろ
        private void toggleSw_Checked(object sender, RoutedEventArgs e)
        {
            General.cam.Opening = true;
        }

        private void toggleSw_Unchecked(object sender, RoutedEventArgs e)
        {
            General.cam.Opening = false;
        }

        //コネクタ画像取得いろいろ
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await TestConnector.CheckCn();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IplImage tmp = Cv.LoadImage(@"C:\pic\temp.bmp");

            //cam1の画像を取得する処理
            General.cam.FlagTestPic = true;
            while (General.cam.FlagTestPic) ;
            IplImage trim = General.cam.imageForTest;

            General.trimming(trim, 290, 90, 240, 100);
            var _src = Cv.LoadImage(@"C:\pic\debug.bmp");

            double max;
            double min;

            var Theta = MakeTheta().ToList();

            IEnumerable<double> MakeTheta()
            {
                var Val = -15.0;
                while (true)
                {
                    yield return Val;
                    Val = Val + 0.1;
                    if (Val > 15.0) yield break;
                }
            }


            var List = new List<(double, double, CvPoint)>();

            Theta.ForEach(t =>
            {
                var src = _src.Clone();
                //傾き補正
                CvPoint2D32f center = new CvPoint2D32f(src.Width / 2, src.Height / 2);
                CvMat affineMatrix = Cv.GetRotationMatrix2D(center, t, 1.0);
                //Cv.WarpAffine(im, im, affineMatrix);
                Cv.WarpAffine(src, src, affineMatrix);

                //Cv.Copy(src, dst, mask);


                CvMat result = new CvMat(src.Height - tmp.Height + 1, src.Width - tmp.Width + 1, MatrixType.F32C1);
                Cv.MatchTemplate(src, tmp, result, MatchTemplateMethod.CCoeffNormed);

                Cv.MinMaxLoc(result, out min, out max);

                CvPoint minPoint = new CvPoint();
                CvPoint maxPoint = new CvPoint();
                Cv.MinMaxLoc(result, out minPoint, out maxPoint);

                List.Add((t, max, maxPoint));
            });
            ////傾き補正
            //CvPoint2D32f center = new CvPoint2D32f(src.Width / 2, src.Height / 2);
            //CvMat affineMatrix = Cv.GetRotationMatrix2D(center, Theta, 1.0);
            ////Cv.WarpAffine(im, im, affineMatrix);
            //Cv.WarpAffine(src, src, affineMatrix);

            ////Cv.Copy(src, dst, mask);


            //CvMat result = new CvMat(src.Height - tmp.Height + 1, src.Width - tmp.Width + 1, MatrixType.F32C1);
            //Cv.MatchTemplate(src, tmp, result, MatchTemplateMethod.CCoeffNormed);

            //Cv.MinMaxLoc(result, out min, out max);

            //////////
            var re = List.OrderByDescending(l => l.Item2).ToList()[0];
            MessageBox.Show($"{re.Item1.ToString()},{ re.Item2.ToString()}");
            //////////

            //CvPoint minPoint = new CvPoint();
            //CvPoint maxPoint = new CvPoint();
            //Cv.MinMaxLoc(result, out minPoint, out maxPoint);

            //var src2 = _src.Clone();
            ////傾き補正
            //CvPoint2D32f center2 = new CvPoint2D32f(src2.Width / 2, src2.Height / 2);
            //CvMat affineMatrix2 = Cv.GetRotationMatrix2D(center2, re.Item1, 1.0);
            ////Cv.WarpAffine(im, im, affineMatrix);
            //Cv.WarpAffine(src2, src2, affineMatrix2);

            //CvRect rect = new CvRect(re.Item3, tmp.Size);
            //src2.DrawRect(rect, new CvScalar(0, 0, 255), 2);

            //center2 = new CvPoint2D32f(src2.Width / 2, src2.Height / 2);
            //affineMatrix2 = Cv.GetRotationMatrix2D(center2, -(re.Item1), 1.0);
            ////Cv.WarpAffine(im, im, affineMatrix);
            //Cv.WarpAffine(src2, src2, affineMatrix2);

            //Cv.NamedWindow("window");
            //Cv.ShowImage("window", src2);
            //Cv.WaitKey();
            //Cv.DestroyWindow("window");

            //Cv.ReleaseImage(src2);
        }


        bool lightOn;
        private void SetCnPointCanvas(RB_CN name)
        {
            if (!lightOn)
            {
                lightOn = true;
                General.SetLight(true);
                State.SetCamPropForCn();
            }

            switch (name)
            {
                case RB_CN.CN220:
                    canvasCn220.IsEnabled = true;
                    canvasCn223.IsEnabled = false;
                    canvasCn224.IsEnabled = false;
                    canvasCn225.IsEnabled = false;
                    canvasCn226.IsEnabled = false;
                    canvasJp1.IsEnabled = false;
                    break;
                case RB_CN.CN223:
                    canvasCn220.IsEnabled = false;
                    canvasCn223.IsEnabled = true;
                    canvasCn224.IsEnabled = false;
                    canvasCn225.IsEnabled = false;
                    canvasCn226.IsEnabled = false;
                    canvasJp1.IsEnabled = false;
                    break;
                case RB_CN.CN224:
                    canvasCn220.IsEnabled = false;
                    canvasCn223.IsEnabled = false;
                    canvasCn224.IsEnabled = true;
                    canvasCn225.IsEnabled = false;
                    canvasCn226.IsEnabled = false;
                    canvasJp1.IsEnabled = false;
                    break;
                case RB_CN.CN225:
                    canvasCn220.IsEnabled = false;
                    canvasCn223.IsEnabled = false;
                    canvasCn224.IsEnabled = false;
                    canvasCn225.IsEnabled = true;
                    canvasCn226.IsEnabled = false;
                    canvasJp1.IsEnabled = false;
                    break;
                case RB_CN.CN226:
                    canvasCn220.IsEnabled = false;
                    canvasCn223.IsEnabled = false;
                    canvasCn224.IsEnabled = false;
                    canvasCn225.IsEnabled = false;
                    canvasCn226.IsEnabled = true;
                    canvasJp1.IsEnabled = false;
                    break;
                case RB_CN.JP1:
                    canvasCn220.IsEnabled = false;
                    canvasCn223.IsEnabled = false;
                    canvasCn224.IsEnabled = false;
                    canvasCn225.IsEnabled = false;
                    canvasCn226.IsEnabled = false;
                    canvasJp1.IsEnabled = true;
                    break;
                case RB_CN.NON:
                    canvasCn220.IsEnabled = false;
                    canvasCn223.IsEnabled = false;
                    canvasCn224.IsEnabled = false;
                    canvasCn225.IsEnabled = false;
                    canvasCn226.IsEnabled = false;
                    canvasJp1.IsEnabled = false;
                    General.cam.ResetFlag();
                    return;
            }
            SetRect(name);
        }

        private void SetRect(RB_CN name)
        {
            int x = 0, y = 0, w = 0, h = 0;

            switch (name)
            {
                case RB_CN.CN220:
                    x = State.VmCnPoint.X_Cn220;
                    y = State.VmCnPoint.Y_Cn220;
                    w = State.VmCnPoint.W_Cn220;
                    h = State.VmCnPoint.H_Cn220;
                    break;
                case RB_CN.CN223:
                    x = State.VmCnPoint.X_Cn223;
                    y = State.VmCnPoint.Y_Cn223;
                    w = State.VmCnPoint.W_Cn223;
                    h = State.VmCnPoint.H_Cn223;
                    break;
                case RB_CN.CN224:
                    x = State.VmCnPoint.X_Cn224;
                    y = State.VmCnPoint.Y_Cn224;
                    w = State.VmCnPoint.W_Cn224;
                    h = State.VmCnPoint.H_Cn224;
                    break;
                case RB_CN.CN225:
                    x = State.VmCnPoint.X_Cn225;
                    y = State.VmCnPoint.Y_Cn225;
                    w = State.VmCnPoint.W_Cn225;
                    h = State.VmCnPoint.H_Cn225;
                    break;
                case RB_CN.CN226:
                    x = State.VmCnPoint.X_Cn226;
                    y = State.VmCnPoint.Y_Cn226;
                    w = State.VmCnPoint.W_Cn226;
                    h = State.VmCnPoint.H_Cn226;
                    break;
                case RB_CN.JP1:
                    x = State.VmCnPoint.X_Jp1;
                    y = State.VmCnPoint.Y_Jp1;
                    w = State.VmCnPoint.W_Jp1;
                    h = State.VmCnPoint.H_Jp1;
                    break;
            }

            General.cam.MakeFrame = (img) =>
            {
                var margin = State.CnProp.Margin;
                img.Rectangle(new CvRect(x, y, w, h), CvColor.Red, 1);
                img.Rectangle(new CvRect(x - margin, y - margin, w + 2 * margin, h + 2 * margin), CvColor.Blue, 1);
            };
            General.cam.FlagFrame = true;

        }

        private void GetTemplatePic()
        {
            //camの画像を取得する処理
            General.cam.FlagTestPic = true;
            while (General.cam.FlagTestPic) ;
            IplImage src = General.cam.imageForTest;

            var tmpCn220 = General.trimming(src, State.VmCnPoint.X_Cn220, State.VmCnPoint.Y_Cn220, State.VmCnPoint.W_Cn220, State.VmCnPoint.H_Cn220);
            tmpCn220.SaveImage(Constants.filePath_TemplateCn220);

            var tmpCn223 = General.trimming(src, State.VmCnPoint.X_Cn223, State.VmCnPoint.Y_Cn223, State.VmCnPoint.W_Cn223, State.VmCnPoint.H_Cn223);
            tmpCn223.SaveImage(Constants.filePath_TemplateCn223);

            var tmpCn224 = General.trimming(src, State.VmCnPoint.X_Cn224, State.VmCnPoint.Y_Cn224, State.VmCnPoint.W_Cn224, State.VmCnPoint.H_Cn224);
            tmpCn224.SaveImage(Constants.filePath_TemplateCn224);

            var tmpCn225 = General.trimming(src, State.VmCnPoint.X_Cn225, State.VmCnPoint.Y_Cn225, State.VmCnPoint.W_Cn225, State.VmCnPoint.H_Cn225);
            tmpCn225.SaveImage(Constants.filePath_TemplateCn225);

            var tmpCn226 = General.trimming(src, State.VmCnPoint.X_Cn226, State.VmCnPoint.Y_Cn226, State.VmCnPoint.W_Cn226, State.VmCnPoint.H_Cn226);
            tmpCn226.SaveImage(Constants.filePath_TemplateCn226);

            var tmpJp1 = General.trimming(src, State.VmCnPoint.X_Jp1, State.VmCnPoint.Y_Jp1, State.VmCnPoint.W_Jp1, State.VmCnPoint.H_Jp1);
            tmpJp1.SaveImage(Constants.filePath_TemplateJp1);

            General.cam.ResetFlag();
        }



        bool frame;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            General.cam.MakeFrame = (img) =>
            {
                img.Rectangle(new CvRect(290, 90, 240, 100), CvColor.Red, 2);
            };

            frame = !frame;
            General.cam.FlagFrame = frame;
        }

        private void rbCn220_Checked(object sender, RoutedEventArgs e)
        {
            SetCnPointCanvas(RB_CN.CN220);
        }

        private void rbCn223_Checked(object sender, RoutedEventArgs e)
        {
            SetCnPointCanvas(RB_CN.CN223);
        }

        private void rbCn224_Checked(object sender, RoutedEventArgs e)
        {
            SetCnPointCanvas(RB_CN.CN224);
        }

        private void rbCn225_Checked(object sender, RoutedEventArgs e)
        {
            SetCnPointCanvas(RB_CN.CN225);
        }

        private void rbCn226_Checked(object sender, RoutedEventArgs e)
        {
            SetCnPointCanvas(RB_CN.CN226);
        }

        private void rbJp1_Checked(object sender, RoutedEventArgs e)
        {
            SetCnPointCanvas(RB_CN.JP1);
        }

        private void Cn220_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect(RB_CN.CN220);
        }

        private void Cn223_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect(RB_CN.CN223);

        }

        private void Cn224_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect(RB_CN.CN224);

        }

        private void Cn225_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect(RB_CN.CN225);

        }

        private void Cn226_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect(RB_CN.CN226);

        }
        private void Jp1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect(RB_CN.JP1);

        }

        private async void buttonSaveCnPoint_Click(object sender, RoutedEventArgs e)
        {
            if (!lightOn)
            {
                lightOn = true;
                General.SetLight(true);
                await Task.Delay(1000);
            }

            buttonSaveCnPoint.Background = Brushes.DodgerBlue;
            //保存する処理
            SaveCnPoint();
            GetTemplatePic();

            //カメラプロパティ（コネクタ試験用）を保存する処理
            SaveCameraCommonPropForCn();

            General.PlaySound(General.soundSuccess);
            await Task.Delay(150);
            buttonSaveCnPoint.Background = Brushes.Transparent;
        }

        private void rbNon_Checked(object sender, RoutedEventArgs e)
        {
            SetCnPointCanvas(RB_CN.NON);
        }

        bool cnTesting;
        private async void buttonCnTest_Click(object sender, RoutedEventArgs e)
        {
            if (cnTesting) return;

            cnTesting = true;
            RingCnTesting.IsActive = true;
            await TestConnector.CheckCn();
            State.VmCnPoint.ResultCn220 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN220).一致率.ToString("F2");
            State.VmCnPoint.ResultCn223 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN223).一致率.ToString("F2");
            State.VmCnPoint.ResultCn224 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN224).一致率.ToString("F2");
            State.VmCnPoint.ResultCn225 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN225).一致率.ToString("F2");
            State.VmCnPoint.ResultCn226 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN226).一致率.ToString("F2");
            State.VmCnPoint.ResultJp1 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.JP1).一致率.ToString("F2");
            RingCnTesting.IsActive = false;
            General.SetLight(true);
            cnTesting = false;
        }

        bool ShowHue;
        private void buttonHue_Click(object sender, RoutedEventArgs e)
        {
            ShowHue = !ShowHue;

            buttonBin.IsEnabled = !ShowHue;
            buttonLabeling.IsEnabled = !ShowHue;

            buttonHue.Background = ShowHue ? General.OnBrush : General.OffBrush;

            if (ShowHue)
            {
                General.cam.ResetFlag();

                Task.Run(() =>
                {
                    while (ShowHue)
                    {
                        TestLed.CheckColorForDebug();
                        Sleep(100);
                    }
                    resetView();
                });
            }
            else
            {
                General.cam.ResetFlag();
            }
        }
    }
}
