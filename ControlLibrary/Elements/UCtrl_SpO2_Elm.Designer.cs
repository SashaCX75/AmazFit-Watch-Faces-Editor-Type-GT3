﻿
namespace ControlLibrary
{
    partial class UCtrl_SpO2_Elm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCtrl_SpO2_Elm));
            this.SuspendLayout();
            // 
            // button_ElementName
            // 
            resources.ApplyResources(this.button_ElementName, "button_ElementName");
            // 
            // UCtrl_SpO2_Elm
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "UCtrl_SpO2_Elm";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
