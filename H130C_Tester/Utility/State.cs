using System;
using System.Collections.Generic;

namespace H130C_Tester
{

    public class TestSpecs
    {
        public int Key;
        public string Value;
        public bool PowSw;

        public TestSpecs(int key, string value, bool powSW = true)
        {
            this.Key = key;
            this.Value = value;
            this.PowSw = powSW;

        }
    }

    public static class State
    {

        //データソース（バインディング用）
        public static ViewModelMainWindow VmMainWindow = new ViewModelMainWindow();
        public static ViewModelTestStatus VmTestStatus = new ViewModelTestStatus();
        public static ViewModelTestResult VmTestResults = new ViewModelTestResult();
        public static TestCommand testCommand = new TestCommand();
        public static ViewModelLed VmLedPoint = new ViewModelLed();
        public static ViewModelCn VmCnPoint = new ViewModelCn();

        public static ViewModelCommunication MainComm = new ViewModelCommunication();
        public static ViewModelCommunication SubComm = new ViewModelCommunication();

        //パブリックメンバ
        public static Configuration Setting { get; set; }
        public static TestSpec TestSpec { get; set; }
        public static Command Command { get; set; }

        public static CameraPropertyForCn CamPropCn220 { get; set; }
        public static CameraPropertyForCn CamPropCn223 { get; set; }
        public static CameraPropertyForCn CamPropCn224 { get; set; }
        public static CameraPropertyForCn CamPropCn225 { get; set; }
        public static CameraPropertyForCn CamPropCn226 { get; set; }
        public static CameraPropertyForCn CamPropJp1 { get; set; }

        public static CameraPropertyForLed CamPropLed { get; set; }

        public static string CurrDir { get; set; }

        public static string AssemblyInfo { get; set; }

        public static double CurrentThemeOpacity { get; set; }

        public static Uri uriOtherInfoPage { get; set; }


        //リトライ履歴保存用リスト
        public static List<string> RetryLogList = new List<string>();


        public static List<TestSpecs> テスト項目 = new List<TestSpecs>()
        {
            new TestSpecs(100, "コネクタ実装チェック", true),
            new TestSpecs(101, "コネクタ未半田チェック", true),

            new TestSpecs(200, "電源電圧チェック", true),

            new TestSpecs(300, "検査ソフト書き込み", false),

            new TestSpecs(400, "SW1検査", false),

            new TestSpecs(500, "IOポート検査（入力）", true),
            new TestSpecs(501, "IOポート検査（出力）", true),

            new TestSpecs(600, "フォトカプラ 接点入力（10msec）", true),
            new TestSpecs(601, "フォトカプラ 接点入力2", true),

            new TestSpecs(700, "リレー 接点出力（10msec）", true),
            new TestSpecs(701, "リレー 接点出力2", true),

            new TestSpecs(800, "LEDカラーチェック", true),
            new TestSpecs(801, "LED輝度チェック", true),

            new TestSpecs(900, "製品ソフト書き込み", false),
        };

        //個別設定のロード
        public static void LoadConfigData()
        {
            //Configファイルのロード
            Setting = Deserialize<Configuration>(Constants.filePath_Configuration);


            VmMainWindow.ListOperator = Setting.作業者リスト;
            VmMainWindow.Theme = Setting.PathTheme;
            VmMainWindow.ThemeOpacity = Setting.OpacityTheme;

            if (Setting.日付 != DateTime.Now.ToString("yyyyMMdd"))
            {
                Setting.日付 = DateTime.Now.ToString("yyyyMMdd");
                Setting.TodayOkCount = 0;
                Setting.TodayNgCount = 0;
            }

            VmTestStatus.OkCount = Setting.TodayOkCount.ToString() + "台";
            VmTestStatus.NgCount = Setting.TodayNgCount.ToString() + "台";

            //TestSpecファイルのロード
            TestSpec = Deserialize<TestSpec>(Constants.filePath_TestSpec);

            //Commandファイルのロード
            Command = Deserialize<Command>(Constants.filePath_Command);

            //コネクタ画像検査 カメラプロパティファイルのロード
            CamPropCn220 = Deserialize<CameraPropertyForCn>(Constants.filePath_CamPropCn220);
            CamPropCn223 = Deserialize<CameraPropertyForCn>(Constants.filePath_CamPropCn223);
            CamPropCn224 = Deserialize<CameraPropertyForCn>(Constants.filePath_CamPropCn224);
            CamPropCn225 = Deserialize<CameraPropertyForCn>(Constants.filePath_CamPropCn225);
            CamPropCn226 = Deserialize<CameraPropertyForCn>(Constants.filePath_CamPropCn226);
            CamPropJp1 = Deserialize<CameraPropertyForCn>(Constants.filePath_CamPropJp1);

            //LEDプロパティファイルのロード
            CamPropLed = Deserialize<CameraPropertyForLed>(Constants.filePath_CameraPropertyForLed);

        }


