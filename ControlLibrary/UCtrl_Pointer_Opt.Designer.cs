
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
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_pointer_Y = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.label02 = new System.Windows.Forms.Label();
            this.label01 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
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
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_Y)).BeginInit();
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
            resources.ApplyResources(this.comboBox_pointer_image, "comboBox_pointer_image");
            this.comboBox_pointer_image.Name = "comboBox_pointer_image";
            this.comboBox_pointer_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_pointer_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_pointer_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_pointer_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_pointer_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_pointer_X
            // 
            this.numericUpDown_pointer_X.ContextMenuStrip = this.contextMenuStrip_X;
            resources.ApplyResources(this.numericUpDown_pointer_X, "numericUpDown_pointer_X");
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
            this.numericUpDown_pointer_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_KeyDown);
            this.numericUpDown_pointer_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // contextMenuStrip_X
            // 
            this.contextMenuStrip_X.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_X.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатуХToolStripMenuItem,
            this.копироватьToolStripMenuItemX,
            this.вставитьToolStripMenuItemX});
            this.contextMenuStrip_X.Name = "contextMenuStrip_X";
            resources.ApplyResources(this.contextMenuStrip_X, "contextMenuStrip_X");
            this.contextMenuStrip_X.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_X_Opening);
            // 
            // вставитьКоординатуХToolStripMenuItem
            // 
            resources.ApplyResources(this.вставитьКоординатуХToolStripMenuItem, "вставитьКоординатуХToolStripMenuItem");
            this.вставитьКоординатуХToolStripMenuItem.Name = "вставитьКоординатуХToolStripMenuItem";
            this.вставитьКоординатуХToolStripMenuItem.Click += new System.EventHandler(this.вставитьКоординатуХToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItemX
            // 
            resources.ApplyResources(this.копироватьToolStripMenuItemX, "копироватьToolStripMenuItemX");
            this.копироватьToolStripMenuItemX.Name = "копироватьToolStripMenuItemX";
            this.копироватьToolStripMenuItemX.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вставитьToolStripMenuItemX
            // 
            resources.ApplyResources(this.вставитьToolStripMenuItemX, "вставитьToolStripMenuItemX");
            this.вставитьToolStripMenuItemX.Name = "вставитьToolStripMenuItemX";
            this.вставитьToolStripMenuItemX.Click += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // numericUpDown_pointer_Y
            // 
            this.numericUpDown_pointer_Y.ContextMenuStrip = this.contextMenuStrip_Y;
            resources.ApplyResources(this.numericUpDown_pointer_Y, "numericUpDown_pointer_Y");
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
            this.numericUpDown_pointer_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_KeyDown);
            this.numericUpDown_pointer_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // contextMenuStrip_Y
            // 
            this.contextMenuStrip_Y.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Y.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатуYToolStripMenuItem,
            this.копироватьToolStripMenuItemY,
            this.вставитьToolStripMenuItemY});
            this.contextMenuStrip_Y.Name = "contextMenuStrip_X";
            resources.ApplyResources(this.contextMenuStrip_Y, "contextMenuStrip_Y");
            this.contextMenuStrip_Y.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Y_Opening);
            // 
            // вставитьКоординатуYToolStripMenuItem
            // 
            resources.ApplyResources(this.вставитьКоординатуYToolStripMenuItem, "вставитьКоординатуYToolStripMenuItem");
            this.вставитьКоординатуYToolStripMenuItem.Name = "вставитьКоординатуYToolStripMenuItem";
            this.вставитьКоординатуYToolStripMenuItem.Click += new System.EventHandler(this.вставитьКоординатуYToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItemY
            // 
            resources.ApplyResources(this.копироватьToolStripMenuItemY, "копироватьToolStripMenuItemY");
            this.копироватьToolStripMenuItemY.Name = "копироватьToolStripMenuItemY";
            this.копироватьToolStripMenuItemY.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вставитьToolStripMenuItemY
            // 
            resources.ApplyResources(this.вставитьToolStripMenuItemY, "вставитьToolStripMenuItemY");
            this.вставитьToolStripMenuItemY.Name = "вставитьToolStripMenuItemY";
            this.вставитьToolStripMenuItemY.Click += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // label02
            // 
            resources.ApplyResources(this.label02, "label02");
            this.label02.Name = "label02";
            // 
            // label01
            // 
            resources.ApplyResources(this.label01, "label01");
            this.label01.Name = "label01";
            // 
            // label05
            // 
            resources.ApplyResources(this.label05, "label05");
            this.label05.Name = "label05";
            // 
            // label04
            // 
            resources.ApplyResources(this.label04, "label04");
            this.label04.Name = "label04";
            // 
            // numericUpDown_pointer_offset_X
            // 
            resources.ApplyResources(this.numericUpDown_pointer_offset_X, "numericUpDown_pointer_offset_X");
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
            this.numericUpDown_pointer_offset_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_offset_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_offset_KeyDown);
            this.numericUpDown_pointer_offset_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_pointer_offset_X_MouseDoubleClick);
            // 
            // numericUpDown_pointer_offset_Y
            // 
            resources.ApplyResources(this.numericUpDown_pointer_offset_Y, "numericUpDown_pointer_offset_Y");
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
            this.numericUpDown_pointer_offset_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_offset_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_offset_KeyDown);
            this.numericUpDown_pointer_offset_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_pointer_offset_Y_MouseDoubleClick);
            // 
            // label03
            // 
            resources.ApplyResources(this.label03, "label03");
            this.label03.Name = "label03";
            // 
            // label07
            // 
            resources.ApplyResources(this.label07, "label07");
            this.label07.Name = "label07";
            // 
            // label06
            // 
            resources.ApplyResources(this.label06, "label06");
            this.label06.Name = "label06";
            // 
            // numericUpDown_pointer_startAngle
            // 
            resources.ApplyResources(this.numericUpDown_pointer_startAngle, "numericUpDown_pointer_startAngle");
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
            this.numericUpDown_pointer_startAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_pointer_endAngle
            // 
            resources.ApplyResources(this.numericUpDown_pointer_endAngle, "numericUpDown_pointer_endAngle");
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
            this.numericUpDown_pointer_endAngle.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown_pointer_endAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBox_pointer_imageCentr
            // 
            this.comboBox_pointer_imageCentr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_pointer_imageCentr.DropDownWidth = 135;
            this.comboBox_pointer_imageCentr.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_pointer_imageCentr, "comboBox_pointer_imageCentr");
            this.comboBox_pointer_imageCentr.Name = "comboBox_pointer_imageCentr";
            this.comboBox_pointer_imageCentr.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_pointer_imageCentr.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_pointer_imageCentr.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_pointer_imageCentr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_pointer_imageCentr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_pointer_centr_X
            // 
            this.numericUpDown_pointer_centr_X.ContextMenuStrip = this.contextMenuStrip_X;
            resources.ApplyResources(this.numericUpDown_pointer_centr_X, "numericUpDown_pointer_centr_X");
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
            this.numericUpDown_pointer_centr_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_centr_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_centr_KeyDown);
            this.numericUpDown_pointer_centr_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_pointer_centr_Y
            // 
            this.numericUpDown_pointer_centr_Y.ContextMenuStrip = this.contextMenuStrip_Y;
            resources.ApplyResources(this.numericUpDown_pointer_centr_Y, "numericUpDown_pointer_centr_Y");
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
            this.numericUpDown_pointer_centr_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_centr_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_centr_KeyDown);
            this.numericUpDown_pointer_centr_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // label09
            // 
            resources.ApplyResources(this.label09, "label09");
            this.label09.Name = "label09";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label08
            // 
            resources.ApplyResources(this.label08, "label08");
            this.label08.Name = "label08";
            // 
            // comboBox_pointer_imageBackground
            // 
            this.comboBox_pointer_imageBackground.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_pointer_imageBackground.DropDownWidth = 135;
            this.comboBox_pointer_imageBackground.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_pointer_imageBackground, "comboBox_pointer_imageBackground");
            this.comboBox_pointer_imageBackground.Name = "comboBox_pointer_imageBackground";
            this.comboBox_pointer_imageBackground.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_pointer_imageBackground.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_pointer_imageBackground.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_pointer_imageBackground.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_pointer_imageBackground.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_pointer_background_X
            // 
            this.numericUpDown_pointer_background_X.ContextMenuStrip = this.contextMenuStrip_X;
            resources.ApplyResources(this.numericUpDown_pointer_background_X, "numericUpDown_pointer_background_X");
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
            this.numericUpDown_pointer_background_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_background_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_background_KeyDown);
            this.numericUpDown_pointer_background_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_pointer_background_Y
            // 
            this.numericUpDown_pointer_background_Y.ContextMenuStrip = this.contextMenuStrip_Y;
            resources.ApplyResources(this.numericUpDown_pointer_background_Y, "numericUpDown_pointer_background_Y");
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
            this.numericUpDown_pointer_background_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_pointer_background_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_pointer_background_KeyDown);
            this.numericUpDown_pointer_background_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // UCtrl_Pointer_Opt
            // 
            resources.ApplyResources(this, "$this");
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_X)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pointer_Y)).EndInit();
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
