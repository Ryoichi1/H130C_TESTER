
namespace H130C_Tester
{
    public class TestSpec
    {
        //テストスペックVER
        public string TestSpecVer { get; set; }

        //ファームウェア
        public string FwVer { get; set; }
        public string FwSum { get; set; }

        //電源電圧5Vの規格
        public double VccMax { get; set; }
        public double VccMin { get; set; }

        //リレー接点出力（10msec）の規格
        public double RelayOnTimeMilliSecMax { get; set; }
        public double RelayOnTimeMilliSecMin { get; set; }
        
        //フォトカプラ接点入力（10msec）の規格
        public double PhotoInTimeMilliSecMax { get; set; }
        public double PhotoInTimeMilliSecMin { get; set; }

        //LED 色相
        public int RedHueMax { get; set; }
        public int RedHueMin { get; set; }

        public int GreenHueMax { get; set; }
        public int GreenHueMin { get; set; }
        
        //コネクタ画像検査 相関係数Min
        public double MatchMin { get; set; }

    }
}
