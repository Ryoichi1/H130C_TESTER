using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Media;

namespace H130C_Tester
{

    public class ViewModelCommunication : BindableBase
    {
        //LPC1768
        private string _TX;
        public string TX
        {
            get { return _TX; }
            set { SetProperty(ref _TX, value); }
        }

        private string _RX;
        public string RX
        {
            get { return _RX; }
            set { SetProperty(ref _RX, value); }
        }

        //CAN通信
        private string _CAN_TX;
        public string CAN_TX
        {
            get { return _CAN_TX; }
            set { SetProperty(ref _CAN_TX, value); }
        }

        private string _CAN_RX;
        public string CAN_RX
        {
            get { return _CAN_RX; }
            set { SetProperty(ref _CAN_RX, value); }
        }


    }
}
