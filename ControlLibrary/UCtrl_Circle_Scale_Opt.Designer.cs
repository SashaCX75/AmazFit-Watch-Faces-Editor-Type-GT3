
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
            this.checkBox_mirror = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_scaleCircle_color = new System.Windows.Forms.ComboBox();
            this.comboBox_scaleCircle_lineCap = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.context_WidgetProperty = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.копироватьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_Alpha = new System.Windows.Forms.NumericUpDown();
            this.label_alpha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleX)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_startAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_endAngle)).BeginInit();
            this.context_WidgetProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_scaleCircleX
            // 
            this.numericUpDown_scaleCircleX.ContextMenuStrip = this.contextMenuStrip_X;
            resources.ApplyResources(this.numericUpDown_scaleCircleX, "numericUpDown_scaleCircleX");
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
            this.numericUpDown_scaleCircleX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleCircleX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_scaleCircle_KeyDown);
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
            // numericUpDown_scaleCircleY
            // 
            this.numericUpDown_scaleCircleY.ContextMenuStrip = this.contextMenuStrip_Y;
            resources.ApplyResources(this.numericUpDown_scaleCircleY, "numericUpDown_scaleCircleY");
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
            this.numericUpDown_scaleCircleY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleCircleY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_scaleCircle_KeyDown);
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
            // numericUpDown_scaleCircle_radius
            // 
            resources.ApplyResources(this.numericUpDown_scaleCircle_radius, "numericUpDown_scaleCircle_radius");
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
            this.numericUpDown_scaleCircle_radius.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_radius.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_scaleCircle_radius.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_scaleCircle_radius_MouseDoubleClick);
            // 
            // numericUpDown_scaleCircle_width
            // 
            resources.ApplyResources(this.numericUpDown_scaleCircle_width, "numericUpDown_scaleCircle_width");
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
            this.numericUpDown_scaleCircle_width.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_width.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            // numericUpDown_scaleCircle_startAngle
            // 
            resources.ApplyResources(this.numericUpDown_scaleCircle_startAngle, "numericUpDown_scaleCircle_startAngle");
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
            this.numericUpDown_scaleCircle_startAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown_scaleCircle_endAngle
            // 
            resources.ApplyResources(this.numericUpDown_scaleCircle_endAngle, "numericUpDown_scaleCircle_endAngle");
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
            this.numericUpDown_scaleCircle_endAngle.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown_scaleCircle_endAngle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label08
            // 
            resources.ApplyResources(this.label08, "label08");
            this.label08.Name = "label08";
            // 
            // label07
            // 
            resources.ApplyResources(this.label07, "label07");
            this.label07.Name = "label07";
            // 
            // label02
            // 
            resources.ApplyResources(this.label02, "label02");
            this.label02.Name = "label02";
            // 
            // checkBox_inversion
            // 
            resources.ApplyResources(this.checkBox_inversion, "checkBox_inversion");
            this.checkBox_inversion.Name = "checkBox_inversion";
            this.checkBox_inversion.UseVisualStyleBackColor = true;
            this.checkBox_inversion.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_mirror
            // 
            resources.ApplyResources(this.checkBox_mirror, "checkBox_mirror");
            this.checkBox_mirror.Name = "checkBox_mirror";
            this.checkBox_mirror.UseVisualStyleBackColor = true;
            this.checkBox_mirror.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBox_scaleCircle_color
            // 
            this.comboBox_scaleCircle_color.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_scaleCircle_color.DropDownHeight = 1;
            this.comboBox_scaleCircle_color.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_scaleCircle_color, "comboBox_scaleCircle_color");
            this.comboBox_scaleCircle_color.Name = "comboBox_scaleCircle_color";
            this.comboBox_scaleCircle_color.Click += new System.EventHandler(this.comboBox_scaleLinear_color_Click);
            // 
            // comboBox_scaleCircle_lineCap
            // 
            this.comboBox_scaleCircle_lineCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_scaleCircle_lineCap, "comboBox_scaleCircle_lineCap");
            this.comboBox_scaleCircle_lineCap.FormattingEnabled = true;
            this.comboBox_scaleCircle_lineCap.Items.AddRange(new object[] {
            resources.GetString("comboBox_scaleCircle_lineCap.Items"),
            resources.GetString("comboBox_scaleCircle_lineCap.Items1")});
            this.comboBox_scaleCircle_lineCap.Name = "comboBox_scaleCircle_lineCap";
            this.comboBox_scaleCircle_lineCap.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // context_WidgetProperty
            // 
            this.context_WidgetProperty.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.копироватьСвойстваToolStripMenuItem,
            this.вставитьСвойстваToolStripMenuItem});
            this.context_WidgetProperty.Name = "context_WidgetProperty";
            resources.ApplyResources(this.context_WidgetProperty, "context_WidgetProperty");
            this.context_WidgetProperty.Opening += new System.ComponentModel.CancelEventHandler(this.context_WidgetProperty_Opening);
            // 
            // копироватьСвойстваToolStripMenuItem
            // 
            this.копироватьСвойстваToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.copy_prop;
            this.копироватьСвойстваToolStripMenuItem.Name = "копироватьСвойстваToolStripMenuItem";
            resources.ApplyResources(this.копироватьСвойстваToolStripMenuItem, "копироватьСвойстваToolStripMenuItem");
            this.копироватьСвойстваToolStripMenuItem.Click += new System.EventHandler(this.копироватьСвойстваToolStripMenuItem_Click);
            // 
            // вставитьСвойстваToolStripMenuItem
            // 
            this.вставитьСвойстваToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.paste_prop;
            this.вставитьСвойстваToolStripMenuItem.Name = "вставитьСвойстваToolStripMenuItem";
            resources.ApplyResources(this.вставитьСвойстваToolStripMenuItem, "вставитьСвойстваToolStripMenuItem");
            this.вставитьСвойстваToolStripMenuItem.Click += new System.EventHandler(this.вставитьСвойстваToolStripMenuItem_Click);
            // 
            // numericUpDown_Alpha
            // 
            resources.ApplyResources(this.numericUpDown_Alpha, "numericUpDown_Alpha");
            this.numericUpDown_Alpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Alpha.Name = "numericUpDown_Alpha";
            this.numericUpDown_Alpha.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Alpha.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label_alpha
            // 
            this.label_alpha.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label_alpha, "label_alpha");
            this.label_alpha.Name = "label_alpha";
            // 
            // UCtrl_Circle_Scale_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.context_WidgetProperty;
            this.Controls.Add(this.numericUpDown_Alpha);
            this.Controls.Add(this.label_alpha);
            this.Controls.Add(this.comboBox_scaleCircle_lineCap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_scaleCircle_color);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.checkBox_inversion);
            this.Controls.Add(this.checkBox_mirror);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleX)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircleY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_startAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_scaleCircle_endAngle)).EndInit();
            this.context_WidgetProperty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).EndInit();
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
        public System.Windows.Forms.CheckBox checkBox_mirror;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_scaleCircle_color;
        private System.Windows.Forms.ComboBox comboBox_scaleCircle_lineCap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip context_WidgetProperty;
        private System.Windows.Forms.ToolStripMenuItem копироватьСвойстваToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьСвойстваToolStripMenuItem;
        public System.Windows.Forms.NumericUpDown numericUpDown_Alpha;
        private System.Windows.Forms.Label label_alpha;
    }
}
