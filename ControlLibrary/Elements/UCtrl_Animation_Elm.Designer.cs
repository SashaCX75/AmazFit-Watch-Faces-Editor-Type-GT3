
namespace ControlLibrary
{
    partial class UCtrl_Animation_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Animation_Elm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_ShowAnimation = new System.Windows.Forms.Button();
            this.panel_FrameAnimation = new System.Windows.Forms.Panel();
            this.checkBox_FrameAnimation = new System.Windows.Forms.CheckBox();
            this.button_FrameAnimation = new System.Windows.Forms.Button();
            this.panel_MotionAnimation = new System.Windows.Forms.Panel();
            this.checkBox_MotionAnimation = new System.Windows.Forms.CheckBox();
            this.button_MotionAnimation = new System.Windows.Forms.Button();
            this.panel_RotateAnimation = new System.Windows.Forms.Panel();
            this.button_RotateAnimation = new System.Windows.Forms.Button();
            this.checkBox_RotateAnimation = new System.Windows.Forms.CheckBox();
            this.pictureBox_Arrow_Down = new System.Windows.Forms.PictureBox();
            this.pictureBox_NotShow = new System.Windows.Forms.PictureBox();
            this.pictureBox_Arrow_Right = new System.Windows.Forms.PictureBox();
            this.pictureBox_Show = new System.Windows.Forms.PictureBox();
            this.pictureBox_Del = new System.Windows.Forms.PictureBox();
            this.button_ElementName = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_FrameAnimation.SuspendLayout();
            this.panel_MotionAnimation.SuspendLayout();
            this.panel_RotateAnimation.SuspendLayout();
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
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.button_ShowAnimation, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel_FrameAnimation, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_MotionAnimation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_RotateAnimation, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragDrop);
            this.tableLayoutPanel1.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel1_DragOver);
            // 
            // button_ShowAnimation
            // 
            resources.ApplyResources(this.button_ShowAnimation, "button_ShowAnimation");
            this.button_ShowAnimation.Name = "button_ShowAnimation";
            this.button_ShowAnimation.UseVisualStyleBackColor = true;
            this.button_ShowAnimation.Click += new System.EventHandler(this.button_ShowAnimation_Click);
            // 
            // panel_FrameAnimation
            // 
            this.panel_FrameAnimation.BackColor = System.Drawing.SystemColors.Control;
            this.panel_FrameAnimation.Controls.Add(this.checkBox_FrameAnimation);
            this.panel_FrameAnimation.Controls.Add(this.button_FrameAnimation);
            resources.ApplyResources(this.panel_FrameAnimation, "panel_FrameAnimation");
            this.panel_FrameAnimation.Name = "panel_FrameAnimation";
            this.panel_FrameAnimation.Click += new System.EventHandler(this.panel_FrameAnimation_Click);
            this.panel_FrameAnimation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_FrameAnimation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_FrameAnimation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_FrameAnimation
            // 
            resources.ApplyResources(this.checkBox_FrameAnimation, "checkBox_FrameAnimation");
            this.checkBox_FrameAnimation.Name = "checkBox_FrameAnimation";
            this.checkBox_FrameAnimation.UseVisualStyleBackColor = true;
            this.checkBox_FrameAnimation.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_FrameAnimation
            // 
            this.button_FrameAnimation.FlatAppearance.BorderSize = 0;
            this.button_FrameAnimation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_FrameAnimation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_FrameAnimation, "button_FrameAnimation");
            this.button_FrameAnimation.Image = global::ControlLibrary.Properties.Resources.frame_anim;
            this.button_FrameAnimation.Name = "button_FrameAnimation";
            this.button_FrameAnimation.UseVisualStyleBackColor = true;
            this.button_FrameAnimation.Click += new System.EventHandler(this.panel_FrameAnimation_Click);
            this.button_FrameAnimation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_FrameAnimation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_FrameAnimation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_MotionAnimation
            // 
            this.panel_MotionAnimation.BackColor = System.Drawing.SystemColors.Control;
            this.panel_MotionAnimation.Controls.Add(this.checkBox_MotionAnimation);
            this.panel_MotionAnimation.Controls.Add(this.button_MotionAnimation);
            resources.ApplyResources(this.panel_MotionAnimation, "panel_MotionAnimation");
            this.panel_MotionAnimation.Name = "panel_MotionAnimation";
            this.panel_MotionAnimation.Click += new System.EventHandler(this.panel_MotionAnimation_Click);
            this.panel_MotionAnimation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_MotionAnimation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_MotionAnimation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_MotionAnimation
            // 
            resources.ApplyResources(this.checkBox_MotionAnimation, "checkBox_MotionAnimation");
            this.checkBox_MotionAnimation.Name = "checkBox_MotionAnimation";
            this.checkBox_MotionAnimation.UseVisualStyleBackColor = true;
            this.checkBox_MotionAnimation.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
            // 
            // button_MotionAnimation
            // 
            this.button_MotionAnimation.FlatAppearance.BorderSize = 0;
            this.button_MotionAnimation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_MotionAnimation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_MotionAnimation, "button_MotionAnimation");
            this.button_MotionAnimation.Image = global::ControlLibrary.Properties.Resources.motion_anim;
            this.button_MotionAnimation.Name = "button_MotionAnimation";
            this.button_MotionAnimation.UseVisualStyleBackColor = true;
            this.button_MotionAnimation.Click += new System.EventHandler(this.panel_MotionAnimation_Click);
            this.button_MotionAnimation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_MotionAnimation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_MotionAnimation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // panel_RotateAnimation
            // 
            this.panel_RotateAnimation.BackColor = System.Drawing.SystemColors.Control;
            this.panel_RotateAnimation.Controls.Add(this.button_RotateAnimation);
            this.panel_RotateAnimation.Controls.Add(this.checkBox_RotateAnimation);
            resources.ApplyResources(this.panel_RotateAnimation, "panel_RotateAnimation");
            this.panel_RotateAnimation.Name = "panel_RotateAnimation";
            this.panel_RotateAnimation.Click += new System.EventHandler(this.panel_RotateAnimation_Click);
            this.panel_RotateAnimation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.panel_RotateAnimation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.panel_RotateAnimation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // button_RotateAnimation
            // 
            this.button_RotateAnimation.FlatAppearance.BorderSize = 0;
            this.button_RotateAnimation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.button_RotateAnimation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_RotateAnimation, "button_RotateAnimation");
            this.button_RotateAnimation.Image = global::ControlLibrary.Properties.Resources.rotate_anim;
            this.button_RotateAnimation.Name = "button_RotateAnimation";
            this.button_RotateAnimation.UseVisualStyleBackColor = true;
            this.button_RotateAnimation.Click += new System.EventHandler(this.panel_RotateAnimation_Click);
            this.button_RotateAnimation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Control_MouseDown);
            this.button_RotateAnimation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Control_MouseMove);
            this.button_RotateAnimation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Control_MouseUp);
            // 
            // checkBox_RotateAnimation
            // 
            resources.ApplyResources(this.checkBox_RotateAnimation, "checkBox_RotateAnimation");
            this.checkBox_RotateAnimation.Name = "checkBox_RotateAnimation";
            this.checkBox_RotateAnimation.UseVisualStyleBackColor = true;
            this.checkBox_RotateAnimation.CheckedChanged += new System.EventHandler(this.checkBox_Elements_CheckedChanged);
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
            // button_ElementName
            // 
            this.button_ElementName.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.animation;
            this.button_ElementName.Name = "button_ElementName";
            this.button_ElementName.UseVisualStyleBackColor = false;
            this.button_ElementName.SizeChanged += new System.EventHandler(this.button_ElementName_SizeChanged);
            this.button_ElementName.Click += new System.EventHandler(this.button_ElementName_Click);
            this.button_ElementName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseDown);
            this.button_ElementName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseMove);
            this.button_ElementName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_ElementName_MouseUp);
            // 
            // UCtrl_Animation_Elm
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
            this.Name = "UCtrl_Animation_Elm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel_FrameAnimation.ResumeLayout(false);
            this.panel_FrameAnimation.PerformLayout();
            this.panel_MotionAnimation.ResumeLayout(false);
            this.panel_MotionAnimation.PerformLayout();
            this.panel_RotateAnimation.ResumeLayout(false);
            this.panel_RotateAnimation.PerformLayout();
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
        private System.Windows.Forms.Panel panel_FrameAnimation;
        public System.Windows.Forms.CheckBox checkBox_FrameAnimation;
        private System.Windows.Forms.Button button_FrameAnimation;
        private System.Windows.Forms.Panel panel_MotionAnimation;
        public System.Windows.Forms.CheckBox checkBox_MotionAnimation;
        private System.Windows.Forms.Button button_MotionAnimation;
        private System.Windows.Forms.Panel panel_RotateAnimation;
        private System.Windows.Forms.Button button_RotateAnimation;
        public System.Windows.Forms.CheckBox checkBox_RotateAnimation;
        protected System.Windows.Forms.Button button_ElementName;
        private System.Windows.Forms.Button button_ShowAnimation;
    }
}
