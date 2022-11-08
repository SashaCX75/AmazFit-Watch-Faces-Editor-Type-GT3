namespace Watch_Face_Editor
{
    partial class FormAddEditableElement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddEditableElement));
            this.groupBox_date = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton_week = new System.Windows.Forms.RadioButton();
            this.radioButton_year = new System.Windows.Forms.RadioButton();
            this.radioButton_month = new System.Windows.Forms.RadioButton();
            this.radioButton_date = new System.Windows.Forms.RadioButton();
            this.groupBox_activity = new System.Windows.Forms.GroupBox();
            this.radioButton_stress = new System.Windows.Forms.RadioButton();
            this.radioButton_fat_burning = new System.Windows.Forms.RadioButton();
            this.radioButton_SpO2 = new System.Windows.Forms.RadioButton();
            this.radioButton_stand = new System.Windows.Forms.RadioButton();
            this.radioButton_distance = new System.Windows.Forms.RadioButton();
            this.radioButton_PAI = new System.Windows.Forms.RadioButton();
            this.radioButton_heart = new System.Windows.Forms.RadioButton();
            this.radioButton_calories = new System.Windows.Forms.RadioButton();
            this.radioButton_steps = new System.Windows.Forms.RadioButton();
            this.groupBox_air = new System.Windows.Forms.GroupBox();
            this.radioButton_moon = new System.Windows.Forms.RadioButton();
            this.radioButton_altimeter = new System.Windows.Forms.RadioButton();
            this.radioButton_wind = new System.Windows.Forms.RadioButton();
            this.radioButton_sunrise = new System.Windows.Forms.RadioButton();
            this.radioButton_humidity = new System.Windows.Forms.RadioButton();
            this.radioButton_UVI = new System.Windows.Forms.RadioButton();
            this.radioButton_weather = new System.Windows.Forms.RadioButton();
            this.button_cancel = new System.Windows.Forms.Button();
            this.groupBox_system = new System.Windows.Forms.GroupBox();
            this.radioButton_battery = new System.Windows.Forms.RadioButton();
            this.button_add = new System.Windows.Forms.Button();
            this.groupBox_date.SuspendLayout();
            this.groupBox_activity.SuspendLayout();
            this.groupBox_air.SuspendLayout();
            this.groupBox_system.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_date
            // 
            resources.ApplyResources(this.groupBox_date, "groupBox_date");
            this.groupBox_date.Controls.Add(this.label1);
            this.groupBox_date.Controls.Add(this.radioButton_week);
            this.groupBox_date.Controls.Add(this.radioButton_year);
            this.groupBox_date.Controls.Add(this.radioButton_month);
            this.groupBox_date.Controls.Add(this.radioButton_date);
            this.groupBox_date.Name = "groupBox_date";
            this.groupBox_date.TabStop = false;
            this.groupBox_date.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // radioButton_week
            // 
            resources.ApplyResources(this.radioButton_week, "radioButton_week");
            this.radioButton_week.Name = "radioButton_week";
            this.radioButton_week.TabStop = true;
            this.radioButton_week.UseVisualStyleBackColor = true;
            this.radioButton_week.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_year
            // 
            resources.ApplyResources(this.radioButton_year, "radioButton_year");
            this.radioButton_year.Name = "radioButton_year";
            this.radioButton_year.TabStop = true;
            this.radioButton_year.UseVisualStyleBackColor = true;
            this.radioButton_year.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_month
            // 
            resources.ApplyResources(this.radioButton_month, "radioButton_month");
            this.radioButton_month.Name = "radioButton_month";
            this.radioButton_month.TabStop = true;
            this.radioButton_month.UseVisualStyleBackColor = true;
            this.radioButton_month.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_date
            // 
            resources.ApplyResources(this.radioButton_date, "radioButton_date");
            this.radioButton_date.Name = "radioButton_date";
            this.radioButton_date.TabStop = true;
            this.radioButton_date.UseVisualStyleBackColor = true;
            this.radioButton_date.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox_activity
            // 
            resources.ApplyResources(this.groupBox_activity, "groupBox_activity");
            this.groupBox_activity.Controls.Add(this.radioButton_stress);
            this.groupBox_activity.Controls.Add(this.radioButton_fat_burning);
            this.groupBox_activity.Controls.Add(this.radioButton_SpO2);
            this.groupBox_activity.Controls.Add(this.radioButton_stand);
            this.groupBox_activity.Controls.Add(this.radioButton_distance);
            this.groupBox_activity.Controls.Add(this.radioButton_PAI);
            this.groupBox_activity.Controls.Add(this.radioButton_heart);
            this.groupBox_activity.Controls.Add(this.radioButton_calories);
            this.groupBox_activity.Controls.Add(this.radioButton_steps);
            this.groupBox_activity.Name = "groupBox_activity";
            this.groupBox_activity.TabStop = false;
            this.groupBox_activity.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_stress
            // 
            resources.ApplyResources(this.radioButton_stress, "radioButton_stress");
            this.radioButton_stress.Name = "radioButton_stress";
            this.radioButton_stress.TabStop = true;
            this.radioButton_stress.UseVisualStyleBackColor = true;
            this.radioButton_stress.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_fat_burning
            // 
            resources.ApplyResources(this.radioButton_fat_burning, "radioButton_fat_burning");
            this.radioButton_fat_burning.Name = "radioButton_fat_burning";
            this.radioButton_fat_burning.TabStop = true;
            this.radioButton_fat_burning.UseVisualStyleBackColor = true;
            this.radioButton_fat_burning.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_SpO2
            // 
            resources.ApplyResources(this.radioButton_SpO2, "radioButton_SpO2");
            this.radioButton_SpO2.Name = "radioButton_SpO2";
            this.radioButton_SpO2.TabStop = true;
            this.radioButton_SpO2.UseVisualStyleBackColor = true;
            this.radioButton_SpO2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_stand
            // 
            resources.ApplyResources(this.radioButton_stand, "radioButton_stand");
            this.radioButton_stand.Name = "radioButton_stand";
            this.radioButton_stand.TabStop = true;
            this.radioButton_stand.UseVisualStyleBackColor = true;
            this.radioButton_stand.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_distance
            // 
            resources.ApplyResources(this.radioButton_distance, "radioButton_distance");
            this.radioButton_distance.Name = "radioButton_distance";
            this.radioButton_distance.TabStop = true;
            this.radioButton_distance.UseVisualStyleBackColor = true;
            this.radioButton_distance.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_PAI
            // 
            resources.ApplyResources(this.radioButton_PAI, "radioButton_PAI");
            this.radioButton_PAI.Name = "radioButton_PAI";
            this.radioButton_PAI.TabStop = true;
            this.radioButton_PAI.UseVisualStyleBackColor = true;
            this.radioButton_PAI.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_heart
            // 
            resources.ApplyResources(this.radioButton_heart, "radioButton_heart");
            this.radioButton_heart.Name = "radioButton_heart";
            this.radioButton_heart.TabStop = true;
            this.radioButton_heart.UseVisualStyleBackColor = true;
            this.radioButton_heart.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_calories
            // 
            resources.ApplyResources(this.radioButton_calories, "radioButton_calories");
            this.radioButton_calories.Name = "radioButton_calories";
            this.radioButton_calories.TabStop = true;
            this.radioButton_calories.UseVisualStyleBackColor = true;
            this.radioButton_calories.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_steps
            // 
            resources.ApplyResources(this.radioButton_steps, "radioButton_steps");
            this.radioButton_steps.Name = "radioButton_steps";
            this.radioButton_steps.TabStop = true;
            this.radioButton_steps.UseVisualStyleBackColor = true;
            this.radioButton_steps.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox_air
            // 
            resources.ApplyResources(this.groupBox_air, "groupBox_air");
            this.groupBox_air.Controls.Add(this.radioButton_moon);
            this.groupBox_air.Controls.Add(this.radioButton_altimeter);
            this.groupBox_air.Controls.Add(this.radioButton_wind);
            this.groupBox_air.Controls.Add(this.radioButton_sunrise);
            this.groupBox_air.Controls.Add(this.radioButton_humidity);
            this.groupBox_air.Controls.Add(this.radioButton_UVI);
            this.groupBox_air.Controls.Add(this.radioButton_weather);
            this.groupBox_air.Name = "groupBox_air";
            this.groupBox_air.TabStop = false;
            this.groupBox_air.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_moon
            // 
            resources.ApplyResources(this.radioButton_moon, "radioButton_moon");
            this.radioButton_moon.Name = "radioButton_moon";
            this.radioButton_moon.TabStop = true;
            this.radioButton_moon.UseVisualStyleBackColor = true;
            this.radioButton_moon.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_altimeter
            // 
            resources.ApplyResources(this.radioButton_altimeter, "radioButton_altimeter");
            this.radioButton_altimeter.Name = "radioButton_altimeter";
            this.radioButton_altimeter.TabStop = true;
            this.radioButton_altimeter.UseVisualStyleBackColor = true;
            this.radioButton_altimeter.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_wind
            // 
            resources.ApplyResources(this.radioButton_wind, "radioButton_wind");
            this.radioButton_wind.Name = "radioButton_wind";
            this.radioButton_wind.TabStop = true;
            this.radioButton_wind.UseVisualStyleBackColor = true;
            this.radioButton_wind.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_sunrise
            // 
            resources.ApplyResources(this.radioButton_sunrise, "radioButton_sunrise");
            this.radioButton_sunrise.Name = "radioButton_sunrise";
            this.radioButton_sunrise.TabStop = true;
            this.radioButton_sunrise.UseVisualStyleBackColor = true;
            this.radioButton_sunrise.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_humidity
            // 
            resources.ApplyResources(this.radioButton_humidity, "radioButton_humidity");
            this.radioButton_humidity.Name = "radioButton_humidity";
            this.radioButton_humidity.TabStop = true;
            this.radioButton_humidity.UseVisualStyleBackColor = true;
            this.radioButton_humidity.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_UVI
            // 
            resources.ApplyResources(this.radioButton_UVI, "radioButton_UVI");
            this.radioButton_UVI.Name = "radioButton_UVI";
            this.radioButton_UVI.TabStop = true;
            this.radioButton_UVI.UseVisualStyleBackColor = true;
            this.radioButton_UVI.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_weather
            // 
            resources.ApplyResources(this.radioButton_weather, "radioButton_weather");
            this.radioButton_weather.Name = "radioButton_weather";
            this.radioButton_weather.TabStop = true;
            this.radioButton_weather.UseVisualStyleBackColor = true;
            this.radioButton_weather.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // button_cancel
            // 
            resources.ApplyResources(this.button_cancel, "button_cancel");
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // groupBox_system
            // 
            resources.ApplyResources(this.groupBox_system, "groupBox_system");
            this.groupBox_system.Controls.Add(this.radioButton_battery);
            this.groupBox_system.Name = "groupBox_system";
            this.groupBox_system.TabStop = false;
            this.groupBox_system.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // radioButton_battery
            // 
            resources.ApplyResources(this.radioButton_battery, "radioButton_battery");
            this.radioButton_battery.Name = "radioButton_battery";
            this.radioButton_battery.TabStop = true;
            this.radioButton_battery.UseVisualStyleBackColor = true;
            this.radioButton_battery.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // button_add
            // 
            resources.ApplyResources(this.button_add, "button_add");
            this.button_add.Name = "button_add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // FormAddEditableElement
            // 
            this.AcceptButton = this.button_add;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.groupBox_system);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.groupBox_air);
            this.Controls.Add(this.groupBox_activity);
            this.Controls.Add(this.groupBox_date);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddEditableElement";
            this.groupBox_date.ResumeLayout(false);
            this.groupBox_date.PerformLayout();
            this.groupBox_activity.ResumeLayout(false);
            this.groupBox_activity.PerformLayout();
            this.groupBox_air.ResumeLayout(false);
            this.groupBox_air.PerformLayout();
            this.groupBox_system.ResumeLayout(false);
            this.groupBox_system.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_date;
        private System.Windows.Forms.RadioButton radioButton_date;
        private System.Windows.Forms.RadioButton radioButton_week;
        private System.Windows.Forms.RadioButton radioButton_year;
        private System.Windows.Forms.RadioButton radioButton_month;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_activity;
        private System.Windows.Forms.RadioButton radioButton_PAI;
        private System.Windows.Forms.RadioButton radioButton_heart;
        private System.Windows.Forms.RadioButton radioButton_calories;
        private System.Windows.Forms.RadioButton radioButton_steps;
        private System.Windows.Forms.RadioButton radioButton_stress;
        private System.Windows.Forms.RadioButton radioButton_fat_burning;
        private System.Windows.Forms.RadioButton radioButton_SpO2;
        private System.Windows.Forms.RadioButton radioButton_stand;
        private System.Windows.Forms.RadioButton radioButton_distance;
        private System.Windows.Forms.GroupBox groupBox_air;
        private System.Windows.Forms.RadioButton radioButton_sunrise;
        private System.Windows.Forms.RadioButton radioButton_humidity;
        private System.Windows.Forms.RadioButton radioButton_UVI;
        private System.Windows.Forms.RadioButton radioButton_weather;
        private System.Windows.Forms.RadioButton radioButton_moon;
        private System.Windows.Forms.RadioButton radioButton_altimeter;
        private System.Windows.Forms.RadioButton radioButton_wind;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.GroupBox groupBox_system;
        private System.Windows.Forms.RadioButton radioButton_battery;
        private System.Windows.Forms.Button button_add;
    }
}