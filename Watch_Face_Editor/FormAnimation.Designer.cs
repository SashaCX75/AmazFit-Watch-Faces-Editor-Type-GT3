namespace Watch_Face_Editor
{
    partial class FormAnimation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnimation));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_AnimationReset = new System.Windows.Forms.Button();
            this.numericUpDown_NumberOfFrames = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button_SaveAnimation = new System.Windows.Forms.Button();
            this.radioButton_xlarge = new System.Windows.Forms.RadioButton();
            this.radioButton_large = new System.Windows.Forms.RadioButton();
            this.radioButton_normal = new System.Windows.Forms.RadioButton();
            this.pictureBox_AnimatiomPreview = new System.Windows.Forms.PictureBox();
            this.progressBar_SaveAnimation = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumberOfFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AnimatiomPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.button_AnimationReset);
            this.panel1.Controls.Add(this.numericUpDown_NumberOfFrames);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_SaveAnimation);
            this.panel1.Controls.Add(this.radioButton_xlarge);
            this.panel1.Controls.Add(this.radioButton_large);
            this.panel1.Controls.Add(this.radioButton_normal);
            this.panel1.Name = "panel1";
            // 
            // button_AnimationReset
            // 
            resources.ApplyResources(this.button_AnimationReset, "button_AnimationReset");
            this.button_AnimationReset.Name = "button_AnimationReset";
            this.button_AnimationReset.UseVisualStyleBackColor = true;
            this.button_AnimationReset.Click += new System.EventHandler(this.button_AnimationReset_Click);
            // 
            // numericUpDown_NumberOfFrames
            // 
            resources.ApplyResources(this.numericUpDown_NumberOfFrames, "numericUpDown_NumberOfFrames");
            this.numericUpDown_NumberOfFrames.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_NumberOfFrames.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_NumberOfFrames.Name = "numericUpDown_NumberOfFrames";
            this.numericUpDown_NumberOfFrames.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button_SaveAnimation
            // 
            resources.ApplyResources(this.button_SaveAnimation, "button_SaveAnimation");
            this.button_SaveAnimation.Name = "button_SaveAnimation";
            this.button_SaveAnimation.UseVisualStyleBackColor = true;
            this.button_SaveAnimation.Click += new System.EventHandler(this.button_SaveAnimation_Click);
            // 
            // radioButton_xlarge
            // 
            resources.ApplyResources(this.radioButton_xlarge, "radioButton_xlarge");
            this.radioButton_xlarge.Name = "radioButton_xlarge";
            this.radioButton_xlarge.UseVisualStyleBackColor = true;
            this.radioButton_xlarge.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_large
            // 
            resources.ApplyResources(this.radioButton_large, "radioButton_large");
            this.radioButton_large.Name = "radioButton_large";
            this.radioButton_large.UseVisualStyleBackColor = true;
            this.radioButton_large.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_normal
            // 
            resources.ApplyResources(this.radioButton_normal, "radioButton_normal");
            this.radioButton_normal.Checked = true;
            this.radioButton_normal.Name = "radioButton_normal";
            this.radioButton_normal.TabStop = true;
            this.radioButton_normal.UseVisualStyleBackColor = true;
            this.radioButton_normal.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // pictureBox_AnimatiomPreview
            // 
            resources.ApplyResources(this.pictureBox_AnimatiomPreview, "pictureBox_AnimatiomPreview");
            this.pictureBox_AnimatiomPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_AnimatiomPreview.Name = "pictureBox_AnimatiomPreview";
            this.pictureBox_AnimatiomPreview.TabStop = false;
            // 
            // progressBar_SaveAnimation
            // 
            resources.ApplyResources(this.progressBar_SaveAnimation, "progressBar_SaveAnimation");
            this.progressBar_SaveAnimation.Name = "progressBar_SaveAnimation";
            this.progressBar_SaveAnimation.Step = 1;
            this.progressBar_SaveAnimation.Value = 50;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormAnimation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar_SaveAnimation);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox_AnimatiomPreview);
            this.MaximizeBox = false;
            this.Name = "FormAnimation";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAnimation_FormClosed);
            this.Shown += new System.EventHandler(this.FormAnimation_Shown);
            this.SizeChanged += new System.EventHandler(this.FormAnimation_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumberOfFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AnimatiomPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_AnimationReset;
        private System.Windows.Forms.NumericUpDown numericUpDown_NumberOfFrames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_SaveAnimation;
        private System.Windows.Forms.RadioButton radioButton_xlarge;
        private System.Windows.Forms.RadioButton radioButton_large;
        private System.Windows.Forms.RadioButton radioButton_normal;
        private System.Windows.Forms.PictureBox pictureBox_AnimatiomPreview;
        private System.Windows.Forms.ProgressBar progressBar_SaveAnimation;
        public System.Windows.Forms.Timer timer1;
    }
}