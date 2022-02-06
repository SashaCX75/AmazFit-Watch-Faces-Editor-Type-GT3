
namespace ControlLibrary
{
    partial class UCtrl_FatBurning_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_FatBurning_Elm));
            this.SuspendLayout();
            // 
            // checkBox_Number
            // 
            resources.ApplyResources(this.checkBox_Number, "checkBox_Number");
            // 
            // checkBox_Images
            // 
            resources.ApplyResources(this.checkBox_Images, "checkBox_Images");
            // 
            // checkBox_Pointer
            // 
            resources.ApplyResources(this.checkBox_Pointer, "checkBox_Pointer");
            // 
            // checkBox_Segments
            // 
            resources.ApplyResources(this.checkBox_Segments, "checkBox_Segments");
            // 
            // checkBox_Linear_Scale
            // 
            resources.ApplyResources(this.checkBox_Linear_Scale, "checkBox_Linear_Scale");
            // 
            // checkBox_Number_Target
            // 
            resources.ApplyResources(this.checkBox_Number_Target, "checkBox_Number_Target");
            // 
            // checkBox_Circle_Scale
            // 
            resources.ApplyResources(this.checkBox_Circle_Scale, "checkBox_Circle_Scale");
            // 
            // checkBox_Icon
            // 
            resources.ApplyResources(this.checkBox_Icon, "checkBox_Icon");
            // 
            // button_ElementName
            // 
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.fat_burning;
            // 
            // UCtrl_FatBurning_Elm
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "UCtrl_FatBurning_Elm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
