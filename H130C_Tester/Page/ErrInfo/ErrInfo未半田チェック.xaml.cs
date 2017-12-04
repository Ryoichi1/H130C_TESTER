using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using System.Linq;

namespace H130C_Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class ErrInfo未半田チェック
    {
        public class vm : BindableBase
        {
            private Visibility _VisCN222_5 = Visibility.Hidden;
            public Visibility VisCN222_5 { get { return _VisCN222_5; } set { SetProperty(ref _VisCN222_5, value); } }

            private Visibility _VisCN222_7 = Visibility.Hidden;
            public Visibility VisCN222_7 { get { return _VisCN222_7; } set { SetProperty(ref _VisCN222_7, value); } }

            private Visibility _VisCN222_8 = Visibility.Hidden;
            public Visibility VisCN222_8 { get { return _VisCN222_8; } set { SetProperty(ref _VisCN222_8, value); } }


            private Visibility _VisJP1_1 = Visibility.Hidden;
            public Visibility VisJP1_1 { get { return _VisJP1_1; } set { SetProperty(ref _VisJP1_1, value); } }

            private Visibility _VisJP1_3 = Visibility.Hidden;
            public Visibility VisJP1_3 { get { return _VisJP1_3; } set { SetProperty(ref _VisJP1_3, value); } }

            private Visibility _VisJP1_5 = Visibility.Hidden;
            public Visibility VisJP1_5 { get { return _VisJP1_5; } set { SetProperty(ref _VisJP1_5, value); } }

            private Visibility _VisJP1_7 = Visibility.Hidden;
            public Visibility VisJP1_7 { get { return _VisJP1_7; } set { SetProperty(ref _VisJP1_7, value); } }

            private Visibility _VisJP1_9 = Visibility.Hidden;
            public Visibility VisJP1_9 { get { return _VisJP1_9; } set { SetProperty(ref _VisJP1_9, value); } }

            private Visibility _VisJP1_11 = Visibility.Hidden;
            public Visibility VisJP1_11 { get { return _VisJP1_11; } set { SetProperty(ref _VisJP1_11, value); } }

            private Visibility _VisJP1_13 = Visibility.Hidden;
            public Visibility VisJP1_13 { get { return _VisJP1_13; } set { SetProperty(ref _VisJP1_13, value); } }

            private Visibility _VisJP1_15 = Visibility.Hidden;
            public Visibility VisJP1_15 { get { return _VisJP1_15; } set { SetProperty(ref _VisJP1_15, value); } }


            private string _NgList;
            public string NgList { get { return _NgList; } set { SetProperty(ref _NgList, value); } }

        }

        private vm viewmodel;

        public ErrInfo未半田チェック()
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
            if (TestSoldering.ListSpecs == null) return;
            var NgList = TestSoldering.ListSpecs.Where(cn => !cn.result);

            foreach (var cn in NgList)
            {
                switch (cn.name)
                {
                    case TestSoldering.NAME.CN222_5:
                        viewmodel.VisCN222_5 = Visibility.Visible;
                        viewmodel.VisCN222_8 = Visibility.Visible;
                        viewmodel.NgList += "CN221-5,8\r\n";
                        break;
                    case TestSoldering.NAME.CN222_7:
                        viewmodel.VisCN222_7 = Visibility.Visible;
                        viewmodel.VisCN222_8 = Visibility.Visible;
                        viewmodel.NgList += "CN221-7,8\r\n";
                        break;
                    case TestSoldering.NAME.JP1_3:
                        viewmodel.VisJP1_1 = Visibility.Visible;
                        viewmodel.VisJP1_3 = Visibility.Visible;
                        viewmodel.NgList += "JP1-1,3\r\n";
                        break;
                    case TestSoldering.NAME.JP1_5:
                        viewmodel.VisJP1_1 = Visibility.Visible;
                        viewmodel.VisJP1_5 = Visibility.Visible;
                        viewmodel.NgList += "JP1-1,5\r\n";
                        break;
                    case TestSoldering.NAME.JP1_7:
                        viewmodel.VisJP1_1 = Visibility.Visible;
                        viewmodel.VisJP1_7 = Visibility.Visible;
                        viewmodel.NgList += "JP1-1,7\r\n";
                        break;
                    case TestSoldering.NAME.JP1_9:
                        viewmodel.VisJP1_1 = Visibility.Visible;
                        viewmodel.VisJP1_9 = Visibility.Visible;
                        viewmodel.NgList += "JP1-1,9\r\n";
                        break;
                    case TestSoldering.NAME.JP1_11:
                        viewmodel.VisJP1_1 = Visibility.Visible;
                        viewmodel.VisJP1_11 = Visibility.Visible;
                        viewmodel.NgList += "JP1-1,11\r\n";
                        break;
                    case TestSoldering.NAME.JP1_13:
                        viewmodel.VisJP1_1 = Visibility.Visible;
                        viewmodel.VisJP1_13 = Visibility.Visible;
                        viewmodel.NgList += "JP1-1,13\r\n";
                        break;
                    case TestSoldering.NAME.JP1_15:
                        viewmodel.VisJP1_1 = Visibility.Visible;
                        viewmodel.VisJP1_15 = Visibility.Visible;
                        viewmodel.NgList += "JP1-1,15\r\n";
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
