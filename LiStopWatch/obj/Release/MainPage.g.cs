﻿#pragma checksum "E:\Windows Phone\提交App\秒表\1.2.0.6\代码\LiStopWatch\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C348497BEAF341FB1A2ED5563052E22C"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34011
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace LiStopWatch {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama myPage;
        
        internal System.Windows.Controls.TextBlock appTitle;
        
        internal Microsoft.Phone.Controls.PanoramaItem StopWatchItem;
        
        internal Coding4Fun.Phone.Controls.RoundButton btnHour;
        
        internal Coding4Fun.Phone.Controls.RoundButton btnSecond;
        
        internal Coding4Fun.Phone.Controls.RoundButton btnMillisecond;
        
        internal System.Windows.Controls.TextBlock elapsedText;
        
        internal Coding4Fun.Phone.Controls.RoundButton startStopToggle;
        
        internal Coding4Fun.Phone.Controls.RoundButton btnReset;
        
        internal Coding4Fun.Phone.Controls.RoundButton btnRecord;
        
        internal System.Windows.Controls.ListBox listBoxHistory;
        
        internal Microsoft.Phone.Controls.ContextMenu ContextMenuShare;
        
        internal System.Windows.Controls.TextBlock Item2;
        
        internal System.Windows.Controls.TextBlock textBlockDownCnt;
        
        internal System.Windows.Controls.TextBlock downCountText;
        
        internal Coding4Fun.Phone.Controls.RoundButton startStopToggleDown;
        
        internal Coding4Fun.Phone.Controls.RoundButton btnResetDown;
        
        internal Coding4Fun.Phone.Controls.RoundButton btnSettingCnt;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbarSkinButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbarSettingButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbarClearButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbarAboutButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem menuItemScreen;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/LiStopWatch;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.myPage = ((Microsoft.Phone.Controls.Panorama)(this.FindName("myPage")));
            this.appTitle = ((System.Windows.Controls.TextBlock)(this.FindName("appTitle")));
            this.StopWatchItem = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("StopWatchItem")));
            this.btnHour = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("btnHour")));
            this.btnSecond = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("btnSecond")));
            this.btnMillisecond = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("btnMillisecond")));
            this.elapsedText = ((System.Windows.Controls.TextBlock)(this.FindName("elapsedText")));
            this.startStopToggle = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("startStopToggle")));
            this.btnReset = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("btnReset")));
            this.btnRecord = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("btnRecord")));
            this.listBoxHistory = ((System.Windows.Controls.ListBox)(this.FindName("listBoxHistory")));
            this.ContextMenuShare = ((Microsoft.Phone.Controls.ContextMenu)(this.FindName("ContextMenuShare")));
            this.Item2 = ((System.Windows.Controls.TextBlock)(this.FindName("Item2")));
            this.textBlockDownCnt = ((System.Windows.Controls.TextBlock)(this.FindName("textBlockDownCnt")));
            this.downCountText = ((System.Windows.Controls.TextBlock)(this.FindName("downCountText")));
            this.startStopToggleDown = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("startStopToggleDown")));
            this.btnResetDown = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("btnResetDown")));
            this.btnSettingCnt = ((Coding4Fun.Phone.Controls.RoundButton)(this.FindName("btnSettingCnt")));
            this.appbarSkinButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbarSkinButton")));
            this.appbarSettingButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbarSettingButton")));
            this.appbarClearButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbarClearButton")));
            this.appbarAboutButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbarAboutButton")));
            this.menuItemScreen = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("menuItemScreen")));
        }
    }
}
