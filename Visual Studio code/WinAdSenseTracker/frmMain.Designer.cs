namespace WinAdSenseTracker
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            label2 = new Label();
            tbTotalRevenue = new TextBox();
            tbTotalPageViews = new TextBox();
            notifyIcon1 = new NotifyIcon(components);
            btnUpdate = new Button();
            timer2 = new System.Windows.Forms.Timer(components);
            label3 = new Label();
            tbTotalClicks = new TextBox();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.DodgerBlue;
            label1.Location = new Point(18, -1);
            label1.Name = "label1";
            label1.Size = new Size(66, 30);
            label1.TabIndex = 0;
            label1.Text = "Views";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.MediumSeaGreen;
            label2.Location = new Point(18, 57);
            label2.Name = "label2";
            label2.Size = new Size(92, 30);
            label2.TabIndex = 1;
            label2.Text = "Revenue";
            // 
            // tbTotalRevenue
            // 
            tbTotalRevenue.BackColor = SystemColors.Control;
            tbTotalRevenue.BorderStyle = BorderStyle.None;
            tbTotalRevenue.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbTotalRevenue.ForeColor = Color.MediumSeaGreen;
            tbTotalRevenue.Location = new Point(174, 57);
            tbTotalRevenue.Name = "tbTotalRevenue";
            tbTotalRevenue.Size = new Size(102, 28);
            tbTotalRevenue.TabIndex = 2;
            tbTotalRevenue.TabStop = false;
            tbTotalRevenue.Text = "$0.00";
            tbTotalRevenue.TextAlign = HorizontalAlignment.Right;
            // 
            // tbTotalPageViews
            // 
            tbTotalPageViews.BackColor = SystemColors.Control;
            tbTotalPageViews.BorderStyle = BorderStyle.None;
            tbTotalPageViews.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbTotalPageViews.ForeColor = Color.DodgerBlue;
            tbTotalPageViews.Location = new Point(174, -1);
            tbTotalPageViews.Name = "tbTotalPageViews";
            tbTotalPageViews.Size = new Size(102, 28);
            tbTotalPageViews.TabIndex = 3;
            tbTotalPageViews.TabStop = false;
            tbTotalPageViews.Text = "0";
            tbTotalPageViews.TextAlign = HorizontalAlignment.Right;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(18, 95);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(258, 27);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "&Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // timer2
            // 
            timer2.Tick += timer2_Tick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.DarkTurquoise;
            label3.Location = new Point(18, 28);
            label3.Name = "label3";
            label3.Size = new Size(65, 30);
            label3.TabIndex = 5;
            label3.Text = "Clicks";
            // 
            // tbTotalClicks
            // 
            tbTotalClicks.BackColor = SystemColors.Control;
            tbTotalClicks.BorderStyle = BorderStyle.None;
            tbTotalClicks.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            tbTotalClicks.ForeColor = Color.DarkTurquoise;
            tbTotalClicks.Location = new Point(174, 28);
            tbTotalClicks.Name = "tbTotalClicks";
            tbTotalClicks.Size = new Size(102, 28);
            tbTotalClicks.TabIndex = 6;
            tbTotalClicks.TabStop = false;
            tbTotalClicks.Text = "0";
            tbTotalClicks.TextAlign = HorizontalAlignment.Right;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(301, 133);
            Controls.Add(tbTotalClicks);
            Controls.Add(label3);
            Controls.Add(btnUpdate);
            Controls.Add(tbTotalPageViews);
            Controls.Add(tbTotalRevenue);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "WinAdSenseTracker";
            WindowState = FormWindowState.Minimized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private Label label2;
        private TextBox tbTotalRevenue;
        private TextBox tbTotalPageViews;
        private NotifyIcon notifyIcon1;
        private Button btnUpdate;
        private System.Windows.Forms.Timer timer2;
        private Label label3;
        private TextBox tbTotalClicks;
    }
}