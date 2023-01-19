namespace ControlLibrary
{
    partial class UCtrl_AnalogTimePro_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_AnalogTimePro_Elm));
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Format_24hour = new System.Windows.Forms.Panel();
            this.button_Format_24hour = new System.Windows.Forms.Button();
            this.checkBox_Format_24hour = new System.Windows.Forms.CheckBox();
            this.panel_SmoothSeconds = new System.Windows.Forms.Panel();
            this.button_SmoothSeconds = new System.Windows.Forms.Button();
            this.checkBox_SmoothSeconds = new System.Windows.Forms.CheckBox();
            this.panel_Minutes = new System.Windows.Forms.Panel();
            this.button_Minutes = new System.Windows.Forms.Button();
            this.checkBox_Minutes = new System.Windows.Forms.CheckBox();
            this.panel_Hours = new System.Windows.Forms.Panel();
            this.checkBox_Hours = new System.Windows.Forms.CheckBox();
            this.button_Hours = new System.Windows.Forms.Button();
            this.panel_Seconds = new System.Windows.Forms.Panel();
            this.button_Seconds = new System.Windows.Forms.Button();
            this.checkBox_Seconds = new System.Windows.Forms.CheckBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Format_24hour.SuspendLayout();
            this.panel_SmoothSeconds.SuspendLayout();
            this.panel_Minutes.SuspendLayout();
            this.panel_Hours.SuspendLayout();
            this.panel_Seconds.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Arrow_Down
            // 
            resources.ApplyResources(this.pictureBox_Arrow_Down, "pictureBox_Arrow_Down");
            this.pictureBox_Arrow_Down.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_down;
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
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.Controls.Add(this.panel_Format_24hour, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel_SmoothSeconds, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_Minutes, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_Hours, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_Seconds, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Format_24hour
            // 
            resources.ApplyResources(this.panel_Format_24hour, "panel_Format_24hour");
            this.panel_Format_24hour.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Format_24hour.Controls.Add(this.button_Format_24hour);
            this.panel_Format_24hour.Controls.Add(this.checkBox_Format_24hour);
            this.panel_Format_24hour.Name = "panel_Format_24hour";
            this.panel_Format_24hour.Click += new System.EventHandler(this.panel_Format_24hour_Click);
            this.panel_Format_24hour.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Format_24hour.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Format_24hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Format_24hour
            // 
            resources.ApplyResources(this.button_Format_24hour, "button_Format_24hour");
            this.button_Format_24hour.FlatAppearance.BorderSize = 0;
            this.button_Format_24hour.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Format_24hour.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Format_24hour.Image = global::ControlLibrary.Properties.Resources.toggle_on;
            this.button_Format_24hour.Name = "button_Format_24hour";
            this.button_Format_24hour.UseVisualStyleBackColor = true;
            this.button_Format_24hour.Click += new System.EventHandler(this.panel_Format_24hour_Click);
            this.button_Format_24hour.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Format_24hour.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Format_24hour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Format_24hour
            // 
            resources.ApplyResources(this.checkBox_Format_24hour, "checkBox_Format_24hour");
            this.checkBox_Format_24hour.Name = "checkBox_Format_24hour";
            this.checkBox_Format_24hour.UseVisualStyleBackColor = true;
            this.checkBox_Format_24hour.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_SmoothSeconds
            // 
            resources.ApplyResources(this.panel_SmoothSeconds, "panel_SmoothSeconds");
            this.panel_SmoothSeconds.BackColor = System.Drawing.SystemColors.Control;
            this.panel_SmoothSeconds.Controls.Add(this.button_SmoothSeconds);
            this.panel_SmoothSeconds.Controls.Add(this.checkBox_SmoothSeconds);
            this.panel_SmoothSeconds.Name = "panel_SmoothSeconds";
            this.panel_SmoothSeconds.Click += new System.EventHandler(this.panel_SmoothSeconds_Click);
            this.panel_SmoothSeconds.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_SmoothSeconds.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_SmoothSeconds.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_SmoothSeconds
            // 
            resources.ApplyResources(this.button_SmoothSeconds, "button_SmoothSeconds");
            this.button_SmoothSeconds.FlatAppearance.BorderSize = 0;
            this.button_SmoothSeconds.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_SmoothSeconds.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_SmoothSeconds.Image = global::ControlLibrary.Properties.Resources.toggle_on;
            this.button_SmoothSeconds.Name = "button_SmoothSeconds";
            this.button_SmoothSeconds.UseVisualStyleBackColor = true;
            this.button_SmoothSeconds.Click += new System.EventHandler(this.panel_SmoothSeconds_Click);
            this.button_SmoothSeconds.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_SmoothSeconds.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_SmoothSeconds.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_SmoothSeconds
            // 
            resources.ApplyResources(this.checkBox_SmoothSeconds, "checkBox_SmoothSeconds");
            this.checkBox_SmoothSeconds.Name = "checkBox_SmoothSeconds";
            this.checkBox_SmoothSeconds.UseVisualStyleBackColor = true;
            this.checkBox_SmoothSeconds.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Minutes
            // 
            resources.ApplyResources(this.panel_Minutes, "panel_Minutes");
            this.panel_Minutes.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Minutes.Controls.Add(this.button_Minutes);
            this.panel_Minutes.Controls.Add(this.checkBox_Minutes);
            this.panel_Minutes.Name = "panel_Minutes";
            this.panel_Minutes.Click += new System.EventHandler(this.panel_Minutes_Click);
            this.panel_Minutes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Minutes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Minutes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Minutes
            // 
            resources.ApplyResources(this.button_Minutes, "button_Minutes");
            this.button_Minutes.FlatAppearance.BorderSize = 0;
            this.button_Minutes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Minutes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Minutes.Image = global::ControlLibrary.Properties.Resources.pointer;
            this.button_Minutes.Name = "button_Minutes";
            this.button_Minutes.UseVisualStyleBackColor = true;
            this.button_Minutes.Click += new System.EventHandler(this.panel_Minutes_Click);
            this.button_Minutes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Minutes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Minutes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Minutes
            // 
            resources.ApplyResources(this.checkBox_Minutes, "checkBox_Minutes");
            this.checkBox_Minutes.Name = "checkBox_Minutes";
            this.checkBox_Minutes.UseVisualStyleBackColor = true;
            this.checkBox_Minutes.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Hours
            // 
            resources.ApplyResources(this.panel_Hours, "panel_Hours");
            this.panel_Hours.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Hours.Controls.Add(this.checkBox_Hours);
            this.panel_Hours.Controls.Add(this.button_Hours);
            this.panel_Hours.Name = "panel_Hours";
            this.panel_Hours.Click += new System.EventHandler(this.panel_Hours_Click);
            this.panel_Hours.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Hours.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Hours.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Hours
            // 
            resources.ApplyResources(this.checkBox_Hours, "checkBox_Hours");
            this.checkBox_Hours.Name = "checkBox_Hours";
            this.checkBox_Hours.UseVisualStyleBackColor = true;
            this.checkBox_Hours.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_Hours
            // 
            resources.ApplyResources(this.button_Hours, "button_Hours");
            this.button_Hours.FlatAppearance.BorderSize = 0;
            this.button_Hours.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Hours.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Hours.Image = global::ControlLibrary.Properties.Resources.pointer;
            this.button_Hours.Name = "button_Hours";
            this.button_Hours.UseVisualStyleBackColor = true;
            this.button_Hours.Click += new System.EventHandler(this.panel_Hours_Click);
            this.button_Hours.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Hours.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Hours.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_Seconds
            // 
            resources.ApplyResources(this.panel_Seconds, "panel_Seconds");
            this.panel_Seconds.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Seconds.Controls.Add(this.button_Seconds);
            this.panel_Seconds.Controls.Add(this.checkBox_Seconds);
            this.panel_Seconds.Name = "panel_Seconds";
            this.panel_Seconds.Click += new System.EventHandler(this.panel_Seconds_Click);
            this.panel_Seconds.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Seconds.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Seconds.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Seconds
            // 
            resources.ApplyResources(this.button_Seconds, "button_Seconds");
            this.button_Seconds.FlatAppearance.BorderSize = 0;
            this.button_Seconds.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Seconds.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Seconds.Name = "button_Seconds";
            this.button_Seconds.UseVisualStyleBackColor = true;
            this.button_Seconds.Click += new System.EventHandler(this.panel_Seconds_Click);
            this.button_Seconds.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Seconds.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Seconds.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Seconds
            // 
            resources.ApplyResources(this.checkBox_Seconds, "checkBox_Seconds");
            this.checkBox_Seconds.Name = "checkBox_Seconds";
            this.checkBox_Seconds.UseVisualStyleBackColor = true;
            this.checkBox_Seconds.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_ElementName
            // 
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.analog_time_18;
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // UCtrl_AnalogTimePro_Elm
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
            this.Name = "UCtrl_AnalogTimePro_Elm";
            this.Load += new System.EventHandler(this.UCtrl_AnalogTimePro_Elm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Format_24hour.ResumeLayout(false);
            this.panel_Format_24hour.PerformLayout();
            this.panel_SmoothSeconds.ResumeLayout(false);
            this.panel_SmoothSeconds.PerformLayout();
            this.panel_Minutes.ResumeLayout(false);
            this.panel_Minutes.PerformLayout();
            this.panel_Hours.ResumeLayout(false);
            this.panel_Hours.PerformLayout();
            this.panel_Seconds.ResumeLayout(false);
            this.panel_Seconds.PerformLayout();
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
        private System.Windows.Forms.Panel panel_Minutes;
        private System.Windows.Forms.Button button_Minutes;
        public System.Windows.Forms.CheckBox checkBox_Minutes;
        private System.Windows.Forms.Panel panel_Hours;
        public System.Windows.Forms.CheckBox checkBox_Hours;
        private System.Windows.Forms.Button button_Hours;
        private System.Windows.Forms.Panel panel_Seconds;
        private System.Windows.Forms.Button button_Seconds;
        public System.Windows.Forms.CheckBox checkBox_Seconds;
        private System.Windows.Forms.Button button_ElementName;
        private System.Windows.Forms.Panel panel_SmoothSeconds;
        private System.Windows.Forms.Button button_SmoothSeconds;
        public System.Windows.Forms.CheckBox checkBox_SmoothSeconds;
        private System.Windows.Forms.Panel panel_Format_24hour;
        private System.Windows.Forms.Button button_Format_24hour;
        public System.Windows.Forms.CheckBox checkBox_Format_24hour;
    }
}
