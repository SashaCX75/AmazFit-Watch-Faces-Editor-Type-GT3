
namespace ControlLibrary
{
    partial class UCtrl_DateWeek_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_DateWeek_Elm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Images = new System.Windows.Forms.Panel();
            this.checkBox_Images = new System.Windows.Forms.CheckBox();
            this.panel_Pointer = new System.Windows.Forms.Panel();
            this.checkBox_Pointer = new System.Windows.Forms.CheckBox();
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.button_Images = new System.Windows.Forms.Button();
            this.button_Pointer = new System.Windows.Forms.Button();
            this.button_ElementName = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_Images.SuspendLayout();
            this.panel_Pointer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel_Images, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_Pointer, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 51);
            this.tableLayoutPanel1.TabIndex = 24;
            this.tableLayoutPanel1.Visible = false;
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // panel_Images
            // 
            this.panel_Images.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Images.Controls.Add(this.checkBox_Images);
            this.panel_Images.Controls.Add(this.button_Images);
            this.panel_Images.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Images.Location = new System.Drawing.Point(1, 26);
            this.panel_Images.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Images.Name = "panel_Images";
            this.panel_Images.Size = new System.Drawing.Size(218, 24);
            this.panel_Images.TabIndex = 0;
            this.panel_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.panel_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Images
            // 
            this.checkBox_Images.AutoSize = true;
            this.checkBox_Images.Location = new System.Drawing.Point(20, 5);
            this.checkBox_Images.Name = "checkBox_Images";
            this.checkBox_Images.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Images.TabIndex = 0;
            this.checkBox_Images.UseVisualStyleBackColor = true;
            this.checkBox_Images.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // panel_Pointer
            // 
            this.panel_Pointer.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Pointer.Controls.Add(this.button_Pointer);
            this.panel_Pointer.Controls.Add(this.checkBox_Pointer);
            this.panel_Pointer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Pointer.Location = new System.Drawing.Point(1, 1);
            this.panel_Pointer.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Pointer.Name = "panel_Pointer";
            this.panel_Pointer.Size = new System.Drawing.Size(218, 24);
            this.panel_Pointer.TabIndex = 2;
            this.panel_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.panel_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_Pointer
            // 
            this.checkBox_Pointer.AutoSize = true;
            this.checkBox_Pointer.Location = new System.Drawing.Point(20, 5);
            this.checkBox_Pointer.Name = "checkBox_Pointer";
            this.checkBox_Pointer.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Pointer.TabIndex = 0;
            this.checkBox_Pointer.UseVisualStyleBackColor = true;
            this.checkBox_Pointer.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // pictureBox_Arrow_Down
            // 
            this.pictureBox_Arrow_Down.BackgroundImage = global::ControlLibrary.Properties.Resources.arrow_down;
            this.pictureBox_Arrow_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Arrow_Down.Location = new System.Drawing.Point(131, 2);
            this.pictureBox_Arrow_Down.Name = "pictureBox_Arrow_Down";
            this.pictureBox_Arrow_Down.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Arrow_Down.TabIndex = 23;
            this.pictureBox_Arrow_Down.TabStop = false;
            this.pictureBox_Arrow_Down.Visible = false;
            this.pictureBox_Arrow_Down.Click += new System.EventHandler(this.button_ElementName_Click);
            // 
            // pictureBox_NotShow
            // 
            this.pictureBox_NotShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_NotShow.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_off_black_24;
            this.pictureBox_NotShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_NotShow.Location = new System.Drawing.Point(101, 2);
            this.pictureBox_NotShow.Name = "pictureBox_NotShow";
            this.pictureBox_NotShow.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_NotShow.TabIndex = 28;
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
            this.pictureBox_Arrow_Right.TabIndex = 27;
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
            this.pictureBox_Show.TabIndex = 26;
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
            this.pictureBox_Del.TabIndex = 25;
            this.pictureBox_Del.TabStop = false;
            this.pictureBox_Del.Click += new System.EventHandler(this.pictureBox_Del_Click);
            // 
            // button_Images
            // 
            this.button_Images.FlatAppearance.BorderSize = 0;
            this.button_Images.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Images.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Images.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Images.Image = global::ControlLibrary.Properties.Resources.images_18;
            this.button_Images.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Images.Location = new System.Drawing.Point(33, 0);
            this.button_Images.Name = "button_Images";
            this.button_Images.Size = new System.Drawing.Size(145, 24);
            this.button_Images.TabIndex = 2;
            this.button_Images.Text = "Изображение";
            this.button_Images.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Images.UseVisualStyleBackColor = true;
            this.button_Images.Click += new System.EventHandler(this.panel_Images_Click);
            this.button_Images.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Images.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Images.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_Pointer
            // 
            this.button_Pointer.FlatAppearance.BorderSize = 0;
            this.button_Pointer.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_Pointer.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.button_Pointer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Pointer.Image = ((System.Drawing.Image)(resources.GetObject("button_Pointer.Image")));
            this.button_Pointer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Pointer.Location = new System.Drawing.Point(33, 0);
            this.button_Pointer.Name = "button_Pointer";
            this.button_Pointer.Size = new System.Drawing.Size(160, 24);
            this.button_Pointer.TabIndex = 4;
            this.button_Pointer.Text = "Стрелочный указатель";
            this.button_Pointer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Pointer.UseVisualStyleBackColor = true;
            this.button_Pointer.Click += new System.EventHandler(this.panel_Pointer_Click);
            this.button_Pointer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_Pointer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_Pointer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_ElementName
            // 
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            this.button_ElementName.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ElementName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.week_18;
            this.button_ElementName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ElementName.Location = new System.Drawing.Point(0, 0);
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button_ElementName.Size = new System.Drawing.Size(220, 28);
            this.button_ElementName.TabIndex = 22;
            this.button_ElementName.Text = "День недели";
            this.button_ElementName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // UCtrl_DateWeek_Elm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_Arrow_Down);
            this.Controls.Add(this.pictureBox_NotShow);
            this.Controls.Add(this.pictureBox_Arrow_Right);
            this.Controls.Add(this.pictureBox_Show);
            this.Controls.Add(this.pictureBox_Del);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button_ElementName);
            this.Name = "UCtrl_DateWeek_Elm";
            this.Size = new System.Drawing.Size(220, 200);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_Images.ResumeLayout(false);
            this.panel_Images.PerformLayout();
            this.panel_Pointer.ResumeLayout(false);
            this.panel_Pointer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Down)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Arrow_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
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
        private System.Windows.Forms.Panel panel_Pointer;
        private System.Windows.Forms.Button button_Pointer;
        public System.Windows.Forms.CheckBox checkBox_Pointer;
        private System.Windows.Forms.Button button_ElementName;
    }
}
