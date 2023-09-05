using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ControlLibrary
{
    public partial class UCtrl_Button_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        private List<string> ListUserScriptClick;
        private List<string> ListUserScriptLongPress;
        private float apiLevel = 0;

        public UCtrl_Button_Opt()
        {
            InitializeComponent();

            dataGridView_buttons.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ListUserScriptClick = new List<string>();
            ListUserScriptLongPress = new List<string>();
        }

        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            if (groupBox.Enabled) DrawGroupBox(groupBox, e.Graphics, Color.Black, Color.DarkGray);
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

        [Browsable(true)]
        [Description("Происходит при изменении выбранной кнопки")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs, int rowIndex);

        [Browsable(true)]
        [Description("Добавление кнопки")]
        public event AddButtonHandler AddButton;
        public delegate void AddButtonHandler(int rowIndex);

        [Browsable(true)]
        [Description("Удаление кнопки")]
        public event DelButtonHandler DelButton;
        public delegate void DelButtonHandler(int rowIndex);

        [Browsable(true)]
        [Description("Выбрали другую кнопку")]
        public event SelectButtonHandler SelectButton;
        public delegate void SelectButtonHandler(int rowIndex);

        [Browsable(true)]
        [Description("Происходит при изменении функций выбранной кнопки")]
        public event ScriptChangedHandler ScriptChanged;
        public delegate void ScriptChangedHandler(int rowIndex, string clickFunc, string longPressFunc);

        public void SetNormalImage(string value)
        {
            comboBox_normal_image.Text = value;
            if (comboBox_normal_image.SelectedIndex < 0) comboBox_normal_image.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetNormalImage()
        {
            if (comboBox_normal_image.SelectedIndex < 0) return "";
            return comboBox_normal_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int comboBoxGetSelectedIndexNormalImage()
        {
            return comboBox_normal_image.SelectedIndex;
        }

        public void SetPressImage(string value)
        {
            comboBox_press_image.Text = value;
            if (comboBox_press_image.SelectedIndex < 0) comboBox_press_image.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetPressImage()
        {
            if (comboBox_press_image.SelectedIndex < 0) return "";
            return comboBox_press_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int comboBoxGetSelectedIndexPressImage()
        {
            return comboBox_press_image.SelectedIndex;
        }

        public void SetColorText(Color color)
        {
            comboBox_Text_color.BackColor = color;
        }

        public Color GetColorText()
        {
            return comboBox_Text_color.BackColor;
        }

        public void SetColorNormal(Color color)
        {
            comboBox_normal_color.BackColor = color;
        }

        public Color GetColorNormal()
        {
            return comboBox_normal_color.BackColor;
        }

        public void SetColorPress(Color color)
        {
            comboBox_press_color.BackColor = color;
        }

        public Color GetColorPress()
        {
            return comboBox_press_color.BackColor;
        }

        public void SetText(string text)
        {
            textBox_script.Text = text;
        }

        public string GetText()
        {
            return textBox_script.Text;
        }

        public void UpdateButtonsList (List<String> buttonsClickFuncList, List<String> buttonsLongPressFuncList, int rowIndex = -1)
        {
            ListUserScriptClick = buttonsClickFuncList;
            ListUserScriptLongPress = buttonsLongPressFuncList;
            dataGridView_buttons.Rows.Clear();

            for (int index = 0; index < buttonsClickFuncList.Count; index++)
            {
                //string scriptTypeClick = Properties.Buttons.Empty;
                //string scriptTypeLongPress = Properties.Buttons.Empty;
                string func = buttonsClickFuncList[index];
                string scriptTypeClick = GetFunctionName(func);
                /*if(func.Length > 3) scriptTypeClick = Properties.Buttons.User_script;
                switch (func)
                {
                    case "hmApp.startApp({url: 'activityAppScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Steps;
                        break;
                    case "hmApp.startApp({url: 'heart_app_Screen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.HeartRete;
                        break;
                    case "hmApp.startApp({url: 'PAI_app_Screen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.PAI;
                        break;
                    case "hmApp.startApp({url: 'Sleep_HomeScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Sleep;
                        break;
                    case "hmApp.startApp({url: 'StressHomeScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Stress;
                        break;
                    case "hmApp.startApp({url: 'spo_HomeScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.SPO2;
                        break;
                    case "hmApp.startApp({url: 'oneKeyAppScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.OneKey;
                        break;
                    case "hmApp.startApp({url: 'RespirationwidgetScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Respiration;
                        break;
                    case "hmApp.startApp({url: 'menstrualAppScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Menstrual;
                        break;
                    case "hmApp.startApp({url: 'SportListScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.SportList;
                        break;
                    case "hmApp.startApp({url: 'SportScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Sport;
                        break;
                    case "hmApp.startApp({url: 'SportRecordListScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.SportRecord;
                        break;
                    case "hmApp.startApp({url: 'SportStatusScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.SportStatus;
                        break;

                    //////////////////////
                    case "hmApp.startApp({url: 'AlarmInfoScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Alarm;
                        break;
                    case "hmApp.startApp({url: 'ScheduleCalScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Schedule;
                        break;
                    case "hmApp.startApp({url: 'WorldClockScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.WorldClock;
                        break;

                    //////////////////////
                    case "hmApp.startApp({url: 'Settings_homeScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Settings;
                        break;
                    case "hmApp.startApp({url: 'LowBatteryScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.LowBattery;
                        break;
                    case "hmApp.startApp({url: 'PowerSaveHintScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.PowerHint;
                        break;
                    case "hmApp.startApp({url: 'Settings_batteryManagerScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.BatteryManager;
                        break;
                    case "hmApp.startApp({url: 'PowerSaveModeScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.SaveMode;
                        break;
                    case "hmApp.startApp({url: 'Settings_lightAdjustScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Light;
                        break;
                    case "hmApp.startApp({url: 'Settings_displayBrightScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.Display;
                        break;
                    case "hmApp.startApp({url: 'Settings_standbyModelScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.AOD;
                        break;
                    case "hmApp.startApp({url: 'Settings_dndModelScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.DND;
                        break;
                    case "hmApp.startApp({url: 'Settings_standbyHomeScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.AODStyle;
                        break;
                    case "hmApp.startApp({url: 'WatchFaceScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.WatchFace;
                        break;
                    case "hmApp.startApp({url: 'Settings_systemScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.System;
                        break;
                    case "hmApp.startApp({url: 'HmReStartScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.ReStart;
                        break;
                    case "hmApp.startApp({url: 'FindPhoneScreen', native: true });":
                        scriptTypeClick = Properties.ButtonFunctions.FindPhone;
                        break;
                }*/

                string scriptTypeLongPress = Properties.Buttons.Empty;
                if (index < buttonsLongPressFuncList.Count)
                {
                    func = buttonsLongPressFuncList[index];
                    scriptTypeLongPress = GetFunctionName(func);
                    /*if (func.Length > 3) scriptTypeLongPress = Properties.Buttons.User_script;
                    switch (func)
                    {
                        case "hmApp.startApp({url: 'activityAppScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Steps;
                            break;
                        case "hmApp.startApp({url: 'heart_app_Screen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.HeartRete;
                            break;
                        case "hmApp.startApp({url: 'PAI_app_Screen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.PAI;
                            break;
                        case "hmApp.startApp({url: 'Sleep_HomeScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Sleep;
                            break;
                        case "hmApp.startApp({url: 'StressHomeScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Stress;
                            break;
                        case "hmApp.startApp({url: 'spo_HomeScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.SPO2;
                            break;
                        case "hmApp.startApp({url: 'oneKeyAppScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.OneKey;
                            break;
                        case "hmApp.startApp({url: 'RespirationwidgetScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Respiration;
                            break;
                        case "hmApp.startApp({url: 'menstrualAppScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Menstrual;
                            break;
                        case "hmApp.startApp({url: 'SportListScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.SportList;
                            break;
                        case "hmApp.startApp({url: 'SportScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Sport;
                            break;
                        case "hmApp.startApp({url: 'SportRecordListScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.SportRecord;
                            break;
                        case "hmApp.startApp({url: 'SportStatusScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.SportStatus;
                            break;

                        //////////////////////
                        case "hmApp.startApp({url: 'AlarmInfoScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Alarm;
                            break;
                        case "hmApp.startApp({url: 'ScheduleCalScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Schedule;
                            break;
                        case "hmApp.startApp({url: 'WorldClockScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.WorldClock;
                            break;

                        //////////////////////
                        case "hmApp.startApp({url: 'Settings_homeScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Settings;
                            break;
                        case "hmApp.startApp({url: 'LowBatteryScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.LowBattery;
                            break;
                        case "hmApp.startApp({url: 'PowerSaveHintScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.PowerHint;
                            break;
                        case "hmApp.startApp({url: 'Settings_batteryManagerScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.BatteryManager;
                            break;
                        case "hmApp.startApp({url: 'PowerSaveModeScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.SaveMode;
                            break;
                        case "hmApp.startApp({url: 'Settings_lightAdjustScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Light;
                            break;
                        case "hmApp.startApp({url: 'Settings_displayBrightScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.Display;
                            break;
                        case "hmApp.startApp({url: 'Settings_standbyModelScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.AOD;
                            break;
                        case "hmApp.startApp({url: 'Settings_dndModelScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.DND;
                            break;
                        case "hmApp.startApp({url: 'Settings_standbyHomeScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.AODStyle;
                            break;
                        case "hmApp.startApp({url: 'WatchFaceScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.WatchFace;
                            break;
                        case "hmApp.startApp({url: 'Settings_systemScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.System;
                            break;
                        case "hmApp.startApp({url: 'HmReStartScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.ReStart;
                            break;
                        case "hmApp.startApp({url: 'FindPhoneScreen', native: true });":
                            scriptTypeLongPress = Properties.ButtonFunctions.FindPhone;
                            break;
                    } */
                }

                string scriptType = Properties.Buttons.Button + " <" + scriptTypeClick + "; " + scriptTypeLongPress + ">";
                DataGridViewRow RowNew = new DataGridViewRow();
                RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = (index + 1).ToString() });
                RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = scriptType });
                RowNew.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RowNew.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView_buttons.Rows.Add(RowNew);
            }
            
            if (buttonsClickFuncList.Count > 0) button_del.Enabled = true; 
            else button_del.Enabled = false;
            if (buttonsClickFuncList.Count >= 25) button_add.Enabled = false;
            else button_add.Enabled = true;

            if (rowIndex >= 0 && rowIndex < dataGridView_buttons.Rows.Count)
            {
                dataGridView_buttons.Rows[rowIndex].Selected = true;
                dataGridView_buttons.CurrentCell = dataGridView_buttons.Rows[rowIndex].Cells[0];
            }
        }

        private string GetFunctionName(string func)
        {
            string functinName = Properties.Buttons.Empty;
            if (func.Length > 3) functinName = Properties.Buttons.User_script;
            switch (func)
            {
                case "hmApp.startApp({url: 'activityAppScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Steps;
                    break;
                case "hmApp.startApp({url: 'heart_app_Screen', native: true });":
                    functinName = Properties.ButtonFunctions.HeartRete;
                    break;
                case "hmApp.startApp({url: 'PAI_app_Screen', native: true });":
                    functinName = Properties.ButtonFunctions.PAI;
                    break;
                case "hmApp.startApp({url: 'Sleep_HomeScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Sleep;
                    break;
                case "hmApp.startApp({url: 'StressHomeScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Stress;
                    break;
                case "hmApp.startApp({url: 'spo_HomeScreen', native: true });":
                    functinName = Properties.ButtonFunctions.SPO2;
                    break;
                case "hmApp.startApp({url: 'oneKeyAppScreen', native: true });":
                    functinName = Properties.ButtonFunctions.OneKey;
                    break;
                case "hmApp.startApp({url: 'RespirationwidgetScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Respiration;
                    break;
                case "hmApp.startApp({url: 'menstrualAppScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Menstrual;
                    break;
                case "hmApp.startApp({url: 'SportListScreen', native: true });":
                    functinName = Properties.ButtonFunctions.SportList;
                    break;
                case "hmApp.startApp({url: 'SportScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Sport;
                    break;
                case "hmApp.startApp({url: 'SportRecordListScreen', native: true });":
                    functinName = Properties.ButtonFunctions.SportRecord;
                    break;
                case "hmApp.startApp({url: 'SportStatusScreen', native: true });":
                    functinName = Properties.ButtonFunctions.SportStatus;
                    break;

                //////////////////////
                case "hmApp.startApp({url: 'AlarmInfoScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Alarm;
                    break;
                case "hmApp.startApp({url: 'ScheduleCalScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Schedule;
                    break;
                case "hmApp.startApp({url: 'WorldClockScreen', native: true });":
                    functinName = Properties.ButtonFunctions.WorldClock;
                    break;

                //////////////////////
                case "hmApp.startApp({url: 'Settings_homeScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Settings;
                    break;
                case "hmApp.startApp({url: 'LowBatteryScreen', native: true });":
                    functinName = Properties.ButtonFunctions.LowBattery;
                    break;
                case "hmApp.startApp({url: 'PowerSaveHintScreen', native: true });":
                    functinName = Properties.ButtonFunctions.PowerHint;
                    break;
                case "hmApp.startApp({url: 'Settings_batteryManagerScreen', native: true });":
                    functinName = Properties.ButtonFunctions.BatteryManager;
                    break;
                case "hmApp.startApp({url: 'PowerSaveModeScreen', native: true });":
                    functinName = Properties.ButtonFunctions.SaveMode;
                    break;
                case "hmApp.startApp({url: 'Settings_lightAdjustScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Light;
                    break;
                case "hmApp.startApp({url: 'Settings_displayBrightScreen', native: true });":
                    functinName = Properties.ButtonFunctions.Display;
                    break;
                case "hmApp.startApp({url: 'Settings_standbyModelScreen', native: true });":
                    functinName = Properties.ButtonFunctions.AOD;
                    break;
                case "hmApp.startApp({url: 'Settings_dndScreen', native: true });":
                    functinName = Properties.ButtonFunctions.DND;
                    break;
                case "hmApp.startApp({url: 'Settings_standbyHomeScreen', native: true });":
                    functinName = Properties.ButtonFunctions.AODStyle;
                    break;
                case "hmApp.startApp({url: 'WatchFaceScreen', native: true });":
                    functinName = Properties.ButtonFunctions.WatchFace;
                    break;
                case "hmApp.startApp({url: 'Settings_systemScreen', native: true });":
                    functinName = Properties.ButtonFunctions.System;
                    break;
                case "hmApp.startApp({url: 'HmReStartScreen', native: true });":
                    functinName = Properties.ButtonFunctions.ReStart;
                    break;
                case "hmApp.startApp({url: 'FindPhoneScreen', native: true });":
                    functinName = Properties.ButtonFunctions.FindPhone;
                    break;
                case "hmApp.startApp({url: 'Test1Screen', native: true });":
                    functinName = Properties.ButtonFunctions.Test1;
                    break;
                case "hmApp.startApp({url: 'Test2Screen', native: true });":
                    functinName = Properties.ButtonFunctions.Test2;
                    break;
            }
            return functinName;
        }

        #region Standard events

        private void textBox_script_TextChanged(object sender, EventArgs e)
        {
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;
                }

                if (ValueChanged != null && !setValue && rowIndex >= 0)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs, rowIndex);
                }
            }
            catch (Exception)
            {
            }
        }

        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                ComboBox comboBox = sender as ComboBox;
                comboBox.Text = "";
                comboBox.SelectedIndex = -1;

                int rowIndex = -1;
                try
                {
                    int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                    if (selectedRowCount > 0)
                    {
                        DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                        rowIndex = selectedCells[0].RowIndex;
                    }

                    if (ValueChanged != null && !setValue && rowIndex >= 0)
                    {
                        EventArgs eventArgs = new EventArgs();
                        ValueChanged(this, eventArgs, rowIndex);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            //if (comboBox.Items.Count < 5) comboBox.DropDownHeight = comboBox.Items.Count * 35;
            //else comboBox.DropDownHeight = 106;
            float size = comboBox.Font.Size;
            Font myFont;
            FontFamily family = comboBox.Font.FontFamily;
            e.DrawBackground();
            int itemWidth = e.Bounds.Height;
            int itemHeight = e.Bounds.Height - 4;

            if (e.Index >= 0)
            {
                try
                {
                    using (FileStream stream = new FileStream(ListImagesFullName[e.Index], FileMode.Open, FileAccess.Read))
                    {
                        Image image = Image.FromStream(stream);
                        float scale = (float)itemWidth / image.Width;
                        if ((float)itemHeight / image.Height < scale) scale = (float)itemHeight / image.Height;
                        float itemWidthRec = image.Width * scale;
                        float itemHeightRec = image.Height * scale;
                        Rectangle rectangle = new Rectangle((int)(itemWidth - itemWidthRec) / 2 + 2,
                            e.Bounds.Top + (int)(itemHeight - itemHeightRec) / 2 + 2, (int)itemWidthRec, (int)itemHeightRec);
                        e.Graphics.DrawImage(image, rectangle);
                    }
                }
                catch { }
            }
            myFont = new Font(family, size);
            StringFormat lineAlignment = new StringFormat(); ;
            lineAlignment.LineAlignment = StringAlignment.Center;
            if (e.Index >= 0)
                e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), myFont, System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + itemWidth, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), lineAlignment);
            e.DrawFocusRectangle();
        }

        private void comboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 35;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;
                }

                if (ValueChanged != null && !setValue && rowIndex >= 0)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs, rowIndex);
                }
            }
            catch (Exception)
            {
            }

            if(comboBox_normal_image.Text.Length > 0 && comboBox_press_image.Text.Length > 0) groupBox_color.Enabled = false;
            else groupBox_color.Enabled = true;
        }

        private void comboBox_color_Click(object sender, EventArgs e)
        {
            Program_Settings ProgramSettings = new Program_Settings();
            ColorDialog colorDialog = new ColorDialog();
            ComboBox comboBox_color = sender as ComboBox;
            colorDialog.Color = comboBox_color.BackColor;
            colorDialog.FullOpen = true;

            // читаем пользовательские цвета из настроек
            if (File.Exists(Application.StartupPath + @"\Settings.json"))
            {
                ProgramSettings = JsonConvert.DeserializeObject<Program_Settings>
                            (File.ReadAllText(Application.StartupPath + @"\Settings.json"), new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
            }
            colorDialog.CustomColors = ProgramSettings.CustomColors;


            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета формы
            comboBox_color.BackColor = colorDialog.Color;
            if (ProgramSettings.CustomColors != colorDialog.CustomColors)
            {
                ProgramSettings.CustomColors = colorDialog.CustomColors;

                string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
            }

            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;
                }

                if (ValueChanged != null && !setValue && rowIndex >= 0)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs, rowIndex);
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_normal_image.Items.Clear();
            comboBox_press_image.Items.Clear();

            ListUserScriptClick.Clear();
            ListUserScriptLongPress.Clear();

            comboBox_normal_image.Items.AddRange(ListImages.ToArray());
            comboBox_press_image.Items.AddRange(ListImages.ToArray());
            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_normal_image.DropDownHeight = 1;
                comboBox_press_image.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_normal_image.DropDownHeight = 35 * count + 1;
                comboBox_press_image.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_normal_image.DropDownHeight = 106;
                comboBox_press_image.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear(float api_level)
        {
            setValue = true;

            comboBox_normal_image.Text = null;
            comboBox_press_image.Text = null;

            numericUpDown_buttonX.Value = 0;
            numericUpDown_buttonY.Value = 0;
            numericUpDown_width.Value = 100;
            numericUpDown_height.Value = 40;
            numericUpDown_radius.Value = 12;
            numericUpDown_textSize.Value = 25;

            dataGridView_buttons.Rows.Clear();
            button_del.Enabled = false;

            apiLevel = api_level;

            setValue = false;
        }

        #endregion

        #region contextMenu
        private void contextMenuStrip_X_Opening(object sender, CancelEventArgs e)
        {
            if ((MouseСoordinates.X < 0) || (MouseСoordinates.Y < 0))
            {
                contextMenuStrip_X.Items[0].Enabled = false;
            }
            else
            {
                contextMenuStrip_X.Items[0].Enabled = true;
            }
            decimal i = 0;
            if ((Clipboard.ContainsText() == true) && (decimal.TryParse(Clipboard.GetText(), out i)))
            {
                contextMenuStrip_X.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip_X.Items[2].Enabled = false;
            }
        }

        private void contextMenuStrip_Y_Opening(object sender, CancelEventArgs e)
        {
            if ((MouseСoordinates.X < 0) || (MouseСoordinates.Y < 0))
            {
                contextMenuStrip_Y.Items[0].Enabled = false;
            }
            else
            {
                contextMenuStrip_Y.Items[0].Enabled = true;
            }
            decimal i = 0;
            if ((Clipboard.ContainsText() == true) && (decimal.TryParse(Clipboard.GetText(), out i)))
            {
                contextMenuStrip_Y.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip_Y.Items[2].Enabled = false;
            }
        }

        private void вставитьКоординатуХToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    numericUpDown.Value = MouseСoordinates.X;
                }
            }
        }

        private void вставитьКоординатуYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    numericUpDown.Value = MouseСoordinates.Y;
                }
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    Clipboard.SetText(numericUpDown.Value.ToString());
                }
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    //Если в буфере обмен содержится текст
                    if (Clipboard.ContainsText() == true)
                    {
                        //Извлекаем (точнее копируем) его и сохраняем в переменную
                        decimal i = 0;
                        if (decimal.TryParse(Clipboard.GetText(), out i))
                        {
                            if (i > numericUpDown.Maximum) i = numericUpDown.Maximum;
                            if (i < numericUpDown.Minimum) i = numericUpDown.Minimum;
                            numericUpDown.Value = i;
                        }
                    }

                }
            }
        }
        #endregion

        #region numericUpDown
        private void numericUpDown_picturesX_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.X < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.X;
            }
        }

        private void numericUpDown_picturesY_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.Y < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.Y;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;
                }

                if (ValueChanged != null && !setValue && rowIndex >= 0)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs, rowIndex);
                }
            }
            catch (Exception)
            {
            }
        }

        private void numericUpDown_length_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.X < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                if ((MouseСoordinates.X - numericUpDown_buttonX.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.X - numericUpDown_buttonX.Value;
                }
                else numericUpDown.Value = 1;
            }
        }

        private void numericUpDown_width_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.Y < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                if ((MouseСoordinates.Y - numericUpDown_buttonY.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.Y - numericUpDown_buttonY.Value;
                }
                else numericUpDown.Value = 1;
            }
        }

        private void numericUpDown_position_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_buttonX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_buttonY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_buttonX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_buttonY.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_buttonY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_buttonY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_buttonY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_buttonY.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_buttonX" || numericUpDown.Name == "numericUpDown_buttonY"))
                    numericUpDown_buttonX.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_buttonX" || numericUpDown.Name == "numericUpDown_buttonY"))
                    numericUpDown_buttonX.UpButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_size_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_width")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_height.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_width")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_height.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_height")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_height.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_height")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_height.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_width" || numericUpDown.Name == "numericUpDown_height"))
                    numericUpDown_width.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_width" || numericUpDown.Name == "numericUpDown_height"))
                    numericUpDown_width.UpButton();

                e.Handled = true;
            }
        }

        #endregion

        private void button_add_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;
                }
            }
            catch (Exception)
            {
            }

            if (AddButton != null && !setValue)
            {
                AddButton(rowIndex);
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;

                    if (DelButton != null && !setValue)
                    {
                        DelButton(rowIndex);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView_bottons_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (SelectButton != null && !setValue)
            {
                SelectButton(rowIndex);
            }
        }

        private void dataGridView_buttons_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                DataGridView dataGridView = sender as DataGridView; 
                if (dataGridView != null)
                {
                    try
                    {
                        int rowIndex = -1;
                        int selectedRowCount = dataGridView.SelectedCells.Count;
                        if (selectedRowCount > 0)
                        {
                            DataGridViewSelectedCellCollection selectedCells = dataGridView.SelectedCells;
                            rowIndex = selectedCells[0].RowIndex;

                            if (DelButton != null && !setValue)
                            {
                                DelButton(rowIndex);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                dataGridView_buttons_MouseDoubleClick(dataGridView_buttons, null);
            }
        }

        private void dataGridView_buttons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            int rowIndex = -1;
            if (dataGridView != null)
            {
                try
                {
                    int selectedRowCount = dataGridView.SelectedCells.Count;
                    if (selectedRowCount > 0)
                    {
                        DataGridViewSelectedCellCollection selectedCells = dataGridView.SelectedCells;
                        rowIndex = selectedCells[0].RowIndex;
                    }
                }
                catch (Exception)
                {
                }
            }
            if (rowIndex >= 0)
            {
                string scriptClick = "";
                string scriptLongPress = "";
                if (rowIndex < ListUserScriptClick.Count) scriptClick = ListUserScriptClick[rowIndex];
                if (rowIndex < ListUserScriptLongPress.Count) scriptLongPress = ListUserScriptLongPress[rowIndex];
                AddButtonFunction f = new AddButtonFunction(scriptClick, scriptLongPress, apiLevel);
                f.ShowDialog();
                bool dialogResult = f.UpdateFunction; 
                if (dialogResult)
                {
                    string clickFunc = f.ClickFunc;
                    string longPressFunc = f.LongPressFunc;

                    if (ScriptChanged != null && !setValue)
                    {
                        ScriptChanged(rowIndex, clickFunc, longPressFunc);
                    }
                }
            }
        }
    }


}
