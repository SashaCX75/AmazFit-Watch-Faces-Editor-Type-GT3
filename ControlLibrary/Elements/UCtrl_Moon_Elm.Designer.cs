
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_Moon_Elm));
            this.toolTip_Moon = new System.Windows.Forms.ToolTip();
            this.SuspendLayout();
            // 
            // button_ElementName
            // 
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.moon;
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.toolTip_Moon.SetToolTip(this.button_ElementName, resources.GetString("button_ElementName.ToolTip"));
            // 
            // toolTip_Moon
            // 
            this.toolTip_Moon.Active = false;
            this.toolTip_Moon.AutoPopDelay = 5000;
            this.toolTip_Moon.InitialDelay = 500;
            this.toolTip_Moon.ReshowDelay = 100;
            this.toolTip_Moon.ToolTipTitle = "Moon Phases";
            // 
            // UCtrl_Moon_Elm
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "UCtrl_Moon_Elm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip_Moon;
    }
}
