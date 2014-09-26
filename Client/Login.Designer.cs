namespace Client
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.tb_userName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_passwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.tb_serverIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lkb_regest = new System.Windows.Forms.LinkLabel();
            this.lkb_setting = new System.Windows.Forms.LinkLabel();
            this.pal_mask = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pal_mask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_userName
            // 
            this.tb_userName.Location = new System.Drawing.Point(99, 32);
            this.tb_userName.Name = "tb_userName";
            this.tb_userName.Size = new System.Drawing.Size(147, 21);
            this.tb_userName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名";
            // 
            // tb_passwd
            // 
            this.tb_passwd.Location = new System.Drawing.Point(99, 70);
            this.tb_passwd.Name = "tb_passwd";
            this.tb_passwd.PasswordChar = '*';
            this.tb_passwd.Size = new System.Drawing.Size(147, 21);
            this.tb_passwd.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密  码";
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(110, 104);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 23);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "登录";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // tb_serverIp
            // 
            this.tb_serverIp.Location = new System.Drawing.Point(129, 176);
            this.tb_serverIp.Name = "tb_serverIp";
            this.tb_serverIp.Size = new System.Drawing.Size(100, 21);
            this.tb_serverIp.TabIndex = 5;
            this.tb_serverIp.Text = "192.168.0.181";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "服务器IP";
            // 
            // lkb_regest
            // 
            this.lkb_regest.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lkb_regest.AutoSize = true;
            this.lkb_regest.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lkb_regest.Location = new System.Drawing.Point(214, 152);
            this.lkb_regest.Name = "lkb_regest";
            this.lkb_regest.Size = new System.Drawing.Size(29, 12);
            this.lkb_regest.TabIndex = 7;
            this.lkb_regest.TabStop = true;
            this.lkb_regest.Text = "注册";
            this.lkb_regest.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lkb_regest.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkb_regest_LinkClicked);
            // 
            // lkb_setting
            // 
            this.lkb_setting.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lkb_setting.AutoSize = true;
            this.lkb_setting.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lkb_setting.Location = new System.Drawing.Point(249, 152);
            this.lkb_setting.Name = "lkb_setting";
            this.lkb_setting.Size = new System.Drawing.Size(29, 12);
            this.lkb_setting.TabIndex = 7;
            this.lkb_setting.TabStop = true;
            this.lkb_setting.Text = "设置";
            this.lkb_setting.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // pal_mask
            // 
            this.pal_mask.BackColor = System.Drawing.Color.Transparent;
            this.pal_mask.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pal_mask.BackgroundImage")));
            this.pal_mask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pal_mask.Controls.Add(this.pictureBox1);
            this.pal_mask.Location = new System.Drawing.Point(-8, -2);
            this.pal_mask.Name = "pal_mask";
            this.pal_mask.Size = new System.Drawing.Size(305, 207);
            this.pal_mask.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(118, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(296, 201);
            this.Controls.Add(this.pal_mask);
            this.Controls.Add(this.lkb_setting);
            this.Controls.Add(this.lkb_regest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_serverIp);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_passwd);
            this.Controls.Add(this.tb_userName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.pal_mask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_userName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_passwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox tb_serverIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lkb_regest;
        private System.Windows.Forms.LinkLabel lkb_setting;
        private System.Windows.Forms.Panel pal_mask;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}