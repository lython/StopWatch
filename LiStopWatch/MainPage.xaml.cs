/**
 * 1.作者：李杰
 * 2.本次更新更正了提示音播放，没有使用MediaElement的方法，而是采用了Microsoft.Xna.Framework.Audio
 *   中的SoundEffect方法，这样就不会打断播放器音乐播放。
 * 3.2012.11.24
 */
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Globalization;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives; //Popup
using System.Windows.Resources;
using System.IO.IsolatedStorage;

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Audio;
using Windows.Phone.Speech.Synthesis; //语音
using Windows.Phone.Devices.Notification;

using Coding4Fun.Phone.Controls;
using ShakeGestures;

namespace LiStopWatch
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        Stopwatch stopwatch = new Stopwatch();  //秒表
        TimeSpan suspensionAdjustment = new TimeSpan();  //时间间隔
        TimeSpan elapsedTime = new TimeSpan();
        TimeSpan lastTime = new TimeSpan(); //上一次记录的时间
        string decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator; //10进制的小数点
        int flag = 0;  //时分秒、秒、毫秒切换
        bool onOffFlag = true;  //秒表开关标志位
        bool appbarFlag = true; //工具栏状态切换
        bool screenLightFlag = false;

        Stopwatch stopwatchDown = new Stopwatch();  //倒计时
        public static TimeSpan downTimeSpan = new TimeSpan();  //倒计时时间间隔
        TimeSpan lessTime; //时间差
        bool onOffFlagDown = true;  //倒计时开关标志位
        int currentPage = 0;  //当前页，用来判断摇一摇
        bool IsDownEnd = false; //判断是否倒计时结束
        int recCount = 0; //记录数据
        int downCnt = 0; //倒计时秒长
        VibrationDevice vibrate_alarm = VibrationDevice.GetDefault();

        PhotoChooserTask photoChooser = new PhotoChooserTask();  //照片选择器

        private SpeechSynthesizer synthesizer; //TTS语音
        int speechCnt = 10; //语音计数

        SoundEffectInstance soundInstance = null;
            
        public MainPage()
        {
            InitializeComponent();
            photoChooser.Completed += new EventHandler<PhotoResult>(OnPhotoChooserCompleted);
            synthesizer = new SpeechSynthesizer();

            //摇一摇初始化
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(OnShakeGesture);
            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 2; //摇动次数
            //ShakeGesturesHelper.Instance.StillCounterThreshold = 1000;
            ShakeGesturesHelper.Instance.MinimumShakeVectorsNeededForShake = 100 - Config.LimitValue;
            //ShakeGesturesHelper.Instance.MinimumStillVectorsNeededForAverage = 100 - Config.LimitValue;
            ShakeGesturesHelper.Instance.Active = true;

            Stream stream = null;
            StreamResourceInfo info = Application.GetResourceStream(new Uri("Source/zx.wav", UriKind.Relative));
            if (stream == null) stream = info.Stream;
            SoundEffect sound = SoundEffect.FromStream(stream);
            soundInstance = sound.CreateInstance();
            soundInstance.IsLooped = true;
        }

        #region 普通时间
        string getLocalTime()
        {
            //格式化系统时间
            DateTime now = DateTime.Now;
            string hour = now.Hour.ToString();
            string minute = now.Minute.ToString();
            string second = now.Second.ToString();
            if (minute.Length == 1) minute = '0' + minute;
            if (second.Length == 1) second = '0' + second;
            return hour + ':' + minute + ':' + second;
        }
        #endregion

        #region 跑表
        private void ClockChange_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //时钟按钮切换
            RoundButton btn = sender as RoundButton;
            if (btn != null)
            {
                btn.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
                switch (btn.Name)
                {
                    case "btnHour":
                        flag = 0;
                        this.btnHour.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
                        this.btnSecond.Background = new SolidColorBrush(Colors.Transparent);
                        this.btnMillisecond.Background = new SolidColorBrush(Colors.Transparent);
                        break;
                    case "btnSecond":
                        flag = 1;
                        this.btnSecond.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
                        this.btnMillisecond.Background = new SolidColorBrush(Colors.Transparent);
                        this.btnHour.Background = new SolidColorBrush(Colors.Transparent);
                        break;
                    case "btnMillisecond":
                        flag = 2;
                        this.btnMillisecond.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
                        this.btnHour.Background = new SolidColorBrush(Colors.Transparent);
                        this.btnSecond.Background = new SolidColorBrush(Colors.Transparent);
                        break;
                }
            }
        }

        private void startTimer()
        {
            //开始计时
            stopwatch.Start();
            CompositionTarget.Rendering += OnCompositionTargetRendering;
            this.startStopToggle.Content = "停止";
            this.startStopToggle.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
            onOffFlag = false;

            Uri uri = new Uri("Images/appbar.off.png", UriKind.Relative);
            BitmapImage bmp = new BitmapImage(uri);
            this.startStopToggle.ImageSource = bmp;
        }

        private void stopTimer()
        {
            //停止计数
            stopwatch.Stop();
            startStopToggle.Content = "开始";
            CompositionTarget.Rendering -= OnCompositionTargetRendering;
            this.startStopToggle.Background = new SolidColorBrush(Colors.Transparent);
            onOffFlag = true;

            Uri uri = new Uri("Images/appbar.on.png", UriKind.Relative);
            BitmapImage bmp = new BitmapImage(uri);
            this.startStopToggle.ImageSource = bmp;
        }

        private void startStopToggle_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            //开始、停止计时
            if (onOffFlag)
            {
                startTimer();
            }
            else
            {
                stopTimer();
            }
        }

        void OnCompositionTargetRendering(object sender, EventArgs args)
        {
            DisplayTime();
        }

        void DisplayTime()
        {
            elapsedTime = stopwatch.Elapsed + suspensionAdjustment;
            string str = null;
            switch (flag)
            {
                case 0:
                    str = String.Format("{0:D2}:{1:D2}:{2:D2}{3}{4:D2}",
                                        elapsedTime.Hours, elapsedTime.Minutes,
                                        elapsedTime.Seconds, decimalSeparator,
                                        elapsedTime.Milliseconds / 10);
                    break;
                case 1:
                    str = String.Format("{0:F2} s", elapsedTime.TotalSeconds);
                    break;
                case 2:
                    str = String.Format("{0:F0} ms", elapsedTime.TotalMilliseconds);
                    break;
            }

            elapsedText.Text = str;
        }

        private void btnReset_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            stopwatch.Reset();
            suspensionAdjustment = new TimeSpan();
            DisplayTime();
            onOffFlag = true;
            startStopToggle.Content = "开始";
            this.startStopToggle.Background = new SolidColorBrush(Colors.Transparent);
            Uri uri = new Uri("Images/appbar.on.png", UriKind.Relative);
            BitmapImage bmp = new BitmapImage(uri);
            this.startStopToggle.ImageSource = bmp;
            lastTime = TimeSpan.Zero;
        }

        private void btnRecord_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //记录
            recCount++;
            string str = "";
            TimeSpan differTime = elapsedTime - lastTime;
            switch (flag)
            {
                case 0:
                    str = String.Format("[{0}] {1:D2}:{2:D2}:{3:D2}{4}{5:D2} Δ={6:D2}:{7:D2}:{8:D2}{9}{10:D2}",
                                        recCount, elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, decimalSeparator,elapsedTime.Milliseconds / 10,
                                        differTime.Hours, differTime.Minutes, differTime.Seconds, decimalSeparator, differTime.Milliseconds / 10);
                    break;
                case 1:
                    str = String.Format("[{0}] {1:F2} s Δ={2:F2} s", recCount, elapsedTime.TotalSeconds, differTime.TotalSeconds);
                    break;
                case 2:
                    str = String.Format("[{0}] {1:F0} ms Δ={2:F0} ms", recCount, elapsedTime.TotalMilliseconds, differTime.TotalMilliseconds);
                    break;
            }
            lastTime = elapsedTime;
            this.listBoxHistory.Items.Add(str);
            this.listBoxHistory.SelectedIndex = recCount-1;
        }

        private void MenuItem_Copy_Click(object sender, RoutedEventArgs e)
        {
            //复制到剪切板
            Clipboard.SetText(this.listBoxHistory.SelectedItem.ToString());
        }

        private void MenuItem_SMS_Click(object sender, RoutedEventArgs e)
        {
            //发送短信
            SmsComposeTask task = new SmsComposeTask
            {
                Body = this.listBoxHistory.SelectedItem.ToString(),
            };
            task.Show();
        }

        private void MenuItem_Email_Click(object sender, RoutedEventArgs e)
        {
            //发送电子邮件
            string all_msg = null;
            for (int i = 0; i < this.listBoxHistory.Items.Count; i++)
            {
                this.listBoxHistory.SelectedIndex = i;
                all_msg += this.listBoxHistory.SelectedItem.ToString() + "\n";
            }
            EmailComposeTask task = new EmailComposeTask
            {
                Subject = "秒表数据列表",
                Body = all_msg,
            };
            task.Show();
        }

        private void listBoxHistory_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (this.listBoxHistory.Items.Count == 0)
            {
                ContextMenuShare.IsEnabled = false;
            }
            else
            {
                ContextMenuShare.IsEnabled = true;
            }
        }

        #endregion

        #region 倒计时
        private void OnCountDownRendering(object sender, EventArgs args)
        {
            DisplayCountDown();
        }

        private void DisplayCountDown()
        {
            TimeSpan elapsedTime = stopwatchDown.Elapsed + suspensionAdjustment;
            lessTime = downTimeSpan - elapsedTime;
            string strer = String.Format("{0:D2}:{1:D2}:{2:D2}{3}{4:D2}",
                    lessTime.Hours, lessTime.Minutes, lessTime.Seconds, decimalSeparator, lessTime.Milliseconds / 10);
            downCountText.Text = strer;
            if (Config.IsSpeackDown)
            {
                if (downCnt > 3) //大于三秒才会3,2,1报数
                {
                    int lessTimeCnt = (int)lessTime.TotalSeconds;
                    if (lessTimeCnt <= 3 && lessTimeCnt != speechCnt)
                    {
                        OnSpeckText(lessTimeCnt);
                        speechCnt = lessTimeCnt;
                    }
                }
            }

            if (lessTime.TotalMilliseconds <= 0)
            {
                if (Config.IsSpeackDown)
                {
                    stopwatchDown.Stop();
                    CompositionTarget.Rendering -= OnCountDownRendering;
                }
                else
                {
                    stopTimerDown();
                }

                Uri img = new Uri("/Images/noti.png", UriKind.Relative);
                BitmapImage bmp = new BitmapImage(img);
                ToastPrompt toast = new ToastPrompt
                {
                    ImageSource = bmp,
                    Title = "时间到!",
                };
                toast.Show();
                downCountText.Text = "00:00:00.00";

                if (Config.IsVibrate) vibrate_alarm.Vibrate(TimeSpan.FromSeconds(0.5));  //震动0.5秒

                if (downCnt <= 3) 
                {
                    if (Config.IsSpeackDown) //播放提示音乐
                    {
                        playSoundEffect();
                    }
                }

                IsDownEnd = true;
            }
        }

        private async void OnSpeckText(int msg)
        {
            try
            {
                if (msg >= 0)
                {
                    await synthesizer.SpeakTextAsync(msg.ToString());
                }
            }
            catch { };
            if (msg <= 0)
            {
                if (Config.IsSpeackDown)
                {
                    playSoundEffect();
                }
            }
        }

        private void playSoundEffect()
        {
            //播放提示音乐
            //Stream stream = null;
            //StreamResourceInfo info = Application.GetResourceStream(new Uri("Source/alarm.wav", UriKind.Relative));
            //if (stream == null) stream = info.Stream;
            //SoundEffect sound = SoundEffect.FromStream(stream);
            soundInstance.Play();
        }

        private void startTimerDown()
        {
            //开始倒计时计时
            if (Config.SecondNum >= 0)
            {
                if (IsDownEnd) //如果倒计时已经完成则重置
                {
                    resetTimerDown();
                }
                stopwatchDown.Start();
                CompositionTarget.Rendering += OnCountDownRendering;
                this.startStopToggleDown.Content = "停止";
                this.startStopToggleDown.Background = Application.Current.Resources["PhoneAccentBrush"] as SolidColorBrush;
                onOffFlagDown = false;
                Uri uri = new Uri("Images/appbar.off.png", UriKind.Relative);
                BitmapImage bmp = new BitmapImage(uri);
                this.startStopToggleDown.ImageSource = bmp;
                IsDownEnd = false;
            }
            else
            {
                MessageBox.Show("时间设置错误，请重新设置！");
            }
        }

        private void stopTimerDown()
        {
            //停止计数
            stopwatchDown.Stop();
            CompositionTarget.Rendering -= OnCountDownRendering;
            startStopToggleDown.Content = "开始";
            this.startStopToggleDown.Background = new SolidColorBrush(Colors.Transparent);
            onOffFlagDown = true;

            Uri uri = new Uri("Images/appbar.on.png", UriKind.Relative);
            BitmapImage bmp = new BitmapImage(uri);
            this.startStopToggleDown.ImageSource = bmp;
            soundInstance.Stop();
        }

        private void resetTimerDown()
        {
            stopwatchDown.Reset();
            suspensionAdjustment = new TimeSpan();
            DisplayCountDown();
            onOffFlagDown = true;
            startStopToggleDown.Content = "开始";
            this.startStopToggleDown.Background = new SolidColorBrush(Colors.Transparent);
            Uri uri = new Uri("Images/appbar.on.png", UriKind.Relative);
            BitmapImage bmp = new BitmapImage(uri);
            this.startStopToggleDown.ImageSource = bmp;
            soundInstance.Stop();
        }

        private void startStopToggleDown_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            //开始、停止计时
            if (onOffFlagDown)
            {
                startTimerDown();
            }
            else
            {
                stopTimerDown();
            }
        }

        private void btnResetDown_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            resetTimerDown();
        }

        private void btnSettingCnt_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LoopPage.xaml", UriKind.Relative));
        }
        #endregion

        #region 摇一摇
        private void OnShakeGesture(object sender, ShakeGestureEventArgs e)
        {
            //摇一摇计时
            if (currentPage == 0)
            {
                if (Config.IsShakeStart && onOffFlag)
                {
                    this.Dispatcher.BeginInvoke(() =>
                    {
                        startTimer();
                    });
                }
                else if (Config.IsShakeStop && !onOffFlag)
                {
                    this.Dispatcher.BeginInvoke(() =>
                    {
                        stopTimer();
                    });
                }
            }
            else if (currentPage == 2)
            {
                if (Config.IsShakeStart && onOffFlagDown)
                {
                    this.Dispatcher.BeginInvoke(() =>
                    {
                        startTimerDown();
                    });
                }
                else if (Config.IsShakeStop && !onOffFlagDown)
                {
                    this.Dispatcher.BeginInvoke(() =>
                    {
                        stopTimerDown();
                    });
                }
            } 
        }
        #endregion

        #region 页面切换
        private void myPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //页面切换触发
            currentPage = myPage.SelectedIndex;
        }
        #endregion

        #region 工具栏
        void OnAppbarAboutClick(object sender, EventArgs e)
        {
            //about工具栏
            NavigationService.Navigate(new Uri("/PivotPageAbout.xaml", UriKind.Relative));
        }

        void OnAppbarSettingClick(object sender, EventArgs e)
        {
            //settings工具栏
            NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        void OnAppbarClearClick(object sender, EventArgs e)
        {
            //Clear工具栏
            recCount = 0;
            this.listBoxHistory.Items.Clear();
        }

        private void menuItemScreen_Click(object sender, EventArgs e)
        {
            //开启屏幕常亮
            if (screenLightFlag == false)
            {
                (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).Text = "屏幕常亮：已开启";
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
                screenLightFlag = true;
                ToastPrompt toast = new ToastPrompt
                {
                    Title = "屏幕常亮已开启",
                };
                toast.Show();
                SystemTray.ForegroundColor = Colors.Red;
            }
            else
            {
                (ApplicationBar.MenuItems[0] as ApplicationBarMenuItem).Text = "屏幕常亮：已关闭";
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
                screenLightFlag = false;
                ToastPrompt toast = new ToastPrompt
                {
                    Title = "屏幕常亮已关闭",
                };
                toast.Show();
                SystemTray.ForegroundColor = Colors.White;
            }
        }

        //切换工具栏背景透明度，暂时还没用
        private void ApplicationBar_StateChanged(object sender, ApplicationBarStateChangedEventArgs e)
        {
            if (appbarFlag == true)
            {
                ApplicationBar.Opacity = 0.99;
                appbarFlag = false;
            }
            else
            {
                ApplicationBar.Opacity = 0.01;
                appbarFlag = true;
            }
        }

        //更换皮肤
        void OnAppbarSkinClick(object sender, EventArgs e)
        {
            photoChooser.PixelWidth = 1280;
            photoChooser.PixelHeight = 768;
            photoChooser.ShowCamera = true;
            photoChooser.Show();
        }

        void OnPhotoChooserCompleted(object sender, PhotoResult e)
        {
            if (e.Error == null && e.ChosenPhoto != null)
            {
                ImageBrush img = new ImageBrush();
                WriteableBitmap bmp = Microsoft.Phone.PictureDecoder.DecodeJpeg(e.ChosenPhoto);
                IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
                string filename = e.OriginalFileName.Substring(e.OriginalFileName.LastIndexOf('\\') + 1);
                if (isf.FileExists(filename)) //如果已经存在这个文件，则将这个文件删除
                {
                    isf.DeleteFile(filename);
                }
                IsolatedStorageFileStream PhotoStream = isf.CreateFile(filename);
                Extensions.SaveJpeg(bmp, PhotoStream, bmp.PixelWidth, bmp.PixelHeight, 0, 85); //这里设置保存后图片的大
                PhotoStream.Close();    //写入完毕，关闭文件流

                img.ImageSource = bmp;
                this.myPage.Background = img;
                //if (isf.FileExists(Config.BackImg)) isf.DeleteFile(Config.BackImg); //删除上一个皮肤，WP8里面会报错
                Config.BackImg = filename;
                Config.IsBackground = true;
            }
        }
        #endregion

        #region 墓碑化
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            //激活墓碑化程序
            downTimeSpan = new TimeSpan(Config.HourNum, Config.MinuteNum, Config.SecondNum);
            downCnt = Config.HourNum * 3600 + Config.MinuteNum * 60 + Config.SecondNum;
            if (downCnt < 60) this.textBlockDownCnt.Text = String.Format("{0}秒",Config.SecondNum);
            else if (downCnt < 3599 && downCnt >= 60) this.textBlockDownCnt.Text = String.Format("{0}分{1}秒", Config.MinuteNum, Config.SecondNum);
            else if (downCnt >= 3600) this.textBlockDownCnt.Text = String.Format("{0}时{1}分{2}秒", Config.HourNum, Config.MinuteNum, Config.SecondNum);
            
            ShakeGesturesHelper.Instance.ShakeGesture += new EventHandler<ShakeGestureEventArgs>(OnShakeGesture);
            ShakeGesturesHelper.Instance.MinimumRequiredMovesForShake = 2;  //摇动次数
            ShakeGesturesHelper.Instance.MinimumShakeVectorsNeededForShake = 100 - Config.LimitValue;
            ShakeGesturesHelper.Instance.Active = true;

            //换皮肤
            try
            {
                ImageBrush img = new ImageBrush();
                BitmapImage bmp = new BitmapImage();
                if (Config.IsBackground == true)
                {
                    using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(Config.BackImg, FileMode.Open, FileAccess.Read))
                        {
                            bmp.SetSource(fileStream);
                            img.ImageSource = bmp;
                        }
                    }
                }
                else
                {
                    Uri uri = new Uri("PanoramaBackground.png", UriKind.Relative);
                    bmp.UriSource = uri;
                    img.ImageSource = bmp;
                }
                this.myPage.Background = img;
            }
            catch { }
            base.OnNavigatedTo(args);
        }
        #endregion

        private void Page_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ContextMenuShare.IsOpen)
            {
                ContextMenuShare.IsOpen = false;
                e.Cancel = true;
                return;
            }
            if (Config.IsExitWarn)
            {
                if (this.NavigationService.CanGoBack == false)
                {
                    if (MessageBox.Show("", "确定退出？",
                        MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        base.OnBackKeyPress(e);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                base.OnBackKeyPress(e);
            }
            else
            {
                base.OnBackKeyPress(e);
            }
        }
    }
}