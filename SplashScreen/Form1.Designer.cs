namespace SplashScreen
{
    partial class SplashScreen
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.pictureBox_SplashScreen = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SplashScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_SplashScreen
            // 
            this.pictureBox_SplashScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_SplashScreen.BackgroundImage")));
            this.pictureBox_SplashScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_SplashScreen.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_SplashScreen.Name = "pictureBox_SplashScreen";
            this.pictureBox_SplashScreen.Padding = new System.Windows.Forms.Padding(250, 300, 0, 0);
            this.pictureBox_SplashScreen.Size = new System.Drawing.Size(649, 449);
            this.pictureBox_SplashScreen.TabIndex = 0;
            this.pictureBox_SplashScreen.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "loader_flash_01.png");
            this.imageList1.Images.SetKeyName(1, "loader_flash_02.png");
            this.imageList1.Images.SetKeyName(2, "loader_flash_03.png");
            this.imageList1.Images.SetKeyName(3, "loader_flash_04.png");
            this.imageList1.Images.SetKeyName(4, "loader_flash_05.png");
            this.imageList1.Images.SetKeyName(5, "loader_flash_06.png");
            this.imageList1.Images.SetKeyName(6, "loader_flash_07.png");
            this.imageList1.Images.SetKeyName(7, "loader_flash_08.png");
            this.imageList1.Images.SetKeyName(8, "loader_flash_09.png");
            this.imageList1.Images.SetKeyName(9, "loader_flash_10.png");
            this.imageList1.Images.SetKeyName(10, "loader_flash_11.png");
            this.imageList1.Images.SetKeyName(11, "loader_flash_12.png");
            this.imageList1.Images.SetKeyName(12, "loader_flash_13.png");
            this.imageList1.Images.SetKeyName(13, "loader_flash_14.png");
            this.imageList1.Images.SetKeyName(14, "loader_flash_15.png");
            this.imageList1.Images.SetKeyName(15, "loader_flash_16.png");
            this.imageList1.Images.SetKeyName(16, "loader_flash_17.png");
            this.imageList1.Images.SetKeyName(17, "loader_flash_18.png");
            this.imageList1.Images.SetKeyName(18, "loader_flash_19.png");
            this.imageList1.Images.SetKeyName(19, "loader_flash_20.png");
            this.imageList1.Images.SetKeyName(20, "loader_flash_21.png");
            this.imageList1.Images.SetKeyName(21, "loader_flash_22.png");
            this.imageList1.Images.SetKeyName(22, "loader_flash_23.png");
            this.imageList1.Images.SetKeyName(23, "loader_flash_24.png");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 70;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 15000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(649, 449);
            this.Controls.Add(this.pictureBox_SplashScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AmazFit WatchFace editor (ZeppOS) SplashScreen";
            this.TransparencyKey = System.Drawing.Color.Lime;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SplashScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_SplashScreen;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}

