
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace H130C_Tester
{
    public class TestCommand
    {

        //デリゲートの宣言
        public Action RefreshDataContext;//Test.Xaml内でテスト結果をクリアするために使用すする
        public Action SbRingLoad;
        public Action SbPass;
        public Action SbFail;

        private bool FlagTestTime;

        DropShadowEffect effect判定表示PASS;
        DropShadowEffect effect判定表示FAIL;

        public TestCommand()
        {
            effect判定表示PASS = new DropShadowEffect();
            effect判定表示PASS.Color = Colors.GreenYellow;
            effect判定表示PASS.Direction = 0;
            effect判定表示PASS.ShadowDepth = 0;
            effect判定表示PASS.Opacity = 1.0;
            effect判定表示PASS.BlurRadius = 20;

            effect判定表示FAIL = new DropShadowEffect();
            effect判定表示FAIL.Color = Colors.HotPink; ;
            effect判定表示FAIL.Direction = 0;
            effect判定表示FAIL.ShadowDepth = 0;
            effect判定表示FAIL.Opacity = 1.0;
            effect判定表示FAIL.BlurRadius = 40;

        }

        public async Task StartCheck()
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    while (!Flags.EnableStartCheck) ;
                    RETRY:
                    while (true)
                    {
                        if (Flags.OtherPage) break;
                        //Thread.Sleep(200);

                        General.CheckAll周辺機器フラグ();
                        if (!Flags.AllOk周辺機器接続)
                        {
                            State.VmTestStatus.Message = Constants.MessCheckConnectMachine;
                            Flags.EnableTestStart = false;
                            continue;
                        }

                        if (Flags.PressOpenCheckBeforeTest)
                        {
                            while (true)
                            {
                                if (Flags.OtherPage)//待機中に他のページに遷移したらメソッドを抜ける
                                    return;

                                if (!General.CheckPress())
                                {
                                    Flags.PressOpenCheckBeforeTest = false;
                                    break;
                                }
                                State.VmTestStatus.Message = "一度プレスのレバーを上げてください！！！";
                                Thread.Sleep(500);
                            }
                        }

                        State.VmTestStatus.Message = Constants.MessSet;
                        Flags.EnableTestStart = true;
                        Flags.Click確認Button = false;

                        while (true)
                        {
                            if (Flags.OtherPage)//待機中に他のページに遷移したらメソッドを抜ける
                                return;

                            if (General.CheckPress())
                                return;

                        }
                    }

                });

                if (Flags.OtherPage)//待機中に他のページに遷移したらメソッドを抜ける
                {
                    Flags.PressOpenCheckBeforeTest = true;
                    return;
                }

                State.VmMainWindow.EnableOtherButton = false;
                State.VmTestStatus.StartButtonContent = Constants.停止;
                State.VmTestStatus.TestSettingEnable = false;

                await Test();//メインルーチンへ

                //試験合格後、ラベル貼り付けページを表示する場合は下記のステップを追加すること
                //if (Flags.ShowLabelPage) return;

                //日常点検合格、一項目試験合格、試験NGの場合は、Whileループを繰り返す
                //通常試験合格の場合は、ラベル貼り付けフォームがロードされた時点で、一旦StartCheckメソッドを終了します
                //その後、ラベル貼り付けフォームが閉じられた後に、Test.xamlがリロードされ、そのフォームロードイベントでStartCheckメソッドがコールされます

            }

        }

        private void Timer()
        {
            var t = Task.Run(() =>
            {
                //Stopwatchオブジェクトを作成する
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                while (FlagTestTime)
                {
                    Thread.Sleep(200);
                    State.VmTestStatus.TestTime = sw.Elapsed.ToString().Substring(3, 5);
                }
                sw.Stop();
            });
        }

        //メインルーチン
        public async Task Test()
        {
            Flags.Testing = true;

            State.VmTestStatus.Message = Constants.MessWait;

            //現在のテーマ透過度の保存
            State.CurrentThemeOpacity = State.VmMainWindow.ThemeOpacity;
            //テーマ透過度を最小にする
            General.SetRadius(true);

            General.cam.ImageOpacity = 1.0;
            State.SetCamPoint();
            General.cam.Start();//非同期メソッドの中で実行すると、カメラがうまく動作しないので注意！！！
            await Task.Delay(1000);

            //強制停止ボタンの設定
            await General.ShowStopButton(true);
            await Task.Delay(500);

            FlagTestTime = true;
            Timer();

            int FailStepNo = 0;
            int RetryCnt = 0;//リトライ用に使用する
            string FailTitle = "";


            var テスト項目最新 = new List<TestSpecs>();
            if (State.VmTestStatus.CheckUnitTest == true)
            {
                //チェックしてある項目の百の桁の解析
                var re = Int32.Parse(State.VmTestStatus.UnitTestName.Split('_').ToArray()[0]);
                int 上位桁 = Int32.Parse(State.VmTestStatus.UnitTestName.Substring(0, (re >= 1000) ? 2 : 1));
                var 抽出データ = State.テスト項目.Where(p => (p.Key / 100) == 上位桁);
                foreach (var p in 抽出データ)
                {
                    テスト項目最新.Add(new TestSpecs(p.Key, p.Value, p.PowSw));
                }
            }
            else
            {
                テスト項目最新 = State.テスト項目;
            }



            try
            {
                //IO初期化
                General.ResetIo();
                await Task.Delay(400);


                foreach (var d in テスト項目最新.Select((s, i) => new { i, s }))
                {
                    Retry:
                    State.VmTestStatus.Spec = "規格値 : ---";
                    State.VmTestStatus.MeasValue = "計測値 : ---";
                    Flags.AddDecision = true;

                    SetTestLog(d.s.Key.ToString() + "_" + d.s.Value);

                    if (d.s.PowSw)
                    {
                        if (!Flags.PowOn)
                        {
                            await Task.Delay(100);
                            await General.PowSupplyAsync(true);
                        }
                    }
                    else
                    {
                        await General.PowSupplyAsync(false);
                        await Task.Delay(100);
                    }

                    switch (d.s.Key)
                    {
                        case 100://コネクタ実装チェック
                            if (await TestConnector.CheckCn()) break;
                            goto case 5000;

                        case 101://GND2 未ハンダチェック
                            if (await TestSoldering.CheckBySwProbe()) break;
                            goto case 5000;

                        case 200://電源電圧チェック
                            if (await TestPowerSupplyVoltage.Check5v()) break;
                            goto case 5000;

                        case 300://テストプログラム書き込み
                            if (State.VmTestStatus.CheckWriteTestFwPass == true) break;
                            if (await 書き込み.WriteFw(書き込み.WriteMode.TEST)) break;
                            goto case 5000;

                        case 400://SW1検査
                            if (await TestSw1.Check()) break;
                            goto case 5000;

                        case 500://IOポート検査（入力）
                            if (await TestIoPortIn.Check()) break;
                            goto case 5000;

                        case 501://IOポート検査（出力）
                            if (await TestIoPortOut.Check()) break;
                            goto case 5000;

                        case 600://フォトカプラ接点入力（10msec）
                            if (await TestPhotoIn.CheckInputPulse()) break;
                            goto case 5000;

                        case 601://フォトカプラ接点入力2
                            if (await TestPhotoIn.CheckInput()) break;
                            goto case 5000;

                        case 700://リレー出力チェック(10msec)
                            if (await TestRelayOut.CheckPulseOut()) break;
                            goto case 5000;

                        case 701://リレー出力チェック
                            if (await TestRelayOut.Check()) break;
                            goto case 5000;

                        case 800://LEDカラーチェック
                            if (await TestLed.CheckColor()) break;
                            goto case 5000;

                        case 801://LED輝度チェック
                            if (await TestLed.CheckLum()) break;
                            goto case 5000;


                        case 900://製品ソフト書き込み
                            if (await 書き込み.WriteFw(書き込み.WriteMode.PRODUCT)) break;
                            goto case 5000;


                        case 5000://NGだっときの処理
                            if (Flags.AddDecision) SetTestLog("---- FAIL\r\n");
                            FailStepNo = d.s.Key;
                            FailTitle = d.s.Value;

                            General.PowSupply(false);
                            General.ResetIo();
                            State.VmTestStatus.IsActiveRing = false;//リング表示してる可能性があるので念のため消す処理



                            if (Flags.ClickStopButton) goto FAIL;

                            if (RetryCnt++ != Constants.RetryCount)
                            {
                                //リトライ履歴リスト更新
                                State.RetryLogList.Add(FailStepNo.ToString() + "," + FailTitle + "," + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                Flags.Retry = true;
                                General.cam.ResetFlag();//LEDテストでNGになったときは、カメラのフラグを初期化しないとNG枠が出たままになる
                                goto Retry;

                            }

                            //General.PlaySoundLoop(General.soundAlarm);
                            //var YesNoResult = MessageBox.Show("この項目はＮＧですがリトライしますか？", "", MessageBoxButtons.YesNo);
                            //General.StopSound();

                            ////何が選択されたか調べる
                            //if (YesNoResult == DialogResult.Yes)
                            //{
                            //    RetryCnt = 0;
                            //    await Task.Delay(1000);
                            //    goto Retry;
                            //}

                            goto FAIL;//自動リトライ後の作業者への確認はしない


                    }
                    //↓↓各ステップが合格した時の処理です↓↓
                    if (Flags.AddDecision) SetTestLog("---- PASS\r\n");

                    State.VmTestStatus.IsActiveRing = false;

                    //リトライステータスをリセットする
                    RetryCnt = 0;
                    Flags.Retry = false;

                    await Task.Run(() =>
                    {
                        var CurrentProgValue = State.VmTestStatus.進捗度;
                        var NextProgValue = (int)(((d.i + 1) / (double)テスト項目最新.Count()) * 100);
                        var 変化量 = NextProgValue - CurrentProgValue;
                        foreach (var p in Enumerable.Range(1, 変化量))
                        {
                            State.VmTestStatus.進捗度 = CurrentProgValue + p;
                            if (State.VmTestStatus.CheckUnitTest == false)
                            {
                                Thread.Sleep(10);
                            }
                        }
                    });
                    if (Flags.ClickStopButton) goto FAIL;
                }


                //↓↓すべての項目が合格した時の処理です↓↓
                General.ResetIo();
                await Task.Delay(500);
                State.VmTestStatus.Message = Constants.MessRemove;
                await General.cam.Stop();

                //強制停止ボタンの設定
                await General.ShowStopButton(false);

                //通しで試験が合格したときの処理です(検査データを保存して、シリアルナンバーをインクリメントする)
                if (State.VmTestStatus.CheckUnitTest != true) //null or False アプリ立ち上げ時はnullになっている！
                {
                    if (!General.SaveTestData())
                    {
                        FailStepNo = 5000;
                        FailTitle = "検査データ保存";
                        goto FAIL_DATA_SAVE;
                    }

                    General.StampOn();//合格印押し

                    //当日試験合格数をインクリメント ビューモデルはまだ更新しない
                    State.Setting.TodayOkCount++;
                }



                FlagTestTime = false;

                State.VmTestStatus.Colorlabel判定 = Brushes.AliceBlue;
                State.VmTestStatus.Decision = "PASS";
                State.VmTestStatus.ColorDecision = effect判定表示PASS;


                ResetRing();
                SetDecision();
                SbPass();

                General.PlaySound(General.soundPassLong);

                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (!General.CheckPress())
                        {
                            break;
                        }
                        Thread.Sleep(400);
                    }
                    General.player.Stop();
                });

                return;

                //不合格時の処理
                FAIL:

                //強制停止ボタンの設定
                await General.ShowStopButton(false);


                General.ResetIo();
                await Task.Delay(500);
                FAIL_DATA_SAVE:


                FlagTestTime = false;
                State.VmTestStatus.Message = Constants.MessRemove;


                //当日試験不合格数をインクリメント ビューモデルはまだ更新しない
                State.Setting.TodayNgCount++;
                await Task.Delay(100);

                State.VmTestStatus.Colorlabel判定 = Brushes.AliceBlue;
                State.VmTestStatus.Decision = "FAIL";
                State.VmTestStatus.ColorDecision = effect判定表示FAIL;

                SetErrorMessage(FailStepNo, FailTitle);

                var NgDataList = new List<string>()
                                    {
                                        System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                                        State.VmTestStatus.FailInfo,
                                        State.VmTestStatus.Spec,
                                        State.VmTestStatus.MeasValue
                                    };

                General.SaveNgData(NgDataList);


                ResetRing();
                SetDecision();
                SetErrInfo();
                SbFail();

                General.PlaySound(General.soundFail);

                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (!General.CheckPress())
                        {
                            break;
                        }
                        Thread.Sleep(400);
                    }
                });


                return;

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("想定外の例外発生DEATH！！！\r\n申し訳ありませんが再起動してください");
                Environment.Exit(0);

            }
            finally
            {
                General.ResetIo();
                SbRingLoad();

                General.cam.ResetFlag();

                await General.cam.Stop();
                General.ResetViewModel();
                RefreshDataContext();
            }

        }

        //フォームきれいにする処理いろいろ
        private void ClearForm()
        {
            SbRingLoad();
            RefreshDataContext();
        }

        private void SetErrorMessage(int stepNo, string title)
        {
            if (Flags.ClickStopButton)
            {
                State.VmTestStatus.FailInfo = "エラーコード ---     強制停止";
            }
            else
            {
                State.VmTestStatus.FailInfo = "エラーコード " + stepNo.ToString("00") + "   " + title + "異常";
            }
        }

        //テストログの更新
        private void SetTestLog(string addData)
        {
            State.VmTestStatus.TestLog += addData;
        }

        private void ResetRing()
        {
            State.VmTestStatus.RingVisibility = System.Windows.Visibility.Hidden;

        }

        private void SetDecision()
        {
            State.VmTestStatus.DecisionVisibility = System.Windows.Visibility.Visible;
        }

        private void SetErrInfo()
        {
            State.VmTestStatus.ErrInfoVisibility = System.Windows.Visibility.Visible;
        }



    }
}
