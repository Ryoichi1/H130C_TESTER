using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;


namespace H130C_Tester
{
    ///<summary>
    ///●注意事項
    ///FDTは正規版をインストールすること
    /// ワークスペースファイルはシンプルインターフェイスで保存する
    ///
    /// </summary>

    public static class FDT
    {
        //********************************************************************************************************
        // 外部プロセスのメイン・ウィンドウを起動するためのWin32 API
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, StringBuilder lParam);

        private const int WM_GETTEXT = 0x000D;

        // ShowWindowAsync関数のパラメータに渡す定義値
        private const int SW_RESTORE = 9;  // 画面を元の大きさに戻す
        //********************************************************************************************************


        //静的メンバの宣言
        private static System.Timers.Timer Tm;
        private static string 出力パネルデータ;

        //フラグ
        private static bool FlagTimer;

        public static string FlashCheckSum { get; private set; }

        //コンストラクタ
        static FDT()
        {
            //タイマー（ウィンドウハンドル取得用）の設定
            Tm = new System.Timers.Timer();
            Tm.Enabled = false;
            Tm.Interval = 5000;
            Tm.Elapsed += new ElapsedEventHandler(tm_Tick);
        }

        //タイマーイベントハンドラ
        private static void tm_Tick(object source, ElapsedEventArgs e)
        {
            Tm.Stop();
            FlagTimer = false;//タイムアウト

        }


        public static async Task<bool> WriteFirmware(string AwsFilePath)
        {
            //フラグの初期化

            var result = await Task.Run<bool>(() =>
            {
                var Fdt = new Process();

                try
                {
                    //プロセスを作成してFDTを立ち上げる

                    Fdt.StartInfo.FileName = AwsFilePath;
                    Fdt.Start();
                    Fdt.WaitForInputIdle();//指定されたプロセスで未処理の入力が存在せず、ユーザーからの入力を待っている状態になるまで、またはタイムアウト時間が経過するまで待機します。

                    //FDTのウィンドウハンドル取得
                    FlagTimer = true;
                    Tm.Start();
                    IntPtr hWnd = IntPtr.Zero;//メインウィンドウのハンドル
                    while (hWnd == IntPtr.Zero)
                    {
                        Application.DoEvents();
                        if (!FlagTimer) return false;
                        hWnd = FindWindow(null, "FDT Simple Interface   (Supported Version)");
                    }

                    IntPtr ログテキストハンドル = FindWindowEx(hWnd, IntPtr.Zero, "RICHEDIT", "");

                    //FDTを最前面に表示してアクティブにする（センドキー送るため）
                    SetForegroundWindow(hWnd);
                    Thread.Sleep(1000);

                    SendKeys.SendWait("{TAB}");
                    Thread.Sleep(300);
                    SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(300);
                    SendKeys.SendWait("P");
                    Thread.Sleep(300);
                    SendKeys.SendWait("5");
                    Thread.Sleep(300);
                    SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(2000);
                    SendKeys.SendWait("{ENTER}");

                    int MaxSize = 4500;
                    StringBuilder sb = new StringBuilder(MaxSize);
                    出力パネルデータ = "";

                    while (true)
                    {
                        Thread.Sleep(1000);
                        //Task.Delay(1000);//インターバル1秒　※インターバル無しの場合FDTがこける
                        sb.Clear();
                        SendMessage(ログテキストハンドル, WM_GETTEXT, MaxSize - 1, sb);
                        出力パネルデータ = sb.ToString();
                        if (出力パネルデータ.IndexOf("Error") >= 0) return false;
                        if (出力パネルデータ.IndexOf("Disconnected") >= 0) return true;
                    }

                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (Fdt != null)
                    {
                        Fdt.Kill();
                        Fdt.Close();
                        Fdt.Dispose();
                    }
                }
            });

            return result;
            //E1エミュレータを切り離すする（クラス外部で処理する）

        }




        public static bool CheckSum(string sum)
        {
            var StartIndex = 出力パネルデータ.IndexOf("User Flash =");
            FlashCheckSum = 出力パネルデータ.Substring(StartIndex + 13, 10);
            return FlashCheckSum == sum;
        }








    }

}