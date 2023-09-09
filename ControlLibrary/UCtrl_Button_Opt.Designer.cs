namespace ControlLibrary
{
    partial class UCtrl_Button_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Button_Opt));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_buttons = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Button = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisibleButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.button_add = new System.Windows.Forms.Button();
            this.button_del = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown_width = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_height = new System.Windows.Forms.NumericUpDown();
            this.label06 = new System.Windows.Forms.Label();
            this.label07 = new System.Windows.Forms.Label();
            this.numericUpDown_buttonX = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_buttonY = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.label02 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.groupBox_image = new System.Windows.Forms.GroupBox();
            this.comboBox_press_image = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_normal_image = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_color = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_press_color = new System.Windows.Forms.ComboBox();
            this.numericUpDown_radius = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_normal_color = new System.Windows.Forms.ComboBox();
            this.numericUpDown_textSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_script = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_Text_color = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_buttons)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_buttonX)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_buttonY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            this.groupBox_image.SuspendLayout();
            this.groupBox_color.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_radius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textSize)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_buttons
            // 
            resources.ApplyResources(this.dataGridView_buttons, "dataGridView_buttons");
            this.dataGridView_buttons.AllowUserToAddRows = false;
            this.dataGridView_buttons.AllowUserToDeleteRows = false;
            this.dataGridView_buttons.AllowUserToResizeRows = false;
            this.dataGridView_buttons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_buttons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.Button,
            this.VisibleButton});
            this.dataGridView_buttons.MultiSelect = false;
            this.dataGridView_buttons.Name = "dataGridView_buttons";
            this.dataGridView_buttons.ReadOnly = true;
            this.dataGridView_buttons.RowHeadersVisible = false;
            this.dataGridView_buttons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_buttons.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_buttons_CellContentClick);
            this.dataGridView_buttons.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_bottons_RowEnter);
            this.dataGridView_buttons.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_buttons_KeyDown);
            this.dataGridView_buttons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_buttons_MouseDoubleClick);
            // 
            // Index
            // 
            this.Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Index.DefaultCellStyle = dataGridViewCellStyle1;
            this.Index.FillWeight = 13F;
            resources.ApplyResources(this.Index, "Index");
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Button
            // 
            this.Button.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight;
            this.Button.DefaultCellStyle = dataGridViewCellStyle2;
            this.Button.FillWeight = 120F;
            resources.ApplyResources(this.Button, "Button");
            this.Button.Name = "Button";
            this.Button.ReadOnly = true;
            this.Button.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // VisibleButton
            // 
            this.VisibleButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VisibleButton.FillWeight = 10F;
            resources.ApplyResources(this.VisibleButton, "VisibleButton");
            this.VisibleButton.Name = "VisibleButton";
            this.VisibleButton.ReadOnly = true;
            this.VisibleButton.TrueValue = "true";
            // 
            // button_add
            // 
            resources.ApplyResources(this.button_add, "button_add");
            this.button_add.Name = "button_add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_del
            // 
            resources.ApplyResources(this.button_del, "button_del");
            this.button_del.Name = "button_del";
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.button_del, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_add, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // numericUpDown_width
            // 
            resources.ApplyResources(this.numericUpDown_width, "numericUpDown_width");
            this.numericUpDown_width.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown_width.Name = "numericUpDown_width";
            this.numericUpDown_width.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_width.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_width.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_size_KeyDown);
            this.numericUpDown_width.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_length_MouseDoubleClick);
            // 
            // numericUpDown_height
            // 
            resources.ApplyResources(this.numericUpDown_height, "numericUpDown_height");
            this.numericUpDown_height.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown_height.Name = "numericUpDown_height";
            this.numericUpDown_height.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericUpDown_height.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_height.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_size_KeyDown);
            this.numericUpDown_height.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_width_MouseDoubleClick);
            // 
            // label06
            // 
            resources.ApplyResources(this.label06, "label06");
            this.label06.Name = "label06";
            // 
            // label07
            // 
            resources.ApplyResources(this.label07, "label07");
            this.label07.Name = "label07";
            // 
            // numericUpDown_buttonX
            // 
            resources.ApplyResources(this.numericUpDown_buttonX, "numericUpDown_buttonX");
            this.numericUpDown_buttonX.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_buttonX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_buttonX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_buttonX.Name = "numericUpDown_buttonX";
            this.numericUpDown_buttonX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_buttonX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_position_KeyDown);
            this.numericUpDown_buttonX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            // numericUpDown_buttonY
            // 
            resources.ApplyResources(this.numericUpDown_buttonY, "numericUpDown_buttonY");
            this.numericUpDown_buttonY.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_buttonY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_buttonY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_buttonY.Name = "numericUpDown_buttonY";
            this.numericUpDown_buttonY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_buttonY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_position_KeyDown);
            this.numericUpDown_buttonY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // groupBox_image
            // 
            resources.ApplyResources(this.groupBox_image, "groupBox_image");
            this.groupBox_image.Controls.Add(this.comboBox_press_image);
            this.groupBox_image.Controls.Add(this.label2);
            this.groupBox_image.Controls.Add(this.comboBox_normal_image);
            this.groupBox_image.Controls.Add(this.label1);
            this.groupBox_image.Name = "groupBox_image";
            this.groupBox_image.TabStop = false;
            this.groupBox_image.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // comboBox_press_image
            // 
            resources.ApplyResources(this.comboBox_press_image, "comboBox_press_image");
            this.comboBox_press_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_press_image.DropDownWidth = 135;
            this.comboBox_press_image.FormattingEnabled = true;
            this.comboBox_press_image.Name = "comboBox_press_image";
            this.comboBox_press_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_press_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_press_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_press_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_press_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBox_normal_image
            // 
            resources.ApplyResources(this.comboBox_normal_image, "comboBox_normal_image");
            this.comboBox_normal_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_normal_image.DropDownWidth = 135;
            this.comboBox_normal_image.FormattingEnabled = true;
            this.comboBox_normal_image.Name = "comboBox_normal_image";
            this.comboBox_normal_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_normal_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_normal_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_normal_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_normal_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox_color
            // 
            resources.ApplyResources(this.groupBox_color, "groupBox_color");
            this.groupBox_color.Controls.Add(this.label5);
            this.groupBox_color.Controls.Add(this.comboBox_press_color);
            this.groupBox_color.Controls.Add(this.numericUpDown_radius);
            this.groupBox_color.Controls.Add(this.label4);
            this.groupBox_color.Controls.Add(this.label3);
            this.groupBox_color.Controls.Add(this.comboBox_normal_color);
            this.groupBox_color.Name = "groupBox_color";
            this.groupBox_color.TabStop = false;
            this.groupBox_color.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // comboBox_press_color
            // 
            resources.ApplyResources(this.comboBox_press_color, "comboBox_press_color");
            this.comboBox_press_color.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.comboBox_press_color.DropDownHeight = 1;
            this.comboBox_press_color.FormattingEnabled = true;
            this.comboBox_press_color.Name = "comboBox_press_color";
            this.comboBox_press_color.Click += new System.EventHandler(this.comboBox_color_Click);
            this.comboBox_press_color.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_radius
            // 
            resources.ApplyResources(this.numericUpDown_radius, "numericUpDown_radius");
            this.numericUpDown_radius.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_radius.Name = "numericUpDown_radius";
            this.numericUpDown_radius.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDown_radius.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBox_normal_color
            // 
            resources.ApplyResources(this.comboBox_normal_color, "comboBox_normal_color");
            this.comboBox_normal_color.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.comboBox_normal_color.DropDownHeight = 1;
            this.comboBox_normal_color.FormattingEnabled = true;
            this.comboBox_normal_color.Name = "comboBox_normal_color";
            this.comboBox_normal_color.Click += new System.EventHandler(this.comboBox_color_Click);
            this.comboBox_normal_color.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_textSize
            // 
            resources.ApplyResources(this.numericUpDown_textSize, "numericUpDown_textSize");
            this.numericUpDown_textSize.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDown_textSize.Name = "numericUpDown_textSize";
            this.numericUpDown_textSize.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown_textSize.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBox_script
            // 
            resources.ApplyResources(this.textBox_script, "textBox_script");
            this.textBox_script.Name = "textBox_script";
            this.textBox_script.TextChanged += new System.EventHandler(this.textBox_script_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // comboBox_Text_color
            // 
            resources.ApplyResources(this.comboBox_Text_color, "comboBox_Text_color");
            this.comboBox_Text_color.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_Text_color.DropDownHeight = 1;
            this.comboBox_Text_color.FormattingEnabled = true;
            this.comboBox_Text_color.Name = "comboBox_Text_color";
            this.comboBox_Text_color.Click += new System.EventHandler(this.comboBox_color_Click);
            this.comboBox_Text_color.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // UCtrl_Button_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox_Text_color);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_script);
            this.Controls.Add(this.numericUpDown_textSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox_color);
            this.Controls.Add(this.groupBox_image);
            this.Controls.Add(this.numericUpDown_width);
            this.Controls.Add(this.numericUpDown_height);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.dataGridView_buttons);
            this.Controls.Add(this.numericUpDown_buttonX);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.numericUpDown_buttonY);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.label04);
            this.Name = "UCtrl_Button_Opt";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_buttons)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_buttonX)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_buttonY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            this.groupBox_image.ResumeLayout(false);
            this.groupBox_color.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_radius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_buttons;
        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.NumericUpDown numericUpDown_width;
        public System.Windows.Forms.NumericUpDown numericUpDown_height;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label07;
        public System.Windows.Forms.NumericUpDown numericUpDown_buttonX;
        public System.Windows.Forms.NumericUpDown numericUpDown_buttonY;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.GroupBox groupBox_image;
        private System.Windows.Forms.ComboBox comboBox_normal_image;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_color;
        private System.Windows.Forms.ComboBox comboBox_press_image;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_normal_color;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_press_color;
        public System.Windows.Forms.NumericUpDown numericUpDown_radius;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUpDown_textSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_script;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_Text_color;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Button;
        private System.Windows.Forms.DataGridViewCheckBoxColumn VisibleButton;
    }
}
