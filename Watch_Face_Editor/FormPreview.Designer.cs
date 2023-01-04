
namespace Watch_Face_Editor
{
    partial class Form_Preview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Preview));
            this.pictureBox_Preview = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_xxlarge = new System.Windows.Forms.RadioButton();
            this.radioButton_xlarge = new System.Windows.Forms.RadioButton();
            this.radioButton_large = new System.Windows.Forms.RadioButton();
            this.radioButton_normal = new System.Windows.Forms.RadioButton();
            this.radioButton_small = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Preview
            // 
            this.pictureBox_Preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_Preview.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox_Preview.Location = new System.Drawing.Point(3, 3);
            this.pictureBox_Preview.Name = "pictureBox_Preview";
            this.pictureBox_Preview.Size = new System.Drawing.Size(456, 456);
            this.pictureBox_Preview.TabIndex = 4;
            this.pictureBox_Preview.TabStop = false;
            this.pictureBox_Preview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Preview_MouseDoubleClick);
            this.pictureBox_Preview.MouseLeave += new System.EventHandler(this.pictureBox_Preview_MouseLeave);
            this.pictureBox_Preview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Preview_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_xxlarge);
            this.panel1.Controls.Add(this.radioButton_xlarge);
            this.panel1.Controls.Add(this.radioButton_large);
            this.panel1.Controls.Add(this.radioButton_normal);
            this.panel1.Controls.Add(this.radioButton_small);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 24);
            this.panel1.TabIndex = 3;
            // 
            // radioButton_xxlarge
            // 
            this.radioButton_xxlarge.AutoSize = true;
            this.radioButton_xxlarge.Location = new System.Drawing.Point(228, 3);
            this.radioButton_xxlarge.Name = "radioButton_xxlarge";
            this.radioButton_xxlarge.Size = new System.Drawing.Size(45, 17);
            this.radioButton_xxlarge.TabIndex = 4;
            this.radioButton_xxlarge.Text = "x2,5";
            this.radioButton_xxlarge.UseVisualStyleBackColor = true;
            this.radioButton_xxlarge.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_xlarge
            // 
            this.radioButton_xlarge.AutoSize = true;
            this.radioButton_xlarge.Location = new System.Drawing.Point(177, 3);
            this.radioButton_xlarge.Name = "radioButton_xlarge";
            this.radioButton_xlarge.Size = new System.Drawing.Size(36, 17);
            this.radioButton_xlarge.TabIndex = 3;
            this.radioButton_xlarge.Text = "x2";
            this.radioButton_xlarge.UseVisualStyleBackColor = true;
            this.radioButton_xlarge.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_large
            // 
            this.radioButton_large.AutoSize = true;
            this.radioButton_large.Location = new System.Drawing.Point(116, 3);
            this.radioButton_large.Name = "radioButton_large";
            this.radioButton_large.Size = new System.Drawing.Size(45, 17);
            this.radioButton_large.TabIndex = 2;
            this.radioButton_large.Text = "x1,5";
            this.radioButton_large.UseVisualStyleBackColor = true;
            this.radioButton_large.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_normal
            // 
            this.radioButton_normal.AutoSize = true;
            this.radioButton_normal.Checked = true;
            this.radioButton_normal.Location = new System.Drawing.Point(64, 3);
            this.radioButton_normal.Name = "radioButton_normal";
            this.radioButton_normal.Size = new System.Drawing.Size(36, 17);
            this.radioButton_normal.TabIndex = 1;
            this.radioButton_normal.TabStop = true;
            this.radioButton_normal.Text = "x1";
            this.radioButton_normal.UseVisualStyleBackColor = true;
            this.radioButton_normal.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_small
            // 
            this.radioButton_small.AutoSize = true;
            this.radioButton_small.Location = new System.Drawing.Point(3, 3);
            this.radioButton_small.Name = "radioButton_small";
            this.radioButton_small.Size = new System.Drawing.Size(45, 17);
            this.radioButton_small.TabIndex = 0;
            this.radioButton_small.Text = "x0,5";
            this.radioButton_small.UseVisualStyleBackColor = true;
            this.radioButton_small.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 3000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            // 
            // Form_Preview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(462, 485);
            this.Controls.Add(this.pictureBox_Preview);
            this.Controls.Add(this.panel1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Watch_Face_Editor.Properties.Settings.Default, "PreviewLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::Watch_Face_Editor.Properties.Settings.Default.PreviewLocation;
            this.MaximizeBox = false;
            this.Name = "Form_Preview";
            this.ShowInTaskbar = false;
            this.Text = "Предпросмотр";
            this.Load += new System.EventHandler(this.Form_Preview_Load);
            this.SizeChanged += new System.EventHandler(this.Form_Preview_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox_Preview;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton radioButton_xxlarge;
        public System.Windows.Forms.RadioButton radioButton_xlarge;
        public System.Windows.Forms.RadioButton radioButton_large;
        public System.Windows.Forms.RadioButton radioButton_normal;
        public System.Windows.Forms.RadioButton radioButton_small;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}