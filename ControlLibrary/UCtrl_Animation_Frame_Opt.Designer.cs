
namespace ControlLibrary
{
    partial class UCtrl_Animation_Frame_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Animation_Frame_Opt));
            this.comboBox_image = new System.Windows.Forms.ComboBox();
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
            this.label01 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.numericUpDown_fps = new System.Windows.Forms.NumericUpDown();
            this.label06 = new System.Windows.Forms.Label();
            this.numericUpDown_images_count = new System.Windows.Forms.NumericUpDown();
            this.label05 = new System.Windows.Forms.Label();
            this.checkBox_anim_repeat = new System.Windows.Forms.CheckBox();
            this.label07 = new System.Windows.Forms.Label();
            this.label_prefix = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_select_anim = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_add = new System.Windows.Forms.Button();
            this.button_del = new System.Windows.Forms.Button();
            this.checkBox_visible = new System.Windows.Forms.CheckBox();
            this.numericUpDown_preview_frame = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).BeginInit();
            this.contextMenuStrip_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).BeginInit();
            this.contextMenuStrip_Y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_images_count)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_preview_frame)).BeginInit();
            this.SuspendLayout();
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
            this.comboBox_image.DropDownClosed += new System.EventHandler(this.comboBox_image_DropDownClosed);
            this.comboBox_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // numericUpDown_imageX
            // 
            this.numericUpDown_imageX.ContextMenuStrip = this.contextMenuStrip_X;
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
            this.numericUpDown_imageX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_image_KeyDown);
            this.numericUpDown_imageX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesX_MouseDoubleClick);
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
            // numericUpDown_imageY
            // 
            this.numericUpDown_imageY.ContextMenuStrip = this.contextMenuStrip_Y;
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
            this.numericUpDown_imageY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_image_KeyDown);
            this.numericUpDown_imageY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDown_picturesY_MouseDoubleClick);
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
            // label03
            // 
            resources.ApplyResources(this.label03, "label03");
            this.label03.Name = "label03";
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
            // label06
            // 
            resources.ApplyResources(this.label06, "label06");
            this.label06.Name = "label06";
            // 
            // numericUpDown_images_count
            // 
            resources.ApplyResources(this.numericUpDown_images_count, "numericUpDown_images_count");
            this.numericUpDown_images_count.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_images_count.Name = "numericUpDown_images_count";
            this.numericUpDown_images_count.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_images_count.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label05
            // 
            resources.ApplyResources(this.label05, "label05");
            this.label05.Name = "label05";
            // 
            // checkBox_anim_repeat
            // 
            resources.ApplyResources(this.checkBox_anim_repeat, "checkBox_anim_repeat");
            this.checkBox_anim_repeat.Checked = true;
            this.checkBox_anim_repeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_anim_repeat.Name = "checkBox_anim_repeat";
            this.checkBox_anim_repeat.UseVisualStyleBackColor = true;
            this.checkBox_anim_repeat.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // label07
            // 
            resources.ApplyResources(this.label07, "label07");
            this.label07.Name = "label07";
            // 
            // label_prefix
            // 
            resources.ApplyResources(this.label_prefix, "label_prefix");
            this.label_prefix.Name = "label_prefix";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBox_select_anim
            // 
            this.comboBox_select_anim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_select_anim.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_select_anim, "comboBox_select_anim");
            this.comboBox_select_anim.Name = "comboBox_select_anim";
            this.comboBox_select_anim.SelectedIndexChanged += new System.EventHandler(this.comboBox_select_anim_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // checkBox_visible
            // 
            resources.ApplyResources(this.checkBox_visible, "checkBox_visible");
            this.checkBox_visible.Checked = true;
            this.checkBox_visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_visible.Name = "checkBox_visible";
            this.checkBox_visible.UseVisualStyleBackColor = true;
            this.checkBox_visible.Click += new System.EventHandler(this.checkBox_Click);
            // 
            // numericUpDown_preview_frame
            // 
            resources.ApplyResources(this.numericUpDown_preview_frame, "numericUpDown_preview_frame");
            this.numericUpDown_preview_frame.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_preview_frame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_preview_frame.Name = "numericUpDown_preview_frame";
            this.numericUpDown_preview_frame.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_preview_frame.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // UCtrl_Animation_Frame_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown_preview_frame);
            this.Controls.Add(this.checkBox_visible);
            this.Controls.Add(this.button_del);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.comboBox_select_anim);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_prefix);
            this.Controls.Add(this.label07);
            this.Controls.Add(this.checkBox_anim_repeat);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.numericUpDown_images_count);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.numericUpDown_fps);
            this.Controls.Add(this.comboBox_image);
            this.Controls.Add(this.numericUpDown_imageX);
            this.Controls.Add(this.numericUpDown_imageY);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.label02);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label03);
            this.Name = "UCtrl_Animation_Frame_Opt";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageX)).EndInit();
            this.contextMenuStrip_X.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_imageY)).EndInit();
            this.contextMenuStrip_Y.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_images_count)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_preview_frame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_image;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageX;
        public System.Windows.Forms.NumericUpDown numericUpDown_imageY;
        private System.Windows.Forms.Label label01;
        protected System.Windows.Forms.Label label02;
        protected System.Windows.Forms.Label label04;
        protected System.Windows.Forms.Label label03;
        public System.Windows.Forms.NumericUpDown numericUpDown_fps;
        private System.Windows.Forms.Label label06;
        public System.Windows.Forms.NumericUpDown numericUpDown_images_count;
        private System.Windows.Forms.Label label05;
        protected System.Windows.Forms.Label label07;
        protected System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox checkBox_anim_repeat;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Y;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemY;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_X;
        private System.Windows.Forms.ToolStripMenuItem вставитьКоординатуХToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem копироватьToolStripMenuItemX;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItemX;
        private System.Windows.Forms.ComboBox comboBox_select_anim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_del;
        public System.Windows.Forms.Label label_prefix;
        public System.Windows.Forms.CheckBox checkBox_visible;
        public System.Windows.Forms.NumericUpDown numericUpDown_preview_frame;
        protected System.Windows.Forms.Label label2;
    }
}
