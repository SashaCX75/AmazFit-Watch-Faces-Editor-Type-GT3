namespace ControlLibrary
{
    partial class UCtrl_RepeatingAlert_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_RepeatingAlert_Opt));
            this.label_hint = new System.Windows.Forms.Label();
            this.groupBox_repeat_period = new System.Windows.Forms.GroupBox();
            this.comboBox_repeat_period_time = new System.Windows.Forms.ComboBox();
            this.label_repeat_period = new System.Windows.Forms.Label();
            this.textBox_repeat_period_toast_text = new System.Windows.Forms.TextBox();
            this.checkBox_repeat_period_toast = new System.Windows.Forms.CheckBox();
            this.comboBox_repeat_period_vibrate_type = new System.Windows.Forms.ComboBox();
            this.label_repeat_period_vibrate = new System.Windows.Forms.Label();
            this.checkBox_repeat_period_vibrate = new System.Windows.Forms.CheckBox();
            this.groupBox_hour = new System.Windows.Forms.GroupBox();
            this.textBox_hour_toast_text = new System.Windows.Forms.TextBox();
            this.checkBox_hour_toast = new System.Windows.Forms.CheckBox();
            this.comboBox_hour_vibrate_type = new System.Windows.Forms.ComboBox();
            this.label_hour_vibrate = new System.Windows.Forms.Label();
            this.checkBox_hour_vibrate = new System.Windows.Forms.CheckBox();
            this.groupBox_repeat_period.SuspendLayout();
            this.groupBox_hour.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_hint
            // 
            resources.ApplyResources(this.label_hint, "label_hint");
            this.label_hint.Name = "label_hint";
            // 
            // groupBox_repeat_period
            // 
            resources.ApplyResources(this.groupBox_repeat_period, "groupBox_repeat_period");
            this.groupBox_repeat_period.Controls.Add(this.comboBox_repeat_period_time);
            this.groupBox_repeat_period.Controls.Add(this.label_repeat_period);
            this.groupBox_repeat_period.Controls.Add(this.textBox_repeat_period_toast_text);
            this.groupBox_repeat_period.Controls.Add(this.checkBox_repeat_period_toast);
            this.groupBox_repeat_period.Controls.Add(this.comboBox_repeat_period_vibrate_type);
            this.groupBox_repeat_period.Controls.Add(this.label_repeat_period_vibrate);
            this.groupBox_repeat_period.Controls.Add(this.checkBox_repeat_period_vibrate);
            this.groupBox_repeat_period.Name = "groupBox_repeat_period";
            this.groupBox_repeat_period.TabStop = false;
            this.groupBox_repeat_period.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // comboBox_repeat_period_time
            // 
            resources.ApplyResources(this.comboBox_repeat_period_time, "comboBox_repeat_period_time");
            this.comboBox_repeat_period_time.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_repeat_period_time.DropDownWidth = 170;
            this.comboBox_repeat_period_time.FormattingEnabled = true;
            this.comboBox_repeat_period_time.Items.AddRange(new object[] {
            resources.GetString("comboBox_repeat_period_time.Items"),
            resources.GetString("comboBox_repeat_period_time.Items1"),
            resources.GetString("comboBox_repeat_period_time.Items2"),
            resources.GetString("comboBox_repeat_period_time.Items3"),
            resources.GetString("comboBox_repeat_period_time.Items4")});
            this.comboBox_repeat_period_time.Name = "comboBox_repeat_period_time";
            this.comboBox_repeat_period_time.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_repeat_period
            // 
            resources.ApplyResources(this.label_repeat_period, "label_repeat_period");
            this.label_repeat_period.Name = "label_repeat_period";
            // 
            // textBox_repeat_period_toast_text
            // 
            resources.ApplyResources(this.textBox_repeat_period_toast_text, "textBox_repeat_period_toast_text");
            this.textBox_repeat_period_toast_text.Name = "textBox_repeat_period_toast_text";
            this.textBox_repeat_period_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_repeat_period_toast
            // 
            resources.ApplyResources(this.checkBox_repeat_period_toast, "checkBox_repeat_period_toast");
            this.checkBox_repeat_period_toast.Name = "checkBox_repeat_period_toast";
            this.checkBox_repeat_period_toast.UseVisualStyleBackColor = true;
            this.checkBox_repeat_period_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_repeat_period_toast.CheckStateChanged += new System.EventHandler(this.checkBox_repeat_period_toast_CheckStateChanged);
            // 
            // comboBox_repeat_period_vibrate_type
            // 
            resources.ApplyResources(this.comboBox_repeat_period_vibrate_type, "comboBox_repeat_period_vibrate_type");
            this.comboBox_repeat_period_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_repeat_period_vibrate_type.DropDownWidth = 170;
            this.comboBox_repeat_period_vibrate_type.FormattingEnabled = true;
            this.comboBox_repeat_period_vibrate_type.Items.AddRange(new object[] {
            resources.GetString("comboBox_repeat_period_vibrate_type.Items"),
            resources.GetString("comboBox_repeat_period_vibrate_type.Items1"),
            resources.GetString("comboBox_repeat_period_vibrate_type.Items2"),
            resources.GetString("comboBox_repeat_period_vibrate_type.Items3")});
            this.comboBox_repeat_period_vibrate_type.Name = "comboBox_repeat_period_vibrate_type";
            this.comboBox_repeat_period_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_repeat_period_vibrate
            // 
            resources.ApplyResources(this.label_repeat_period_vibrate, "label_repeat_period_vibrate");
            this.label_repeat_period_vibrate.Name = "label_repeat_period_vibrate";
            // 
            // checkBox_repeat_period_vibrate
            // 
            resources.ApplyResources(this.checkBox_repeat_period_vibrate, "checkBox_repeat_period_vibrate");
            this.checkBox_repeat_period_vibrate.Name = "checkBox_repeat_period_vibrate";
            this.checkBox_repeat_period_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_repeat_period_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_repeat_period_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_repeat_period_vibrate_CheckStateChanged);
            // 
            // groupBox_hour
            // 
            resources.ApplyResources(this.groupBox_hour, "groupBox_hour");
            this.groupBox_hour.Controls.Add(this.textBox_hour_toast_text);
            this.groupBox_hour.Controls.Add(this.checkBox_hour_toast);
            this.groupBox_hour.Controls.Add(this.comboBox_hour_vibrate_type);
            this.groupBox_hour.Controls.Add(this.label_hour_vibrate);
            this.groupBox_hour.Controls.Add(this.checkBox_hour_vibrate);
            this.groupBox_hour.Name = "groupBox_hour";
            this.groupBox_hour.TabStop = false;
            this.groupBox_hour.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // textBox_hour_toast_text
            // 
            resources.ApplyResources(this.textBox_hour_toast_text, "textBox_hour_toast_text");
            this.textBox_hour_toast_text.Name = "textBox_hour_toast_text";
            this.textBox_hour_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_hour_toast
            // 
            resources.ApplyResources(this.checkBox_hour_toast, "checkBox_hour_toast");
            this.checkBox_hour_toast.Name = "checkBox_hour_toast";
            this.checkBox_hour_toast.UseVisualStyleBackColor = true;
            this.checkBox_hour_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_hour_toast.CheckStateChanged += new System.EventHandler(this.checkBox_hour_toast_CheckStateChanged);
            // 
            // comboBox_hour_vibrate_type
            // 
            resources.ApplyResources(this.comboBox_hour_vibrate_type, "comboBox_hour_vibrate_type");
            this.comboBox_hour_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_hour_vibrate_type.DropDownWidth = 170;
            this.comboBox_hour_vibrate_type.FormattingEnabled = true;
            this.comboBox_hour_vibrate_type.Items.AddRange(new object[] {
            resources.GetString("comboBox_hour_vibrate_type.Items"),
            resources.GetString("comboBox_hour_vibrate_type.Items1"),
            resources.GetString("comboBox_hour_vibrate_type.Items2"),
            resources.GetString("comboBox_hour_vibrate_type.Items3")});
            this.comboBox_hour_vibrate_type.Name = "comboBox_hour_vibrate_type";
            this.comboBox_hour_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_hour_vibrate
            // 
            resources.ApplyResources(this.label_hour_vibrate, "label_hour_vibrate");
            this.label_hour_vibrate.Name = "label_hour_vibrate";
            // 
            // checkBox_hour_vibrate
            // 
            resources.ApplyResources(this.checkBox_hour_vibrate, "checkBox_hour_vibrate");
            this.checkBox_hour_vibrate.Name = "checkBox_hour_vibrate";
            this.checkBox_hour_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_hour_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_hour_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_hour_vibrate_CheckStateChanged);
            // 
            // UCtrl_RepeatingAlert_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_hint);
            this.Controls.Add(this.groupBox_repeat_period);
            this.Controls.Add(this.groupBox_hour);
            this.Name = "UCtrl_RepeatingAlert_Opt";
            this.groupBox_repeat_period.ResumeLayout(false);
            this.groupBox_repeat_period.PerformLayout();
            this.groupBox_hour.ResumeLayout(false);
            this.groupBox_hour.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_hint;
        private System.Windows.Forms.GroupBox groupBox_repeat_period;
        public System.Windows.Forms.TextBox textBox_repeat_period_toast_text;
        public System.Windows.Forms.CheckBox checkBox_repeat_period_toast;
        private System.Windows.Forms.ComboBox comboBox_repeat_period_vibrate_type;
        private System.Windows.Forms.Label label_repeat_period_vibrate;
        public System.Windows.Forms.CheckBox checkBox_repeat_period_vibrate;
        private System.Windows.Forms.GroupBox groupBox_hour;
        public System.Windows.Forms.TextBox textBox_hour_toast_text;
        public System.Windows.Forms.CheckBox checkBox_hour_toast;
        private System.Windows.Forms.ComboBox comboBox_hour_vibrate_type;
        private System.Windows.Forms.Label label_hour_vibrate;
        public System.Windows.Forms.CheckBox checkBox_hour_vibrate;
        private System.Windows.Forms.ComboBox comboBox_repeat_period_time;
        private System.Windows.Forms.Label label_repeat_period;
    }
}
