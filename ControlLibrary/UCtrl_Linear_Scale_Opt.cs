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
    public partial class UCtrl_Linear_Scale_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _LinearScale;

        public UCtrl_Linear_Scale_Opt()
        {
            InitializeComponent();
            setValue = false;
            comboBox_scaleLinear_lineCap.SelectedIndex = 1;
        }

        private void comboBox_scaleLinear_color_Click(object sender, EventArgs e)
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

        public void SetImagePointer(string value)
        {
            comboBox_scaleLinear_image_pointer.Text = value;
            if (comboBox_scaleLinear_image_pointer.SelectedIndex < 0) comboBox_scaleLinear_image_pointer.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetImagePointer()
        {
            if (comboBox_scaleLinear_image_pointer.SelectedIndex < 0) return "";
            return comboBox_scaleLinear_image_pointer.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int comboBoxGetSelectedIndexImagePointer()
        {
            return comboBox_scaleLinear_image_pointer.SelectedIndex;
        }

        public void SetColorScale(Color color)
        {
            comboBox_scaleLinear_color.BackColor = color;
        }

        public Color GetColorScale()
        {
            return comboBox_scaleLinear_color.BackColor;
        }

        /// <summary>Устанавливает тип окончания линии "Rounded", "Flat"</summary>
        public void SetLineCap(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "Rounded":
                    result = 0;
                    break;
                case "Flat":
                    result = 1;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_scaleLinear_lineCap.SelectedIndex = result;
        }

        /// <summary>Возвращает тип окончания линии строкой "Rounded", "Flat"</summary>
        public string GetLineCap()
        {
            string result;
            switch (comboBox_scaleLinear_lineCap.SelectedIndex)
            {
                case 0:
                    result = "Rounded";
                    break;
                case 1:
                    result = "Flat";
                    break;

                default:
                    result = "Rounded";
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexLineCap()
        {
            return comboBox_scaleLinear_lineCap.SelectedIndex;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        #region Standard events
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
            //if (comboBox.Items.Count < 10) comboBox.DropDownHeight = comboBox.Items.Count * 35;
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
            //e.Graphics.DrawImage(imageList1.Images[e.Index], rectangle);
            myFont = new Font(family, size);
            StringFormat lineAlignment = new StringFormat();
            //lineAlignment.Alignment = StringAlignment.Center;
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

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_scaleLinear_image_pointer.Items.Clear();

            comboBox_scaleLinear_image_pointer.Items.AddRange(ListImages.ToArray());

            ListImagesFullName = _ListImagesFullName;
            comboBox_scaleLinear_lineCap.SelectedIndex = 1;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_scaleLinear_image_pointer.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_scaleLinear_image_pointer.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_scaleLinear_image_pointer.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            comboBox_scaleLinear_image_pointer.Text = null;

            numericUpDown_scaleLinearX.Value = 0;
            numericUpDown_scaleLinearY.Value = 0;

            numericUpDown_scaleLinear_length.Value = 100;
            numericUpDown_scaleLinear_width.Value = 5;

            radioButton_horizontal.Checked = true;
            //radioButton_vertical.Checked = false;

            checkBox_mirror.Checked = false;
            checkBox_inversion.Checked = false;

            setValue = false;
        }
        #endregion

        private void numericUpDown_scaleLinear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_scaleLinearX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_scaleLinearY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_scaleLinearX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_scaleLinearY.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_scaleLinearY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_scaleLinearY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_scaleLinearY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_scaleLinearY.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_scaleLinearX" || numericUpDown.Name == "numericUpDown_scaleLinearY"))
                    numericUpDown_scaleLinearX.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_scaleLinearX" || numericUpDown.Name == "numericUpDown_scaleLinearY"))
                    numericUpDown_scaleLinearX.UpButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_scaleLinear_length_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (radioButton_horizontal.Checked)
            {
                if (MouseСoordinates.X < 0) return;
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.X <= numericUpDown.Controls[1].Width + 1)
                {
                    // Click is in text area
                    numericUpDown.Value = MouseСoordinates.X - numericUpDown_scaleLinearX.Value;
                } 
            }
            else
            {
                if (MouseСoordinates.Y < 0) return;
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.X <= numericUpDown.Controls[1].Width + 1)
                {
                    // Click is in text area
                    numericUpDown.Value = MouseСoordinates.Y - numericUpDown_scaleLinearY.Value;
                }
            }
        }

        private void checkBox_mirror_Click(object sender, EventArgs e)
        {
            //comboBox_scaleLinear_lineCap.Enabled = !checkBox_mirror.Checked;
            //label2.Enabled = !checkBox_mirror.Checked;
        }
    }
}
