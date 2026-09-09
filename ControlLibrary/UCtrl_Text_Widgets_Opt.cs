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

namespace ControlLibrary
{
    public partial class UCtrl_Text_Widgets_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        public Object _TextWidgets; // Общий виджет
        //public Object _Text;  // конкретный выбранный виджет
        private int[] CustomColors = { }; // пользовательские цвета

        private bool Font_mode;
        public Dictionary<string, Object> WidgetProperty = new Dictionary<string, Object>();

        public UCtrl_Text_Widgets_Opt()
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

            dataGridView_buttons.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void AddFonts(Dictionary<string, string> fontsList)
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

        public void SetTextStr(string text)
        {
            textBox_TextStr.Text = text;
        }

        public string GetTextStr()
        {
            return textBox_TextStr.Text;
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

        /// <summary>Возвращает имя файла выбраного шрифта</summary>
        public string GetFont()
        {
            string font = "";
            if (comboBox_fonts.SelectedIndex > 0)
            {
                string font_fileName = comboBox_fonts.Text;
                if (font_fileName.IndexOf(".ttf") > 0) font_fileName = font_fileName.Substring(0, font_fileName.IndexOf(".ttf") + ".ttf".Length);
                else if (font_fileName.IndexOf(".TTF") > 0) font_fileName = font_fileName.Substring(0, font_fileName.IndexOf(".TTF") + ".TTF".Length);
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

        [Browsable(true)]
        [Description("Происходит при изменении выбранного текста")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs, int rowIndex);

        [Browsable(true)]
        [Description("Добавление виджета текста")]
        public event AddTextHandler AddText;
        public delegate void AddTextHandler(int rowIndex);

        [Browsable(true)]
        [Description("Удаление виджета текста")]
        public event DelTextHandler DelText;
        public delegate void DelTextHandler(int rowIndex);

        [Browsable(true)]
        [Description("Выбрали другой виджета текста")]
        public event SelectTextWidgetHandler SelectTextWidget;
        public delegate void SelectTextWidgetHandler(int rowIndex);

        [Browsable(true)]
        [Description("Происходит при изменении надписи в виджете текста")]
        public event TextChangedHandler TextStrChanged;
        public delegate void TextChangedHandler(string textStr, int rowIndex);

        [Browsable(true)]
        [Description("Происходит при изменении видимости выбранного виджета текста")]
        public event VisibleTextChangedHandler VisibleTextChanged;
        public delegate void VisibleTextChangedHandler(int rowIndex, bool visible);

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
        public delegate void WidgetProperty_Paste_Handler(object sender, EventArgs eventArgs, int rowIndex);

        [Browsable(true)]
        [Description("Происходит при изменении пользовательских цветов")]
        public event CustomColorsChangedHandler CustomColorsChanged;
        public delegate void CustomColorsChangedHandler(int[] customColors);

        public void UpdateTextList(List<String> textsList, List<bool> widgetsVisibleList, int rowIndex = 0)
        {
            setValue = true;

            dataGridView_buttons.Rows.Clear();

            for (int index = 0; index < textsList.Count; index++)
            {
                string text = textsList[index];
                DataGridViewRow RowNew = new DataGridViewRow();
                RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = (index + 1).ToString() });
                RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = text });
                RowNew.Cells.Add(new DataGridViewCheckBoxCell() { Value = widgetsVisibleList[index] });
                RowNew.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                RowNew.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                RowNew.Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_buttons.Rows.Add(RowNew);
            }

            if (textsList.Count > 0) button_del.Enabled = true;
            else button_del.Enabled = false;
            if (textsList.Count >= 30) button_add.Enabled = false;
            else button_add.Enabled = true;

