﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace LiStopWatch
{
    class AssociationUriMapper : UriMapperBase
    {
        private string tempUri;

        public override Uri MapUri(Uri uri)
        {
            //tempUri = uri.ToString();
            tempUri = System.Net.HttpUtility.UrlDecode(uri.ToString());

            if (tempUri.Contains("stopwatch"))
            {
                return new Uri("/MainPage.xaml?goto=" + tempUri, UriKind.Relative);
            }
            return uri;
        }

    }
}