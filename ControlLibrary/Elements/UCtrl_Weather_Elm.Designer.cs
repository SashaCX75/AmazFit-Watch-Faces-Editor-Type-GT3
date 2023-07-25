
namespace ControlLibrary
{
    partial class UCtrl_Weather_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Weather_Elm));
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Text_Max_circle = new System.Windows.Forms.Panel();
            this.button_Text_Max_circle = new System.Windows.Forms.Button();
            this.checkBox_Text_Max_circle = new System.Windows.Forms.CheckBox();
            this.panel_Text_Max_rotation = new System.Windows.Forms.Panel();
            this.button_Text_Max_rotation = new System.Windows.Forms.Button();
            this.checkBox_Text_Max_rotation = new System.Windows.Forms.CheckBox();
            this.panel_Text_Min_circle = new System.Windows.Forms.Panel();
            this.button_Text_Min_circle = new System.Windows.Forms.Button();
            this.checkBox_Text_Min_circle = new System.Windows.Forms.CheckBox();
            this.panel_Text_Min_rotation = new System.Windows.Forms.Panel();
            this.button_Text_Min_rotation = new System.Windows.Forms.Button();
            this.checkBox_Text_Min_rotation = new System.Windows.Forms.CheckBox();
            this.panel_Text_CityName = new System.Windows.Forms.Panel();
            this.button_Text_CityName = new System.Windows.Forms.Button();
            this.checkBox_Text_CityName = new System.Windows.Forms.CheckBox();
            this.panel_Number_Min = new System.Windows.Forms.Panel();
            this.button_Number_Min = new System.Windows.Forms.Button();
            this.checkBox_Number_Min = new System.Windows.Forms.CheckBox();
            this.panel_Images = new System.Windows.Forms.Panel();
            this.checkBox_Images = new System.Windows.Forms.CheckBox();
            this.button_Images = new System.Windows.Forms.Button();
            this.panel_Number = new System.Windows.Forms.Panel();
            this.button_Number = new System.Windows.Forms.Button();
            this.checkBox_Number = new System.Windows.Forms.CheckBox();
            this.panel_Number_Max = new System.Windows.Forms.Panel();
            this.button_Number_Max = new System.Windows.Forms.Button();
            this.checkBox_Number_Max = new System.Windows.Forms.CheckBox();
            this.panel_Icon = new System.Windows.Forms.Panel();
            this.button_Icon = new System.Windows.Forms.Button();
            this.checkBox_Icon = new System.Windows.Forms.CheckBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            this.toolTip_Weather = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Text_Max_circle.SuspendLayout();
            this.panel_Text_Max_rotation.SuspendLayout();
            this.panel_Text_Min_circle.SuspendLayout();
            this.panel_Text_Min_rotation.SuspendLayout();
            this.panel_Text_CityName.SuspendLayout();
            this.panel_Number_Min.SuspendLayout();
            this.panel_Images.SuspendLayout();
            this.panel_Number.SuspendLayout();
            this.panel_Number_Max.SuspendLayout();
            this.panel_Icon.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Arrow_Down
            // 
            this.pictureBox_Arrow_Down.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_down;
            resources.ApplyResources(this.pictureBox_Arrow_Down, "pictureBox_Arrow_Down");
            this.pictureBox_Arrow_Down.Name = "pictureBox_Arrow_Down";
            this.pictureBox_Arrow_Down.TabStop = false;
            this.pictureBox_Arrow_Down.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_NotShow
            // 
            resources.ApplyResources(this.pictureBox_NotShow, "pictureBox_NotShow");
            this.pictureBox_NotShow.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_off_black_24;
            this.pictureBox_NotShow.Name = "pictureBox_NotShow";
            this.pictureBox_NotShow.TabStop = false;
            this.pictureBox_NotShow.Click += new System.EventHandler(this.pictureBox_NotShow_Click);
            // 
            // pictureBox_Arrow_Right
            // 
            this.pictureBox_Arrow_Right.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_right;
            resources.ApplyResources(this.pictureBox_Arrow_Right, "pictureBox_Arrow_Right");
            this.pictureBox_Arrow_Right.Name = "pictureBox_Arrow_Right";
            this.pictureBox_Arrow_Right.TabStop = false;
            this.pictureBox_Arrow_Right.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_Show
            // 
            resources.ApplyResources(this.pictureBox_Show, "pictureBox_Show");
            this.pictureBox_Show.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_black_24;
            this.pictureBox_Show.Name = "pictureBox_Show";
            this.pictureBox_Show.TabStop = false;
            this.pictureBox_Show.Click += new System.EventHandler(this.pictureBox_Show_Click);
            // 
            // pictureBox_Del
            // 
            this.pictureBox_Del.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_delete_forever_black_24;
            resources.ApplyResources(this.pictureBox_Del, "pictureBox_Del");
            this.pictureBox_Del.Name = "pictureBox_Del";
            this.pictureBox_Del.TabStop = false;
            this.pictureBox_Del.Click += new System.EventHandler(this.pictureBox_Del_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel_Text_Max_circle, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel_Text_Max_rotation, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel_Text_Min_circle, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel_Text_Min_rotation, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_Text_CityName, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.panel_Number_Min, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_Images, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_Number, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_Number_Max, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel_Icon, 0, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Text_Max_circle
            // 
            this.panel_Text_Max_circle.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Text_Max_circle.Controls.Add(this.button_Text_Max_circle);
            this.panel_Text_Max_circle.Controls.Add(this.checkBox_Text_Max_circle);
            resources.ApplyResources(this.panel_Text_Max_circle, "panel_Text_Max_circle");
            this.panel_Text_Max_circle.Name = "panel_Text_Max_circle";
            this.panel_Text_Max_circle.Click += new System.EventHandler(this.panel_Text_Max_circle_Click);
            this.panel_Text_Max_circle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Text_Max_circle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Text_Max_circle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Text_Max_circle
            // 
            this.button_Text_Max_circle.FlatAppearance.BorderSize = 0;
            this.button_Text_Max_circle.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Text_Max_circle.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Text_Max_circle, "button_Text_Max_circle");
            this.button_Text_Max_circle.Name = "button_Text_Max_circle";
            this.button_Text_Max_circle.UseVisualStyleBackColor = true;
            this.button_Text_Max_circle.Click += new System.EventHandler(this.panel_Text_Max_circle_Click);
            this.button_Text_Max_circle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Text_Max_circle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Text_Max_circle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Text_Max_circle
            // 
            resources.ApplyResources(this.checkBox_Text_Max_circle, "checkBox_Text_Max_circle");
            this.checkBox_Text_Max_circle.Name = "checkBox_Text_Max_circle";
            this.checkBox_Text_Max_circle.UseVisualStyleBackColor = true;
            this.checkBox_Text_Max_circle.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Text_Max_rotation
            // 
            this.panel_Text_Max_rotation.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Text_Max_rotation.Controls.Add(this.button_Text_Max_rotation);
            this.panel_Text_Max_rotation.Controls.Add(this.checkBox_Text_Max_rotation);
            resources.ApplyResources(this.panel_Text_Max_rotation, "panel_Text_Max_rotation");
            this.panel_Text_Max_rotation.Name = "panel_Text_Max_rotation";
            this.panel_Text_Max_rotation.Click += new System.EventHandler(this.panel_Text_Max_rotation_Click);
            this.panel_Text_Max_rotation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Text_Max_rotation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Text_Max_rotation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Text_Max_rotation
            // 
            this.button_Text_Max_rotation.FlatAppearance.BorderSize = 0;
            this.button_Text_Max_rotation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Text_Max_rotation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Text_Max_rotation, "button_Text_Max_rotation");
            this.button_Text_Max_rotation.Name = "button_Text_Max_rotation";
            this.button_Text_Max_rotation.UseVisualStyleBackColor = true;
            this.button_Text_Max_rotation.Click += new System.EventHandler(this.panel_Text_Max_rotation_Click);
            this.button_Text_Max_rotation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Text_Max_rotation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Text_Max_rotation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Text_Max_rotation
            // 
            resources.ApplyResources(this.checkBox_Text_Max_rotation, "checkBox_Text_Max_rotation");
            this.checkBox_Text_Max_rotation.Name = "checkBox_Text_Max_rotation";
            this.checkBox_Text_Max_rotation.UseVisualStyleBackColor = true;
            this.checkBox_Text_Max_rotation.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Text_Min_circle
            // 
            this.panel_Text_Min_circle.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Text_Min_circle.Controls.Add(this.button_Text_Min_circle);
            this.panel_Text_Min_circle.Controls.Add(this.checkBox_Text_Min_circle);
            resources.ApplyResources(this.panel_Text_Min_circle, "panel_Text_Min_circle");
            this.panel_Text_Min_circle.Name = "panel_Text_Min_circle";
            this.panel_Text_Min_circle.Click += new System.EventHandler(this.panel_Text_Min_circle_Click);
            this.panel_Text_Min_circle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Text_Min_circle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Text_Min_circle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Text_Min_circle
            // 
            this.button_Text_Min_circle.FlatAppearance.BorderSize = 0;
            this.button_Text_Min_circle.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Text_Min_circle.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Text_Min_circle, "button_Text_Min_circle");
            this.button_Text_Min_circle.Name = "button_Text_Min_circle";
            this.button_Text_Min_circle.UseVisualStyleBackColor = true;
            this.button_Text_Min_circle.Click += new System.EventHandler(this.panel_Text_Min_circle_Click);
            this.button_Text_Min_circle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Text_Min_circle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Text_Min_circle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Text_Min_circle
            // 
            resources.ApplyResources(this.checkBox_Text_Min_circle, "checkBox_Text_Min_circle");
            this.checkBox_Text_Min_circle.Name = "checkBox_Text_Min_circle";
            this.checkBox_Text_Min_circle.UseVisualStyleBackColor = true;
            this.checkBox_Text_Min_circle.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Text_Min_rotation
            // 
            this.panel_Text_Min_rotation.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Text_Min_rotation.Controls.Add(this.button_Text_Min_rotation);
            this.panel_Text_Min_rotation.Controls.Add(this.checkBox_Text_Min_rotation);
            resources.ApplyResources(this.panel_Text_Min_rotation, "panel_Text_Min_rotation");
            this.panel_Text_Min_rotation.Name = "panel_Text_Min_rotation";
            this.panel_Text_Min_rotation.Click += new System.EventHandler(this.panel_Text_Min_rotation_Click);
            this.panel_Text_Min_rotation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Text_Min_rotation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Text_Min_rotation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Text_Min_rotation
            // 
            this.button_Text_Min_rotation.FlatAppearance.BorderSize = 0;
            this.button_Text_Min_rotation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Text_Min_rotation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Text_Min_rotation, "button_Text_Min_rotation");
            this.button_Text_Min_rotation.Name = "button_Text_Min_rotation";
            this.button_Text_Min_rotation.UseVisualStyleBackColor = true;
            this.button_Text_Min_rotation.Click += new System.EventHandler(this.panel_Text_Min_rotation_Click);
            this.button_Text_Min_rotation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Text_Min_rotation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Text_Min_rotation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Text_Min_rotation
            // 
            resources.ApplyResources(this.checkBox_Text_Min_rotation, "checkBox_Text_Min_rotation");
            this.checkBox_Text_Min_rotation.Name = "checkBox_Text_Min_rotation";
            this.checkBox_Text_Min_rotation.UseVisualStyleBackColor = true;
            this.checkBox_Text_Min_rotation.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Text_CityName
            // 
            this.panel_Text_CityName.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Text_CityName.Controls.Add(this.button_Text_CityName);
            this.panel_Text_CityName.Controls.Add(this.checkBox_Text_CityName);
            resources.ApplyResources(this.panel_Text_CityName, "panel_Text_CityName");
            this.panel_Text_CityName.Name = "panel_Text_CityName";
            this.panel_Text_CityName.Click += new System.EventHandler(this.panel_Text_CityName_Click);
            this.panel_Text_CityName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Text_CityName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Text_CityName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Text_CityName
            // 
            this.button_Text_CityName.FlatAppearance.BorderSize = 0;
            this.button_Text_CityName.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Text_CityName.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Text_CityName, "button_Text_CityName");
            this.button_Text_CityName.Image = global::ControlLibrary.Properties.Resources.text_fields;
            this.button_Text_CityName.Name = "button_Text_CityName";
            this.button_Text_CityName.UseVisualStyleBackColor = true;
            this.button_Text_CityName.Click += new System.EventHandler(this.panel_Text_CityName_Click);
            this.button_Text_CityName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Text_CityName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Text_CityName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Text_CityName
            // 
            resources.ApplyResources(this.checkBox_Text_CityName, "checkBox_Text_CityName");
            this.checkBox_Text_CityName.Name = "checkBox_Text_CityName";
            this.checkBox_Text_CityName.UseVisualStyleBackColor = true;
            this.checkBox_Text_CityName.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Number_Min
            // 
            this.panel_Number_Min.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Number_Min.Controls.Add(this.button_Number_Min);
            this.panel_Number_Min.Controls.Add(this.checkBox_Number_Min);
            resources.ApplyResources(this.panel_Number_Min, "panel_Number_Min");
            this.panel_Number_Min.Name = "panel_Number_Min";
            this.panel_Number_Min.Click += new System.EventHandler(this.panel_Number_Min_Click);
            this.panel_Number_Min.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Number_Min.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Number_Min.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Number_Min
            // 
            this.button_Number_Min.FlatAppearance.BorderSize = 0;
            this.button_Number_Min.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Number_Min.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Number_Min, "button_Number_Min");
            this.button_Number_Min.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Number_Min.Name = "button_Number_Min";
            this.button_Number_Min.UseVisualStyleBackColor = true;
            this.button_Number_Min.Click += new System.EventHandler(this.panel_Number_Min_Click);
            this.button_Number_Min.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Number_Min.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Number_Min.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Number_Min
            // 
            resources.ApplyResources(this.checkBox_Number_Min, "checkBox_Number_Min");
            this.checkBox_Number_Min.Name = "checkBox_Number_Min";
            this.checkBox_Number_Min.UseVisualStyleBackColor = true;
            this.checkBox_Number_Min.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Images
            // 
            this.panel_Images.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Images.Controls.Add(this.checkBox_Images);
            this.panel_Images.Controls.Add(this.button_Images);
            resources.ApplyResources(this.panel_Images, "panel_Images");
            this.panel_Images.Name = "panel_Images";
            this.toolTip_Weather.SetToolTip(this.panel_Images, resources.GetString("panel_Images.ToolTip"));
            this.panel_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.panel_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Images
            // 
            resources.ApplyResources(this.checkBox_Images, "checkBox_Images");
            this.checkBox_Images.Name = "checkBox_Images";
            this.checkBox_Images.UseVisualStyleBackColor = true;
            this.checkBox_Images.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_Images
            // 
            this.button_Images.FlatAppearance.BorderSize = 0;
            this.button_Images.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Images.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Images, "button_Images");
            this.button_Images.Image = global::ControlLibrary.Properties.Resources.images_18;
            this.button_Images.Name = "button_Images";
            this.toolTip_Weather.SetToolTip(this.button_Images, resources.GetString("button_Images.ToolTip"));
            this.button_Images.UseVisualStyleBackColor = true;
            this.button_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.button_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_Number
            // 
            this.panel_Number.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Number.Controls.Add(this.button_Number);
            this.panel_Number.Controls.Add(this.checkBox_Number);
            resources.ApplyResources(this.panel_Number, "panel_Number");
            this.panel_Number.Name = "panel_Number";
            this.panel_Number.Click += new System.EventHandler(this.panel_Number_Click);
            this.panel_Number.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Number.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Number.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Number
            // 
            this.button_Number.FlatAppearance.BorderSize = 0;
            this.button_Number.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Number.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Number, "button_Number");
            this.button_Number.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Number.Name = "button_Number";
            this.button_Number.UseVisualStyleBackColor = true;
            this.button_Number.Click += new System.EventHandler(this.panel_Number_Click);
            this.button_Number.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Number.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Number.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Number
            // 
            resources.ApplyResources(this.checkBox_Number, "checkBox_Number");
            this.checkBox_Number.Name = "checkBox_Number";
            this.checkBox_Number.UseVisualStyleBackColor = true;
            this.checkBox_Number.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Number_Max
            // 
            this.panel_Number_Max.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Number_Max.Controls.Add(this.button_Number_Max);
            this.panel_Number_Max.Controls.Add(this.checkBox_Number_Max);
            resources.ApplyResources(this.panel_Number_Max, "panel_Number_Max");
            this.panel_Number_Max.Name = "panel_Number_Max";
            this.panel_Number_Max.Click += new System.EventHandler(this.panel_Number_Max_Click);
            this.panel_Number_Max.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Number_Max.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Number_Max.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Number_Max
            // 
            this.button_Number_Max.FlatAppearance.BorderSize = 0;
            this.button_Number_Max.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Number_Max.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Number_Max, "button_Number_Max");
            this.button_Number_Max.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Number_Max.Name = "button_Number_Max";
            this.button_Number_Max.UseVisualStyleBackColor = true;
            this.button_Number_Max.Click += new System.EventHandler(this.panel_Number_Max_Click);
            this.button_Number_Max.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Number_Max.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Number_Max.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Number_Max
            // 
            resources.ApplyResources(this.checkBox_Number_Max, "checkBox_Number_Max");
            this.checkBox_Number_Max.Name = "checkBox_Number_Max";
            this.checkBox_Number_Max.UseVisualStyleBackColor = true;
            this.checkBox_Number_Max.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Icon
            // 
            this.panel_Icon.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Icon.Controls.Add(this.button_Icon);
            this.panel_Icon.Controls.Add(this.checkBox_Icon);
            resources.ApplyResources(this.panel_Icon, "panel_Icon");
            this.panel_Icon.Name = "panel_Icon";
            this.panel_Icon.Click += new System.EventHandler(this.panel_Icon_Click);
            this.panel_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Icon
            // 
            this.button_Icon.FlatAppearance.BorderSize = 0;
            this.button_Icon.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Icon.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_Icon, "button_Icon");
            this.button_Icon.Image = global::ControlLibrary.Properties.Resources.wallpaper_18;
            this.button_Icon.Name = "button_Icon";
            this.button_Icon.UseVisualStyleBackColor = true;
            this.button_Icon.Click += new System.EventHandler(this.panel_Icon_Click);
            this.button_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Icon
            // 
            resources.ApplyResources(this.checkBox_Icon, "checkBox_Icon");
            this.checkBox_Icon.Name = "checkBox_Icon";
            this.checkBox_Icon.UseVisualStyleBackColor = true;
            this.checkBox_Icon.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_ElementName
            // 
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.weatherIcon;
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // toolTip_Weather
            // 
            this.toolTip_Weather.AutoPopDelay = 32000;
            this.toolTip_Weather.InitialDelay = 500;
            this.toolTip_Weather.ReshowDelay = 100;
            this.toolTip_Weather.ToolTipTitle = "Weather images";
            // 
            // UCtrl_Weather_Elm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_Arrow_Down);
            this.Controls.Add(this.pictureBox_NotShow);
            this.Controls.Add(this.pictureBox_Arrow_Right);
            this.Controls.Add(this.pictureBox_Show);
            this.Controls.Add(this.pictureBox_Del);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button_ElementName);
            this.Name = "UCtrl_Weather_Elm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Text_Max_circle.ResumeLayout(false);
            this.panel_Text_Max_circle.PerformLayout();
            this.panel_Text_Max_rotation.ResumeLayout(false);
            this.panel_Text_Max_rotation.PerformLayout();
            this.panel_Text_Min_circle.ResumeLayout(false);
            this.panel_Text_Min_circle.PerformLayout();
            this.panel_Text_Min_rotation.ResumeLayout(false);
            this.panel_Text_Min_rotation.PerformLayout();
            this.panel_Text_CityName.ResumeLayout(false);
            this.panel_Text_CityName.PerformLayout();
            this.panel_Number_Min.ResumeLayout(false);
            this.panel_Number_Min.PerformLayout();
            this.panel_Images.ResumeLayout(false);
            this.panel_Images.PerformLayout();
            this.panel_Number.ResumeLayout(false);
            this.panel_Number.PerformLayout();
            this.panel_Number_Max.ResumeLayout(false);
            this.panel_Number_Max.PerformLayout();
            this.panel_Icon.ResumeLayout(false);
            this.panel_Icon.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Arrow_Down;
        private System.Windows.Forms.PictureBox pictureBox_NotShow;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Right;
        private System.Windows.Forms.PictureBox pictureBox_Show;
        private System.Windows.Forms.PictureBox pictureBox_Del;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_Images;
        public System.Windows.Forms.CheckBox checkBox_Images;
        private System.Windows.Forms.Button button_Images;
        private System.Windows.Forms.Panel panel_Number;
        private System.Windows.Forms.Button button_Number;
        public System.Windows.Forms.CheckBox checkBox_Number;
        private System.Windows.Forms.Panel panel_Number_Max;
        private System.Windows.Forms.Button button_Number_Max;
        public System.Windows.Forms.CheckBox checkBox_Number_Max;
        private System.Windows.Forms.Panel panel_Icon;
        private System.Windows.Forms.Button button_Icon;
        public System.Windows.Forms.CheckBox checkBox_Icon;
        private System.Windows.Forms.Button button_ElementName;
        private System.Windows.Forms.Panel panel_Number_Min;
        private System.Windows.Forms.Button button_Number_Min;
        public System.Windows.Forms.CheckBox checkBox_Number_Min;
        private System.Windows.Forms.Panel panel_Text_CityName;
        private System.Windows.Forms.Button button_Text_CityName;
        public System.Windows.Forms.CheckBox checkBox_Text_CityName;
        private System.Windows.Forms.ToolTip toolTip_Weather;
        private System.Windows.Forms.Panel panel_Text_Min_rotation;
        private System.Windows.Forms.Button button_Text_Min_rotation;
        public System.Windows.Forms.CheckBox checkBox_Text_Min_rotation;
        private System.Windows.Forms.Panel panel_Text_Min_circle;
        private System.Windows.Forms.Button button_Text_Min_circle;
        public System.Windows.Forms.CheckBox checkBox_Text_Min_circle;
        private System.Windows.Forms.Panel panel_Text_Max_rotation;
        private System.Windows.Forms.Button button_Text_Max_rotation;
        public System.Windows.Forms.CheckBox checkBox_Text_Max_rotation;
        private System.Windows.Forms.Panel panel_Text_Max_circle;
        private System.Windows.Forms.Button button_Text_Max_circle;
        public System.Windows.Forms.CheckBox checkBox_Text_Max_circle;
    }
}
