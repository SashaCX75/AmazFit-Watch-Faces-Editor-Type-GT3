
namespace ControlLibrary
{
    partial class UCtrl_DigitalTime_Elm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Minutes = new System.Windows.Forms.Panel();
            this.button_Minutes = new System.Windows.Forms.Button();
            this.checkBox_Minutes = new System.Windows.Forms.CheckBox();
            this.panel_AmPm = new System.Windows.Forms.Panel();
            this.button_AmPm = new System.Windows.Forms.Button();
            this.checkBox_AmPm = new System.Windows.Forms.CheckBox();
            this.panel_Hours = new System.Windows.Forms.Panel();
            this.checkBox_Hours = new System.Windows.Forms.CheckBox();
            this.button_Hours = new System.Windows.Forms.Button();
            this.panel_Seconds = new System.Windows.Forms.Panel();
            this.button_Seconds = new System.Windows.Forms.Button();
            this.checkBox_Seconds = new System.Windows.Forms.CheckBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Minutes.SuspendLayout();
            this.panel_AmPm.SuspendLayout();
            this.panel_Hours.SuspendLayout();
            this.panel_Seconds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel_Minutes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_AmPm, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_Hours, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_Seconds, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 101);
            this.tableLayoutPanel1.TabIndex = 3;
            this.tableLayoutPanel1.Visible = false;
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Minutes
            // 
            this.panel_Minutes.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Minutes.Controls.Add(this.button_Minutes);
            this.panel_Minutes.Controls.Add(this.checkBox_Minutes);
            this.panel_Minutes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Minutes.Location = new System.Drawing.Point(1, 26);
            this.panel_Minutes.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Minutes.Name = "panel_Minutes";
            this.panel_Minutes.Size = new System.Drawing.Size(218, 24);
            this.panel_Minutes.TabIndex = 1;
            this.panel_Minutes.Click += new System.EventHandler(this.panel_Minutes_Click);
            this.panel_Minutes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Minutes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Minutes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Minutes
            // 
            this.button_Minutes.FlatAppearance.BorderSize = 0;
            this.button_Minutes.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Minutes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Minutes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Minutes.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Minutes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Minutes.Location = new System.Drawing.Point(33, 0);
            this.button_Minutes.Name = "button_Minutes";
            this.button_Minutes.Size = new System.Drawing.Size(125, 24);
            this.button_Minutes.TabIndex = 3;
            this.button_Minutes.Text = "Минуты";
            this.button_Minutes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Minutes.UseVisualStyleBackColor = true;
            this.button_Minutes.Click += new System.EventHandler(this.panel_Minutes_Click);
            this.button_Minutes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Minutes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Minutes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Minutes
            // 
            this.checkBox_Minutes.AutoSize = true;
            this.checkBox_Minutes.Location = new System.Drawing.Point(20, 5);
            this.checkBox_Minutes.Name = "checkBox_Minutes";
            this.checkBox_Minutes.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Minutes.TabIndex = 0;
            this.checkBox_Minutes.UseVisualStyleBackColor = true;
            this.checkBox_Minutes.CheckedChanged += new System.EventHandler(this.checkBox_Hours_CheckedChanged);
            // 
            // panel_AmPm
            // 
            this.panel_AmPm.BackColor = System.Drawing.SystemColors.Control;
            this.panel_AmPm.Controls.Add(this.button_AmPm);
            this.panel_AmPm.Controls.Add(this.checkBox_AmPm);
            this.panel_AmPm.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_AmPm.Location = new System.Drawing.Point(1, 76);
            this.panel_AmPm.Margin = new System.Windows.Forms.Padding(0);
            this.panel_AmPm.Name = "panel_AmPm";
            this.panel_AmPm.Size = new System.Drawing.Size(218, 24);
            this.panel_AmPm.TabIndex = 6;
            this.panel_AmPm.Click += new System.EventHandler(this.panel_AmPm_Click);
            this.panel_AmPm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_AmPm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_AmPm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_AmPm
            // 
            this.button_AmPm.FlatAppearance.BorderSize = 0;
            this.button_AmPm.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_AmPm.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_AmPm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_AmPm.Image = global::ControlLibrary.Properties.Resources.outline_image_black_18;
            this.button_AmPm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_AmPm.Location = new System.Drawing.Point(33, 0);
            this.button_AmPm.Name = "button_AmPm";
            this.button_AmPm.Size = new System.Drawing.Size(155, 24);
            this.button_AmPm.TabIndex = 4;
            this.button_AmPm.Text = "Am/Pm";
            this.button_AmPm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_AmPm.UseVisualStyleBackColor = true;
            this.button_AmPm.Click += new System.EventHandler(this.panel_AmPm_Click);
            this.button_AmPm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_AmPm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_AmPm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_AmPm
            // 
            this.checkBox_AmPm.AutoSize = true;
            this.checkBox_AmPm.Location = new System.Drawing.Point(20, 5);
            this.checkBox_AmPm.Name = "checkBox_AmPm";
            this.checkBox_AmPm.Size = new System.Drawing.Size(15, 14);
            this.checkBox_AmPm.TabIndex = 0;
            this.checkBox_AmPm.UseVisualStyleBackColor = true;
            this.checkBox_AmPm.CheckedChanged += new System.EventHandler(this.checkBox_Hours_CheckedChanged);
            // 
            // panel_Hours
            // 
            this.panel_Hours.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Hours.Controls.Add(this.checkBox_Hours);
            this.panel_Hours.Controls.Add(this.button_Hours);
            this.panel_Hours.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Hours.Location = new System.Drawing.Point(1, 51);
            this.panel_Hours.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Hours.Name = "panel_Hours";
            this.panel_Hours.Size = new System.Drawing.Size(218, 24);
            this.panel_Hours.TabIndex = 0;
            this.panel_Hours.Click += new System.EventHandler(this.panel_Hours_Click);
            this.panel_Hours.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Hours.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Hours.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Hours
            // 
            this.checkBox_Hours.AutoSize = true;
            this.checkBox_Hours.Location = new System.Drawing.Point(20, 5);
            this.checkBox_Hours.Name = "checkBox_Hours";
            this.checkBox_Hours.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Hours.TabIndex = 0;
            this.checkBox_Hours.UseVisualStyleBackColor = true;
            this.checkBox_Hours.CheckedChanged += new System.EventHandler(this.checkBox_Hours_CheckedChanged);
            // 
            // button_Hours
            // 
            this.button_Hours.FlatAppearance.BorderSize = 0;
            this.button_Hours.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Hours.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Hours.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Hours.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Hours.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Hours.Location = new System.Drawing.Point(33, 0);
            this.button_Hours.Name = "button_Hours";
            this.button_Hours.Size = new System.Drawing.Size(125, 24);
            this.button_Hours.TabIndex = 2;
            this.button_Hours.Text = "Часы";
            this.button_Hours.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Hours.UseVisualStyleBackColor = true;
            this.button_Hours.Click += new System.EventHandler(this.panel_Hours_Click);
            this.button_Hours.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Hours.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Hours.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_Seconds
            // 
            this.panel_Seconds.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Seconds.Controls.Add(this.button_Seconds);
            this.panel_Seconds.Controls.Add(this.checkBox_Seconds);
            this.panel_Seconds.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Seconds.Location = new System.Drawing.Point(1, 1);
            this.panel_Seconds.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Seconds.Name = "panel_Seconds";
            this.panel_Seconds.Size = new System.Drawing.Size(218, 24);
            this.panel_Seconds.TabIndex = 2;
            this.panel_Seconds.Click += new System.EventHandler(this.panel_Seconds_Click);
            this.panel_Seconds.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Seconds.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Seconds.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Seconds
            // 
            this.button_Seconds.FlatAppearance.BorderSize = 0;
            this.button_Seconds.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Seconds.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Seconds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Seconds.Image = global::ControlLibrary.Properties.Resources.text_icon;
            this.button_Seconds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Seconds.Location = new System.Drawing.Point(33, 0);
            this.button_Seconds.Name = "button_Seconds";
            this.button_Seconds.Size = new System.Drawing.Size(125, 24);
            this.button_Seconds.TabIndex = 4;
            this.button_Seconds.Text = "Секунды";
            this.button_Seconds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Seconds.UseVisualStyleBackColor = true;
            this.button_Seconds.Click += new System.EventHandler(this.panel_Seconds_Click);
            this.button_Seconds.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Seconds.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Seconds.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Seconds
            // 
            this.checkBox_Seconds.AutoSize = true;
            this.checkBox_Seconds.Location = new System.Drawing.Point(20, 5);
            this.checkBox_Seconds.Name = "checkBox_Seconds";
            this.checkBox_Seconds.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Seconds.TabIndex = 0;
            this.checkBox_Seconds.UseVisualStyleBackColor = true;
            this.checkBox_Seconds.CheckedChanged += new System.EventHandler(this.checkBox_Hours_CheckedChanged);
            // 
            // pictureBox_NotShow
            // 
            this.pictureBox_NotShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_NotShow.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_off_black_24;
            this.pictureBox_NotShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_NotShow.Location = new System.Drawing.Point(101, 2);
            this.pictureBox_NotShow.Name = "pictureBox_NotShow";
            this.pictureBox_NotShow.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_NotShow.TabIndex = 7;
            this.pictureBox_NotShow.TabStop = false;
            this.pictureBox_NotShow.Visible = false;
            this.pictureBox_NotShow.Click += new System.EventHandler(this.pictureBox_NotShow_Click);
            // 
            // pictureBox_Arrow_Right
            // 
            this.pictureBox_Arrow_Right.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_right;
            this.pictureBox_Arrow_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Arrow_Right.Location = new System.Drawing.Point(1, 2);
            this.pictureBox_Arrow_Right.Name = "pictureBox_Arrow_Right";
            this.pictureBox_Arrow_Right.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Arrow_Right.TabIndex = 6;
            this.pictureBox_Arrow_Right.TabStop = false;
            this.pictureBox_Arrow_Right.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_Show
            // 
            this.pictureBox_Show.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Show.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_black_24;
            this.pictureBox_Show.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Show.Location = new System.Drawing.Point(163, 2);
            this.pictureBox_Show.Name = "pictureBox_Show";
            this.pictureBox_Show.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Show.TabIndex = 5;
            this.pictureBox_Show.TabStop = false;
            this.pictureBox_Show.Click += new System.EventHandler(this.pictureBox_Show_Click);
            // 
            // pictureBox_Del
            // 
            this.pictureBox_Del.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_delete_forever_black_24;
            this.pictureBox_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Del.Location = new System.Drawing.Point(192, 2);
            this.pictureBox_Del.Name = "pictureBox_Del";
            this.pictureBox_Del.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Del.TabIndex = 4;
            this.pictureBox_Del.TabStop = false;
            this.pictureBox_Del.Click += new System.EventHandler(this.pictureBox_Del_Click);
            // 
            // pictureBox_Arrow_Down
            // 
            this.pictureBox_Arrow_Down.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_down;
            this.pictureBox_Arrow_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Arrow_Down.Location = new System.Drawing.Point(131, 2);
            this.pictureBox_Arrow_Down.Name = "pictureBox_Arrow_Down";
            this.pictureBox_Arrow_Down.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Arrow_Down.TabIndex = 2;
            this.pictureBox_Arrow_Down.TabStop = false;
            this.pictureBox_Arrow_Down.Visible = false;
            this.pictureBox_Arrow_Down.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // button_ElementName
            // 
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            this.button_ElementName.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ElementName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.digital_clock_icon;
            this.button_ElementName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ElementName.Location = new System.Drawing.Point(0, 0);
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button_ElementName.Size = new System.Drawing.Size(220, 28);
            this.button_ElementName.TabIndex = 1;
            this.button_ElementName.Text = "Цыфровое время";
            this.button_ElementName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // UCtrl_DigitalTime_Elm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_NotShow);
            this.Controls.Add(this.pictureBox_Arrow_Right);
            this.Controls.Add(this.pictureBox_Show);
            this.Controls.Add(this.pictureBox_Del);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox_Arrow_Down);
            this.Controls.Add(this.button_ElementName);
            this.Name = "UCtrl_DigitalTime_Elm";
            this.Size = new System.Drawing.Size(220, 227);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Minutes.ResumeLayout(false);
            this.panel_Minutes.PerformLayout();
            this.panel_AmPm.ResumeLayout(false);
            this.panel_AmPm.PerformLayout();
            this.panel_Hours.ResumeLayout(false);
            this.panel_Hours.PerformLayout();
            this.panel_Seconds.ResumeLayout(false);
            this.panel_Seconds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ElementName;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Down;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_Seconds;
        private System.Windows.Forms.Panel panel_Minutes;
        private System.Windows.Forms.Panel panel_Hours;
        private System.Windows.Forms.PictureBox pictureBox_Del;
        private System.Windows.Forms.PictureBox pictureBox_Show;
        private System.Windows.Forms.PictureBox pictureBox_Arrow_Right;
        private System.Windows.Forms.Button button_Hours;
        private System.Windows.Forms.Button button_Seconds;
        private System.Windows.Forms.Button button_Minutes;
        private System.Windows.Forms.PictureBox pictureBox_NotShow;
        private System.Windows.Forms.Panel panel_AmPm;
        private System.Windows.Forms.Button button_AmPm;
        public System.Windows.Forms.CheckBox checkBox_Seconds;
        public System.Windows.Forms.CheckBox checkBox_Minutes;
        public System.Windows.Forms.CheckBox checkBox_Hours;
        public System.Windows.Forms.CheckBox checkBox_AmPm;
    }
}
