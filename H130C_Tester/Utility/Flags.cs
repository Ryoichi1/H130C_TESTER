using System.Windows.Media;
using static H130C_Tester.General;

namespace H130C_Tester
{
    public static class Flags
    {
        public static bool OtherPage { get; set; }

        //試験開始時に初期化が必要なフラグ
        public static bool StopInit周辺機器 { get; set; }
        public static bool Initializing周辺機器 { get; set; }
        public static bool EnableTestStart { get; set; }
        public static bool Testing { get; set; }
        public static bool PowOn { get; set; }//メイン電源ON/OFF
        public static bool ShowErrInfo { get; set; }
        public static bool AddDecision { get; set; }


        public static bool ClickStopButton { get; set; }
        public static bool Click確認Button { get; set; }

        public static bool PressOpenCheckBeforeTest { get; set; }

        public static bool EnableStartCheck
        {
            get
            {
                return EnableConfCnPage && EnableConfLedPage && EnableMaintePage;
            }
        }
        public static bool EnableConfCnPage { get; set; }
        public static bool EnableConfLedPage { get; set; }
        public static bool EnableMaintePage { get; set; }

        private static SolidColorBrush RetryPanelBrush = new SolidColorBrush();
        private static SolidColorBrush StatePanelOkBrush = new SolidColorBrush();
        private static SolidColorBrush StatePanelNgBrush = new SolidColorBrush();
        private const double StatePanelOpacity = 0.3;

        static Flags()//コンストラクタ
        {
            RetryPanelBrush.Color = Colors.DodgerBlue;
            RetryPanelBrush.Opacity = StatePanelOpacity;

            StatePanelOkBrush.Color = Colors.DodgerBlue;
            StatePanelOkBrush.Opacity = StatePanelOpacity;

            StatePanelNgBrush.Color = Colors.DeepPink;
            StatePanelNgBrush.Opacity = StatePanelOpacity;
        }

        //例外ステータス
        private static bool _throwException;
        public static bool ThrowException
        {
            get { return _throwException; }
            set
            {
                _throwException = value;
                State.VmTestStatus.Color1768 = value ? StatePanelNgBrush : Brushes.Transparent;
            }
        }

        //周辺機器ステータス
        private static bool _State1768;
        public static bool State1768
        {
            get { return _State1768; }
            set
            {
                _State1768 = value;
                State.VmTestStatus.Color1768 = value ? StatePanelOkBrush : StatePanelNgBrush;
            }
        }

        private static bool _StateCam;
        public static bool StateCam
        {
            get { return _StateCam; }
            set
            {
                _StateCam = value;
                State.VmTestStatus.ColorCam = value ? OnBrush : NgBrush;
            }
        }



        private static bool _Retry;
        public static bool Retry
        {
            get { return _Retry; }
            set
            {
                _Retry = value;
                State.VmTestStatus.RetryLabelVis = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            }
        }


        public static bool AllOk周辺機器接続 { get; set; }

    }
}
