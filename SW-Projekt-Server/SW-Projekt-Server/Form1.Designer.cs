﻿namespace SW_Projekt_Server
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
            this.button1 = new System.Windows.Forms.Button();
            this.ListIPs = new System.Windows.Forms.ComboBox();
            this.DataBox = new System.Windows.Forms.RichTextBox();
            this.Update_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ListIPs
            // 
            this.ListIPs.FormattingEnabled = true;
            this.ListIPs.Location = new System.Drawing.Point(15, 52);
            this.ListIPs.Name = "ListIPs";
            this.ListIPs.Size = new System.Drawing.Size(121, 24);
            this.ListIPs.TabIndex = 1;
            this.ListIPs.SelectedIndexChanged += new System.EventHandler(this.ListIPs_SelectedIndexChanged);
            // 
            // DataBox
            // 
            this.DataBox.Location = new System.Drawing.Point(15, 98);
            this.DataBox.Name = "DataBox";
            this.DataBox.ReadOnly = true;
            this.DataBox.Size = new System.Drawing.Size(121, 73);
            this.DataBox.TabIndex = 2;
            this.DataBox.Text = "";
            // 
            // Update_Button
            // 
            this.Update_Button.Location = new System.Drawing.Point(163, 52);
            this.Update_Button.Name = "Update_Button";
            this.Update_Button.Size = new System.Drawing.Size(75, 23);
            this.Update_Button.TabIndex = 3;
            this.Update_Button.Text = "Update";
            this.Update_Button.UseVisualStyleBackColor = true;
            this.Update_Button.Click += new System.EventHandler(this.Update_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 280);
            this.Controls.Add(this.Update_Button);
            this.Controls.Add(this.DataBox);
            this.Controls.Add(this.ListIPs);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "WatchDog-Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox ListIPs;
        private System.Windows.Forms.RichTextBox DataBox;
        private System.Windows.Forms.Button Update_Button;
    }
}

