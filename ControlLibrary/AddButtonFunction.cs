using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ControlLibrary
{
    public partial class AddButtonFunction : Form
    {
        bool updateFunction = false;
        string clickFunc = "";
        string longPressFunc = "";
        float apiLevel = 0;
        public bool UpdateFunction
        {
            get
            {
                return updateFunction;
            }
        }
        public string ClickFunc
        {
            get
            {
                return clickFunc;
            }
        }
        public string LongPressFunc
        {
            get
            {
                return longPressFunc;
            }
        }

        private Dictionary<string, string> UserScriptList = new Dictionary<string, string>();

        public AddButtonFunction(string click_func, string longpress_func, float api_level)
        {
            InitializeComponent();
            clickFunc = click_func;
            longPressFunc = longpress_func;
            apiLevel = api_level;
        }

        private void AddButtonFunction_Load(object sender, EventArgs e)
        {
            comboBox_Activity.Items.Add(Properties.Buttons.comboBox_Activity);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.Steps);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.HeartRete);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.PAI);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.Sleep);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.Stress);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.SPO2);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.OneKey);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.Respiration);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.Menstrual);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.SportList);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.Sport);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.SportRecord);
            comboBox_Activity.Items.Add(Properties.ButtonFunctions.SportStatus);

            comboBox_App.Items.Add(Properties.Buttons.comboBox_App);
            comboBox_App.Items.Add(Properties.ButtonFunctions.Alarm);
            comboBox_App.Items.Add(Properties.ButtonFunctions.Schedule);
            comboBox_App.Items.Add(Properties.ButtonFunctions.WorldClock);

            comboBox_System.Items.Add(Properties.Buttons.comboBox_System);
            comboBox_System.Items.Add(Properties.ButtonFunctions.Settings);
            comboBox_System.Items.Add(Properties.ButtonFunctions.LowBattery);
            comboBox_System.Items.Add(Properties.ButtonFunctions.PowerHint);
            comboBox_System.Items.Add(Properties.ButtonFunctions.BatteryManager);
            comboBox_System.Items.Add(Properties.ButtonFunctions.SaveMode);
            comboBox_System.Items.Add(Properties.ButtonFunctions.Light);
            comboBox_System.Items.Add(Properties.ButtonFunctions.Display);
            comboBox_System.Items.Add(Properties.ButtonFunctions.AODStyle);
            comboBox_System.Items.Add(Properties.ButtonFunctions.AOD);
            comboBox_System.Items.Add(Properties.ButtonFunctions.DND);
            comboBox_System.Items.Add(Properties.ButtonFunctions.WatchFace);
            comboBox_System.Items.Add(Properties.ButtonFunctions.System);
            comboBox_System.Items.Add(Properties.ButtonFunctions.ReStart);
            comboBox_System.Items.Add(Properties.ButtonFunctions.FindPhone);
            comboBox_System.Items.Add(Properties.ButtonFunctions.Test1);
            comboBox_System.Items.Add(Properties.ButtonFunctions.Test2);

            comboBox_UserScript.Items.Add(Properties.Buttons.comboBox_UserScript);

            radioButton_londPress.Enabled = !(apiLevel < 2);

            #region user_script
            DirectoryInfo Folder;
            string dirName = Path.Combine(Application.StartupPath, "user_scripts");
            if (Directory.Exists(dirName))
            {
                Folder = new DirectoryInfo(dirName);
                FileInfo[] Scripts;
                Scripts = Folder.GetFiles("*.js").OrderBy(p => Path.GetFileNameWithoutExtension(p.Name)).ToArray();

                if (Scripts.Length > 0)
                {
                    UserScriptList.Clear();
                    try
                    {
                        foreach (FileInfo file in Scripts)
                        {
                            string fileNameOnly = Path.GetFileNameWithoutExtension(file.Name);
                            string js_script = File.ReadAllText(file.FullName);
                            UserScriptList.Add(fileNameOnly, js_script);
                            comboBox_UserScript.Items.Add(fileNameOnly);
                        }
                        comboBox_UserScript.Enabled = true;
                    }
                    catch
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show(Properties.Strings.Message_ReadScript_Error, Properties.Strings.Message_Error_Caption);
                    } 
                }
            }



            //string json = "[{ key: \"value1\"}, {key: \"value2\"}]";
            string json = "[{ \"name\": \"test 1\", \"function\": \"console.log('test 1');\"}, { \"name\": \"test 2\", \"function\": \"console.log('test 2');\"}]";

            var dict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);
            #endregion

            comboBox_Activity.SelectedIndex = 0;
            comboBox_App.SelectedIndex = 0;
            comboBox_System.SelectedIndex = 0;
            comboBox_UserScript.SelectedIndex = 0;

            richTextBox_click.Text = clickFunc;
            richTextBox_longPress.Text = longPressFunc;
        }

        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            Color color = groupBox.ForeColor;
            if (groupBox.Enabled) DrawGroupBox(groupBox, e.Graphics, color, Color.DarkGray);
            else DrawGroupBox(groupBox, e.Graphics, Color.DarkGray, Color.DarkGray);
        }
        private void DrawGroupBox(GroupBox groupBox, Graphics g, Color textColor, Color borderColor)
        {
            if (groupBox != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(groupBox.Text, groupBox.Font);
                Rectangle rect = new Rectangle(groupBox.ClientRectangle.X,
                                               groupBox.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               groupBox.ClientRectangle.Width - 1,
                                               groupBox.ClientRectangle.Height - (int)(strSize.Height / 2) - 5);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(groupBox.Text, groupBox.Font, textBrush, groupBox.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + groupBox.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + groupBox.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

        private void comboBox_Click(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.Items.RemoveAt(0);
        }

        private void comboBox_Activity_DropDownClosed(object sender, EventArgs e)
        {
            string script = "";
            if (comboBox_Activity.SelectedIndex == 0) script = Properties.ButtonFunctions.Steps_function;
            if (comboBox_Activity.SelectedIndex == 1) script = Properties.ButtonFunctions.HeartRete_function;
            if (comboBox_Activity.SelectedIndex == 2) script = Properties.ButtonFunctions.PAI_function;
            if (comboBox_Activity.SelectedIndex == 3) script = Properties.ButtonFunctions.Sleep_function;
            if (comboBox_Activity.SelectedIndex == 4) script = Properties.ButtonFunctions.Stress_function;
            if (comboBox_Activity.SelectedIndex == 5) script = Properties.ButtonFunctions.SPO2_function;
            if (comboBox_Activity.SelectedIndex == 6) script = Properties.ButtonFunctions.OneKey_function;
            if (comboBox_Activity.SelectedIndex == 7) script = Properties.ButtonFunctions.Respiration_function;
            if (comboBox_Activity.SelectedIndex == 8) script = Properties.ButtonFunctions.Menstrual_function;
            if (comboBox_Activity.SelectedIndex == 9) script = Properties.ButtonFunctions.SportList_function;
            if (comboBox_Activity.SelectedIndex == 10) script = Properties.ButtonFunctions.Sport_function;
            if (comboBox_Activity.SelectedIndex == 11) script = Properties.ButtonFunctions.SportRecord_function;
            if (comboBox_Activity.SelectedIndex == 12) script = Properties.ButtonFunctions.SportStatus_function;

            comboBox_Activity.Items.Insert(0, Properties.Buttons.comboBox_Activity);
            comboBox_Activity.SelectedIndex = 0;

            if (script.Length > 0)
            {
                if (radioButton_click.Checked) richTextBox_click.Text = script;
                else richTextBox_longPress.Text = script;
            }
        }

        private void comboBox_App_DropDownClosed(object sender, EventArgs e)
        {
            string script = "";
            if (comboBox_App.SelectedIndex == 0) script = Properties.ButtonFunctions.Alarm_function;
            if (comboBox_App.SelectedIndex == 1) script = Properties.ButtonFunctions.Schedule_function;
            if (comboBox_App.SelectedIndex == 2) script = Properties.ButtonFunctions.WorldClock_function;

            comboBox_App.Items.Insert(0, Properties.Buttons.comboBox_App);
            comboBox_App.SelectedIndex = 0;

            if (script.Length > 0)
            {
                if (radioButton_click.Checked) richTextBox_click.Text = script;
                else richTextBox_longPress.Text = script;
            }
        }

        private void comboBox_System_DropDownClosed(object sender, EventArgs e)
        {
            string script = "";
            if (comboBox_System.SelectedIndex == 0) script = Properties.ButtonFunctions.Settings_function;
            if (comboBox_System.SelectedIndex == 1) script = Properties.ButtonFunctions.LowBattery_function;
            if (comboBox_System.SelectedIndex == 2) script = Properties.ButtonFunctions.PowerHint_function;
            if (comboBox_System.SelectedIndex == 3) script = Properties.ButtonFunctions.BatteryManager_function;
            if (comboBox_System.SelectedIndex == 4) script = Properties.ButtonFunctions.SaveMode_function;
            if (comboBox_System.SelectedIndex == 5) script = Properties.ButtonFunctions.Light_function;
            if (comboBox_System.SelectedIndex == 6) script = Properties.ButtonFunctions.Display_function;
            if (comboBox_System.SelectedIndex == 7) script = Properties.ButtonFunctions.AODStyle_function;
            if (comboBox_System.SelectedIndex == 8) script = Properties.ButtonFunctions.AOD_function;
            if (comboBox_System.SelectedIndex == 9) script = Properties.ButtonFunctions.DND_function;
            if (comboBox_System.SelectedIndex == 10) script = Properties.ButtonFunctions.WatchFace_function;
            if (comboBox_System.SelectedIndex == 11) script = Properties.ButtonFunctions.System_function;
            if (comboBox_System.SelectedIndex == 12) script = Properties.ButtonFunctions.ReStart_function;
            if (comboBox_System.SelectedIndex == 13) script = Properties.ButtonFunctions.FindPhone_function;
            if (comboBox_System.SelectedIndex == 14) script = Properties.ButtonFunctions.Test1_function;
            if (comboBox_System.SelectedIndex == 15) script = Properties.ButtonFunctions.Test2_function;

            comboBox_System.Items.Insert(0, Properties.Buttons.comboBox_System);
            comboBox_System.SelectedIndex = 0;

            if (script.Length > 0)
            {
                if (radioButton_click.Checked) richTextBox_click.Text = script;
                else richTextBox_longPress.Text = script;
            }
        }

        private void comboBox_UserScript_DropDownClosed(object sender, EventArgs e)
        {
            string script = "";
            string scriptName = comboBox_UserScript.Text;
            if (UserScriptList.ContainsKey(scriptName)) script= UserScriptList[scriptName];

            comboBox_UserScript.Items.Insert(0, Properties.Buttons.comboBox_UserScript);
            comboBox_UserScript.SelectedIndex = 0;

            if (script.Length > 0)
            {
                if (radioButton_click.Checked) richTextBox_click.Text = script;
                else richTextBox_longPress.Text = script;
            }
        }

        private void radioButton_click_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_click.Visible = radioButton_click.Checked;
            groupBox_longPress.Visible = radioButton_londPress.Checked;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            updateFunction = true;
            clickFunc = richTextBox_click.Text;
            longPressFunc = richTextBox_longPress.Text;

            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            updateFunction = false;
            this.Close();
        }
    }
}
