namespace Client
{
    partial class frmScreen
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //Download by http://www.codefans.net
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
            System.Windows.Forms.Button button1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScreen));
            System.Windows.Forms.Button button2;
            System.Windows.Forms.Button button3;
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            button1.Location = new System.Drawing.Point(37, 37);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(32, 33);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            button2.Location = new System.Drawing.Point(84, 37);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(32, 33);
            button2.TabIndex = 0;
            button2.UseVisualStyleBackColor = true;
            button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            button3.Location = new System.Drawing.Point(132, 37);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(32, 33);
            button3.TabIndex = 0;
            button3.UseVisualStyleBackColor = true;
            button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(button3);
            this.Controls.Add(button2);
            this.Controls.Add(button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmScreen";
            this.Text = "frmScreen";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmScreen_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmScreen_Paint);
            this.DoubleClick += new System.EventHandler(this.frmScreen_DoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmScreen_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmScreen_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmScreen_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmScreen_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}