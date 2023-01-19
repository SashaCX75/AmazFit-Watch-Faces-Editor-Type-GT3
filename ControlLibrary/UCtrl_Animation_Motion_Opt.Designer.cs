
namespace ControlLibrary
{
    partial class UCtrl_Animation_Motion_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Animation_Motion_Opt));
            this.checkBox_show_in_startPos = new System.Windows.Forms.CheckBox();
            this.checkBox_anim_two_sides = new System.Windows.Forms.CheckBox();
            this.numericUpDown_start_x = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_X = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуХToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemX = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_start_y = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip_Y = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьКоординатуYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItemY = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_fps = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.comboBox_image = new System.Windows.Forms.ComboBox();
            this.label02 = new System.Windows.Forms.Label();
            this.numericUpDown_end_x = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_end_y = new System.Windows.Forms.NumericUpDown();
            this.label06 = new System.Windows.Forms.Label();
            this.label08 = new System.Windows.Forms.Label();
            this.label07 = new System.Windows.Forms.Label();
            this.numericUpDown_anim_duration = new System.Windows.Forms.NumericUpDown();
            this.label09 = new System.Windows.Forms.Label();
            this.numericUpDown_repeat_count = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_select_anim = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_del = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.checkBox_visible = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_start_x)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_start_y)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_end_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_end_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_anim_duration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_repeat_count)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_show_in_startPos
            // 
            resources.ApplyResources(this.checkBox_show_in_startPos, "checkBox_show_in_startPos");
            this.checkBox_show_in_startPos.Checked = true;
            this.checkBox_show_in_startPos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_show_in_startPos.Name = "checkBox_show_in_startPos";
            this.checkBox_show_in_startPos.UseVisualStyleBackColor = true;
            this.checkBox_show_in_startPos.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // checkBox_anim_two_sides
            // 
            resources.ApplyResources(this.checkBox_anim_two_sides, "checkBox_anim_two_sides");
            this.checkBox_anim_two_sides.Checked = true;
            this.checkBox_anim_two_sides.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_anim_two_sides.Name = "checkBox_anim_two_sides";
            this.checkBox_anim_two_sides.UseVisualStyleBackColor = true;
            this.checkBox_anim_two_sides.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // numericUpDown_start_x
            // 
            resources.ApplyResources(this.numericUpDown_start_x, "numericUpDown_start_x");
            this.numericUpDown_start_x.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_start_x.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_start_x.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_start_x.Name = "numericUpDown_start_x";
            this.numericUpDown_start_x.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_start_x.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_start_KeyDown);
            this.numericUpDown_start_x.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            // numericUpDown_start_y
            // 
            resources.ApplyResources(this.numericUpDown_start_y, "numericUpDown_start_y");
            this.numericUpDown_start_y.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_start_y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_start_y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_start_y.Name = "numericUpDown_start_y";
            this.numericUpDown_start_y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_start_y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_start_KeyDown);
            this.numericUpDown_start_y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // numericUpDown_fps
            // 
            resources.ApplyResources(this.numericUpDown_fps, "numericUpDown_fps");
            this.numericUpDown_fps.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_fps.Name = "numericUpDown_fps";
            this.numericUpDown_fps.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_fps.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
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
            // label05
            // 
            resources.ApplyResources(this.label05, "label05");
            this.label05.Name = "label05";
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
            // label02
            // 
            resources.ApplyResources(this.label02, "label02");
            this.label02.Name = "label02";
            // 
            // numericUpDown_end_x
            // 
            resources.ApplyResources(this.numericUpDown_end_x, "numericUpDown_end_x");
            this.numericUpDown_end_x.ContextMenuStrip = this.contextMenuStrip_X;
            this.numericUpDown_end_x.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_end_x.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_end_x.Name = "numericUpDown_end_x";
            this.numericUpDown_end_x.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_end_x.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_end_KeyDown);
            this.numericUpDown_end_x.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
            // 
            // numericUpDown_end_y
            // 
            resources.ApplyResources(this.numericUpDown_end_y, "numericUpDown_end_y");
            this.numericUpDown_end_y.ContextMenuStrip = this.contextMenuStrip_Y;
            this.numericUpDown_end_y.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_end_y.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_end_y.Name = "numericUpDown_end_y";
            this.numericUpDown_end_y.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDown_end_y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_end_KeyDown);
            this.numericUpDown_end_y.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
            // 
            // label06
            // 
            resources.ApplyResources(this.label06, "label06");
            this.label06.Name = "label06";
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
            // numericUpDown_anim_duration
            // 
            resources.ApplyResources(this.numericUpDown_anim_duration, "numericUpDown_anim_duration");
            this.numericUpDown_anim_duration.DecimalPlaces = 1;
            this.numericUpDown_anim_duration.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_anim_duration.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_anim_duration.Name = "numericUpDown_anim_duration";
            this.numericUpDown_anim_duration.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_anim_duration.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label09
            // 
            resources.ApplyResources(this.label09, "label09");
            this.label09.Name = "label09";
            // 
            // numericUpDown_repeat_count
            // 
            resources.ApplyResources(this.numericUpDown_repeat_count, "numericUpDown_repeat_count");
            this.numericUpDown_repeat_count.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_repeat_count.Name = "numericUpDown_repeat_count";
            this.numericUpDown_repeat_count.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBox_select_anim
            // 
            resources.ApplyResources(this.comboBox_select_anim, "comboBox_select_anim");
            this.comboBox_select_anim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_select_anim.FormattingEnabled = true;
            this.comboBox_select_anim.Name = "comboBox_select_anim";
            this.comboBox_select_anim.SelectedIndexChanged += new System.EventHandler(this.comboBox_select_anim_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // checkBox_visible
            // 
            resources.ApplyResources(this.checkBox_visible, "checkBox_visible");
            this.checkBox_visible.Checked = true;
            this.checkBox_visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_visible.Name = "checkBox_visible";
            this.checkBox_visible.UseVisualStyleBackColor = true;
            this.checkBox_visible.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // UCtrl_Animation_Motion_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_visible);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.comboBox_select_anim);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDown_repeat_count);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown_anim_duration);
            this.Controls.Add(this.label09);
            this.Controls.Add(this.numericUpDown_end_x);
            this.Controls.Add(this.numericUpDown_end_y);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label08);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.checkBox_show_in_startPos);
            this.Controls.Add(this.checkBox_anim_two_sides);
            this.Controls.Add(this.numericUpDown_start_x);
            this.Controls.Add(this.numericUpDown_start_y);
            this.Controls.Add(this.numericUpDown_fps);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.comboBox_image);
            this.Controls.Add(this.label02);
            this.Name = "UCtrl_Animation_Motion_Opt";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_start_x)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_start_y)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_end_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_end_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_anim_duration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_repeat_count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.CheckBox checkBox_show_in_startPos;
        public System.Windows.Forms.CheckBox checkBox_anim_two_sides;
        public System.Windows.Forms.NumericUpDown numericUpDown_start_x;
        public System.Windows.Forms.NumericUpDown numericUpDown_start_y;
        public System.Windows.Forms.NumericUpDown numericUpDown_fps;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.ComboBox comboBox_image;
        private System.Windows.Forms.Label label02;
        public System.Windows.Forms.NumericUpDown numericUpDown_end_x;
        public System.Windows.Forms.NumericUpDown numericUpDown_end_y;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label08;
        private System.Windows.Forms.Label label07;
        public System.Windows.Forms.NumericUpDown numericUpDown_anim_duration;
        private System.Windows.Forms.Label label09;
        public System.Windows.Forms.NumericUpDown numericUpDown_repeat_count;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_select_anim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_del;
        private System.Windows.Forms.Button button_add;
        public System.Windows.Forms.CheckBox checkBox_visible;
    }
}
