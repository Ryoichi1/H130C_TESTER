using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace H130C_Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class Mainte
    {
        private SolidColorBrush ButtonOnBrush = new SolidColorBrush();
        private SolidColorBrush ButtonOffBrush = new SolidColorBrush();
        private const double ButtonOpacity = 0.4;

        public Mainte()
        {
            InitializeComponent();
            CanvasMainIo.DataContext = State.MainComm;
            CanvasSubIo.DataContext = State.SubComm;
            CanvasCan.DataContext = State.MainComm;

            ButtonOnBrush.Color = Colors.DodgerBlue;
            ButtonOffBrush.Color = Colors.Transparent;
            ButtonOnBrush.Opacity = ButtonOpacity;
            ButtonOffBrush.Opacity = ButtonOpacity;

            SetCommand();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Flags.EnableMaintePage = false;
            State.MainComm.TX = "";
            State.MainComm.RX = "";
            State.MainComm.CAN_TX = "";
            State.MainComm.CAN_RX = "";
            State.SubComm.TX = "";
            State.SubComm.RX = "";

            tbVol.Text = "";
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            buttonPow.Background = Brushes.Transparent;
            General.PowSupply(false);
            Flags.EnableMaintePage = true;
        }


        bool FlagPow;
        private void buttonPow_Click(object sender, RoutedEventArgs e)
        {
            if (FlagPow)
            {
                General.PowSupply(false);
                buttonPow.Background = ButtonOffBrush;
            }
            else
            {
                General.PowSupply(true);
                buttonPow.Background = ButtonOnBrush;
            }

            FlagPow = !FlagPow;
        }



        private void buttonStamp_Click(object sender, RoutedEventArgs e)
        {
            buttonStamp.Background = ButtonOnBrush;
            General.StampOn();
            buttonStamp.Background = ButtonOffBrush;
        }

        private void buttonSendSub_Click(object sender, RoutedEventArgs e)
        {
            if (cbCommandSub.SelectedIndex == -1) return;
            General.SubIo.SendData1768(cbCommandSub.SelectedItem.ToString());
        }

        private void buttonSendCan_Click(object sender, RoutedEventArgs e)
        {
            if (cbCommandCan.SelectedIndex == -1) return;
            General.MainIo.SendDataTarget(cbCommandCan.SelectedItem.ToString());
        }

        private void SetCommand()
        {
            State.Command.Main.ForEach(m =>
            {
                cbCommandMain.Items.Add(m);
            });
            State.Command.Sub.ForEach(m =>
            {
                cbCommandSub.Items.Add(m);
            });
            State.Command.Can.ForEach(m =>
            {
                cbCommandCan.Items.Add(m);
            });
        }

        private void buttonSendMain_Click(object sender, RoutedEventArgs e)
        {
            if (cbCommandMain.SelectedIndex == -1) return;
            General.MainIo.SendData1768(cbCommandMain.SelectedItem.ToString());
        }

        private void buttonMeasVol_Click(object sender, RoutedEventArgs e)
        {
            var re = TestPowerSupplyVoltage.MeasVol();
            tbVol.Text = $"{re.vol.ToString("F2")}V";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            General.DisCharge();
        }
    }
}
