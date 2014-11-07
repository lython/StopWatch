using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Info;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace LiStopWatch
{
    public class Config
    {
        //倒计时最后三秒报数
        public static bool IsSpeackDown
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("IsSpeackDown") ?
                       (bool)IsolatedStorageSettings.ApplicationSettings["IsSpeackDown"] : true;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["IsSpeackDown"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //倒计时震动提醒
        public static bool IsVibrate
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("IsVibrate") ?
                       (bool)IsolatedStorageSettings.ApplicationSettings["IsVibrate"] : false;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["IsVibrate"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //自定义背景
        public static bool IsBackground
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("IsBackground") ?
                       (bool)IsolatedStorageSettings.ApplicationSettings["IsBackground"] : false;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["IsBackground"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //摇动开始
        public static bool IsShakeStart
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("IsShakeStart") ?
                       (bool)IsolatedStorageSettings.ApplicationSettings["IsShakeStart"] : false;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["IsShakeStart"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //摇动停止
        public static bool IsShakeStop
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("IsShakeStop") ?
                       (bool)IsolatedStorageSettings.ApplicationSettings["IsShakeStop"] : false;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["IsShakeStop"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //灵敏度调整
        public static int LimitValue
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("LimitValue") ?
                       (int)IsolatedStorageSettings.ApplicationSettings["LimitValue"] : 80;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["LimitValue"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //背景图
        public static string BackImg
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("BackImg") ?
                       (string)IsolatedStorageSettings.ApplicationSettings["BackImg"] : null;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["BackImg"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //倒计时,小时
        public static int HourNum
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("HourNum") ?
                       (int)IsolatedStorageSettings.ApplicationSettings["HourNum"] : 0;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["HourNum"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //倒计时,分钟
        public static int MinuteNum
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("MinuteNum") ?
                       (int)IsolatedStorageSettings.ApplicationSettings["MinuteNum"] : 0;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["MinuteNum"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //倒计时,秒
        public static int SecondNum
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("SecondNum") ?
                       (int)IsolatedStorageSettings.ApplicationSettings["SecondNum"] : 10;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["SecondNum"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        //退出提示
        public static bool IsExitWarn
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.Contains("IsExitWarn") ?
                       (bool)IsolatedStorageSettings.ApplicationSettings["IsExitWarn"] : true;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["IsExitWarn"] = value;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }
    }
}
