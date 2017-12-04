using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace H130C_Tester
{
    public static class TestSoldering
    {
        public enum NAME { CN222_5, CN222_7, JP1_3, JP1_5, JP1_7, JP1_9, JP1_11, JP1_13, JP1_15 }

        public class SolderingSpec
        {
            public NAME name;
            public bool result;
        }

        public static List<SolderingSpec> ListSpecs;

        private static void InitList()
        {
            ListSpecs = new List<SolderingSpec>();
            foreach (var n in Enum.GetValues(typeof(NAME)))
            {
                ListSpecs.Add(new SolderingSpec { name = (NAME)n });
            }
        }


        public static async Task<bool> CheckBySwProbe()
        {
            var result = false;

            try
            {
                return result = await Task<bool>.Run(() =>
                {

                    InitList();
                    var command = "";
                    ListSpecs.ForEach(l =>
                    {
                        switch (l.name)
                        {
                            case NAME.CN222_5:
                                command = "R,P26";
                                break;
                            case NAME.CN222_7:
                                command = "R,P27";
                                break;
                            case NAME.JP1_3:
                                command = "R,P12";
                                break;
                            case NAME.JP1_5:
                                command = "R,P13";
                                break;
                            case NAME.JP1_7:
                                command = "R,P14";
                                break;
                            case NAME.JP1_9:
                                command = "R,P15";
                                break;
                            case NAME.JP1_11:
                                command = "R,P16";
                                break;
                            case NAME.JP1_13:
                                command = "R,P17";
                                break;
                            case NAME.JP1_15:
                                command = "R,P18";
                                break;
                        }

                        switch (l.name)
                        {
                            case NAME.CN222_5:
                            case NAME.CN222_7:
                                if (General.SubIo.SendData1768(command))
                                {
                                    l.result = General.SubIo.RecieveData == "L" ? true : false;
                                }
                                break;
                            default:
                                if (General.MainIo.SendData1768(command))
                                {
                                    l.result = General.MainIo.RecieveData == "L" ? true : false;
                                }
                                break;
                        }

                    });

                    return ListSpecs.All(l => l.result);
                });
            }
            catch
            {
                Flags.ThrowException = true;
                return result = false;
            }
            finally
            {
                if (!result)
                {
                    State.uriOtherInfoPage = new Uri("Page/ErrInfo/ErrInfo未半田チェック.xaml", UriKind.Relative);
                    State.VmTestStatus.EnableButtonErrInfo = System.Windows.Visibility.Visible;
                }
            }
        }


    }
}
