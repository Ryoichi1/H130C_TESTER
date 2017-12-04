using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using System.Linq;

namespace H130C_Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class ErrInfoコネクタチェック
    {
        public class vm : BindableBase
        {
            private Visibility _VisCN220 = Visibility.Hidden;
            public Visibility VisCN220 { get { return _VisCN220; } set { SetProperty(ref _VisCN220, value); } }

            private Visibility _VisCN221 = Visibility.Hidden;
            public Visibility VisCN221 { get { return _VisCN221; } set { SetProperty(ref _VisCN221, value); } }

            private Visibility _VisCN222 = Visibility.Hidden;
            public Visibility VisCN222 { get { return _VisCN222; } set { SetProperty(ref _VisCN222, value); } }

            private Visibility _VisCN223 = Visibility.Hidden;
            public Visibility VisCN223 { get { return _VisCN223; } set { SetProperty(ref _VisCN223, value); } }

            private Visibility _VisCN224 = Visibility.Hidden;
            public Visibility VisCN224 { get { return _VisCN224; } set { SetProperty(ref _VisCN224, value); } }

            private Visibility _VisCN225 = Visibility.Hidden;
            public Visibility VisCN225 { get { return _VisCN225; } set { SetProperty(ref _VisCN225, value); } }

            private Visibility _VisCN226 = Visibility.Hidden;
            public Visibility VisCN226 { get { return _VisCN226; } set { SetProperty(ref _VisCN226, value); } }

            private Visibility _VisCN227 = Visibility.Hidden;
            public Visibility VisCN227 { get { return _VisCN227; } set { SetProperty(ref _VisCN227, value); } }

            private Visibility _VisJP1 = Visibility.Hidden;
            public Visibility VisJP1 { get { return _VisJP1; } set { SetProperty(ref _VisJP1, value); } }


            private string _NgList;
            public string NgList { get { return _NgList; } set { SetProperty(ref _NgList, value); } }

        }

        private vm viewmodel;

        public ErrInfoコネクタチェック()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            viewmodel = new vm();
            this.DataContext = viewmodel;
            SetErrInfo();
        }
        private void SetErrInfo()
        {
            if (TestConnector.ListCnSpecs1 == null) return;
            var NgList1 = TestConnector.ListCnSpecs1.Where(cn => !cn.result);

            foreach (var cn in NgList1)
            {
                switch (cn.name)
                {
                    case TestConnector.NAME1.CN221:
                        viewmodel.VisCN221 = Visibility.Visible;
                        viewmodel.NgList += "CN221\r\n";
                        break;
                    case TestConnector.NAME1.CN222:
                        viewmodel.VisCN222 = Visibility.Visible;
                        viewmodel.NgList += "CN222\r\n";
                        break;
                    case TestConnector.NAME1.CN227:
                        viewmodel.VisCN227 = Visibility.Visible;
                        viewmodel.NgList += "CN227\r\n";
                        break;
                }
            }

            if (TestConnector.ListCnSpecs2 == null) return;
            var NgList2 = TestConnector.ListCnSpecs2.Where(cn => !cn.result);

            foreach (var cn in NgList2)
            {
                switch (cn.name)
                {
                    case TestConnector.NAME2.CN220:
                        viewmodel.VisCN220 = Visibility.Visible;
                        viewmodel.NgList += "CN220\r\n";
                        break;
                    case TestConnector.NAME2.CN223:
                        viewmodel.VisCN223 = Visibility.Visible;
                        viewmodel.NgList += "CN223\r\n";
                        break;
                    case TestConnector.NAME2.CN224:
                        viewmodel.VisCN224 = Visibility.Visible;
                        viewmodel.NgList += "CN224\r\n";
                        break;
                    case TestConnector.NAME2.CN225:
                        viewmodel.VisCN225 = Visibility.Visible;
                        viewmodel.NgList += "CN225\r\n";
                        break;
                    case TestConnector.NAME2.CN226:
                        viewmodel.VisCN226 = Visibility.Visible;
                        viewmodel.NgList += "CN226\r\n";
                        break;
                    case TestConnector.NAME2.JP1:
                        viewmodel.VisJP1 = Visibility.Visible;
                        viewmodel.NgList += "JP1\r\n";
                        break;
                }
            }
        }

        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            State.VmMainWindow.TabIndex = 0;
            
        }

    }
}
