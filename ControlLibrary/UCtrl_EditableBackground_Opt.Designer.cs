namespace ControlLibrary
{
    partial class UCtrl_EditableBackground_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_EditableBackground_Opt));
            this.button_del = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.comboBox_select_background = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_image = new System.Windows.Forms.ComboBox();
            this.label01 = new System.Windows.Forms.Label();
            this.comboBox_Preview_image = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_tip = new System.Windows.Forms.ComboBox();
            this.numericUpDown_tipX = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_tipY = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.comboBox_foreground = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_edit_mode = new System.Windows.Forms.CheckBox();
            this.groupBox_EditableBackground = new System.Windows.Forms.GroupBox();
            this.button_PreviewAdd = new System.Windows.Forms.Button();
            this.button_PreviewRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tipX)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tipY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            this.groupBox_EditableBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_del
            // 
            resources.ApplyResources(this.button_del, "button_del");
            this.button_del.Name = "button_del";
            this.button_del.UseVisualStyleBackColor = true;
            this.button_del.Click += new System.EventHandler(this.button_del_Click);
            // 
            // button_add
            // 
            resources.ApplyResources(this.button_add, "button_add");
            this.button_add.Name = "button_add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // comboBox_select_background
            // 
            resources.ApplyResources(this.comboBox_select_background, "comboBox_select_background");
            this.comboBox_select_background.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_select_background.FormattingEnabled = true;
            this.comboBox_select_background.Name = "comboBox_select_background";
            this.comboBox_select_background.SelectedIndexChanged += new System.EventHandler(this.comboBox_select_background_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // comboBox_Preview_image
            // 
            resources.ApplyResources(this.comboBox_Preview_image, "comboBox_Preview_image");
            this.comboBox_Preview_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_Preview_image.DropDownWidth = 135;
            this.comboBox_Preview_image.FormattingEnabled = true;
            this.comboBox_Preview_image.Name = "comboBox_Preview_image";
            this.comboBox_Preview_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_Preview_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_Preview_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_Preview_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_Preview_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // comboBox_tip
            // 
            resources.ApplyResources(this.comboBox_tip, "comboBox_tip");
            this.comboBox_tip.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_tip.DropDownWidth = 135;
            this.comboBox_tip.FormattingEnabled = true;
            this.comboBox_tip.Name = "comboBox_tip";
            this.comboBox_tip.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_tip.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_tip.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_tip.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_tip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_tipX
            // 
            resources.ApplyResources(this.numericUpDown_tipX, "numericUpDown_tipX");
            this.numericUpDown_tipX.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_tipX.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_tipX.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_tipX.Name = "numericUpDown_tipX";
            this.numericUpDown_tipX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_tipX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_tip_KeyDown);
            this.numericUpDown_tipX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            this.вставитьToolStripMenuItemX.DropDownClosed += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // numericUpDown_tipY
            // 
            resources.ApplyResources(this.numericUpDown_tipY, "numericUpDown_tipY");
            this.numericUpDown_tipY.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_tipY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_tipY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_tipY.Name = "numericUpDown_tipY";
            this.numericUpDown_tipY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_tipY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_tip_KeyDown);
            this.numericUpDown_tipY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // label03
            // 
            resources.ApplyResources(this.label03, "label03");
            this.label03.Name = "label03";
            // 
            // comboBox_foreground
            // 
            resources.ApplyResources(this.comboBox_foreground, "comboBox_foreground");
            this.comboBox_foreground.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_foreground.DropDownWidth = 135;
            this.comboBox_foreground.FormattingEnabled = true;
            this.comboBox_foreground.Name = "comboBox_foreground";
            this.comboBox_foreground.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_foreground.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_foreground.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_foreground.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_foreground.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // checkBox_edit_mode
            // 
            resources.ApplyResources(this.checkBox_edit_mode, "checkBox_edit_mode");
            this.checkBox_edit_mode.Name = "checkBox_edit_mode";
            this.checkBox_edit_mode.UseVisualStyleBackColor = true;
            this.checkBox_edit_mode.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // groupBox_EditableBackground
            // 
            resources.ApplyResources(this.groupBox_EditableBackground, "groupBox_EditableBackground");
            this.groupBox_EditableBackground.Controls.Add(this.button_PreviewAdd);
            this.groupBox_EditableBackground.Controls.Add(this.button_PreviewRefresh);
            this.groupBox_EditableBackground.Controls.Add(this.comboBox_select_background);
            this.groupBox_EditableBackground.Controls.Add(this.checkBox_edit_mode);
            this.groupBox_EditableBackground.Controls.Add(this.label01);
            this.groupBox_EditableBackground.Controls.Add(this.comboBox_image);
            this.groupBox_EditableBackground.Controls.Add(this.label1);
            this.groupBox_EditableBackground.Controls.Add(this.comboBox_foreground);
            this.groupBox_EditableBackground.Controls.Add(this.button_add);
            this.groupBox_EditableBackground.Controls.Add(this.label4);
            this.groupBox_EditableBackground.Controls.Add(this.button_del);
            this.groupBox_EditableBackground.Controls.Add(this.comboBox_tip);
            this.groupBox_EditableBackground.Controls.Add(this.label2);
            this.groupBox_EditableBackground.Controls.Add(this.numericUpDown_tipX);
            this.groupBox_EditableBackground.Controls.Add(this.comboBox_Preview_image);
            this.groupBox_EditableBackground.Controls.Add(this.numericUpDown_tipY);
            this.groupBox_EditableBackground.Controls.Add(this.label03);
            this.groupBox_EditableBackground.Controls.Add(this.label3);
            this.groupBox_EditableBackground.Controls.Add(this.label04);
            this.groupBox_EditableBackground.Controls.Add(this.label02);
            this.groupBox_EditableBackground.Name = "groupBox_EditableBackground";
            this.groupBox_EditableBackground.TabStop = false;
            this.groupBox_EditableBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // button_PreviewAdd
            // 
            resources.ApplyResources(this.button_PreviewAdd, "button_PreviewAdd");
            this.button_PreviewAdd.Name = "button_PreviewAdd";
            this.button_PreviewAdd.UseVisualStyleBackColor = true;
            this.button_PreviewAdd.Click += new System.EventHandler(this.button_PreviewAdd_Click);
            // 
            // button_PreviewRefresh
            // 
            resources.ApplyResources(this.button_PreviewRefresh, "button_PreviewRefresh");
            this.button_PreviewRefresh.Name = "button_PreviewRefresh";
            this.button_PreviewRefresh.UseVisualStyleBackColor = true;
            this.button_PreviewRefresh.Click += new System.EventHandler(this.button_PreviewRefresh_Click);
            // 
            // UCtrl_EditableBackground_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_EditableBackground);
            this.Name = "UCtrl_EditableBackground_Opt";
            this.Load += new System.EventHandler(this.UCtrl_EditableBackground_Opt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tipX)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_tipY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            this.groupBox_EditableBackground.ResumeLayout(false);
            this.groupBox_EditableBackground.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.ComboBox comboBox_select_background;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.ComboBox comboBox_Preview_image;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_tip;
        public System.Windows.Forms.NumericUpDown numericUpDown_tipX;
        public System.Windows.Forms.NumericUpDown numericUpDown_tipY;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label label02;
        protected System.Windows.Forms.Label label04;
        protected System.Windows.Forms.Label label03;
        private System.Windows.Forms.ComboBox comboBox_image;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.ComboBox comboBox_foreground;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox checkBox_edit_mode;
        private System.Windows.Forms.GroupBox groupBox_EditableBackground;
        private System.Windows.Forms.Button button_PreviewAdd;
        private System.Windows.Forms.Button button_PreviewRefresh;
    }
}
