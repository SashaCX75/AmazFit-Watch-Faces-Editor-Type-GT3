
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
            this.checkBox_mirror = new System.Windows.Forms.CheckBox();
            this.checkBox_inversion = new System.Windows.Forms.CheckBox();
            this.label08 = new System.Windows.Forms.Label();
            this.comboBox_scaleLinear_lineCap = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
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
            resources.ApplyResources(this.comboBox_scaleLinear_image_pointer, "comboBox_scaleLinear_image_pointer");
            this.comboBox_scaleLinear_image_pointer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_scaleLinear_image_pointer.DropDownWidth = 135;
            this.comboBox_scaleLinear_image_pointer.FormattingEnabled = true;
            this.comboBox_scaleLinear_image_pointer.Name = "comboBox_scaleLinear_image_pointer";
            this.comboBox_scaleLinear_image_pointer.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_scaleLinear_image_pointer.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_scaleLinear_image_pointer.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_scaleLinear_image_pointer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_scaleLinear_image_pointer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label02
            // 
            resources.ApplyResources(this.label02, "label02");
            this.label02.Name = "label02";
            // 
            // numericUpDown_scaleLinearX
            // 
            resources.ApplyResources(this.numericUpDown_scaleLinearX, "numericUpDown_scaleLinearX");
            this.numericUpDown_scaleLinearX.ContextMenuStrip = this.contextMenuStrip_X;
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
            this.numericUpDown_scaleLinearX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleLinearX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_scaleLinear_KeyDown);
            this.numericUpDown_scaleLinearX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // contextMenuStrip_X
            // 
            resources.ApplyResources(this.contextMenuStrip_X, "contextMenuStrip_X");
            this.contextMenuStrip_X.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_X.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатуХToolStripMenuItem,
            this.копироватьToolStripMenuItemX,
            this.вставитьToolStripMenuItemX});
            this.contextMenuStrip_X.Name = "contextMenuStrip_X";
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
            // numericUpDown_scaleLinearY
            // 
            resources.ApplyResources(this.numericUpDown_scaleLinearY, "numericUpDown_scaleLinearY");
            this.numericUpDown_scaleLinearY.ContextMenuStrip = this.contextMenuStrip_Y;
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
            this.numericUpDown_scaleLinearY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleLinearY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_scaleLinear_KeyDown);
            this.numericUpDown_scaleLinearY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // contextMenuStrip_Y
            // 
            resources.ApplyResources(this.contextMenuStrip_Y, "contextMenuStrip_Y");
            this.contextMenuStrip_Y.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_Y.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьКоординатуYToolStripMenuItem,
            this.копироватьToolStripMenuItemY,
            this.вставитьToolStripMenuItemY});
            this.contextMenuStrip_Y.Name = "contextMenuStrip_X";
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
            // numericUpDown_scaleLinear_length
            // 
            resources.ApplyResources(this.numericUpDown_scaleLinear_length, "numericUpDown_scaleLinear_length");
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
            this.numericUpDown_scaleLinear_length.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_scaleLinear_length.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleLinear_length.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_scaleLinear_length_MouseDoubleClick);
            // 
            // numericUpDown_scaleLinear_width
            // 
            resources.ApplyResources(this.numericUpDown_scaleLinear_width, "numericUpDown_scaleLinear_width");
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
            this.numericUpDown_scaleLinear_width.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_scaleLinear_width.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label01
            // 
            resources.ApplyResources(this.label01, "label01");
            this.label01.Name = "label01";
            // 
            // label04
            // 
            resources.ApplyResources(this.label04, "label04");
            this.label04.Name = "label04";
            // 
            // label03
            // 
            resources.ApplyResources(this.label03, "label03");
            this.label03.Name = "label03";
            // 
            // label06
            // 
            resources.ApplyResources(this.label06, "label06");
            this.label06.Name = "label06";
            // 
            // label05
            // 
            resources.ApplyResources(this.label05, "label05");
            this.label05.Name = "label05";
            // 
            // comboBox_scaleLinear_color
            // 
            resources.ApplyResources(this.comboBox_scaleLinear_color, "comboBox_scaleLinear_color");
            this.comboBox_scaleLinear_color.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_scaleLinear_color.DropDownHeight = 1;
            this.comboBox_scaleLinear_color.FormattingEnabled = true;
            this.comboBox_scaleLinear_color.Name = "comboBox_scaleLinear_color";
            this.comboBox_scaleLinear_color.Click += new System.EventHandler(this.comboBox_scaleLinear_color_Click);
            // 
            // label07
            // 
            resources.ApplyResources(this.label07, "label07");
            this.label07.Name = "label07";
            // 
            // groupBox_Orientation
            // 
            resources.ApplyResources(this.groupBox_Orientation, "groupBox_Orientation");
            this.groupBox_Orientation.Controls.Add(this.radioButton_vertical);
            this.groupBox_Orientation.Controls.Add(this.radioButton_horizontal);
            this.groupBox_Orientation.Name = "groupBox_Orientation";
            this.groupBox_Orientation.TabStop = false;
            // 
            // radioButton_vertical
            // 
            resources.ApplyResources(this.radioButton_vertical, "radioButton_vertical");
            this.radioButton_vertical.Name = "radioButton_vertical";
            this.radioButton_vertical.UseVisualStyleBackColor = true;
            // 
            // radioButton_horizontal
            // 
            resources.ApplyResources(this.radioButton_horizontal, "radioButton_horizontal");
            this.radioButton_horizontal.Checked = true;
            this.radioButton_horizontal.Name = "radioButton_horizontal";
            this.radioButton_horizontal.TabStop = true;
            this.radioButton_horizontal.UseVisualStyleBackColor = true;
            this.radioButton_horizontal.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // checkBox_mirror
            // 
            resources.ApplyResources(this.checkBox_mirror, "checkBox_mirror");
            this.checkBox_mirror.Name = "checkBox_mirror";
            this.checkBox_mirror.UseVisualStyleBackColor = true;
            this.checkBox_mirror.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_mirror.Click += new System.EventHandler(this.checkBox_mirror_Click);
            // 
            // checkBox_inversion
            // 
            resources.ApplyResources(this.checkBox_inversion, "checkBox_inversion");
            this.checkBox_inversion.Name = "checkBox_inversion";
            this.checkBox_inversion.UseVisualStyleBackColor = true;
            this.checkBox_inversion.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // label08
            // 
            resources.ApplyResources(this.label08, "label08");
            this.label08.Name = "label08";
            // 
            // comboBox_scaleLinear_lineCap
            // 
            resources.ApplyResources(this.comboBox_scaleLinear_lineCap, "comboBox_scaleLinear_lineCap");
            this.comboBox_scaleLinear_lineCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_scaleLinear_lineCap.FormattingEnabled = true;
            this.comboBox_scaleLinear_lineCap.Items.AddRange(new object[] {
            resources.GetString("comboBox_scaleLinear_lineCap.Items"),
            resources.GetString("comboBox_scaleLinear_lineCap.Items1")});
            this.comboBox_scaleLinear_lineCap.Name = "comboBox_scaleLinear_lineCap";
            this.comboBox_scaleLinear_lineCap.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // UCtrl_Linear_Scale_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_scaleLinear_lineCap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label08);
            this.Controls.Add(this.checkBox_inversion);
            this.Controls.Add(this.checkBox_mirror);
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
        public System.Windows.Forms.CheckBox checkBox_mirror;
        public System.Windows.Forms.CheckBox checkBox_inversion;
        private System.Windows.Forms.ComboBox comboBox_scaleLinear_lineCap;
        private System.Windows.Forms.Label label2;
    }
}
