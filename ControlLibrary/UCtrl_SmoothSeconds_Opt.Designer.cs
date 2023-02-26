namespace ControlLibrary
{
    partial class UCtrl_SmoothSeconds_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_SmoothSeconds_Opt));
            this.radioButton_type1 = new System.Windows.Forms.RadioButton();
            this.radioButton_type2 = new System.Windows.Forms.RadioButton();
            this.toolTipTypeHint = new System.Windows.Forms.ToolTip(this.components);
            this.radioButton_type3 = new System.Windows.Forms.RadioButton();
            this.radioButton_type4 = new System.Windows.Forms.RadioButton();
            this.numericUpDown_fps = new System.Windows.Forms.NumericUpDown();
            this.label01 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fps)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton_type1
            // 
            resources.ApplyResources(this.radioButton_type1, "radioButton_type1");
            this.radioButton_type1.Name = "radioButton_type1";
            this.radioButton_type1.TabStop = true;
            this.toolTipTypeHint.SetToolTip(this.radioButton_type1, resources.GetString("radioButton_type1.ToolTip"));
            this.radioButton_type1.UseVisualStyleBackColor = true;
            this.radioButton_type1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_type2
            // 
            resources.ApplyResources(this.radioButton_type2, "radioButton_type2");
            this.radioButton_type2.Name = "radioButton_type2";
            this.radioButton_type2.TabStop = true;
            this.toolTipTypeHint.SetToolTip(this.radioButton_type2, resources.GetString("radioButton_type2.ToolTip"));
            this.radioButton_type2.UseVisualStyleBackColor = true;
            this.radioButton_type2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // toolTipTypeHint
            // 
            this.toolTipTypeHint.AutoPopDelay = 32000;
            this.toolTipTypeHint.InitialDelay = 500;
            this.toolTipTypeHint.IsBalloon = true;
            this.toolTipTypeHint.ReshowDelay = 100;
            // 
            // radioButton_type3
            // 
            resources.ApplyResources(this.radioButton_type3, "radioButton_type3");
            this.radioButton_type3.Name = "radioButton_type3";
            this.radioButton_type3.TabStop = true;
            this.toolTipTypeHint.SetToolTip(this.radioButton_type3, resources.GetString("radioButton_type3.ToolTip"));
            this.radioButton_type3.UseVisualStyleBackColor = true;
            this.radioButton_type3.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_type4
            // 
            resources.ApplyResources(this.radioButton_type4, "radioButton_type4");
            this.radioButton_type4.Name = "radioButton_type4";
            this.radioButton_type4.TabStop = true;
            this.toolTipTypeHint.SetToolTip(this.radioButton_type4, resources.GetString("radioButton_type4.ToolTip"));
            this.radioButton_type4.UseVisualStyleBackColor = true;
            this.radioButton_type4.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // numericUpDown_fps
            // 
            resources.ApplyResources(this.numericUpDown_fps, "numericUpDown_fps");
            this.numericUpDown_fps.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDown_fps.Name = "numericUpDown_fps";
            this.toolTipTypeHint.SetToolTip(this.numericUpDown_fps, resources.GetString("numericUpDown_fps.ToolTip"));
            this.numericUpDown_fps.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_fps.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label01
            // 
            resources.ApplyResources(this.label01, "label01");
            this.label01.Name = "label01";
            this.toolTipTypeHint.SetToolTip(this.label01, resources.GetString("label01.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTipTypeHint.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTipTypeHint.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTipTypeHint.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTipTypeHint.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // UCtrl_SmoothSeconds_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radioButton_type4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton_type3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown_fps);
            this.Controls.Add(this.label01);
            this.Controls.Add(this.radioButton_type2);
            this.Controls.Add(this.radioButton_type1);
            this.Name = "UCtrl_SmoothSeconds_Opt";
            this.toolTipTypeHint.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTipTypeHint;
        public System.Windows.Forms.RadioButton radioButton_type1;
        public System.Windows.Forms.RadioButton radioButton_type2;
        public System.Windows.Forms.NumericUpDown numericUpDown_fps;
        public System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RadioButton radioButton_type3;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.RadioButton radioButton_type4;
        private System.Windows.Forms.Label label4;
    }
}
