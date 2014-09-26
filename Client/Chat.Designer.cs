namespace Client
{
    partial class Chat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chat));
            this.btn_send = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_info = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitCont_main = new System.Windows.Forms.SplitContainer();
            this.splitCon_chat = new System.Windows.Forms.SplitContainer();
            this.btn_sendFile = new System.Windows.Forms.Button();
            this.picb_chatTo = new System.Windows.Forms.PictureBox();
            this.lb_selfIntr = new System.Windows.Forms.Label();
            this.btn_voice = new System.Windows.Forms.Button();
            this.lb_chatTo = new System.Windows.Forms.Label();
            this.btn_video = new System.Windows.Forms.Button();
            this.splitCon_text = new System.Windows.Forms.SplitContainer();
            this.rtb_content = new Client.ExRichTextBox();
            this.text_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示比例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_shark = new System.Windows.Forms.Button();
            this.btn_copyscreen = new System.Windows.Forms.Button();
            this.btn_faces = new System.Windows.Forms.Button();
            this.btn_font = new System.Windows.Forms.Button();
            this.tb_send = new Client.ExRichTextBox();
            this.splitCon_video = new System.Windows.Forms.SplitContainer();
            this.btn_vidStop = new System.Windows.Forms.Button();
            this.pic_vidSelf = new System.Windows.Forms.PictureBox();
            this.btn_vidAccept = new System.Windows.Forms.Button();
            this.btn_vidRefuse = new System.Windows.Forms.Button();
            this.pic_vidChat = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.splitCont_main.Panel1.SuspendLayout();
            this.splitCont_main.Panel2.SuspendLayout();
            this.splitCont_main.SuspendLayout();
            this.splitCon_chat.Panel1.SuspendLayout();
            this.splitCon_chat.Panel2.SuspendLayout();
            this.splitCon_chat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picb_chatTo)).BeginInit();
            this.splitCon_text.Panel1.SuspendLayout();
            this.splitCon_text.Panel2.SuspendLayout();
            this.splitCon_text.SuspendLayout();
            this.text_menu.SuspendLayout();
            this.splitCon_video.Panel1.SuspendLayout();
            this.splitCon_video.Panel2.SuspendLayout();
            this.splitCon_video.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_vidSelf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_vidChat)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.Location = new System.Drawing.Point(431, 80);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(80, 23);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_info});
            this.statusStrip1.Location = new System.Drawing.Point(0, 417);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(513, 26);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_info
            // 
            this.tssl_info.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
            this.tssl_info.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.tssl_info.Name = "tssl_info";
            this.tssl_info.Size = new System.Drawing.Size(192, 21);
            this.tssl_info.Text = "您的聊天数据经过加密与签名验证";
            // 
            // splitCont_main
            // 
            this.splitCont_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitCont_main.BackColor = System.Drawing.Color.Transparent;
            this.splitCont_main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitCont_main.Location = new System.Drawing.Point(0, 1);
            this.splitCont_main.Name = "splitCont_main";
            // 
            // splitCont_main.Panel1
            // 
            this.splitCont_main.Panel1.Controls.Add(this.splitCon_chat);
            this.splitCont_main.Panel1MinSize = 10;
            // 
            // splitCont_main.Panel2
            // 
            this.splitCont_main.Panel2.Controls.Add(this.splitCon_video);
            this.splitCont_main.Panel2Collapsed = true;
            this.splitCont_main.Panel2MinSize = 10;
            this.splitCont_main.Size = new System.Drawing.Size(513, 419);
            this.splitCont_main.SplitterDistance = this.splitCont_main.Panel1.Width;
            this.splitCont_main.TabIndex = 11;
            // 
            // splitCon_chat
            // 
            this.splitCon_chat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon_chat.IsSplitterFixed = true;
            this.splitCon_chat.Location = new System.Drawing.Point(0, 0);
            this.splitCon_chat.Name = "splitCon_chat";
            this.splitCon_chat.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCon_chat.Panel1
            // 
            this.splitCon_chat.Panel1.Controls.Add(this.btn_sendFile);
            this.splitCon_chat.Panel1.Controls.Add(this.picb_chatTo);
            this.splitCon_chat.Panel1.Controls.Add(this.lb_selfIntr);
            this.splitCon_chat.Panel1.Controls.Add(this.btn_voice);
            this.splitCon_chat.Panel1.Controls.Add(this.lb_chatTo);
            this.splitCon_chat.Panel1.Controls.Add(this.btn_video);
            // 
            // splitCon_chat.Panel2
            // 
            this.splitCon_chat.Panel2.Controls.Add(this.splitCon_text);
            this.splitCon_chat.Size = new System.Drawing.Size(509, 415);
            this.splitCon_chat.SplitterDistance = 89;
            this.splitCon_chat.TabIndex = 11;
            // 
            // btn_sendFile
            // 
            this.btn_sendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_sendFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_sendFile.BackgroundImage")));
            this.btn_sendFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_sendFile.Location = new System.Drawing.Point(345, 12);
            this.btn_sendFile.Name = "btn_sendFile";
            this.btn_sendFile.Size = new System.Drawing.Size(36, 38);
            this.btn_sendFile.TabIndex = 9;
            this.btn_sendFile.UseVisualStyleBackColor = true;
            this.btn_sendFile.Click += new System.EventHandler(this.btn_sendFile_Click);
            // 
            // picb_chatTo
            // 
            this.picb_chatTo.Location = new System.Drawing.Point(19, 22);
            this.picb_chatTo.Name = "picb_chatTo";
            this.picb_chatTo.Size = new System.Drawing.Size(57, 50);
            this.picb_chatTo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picb_chatTo.TabIndex = 7;
            this.picb_chatTo.TabStop = false;
            // 
            // lb_selfIntr
            // 
            this.lb_selfIntr.AutoSize = true;
            this.lb_selfIntr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_selfIntr.Location = new System.Drawing.Point(82, 54);
            this.lb_selfIntr.Name = "lb_selfIntr";
            this.lb_selfIntr.Size = new System.Drawing.Size(161, 12);
            this.lb_selfIntr.TabIndex = 10;
            this.lb_selfIntr.Text = "个性签名。。。我很个性！";
            // 
            // btn_voice
            // 
            this.btn_voice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_voice.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_voice.BackgroundImage")));
            this.btn_voice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_voice.Location = new System.Drawing.Point(402, 12);
            this.btn_voice.Name = "btn_voice";
            this.btn_voice.Size = new System.Drawing.Size(38, 38);
            this.btn_voice.TabIndex = 10;
            this.btn_voice.UseVisualStyleBackColor = true;
            this.btn_voice.Click += new System.EventHandler(this.btn_voice_Click);
            // 
            // lb_chatTo
            // 
            this.lb_chatTo.AutoSize = true;
            this.lb_chatTo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_chatTo.Location = new System.Drawing.Point(82, 31);
            this.lb_chatTo.Name = "lb_chatTo";
            this.lb_chatTo.Size = new System.Drawing.Size(47, 12);
            this.lb_chatTo.TabIndex = 8;
            this.lb_chatTo.Text = "label1";
            // 
            // btn_video
            // 
            this.btn_video.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_video.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_video.BackgroundImage")));
            this.btn_video.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_video.Location = new System.Drawing.Point(461, 12);
            this.btn_video.Name = "btn_video";
            this.btn_video.Size = new System.Drawing.Size(36, 38);
            this.btn_video.TabIndex = 9;
            this.btn_video.UseVisualStyleBackColor = true;
            this.btn_video.Click += new System.EventHandler(this.btn_video_Click);
            // 
            // splitCon_text
            // 
            this.splitCon_text.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitCon_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon_text.Location = new System.Drawing.Point(0, 0);
            this.splitCon_text.Name = "splitCon_text";
            this.splitCon_text.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCon_text.Panel1
            // 
            this.splitCon_text.Panel1.Controls.Add(this.rtb_content);
            // 
            // splitCon_text.Panel2
            // 
            this.splitCon_text.Panel2.Controls.Add(this.btn_shark);
            this.splitCon_text.Panel2.Controls.Add(this.btn_copyscreen);
            this.splitCon_text.Panel2.Controls.Add(this.btn_faces);
            this.splitCon_text.Panel2.Controls.Add(this.btn_font);
            this.splitCon_text.Panel2.Controls.Add(this.tb_send);
            this.splitCon_text.Panel2.Controls.Add(this.btn_send);
            this.splitCon_text.Size = new System.Drawing.Size(509, 322);
            this.splitCon_text.SplitterDistance = 210;
            this.splitCon_text.SplitterWidth = 2;
            this.splitCon_text.TabIndex = 0;
            // 
            // rtb_content
            // 
            this.rtb_content.BackColor = System.Drawing.SystemColors.Window;
            this.rtb_content.ContextMenuStrip = this.text_menu;
            this.rtb_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_content.HiglightColor = Client.RtfColor.White;
            this.rtb_content.Location = new System.Drawing.Point(0, 0);
            this.rtb_content.Name = "rtb_content";
            this.rtb_content.ReadOnly = true;
            this.rtb_content.Size = new System.Drawing.Size(505, 206);
            this.rtb_content.TabIndex = 2;
            this.rtb_content.Text = "";
            this.rtb_content.TextColor = Client.RtfColor.Black;
            // 
            // text_menu
            // 
            this.text_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.显示比例ToolStripMenuItem,
            this.清空ToolStripMenuItem});
            this.text_menu.Name = "text_menu";
            this.text_menu.Size = new System.Drawing.Size(149, 92);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 显示比例ToolStripMenuItem
            // 
            this.显示比例ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.显示比例ToolStripMenuItem.Name = "显示比例ToolStripMenuItem";
            this.显示比例ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示比例ToolStripMenuItem.Text = "显示比例";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem2.Text = "50%";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem3.Text = "100%";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem4.Text = "150%";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem5.Text = "200%";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.清空ToolStripMenuItem.Text = "清空聊天记录";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // btn_shark
            // 
            this.btn_shark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_shark.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_shark.BackgroundImage")));
            this.btn_shark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_shark.Location = new System.Drawing.Point(112, 80);
            this.btn_shark.Name = "btn_shark";
            this.btn_shark.Size = new System.Drawing.Size(26, 23);
            this.btn_shark.TabIndex = 12;
            this.btn_shark.UseVisualStyleBackColor = true;
            this.btn_shark.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_shark_MouseDown);
            // 
            // btn_copyscreen
            // 
            this.btn_copyscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_copyscreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_copyscreen.BackgroundImage")));
            this.btn_copyscreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_copyscreen.Location = new System.Drawing.Point(80, 80);
            this.btn_copyscreen.Name = "btn_copyscreen";
            this.btn_copyscreen.Size = new System.Drawing.Size(26, 23);
            this.btn_copyscreen.TabIndex = 12;
            this.btn_copyscreen.UseVisualStyleBackColor = true;
            this.btn_copyscreen.Click += new System.EventHandler(this.btn_copyscreen_Click);
            // 
            // btn_faces
            // 
            this.btn_faces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_faces.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_faces.BackgroundImage")));
            this.btn_faces.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_faces.Location = new System.Drawing.Point(46, 80);
            this.btn_faces.Name = "btn_faces";
            this.btn_faces.Size = new System.Drawing.Size(28, 23);
            this.btn_faces.TabIndex = 12;
            this.btn_faces.UseVisualStyleBackColor = true;
            // 
            // btn_font
            // 
            this.btn_font.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_font.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_font.BackgroundImage")));
            this.btn_font.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_font.Location = new System.Drawing.Point(17, 80);
            this.btn_font.Name = "btn_font";
            this.btn_font.Size = new System.Drawing.Size(26, 23);
            this.btn_font.TabIndex = 4;
            this.btn_font.UseVisualStyleBackColor = true;
            this.btn_font.Click += new System.EventHandler(this.btn_font_Click);
            // 
            // tb_send
            // 
            this.tb_send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_send.BackColor = System.Drawing.SystemColors.Window;
            this.tb_send.ContextMenuStrip = this.text_menu;
            this.tb_send.HiglightColor = Client.RtfColor.White;
            this.tb_send.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tb_send.Location = new System.Drawing.Point(0, 0);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(519, 74);
            this.tb_send.TabIndex = 1;
            this.tb_send.Text = "";
            this.tb_send.TextColor = Client.RtfColor.Black;
            this.tb_send.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_send_KeyDown);
            this.tb_send.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_send_KeyUp);
            // 
            // splitCon_video
            // 
            this.splitCon_video.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCon_video.IsSplitterFixed = true;
            this.splitCon_video.Location = new System.Drawing.Point(0, 0);
            this.splitCon_video.Name = "splitCon_video";
            this.splitCon_video.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCon_video.Panel1
            // 
            this.splitCon_video.Panel1.Controls.Add(this.btn_vidStop);
            this.splitCon_video.Panel1.Controls.Add(this.pic_vidSelf);
            this.splitCon_video.Panel1.Controls.Add(this.btn_vidAccept);
            this.splitCon_video.Panel1.Controls.Add(this.btn_vidRefuse);
            // 
            // splitCon_video.Panel2
            // 
            this.splitCon_video.Panel2.Controls.Add(this.pic_vidChat);
            this.splitCon_video.Size = new System.Drawing.Size(92, 96);
            this.splitCon_video.SplitterDistance = 25;
            this.splitCon_video.TabIndex = 0;
            // 
            // btn_vidStop
            // 
            this.btn_vidStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_vidStop.BackgroundImage")));
            this.btn_vidStop.Location = new System.Drawing.Point(13, 28);
            this.btn_vidStop.Name = "btn_vidStop";
            this.btn_vidStop.Size = new System.Drawing.Size(64, 22);
            this.btn_vidStop.TabIndex = 9;
            this.btn_vidStop.UseVisualStyleBackColor = true;
            this.btn_vidStop.Click += new System.EventHandler(this.btn_vidStop_Click);
            // 
            // pic_vidSelf
            // 
            this.pic_vidSelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_vidSelf.Location = new System.Drawing.Point(-67, 3);
            this.pic_vidSelf.Name = "pic_vidSelf";
            this.pic_vidSelf.Size = new System.Drawing.Size(149, 102);
            this.pic_vidSelf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_vidSelf.TabIndex = 12;
            this.pic_vidSelf.TabStop = false;
            // 
            // btn_vidAccept
            // 
            this.btn_vidAccept.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_vidAccept.BackgroundImage")));
            this.btn_vidAccept.Location = new System.Drawing.Point(83, 28);
            this.btn_vidAccept.Name = "btn_vidAccept";
            this.btn_vidAccept.Size = new System.Drawing.Size(64, 22);
            this.btn_vidAccept.TabIndex = 9;
            this.btn_vidAccept.UseVisualStyleBackColor = true;
            this.btn_vidAccept.Click += new System.EventHandler(this.btn_vidAccept_Click);
            // 
            // btn_vidRefuse
            // 
            this.btn_vidRefuse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_vidRefuse.BackgroundImage")));
            this.btn_vidRefuse.Location = new System.Drawing.Point(153, 28);
            this.btn_vidRefuse.Name = "btn_vidRefuse";
            this.btn_vidRefuse.Size = new System.Drawing.Size(64, 22);
            this.btn_vidRefuse.TabIndex = 9;
            this.btn_vidRefuse.UseVisualStyleBackColor = true;
            this.btn_vidRefuse.Click += new System.EventHandler(this.btn_vidRefuse_Click);
            // 
            // pic_vidChat
            // 
            this.pic_vidChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_vidChat.Location = new System.Drawing.Point(0, 0);
            this.pic_vidChat.Name = "pic_vidChat";
            this.pic_vidChat.Size = new System.Drawing.Size(92, 67);
            this.pic_vidChat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_vidChat.TabIndex = 11;
            this.pic_vidChat.TabStop = false;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(513, 443);
            this.Controls.Add(this.splitCont_main);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(270, 330);
            this.Name = "Chat";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chat_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitCont_main.Panel1.ResumeLayout(false);
            this.splitCont_main.Panel2.ResumeLayout(false);
            this.splitCont_main.ResumeLayout(false);
            this.splitCon_chat.Panel1.ResumeLayout(false);
            this.splitCon_chat.Panel1.PerformLayout();
            this.splitCon_chat.Panel2.ResumeLayout(false);
            this.splitCon_chat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picb_chatTo)).EndInit();
            this.splitCon_text.Panel1.ResumeLayout(false);
            this.splitCon_text.Panel2.ResumeLayout(false);
            this.splitCon_text.ResumeLayout(false);
            this.text_menu.ResumeLayout(false);
            this.splitCon_video.Panel1.ResumeLayout(false);
            this.splitCon_video.Panel2.ResumeLayout(false);
            this.splitCon_video.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_vidSelf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_vidChat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel tssl_info;
        internal System.Windows.Forms.PictureBox pic_vidChat;
        internal System.Windows.Forms.PictureBox pic_vidSelf;
        private System.Windows.Forms.Button btn_vidRefuse;
        private System.Windows.Forms.Button btn_vidAccept;
        private System.Windows.Forms.Button btn_vidStop;
        private System.Windows.Forms.SplitContainer splitCont_main;
        private System.Windows.Forms.PictureBox picb_chatTo;
        private System.Windows.Forms.Button btn_voice;
        private System.Windows.Forms.Label lb_selfIntr;
        private System.Windows.Forms.Button btn_sendFile;
        private System.Windows.Forms.Label lb_chatTo;
        private System.Windows.Forms.Button btn_video;
        private System.Windows.Forms.SplitContainer splitCon_chat;
        private System.Windows.Forms.SplitContainer splitCon_text;
        private System.Windows.Forms.SplitContainer splitCon_video;
        private ExRichTextBox rtb_content;
        private ExRichTextBox tb_send;
        private System.Windows.Forms.Button btn_font;
        private System.Windows.Forms.Button btn_faces;
        private System.Windows.Forms.Button btn_copyscreen;
        private System.Windows.Forms.ContextMenuStrip text_menu;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示比例ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.Button btn_shark;
    }
}