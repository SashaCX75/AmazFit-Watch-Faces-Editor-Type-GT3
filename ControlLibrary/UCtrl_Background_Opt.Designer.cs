
namespace ControlLibrary
{
    partial class UCtrl_Background_Opt
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
            this.button_GenerateID = new System.Windows.Forms.Button();
            this.radioButton_Background_color = new System.Windows.Forms.RadioButton();
            this.radioButton_Background_image = new System.Windows.Forms.RadioButton();
            this.comboBox_Background_color = new System.Windows.Forms.ComboBox();
            this.comboBox_Preview_image = new System.Windows.Forms.ComboBox();
            this.comboBox_Background_image = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_GenerateID
            // 
            this.button_GenerateID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_GenerateID.Location = new System.Drawing.Point(6, 143);
            this.button_GenerateID.Name = "button_GenerateID";
            this.button_GenerateID.Size = new System.Drawing.Size(144, 23);
            this.button_GenerateID.TabIndex = 64;
            this.button_GenerateID.Text = "Сгенерировать";
            this.button_GenerateID.UseVisualStyleBackColor = true;
            this.button_GenerateID.Click += new System.EventHandler(this.button_GenerateID_Click);
            // 
            // radioButton_Background_color
            // 
            this.radioButton_Background_color.AutoSize = true;
            this.radioButton_Background_color.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton_Background_color.Location = new System.Drawing.Point(141, 3);
            this.radioButton_Background_color.Name = "radioButton_Background_color";
            this.radioButton_Background_color.Size = new System.Drawing.Size(50, 17);
            this.radioButton_Background_color.TabIndex = 61;
            this.radioButton_Background_color.TabStop = true;
            this.radioButton_Background_color.Text = "Цвет";
            this.radioButton_Background_color.UseVisualStyleBackColor = true;
            // 
            // radioButton_Background_image
            // 
            this.radioButton_Background_image.AutoSize = true;
            this.radioButton_Background_image.Checked = true;
            this.radioButton_Background_image.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton_Background_image.Location = new System.Drawing.Point(6, 3);
            this.radioButton_Background_image.Name = "radioButton_Background_image";
            this.radioButton_Background_image.Size = new System.Drawing.Size(95, 17);
            this.radioButton_Background_image.TabIndex = 60;
            this.radioButton_Background_image.TabStop = true;
            this.radioButton_Background_image.Text = "Изображение";
            this.radioButton_Background_image.UseVisualStyleBackColor = true;
            this.radioButton_Background_image.CheckedChanged += new System.EventHandler(this.radioButton_Background_image_CheckedChanged);
            // 
            // comboBox_Background_color
            // 
            this.comboBox_Background_color.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_Background_color.DropDownHeight = 1;
            this.comboBox_Background_color.Enabled = false;
            this.comboBox_Background_color.FormattingEnabled = true;
            this.comboBox_Background_color.IntegralHeight = false;
            this.comboBox_Background_color.Location = new System.Drawing.Point(141, 23);
            this.comboBox_Background_color.Name = "comboBox_Background_color";
            this.comboBox_Background_color.Size = new System.Drawing.Size(45, 21);
            this.comboBox_Background_color.TabIndex = 59;
            this.comboBox_Background_color.Click += new System.EventHandler(this.comboBox_Background_color_Click);
            this.comboBox_Background_color.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBox_Preview_image
            // 
            this.comboBox_Preview_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_Preview_image.DropDownWidth = 135;
            this.comboBox_Preview_image.FormattingEnabled = true;
            this.comboBox_Preview_image.Location = new System.Drawing.Point(6, 83);
            this.comboBox_Preview_image.MaxDropDownItems = 25;
            this.comboBox_Preview_image.Name = "comboBox_Preview_image";
            this.comboBox_Preview_image.Size = new System.Drawing.Size(76, 21);
            this.comboBox_Preview_image.TabIndex = 58;
            this.comboBox_Preview_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_Preview_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_Preview_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_Preview_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_Preview_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBox_Background_image
            // 
            this.comboBox_Background_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_Background_image.DropDownWidth = 135;
            this.comboBox_Background_image.FormattingEnabled = true;
            this.comboBox_Background_image.Location = new System.Drawing.Point(6, 23);
            this.comboBox_Background_image.MaxDropDownItems = 25;
            this.comboBox_Background_image.Name = "comboBox_Background_image";
            this.comboBox_Background_image.Size = new System.Drawing.Size(76, 21);
            this.comboBox_Background_image.TabIndex = 57;
            this.comboBox_Background_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_Background_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_Background_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_Background_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_Background_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(3, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Preview:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "ID циферблата:";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(92, 127);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(0, 13);
            this.label_ID.TabIndex = 66;
            // 
            // UCtrl_Background_Opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_GenerateID);
            this.Controls.Add(this.radioButton_Background_color);
            this.Controls.Add(this.radioButton_Background_image);
            this.Controls.Add(this.comboBox_Background_color);
            this.Controls.Add(this.comboBox_Preview_image);
            this.Controls.Add(this.comboBox_Background_image);
            this.Controls.Add(this.label3);
            this.Name = "UCtrl_Background_Opt";
            this.Size = new System.Drawing.Size(255, 175);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_GenerateID;
        private System.Windows.Forms.RadioButton radioButton_Background_color;
        private System.Windows.Forms.ComboBox comboBox_Background_color;
        private System.Windows.Forms.ComboBox comboBox_Preview_image;
        private System.Windows.Forms.ComboBox comboBox_Background_image;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_ID;
        public System.Windows.Forms.RadioButton radioButton_Background_image;
    }
}
