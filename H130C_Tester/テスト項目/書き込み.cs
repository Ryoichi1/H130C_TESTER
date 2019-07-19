using System.Threading;
using System.Threading.Tasks;

namespace H130C_Tester
{
    public static class 書き込み
    {
        public enum WriteMode { TEST, PRODUCT }

        public static async Task<bool> WriteFw(WriteMode mode)
        {
            bool result = false;
            try
            {
                General.DisCharge();
                string Path = "";
                if (mode == WriteMode.TEST)
                {
                    Path = Constants.RwsPath_Test;
                }
                else
                {
                    Path = Constants.RwsPath_Product;
                }

                General.SetK1_2(true);

                await Task.Delay(1000);

                if (!await FDT.WriteFirmware(Path)) return false;

                if (mode == WriteMode.TEST)
                {
                    return result = true;
                }
                else
                {
                    return result = FDT.CheckSum(State.TestSpec.FwSum);
                }
            }
            catch
            {
                return result = false;
            }
            finally
            {
                General.SetK1_2(false);
                await Task.Run(() => General.DisCharge(mode == WriteMode.TEST? 3000 : 500));

                if (!result)
                {
                    State.VmTestStatus.Spec = "規格値 : チェックサム " + State.TestSpec.FwSum;
                    State.VmTestStatus.MeasValue = "計測値 : チェックサム " + FDT.FlashCheckSum;
                }
            }


        }



    }
}
