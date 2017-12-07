using OpenCvSharp;
using OpenCvSharp.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace H130C_Tester
{

    public static class TestLed
    {
        public enum NAME
        {
            LED1, LED2, LED3, LED4, LED5, LED6, LED7, LED8,
            LED9, LED10, LED11, LED12, LED13, LED14, LED15, LED16,
        }


        const int WIDTH = 640;
        const int HEIGHT = 360;

        private static IplImage source = new IplImage(WIDTH, HEIGHT, BitDepth.U8, 3);

        public static List<LedSpec> ListLedSpec;

        public class LedSpec
        {
            public NAME name;
            public double Area;
            public double Hue;
            public bool resultArea;
            public bool resultHue;
        }

        private static void InitList()
        {
            ListLedSpec = new List<LedSpec>();
            foreach (var n in Enum.GetValues(typeof(NAME)))
            {
                ListLedSpec.Add(new LedSpec { name = (NAME)n });
            }
        }

        private static (int x, int y) GetPpoint(NAME name)
        {
            var X = 0;
            var Y = 0;

            switch (name)
            {
                case NAME.LED1:
                    X = Int32.Parse(State.CamPropLed.Led1.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led1.Split('/').ToArray()[1]);
                    break;
                case NAME.LED2:
                    X = Int32.Parse(State.CamPropLed.Led2.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led2.Split('/').ToArray()[1]);
                    break;
                case NAME.LED3:
                    X = Int32.Parse(State.CamPropLed.Led3.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led3.Split('/').ToArray()[1]);
                    break;
                case NAME.LED4:
                    X = Int32.Parse(State.CamPropLed.Led4.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led4.Split('/').ToArray()[1]);
                    break;
                case NAME.LED5:
                    X = Int32.Parse(State.CamPropLed.Led5.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led5.Split('/').ToArray()[1]);
                    break;
                case NAME.LED6:
                    X = Int32.Parse(State.CamPropLed.Led6.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led6.Split('/').ToArray()[1]);
                    break;
                case NAME.LED7:
                    X = Int32.Parse(State.CamPropLed.Led7.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led7.Split('/').ToArray()[1]);
                    break;
                case NAME.LED8:
                    X = Int32.Parse(State.CamPropLed.Led8.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led8.Split('/').ToArray()[1]);
                    break;
                case NAME.LED9:
                    X = Int32.Parse(State.CamPropLed.Led9.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led9.Split('/').ToArray()[1]);
                    break;
                case NAME.LED10:
                    X = Int32.Parse(State.CamPropLed.Led10.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led10.Split('/').ToArray()[1]);
                    break;
                case NAME.LED11:
                    X = Int32.Parse(State.CamPropLed.Led11.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led11.Split('/').ToArray()[1]);
                    break;
                case NAME.LED12:
                    X = Int32.Parse(State.CamPropLed.Led12.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led12.Split('/').ToArray()[1]);
                    break;
                case NAME.LED13:
                    X = Int32.Parse(State.CamPropLed.Led13.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led13.Split('/').ToArray()[1]);
                    break;
                case NAME.LED14:
                    X = Int32.Parse(State.CamPropLed.Led14.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led14.Split('/').ToArray()[1]);
                    break;
                case NAME.LED15:
                    X = Int32.Parse(State.CamPropLed.Led15.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led15.Split('/').ToArray()[1]);
                    break;
                case NAME.LED16:
                    X = Int32.Parse(State.CamPropLed.Led16.Split('/').ToArray()[0]);
                    Y = Int32.Parse(State.CamPropLed.Led16.Split('/').ToArray()[1]);
                    break;
            }

            return (X, Y);

        }

        private static double GetRefLum(NAME name)
        {
            double _ref = 0;

            switch (name)
            {
                case NAME.LED1:
                    _ref = State.CamPropLed.LumLed1;
                    break;
                case NAME.LED2:
                    _ref = State.CamPropLed.LumLed2;
                    break;
                case NAME.LED3:
                    _ref = State.CamPropLed.LumLed3;
                    break;
                case NAME.LED4:
                    _ref = State.CamPropLed.LumLed4;
                    break;
                case NAME.LED5:
                    _ref = State.CamPropLed.LumLed5;
                    break;
                case NAME.LED6:
                    _ref = State.CamPropLed.LumLed6;
                    break;
                case NAME.LED7:
                    _ref = State.CamPropLed.LumLed7;
                    break;
                case NAME.LED8:
                    _ref = State.CamPropLed.LumLed8;
                    break;
                case NAME.LED9:
                    _ref = State.CamPropLed.LumLed9;
                    break;
                case NAME.LED10:
                    _ref = State.CamPropLed.LumLed10;
                    break;
                case NAME.LED11:
                    _ref = State.CamPropLed.LumLed11;
                    break;
                case NAME.LED12:
                    _ref = State.CamPropLed.LumLed12;
                    break;
                case NAME.LED13:
                    _ref = State.CamPropLed.LumLed13;
                    break;
                case NAME.LED14:
                    _ref = State.CamPropLed.LumLed14;
                    break;
                case NAME.LED15:
                    _ref = State.CamPropLed.LumLed15;
                    break;
                case NAME.LED16:
                    _ref = State.CamPropLed.LumLed16;
                    break;
            }

            return _ref;
        }

        private static void SetViewModelLum(NAME name, double area, bool result)
        {
            //ビューモデルの更新
            switch (name)
            {
                case NAME.LED1:
                    State.VmTestResults.LumLed1 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed1 = General.NgBrush;
                    break;
                case NAME.LED2:
                    State.VmTestResults.LumLed2 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed2 = General.NgBrush;
                    break;
                case NAME.LED3:
                    State.VmTestResults.LumLed3 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed3 = General.NgBrush;
                    break;
                case NAME.LED4:
                    State.VmTestResults.LumLed4 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed4 = General.NgBrush;
                    break;
                case NAME.LED5:
                    State.VmTestResults.LumLed5 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed5 = General.NgBrush;
                    break;
                case NAME.LED6:
                    State.VmTestResults.LumLed6 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed6 = General.NgBrush;
                    break;
                case NAME.LED7:
                    State.VmTestResults.LumLed7 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed7 = General.NgBrush;
                    break;
                case NAME.LED8:
                    State.VmTestResults.LumLed8 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed8 = General.NgBrush;
                    break;
                case NAME.LED9:
                    State.VmTestResults.LumLed9 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed9 = General.NgBrush;
                    break;
                case NAME.LED10:
                    State.VmTestResults.LumLed10 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed10 = General.NgBrush;
                    break;
                case NAME.LED11:
                    State.VmTestResults.LumLed11 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed11 = General.NgBrush;
                    break;
                case NAME.LED12:
                    State.VmTestResults.LumLed12 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed12 = General.NgBrush;
                    break;
                case NAME.LED13:
                    State.VmTestResults.LumLed13 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed13 = General.NgBrush;
                    break;
                case NAME.LED14:
                    State.VmTestResults.LumLed14 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed14 = General.NgBrush;
                    break;
                case NAME.LED15:
                    State.VmTestResults.LumLed15 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed15 = General.NgBrush;
                    break;
                case NAME.LED16:
                    State.VmTestResults.LumLed16 = area.ToString();
                    if (!result) State.VmTestResults.ColLumLed16 = General.NgBrush;
                    break;
            }
        }


        public static void CheckColorForDebug()
        {
            var side = 25;
            var X = 0;
            var Y = 0;

            InitList();

            using (IplImage hsv = new IplImage(640, 360, BitDepth.U8, 3)) // グレースケール画像格納用の変数
            {
                //cam1の画像を取得する処理
                General.cam.FlagTestPic = true;
                while (General.cam.FlagTestPic) ;
                using (IplImage src = General.cam.imageForTest.Clone()) // グレースケール画像格納用の変数
                {
                    General.cam.ResetFlag();
                    //src.SaveImage(@"C:\Users\TSDP00059\Desktop\src.jpg");
                    //RGBからHSVに変換
                    Cv.CvtColor(src, hsv, ColorConversion.BgrToHsv);
                    OpenCvSharp.CPlusPlus.Mat mat = new OpenCvSharp.CPlusPlus.Mat(hsv, true);

                    ListLedSpec.ForEach(l =>
                    {
                        var p = GetPpoint(l.name);
                        X = p.x;
                        Y = p.y;

                        var ListH = new List<int>();
                        var ListS = new List<int>();
                        var ListV = new List<int>();

                        foreach (var i in Enumerable.Range(0, side))
                        {
                            foreach (var j in Enumerable.Range(0, side))
                            {
                                var re = mat.At<OpenCvSharp.CPlusPlus.Vec3b>(Y - (side / 2) + i, X - (side / 2) + j);
                                if (re[0] != 0 && re[1] > 200 && re[2] > 200)
                                {
                                    ListH.Add(re[0]);
                                    ListS.Add(re[1]);
                                    ListV.Add(re[2]);
                                }
                            }
                        }
                        var Hue = (ListH.Count != 0) ? ListH.Average() : 0;
                        var Sat = (ListS.Count != 0) ? ListS.Average() : 0;
                        var Val = (ListV.Count != 0) ? ListV.Average() : 0;

                        var H = Hue.ToString("F0");

                        ColorHSV CurrentHsv = new ColorHSV((float)Hue / 180, (float)Sat / 255, (float)Val / 255); //正規化
                        var rgb = ColorConv.HSV2RGB(CurrentHsv);
                        var color = new SolidColorBrush(Color.FromRgb(rgb.R, rgb.G, rgb.B));
                        color.Opacity = 0.5;
                        color.Freeze();//これ重要！！！  

                        //ビューモデルの更新
                        switch (l.name)
                        {
                            case NAME.LED1:
                                State.VmLedPoint.LED1Hue = H;
                                State.VmLedPoint.ColLED1Hue = color;
                                break;
                            case NAME.LED2:
                                State.VmLedPoint.LED2Hue = H;
                                State.VmLedPoint.ColLED2Hue = color;
                                break;
                            case NAME.LED3:
                                State.VmLedPoint.LED3Hue = H;
                                State.VmLedPoint.ColLED3Hue = color;
                                break;
                            case NAME.LED4:
                                State.VmLedPoint.LED4Hue = H;
                                State.VmLedPoint.ColLED4Hue = color;
                                break;
                            case NAME.LED5:
                                State.VmLedPoint.LED5Hue = H;
                                State.VmLedPoint.ColLED5Hue = color;
                                break;
                            case NAME.LED6:
                                State.VmLedPoint.LED6Hue = H;
                                State.VmLedPoint.ColLED6Hue = color;
                                break;
                            case NAME.LED7:
                                State.VmLedPoint.LED7Hue = H;
                                State.VmLedPoint.ColLED7Hue = color;
                                break;
                            case NAME.LED8:
                                State.VmLedPoint.LED8Hue = H;
                                State.VmLedPoint.ColLED8Hue = color;
                                break;
                            case NAME.LED9:
                                State.VmLedPoint.LED9Hue = H;
                                State.VmLedPoint.ColLED9Hue = color;
                                break;
                            case NAME.LED10:
                                State.VmLedPoint.LED10Hue = H;
                                State.VmLedPoint.ColLED10Hue = color;
                                break;
                            case NAME.LED11:
                                State.VmLedPoint.LED11Hue = H;
                                State.VmLedPoint.ColLED11Hue = color;
                                break;
                            case NAME.LED12:
                                State.VmLedPoint.LED12Hue = H;
                                State.VmLedPoint.ColLED12Hue = color;
                                break;
                            case NAME.LED13:
                                State.VmLedPoint.LED13Hue = H;
                                State.VmLedPoint.ColLED13Hue = color;
                                break;
                            case NAME.LED14:
                                State.VmLedPoint.LED14Hue = H;
                                State.VmLedPoint.ColLED14Hue = color;
                                break;
                            case NAME.LED15:
                                State.VmLedPoint.LED15Hue = H;
                                State.VmLedPoint.ColLED15Hue = color;
                                break;
                            case NAME.LED16:
                                State.VmLedPoint.LED16Hue = H;
                                State.VmLedPoint.ColLED16Hue = color;
                                break;
                        }
                    });
                }
            }
        }

        private static bool LedAllOn;

        public static async Task<bool> CheckColor()
        {
            bool result = false;
            var side = 25;
            var HueMax = 0.0;
            var HueMin = 0.0;

            var X = 0;
            var Y = 0;

            InitList();
            General.cam.ResetFlag();//カメラのフラグを初期化 リトライ時にフラグが初期化できてないとだめ
                                    //例 ＮＧリトライ時は、General.cam.FlagFrame = trueになっていてNGフレーム表示の無限ループにいる
            State.SetCamPropForLed();

            await General.LedAllOn();
            await Task.Delay(3000);
            LedAllOn = true;

            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    using (IplImage hsv = new IplImage(640, 360, BitDepth.U8, 3)) // グレースケール画像格納用の変数
                    {
                        //cam1の画像を取得する処理
                        General.cam.FlagTestPic = true;
                        while (General.cam.FlagTestPic) ;
                        using (IplImage src = General.cam.imageForTest.Clone()) // グレースケール画像格納用の変数
                        {
                            General.cam.ResetFlag();
                            //src.SaveImage(@"C:\Users\TSDP00059\Desktop\src.jpg");
                            //RGBからHSVに変換
                            Cv.CvtColor(src, hsv, ColorConversion.BgrToHsv);
                            OpenCvSharp.CPlusPlus.Mat mat = new OpenCvSharp.CPlusPlus.Mat(hsv, true);

                            ListLedSpec.ForEach(l =>
                            {
                                var p = GetPpoint(l.name);
                                X = p.x;
                                Y = p.y;

                                var ListH = new List<int>();
                                var ListS = new List<int>();
                                var ListV = new List<int>();

                                foreach (var i in Enumerable.Range(0, side))
                                {
                                    foreach (var j in Enumerable.Range(0, side))
                                    {
                                        var re = mat.At<OpenCvSharp.CPlusPlus.Vec3b>(Y - (side / 2) + i, X - (side / 2) + j);
                                        if (re[0] != 0 && re[1] > 200 && re[2] > 200)
                                        {
                                            ListH.Add(re[0]);
                                            ListS.Add(re[1]);
                                            ListV.Add(re[2]);
                                        }
                                    }
                                }
                                var Hue = (ListH.Count != 0) ? ListH.Average() : 0;
                                var Sat = (ListS.Count != 0) ? ListS.Average() : 0;
                                var Val = (ListV.Count != 0) ? ListV.Average() : 0;


                                switch (l.name)
                                {
                                    case NAME.LED1:
                                    case NAME.LED2:
                                    case NAME.LED3:
                                    case NAME.LED4:
                                    case NAME.LED5:
                                    case NAME.LED6:
                                    case NAME.LED7:
                                    case NAME.LED8:
                                        HueMax = State.TestSpec.RedHueMax;
                                        HueMin = State.TestSpec.RedHueMin;
                                        break;
                                    default:
                                        HueMax = State.TestSpec.GreenHueMax;
                                        HueMin = State.TestSpec.GreenHueMin;
                                        break;
                                }

                                l.Hue = Hue;
                                l.resultHue = Hue >= HueMin && Hue <= HueMax;

                                //ビューモデルの更新
                                var H = Hue.ToString("F0");

                                ColorHSV CurrentHsv = new ColorHSV((float)Hue / 180, (float)Sat / 255, (float)Val / 255); //正規化
                                var rgb = ColorConv.HSV2RGB(CurrentHsv);
                                var color = new SolidColorBrush(Color.FromRgb(rgb.R, rgb.G, rgb.B));
                                color.Opacity = 0.5;
                                color.Freeze();//これ重要！！！  

                                switch (l.name)
                                {
                                    case NAME.LED1:
                                        State.VmTestResults.HueLed1 = H;
                                        State.VmTestResults.ColLed1 = color;
                                        break;
                                    case NAME.LED2:
                                        State.VmTestResults.HueLed2 = H;
                                        State.VmTestResults.ColLed2 = color;
                                        break;
                                    case NAME.LED3:
                                        State.VmTestResults.HueLed3 = H;
                                        State.VmTestResults.ColLed3 = color;
                                        break;
                                    case NAME.LED4:
                                        State.VmTestResults.HueLed4 = H;
                                        State.VmTestResults.ColLed4 = color;
                                        break;
                                    case NAME.LED5:
                                        State.VmTestResults.HueLed5 = H;
                                        State.VmTestResults.ColLed5 = color;
                                        break;
                                    case NAME.LED6:
                                        State.VmTestResults.HueLed6 = H;
                                        State.VmTestResults.ColLed6 = color;
                                        break;
                                    case NAME.LED7:
                                        State.VmTestResults.HueLed7 = H;
                                        State.VmTestResults.ColLed7 = color;
                                        break;
                                    case NAME.LED8:
                                        State.VmTestResults.HueLed8 = H;
                                        State.VmTestResults.ColLed8 = color;
                                        break;
                                    case NAME.LED9:
                                        State.VmTestResults.HueLed9 = H;
                                        State.VmTestResults.ColLed9 = color;
                                        break;
                                    case NAME.LED10:
                                        State.VmTestResults.HueLed10 = H;
                                        State.VmTestResults.ColLed10 = color;
                                        break;
                                    case NAME.LED11:
                                        State.VmTestResults.HueLed11 = H;
                                        State.VmTestResults.ColLed11 = color;
                                        break;
                                    case NAME.LED12:
                                        State.VmTestResults.HueLed12 = H;
                                        State.VmTestResults.ColLed12 = color;
                                        break;
                                    case NAME.LED13:
                                        State.VmTestResults.HueLed13 = H;
                                        State.VmTestResults.ColLed13 = color;
                                        break;
                                    case NAME.LED14:
                                        State.VmTestResults.HueLed14 = H;
                                        State.VmTestResults.ColLed14 = color;
                                        break;
                                    case NAME.LED15:
                                        State.VmTestResults.HueLed15 = H;
                                        State.VmTestResults.ColLed15 = color;
                                        break;
                                    case NAME.LED16:
                                        State.VmTestResults.HueLed16 = H;
                                        State.VmTestResults.ColLed16 = color;
                                        break;

                                }
                            });

                            return ListLedSpec.All(l => l.resultHue);
                        }
                    }
                });
            }
            finally
            {
                if (!result)
                {
                    General.cam.MakeNgFrame = (img) =>
                    {
                        //リストからNGの座標を抽出する
                        var NgList = ListLedSpec.Where(l => !l.resultHue).ToList();
                        NgList.ForEach(n =>
                        {
                            var p = GetPpoint(n.name);
                            var x = p.x;
                            var y = p.y;
                            var length = 30;
                            img.Rectangle(new CvRect(x - (length / 2), y - (length / 2), length, length), CvColor.DodgerBlue, 4);
                        });
                    };
                    General.cam.FlagNgFrame = true;
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }
        }

        public static async Task<bool> CheckLum()
        {
            bool result = false;
            double refLum = 0;
            double errLum = 25;

            InitList();
            General.cam.ResetFlag();//カメラのフラグを初期化 リトライ時にフラグが初期化できてないとだめ
                                    //例 ＮＧリトライ時は、General.cam.FlagFrame = trueになっていてNGフレーム表示の無限ループにいる
            if (!LedAllOn)
            {
                await General.LedAllOn();
                await Task.Delay(3000);
                LedAllOn = true;
            }
            try
            {
                return result = await Task<bool>.Run(() =>
                {

                    General.cam.ResetFlag();//カメラのフラグを初期化 リトライ時にフラグが初期化できてないとだめ
                                            //例 ＮＧリトライ時は、General.cam.FlagFrame = trueになっていてNGフレーム表示の無限ループにいる

                    General.cam.FlagLabeling = true;
                    Thread.Sleep(1000);
                    var blobInfo = General.cam.blobs.Clone();


                    //LED1～8の座標を抽出する（画面半分より上側のブロブを抽出する）
                    var blobLed1_8 = blobInfo.Where(b => b.Value.Centroid.Y < 180).OrderByDescending(b => b.Value.Centroid.X).ToList();

                    //LED9～16の座標を抽出する（画面半分より下側のブロブを抽出する）
                    var blobLed9_16 = blobInfo.Where(b => b.Value.Centroid.Y > 180).OrderByDescending(b => b.Value.Centroid.X).ToList();

                    ListLedSpec.ForEach(l =>
                    {
                        var p = GetPpoint(l.name);
                        var x = p.x;
                        var y = p.y;

                        refLum = GetRefLum(l.name);

                        var blob = new List<KeyValuePair<int, CvBlob>>();
                        switch (l.name)
                        {
                            case NAME.LED1:
                            case NAME.LED2:
                            case NAME.LED3:
                            case NAME.LED4:
                            case NAME.LED5:
                            case NAME.LED6:
                            case NAME.LED7:
                            case NAME.LED8:
                                blob = blobLed1_8;
                                break;
                            default:
                                blob = blobLed9_16;
                                break;
                        }


                        var match = blob.FirstOrDefault(b =>
                        {
                            var pointX = b.Value.Centroid.X;
                            var pointY = b.Value.Centroid.Y;
                            //X座標の確認
                            if ((int)pointX < x - 10 || (int)pointX > x + 10) return false;

                            //Y座標の確認
                            if ((int)pointY < y - 10 || (int)pointY > y + 10) return false;

                            return true;
                        });

                        var area = match.Equals(default(KeyValuePair<int, CvBlob>)) ? 0 : match.Value.Area;

                        l.Area = area;
                        //面積の計算
                        l.resultArea = area >= refLum * (1 - (errLum / 100.0)) && area <= refLum * (1 + (errLum / 100.0));
                        //ビューモデルの更新
                        SetViewModelLum(l.name, area, l.resultArea);

                    });

                    return ListLedSpec.All(l => l.resultArea);
                });
            }
            finally
            {
                if (!result)
                {

                    LedAllOn = false;
                    General.cam.ResetFlag();
                    General.cam.MakeNgFrame = (img) =>
                    {
                        //リストからNGの座標を抽出する
                        var NgList = ListLedSpec.Where(l => !l.resultArea).ToList();
                        NgList.ForEach(n =>
                        {
                            var p = GetPpoint(n.name);
                            var x = p.x;
                            var y = p.y;
                            var length = 30;
                            img.Rectangle(new CvRect(x - (length / 2), y - (length / 2), length, length), CvColor.DodgerBlue, 4);
                        });
                    };
                    General.cam.FlagNgFrame = true;
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                }
            }
        }

    }

}









