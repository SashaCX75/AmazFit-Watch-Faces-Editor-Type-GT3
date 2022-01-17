
namespace ControlLibrary
{
    partial class UCtrl_Text_Weather_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Text_Weather_Opt));
            this.comboBox_unit_f = new System.Windows.Forms.ComboBox();
            this.numericUpDown_spacing = new System.Windows.Forms.NumericUpDown();
            this.checkBox_addZero = new System.Windows.Forms.CheckBox();
            this.comboBox_imageError = new System.Windows.Forms.ComboBox();
            this.comboBox_imageMinus = new System.Windows.Forms.ComboBox();
            this.checkBox_follow = new System.Windows.Forms.CheckBox();
            this.label03 = new System.Windows.Forms.Label();
            this.label06 = new System.Windows.Forms.Label();
            this.label07 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_unit_c = new System.Windows.Forms.ComboBox();
            this.comboBox_alignment = new System.Windows.Forms.ComboBox();
            this.label08 = new System.Windows.Forms.Label();
            this.label09 = new System.Windows.Forms.Label();
            this.comboBox_image = new System.Windows.Forms.ComboBox();
            this.comboBox_icon = new System.Windows.Forms.ComboBox();
            this.numericUpDown_imageX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_imageY = new System.Windows.Forms.NumericUpDown();
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
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            this.contextMenuStrip_X.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_unit_f
            // 
            this.comboBox_unit_f.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_unit_f.DropDownWidth = 135;
            this.comboBox_unit_f.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_unit_f, "comboBox_unit_f");
            this.comboBox_unit_f.Name = "comboBox_unit_f";
            this.comboBox_unit_f.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_unit_f.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_unit_f.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_unit_f.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_unit_f.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
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
            this.comboBox_imageError.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_imageError.DropDownWidth = 135;
            this.comboBox_imageError.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_imageError, "comboBox_imageError");
            this.comboBox_imageError.Name = "comboBox_imageError";
            this.comboBox_imageError.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_imageError.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_imageError.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_imageError.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_imageError.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBox_imageMinus
            // 
            this.comboBox_imageMinus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_imageMinus.DropDownWidth = 135;
            this.comboBox_imageMinus.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_imageMinus, "comboBox_imageMinus");
            this.comboBox_imageMinus.Name = "comboBox_imageMinus";
            this.comboBox_imageMinus.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_imageMinus.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_imageMinus.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_imageMinus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_imageMinus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
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
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBox_unit_c
            // 
            this.comboBox_unit_c.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_unit_c.DropDownWidth = 135;
            this.comboBox_unit_c.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_unit_c, "comboBox_unit_c");
            this.comboBox_unit_c.Name = "comboBox_unit_c";
            this.comboBox_unit_c.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_unit_c.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_unit_c.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_unit_c.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_unit_c.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBox_alignment
            // 
            this.comboBox_alignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_alignment.FormattingEnabled = true;
            this.comboBox_alignment.Items.AddRange(new object[] {
            resources.GetString("comboBox_alignment.Items"),
            resources.GetString("comboBox_alignment.Items1"),
            resources.GetString("comboBox_alignment.Items2")});
            resources.ApplyResources(this.comboBox_alignment, "comboBox_alignment");
            this.comboBox_alignment.Name = "comboBox_alignment";
            this.comboBox_alignment.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label08
            // 
            resources.ApplyResources(this.label08, "label08");
            this.label08.Name = "label08";
            // 
            // label09
            // 
            resources.ApplyResources(this.label09, "label09");
            this.label09.Name = "label09";
            // 
            // comboBox_image
            // 
            this.comboBox_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_image.DropDownWidth = 135;
            this.comboBox_image.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_image, "comboBox_image");
            this.comboBox_image.Name = "comboBox_image";
            this.comboBox_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBox_icon
            // 
            this.comboBox_icon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_icon.DropDownWidth = 135;
            this.comboBox_icon.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_icon, "comboBox_icon");
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
            this.numericUpDown_imageX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_imageY
            // 
            resources.ApplyResources(this.numericUpDown_imageY, "numericUpDown_imageY");
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
            this.numericUpDown_imageY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // numericUpDown_iconX
            // 
            resources.ApplyResources(this.numericUpDown_iconX, "numericUpDown_iconX");
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
            this.numericUpDown_iconX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_iconY
            // 
            resources.ApplyResources(this.numericUpDown_iconY, "numericUpDown_iconY");
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
            // UCtrl_Text_Weather_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_unit_f);
            this.Controls.Add(this.numericUpDown_spacing);
            this.Controls.Add(this.checkBox_addZero);
            this.Controls.Add(this.comboBox_imageError);
            this.Controls.Add(this.comboBox_imageMinus);
            this.Controls.Add(this.checkBox_follow);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBox_unit_c);
            this.Controls.Add(this.comboBox_alignment);
            this.Controls.Add(this.label08);
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
            this.Name = "UCtrl_Text_Weather_Opt";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_spacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_iconY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            this.contextMenuStrip_X.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.NumericUpDown numericUpDown_spacing;
        public System.Windows.Forms.CheckBox checkBox_addZero;
        public System.Windows.Forms.CheckBox checkBox_follow;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.ComboBox comboBox_alignment;
        private System.Windows.Forms.Label label09;
        private System.Windows.Forms.ComboBox comboBox_image;
        private System.Windows.Forms.ComboBox comboBox_icon;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageX;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageY;
        public System.Windows.Forms.NumericUpDown numericUpDown_iconX;
        public System.Windows.Forms.NumericUpDown numericUpDown_iconY;
        private System.Windows.Forms.Label label01;
        protected System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label1083;
        protected System.Windows.Forms.Label label1084;
        protected System.Windows.Forms.Label label1085;
        private System.Windows.Forms.Label label1086;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.ComboBox comboBox_unit_f;
        private System.Windows.Forms.ComboBox comboBox_imageError;
        private System.Windows.Forms.ComboBox comboBox_imageMinus;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label07;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_unit_c;
        private System.Windows.Forms.Label label08;
        private System.Windows.Forms.Label label04;
    }
}
