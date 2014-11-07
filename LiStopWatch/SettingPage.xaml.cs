/*
 * 原作者: 李杰
 * 
 */
using System. Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media;

namespace LiStopWatch
{
    public partial class SettingPage : PhoneApplicationPage
    {
        #region 获取设置状态
        public SettingPage( )
        {
            InitializeComponent( );
            storyboard_1.Begin();
            //this. Loaded += (s, e) =>
            //{
            this.togglePlayer.IsChecked = Config.IsSpeackDown;
            this.toggleBackground.IsChecked = Config.IsBackground;
            this.toggleExit.IsChecked = Config.IsExitWarn;
            this.toggleVibrate.IsChecked = Config.IsVibrate;
            this.toggleStart.IsChecked = Config.IsShakeStart;
            this.toggleStop.IsChecked = Config.IsShakeStop;
            this.sliderLimit.Value = Config.LimitValue;
            this.textBlockLv.Text = Config.LimitValue.ToString();
            //};
        }
        #endregion
        
        #region 存储设置状态
        private void OnToggleExitChecked(object sender, RoutedEventArgs e)
        {
            Config.IsExitWarn = (bool)this.toggleExit.IsChecked;
        }

        private void OnTogglePlayerChecked(object sender, RoutedEventArgs e)
        {
            Config.IsSpeackDown = (bool)this.togglePlayer.IsChecked;
        }

        private void OnToggleVibrateChecked(object sender, RoutedEventArgs e)
        {
            Config.IsVibrate = (bool)this.toggleVibrate.IsChecked;
        }

        private void OnToggleStartChecked(object sender, RoutedEventArgs e)
        {
            Config.IsShakeStart = (bool)this.toggleStart.IsChecked;
        }

        private void toggleBackground_Checked(object sender, RoutedEventArgs e)
        {
            Config.IsBackground = (bool)this.toggleBackground.IsChecked;
        }

        private void OnToggleStopChecked(object sender, RoutedEventArgs e)
        {
            Config.IsShakeStop = (bool)this.toggleStop.IsChecked;
        }
       
        private void sliderLimit_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int lv = (int)this.sliderLimit.Value;
            this.textBlockLv.Text = lv.ToString();
            Config.LimitValue = lv;
        }
        #endregion

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            storyboard_2.Begin();
        }
    }
}