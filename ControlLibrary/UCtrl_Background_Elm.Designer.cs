
namespace ControlLibrary
{
    partial class UCtrl_Background_Elm
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
            this.button_ElementName = new System.Windows.Forms.Button();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).BeginInit();
            this.SuspendLayout();
            // 
            // button_ElementName
            // 
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            this.button_ElementName.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_ElementName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.Background_icon;
            this.button_ElementName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ElementName.Location = new System.Drawing.Point(0, 0);
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button_ElementName.Size = new System.Drawing.Size(220, 28);
            this.button_ElementName.TabIndex = 0;
            this.button_ElementName.Text = "Задний фон";
            this.button_ElementName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // pictureBox_NotShow
            // 
            this.pictureBox_NotShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_NotShow.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_off_black_24;
            this.pictureBox_NotShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_NotShow.Location = new System.Drawing.Point(134, 3);
            this.pictureBox_NotShow.Name = "pictureBox_NotShow";
            this.pictureBox_NotShow.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_NotShow.TabIndex = 10;
            this.pictureBox_NotShow.TabStop = false;
            this.pictureBox_NotShow.Visible = false;
            this.pictureBox_NotShow.Click += new System.EventHandler(this.pictureBox_Show_Click);
            // 
            // pictureBox_Show
            // 
            this.pictureBox_Show.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Show.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_visibility_black_24;
            this.pictureBox_Show.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Show.Location = new System.Drawing.Point(164, 3);
            this.pictureBox_Show.Name = "pictureBox_Show";
            this.pictureBox_Show.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Show.TabIndex = 9;
            this.pictureBox_Show.TabStop = false;
            this.pictureBox_Show.Click += new System.EventHandler(this.pictureBox_Show_Click);
            // 
            // pictureBox_Del
            // 
            this.pictureBox_Del.BackgroundImage = global::ControlLibrary.Properties.Resources.outline_delete_forever_black_24;
            this.pictureBox_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Del.Location = new System.Drawing.Point(193, 3);
            this.pictureBox_Del.Name = "pictureBox_Del";
            this.pictureBox_Del.Size = new System.Drawing.Size(24, 24);
            this.pictureBox_Del.TabIndex = 8;
            this.pictureBox_Del.TabStop = false;
            this.pictureBox_Del.Click += new System.EventHandler(this.pictureBox_Del_Click);
            // 
            // UCtrl_Background_Elm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_NotShow);
            this.Controls.Add(this.pictureBox_Show);
            this.Controls.Add(this.pictureBox_Del);
            this.Controls.Add(this.button_ElementName);
            this.Name = "UCtrl_Background_Elm";
            this.Size = new System.Drawing.Size(220, 65);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_NotShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Show)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Del)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_ElementName;
        private System.Windows.Forms.PictureBox pictureBox_NotShow;
        private System.Windows.Forms.PictureBox pictureBox_Show;
        private System.Windows.Forms.PictureBox pictureBox_Del;
    }
}
