/*
Copyright 2023 Rob Latour
License: MIT https://rlatour.com/adsensetracker/License.txt
*/

using AdSense.Driver;
using Google.Apis.Adsense.v2;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Reflection.Metadata;
using WinAdSenseTracker.Properties;

namespace WinAdSenseTracker
{
    public partial class frmMain : Form
    {

        public static String ApplicationName;

        public static String ApplicationVersion;

        MqttFactory factory = new MqttFactory();
        IMqttClient client;
        ManagementApiConsumer managementApiConsumer;
        private static readonly int MaxListPageSize = 50;

        // Publishing stuff
        int howOftenToPublish = Properties.Settings.Default.UpdateFrequency * 60 * 1000;

        int lastTotalPageViews = -1;
        int lastTotalClicks = -1;
        decimal lastTotalEarnings = -1;

        bool ShutdownWithoutQuestion = false;         

        public frmMain()
        {
            InitializeComponent();

            InitialeSetup();
        }
        public static bool SettingsNeedToBeUpdated()
        {

            // perform a very basic validation of the settings

            Properties.Settings.Default.Reload();

            return (Properties.Settings.Default.GoogleClientID == "") ||
                 (Properties.Settings.Default.GoogleClientSecret == "") ||
                 (Properties.Settings.Default.MQTTBrokerAddress == "") ||
                 (Properties.Settings.Default.MQTTBrokerPort < 1) ||
                 (Properties.Settings.Default.MQTTBrokerPort > 65536) ||
                 (Properties.Settings.Default.MQTTBrokerUserID == "") ||
                 (Properties.Settings.Default.MQTTBrokerPassword == "");

        }
        private async void InitialeSetup()
        {

            // ensure only one instance of this program is running

            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();
            ApplicationName = assemblyName.Name;

            int count = Process.GetProcesses().Where(p => p.ProcessName == ApplicationName).Count();

            if (count > 1)
            {
                MessageBox.Show("Another instance of " + ApplicationName + " is already running.\r\n\r\nThis instance will now exit.", ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (Application.MessageLoop)
                    Application.Exit();
                else
                    Environment.Exit(1);
            };


            // setup Window and icon stuff    

            this.Text = ApplicationName;
            this.Icon = Resources.WinAdSenseTracker;
                      
            ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".0", "");                       

            notifyIcon1.Text = this.Text;
            notifyIcon1.Icon = this.Icon;

            this.ShowInTaskbar = false;
            this.Visible = false;

            ContextMenuStrip = new ContextMenuStrip();
    
            ToolStripMenuItem header = new ToolStripMenuItem();
            header.Text = ApplicationName + " v" + ApplicationVersion;  
            header.ForeColor = Color.Gray;
            header.Enabled = false; 

            ToolStripSeparator toolStripSeparator1 = new ToolStripSeparator();

            ContextMenuStrip.Items.Add(header);
            ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Show", null, new EventHandler(ShowNow), "Show"));
            ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Settings", null, new EventHandler(UpdateSettings), "Settings"));
            ContextMenuStrip.Items.Add(new ToolStripMenuItem("&About", null, new EventHandler(About), "About"));
            ContextMenuStrip.Items.Add(toolStripSeparator1);
            ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Exit", null, new EventHandler(ExitNow), "Exit"));
            notifyIcon1.ContextMenuStrip = ContextMenuStrip;
            ContextMenuStrip.Refresh();

            this.FormClosing += new FormClosingEventHandler(Form1_Closing);
            this.Resize += new System.EventHandler(this.Form1_Resize);

            // Settings ApplicationVersion mananagement                    

            if (Properties.Settings.Default.AppVersion != ApplicationVersion)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.AppVersion = ApplicationVersion;
                Properties.Settings.Default.Save();
            };

            // perform a very basic settings validation,
            // if something is wrong open the settings window so the user can correct it
            // keep doing this until the user gets it right or cancels

