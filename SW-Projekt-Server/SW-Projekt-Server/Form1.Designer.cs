namespace SW_Projekt_Server
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
            this.ListIPs = new System.Windows.Forms.ComboBox();
            this.DataBox = new System.Windows.Forms.RichTextBox();
            this.Update_Button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.FarbPanel = new System.Windows.Forms.Panel();
            this.FehlerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListIPs
            // 
            this.ListIPs.FormattingEnabled = true;
            this.ListIPs.Location = new System.Drawing.Point(11, 42);
            this.ListIPs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ListIPs.Name = "ListIPs";
            this.ListIPs.Size = new System.Drawing.Size(92, 21);
            this.ListIPs.TabIndex = 1;
            this.ListIPs.SelectedIndexChanged += new System.EventHandler(this.ListIPs_SelectedIndexChanged);
            // 
            // DataBox
            // 
            this.DataBox.Location = new System.Drawing.Point(11, 80);
            this.DataBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DataBox.Name = "DataBox";
            this.DataBox.ReadOnly = true;
            this.DataBox.Size = new System.Drawing.Size(192, 139);
            this.DataBox.TabIndex = 2;
            this.DataBox.Text = "";
            // 
            // Update_Button
            // 
            this.Update_Button.Location = new System.Drawing.Point(147, 41);
            this.Update_Button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Update_Button.Name = "Update_Button";
            this.Update_Button.Size = new System.Drawing.Size(56, 20);
            this.Update_Button.TabIndex = 3;
            this.Update_Button.Text = "Update";
            this.Update_Button.UseVisualStyleBackColor = true;
            this.Update_Button.Click += new System.EventHandler(this.Update_Button_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FarbPanel
            // 
            this.FarbPanel.Location = new System.Drawing.Point(107, 42);
            this.FarbPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FarbPanel.Name = "FarbPanel";
            this.FarbPanel.Size = new System.Drawing.Size(16, 19);
            this.FarbPanel.TabIndex = 4;
            // 
            // FehlerButton
            // 
            this.FehlerButton.Location = new System.Drawing.Point(11, 11);
            this.FehlerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FehlerButton.Name = "FehlerButton";
            this.FehlerButton.Size = new System.Drawing.Size(189, 20);
            this.FehlerButton.TabIndex = 5;
            this.FehlerButton.Text = "Fehler Anzeigen";
            this.FehlerButton.UseVisualStyleBackColor = true;
            this.FehlerButton.Click += new System.EventHandler(this.FehlerButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 228);
            this.Controls.Add(this.FehlerButton);
            this.Controls.Add(this.FarbPanel);
            this.Controls.Add(this.Update_Button);
            this.Controls.Add(this.DataBox);
            this.Controls.Add(this.ListIPs);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "WatchDog-Server";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox ListIPs;
        private System.Windows.Forms.RichTextBox DataBox;
        private System.Windows.Forms.Button Update_Button;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel FarbPanel;
        private System.Windows.Forms.Button FehlerButton;
    }
}

