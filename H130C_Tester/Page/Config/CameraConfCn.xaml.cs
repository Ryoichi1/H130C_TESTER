using OpenCvSharp;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static H130C_Tester.General;

namespace H130C_Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class CameraConfCn
    {
        private enum RB_CN { NON, CN220, CN223, CN224, CN225, CN226, JP1 }

        private RB_CN rbState = RB_CN.NON;

        bool CanChangeCnPoint;

        public CameraConfCn()
        {
            InitializeComponent();
            this.DataContext = General.cam;
            canvasCnPoint.DataContext = State.VmCnPoint;
            RingCnTesting.IsActive = false;

        }

        //フォームイベントいろいろ
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Flags.EnableConfCnPage = false;
            resetView();
            //フォームロード時真っ暗だと困るので、照明1点灯にしておく
            OnLight1 = true;
            //LED側のカメラ調整後だとカメラプロパティが全く異なり撮影画像の見栄えが良くないため、CN220のカメラプロパティに設定しておく
            State.SetCamPropForCn(State.CN_NAME.CN220);

            buttonLight1.Background = General.OnBrush;
            General.SetLight1(true);


            if (!General.cam.CamState)
                return;

            State.VmMainWindow.MainWinEnable = false;
            await Task.Delay(1200);
            State.VmMainWindow.MainWinEnable = true;
            State.SetCamPoint();
            General.cam.Start();
            tbPoint.Visibility = System.Windows.Visibility.Hidden;
            tbHsv.Visibility = System.Windows.Visibility.Hidden;
            rbNon.IsChecked = true;
            CanChangeCnPoint = true;

            canvasCnPoint.IsEnabled = true;
        }

        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            CanChangeCnPoint = false;
            rbNon.IsChecked = true;


            //TODO:
            //LEDを全消灯させる処理
            General.ResetIo();
            if (!General.cam.CamState)
                return;
            await General.cam.Stop();
            State.SetCamPoint();
            await Task.Delay(500);
            Flags.EnableConfCnPage = true;
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


        private void ResetLight()
        {
            OnLight1 = false;
            OnLight2 = false;
            OnLight3 = false;
            General.SetLight1(false);
            General.SetLight2(false);
            General.SetLight3(false);
            buttonLight1.Background = General.OffBrush;
            buttonLight2.Background = General.OffBrush;
            buttonLight3.Background = General.OffBrush;
        }


        //データ保存いろいろ
        private void resetView()
        {
            State.VmCnPoint.ResultCn220 = "";
            State.VmCnPoint.ResultCn223 = "";
            State.VmCnPoint.ResultCn224 = "";
            State.VmCnPoint.ResultCn225 = "";
            State.VmCnPoint.ResultCn226 = "";
            State.VmCnPoint.ResultJp1 = "";
            State.VmCnPoint.ColCn220 = OffBrush;
            State.VmCnPoint.ColCn223 = OffBrush;
            State.VmCnPoint.ColCn224 = OffBrush;
            State.VmCnPoint.ColCn225 = OffBrush;
            State.VmCnPoint.ColCn226 = OffBrush;
            State.VmCnPoint.ColJp1 = OffBrush;
        }

        private void SaveCnPoint()
        {
            switch (rbState)
            {
                case RB_CN.CN220:
                    State.CamPropCn220.X = State.VmCnPoint.X_Cn220;
                    State.CamPropCn220.Y = State.VmCnPoint.Y_Cn220;
                    State.CamPropCn220.W = State.VmCnPoint.W_Cn220;
                    State.CamPropCn220.H = State.VmCnPoint.H_Cn220;
                    break;
                case RB_CN.CN223:
                    State.CamPropCn223.X = State.VmCnPoint.X_Cn223;
                    State.CamPropCn223.Y = State.VmCnPoint.Y_Cn223;
                    State.CamPropCn223.W = State.VmCnPoint.W_Cn223;
                    State.CamPropCn223.H = State.VmCnPoint.H_Cn223;
                    break;
                case RB_CN.CN224:
                    State.CamPropCn224.X = State.VmCnPoint.X_Cn224;
                    State.CamPropCn224.Y = State.VmCnPoint.Y_Cn224;
                    State.CamPropCn224.W = State.VmCnPoint.W_Cn224;
                    State.CamPropCn224.H = State.VmCnPoint.H_Cn224;
                    break;
                case RB_CN.CN225:
                    State.CamPropCn225.X = State.VmCnPoint.X_Cn225;
                    State.CamPropCn225.Y = State.VmCnPoint.Y_Cn225;
                    State.CamPropCn225.W = State.VmCnPoint.W_Cn225;
                    State.CamPropCn225.H = State.VmCnPoint.H_Cn225;
                    break;
                case RB_CN.CN226:
                    State.CamPropCn226.X = State.VmCnPoint.X_Cn226;
                    State.CamPropCn226.Y = State.VmCnPoint.Y_Cn226;
                    State.CamPropCn226.W = State.VmCnPoint.W_Cn226;
                    State.CamPropCn226.H = State.VmCnPoint.H_Cn226;
                    break;
                case RB_CN.JP1:
                    State.CamPropJp1.X = State.VmCnPoint.X_Jp1;
                    State.CamPropJp1.Y = State.VmCnPoint.Y_Jp1;
                    State.CamPropJp1.W = State.VmCnPoint.W_Jp1;
                    State.CamPropJp1.H = State.VmCnPoint.H_Jp1;
                    break;

            }
        }

        private void SaveCameraCommonPropForCn()
        {
            switch (rbState)
            {
                case RB_CN.CN220:
                    State.CamPropCn220.BinLevel = General.cam.BinLevel;
                    State.CamPropCn220.Opening = General.cam.Opening;
                    State.CamPropCn220.OpenCnt = General.cam.OpenCnt;
                    State.CamPropCn220.CloseCnt = General.cam.CloseCnt;
                    State.CamPropCn220.Brightness = General.cam.Brightness;
                    State.CamPropCn220.Contrast = General.cam.Contrast;
                    State.CamPropCn220.Hue = General.cam.Hue;
                    State.CamPropCn220.Saturation = General.cam.Saturation;
                    State.CamPropCn220.Sharpness = General.cam.Sharpness;
                    State.CamPropCn220.Gamma = General.cam.Gamma;
                    State.CamPropCn220.Gain = General.cam.Gain;
                    State.CamPropCn220.Exposure = General.cam.Exposure;
                    State.CamPropCn220.Whitebalance = General.cam.Wb;
                    State.CamPropCn220.Theta = General.cam.Theta;
                    State.CamPropCn220.Light1On = OnLight1;
                    State.CamPropCn220.Light2On = OnLight2;
                    State.CamPropCn220.Light3On = OnLight3;
                    break;
                case RB_CN.CN223:
                    State.CamPropCn223.BinLevel = General.cam.BinLevel;
                    State.CamPropCn223.Opening = General.cam.Opening;
                    State.CamPropCn223.OpenCnt = General.cam.OpenCnt;
                    State.CamPropCn223.CloseCnt = General.cam.CloseCnt;
                    State.CamPropCn223.Brightness = General.cam.Brightness;
                    State.CamPropCn223.Contrast = General.cam.Contrast;
                    State.CamPropCn223.Hue = General.cam.Hue;
                    State.CamPropCn223.Saturation = General.cam.Saturation;
                    State.CamPropCn223.Sharpness = General.cam.Sharpness;
                    State.CamPropCn223.Gamma = General.cam.Gamma;
                    State.CamPropCn223.Gain = General.cam.Gain;
                    State.CamPropCn223.Exposure = General.cam.Exposure;
                    State.CamPropCn223.Whitebalance = General.cam.Wb;
                    State.CamPropCn223.Theta = General.cam.Theta;
                    State.CamPropCn223.Light1On = OnLight1;
                    State.CamPropCn223.Light2On = OnLight2;
                    State.CamPropCn223.Light3On = OnLight3;
                    break;
                case RB_CN.CN224:
                    State.CamPropCn224.BinLevel = General.cam.BinLevel;
                    State.CamPropCn224.Opening = General.cam.Opening;
                    State.CamPropCn224.OpenCnt = General.cam.OpenCnt;
                    State.CamPropCn224.CloseCnt = General.cam.CloseCnt;
                    State.CamPropCn224.Brightness = General.cam.Brightness;
                    State.CamPropCn224.Contrast = General.cam.Contrast;
                    State.CamPropCn224.Hue = General.cam.Hue;
                    State.CamPropCn224.Saturation = General.cam.Saturation;
                    State.CamPropCn224.Sharpness = General.cam.Sharpness;
                    State.CamPropCn224.Gamma = General.cam.Gamma;
                    State.CamPropCn224.Gain = General.cam.Gain;
                    State.CamPropCn224.Exposure = General.cam.Exposure;
                    State.CamPropCn224.Whitebalance = General.cam.Wb;
                    State.CamPropCn224.Theta = General.cam.Theta;
                    State.CamPropCn224.Light1On = OnLight1;
                    State.CamPropCn224.Light2On = OnLight2;
                    State.CamPropCn224.Light3On = OnLight3;
                    break;
                case RB_CN.CN225:
                    State.CamPropCn225.BinLevel = General.cam.BinLevel;
                    State.CamPropCn225.Opening = General.cam.Opening;
                    State.CamPropCn225.OpenCnt = General.cam.OpenCnt;
                    State.CamPropCn225.CloseCnt = General.cam.CloseCnt;
                    State.CamPropCn225.Brightness = General.cam.Brightness;
                    State.CamPropCn225.Contrast = General.cam.Contrast;
                    State.CamPropCn225.Hue = General.cam.Hue;
                    State.CamPropCn225.Saturation = General.cam.Saturation;
                    State.CamPropCn225.Sharpness = General.cam.Sharpness;
                    State.CamPropCn225.Gamma = General.cam.Gamma;
                    State.CamPropCn225.Gain = General.cam.Gain;
                    State.CamPropCn225.Exposure = General.cam.Exposure;
                    State.CamPropCn225.Whitebalance = General.cam.Wb;
                    State.CamPropCn225.Theta = General.cam.Theta;
                    State.CamPropCn225.Light1On = OnLight1;
                    State.CamPropCn225.Light2On = OnLight2;
                    State.CamPropCn225.Light3On = OnLight3;
                    break;
                case RB_CN.CN226:
                    State.CamPropCn226.BinLevel = General.cam.BinLevel;
                    State.CamPropCn226.Opening = General.cam.Opening;
                    State.CamPropCn226.OpenCnt = General.cam.OpenCnt;
                    State.CamPropCn226.CloseCnt = General.cam.CloseCnt;
                    State.CamPropCn226.Brightness = General.cam.Brightness;
                    State.CamPropCn226.Contrast = General.cam.Contrast;
                    State.CamPropCn226.Hue = General.cam.Hue;
                    State.CamPropCn226.Saturation = General.cam.Saturation;
                    State.CamPropCn226.Sharpness = General.cam.Sharpness;
                    State.CamPropCn226.Gamma = General.cam.Gamma;
                    State.CamPropCn226.Gain = General.cam.Gain;
                    State.CamPropCn226.Exposure = General.cam.Exposure;
                    State.CamPropCn226.Whitebalance = General.cam.Wb;
                    State.CamPropCn226.Theta = General.cam.Theta;
                    State.CamPropCn226.Light1On = OnLight1;
                    State.CamPropCn226.Light2On = OnLight2;
                    State.CamPropCn226.Light3On = OnLight3;
                    break;
                case RB_CN.JP1:
                    State.CamPropJp1.BinLevel = General.cam.BinLevel;
                    State.CamPropJp1.Opening = General.cam.Opening;
                    State.CamPropJp1.OpenCnt = General.cam.OpenCnt;
                    State.CamPropJp1.CloseCnt = General.cam.CloseCnt;
                    State.CamPropJp1.Brightness = General.cam.Brightness;
                    State.CamPropJp1.Contrast = General.cam.Contrast;
                    State.CamPropJp1.Hue = General.cam.Hue;
                    State.CamPropJp1.Saturation = General.cam.Saturation;
                    State.CamPropJp1.Sharpness = General.cam.Sharpness;
                    State.CamPropJp1.Gamma = General.cam.Gamma;
                    State.CamPropJp1.Gain = General.cam.Gain;
                    State.CamPropJp1.Exposure = General.cam.Exposure;
                    State.CamPropJp1.Whitebalance = General.cam.Wb;
                    State.CamPropJp1.Theta = General.cam.Theta;
                    State.CamPropJp1.Light1On = OnLight1;
                    State.CamPropJp1.Light2On = OnLight2;
                    State.CamPropJp1.Light3On = OnLight3;
                    break;
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

        private void SetCnPointCanvas()
        {
            switch (rbState)
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
            SetLight();
            SetRect();
            resetView();
        }

        private void SetRect()
        {
            int x = 0, y = 0, w = 0, h = 0, m = 0;

            switch (rbState)
            {
                case RB_CN.CN220:
                    x = State.VmCnPoint.X_Cn220;
                    y = State.VmCnPoint.Y_Cn220;
                    w = State.VmCnPoint.W_Cn220;
                    h = State.VmCnPoint.H_Cn220;
                    m = State.CamPropCn220.Margin;
                    break;
                case RB_CN.CN223:
                    x = State.VmCnPoint.X_Cn223;
                    y = State.VmCnPoint.Y_Cn223;
                    w = State.VmCnPoint.W_Cn223;
                    h = State.VmCnPoint.H_Cn223;
                    m = State.CamPropCn223.Margin;
                    break;
                case RB_CN.CN224:
                    x = State.VmCnPoint.X_Cn224;
                    y = State.VmCnPoint.Y_Cn224;
                    w = State.VmCnPoint.W_Cn224;
                    h = State.VmCnPoint.H_Cn224;
                    m = State.CamPropCn224.Margin;
                    break;
                case RB_CN.CN225:
                    x = State.VmCnPoint.X_Cn225;
                    y = State.VmCnPoint.Y_Cn225;
                    w = State.VmCnPoint.W_Cn225;
                    h = State.VmCnPoint.H_Cn225;
                    m = State.CamPropCn225.Margin;
                    break;
                case RB_CN.CN226:
                    x = State.VmCnPoint.X_Cn226;
                    y = State.VmCnPoint.Y_Cn226;
                    w = State.VmCnPoint.W_Cn226;
                    h = State.VmCnPoint.H_Cn226;
                    m = State.CamPropCn226.Margin;
                    break;
                case RB_CN.JP1:
                    x = State.VmCnPoint.X_Jp1;
                    y = State.VmCnPoint.Y_Jp1;
                    w = State.VmCnPoint.W_Jp1;
                    h = State.VmCnPoint.H_Jp1;
                    m = State.CamPropJp1.Margin;
                    break;
            }

            General.cam.MakeFrame = (img) =>
            {
                img.Rectangle(new CvRect(x, y, w, h), CvColor.Red, 1);
                img.Rectangle(new CvRect(x - m, y - m, w + 2 * m, h + 2 * m), CvColor.Blue, 1);
            };
            General.cam.FlagFrame = true;

        }

        private void GetTemplatePic()
        {
            //camの画像を取得する処理
            General.cam.FlagTestPic = true;
            while (General.cam.FlagTestPic) ;
            IplImage src = General.cam.imageForTest;

            switch (rbState)
            {
                case RB_CN.CN220:
                    var tmpCn220 = General.trimming(src, State.VmCnPoint.X_Cn220, State.VmCnPoint.Y_Cn220, State.VmCnPoint.W_Cn220, State.VmCnPoint.H_Cn220);
                    tmpCn220.SaveImage(Constants.filePath_TemplateCn220);
                    break;
                case RB_CN.CN223:
                    var tmpCn223 = General.trimming(src, State.VmCnPoint.X_Cn223, State.VmCnPoint.Y_Cn223, State.VmCnPoint.W_Cn223, State.VmCnPoint.H_Cn223);
                    tmpCn223.SaveImage(Constants.filePath_TemplateCn223);
                    break;
                case RB_CN.CN224:
                    var tmpCn224 = General.trimming(src, State.VmCnPoint.X_Cn224, State.VmCnPoint.Y_Cn224, State.VmCnPoint.W_Cn224, State.VmCnPoint.H_Cn224);
                    tmpCn224.SaveImage(Constants.filePath_TemplateCn224);
                    break;
                case RB_CN.CN225:
                    var tmpCn225 = General.trimming(src, State.VmCnPoint.X_Cn225, State.VmCnPoint.Y_Cn225, State.VmCnPoint.W_Cn225, State.VmCnPoint.H_Cn225);
                    tmpCn225.SaveImage(Constants.filePath_TemplateCn225);
                    break;
                case RB_CN.CN226:
                    var tmpCn226 = General.trimming(src, State.VmCnPoint.X_Cn226, State.VmCnPoint.Y_Cn226, State.VmCnPoint.W_Cn226, State.VmCnPoint.H_Cn226);
                    tmpCn226.SaveImage(Constants.filePath_TemplateCn226);
                    break;
                case RB_CN.JP1:
                    var tmpJp1 = General.trimming(src, State.VmCnPoint.X_Jp1, State.VmCnPoint.Y_Jp1, State.VmCnPoint.W_Jp1, State.VmCnPoint.H_Jp1);
                    tmpJp1.SaveImage(Constants.filePath_TemplateJp1);
                    break;
            }
            General.cam.ResetFlag();
        }

        private void SetLight()
        {
            switch (rbState)
            {
                case RB_CN.CN220:
                    General.SetLight1(State.CamPropCn220.Light1On);
                    General.SetLight2(State.CamPropCn220.Light2On);
                    General.SetLight3(State.CamPropCn220.Light3On);
                    OnLight1 = State.CamPropCn220.Light1On;
                    OnLight2 = State.CamPropCn220.Light2On;
                    OnLight3 = State.CamPropCn220.Light3On;
                    break;
                case RB_CN.CN223:
                    General.SetLight1(State.CamPropCn223.Light1On);
                    General.SetLight2(State.CamPropCn223.Light2On);
                    General.SetLight3(State.CamPropCn223.Light3On);
                    OnLight1 = State.CamPropCn223.Light1On;
                    OnLight2 = State.CamPropCn223.Light2On;
                    OnLight3 = State.CamPropCn223.Light3On;
                    break;
                case RB_CN.CN224:
                    General.SetLight1(State.CamPropCn224.Light1On);
                    General.SetLight2(State.CamPropCn224.Light2On);
                    General.SetLight3(State.CamPropCn224.Light3On);
                    OnLight1 = State.CamPropCn224.Light1On;
                    OnLight2 = State.CamPropCn224.Light2On;
                    OnLight3 = State.CamPropCn224.Light3On;
                    break;
                case RB_CN.CN225:
                    General.SetLight1(State.CamPropCn225.Light1On);
                    General.SetLight2(State.CamPropCn225.Light2On);
                    General.SetLight3(State.CamPropCn225.Light3On);
                    OnLight1 = State.CamPropCn225.Light1On;
                    OnLight2 = State.CamPropCn225.Light2On;
                    OnLight3 = State.CamPropCn225.Light3On;
                    break;
                case RB_CN.CN226:
                    General.SetLight1(State.CamPropCn226.Light1On);
                    General.SetLight2(State.CamPropCn226.Light2On);
                    General.SetLight3(State.CamPropCn226.Light3On);
                    OnLight1 = State.CamPropCn226.Light1On;
                    OnLight2 = State.CamPropCn226.Light2On;
                    OnLight3 = State.CamPropCn226.Light3On;
                    break;
                case RB_CN.JP1:
                    General.SetLight1(State.CamPropJp1.Light1On);
                    General.SetLight2(State.CamPropJp1.Light2On);
                    General.SetLight3(State.CamPropJp1.Light3On);
                    OnLight1 = State.CamPropJp1.Light1On;
                    OnLight2 = State.CamPropJp1.Light2On;
                    OnLight3 = State.CamPropJp1.Light3On;
                    break;
            }
            buttonLight1.Background = OnLight1 ? General.OnBrush : General.OffBrush;
            buttonLight2.Background = OnLight2 ? General.OnBrush : General.OffBrush;
            buttonLight3.Background = OnLight3 ? General.OnBrush : General.OffBrush;
        }


        private void rbCn220_Checked(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("プレス治具のレバーを下げてください");
                rbNon.IsChecked = true;
                return;
            }
            rbState = RB_CN.CN220;
            State.SetCamPropForCn(State.CN_NAME.CN220);
            SetCnPointCanvas();
        }

        private void rbCn223_Checked(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("プレス治具のレバーを下げてください");
                rbNon.IsChecked = true;
                return;
            }
            rbState = RB_CN.CN223;
            State.SetCamPropForCn(State.CN_NAME.CN223);
            SetCnPointCanvas();
        }

        private void rbCn224_Checked(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("プレス治具のレバーを下げてください");
                rbNon.IsChecked = true;
                return;
            }
            rbState = RB_CN.CN224;
            State.SetCamPropForCn(State.CN_NAME.CN224);
            SetCnPointCanvas();
        }

        private void rbCn225_Checked(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("プレス治具のレバーを下げてください");
                rbNon.IsChecked = true;
                return;
            }
            rbState = RB_CN.CN225;
            State.SetCamPropForCn(State.CN_NAME.CN225);
            SetCnPointCanvas();
        }

        private void rbCn226_Checked(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("プレス治具のレバーを下げてください");
                rbNon.IsChecked = true;
                return;
            }
            rbState = RB_CN.CN226;
            State.SetCamPropForCn(State.CN_NAME.CN226);
            SetCnPointCanvas();
        }

        private void rbJp1_Checked(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("プレス治具のレバーを下げてください");
                rbNon.IsChecked = true;
                return;
            }
            rbState = RB_CN.JP1;
            State.SetCamPropForCn(State.CN_NAME.JP1);
            SetCnPointCanvas();
        }

        private void Cn220_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect();
        }

        private void Cn223_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect();

        }

        private void Cn224_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect();

        }

        private void Cn225_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect();

        }

        private void Cn226_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect();

        }
        private void Jp1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!CanChangeCnPoint) return;
            SetRect();

        }

        private async void buttonSaveCnPoint_Click(object sender, RoutedEventArgs e)
        {
            if (rbNon.IsChecked == true) return;

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
            rbState = RB_CN.NON;
            SetCnPointCanvas();
        }

        bool cnTesting;
        private async void buttonCnTest_Click(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("プレス治具のレバーを下げてください");
                return;
            }

            if (cnTesting) return;

            resetView();
            ResetLight();
            rbNon.IsChecked = true;

            cnTesting = true;
            RingCnTesting.IsActive = true;
            await TestConnector.CheckCn();
            State.VmCnPoint.ResultCn220 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN220).一致率.ToString("F2");
            State.VmCnPoint.ResultCn223 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN223).一致率.ToString("F2");
            State.VmCnPoint.ResultCn224 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN224).一致率.ToString("F2");
            State.VmCnPoint.ResultCn225 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN225).一致率.ToString("F2");
            State.VmCnPoint.ResultCn226 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN226).一致率.ToString("F2");
            State.VmCnPoint.ResultJp1 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.JP1).一致率.ToString("F2");
            State.VmCnPoint.ColCn220 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN220).一致率 > State.TestSpec.MatchMin ? OkBrush : NgBrush;
            State.VmCnPoint.ColCn223 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN223).一致率 > State.TestSpec.MatchMin ? OkBrush : NgBrush;
            State.VmCnPoint.ColCn224 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN224).一致率 > State.TestSpec.MatchMin ? OkBrush : NgBrush;
            State.VmCnPoint.ColCn225 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN225).一致率 > State.TestSpec.MatchMin ? OkBrush : NgBrush;
            State.VmCnPoint.ColCn226 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.CN226).一致率 > State.TestSpec.MatchMin ? OkBrush : NgBrush;
            State.VmCnPoint.ColJp1 = TestConnector.ListCnSpecs2.Find(l => l.name == TestConnector.NAME2.JP1).一致率 > State.TestSpec.MatchMin ? OkBrush : NgBrush;



            RingCnTesting.IsActive = false;
            General.SetLight1(true);
            cnTesting = false;
            State.VmTestStatus.EnableButtonErrInfo = Visibility.Hidden;//コネクタチェックでエラーになるとエラー詳細ボタンが表示されてしまう！！！
        }


        bool OnLight1;
        private void buttonLight1_Click(object sender, RoutedEventArgs e)
        {
            OnLight1 = !OnLight1;
            if (OnLight1)
            {
                buttonLight1.Background = General.OnBrush;
                General.SetLight1(true);
            }
            else
            {
                buttonLight1.Background = General.OffBrush;
                General.SetLight1(false);
            }
        }


        bool OnLight2;
        private void buttonLight2_Click(object sender, RoutedEventArgs e)
        {
            OnLight2 = !OnLight2;
            if (OnLight2)
            {
                buttonLight2.Background = General.OnBrush;
                General.SetLight2(true);
            }
            else
            {
                buttonLight2.Background = General.OffBrush;
                General.SetLight2(false);
            }

        }

        bool OnLight3;
        private void buttonLight3_Click(object sender, RoutedEventArgs e)
        {
            OnLight3 = !OnLight3;
            if (OnLight3)
            {
                buttonLight3.Background = General.OnBrush;
                General.SetLight3(true);
            }
            else
            {
                buttonLight3.Background = General.OffBrush;
                General.SetLight3(false);
            }

        }
    }
}
