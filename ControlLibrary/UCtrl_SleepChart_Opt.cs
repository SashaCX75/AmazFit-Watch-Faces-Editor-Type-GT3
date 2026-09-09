using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_SleepChart_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _ChartSettings;
        private int[] CustomColors = { }; // пользовательские цвета

        public UCtrl_SleepChart_Opt()
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

        public void SetDeepSleepColour(Color color)
        {
            comboBox_deepSleep_colour.BackColor = color;
        }
        public Color GetDeepSleepColour()
        {
            return comboBox_deepSleep_colour.BackColor;
        }

        public void SetLightSleepColour(Color color)
        {
            comboBox_lightSleep_colour.BackColor = color;
        }
        public Color GetLightSleepColour()
        {
            return comboBox_lightSleep_colour.BackColor;
        }

        public void SetRemColour(Color color)
        {
            comboBox_REM_colour.BackColor = color;
        }
        public Color GetRemColour()
        {
            return comboBox_REM_colour.BackColor;
        }

        public void SetWakeupColour(Color color)
        {
            comboBox_wakeup_colour.BackColor = color;
        }
        public Color GetWakeupColour()
        {
            return comboBox_wakeup_colour.BackColor;
        }

        public void SetHRColour(Color color)
        {
            comboBox_hr_colour.BackColor = color;
        }
        public Color GetHRColour()
        {
            return comboBox_hr_colour.BackColor;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении пользовательских цветов")]
        public event CustomColorsChangedHandler CustomColorsChanged;
        public delegate void CustomColorsChangedHandler(int[] customColors);

        public void SetBackground(string value)
        {
            comboBox_image.Text = value;
            if (comboBox_image.SelectedIndex < 0) comboBox_image.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetBackground()
        {
            if (comboBox_image.SelectedIndex < 0) return "";
            return comboBox_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int comboBoxGetSelectedIndexBackground()
        {
            return comboBox_image.SelectedIndex;
        }

        #region Standard events

        private void comboBox_color_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            ComboBox comboBox_color = sender as ComboBox;
            colorDialog.Color = comboBox_color.BackColor;
            colorDialog.FullOpen = true;

            colorDialog.CustomColors = CustomColors;


            if (colorDialog.ShowDialog() == DialogResult.Cancel) return;

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

            if (ValueChanged != null)
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
        }

        #endregion

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_image.Items.Clear();

            comboBox_image.Items.AddRange(ListImages.ToArray());
            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_image.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_image.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_image.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            comboBox_image.Text = null;

            numericUpDown_posX.Value = 0;
            numericUpDown_posY.Value = 0;

            numericUpDown_height.Value = 7;
            numericUpDown_width.Value = 50;

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

        #endregion

        private void numericUpDown_pos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_posX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_posY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_posX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_posY.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_posY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_posY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_posY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_posY.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_posX" || numericUpDown.Name == "numericUpDown_posY"))
                    numericUpDown_posX.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_posX" || numericUpDown.Name == "numericUpDown_posY"))
                    numericUpDown_posX.UpButton();

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

        private void checkBox_graph_fullScreen_CheckedChanged(object sender, EventArgs e)
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
                if ((MouseСoordinates.X - numericUpDown_posX.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.X - numericUpDown_posX.Value;
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
                if ((MouseСoordinates.Y - numericUpDown_posY.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.Y - numericUpDown_posY.Value;
                }
                else numericUpDown.Value = 1;
            }
        }

        private void checkBox_use_chartSleep_CheckedChanged(object sender, EventArgs e)
        {
            bool cheked = checkBox_use_chartSleep.Checked;
            comboBox_deepSleep_colour.Enabled = cheked;
            comboBox_lightSleep_colour.Enabled = cheked;
            comboBox_REM_colour.Enabled = cheked;
            comboBox_wakeup_colour.Enabled = cheked;
            numericUpDown_radius.Enabled = cheked;
            //checkBox_graph_fullScreen.Enabled = cheked;

            label16.Enabled = cheked;
            label5.Enabled = cheked;
            label4.Enabled = cheked;
            label3.Enabled = cheked;
            label2.Enabled = cheked;

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void checkBox_use_chartHR_CheckedChanged(object sender, EventArgs e)
        {
            bool cheked = checkBox_use_chartHR.Checked;
            comboBox_hr_colour.Enabled = cheked;
            numericUpDown_hr_lineWidth.Enabled = cheked;

            label6.Enabled = cheked;
            label7.Enabled = cheked;

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }
    }
}
