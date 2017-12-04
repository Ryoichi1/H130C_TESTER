using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Media;

namespace H130C_Tester
{
    public class ViewModelLed : BindableBase
    {

        //LED座標
        private string _LED1;
        public string LED1 { get { return _LED1; } set { SetProperty(ref _LED1, value); } }

        private string _LED2;
        public string LED2 { get { return _LED2; } set { SetProperty(ref _LED2, value); } }

        private string _LED3;
        public string LED3 { get { return _LED3; } set { SetProperty(ref _LED3, value); } }

        private string _LED4;
        public string LED4 { get { return _LED4; } set { SetProperty(ref _LED4, value); } }

        private string _LED5;
        public string LED5 { get { return _LED5; } set { SetProperty(ref _LED5, value); } }

        private string _LED6;
        public string LED6 { get { return _LED6; } set { SetProperty(ref _LED6, value); } }

        private string _LED7;
        public string LED7 { get { return _LED7; } set { SetProperty(ref _LED7, value); } }

        private string _LED8;
        public string LED8 { get { return _LED8; } set { SetProperty(ref _LED8, value); } }

        private string _LED9;
        public string LED9 { get { return _LED9; } set { SetProperty(ref _LED9, value); } }

        private string _LED10;
        public string LED10 { get { return _LED10; } set { SetProperty(ref _LED10, value); } }

        private string _LED11;
        public string LED11 { get { return _LED11; } set { SetProperty(ref _LED11, value); } }

        private string _LED12;
        public string LED12 { get { return _LED12; } set { SetProperty(ref _LED12, value); } }

        private string _LED13;
        public string LED13 { get { return _LED13; } set { SetProperty(ref _LED13, value); } }

        private string _LED14;
        public string LED14 { get { return _LED14; } set { SetProperty(ref _LED14, value); } }

        private string _LED15;
        public string LED15 { get { return _LED15; } set { SetProperty(ref _LED15, value); } }

        private string _LED16;
        public string LED16 { get { return _LED16; } set { SetProperty(ref _LED16, value); } }

        //LED輝度
        private string _LED1Lum;
        public string LED1Lum { get { return _LED1Lum; } set { SetProperty(ref _LED1Lum, value); } }

        private string _LED2Lum;
        public string LED2Lum { get { return _LED2Lum; } set { SetProperty(ref _LED2Lum, value); } }

        private string _LED3Lum;
        public string LED3Lum { get { return _LED3Lum; } set { SetProperty(ref _LED3Lum, value); } }

        private string _LED4Lum;
        public string LED4Lum { get { return _LED4Lum; } set { SetProperty(ref _LED4Lum, value); } }

        private string _LED5Lum;
        public string LED5Lum { get { return _LED5Lum; } set { SetProperty(ref _LED5Lum, value); } }

        private string _LED6Lum;
        public string LED6Lum { get { return _LED6Lum; } set { SetProperty(ref _LED6Lum, value); } }

        private string _LED7Lum;
        public string LED7Lum { get { return _LED7Lum; } set { SetProperty(ref _LED7Lum, value); } }

        private string _LED8Lum;
        public string LED8Lum { get { return _LED8Lum; } set { SetProperty(ref _LED8Lum, value); } }

        private string _LED9Lum;
        public string LED9Lum { get { return _LED9Lum; } set { SetProperty(ref _LED9Lum, value); } }

        private string _LED10Lum;
        public string LED10Lum { get { return _LED10Lum; } set { SetProperty(ref _LED10Lum, value); } }

        private string _LED11Lum;
        public string LED11Lum { get { return _LED11Lum; } set { SetProperty(ref _LED11Lum, value); } }

        private string _LED12Lum;
        public string LED12Lum { get { return _LED12Lum; } set { SetProperty(ref _LED12Lum, value); } }

        private string _LED13Lum;
        public string LED13Lum { get { return _LED13Lum; } set { SetProperty(ref _LED13Lum, value); } }

        private string _LED14Lum;
        public string LED14Lum { get { return _LED14Lum; } set { SetProperty(ref _LED14Lum, value); } }

        private string _LED15Lum;
        public string LED15Lum { get { return _LED15Lum; } set { SetProperty(ref _LED15Lum, value); } }

        private string _LED16Lum;
        public string LED16Lum { get { return _LED16Lum; } set { SetProperty(ref _LED16Lum, value); } }

        //LED色相
        private string _LED1Hue;
        public string LED1Hue { get { return _LED1Hue; } set { SetProperty(ref _LED1Hue, value); } }

        private string _LED2Hue;
        public string LED2Hue { get { return _LED2Hue; } set { SetProperty(ref _LED2Hue, value); } }

        private string _LED3Hue;
        public string LED3Hue { get { return _LED3Hue; } set { SetProperty(ref _LED3Hue, value); } }

