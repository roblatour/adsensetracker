namespace WinAdSenseTracker
{
    partial class frmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            tableLayoutPanel = new TableLayoutPanel();
            label1 = new Label();
            labelProductAndVersion = new Label();
            labelCopyright = new Label();
            labelLicense = new Label();
            labelWebSite = new Label();
            textBoxDescription = new TextBox();
            okButton = new Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.72215843F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 98.27784F));
            tableLayoutPanel.Controls.Add(label1, 0, 5);
            tableLayoutPanel.Controls.Add(labelProductAndVersion, 1, 0);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 1);
            tableLayoutPanel.Controls.Add(labelLicense, 1, 2);
            tableLayoutPanel.Controls.Add(labelWebSite, 1, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
            tableLayoutPanel.Controls.Add(okButton, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(10, 10);
            tableLayoutPanel.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5.6603775F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 6.36792469F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5.89622641F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 6.26631832F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 65.53525F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.Size = new Size(641, 383);
            tableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(7, 342);
            label1.Margin = new Padding(7, 0, 4, 0);
            label1.MaximumSize = new Size(0, 20);
            label1.Name = "label1";
            label1.Size = new Size(1, 20);
            label1.TabIndex = 25;
            label1.Text = "Website";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelProductAndVersion
            // 
            labelProductAndVersion.Dock = DockStyle.Fill;
            labelProductAndVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelProductAndVersion.Location = new Point(18, 0);
            labelProductAndVersion.Margin = new Padding(7, 0, 4, 0);
            labelProductAndVersion.MaximumSize = new Size(0, 20);
            labelProductAndVersion.Name = "labelProductAndVersion";
            labelProductAndVersion.Size = new Size(619, 20);
            labelProductAndVersion.TabIndex = 19;
            labelProductAndVersion.Text = "Product and version";
            labelProductAndVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock = DockStyle.Fill;
            labelCopyright.Location = new Point(18, 21);
            labelCopyright.Margin = new Padding(7, 0, 4, 0);
            labelCopyright.MaximumSize = new Size(0, 20);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(619, 20);
            labelCopyright.TabIndex = 0;
            labelCopyright.Text = "Copyright";
            labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelLicense
            // 
            labelLicense.Dock = DockStyle.Fill;
            labelLicense.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            labelLicense.ForeColor = SystemColors.MenuHighlight;
            labelLicense.Location = new Point(18, 45);
            labelLicense.Margin = new Padding(7, 0, 4, 0);
            labelLicense.MaximumSize = new Size(0, 20);
            labelLicense.Name = "labelLicense";
            labelLicense.Size = new Size(619, 20);
            labelLicense.TabIndex = 21;
            labelLicense.Text = "License";
            labelLicense.TextAlign = ContentAlignment.MiddleLeft;
            labelLicense.Click += labelLicense_Click;
            // 
            // labelWebSite
            // 
            labelWebSite.Dock = DockStyle.Fill;
            labelWebSite.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            labelWebSite.ForeColor = SystemColors.MenuHighlight;
            labelWebSite.Location = new Point(18, 67);
            labelWebSite.Margin = new Padding(7, 0, 4, 0);
            labelWebSite.MaximumSize = new Size(0, 20);
            labelWebSite.Name = "labelWebSite";
            labelWebSite.Size = new Size(619, 20);
            labelWebSite.TabIndex = 22;
            labelWebSite.Text = "Website";
            labelWebSite.TextAlign = ContentAlignment.MiddleLeft;
            labelWebSite.Click += labelWebSite_Click;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Dock = DockStyle.Fill;
            textBoxDescription.Location = new Point(18, 94);
            textBoxDescription.Margin = new Padding(7, 3, 4, 3);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.Size = new Size(619, 245);
            textBoxDescription.TabIndex = 23;
            textBoxDescription.TabStop = false;
            textBoxDescription.Text = "Description";
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.DialogResult = DialogResult.Cancel;
            okButton.Location = new Point(549, 353);
            okButton.Margin = new Padding(4, 3, 4, 3);
            okButton.Name = "okButton";
            okButton.Size = new Size(88, 27);
            okButton.TabIndex = 24;
            okButton.Text = "&OK";
            // 
            // frmAbout
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(661, 403);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAbout";
            Padding = new Padding(10);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmAbout";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private Label labelProductAndVersion;
        private Label labelCopyright;
        private Label labelLicense;
        private Label labelWebSite;
        private TextBox textBoxDescription;
        private Button okButton;
        private Label label1;
    }
}
