
namespace H130C_Tester
{
    public static class Constants
    {
        //スタートボタンの表示
        public const string 開始 = "検査開始";
        public const string 停止 = "強制停止";
        public const string 確認 = "確認";

        //作業者へのメッセージ
        public const string MessOpecode = "工番を入力してください";
        public const string MessOperator = "作業者名を選択してください";
        public const string MessSet = "製品をセットしてレバーを下げてください";
        public const string MessRemove = "製品を取り外してください";

        public const string MessWait = "検査中！　しばらくお待ちください・・・";
        public const string MessCheckConnectMachine = "周辺機器の接続を確認してください！";

        public static readonly string RootPath = State.MachineName == "TSPCDP00059" ? @"D:\試験機用設定ファイル\H130C" : @"C:\H130C";
        public static readonly string filePath_TestSpec = $@"{RootPath}\ConfigData\TestSpec.config";
        public static readonly string filePath_Configuration = $@"{RootPath}\ConfigData\Configuration.config";
        public static readonly string filePath_Command = $@"{RootPath}\ConfigData\Command.config";
        public static readonly string filePath_CamPropCn220 = $@"{RootPath}\ConfigData\CamPropCn220.config";
        public static readonly string filePath_CamPropCn223 = $@"{RootPath}\ConfigData\CamPropCn223.config";
        public static readonly string filePath_CamPropCn224 = $@"{RootPath}\ConfigData\CamPropCn224.config";
        public static readonly string filePath_CamPropCn225 = $@"{RootPath}\ConfigData\CamPropCn225.config";
        public static readonly string filePath_CamPropCn226 = $@"{RootPath}\ConfigData\CamPropCn226.config";
        public static readonly string filePath_CamPropJp1 = $@"{RootPath}\ConfigData\CamPropJp1.config";
        public static readonly string filePath_CameraPropertyForLed = $@"{RootPath}\ConfigData\CameraPropertyForLed.config";
        public static readonly string filePath_LedProperty = $@"{RootPath}\ConfigData\LedProperty.config";
        public static readonly string filePath_CnProperty = $@"{RootPath}\ConfigData\CnProperty.config";
        public static readonly string filePath_Cam1CalFilePath = $@"{RootPath}\ConfigData\AN170600260.xml";

        public static readonly string filePath_TemplateCn220 = $@"{RootPath}\Pic\tempCn220.bmp";
        public static readonly string filePath_TemplateCn223 = $@"{RootPath}\Pic\tempCn223.bmp";
        public static readonly string filePath_TemplateCn224 = $@"{RootPath}\Pic\tempCn224.bmp";
        public static readonly string filePath_TemplateCn225 = $@"{RootPath}\Pic\tempCn225.bmp";
        public static readonly string filePath_TemplateCn226 = $@"{RootPath}\Pic\tempCn226.bmp";
        public static readonly string filePath_TemplateJp1 = $@"{RootPath}\Pic\tempJp1.bmp";


        public static readonly string RwsPath_Test = $@"{RootPath}\FW_WRITE\ForTest\R5F2123C_WRITE\R5F2123C_WRITE.AWS";
        public static readonly string RwsPath_Product = $@"{RootPath}\FW_WRITE\ForProduct\R5F2123C_WRITE\R5F2123C_WRITE.AWS";

        public static readonly string Path_Manual = $@"{RootPath}\Manual.pdf";

        //検査データフォルダのパス
        public static readonly string PassDataFolderPath = $@"{RootPath}\検査データ\合格品データ\";
        public static readonly string FailDataFolderPath = $@"{RootPath}\検査データ\不良品データ\";
        public static readonly string fileName_RetryLog = $@"{RootPath}\検査データ\不良品データ\" + "リトライ履歴.txt";

        //Imageの透明度
        public const double OpacityImgMin = 0.0;

        //リトライ回数
        public static readonly int RetryCount = 1;












    }
}
