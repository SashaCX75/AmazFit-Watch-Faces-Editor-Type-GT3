
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Background_Opt));
            this.button_GenerateID = new System.Windows.Forms.Button();
            this.radioButton_Background_color = new System.Windows.Forms.RadioButton();
            this.radioButton_Background_image = new System.Windows.Forms.RadioButton();
            this.comboBox_Background_color = new System.Windows.Forms.ComboBox();
            this.comboBox_Preview_image = new System.Windows.Forms.ComboBox();
            this.comboBox_Background_image = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.radioButton_EditableBackground = new System.Windows.Forms.RadioButton();
            this.label_EditableBackground_Hint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_GenerateID
            // 
            resources.ApplyResources(this.button_GenerateID, "button_GenerateID");
            this.button_GenerateID.Name = "button_GenerateID";
            this.button_GenerateID.UseVisualStyleBackColor = true;
            this.button_GenerateID.Click += new System.EventHandler(this.button_GenerateID_Click);
            // 
            // radioButton_Background_color
            // 
            resources.ApplyResources(this.radioButton_Background_color, "radioButton_Background_color");
            this.radioButton_Background_color.Checked = true;
            this.radioButton_Background_color.Name = "radioButton_Background_color";
            this.radioButton_Background_color.TabStop = true;
            this.radioButton_Background_color.UseVisualStyleBackColor = true;
            this.radioButton_Background_color.CheckedChanged += new System.EventHandler(this.radioButton_Background_image_CheckedChanged);
            // 
            // radioButton_Background_image
            // 
            resources.ApplyResources(this.radioButton_Background_image, "radioButton_Background_image");
            this.radioButton_Background_image.Name = "radioButton_Background_image";
            this.radioButton_Background_image.UseVisualStyleBackColor = true;
            this.radioButton_Background_image.CheckedChanged += new System.EventHandler(this.radioButton_Background_image_CheckedChanged);
            // 
            // comboBox_Background_color
            // 
            resources.ApplyResources(this.comboBox_Background_color, "comboBox_Background_color");
            this.comboBox_Background_color.BackColor = System.Drawing.Color.DarkOrange;
            this.comboBox_Background_color.DropDownHeight = 1;
            this.comboBox_Background_color.FormattingEnabled = true;
            this.comboBox_Background_color.Name = "comboBox_Background_color";
            this.comboBox_Background_color.Click += new System.EventHandler(this.comboBox_Background_color_Click);
            this.comboBox_Background_color.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBox_Preview_image
            // 
            resources.ApplyResources(this.comboBox_Preview_image, "comboBox_Preview_image");
            this.comboBox_Preview_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_Preview_image.DropDownWidth = 135;
            this.comboBox_Preview_image.FormattingEnabled = true;
            this.comboBox_Preview_image.Name = "comboBox_Preview_image";
            this.comboBox_Preview_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_Preview_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_Preview_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_Preview_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_Preview_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // comboBox_Background_image
            // 
            resources.ApplyResources(this.comboBox_Background_image, "comboBox_Background_image");
            this.comboBox_Background_image.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBox_Background_image.DropDownWidth = 135;
            this.comboBox_Background_image.FormattingEnabled = true;
            this.comboBox_Background_image.Name = "comboBox_Background_image";
            this.comboBox_Background_image.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_DrawItem);
            this.comboBox_Background_image.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.comboBox_MeasureItem);
            this.comboBox_Background_image.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            this.comboBox_Background_image.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyDown);
            this.comboBox_Background_image.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox_KeyPress);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label_ID
            // 
            resources.ApplyResources(this.label_ID, "label_ID");
            this.label_ID.Name = "label_ID";
            // 
            // radioButton_EditableBackground
            // 
            resources.ApplyResources(this.radioButton_EditableBackground, "radioButton_EditableBackground");
            this.radioButton_EditableBackground.Name = "radioButton_EditableBackground";
            this.radioButton_EditableBackground.UseVisualStyleBackColor = true;
            this.radioButton_EditableBackground.CheckedChanged += new System.EventHandler(this.radioButton_Background_image_CheckedChanged);
            // 
            // label_EditableBackground_Hint
            // 
            resources.ApplyResources(this.label_EditableBackground_Hint, "label_EditableBackground_Hint");
            this.label_EditableBackground_Hint.Name = "label_EditableBackground_Hint";
            // 
            // UCtrl_Background_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_EditableBackground_Hint);
            this.Controls.Add(this.radioButton_EditableBackground);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_GenerateID;
        private System.Windows.Forms.ComboBox comboBox_Background_color;
        private System.Windows.Forms.ComboBox comboBox_Preview_image;
        private System.Windows.Forms.ComboBox comboBox_Background_image;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_ID;
        public System.Windows.Forms.RadioButton radioButton_Background_image;
        public System.Windows.Forms.RadioButton radioButton_Background_color;
        public System.Windows.Forms.RadioButton radioButton_EditableBackground;
        private System.Windows.Forms.Label label_EditableBackground_Hint;
    }
}
