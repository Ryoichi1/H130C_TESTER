using Microsoft.Practices.Prism.Mvvm;
using System;
using System.IO.Ports;
using System.Threading;

namespace H130C_Tester
{
    public class LPC1768 : BindableBase
    {
        public ViewModelCommunication vm = new ViewModelCommunication();

        //変数の宣言（インスタンスメンバーになります）
        private SerialPort port;
        public string RecieveData { get; set; }//LPC1768から受信した生データ

        public string ID_LPC1768 { get; private set; }

        //コンストラクタ
        public LPC1768()
        {
            port = new SerialPort();
        }

        //**************************************************************************
        //LPC1768の初期化
        //**************************************************************************
        public bool Init(string comNum)
        {
            var result = false;
            try
            {
                if (!port.IsOpen)
                {
                    //Agilent34401A用のシリアルポート設定
                    port.PortName = comNum; //この時点で既にポートが開いている場合COM番号は設定できず例外となる（イニシャライズは１回のみ有効）
                    port.BaudRate = 115200;
                    port.DataBits = 8;
                    port.Parity = System.IO.Ports.Parity.None;
                    port.StopBits = System.IO.Ports.StopBits.One;
                    //port.DtrEnable = true;//これ設定しないとコマンド送るたびにErrorになります！
                    port.NewLine = ("\r\n");
                    port.Open();
                }

                //クエリ送信
                if (!SendData1768("*IDN?")) return false;
                if (RecieveData.Contains("H130C"))// H130C試験機が接続されていれば、"H130C_MAIN_V**" または "H130C_SUB_V**" が返ってきます。
                {
                    ID_LPC1768 = RecieveData;
                    return result = true;
                }
                else
                {
                    return result = false;
                }
            }
            catch
            {
                return result = false;
            }
            finally
            {
                if (!result)
                {
                    ClosePort();
                }
            }
        }

        //**************************************************************************
        //LPC1768を制御する 
        //**************************************************************************
        public bool SendData1768(string Data, int Wait = 1000, bool setLog = true)
        {
            //送信処理
            try
            {
                vm.TX = "";
                vm.RX = "";

                ClearBuff();//受信バッファのクリア

                port.WriteLine(Data);// \r\n は自動的に付加されます
                if (setLog) vm.TX = Data;

            }
            catch
            {
                vm.TX = "TX_Error";
                return false;
            }

            //受信処理
            try
            {
                RecieveData = "";//初期化
                port.ReadTimeout = Wait;
                RecieveData = port.ReadLine();
                if (setLog) vm.RX = RecieveData;
                return true;
            }
            catch
            {
                vm.RX = "TimeoutErr";
                return false;
            }
            finally
            {
                Thread.Sleep(50);
            }
        }


        //**************************************************************************
        //ターゲット(H170C)にコマンドを送る 
        //**************************************************************************
        public bool SendDataTarget(string Data, int Wait = 1000, bool setLog = true)
        {
            //送信処理
            try
            {
                vm.CAN_TX = "";
                vm.CAN_RX = "";

                ClearBuff();//受信バッファのクリア

                port.WriteLine(Data);// \r\n は自動的に付加されます
                if (setLog) vm.CAN_TX = $"[STX]{Data}[ETX]";

            }
            catch
            {
                vm.CAN_TX = "TX_Error";
                return false;
            }

            //受信処理
            try
            {
                RecieveData = "";//初期化
                port.ReadTimeout = Wait;
                var RxData = port.ReadLine();
                return AnalysisData(RxData);
            }
            catch
            {
                if (setLog) vm.CAN_RX = "TimeoutErr";
                return false;
            }

            //ローカル関数の定義
            bool AnalysisData(string data)
            {
                var result = false;

                try
                {
                    //受信データのフレームが正しいかチェックする（先頭STX）
                    if (!data.StartsWith("[STX]") || !data.EndsWith("[ETX]"))
                    {
                        RecieveData = "FrameError";
                        return result = false;
                    }

                    //先頭の[STX],末尾の[ETX]を取り除く
                    RecieveData = data.Replace("[STX]", "").Replace("[ETX]", "");
                    return result = true;
                }
                catch
                {
                    RecieveData = "Error例外";
                    return result = false;
                }
                finally
                {
                    if (!result)
                    {   //TODO：
                        //ラベルの色を赤くするなどの処理を追加する
                    }
                    //先頭の[STX],末尾の[ETX]はログにそのまま表示する
                    if (setLog) vm.CAN_RX = data; Thread.Sleep(150);
                }
            }

        }

        public void ClearViewModel()
        {
            vm.TX = vm.RX = vm.CAN_TX = vm.CAN_RX = "";
        }

        //**************************************************************************
        //受信バッファをクリアする
        //**************************************************************************
        private void ClearBuff()
        {
            if (port.IsOpen)
                port.DiscardInBuffer();
        }


        //**************************************************************************
        //COMポートを閉じる
        //引数：なし
        //戻値：bool
        //**************************************************************************   
        public bool ClosePort()
        {
            try
            {
                //ポートが開いているかどうかの判定
                if (port.IsOpen)
                {
                    SendData1768("ResetIo");//製品を初期化して終了
                    port.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }





    }

}

