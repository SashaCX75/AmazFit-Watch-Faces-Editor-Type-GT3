namespace ControlLibrary
{
    partial class UCtrl_DateYear_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_DateYear_Elm));
            this.SuspendLayout();
            // 
            // checkBox_Number
            // 
            resources.ApplyResources(this.checkBox_Number, "checkBox_Number");
            // 
            // checkBox_Text_rotation
            // 
            resources.ApplyResources(this.checkBox_Text_rotation, "checkBox_Text_rotation");
            // 
            // checkBox_Text_circle
            // 
            resources.ApplyResources(this.checkBox_Text_circle, "checkBox_Text_circle");
            // 
            // checkBox_Icon
            // 
            resources.ApplyResources(this.checkBox_Icon, "checkBox_Icon");
            // 
            // button_ElementName
            // 
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            this.button_ElementName.Image = global::ControlLibrary.Properties.Resources.year_18;
            // 
            // UCtrl_DateYear_Elm
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "UCtrl_DateYear_Elm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
