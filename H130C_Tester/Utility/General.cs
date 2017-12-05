using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media;
using OpenCvSharp;

namespace H130C_Tester
{


    public static class General
    {

        //インスタンス変数の宣言
        public static SoundPlayer player = null;
        public static SoundPlayer soundPass = null;
        public static SoundPlayer soundPassLong = null;
        public static SoundPlayer soundFail = null;
        public static SoundPlayer soundAlarm = null;
        public static SoundPlayer soundSuccess = null;
        public static SoundPlayer soundNotice = null;


        public static SolidColorBrush DialogOnBrush = new SolidColorBrush();
        public static SolidColorBrush OnBrush = new SolidColorBrush();
        public static SolidColorBrush OffBrush = new SolidColorBrush();
        public static SolidColorBrush NgBrush = new SolidColorBrush();

        public static LPC1768 MainIo = new LPC1768();
        public static LPC1768 SubIo = new LPC1768();

        //インスタンスを生成する必要がある周辺機器
        public static Camera cam;

        static General()
        {
            //オーディオリソースを取り出す
            General.soundPass = new SoundPlayer(@"Resources\Wav\Pass.wav");
            General.soundPassLong = new SoundPlayer(@"Resources\Wav\PassLong.wav");
            General.soundFail = new SoundPlayer(@"Resources\Wav\Fail.wav");
            General.soundAlarm = new SoundPlayer(@"Resources\Wav\Alarm.wav");
            General.soundSuccess = new SoundPlayer(@"Resources\Wav\Success.wav");
            General.soundNotice = new SoundPlayer(@"Resources\Wav\Notice.wav");

            OffBrush.Color = Colors.Transparent;

            DialogOnBrush.Color = Colors.DodgerBlue;
            DialogOnBrush.Opacity = 0.6;

            OnBrush.Color = Colors.DodgerBlue;
            OnBrush.Opacity = 0.4;

            NgBrush.Color = Colors.HotPink;
            NgBrush.Opacity = 0.4;
        }

        public static void Show()
        {
            var T = 0.3;
            var t = 0.005;

            State.Setting.OpacityTheme = State.VmMainWindow.ThemeOpacity;
            //10msec刻みでT秒で元のOpacityに戻す
            int times = (int)(T / t);

            State.VmMainWindow.ThemeOpacity = 0;
            Task.Run(() =>
            {
                while (true)
                {

                    State.VmMainWindow.ThemeOpacity += State.Setting.OpacityTheme / (double)times;
                    Thread.Sleep((int)(t * 1000));
                    if (State.VmMainWindow.ThemeOpacity >= State.Setting.OpacityTheme) return;

                }
            });
        }

        public static void SetRadius(bool sw)
        {
            var T = 0.45;//アニメーションが完了するまでの時間（秒）
            var t = 0.005;//（秒）

            //5msec刻みでT秒で元のOpacityに戻す
            int times = (int)(T / t);


            Task.Run(() =>
            {
                if (sw)
                {
                    while (true)
                    {
                        State.VmMainWindow.ThemeBlurEffectRadius += 25 / (double)times;
                        Thread.Sleep((int)(t * 1000));
                        if (State.VmMainWindow.ThemeBlurEffectRadius >= 25) return;

                    }
                }
                else
                {
                    var CurrentRadius = State.VmMainWindow.ThemeBlurEffectRadius;
                    while (true)
                    {
                        CurrentRadius -= 25 / (double)times;
                        if (CurrentRadius > 0)
                        {
                            State.VmMainWindow.ThemeBlurEffectRadius = CurrentRadius;
                        }
                        else
                        {
                            State.VmMainWindow.ThemeBlurEffectRadius = 0;
                            return;
                        }
                        Thread.Sleep((int)(t * 1000));
                    }
                }

            });
        }



