
namespace ControlLibrary
{
    partial class UCtrl_DateDay_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_DateDay_Elm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Number = new System.Windows.Forms.Panel();
            this.checkBox_Number = new System.Windows.Forms.CheckBox();
            this.panel_Pointer = new System.Windows.Forms.Panel();
            this.checkBox_Pointer = new System.Windows.Forms.CheckBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.button_Number = new System.Windows.Forms.Button();
            this.button_Pointer = new System.Windows.Forms.Button();
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Number.SuspendLayout();
            this.panel_Pointer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.Controls.Add(this.panel_Number, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_Pointer, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Number
            // 
            resources.ApplyResources(this.panel_Number, "panel_Number");
            this.panel_Number.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Number.Controls.Add(this.button_Number);
            this.panel_Number.Controls.Add(this.checkBox_Number);
            this.panel_Number.Name = "panel_Number";
            this.panel_Number.Click += new System.EventHandler(this.panel_Number_Click);
            this.panel_Number.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Number.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Number.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
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
            resources.ApplyResources(this.panel_Pointer, "panel_Pointer");
            this.panel_Pointer.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Pointer.Controls.Add(this.button_Pointer);
            this.panel_Pointer.Controls.Add(this.checkBox_Pointer);
            this.panel_Pointer.Name = "panel_Pointer";
            this.panel_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.panel_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Pointer
            // 
            resources.ApplyResources(this.checkBox_Pointer, "checkBox_Pointer");
            this.checkBox_Pointer.Name = "checkBox_Pointer";
            this.checkBox_Pointer.UseVisualStyleBackColor = true;
            this.checkBox_Pointer.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
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
            resources.ApplyResources(this.pictureBox_Arrow_Right, "pictureBox_Arrow_Right");
            this.pictureBox_Arrow_Right.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_right;
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
            resources.ApplyResources(this.pictureBox_Del, "pictureBox_Del");
            this.pictureBox_Del.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_delete_forever_black_24;
            this.pictureBox_Del.Name = "pictureBox_Del";
            this.pictureBox_Del.TabStop = false;
            this.pictureBox_Del.Click += new System.EventHandler(this.pictureBox_Del_Click);
            // 
            // button_Number
            // 
            resources.ApplyResources(this.button_Number, "button_Number");
            this.button_Number.FlatAppearance.BorderSize = 0;
            this.button_Number.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Number.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Number.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Number.Name = "button_Number";
            this.button_Number.UseVisualStyleBackColor = true;
            this.button_Number.Click += new System.EventHandler(this.panel_Number_Click);
            this.button_Number.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Number.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Number.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Pointer
            // 
            resources.ApplyResources(this.button_Pointer, "button_Pointer");
            this.button_Pointer.FlatAppearance.BorderSize = 0;
            this.button_Pointer.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Pointer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Pointer.Image = global::ControlLibrary.Properties.Resources.pointer;
            this.button_Pointer.Name = "button_Pointer";
            this.button_Pointer.UseVisualStyleBackColor = true;
            this.button_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.button_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // pictureBox_Arrow_Down
            // 
            resources.ApplyResources(this.pictureBox_Arrow_Down, "pictureBox_Arrow_Down");
            this.pictureBox_Arrow_Down.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_down;
            this.pictureBox_Arrow_Down.Name = "pictureBox_Arrow_Down";
            this.pictureBox_Arrow_Down.TabStop = false;
            this.pictureBox_Arrow_Down.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // button_ElementName
            // 
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.Very_Basic_Calendar_16;
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // UCtrl_DateDay_Elm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_NotShow);
            this.Controls.Add(this.pictureBox_Arrow_Right);
            this.Controls.Add(this.pictureBox_Show);
            this.Controls.Add(this.pictureBox_Del);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox_Arrow_Down);
            this.Controls.Add(this.button_ElementName);
            this.Name = "UCtrl_DateDay_Elm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Number.ResumeLayout(false);
            this.panel_Number.PerformLayout();
            this.panel_Pointer.ResumeLayout(false);
            this.panel_Pointer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_NotShow;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Right;
        private System.Windows.Forms.PictureBox pictureBox_Show;
        private System.Windows.Forms.PictureBox pictureBox_Del;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_Number;
        private System.Windows.Forms.Button button_Number;
        public System.Windows.Forms.CheckBox checkBox_Number;
        private System.Windows.Forms.Panel panel_Pointer;
        private System.Windows.Forms.Button button_Pointer;
        public System.Windows.Forms.CheckBox checkBox_Pointer;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Down;
        private System.Windows.Forms.Button button_ElementName;
    }
}
