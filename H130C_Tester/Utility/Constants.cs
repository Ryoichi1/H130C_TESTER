
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

        public static readonly string filePath_TestSpec        = @"C:\H130C\ConfigData\TestSpec.config";
        public static readonly string filePath_Configuration   = @"C:\H130C\ConfigData\Configuration.config";
        public static readonly string filePath_Command         = @"C:\H130C\ConfigData\Command.config";
        public static readonly string filePath_CameraPropertyForCn  = @"C:\H130C\ConfigData\CameraPropertyForCn.config";
        public static readonly string filePath_CameraPropertyForLed  = @"C:\H130C\ConfigData\CameraPropertyForLed.config";
        public static readonly string filePath_LedProperty     = @"C:\H130C\ConfigData\LedProperty.config";
        public static readonly string filePath_CnProperty      = @"C:\H130C\ConfigData\CnProperty.config";
        public static readonly string filePath_Cam1CalFilePath = @"C:\H130C\ConfigData\AN170600260.xml";

        public static readonly string filePath_TemplateCn220 = @"C:\H130C\Pic\tempCn220.bmp";
        public static readonly string filePath_TemplateCn223 = @"C:\H130C\Pic\tempCn223.bmp";
        public static readonly string filePath_TemplateCn224 = @"C:\H130C\Pic\tempCn224.bmp";
        public static readonly string filePath_TemplateCn225 = @"C:\H130C\Pic\tempCn225.bmp";
        public static readonly string filePath_TemplateCn226 = @"C:\H130C\Pic\tempCn226.bmp";
        public static readonly string filePath_TemplateJp1   = @"C:\H130C\Pic\tempJp1.bmp";


        public static readonly string RwsPath_Test    = @"C:\H130C\FW_WRITE\ForTest\R5F2123C_WRITE\R5F2123C_WRITE.AWS";
        public static readonly string RwsPath_Product = @"C:\H130C\FW_WRITE\ForProduct\R5F2123C_WRITE\R5F2123C_WRITE.AWS";

        public static readonly string Path_Manual = @"C:\H130C\Manual.pdf";

        //検査データフォルダのパス
        public static readonly string PassDataFolderPath = @"C:\H130C\検査データ\合格品データ\";
        public static readonly string FailDataFolderPath = @"C:\H130C\検査データ\不良品データ\";
        public static readonly string fileName_RetryLog  = @"C:\H130C\検査データ\不良品データ\" + "リトライ履歴.txt";

        //Imageの透明度
        public const double OpacityImgMin = 0.0;

        //リトライ回数
        public static readonly int RetryCount = 0;












    }
}
