

namespace H130C_Tester
{
    public class CameraPropertyForCn
    {
        //ノイズ処理
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

        //マスター画像の位置と大きさ
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public int Margin { get; set; }

        //照明設定
        public bool Light1On { get; set; }
        public bool Light2On { get; set; }
        public bool Light3On { get; set; }
    }
}
