using System;
using System.Windows;
using System.Windows.Navigation;

namespace H130C_Tester
{
    /// <summary>
    /// Config.xaml の相互作用ロジック
    /// </summary>
    public partial class Conf
    {
        private NavigationService naviMente;
        private NavigationService naviCameraLed;
        private NavigationService naviCameraCn;
        private NavigationService naviTheme;
        Uri uriMentePage    = new Uri("Page/Config/Mente.xaml", UriKind.Relative);
        Uri uriCameraLedPage  = new Uri("Page/Config/CameraConfLed.xaml", UriKind.Relative);
        Uri uriCameraCnPage  = new Uri("Page/Config/CameraConf.xaml", UriKind.Relative);//TODO:
        Uri uriThemePage    = new Uri("Page/Config/Theme.xaml", UriKind.Relative);

        public Conf()
        {
            InitializeComponent();
            naviMente    = FrameMente.NavigationService;
            naviCameraLed  = FrameCameraLed.NavigationService;
            naviCameraCn  = FrameCameraCn.NavigationService;
            naviTheme    = FrameTheme.NavigationService;

            FrameMente.NavigationUIVisibility    = NavigationUIVisibility.Hidden;
            FrameCameraLed.NavigationUIVisibility  = NavigationUIVisibility.Hidden;
            FrameCameraCn.NavigationUIVisibility  = NavigationUIVisibility.Hidden;
            FrameTheme.NavigationUIVisibility    = NavigationUIVisibility.Hidden;

            TabMenu.SelectedIndex = 0;

            // オブジェクト作成に必要なコードをこの下に挿入します。
        }

        private void TabMente_Loaded(object sender, RoutedEventArgs e)
        {
            naviMente.Navigate(uriMentePage);
        }


        private void TabTheme_Loaded(object sender, RoutedEventArgs e)
        {
            naviTheme.Navigate(uriThemePage);
        }

        private void TabCameraLed_Loaded(object sender, RoutedEventArgs e)
        {
            naviCameraLed.Navigate(uriCameraLedPage);
        }

        private void TabCameraCn_Loaded(object sender, RoutedEventArgs e)
        {
            naviCameraCn.Navigate(uriCameraLedPage);
        }
    }
}
