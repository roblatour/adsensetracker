namespace WinAdSenseTracker
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            tbMQTTBrokerPassword = new TextBox();
            tbMQTTBrokerUserID = new TextBox();
            tbMQTTBrokerPort = new TextBox();
            tbMQTTBrokerAddress = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            tbGoogleClientSecret = new TextBox();
            tbGoogleClientID = new TextBox();
            label7 = new Label();
            label8 = new Label();
            groupBox3 = new GroupBox();
            label5 = new Label();
            nudUpdateFrequency = new NumericUpDown();
            btnOK = new Button();
            btnCancel = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudUpdateFrequency).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbMQTTBrokerPassword);
            groupBox1.Controls.Add(tbMQTTBrokerUserID);
            groupBox1.Controls.Add(tbMQTTBrokerPort);
            groupBox1.Controls.Add(tbMQTTBrokerAddress);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 136);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(580, 182);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "MQTT";
            // 
            // tbMQTTBrokerPassword
            // 
            tbMQTTBrokerPassword.Font = new Font("Wingdings", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbMQTTBrokerPassword.Location = new Point(128, 136);
            tbMQTTBrokerPassword.Name = "tbMQTTBrokerPassword";
            tbMQTTBrokerPassword.PasswordChar = 'l';
            tbMQTTBrokerPassword.Size = new Size(429, 21);
            tbMQTTBrokerPassword.TabIndex = 3;
            // 
            // tbMQTTBrokerUserID
            // 
            tbMQTTBrokerUserID.Location = new Point(128, 100);
            tbMQTTBrokerUserID.Name = "tbMQTTBrokerUserID";
            tbMQTTBrokerUserID.Size = new Size(429, 23);
            tbMQTTBrokerUserID.TabIndex = 2;
            // 
            // tbMQTTBrokerPort
            // 
            tbMQTTBrokerPort.Location = new Point(128, 68);
            tbMQTTBrokerPort.Name = "tbMQTTBrokerPort";
            tbMQTTBrokerPort.Size = new Size(429, 23);
            tbMQTTBrokerPort.TabIndex = 1;
            // 
            // tbMQTTBrokerAddress
            // 
            tbMQTTBrokerAddress.Location = new Point(128, 36);
            tbMQTTBrokerAddress.Name = "tbMQTTBrokerAddress";
            tbMQTTBrokerAddress.Size = new Size(429, 23);
            tbMQTTBrokerAddress.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 136);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 3;
            label3.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 100);
            label4.Name = "label4";
            label4.Size = new Size(44, 15);
            label4.TabIndex = 2;
            label4.Text = "User ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 1;
            label2.Text = "Port";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 35);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "URL/IP address";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tbGoogleClientSecret);
            groupBox2.Controls.Add(tbGoogleClientID);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(580, 118);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Google";
            // 
            // tbGoogleClientSecret
            // 
            tbGoogleClientSecret.Font = new Font("Wingdings", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbGoogleClientSecret.Location = new Point(128, 68);
            tbGoogleClientSecret.Name = "tbGoogleClientSecret";
            tbGoogleClientSecret.PasswordChar = 'l';
            tbGoogleClientSecret.Size = new Size(429, 21);
            tbGoogleClientSecret.TabIndex = 1;
            // 
            // tbGoogleClientID
            // 
            tbGoogleClientID.Location = new Point(128, 36);
            tbGoogleClientID.Name = "tbGoogleClientID";
            tbGoogleClientID.Size = new Size(429, 23);
            tbGoogleClientID.TabIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 71);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 1;
            label7.Text = "Client secret";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 35);
            label8.Name = "label8";
            label8.Size = new Size(52, 15);
            label8.TabIndex = 0;
            label8.Text = "Client ID";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(nudUpdateFrequency);
            groupBox3.Location = new Point(12, 324);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(580, 68);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Update frequency";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 34);
            label5.Name = "label5";
            label5.Size = new Size(160, 15);
            label5.TabIndex = 2;
            label5.Text = "Update frequency in minutes";
            // 
            // nudUpdateFrequency
            // 
            nudUpdateFrequency.Location = new Point(492, 32);
            nudUpdateFrequency.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            nudUpdateFrequency.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudUpdateFrequency.Name = "nudUpdateFrequency";
            nudUpdateFrequency.Size = new Size(56, 23);
            nudUpdateFrequency.TabIndex = 0;
            nudUpdateFrequency.TextAlign = HorizontalAlignment.Right;
            nudUpdateFrequency.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // btnOK
            // 
            btnOK.Location = new Point(462, 411);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(130, 35);
            btnOK.TabIndex = 3;
            btnOK.Text = "&OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(12, 411);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 35);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmSettings
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(615, 457);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "frmSettings";
            Text = "WinAdSenseTracker - UpdateSettings";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudUpdateFrequency).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private TextBox tbMQTTBrokerUserID;
        private TextBox tbMQTTBrokerPort;
        private TextBox tbMQTTBrokerAddress;
        private Label label3;
        private Label label4;
        private Label label2;
        private TextBox tbMQTTBrokerPassword;
        private GroupBox groupBox2;
        private TextBox tbGoogleClientSecret;
        private TextBox tbGoogleClientID;
        private Label label7;
        private Label label8;
        private GroupBox groupBox3;
        private Label label5;
        private NumericUpDown nudUpdateFrequency;
        private Button btnOK;
        private Button btnCancel;
    }
}