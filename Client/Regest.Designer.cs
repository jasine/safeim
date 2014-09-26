namespace Client
{
    partial class Regest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Regest));
            this.picb_head = new System.Windows.Forms.PictureBox();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lst_heads = new System.Windows.Forms.ListView();
            this.imgl_heads = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_passwdChack = new System.Windows.Forms.Label();
            this.lb_userChack = new System.Windows.Forms.Label();
            this.btn_regest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_password2 = new System.Windows.Forms.TextBox();
            this.tb_intro = new System.Windows.Forms.TextBox();
            this.tb_password1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picb_head)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picb_head
            // 
            this.picb_head.Location = new System.Drawing.Point(18, 39);
            this.picb_head.Name = "picb_head";
            this.picb_head.Size = new System.Drawing.Size(100, 97);
            this.picb_head.TabIndex = 0;
            this.picb_head.TabStop = false;
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(53, 14);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(100, 21);
            this.tb_username.TabIndex = 1;
            this.tb_username.Enter += new System.EventHandler(this.tb_username_Enter);
            this.tb_username.Leave += new System.EventHandler(this.tb_username_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lst_heads);
            this.groupBox1.Controls.Add(this.picb_head);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 165);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "头像";
            // 
            // lst_heads
            // 
            this.lst_heads.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lst_heads.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lst_heads.ForeColor = System.Drawing.Color.Transparent;
            this.lst_heads.HideSelection = false;
            this.lst_heads.LargeImageList = this.imgl_heads;
            this.lst_heads.Location = new System.Drawing.Point(138, 11);
            this.lst_heads.MultiSelect = false;
            this.lst_heads.Name = "lst_heads";
            this.lst_heads.ShowGroups = false;
            this.lst_heads.Size = new System.Drawing.Size(440, 148);
            this.lst_heads.SmallImageList = this.imgl_heads;
            this.lst_heads.TabIndex = 1;
            this.lst_heads.UseCompatibleStateImageBehavior = false;
            this.lst_heads.SelectedIndexChanged += new System.EventHandler(this.lst_heads_SelectedIndexChanged);
            // 
            // imgl_heads
            // 
            this.imgl_heads.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgl_heads.ImageSize = new System.Drawing.Size(40, 40);
            this.imgl_heads.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lb_passwdChack);
            this.groupBox2.Controls.Add(this.lb_userChack);
            this.groupBox2.Controls.Add(this.btn_regest);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tb_password2);
            this.groupBox2.Controls.Add(this.tb_intro);
            this.groupBox2.Controls.Add(this.tb_password1);
            this.groupBox2.Controls.Add(this.tb_username);
            this.groupBox2.Location = new System.Drawing.Point(59, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(495, 104);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息";
            // 
            // lb_passwdChack
            // 
            this.lb_passwdChack.AutoSize = true;
            this.lb_passwdChack.Location = new System.Drawing.Point(367, 44);
            this.lb_passwdChack.Name = "lb_passwdChack";
            this.lb_passwdChack.Size = new System.Drawing.Size(11, 12);
            this.lb_passwdChack.TabIndex = 7;
            this.lb_passwdChack.Text = " ";
            // 
            // lb_userChack
            // 
            this.lb_userChack.AutoSize = true;
            this.lb_userChack.Location = new System.Drawing.Point(159, 17);
            this.lb_userChack.Name = "lb_userChack";
            this.lb_userChack.Size = new System.Drawing.Size(11, 12);
            this.lb_userChack.TabIndex = 7;
            this.lb_userChack.Text = " ";
            // 
            // btn_regest
            // 
            this.btn_regest.Location = new System.Drawing.Point(384, 68);
            this.btn_regest.Name = "btn_regest";
            this.btn_regest.Size = new System.Drawing.Size(75, 21);
            this.btn_regest.TabIndex = 4;
            this.btn_regest.Text = "注册";
            this.btn_regest.UseVisualStyleBackColor = true;
            this.btn_regest.Click += new System.EventHandler(this.btn_regest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "*再次输入密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "个性签名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "*密  码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "*用户名";
            // 
            // tb_password2
            // 
            this.tb_password2.Location = new System.Drawing.Point(261, 41);
            this.tb_password2.Name = "tb_password2";
            this.tb_password2.PasswordChar = '*';
            this.tb_password2.Size = new System.Drawing.Size(100, 21);
            this.tb_password2.TabIndex = 1;
            this.tb_password2.Enter += new System.EventHandler(this.tb_password2_Enter);
            this.tb_password2.Leave += new System.EventHandler(this.tb_password2_Leave);
            // 
            // tb_intro
            // 
            this.tb_intro.Location = new System.Drawing.Point(65, 68);
            this.tb_intro.Name = "tb_intro";
            this.tb_intro.Size = new System.Drawing.Size(296, 21);
            this.tb_intro.TabIndex = 1;
            // 
            // tb_password1
            // 
            this.tb_password1.Location = new System.Drawing.Point(53, 41);
            this.tb_password1.Name = "tb_password1";
            this.tb_password1.PasswordChar = '*';
            this.tb_password1.Size = new System.Drawing.Size(100, 21);
            this.tb_password1.TabIndex = 1;
            this.tb_password1.Enter += new System.EventHandler(this.tb_password1_Enter);
            this.tb_password1.Leave += new System.EventHandler(this.tb_password1_Leave);
            // 
            // Regest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 314);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Regest";
            this.Text = "用户注册";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Regest_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picb_head)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picb_head;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_password2;
        private System.Windows.Forms.TextBox tb_intro;
        private System.Windows.Forms.TextBox tb_password1;
        private System.Windows.Forms.Button btn_regest;
        private System.Windows.Forms.Label lb_passwdChack;
        private System.Windows.Forms.Label lb_userChack;
        private System.Windows.Forms.ListView lst_heads;
        private System.Windows.Forms.ImageList imgl_heads;
    }
}