            if (rowIndex >= 0 && rowIndex < dataGridView_buttons.Rows.Count)
            {
                dataGridView_buttons.Rows[rowIndex].Selected = true;
                dataGridView_buttons.CurrentCell = dataGridView_buttons.Rows[rowIndex].Cells[0];
                SelectTextWidget(rowIndex);
            }
            setValue = false;
        }

        #region Standard events

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

            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (numericUpDown.Name == "numericUpDown_start_angle" || numericUpDown.Name == "numericUpDown_end_angle")
            {
                if (Math.Abs(numericUpDown_end_angle.Value - numericUpDown_start_angle.Value) > 360)
                {
                    toolTip_Hint360.ToolTipTitle = Properties.Strings.Hint_360_Title;
                    toolTip_Hint360.Show(Properties.Strings.Hint_360_Text_Circle, numericUpDown, numericUpDown.Width, 0, 2000);
                }

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
            ColorDialog colorDialog = new ColorDialog();
            ComboBox comboBox_color = sender as ComboBox;
            colorDialog.Color = comboBox_color.BackColor;
            colorDialog.FullOpen = true;

            colorDialog.CustomColors = CustomColors;


            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета формы
            comboBox_color.BackColor = colorDialog.Color;
            LastColor.last_color = colorDialog.Color;
            if (CustomColors != colorDialog.CustomColors)
            {
                CustomColors = colorDialog.CustomColors;

                if (CustomColorsChanged != null && !setValue)
                {
                    CustomColorsChanged(CustomColors);
                }
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

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void SetColorText(Color color)
        {
            comboBox_Color.BackColor = color;
        }

        public Color GetColorText()
        {
            return comboBox_Color.BackColor;
        }

        public void SetMode(int mode)
        {
            if (mode == 1) radioButton_counterclockwise.Checked = true;
            else radioButton_clockwise.Checked = true;
        }

        /// <summary>Направление вращения для тексты по окружности</summary>
        public int GetMode()
        {
            if (radioButton_counterclockwise.Checked) return 1;
            else return 0;
        }

        #region Settings Set/Clear

        /// <summary>Сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear(int[] customColors)
        {
            setValue = true;
            CustomColors = customColors;

            comboBox_alignmentHorizontal.SelectedIndex = 0;
            comboBox_alignmentVertical.SelectedIndex = 0;
            comboBox_textStyle.SelectedIndex = 0;

            comboBox_fonts.SelectedIndex = 0;

            numericUpDown_X.Enabled = true;
            comboBox_alignmentHorizontal.Enabled = true;
            checkBox_CentreHorizontally.Checked = false;
            numericUpDown_Y.Enabled = true;
            comboBox_alignmentVertical.Enabled = true;
            checkBox_CentreVertically.Checked = false;

            UserFont = false;

            checkBox_use_text_circle.Checked = false;
            radioButton_clockwise.Checked = true;

            dataGridView_buttons.Rows.Clear();
            button_add.Enabled = true;
            button_del.Enabled = false;

            setValue = false;
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

            if (AddText != null && !setValue)
            {
                AddText(rowIndex);
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

                    if (DelText != null && !setValue)
                    {
                        DelText(rowIndex);
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

            if (SelectTextWidget != null && !setValue)
            {
                SelectTextWidget(rowIndex);
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

                            if (DelText != null && !setValue)
                            {
                                DelText(rowIndex);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void dataGridView_buttons_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;
            senderGrid.EndEdit();
            int rowIndex = e.RowIndex;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {

                DataGridViewCheckBoxCell cbxCell = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                bool value = !(bool)cbxCell.Value;
                cbxCell.Value = value;

                if (VisibleTextChanged != null && !setValue && rowIndex >= 0) VisibleTextChanged(rowIndex, value);
            }
        }

        ///

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
            if (comboBox_fonts.SelectedIndex < 1) button_DelFont.Visible = false;
            else button_DelFont.Visible = true;
        }

        private void checkBox_CentreHorizontally_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_X.Enabled = !checkBox_CentreHorizontally.Checked;
            comboBox_alignmentHorizontal.Enabled = !checkBox_CentreHorizontally.Checked;
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

        private void checkBox_CentreVertically_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_Y.Enabled = !checkBox_CentreVertically.Checked;
            comboBox_alignmentVertical.Enabled = !checkBox_CentreVertically.Checked;
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
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_buttons.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_buttons.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;
                }

                if (WidgetProperty_Paste != null && !setValue)
                {
                    EventArgs eventArgs = new EventArgs();
                    WidgetProperty_Paste(this, eventArgs, rowIndex);
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBox_unit_string_TextChanged(object sender, EventArgs e)
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

        private void numericUpDown_Size_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_Size.Value > 150)
            {
                string text = toolTip.GetToolTip(numericUpDown_Size);
                Point p = new Point(MouseСoordinates.X, MouseСoordinates.Y);
                toolTip.Show(text, numericUpDown_Size, p, 1500);
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

        private void checkBox_use_text_circle_CheckedChanged(object sender, EventArgs e)
        {
            bool use_text_circle = checkBox_use_text_circle.Checked;

            label9.Enabled = use_text_circle;
            label10.Enabled = use_text_circle;
            label11.Enabled = use_text_circle;
            numericUpDown_start_angle.Enabled = use_text_circle;
            numericUpDown_end_angle.Enabled = use_text_circle;
            numericUpDown_radius.Enabled = use_text_circle;
            radioButton_clockwise.Enabled = use_text_circle;
            radioButton_counterclockwise.Enabled = use_text_circle;

            label07.Enabled = !use_text_circle;
            label08.Enabled = !use_text_circle;
            label1.Enabled = !use_text_circle;
            label3.Enabled = !use_text_circle;
            label4.Enabled = !use_text_circle;
            numericUpDown_Width.Enabled = !use_text_circle;
            numericUpDown_Height.Enabled = !use_text_circle;
            comboBox_alignmentVertical.Enabled = !use_text_circle;
            numericUpDown_LineSpace.Enabled = !use_text_circle;
            comboBox_textStyle.Enabled = !use_text_circle;

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

        private void radioButton_clockwise_CheckedChanged(object sender, EventArgs e)
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

        private void textBox_TextStr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
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
                    if (TextStrChanged != null && !setValue && rowIndex >= 0)
                    {
                        EventArgs eventArgs = new EventArgs();
                        TextStrChanged(textBox_TextStr.Text, rowIndex);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
