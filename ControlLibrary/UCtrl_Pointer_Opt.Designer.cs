
namespace ControlLibrary
{
    partial class UCtrl_Pointer_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Pointer_Opt));
            this.comboBox_pointer_image = new System.Windows.Forms.ComboBox();
            this.numericUpDown_pointer_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_pointer_Y = new System.Windows.Forms.NumericUpDown();
            this.label02 = new System.Windows.Forms.Label();
            this.label01 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_pointer_offset_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_pointer_offset_Y = new System.Windows.Forms.NumericUpDown();
            this.label03 = new System.Windows.Forms.Label();
            this.label07 = new System.Windows.Forms.Label();
            this.label06 = new System.Windows.Forms.Label();
            this.numericUpDown_pointer_startAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_pointer_endAngle = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_pointer_imageCentr = new System.Windows.Forms.ComboBox();
            this.numericUpDown_pointer_centr_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_pointer_centr_Y = new System.Windows.Forms.NumericUpDown();
            this.label09 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label08 = new System.Windows.Forms.Label();
            this.comboBox_pointer_imageBackground = new System.Windows.Forms.ComboBox();
            this.numericUpDown_pointer_background_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_pointer_background_Y = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_Y)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_offset_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_offset_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_startAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_endAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_centr_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_centr_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_background_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_background_Y)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_pointer_image
            // 
            this.comboBox_pointer_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_pointer_image.DropDownWidth = 135;
            this.comboBox_pointer_image.FormattingEnabled = true;
            this.comboBox_pointer_image.Location = new System.Drawing.Point(3, 34);
            this.comboBox_pointer_image.MaxDropDownItems = 25;
            this.comboBox_pointer_image.Name = "comboBox_pointer_image";
            this.comboBox_pointer_image.Size = new System.Drawing.Size(76, 21);
            this.comboBox_pointer_image.TabIndex = 114;
            this.comboBox_pointer_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_pointer_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_pointer_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_pointer_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_pointer_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_pointer_X
            // 
            this.numericUpDown_pointer_X.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_pointer_X.Location = new System.Drawing.Point(120, 34);
            this.numericUpDown_pointer_X.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_X.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_X.Name = "numericUpDown_pointer_X";
            this.numericUpDown_pointer_X.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_X.TabIndex = 117;
            this.numericUpDown_pointer_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_pointer_Y
            // 
            this.numericUpDown_pointer_Y.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_pointer_Y.Location = new System.Drawing.Point(193, 34);
            this.numericUpDown_pointer_Y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_Y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_Y.Name = "numericUpDown_pointer_Y";
            this.numericUpDown_pointer_Y.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_Y.TabIndex = 118;
            this.numericUpDown_pointer_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // label02
            // 
            this.label02.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label02.Location = new System.Drawing.Point(99, 3);
            this.label02.Margin = new System.Windows.Forms.Padding(3);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(144, 27);
            this.label02.TabIndex = 119;
            this.label02.Text = "Координаты центра вращения";
            this.label02.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label01
            // 
            this.label01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label01.Location = new System.Drawing.Point(3, 0);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(80, 30);
            this.label01.TabIndex = 113;
            this.label01.Text = "Изображение стрелки";
            this.label01.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label05
            // 
            this.label05.AutoSize = true;
            this.label05.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label05.Location = new System.Drawing.Point(176, 36);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(17, 13);
            this.label05.TabIndex = 116;
            this.label05.Text = "Y:";
            // 
            // label04
            // 
            this.label04.AutoSize = true;
            this.label04.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label04.Location = new System.Drawing.Point(103, 36);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(17, 13);
            this.label04.TabIndex = 115;
            this.label04.Text = "X:";
            // 
            // contextMenuStrip_X
            // 
            this.contextMenuStrip_X.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_X.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатуХToolStripMenuItem,
            this.копироватьToolStripMenuItemX,
            this.вставитьToolStripMenuItemX});
            this.contextMenuStrip_X.Name = "contextMenuStrip_X";
            this.contextMenuStrip_X.Size = new System.Drawing.Size(204, 104);
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
            // numericUpDown_pointer_offset_X
            // 
            this.numericUpDown_pointer_offset_X.Location = new System.Drawing.Point(20, 97);
            this.numericUpDown_pointer_offset_X.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_offset_X.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_offset_X.Name = "numericUpDown_pointer_offset_X";
            this.numericUpDown_pointer_offset_X.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_offset_X.TabIndex = 124;
            this.numericUpDown_pointer_offset_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_pointer_offset_Y
            // 
            this.numericUpDown_pointer_offset_Y.Location = new System.Drawing.Point(93, 97);
            this.numericUpDown_pointer_offset_Y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_offset_Y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_offset_Y.Name = "numericUpDown_pointer_offset_Y";
            this.numericUpDown_pointer_offset_Y.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_offset_Y.TabIndex = 125;
            this.numericUpDown_pointer_offset_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label03
            // 
            this.label03.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label03.Location = new System.Drawing.Point(3, 66);
            this.label03.Margin = new System.Windows.Forms.Padding(3);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(130, 27);
            this.label03.TabIndex = 126;
            this.label03.Text = "Центр вращения стрелки";
            this.label03.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label07
            // 
            this.label07.AutoSize = true;
            this.label07.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label07.Location = new System.Drawing.Point(76, 99);
            this.label07.Name = "label07";
            this.label07.Size = new System.Drawing.Size(17, 13);
            this.label07.TabIndex = 123;
            this.label07.Text = "Y:";
            // 
            // label06
            // 
            this.label06.AutoSize = true;
            this.label06.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label06.Location = new System.Drawing.Point(3, 99);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(17, 13);
            this.label06.TabIndex = 122;
            this.label06.Text = "X:";
            // 
            // numericUpDown_pointer_startAngle
            // 
            this.numericUpDown_pointer_startAngle.Location = new System.Drawing.Point(20, 160);
            this.numericUpDown_pointer_startAngle.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_startAngle.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_startAngle.Name = "numericUpDown_pointer_startAngle";
            this.numericUpDown_pointer_startAngle.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_startAngle.TabIndex = 127;
            this.numericUpDown_pointer_startAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_pointer_endAngle
            // 
            this.numericUpDown_pointer_endAngle.Location = new System.Drawing.Point(93, 160);
            this.numericUpDown_pointer_endAngle.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_endAngle.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_endAngle.Name = "numericUpDown_pointer_endAngle";
            this.numericUpDown_pointer_endAngle.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_endAngle.TabIndex = 129;
            this.numericUpDown_pointer_endAngle.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown_pointer_endAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(76, 126);
            this.label11.Margin = new System.Windows.Forms.Padding(3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 30);
            this.label11.TabIndex = 130;
            this.label11.Text = "Конечный угол";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label10
            // 
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(3, 126);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 30);
            this.label10.TabIndex = 128;
            this.label10.Text = "Начальный угол";
            this.label10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // comboBox_pointer_imageCentr
            // 
            this.comboBox_pointer_imageCentr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_pointer_imageCentr.DropDownWidth = 135;
            this.comboBox_pointer_imageCentr.FormattingEnabled = true;
            this.comboBox_pointer_imageCentr.Location = new System.Drawing.Point(3, 223);
            this.comboBox_pointer_imageCentr.MaxDropDownItems = 25;
            this.comboBox_pointer_imageCentr.Name = "comboBox_pointer_imageCentr";
            this.comboBox_pointer_imageCentr.Size = new System.Drawing.Size(76, 21);
            this.comboBox_pointer_imageCentr.TabIndex = 132;
            this.comboBox_pointer_imageCentr.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_pointer_imageCentr.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_pointer_imageCentr.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_pointer_imageCentr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_pointer_imageCentr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_pointer_centr_X
            // 
            this.numericUpDown_pointer_centr_X.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_pointer_centr_X.Location = new System.Drawing.Point(120, 225);
            this.numericUpDown_pointer_centr_X.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_centr_X.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_centr_X.Name = "numericUpDown_pointer_centr_X";
            this.numericUpDown_pointer_centr_X.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_centr_X.TabIndex = 135;
            this.numericUpDown_pointer_centr_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_centr_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_pointer_centr_Y
            // 
            this.numericUpDown_pointer_centr_Y.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_pointer_centr_Y.Location = new System.Drawing.Point(193, 225);
            this.numericUpDown_pointer_centr_Y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_centr_Y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_centr_Y.Name = "numericUpDown_pointer_centr_Y";
            this.numericUpDown_pointer_centr_Y.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_centr_Y.TabIndex = 136;
            this.numericUpDown_pointer_centr_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_centr_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // label09
            // 
            this.label09.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label09.Location = new System.Drawing.Point(99, 190);
            this.label09.Margin = new System.Windows.Forms.Padding(3);
            this.label09.Name = "label09";
            this.label09.Size = new System.Drawing.Size(144, 30);
            this.label09.TabIndex = 137;
            this.label09.Text = "Координаты центрального изображения";
            this.label09.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(103, 227);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 133;
            this.label12.Text = "X:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(176, 227);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 13);
            this.label13.TabIndex = 134;
            this.label13.Text = "Y:";
            // 
            // label08
            // 
            this.label08.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label08.Location = new System.Drawing.Point(3, 190);
            this.label08.Margin = new System.Windows.Forms.Padding(3);
            this.label08.Name = "label08";
            this.label08.Size = new System.Drawing.Size(80, 30);
            this.label08.TabIndex = 131;
            this.label08.Text = "Центральное изображение";
            this.label08.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // comboBox_pointer_imageBackground
            // 
            this.comboBox_pointer_imageBackground.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_pointer_imageBackground.DropDownWidth = 135;
            this.comboBox_pointer_imageBackground.FormattingEnabled = true;
            this.comboBox_pointer_imageBackground.Location = new System.Drawing.Point(3, 286);
            this.comboBox_pointer_imageBackground.MaxDropDownItems = 25;
            this.comboBox_pointer_imageBackground.Name = "comboBox_pointer_imageBackground";
            this.comboBox_pointer_imageBackground.Size = new System.Drawing.Size(76, 21);
            this.comboBox_pointer_imageBackground.TabIndex = 139;
            this.comboBox_pointer_imageBackground.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_pointer_imageBackground.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_pointer_imageBackground.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_pointer_imageBackground.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_pointer_imageBackground.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_pointer_background_X
            // 
            this.numericUpDown_pointer_background_X.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_pointer_background_X.Location = new System.Drawing.Point(120, 288);
            this.numericUpDown_pointer_background_X.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_background_X.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_background_X.Name = "numericUpDown_pointer_background_X";
            this.numericUpDown_pointer_background_X.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_background_X.TabIndex = 142;
            this.numericUpDown_pointer_background_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_background_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_pointer_background_Y
            // 
            this.numericUpDown_pointer_background_Y.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_pointer_background_Y.Location = new System.Drawing.Point(193, 288);
            this.numericUpDown_pointer_background_Y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_pointer_background_Y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_pointer_background_Y.Name = "numericUpDown_pointer_background_Y";
            this.numericUpDown_pointer_background_Y.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_pointer_background_Y.TabIndex = 143;
            this.numericUpDown_pointer_background_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_background_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // label15
            // 
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(99, 253);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(144, 30);
            this.label15.TabIndex = 144;
            this.label15.Text = "Координаты фонового изображения";
            this.label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(103, 290);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 13);
            this.label16.TabIndex = 140;
            this.label16.Text = "X:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(176, 290);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(17, 13);
            this.label17.TabIndex = 141;
            this.label17.Text = "Y:";
            // 
            // label14
            // 
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(3, 253);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 30);
            this.label14.TabIndex = 138;
            this.label14.Text = "Фоновое изображение";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // UCtrl_Pointer_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_pointer_imageBackground);
            this.Controls.Add(this.numericUpDown_pointer_background_X);
            this.Controls.Add(this.numericUpDown_pointer_background_Y);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.comboBox_pointer_imageCentr);
            this.Controls.Add(this.numericUpDown_pointer_centr_X);
            this.Controls.Add(this.numericUpDown_pointer_centr_Y);
            this.Controls.Add(this.label09);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label08);
            this.Controls.Add(this.numericUpDown_pointer_startAngle);
            this.Controls.Add(this.numericUpDown_pointer_endAngle);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDown_pointer_offset_X);
            this.Controls.Add(this.numericUpDown_pointer_offset_Y);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.comboBox_pointer_image);
            this.Controls.Add(this.numericUpDown_pointer_X);
            this.Controls.Add(this.numericUpDown_pointer_Y);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.label04);
            this.Name = "UCtrl_Pointer_Opt";
            this.Size = new System.Drawing.Size(250, 430);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_Y)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_offset_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_offset_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_startAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_endAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_centr_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_centr_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_background_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_background_Y)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_pointer_image;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.Label label07;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_pointer_imageCentr;
        private System.Windows.Forms.Label label09;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label08;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_X;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_Y;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_offset_X;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_offset_Y;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_startAngle;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_endAngle;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_centr_X;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_centr_Y;
        private System.Windows.Forms.ComboBox comboBox_pointer_imageBackground;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_background_X;
        public System.Windows.Forms.NumericUpDown numericUpDown_pointer_background_Y;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
    }
}
