﻿#pragma checksum "E:\Windows Phone\提交App\秒表\1.2.0.6\代码\LiStopWatch\LoopPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ED447DEB6F8ABFC2A757A28500A8DF39"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34011
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Primitives;
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
    
    
    public partial class LoopPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.PlaneProjection translate;
        
        internal System.Windows.Media.Animation.Storyboard storyboard_1;
        
        internal System.Windows.Media.Animation.Storyboard storyboard_2;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.Grid grid1;
        
        internal Microsoft.Phone.Controls.Primitives.LoopingSelector selectorOne;
        
        internal Microsoft.Phone.Controls.Primitives.LoopingSelector selectorTwo;
        
        internal Microsoft.Phone.Controls.Primitives.LoopingSelector selectorThree;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbar_ok;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appbar_cancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LiStopWatch;component/LoopPage.xaml", System.UriKind.Relative));
            this.translate = ((System.Windows.Media.PlaneProjection)(this.FindName("translate")));
            this.storyboard_1 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("storyboard_1")));
            this.storyboard_2 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("storyboard_2")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.grid1 = ((System.Windows.Controls.Grid)(this.FindName("grid1")));
            this.selectorOne = ((Microsoft.Phone.Controls.Primitives.LoopingSelector)(this.FindName("selectorOne")));
            this.selectorTwo = ((Microsoft.Phone.Controls.Primitives.LoopingSelector)(this.FindName("selectorTwo")));
            this.selectorThree = ((Microsoft.Phone.Controls.Primitives.LoopingSelector)(this.FindName("selectorThree")));
            this.appbar_ok = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbar_ok")));
            this.appbar_cancel = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appbar_cancel")));
        }
    }
}

