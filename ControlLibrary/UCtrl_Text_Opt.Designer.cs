
namespace ControlLibrary
{
    partial class UCtrl_Text_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Text_Opt));
            this.comboBox_image = new System.Windows.Forms.ComboBox();
            this.comboBox_icon = new System.Windows.Forms.ComboBox();
            this.numericUpDown_imageX = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_imageY = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_iconX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_iconY = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.label1083 = new System.Windows.Forms.Label();
            this.label1084 = new System.Windows.Forms.Label();
            this.label1085 = new System.Windows.Forms.Label();
            this.label1086 = new System.Windows.Forms.Label();
            this.comboBox_unit = new System.Windows.Forms.ComboBox();
            this.comboBox_alignment = new System.Windows.Forms.ComboBox();
            this.label_unit = new System.Windows.Forms.Label();
            this.label09 = new System.Windows.Forms.Label();
            this.comboBox_unit_miles = new System.Windows.Forms.ComboBox();
            this.numericUpDown_spacing = new System.Windows.Forms.NumericUpDown();
            this.checkBox_addZero = new System.Windows.Forms.CheckBox();
            this.comboBox_imageError = new System.Windows.Forms.ComboBox();
            this.comboBox_imageDecimalPoint = new System.Windows.Forms.ComboBox();
            this.checkBox_follow = new System.Windows.Forms.CheckBox();
            this.label03 = new System.Windows.Forms.Label();
            this.label_imageError = new System.Windows.Forms.Label();
            this.label_imageDecimalPoint = new System.Windows.Forms.Label();
            this.label_unit_miles = new System.Windows.Forms.Label();
            this.numericUpDown_angle = new System.Windows.Forms.NumericUpDown();
            this.label_angle = new System.Windows.Forms.Label();
            this.context_WidgetProperty = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.копироватьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_Alpha = new System.Windows.Forms.NumericUpDown();
            this.label_alpha = new System.Windows.Forms.Label();
            this.numericUpDown_iconAlpha = new System.Windows.Forms.NumericUpDown();
            this.label_icon_alpha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_angle)).BeginInit();
            this.context_WidgetProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconAlpha)).BeginInit();
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
            // comboBox_icon
            // 
            resources.ApplyResources(this.comboBox_icon, "comboBox_icon");
            this.comboBox_icon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_icon.DropDownWidth = 135;
            this.comboBox_icon.FormattingEnabled = true;
            this.comboBox_icon.Name = "comboBox_icon";
            this.comboBox_icon.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_icon.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_icon.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_icon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_icon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_imageX
            // 
            resources.ApplyResources(this.numericUpDown_imageX, "numericUpDown_imageX");
            this.numericUpDown_imageX.ContextMenuStrip = this.contextMenuStrip_X;
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
            this.numericUpDown_imageX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_imageX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_image_KeyDown);
            this.numericUpDown_imageX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            // numericUpDown_imageY
            // 
            resources.ApplyResources(this.numericUpDown_imageY, "numericUpDown_imageY");
            this.numericUpDown_imageY.ContextMenuStrip = this.contextMenuStrip_Y;
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
            this.numericUpDown_imageY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_imageY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_image_KeyDown);
            this.numericUpDown_imageY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // numericUpDown_iconX
            // 
            resources.ApplyResources(this.numericUpDown_iconX, "numericUpDown_iconX");
            this.numericUpDown_iconX.ContextMenuStrip = this.contextMenuStrip_X;
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
            this.numericUpDown_iconX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_iconX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_icon_KeyDown);
            this.numericUpDown_iconX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_iconY
            // 
            resources.ApplyResources(this.numericUpDown_iconY, "numericUpDown_iconY");
            this.numericUpDown_iconY.ContextMenuStrip = this.contextMenuStrip_Y;
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
            this.numericUpDown_iconY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_iconY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_icon_KeyDown);
            this.numericUpDown_iconY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // label01
            // 
            resources.ApplyResources(this.label01, "label01");
            this.label01.Name = "label01";
            // 
            // label02
            // 
            resources.ApplyResources(this.label02, "label02");
            this.label02.Name = "label02";
            // 
            // label04
            // 
            resources.ApplyResources(this.label04, "label04");
            this.label04.Name = "label04";
            // 
            // label05
            // 
            resources.ApplyResources(this.label05, "label05");
            this.label05.Name = "label05";
            // 
            // label1083
            // 
            resources.ApplyResources(this.label1083, "label1083");
            this.label1083.Name = "label1083";
            // 
            // label1084
            // 
            resources.ApplyResources(this.label1084, "label1084");
            this.label1084.Name = "label1084";
            // 
            // label1085
            // 
            resources.ApplyResources(this.label1085, "label1085");
            this.label1085.Name = "label1085";
            // 
            // label1086
            // 
            resources.ApplyResources(this.label1086, "label1086");
            this.label1086.Name = "label1086";
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
            // comboBox_alignment
            // 
            resources.ApplyResources(this.comboBox_alignment, "comboBox_alignment");
            this.comboBox_alignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_alignment.FormattingEnabled = true;
            this.comboBox_alignment.Items.AddRange(new object[] {
            resources.GetString("comboBox_alignment.Items"),
            resources.GetString("comboBox_alignment.Items1"),
            resources.GetString("comboBox_alignment.Items2")});
            this.comboBox_alignment.Name = "comboBox_alignment";
            this.comboBox_alignment.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_unit
            // 
            resources.ApplyResources(this.label_unit, "label_unit");
            this.label_unit.Name = "label_unit";
            // 
            // label09
            // 
            resources.ApplyResources(this.label09, "label09");
            this.label09.Name = "label09";
            // 
            // comboBox_unit_miles
            // 
            resources.ApplyResources(this.comboBox_unit_miles, "comboBox_unit_miles");
            this.comboBox_unit_miles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_unit_miles.DropDownWidth = 135;
            this.comboBox_unit_miles.FormattingEnabled = true;
            this.comboBox_unit_miles.Name = "comboBox_unit_miles";
            this.comboBox_unit_miles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_unit_miles.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_unit_miles.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_unit_miles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_unit_miles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
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
            // checkBox_addZero
            // 
            resources.ApplyResources(this.checkBox_addZero, "checkBox_addZero");
            this.checkBox_addZero.Name = "checkBox_addZero";
            this.checkBox_addZero.UseVisualStyleBackColor = true;
            this.checkBox_addZero.Click += new System.EventHandler(this.checkBox_Click);
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
            // checkBox_follow
            // 
            resources.ApplyResources(this.checkBox_follow, "checkBox_follow");
            this.checkBox_follow.Name = "checkBox_follow";
            this.checkBox_follow.UseVisualStyleBackColor = true;
            this.checkBox_follow.CheckedChanged += new System.EventHandler(this.checkBox_follow_CheckedChanged);
            this.checkBox_follow.Click += new System.EventHandler(this.checkBox_Click);
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
            // label_imageDecimalPoint
            // 
            resources.ApplyResources(this.label_imageDecimalPoint, "label_imageDecimalPoint");
            this.label_imageDecimalPoint.Name = "label_imageDecimalPoint";
            // 
            // label_unit_miles
            // 
            resources.ApplyResources(this.label_unit_miles, "label_unit_miles");
            this.label_unit_miles.Name = "label_unit_miles";
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
            // label_angle
            // 
            resources.ApplyResources(this.label_angle, "label_angle");
            this.label_angle.Name = "label_angle";
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
            resources.ApplyResources(this.label_alpha, "label_alpha");
            this.label_alpha.BackColor = System.Drawing.Color.Transparent;
            this.label_alpha.Name = "label_alpha";
            // 
            // numericUpDown_iconAlpha
            // 
            resources.ApplyResources(this.numericUpDown_iconAlpha, "numericUpDown_iconAlpha");
            this.numericUpDown_iconAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_iconAlpha.Name = "numericUpDown_iconAlpha";
            this.numericUpDown_iconAlpha.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_iconAlpha.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label_icon_alpha
            // 
            resources.ApplyResources(this.label_icon_alpha, "label_icon_alpha");
            this.label_icon_alpha.BackColor = System.Drawing.Color.Transparent;
            this.label_icon_alpha.Name = "label_icon_alpha";
            // 
            // UCtrl_Text_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.context_WidgetProperty;
            this.Controls.Add(this.numericUpDown_iconAlpha);
            this.Controls.Add(this.numericUpDown_Alpha);
            this.Controls.Add(this.label_alpha);
            this.Controls.Add(this.numericUpDown_angle);
            this.Controls.Add(this.label_angle);
            this.Controls.Add(this.comboBox_unit_miles);
            this.Controls.Add(this.numericUpDown_spacing);
            this.Controls.Add(this.checkBox_addZero);
            this.Controls.Add(this.comboBox_imageError);
            this.Controls.Add(this.comboBox_imageDecimalPoint);
            this.Controls.Add(this.checkBox_follow);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label_imageError);
            this.Controls.Add(this.label_imageDecimalPoint);
            this.Controls.Add(this.label_unit_miles);
            this.Controls.Add(this.comboBox_unit);
            this.Controls.Add(this.comboBox_alignment);
            this.Controls.Add(this.label_unit);
            this.Controls.Add(this.label09);
            this.Controls.Add(this.comboBox_image);
            this.Controls.Add(this.comboBox_icon);
            this.Controls.Add(this.numericUpDown_imageX);
            this.Controls.Add(this.numericUpDown_imageY);
            this.Controls.Add(this.numericUpDown_iconX);
            this.Controls.Add(this.numericUpDown_iconY);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.label1083);
            this.Controls.Add(this.label1084);
            this.Controls.Add(this.label1085);
            this.Controls.Add(this.label1086);
            this.Controls.Add(this.label_icon_alpha);
            this.Name = "UCtrl_Text_Opt";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_angle)).EndInit();
            this.context_WidgetProperty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_image;
        private System.Windows.Forms.ComboBox comboBox_icon;
        private System.Windows.Forms.Label label01;
        protected System.Windows.Forms.Label label02;
        protected System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label1083;
        protected System.Windows.Forms.Label label1084;
        protected System.Windows.Forms.Label label1085;
        private System.Windows.Forms.Label label1086;
        protected System.Windows.Forms.ComboBox comboBox_unit;
        private System.Windows.Forms.ComboBox comboBox_alignment;
        protected System.Windows.Forms.Label label_unit;
        private System.Windows.Forms.Label label09;
        protected System.Windows.Forms.ComboBox comboBox_unit_miles;
        public System.Windows.Forms.CheckBox checkBox_addZero;
        protected System.Windows.Forms.ComboBox comboBox_imageError;
        protected System.Windows.Forms.ComboBox comboBox_imageDecimalPoint;
        public System.Windows.Forms.CheckBox checkBox_follow;
        private System.Windows.Forms.Label label03;
        protected System.Windows.Forms.Label label_imageError;
        protected System.Windows.Forms.Label label_imageDecimalPoint;
        protected System.Windows.Forms.Label label_unit_miles;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageX;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageY;
        public System.Windows.Forms.NumericUpDown numericUpDown_iconX;
        public System.Windows.Forms.NumericUpDown numericUpDown_iconY;
        public System.Windows.Forms.NumericUpDown numericUpDown_spacing;
        public System.Windows.Forms.NumericUpDown numericUpDown_angle;
        private System.Windows.Forms.Label label_angle;
        private System.Windows.Forms.ContextMenuStrip context_WidgetProperty;
        private System.Windows.Forms.ToolStripMenuItem копироватьСвойстваToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьСвойстваToolStripMenuItem;
        public System.Windows.Forms.NumericUpDown numericUpDown_Alpha;
        private System.Windows.Forms.Label label_alpha;
        public System.Windows.Forms.NumericUpDown numericUpDown_iconAlpha;
        private System.Windows.Forms.Label label_icon_alpha;
    }
}
