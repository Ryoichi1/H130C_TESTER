using System;
using System.Threading.Tasks;
using static System.Threading.Thread;


namespace H130C_Tester
{
    public static class TestPowerSupplyVoltage
    {
        /// <summary>
        /// USBハブにACアダプターから電源供給しないと、取り込んだAD値が高めになるので注意すること！！！
        /// </summary>
        /// <returns></returns>

        public static async Task<bool> Check5v()
        {
            var result = false;
            double ad = 0;


            try
            {
                return result = await Task<bool>.Run(() =>
                {
                    Sleep(1500);
                    if (!General.MainIo.SendData1768("R,Ad"))
                        return false;

                    var hexData = General.MainIo.RecieveData; //mbedからは3桁16進で返ってっくる ex. C35

                    //mbed 12ビットのADコンバーターを搭載
                    //0(0V)～4095(0xFFF, 3.3V)

                    ad = Convert.ToInt32(hexData, 16) * (3.3 / Math.Pow(2, 12)) * 2;//試験機側では入力電圧を抵抗で分圧して1/2にしています（mbedへの入力が3.3VMaxのため）
                    return result = (ad > State.TestSpec.VccMin) && (ad < State.TestSpec.VccMax); 
                });
            }
            catch
            {
                Flags.ThrowException = true;
                return result = false;
            }
            finally
            {
                State.VmTestResults.Vol5v = $"{ad.ToString("F2")}V";
                if (!result)
                    State.VmTestResults.ColVol5v = General.NgBrush;
            }
        }


    }
}
