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

        public static CameraPropertyForCn CamPropCn { get; set; }
        public static CameraPropertyForLed CamPropLed { get; set; }
        public static LedProperty LedProp { get; set; }
        public static CnProperty CnProp { get; set; }

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

            //カメラプロパティファイルのロード
            CamPropCn = Deserialize<CameraPropertyForCn>(Constants.filePath_CameraPropertyForCn);
            CamPropLed = Deserialize<CameraPropertyForLed>(Constants.filePath_CameraPropertyForLed);

            //LEDプロパティファイルのロード
            LedProp = Deserialize<LedProperty>(Constants.filePath_LedProperty);

            //コネクタプロパティファイルのロード
            CnProp = Deserialize<CnProperty>(Constants.filePath_CnProperty);

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

                //Camプロパティの保存
                Serialization<CameraPropertyForCn>(State.CamPropCn, Constants.filePath_CameraPropertyForCn);
                Serialization<CameraPropertyForLed>(State.CamPropLed, Constants.filePath_CameraPropertyForLed);

                //Ledプロパティの保存
                Serialization<LedProperty>(State.LedProp, Constants.filePath_LedProperty);

                //Cnプロパティの保存
                Serialization<CnProperty>(State.CnProp, Constants.filePath_CnProperty);

                return true;
            }
            catch
            {
                return false;

            }

        }

        public static void SetCamPropForCn()
        {
            General.cam.Opening = CamPropCn.Opening;
            General.cam.openCnt = CamPropCn.OpenCnt;
            General.cam.closeCnt = CamPropCn.CloseCnt;

            General.cam.Brightness = CamPropCn.Brightness;
            General.cam.Contrast = CamPropCn.Contrast;
            General.cam.Hue = CamPropCn.Hue;
            General.cam.Saturation = CamPropCn.Saturation;
            General.cam.Sharpness = CamPropCn.Sharpness;
            General.cam.Gamma = CamPropCn.Gamma;
            General.cam.Gain = CamPropCn.Gain;
            General.cam.Exposure = CamPropCn.Exposure;
            General.cam.Wb = CamPropCn.Whitebalance;
            General.cam.Theta = CamPropCn.Theta;
            General.cam.BinLevel = CamPropCn.BinLevel;
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
            VmLedPoint.LED1 = LedProp.Led1;
            VmLedPoint.LED2 = LedProp.Led2;
            VmLedPoint.LED3 = LedProp.Led3;
            VmLedPoint.LED4 = LedProp.Led4;
            VmLedPoint.LED5 = LedProp.Led5;
            VmLedPoint.LED6 = LedProp.Led6;
            VmLedPoint.LED7 = LedProp.Led7;
            VmLedPoint.LED8 = LedProp.Led8;
            VmLedPoint.LED9 = LedProp.Led9;
            VmLedPoint.LED10 = LedProp.Led10;
            VmLedPoint.LED11 = LedProp.Led11;
            VmLedPoint.LED12 = LedProp.Led12;
            VmLedPoint.LED13 = LedProp.Led13;
            VmLedPoint.LED14 = LedProp.Led14;
            VmLedPoint.LED15 = LedProp.Led15;
            VmLedPoint.LED16 = LedProp.Led16;

            VmCnPoint.X_Cn220 = CnProp.X_Cn220;
            VmCnPoint.Y_Cn220 = CnProp.Y_Cn220;
            VmCnPoint.W_Cn220 = CnProp.W_Cn220;
            VmCnPoint.H_Cn220 = CnProp.H_Cn220;

            VmCnPoint.X_Cn223 = CnProp.X_Cn223;
            VmCnPoint.Y_Cn223 = CnProp.Y_Cn223;
            VmCnPoint.W_Cn223 = CnProp.W_Cn223;
            VmCnPoint.H_Cn223 = CnProp.H_Cn223;

            VmCnPoint.X_Cn224 = CnProp.X_Cn224;
            VmCnPoint.Y_Cn224 = CnProp.Y_Cn224;
            VmCnPoint.W_Cn224 = CnProp.W_Cn224;
            VmCnPoint.H_Cn224 = CnProp.H_Cn224;

            VmCnPoint.X_Cn225 = CnProp.X_Cn225;
            VmCnPoint.Y_Cn225 = CnProp.Y_Cn225;
            VmCnPoint.W_Cn225 = CnProp.W_Cn225;
            VmCnPoint.H_Cn225 = CnProp.H_Cn225;

            VmCnPoint.X_Cn226 = CnProp.X_Cn226;
            VmCnPoint.Y_Cn226 = CnProp.Y_Cn226;
            VmCnPoint.W_Cn226 = CnProp.W_Cn226;
            VmCnPoint.H_Cn226 = CnProp.H_Cn226;

            VmCnPoint.X_Jp1 = CnProp.X_Jp1;
            VmCnPoint.Y_Jp1 = CnProp.Y_Jp1;
            VmCnPoint.W_Jp1 = CnProp.W_Jp1;
            VmCnPoint.H_Jp1 = CnProp.H_Jp1;

        }

    }

}
