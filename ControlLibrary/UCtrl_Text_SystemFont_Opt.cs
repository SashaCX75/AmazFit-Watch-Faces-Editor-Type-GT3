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

namespace ControlLibrary
{
    public partial class UCtrl_Text_SystemFont_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        public Object _ElementWithSystemFont;
        //public string fonts_path; // папка со шрифтами
        public Dictionary<string, Object> WidgetProperty = new Dictionary<string, Object>();

        private bool Font_mode;
        //private bool Number_mode = true;
        private bool Unit_mode = true;
        private bool Zero_mode = true;
        private bool Year_mode = false;
        private bool DayMonthYear_mode = false;
        private bool AmPm_mode = false;
        private bool UnitStr_mode = false;

        public UCtrl_Text_SystemFont_Opt()
        {
            InitializeComponent();
            setValue = true;
            comboBox_alignmentHorizontal.SelectedIndex = 0;
            comboBox_alignmentVertical.SelectedIndex = 0;
            comboBox_textStyle.SelectedIndex = 0;
            comboBox_fonts.Items.Add(Properties.Strings.SystemFont);
            comboBox_fonts.SelectedIndex = 0;
            UserFont = false;
            setValue = false;
        }

        public void AddFonts(Dictionary<string,string> fontsList)
        {
            comboBox_fonts.Items.Clear();
            comboBox_fonts.Items.Add(Properties.Strings.SystemFont);
            comboBox_fonts.SelectedIndex = 0;
            foreach (KeyValuePair<string, string> fontNames in fontsList)
            {
                //string fileName = Path.Combine(relativePath, fontNames.Value);
                try
                {
                    if (File.Exists(fontNames.Value))
                    {
                        System.Drawing.Text.PrivateFontCollection fontCollection = new System.Drawing.Text.PrivateFontCollection();
                        fontCollection.AddFontFile(fontNames.Value);
                        Font addFont = new Font(fontCollection.Families[0], 18);
                        string fontName = addFont.Name;
                        string item = fontNames.Key;
                        if (fontName.Length > 3) item += " (" + fontName + ")";
                        comboBox_fonts.Items.Add(item); 
                    }

                }
                catch
                {
                    MessageBox.Show("Ошибка добавления шрифта " + fontNames.Key);
                }
            }
            setValue = true;
            comboBox_fonts.SelectedIndex = 0;
            setValue = false;
        }

        public void SetHorizontalAlignment(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "LEFT":
                    result = 0;
                    break;
                case "CENTER_H":
                    result = 1;
                    break;
                case "RIGHT":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_alignmentHorizontal.SelectedIndex = result;
        }

