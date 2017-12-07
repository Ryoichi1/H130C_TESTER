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
            (bool result, double volData) Result = (false, 0.0);

            try
            {
                return await Task<bool>.Run(() =>
                {
                    Sleep(1500);
                    Result = MeasVol();
                    if (!Result.result) return false;
                    return (Result.volData > State.TestSpec.VccMin) && (Result.volData < State.TestSpec.VccMax);
                });
            }
            catch
            {
                Flags.ThrowException = true;
                return false;
            }
            finally
            {
                State.VmTestResults.Vol5v = $"{Result.volData.ToString("F2")}V";
                if (!Result.result)
                    State.VmTestResults.ColVol5v = General.NgBrush;
            }
        }

        public static (bool result, double vol) MeasVol()
        {
            if (!General.MainIo.SendData1768("R,Ad"))
                return (false, 0.0);

            var hexData = General.MainIo.RecieveData; //mbedからは3桁16進で返ってっくる ex. C35

            //mbed 12ビットのADコンバーターを搭載
            //0(0V)～4095(0xFFF, 3.3V)

            var ad = Convert.ToInt32(hexData, 16) * (3.3 / Math.Pow(2, 12)) * 2;//試験機側では入力電圧を抵抗で分圧して1/2にしています（mbedへの入力が3.3VMaxのため）

            return (true, ad);

        }

    }
}
