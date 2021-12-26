
namespace ControlLibrary
{
    partial class UCtrl_Shortcut_Opt
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Shortcut_Opt));
            this.comboBox_Image = new System.Windows.Forms.ComboBox();
            this.numericUpDown_imageX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_imageY = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_height = new System.Windows.Forms.NumericUpDown();
            this.label06 = new System.Windows.Forms.Label();
            this.label07 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_height)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_Image
            // 
            this.comboBox_Image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_Image.DropDownWidth = 135;
            this.comboBox_Image.FormattingEnabled = true;
            this.comboBox_Image.Location = new System.Drawing.Point(159, 23);
            this.comboBox_Image.MaxDropDownItems = 25;
            this.comboBox_Image.Name = "comboBox_Image";
            this.comboBox_Image.Size = new System.Drawing.Size(76, 21);
            this.comboBox_Image.TabIndex = 115;
            this.comboBox_Image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_Image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_Image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_Image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_Image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_imageX
            // 
            this.numericUpDown_imageX.Location = new System.Drawing.Point(23, 23);
            this.numericUpDown_imageX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_imageX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_imageX.Name = "numericUpDown_imageX";
            this.numericUpDown_imageX.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_imageX.TabIndex = 118;
            this.numericUpDown_imageX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_imageX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_imageY
            // 
            this.numericUpDown_imageY.Location = new System.Drawing.Point(96, 23);
            this.numericUpDown_imageY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_imageY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_imageY.Name = "numericUpDown_imageY";
            this.numericUpDown_imageY.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_imageY.TabIndex = 119;
            this.numericUpDown_imageY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_imageY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // label01
            // 
            this.label01.AutoSize = true;
            this.label01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label01.Location = new System.Drawing.Point(159, 6);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(84, 13);
            this.label01.TabIndex = 114;
            this.label01.Text = "Изображение *";
            this.label01.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label02
            // 
            this.label02.AutoSize = true;
            this.label02.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label02.Location = new System.Drawing.Point(6, 6);
            this.label02.Margin = new System.Windows.Forms.Padding(3);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(69, 13);
            this.label02.TabIndex = 120;
            this.label02.Text = "Координаты";
            // 
            // label04
            // 
            this.label04.AutoSize = true;
            this.label04.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label04.Location = new System.Drawing.Point(6, 25);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(17, 13);
            this.label04.TabIndex = 116;
            this.label04.Text = "X:";
            // 
            // label05
            // 
            this.label05.AutoSize = true;
            this.label05.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label05.Location = new System.Drawing.Point(79, 25);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(17, 13);
            this.label05.TabIndex = 117;
            this.label05.Text = "Y:";
            // 
            // contextMenuStrip_X
            // 
            this.contextMenuStrip_X.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_X.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатуХToolStripMenuItem,
            this.копироватьToolStripMenuItemX,
            this.вставитьToolStripMenuItemX});
            this.contextMenuStrip_X.Name = "contextMenuStrip_X";
            this.contextMenuStrip_X.Size = new System.Drawing.Size(204, 82);
            this.contextMenuStrip_X.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_X_Opening);
            // 
            // вставитьКоординатуХToolStripMenuItem
            // 
            this.вставитьКоординатуХToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("вставитьКоординатуХToolStripMenuItem.Image")));
            this.вставитьКоординатуХToolStripMenuItem.Name = "вставитьКоординатуХToolStripMenuItem";
            this.вставитьКоординатуХToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.вставитьКоординатуХToolStripMenuItem.Text = "Вставить координату Х";
            this.вставитьКоординатуХToolStripMenuItem.Click += new System.EventHandler(this.вставитьКоординатуХToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItemX
            // 
            this.копироватьToolStripMenuItemX.Image = ((System.Drawing.Image)(resources.GetObject("копироватьToolStripMenuItemX.Image")));
            this.копироватьToolStripMenuItemX.Name = "копироватьToolStripMenuItemX";
            this.копироватьToolStripMenuItemX.Size = new System.Drawing.Size(203, 26);
            this.копироватьToolStripMenuItemX.Text = "Копировать";
            this.копироватьToolStripMenuItemX.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вставитьToolStripMenuItemX
            // 
            this.вставитьToolStripMenuItemX.Image = ((System.Drawing.Image)(resources.GetObject("вставитьToolStripMenuItemX.Image")));
            this.вставитьToolStripMenuItemX.Name = "вставитьToolStripMenuItemX";
            this.вставитьToolStripMenuItemX.Size = new System.Drawing.Size(203, 26);
            this.вставитьToolStripMenuItemX.Text = "Вставить";
            this.вставитьToolStripMenuItemX.Click += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // contextMenuStrip_Y
            // 
            this.contextMenuStrip_Y.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Y.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатуYToolStripMenuItem,
            this.копироватьToolStripMenuItemY,
            this.вставитьToolStripMenuItemY});
            this.contextMenuStrip_Y.Name = "contextMenuStrip_X";
            this.contextMenuStrip_Y.Size = new System.Drawing.Size(204, 82);
            this.contextMenuStrip_Y.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Y_Opening);
            // 
            // вставитьКоординатуYToolStripMenuItem
            // 
            this.вставитьКоординатуYToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("вставитьКоординатуYToolStripMenuItem.Image")));
            this.вставитьКоординатуYToolStripMenuItem.Name = "вставитьКоординатуYToolStripMenuItem";
            this.вставитьКоординатуYToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.вставитьКоординатуYToolStripMenuItem.Text = "Вставить координату Y";
            this.вставитьКоординатуYToolStripMenuItem.Click += new System.EventHandler(this.вставитьКоординатуYToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItemY
            // 
            this.копироватьToolStripMenuItemY.Image = ((System.Drawing.Image)(resources.GetObject("копироватьToolStripMenuItemY.Image")));
            this.копироватьToolStripMenuItemY.Name = "копироватьToolStripMenuItemY";
            this.копироватьToolStripMenuItemY.Size = new System.Drawing.Size(203, 26);
            this.копироватьToolStripMenuItemY.Text = "Копировать";
            this.копироватьToolStripMenuItemY.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вставитьToolStripMenuItemY
            // 
            this.вставитьToolStripMenuItemY.Image = ((System.Drawing.Image)(resources.GetObject("вставитьToolStripMenuItemY.Image")));
            this.вставитьToolStripMenuItemY.Name = "вставитьToolStripMenuItemY";
            this.вставитьToolStripMenuItemY.Size = new System.Drawing.Size(203, 26);
            this.вставитьToolStripMenuItemY.Text = "Вставить";
            this.вставитьToolStripMenuItemY.Click += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // numericUpDown_width
            // 
            this.numericUpDown_width.Location = new System.Drawing.Point(23, 90);
            this.numericUpDown_width.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_width.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_width.Name = "numericUpDown_width";
            this.numericUpDown_width.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_width.TabIndex = 140;
            this.numericUpDown_width.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_width.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_width.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_length_MouseDoubleClick);
            // 
            // numericUpDown_height
            // 
            this.numericUpDown_height.Location = new System.Drawing.Point(96, 90);
            this.numericUpDown_height.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_height.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_height.Name = "numericUpDown_height";
            this.numericUpDown_height.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_height.TabIndex = 142;
            this.numericUpDown_height.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_height.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_height.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_width_MouseDoubleClick);
            // 
            // label06
            // 
            this.label06.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label06.Location = new System.Drawing.Point(79, 56);
            this.label06.Margin = new System.Windows.Forms.Padding(3);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(78, 30);
            this.label06.TabIndex = 143;
            this.label06.Text = "Высота области";
            this.label06.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label07
            // 
            this.label07.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label07.Location = new System.Drawing.Point(11, 56);
            this.label07.Margin = new System.Windows.Forms.Padding(3);
            this.label07.Name = "label07";
            this.label07.Size = new System.Drawing.Size(68, 30);
            this.label07.TabIndex = 141;
            this.label07.Text = "Ширина области";
            this.label07.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 26);
            this.label1.TabIndex = 144;
            this.label1.Text = "* Изображение отображаетмся только \r\nв момент нажатия на ярлык";
            // 
            // UCtrl_Shortcut_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_width);
            this.Controls.Add(this.numericUpDown_height);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.comboBox_Image);
            this.Controls.Add(this.numericUpDown_imageX);
            this.Controls.Add(this.numericUpDown_imageY);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label05);
            this.Name = "UCtrl_Shortcut_Opt";
            this.Size = new System.Drawing.Size(255, 175);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBox_Image;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageX;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageY;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        public System.Windows.Forms.NumericUpDown numericUpDown_width;
        public System.Windows.Forms.NumericUpDown numericUpDown_height;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label07;
        private System.Windows.Forms.Label label1;
    }
}