        /// <summary>Возвращает выравнивание строкой "LEFT", "RIGHT", "CENTER_H"</summary>
        public string GetHorizontalAlignment()
        {
            string result;
            switch (comboBox_alignmentHorizontal.SelectedIndex)
            {
                case 0:
                    result = "LEFT";
                    break;
                case 1:
                    result = "CENTER_H";
                    break;
                case 2:
                    result = "RIGHT";
                    break;

                default:
                    result = "Left";
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexHorizontalAlignment()
        {
            return comboBox_alignmentHorizontal.SelectedIndex;
        }

        public void SetVerticalAlignment(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "TOP":
                    result = 0;
                    break;
                case "CENTER_V":
                    result = 1;
                    break;
                case "BOTTOM":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_alignmentVertical.SelectedIndex = result;
        }

        /// <summary>Возвращает выравнивание строкой "LEFT", "RIGHT", "CENTER_H"</summary>
        public string GetVerticalAlignment()
        {
            string result;
            switch (comboBox_alignmentVertical.SelectedIndex)
            {
                case 0:
                    result = "TOP";
                    break;
                case 1:
                    result = "CENTER_V";
                    break;
                case 2:
                    result = "BOTTOM";
                    break;

                default:
                    result = "TOP";
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexVerticalAlignment()
        {
            return comboBox_alignmentVertical.SelectedIndex;
        }

        public void SetTextStyle(string style)
        {
            int result;
            switch (style)
            {
                case "NONE":
                    result = 0;
                    break;
                case "WRAP":
                    result = 1;
                    break;
                //case "CHAR_WRAP":
                //    result = 2;
                //    break;
                case "ELLIPSIS":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_textStyle.SelectedIndex = result;
        }

        /// <summary>Возвращает выравнивание строкой "ELLIPSIS", "WRAP", "CHAR_WRAP", "NONE"</summary>
        public string GetTextStyle()
        {
            string result;
            switch (comboBox_textStyle.SelectedIndex)
            {
                case 0:
                    result = "NONE";
                    break;
                case 1:
                    result = "WRAP";
                    break;
                //case 2:
                //    result = "CHAR_WRAP";
                //    break;
                case 2:
                    result = "ELLIPSIS";
                    break;

                default:
                    result = "NONE";
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexTextStyle()
        {
            return comboBox_textStyle.SelectedIndex;
        }

        /// <summary>Устанавливаем тип отображения единиц измерения</summary>
        public void SetUnitType(int unit_type)
        {
            switch (unit_type)
            {
                case 0:
                    checkBox_unit.CheckState= CheckState.Unchecked;
                    break;
                case 1:
                    checkBox_unit.CheckState = CheckState.Checked;
                    break;
                case 2:
                    checkBox_unit.CheckState = CheckState.Indeterminate;
                    break;

                default:
                    checkBox_unit.CheckState = CheckState.Indeterminate;
                    break;
            }
        }

        /// <summary>Возвращает тип отображения единиц измерения</summary>
        public int GetUnitType()
        {
            int unit_type = 0;
            switch (checkBox_unit.CheckState)
            {
                case CheckState.Unchecked:
                    unit_type = 0; 
                    break;
                case CheckState.Checked:
                    unit_type = 1;
                    break;
                case CheckState.Indeterminate:
                    unit_type = 2;
                    break;
            }
            return unit_type;
        }

        /// <summary>Устанавливаем место отображения am/pm</summary>
        public void SetUnitEnd(bool unit_end)
        {
            checkBox_inEnd.Checked = unit_end;
        }

        /// <summary>Возвращает место отображения am/pm</summary>
        public bool GetUnitEnd()
        {  
            return checkBox_inEnd.Checked;
        }

        /// <summary>Возвращает имя файла выбраного шрифта</summary>
        public string GetFont()
        {
            string font = "";
            //if (fonts_path != null && fonts_path.Length > 5)
            //{
            //    if (comboBox_fonts.SelectedIndex > 0)
            //    {
            //        string font_fileName = comboBox_fonts.Text;
            //        if (font_fileName.IndexOf(".ttf") > 0) font_fileName = font_fileName.Substring(0, font_fileName.IndexOf(".ttf") + ".ttf".Length);
            //        if (File.Exists(Path.Combine(fonts_path, font_fileName))) font = font_fileName;
            //    } 
            //}
            if (comboBox_fonts.SelectedIndex > 0)
            {
                string font_fileName = comboBox_fonts.Text;
                if (font_fileName.IndexOf(".ttf") > 0) font_fileName = font_fileName.Substring(0, font_fileName.IndexOf(".ttf") + ".ttf".Length);
                font = font_fileName;
            }
            return font;
        }

        /// <summary>Устанавливает выбраный шрифт</summary>
        public void SetFont(string font_fileName)
        {
            if (font_fileName == null || font_fileName.Length == 0) return;
            //comboBox_fonts.SelectedIndex = 0;
            for (int i = 0; i < comboBox_fonts.Items.Count; i++)
            {
                if ((comboBox_fonts.Items[i].ToString().StartsWith(font_fileName) && comboBox_fonts.Items[i].ToString().Length == font_fileName.Length) ||
                    (comboBox_fonts.Items[i].ToString().StartsWith(font_fileName + " ")))
                {
                    comboBox_fonts.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>Отображение возможности выбора пользовательского шрифта</summary>
        [Description("Отображение возможности выбора пользовательского шрифта")]
        public virtual bool UserFont
        {
            get
            {
                return Font_mode;
            }
            set
            {
                Font_mode = value;
                label6.Enabled = Font_mode;
                comboBox_fonts.Enabled = Font_mode;
                button_AddFont.Enabled = Font_mode;
                button_AddFont.Enabled = Font_mode;
            }
        }

        /// <summary>Отображение настроек единиц измерения</summary>
        [Description("Отображение настроек единиц измерения")]
        public virtual bool UnitMode
        {
            get
            {
                return Unit_mode;
            }
            set
            {
                Unit_mode = value;
                checkBox_unit.Enabled = Unit_mode;
            }
        }

        /// <summary>Отображение настроек ведущих нулей</summary>
        [Description("Отображение настроек ведущих нулей")]
        public virtual bool ZeroMode
        {
            get
            {
                return Zero_mode;
            }
            set
            {
                Zero_mode = value;
                checkBox_addZero.Enabled = Zero_mode;
            }
        }

        /// <summary>Режим отображения года</summary>
        [Description("Режим отображения года")]
        public virtual bool Year
        {
            get
            {
                return Year_mode;
            }
            set
            {
                Year_mode = value;
                if (Year_mode)
                {
                    checkBox_addZero.Text = Properties.Strings.UCtrl_Text_Opt_Year_true;
                }
                else
                {
                    checkBox_addZero.Text = Properties.Strings.UCtrl_Text_Opt_Year_false;
                }
            }
        }

        /// <summary>Режим отображения Am/Pm</summary>
        [Description("Режим отображения Am/Pm")]
        public virtual bool AmPm
        {
            get
            {
                return AmPm_mode;
            }
            set
            {
                AmPm_mode = value;
                if (AmPm_mode)
                {
                    checkBox_unit.Text = Properties.Strings.UCtrl_Text_Opt_AmPm_true;
                    checkBox_inEnd.Visible = true;
                }
                else
                {
                    if (DayMonthYear_mode) checkBox_unit.Text = Properties.Strings.UCtrl_Text_Opt_Year_true;
                    else checkBox_unit.Text = Properties.Strings.UCtrl_Text_Opt_AmPm_false;
                    checkBox_inEnd.Visible = false;
                }
            }
        }

        /// <summary>Режим отображения для дня/месяца/года</summary>
        [Description("Режим отображения для дня/месяца/года")]
        public virtual bool DayMonthYear
        {
            get
            {
                return DayMonthYear_mode;
            }
            set
            {
                DayMonthYear_mode = value;
                if (DayMonthYear_mode)
                {
                    checkBox_unit.Text = Properties.Strings.UCtrl_Text_Opt_Year_true;
                    checkBox_inEnd.Visible = false;
                }
                else
                {
                    if (AmPm_mode)
                    {
                        checkBox_unit.Text = Properties.Strings.UCtrl_Text_Opt_AmPm_true;
                        checkBox_inEnd.Visible = true;
                    }
                    else
                    {
                        checkBox_unit.Text = Properties.Strings.UCtrl_Text_Opt_AmPm_false;
                        checkBox_inEnd.Visible = false;
                    }
                }
            }
        }

        /// <summary>Режим отображения строки для единиц измерения</summary>
        [Description("Режим отображения строки для единиц измерения")]
        public virtual bool UnitStrMode
        {
            get
            {
                return UnitStr_mode;
            }
            set
            {
                UnitStr_mode = value;
                label_unit_string.Visible = UnitStr_mode;
                textBox_unit_string.Visible = UnitStr_mode;
            }
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при нажатии кнопки добавления шрифта")]
        public event AddFont_ClickHandler AddFont_Click;
        public delegate void AddFont_ClickHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при нажатии кнопки удаления шрифта")]
        public event DelFont_ClickHandler DelFont_Click;
        public delegate void DelFont_ClickHandler(object sender, EventArgs eventArgs, string fontName);

        [Browsable(true)]
        [Description("Происходит при копировании свойст виджета")]
        public event WidgetProperty_Copy_Handler WidgetProperty_Copy;
        public delegate void WidgetProperty_Copy_Handler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при вставке свойст виджета")]
        public event WidgetProperty_Paste_Handler WidgetProperty_Paste;
        public delegate void WidgetProperty_Paste_Handler(object sender, EventArgs eventArgs);


        #region Standard events

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
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
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        #endregion

        private void numericUpDown_Width_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.X < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                if ((MouseСoordinates.X - numericUpDown_X.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.X - numericUpDown_X.Value;
                }
                else numericUpDown.Value = 1;
            }
        }

        private void numericUpDown_Height_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.Y < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                if ((MouseСoordinates.Y - numericUpDown_Y.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.Y - numericUpDown_Y.Value;
                }
                else numericUpDown.Value = 1;
            }
        }

        private void comboBox_Color_Click(object sender, EventArgs e)
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

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        public void SetColorText(Color color)
        {
            comboBox_Color.BackColor = color;
        }

        public Color GetColorText()
        {
            return comboBox_Color.BackColor;
        }

        public void SetText(string text)
        {
            textBox_unit_string.Text = text;
        }

        public string GetText()
        {
            return textBox_unit_string.Text;
        }

        #region Settings Set/Clear

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            comboBox_alignmentHorizontal.SelectedIndex = 0;
            comboBox_alignmentVertical.SelectedIndex = 0;
            comboBox_textStyle.SelectedIndex = 0;

            //comboBox_fonts.Items.Clear();
            //comboBox_fonts.Items.Add(Properties.Strings.SystemFont);
            comboBox_fonts.SelectedIndex = 0;
            textBox_unit_string.Text = "";

            numericUpDown_X.Enabled = true;
            comboBox_alignmentHorizontal.Enabled = true;
            numericUpDown_Y.Enabled = true;
            comboBox_alignmentVertical.Enabled = true;

            checkBox_unit.CheckState = CheckState.Unchecked;
            checkBox_addZero.Checked = false;
            checkBox_inEnd.Checked = false;

            UserFont = false;
            UnitMode = true;
            ZeroMode = true;
            Year = false;
            AmPm = false;
            DayMonthYear = false;
            UnitStrMode = false;

            setValue = false;
        }
        #endregion

        private void numericUpDown_Pos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_X" || numericUpDown.Name == "numericUpDown_Y"))
                    numericUpDown_X.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_X" || numericUpDown.Name == "numericUpDown_Y"))
                    numericUpDown_X.UpButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_size_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_Width")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_Width")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_Height")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_Height")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_Width" || numericUpDown.Name == "numericUpDown_Height"))
                    numericUpDown_Width.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_Width" || numericUpDown.Name == "numericUpDown_Height"))
                    numericUpDown_Width.UpButton();

                e.Handled = true;
            }
        }

        private void button_AddFont_Click(object sender, EventArgs e)
        {
            if (AddFont_Click != null && !setValue)
            {
                AddFont_Click(this, e);
            }
        }

        private void button_DelFont_Click(object sender, EventArgs e)
        {
            if (DelFont_Click != null)
            {
                string font = "";
                if (comboBox_fonts.SelectedIndex > 0)
                {
                    string font_fileName = comboBox_fonts.Text;
                    if (font_fileName.IndexOf(".ttf") > 0) font_fileName = font_fileName.Substring(0, font_fileName.IndexOf(".ttf") + ".ttf".Length);
                    font = font_fileName;
                }
                DelFont_Click(this, e, font);
            }
        }

        private void comboBox_fonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
            if(comboBox_fonts.SelectedIndex < 1) button_DelFont.Visible = false;
            else button_DelFont.Visible = true;
        }

        //public void SetScreenSize(int width, int height)
        //{
        //    ScreenSize.Width = width;
        //    ScreenSize.Height = height;
        //}

        private void checkBox_CentreHorizontally_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_X.Enabled = !checkBox_CentreHorizontally.Checked;
            comboBox_alignmentHorizontal.Enabled = !checkBox_CentreHorizontally.Checked;
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void checkBox_CentreVertically_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_Y.Enabled = !checkBox_CentreVertically.Checked;
            comboBox_alignmentVertical.Enabled = !checkBox_CentreVertically.Checked;
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void context_WidgetProperty_Opening(object sender, CancelEventArgs e)
        {
            if (WidgetProperty.ContainsKey("hmUI_widget_TEXT")) context_WidgetProperty.Items[1].Enabled = true;
            else context_WidgetProperty.Items[1].Enabled = false;
        }

        private void копироватьСвойстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WidgetProperty_Copy != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                WidgetProperty_Copy(this, eventArgs);
            }
        }

        private void вставитьСвойстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Focus();
            if (WidgetProperty_Paste != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                WidgetProperty_Paste(this, eventArgs);
            }
        }

        private void textBox_unit_string_TextChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

    }
}

//public class ScreenSize
//{
//    public static int Width = -1;
//    public static int Height = -1;
//}
