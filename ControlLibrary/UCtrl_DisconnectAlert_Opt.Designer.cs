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
            this.groupBox_disconneсnt = new System.Windows.Forms.GroupBox();
            this.textBox_disconneсnt_toast_text = new System.Windows.Forms.TextBox();
            this.checkBox_disconneсnt_toast = new System.Windows.Forms.CheckBox();
            this.comboBox_disconneсnt_vibrate_type = new System.Windows.Forms.ComboBox();
            this.label_disconneсnt_vibrate = new System.Windows.Forms.Label();
            this.checkBox_disconneсnt_vibrate = new System.Windows.Forms.CheckBox();
            this.groupBox_conneсnt = new System.Windows.Forms.GroupBox();
            this.textBox_conneсnt_toast_text = new System.Windows.Forms.TextBox();
            this.checkBox_conneсnt_toast = new System.Windows.Forms.CheckBox();
            this.comboBox_conneсnt_vibrate_type = new System.Windows.Forms.ComboBox();
            this.label_conneсnt_vibrate = new System.Windows.Forms.Label();
            this.checkBox_conneсnt_vibrate = new System.Windows.Forms.CheckBox();
            this.label_hint = new System.Windows.Forms.Label();
            this.groupBox_disconneсnt.SuspendLayout();
            this.groupBox_conneсnt.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_disconneсnt
            // 
            resources.ApplyResources(this.groupBox_disconneсnt, "groupBox_disconneсnt");
            this.groupBox_disconneсnt.Controls.Add(this.textBox_disconneсnt_toast_text);
            this.groupBox_disconneсnt.Controls.Add(this.checkBox_disconneсnt_toast);
            this.groupBox_disconneсnt.Controls.Add(this.comboBox_disconneсnt_vibrate_type);
            this.groupBox_disconneсnt.Controls.Add(this.label_disconneсnt_vibrate);
            this.groupBox_disconneсnt.Controls.Add(this.checkBox_disconneсnt_vibrate);
            this.groupBox_disconneсnt.Name = "groupBox_disconneсnt";
            this.groupBox_disconneсnt.TabStop = false;
            this.groupBox_disconneсnt.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // textBox_disconneсnt_toast_text
            // 
            resources.ApplyResources(this.textBox_disconneсnt_toast_text, "textBox_disconneсnt_toast_text");
            this.textBox_disconneсnt_toast_text.Name = "textBox_disconneсnt_toast_text";
            this.textBox_disconneсnt_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_disconneсnt_toast
            // 
            resources.ApplyResources(this.checkBox_disconneсnt_toast, "checkBox_disconneсnt_toast");
            this.checkBox_disconneсnt_toast.Name = "checkBox_disconneсnt_toast";
            this.checkBox_disconneсnt_toast.UseVisualStyleBackColor = true;
            this.checkBox_disconneсnt_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_disconneсnt_toast.CheckStateChanged += new System.EventHandler(this.checkBox_disconneсnt_toast_CheckStateChanged);
            // 
            // comboBox_disconneсnt_vibrate_type
            // 
            resources.ApplyResources(this.comboBox_disconneсnt_vibrate_type, "comboBox_disconneсnt_vibrate_type");
            this.comboBox_disconneсnt_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_disconneсnt_vibrate_type.DropDownWidth = 170;
            this.comboBox_disconneсnt_vibrate_type.FormattingEnabled = true;
            this.comboBox_disconneсnt_vibrate_type.Items.AddRange(new object[] {
            resources.GetString("comboBox_disconneсnt_vibrate_type.Items"),
            resources.GetString("comboBox_disconneсnt_vibrate_type.Items1"),
            resources.GetString("comboBox_disconneсnt_vibrate_type.Items2"),
            resources.GetString("comboBox_disconneсnt_vibrate_type.Items3")});
            this.comboBox_disconneсnt_vibrate_type.Name = "comboBox_disconneсnt_vibrate_type";
            this.comboBox_disconneсnt_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_disconneсnt_vibrate
            // 
            resources.ApplyResources(this.label_disconneсnt_vibrate, "label_disconneсnt_vibrate");
            this.label_disconneсnt_vibrate.Name = "label_disconneсnt_vibrate";
            // 
            // checkBox_disconneсnt_vibrate
            // 
            resources.ApplyResources(this.checkBox_disconneсnt_vibrate, "checkBox_disconneсnt_vibrate");
            this.checkBox_disconneсnt_vibrate.Name = "checkBox_disconneсnt_vibrate";
            this.checkBox_disconneсnt_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_disconneсnt_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_disconneсnt_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_disconneсnt_vibrate_CheckStateChanged);
            // 
            // groupBox_conneсnt
            // 
            resources.ApplyResources(this.groupBox_conneсnt, "groupBox_conneсnt");
            this.groupBox_conneсnt.Controls.Add(this.textBox_conneсnt_toast_text);
            this.groupBox_conneсnt.Controls.Add(this.checkBox_conneсnt_toast);
            this.groupBox_conneсnt.Controls.Add(this.comboBox_conneсnt_vibrate_type);
            this.groupBox_conneсnt.Controls.Add(this.label_conneсnt_vibrate);
            this.groupBox_conneсnt.Controls.Add(this.checkBox_conneсnt_vibrate);
            this.groupBox_conneсnt.Name = "groupBox_conneсnt";
            this.groupBox_conneсnt.TabStop = false;
            this.groupBox_conneсnt.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // textBox_conneсnt_toast_text
            // 
            resources.ApplyResources(this.textBox_conneсnt_toast_text, "textBox_conneсnt_toast_text");
            this.textBox_conneсnt_toast_text.Name = "textBox_conneсnt_toast_text";
            this.textBox_conneсnt_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_conneсnt_toast
            // 
            resources.ApplyResources(this.checkBox_conneсnt_toast, "checkBox_conneсnt_toast");
            this.checkBox_conneсnt_toast.Name = "checkBox_conneсnt_toast";
            this.checkBox_conneсnt_toast.UseVisualStyleBackColor = true;
            this.checkBox_conneсnt_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_conneсnt_toast.CheckStateChanged += new System.EventHandler(this.checkBox_conneсnt_toast_CheckStateChanged);
            // 
            // comboBox_conneсnt_vibrate_type
            // 
            resources.ApplyResources(this.comboBox_conneсnt_vibrate_type, "comboBox_conneсnt_vibrate_type");
            this.comboBox_conneсnt_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_conneсnt_vibrate_type.DropDownWidth = 170;
            this.comboBox_conneсnt_vibrate_type.FormattingEnabled = true;
            this.comboBox_conneсnt_vibrate_type.Items.AddRange(new object[] {
            resources.GetString("comboBox_conneсnt_vibrate_type.Items"),
            resources.GetString("comboBox_conneсnt_vibrate_type.Items1"),
            resources.GetString("comboBox_conneсnt_vibrate_type.Items2"),
            resources.GetString("comboBox_conneсnt_vibrate_type.Items3")});
            this.comboBox_conneсnt_vibrate_type.Name = "comboBox_conneсnt_vibrate_type";
            this.comboBox_conneсnt_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_conneсnt_vibrate
            // 
            resources.ApplyResources(this.label_conneсnt_vibrate, "label_conneсnt_vibrate");
            this.label_conneсnt_vibrate.Name = "label_conneсnt_vibrate";
            // 
            // checkBox_conneсnt_vibrate
            // 
            resources.ApplyResources(this.checkBox_conneсnt_vibrate, "checkBox_conneсnt_vibrate");
            this.checkBox_conneсnt_vibrate.Name = "checkBox_conneсnt_vibrate";
            this.checkBox_conneсnt_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_conneсnt_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_conneсnt_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_conneсnt_vibrate_CheckStateChanged);
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
            this.Controls.Add(this.groupBox_conneсnt);
            this.Controls.Add(this.groupBox_disconneсnt);
            this.Name = "UCtrl_DisconnectAlert_Opt";
            this.groupBox_disconneсnt.ResumeLayout(false);
            this.groupBox_disconneсnt.PerformLayout();
            this.groupBox_conneсnt.ResumeLayout(false);
            this.groupBox_conneсnt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_disconneсnt;
        private System.Windows.Forms.Label label_disconneсnt_vibrate;
        private System.Windows.Forms.ComboBox comboBox_disconneсnt_vibrate_type;
        private System.Windows.Forms.GroupBox groupBox_conneсnt;
        private System.Windows.Forms.ComboBox comboBox_conneсnt_vibrate_type;
        private System.Windows.Forms.Label label_conneсnt_vibrate;
        private System.Windows.Forms.Label label_hint;
        public System.Windows.Forms.CheckBox checkBox_disconneсnt_vibrate;
        public System.Windows.Forms.TextBox textBox_disconneсnt_toast_text;
        public System.Windows.Forms.CheckBox checkBox_disconneсnt_toast;
        public System.Windows.Forms.TextBox textBox_conneсnt_toast_text;
        public System.Windows.Forms.CheckBox checkBox_conneсnt_toast;
        public System.Windows.Forms.CheckBox checkBox_conneсnt_vibrate;
    }
}
