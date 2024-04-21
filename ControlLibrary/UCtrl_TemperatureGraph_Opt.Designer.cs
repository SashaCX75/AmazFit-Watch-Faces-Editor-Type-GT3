﻿namespace ControlLibrary
{
    partial class UCtrl_TemperatureGraph_Opt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_TemperatureGraph_Opt));
            this.groupBox_min = new System.Windows.Forms.GroupBox();
            this.checkBox_use_min = new System.Windows.Forms.CheckBox();
            this.numericUpDown_min_pointSize = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_min_lineWidth = new System.Windows.Forms.NumericUpDown();
            this.label06 = new System.Windows.Forms.Label();
            this.numericUpDown_min_offsetX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_min_pointType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_minColor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_height = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown_posY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox_max = new System.Windows.Forms.GroupBox();
            this.checkBox_use_max = new System.Windows.Forms.CheckBox();
            this.numericUpDown_max_pointSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown_max_lineWidth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_max_offsetX = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_max_pointType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_maxColor = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox_min.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min_pointSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min_lineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min_offsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_posY)).BeginInit();
            this.groupBox_max.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max_pointSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max_lineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max_offsetX)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_min
            // 
            resources.ApplyResources(this.groupBox_min, "groupBox_min");
            this.groupBox_min.Controls.Add(this.checkBox_use_min);
            this.groupBox_min.Controls.Add(this.numericUpDown_min_pointSize);
            this.groupBox_min.Controls.Add(this.label7);
            this.groupBox_min.Controls.Add(this.numericUpDown_min_lineWidth);
            this.groupBox_min.Controls.Add(this.label06);
            this.groupBox_min.Controls.Add(this.numericUpDown_min_offsetX);
            this.groupBox_min.Controls.Add(this.label6);
            this.groupBox_min.Controls.Add(this.comboBox_min_pointType);
            this.groupBox_min.Controls.Add(this.label4);
            this.groupBox_min.Controls.Add(this.comboBox_minColor);
            this.groupBox_min.Controls.Add(this.label5);
            this.groupBox_min.Name = "groupBox_min";
            this.groupBox_min.TabStop = false;
            this.groupBox_min.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // checkBox_use_min
            // 
            resources.ApplyResources(this.checkBox_use_min, "checkBox_use_min");
            this.checkBox_use_min.Checked = true;
            this.checkBox_use_min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_use_min.Name = "checkBox_use_min";
            this.checkBox_use_min.UseVisualStyleBackColor = true;
            this.checkBox_use_min.CheckedChanged += new System.EventHandler(this.checkBox_use_min_CheckedChanged);
            // 
            // numericUpDown_min_pointSize
            // 
            resources.ApplyResources(this.numericUpDown_min_pointSize, "numericUpDown_min_pointSize");
            this.numericUpDown_min_pointSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_min_pointSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_min_pointSize.Name = "numericUpDown_min_pointSize";
            this.numericUpDown_min_pointSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDown_min_pointSize.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // numericUpDown_min_lineWidth
            // 
            resources.ApplyResources(this.numericUpDown_min_lineWidth, "numericUpDown_min_lineWidth");
            this.numericUpDown_min_lineWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_min_lineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_min_lineWidth.Name = "numericUpDown_min_lineWidth";
            this.numericUpDown_min_lineWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_min_lineWidth.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label06
            // 
            resources.ApplyResources(this.label06, "label06");
            this.label06.Name = "label06";
            // 
            // numericUpDown_min_offsetX
            // 
            resources.ApplyResources(this.numericUpDown_min_offsetX, "numericUpDown_min_offsetX");
            this.numericUpDown_min_offsetX.Maximum = new decimal(new int[] {
            199,
            0,
            0,
            0});
            this.numericUpDown_min_offsetX.Minimum = new decimal(new int[] {
            199,
            0,
            0,
            -2147483648});
            this.numericUpDown_min_offsetX.Name = "numericUpDown_min_offsetX";
            this.numericUpDown_min_offsetX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // comboBox_min_pointType
            // 
            resources.ApplyResources(this.comboBox_min_pointType, "comboBox_min_pointType");
            this.comboBox_min_pointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_min_pointType.FormattingEnabled = true;
            this.comboBox_min_pointType.Items.AddRange(new object[] {
            resources.GetString("comboBox_min_pointType.Items"),
            resources.GetString("comboBox_min_pointType.Items1"),
            resources.GetString("comboBox_min_pointType.Items2"),
            resources.GetString("comboBox_min_pointType.Items3"),
            resources.GetString("comboBox_min_pointType.Items4")});
            this.comboBox_min_pointType.Name = "comboBox_min_pointType";
            this.comboBox_min_pointType.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBox_minColor
            // 
            resources.ApplyResources(this.comboBox_minColor, "comboBox_minColor");
            this.comboBox_minColor.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.comboBox_minColor.DropDownHeight = 1;
            this.comboBox_minColor.FormattingEnabled = true;
            this.comboBox_minColor.Name = "comboBox_minColor";
            this.comboBox_minColor.Click += new System.EventHandler(this.comboBox_color_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // numericUpDown_height
            // 
            resources.ApplyResources(this.numericUpDown_height, "numericUpDown_height");
            this.numericUpDown_height.Maximum = new decimal(new int[] {
            215,
            0,
            0,
            0});
            this.numericUpDown_height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_height.Name = "numericUpDown_height";
            this.numericUpDown_height.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_height.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericUpDown_posY
            // 
            resources.ApplyResources(this.numericUpDown_posY, "numericUpDown_posY");
            this.numericUpDown_posY.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_posY.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.numericUpDown_posY.Name = "numericUpDown_posY";
            this.numericUpDown_posY.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_posY.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox_max
            // 
            resources.ApplyResources(this.groupBox_max, "groupBox_max");
            this.groupBox_max.Controls.Add(this.checkBox_use_max);
            this.groupBox_max.Controls.Add(this.numericUpDown_max_pointSize);
            this.groupBox_max.Controls.Add(this.label1);
            this.groupBox_max.Controls.Add(this.numericUpDown_max_lineWidth);
            this.groupBox_max.Controls.Add(this.label8);
            this.groupBox_max.Controls.Add(this.numericUpDown_max_offsetX);
            this.groupBox_max.Controls.Add(this.label9);
            this.groupBox_max.Controls.Add(this.comboBox_max_pointType);
            this.groupBox_max.Controls.Add(this.label10);
            this.groupBox_max.Controls.Add(this.comboBox_maxColor);
            this.groupBox_max.Controls.Add(this.label11);
            this.groupBox_max.Name = "groupBox_max";
            this.groupBox_max.TabStop = false;
            this.groupBox_max.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // checkBox_use_max
            // 
            resources.ApplyResources(this.checkBox_use_max, "checkBox_use_max");
            this.checkBox_use_max.Checked = true;
            this.checkBox_use_max.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_use_max.Name = "checkBox_use_max";
            this.checkBox_use_max.UseVisualStyleBackColor = true;
            this.checkBox_use_max.CheckedChanged += new System.EventHandler(this.checkBox_use_max_CheckedChanged);
            // 
            // numericUpDown_max_pointSize
            // 
            resources.ApplyResources(this.numericUpDown_max_pointSize, "numericUpDown_max_pointSize");
            this.numericUpDown_max_pointSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_max_pointSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_max_pointSize.Name = "numericUpDown_max_pointSize";
            this.numericUpDown_max_pointSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDown_max_pointSize.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericUpDown_max_lineWidth
            // 
            resources.ApplyResources(this.numericUpDown_max_lineWidth, "numericUpDown_max_lineWidth");
            this.numericUpDown_max_lineWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_max_lineWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_max_lineWidth.Name = "numericUpDown_max_lineWidth";
            this.numericUpDown_max_lineWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_max_lineWidth.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numericUpDown_max_offsetX
            // 
            resources.ApplyResources(this.numericUpDown_max_offsetX, "numericUpDown_max_offsetX");
            this.numericUpDown_max_offsetX.Maximum = new decimal(new int[] {
            199,
            0,
            0,
            0});
            this.numericUpDown_max_offsetX.Minimum = new decimal(new int[] {
            199,
            0,
            0,
            -2147483648});
            this.numericUpDown_max_offsetX.Name = "numericUpDown_max_offsetX";
            this.numericUpDown_max_offsetX.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // comboBox_max_pointType
            // 
            resources.ApplyResources(this.comboBox_max_pointType, "comboBox_max_pointType");
            this.comboBox_max_pointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_max_pointType.FormattingEnabled = true;
            this.comboBox_max_pointType.Items.AddRange(new object[] {
            resources.GetString("comboBox_max_pointType.Items"),
            resources.GetString("comboBox_max_pointType.Items1"),
            resources.GetString("comboBox_max_pointType.Items2"),
            resources.GetString("comboBox_max_pointType.Items3"),
            resources.GetString("comboBox_max_pointType.Items4")});
            this.comboBox_max_pointType.Name = "comboBox_max_pointType";
            this.comboBox_max_pointType.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // comboBox_maxColor
            // 
            resources.ApplyResources(this.comboBox_maxColor, "comboBox_maxColor");
            this.comboBox_maxColor.BackColor = System.Drawing.Color.Red;
            this.comboBox_maxColor.DropDownHeight = 1;
            this.comboBox_maxColor.FormattingEnabled = true;
            this.comboBox_maxColor.Name = "comboBox_maxColor";
            this.comboBox_maxColor.Click += new System.EventHandler(this.comboBox_color_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.numericUpDown_posY);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numericUpDown_height);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Name = "panel1";
            // 
            // UCtrl_TemperatureGraph_Opt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_min);
            this.Controls.Add(this.groupBox_max);
            this.Controls.Add(this.panel1);
            this.Name = "UCtrl_TemperatureGraph_Opt";
            this.groupBox_min.ResumeLayout(false);
            this.groupBox_min.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min_pointSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min_lineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_min_offsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_posY)).EndInit();
            this.groupBox_max.ResumeLayout(false);
            this.groupBox_max.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max_pointSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max_lineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_max_offsetX)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_min;
        private System.Windows.Forms.ComboBox comboBox_min_pointType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_minColor;
        public System.Windows.Forms.NumericUpDown numericUpDown_min_offsetX;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown numericUpDown_min_pointSize;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown numericUpDown_min_lineWidth;
        private System.Windows.Forms.Label label06;
        public System.Windows.Forms.NumericUpDown numericUpDown_height;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericUpDown_posY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox_max;
        public System.Windows.Forms.NumericUpDown numericUpDown_max_pointSize;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDown_max_lineWidth;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown numericUpDown_max_offsetX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_max_pointType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_maxColor;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.CheckBox checkBox_use_max;
        public System.Windows.Forms.CheckBox checkBox_use_min;
    }
}
