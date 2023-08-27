using System.Drawing;
using System.Windows.Forms;

namespace SteamConfigManager
{
    partial class PopUp
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
            this.button1 = new System.Windows.Forms.Button();
            this.PopupTimer = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.messageLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Tamam";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(200, 10);
            this.progressBar1.TabIndex = 3;
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.SystemColors.Control;
            this.messageLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageLabel.Enabled = false;
            this.messageLabel.Location = new System.Drawing.Point(12, 16);
            this.messageLabel.Multiline = true;
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.ReadOnly = true;
            this.messageLabel.Size = new System.Drawing.Size(176, 43);
            this.messageLabel.TabIndex = 4;
            this.messageLabel.Text = "Hata Mesajı";
            // 
            // ss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(200, 100);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ss";
            this.Opacity = 0.6D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PopupMessage";
            this.Load += new System.EventHandler(this.PopupMessage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer PopupTimer;
        private ProgressBar progressBar1;
        private TextBox messageLabel;
    }
}