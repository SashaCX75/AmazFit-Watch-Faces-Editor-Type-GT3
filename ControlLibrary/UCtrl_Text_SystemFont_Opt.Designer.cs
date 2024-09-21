
namespace ControlLibrary
{
    partial class UCtrl_Text_SystemFont_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Text_SystemFont_Opt));
            this.numericUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Height = new System.Windows.Forms.NumericUpDown();
            this.label08 = new System.Windows.Forms.Label();
            this.label07 = new System.Windows.Forms.Label();
            this.numericUpDown_X = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_Y = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_Size = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Spacing = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.label06 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.comboBox_alignmentHorizontal = new System.Windows.Forms.ComboBox();
            this.label09 = new System.Windows.Forms.Label();
            this.comboBox_alignmentVertical = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Color = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_LineSpace = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_textStyle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_fonts = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_AddFont = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button_DelFont = new System.Windows.Forms.Button();
            this.checkBox_CentreHorizontally = new System.Windows.Forms.CheckBox();
            this.checkBox_CentreVertically = new System.Windows.Forms.CheckBox();
            this.checkBox_addZero = new System.Windows.Forms.CheckBox();
            this.checkBox_unit = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.context_WidgetProperty = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.копироватьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьСвойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_unit_string = new System.Windows.Forms.TextBox();
            this.label_unit_string = new System.Windows.Forms.Label();
            this.checkBox_inEnd = new System.Windows.Forms.CheckBox();
            this.textBox_DOW = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label_DOW = new System.Windows.Forms.Label();
            this.label_Month = new System.Windows.Forms.Label();
            this.comboBox_Color2 = new System.Windows.Forms.ComboBox();
            this.label_Color2 = new System.Windows.Forms.Label();
            this.checkBox_Color2 = new System.Windows.Forms.CheckBox();
            this.numericUpDown_Alpha = new System.Windows.Forms.NumericUpDown();
            this.label_alpha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Size)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Spacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineSpace)).BeginInit();
            this.context_WidgetProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_Width
            // 
            resources.ApplyResources(this.numericUpDown_Width, "numericUpDown_Width");
            this.numericUpDown_Width.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_Width.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_Width.Name = "numericUpDown_Width";
            this.numericUpDown_Width.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown_Width.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_Width.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_size_KeyDown);
            this.numericUpDown_Width.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_Width_MouseDoubleClick);
            // 
            // numericUpDown_Height
            // 
            resources.ApplyResources(this.numericUpDown_Height, "numericUpDown_Height");
            this.numericUpDown_Height.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_Height.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_Height.Name = "numericUpDown_Height";
            this.numericUpDown_Height.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numericUpDown_Height.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_Height.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_size_KeyDown);
            this.numericUpDown_Height.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_Height_MouseDoubleClick);
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
            // numericUpDown_X
            // 
            this.numericUpDown_X.ContextMenuStrip = this.contextMenuStrip_X;
            resources.ApplyResources(this.numericUpDown_X, "numericUpDown_X");
            this.numericUpDown_X.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_X.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_X.Name = "numericUpDown_X";
            this.numericUpDown_X.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_Pos_KeyDown);
            this.numericUpDown_X.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            // numericUpDown_Y
            // 
            this.numericUpDown_Y.ContextMenuStrip = this.contextMenuStrip_Y;
            resources.ApplyResources(this.numericUpDown_Y, "numericUpDown_Y");
            this.numericUpDown_Y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_Y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_Y.Name = "numericUpDown_Y";
            this.numericUpDown_Y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_Pos_KeyDown);
            this.numericUpDown_Y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // numericUpDown_Size
            // 
            resources.ApplyResources(this.numericUpDown_Size, "numericUpDown_Size");
            this.numericUpDown_Size.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Size.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Size.Name = "numericUpDown_Size";
            this.toolTip1.SetToolTip(this.numericUpDown_Size, resources.GetString("numericUpDown_Size.ToolTip"));
            this.numericUpDown_Size.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown_Size.ValueChanged += new System.EventHandler(this.numericUpDown_Size_ValueChanged);
            // 
            // numericUpDown_Spacing
            // 
            resources.ApplyResources(this.numericUpDown_Spacing, "numericUpDown_Spacing");
            this.numericUpDown_Spacing.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_Spacing.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_Spacing.Name = "numericUpDown_Spacing";
            this.numericUpDown_Spacing.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            // comboBox_alignmentHorizontal
            // 
            this.comboBox_alignmentHorizontal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_alignmentHorizontal.FormattingEnabled = true;
            this.comboBox_alignmentHorizontal.Items.AddRange(new object[] {
            resources.GetString("comboBox_alignmentHorizontal.Items"),
            resources.GetString("comboBox_alignmentHorizontal.Items1"),
            resources.GetString("comboBox_alignmentHorizontal.Items2")});
            resources.ApplyResources(this.comboBox_alignmentHorizontal, "comboBox_alignmentHorizontal");
            this.comboBox_alignmentHorizontal.Name = "comboBox_alignmentHorizontal";
            this.comboBox_alignmentHorizontal.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label09
            // 
            resources.ApplyResources(this.label09, "label09");
            this.label09.Name = "label09";
            // 
            // comboBox_alignmentVertical
            // 
            this.comboBox_alignmentVertical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_alignmentVertical.FormattingEnabled = true;
            this.comboBox_alignmentVertical.Items.AddRange(new object[] {
            resources.GetString("comboBox_alignmentVertical.Items"),
            resources.GetString("comboBox_alignmentVertical.Items1"),
            resources.GetString("comboBox_alignmentVertical.Items2")});
            resources.ApplyResources(this.comboBox_alignmentVertical, "comboBox_alignmentVertical");
            this.comboBox_alignmentVertical.Name = "comboBox_alignmentVertical";
            this.comboBox_alignmentVertical.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // comboBox_Color
            // 
            this.comboBox_Color.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_Color.DropDownHeight = 1;
            this.comboBox_Color.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_Color, "comboBox_Color");
            this.comboBox_Color.Name = "comboBox_Color";
            this.comboBox_Color.Click += new System.EventHandler(this.comboBox_Color_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDown_LineSpace
            // 
            resources.ApplyResources(this.numericUpDown_LineSpace, "numericUpDown_LineSpace");
            this.numericUpDown_LineSpace.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_LineSpace.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_LineSpace.Name = "numericUpDown_LineSpace";
            this.numericUpDown_LineSpace.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBox_textStyle
            // 
            this.comboBox_textStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_textStyle.FormattingEnabled = true;
            this.comboBox_textStyle.Items.AddRange(new object[] {
            resources.GetString("comboBox_textStyle.Items"),
            resources.GetString("comboBox_textStyle.Items1"),
            resources.GetString("comboBox_textStyle.Items2")});
            resources.ApplyResources(this.comboBox_textStyle, "comboBox_textStyle");
            this.comboBox_textStyle.Name = "comboBox_textStyle";
            this.comboBox_textStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // comboBox_fonts
            // 
            this.comboBox_fonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_fonts.DropDownWidth = 160;
            this.comboBox_fonts.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_fonts, "comboBox_fonts");
            this.comboBox_fonts.Name = "comboBox_fonts";
            this.comboBox_fonts.SelectedIndexChanged += new System.EventHandler(this.comboBox_fonts_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // button_AddFont
            // 
            resources.ApplyResources(this.button_AddFont, "button_AddFont");
            this.button_AddFont.Name = "button_AddFont";
            this.button_AddFont.UseVisualStyleBackColor = true;
            this.button_AddFont.Click += new System.EventHandler(this.button_AddFont_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // button_DelFont
            // 
            resources.ApplyResources(this.button_DelFont, "button_DelFont");
            this.button_DelFont.Name = "button_DelFont";
            this.button_DelFont.UseVisualStyleBackColor = true;
            this.button_DelFont.Click += new System.EventHandler(this.button_DelFont_Click);
            // 
            // checkBox_CentreHorizontally
            // 
            resources.ApplyResources(this.checkBox_CentreHorizontally, "checkBox_CentreHorizontally");
            this.checkBox_CentreHorizontally.Name = "checkBox_CentreHorizontally";
            this.checkBox_CentreHorizontally.UseVisualStyleBackColor = true;
            this.checkBox_CentreHorizontally.CheckedChanged += new System.EventHandler(this.checkBox_CentreHorizontally_CheckedChanged);
            // 
            // checkBox_CentreVertically
            // 
            resources.ApplyResources(this.checkBox_CentreVertically, "checkBox_CentreVertically");
            this.checkBox_CentreVertically.Name = "checkBox_CentreVertically";
            this.checkBox_CentreVertically.UseVisualStyleBackColor = true;
            this.checkBox_CentreVertically.CheckedChanged += new System.EventHandler(this.checkBox_CentreVertically_CheckedChanged);
            // 
            // checkBox_addZero
            // 
            resources.ApplyResources(this.checkBox_addZero, "checkBox_addZero");
            this.checkBox_addZero.Name = "checkBox_addZero";
            this.checkBox_addZero.UseVisualStyleBackColor = true;
            this.checkBox_addZero.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox_unit
            // 
            resources.ApplyResources(this.checkBox_unit, "checkBox_unit");
            this.checkBox_unit.Name = "checkBox_unit";
            this.checkBox_unit.ThreeState = true;
            this.checkBox_unit.UseVisualStyleBackColor = true;
            this.checkBox_unit.CheckStateChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
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
            // textBox_unit_string
            // 
            resources.ApplyResources(this.textBox_unit_string, "textBox_unit_string");
            this.textBox_unit_string.Name = "textBox_unit_string";
            this.textBox_unit_string.TextChanged += new System.EventHandler(this.textBox_unit_string_TextChanged);
            // 
            // label_unit_string
            // 
            resources.ApplyResources(this.label_unit_string, "label_unit_string");
            this.label_unit_string.Name = "label_unit_string";
            // 
            // checkBox_inEnd
            // 
            resources.ApplyResources(this.checkBox_inEnd, "checkBox_inEnd");
            this.checkBox_inEnd.Name = "checkBox_inEnd";
            this.checkBox_inEnd.UseVisualStyleBackColor = true;
            this.checkBox_inEnd.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // textBox_DOW
            // 
            resources.ApplyResources(this.textBox_DOW, "textBox_DOW");
            this.textBox_DOW.Name = "textBox_DOW";
            this.toolTip1.SetToolTip(this.textBox_DOW, resources.GetString("textBox_DOW.ToolTip"));
            this.textBox_DOW.TextChanged += new System.EventHandler(this.textBox_unit_string_TextChanged);
            // 
            // label_DOW
            // 
            resources.ApplyResources(this.label_DOW, "label_DOW");
            this.label_DOW.Name = "label_DOW";
            // 
            // label_Month
            // 
            resources.ApplyResources(this.label_Month, "label_Month");
            this.label_Month.Name = "label_Month";
            // 
            // comboBox_Color2
            // 
            this.comboBox_Color2.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_Color2.DropDownHeight = 1;
            resources.ApplyResources(this.comboBox_Color2, "comboBox_Color2");
            this.comboBox_Color2.FormattingEnabled = true;
            this.comboBox_Color2.Name = "comboBox_Color2";
            this.comboBox_Color2.Click += new System.EventHandler(this.comboBox_Color_Click);
            // 
            // label_Color2
            // 
            resources.ApplyResources(this.label_Color2, "label_Color2");
            this.label_Color2.Name = "label_Color2";
            // 
            // checkBox_Color2
            // 
            resources.ApplyResources(this.checkBox_Color2, "checkBox_Color2");
            this.checkBox_Color2.Name = "checkBox_Color2";
            this.checkBox_Color2.UseVisualStyleBackColor = true;
            this.checkBox_Color2.CheckedChanged += new System.EventHandler(this.checkBox_Color2_CheckedChanged);
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
            this.numericUpDown_Alpha.ValueChanged += new System.EventHandler(this.numericUpDown_Size_ValueChanged);
            // 
            // label_alpha
            // 
            this.label_alpha.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.label_alpha, "label_alpha");
            this.label_alpha.Name = "label_alpha";
            // 
            // UCtrl_Text_SystemFont_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.context_WidgetProperty;
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label09);
            this.Controls.Add(this.numericUpDown_Alpha);
            this.Controls.Add(this.label_alpha);
            this.Controls.Add(this.checkBox_Color2);
            this.Controls.Add(this.comboBox_Color2);
            this.Controls.Add(this.label_Color2);
            this.Controls.Add(this.label_Month);
            this.Controls.Add(this.label_DOW);
            this.Controls.Add(this.textBox_DOW);
            this.Controls.Add(this.checkBox_inEnd);
            this.Controls.Add(this.label_unit_string);
            this.Controls.Add(this.textBox_unit_string);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBox_unit);
            this.Controls.Add(this.checkBox_addZero);
            this.Controls.Add(this.checkBox_CentreVertically);
            this.Controls.Add(this.checkBox_CentreHorizontally);
            this.Controls.Add(this.button_DelFont);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_AddFont);
            this.Controls.Add(this.comboBox_fonts);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_textStyle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown_LineSpace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_Color);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_alignmentVertical);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_alignmentHorizontal);
            this.Controls.Add(this.numericUpDown_Width);
            this.Controls.Add(this.numericUpDown_Height);
            this.Controls.Add(this.label08);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.numericUpDown_X);
            this.Controls.Add(this.numericUpDown_Y);
            this.Controls.Add(this.numericUpDown_Size);
            this.Controls.Add(this.numericUpDown_Spacing);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label05);
            this.Name = "UCtrl_Text_SystemFont_Opt";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Size)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Spacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_LineSpace)).EndInit();
            this.context_WidgetProperty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Alpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NumericUpDown numericUpDown_Width;
        public System.Windows.Forms.NumericUpDown numericUpDown_Height;
        private System.Windows.Forms.Label label08;
        private System.Windows.Forms.Label label07;
        public System.Windows.Forms.NumericUpDown numericUpDown_X;
        public System.Windows.Forms.NumericUpDown numericUpDown_Y;
        public System.Windows.Forms.NumericUpDown numericUpDown_Size;
        public System.Windows.Forms.NumericUpDown numericUpDown_Spacing;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.ComboBox comboBox_alignmentHorizontal;
        private System.Windows.Forms.Label label09;
        private System.Windows.Forms.ComboBox comboBox_alignmentVertical;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Color;
        protected System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDown_LineSpace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_textStyle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_fonts;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_AddFont;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_DelFont;
        public System.Windows.Forms.CheckBox checkBox_CentreHorizontally;
        public System.Windows.Forms.CheckBox checkBox_CentreVertically;
        public System.Windows.Forms.CheckBox checkBox_addZero;
        private System.Windows.Forms.CheckBox checkBox_unit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip context_WidgetProperty;
        private System.Windows.Forms.ToolStripMenuItem копироватьСвойстваToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьСвойстваToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_unit_string;
        private System.Windows.Forms.Label label_unit_string;
        private System.Windows.Forms.CheckBox checkBox_inEnd;
        private System.Windows.Forms.TextBox textBox_DOW;
        private System.Windows.Forms.ToolTip toolTip1;
        protected System.Windows.Forms.Label label_DOW;
        protected System.Windows.Forms.Label label_Month;
        private System.Windows.Forms.ComboBox comboBox_Color2;
        protected System.Windows.Forms.Label label_Color2;
        public System.Windows.Forms.CheckBox checkBox_Color2;
        public System.Windows.Forms.NumericUpDown numericUpDown_Alpha;
        private System.Windows.Forms.Label label_alpha;
    }
}
