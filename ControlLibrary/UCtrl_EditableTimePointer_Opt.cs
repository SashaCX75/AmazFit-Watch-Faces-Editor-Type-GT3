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
    public partial class UCtrl_EditableTimePointer_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _EditableTimePointer;

        public UCtrl_EditableTimePointer_Opt()
        {
            InitializeComponent();
            setValue = false;
        }

        /// <summary>Задает индекс выбраного набора стрелок</summary>
        public void SetPointerSetIndex(int index)
        {
            setValue = true;
            comboBox_select_pointerSet.SelectedIndex = index;
            setValue = false;
        }

        /// <summary>Задает количество наборов стрелок в выпадающем списке</summary>
        public void SetPointerSetCount(int count)
        {
            setValue = true;
            comboBox_select_pointerSet.Items.Clear();
            for (int i = 1; i < count + 1; i++)
            {
                comboBox_select_pointerSet.Items.Add(i.ToString());
            }
            if (count >= 5) button_add.Enabled = false;
            else button_add.Enabled = true;
            setValue = false;
        }


        /// <summary>Задает название выбранной картинки для часов</summary>
        public void SetHourPointerImage(string value)
        {
            comboBox_hourPointer_image.Text = value;
            if (comboBox_hourPointer_image.SelectedIndex < 0) comboBox_hourPointer_image.Text = "";
        }
        /// <summary>Возвращает номер выбранной картинки для часов, в случае ошибки возвращает -1</summary>
        public string GetHourPointerImage()
        {
            if (comboBox_hourPointer_image.SelectedIndex < 0) return "";
            return comboBox_hourPointer_image.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка для часов</summary>
        public int GetSelectedIndexHourPointerImage()
        {
            return comboBox_hourPointer_image.SelectedIndex;
        }


        /// <summary>Задает название выбранной картинки для минут</summary>
        public void SetMinutePointerImage(string value)
        {
            comboBox_minutePointer_image.Text = value;
            if (comboBox_minutePointer_image.SelectedIndex < 0) comboBox_minutePointer_image.Text = "";
        }
        /// <summary>Возвращает номер выбранной картинки для минут, в случае ошибки возвращает -1</summary>
        public string GetMinutePointerImage()
        {
            if (comboBox_minutePointer_image.SelectedIndex < 0) return "";
            return comboBox_minutePointer_image.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка для минут</summary>
        public int GetSelectedIndexMinutePointerImage()
        {
            return comboBox_minutePointer_image.SelectedIndex;
        }


        /// <summary>Задает название выбранной картинки для секунд</summary>
        public void SetSecondPointerImage(string value)
        {
            comboBox_secondPointer_image.Text = value;
            if (comboBox_secondPointer_image.SelectedIndex < 0) comboBox_secondPointer_image.Text = "";
        }
        /// <summary>Возвращает номер выбранной картинки для секунд, в случае ошибки возвращает -1</summary>
        public string GetSecondPointerImage()
        {
            if (comboBox_secondPointer_image.SelectedIndex < 0) return "";
            return comboBox_secondPointer_image.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка для секунд</summary>
        public int GetSelectedIndexSecondPointerImage()
        {
            return comboBox_secondPointer_image.SelectedIndex;
        }


        /// <summary>Задает название выбранной картинки фона описания</summary>
        public void SetTip(string value)
        {
            comboBox_tip.Text = value;
            if (comboBox_tip.SelectedIndex < 0) comboBox_tip.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки фона описания</summary>
        public string GetTip()
        {
            if (comboBox_tip.SelectedIndex < 0) return "";
            return comboBox_tip.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка фона описания</summary>
        public int GetSelectedIndexTip()
        {
            return comboBox_tip.SelectedIndex;
        }


        /// <summary>Задает название выбранной рамки выделения</summary>
        public void SetForeground(string value)
        {
            comboBox_foreground.Text = value;
            if (comboBox_foreground.SelectedIndex < 0) comboBox_foreground.Text = "";
        }

        /// <summary>Возвращает название выбранной рамки выделения</summary>
        public string GetForeground()
        {
            if (comboBox_foreground.SelectedIndex < 0) return "";
            return comboBox_foreground.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка рамки выделения</summary>
        public int GetSelectedIndexForeground()
        {
            return comboBox_foreground.SelectedIndex;
        }


        /// <summary>Задает название выбранной картинки предпросмотра</summary>
        public void SetPreview(string value)
        {
            comboBox_Preview_image.Text = value;
            if (comboBox_Preview_image.SelectedIndex < 0) comboBox_Preview_image.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки предпросмотра</summary>
        public string GetPreview()
        {
            if (comboBox_Preview_image.SelectedIndex < 0) return "";
            return comboBox_Preview_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка предпросмотра</summary>
        public int GetSelectedIndexPreview()
        {
            return comboBox_Preview_image.SelectedIndex;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при удалении набора стрелок")]
        public event PointersDelHandler PointersDel;
        public delegate void PointersDelHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при добавлении набора стрелок")]
        public event PointersAddHandler PointersAdd;
        public delegate void PointersAddHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при изменении выбраного набора стрелок")]
        public event PointersIndexChangedHandler PointersIndexChanged;
        public delegate void PointersIndexChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при обновлении предпросмотра")]
        public event PreviewRefreshHandler PreviewRefresh;
        public delegate void PreviewRefreshHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при добавлении предпросмотра")]
        public event PreviewAddHandler PreviewAdd;
        public delegate void PreviewAddHandler(object sender, EventArgs eventArgs, int index);

        private void checkBox_Click(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
        }

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
                    ValueChanged(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
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
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Name == "comboBox_Preview_image")
            {
                if (comboBox_Preview_image.Text.Length > 0)
                {
                    button_PreviewAdd.Visible = false;
                    button_PreviewRefresh.Visible = true;
                }
                else
                {
                    button_PreviewAdd.Visible = true;
                    button_PreviewRefresh.Visible = false;
                }
            }
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
        }
        #endregion

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_hourPointer_image.Items.Clear();
            comboBox_minutePointer_image.Items.Clear();
            comboBox_secondPointer_image.Items.Clear();
            comboBox_Preview_image.Items.Clear();
            comboBox_tip.Items.Clear();
            comboBox_foreground.Items.Clear();

            comboBox_hourPointer_image.Items.AddRange(ListImages.ToArray());
            comboBox_minutePointer_image.Items.AddRange(ListImages.ToArray());
            comboBox_secondPointer_image.Items.AddRange(ListImages.ToArray());
            comboBox_Preview_image.Items.AddRange(ListImages.ToArray());
            comboBox_tip.Items.AddRange(ListImages.ToArray());
            comboBox_foreground.Items.AddRange(ListImages.ToArray());

            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_hourPointer_image.DropDownHeight = 1;
                comboBox_minutePointer_image.DropDownHeight = 1;
                comboBox_secondPointer_image.DropDownHeight = 1;
                comboBox_Preview_image.DropDownHeight = 1;
                comboBox_tip.DropDownHeight = 1;
                comboBox_foreground.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_hourPointer_image.DropDownHeight = 35 * count + 1;
                comboBox_minutePointer_image.DropDownHeight = 35 * count + 1;
                comboBox_secondPointer_image.DropDownHeight = 35 * count + 1;
                comboBox_Preview_image.DropDownHeight = 35 * count + 1;
                comboBox_tip.DropDownHeight = 35 * count + 1;
                comboBox_foreground.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_hourPointer_image.DropDownHeight = 106;
                comboBox_minutePointer_image.DropDownHeight = 106;
                comboBox_secondPointer_image.DropDownHeight = 106;
                comboBox_Preview_image.DropDownHeight = 106;
                comboBox_tip.DropDownHeight = 106;
                comboBox_foreground.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;
            comboBox_select_pointerSet.Items.Clear();
            button_del.Enabled = false;
            button_PreviewAdd.Enabled = false;
            button_PreviewRefresh.Enabled = false;

            comboBox_hourPointer_image.Text = null;
            comboBox_minutePointer_image.Text = null;
            comboBox_secondPointer_image.Text = null;
            comboBox_Preview_image.Text = null;
            comboBox_tip.Text = null;
            comboBox_foreground.Text = null;

            numericUpDown_tipX.Value = 0;
            numericUpDown_tipY.Value = 0;

            checkBox_secondInAOD.Checked = false;
            checkBox_edit_mode.Checked = false;

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
                ValueChanged(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
        }

        #endregion

        private void button_add_Click(object sender, EventArgs e)
        {
            if (PointersAdd != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                PointersAdd(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if (PointersDel != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                PointersDel(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
        }

        private void comboBox_select_pointerSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_select_pointerSet.SelectedIndex >= 0)
            {
                button_del.Enabled = true;
                button_PreviewAdd.Enabled = true;
                button_PreviewRefresh.Enabled = true;
            }
            else
            {
                button_del.Enabled = false;
                button_PreviewAdd.Enabled = false;
                button_PreviewRefresh.Enabled = false;
            }

            if (PointersIndexChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                PointersIndexChanged(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
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

        private void UCtrl_EditableTimePointer_Opt_Load(object sender, EventArgs e)
        {
            button_PreviewAdd.Location = button_PreviewRefresh.Location;
        }

        private void button_PreviewAdd_Click(object sender, EventArgs e)
        {
            if (PreviewAdd != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                PreviewAdd(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
        }

        private void button_PreviewRefresh_Click(object sender, EventArgs e)
        {
            if (PreviewRefresh != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                PreviewRefresh(this, eventArgs, comboBox_select_pointerSet.SelectedIndex);
            }
        }
    }
}
