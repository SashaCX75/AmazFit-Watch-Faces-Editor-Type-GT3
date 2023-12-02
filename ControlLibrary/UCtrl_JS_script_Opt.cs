using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_JS_script_Opt : UserControl
    {
        string ProjectDir = "";

        private bool setValue; // режим задания параметров
        public Object _ElementScript;

        private bool AOD_mode = true;

        public UCtrl_JS_script_Opt()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        /// <summary>Режим отображения для АОД</summary>
        [Description("Режим отображения для АОД")]
        public virtual bool AOD
        {
            get
            {
                return AOD_mode;
            }
            set
            {
                AOD_mode = value;

                checkBox_user_functions.Visible = !AOD_mode;
                checkBox_user_script_start.Visible = !AOD_mode;
                //checkBox_user_script.Visible = !AOD_mode;
                checkBox_user_script_beforeShortcuts.Visible = !AOD_mode;
                checkBox_user_script_end.Visible = !AOD_mode;
                checkBox_resume_call.Visible = !AOD_mode;
                checkBox_pause_call.Visible = !AOD_mode;

                splitContainer_user_functions.Visible = !AOD_mode;
                splitContainer_user_script_start.Visible = !AOD_mode;
                //splitContainer_user_script.Visible = !AOD_mode;
                splitContainer_user_script_beforeShortcuts.Visible = !AOD_mode;
                splitContainer_user_script_end.Visible = !AOD_mode;
                splitContainer_resume_call.Visible = !AOD_mode;
                splitContainer_pause_call.Visible = !AOD_mode;

                CheckScriptFile();
            }
        }


        #region Settings Clear
        private void CheckScriptFile()
        {
            string jsDir = Path.Combine(ProjectDir, "JS");
            //string fileName = Path.Combine(jsDir, "user_functions.js");
            //string fileNameOld = Path.Combine(ProjectDir, "user_functions.js");

            //if (Directory.Exists(jsDir))
            //{
            string fileName = "user_functions.js";
            if (AOD) fileName = "AOD_" + fileName;
            fileName = Path.Combine(jsDir, fileName);
            if (File.Exists(fileName))
            {
                checkBox_user_functions.Enabled = true;
                button_user_functions_Add.Enabled = false;
                button_user_functions_Del.Enabled = true;
            }
            else
            {
                checkBox_user_functions.Enabled = false;
                button_user_functions_Add.Enabled = true;
                button_user_functions_Del.Enabled = false;
            }

            fileName = "user_script_start.js";
            if (AOD) fileName = "AOD_" + fileName;
            fileName = Path.Combine(jsDir, fileName);
            if (File.Exists(fileName))
            {
                checkBox_user_script_start.Enabled = true;
                button_user_script_start_Add.Enabled = false;
                button_user_script_start_Del.Enabled = true;
            }
            else
            {
                checkBox_user_script_start.Enabled = false;
                button_user_script_start_Add.Enabled = true;
                button_user_script_start_Del.Enabled = false;
            }

            fileName = "user_script.js";
            if (AOD) fileName = "AOD_" + fileName;
            fileName = Path.Combine(jsDir, fileName);
            if (File.Exists(fileName))
            {
                checkBox_user_script.Enabled = true;
                button_user_script_Add.Enabled = false;
                button_user_script_Del.Enabled = true;
            }
            else
            {
                checkBox_user_script.Enabled = false;
                button_user_script_Add.Enabled = true;
                button_user_script_Del.Enabled = false;
            }

            fileName = "user_script_beforeShortcuts.js";
            if (AOD) fileName = "AOD_" + fileName;
            fileName = Path.Combine(jsDir, fileName);
            if (File.Exists(fileName))
            {
                checkBox_user_script_beforeShortcuts.Enabled = true;
                button_user_script_beforeShortcuts_Add.Enabled = false;
                button_user_script_beforeShortcuts_Del.Enabled = true;
            }
            else
            {
                checkBox_user_script_beforeShortcuts.Enabled = false;
                button_user_script_beforeShortcuts_Add.Enabled = true;
                button_user_script_beforeShortcuts_Del.Enabled = false;
            }

            fileName = "user_script_end.js";
            if (AOD) fileName = "AOD_" + fileName;
            fileName = Path.Combine(jsDir, fileName);
            if (File.Exists(fileName))
            {
                checkBox_user_script_end.Enabled = true;
                button_user_script_end_Add.Enabled = false;
                button_user_script_end_Del.Enabled = true;
            }
            else
            {
                checkBox_user_script_end.Enabled = false;
                button_user_script_end_Add.Enabled = true;
                button_user_script_end_Del.Enabled = false;
            }

            fileName = "resume_call.js";
            if (AOD) fileName = "AOD_" + fileName;
            fileName = Path.Combine(jsDir, fileName);
            if (File.Exists(fileName))
            {
                checkBox_resume_call.Enabled = true;
                button_resume_call_Add.Enabled = false;
                button_resume_call_Del.Enabled = true;
            }
            else
            {
                checkBox_resume_call.Enabled = false;
                button_resume_call_Add.Enabled = true;
                button_resume_call_Del.Enabled = false;
            }

            fileName = "pause_call.js";
            if (AOD) fileName = "AOD_" + fileName;
            fileName = Path.Combine(jsDir, fileName);
            if (File.Exists(fileName))
            {
                checkBox_pause_call.Enabled = true;
                button_pause_call_Add.Enabled = false;
                button_pause_call_Del.Enabled = true;
            }
            else
            {
                checkBox_pause_call.Enabled = false;
                button_pause_call_Add.Enabled = true;
                button_pause_call_Del.Enabled = false;
            }
            //}
        }

        /// <summary>сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear(string projectDir)
        {
            setValue = true;

            checkBox_user_functions.Checked = false;
            checkBox_user_script_start.Checked = false;
            checkBox_user_script.Checked = false;
            checkBox_user_script_beforeShortcuts.Checked = false;
            checkBox_user_script_end.Checked = false;
            checkBox_resume_call.Checked = false;
            checkBox_pause_call.Checked = false;

            //AOD = true;

            ProjectDir = projectDir;
            CheckScriptFile();

            setValue = false;
        }
        #endregion

        private void button_Add_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            //dialog.FileName = "New_Project"; 
            dialog.Title = Properties.Strings.Dialog_Title_JS_Open;
            dialog.Filter = Properties.Strings.FilterJS;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            dialog.RestoreDirectory = true;
            //dialog.OverwritePrompt = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Button button = (Button)sender;
                string destFileName = "";
                switch (button.Name)
                {
                    case "button_user_functions_Add":
                        destFileName = "user_functions.js";
                        break;
                    case "button_user_script_start_Add":
                        destFileName = "user_script_start.js";
                        break;
                    case "button_user_script_Add":
                        destFileName = "user_script.js";
                        break;
                    case "button_user_script_beforeShortcuts_Add":
                        destFileName = "user_script_beforeShortcuts.js";
                        break;
                    case "button_user_script_end_Add":
                        destFileName = "user_script_end.js";
                        break;
                    case "button_resume_call_Add":
                        destFileName = "resume_call.js";
                        break;
                    case "button_pause_call_Add":
                        destFileName = "pause_call.js";
                        break;
                }
                if (destFileName.Length > 3)
                {
                    if (AOD) destFileName = "AOD_" + destFileName;
                    string jsDir = Path.Combine(ProjectDir, "JS");
                    try
                    {
                        if (!Directory.Exists(jsDir)) Directory.CreateDirectory(jsDir);
                        File.Copy(dialog.FileName, Path.Combine(jsDir, destFileName), true);
                        switch (button.Name)
                        {
                            case "button_user_functions_Add":
                                checkBox_user_functions.Checked = true;
                                break;
                            case "button_user_script_start_Add":
                                checkBox_user_script_start.Checked = true;
                                break;
                            case "button_user_script_Add":
                                checkBox_user_script.Checked = true;
                                break;
                            case "button_user_script_beforeShortcuts_Add":
                                checkBox_user_script_beforeShortcuts.Checked = true;
                                break;
                            case "button_user_script_end_Add":
                                checkBox_user_script_end.Checked = true;
                                break;
                            case "button_resume_call_Add":
                                checkBox_resume_call.Checked = true;
                                break;
                            case "button_pause_call_Add":
                                checkBox_pause_call.Checked= true;
                                break;
                            }
                        }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Properties.Strings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    CheckScriptFile();
                }
            }
        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Properties.Strings.Message_JS_Del, Properties.Strings.Message_Del_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Button button = (Button)sender;
                string destFileName = "";
                switch (button.Name)
                {
                    case "button_user_functions_Del":
                        destFileName = "user_functions.js";
                        break;
                    case "button_user_script_start_Del":
                        destFileName = "user_script_start.js";
                        break;
                    case "button_user_script_Del":
                        destFileName = "user_script.js";
                        break;
                    case "button_user_script_beforeShortcuts_Del":
                        destFileName = "user_script_beforeShortcuts.js";
                        break;
                    case "button_user_script_end_Del":
                        destFileName = "user_script_end.js";
                        break;
                    case "button_resume_call_Del":
                        destFileName = "resume_call.js";
                        break;
                    case "button_pause_call_Del":
                        destFileName = "pause_call.js";
                        break;
                }
                if (destFileName.Length > 3)
                {
                    if (AOD) destFileName = "AOD_" + destFileName;
                    string jsDir = Path.Combine(ProjectDir, "JS");
                    try
                    {
                        string fileName = Path.Combine(jsDir, destFileName);
                        if(File.Exists(fileName)) File.Delete(fileName);
                        switch (button.Name)
                        {
                            case "button_user_functions_Del":
                                checkBox_user_functions.Checked = false;
                                break;
                            case "button_user_script_start_Del":
                                checkBox_user_script_start.Checked = false;
                                break;
                            case "button_user_script_Del":
                                checkBox_user_script.Checked = false;
                                break;
                            case "button_user_script_beforeShortcuts_Del":
                                checkBox_user_script_beforeShortcuts.Checked = false;
                                break;
                            case "button_user_script_end_Del":
                                checkBox_user_script_end.Checked = false;
                                break;
                            case "button_resume_call_Del":
                                checkBox_resume_call.Checked = false;
                                break;
                            case "button_pause_call_Del":
                                checkBox_pause_call.Checked = false;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Properties.Strings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    CheckScriptFile();
                }
            }
        }

        private void contextMenuStrip_Edit_JS_Opening(object sender, CancelEventArgs e)
        {
            ContextMenuStrip cms = sender as ContextMenuStrip;
            if (cms != null)
            {
                // Get the control that is displaying this context menu
                Control sourceControl = cms.SourceControl;
                if (sourceControl.GetType() == typeof(CheckBox))
                {
                    CheckBox checkBox = sourceControl as CheckBox;
                    string fileName = "";
                    switch (checkBox.Name)
                    {
                        case "checkBox_user_functions":
                            fileName = "user_functions.js";
                            break;
                        case "checkBox_user_script_start":
                            fileName = "user_script_start.js";
                            break;
                        case "checkBox_user_script":
                            fileName = "user_script.js";
                            break;
                        case "checkBox_user_script_beforeShortcuts":
                            fileName = "user_script_beforeShortcuts.js";
                            break;
                        case "checkBox_user_script_end":
                            fileName = "user_script_end.js";
                            break;
                        case "checkBox_resume_call":
                            fileName = "resume_call.js";
                            break;
                        case "checkBox_pause_call":
                            fileName = "pause_call.js";
                            break;
                    }
                    if (fileName.Length > 3)
                    {
                        if (AOD) fileName = "AOD_" + fileName;
                        string jsDir = Path.Combine(ProjectDir, "JS");
                        try
                        {
                            string fileFullName = Path.Combine(jsDir, fileName);
                            if (File.Exists(fileFullName)) 
                            {
                                //string commandText = fileFullName;
                                //var proc = new System.Diagnostics.Process();
                                //proc.StartInfo.FileName = commandText;
                                //proc.StartInfo.UseShellExecute = true;
                                //proc.Start();
                                Process.Start(fileFullName);

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Properties.Strings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        CheckScriptFile();
                    }
                }

                cms.Close();
            }
        }

        private void contextMenuStrip_Edit_JS_Opened(object sender, EventArgs e)
        {
            ContextMenuStrip cms = sender as ContextMenuStrip;
            if (cms != null) cms.Close();
        }

    }
}