        public static bool SaveRetryLog()
        {
            if (State.RetryLogList.Count() == 0) return true;

            //出力用のファイルを開く appendをtrueにすると既存のファイルに追記、falseにするとファイルを新規作成する
            using (var sw = new System.IO.StreamWriter(Constants.fileName_RetryLog, true, Encoding.GetEncoding("Shift_JIS")))
            {
                try
                {
                    State.RetryLogList.ForEach(d =>
                    {
                        sw.WriteLine(d);
                    });

                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }



        private static List<string> MakePassTestData()//TODO:
        {
            var ListData = new List<string>
            {
                "AssemblyVer " + State.AssemblyInfo,
                "TestSpecVer " + State.TestSpec.TestSpecVer,
                System.DateTime.Now.ToString("yyyy年MM月dd日(ddd) HH:mm:ss"),
                State.VmMainWindow.Operator,
                State.VmTestStatus.FwVer,
                State.VmTestStatus.FwSum,

                State.VmTestResults.Vol5v,

                State.VmTestResults.Ry1OnTime,
                State.VmTestResults.Ry2OnTime,
                State.VmTestResults.Ry3OnTime,
                State.VmTestResults.Ry4OnTime,
                State.VmTestResults.Ry5OnTime,
                State.VmTestResults.Ry6OnTime,
                State.VmTestResults.Ry7OnTime,
                State.VmTestResults.Ry8OnTime,

                State.VmTestResults.Ph1_1InTime,
                State.VmTestResults.Ph1_2InTime,
                State.VmTestResults.Ph1_3InTime,
                State.VmTestResults.Ph1_4InTime,
                State.VmTestResults.Ph2_1InTime,
                State.VmTestResults.Ph2_2InTime,
                State.VmTestResults.Ph2_3InTime,
                State.VmTestResults.Ph2_4InTime,

                State.VmTestResults.LumLed1,
                State.VmTestResults.LumLed2,
                State.VmTestResults.LumLed3,
                State.VmTestResults.LumLed4,
                State.VmTestResults.LumLed5,
                State.VmTestResults.LumLed6,
                State.VmTestResults.LumLed7,
                State.VmTestResults.LumLed8,
                State.VmTestResults.LumLed9,
                State.VmTestResults.LumLed10,
                State.VmTestResults.LumLed11,
                State.VmTestResults.LumLed12,
                State.VmTestResults.LumLed13,
                State.VmTestResults.LumLed14,
                State.VmTestResults.LumLed15,
                State.VmTestResults.LumLed16,
                
            };

            return ListData;
        }

        public static bool SaveTestData()
        {
            try
            {
                var dataList = new List<string>();
                dataList = MakePassTestData();

                var OkDataFilePath = Constants.PassDataFolderPath + State.VmMainWindow.Opecode + ".csv";

                if (!System.IO.File.Exists(OkDataFilePath))
                {
                    //既存検査データがなければ新規作成
                    File.Copy(Constants.PassDataFolderPath + "Format.csv", OkDataFilePath);
                }

                // リストデータをすべてカンマ区切りで連結する
                string stCsvData = string.Join(",", dataList);

                // appendをtrueにすると，既存のファイルに追記
                // falseにすると，ファイルを新規作成する
                var append = true;

                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(OkDataFilePath, append, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine(stCsvData);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        //**************************************************************************
        //検査データの保存　　　　
        //引数：なし
        //戻値：なし
        //**************************************************************************

        public static bool SaveNgData(List<string> dataList)
        {
            try
            {
                var NgDataFilePath = Constants.FailDataFolderPath + State.VmMainWindow.Opecode + ".csv";
                if (!File.Exists(NgDataFilePath))
                {
                    //既存検査データがなければ新規作成
                    File.Copy(Constants.FailDataFolderPath + "FormatNg.csv", NgDataFilePath);
                }

                var stArrayData = dataList.ToArray();
                // リストデータをすべてカンマ区切りで連結する
                string stCsvData = string.Join(",", stArrayData);

                // appendをtrueにすると，既存のファイルに追記
                //         falseにすると，ファイルを新規作成する
                var append = true;

                // 出力用のファイルを開く
                using (var sw = new System.IO.StreamWriter(NgDataFilePath, append, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine(stCsvData);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///レバーが下がっていればTrueを返す 
        /// </summary>
        /// <returns></returns>
        public static bool CheckPress()//レバーが下がっていればTrueを返す
        {
            MainIo.SendData1768("R,P19", setLog: false);
            return MainIo.RecieveData == "L";
        }

        //**************************************************************************
        //EPX64のリセット
        //引数：なし
        //戻値：なし
        //**************************************************************************
        public static void ResetIo() //P7:0 P6:0 P5:1 P4:1  P3:1 P2:1 P1:1 P0:1  
        {
            //IOを初期化する処理（出力をすべてＬに落とす）
            MainIo.SendData1768("ResetIo");
            SubIo.SendData1768("ResetIo");
            Flags.PowOn = false;
        }

        public static void PowSupply(bool sw)
        {
            if (Flags.PowOn == sw) return;

            if (sw)
            {
                SetK3(true);
                Thread.Sleep(2000);
            }
            else
            {
                SetK3(false);
            }

            Flags.PowOn = sw;

        }


        //**************************************************************************
        //WAVEファイルを再生する
        //引数：なし
        //戻値：なし
        //**************************************************************************  

        //WAVEファイルを再生する（非同期で再生）
        public static void PlaySound(SoundPlayer p)
        {
            //再生されているときは止める
            if (player != null)
                player.Stop();

            //waveファイルを読み込む
            player = p;
            //最後まで再生し終えるまで待機する
            player.Play();
        }
        //WAVEファイルを再生する（同期で再生）
        public static void PlaySound2(SoundPlayer p)
        {
            //再生されているときは止める
            if (player != null)
                player.Stop();

            //waveファイルを読み込む
            player = p;
            //最後まで再生し終えるまで待機する
            player.PlaySync();

        }

        public static void PlaySoundLoop(SoundPlayer p)
        {
            //再生されているときは止める
            if (player != null)
                player.Stop();

            //waveファイルを読み込む
            player = p;
            //最後まで再生し終えるまで待機する
            player.PlayLooping();
        }

        //再生されているWAVEファイルを止める
        public static void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }



        public static void ResetViewModel()//TODO:
        {
            //ViewModel OK台数、NG台数、Total台数の更新
            State.VmTestStatus.OkCount = State.Setting.TodayOkCount.ToString() + "台";
            State.VmTestStatus.NgCount = State.Setting.TodayNgCount.ToString() + "台";
            State.VmTestStatus.Message = Constants.MessSet;
            cam.ImageOpacity = Constants.OpacityImgMin;


            State.VmTestStatus.DecisionVisibility = System.Windows.Visibility.Hidden;
            State.VmTestStatus.ErrInfoVisibility = System.Windows.Visibility.Hidden;
            State.VmTestStatus.RingVisibility = System.Windows.Visibility.Visible;

            State.VmTestStatus.TestTime = "00:00";
            State.VmTestStatus.進捗度 = 0;
            State.VmTestStatus.TestLog = "";

            State.VmTestStatus.FailInfo = "";
            State.VmTestStatus.Spec = "";
            State.VmTestStatus.MeasValue = "";


            //試験結果のクリア
            State.VmTestResults = new ViewModelTestResult();

            //通信ログのクリア
            MainIo.ClearViewModel();
            SubIo.ClearViewModel();

            State.VmMainWindow.EnableOtherButton = true;

            //各種フラグの初期化
            Flags.PowOn = false;
            Flags.ClickStopButton = false;
            Flags.Testing = false;


            //テーマ透過度を元に戻す
            General.SetRadius(false);

            State.VmTestStatus.RetryLabelVis = System.Windows.Visibility.Hidden;
            State.VmTestStatus.TestSettingEnable = true;
            State.VmMainWindow.OperatorEnable = true;

            //コネクタチェックでエラーになると表示されたままになるので隠す（誤動作防止！！！）
            State.VmTestStatus.EnableButtonErrInfo = System.Windows.Visibility.Hidden;
        }


        public static void CheckAll周辺機器フラグ()
        {
            Flags.AllOk周辺機器接続 = (Flags.State1768 && cam.CamState);
        }


        public static void Init周辺機器()//TODO:
        {
            Flags.Initializing周辺機器 = true;

            //LPC1768の初期化
            bool Stop1768 = false;
            Task.Run(() =>
            {
                if (InitLpc1768())
                {
                    //IOボードのリセット（出力をすべてLする）
                    ResetIo();
                }
                Stop1768 = true;
            });


            //カメラ1（CMS_V37BK）の初期化
            bool StopCAMERA1 = false;
            Task.Run(() =>
            {
                cam = new Camera(0, Constants.filePath_Cam1CalFilePath);
                while (true)
                {
                    if (Flags.StopInit周辺機器)
                    {
                        break;
                    }

                    if (!Flags.State1768) continue;//他のアイテムの試験機が接続されていたらカメラのイニシャライズは行わない

                    cam.InitCamera();
                    if (cam.CamState) break;

                    Thread.Sleep(500);
                }
                StopCAMERA1 = true;
            });



            Task.Run(() =>
            {
                while (true)
                {
                    CheckAll周辺機器フラグ();

                    //EPX64Sの初期化の中で、K100、K101の溶着チェックを行っているが、これがNGだとしてもInit周辺機器()は終了する
                    var IsAllStopped = Stop1768 && StopCAMERA1;

                    if (Flags.AllOk周辺機器接続 || IsAllStopped) break;
                    Thread.Sleep(400);

                }
                Flags.Initializing周辺機器 = false;
            });

        }
        //強制停止ボタンの設定
        public static async Task ShowStopButton(bool sw)
        {
            if (sw)
            {
                State.VmTestStatus.StopButtonEnable = true;
                await Task.Run(() =>
                {
                    foreach (var i in Enumerable.Range(1, 100))
                    {
                        State.VmTestStatus.StopButtonVis = i / 100.0;
                        Thread.Sleep(10);
                    }
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    foreach (var i in Enumerable.Range(1, 100))
                    {
                        State.VmTestStatus.StopButtonVis = (1 - (i / 100.0));
                        Thread.Sleep(10);
                    }
                });
                State.VmTestStatus.StopButtonEnable = false;
            }

        }





        public static bool InitLpc1768()
        {
            //デバイスマネージャに表示される
            const string ComName = "mbed Serial Port";

            Flags.State1768 = false;

            List<string> ComNumList;

            while (true)
            {
                if (Flags.StopInit周辺機器)
                {
                    return false;
                }

                ComNumList = FindSerialPort.GetComNoList(ComName);
                if (ComNumList != null && ComNumList.Count() == 2) break;
            }

            var buff1 = new LPC1768();
            if (!buff1.Init(ComNumList[0])) return false;

            var buff2 = new LPC1768();
            if (!buff2.Init(ComNumList[1])) return false;

            if (buff1.ID_LPC1768.Contains("H130C_MAIN"))
            {
                MainIo = buff1;
                SubIo = buff2;
            }
            else
            {
                SubIo = buff1;
                MainIo = buff2;
            }
            MainIo.vm = State.MainComm;
            SubIo.vm = State.SubComm;

            return Flags.State1768 = true;
        }


        public static IplImage trimming(IplImage src, int x, int y, int width, int height)
        {
            IplImage dest = new IplImage(width, height, src.Depth, src.NChannels);
            Cv.SetImageROI(src, new CvRect(x, y, width, height));
            dest = src.Clone();
            Cv.ResetImageROI(src);
            return dest;
        }

        public static async Task LedAllOn()
        {
            await Task.Run(() =>
            {
                //LED1～8全点灯させる処理（製品のRy1～8をすべてOnさせる）
                General.MainIo.SendDataTarget("WP1002");
                General.MainIo.SendDataTarget("WP1102");
                General.MainIo.SendDataTarget("WP1202");
                General.MainIo.SendDataTarget("WP1302");
                General.MainIo.SendDataTarget("WP1402");
                General.MainIo.SendDataTarget("WP1502");
                General.MainIo.SendDataTarget("WP1602");
                General.MainIo.SendDataTarget("WP1702");

                //LED9～16全点灯させる処理（製品のPH1,PH2に接点入力を行う）
                General.SubIo.SendData1768("W,P14,1");
                General.SubIo.SendData1768("W,P15,1");
                General.SubIo.SendData1768("W,P16,1");
                General.SubIo.SendData1768("W,P17,1");
                General.SubIo.SendData1768("W,P18,1");
                General.SubIo.SendData1768("W,P19,1");
                General.SubIo.SendData1768("W,P20,1");
                General.SubIo.SendData1768("W,P25,1");
            });
        }

        //試験機リレー、ソレノイドスタンプ制御
        public static void SetK1_2(bool sw) { SubIo.SendData1768("W,P05," + (sw ? "1" : "0")); }
        public static void SetK3(bool sw) { SubIo.SendData1768("W,P06," + (sw ? "1" : "0")); }
        public static void SetK4_5(bool sw) { SubIo.SendData1768("W,P07," + (sw ? "1" : "0")); }
        public static void SetK6_7(bool sw) { SubIo.SendData1768("W,P08," + (sw ? "1" : "0")); }
        public static void SetK8_9(bool sw) { SubIo.SendData1768("W,P09," + (sw ? "1" : "0")); }
        public static void SetRl1(bool sw) { SubIo.SendData1768("W,P10," + (sw ? "1" : "0")); }
        public static void SetRl2(bool sw) { SubIo.SendData1768("W,P11," + (sw ? "1" : "0")); }
        public static void SetLight1(bool sw) { SubIo.SendData1768("W,P13," + (sw ? "1" : "0")); }
        public static void SetLight2(bool sw) { SubIo.SendData1768("W,P28," + (sw ? "1" : "0")); }
        public static void SetLight3(bool sw) { SubIo.SendData1768("W,P29," + (sw ? "1" : "0")); }

        public static void StampOn() { SubIo.SendData1768("STAMP"); }






    }

}