        private string _LED4Hue;
        public string LED4Hue { get { return _LED4Hue; } set { SetProperty(ref _LED4Hue, value); } }

        private string _LED5Hue;
        public string LED5Hue { get { return _LED5Hue; } set { SetProperty(ref _LED5Hue, value); } }

        private string _LED6Hue;
        public string LED6Hue { get { return _LED6Hue; } set { SetProperty(ref _LED6Hue, value); } }

        private string _LED7Hue;
        public string LED7Hue { get { return _LED7Hue; } set { SetProperty(ref _LED7Hue, value); } }

        private string _LED8Hue;
        public string LED8Hue { get { return _LED8Hue; } set { SetProperty(ref _LED8Hue, value); } }

        private string _LED9Hue;
        public string LED9Hue { get { return _LED9Hue; } set { SetProperty(ref _LED9Hue, value); } }

        private string _LED10Hue;
        public string LED10Hue { get { return _LED10Hue; } set { SetProperty(ref _LED10Hue, value); } }

        private string _LED11Hue;
        public string LED11Hue { get { return _LED11Hue; } set { SetProperty(ref _LED11Hue, value); } }

        private string _LED12Hue;
        public string LED12Hue { get { return _LED12Hue; } set { SetProperty(ref _LED12Hue, value); } }

        private string _LED13Hue;
        public string LED13Hue { get { return _LED13Hue; } set { SetProperty(ref _LED13Hue, value); } }

        private string _LED14Hue;
        public string LED14Hue { get { return _LED14Hue; } set { SetProperty(ref _LED14Hue, value); } }

        private string _LED15Hue;
        public string LED15Hue { get { return _LED15Hue; } set { SetProperty(ref _LED15Hue, value); } }

        private string _LED16Hue;
        public string LED16Hue { get { return _LED16Hue; } set { SetProperty(ref _LED16Hue, value); } }

        private Brush _ColLED1Hue;
        public Brush ColLED1Hue { get { return _ColLED1Hue; } set { SetProperty(ref _ColLED1Hue, value); } }
       
        private Brush _ColLED2Hue;
        public Brush ColLED2Hue { get { return _ColLED2Hue; } set { SetProperty(ref _ColLED2Hue, value); } }
       
        private Brush _ColLED3Hue;
        public Brush ColLED3Hue { get { return _ColLED3Hue; } set { SetProperty(ref _ColLED3Hue, value); } }
       
        private Brush _ColLED4Hue;
        public Brush ColLED4Hue { get { return _ColLED4Hue; } set { SetProperty(ref _ColLED4Hue, value); } }

        private Brush _ColLED5Hue;
        public Brush ColLED5Hue { get { return _ColLED5Hue; } set { SetProperty(ref _ColLED5Hue, value); } }
       
        private Brush _ColLED6Hue;
        public Brush ColLED6Hue { get { return _ColLED6Hue; } set { SetProperty(ref _ColLED6Hue, value); } }
       
        private Brush _ColLED7Hue;
        public Brush ColLED7Hue { get { return _ColLED7Hue; } set { SetProperty(ref _ColLED7Hue, value); } }
       
        private Brush _ColLED8Hue;
        public Brush ColLED8Hue { get { return _ColLED8Hue; } set { SetProperty(ref _ColLED8Hue, value); } }
       
        private Brush _ColLED9Hue;
        public Brush ColLED9Hue { get { return _ColLED9Hue; } set { SetProperty(ref _ColLED9Hue, value); } }
       
        private Brush _ColLED10Hue;
        public Brush ColLED10Hue { get { return _ColLED10Hue; } set { SetProperty(ref _ColLED10Hue, value); } }
       
        private Brush _ColLED11Hue;
        public Brush ColLED11Hue { get { return _ColLED11Hue; } set { SetProperty(ref _ColLED11Hue, value); } }
       
        private Brush _ColLED12Hue;
        public Brush ColLED12Hue { get { return _ColLED12Hue; } set { SetProperty(ref _ColLED12Hue, value); } }
       
        private Brush _ColLED13Hue;
        public Brush ColLED13Hue { get { return _ColLED13Hue; } set { SetProperty(ref _ColLED13Hue, value); } }
       
        private Brush _ColLED14Hue;
        public Brush ColLED14Hue { get { return _ColLED14Hue; } set { SetProperty(ref _ColLED14Hue, value); } }
       
        private Brush _ColLED15Hue;
        public Brush ColLED15Hue { get { return _ColLED15Hue; } set { SetProperty(ref _ColLED15Hue, value); } }
       
        private Brush _ColLED16Hue;
        public Brush ColLED16Hue { get { return _ColLED16Hue; } set { SetProperty(ref _ColLED16Hue, value); } }
       
    }
}
