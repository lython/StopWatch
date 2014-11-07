using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Info;
using System.Windows.Navigation;
using Windows.System;

namespace InternetOfThing
{
    public partial class PivotPageAbout : PhoneApplicationPage
    {
        public PivotPageAbout()
        {
            InitializeComponent();
            storyboard_1.Begin();
        }

        private void version_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //登录商店
            MarketplaceDetailTask task = new MarketplaceDetailTask { };
            task.Show();
        }

        private void email_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string OSVersion = Environment.OSVersion.ToString();   //OS版本
            string manufacturer = DeviceStatus.DeviceManufacturer.ToString();       //硬件厂商
            string name = DeviceStatus.DeviceName.ToString();   //设备型号
            //发送电子邮件
            EmailComposeTask task = new EmailComposeTask
            {
                To = "lython@outlook.com",
                Subject = "[Lython][WP8]" + this.pivot.Title.ToString() + version.Title.ToString(),
                Body = "\n\n\n生产厂商：" + manufacturer + "\n手机型号：" + name + "\nOS版本：" + OSVersion,
            };
            task.Show();
        }

        private void star_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //评价
            MarketplaceReviewTask task = new MarketplaceReviewTask();
            task.Show();
        }

        private async void searchStore_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //登录商店搜索
            await Launcher.LaunchUriAsync(new System.Uri("zune:search?publisher=Lython"));
        }

        private void shareapp_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string content = String.Format("我在微软Windows Phone应用商店发现这款 {0} 相当不错，推荐给你，点击 http://www.windowsphone.com/s?appid=063f4d60-5aea-4c7f-8cda-ad32ed0e6803 可以下载哦！",
                this.pivot.Title.ToString());
            //发送电子邮件
            SmsComposeTask task = new SmsComposeTask
            {
                Body = content,
            };
            task.Show();
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            storyboard_2.Begin();
        }
    }
}