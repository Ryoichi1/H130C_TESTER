﻿using System;
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
        private NavigationService naviCamera1;
        private NavigationService naviTheme;
        Uri uriMentePage    = new Uri("Page/Config/Mente.xaml", UriKind.Relative);
        Uri uriCamera1Page  = new Uri("Page/Config/CameraConf.xaml", UriKind.Relative);
        Uri uriThemePage    = new Uri("Page/Config/Theme.xaml", UriKind.Relative);

        public Conf()
        {
            InitializeComponent();
            naviMente    = FrameMente.NavigationService;
            naviCamera1  = FrameCamera1.NavigationService;
            naviTheme    = FrameTheme.NavigationService;

            FrameMente.NavigationUIVisibility    = NavigationUIVisibility.Hidden;
            FrameCamera1.NavigationUIVisibility  = NavigationUIVisibility.Hidden;
            FrameTheme.NavigationUIVisibility    = NavigationUIVisibility.Hidden;

            TabMenu.SelectedIndex = 0;

            // オブジェクト作成に必要なコードをこの下に挿入します。
        }

        private void TabMente_Loaded(object sender, RoutedEventArgs e)
        {
            naviMente.Navigate(uriMentePage);
        }

        private void TabCamera1_Loaded(object sender, RoutedEventArgs e)
        {
            naviCamera1.Navigate(uriCamera1Page);
        }


        private void TabTheme_Loaded(object sender, RoutedEventArgs e)
        {
            naviTheme.Navigate(uriThemePage);
        }


    }
}
