namespace ControlLibrary
{
    partial class UCtrl_DisconnectAlert_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_DisconnectAlert_Opt));
            this.groupBox_disconneсt = new System.Windows.Forms.GroupBox();
            this.textBox_disconneсt_toast_text = new System.Windows.Forms.TextBox();
            this.checkBox_disconneсt_toast = new System.Windows.Forms.CheckBox();
            this.comboBox_disconneсt_vibrate_type = new System.Windows.Forms.ComboBox();
            this.label_disconneсt_vibrate = new System.Windows.Forms.Label();
            this.checkBox_disconneсt_vibrate = new System.Windows.Forms.CheckBox();
            this.groupBox_conneсt = new System.Windows.Forms.GroupBox();
            this.textBox_conneсt_toast_text = new System.Windows.Forms.TextBox();
            this.checkBox_conneсt_toast = new System.Windows.Forms.CheckBox();
            this.comboBox_conneсt_vibrate_type = new System.Windows.Forms.ComboBox();
            this.label_conneсt_vibrate = new System.Windows.Forms.Label();
            this.checkBox_conneсt_vibrate = new System.Windows.Forms.CheckBox();
            this.label_hint = new System.Windows.Forms.Label();
            this.groupBox_disconneсt.SuspendLayout();
            this.groupBox_conneсt.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_disconneсt
            // 
            resources.ApplyResources(this.groupBox_disconneсt, "groupBox_disconneсt");
            this.groupBox_disconneсt.Controls.Add(this.textBox_disconneсt_toast_text);
            this.groupBox_disconneсt.Controls.Add(this.checkBox_disconneсt_toast);
            this.groupBox_disconneсt.Controls.Add(this.comboBox_disconneсt_vibrate_type);
            this.groupBox_disconneсt.Controls.Add(this.label_disconneсt_vibrate);
            this.groupBox_disconneсt.Controls.Add(this.checkBox_disconneсt_vibrate);
            this.groupBox_disconneсt.Name = "groupBox_disconneсt";
            this.groupBox_disconneсt.TabStop = false;
            this.groupBox_disconneсt.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // textBox_disconneсt_toast_text
            // 
            resources.ApplyResources(this.textBox_disconneсt_toast_text, "textBox_disconneсt_toast_text");
            this.textBox_disconneсt_toast_text.Name = "textBox_disconneсt_toast_text";
            this.textBox_disconneсt_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_disconneсt_toast
            // 
            resources.ApplyResources(this.checkBox_disconneсt_toast, "checkBox_disconneсt_toast");
            this.checkBox_disconneсt_toast.Name = "checkBox_disconneсt_toast";
            this.checkBox_disconneсt_toast.UseVisualStyleBackColor = true;
            this.checkBox_disconneсt_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_disconneсt_toast.CheckStateChanged += new System.EventHandler(this.checkBox_disconneсt_toast_CheckStateChanged);
            // 
            // comboBox_disconneсt_vibrate_type
            // 
            resources.ApplyResources(this.comboBox_disconneсt_vibrate_type, "comboBox_disconneсt_vibrate_type");
            this.comboBox_disconneсt_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_disconneсt_vibrate_type.DropDownWidth = 170;
            this.comboBox_disconneсt_vibrate_type.FormattingEnabled = true;
            this.comboBox_disconneсt_vibrate_type.Items.AddRange(new object[] {
            resources.GetString("comboBox_disconneсt_vibrate_type.Items"),
            resources.GetString("comboBox_disconneсt_vibrate_type.Items1"),
            resources.GetString("comboBox_disconneсt_vibrate_type.Items2"),
            resources.GetString("comboBox_disconneсt_vibrate_type.Items3")});
            this.comboBox_disconneсt_vibrate_type.Name = "comboBox_disconneсt_vibrate_type";
            this.comboBox_disconneсt_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_disconneсt_vibrate
            // 
            resources.ApplyResources(this.label_disconneсt_vibrate, "label_disconneсt_vibrate");
            this.label_disconneсt_vibrate.Name = "label_disconneсt_vibrate";
            // 
            // checkBox_disconneсt_vibrate
            // 
            resources.ApplyResources(this.checkBox_disconneсt_vibrate, "checkBox_disconneсt_vibrate");
            this.checkBox_disconneсt_vibrate.Name = "checkBox_disconneсt_vibrate";
            this.checkBox_disconneсt_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_disconneсt_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_disconneсt_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_disconneсt_vibrate_CheckStateChanged);
            // 
            // groupBox_conneсt
            // 
            resources.ApplyResources(this.groupBox_conneсt, "groupBox_conneсt");
            this.groupBox_conneсt.Controls.Add(this.textBox_conneсt_toast_text);
            this.groupBox_conneсt.Controls.Add(this.checkBox_conneсt_toast);
            this.groupBox_conneсt.Controls.Add(this.comboBox_conneсt_vibrate_type);
            this.groupBox_conneсt.Controls.Add(this.label_conneсt_vibrate);
            this.groupBox_conneсt.Controls.Add(this.checkBox_conneсt_vibrate);
            this.groupBox_conneсt.Name = "groupBox_conneсt";
            this.groupBox_conneсt.TabStop = false;
            this.groupBox_conneсt.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // textBox_conneсt_toast_text
            // 
            resources.ApplyResources(this.textBox_conneсt_toast_text, "textBox_conneсt_toast_text");
            this.textBox_conneсt_toast_text.Name = "textBox_conneсt_toast_text";
            this.textBox_conneсt_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_conneсt_toast
            // 
            resources.ApplyResources(this.checkBox_conneсt_toast, "checkBox_conneсt_toast");
            this.checkBox_conneсt_toast.Name = "checkBox_conneсt_toast";
            this.checkBox_conneсt_toast.UseVisualStyleBackColor = true;
            this.checkBox_conneсt_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_conneсt_toast.CheckStateChanged += new System.EventHandler(this.checkBox_conneсt_toast_CheckStateChanged);
            // 
            // comboBox_conneсt_vibrate_type
            // 
            resources.ApplyResources(this.comboBox_conneсt_vibrate_type, "comboBox_conneсt_vibrate_type");
            this.comboBox_conneсt_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_conneсt_vibrate_type.DropDownWidth = 170;
            this.comboBox_conneсt_vibrate_type.FormattingEnabled = true;
            this.comboBox_conneсt_vibrate_type.Items.AddRange(new object[] {
            resources.GetString("comboBox_conneсt_vibrate_type.Items"),
            resources.GetString("comboBox_conneсt_vibrate_type.Items1"),
            resources.GetString("comboBox_conneсt_vibrate_type.Items2"),
            resources.GetString("comboBox_conneсt_vibrate_type.Items3")});
            this.comboBox_conneсt_vibrate_type.Name = "comboBox_conneсt_vibrate_type";
            this.comboBox_conneсt_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_conneсt_vibrate
            // 
            resources.ApplyResources(this.label_conneсt_vibrate, "label_conneсt_vibrate");
            this.label_conneсt_vibrate.Name = "label_conneсt_vibrate";
            // 
            // checkBox_conneсt_vibrate
            // 
            resources.ApplyResources(this.checkBox_conneсt_vibrate, "checkBox_conneсt_vibrate");
            this.checkBox_conneсt_vibrate.Name = "checkBox_conneсt_vibrate";
            this.checkBox_conneсt_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_conneсt_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_conneсt_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_conneсt_vibrate_CheckStateChanged);
            // 
            // label_hint
            // 
            resources.ApplyResources(this.label_hint, "label_hint");
            this.label_hint.Name = "label_hint";
            // 
            // UCtrl_DisconnectAlert_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_hint);
            this.Controls.Add(this.groupBox_conneсt);
            this.Controls.Add(this.groupBox_disconneсt);
            this.Name = "UCtrl_DisconnectAlert_Opt";
            this.groupBox_disconneсt.ResumeLayout(false);
            this.groupBox_disconneсt.PerformLayout();
            this.groupBox_conneсt.ResumeLayout(false);
            this.groupBox_conneсt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_disconneсt;
        private System.Windows.Forms.Label label_disconneсt_vibrate;
        private System.Windows.Forms.ComboBox comboBox_disconneсt_vibrate_type;
        private System.Windows.Forms.GroupBox groupBox_conneсt;
        private System.Windows.Forms.ComboBox comboBox_conneсt_vibrate_type;
        private System.Windows.Forms.Label label_conneсt_vibrate;
        private System.Windows.Forms.Label label_hint;
        public System.Windows.Forms.CheckBox checkBox_disconneсt_vibrate;
        public System.Windows.Forms.TextBox textBox_disconneсt_toast_text;
        public System.Windows.Forms.CheckBox checkBox_disconneсt_toast;
        public System.Windows.Forms.TextBox textBox_conneсt_toast_text;
        public System.Windows.Forms.CheckBox checkBox_conneсt_toast;
        public System.Windows.Forms.CheckBox checkBox_conneсt_vibrate;
    }
}
