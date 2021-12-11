
namespace ControlLibrary
{
    partial class UCtrl_Circle_Scale_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Circle_Scale_Opt));
            this.numericUpDown_scaleCircleX = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_scaleCircleY = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_scaleCircle_radius = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_scaleCircle_width = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.label06 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.numericUpDown_scaleCircle_startAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_scaleCircle_endAngle = new System.Windows.Forms.NumericUpDown();
            this.label08 = new System.Windows.Forms.Label();
            this.label07 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.checkBox_inversion = new System.Windows.Forms.CheckBox();
            this.checkBox_direction = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleX)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_startAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_endAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_scaleCircleX
            // 
            this.numericUpDown_scaleCircleX.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_scaleCircleX.Location = new System.Drawing.Point(20, 23);
            this.numericUpDown_scaleCircleX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleCircleX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleCircleX.Name = "numericUpDown_scaleCircleX";
            this.numericUpDown_scaleCircleX.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleCircleX.TabIndex = 138;
            this.numericUpDown_scaleCircleX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleCircleX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            // numericUpDown_scaleCircleY
            // 
            this.numericUpDown_scaleCircleY.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_scaleCircleY.Location = new System.Drawing.Point(93, 23);
            this.numericUpDown_scaleCircleY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleCircleY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleCircleY.Name = "numericUpDown_scaleCircleY";
            this.numericUpDown_scaleCircleY.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleCircleY.TabIndex = 139;
            this.numericUpDown_scaleCircleY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleCircleY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // numericUpDown_scaleCircle_radius
            // 
            this.numericUpDown_scaleCircle_radius.Location = new System.Drawing.Point(169, 23);
            this.numericUpDown_scaleCircle_radius.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_radius.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleCircle_radius.Name = "numericUpDown_scaleCircle_radius";
            this.numericUpDown_scaleCircle_radius.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleCircle_radius.TabIndex = 141;
            this.numericUpDown_scaleCircle_radius.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_radius.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_scaleCircle_width
            // 
            this.numericUpDown_scaleCircle_width.Location = new System.Drawing.Point(169, 88);
            this.numericUpDown_scaleCircle_width.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_width.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleCircle_width.Name = "numericUpDown_scaleCircle_width";
            this.numericUpDown_scaleCircle_width.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleCircle_width.TabIndex = 143;
            this.numericUpDown_scaleCircle_width.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_width.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label01
            // 
            this.label01.AutoSize = true;
            this.label01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label01.Location = new System.Drawing.Point(3, 6);
            this.label01.Margin = new System.Windows.Forms.Padding(3);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(139, 13);
            this.label01.TabIndex = 140;
            this.label01.Text = "Координаты ценра шкалы";
            // 
            // label04
            // 
            this.label04.AutoSize = true;
            this.label04.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label04.Location = new System.Drawing.Point(76, 25);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(17, 13);
            this.label04.TabIndex = 137;
            this.label04.Text = "Y:";
            // 
            // label03
            // 
            this.label03.AutoSize = true;
            this.label03.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label03.Location = new System.Drawing.Point(3, 25);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(17, 13);
            this.label03.TabIndex = 136;
            this.label03.Text = "X:";
            // 
            // label06
            // 
            this.label06.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label06.Location = new System.Drawing.Point(150, 53);
            this.label06.Margin = new System.Windows.Forms.Padding(3);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(78, 30);
            this.label06.TabIndex = 144;
            this.label06.Text = "Толщина шкалы";
            this.label06.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label05
            // 
            this.label05.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label05.Location = new System.Drawing.Point(150, 6);
            this.label05.Margin = new System.Windows.Forms.Padding(3);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(78, 13);
            this.label05.TabIndex = 142;
            this.label05.Text = "Радиус";
            this.label05.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numericUpDown_scaleCircle_startAngle
            // 
            this.numericUpDown_scaleCircle_startAngle.Location = new System.Drawing.Point(20, 88);
            this.numericUpDown_scaleCircle_startAngle.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_startAngle.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleCircle_startAngle.Name = "numericUpDown_scaleCircle_startAngle";
            this.numericUpDown_scaleCircle_startAngle.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleCircle_startAngle.TabIndex = 145;
            this.numericUpDown_scaleCircle_startAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_scaleCircle_endAngle
            // 
            this.numericUpDown_scaleCircle_endAngle.Location = new System.Drawing.Point(93, 88);
            this.numericUpDown_scaleCircle_endAngle.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_endAngle.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_scaleCircle_endAngle.Name = "numericUpDown_scaleCircle_endAngle";
            this.numericUpDown_scaleCircle_endAngle.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_scaleCircle_endAngle.TabIndex = 147;
            this.numericUpDown_scaleCircle_endAngle.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_endAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label08
            // 
            this.label08.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label08.Location = new System.Drawing.Point(76, 53);
            this.label08.Margin = new System.Windows.Forms.Padding(3);
            this.label08.Name = "label08";
            this.label08.Size = new System.Drawing.Size(78, 30);
            this.label08.TabIndex = 148;
            this.label08.Text = "Конечный угол";
            this.label08.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label07
            // 
            this.label07.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label07.Location = new System.Drawing.Point(3, 53);
            this.label07.Margin = new System.Windows.Forms.Padding(3);
            this.label07.Name = "label07";
            this.label07.Size = new System.Drawing.Size(78, 30);
            this.label07.TabIndex = 146;
            this.label07.Text = "Начальный угол";
            this.label07.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label02
            // 
            this.label02.AutoSize = true;
            this.label02.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label02.Location = new System.Drawing.Point(3, 170);
            this.label02.Margin = new System.Windows.Forms.Padding(3);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(210, 39);
            this.label02.TabIndex = 168;
            this.label02.Text = "* При включенной инверсии шкалы при \r\nувеличении показаний размер шкалы \r\nбудет у" +
    "меньшаться.";
            // 
            // checkBox_inversion
            // 
            this.checkBox_inversion.AutoSize = true;
            this.checkBox_inversion.Location = new System.Drawing.Point(6, 147);
            this.checkBox_inversion.Name = "checkBox_inversion";
            this.checkBox_inversion.Size = new System.Drawing.Size(117, 17);
            this.checkBox_inversion.TabIndex = 167;
            this.checkBox_inversion.Text = "Инверсия шкалы*";
            this.checkBox_inversion.UseVisualStyleBackColor = true;
            this.checkBox_inversion.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_direction
            // 
            this.checkBox_direction.AutoSize = true;
            this.checkBox_direction.Location = new System.Drawing.Point(6, 124);
            this.checkBox_direction.Name = "checkBox_direction";
            this.checkBox_direction.Size = new System.Drawing.Size(146, 17);
            this.checkBox_direction.TabIndex = 166;
            this.checkBox_direction.Text = "Направление от центра";
            this.checkBox_direction.UseVisualStyleBackColor = true;
            this.checkBox_direction.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // UCtrl_Circle_Scale_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label02);
            this.Controls.Add(this.checkBox_inversion);
            this.Controls.Add(this.checkBox_direction);
            this.Controls.Add(this.numericUpDown_scaleCircle_startAngle);
            this.Controls.Add(this.numericUpDown_scaleCircle_endAngle);
            this.Controls.Add(this.label08);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.numericUpDown_scaleCircleX);
            this.Controls.Add(this.numericUpDown_scaleCircleY);
            this.Controls.Add(this.numericUpDown_scaleCircle_radius);
            this.Controls.Add(this.numericUpDown_scaleCircle_width);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label05);
            this.Name = "UCtrl_Circle_Scale_Opt";
            this.Size = new System.Drawing.Size(250, 274);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleX)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_startAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_endAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label08;
        private System.Windows.Forms.Label label07;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.Label label02;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleCircleX;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleCircleY;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleCircle_radius;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleCircle_width;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleCircle_startAngle;
        public System.Windows.Forms.NumericUpDown numericUpDown_scaleCircle_endAngle;
        public System.Windows.Forms.CheckBox checkBox_inversion;
        public System.Windows.Forms.CheckBox checkBox_direction;
    }
}
