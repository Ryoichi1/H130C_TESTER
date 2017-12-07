

namespace H130C_Tester
{
    public class CameraPropertyForLed
    {
        public int BinLevel { get; set; }
        public bool Opening { get; set; }//オープニング処理 or クロージング処理
        public int OpenCnt { get; set; }//クロージング処理時の拡張回数
        public int CloseCnt { get; set; }//クロージング処理時の収縮回数

        //カメラプロパティ
        public double Brightness { get; set; }
        public double Contrast { get; set; }
        public double Hue { get; set; }
        public double Saturation { get; set; }
        public double Sharpness { get; set; }
        public double Gamma { get; set; }
        public double Gain { get; set; }
        public double Exposure { get; set; }
        public int Whitebalance { get; set; }
        public double Theta { get; set; }


        //LEDの座標
        public string Led1 { get; set; }
        public string Led2 { get; set; }
        public string Led3 { get; set; }
        public string Led4 { get; set; }
        public string Led5 { get; set; }
        public string Led6 { get; set; }
        public string Led7 { get; set; }
        public string Led8 { get; set; }
        public string Led9 { get; set; }
        public string Led10 { get; set; }
        public string Led11 { get; set; }
        public string Led12 { get; set; }
        public string Led13 { get; set; }
        public string Led14 { get; set; }
        public string Led15 { get; set; }
        public string Led16 { get; set; }

        //LEDの輝度
        public double LumLed1 { get; set; }
        public double LumLed2 { get; set; }
        public double LumLed3 { get; set; }
        public double LumLed4 { get; set; }
        public double LumLed5 { get; set; }
        public double LumLed6 { get; set; }
        public double LumLed7 { get; set; }
        public double LumLed8 { get; set; }
        public double LumLed9 { get; set; }
        public double LumLed10 { get; set; }
        public double LumLed11 { get; set; }
        public double LumLed12 { get; set; }
        public double LumLed13 { get; set; }
        public double LumLed14 { get; set; }
        public double LumLed15 { get; set; }
        public double LumLed16 { get; set; }


    }
}
