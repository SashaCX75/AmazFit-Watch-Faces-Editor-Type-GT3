
namespace ControlLibrary
{
    partial class UCtrl_Statuses_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Statuses_Elm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Bluetooth = new System.Windows.Forms.Panel();
            this.button_Bluetooth = new System.Windows.Forms.Button();
            this.checkBox_Bluetooth = new System.Windows.Forms.CheckBox();
            this.panel_Lock = new System.Windows.Forms.Panel();
            this.button_Lock = new System.Windows.Forms.Button();
            this.checkBox_Lock = new System.Windows.Forms.CheckBox();
            this.panel_DND = new System.Windows.Forms.Panel();
            this.checkBox_DND = new System.Windows.Forms.CheckBox();
            this.button_DND = new System.Windows.Forms.Button();
            this.panel_Alarm = new System.Windows.Forms.Panel();
            this.button_Alarm = new System.Windows.Forms.Button();
            this.checkBox_Alarm = new System.Windows.Forms.CheckBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Bluetooth.SuspendLayout();
            this.panel_Lock.SuspendLayout();
            this.panel_DND.SuspendLayout();
            this.panel_Alarm.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.panel_Bluetooth, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_Lock, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_DND, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_Alarm, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Bluetooth
            // 
            resources.ApplyResources(this.panel_Bluetooth, "panel_Bluetooth");
            this.panel_Bluetooth.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Bluetooth.Controls.Add(this.button_Bluetooth);
            this.panel_Bluetooth.Controls.Add(this.checkBox_Bluetooth);
            this.panel_Bluetooth.Name = "panel_Bluetooth";
            this.panel_Bluetooth.Click += new System.EventHandler(this.panel_Bluetooth_Click);
            this.panel_Bluetooth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Bluetooth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Bluetooth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Bluetooth
            // 
            resources.ApplyResources(this.button_Bluetooth, "button_Bluetooth");
            this.button_Bluetooth.FlatAppearance.BorderSize = 0;
            this.button_Bluetooth.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Bluetooth.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Bluetooth.Image = global::ControlLibrary.Properties.Resources.edit_attributes;
            this.button_Bluetooth.Name = "button_Bluetooth";
            this.button_Bluetooth.UseVisualStyleBackColor = true;
            this.button_Bluetooth.Click += new System.EventHandler(this.panel_Bluetooth_Click);
            this.button_Bluetooth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Bluetooth.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Bluetooth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Bluetooth
            // 
            resources.ApplyResources(this.checkBox_Bluetooth, "checkBox_Bluetooth");
            this.checkBox_Bluetooth.Name = "checkBox_Bluetooth";
            this.checkBox_Bluetooth.UseVisualStyleBackColor = true;
            this.checkBox_Bluetooth.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Lock
            // 
            resources.ApplyResources(this.panel_Lock, "panel_Lock");
            this.panel_Lock.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Lock.Controls.Add(this.button_Lock);
            this.panel_Lock.Controls.Add(this.checkBox_Lock);
            this.panel_Lock.Name = "panel_Lock";
            this.panel_Lock.Click += new System.EventHandler(this.panel_Lock_Click);
            this.panel_Lock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Lock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Lock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Lock
            // 
            resources.ApplyResources(this.button_Lock, "button_Lock");
            this.button_Lock.FlatAppearance.BorderSize = 0;
            this.button_Lock.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Lock.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Lock.Image = global::ControlLibrary.Properties.Resources.edit_attributes;
            this.button_Lock.Name = "button_Lock";
            this.button_Lock.UseVisualStyleBackColor = true;
            this.button_Lock.Click += new System.EventHandler(this.panel_Lock_Click);
            this.button_Lock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Lock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Lock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Lock
            // 
            resources.ApplyResources(this.checkBox_Lock, "checkBox_Lock");
            this.checkBox_Lock.Name = "checkBox_Lock";
            this.checkBox_Lock.UseVisualStyleBackColor = true;
            this.checkBox_Lock.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_DND
            // 
            resources.ApplyResources(this.panel_DND, "panel_DND");
            this.panel_DND.BackColor = System.Drawing.SystemColors.Control;
            this.panel_DND.Controls.Add(this.checkBox_DND);
            this.panel_DND.Controls.Add(this.button_DND);
            this.panel_DND.Name = "panel_DND";
            this.panel_DND.Click += new System.EventHandler(this.panel_DND_Click);
            this.panel_DND.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_DND.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_DND.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_DND
            // 
            resources.ApplyResources(this.checkBox_DND, "checkBox_DND");
            this.checkBox_DND.Name = "checkBox_DND";
            this.checkBox_DND.UseVisualStyleBackColor = true;
            this.checkBox_DND.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_DND
            // 
            resources.ApplyResources(this.button_DND, "button_DND");
            this.button_DND.FlatAppearance.BorderSize = 0;
            this.button_DND.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_DND.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_DND.Image = global::ControlLibrary.Properties.Resources.edit_attributes;
            this.button_DND.Name = "button_DND";
            this.button_DND.UseVisualStyleBackColor = true;
            this.button_DND.Click += new System.EventHandler(this.panel_DND_Click);
            this.button_DND.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_DND.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_DND.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_Alarm
            // 
            resources.ApplyResources(this.panel_Alarm, "panel_Alarm");
            this.panel_Alarm.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Alarm.Controls.Add(this.button_Alarm);
            this.panel_Alarm.Controls.Add(this.checkBox_Alarm);
            this.panel_Alarm.Name = "panel_Alarm";
            this.panel_Alarm.Click += new System.EventHandler(this.panel_Alarm_Click);
            this.panel_Alarm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Alarm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Alarm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Alarm
            // 
            resources.ApplyResources(this.button_Alarm, "button_Alarm");
            this.button_Alarm.FlatAppearance.BorderSize = 0;
            this.button_Alarm.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Alarm.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Alarm.Image = global::ControlLibrary.Properties.Resources.edit_attributes;
            this.button_Alarm.Name = "button_Alarm";
            this.button_Alarm.UseVisualStyleBackColor = true;
            this.button_Alarm.Click += new System.EventHandler(this.panel_Alarm_Click);
            this.button_Alarm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Alarm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Alarm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Alarm
            // 
            resources.ApplyResources(this.checkBox_Alarm, "checkBox_Alarm");
            this.checkBox_Alarm.Name = "checkBox_Alarm";
            this.checkBox_Alarm.UseVisualStyleBackColor = true;
            this.checkBox_Alarm.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
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
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.on_off;
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // UCtrl_Statuses_Elm
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
            this.Name = "UCtrl_Statuses_Elm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Bluetooth.ResumeLayout(false);
            this.panel_Bluetooth.PerformLayout();
            this.panel_Lock.ResumeLayout(false);
            this.panel_Lock.PerformLayout();
            this.panel_DND.ResumeLayout(false);
            this.panel_DND.PerformLayout();
            this.panel_Alarm.ResumeLayout(false);
            this.panel_Alarm.PerformLayout();
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
        private System.Windows.Forms.Panel panel_Bluetooth;
        private System.Windows.Forms.Button button_Bluetooth;
        public System.Windows.Forms.CheckBox checkBox_Bluetooth;
        private System.Windows.Forms.Panel panel_Lock;
        private System.Windows.Forms.Button button_Lock;
        public System.Windows.Forms.CheckBox checkBox_Lock;
        private System.Windows.Forms.Panel panel_DND;
        public System.Windows.Forms.CheckBox checkBox_DND;
        private System.Windows.Forms.Button button_DND;
        private System.Windows.Forms.Panel panel_Alarm;
        private System.Windows.Forms.Button button_Alarm;
        public System.Windows.Forms.CheckBox checkBox_Alarm;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Down;
        private System.Windows.Forms.Button button_ElementName;
    }
}
