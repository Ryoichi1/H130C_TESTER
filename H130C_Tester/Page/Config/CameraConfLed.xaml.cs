﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static System.Threading.Thread;

namespace H130C_Tester
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class CameraConfLed
    {

        public CameraConfLed()
        {
            InitializeComponent();
            this.DataContext = General.cam;
            canvasLdPoint.DataContext = State.VmLedPoint;
        }

        //フォームイベントいろいろ
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Flags.EnableConfLedPage = false;

            RingCal.IsActive = false;
            tbPoint.Visibility = System.Windows.Visibility.Hidden;
            tbHsv.Visibility = System.Windows.Visibility.Hidden;

            buttonSave.IsEnabled = true;
            buttonLedOn.IsEnabled = true;
            buttonLabeling.IsEnabled = false;
            buttonHue.IsEnabled = false;

            buttonSave.Background = General.OffBrush;
            buttonLedOn.Background = General.OffBrush;
            buttonLabeling.Background = General.OffBrush;
            buttonHue.Background = General.OffBrush;
            resetView();
            labelMess.Opacity = 0;

            if (!General.cam.CamState)
                return;

            LedOn = false;
            State.VmMainWindow.MainWinEnable = false;
            await Task.Delay(1200);
            State.VmMainWindow.MainWinEnable = true;
            State.SetCamPropForLed();
            General.cam.Start();
            toggleSw.IsChecked = General.cam.Opening;

        }

        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {

            FlagLabeling = false;
            ShowHue = false;

            //TODO:
            //LEDを全消灯させる処理
            General.ResetIo();
            if (!General.cam.CamState)
                return;
            await General.cam.Stop();
            await Task.Delay(500);
            Flags.EnableConfLedPage = true;
        }

        private void im_MouseLeave(object sender, MouseEventArgs e)
        {
            tbPoint.Visibility = System.Windows.Visibility.Hidden;
            tbHsv.Visibility = System.Windows.Visibility.Hidden;
            General.cam.FlagHsv = false;
        }

        private void im_MouseEnter(object sender, MouseEventArgs e)
        {
            General.cam.FlagHsv = true;
            tbHsv.Visibility = System.Windows.Visibility.Visible;
        }

        private void im_MouseMove(object sender, MouseEventArgs e)
        {
            tbPoint.Visibility = System.Windows.Visibility.Visible;
            Point point = e.GetPosition(im);
            tbPoint.Text = "XY=" + ((int)(point.X)).ToString() + "/" + ((int)(point.Y)).ToString();

            General.cam.PointX = (int)point.X;
            General.cam.PointY = (int)point.Y;

            tbHsv.Text = "HSV=" + General.cam.Hdata.ToString() + "," + General.cam.Sdata.ToString() + "," + General.cam.Vdata.ToString();
        }



        //データ保存いろいろ
        private void resetView()
        {
            State.VmLedPoint.LED1 = "";
            State.VmLedPoint.LED2 = "";
            State.VmLedPoint.LED3 = "";
            State.VmLedPoint.LED4 = "";
            State.VmLedPoint.LED5 = "";
            State.VmLedPoint.LED6 = "";
            State.VmLedPoint.LED7 = "";
            State.VmLedPoint.LED8 = "";
            State.VmLedPoint.LED9 = "";
            State.VmLedPoint.LED10 = "";
            State.VmLedPoint.LED11 = "";
            State.VmLedPoint.LED12 = "";
            State.VmLedPoint.LED13 = "";
            State.VmLedPoint.LED14 = "";
            State.VmLedPoint.LED15 = "";
            State.VmLedPoint.LED16 = "";

            State.VmLedPoint.LED1Lum = "";
            State.VmLedPoint.LED2Lum = "";
            State.VmLedPoint.LED3Lum = "";
            State.VmLedPoint.LED4Lum = "";
            State.VmLedPoint.LED5Lum = "";
            State.VmLedPoint.LED6Lum = "";
            State.VmLedPoint.LED7Lum = "";
            State.VmLedPoint.LED8Lum = "";
            State.VmLedPoint.LED9Lum = "";
            State.VmLedPoint.LED10Lum = "";
            State.VmLedPoint.LED11Lum = "";
            State.VmLedPoint.LED12Lum = "";
            State.VmLedPoint.LED13Lum = "";
            State.VmLedPoint.LED14Lum = "";
            State.VmLedPoint.LED15Lum = "";
            State.VmLedPoint.LED16Lum = "";

            State.VmLedPoint.LED1Hue = "";
            State.VmLedPoint.LED2Hue = "";
            State.VmLedPoint.LED3Hue = "";
            State.VmLedPoint.LED4Hue = "";
            State.VmLedPoint.LED5Hue = "";
            State.VmLedPoint.LED6Hue = "";
            State.VmLedPoint.LED7Hue = "";
            State.VmLedPoint.LED8Hue = "";
            State.VmLedPoint.LED9Hue = "";
            State.VmLedPoint.LED10Hue = "";
            State.VmLedPoint.LED11Hue = "";
            State.VmLedPoint.LED12Hue = "";
            State.VmLedPoint.LED13Hue = "";
            State.VmLedPoint.LED14Hue = "";
            State.VmLedPoint.LED15Hue = "";
            State.VmLedPoint.LED16Hue = "";

            State.VmLedPoint.ColLED1Hue = General.OffBrush;
            State.VmLedPoint.ColLED2Hue = General.OffBrush;
            State.VmLedPoint.ColLED3Hue = General.OffBrush;
            State.VmLedPoint.ColLED4Hue = General.OffBrush;
            State.VmLedPoint.ColLED5Hue = General.OffBrush;
            State.VmLedPoint.ColLED6Hue = General.OffBrush;
            State.VmLedPoint.ColLED7Hue = General.OffBrush;
            State.VmLedPoint.ColLED8Hue = General.OffBrush;
            State.VmLedPoint.ColLED9Hue = General.OffBrush;
            State.VmLedPoint.ColLED10Hue = General.OffBrush;
            State.VmLedPoint.ColLED11Hue = General.OffBrush;
            State.VmLedPoint.ColLED12Hue = General.OffBrush;
            State.VmLedPoint.ColLED13Hue = General.OffBrush;
            State.VmLedPoint.ColLED14Hue = General.OffBrush;
            State.VmLedPoint.ColLED15Hue = General.OffBrush;
            State.VmLedPoint.ColLED16Hue = General.OffBrush;
        }

        private void SaveCameraCommonPropForLed()
        {
            State.CamPropLed.Brightness = General.cam.Brightness;
            State.CamPropLed.Contrast = General.cam.Contrast;
            State.CamPropLed.Hue = General.cam.Hue;
            State.CamPropLed.Saturation = General.cam.Saturation;
            State.CamPropLed.Sharpness = General.cam.Sharpness;
            State.CamPropLed.Gamma = General.cam.Gamma;
            State.CamPropLed.Gain = General.cam.Gain;
            State.CamPropLed.Exposure = General.cam.Exposure;
            State.CamPropLed.Whitebalance = General.cam.Wb;
            State.CamPropLed.Theta = General.cam.Theta;
            State.CamPropLed.BinLevel = General.cam.BinLevel;

            State.CamPropLed.Opening = General.cam.Opening;
            State.CamPropLed.OpenCnt = General.cam.OpenCnt;
            State.CamPropLed.CloseCnt = General.cam.CloseCnt;
        }

        private bool SaveLedPoint()
        {
            try
            {
                State.CamPropLed.Led1 = State.VmLedPoint.LED1;
                State.CamPropLed.Led2 = State.VmLedPoint.LED2;
                State.CamPropLed.Led3 = State.VmLedPoint.LED3;
                State.CamPropLed.Led4 = State.VmLedPoint.LED4;
                State.CamPropLed.Led5 = State.VmLedPoint.LED5;
                State.CamPropLed.Led6 = State.VmLedPoint.LED6;
                State.CamPropLed.Led7 = State.VmLedPoint.LED7;
                State.CamPropLed.Led8 = State.VmLedPoint.LED8;
                State.CamPropLed.Led9 = State.VmLedPoint.LED9;
                State.CamPropLed.Led10 = State.VmLedPoint.LED10;
                State.CamPropLed.Led11 = State.VmLedPoint.LED11;
                State.CamPropLed.Led12 = State.VmLedPoint.LED12;
                State.CamPropLed.Led13 = State.VmLedPoint.LED13;
                State.CamPropLed.Led14 = State.VmLedPoint.LED14;
                State.CamPropLed.Led15 = State.VmLedPoint.LED15;
                State.CamPropLed.Led16 = State.VmLedPoint.LED16;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool SaveLedLum()
        {
            try
            {
                State.CamPropLed.LumLed1 = Double.Parse(State.VmLedPoint.LED1Lum);
                State.CamPropLed.LumLed2 = Double.Parse(State.VmLedPoint.LED2Lum);
                State.CamPropLed.LumLed3 = Double.Parse(State.VmLedPoint.LED3Lum);
                State.CamPropLed.LumLed4 = Double.Parse(State.VmLedPoint.LED4Lum);
                State.CamPropLed.LumLed5 = Double.Parse(State.VmLedPoint.LED5Lum);
                State.CamPropLed.LumLed6 = Double.Parse(State.VmLedPoint.LED6Lum);
                State.CamPropLed.LumLed7 = Double.Parse(State.VmLedPoint.LED7Lum);
                State.CamPropLed.LumLed8 = Double.Parse(State.VmLedPoint.LED8Lum);
                State.CamPropLed.LumLed9 = Double.Parse(State.VmLedPoint.LED9Lum);
                State.CamPropLed.LumLed10 = Double.Parse(State.VmLedPoint.LED10Lum);
                State.CamPropLed.LumLed11 = Double.Parse(State.VmLedPoint.LED11Lum);
                State.CamPropLed.LumLed12 = Double.Parse(State.VmLedPoint.LED12Lum);
                State.CamPropLed.LumLed13 = Double.Parse(State.VmLedPoint.LED13Lum);
                State.CamPropLed.LumLed14 = Double.Parse(State.VmLedPoint.LED14Lum);
                State.CamPropLed.LumLed15 = Double.Parse(State.VmLedPoint.LED15Lum);
                State.CamPropLed.LumLed16 = Double.Parse(State.VmLedPoint.LED16Lum);
                return true;
            }
            catch
            {
                return false;
            }
        }


        bool CanSaveLedPpint = false;
        private void labeling()
        {
            Task.Run(() =>
            {
                General.cam.ResetFlag();
                General.cam.FlagLabeling = true;
                while (FlagLabeling)
                {
                    if (General.cam.blobs == null) continue;
                    var blobs = General.cam.blobs.Clone();


                    //画面下にmbedのLED光が写り込んでしまうため、マスク処理（Y座標280以下のものを抽出）
                    var blobInfo = blobs.Where(b => b.Value.Centroid.Y < 280);

                    //LED1～8の座標を抽出する（画面半分より上側のブロブを抽出する）
                    var blobLed1_8 = blobInfo.Where(b => b.Value.Centroid.Y < 180).OrderByDescending(b => b.Value.Centroid.X).ToList();

                    //LED9～16の座標を抽出する（画面半分より下側のブロブを抽出する）
                    var blobLed9_16 = blobInfo.Where(b => b.Value.Centroid.Y > 180).OrderByDescending(b => b.Value.Centroid.X).ToList();

                    if (blobLed1_8.Count() != 8 || blobLed9_16.Count() != 8)
                    {
                        resetView();
                        CanSaveLedPpint = false;
                        continue;
                    }

                    CanSaveLedPpint = true;
                    //ビューモデルの更新
                    State.VmLedPoint.LED1 = blobLed1_8[0].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[0].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED1Lum = blobLed1_8[0].Value.Area.ToString();

                    State.VmLedPoint.LED2 = blobLed1_8[1].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[1].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED2Lum = blobLed1_8[1].Value.Area.ToString();

                    State.VmLedPoint.LED3 = blobLed1_8[2].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[2].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED3Lum = blobLed1_8[2].Value.Area.ToString();

                    State.VmLedPoint.LED4 = blobLed1_8[3].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[3].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED4Lum = blobLed1_8[3].Value.Area.ToString();

                    State.VmLedPoint.LED5 = blobLed1_8[4].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[4].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED5Lum = blobLed1_8[4].Value.Area.ToString();

                    State.VmLedPoint.LED6 = blobLed1_8[5].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[5].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED6Lum = blobLed1_8[5].Value.Area.ToString();

                    State.VmLedPoint.LED7 = blobLed1_8[6].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[6].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED7Lum = blobLed1_8[6].Value.Area.ToString();

                    State.VmLedPoint.LED8 = blobLed1_8[7].Value.Centroid.X.ToString("F0") + "/" + blobLed1_8[7].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED8Lum = blobLed1_8[7].Value.Area.ToString();

                    State.VmLedPoint.LED9 = blobLed9_16[0].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[0].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED9Lum = blobLed9_16[0].Value.Area.ToString();

                    State.VmLedPoint.LED10 = blobLed9_16[1].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[1].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED10Lum = blobLed9_16[1].Value.Area.ToString();

                    State.VmLedPoint.LED11 = blobLed9_16[2].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[2].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED11Lum = blobLed9_16[2].Value.Area.ToString();

                    State.VmLedPoint.LED12 = blobLed9_16[3].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[3].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED12Lum = blobLed9_16[3].Value.Area.ToString();

                    State.VmLedPoint.LED13 = blobLed9_16[4].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[4].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED13Lum = blobLed9_16[4].Value.Area.ToString();

                    State.VmLedPoint.LED14 = blobLed9_16[5].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[5].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED14Lum = blobLed9_16[5].Value.Area.ToString();

                    State.VmLedPoint.LED15 = blobLed9_16[6].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[6].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED15Lum = blobLed9_16[6].Value.Area.ToString();

                    State.VmLedPoint.LED16 = blobLed9_16[7].Value.Centroid.X.ToString("F0") + "/" + blobLed9_16[7].Value.Centroid.Y.ToString("F0");
                    State.VmLedPoint.LED16Lum = blobLed9_16[7].Value.Area.ToString();

                }
                General.cam.ResetFlag();
                CanSaveLedPpint = false;
            });
        }

        bool FlagLabeling;
        private void buttonLabeling_Click(object sender, RoutedEventArgs e)
        {
            FlagLabeling = !FlagLabeling;
            buttonHue.IsEnabled = !FlagLabeling;
            buttonLabeling.Background = FlagLabeling ? General.OnBrush : General.OffBrush;

            if (FlagLabeling)
                labeling();
        }

        bool IsBusy = false;
        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsBusy)
                return;

            if (!General.CheckPress())
            {
                MessageBox.Show("ゴールデンサンプルをセットして\nレバーを下げてください");
                return;
            }

            try
            {
                //LED全点灯の処理
                buttonSave.Background = Brushes.DodgerBlue;

                buttonLedOn.IsEnabled = false;
                buttonLabeling.IsEnabled = false;
                buttonHue.IsEnabled = false;

                IsBusy = true;
                RingCal.IsActive = true;

                await Task.Run(() => General.PowSupply(true));
                await General.LedAllOn();

                var tm = new GeneralTimer(3000);
                tm.start();

                FlagLabeling = true;
                labeling();

                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (tm.FlagTimeout)
                            break;
                        if (CanSaveLedPpint)
                            break;
                    }
                });

                if (!CanSaveLedPpint)
                {
                    goto FAIL;
                }
                else
                {
                    await Task.Delay(1000);
                    if (!SaveLedPoint()/*座標保存*/ || !SaveLedLum()/*輝度保存*/)
                        goto FAIL;

                    SaveCameraCommonPropForLed();//カメラプロパティ保存

                    //カラーチェック
                    FlagLabeling = false;
                    General.cam.ResetFlag();

                    await Task.Delay(1000);

                    TestLed.CheckColorForDebug();
                    if (!CheckCol())
                        goto FAIL;

                    General.PlaySound(General.soundSuccess);
                    labelMess.Content = "自動補正完了しました！";
                    labelMess.Foreground = Brushes.DodgerBlue;
                    (FindResource("SbMessage") as Storyboard).Begin();
                    await Task.Delay(1000);
                    return;
                }

                FAIL:
                General.PlaySound(General.soundFail);
                labelMess.Content = "LED全点灯が認識できません\nカメラプロパティの調整をしてください";
                labelMess.Foreground = Brushes.HotPink;
                (FindResource("SbMessage") as Storyboard).Begin();
                await Task.Delay(1000);

            }
            finally
            {
                FlagLabeling = false;
                RingCal.IsActive = false;
                await Task.Delay(150);
                buttonSave.Background = Brushes.Transparent;

                buttonLedOn.IsEnabled = true;
                buttonLabeling.IsEnabled = true;
                buttonHue.IsEnabled = true;

                IsBusy = false;
                General.ResetIo();
            }

        }


        private bool CheckCol()
        {

            var ColCheck = true;

            try
            {
                //RED
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED1Hue) && Int32.Parse(State.VmLedPoint.LED1Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED1Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED2Hue) && Int32.Parse(State.VmLedPoint.LED2Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED2Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED3Hue) && Int32.Parse(State.VmLedPoint.LED3Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED3Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED4Hue) && Int32.Parse(State.VmLedPoint.LED4Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED4Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED5Hue) && Int32.Parse(State.VmLedPoint.LED5Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED5Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED6Hue) && Int32.Parse(State.VmLedPoint.LED6Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED6Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED7Hue) && Int32.Parse(State.VmLedPoint.LED7Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED7Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.RedHueMin < Int32.Parse(State.VmLedPoint.LED8Hue) && Int32.Parse(State.VmLedPoint.LED8Hue) < State.TestSpec.RedHueMax))
                {
                    State.VmLedPoint.ColLED8Hue = Brushes.Gray;
                    ColCheck = false;
                }

                //GREEN
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED9Hue) && Int32.Parse(State.VmLedPoint.LED9Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED9Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED10Hue) && Int32.Parse(State.VmLedPoint.LED10Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED10Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED11Hue) && Int32.Parse(State.VmLedPoint.LED11Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED11Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED12Hue) && Int32.Parse(State.VmLedPoint.LED12Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED12Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED13Hue) && Int32.Parse(State.VmLedPoint.LED13Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED13Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED14Hue) && Int32.Parse(State.VmLedPoint.LED14Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED14Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED15Hue) && Int32.Parse(State.VmLedPoint.LED15Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED15Hue = Brushes.Gray;
                    ColCheck = false;
                }
                if (!(State.TestSpec.GreenHueMin < Int32.Parse(State.VmLedPoint.LED16Hue) && Int32.Parse(State.VmLedPoint.LED16Hue) < State.TestSpec.GreenHueMax))
                {
                    State.VmLedPoint.ColLED16Hue = Brushes.Gray;
                    ColCheck = false;
                }

                return ColCheck;
            }
            catch
            {
                return false;
            }
        }






        bool LedOn;
        private async void buttonLedOn_Click(object sender, RoutedEventArgs e)
        {
            if (!General.CheckPress())
            {
                MessageBox.Show("ゴールデンサンプルをセットして\nレバーを下げてください");
                return;
            }

            if (FlagLabeling || ShowHue)
                return;


            LedOn = !LedOn;

            if (LedOn)
            {
                buttonLabeling.IsEnabled = true;
                buttonHue.IsEnabled = true;
                buttonSave.IsEnabled = false;
                buttonLedOn.Background = General.OnBrush;
                await Task.Run(() => General.PowSupply(true));
                await General.LedAllOn();
            }
            else
            {
                buttonLabeling.IsEnabled = false;
                buttonHue.IsEnabled = false;
                buttonSave.IsEnabled = true;

                buttonLedOn.Background = General.OffBrush;
                General.ResetIo();
                resetView();
            }

        }

        bool ShowHue;
        private void buttonHue_Click(object sender, RoutedEventArgs e)
        {
            ShowHue = !ShowHue;

            buttonLabeling.IsEnabled = !ShowHue;

            buttonHue.Background = ShowHue ? General.OnBrush : General.OffBrush;

            if (ShowHue)
            {
                General.cam.ResetFlag();

                Task.Run(() =>
                {
                    while (ShowHue)
                    {
                        TestLed.CheckColorForDebug();
                        Sleep(100);
                    }
                });
            }
            else
            {
                General.cam.ResetFlag();
            }
        }

        //カメラプロパティ調整いろいろ
        private void toggleSw_Checked(object sender, RoutedEventArgs e)
        {
            General.cam.Opening = true;
        }

        private void toggleSw_Unchecked(object sender, RoutedEventArgs e)
        {
            General.cam.Opening = false;
        }



    }
}
