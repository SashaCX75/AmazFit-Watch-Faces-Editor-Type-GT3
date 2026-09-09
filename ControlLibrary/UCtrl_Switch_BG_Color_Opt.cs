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
    public partial class UCtrl_Switch_BG_Color_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        private List<Color> ListBackgroundColors = new List<Color>(); // перечень цветов
        private List<string> ListToast = new List<string>(); // перечень уведомлений
        private int[] CustomColors = { }; // пользовательские цвета

        public UCtrl_Switch_BG_Color_Opt()
        {
            InitializeComponent();
            setValue = false;
        }

        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            //if (groupBox.Enabled) DrawGroupBox(groupBox, e.Graphics, Color.Black, Color.DarkGray);
            //else DrawGroupBox(groupBox, e.Graphics, Color.DarkGray, Color.DarkGray);
            if (groupBox.Enabled) DrawGroupBox(groupBox, e.Graphics, this.ForeColor, Color.DarkGray);
            else DrawGroupBox(groupBox, e.Graphics, SystemColors.GrayText, Color.DarkGray);
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
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Добавление цвета")]
        public event AddColorHandler AddColor;
        public delegate void AddColorHandler(List<Color> BG_Colors, List<string> Toast, int rowIndex);

        [Browsable(true)]
        [Description("Удаление цвета")]
        public event DelColorHandler DelColor;
        public delegate void DelColorHandler(List<Color> BG_Colors, List<string> Toast, int rowIndex);

        [Browsable(true)]
        [Description("Изменить цвет")]
        public event СhangeColorHandler СhangeColor;
        public delegate void СhangeColorHandler(List<Color> BG_Colors, List<string> Toast, int rowIndex);

        [Browsable(true)]
        [Description("Выбрали другой цвет")]
        public event SelectColorHandler SelectColor;
        public delegate void SelectColorHandler(int rowIndex);

        [Browsable(true)]
        [Description("Редактирование уведомления")]
        public event EditToastHandler EditToast;
        public delegate void EditToastHandler(string ToastText, int rowIndex);

        [Browsable(true)]
        [Description("Происходит при изменении пользовательских цветов")]
        public event CustomColorsChangedHandler CustomColorsChanged;
        public delegate void CustomColorsChangedHandler(int[] customColors);

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
            textBox_text.Text = text;
        }

        public string GetText()
        {
            return textBox_text.Text;
        }

        #region Standard events

        private void textBox_text_TextChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                ComboBox comboBox = sender as ComboBox;
                comboBox.Text = "";
                comboBox.SelectedIndex = -1;

                if (ValueChanged != null && !setValue)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs);
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
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }

            if (comboBox_normal_image.Text.Length > 0 && comboBox_press_image.Text.Length > 0) groupBox_color.Enabled = false;
            else groupBox_color.Enabled = true;

            //ComboBox comboBox = sender as ComboBox;
            //if (comboBox.Name == "comboBox_bg_image")
            //{
            //    button_add.Enabled = comboBox.SelectedIndex >= 0;
            //}
        }

        private void comboBox_color_Click(object sender, EventArgs e)
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
            if (CustomColors != colorDialog.CustomColors)
            {
                CustomColors = colorDialog.CustomColors;

                if (CustomColorsChanged != null && !setValue)
                {
                    CustomColorsChanged(CustomColors);
                }
            }

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        #endregion

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_normal_image.Items.Clear();
            comboBox_press_image.Items.Clear();

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

        /// <summary>Добавляет перечень фоновых цветов</summary>
        public void AddBackgroundColors(List<Color> listColors, List<string> listToast, int select_index)
        {
            setValue = true;
            ListBackgroundColors = listColors;
            ListToast = listToast;
            dataGridView_colors.Rows.Clear();
            if (listColors != null)
            {
                for (int i = 0; i < listColors.Count; i++)
                {
                    DataGridViewRow RowNew = new DataGridViewRow();
                    RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = (i + 1).ToString() });
                    DataGridViewTextBoxCell colorCell = new DataGridViewTextBoxCell();
                    colorCell.Style.BackColor = listColors[i];
                    colorCell.Style.SelectionBackColor = listColors[i];
                    RowNew.Cells.Add(colorCell);
                    //RowNew.Cells.Add(new DataGridViewTextBoxCell() { Style.BackColor = listColors[i] });
                    if (listToast != null && i < listToast.Count) RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = listToast[i] });
                    dataGridView_colors.Rows.Add(RowNew);
                }
                if (select_index < dataGridView_colors.Rows.Count)
                {
                    dataGridView_colors.Rows[select_index].Selected = true;
                }
                else select_index = 0;
                button_del.Enabled = select_index > 0;
                if (listColors.Count > 25) button_add.Enabled = false;
            }
            setValue = false;
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear(int[] customColors)
        {
            setValue = true;
            CustomColors = customColors;

            comboBox_normal_image.Text = null;
            comboBox_press_image.Text = null;

            numericUpDown_buttonX.Value = 0;
            numericUpDown_buttonY.Value = 0;
            numericUpDown_width.Value = 100;
            numericUpDown_height.Value = 40;
            numericUpDown_radius.Value = 12;
            numericUpDown_textSize.Value = 25;

            //dataGridView_images.Rows.Clear();
            button_add.Enabled = true;
            button_del.Enabled = false;

            checkBox_use_crown.Checked = false;
            checkBox_use_in_AOD.Checked = false;

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
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void numericUpDown_width_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void numericUpDown_height_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            try
            {
                int selectedRowCount = dataGridView_colors.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_colors.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;
                }
                rowIndex++;
                if (rowIndex < 1 || rowIndex >= ListBackgroundColors.Count)
                {
                    ListBackgroundColors.Add(comboBox_bg_color.BackColor);
                    ListToast.Add("");
                    rowIndex = ListBackgroundColors.Count - 1;
                }
                else
                {
                    ListBackgroundColors.Insert(rowIndex, comboBox_bg_color.BackColor);
                    ListToast.Insert(rowIndex, "");
                }
            }
            catch (Exception)
            {
            }

            if (AddColor != null && !setValue)
            {
                AddColor(ListBackgroundColors, ListToast, rowIndex);
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_colors.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_colors.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;

                    if (DelColor != null && !setValue)
                    {
                        if (rowIndex >= ListBackgroundColors.Count) rowIndex = ListBackgroundColors.Count - 1;
                        ListBackgroundColors.RemoveAt(rowIndex);
                        ListToast.RemoveAt(rowIndex);
                        if (rowIndex >= ListBackgroundColors.Count) rowIndex = ListBackgroundColors.Count - 1;
                        DelColor(ListBackgroundColors, ListToast, rowIndex);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView_colors_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            button_del.Enabled = rowIndex > 0;

            if (SelectColor != null && !setValue)
            {
                SelectColor(rowIndex);
            }
        }

        private void dataGridView_colors_KeyDown(object sender, KeyEventArgs e)
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

                            if (rowIndex >= 0)
                            {
                                if (rowIndex >= ListBackgroundColors.Count) rowIndex = ListBackgroundColors.Count - 1;
                                ListBackgroundColors.RemoveAt(rowIndex);
                                ListToast.RemoveAt(rowIndex);
                                if (rowIndex >= ListBackgroundColors.Count) rowIndex = ListBackgroundColors.Count - 1;
                                if (DelColor != null && !setValue)
                                {
                                    DelColor(ListBackgroundColors, ListToast, rowIndex);
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void dataGridView_colors_MouseDoubleClick(object sender, MouseEventArgs e)
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
                string toastText = "";
                if (rowIndex < ListToast.Count) toastText = ListToast[rowIndex];
                EditToast f = new EditToast(toastText);
                f.ShowDialog();
                bool dialogResult = f.UpdateText;
                if (dialogResult)
                {
                    string toastNewText = f.ToastText;

                    if (EditToast != null && !setValue)
                    {
                        EditToast(toastNewText, rowIndex);
                    }
                }
            }
        }

        private void dataGridView_colors_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView dataGridView = sender as DataGridView;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    dataGridView.CurrentCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
        }

        private void изменитьФонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowIndex = -1;
            try
            {
                int selectedRowCount = dataGridView_colors.SelectedCells.Count;
                if (selectedRowCount > 0)
                {
                    DataGridViewSelectedCellCollection selectedCells = dataGridView_colors.SelectedCells;
                    rowIndex = selectedCells[0].RowIndex;

                    if (СhangeColor != null && !setValue && rowIndex < ListBackgroundColors.Count)
                    {
                        ListBackgroundColors[rowIndex] = comboBox_bg_color.BackColor;
                        СhangeColor(ListBackgroundColors, ListToast, rowIndex);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
