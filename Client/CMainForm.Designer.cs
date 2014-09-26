namespace Client
{
    partial class CMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMainForm));
            this.picb_selfHead = new System.Windows.Forms.PictureBox();
            this.lb_userName = new System.Windows.Forms.Label();
            this.ntf_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lb_selfintr = new System.Windows.Forms.Label();
            this.lkb_about = new System.Windows.Forms.LinkLabel();
            this.qqLstb_friend = new Client.QQListBox();
            ((System.ComponentModel.ISupportInitialize)(this.picb_selfHead)).BeginInit();
            this.SuspendLayout();
            // 
            // picb_selfHead
            // 
            this.picb_selfHead.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picb_selfHead.Location = new System.Drawing.Point(24, 19);
            this.picb_selfHead.Name = "picb_selfHead";
            this.picb_selfHead.Size = new System.Drawing.Size(62, 61);
            this.picb_selfHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picb_selfHead.TabIndex = 0;
            this.picb_selfHead.TabStop = false;
            // 
            // lb_userName
            // 
            this.lb_userName.AutoSize = true;
            this.lb_userName.Location = new System.Drawing.Point(92, 23);
            this.lb_userName.Name = "lb_userName";
            this.lb_userName.Size = new System.Drawing.Size(41, 12);
            this.lb_userName.TabIndex = 1;
            this.lb_userName.Text = "label1";
            // 
            // ntf_icon
            // 
            this.ntf_icon.Text = "SafeIM";
            this.ntf_icon.Visible = true;
            // 
            // lb_selfintr
            // 
            this.lb_selfintr.AutoSize = true;
            this.lb_selfintr.Location = new System.Drawing.Point(92, 49);
            this.lb_selfintr.Name = "lb_selfintr";
            this.lb_selfintr.Size = new System.Drawing.Size(71, 12);
            this.lb_selfintr.TabIndex = 7;
            this.lb_selfintr.Text = "lb_selfintr";
            // 
            // lkb_about
            // 
            this.lkb_about.ActiveLinkColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lkb_about.AutoSize = true;
            this.lkb_about.BackColor = System.Drawing.Color.Transparent;
            this.lkb_about.LinkColor = System.Drawing.Color.Black;
            this.lkb_about.Location = new System.Drawing.Point(200, 406);
            this.lkb_about.Name = "lkb_about";
            this.lkb_about.Size = new System.Drawing.Size(29, 12);
            this.lkb_about.TabIndex = 9;
            this.lkb_about.TabStop = true;
            this.lkb_about.Text = "关于";
            this.lkb_about.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkb_about_LinkClicked);
            // 
            // qqLstb_friend
            // 
            this.qqLstb_friend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qqLstb_friend.BackColor = System.Drawing.Color.Transparent;
            this.qqLstb_friend.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.qqLstb_friend.FormattingEnabled = true;
            this.qqLstb_friend.Location = new System.Drawing.Point(12, 99);
            this.qqLstb_friend.Name = "qqLstb_friend";
            this.qqLstb_friend.Size = new System.Drawing.Size(197, 304);
            this.qqLstb_friend.TabIndex = 8;
            this.qqLstb_friend.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.qqLstb_friend_MouseDoubleClick);
            // 
            // CMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(229, 418);
            this.Controls.Add(this.lkb_about);
            this.Controls.Add(this.qqLstb_friend);
            this.Controls.Add(this.lb_selfintr);
            this.Controls.Add(this.lb_userName);
            this.Controls.Add(this.picb_selfHead);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CMainForm";
            this.Opacity = 0.9D;
            this.Text = "SafeIM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CMainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picb_selfHead)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picb_selfHead;
        private System.Windows.Forms.Label lb_userName;
        private System.Windows.Forms.NotifyIcon ntf_icon;
        private System.Windows.Forms.Label lb_selfintr;
        private QQListBox qqLstb_friend;
        private System.Windows.Forms.LinkLabel lkb_about;
    }
}

