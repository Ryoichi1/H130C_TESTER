using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;



namespace H130C_Tester
{

    public class ViewModelMainWindow : BindableBase
    {
        //メンテナンス画面で、カメラ設定ページと別のページを高速に遷移させると、カメラがヌルポで死ぬ
        //カメラがdisposeしている最中は、別のカメラをスタートさせてはいけない
        //カメラ設定ページに遷移したら、1～1.5秒は他のページに遷移できないようにする
        private bool _MainWinEnable = true;
        public bool MainWinEnable
        {

            get { return _MainWinEnable; }
            set { SetProperty(ref _MainWinEnable, value); }
        }



        public ViewModelMainWindow()
        {
            SelectIndex = -1;
        }



        private string _Theme;
        public string Theme
        {
            get { return _Theme; }
            set { SetProperty(ref _Theme, value); }
        }


        private double _ThemeBlurEffectRadius;
        public double ThemeBlurEffectRadius
        {
            get { return _ThemeBlurEffectRadius; }
            set { SetProperty(ref _ThemeBlurEffectRadius, value); }
        }

        private double _ThemeOpacity;
        public double ThemeOpacity
        {
            get { return _ThemeOpacity; }
            set { SetProperty(ref _ThemeOpacity, value); }
        }

        private int _SelectIndex;
        public int SelectIndex
        {

            get { return _SelectIndex; }
            set { SetProperty(ref _SelectIndex, value); }

        }



        private bool _EnableOtherButton;
        public bool EnableOtherButton //MainWindowのタブコントロールの各TabItemのイネーブルにバインドする
        {
            get { return _EnableOtherButton; }
            set { SetProperty(ref _EnableOtherButton, value); }
        }


        private int _TabIndex;
        public int TabIndex
        {

            get { return _TabIndex; }
            set { SetProperty(ref _TabIndex, value); }

        }












    }
}
