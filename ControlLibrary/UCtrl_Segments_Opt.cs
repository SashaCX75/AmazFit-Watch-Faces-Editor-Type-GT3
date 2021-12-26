using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_Segments_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private int imagesCount = -1; // предварительно заданное количество изображений
        private bool fixedRowsCount = false;

        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _ElementWithSegments;

        /// <summary>Задает максимальное количество координат в наборе</summary>
        [Description("Задает максимальное количество координат в наборе")]
        public int ImagesCount
        {
            get
            {
                return imagesCount;
            }
            set
            {
                imagesCount = value;
            }
        }

        [Description("true - если количество строк фиксированное")]
        public bool FixedRowsCount
        {
            get
            {
                return fixedRowsCount;
            }
            set
            {
                fixedRowsCount = value;
            }
        }

        public UCtrl_Segments_Opt()
        {
            InitializeComponent();
            setValue = false;

            //dataGridView_coordinates_set.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkGray;
            //dataGridView_coordinates_set.DefaultCellStyle.ForeColor = Color.DarkGray;
            //dataGridView_coordinates_set.DefaultCellStyle.SelectionForeColor = Color.DarkGray;
            //dataGridView_coordinates_set.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            //dataGridView_coordinates_set.RowHeadersDefaultCellStyle.ForeColor = Color.DarkGray;
        }


        /// <summary>Задает название выбранной картинки</summary>
        public void SetImage(string value)
        {
            comboBox_image.Text = value;
            if (comboBox_image.SelectedIndex < 0) comboBox_image.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetImage()
        {
            if (comboBox_image.SelectedIndex < 0) return "";
            return comboBox_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexImage()
        {
            return comboBox_image.SelectedIndex;
        }


        /// <summary>Добавляем набор координат</summary>
        public void SetCoordinates(List<int> coordinatesX, List<int> coordinatesY)
        {
            if (coordinatesX == null || coordinatesY == null) return;
            if (coordinatesX.Count != coordinatesY.Count) return;

            dataGridView_coordinates_set.Rows.Clear();

            if (fixedRowsCount && imagesCount > 0)
            {
                while (dataGridView_coordinates_set.Rows.Count < imagesCount)
                {
                    dataGridView_coordinates_set.Rows.Add(null, null);
                }
            }

            //if (fixedRowsCount && imagesCount > 0)
            if (imagesCount > 0)
            {
                if (fixedRowsCount)
                {
                    for (int i = 0; i < coordinatesX.Count; i++)
                    {
                        if (i < dataGridView_coordinates_set.Rows.Count && i < imagesCount)
                        {
                            dataGridView_coordinates_set.Rows[i].Cells[0].Value = coordinatesX[i];
                            dataGridView_coordinates_set.Rows[i].Cells[1].Value = coordinatesY[i];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < coordinatesX.Count; i++)
                    {
                        if (i < imagesCount - 1)
                        {
                            dataGridView_coordinates_set.Rows.Add(coordinatesX[i], coordinatesY[i]);
                        }
                        else if (i == imagesCount - 1)
                        {
                            dataGridView_coordinates_set.Rows[i].Cells[0].Value = coordinatesX[i];
                            dataGridView_coordinates_set.Rows[i].Cells[1].Value = coordinatesY[i];
                        }
                    }
                }
            }
            else
            {
                for(int i=0; i< coordinatesX.Count; i++)
                {
                    dataGridView_coordinates_set.Rows.Add(coordinatesX[i], coordinatesY[i]);
                }
            }

        }

        /// <summary>Получаем набор координат</summary>
        public void GetCoordinates(out List<int> coordinatesX, out List<int> coordinatesY)
        {
            coordinatesX = new List<int>();
            coordinatesY = new List<int>();
            foreach (DataGridViewRow row in dataGridView_coordinates_set.Rows)
            {
                int x = 0;
                int y = 0;
                if (row.Cells[0].Value != null) Int32.TryParse(row.Cells[0].Value.ToString(), out x);
                if (row.Cells[1].Value != null) Int32.TryParse(row.Cells[1].Value.ToString(), out y);
                if ((row.Cells[0].Value != null || row.Cells[1].Value != null) || fixedRowsCount) 
                {
                    coordinatesX.Add(x);
                    coordinatesY.Add(y);
                }
            }
            if (coordinatesX.Count == 0) coordinatesX = null;
            if (coordinatesY.Count == 0) coordinatesY = null;
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

        imagesCount = -1; // предварительно заданное количество изображений
        fixedRowsCount = false;
        comboBox_image.Text = null;

            dataGridView_coordinates_set.Rows.Clear();

            if (fixedRowsCount && imagesCount > 0)
            {
                while (dataGridView_coordinates_set.Rows.Count < imagesCount)
                {
                    dataGridView_coordinates_set.Rows.Add(null, null);
                }
            }

            setValue = false;
        }
        #endregion

        #region contextMenu
        private void dataGridView_coordinates_set_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.ColumnIndex == -1)
            {
                dataGridView.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                dataGridView.EndEdit();
            }
            else if (dataGridView.EditMode != DataGridViewEditMode.EditOnEnter)
            {
                dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
                dataGridView.BeginEdit(false);
            }

            try
            {
                for (int i = dataGridView.Rows.Count - 1; i > -1; i--)
                {
                    DataGridViewRow row = dataGridView.Rows[i];
                    if (!row.IsNewRow && row.Cells[0].Value == null && row.Cells[1].Value == null)
                    {
                        if (!fixedRowsCount) dataGridView.Rows.Remove(row);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView_coordinates_set_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            DataGridView dataGridView = sender as DataGridView;
            if (e.ColumnIndex == 0 && MouseСoordinates.X >= 0)
            {
                dataGridView.Rows[e.RowIndex].Cells[0].Value = MouseСoordinates.X;
            }
            if (e.ColumnIndex == 1 && MouseСoordinates.Y >= 0)
            {
                dataGridView.Rows[e.RowIndex].Cells[1].Value = MouseСoordinates.Y;
            }

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void dataGridView_coordinates_set_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
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

        private void dataGridView_coordinates_set_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            object head = dataGridView.Rows[e.RowIndex].HeaderCell.Value;
            if (head == null || !head.Equals((e.RowIndex + 1).ToString()))
                dataGridView.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView_coordinates_set_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                Regex my_reg = new Regex(@"[^-\d]");
                string oldValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = my_reg.Replace(oldValue, "");
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;
                if (newValue.Length == 0) dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
            }

            //try
            //{
            //    if (!fixedRowsCount && (dataGridView.Rows[e.RowIndex].Cells[0].Value == null) &&
            //            (dataGridView.Rows[e.RowIndex].Cells[1].Value == null) && (e.RowIndex < dataGridView.Rows.Count - 1))
            //        dataGridView.Rows.RemoveAt(e.RowIndex);
            //}
            //catch (Exception)
            //{
            //}

            try
            {
                for (int i = dataGridView.Rows.Count - 1; i > -1; i--)
                {
                    DataGridViewRow row = dataGridView.Rows[i];
                    if (!row.IsNewRow && row.Cells[0].Value == null && row.Cells[1].Value == null)
                    {
                        if (!fixedRowsCount) dataGridView.Rows.RemoveAt(i);
                    }
                }
            }
            catch (Exception)
            {
            }

            if (fixedRowsCount && imagesCount > 0)
            {
                while (dataGridView_coordinates_set.Rows.Count < imagesCount)
                {
                    dataGridView_coordinates_set.Rows.Add(null, null);
                }
            }

            //if (imagesCount > 0)
            //{
            //    if (dataGridView_coordinates_set.Rows.Count > imagesCount)
            //    {
            //        dataGridView_coordinates_set.AllowUserToAddRows = false;
            //    }
            //}

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void dataGridView_coordinates_set_EnabledChanged(object sender, EventArgs e)
        {
            if (dataGridView_coordinates_set.Enabled)
            {
                dataGridView_coordinates_set.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridView_coordinates_set.DefaultCellStyle.ForeColor = Color.Black;
                dataGridView_coordinates_set.DefaultCellStyle.SelectionForeColor = Color.White;
                dataGridView_coordinates_set.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                dataGridView_coordinates_set.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                dataGridView_coordinates_set.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkGray;
                dataGridView_coordinates_set.DefaultCellStyle.ForeColor = Color.DarkGray;
                dataGridView_coordinates_set.DefaultCellStyle.SelectionForeColor = Color.DarkGray;
                dataGridView_coordinates_set.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                dataGridView_coordinates_set.RowHeadersDefaultCellStyle.ForeColor = Color.DarkGray;
            }
        }

        private void вставитьКоординатыToolStripMenuItem_Click(object sender, EventArgs e)
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
                    DataGridView dataGridView = sourceControl as DataGridView;
                    int x = dataGridView.CurrentCellAddress.X;
                    int y = dataGridView.CurrentCellAddress.Y;
                    if (x < 0) x = 0;
                    if (y < 0) y = 0;

                    if (y == dataGridView.Rows.Count - 1 && !fixedRowsCount)
                    {
                        if ((imagesCount > 0 && y < imagesCount - 1) || (imagesCount <= 0)) dataGridView.Rows.Add(null, null);
                    }
                    dataGridView.Rows[y].Cells[0].Value = MouseСoordinates.X;
                    dataGridView.Rows[y].Cells[1].Value = MouseСoordinates.Y;
                    //dataGridView.Rows.InsertCopy(y, 0);
                    //dataGridView.Rows.RemoveAt(0);
                    dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                    if (x == 0 && y == 0) dataGridView.CurrentCell = dataGridView.Rows[0].Cells[1];
                    dataGridView.CurrentCell = dataGridView.Rows[y].Cells[x];
                    dataGridView.BeginEdit(false);

                    if (ValueChanged != null && !setValue)
                    {
                        EventArgs eventArgs = new EventArgs();
                        ValueChanged(this, eventArgs);
                    }
                }
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
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
                    DataGridView dataGridView = sourceControl as DataGridView;
                    DataGridViewCell cell = dataGridView.CurrentCell;
                    try
                    {
                        Clipboard.SetText(cell.Value.ToString());
                    }
                    catch
                    {
                    }
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
                    DataGridView dataGridView = sourceControl as DataGridView;
                    DataGridViewCell cell = dataGridView.CurrentCell;
                    //Если в буфере обмен содержится текст
                    if (Clipboard.ContainsText() == true)
                    {
                        //Извлекаем (точнее копируем) его и сохраняем в переменную
                        decimal i = 0;
                        if (cell != null && decimal.TryParse(Clipboard.GetText(), out i))
                        {
                            int x = dataGridView.CurrentCellAddress.X;
                            int y = dataGridView.CurrentCellAddress.Y;
                            //if (x < 0) x = 0;
                            //if (y < 0) y = 0;
                            if (y == dataGridView.Rows.Count - 1 && !fixedRowsCount) dataGridView.Rows.Add(null, null);
                            dataGridView.Rows[y].Cells[x].Value = i;
                            dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                            if (x == 0 && y == 0) dataGridView.CurrentCell = dataGridView.Rows[0].Cells[1];
                            dataGridView.CurrentCell = dataGridView.Rows[y].Cells[x];
                            dataGridView.BeginEdit(false);

                            if (ValueChanged != null && !setValue)
                            {
                                EventArgs eventArgs = new EventArgs();
                                ValueChanged(this, eventArgs);
                            }
                        }
                    }

                }
            }
        }

        private void удалитьСтрокуToolStripMenuItem_Click(object sender, EventArgs e)
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
                    DataGridView dataGridView = sourceControl as DataGridView;
                    try
                    {
                        if (fixedRowsCount)
                        {
                            int rowIndex = dataGridView.CurrentCellAddress.Y;
                            int rowCount = dataGridView.Rows.Count;
                            if (rowIndex <= rowCount - 1)
                            {
                                dataGridView.Rows[rowIndex].Cells[0].Value = null;
                                dataGridView.Rows[rowIndex].Cells[1].Value = null;
                            }
                        }
                        else
                        {
                            int rowIndex = dataGridView.CurrentCellAddress.Y;
                            int rowCount = dataGridView.Rows.Count;
                            if (rowIndex == rowCount - 1)
                            {
                                dataGridView.Rows[rowIndex].Cells[0].Value = null;
                                dataGridView.Rows[rowIndex].Cells[1].Value = null;
                            }
                            else
                            {
                                dataGridView.Rows.RemoveAt(rowIndex);
                                if (rowCount == imagesCount)
                                {
                                    object value1 = dataGridView.Rows[rowCount - 2].Cells[0].Value;
                                    object value2 = dataGridView.Rows[rowCount - 2].Cells[1].Value;
                                    if (value1 != null && value2 != null)
                                        dataGridView.Rows.Insert(rowCount - 2, value1, value2);
                                    dataGridView.Rows[rowCount - 1].Cells[0].Value = null;
                                    dataGridView.Rows[rowCount - 1].Cells[1].Value = null;
                                }
                            }
                        }

                        if (ValueChanged != null && !setValue)
                        {
                            EventArgs eventArgs = new EventArgs();
                            ValueChanged(this, eventArgs);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void contextMenuStrip_XY_InTable_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView_coordinates_set.CurrentCell == null) e.Cancel = true;
            if ((MouseСoordinates.X < 0) || (MouseСoordinates.Y < 0))
            {
                contextMenuStrip_XY_InTable.Items[0].Enabled = false;
            }
            else
            {
                contextMenuStrip_XY_InTable.Items[0].Enabled = true;
            }
            decimal i = 0;
            if ((Clipboard.ContainsText() == true) && (decimal.TryParse(Clipboard.GetText(), out i)))
            {
                contextMenuStrip_XY_InTable.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip_XY_InTable.Items[2].Enabled = false;
            }
        }

        private void UserControl_segments_Load(object sender, EventArgs e)
        {
            dataGridView_coordinates_set.AllowUserToAddRows = !fixedRowsCount;
            if (fixedRowsCount && imagesCount > 0)
            {
                while (dataGridView_coordinates_set.Rows.Count < imagesCount)
                {
                    dataGridView_coordinates_set.Rows.Add(null, null);
                }
            }
        }

        private void dataGridView_coordinates_set_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (imagesCount > 0)
            {
                if (dataGridView_coordinates_set.Rows.Count < imagesCount)
                {
                    dataGridView_coordinates_set.AllowUserToAddRows = true;
                }
            }
        }

        private void dataGridView_coordinates_set_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (imagesCount > 0)
            {
                if (dataGridView_coordinates_set.Rows.Count > imagesCount)
                {
                    try
                    {
                        dataGridView_coordinates_set.AllowUserToAddRows = false;
                    }
                    catch
                    {
                    }
                }
            }
        }
        #endregion

        private void UCtrl_Segments_Opt_Load(object sender, EventArgs e)
        {
            dataGridView_coordinates_set.AllowUserToAddRows = !fixedRowsCount;
            if (fixedRowsCount && imagesCount > 0)
            {
                while (dataGridView_coordinates_set.Rows.Count < imagesCount)
                {
                    dataGridView_coordinates_set.Rows.Add(null, null);
                }
            }
        }

        public void UpdateTable()
        {
            UCtrl_Segments_Opt_Load(null, null);
        }
    }
}
//public class Coordinates
//{
//    public long X { get; set; }

//    public long Y { get; set; }
//}
