/*
Copyright 2023 Rob Latour
License: MIT https://rlatour.com/adsensetracker/License.txt
*/

using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAdSenseTracker.Properties;

namespace WinAdSenseTracker
{
    partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            this.Text = frmMain.ApplicationName + " - About";
            this.Icon = Resources.WinAdSenseTracker;

            this.labelProductAndVersion.Text = frmMain.ApplicationName + " v" + frmMain.ApplicationVersion;

            this.labelCopyright.Text = AssemblyCopyright;

            this.labelLicense.Text = "License: MIT";

            this.labelWebSite.Text = "rlatour.com/adsensetracker";

            this.textBoxDescription.Text = frmMain.ApplicationName + " makes use of:\r\n\r\n" +
                "MQTTNet\r\nLicense: MIT (https://github.com/dotnet/MQTTnet/blob/master/LICENSE)\r\nhttps://github.com/dotnet/MQTTnet\r\n\r\n" +
                "google-api-dotnet-client\r\nLicense: Apache License 2.0 (https://github.com/googleapis/google-api-dotnet-client/blob/main/LICENSE) \r\nhttps://github.com/googleapis/google-api-dotnet-client\r\n\r\n" +
                "and some code from\r\ngoogleads-adsense-examples\r\nLicense: Apache License 2.0 (https://github.com/googleads/googleads-adsense-examples/blob/main/LICENSE)\r\nhttps://github.com/googleads/googleads-adsense-examples";
        }

        #region Assembly Attribute Accessors             

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        #endregion
        private void labelWebSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "http://rlatour.com/adsensetracker",
                UseShellExecute = true
            });
        }

        private void labelLicense_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://rlatour.com/adsensetracker/License.txt",
                UseShellExecute = true
            });

        }
    }
}
