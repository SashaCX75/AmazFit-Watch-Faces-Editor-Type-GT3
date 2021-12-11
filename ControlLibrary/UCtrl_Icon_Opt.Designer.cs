
namespace ControlLibrary
{
    partial class UCtrl_Icon_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Icon_Opt));
            this.comboBox_icon_image = new System.Windows.Forms.ComboBox();
            this.numericUpDown_iconX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_iconY = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            this.contextMenuStrip_X.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_icon_image
            // 
            this.comboBox_icon_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_icon_image.DropDownWidth = 135;
            this.comboBox_icon_image.FormattingEnabled = true;
            this.comboBox_icon_image.Location = new System.Drawing.Point(6, 23);
            this.comboBox_icon_image.MaxDropDownItems = 25;
            this.comboBox_icon_image.Name = "comboBox_icon_image";
            this.comboBox_icon_image.Size = new System.Drawing.Size(76, 21);
            this.comboBox_icon_image.TabIndex = 108;
            this.comboBox_icon_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_icon_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_icon_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_icon_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_icon_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_iconX
            // 
            this.numericUpDown_iconX.Location = new System.Drawing.Point(120, 23);
            this.numericUpDown_iconX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_iconX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_iconX.Name = "numericUpDown_iconX";
            this.numericUpDown_iconX.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_iconX.TabIndex = 111;
            this.numericUpDown_iconX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_iconX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_iconY
            // 
            this.numericUpDown_iconY.Location = new System.Drawing.Point(193, 23);
            this.numericUpDown_iconY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_iconY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_iconY.Name = "numericUpDown_iconY";
            this.numericUpDown_iconY.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_iconY.TabIndex = 112;
            this.numericUpDown_iconY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_iconY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // label01
            // 
            this.label01.AutoSize = true;
            this.label01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label01.Location = new System.Drawing.Point(6, 6);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(77, 13);
            this.label01.TabIndex = 107;
            this.label01.Text = "Изображение";
            this.label01.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label02
            // 
            this.label02.AutoSize = true;
            this.label02.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label02.Location = new System.Drawing.Point(103, 6);
            this.label02.Margin = new System.Windows.Forms.Padding(3);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(69, 13);
            this.label02.TabIndex = 113;
            this.label02.Text = "Координаты";
            // 
            // label04
            // 
            this.label04.AutoSize = true;
            this.label04.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label04.Location = new System.Drawing.Point(103, 25);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(17, 13);
            this.label04.TabIndex = 109;
            this.label04.Text = "X:";
            // 
            // label05
            // 
            this.label05.AutoSize = true;
            this.label05.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label05.Location = new System.Drawing.Point(176, 25);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(17, 13);
            this.label05.TabIndex = 110;
            this.label05.Text = "Y:";
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
            // UCtrl_Icon_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_icon_image);
            this.Controls.Add(this.numericUpDown_iconX);
            this.Controls.Add(this.numericUpDown_iconY);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label05);
            this.Name = "UCtrl_Icon_Opt";
            this.Size = new System.Drawing.Size(255, 175);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            this.contextMenuStrip_X.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBox_icon_image;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        public System.Windows.Forms.NumericUpDown numericUpDown_iconX;
        public System.Windows.Forms.NumericUpDown numericUpDown_iconY;
    }
}
