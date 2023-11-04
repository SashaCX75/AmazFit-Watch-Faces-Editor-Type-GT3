
namespace ControlLibrary
{
    partial class UCtrl_Wind_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Wind_Elm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Direction = new System.Windows.Forms.Panel();
            this.button_Direction = new System.Windows.Forms.Button();
            this.checkBox_Direction = new System.Windows.Forms.CheckBox();
            this.panel_Images = new System.Windows.Forms.Panel();
            this.checkBox_Images = new System.Windows.Forms.CheckBox();
            this.button_Images = new System.Windows.Forms.Button();
            this.panel_Segments = new System.Windows.Forms.Panel();
            this.checkBox_Segments = new System.Windows.Forms.CheckBox();
            this.button_Segments = new System.Windows.Forms.Button();
            this.panel_Number = new System.Windows.Forms.Panel();
            this.button_Number = new System.Windows.Forms.Button();
            this.checkBox_Number = new System.Windows.Forms.CheckBox();
            this.panel_Pointer = new System.Windows.Forms.Panel();
            this.button_Pointer = new System.Windows.Forms.Button();
            this.checkBox_Pointer = new System.Windows.Forms.CheckBox();
            this.panel_Icon = new System.Windows.Forms.Panel();
            this.button_Icon = new System.Windows.Forms.Button();
            this.checkBox_Icon = new System.Windows.Forms.CheckBox();
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel_Number_Font = new System.Windows.Forms.Panel();
            this.button_Number_Font = new System.Windows.Forms.Button();
            this.checkBox_Number_Font = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Direction.SuspendLayout();
            this.panel_Images.SuspendLayout();
            this.panel_Segments.SuspendLayout();
            this.panel_Number.SuspendLayout();
            this.panel_Pointer.SuspendLayout();
            this.panel_Icon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            this.panel_Number_Font.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel_Number_Font, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_Direction, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel_Images, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_Segments, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_Number, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_Pointer, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel_Icon, 0, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Direction
            // 
            this.panel_Direction.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Direction.Controls.Add(this.button_Direction);
            this.panel_Direction.Controls.Add(this.checkBox_Direction);
            resources.ApplyResources(this.panel_Direction, "panel_Direction");
            this.panel_Direction.Name = "panel_Direction";
            this.toolTip1.SetToolTip(this.panel_Direction, resources.GetString("panel_Direction.ToolTip"));
            this.panel_Direction.Click += new System.EventHandler(this.panel_Direction_Click);
            this.panel_Direction.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Direction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Direction.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Direction
            // 
            this.button_Direction.FlatAppearance.BorderSize = 0;
            this.button_Direction.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Direction.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Direction, "button_Direction");
            this.button_Direction.Image = global::ControlLibrary.Properties.Resources.direction;
            this.button_Direction.Name = "button_Direction";
            this.toolTip1.SetToolTip(this.button_Direction, resources.GetString("button_Direction.ToolTip"));
            this.button_Direction.UseVisualStyleBackColor = true;
            this.button_Direction.Click += new System.EventHandler(this.panel_Direction_Click);
            this.button_Direction.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Direction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Direction.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Direction
            // 
            resources.ApplyResources(this.checkBox_Direction, "checkBox_Direction");
            this.checkBox_Direction.Name = "checkBox_Direction";
            this.checkBox_Direction.UseVisualStyleBackColor = true;
            this.checkBox_Direction.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Images
            // 
            this.panel_Images.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Images.Controls.Add(this.checkBox_Images);
            this.panel_Images.Controls.Add(this.button_Images);
            resources.ApplyResources(this.panel_Images, "panel_Images");
            this.panel_Images.Name = "panel_Images";
            this.panel_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.panel_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Images
            // 
            resources.ApplyResources(this.checkBox_Images, "checkBox_Images");
            this.checkBox_Images.Name = "checkBox_Images";
            this.checkBox_Images.UseVisualStyleBackColor = true;
            this.checkBox_Images.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_Images
            // 
            this.button_Images.FlatAppearance.BorderSize = 0;
            this.button_Images.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Images.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Images, "button_Images");
            this.button_Images.Image = global::ControlLibrary.Properties.Resources.images_18;
            this.button_Images.Name = "button_Images";
            this.button_Images.UseVisualStyleBackColor = true;
            this.button_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.button_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_Segments
            // 
            this.panel_Segments.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Segments.Controls.Add(this.checkBox_Segments);
            this.panel_Segments.Controls.Add(this.button_Segments);
            resources.ApplyResources(this.panel_Segments, "panel_Segments");
            this.panel_Segments.Name = "panel_Segments";
            this.panel_Segments.Click += new System.EventHandler(this.panel_Segments_Click);
            this.panel_Segments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Segments.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Segments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Segments
            // 
            resources.ApplyResources(this.checkBox_Segments, "checkBox_Segments");
            this.checkBox_Segments.Name = "checkBox_Segments";
            this.checkBox_Segments.UseVisualStyleBackColor = true;
            this.checkBox_Segments.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_Segments
            // 
            this.button_Segments.FlatAppearance.BorderSize = 0;
            this.button_Segments.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Segments.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Segments, "button_Segments");
            this.button_Segments.Image = global::ControlLibrary.Properties.Resources.segment_18;
            this.button_Segments.Name = "button_Segments";
            this.button_Segments.UseVisualStyleBackColor = true;
            this.button_Segments.Click += new System.EventHandler(this.panel_Segments_Click);
            this.button_Segments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Segments.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Segments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_Number
            // 
            this.panel_Number.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Number.Controls.Add(this.button_Number);
            this.panel_Number.Controls.Add(this.checkBox_Number);
            resources.ApplyResources(this.panel_Number, "panel_Number");
            this.panel_Number.Name = "panel_Number";
            this.panel_Number.Click += new System.EventHandler(this.panel_Number_Click);
            this.panel_Number.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Number.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Number.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Number
            // 
            this.button_Number.FlatAppearance.BorderSize = 0;
            this.button_Number.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Number.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Number, "button_Number");
            this.button_Number.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Number.Name = "button_Number";
            this.button_Number.UseVisualStyleBackColor = true;
            this.button_Number.Click += new System.EventHandler(this.panel_Number_Click);
            this.button_Number.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Number.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Number.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Number
            // 
            resources.ApplyResources(this.checkBox_Number, "checkBox_Number");
            this.checkBox_Number.Name = "checkBox_Number";
            this.checkBox_Number.UseVisualStyleBackColor = true;
            this.checkBox_Number.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Pointer
            // 
            this.panel_Pointer.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Pointer.Controls.Add(this.button_Pointer);
            this.panel_Pointer.Controls.Add(this.checkBox_Pointer);
            resources.ApplyResources(this.panel_Pointer, "panel_Pointer");
            this.panel_Pointer.Name = "panel_Pointer";
            this.panel_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.panel_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Pointer
            // 
            this.button_Pointer.FlatAppearance.BorderSize = 0;
            this.button_Pointer.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Pointer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Pointer, "button_Pointer");
            this.button_Pointer.Name = "button_Pointer";
            this.button_Pointer.UseVisualStyleBackColor = true;
            this.button_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.button_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Pointer
            // 
            resources.ApplyResources(this.checkBox_Pointer, "checkBox_Pointer");
            this.checkBox_Pointer.Name = "checkBox_Pointer";
            this.checkBox_Pointer.UseVisualStyleBackColor = true;
            this.checkBox_Pointer.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Icon
            // 
            this.panel_Icon.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Icon.Controls.Add(this.button_Icon);
            this.panel_Icon.Controls.Add(this.checkBox_Icon);
            resources.ApplyResources(this.panel_Icon, "panel_Icon");
            this.panel_Icon.Name = "panel_Icon";
            this.panel_Icon.Click += new System.EventHandler(this.panel_Icon_Click);
            this.panel_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Icon
            // 
            this.button_Icon.FlatAppearance.BorderSize = 0;
            this.button_Icon.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Icon.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Icon, "button_Icon");
            this.button_Icon.Image = global::ControlLibrary.Properties.Resources.wallpaper_18;
            this.button_Icon.Name = "button_Icon";
            this.button_Icon.UseVisualStyleBackColor = true;
            this.button_Icon.Click += new System.EventHandler(this.panel_Icon_Click);
            this.button_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Icon
            // 
            resources.ApplyResources(this.checkBox_Icon, "checkBox_Icon");
            this.checkBox_Icon.Name = "checkBox_Icon";
            this.checkBox_Icon.UseVisualStyleBackColor = true;
            this.checkBox_Icon.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // pictureBox_Arrow_Down
            // 
            this.pictureBox_Arrow_Down.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_down;
            resources.ApplyResources(this.pictureBox_Arrow_Down, "pictureBox_Arrow_Down");
            this.pictureBox_Arrow_Down.Name = "pictureBox_Arrow_Down";
            this.pictureBox_Arrow_Down.TabStop = false;
            this.pictureBox_Arrow_Down.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_NotShow
            // 
            resources.ApplyResources(this.pictureBox_NotShow, "pictureBox_NotShow");
            this.pictureBox_NotShow.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_off_black_24;
            this.pictureBox_NotShow.Name = "pictureBox_NotShow";
            this.pictureBox_NotShow.TabStop = false;
            this.pictureBox_NotShow.Click += new System.EventHandler(this.pictureBox_NotShow_Click);
            // 
            // pictureBox_Arrow_Right
            // 
            this.pictureBox_Arrow_Right.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_right;
            resources.ApplyResources(this.pictureBox_Arrow_Right, "pictureBox_Arrow_Right");
            this.pictureBox_Arrow_Right.Name = "pictureBox_Arrow_Right";
            this.pictureBox_Arrow_Right.TabStop = false;
            this.pictureBox_Arrow_Right.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_Show
            // 
            resources.ApplyResources(this.pictureBox_Show, "pictureBox_Show");
            this.pictureBox_Show.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_black_24;
            this.pictureBox_Show.Name = "pictureBox_Show";
            this.pictureBox_Show.TabStop = false;
            this.pictureBox_Show.Click += new System.EventHandler(this.pictureBox_Show_Click);
            // 
            // pictureBox_Del
            // 
            this.pictureBox_Del.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_delete_forever_black_24;
            resources.ApplyResources(this.pictureBox_Del, "pictureBox_Del");
            this.pictureBox_Del.Name = "pictureBox_Del";
            this.pictureBox_Del.TabStop = false;
            this.pictureBox_Del.Click += new System.EventHandler(this.pictureBox_Del_Click);
            // 
            // button_ElementName
            // 
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.wind;
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 32000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // panel_Number_Font
            // 
            this.panel_Number_Font.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Number_Font.Controls.Add(this.button_Number_Font);
            this.panel_Number_Font.Controls.Add(this.checkBox_Number_Font);
            resources.ApplyResources(this.panel_Number_Font, "panel_Number_Font");
            this.panel_Number_Font.Name = "panel_Number_Font";
            this.panel_Number_Font.Click += new System.EventHandler(this.panel_Number_Font_Click);
            this.panel_Number_Font.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Number_Font.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Number_Font.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Number_Font
            // 
            this.button_Number_Font.FlatAppearance.BorderSize = 0;
            this.button_Number_Font.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Number_Font.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Number_Font, "button_Number_Font");
            this.button_Number_Font.Image = global::ControlLibrary.Properties.Resources.text_fields;
            this.button_Number_Font.Name = "button_Number_Font";
            this.button_Number_Font.UseVisualStyleBackColor = true;
            this.button_Number_Font.Click += new System.EventHandler(this.panel_Number_Font_Click);
            this.button_Number_Font.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Number_Font.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Number_Font.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Number_Font
            // 
            resources.ApplyResources(this.checkBox_Number_Font, "checkBox_Number_Font");
            this.checkBox_Number_Font.Name = "checkBox_Number_Font";
            this.checkBox_Number_Font.UseVisualStyleBackColor = true;
            this.checkBox_Number_Font.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // UCtrl_Wind_Elm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_Arrow_Down);
            this.Controls.Add(this.pictureBox_NotShow);
            this.Controls.Add(this.pictureBox_Arrow_Right);
            this.Controls.Add(this.pictureBox_Show);
            this.Controls.Add(this.pictureBox_Del);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button_ElementName);
            this.Name = "UCtrl_Wind_Elm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Direction.ResumeLayout(false);
            this.panel_Direction.PerformLayout();
            this.panel_Images.ResumeLayout(false);
            this.panel_Images.PerformLayout();
            this.panel_Segments.ResumeLayout(false);
            this.panel_Segments.PerformLayout();
            this.panel_Number.ResumeLayout(false);
            this.panel_Number.PerformLayout();
            this.panel_Pointer.ResumeLayout(false);
            this.panel_Pointer.PerformLayout();
            this.panel_Icon.ResumeLayout(false);
            this.panel_Icon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
            this.panel_Number_Font.ResumeLayout(false);
            this.panel_Number_Font.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Arrow_Down;
        private System.Windows.Forms.PictureBox pictureBox_NotShow;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Right;
        private System.Windows.Forms.PictureBox pictureBox_Show;
        private System.Windows.Forms.PictureBox pictureBox_Del;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_Images;
        public System.Windows.Forms.CheckBox checkBox_Images;
        private System.Windows.Forms.Button button_Images;
        private System.Windows.Forms.Panel panel_Segments;
        public System.Windows.Forms.CheckBox checkBox_Segments;
        private System.Windows.Forms.Button button_Segments;
        private System.Windows.Forms.Panel panel_Number;
        private System.Windows.Forms.Button button_Number;
        public System.Windows.Forms.CheckBox checkBox_Number;
        private System.Windows.Forms.Panel panel_Pointer;
        private System.Windows.Forms.Button button_Pointer;
        public System.Windows.Forms.CheckBox checkBox_Pointer;
        private System.Windows.Forms.Panel panel_Icon;
        private System.Windows.Forms.Button button_Icon;
        public System.Windows.Forms.CheckBox checkBox_Icon;
        protected System.Windows.Forms.Button button_ElementName;
        private System.Windows.Forms.Panel panel_Direction;
        private System.Windows.Forms.Button button_Direction;
        public System.Windows.Forms.CheckBox checkBox_Direction;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel_Number_Font;
        private System.Windows.Forms.Button button_Number_Font;
        public System.Windows.Forms.CheckBox checkBox_Number_Font;
    }
}