            while (SettingsNeedToBeUpdated())
            {
                frmSettings settingsForm = new frmSettings();
                DialogResult result = settingsForm.ShowDialog(this);
                settingsForm.Dispose();

                if (result == DialogResult.Cancel)
                {
                    if (Application.MessageLoop)
                        Application.Exit();
                    else
                        Environment.Exit(1);
                }

                if (SettingsNeedToBeUpdated())
                {
                    if (MessageBox.Show("There is a problem with your settings.\r\n\r\nPlease click 'Retry' to try again, or\r\n\r\n'Cancel' to exit.", this.Text + " - Confirmation", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        if (Application.MessageLoop)
                            Application.Exit();
                        else
                            Environment.Exit(1);
                    }

                }

            };

            // Google stuff

            // Folder in which the client secrets file is stored
            GoogleWebAuthorizationBroker.Folder = "WinAdSenseTracker.Google";


            // Create the credential

            // the user will be prompted to login to Google and authorize this app if they have not already done so
            // otherwise processing will simply continue without any further user interaction

            DateTime startTime = DateTime.Now;

            var credential =
                GoogleWebAuthorizationBroker
                    .AuthorizeAsync(new ClientSecrets
                    {
                        ClientId = Properties.Settings.Default.GoogleClientID,
                        ClientSecret = Properties.Settings.Default.GoogleClientSecret
                    },
                     new string[] { AdsenseService.Scope.Adsense },
                     "user",
                     CancellationToken.None)
                    .Result;

            TimeSpan duration = DateTime.Now - startTime;


            if (duration.TotalSeconds > 2)
            {

                // if it took more than a couple seconds to create the credential then the user was prompted to login to Google and authorize this app
                // so we now need for up to 20 seconds for that to go live                            

                bool waitForGoogleAuthorizationToGoLive = true;

                while (waitForGoogleAuthorizationToGoLive)
                {

                    var testservice = new AdsenseService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "WinAdsenseTracker"

                    });

                    managementApiConsumer = new ManagementApiConsumer(testservice, MaxListPageSize);
                    managementApiConsumer.SetupCalls();
                    managementApiConsumer.RunCalls();

                    // this process assumes the user will have at least one page view reported, 
                    // if not then we will have to wait the full 20 seconds
                    if (managementApiConsumer.TotalPageViews > 0) 
                    {
                        waitForGoogleAuthorizationToGoLive = false;
                    }
                    else
                    {

                        duration = DateTime.Now - startTime;

                        if (duration.TotalSeconds > 20)               
                           waitForGoogleAuthorizationToGoLive = false;                        
                        else
                           await Task.Delay(1000);                       

                    };

                    testservice.Dispose();              

                };

            };

