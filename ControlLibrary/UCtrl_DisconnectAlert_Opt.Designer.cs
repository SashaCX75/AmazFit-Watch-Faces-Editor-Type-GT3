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
            this.groupBox_disconneсnt.Controls.Add(this.textBox_disconneсnt_toast_text);
            this.groupBox_disconneсnt.Controls.Add(this.checkBox_disconneсnt_toast);
            this.groupBox_disconneсnt.Controls.Add(this.comboBox_disconneсnt_vibrate_type);
            this.groupBox_disconneсnt.Controls.Add(this.label_disconneсnt_vibrate);
            this.groupBox_disconneсnt.Controls.Add(this.checkBox_disconneсnt_vibrate);
            this.groupBox_disconneсnt.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_disconneсnt.Location = new System.Drawing.Point(3, 0);
            this.groupBox_disconneсnt.Name = "groupBox_disconneсnt";
            this.groupBox_disconneсnt.Size = new System.Drawing.Size(249, 100);
            this.groupBox_disconneсnt.TabIndex = 0;
            this.groupBox_disconneсnt.TabStop = false;
            this.groupBox_disconneсnt.Text = "Связь потеряна";
            this.groupBox_disconneсnt.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // textBox_disconneсnt_toast_text
            // 
            this.textBox_disconneсnt_toast_text.Enabled = false;
            this.textBox_disconneсnt_toast_text.Location = new System.Drawing.Point(122, 41);
            this.textBox_disconneсnt_toast_text.Multiline = true;
            this.textBox_disconneсnt_toast_text.Name = "textBox_disconneсnt_toast_text";
            this.textBox_disconneсnt_toast_text.Size = new System.Drawing.Size(120, 50);
            this.textBox_disconneсnt_toast_text.TabIndex = 166;
            this.textBox_disconneсnt_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_disconneсnt_toast
            // 
            this.checkBox_disconneсnt_toast.AutoSize = true;
            this.checkBox_disconneсnt_toast.Location = new System.Drawing.Point(122, 19);
            this.checkBox_disconneсnt_toast.Name = "checkBox_disconneсnt_toast";
            this.checkBox_disconneсnt_toast.Size = new System.Drawing.Size(84, 17);
            this.checkBox_disconneсnt_toast.TabIndex = 165;
            this.checkBox_disconneсnt_toast.Text = "Сообщение";
            this.checkBox_disconneсnt_toast.UseVisualStyleBackColor = true;
            this.checkBox_disconneсnt_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_disconneсnt_toast.CheckStateChanged += new System.EventHandler(this.checkBox_disconneсnt_toast_CheckStateChanged);
            // 
            // comboBox_disconneсnt_vibrate_type
            // 
            this.comboBox_disconneсnt_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_disconneсnt_vibrate_type.DropDownWidth = 170;
            this.comboBox_disconneсnt_vibrate_type.Enabled = false;
            this.comboBox_disconneсnt_vibrate_type.FormattingEnabled = true;
            this.comboBox_disconneсnt_vibrate_type.Items.AddRange(new object[] {
            "Короткая",
            "Средняя",
            "Длинная",
            "Длинная (непрерывная)"});
            this.comboBox_disconneсnt_vibrate_type.Location = new System.Drawing.Point(6, 70);
            this.comboBox_disconneсnt_vibrate_type.Name = "comboBox_disconneсnt_vibrate_type";
            this.comboBox_disconneсnt_vibrate_type.Size = new System.Drawing.Size(110, 21);
            this.comboBox_disconneсnt_vibrate_type.TabIndex = 164;
            this.comboBox_disconneсnt_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_disconneсnt_vibrate
            // 
            this.label_disconneсnt_vibrate.AutoSize = true;
            this.label_disconneсnt_vibrate.Enabled = false;
            this.label_disconneсnt_vibrate.Location = new System.Drawing.Point(3, 54);
            this.label_disconneсnt_vibrate.Name = "label_disconneсnt_vibrate";
            this.label_disconneсnt_vibrate.Size = new System.Drawing.Size(77, 13);
            this.label_disconneсnt_vibrate.TabIndex = 1;
            this.label_disconneсnt_vibrate.Text = "Тип вибрации";
            // 
            // checkBox_disconneсnt_vibrate
            // 
            this.checkBox_disconneсnt_vibrate.AutoSize = true;
            this.checkBox_disconneсnt_vibrate.Location = new System.Drawing.Point(6, 19);
            this.checkBox_disconneсnt_vibrate.Name = "checkBox_disconneсnt_vibrate";
            this.checkBox_disconneсnt_vibrate.Size = new System.Drawing.Size(75, 17);
            this.checkBox_disconneсnt_vibrate.TabIndex = 0;
            this.checkBox_disconneсnt_vibrate.Text = "Вибрация";
            this.checkBox_disconneсnt_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_disconneсnt_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_disconneсnt_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_disconneсnt_vibrate_CheckStateChanged);
            // 
            // groupBox_conneсnt
            // 
            this.groupBox_conneсnt.Controls.Add(this.textBox_conneсnt_toast_text);
            this.groupBox_conneсnt.Controls.Add(this.checkBox_conneсnt_toast);
            this.groupBox_conneсnt.Controls.Add(this.comboBox_conneсnt_vibrate_type);
            this.groupBox_conneсnt.Controls.Add(this.label_conneсnt_vibrate);
            this.groupBox_conneсnt.Controls.Add(this.checkBox_conneсnt_vibrate);
            this.groupBox_conneсnt.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_conneсnt.Location = new System.Drawing.Point(3, 100);
            this.groupBox_conneсnt.Name = "groupBox_conneсnt";
            this.groupBox_conneсnt.Size = new System.Drawing.Size(249, 100);
            this.groupBox_conneсnt.TabIndex = 1;
            this.groupBox_conneсnt.TabStop = false;
            this.groupBox_conneсnt.Text = "Связь востановлена";
            this.groupBox_conneсnt.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // textBox_conneсnt_toast_text
            // 
            this.textBox_conneсnt_toast_text.Enabled = false;
            this.textBox_conneсnt_toast_text.Location = new System.Drawing.Point(122, 41);
            this.textBox_conneсnt_toast_text.Multiline = true;
            this.textBox_conneсnt_toast_text.Name = "textBox_conneсnt_toast_text";
            this.textBox_conneсnt_toast_text.Size = new System.Drawing.Size(120, 50);
            this.textBox_conneсnt_toast_text.TabIndex = 166;
            this.textBox_conneсnt_toast_text.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // checkBox_conneсnt_toast
            // 
            this.checkBox_conneсnt_toast.AutoSize = true;
            this.checkBox_conneсnt_toast.Location = new System.Drawing.Point(122, 19);
            this.checkBox_conneсnt_toast.Name = "checkBox_conneсnt_toast";
            this.checkBox_conneсnt_toast.Size = new System.Drawing.Size(84, 17);
            this.checkBox_conneсnt_toast.TabIndex = 165;
            this.checkBox_conneсnt_toast.Text = "Сообщение";
            this.checkBox_conneсnt_toast.UseVisualStyleBackColor = true;
            this.checkBox_conneсnt_toast.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_conneсnt_toast.CheckStateChanged += new System.EventHandler(this.checkBox_conneсnt_toast_CheckStateChanged);
            // 
            // comboBox_conneсnt_vibrate_type
            // 
            this.comboBox_conneсnt_vibrate_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_conneсnt_vibrate_type.DropDownWidth = 170;
            this.comboBox_conneсnt_vibrate_type.Enabled = false;
            this.comboBox_conneсnt_vibrate_type.FormattingEnabled = true;
            this.comboBox_conneсnt_vibrate_type.Items.AddRange(new object[] {
            "Короткая",
            "Средняя",
            "Длинная",
            "Длинная (непрерывная)"});
            this.comboBox_conneсnt_vibrate_type.Location = new System.Drawing.Point(6, 70);
            this.comboBox_conneсnt_vibrate_type.Name = "comboBox_conneсnt_vibrate_type";
            this.comboBox_conneсnt_vibrate_type.Size = new System.Drawing.Size(110, 21);
            this.comboBox_conneсnt_vibrate_type.TabIndex = 164;
            this.comboBox_conneсnt_vibrate_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label_conneсnt_vibrate
            // 
            this.label_conneсnt_vibrate.AutoSize = true;
            this.label_conneсnt_vibrate.Enabled = false;
            this.label_conneсnt_vibrate.Location = new System.Drawing.Point(3, 54);
            this.label_conneсnt_vibrate.Name = "label_conneсnt_vibrate";
            this.label_conneсnt_vibrate.Size = new System.Drawing.Size(77, 13);
            this.label_conneсnt_vibrate.TabIndex = 1;
            this.label_conneсnt_vibrate.Text = "Тип вибрации";
            // 
            // checkBox_conneсnt_vibrate
            // 
            this.checkBox_conneсnt_vibrate.AutoSize = true;
            this.checkBox_conneсnt_vibrate.Location = new System.Drawing.Point(6, 19);
            this.checkBox_conneсnt_vibrate.Name = "checkBox_conneсnt_vibrate";
            this.checkBox_conneсnt_vibrate.Size = new System.Drawing.Size(75, 17);
            this.checkBox_conneсnt_vibrate.TabIndex = 0;
            this.checkBox_conneсnt_vibrate.Text = "Вибрация";
            this.checkBox_conneсnt_vibrate.UseVisualStyleBackColor = true;
            this.checkBox_conneсnt_vibrate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            this.checkBox_conneсnt_vibrate.CheckStateChanged += new System.EventHandler(this.checkBox_conneсnt_vibrate_CheckStateChanged);
            // 
            // label_hint
            // 
            this.label_hint.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_hint.Location = new System.Drawing.Point(3, 200);
            this.label_hint.Name = "label_hint";
            this.label_hint.Size = new System.Drawing.Size(249, 34);
            this.label_hint.TabIndex = 2;
            this.label_hint.Text = "Для работы данной функции на часах должен быть включен AOD.";
            // 
            // UCtrl_DisconnectAlert_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_hint);
            this.Controls.Add(this.groupBox_conneсnt);
            this.Controls.Add(this.groupBox_disconneсnt);
            this.Name = "UCtrl_DisconnectAlert_Opt";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Size = new System.Drawing.Size(255, 279);
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
