
namespace ControlLibrary
{
    partial class UCtrl_Linear_Scale_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Linear_Scale_Opt));
            this.comboBox_scaleLinear_image_pointer = new System.Windows.Forms.ComboBox();
            this.label02 = new System.Windows.Forms.Label();
            this.numericUpDown_scaleLinearX = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_scaleLinearY = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_scaleLinear_length = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_scaleLinear_width = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.label06 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.comboBox_scaleLinear_color = new System.Windows.Forms.ComboBox();
            this.label07 = new System.Windows.Forms.Label();
            this.groupBox_Orientation = new System.Windows.Forms.GroupBox();
            this.radioButton_vertical = new System.Windows.Forms.RadioButton();
            this.radioButton_horizontal = new System.Windows.Forms.RadioButton();
            this.checkBox_direction = new System.Windows.Forms.CheckBox();
            this.checkBox_inversion = new System.Windows.Forms.CheckBox();
            this.label08 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinearX)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinearY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinear_length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinear_width)).BeginInit();
            this.groupBox_Orientation.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_scaleLinear_image_pointer
            // 
            this.comboBox_scaleLinear_image_pointer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_scaleLinear_image_pointer.DropDownWidth = 135;
            this.comboBox_scaleLinear_image_pointer.FormattingEnabled = true;
            this.comboBox_scaleLinear_image_pointer.Location = new System.Drawing.Point(155, 34);
            this.comboBox_scaleLinear_image_pointer.MaxDropDownItems = 25;
            this.comboBox_scaleLinear_image_pointer.Name = "comboBox_scaleLinear_image_pointer";
            this.comboBox_scaleLinear_image_pointer.Size = new System.Drawing.Size(76, 21);
            this.comboBox_scaleLinear_image_pointer.TabIndex = 130;
            this.comboBox_scaleLinear_image_pointer.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_scaleLinear_image_pointer.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_scaleLinear_image_pointer.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_scaleLinear_image_pointer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_scaleLinear_image_pointer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label02
            // 
            this.label02.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label02.Location = new System.Drawing.Point(153, -1);
            this.label02.Margin = new System.Windows.Forms.Padding(3);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(80, 30);
            this.label02.TabIndex = 129;
            this.label02.Text = "Изображение указателя";
            this.label02.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numericUpDown_scaleLinearX
            // 
            this.numericUpDown_scaleLinearX.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_scaleLinearX.Location = new System.Drawing.Point(20, 34);
            this.numericUpDown_scaleLinearX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleLinearX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleLinearX.Name = "numericUpDown_scaleLinearX";
            this.numericUpDown_scaleLinearX.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleLinearX.TabIndex = 133;
            this.numericUpDown_scaleLinearX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleLinearX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            // numericUpDown_scaleLinearY
            // 
            this.numericUpDown_scaleLinearY.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_scaleLinearY.Location = new System.Drawing.Point(93, 34);
            this.numericUpDown_scaleLinearY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleLinearY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleLinearY.Name = "numericUpDown_scaleLinearY";
            this.numericUpDown_scaleLinearY.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleLinearY.TabIndex = 134;
            this.numericUpDown_scaleLinearY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleLinearY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // numericUpDown_scaleLinear_length
            // 
            this.numericUpDown_scaleLinear_length.Location = new System.Drawing.Point(20, 99);
            this.numericUpDown_scaleLinear_length.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleLinear_length.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleLinear_length.Name = "numericUpDown_scaleLinear_length";
            this.numericUpDown_scaleLinear_length.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleLinear_length.TabIndex = 136;
            this.numericUpDown_scaleLinear_length.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_scaleLinear_length.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_scaleLinear_width
            // 
            this.numericUpDown_scaleLinear_width.Location = new System.Drawing.Point(93, 99);
            this.numericUpDown_scaleLinear_width.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleLinear_width.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleLinear_width.Name = "numericUpDown_scaleLinear_width";
            this.numericUpDown_scaleLinear_width.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleLinear_width.TabIndex = 138;
            this.numericUpDown_scaleLinear_width.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_scaleLinear_width.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label01
            // 
            this.label01.AutoSize = true;
            this.label01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label01.Location = new System.Drawing.Point(3, 17);
            this.label01.Margin = new System.Windows.Forms.Padding(3);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(144, 13);
            this.label01.TabIndex = 135;
            this.label01.Text = "Координаты начала шкалы";
            // 
            // label04
            // 
            this.label04.AutoSize = true;
            this.label04.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label04.Location = new System.Drawing.Point(76, 36);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(17, 13);
            this.label04.TabIndex = 132;
            this.label04.Text = "Y:";
            // 
            // label03
            // 
            this.label03.AutoSize = true;
            this.label03.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label03.Location = new System.Drawing.Point(3, 36);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(17, 13);
            this.label03.TabIndex = 131;
            this.label03.Text = "X:";
            // 
            // label06
            // 
            this.label06.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label06.Location = new System.Drawing.Point(76, 65);
            this.label06.Margin = new System.Windows.Forms.Padding(3);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(78, 30);
            this.label06.TabIndex = 139;
            this.label06.Text = "Толщина шкалы";
            this.label06.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label05
            // 
            this.label05.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label05.Location = new System.Drawing.Point(8, 65);
            this.label05.Margin = new System.Windows.Forms.Padding(3);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(68, 30);
            this.label05.TabIndex = 137;
            this.label05.Text = "Длина шкалы";
            this.label05.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // comboBox_scaleLinear_color
            // 
            this.comboBox_scaleLinear_color.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_scaleLinear_color.DropDownHeight = 1;
            this.comboBox_scaleLinear_color.FormattingEnabled = true;
            this.comboBox_scaleLinear_color.IntegralHeight = false;
            this.comboBox_scaleLinear_color.Location = new System.Drawing.Point(155, 98);
            this.comboBox_scaleLinear_color.Name = "comboBox_scaleLinear_color";
            this.comboBox_scaleLinear_color.Size = new System.Drawing.Size(75, 21);
            this.comboBox_scaleLinear_color.TabIndex = 140;
            this.comboBox_scaleLinear_color.Click += new System.EventHandler(this.comboBox_scaleLinear_color_Click);
            // 
            // label07
            // 
            this.label07.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label07.Location = new System.Drawing.Point(153, 65);
            this.label07.Margin = new System.Windows.Forms.Padding(3);
            this.label07.Name = "label07";
            this.label07.Size = new System.Drawing.Size(78, 30);
            this.label07.TabIndex = 141;
            this.label07.Text = "Цвет";
            this.label07.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // groupBox_Orientation
            // 
            this.groupBox_Orientation.Controls.Add(this.radioButton_vertical);
            this.groupBox_Orientation.Controls.Add(this.radioButton_horizontal);
            this.groupBox_Orientation.Location = new System.Drawing.Point(3, 130);
            this.groupBox_Orientation.Name = "groupBox_Orientation";
            this.groupBox_Orientation.Size = new System.Drawing.Size(230, 54);
            this.groupBox_Orientation.TabIndex = 162;
            this.groupBox_Orientation.TabStop = false;
            this.groupBox_Orientation.Text = "Ориентация";
            // 
            // radioButton_vertical
            // 
            this.radioButton_vertical.AutoSize = true;
            this.radioButton_vertical.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton_vertical.Location = new System.Drawing.Point(125, 31);
            this.radioButton_vertical.Name = "radioButton_vertical";
            this.radioButton_vertical.Size = new System.Drawing.Size(97, 17);
            this.radioButton_vertical.TabIndex = 1;
            this.radioButton_vertical.Text = "Вертикальная";
            this.radioButton_vertical.UseVisualStyleBackColor = true;
            // 
            // radioButton_horizontal
            // 
            this.radioButton_horizontal.AutoSize = true;
            this.radioButton_horizontal.Checked = true;
            this.radioButton_horizontal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton_horizontal.Location = new System.Drawing.Point(7, 31);
            this.radioButton_horizontal.Name = "radioButton_horizontal";
            this.radioButton_horizontal.Size = new System.Drawing.Size(108, 17);
            this.radioButton_horizontal.TabIndex = 0;
            this.radioButton_horizontal.TabStop = true;
            this.radioButton_horizontal.Text = "Горизонтальная";
            this.radioButton_horizontal.UseVisualStyleBackColor = true;
            this.radioButton_horizontal.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // checkBox_direction
            // 
            this.checkBox_direction.AutoSize = true;
            this.checkBox_direction.Location = new System.Drawing.Point(6, 190);
            this.checkBox_direction.Name = "checkBox_direction";
            this.checkBox_direction.Size = new System.Drawing.Size(146, 17);
            this.checkBox_direction.TabIndex = 163;
            this.checkBox_direction.Text = "Направление от центра";
            this.checkBox_direction.UseVisualStyleBackColor = true;
            this.checkBox_direction.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_inversion
            // 
            this.checkBox_inversion.AutoSize = true;
            this.checkBox_inversion.Location = new System.Drawing.Point(6, 213);
            this.checkBox_inversion.Name = "checkBox_inversion";
            this.checkBox_inversion.Size = new System.Drawing.Size(117, 17);
            this.checkBox_inversion.TabIndex = 164;
            this.checkBox_inversion.Text = "Инверсия шкалы*";
            this.checkBox_inversion.UseVisualStyleBackColor = true;
            this.checkBox_inversion.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // label08
            // 
            this.label08.AutoSize = true;
            this.label08.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label08.Location = new System.Drawing.Point(3, 236);
            this.label08.Margin = new System.Windows.Forms.Padding(3);
            this.label08.Name = "label08";
            this.label08.Size = new System.Drawing.Size(210, 39);
            this.label08.TabIndex = 165;
            this.label08.Text = "* При включенной инверсии шкалы при \r\nувеличении показаний размер шкалы \r\nбудет у" +
    "меньшаться.";
            // 
            // UCtrl_Linear_Scale_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label08);
            this.Controls.Add(this.checkBox_inversion);
            this.Controls.Add(this.checkBox_direction);
            this.Controls.Add(this.groupBox_Orientation);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.comboBox_scaleLinear_color);
            this.Controls.Add(this.numericUpDown_scaleLinearX);
            this.Controls.Add(this.numericUpDown_scaleLinearY);
            this.Controls.Add(this.numericUpDown_scaleLinear_length);
            this.Controls.Add(this.numericUpDown_scaleLinear_width);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.comboBox_scaleLinear_image_pointer);
            this.Controls.Add(this.label02);
            this.Name = "UCtrl_Linear_Scale_Opt";
            this.Size = new System.Drawing.Size(250, 430);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinearX)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinearY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinear_length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleLinear_width)).EndInit();
            this.groupBox_Orientation.ResumeLayout(false);
            this.groupBox_Orientation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_scaleLinear_image_pointer;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.ComboBox comboBox_scaleLinear_color;
        private System.Windows.Forms.Label label07;
        private System.Windows.Forms.GroupBox groupBox_Orientation;
        private System.Windows.Forms.Label label08;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleLinearX;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleLinearY;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleLinear_length;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleLinear_width;
        public System.Windows.Forms.RadioButton radioButton_vertical;
        public System.Windows.Forms.RadioButton radioButton_horizontal;
        public System.Windows.Forms.CheckBox checkBox_direction;
        public System.Windows.Forms.CheckBox checkBox_inversion;
    }
}