        //インスタンスをXMLデータに変換する
        public static bool Serialization<T>(T obj, string xmlFilePath)
        {
            try
            {
                //XmlSerializerオブジェクトを作成
                //オブジェクトの型を指定する
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(T));
                //書き込むファイルを開く（UTF-8 BOM無し）
                System.IO.StreamWriter sw = new System.IO.StreamWriter(xmlFilePath, false, new System.Text.UTF8Encoding(false));
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, obj);
                //ファイルを閉じる
                sw.Close();

                return true;

            }
            catch
            {
                return false;
            }

        }

        //XMLデータからインスタンスを生成する
        public static T Deserialize<T>(string xmlFilePath)
        {
            System.Xml.Serialization.XmlSerializer serializer;
            using (var sr = new System.IO.StreamReader(xmlFilePath, new System.Text.UTF8Encoding(false)))
            {
                serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

        //********************************************************
        //個別設定データの保存
        //********************************************************
        public static bool Save個別データ()
        {
            try
            {
                //Configファイルの保存
                Setting.作業者リスト = VmMainWindow.ListOperator;
                Setting.PathTheme = VmMainWindow.Theme;
                Setting.OpacityTheme = VmMainWindow.ThemeOpacity;

                Serialization<Configuration>(Setting, Constants.filePath_Configuration);

                //コネクタ画像検査 Camプロパティの保存
                Serialization<CameraPropertyForCn>(State.CamPropCn220, Constants.filePath_CamPropCn220);
                Serialization<CameraPropertyForCn>(State.CamPropCn223, Constants.filePath_CamPropCn223);
                Serialization<CameraPropertyForCn>(State.CamPropCn224, Constants.filePath_CamPropCn224);
                Serialization<CameraPropertyForCn>(State.CamPropCn225, Constants.filePath_CamPropCn225);
                Serialization<CameraPropertyForCn>(State.CamPropCn226, Constants.filePath_CamPropCn226);
                Serialization<CameraPropertyForCn>(State.CamPropJp1, Constants.filePath_CamPropJp1);

                //Ledプロパティの保存
                Serialization<CameraPropertyForLed>(State.CamPropLed, Constants.filePath_CameraPropertyForLed);

                return true;
            }
            catch
            {
                return false;

            }

        }

        public enum CN_NAME { CN220, CN223, CN224, CN225, CN226, JP1 }
        public static void SetCamPropForCn(CN_NAME name)
        {
            switch (name)
            {
                case CN_NAME.CN220:
                    General.cam.BinLevel = CamPropCn220.BinLevel;
                    General.cam.Opening = CamPropCn220.Opening;
                    General.cam.openCnt = CamPropCn220.OpenCnt;
                    General.cam.closeCnt = CamPropCn220.CloseCnt;

                    General.cam.Brightness = CamPropCn220.Brightness;
                    General.cam.Contrast = CamPropCn220.Contrast;
                    General.cam.Hue = CamPropCn220.Hue;
                    General.cam.Saturation = CamPropCn220.Saturation;
                    General.cam.Sharpness = CamPropCn220.Sharpness;
                    General.cam.Gamma = CamPropCn220.Gamma;
                    General.cam.Gain = CamPropCn220.Gain;
                    General.cam.Exposure = CamPropCn220.Exposure;
                    General.cam.Wb = CamPropCn220.Whitebalance;
                    General.cam.Theta = CamPropCn220.Theta;
                    break;
                case CN_NAME.CN223:
                    General.cam.BinLevel = CamPropCn223.BinLevel;
                    General.cam.Opening = CamPropCn223.Opening;
                    General.cam.openCnt = CamPropCn223.OpenCnt;
                    General.cam.closeCnt = CamPropCn223.CloseCnt;

                    General.cam.Brightness = CamPropCn223.Brightness;
                    General.cam.Contrast = CamPropCn223.Contrast;
                    General.cam.Hue = CamPropCn223.Hue;
                    General.cam.Saturation = CamPropCn223.Saturation;
                    General.cam.Sharpness = CamPropCn223.Sharpness;
                    General.cam.Gamma = CamPropCn223.Gamma;
                    General.cam.Gain = CamPropCn223.Gain;
                    General.cam.Exposure = CamPropCn223.Exposure;
                    General.cam.Wb = CamPropCn223.Whitebalance;
                    General.cam.Theta = CamPropCn223.Theta;
                    break;
                case CN_NAME.CN224:
                    General.cam.BinLevel = CamPropCn224.BinLevel;
                    General.cam.Opening = CamPropCn224.Opening;
                    General.cam.openCnt = CamPropCn224.OpenCnt;
                    General.cam.closeCnt = CamPropCn224.CloseCnt;

                    General.cam.Brightness = CamPropCn224.Brightness;
                    General.cam.Contrast = CamPropCn224.Contrast;
                    General.cam.Hue = CamPropCn224.Hue;
                    General.cam.Saturation = CamPropCn224.Saturation;
                    General.cam.Sharpness = CamPropCn224.Sharpness;
                    General.cam.Gamma = CamPropCn224.Gamma;
                    General.cam.Gain = CamPropCn224.Gain;
                    General.cam.Exposure = CamPropCn224.Exposure;
                    General.cam.Wb = CamPropCn224.Whitebalance;
                    General.cam.Theta = CamPropCn224.Theta;
                    break;
                case CN_NAME.CN225:
                    General.cam.BinLevel = CamPropCn225.BinLevel;
                    General.cam.Opening = CamPropCn225.Opening;
                    General.cam.openCnt = CamPropCn225.OpenCnt;
                    General.cam.closeCnt = CamPropCn225.CloseCnt;

                    General.cam.Brightness = CamPropCn225.Brightness;
                    General.cam.Contrast = CamPropCn225.Contrast;
                    General.cam.Hue = CamPropCn225.Hue;
                    General.cam.Saturation = CamPropCn225.Saturation;
                    General.cam.Sharpness = CamPropCn225.Sharpness;
                    General.cam.Gamma = CamPropCn225.Gamma;
                    General.cam.Gain = CamPropCn225.Gain;
                    General.cam.Exposure = CamPropCn225.Exposure;
                    General.cam.Wb = CamPropCn225.Whitebalance;
                    General.cam.Theta = CamPropCn225.Theta;
                    break;
                case CN_NAME.CN226:
                    General.cam.BinLevel = CamPropCn226.BinLevel;
                    General.cam.Opening = CamPropCn226.Opening;
                    General.cam.openCnt = CamPropCn226.OpenCnt;
                    General.cam.closeCnt = CamPropCn226.CloseCnt;

                    General.cam.Brightness = CamPropCn226.Brightness;
                    General.cam.Contrast = CamPropCn226.Contrast;
                    General.cam.Hue = CamPropCn226.Hue;
                    General.cam.Saturation = CamPropCn226.Saturation;
                    General.cam.Sharpness = CamPropCn226.Sharpness;
                    General.cam.Gamma = CamPropCn226.Gamma;
                    General.cam.Gain = CamPropCn226.Gain;
                    General.cam.Exposure = CamPropCn226.Exposure;
                    General.cam.Wb = CamPropCn226.Whitebalance;
                    General.cam.Theta = CamPropCn226.Theta;
                    break;
                case CN_NAME.JP1:
                    General.cam.BinLevel = CamPropJp1.BinLevel;
                    General.cam.Opening = CamPropJp1.Opening;
                    General.cam.openCnt = CamPropJp1.OpenCnt;
                    General.cam.closeCnt = CamPropJp1.CloseCnt;

                    General.cam.Brightness = CamPropJp1.Brightness;
                    General.cam.Contrast = CamPropJp1.Contrast;
                    General.cam.Hue = CamPropJp1.Hue;
                    General.cam.Saturation = CamPropJp1.Saturation;
                    General.cam.Sharpness = CamPropJp1.Sharpness;
                    General.cam.Gamma = CamPropJp1.Gamma;
                    General.cam.Gain = CamPropJp1.Gain;
                    General.cam.Exposure = CamPropJp1.Exposure;
                    General.cam.Wb = CamPropJp1.Whitebalance;
                    General.cam.Theta = CamPropJp1.Theta;
                    break;
            }

        }

        public static void SetCamPropForLed()
        {
            General.cam.Opening = CamPropLed.Opening;
            General.cam.openCnt = CamPropLed.OpenCnt;
            General.cam.closeCnt = CamPropLed.CloseCnt;

            General.cam.Brightness = CamPropLed.Brightness;
            General.cam.Contrast = CamPropLed.Contrast;
            General.cam.Hue = CamPropLed.Hue;
            General.cam.Saturation = CamPropLed.Saturation;
            General.cam.Sharpness = CamPropLed.Sharpness;
            General.cam.Gamma = CamPropLed.Gamma;
            General.cam.Gain = CamPropLed.Gain;
            General.cam.Exposure = CamPropLed.Exposure;
            General.cam.Wb = CamPropLed.Whitebalance;
            General.cam.Theta = CamPropLed.Theta;
            General.cam.BinLevel = CamPropLed.BinLevel;
        }

        public static void SetCamPoint()
        {
            //VmLedPoint.LED1 = CamPropLed.Led1;
            //VmLedPoint.LED2 = CamPropLed.Led2;
            //VmLedPoint.LED3 = CamPropLed.Led3;
            //VmLedPoint.LED4 = CamPropLed.Led4;
            //VmLedPoint.LED5 = CamPropLed.Led5;
            //VmLedPoint.LED6 = CamPropLed.Led6;
            //VmLedPoint.LED7 = CamPropLed.Led7;
            //VmLedPoint.LED8 = CamPropLed.Led8;
            //VmLedPoint.LED9 = CamPropLed.Led9;
            //VmLedPoint.LED10 = CamPropLed.Led10;
            //VmLedPoint.LED11 = CamPropLed.Led11;
            //VmLedPoint.LED12 = CamPropLed.Led12;
            //VmLedPoint.LED13 = CamPropLed.Led13;
            //VmLedPoint.LED14 = CamPropLed.Led14;
            //VmLedPoint.LED15 = CamPropLed.Led15;
            //VmLedPoint.LED16 = CamPropLed.Led16;

            VmCnPoint.X_Cn220 = CamPropCn220.X;
            VmCnPoint.Y_Cn220 = CamPropCn220.Y;
            VmCnPoint.W_Cn220 = CamPropCn220.W;
            VmCnPoint.H_Cn220 = CamPropCn220.H;

            VmCnPoint.X_Cn223 = CamPropCn223.X;
            VmCnPoint.Y_Cn223 = CamPropCn223.Y;
            VmCnPoint.W_Cn223 = CamPropCn223.W;
            VmCnPoint.H_Cn223 = CamPropCn223.H;

            VmCnPoint.X_Cn224 = CamPropCn224.X;
            VmCnPoint.Y_Cn224 = CamPropCn224.Y;
            VmCnPoint.W_Cn224 = CamPropCn224.W;
            VmCnPoint.H_Cn224 = CamPropCn224.H;

            VmCnPoint.X_Cn225 = CamPropCn225.X;
            VmCnPoint.Y_Cn225 = CamPropCn225.Y;
            VmCnPoint.W_Cn225 = CamPropCn225.W;
            VmCnPoint.H_Cn225 = CamPropCn225.H;

            VmCnPoint.X_Cn226 = CamPropCn226.X;
            VmCnPoint.Y_Cn226 = CamPropCn226.Y;
            VmCnPoint.W_Cn226 = CamPropCn226.W;
            VmCnPoint.H_Cn226 = CamPropCn226.H;

            VmCnPoint.X_Jp1 = CamPropJp1.X;
            VmCnPoint.Y_Jp1 = CamPropJp1.Y;
            VmCnPoint.W_Jp1 = CamPropJp1.W;
            VmCnPoint.H_Jp1 = CamPropJp1.H;
        }

    }

}
