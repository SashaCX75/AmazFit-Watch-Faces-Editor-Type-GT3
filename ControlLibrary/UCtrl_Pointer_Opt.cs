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
    public partial class UCtrl_Pointer_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private bool showBackground;

        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _ElementWithPointer;

        private bool Time_mode = false;
        private int posUp;
        private int posDown;

        private int offsetUp;
        private int offsetDown;

        public UCtrl_Pointer_Opt()
        {
            InitializeComponent();
            setValue = false;

            posUp = numericUpDown_pointer_startAngle.Location.Y;
            posDown = comboBox_pointer_imageCentr.Location.Y;

            offsetUp = label08.Location.Y - posDown;
            offsetDown = label12.Location.Y - posDown;
        }

        /// <summary>Отображение начального и конечного угла </summary>
        [Description("Отображение начального и конечного угла")]
        public virtual bool TimeMode
        {
            get
            {
                return Time_mode;
            }
            set
            {
                Time_mode = value;
                numericUpDown_pointer_startAngle.Visible = !Time_mode;
                numericUpDown_pointer_endAngle.Visible = !Time_mode;

                label10.Visible = !Time_mode;
                label11.Visible = !Time_mode;

                if (Time_mode)
                {
                    comboBox_pointer_imageCentr.Location = new Point(comboBox_pointer_imageCentr.Location.X, posUp);
                    numericUpDown_pointer_centr_X.Location = new Point(numericUpDown_pointer_centr_X.Location.X, posUp);
                    numericUpDown_pointer_centr_Y.Location = new Point(numericUpDown_pointer_centr_Y.Location.X, posUp);

                    label08.Location = new Point(label08.Location.X, posUp + offsetUp);
                    label09.Location = new Point(label09.Location.X, posUp + offsetUp);
                    label12.Location = new Point(label12.Location.X, posUp + offsetDown);
                    label13.Location = new Point(label13.Location.X, posUp + offsetDown);
                }
                else
                {
                    comboBox_pointer_imageCentr.Location = new Point(comboBox_pointer_imageCentr.Location.X, posDown);
                    numericUpDown_pointer_centr_X.Location = new Point(numericUpDown_pointer_centr_X.Location.X, posDown);
                    numericUpDown_pointer_centr_Y.Location = new Point(numericUpDown_pointer_centr_Y.Location.X, posDown);

                    label08.Location = new Point(label08.Location.X, posDown + offsetUp);
                    label09.Location = new Point(label09.Location.X, posDown + offsetUp);
                    label12.Location = new Point(label12.Location.X, posDown + offsetDown);
                    label13.Location = new Point(label13.Location.X, posDown + offsetDown);
                }
            }
        }


        /// <summary>Задает название выбранной картинки</summary>
        public void SetPointerImage(string value)
        {
            comboBox_pointer_image.Text = value;
            if (comboBox_pointer_image.SelectedIndex < 0) comboBox_pointer_image.Text = "";
        }
        /// <summary>Возвращает номер выбранной картинки, в случае ошибки возвращает -1</summary>
        public string GetPointerImage()
        {
            if (comboBox_pointer_image.SelectedIndex < 0) return "";
            return comboBox_pointer_image.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexPointerImage()
        {
            return comboBox_pointer_image.SelectedIndex;
        }

        public void SetPointerImageCentr(string value)
        {
            comboBox_pointer_imageCentr.Text = value;
            if (comboBox_pointer_imageCentr.SelectedIndex < 0) comboBox_pointer_imageCentr.Text = "";
        }
        /// <summary>Возвращает номер выбранной картинки, в случае ошибки возвращает -1</summary>
        public string GetPointerImageCentr()
        {
            if (comboBox_pointer_imageCentr.SelectedIndex < 0) return "";
            return comboBox_pointer_imageCentr.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexPointerImageCentr()
        {
            return comboBox_pointer_imageCentr.SelectedIndex;
        }

        public void SetPointerImageBackground(string value)
        {
            comboBox_pointer_imageBackground.Text = value;
            if (comboBox_pointer_imageBackground.SelectedIndex < 0) comboBox_pointer_imageBackground.Text = "";
        }

        /// <summary>Возвращает номер выбранной картинки, в случае ошибки возвращает -1</summary>
        public string GetPointerImageBackground()
        {
            if (comboBox_pointer_imageBackground.SelectedIndex < 0) return "";
            return comboBox_pointer_imageBackground.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexPointerImageBackground()
        {
            return comboBox_pointer_imageBackground.SelectedIndex;
        }

        /// <summary>Отображение поля изображения при ошибке</summary>
        [Description("Отображение поля настройки фонового изображения")]
        public virtual bool ShowBackground
        {
            get
            {
                return showBackground;
            }
            set
            {
                showBackground = value;
                comboBox_pointer_imageBackground.Visible = showBackground;
                numericUpDown_pointer_background_X.Visible = showBackground;
                numericUpDown_pointer_background_Y.Visible = showBackground;

                label14.Visible = showBackground;
                label15.Visible = showBackground;
                label16.Visible = showBackground;
                label17.Visible = showBackground;
            }
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
        #endregion

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_pointer_image.Items.Clear();
            comboBox_pointer_imageCentr.Items.Clear();
            comboBox_pointer_imageBackground.Items.Clear();

            comboBox_pointer_image.Items.AddRange(ListImages.ToArray());
            comboBox_pointer_imageCentr.Items.AddRange(ListImages.ToArray());
            comboBox_pointer_imageBackground.Items.AddRange(ListImages.ToArray());

            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_pointer_image.DropDownHeight = 1;
                comboBox_pointer_imageCentr.DropDownHeight = 1;
                comboBox_pointer_imageBackground.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_pointer_image.DropDownHeight = 35 * count + 1;
                comboBox_pointer_imageCentr.DropDownHeight = 35 * count + 1;
                comboBox_pointer_imageBackground.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_pointer_image.DropDownHeight = 106;
                comboBox_pointer_imageCentr.DropDownHeight = 106;
                comboBox_pointer_imageBackground.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            comboBox_pointer_image.Text = null;
            comboBox_pointer_imageCentr.Text = null;
            comboBox_pointer_imageBackground.Text = null;
            this.TimeMode = false;

            numericUpDown_pointer_X.Value = 0;
            numericUpDown_pointer_Y.Value = 0;

            numericUpDown_pointer_centr_X.Value = 0;
            numericUpDown_pointer_centr_Y.Value = 0;

            numericUpDown_pointer_background_X.Value = 0;
            numericUpDown_pointer_background_Y.Value = 0;

            numericUpDown_pointer_offset_X.Value = 0;
            numericUpDown_pointer_offset_Y.Value = 0;

            numericUpDown_pointer_startAngle.Value = 0;
            numericUpDown_pointer_endAngle.Value = 360;

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

        private void numericUpDown_pointer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_Y.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_Y.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_pointer_X" || numericUpDown.Name == "numericUpDown_pointer_Y"))
                    numericUpDown_pointer_X.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_pointer_X" || numericUpDown.Name == "numericUpDown_pointer_Y"))
                    numericUpDown_pointer_X.UpButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_pointer_offset_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_offset_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_offset_Y.UpButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_offset_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_offset_Y.DownButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_offset_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_offset_Y.UpButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_offset_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_offset_Y.DownButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_pointer_offset_X" || numericUpDown.Name == "numericUpDown_pointer_offset_Y"))
                    numericUpDown_pointer_offset_X.UpButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_pointer_offset_X" || numericUpDown.Name == "numericUpDown_pointer_offset_Y"))
                    numericUpDown_pointer_offset_X.DownButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_pointer_centr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_centr_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_centr_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_centr_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_centr_Y.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_centr_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_centr_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_centr_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_centr_Y.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_pointer_centr_X" || numericUpDown.Name == "numericUpDown_pointer_centr_Y"))
                    numericUpDown_pointer_centr_X.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_pointer_centr_X" || numericUpDown.Name == "numericUpDown_pointer_centr_Y"))
                    numericUpDown_pointer_centr_X.UpButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_pointer_background_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_background_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_background_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_background_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_background_Y.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_pointer_background_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_background_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_pointer_background_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_pointer_background_Y.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_pointer_background_X" || numericUpDown.Name == "numericUpDown_pointer_background_Y"))
                    numericUpDown_pointer_background_X.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_pointer_background_X" || numericUpDown.Name == "numericUpDown_pointer_background_Y"))
                    numericUpDown_pointer_background_X.UpButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_pointer_offset_X_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.X < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.X - numericUpDown_pointer_X.Value;
            }
        }

        private void numericUpDown_pointer_offset_Y_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.Y < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.Y - numericUpDown_pointer_Y.Value;
            }
        }
    }
}
