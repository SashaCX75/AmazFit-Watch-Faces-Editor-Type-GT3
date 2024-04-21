namespace ControlLibrary
{
    partial class AddButtonFunction
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddButtonFunction));
            this.radioButton_click = new System.Windows.Forms.RadioButton();
            this.radioButton_londPress = new System.Windows.Forms.RadioButton();
            this.comboBox_Activity = new System.Windows.Forms.ComboBox();
            this.comboBox_App = new System.Windows.Forms.ComboBox();
            this.comboBox_System = new System.Windows.Forms.ComboBox();
            this.comboBox_UserScript = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox_selectScript = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox_IconSystem = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.groupBox_click = new System.Windows.Forms.GroupBox();
            this.richTextBox_click = new System.Windows.Forms.RichTextBox();
            this.groupBox_longPress = new System.Windows.Forms.GroupBox();
            this.richTextBox_longPress = new System.Windows.Forms.RichTextBox();
            this.toolTip_help = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox_selectScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox_click.SuspendLayout();
            this.groupBox_longPress.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton_click
            // 
            resources.ApplyResources(this.radioButton_click, "radioButton_click");
            this.radioButton_click.Checked = true;
            this.radioButton_click.Name = "radioButton_click";
            this.radioButton_click.TabStop = true;
            this.radioButton_click.UseVisualStyleBackColor = true;
            this.radioButton_click.CheckedChanged += new System.EventHandler(this.radioButton_click_CheckedChanged);
            // 
            // radioButton_londPress
            // 
            resources.ApplyResources(this.radioButton_londPress, "radioButton_londPress");
            this.radioButton_londPress.Name = "radioButton_londPress";
            this.radioButton_londPress.UseVisualStyleBackColor = true;
            // 
            // comboBox_Activity
            // 
            this.comboBox_Activity.DropDownHeight = 200;
            this.comboBox_Activity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Activity.DropDownWidth = 190;
            this.comboBox_Activity.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_Activity, "comboBox_Activity");
            this.comboBox_Activity.Name = "comboBox_Activity";
            this.comboBox_Activity.DropDownClosed += new System.EventHandler(this.comboBox_Activity_DropDownClosed);
            this.comboBox_Activity.Click += new System.EventHandler(this.comboBox_Click);
            // 
            // comboBox_App
            // 
            this.comboBox_App.DropDownHeight = 200;
            this.comboBox_App.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_App.DropDownWidth = 190;
            this.comboBox_App.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_App, "comboBox_App");
            this.comboBox_App.Name = "comboBox_App";
            this.comboBox_App.DropDownClosed += new System.EventHandler(this.comboBox_App_DropDownClosed);
            this.comboBox_App.Click += new System.EventHandler(this.comboBox_Click);
            // 
            // comboBox_System
            // 
            this.comboBox_System.DropDownHeight = 200;
            this.comboBox_System.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_System.DropDownWidth = 190;
            this.comboBox_System.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_System, "comboBox_System");
            this.comboBox_System.Name = "comboBox_System";
            this.comboBox_System.DropDownClosed += new System.EventHandler(this.comboBox_System_DropDownClosed);
            this.comboBox_System.Click += new System.EventHandler(this.comboBox_Click);
            // 
            // comboBox_UserScript
            // 
            this.comboBox_UserScript.DropDownHeight = 200;
            this.comboBox_UserScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_UserScript.DropDownWidth = 190;
            resources.ApplyResources(this.comboBox_UserScript, "comboBox_UserScript");
            this.comboBox_UserScript.FormattingEnabled = true;
            this.comboBox_UserScript.Name = "comboBox_UserScript";
            this.comboBox_UserScript.DropDownClosed += new System.EventHandler(this.comboBox_UserScript_DropDownClosed);
            this.comboBox_UserScript.Click += new System.EventHandler(this.comboBox_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_click);
            this.panel1.Controls.Add(this.radioButton_londPress);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // groupBox_selectScript
            // 
            this.groupBox_selectScript.Controls.Add(this.pictureBox4);
            this.groupBox_selectScript.Controls.Add(this.pictureBox_IconSystem);
            this.groupBox_selectScript.Controls.Add(this.comboBox_Activity);
            this.groupBox_selectScript.Controls.Add(this.pictureBox3);
            this.groupBox_selectScript.Controls.Add(this.comboBox_App);
            this.groupBox_selectScript.Controls.Add(this.comboBox_UserScript);
            this.groupBox_selectScript.Controls.Add(this.pictureBox1);
            this.groupBox_selectScript.Controls.Add(this.pictureBox2);
            this.groupBox_selectScript.Controls.Add(this.comboBox_System);
            resources.ApplyResources(this.groupBox_selectScript, "groupBox_selectScript");
            this.groupBox_selectScript.Name = "groupBox_selectScript";
            this.groupBox_selectScript.TabStop = false;
            this.groupBox_selectScript.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::ControlLibrary.Properties.Resources.help;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            this.toolTip_help.SetToolTip(this.pictureBox4, resources.GetString("pictureBox4.ToolTip"));
            // 
            // pictureBox_IconSystem
            // 
            this.pictureBox_IconSystem.BackgroundImage = global::ControlLibrary.Properties.Resources.Sports;
            resources.ApplyResources(this.pictureBox_IconSystem, "pictureBox_IconSystem");
            this.pictureBox_IconSystem.Name = "pictureBox_IconSystem";
            this.pictureBox_IconSystem.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::ControlLibrary.Properties.Resources.Script;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ControlLibrary.Properties.Resources.App;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::ControlLibrary.Properties.Resources.Tools;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // button_Cancel
            // 
            resources.ApplyResources(this.button_Cancel, "button_Cancel");
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Save
            // 
            resources.ApplyResources(this.button_Save, "button_Save");
            this.button_Save.Name = "button_Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // groupBox_click
            // 
            this.groupBox_click.Controls.Add(this.richTextBox_click);
            resources.ApplyResources(this.groupBox_click, "groupBox_click");
            this.groupBox_click.Name = "groupBox_click";
            this.groupBox_click.TabStop = false;
            this.groupBox_click.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // richTextBox_click
            // 
            this.richTextBox_click.AcceptsTab = true;
            resources.ApplyResources(this.richTextBox_click, "richTextBox_click");
            this.richTextBox_click.Name = "richTextBox_click";
            // 
            // groupBox_longPress
            // 
            this.groupBox_longPress.Controls.Add(this.richTextBox_longPress);
            resources.ApplyResources(this.groupBox_longPress, "groupBox_longPress");
            this.groupBox_longPress.ForeColor = System.Drawing.Color.Navy;
            this.groupBox_longPress.Name = "groupBox_longPress";
            this.groupBox_longPress.TabStop = false;
            this.groupBox_longPress.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // richTextBox_longPress
            // 
            this.richTextBox_longPress.AcceptsTab = true;
            resources.ApplyResources(this.richTextBox_longPress, "richTextBox_longPress");
            this.richTextBox_longPress.Name = "richTextBox_longPress";
            // 
            // toolTip_help
            // 
            this.toolTip_help.AutoPopDelay = 32000;
            this.toolTip_help.InitialDelay = 500;
            this.toolTip_help.IsBalloon = true;
            this.toolTip_help.ReshowDelay = 100;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // AddButtonFunction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox_longPress);
            this.Controls.Add(this.groupBox_click);
            this.Controls.Add(this.groupBox_selectScript);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddButtonFunction";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.AddButtonFunction_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox_selectScript.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_IconSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox_click.ResumeLayout(false);
            this.groupBox_longPress.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton_click;
        private System.Windows.Forms.RadioButton radioButton_londPress;
        private System.Windows.Forms.PictureBox pictureBox_IconSystem;
        private System.Windows.Forms.ComboBox comboBox_Activity;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox_App;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox comboBox_System;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox comboBox_UserScript;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox_selectScript;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.GroupBox groupBox_click;
        private System.Windows.Forms.GroupBox groupBox_longPress;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ToolTip toolTip_help;
        private System.Windows.Forms.RichTextBox richTextBox_click;
        private System.Windows.Forms.RichTextBox richTextBox_longPress;
        private System.Windows.Forms.Label label1;
    }
}