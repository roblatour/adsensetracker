/*
Copyright 2023 Rob Latour
License: MIT https://rlatour.com/adsensetracker/License.txt
*/

using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAdSenseTracker.Properties;

namespace WinAdSenseTracker
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();

            this.Text = frmMain.ApplicationName + " - Settings";
            this.Icon = Resources.WinAdSenseTracker;

            frmSettings_Load(this, new EventArgs());

        }
        private void frmSettings_Load(object sender, EventArgs e)
        {
            // load the settings from the app.config file

            Properties.Settings.Default.Reload();

            tbGoogleClientID.Text = Properties.Settings.Default.GoogleClientID;

            tbGoogleClientSecret.Text = Properties.Settings.Default.GoogleClientSecret;

            tbMQTTBrokerAddress.Text = Properties.Settings.Default.MQTTBrokerAddress;

            tbMQTTBrokerPort.Text = Properties.Settings.Default.MQTTBrokerPort.ToString();

            tbMQTTBrokerUserID.Text = Properties.Settings.Default.MQTTBrokerUserID;

            tbMQTTBrokerPassword.Text = Properties.Settings.Default.MQTTBrokerPassword;

            nudRefreshFrequency.Value = Properties.Settings.Default.RefreshFrequency;

            // Set focus to the first textbox

            tbGoogleClientID.Focus();

            tbGoogleClientID.Select(tbGoogleClientID.Text.Length, 0);

        }

        private void SaveSettings()
        {
            try
            {
                Properties.Settings.Default.GoogleClientID = tbGoogleClientID.Text.Trim();

                Properties.Settings.Default.GoogleClientSecret = tbGoogleClientSecret.Text.Trim();

                Properties.Settings.Default.MQTTBrokerAddress = tbMQTTBrokerAddress.Text.Trim();

                int portNumber = 0;
                if (int.TryParse(tbMQTTBrokerPort.Text, out portNumber))
                {
                    if (portNumber < 1 || portNumber > 65535)
                        Properties.Settings.Default.MQTTBrokerPort = 1883;
                    else
                        Properties.Settings.Default.MQTTBrokerPort = portNumber;
                }
                else
                    Properties.Settings.Default.MQTTBrokerPort = 1883;

                Properties.Settings.Default.MQTTBrokerUserID = tbMQTTBrokerUserID.Text.Trim();

                Properties.Settings.Default.MQTTBrokerPassword = tbMQTTBrokerPassword.Text.Trim();

                Properties.Settings.Default.RefreshFrequency = (int)nudRefreshFrequency.Value;

                Properties.Settings.Default.Save();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving settings: " + ex.Message, this.Text + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();

            if (frmMain.SettingsNeedToBeUpdated())
            {
                System.Media.SystemSounds.Beep.Play();

                if (MessageBox.Show("There is a problem with your settings.\r\n\r\nPlease click 'Retry' to try again, or\r\n\r\n'Cancel' to exit.", this.Text + " - Confirmation", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