            // Create the adsense service
            var service = new AdsenseService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "WinAdsenseTracker"
            });

            managementApiConsumer = new ManagementApiConsumer(service, MaxListPageSize);
            managementApiConsumer.SetupCalls();

            // MQTT stuff

            // Create a MQTT client instance 
            client = factory.CreateMqttClient();

            // Create MQTT client options
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(Properties.Settings.Default.MQTTBrokerAddress, Properties.Settings.Default.MQTTBrokerPort)
                .WithCredentials(Properties.Settings.Default.MQTTBrokerUserID, Properties.Settings.Default.MQTTBrokerPassword)
                .WithClientId(Guid.NewGuid().ToString())
                .WithCleanSession()
                .Build();

            btnUpdate.Enabled = false;
            btnUpdate.Text = "MQTT not connected";

            _ = Task.Run(
               async () =>
               {
                   while (true)
                   {
                       try
                       {
                           if ((!client.IsConnected) || (!await client.TryPingAsync()))
                           {
                               await client.ConnectAsync(options, CancellationToken.None);

                               // Subscribe to the topic "adsense/updaterequest"
                               var mqttSubscribeOptions = factory.CreateSubscribeOptionsBuilder()
                                     .WithTopicFilter(
                                        f =>
                                            {
                                                f.WithTopic("adsense/updaterequest");
                                            })
                                    .Build();

                               var response = await client.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                               // If we receive a message on the topic "adsense/updaterequest" with the payload "update please" (which come from the ESP32 when it is booted)
                               // then publish the currently understood values of TotalPageViews and TotalEarnings
                               // this is done without first refreshing these values from Google; they are simply the last values that were published
                               Func<MqttApplicationMessageReceivedEventArgs, Task> funct = async (e) =>
                               {
                                   var message = e.ApplicationMessage;
                                   var topic = message.Topic;
                                   var payload = message.ConvertPayloadToString();

                                   if ((topic == "adsense/updaterequest") && (payload == "update please"))
                                   {
                                       var applicationMessage = new MqttApplicationMessageBuilder()
                                      .WithTopic("adsense/TotalPageViews")
                                      .WithPayload(lastTotalPageViews.ToString())
                                      .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                                      .Build();
                                       await client.PublishAsync(applicationMessage, CancellationToken.None);
                                       lastTotalPageViews = managementApiConsumer.TotalPageViews;

                                       applicationMessage = new MqttApplicationMessageBuilder()
                                       .WithTopic("adsense/TotalClicks")
                                       .WithPayload(lastTotalClicks.ToString())
                                       .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                                       .Build();
                                       await client.PublishAsync(applicationMessage, CancellationToken.None);
                                       lastTotalPageViews = managementApiConsumer.TotalPageViews;


                                       applicationMessage = new MqttApplicationMessageBuilder()
                                      .WithTopic("adsense/TotalEarnings")
                                      .WithPayload(lastTotalEarnings.ToString())
                                      .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                                      .Build();
                                       await client.PublishAsync(applicationMessage, CancellationToken.None);
                                   }
                               };

                               client.ApplicationMessageReceivedAsync += funct;

                           };

                       }
                       catch
                       {
                           // Handle the exception properly (logging etc.).                           
                       }
                       finally
                       {
                           // wait 5 seconds before returning to the top of this loop to perform a reconnect if required
                           await Task.Delay(TimeSpan.FromSeconds(5));
                       }

                   }
               });

            while (!client.IsConnected)
                await Task.Delay(500);

            btnUpdate.Enabled = true;
            btnUpdate.Text = "&Update";

            // Start timer1; setting the interval to 100ms below causes the timer to fire in the next second as opposed to waiting for the first interval to elapse
            timer1.Tag = "First pass";
            timer1.Interval = 100;
            timer1.Start();

        }

        // timer1 is responsible for getting current the total page views and total revenue values from Google and publishing them to the MQTT broker
        //
        // timer1 is fired when initial setup is complete, and after that every fifteen minutes
        // It also fires when the user clicks the 'Check for an update now' button on the main window.

        private async void timer1_Tick(object sender, EventArgs e)
        {

            try
            {

                timer1.Interval = howOftenToPublish;

                if (client.IsConnected)
                {

                    // get the AdSense data from Google
                    managementApiConsumer.RunCalls();

                    // Publish TotalPageViews if it has changed
                    if (managementApiConsumer.TotalPageViews != lastTotalPageViews)
                    {
                        var applicationMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("adsense/TotalPageViews")
                        .WithPayload(managementApiConsumer.TotalPageViews.ToString())
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                        .Build();
                        await client.PublishAsync(applicationMessage, CancellationToken.None);
                        tbTotalPageViews.Text = managementApiConsumer.TotalPageViews.ToString();
                        lastTotalPageViews = managementApiConsumer.TotalPageViews;
                    };

                    // Publish TotalClicks if it has changed
                    if (managementApiConsumer.TotalClicks != lastTotalClicks)
                    {
                        var applicationMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("adsense/TotalClicks")
                        .WithPayload(managementApiConsumer.TotalClicks.ToString())
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                        .Build();
                        await client.PublishAsync(applicationMessage, CancellationToken.None);
                        tbTotalClicks.Text = managementApiConsumer.TotalClicks.ToString();
                        lastTotalClicks = managementApiConsumer.TotalClicks;
                    };

                    // Publish TotalEarnings if it has changed
                    if (managementApiConsumer.TotalEarnings != lastTotalEarnings)
                    {
                        var applicationMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("adsense/TotalEarnings")
                        .WithPayload(managementApiConsumer.TotalEarnings.ToString())
                        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                        .Build();
                        await client.PublishAsync(applicationMessage, CancellationToken.None);
                        tbTotalRevenue.Text = "$" + managementApiConsumer.TotalEarnings.ToString();
                        lastTotalEarnings = managementApiConsumer.TotalEarnings;
                    };

                    if (timer1.Tag == "Update now")
                        btnUpdate.Text = "Update complete!";

                    timer1.Tag = "";
                }
                else
                {
                    btnUpdate.Text = "MQTT not connected";
                    btnUpdate.Enabled = false;
                    timer2.Stop();
                    timer2.Interval = 100;
                    timer2.Start();
                };


                // the following tweaks timer1 so that it fires at one second after midnight
                // by which time Google should start reporting zero values for the day
                // (assuming the PC time is correct)
                      
                DateTime now = DateTime.Now;
                DateTime tomorrow = now.AddDays(1).Date;
                TimeSpan duration = tomorrow - now;
                int minutesUntilMidnight = (int)duration.TotalMinutes;
                if (minutesUntilMidnight <= Properties.Settings.Default.UpdateFrequency)
                {
                    //calculate milliseconds until midnight
                    int millisecondsUntilMidnight = (int)duration.TotalMilliseconds;

                    // set timer1 to fire at one second after midnight
                    timer1.Stop();
                    timer1.Interval = millisecondsUntilMidnight + 1000;
                    timer1.Start();

                }              
               
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        // timer2 is used to keep the user from clicking the 'Update' button
        // for five seconds after last clicking it, and 
        // when the connect is lost
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 5000;

            if (client.IsConnected)
            {
                btnUpdate.Text = "&Update";
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Text = "MQTT not connected";
                btnUpdate.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;

            if (client.IsConnected)
            {
                // reset timer1 to trigger an immediate update
                timer1.Stop();
                timer1.Tag = "Update now";
                timer1.Interval = 100;
                timer1.Start();
            }
            else
                btnUpdate.Text = "MQTT not connected";

            // use timer2 to re-enable the 'Update' button when appropriate
            timer2.Stop();
            timer2.Interval = 5000;
            timer2.Start();

        }
        private void About(object sender, EventArgs e)
        {

            frmAbout frmAbout = new frmAbout();
            DialogResult result = frmAbout.ShowDialog(this);
            frmAbout.Dispose();

        }

        private void UpdateSettings(object sender, EventArgs e)

        {
            String holdGoogleClientId = Properties.Settings.Default.GoogleClientID;
            String holdGoogleClientSecret = Properties.Settings.Default.GoogleClientSecret;
            String holdMqttbrokeraddress = Properties.Settings.Default.MQTTBrokerAddress;
            int holdMqttbrokerport = Properties.Settings.Default.MQTTBrokerPort;
            String holdMqttbrokerusername = Properties.Settings.Default.MQTTBrokerUserID;
            String holdMqttbrokerpassword = Properties.Settings.Default.MQTTBrokerPassword;
            int holdUpdateFrequency = Properties.Settings.Default.UpdateFrequency;

            frmSettings settingsForm = new frmSettings();
            DialogResult result = settingsForm.ShowDialog(this);
            settingsForm.Dispose();

            if (result == DialogResult.OK)
            {

                bool somethingChanged = ((holdGoogleClientId != Properties.Settings.Default.GoogleClientID) ||
                                       (holdGoogleClientSecret != Properties.Settings.Default.GoogleClientSecret) ||
                                       (holdMqttbrokeraddress != Properties.Settings.Default.MQTTBrokerAddress) ||
                                       (holdMqttbrokerport != Properties.Settings.Default.MQTTBrokerPort) ||
                                       (holdMqttbrokerusername != Properties.Settings.Default.MQTTBrokerUserID) ||
                                       (holdMqttbrokerpassword != Properties.Settings.Default.MQTTBrokerPassword) ||
                                       (holdUpdateFrequency != Properties.Settings.Default.UpdateFrequency));

                if (somethingChanged)
                {
                    ShutdownWithoutQuestion = true;
                    if (MessageBox.Show("For your changed settings to take effect this program needs to be restarted.\r\n\r\nPlease click 'OK' to have the program automatically restarted, or 'Cancel' to exit it.", this.Text + " - Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        Application.Restart();
                    else
                        this.Close();

                }
            }

        }

        private void ShowNow(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ExitNow(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_Resize(Object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private async void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((ShutdownWithoutQuestion) || (MessageBox.Show("Do you really quit?", this.Text + " - Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                if (client.IsConnected)
                {
                    await client.DisconnectAsync(new MqttClientDisconnectOptionsBuilder().WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection).Build());
                };
            }
            else
            {
                e.Cancel = true;
            }
        }

    }

}