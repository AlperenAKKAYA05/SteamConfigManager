namespace SteamConfigManager
{
    partial class SteamInfo
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
            this.realname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.steamid = new System.Windows.Forms.TextBox();
            this.personaname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.avatarfull = new System.Windows.Forms.PictureBox();
            this.steamuserid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.userdata = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.avatarfull)).BeginInit();
            this.SuspendLayout();
            // 
            // realname
            // 
            this.realname.Location = new System.Drawing.Point(101, 67);
            this.realname.Name = "realname";
            this.realname.Size = new System.Drawing.Size(246, 23);
            this.realname.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Steam Kimliği:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Takma Ad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Gerçek Ad:";
            // 
            // steamid
            // 
            this.steamid.Location = new System.Drawing.Point(101, 38);
            this.steamid.Name = "steamid";
            this.steamid.Size = new System.Drawing.Size(246, 23);
            this.steamid.TabIndex = 20;
            // 
            // personaname
            // 
            this.personaname.Location = new System.Drawing.Point(101, 96);
            this.personaname.Name = "personaname";
            this.personaname.Size = new System.Drawing.Size(246, 23);
            this.personaname.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 22;
            this.label4.Text = "Profil Resmi:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 337);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 15);
            this.lblStatus.TabIndex = 24;
            this.lblStatus.Text = "lblStatus";
            this.lblStatus.Visible = false;
            // 
            // avatarfull
            // 
            this.avatarfull.Location = new System.Drawing.Point(101, 135);
            this.avatarfull.Name = "avatarfull";
            this.avatarfull.Size = new System.Drawing.Size(184, 184);
            this.avatarfull.TabIndex = 25;
            this.avatarfull.TabStop = false;
            // 
            // steamuserid
            // 
            this.steamuserid.Location = new System.Drawing.Point(101, 6);
            this.steamuserid.Name = "steamuserid";
            this.steamuserid.Size = new System.Drawing.Size(246, 23);
            this.steamuserid.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Kullanıcı ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "Kullanıcı Verileri:";
            // 
            // userdata
            // 
            this.userdata.AutoSize = true;
            this.userdata.Location = new System.Drawing.Point(12, 337);
            this.userdata.Name = "userdata";
            this.userdata.Size = new System.Drawing.Size(66, 15);
            this.userdata.TabIndex = 32;
            this.userdata.Text = "User Data...";
            this.userdata.Visible = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(318, 240);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 33;
            // 
            // SteamInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(359, 361);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.steamuserid);
            this.Controls.Add(this.avatarfull);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.personaname);
            this.Controls.Add(this.steamid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.realname);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.userdata);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.Gray;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(375, 400);
            this.MinimumSize = new System.Drawing.Size(375, 400);
            this.Name = "SteamInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steam Community :: Terms of Service";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.avatarfull)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox realname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox steamid;
        private System.Windows.Forms.TextBox personaname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox avatarfull;
        private System.Windows.Forms.TextBox steamuserid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label userdata;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

