namespace ControlLibrary
{
    partial class UCtrl_Text_Circle_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Text_Circle_Opt));
            this.comboBox_image = new System.Windows.Forms.ComboBox();
            this.label01 = new System.Windows.Forms.Label();
            this.numericUpDown_centr_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_centr_Y = new System.Windows.Forms.NumericUpDown();
            this.label02 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.numericUpDown_spacing = new System.Windows.Forms.NumericUpDown();
            this.comboBox_imageError = new System.Windows.Forms.ComboBox();
            this.label03 = new System.Windows.Forms.Label();
            this.label_imageError = new System.Windows.Forms.Label();
            this.comboBox_unit = new System.Windows.Forms.ComboBox();
            this.label_unit = new System.Windows.Forms.Label();
            this.comboBox_imageDecimalPoint = new System.Windows.Forms.ComboBox();
            this.label_imageDecimalPoint = new System.Windows.Forms.Label();
            this.comboBox_h_alignment = new System.Windows.Forms.ComboBox();
            this.label09 = new System.Windows.Forms.Label();
            this.comboBox_unit_imperial = new System.Windows.Forms.ComboBox();
            this.label_unit_miles = new System.Windows.Forms.Label();
            this.comboBox_v_alignment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_reverse_direction = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox_addZero = new System.Windows.Forms.CheckBox();
            this.numericUpDown_angle = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_radius = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_unit_in_alignment = new System.Windows.Forms.CheckBox();
            this.label_minus_image = new System.Windows.Forms.Label();
            this.context_WidgetProperty = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.копироватьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox_separator = new System.Windows.Forms.ComboBox();
            this.label_separator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_centr_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_centr_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spacing)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_angle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_radius)).BeginInit();
            this.context_WidgetProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_image
            // 
            resources.ApplyResources(this.comboBox_image, "comboBox_image");
            this.comboBox_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_image.DropDownWidth = 135;
            this.comboBox_image.FormattingEnabled = true;
            this.comboBox_image.Name = "comboBox_image";
            this.comboBox_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label01
            // 
            resources.ApplyResources(this.label01, "label01");
            this.label01.Name = "label01";
            // 
            // numericUpDown_centr_X
            // 
            resources.ApplyResources(this.numericUpDown_centr_X, "numericUpDown_centr_X");
            this.numericUpDown_centr_X.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_centr_X.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_centr_X.Name = "numericUpDown_centr_X";
            this.numericUpDown_centr_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_centr_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_image_KeyDown);
            this.numericUpDown_centr_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_centr_Y
            // 
            resources.ApplyResources(this.numericUpDown_centr_Y, "numericUpDown_centr_Y");
            this.numericUpDown_centr_Y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_centr_Y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_centr_Y.Name = "numericUpDown_centr_Y";
            this.numericUpDown_centr_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_centr_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_image_KeyDown);
            this.numericUpDown_centr_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // label02
            // 
            resources.ApplyResources(this.label02, "label02");
            this.label02.Name = "label02";
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
            // numericUpDown_spacing
            // 
            resources.ApplyResources(this.numericUpDown_spacing, "numericUpDown_spacing");
            this.numericUpDown_spacing.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_spacing.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_spacing.Name = "numericUpDown_spacing";
            this.numericUpDown_spacing.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // comboBox_imageError
            // 
            resources.ApplyResources(this.comboBox_imageError, "comboBox_imageError");
            this.comboBox_imageError.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_imageError.DropDownWidth = 135;
            this.comboBox_imageError.FormattingEnabled = true;
            this.comboBox_imageError.Name = "comboBox_imageError";
            this.comboBox_imageError.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_imageError.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_imageError.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_imageError.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_imageError.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label03
            // 
            resources.ApplyResources(this.label03, "label03");
            this.label03.Name = "label03";
            // 
            // label_imageError
            // 
            resources.ApplyResources(this.label_imageError, "label_imageError");
            this.label_imageError.Name = "label_imageError";
            // 
            // comboBox_unit
            // 
            resources.ApplyResources(this.comboBox_unit, "comboBox_unit");
            this.comboBox_unit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_unit.DropDownWidth = 135;
            this.comboBox_unit.FormattingEnabled = true;
            this.comboBox_unit.Name = "comboBox_unit";
            this.comboBox_unit.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_unit.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_unit.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_unit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_unit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label_unit
            // 
            resources.ApplyResources(this.label_unit, "label_unit");
            this.label_unit.Name = "label_unit";
            // 
            // comboBox_imageDecimalPoint
            // 
            resources.ApplyResources(this.comboBox_imageDecimalPoint, "comboBox_imageDecimalPoint");
            this.comboBox_imageDecimalPoint.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_imageDecimalPoint.DropDownWidth = 135;
            this.comboBox_imageDecimalPoint.FormattingEnabled = true;
            this.comboBox_imageDecimalPoint.Name = "comboBox_imageDecimalPoint";
            this.comboBox_imageDecimalPoint.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_imageDecimalPoint.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_imageDecimalPoint.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_imageDecimalPoint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_imageDecimalPoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label_imageDecimalPoint
            // 
            resources.ApplyResources(this.label_imageDecimalPoint, "label_imageDecimalPoint");
            this.label_imageDecimalPoint.Name = "label_imageDecimalPoint";
            // 
            // comboBox_h_alignment
            // 
            resources.ApplyResources(this.comboBox_h_alignment, "comboBox_h_alignment");
            this.comboBox_h_alignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_h_alignment.FormattingEnabled = true;
            this.comboBox_h_alignment.Items.AddRange(new object[] {
            resources.GetString("comboBox_h_alignment.Items"),
            resources.GetString("comboBox_h_alignment.Items1"),
            resources.GetString("comboBox_h_alignment.Items2")});
            this.comboBox_h_alignment.Name = "comboBox_h_alignment";
            this.comboBox_h_alignment.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label09
            // 
            resources.ApplyResources(this.label09, "label09");
            this.label09.Name = "label09";
            // 
            // comboBox_unit_imperial
            // 
            resources.ApplyResources(this.comboBox_unit_imperial, "comboBox_unit_imperial");
            this.comboBox_unit_imperial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_unit_imperial.DropDownWidth = 135;
            this.comboBox_unit_imperial.FormattingEnabled = true;
            this.comboBox_unit_imperial.Name = "comboBox_unit_imperial";
            this.comboBox_unit_imperial.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_unit_imperial.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_unit_imperial.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_unit_imperial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_unit_imperial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label_unit_miles
            // 
            resources.ApplyResources(this.label_unit_miles, "label_unit_miles");
            this.label_unit_miles.Name = "label_unit_miles";
            // 
            // comboBox_v_alignment
            // 
            resources.ApplyResources(this.comboBox_v_alignment, "comboBox_v_alignment");
            this.comboBox_v_alignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_v_alignment.FormattingEnabled = true;
            this.comboBox_v_alignment.Items.AddRange(new object[] {
            resources.GetString("comboBox_v_alignment.Items"),
            resources.GetString("comboBox_v_alignment.Items1"),
            resources.GetString("comboBox_v_alignment.Items2")});
            this.comboBox_v_alignment.Name = "comboBox_v_alignment";
            this.comboBox_v_alignment.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBox_reverse_direction
            // 
            resources.ApplyResources(this.checkBox_reverse_direction, "checkBox_reverse_direction");
            this.checkBox_reverse_direction.Name = "checkBox_reverse_direction";
            this.checkBox_reverse_direction.UseVisualStyleBackColor = true;
            this.checkBox_reverse_direction.Click += new System.EventHandler(this.checkBox_Click);
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
            // checkBox_addZero
            // 
            resources.ApplyResources(this.checkBox_addZero, "checkBox_addZero");
            this.checkBox_addZero.Name = "checkBox_addZero";
            this.checkBox_addZero.UseVisualStyleBackColor = true;
            this.checkBox_addZero.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // numericUpDown_angle
            // 
            resources.ApplyResources(this.numericUpDown_angle, "numericUpDown_angle");
            this.numericUpDown_angle.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_angle.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_angle.Name = "numericUpDown_angle";
            this.numericUpDown_angle.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDown_radius
            // 
            resources.ApplyResources(this.numericUpDown_radius, "numericUpDown_radius");
            this.numericUpDown_radius.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_radius.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_radius.Name = "numericUpDown_radius";
            this.numericUpDown_radius.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_radius.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // checkBox_unit_in_alignment
            // 
            resources.ApplyResources(this.checkBox_unit_in_alignment, "checkBox_unit_in_alignment");
            this.checkBox_unit_in_alignment.Name = "checkBox_unit_in_alignment";
            this.checkBox_unit_in_alignment.UseVisualStyleBackColor = true;
            this.checkBox_unit_in_alignment.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // label_minus_image
            // 
            resources.ApplyResources(this.label_minus_image, "label_minus_image");
            this.label_minus_image.Name = "label_minus_image";
            // 
            // context_WidgetProperty
            // 
            resources.ApplyResources(this.context_WidgetProperty, "context_WidgetProperty");
            this.context_WidgetProperty.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.копироватьСвойстваToolStripMenuItem,
            this.вставитьСвойстваToolStripMenuItem});
            this.context_WidgetProperty.Name = "context_WidgetProperty";
            this.context_WidgetProperty.Opening += new System.ComponentModel.CancelEventHandler(this.context_WidgetProperty_Opening);
            // 
            // копироватьСвойстваToolStripMenuItem
            // 
            resources.ApplyResources(this.копироватьСвойстваToolStripMenuItem, "копироватьСвойстваToolStripMenuItem");
            this.копироватьСвойстваToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.copy_prop;
            this.копироватьСвойстваToolStripMenuItem.Name = "копироватьСвойстваToolStripMenuItem";
            this.копироватьСвойстваToolStripMenuItem.Click += new System.EventHandler(this.копироватьСвойстваToolStripMenuItem_Click);
            // 
            // вставитьСвойстваToolStripMenuItem
            // 
            resources.ApplyResources(this.вставитьСвойстваToolStripMenuItem, "вставитьСвойстваToolStripMenuItem");
            this.вставитьСвойстваToolStripMenuItem.Image = global::ControlLibrary.Properties.Resources.paste_prop;
            this.вставитьСвойстваToolStripMenuItem.Name = "вставитьСвойстваToolStripMenuItem";
            this.вставитьСвойстваToolStripMenuItem.Click += new System.EventHandler(this.вставитьСвойстваToolStripMenuItem_Click);
            // 
            // comboBox_separator
            // 
            resources.ApplyResources(this.comboBox_separator, "comboBox_separator");
            this.comboBox_separator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_separator.DropDownWidth = 135;
            this.comboBox_separator.FormattingEnabled = true;
            this.comboBox_separator.Name = "comboBox_separator";
            this.comboBox_separator.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_separator.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_separator.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_separator.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_separator.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label_separator
            // 
            resources.ApplyResources(this.label_separator, "label_separator");
            this.label_separator.Name = "label_separator";
            // 
            // UCtrl_Text_Circle_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.context_WidgetProperty;
            this.Controls.Add(this.comboBox_separator);
            this.Controls.Add(this.label_separator);
            this.Controls.Add(this.label_minus_image);
            this.Controls.Add(this.checkBox_unit_in_alignment);
            this.Controls.Add(this.numericUpDown_radius);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDown_angle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_addZero);
            this.Controls.Add(this.checkBox_reverse_direction);
            this.Controls.Add(this.comboBox_v_alignment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_unit_imperial);
            this.Controls.Add(this.label_unit_miles);
            this.Controls.Add(this.comboBox_imageDecimalPoint);
            this.Controls.Add(this.label_imageDecimalPoint);
            this.Controls.Add(this.comboBox_h_alignment);
            this.Controls.Add(this.label09);
            this.Controls.Add(this.numericUpDown_spacing);
            this.Controls.Add(this.comboBox_imageError);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label_imageError);
            this.Controls.Add(this.comboBox_unit);
            this.Controls.Add(this.label_unit);
            this.Controls.Add(this.numericUpDown_centr_X);
            this.Controls.Add(this.numericUpDown_centr_Y);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.comboBox_image);
            this.Controls.Add(this.label01);
            this.Name = "UCtrl_Text_Circle_Opt";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_centr_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_centr_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spacing)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_angle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_radius)).EndInit();
            this.context_WidgetProperty.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_image;
        private System.Windows.Forms.Label label01;
        public System.Windows.Forms.NumericUpDown numericUpDown_centr_X;
        public System.Windows.Forms.NumericUpDown numericUpDown_centr_Y;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label04;
        public System.Windows.Forms.NumericUpDown numericUpDown_spacing;
        protected System.Windows.Forms.ComboBox comboBox_imageError;
        private System.Windows.Forms.Label label03;
        protected System.Windows.Forms.Label label_imageError;
        protected System.Windows.Forms.ComboBox comboBox_unit;
        protected System.Windows.Forms.Label label_unit;
        protected System.Windows.Forms.ComboBox comboBox_imageDecimalPoint;
        protected System.Windows.Forms.Label label_imageDecimalPoint;
        private System.Windows.Forms.ComboBox comboBox_h_alignment;
        private System.Windows.Forms.Label label09;
        protected System.Windows.Forms.ComboBox comboBox_unit_imperial;
        protected System.Windows.Forms.Label label_unit_miles;
        private System.Windows.Forms.ComboBox comboBox_v_alignment;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox checkBox_reverse_direction;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        public System.Windows.Forms.CheckBox checkBox_addZero;
        public System.Windows.Forms.NumericUpDown numericUpDown_angle;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDown_radius;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.CheckBox checkBox_unit_in_alignment;
        protected System.Windows.Forms.Label label_minus_image;
        private System.Windows.Forms.ContextMenuStrip context_WidgetProperty;
        private System.Windows.Forms.ToolStripMenuItem копироватьСвойстваToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьСвойстваToolStripMenuItem;
        protected System.Windows.Forms.ComboBox comboBox_separator;
        protected System.Windows.Forms.Label label_separator;
    }
}
