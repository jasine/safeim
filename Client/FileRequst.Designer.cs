namespace Client
{
    partial class FileRequst
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileRequst));
            this.lb_info = new System.Windows.Forms.Label();
            this.picb_head = new System.Windows.Forms.PictureBox();
            this.pross_precent = new System.Windows.Forms.ProgressBar();
            this.lb_percent = new System.Windows.Forms.Label();
            this.lkb_accept = new System.Windows.Forms.LinkLabel();
            this.lkb_refuse = new System.Windows.Forms.LinkLabel();
            this.lkb_saveAs = new System.Windows.Forms.LinkLabel();
            this.lb_file = new System.Windows.Forms.Label();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.lb_current = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picb_head)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_info
            // 
            this.lb_info.AutoSize = true;
            this.lb_info.Location = new System.Drawing.Point(84, 12);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(149, 12);
            this.lb_info.TabIndex = 0;
            this.lb_info.Text = "。。。向你发送文件。。。";
            // 
            // picb_head
            // 
            this.picb_head.Location = new System.Drawing.Point(12, 12);
            this.picb_head.Name = "picb_head";
            this.picb_head.Size = new System.Drawing.Size(66, 61);
            this.picb_head.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picb_head.TabIndex = 1;
            this.picb_head.TabStop = false;
            // 
            // pross_precent
            // 
            this.pross_precent.Location = new System.Drawing.Point(84, 48);
            this.pross_precent.Name = "pross_precent";
            this.pross_precent.Size = new System.Drawing.Size(160, 12);
            this.pross_precent.TabIndex = 2;
            // 
            // lb_percent
            // 
            this.lb_percent.AutoSize = true;
            this.lb_percent.Location = new System.Drawing.Point(250, 48);
            this.lb_percent.Name = "lb_percent";
            this.lb_percent.Size = new System.Drawing.Size(11, 12);
            this.lb_percent.TabIndex = 3;
            this.lb_percent.Text = "%";
            // 
            // lkb_accept
            // 
            this.lkb_accept.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lkb_accept.AutoSize = true;
            this.lkb_accept.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lkb_accept.Location = new System.Drawing.Point(190, 80);
            this.lkb_accept.Name = "lkb_accept";
            this.lkb_accept.Size = new System.Drawing.Size(29, 12);
            this.lkb_accept.TabIndex = 4;
            this.lkb_accept.TabStop = true;
            this.lkb_accept.Text = "接受";
            this.lkb_accept.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkb_accept_LinkClicked);
            // 
            // lkb_refuse
            // 
            this.lkb_refuse.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lkb_refuse.AutoSize = true;
            this.lkb_refuse.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lkb_refuse.Location = new System.Drawing.Point(155, 80);
            this.lkb_refuse.Name = "lkb_refuse";
            this.lkb_refuse.Size = new System.Drawing.Size(29, 12);
            this.lkb_refuse.TabIndex = 5;
            this.lkb_refuse.TabStop = true;
            this.lkb_refuse.Text = "拒绝";
            this.lkb_refuse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkb_refuse_LinkClicked);
            // 
            // lkb_saveAs
            // 
            this.lkb_saveAs.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lkb_saveAs.AutoSize = true;
            this.lkb_saveAs.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lkb_saveAs.Location = new System.Drawing.Point(225, 80);
            this.lkb_saveAs.Name = "lkb_saveAs";
            this.lkb_saveAs.Size = new System.Drawing.Size(41, 12);
            this.lkb_saveAs.TabIndex = 6;
            this.lkb_saveAs.TabStop = true;
            this.lkb_saveAs.Text = "另存为";
            this.lkb_saveAs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkb_saveAs_LinkClicked);
            // 
            // lb_file
            // 
            this.lb_file.AutoSize = true;
            this.lb_file.Location = new System.Drawing.Point(84, 24);
            this.lb_file.Name = "lb_file";
            this.lb_file.Size = new System.Drawing.Size(23, 12);
            this.lb_file.TabIndex = 7;
            this.lb_file.Text = "...";
            // 
            // sfd
            // 
            this.sfd.Title = "另存为";
            // 
            // lb_current
            // 
            this.lb_current.AutoSize = true;
            this.lb_current.Location = new System.Drawing.Point(84, 63);
            this.lb_current.Name = "lb_current";
            this.lb_current.Size = new System.Drawing.Size(53, 12);
            this.lb_current.TabIndex = 8;
            this.lb_current.Text = "开始发送";
            // 
            // FileRequst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.lb_current);
            this.Controls.Add(this.lb_file);
            this.Controls.Add(this.lkb_saveAs);
            this.Controls.Add(this.lkb_refuse);
            this.Controls.Add(this.lkb_accept);
            this.Controls.Add(this.lb_percent);
            this.Controls.Add(this.pross_precent);
            this.Controls.Add(this.picb_head);
            this.Controls.Add(this.lb_info);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 140);
            this.MinimumSize = new System.Drawing.Size(300, 140);
            this.Name = "FileRequst";
            this.ShowInTaskbar = false;
            this.Text = "发送文件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileRequst_FormClosing);
            this.Resize += new System.EventHandler(this.FileRequst_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picb_head)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_info;
        private System.Windows.Forms.PictureBox picb_head;
        private System.Windows.Forms.ProgressBar pross_precent;
        private System.Windows.Forms.Label lb_percent;
        private System.Windows.Forms.LinkLabel lkb_accept;
        private System.Windows.Forms.LinkLabel lkb_refuse;
        private System.Windows.Forms.LinkLabel lkb_saveAs;
        private System.Windows.Forms.Label lb_file;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.Label lb_current;
    }
}