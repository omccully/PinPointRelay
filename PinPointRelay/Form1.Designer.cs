namespace PinPointRelay
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.RefreshButton = new System.Windows.Forms.Button();
            this.COMPortsList = new System.Windows.Forms.ListBox();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.WebServerAddressBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SysTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(234, 9);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 0;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // COMPortsList
            // 
            this.COMPortsList.FormattingEnabled = true;
            this.COMPortsList.Location = new System.Drawing.Point(108, 9);
            this.COMPortsList.Name = "COMPortsList";
            this.COMPortsList.Size = new System.Drawing.Size(120, 69);
            this.COMPortsList.TabIndex = 1;
            this.COMPortsList.SelectedValueChanged += new System.EventHandler(this.COMPortsList_SelectedValueChanged);
            // 
            // LogBox
            // 
            this.LogBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LogBox.Location = new System.Drawing.Point(0, 136);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(436, 117);
            this.LogBox.TabIndex = 2;
            // 
            // WebServerAddressBox
            // 
            this.WebServerAddressBox.Location = new System.Drawing.Point(12, 110);
            this.WebServerAddressBox.Name = "WebServerAddressBox";
            this.WebServerAddressBox.Size = new System.Drawing.Size(412, 20);
            this.WebServerAddressBox.TabIndex = 3;
            this.WebServerAddressBox.TextChanged += new System.EventHandler(this.WebServerAddressBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Destination web server address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Source COM port:";
            // 
            // SysTrayIcon
            // 
            this.SysTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SysTrayIcon.Icon")));
            this.SysTrayIcon.Text = "Pinpoint Relay Station";
            this.SysTrayIcon.Visible = true;
            this.SysTrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SysTrayIcon_MouseDoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 253);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WebServerAddressBox);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.COMPortsList);
            this.Controls.Add(this.RefreshButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Pinpoint Relay Station";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ListBox COMPortsList;
        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.TextBox WebServerAddressBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon SysTrayIcon;
        private System.Windows.Forms.Button button1;
    }
}

