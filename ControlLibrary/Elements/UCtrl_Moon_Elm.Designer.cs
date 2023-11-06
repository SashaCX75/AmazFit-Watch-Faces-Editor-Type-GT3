namespace ControlLibrary
{
    partial class UCtrl_Moon_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Moon_Elm));
            this.toolTip_Moon = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Sunrise_Font = new System.Windows.Forms.Panel();
            this.button_Sunrise_Font = new System.Windows.Forms.Button();
            this.checkBox_Sunrise_Font = new System.Windows.Forms.CheckBox();
            this.panel_Sunset_Font = new System.Windows.Forms.Panel();
            this.button_Sunset_Font = new System.Windows.Forms.Button();
            this.checkBox_Sunset_Font = new System.Windows.Forms.CheckBox();
            this.panel_Images = new System.Windows.Forms.Panel();
            this.checkBox_Images = new System.Windows.Forms.CheckBox();
            this.button_Images = new System.Windows.Forms.Button();
            this.panel_Pointer = new System.Windows.Forms.Panel();
            this.button_Pointer = new System.Windows.Forms.Button();
            this.checkBox_Pointer = new System.Windows.Forms.CheckBox();
            this.panel_Sunset = new System.Windows.Forms.Panel();
            this.button_Sunset = new System.Windows.Forms.Button();
            this.checkBox_Sunset = new System.Windows.Forms.CheckBox();
            this.panel_Sunrise = new System.Windows.Forms.Panel();
            this.button_Sunrise = new System.Windows.Forms.Button();
            this.checkBox_Sunrise = new System.Windows.Forms.CheckBox();
            this.panel_Sunset_Sunrise = new System.Windows.Forms.Panel();
            this.button_Sunset_Sunrise = new System.Windows.Forms.Button();
            this.checkBox_Sunset_Sunrise = new System.Windows.Forms.CheckBox();
            this.panel_Icon = new System.Windows.Forms.Panel();
            this.button_Icon = new System.Windows.Forms.Button();
            this.checkBox_Icon = new System.Windows.Forms.CheckBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Sunrise_Font.SuspendLayout();
            this.panel_Sunset_Font.SuspendLayout();
            this.panel_Images.SuspendLayout();
            this.panel_Pointer.SuspendLayout();
            this.panel_Sunset.SuspendLayout();
            this.panel_Sunrise.SuspendLayout();
            this.panel_Sunset_Sunrise.SuspendLayout();
            this.panel_Icon.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip_Moon
            // 
            this.toolTip_Moon.Active = false;
            // 
            // pictureBox_Arrow_Down
            // 
            resources.ApplyResources(this.pictureBox_Arrow_Down, "pictureBox_Arrow_Down");
            this.pictureBox_Arrow_Down.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_down;
            this.pictureBox_Arrow_Down.Name = "pictureBox_Arrow_Down";
            this.pictureBox_Arrow_Down.TabStop = false;
            this.toolTip_Moon.SetToolTip(this.pictureBox_Arrow_Down, resources.GetString("pictureBox_Arrow_Down.ToolTip"));
            this.pictureBox_Arrow_Down.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_NotShow
            // 
            resources.ApplyResources(this.pictureBox_NotShow, "pictureBox_NotShow");
            this.pictureBox_NotShow.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_off_black_24;
            this.pictureBox_NotShow.Name = "pictureBox_NotShow";
            this.pictureBox_NotShow.TabStop = false;
            this.toolTip_Moon.SetToolTip(this.pictureBox_NotShow, resources.GetString("pictureBox_NotShow.ToolTip"));
            this.pictureBox_NotShow.Click += new System.EventHandler(this.pictureBox_NotShow_Click);
            // 
            // pictureBox_Arrow_Right
            // 
            resources.ApplyResources(this.pictureBox_Arrow_Right, "pictureBox_Arrow_Right");
            this.pictureBox_Arrow_Right.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_right;
            this.pictureBox_Arrow_Right.Name = "pictureBox_Arrow_Right";
            this.pictureBox_Arrow_Right.TabStop = false;
            this.toolTip_Moon.SetToolTip(this.pictureBox_Arrow_Right, resources.GetString("pictureBox_Arrow_Right.ToolTip"));
            this.pictureBox_Arrow_Right.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_Show
            // 
            resources.ApplyResources(this.pictureBox_Show, "pictureBox_Show");
            this.pictureBox_Show.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_black_24;
            this.pictureBox_Show.Name = "pictureBox_Show";
            this.pictureBox_Show.TabStop = false;
            this.toolTip_Moon.SetToolTip(this.pictureBox_Show, resources.GetString("pictureBox_Show.ToolTip"));
            this.pictureBox_Show.Click += new System.EventHandler(this.pictureBox_Show_Click);
            // 
            // pictureBox_Del
            // 
            resources.ApplyResources(this.pictureBox_Del, "pictureBox_Del");
            this.pictureBox_Del.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_delete_forever_black_24;
            this.pictureBox_Del.Name = "pictureBox_Del";
            this.pictureBox_Del.TabStop = false;
            this.toolTip_Moon.SetToolTip(this.pictureBox_Del, resources.GetString("pictureBox_Del.ToolTip"));
            this.pictureBox_Del.Click += new System.EventHandler(this.pictureBox_Del_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.Controls.Add(this.panel_Sunrise_Font, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel_Sunset_Font, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel_Images, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_Pointer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_Sunset, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_Sunrise, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_Sunset_Sunrise, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel_Icon, 0, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.toolTip_Moon.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Sunrise_Font
            // 
            resources.ApplyResources(this.panel_Sunrise_Font, "panel_Sunrise_Font");
            this.panel_Sunrise_Font.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Sunrise_Font.Controls.Add(this.button_Sunrise_Font);
            this.panel_Sunrise_Font.Controls.Add(this.checkBox_Sunrise_Font);
            this.panel_Sunrise_Font.Name = "panel_Sunrise_Font";
            this.toolTip_Moon.SetToolTip(this.panel_Sunrise_Font, resources.GetString("panel_Sunrise_Font.ToolTip"));
            this.panel_Sunrise_Font.Click += new System.EventHandler(this.panel_Sunrise_Font_Click);
            this.panel_Sunrise_Font.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Sunrise_Font.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Sunrise_Font.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Sunrise_Font
            // 
            resources.ApplyResources(this.button_Sunrise_Font, "button_Sunrise_Font");
            this.button_Sunrise_Font.FlatAppearance.BorderSize = 0;
            this.button_Sunrise_Font.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunrise_Font.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunrise_Font.Image = global::ControlLibrary.Properties.Resources.text_fields;
            this.button_Sunrise_Font.Name = "button_Sunrise_Font";
            this.toolTip_Moon.SetToolTip(this.button_Sunrise_Font, resources.GetString("button_Sunrise_Font.ToolTip"));
            this.button_Sunrise_Font.UseVisualStyleBackColor = true;
            this.button_Sunrise_Font.Click += new System.EventHandler(this.panel_Sunrise_Font_Click);
            this.button_Sunrise_Font.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Sunrise_Font.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Sunrise_Font.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Sunrise_Font
            // 
            resources.ApplyResources(this.checkBox_Sunrise_Font, "checkBox_Sunrise_Font");
            this.checkBox_Sunrise_Font.Name = "checkBox_Sunrise_Font";
            this.toolTip_Moon.SetToolTip(this.checkBox_Sunrise_Font, resources.GetString("checkBox_Sunrise_Font.ToolTip"));
            this.checkBox_Sunrise_Font.UseVisualStyleBackColor = true;
            this.checkBox_Sunrise_Font.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Sunset_Font
            // 
            resources.ApplyResources(this.panel_Sunset_Font, "panel_Sunset_Font");
            this.panel_Sunset_Font.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Sunset_Font.Controls.Add(this.button_Sunset_Font);
            this.panel_Sunset_Font.Controls.Add(this.checkBox_Sunset_Font);
            this.panel_Sunset_Font.Name = "panel_Sunset_Font";
            this.toolTip_Moon.SetToolTip(this.panel_Sunset_Font, resources.GetString("panel_Sunset_Font.ToolTip"));
            this.panel_Sunset_Font.Click += new System.EventHandler(this.panel_Sunset_Font_Click);
            this.panel_Sunset_Font.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Sunset_Font.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Sunset_Font.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Sunset_Font
            // 
            resources.ApplyResources(this.button_Sunset_Font, "button_Sunset_Font");
            this.button_Sunset_Font.FlatAppearance.BorderSize = 0;
            this.button_Sunset_Font.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunset_Font.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunset_Font.Image = global::ControlLibrary.Properties.Resources.text_fields;
            this.button_Sunset_Font.Name = "button_Sunset_Font";
            this.toolTip_Moon.SetToolTip(this.button_Sunset_Font, resources.GetString("button_Sunset_Font.ToolTip"));
            this.button_Sunset_Font.UseVisualStyleBackColor = true;
            this.button_Sunset_Font.Click += new System.EventHandler(this.panel_Sunset_Font_Click);
            this.button_Sunset_Font.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Sunset_Font.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Sunset_Font.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Sunset_Font
            // 
            resources.ApplyResources(this.checkBox_Sunset_Font, "checkBox_Sunset_Font");
            this.checkBox_Sunset_Font.Name = "checkBox_Sunset_Font";
            this.toolTip_Moon.SetToolTip(this.checkBox_Sunset_Font, resources.GetString("checkBox_Sunset_Font.ToolTip"));
            this.checkBox_Sunset_Font.UseVisualStyleBackColor = true;
            this.checkBox_Sunset_Font.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Images
            // 
            resources.ApplyResources(this.panel_Images, "panel_Images");
            this.panel_Images.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Images.Controls.Add(this.checkBox_Images);
            this.panel_Images.Controls.Add(this.button_Images);
            this.panel_Images.Name = "panel_Images";
            this.toolTip_Moon.SetToolTip(this.panel_Images, resources.GetString("panel_Images.ToolTip"));
            this.panel_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.panel_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Images
            // 
            resources.ApplyResources(this.checkBox_Images, "checkBox_Images");
            this.checkBox_Images.Name = "checkBox_Images";
            this.toolTip_Moon.SetToolTip(this.checkBox_Images, resources.GetString("checkBox_Images.ToolTip"));
            this.checkBox_Images.UseVisualStyleBackColor = true;
            this.checkBox_Images.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_Images
            // 
            resources.ApplyResources(this.button_Images, "button_Images");
            this.button_Images.FlatAppearance.BorderSize = 0;
            this.button_Images.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Images.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Images.Image = global::ControlLibrary.Properties.Resources.images_18;
            this.button_Images.Name = "button_Images";
            this.toolTip_Moon.SetToolTip(this.button_Images, resources.GetString("button_Images.ToolTip"));
            this.button_Images.UseVisualStyleBackColor = true;
            this.button_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.button_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_Pointer
            // 
            resources.ApplyResources(this.panel_Pointer, "panel_Pointer");
            this.panel_Pointer.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Pointer.Controls.Add(this.button_Pointer);
            this.panel_Pointer.Controls.Add(this.checkBox_Pointer);
            this.panel_Pointer.Name = "panel_Pointer";
            this.toolTip_Moon.SetToolTip(this.panel_Pointer, resources.GetString("panel_Pointer.ToolTip"));
            this.panel_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.panel_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Pointer
            // 
            resources.ApplyResources(this.button_Pointer, "button_Pointer");
            this.button_Pointer.FlatAppearance.BorderSize = 0;
            this.button_Pointer.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Pointer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Pointer.Image = global::ControlLibrary.Properties.Resources.pointer;
            this.button_Pointer.Name = "button_Pointer";
            this.toolTip_Moon.SetToolTip(this.button_Pointer, resources.GetString("button_Pointer.ToolTip"));
            this.button_Pointer.UseVisualStyleBackColor = true;
            this.button_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.button_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Pointer
            // 
            resources.ApplyResources(this.checkBox_Pointer, "checkBox_Pointer");
            this.checkBox_Pointer.Name = "checkBox_Pointer";
            this.toolTip_Moon.SetToolTip(this.checkBox_Pointer, resources.GetString("checkBox_Pointer.ToolTip"));
            this.checkBox_Pointer.UseVisualStyleBackColor = true;
            this.checkBox_Pointer.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Sunset
            // 
            resources.ApplyResources(this.panel_Sunset, "panel_Sunset");
            this.panel_Sunset.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Sunset.Controls.Add(this.button_Sunset);
            this.panel_Sunset.Controls.Add(this.checkBox_Sunset);
            this.panel_Sunset.Name = "panel_Sunset";
            this.toolTip_Moon.SetToolTip(this.panel_Sunset, resources.GetString("panel_Sunset.ToolTip"));
            this.panel_Sunset.Click += new System.EventHandler(this.panel_Sunset_Click);
            this.panel_Sunset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Sunset.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Sunset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Sunset
            // 
            resources.ApplyResources(this.button_Sunset, "button_Sunset");
            this.button_Sunset.FlatAppearance.BorderSize = 0;
            this.button_Sunset.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunset.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunset.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Sunset.Name = "button_Sunset";
            this.toolTip_Moon.SetToolTip(this.button_Sunset, resources.GetString("button_Sunset.ToolTip"));
            this.button_Sunset.UseVisualStyleBackColor = true;
            this.button_Sunset.Click += new System.EventHandler(this.panel_Sunset_Click);
            this.button_Sunset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Sunset.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Sunset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Sunset
            // 
            resources.ApplyResources(this.checkBox_Sunset, "checkBox_Sunset");
            this.checkBox_Sunset.Name = "checkBox_Sunset";
            this.toolTip_Moon.SetToolTip(this.checkBox_Sunset, resources.GetString("checkBox_Sunset.ToolTip"));
            this.checkBox_Sunset.UseVisualStyleBackColor = true;
            this.checkBox_Sunset.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Sunrise
            // 
            resources.ApplyResources(this.panel_Sunrise, "panel_Sunrise");
            this.panel_Sunrise.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Sunrise.Controls.Add(this.button_Sunrise);
            this.panel_Sunrise.Controls.Add(this.checkBox_Sunrise);
            this.panel_Sunrise.Name = "panel_Sunrise";
            this.toolTip_Moon.SetToolTip(this.panel_Sunrise, resources.GetString("panel_Sunrise.ToolTip"));
            this.panel_Sunrise.Click += new System.EventHandler(this.panel_Sunrise_Click);
            this.panel_Sunrise.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Sunrise.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Sunrise.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Sunrise
            // 
            resources.ApplyResources(this.button_Sunrise, "button_Sunrise");
            this.button_Sunrise.FlatAppearance.BorderSize = 0;
            this.button_Sunrise.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunrise.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunrise.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Sunrise.Name = "button_Sunrise";
            this.toolTip_Moon.SetToolTip(this.button_Sunrise, resources.GetString("button_Sunrise.ToolTip"));
            this.button_Sunrise.UseVisualStyleBackColor = true;
            this.button_Sunrise.Click += new System.EventHandler(this.panel_Sunrise_Click);
            this.button_Sunrise.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Sunrise.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Sunrise.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Sunrise
            // 
            resources.ApplyResources(this.checkBox_Sunrise, "checkBox_Sunrise");
            this.checkBox_Sunrise.Name = "checkBox_Sunrise";
            this.toolTip_Moon.SetToolTip(this.checkBox_Sunrise, resources.GetString("checkBox_Sunrise.ToolTip"));
            this.checkBox_Sunrise.UseVisualStyleBackColor = true;
            this.checkBox_Sunrise.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Sunset_Sunrise
            // 
            resources.ApplyResources(this.panel_Sunset_Sunrise, "panel_Sunset_Sunrise");
            this.panel_Sunset_Sunrise.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Sunset_Sunrise.Controls.Add(this.button_Sunset_Sunrise);
            this.panel_Sunset_Sunrise.Controls.Add(this.checkBox_Sunset_Sunrise);
            this.panel_Sunset_Sunrise.Name = "panel_Sunset_Sunrise";
            this.toolTip_Moon.SetToolTip(this.panel_Sunset_Sunrise, resources.GetString("panel_Sunset_Sunrise.ToolTip"));
            this.panel_Sunset_Sunrise.Click += new System.EventHandler(this.panel_Sunset_Sunrise_Click);
            this.panel_Sunset_Sunrise.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Sunset_Sunrise.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Sunset_Sunrise.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Sunset_Sunrise
            // 
            resources.ApplyResources(this.button_Sunset_Sunrise, "button_Sunset_Sunrise");
            this.button_Sunset_Sunrise.FlatAppearance.BorderSize = 0;
            this.button_Sunset_Sunrise.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunset_Sunrise.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Sunset_Sunrise.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Sunset_Sunrise.Name = "button_Sunset_Sunrise";
            this.toolTip_Moon.SetToolTip(this.button_Sunset_Sunrise, resources.GetString("button_Sunset_Sunrise.ToolTip"));
            this.button_Sunset_Sunrise.UseVisualStyleBackColor = true;
            this.button_Sunset_Sunrise.Click += new System.EventHandler(this.panel_Sunset_Sunrise_Click);
            this.button_Sunset_Sunrise.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Sunset_Sunrise.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Sunset_Sunrise.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Sunset_Sunrise
            // 
            resources.ApplyResources(this.checkBox_Sunset_Sunrise, "checkBox_Sunset_Sunrise");
            this.checkBox_Sunset_Sunrise.Name = "checkBox_Sunset_Sunrise";
            this.toolTip_Moon.SetToolTip(this.checkBox_Sunset_Sunrise, resources.GetString("checkBox_Sunset_Sunrise.ToolTip"));
            this.checkBox_Sunset_Sunrise.UseVisualStyleBackColor = true;
            this.checkBox_Sunset_Sunrise.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Icon
            // 
            resources.ApplyResources(this.panel_Icon, "panel_Icon");
            this.panel_Icon.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Icon.Controls.Add(this.button_Icon);
            this.panel_Icon.Controls.Add(this.checkBox_Icon);
            this.panel_Icon.Name = "panel_Icon";
            this.toolTip_Moon.SetToolTip(this.panel_Icon, resources.GetString("panel_Icon.ToolTip"));
            this.panel_Icon.Click += new System.EventHandler(this.panel_Icon_Click);
            this.panel_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Icon
            // 
            resources.ApplyResources(this.button_Icon, "button_Icon");
            this.button_Icon.FlatAppearance.BorderSize = 0;
            this.button_Icon.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Icon.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Icon.Image = global::ControlLibrary.Properties.Resources.wallpaper_18;
            this.button_Icon.Name = "button_Icon";
            this.toolTip_Moon.SetToolTip(this.button_Icon, resources.GetString("button_Icon.ToolTip"));
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
            this.toolTip_Moon.SetToolTip(this.checkBox_Icon, resources.GetString("checkBox_Icon.ToolTip"));
            this.checkBox_Icon.UseVisualStyleBackColor = true;
            this.checkBox_Icon.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_ElementName
            // 
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.moon;
            this.button_ElementName.Name = "button_ElementName";
            this.toolTip_Moon.SetToolTip(this.button_ElementName, resources.GetString("button_ElementName.ToolTip"));
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // UCtrl_Moon_Elm
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
            this.Name = "UCtrl_Moon_Elm";
            this.toolTip_Moon.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Sunrise_Font.ResumeLayout(false);
            this.panel_Sunrise_Font.PerformLayout();
            this.panel_Sunset_Font.ResumeLayout(false);
            this.panel_Sunset_Font.PerformLayout();
            this.panel_Images.ResumeLayout(false);
            this.panel_Images.PerformLayout();
            this.panel_Pointer.ResumeLayout(false);
            this.panel_Pointer.PerformLayout();
            this.panel_Sunset.ResumeLayout(false);
            this.panel_Sunset.PerformLayout();
            this.panel_Sunrise.ResumeLayout(false);
            this.panel_Sunrise.PerformLayout();
            this.panel_Sunset_Sunrise.ResumeLayout(false);
            this.panel_Sunset_Sunrise.PerformLayout();
            this.panel_Icon.ResumeLayout(false);
            this.panel_Icon.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip_Moon;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Down;
        private System.Windows.Forms.PictureBox pictureBox_NotShow;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Right;
        private System.Windows.Forms.PictureBox pictureBox_Show;
        private System.Windows.Forms.PictureBox pictureBox_Del;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_Sunrise_Font;
        private System.Windows.Forms.Button button_Sunrise_Font;
        public System.Windows.Forms.CheckBox checkBox_Sunrise_Font;
        private System.Windows.Forms.Panel panel_Sunset_Font;
        private System.Windows.Forms.Button button_Sunset_Font;
        public System.Windows.Forms.CheckBox checkBox_Sunset_Font;
        private System.Windows.Forms.Panel panel_Images;
        public System.Windows.Forms.CheckBox checkBox_Images;
        private System.Windows.Forms.Button button_Images;
        private System.Windows.Forms.Panel panel_Pointer;
        private System.Windows.Forms.Button button_Pointer;
        public System.Windows.Forms.CheckBox checkBox_Pointer;
        private System.Windows.Forms.Panel panel_Sunset;
        private System.Windows.Forms.Button button_Sunset;
        public System.Windows.Forms.CheckBox checkBox_Sunset;
        private System.Windows.Forms.Panel panel_Sunrise;
        private System.Windows.Forms.Button button_Sunrise;
        public System.Windows.Forms.CheckBox checkBox_Sunrise;
        private System.Windows.Forms.Panel panel_Sunset_Sunrise;
        private System.Windows.Forms.Button button_Sunset_Sunrise;
        public System.Windows.Forms.CheckBox checkBox_Sunset_Sunrise;
        private System.Windows.Forms.Panel panel_Icon;
        private System.Windows.Forms.Button button_Icon;
        public System.Windows.Forms.CheckBox checkBox_Icon;
        private System.Windows.Forms.Button button_ElementName;
    }
}
