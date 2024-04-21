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
    public partial class UCtrl_Text_Rotate_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private bool ImageError_mode;
        private bool OptionalSymbol_mode;
        private bool Padding_zero;
        private bool Distance_mode;
        private bool Year_mode = false;
        private bool Sunrise_mode = false;
        private bool Weather_mode = false;
        private bool Separator_mode = false;
        private bool Imperial_unit_mode = false;
        private bool Unit_alignment_mode = true;

        //private Point location_unit;
        private Point location_unit_miles;
        private Point location_imageDecimalPoint;
        private Point location_imageError;
        //private Point location_unit_label;
        private Point location_unit_miles_label;
        private Point location_imageDecimalPoint_label;
        private Point location_imageError_label;
        private String unit_label_text;

        private Point location_addZero;
        private Point location_unit_in_alignment;
        int checkBox_location_offset;

        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _ElementWithText;
        public Dictionary<string, Object> WidgetProperty = new Dictionary<string, Object>();

        public UCtrl_Text_Rotate_Opt()
        {
            InitializeComponent();
            setValue = true;
            comboBox_alignment.SelectedIndex = 0;
            setValue = false;

            //location_unit = comboBox_unit.Location;
            location_unit_miles = comboBox_unit_miles.Location;
            location_imageDecimalPoint = comboBox_imageDecimalPoint.Location;
            location_imageError = comboBox_imageError.Location;
            //location_unit_label = label08.Location; // km
            location_unit_miles_label = label_unit_miles.Location; // ml
            location_imageDecimalPoint_label = label_imageDecimalPoint.Location; // десятичный разделитель
            location_imageError_label = label_imageError.Location; // изображение при ошибке
            unit_label_text = label_unit.Text;

            location_addZero = checkBox_addZero.Location;
            location_unit_in_alignment = checkBox_unit_in_alignment.Location;
            checkBox_location_offset = location_addZero.Y - location_imageDecimalPoint_label.Y;
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

        public void SetUnit(string value)
        {
            comboBox_unit.Text = value;
            if (comboBox_unit.SelectedIndex < 0) comboBox_unit.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetUnit()
        {
            if (comboBox_unit.SelectedIndex < 0) return "";
            return comboBox_unit.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexUnit()
        {
            return comboBox_unit.SelectedIndex;
        }

        public void SetUnitMile(string value)
        {
            comboBox_unit_miles.Text = value;
            if (comboBox_unit_miles.SelectedIndex < 0) comboBox_unit_miles.Text = "";
        }
        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetUnitMile()
        {
            if (comboBox_unit_miles.SelectedIndex < 0) return "";
            return comboBox_unit_miles.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexUnitMile()
        {
            return comboBox_unit_miles.SelectedIndex;
        }

        public void SetImageError(string value)
        {
            comboBox_imageError.Text = value;
            if (comboBox_imageError.SelectedIndex < 0) comboBox_imageError.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetImageError()
        {
            if (comboBox_imageError.SelectedIndex < 0) return "";
            return comboBox_imageError.Text;
        }

        public void SetImageDecimalPointOrMinus(string value)
        {
            comboBox_imageDecimalPoint.Text = value;
            if (comboBox_imageDecimalPoint.SelectedIndex < 0) comboBox_imageDecimalPoint.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetImageDecimalPointOrMinus()
        {
            if (comboBox_imageDecimalPoint.SelectedIndex < 0) return "";
            return comboBox_imageDecimalPoint.Text;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexImageDecimalPointOrMinus()
        {
            return comboBox_imageDecimalPoint.SelectedIndex;
        }

        public void SetSeparator(string value)
        {
            comboBox_separator.Text = value;
            if (comboBox_separator.SelectedIndex < 0) comboBox_separator.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetSeparator()
        {
            if (comboBox_separator.SelectedIndex < 0) return "";
            return comboBox_separator.Text;
        }

        public void SetAlignment(string alignment)
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
            comboBox_alignment.SelectedIndex = result;
        }

        /// <summary>Возвращает выравнивание строкой "LEFT", "RIGHT", "CENTER_H"</summary>
        public string GetAlignment()
        {
            string result;
            switch (comboBox_alignment.SelectedIndex)
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
        public int GetSelectedIndexAlignment()
        {
            return comboBox_alignment.SelectedIndex;
        }

        /// <summary>Отображение поля изображения при ошибке</summary>
        [Description("Отображение поля изображения при ошибке")]
        public virtual bool ImageError
        {
            get
            {
                return ImageError_mode;
            }
            set
            {
                ImageError_mode = value;
                comboBox_imageError.Visible = ImageError_mode;
                label_imageError.Visible = ImageError_mode;
            }
        }

        /// <summary>Отображение поля изображения десятичного разделителя</summary>
        [Description("Отображение поля изображения десятичного разделителя")]
        public virtual bool OptionalSymbol
        {
            get
            {
                return OptionalSymbol_mode;
            }
            set
            {
                OptionalSymbol_mode = value;
                comboBox_imageDecimalPoint.Visible = OptionalSymbol_mode;
                label_imageDecimalPoint.Visible = OptionalSymbol_mode;

                if (!OptionalSymbol_mode && !Distance_mode && !Sunrise_mode && !Weather_mode)
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y - checkBox_location_offset);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y - checkBox_location_offset);
                    checkBox_unit_in_alignment.Location = location;
                }
                else
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y);
                    checkBox_unit_in_alignment.Location = location;
                }
            }
        }

        /// <summary>Отображение чекбокса добавления нулей в начале</summary>
        [Description("Отображение чекбокса добавления нулей в начале")]
        public virtual bool PaddingZero
        {
            get
            {
                return Padding_zero;
            }
            set
            {
                Padding_zero = value;
                checkBox_addZero.Visible = Padding_zero;
            }
        }

        /// <summary>Режим для дистанции</summary>
        [Description("Режим для дистанции")]
        public virtual bool Distance
        {
            get
            {
                return Distance_mode;
            }
            set
            {
                Distance_mode = value;
                if (Distance_mode)
                {
                    //Distance = false;
                    Year = false;
                    Sunrise = false;
                    Weather = false;

                    label_unit.Text = unit_label_text + " (km)";
                    label_unit_miles.Text = unit_label_text + " (ml)";

                    comboBox_imageDecimalPoint.Location = location_imageDecimalPoint;
                    comboBox_imageError.Location = location_unit_miles;
                    comboBox_unit_miles.Location = location_imageError;

                    label_imageDecimalPoint.Location = location_imageDecimalPoint_label;
                    label_unit_miles.Location = location_imageError_label;
                    label_imageError.Location = location_unit_miles_label;

                    comboBox_unit_miles.Visible = true;
                    label_unit_miles.Visible = true;
                }
                else
                {
                    label_unit.Text = unit_label_text;
                    label_unit_miles.Text = unit_label_text;
                }

                if (!OptionalSymbol_mode && !Distance_mode && !Sunrise_mode && !Weather_mode)
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y - checkBox_location_offset);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y - checkBox_location_offset);
                    checkBox_unit_in_alignment.Location = location;
                }
                else
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y);
                    checkBox_unit_in_alignment.Location = location;
                }
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
                    Distance = false;
                    //Year = false;
                    Sunrise = false;
                    Weather = false;

                    checkBox_addZero.Text = Properties.Strings.UCtrl_Text_Opt_Year_true;
                }
                else
                {
                    checkBox_addZero.Text = Properties.Strings.UCtrl_Text_Opt_Year_false;
                }
            }
        }

        /// <summary>Режим отображения восхода</summary>
        [Description("Режим отображения восхода")]
        public virtual bool Sunrise
        {
            get
            {
                return Sunrise_mode;
            }
            set
            {
                Sunrise_mode = value;
                if (Sunrise_mode)
                {
                    Distance = false;
                    Year = false;
                    //Sunrise = false;
                    Weather = false;

                    label_imageDecimalPoint.Text = Properties.Strings.UCtrl_Text_Opt_Sunrise_true;

                    comboBox_imageDecimalPoint.Location = location_imageError;
                    comboBox_imageError.Location = location_imageDecimalPoint;
                    comboBox_unit_miles.Location = location_unit_miles;

                    label_imageDecimalPoint.Location = location_imageError_label;
                    label_imageError.Location = location_imageDecimalPoint_label;
                    label_unit_miles.Location = location_unit_miles_label;
                }
                else
                {
                    label_imageDecimalPoint.Text = Properties.Strings.UCtrl_Text_Opt_Sunrise_false;
                }

                if (!OptionalSymbol_mode && !Distance_mode && !Sunrise_mode && !Weather_mode)
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y - checkBox_location_offset);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y - checkBox_location_offset);
                    checkBox_unit_in_alignment.Location = location;
                }
                else
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y);
                    checkBox_unit_in_alignment.Location = location;
                }
            }
        }

        /// <summary>Режим отображения погоды</summary>
        [Description("Режим отображения погоды")]
        public virtual bool Weather
        {
            get
            {
                return Weather_mode;
            }
            set
            {
                Weather_mode = value;
                if (Weather_mode)
                {
                    Distance = false;
                    Year = false;
                    Sunrise = false;
                    //Weather = false;

                    label_unit.Text = unit_label_text + " (°C)";
                    label_unit_miles.Text = unit_label_text + " (°F)";

                    comboBox_imageDecimalPoint.Location = location_imageDecimalPoint;
                    comboBox_imageError.Location = location_unit_miles;
                    comboBox_unit_miles.Location = location_imageError;

                    label_imageDecimalPoint.Location = location_imageDecimalPoint_label;
                    label_minus_image.Location = location_imageDecimalPoint_label;
                    label_unit_miles.Location = location_imageError_label;
                    label_imageError.Location = location_unit_miles_label;

                    comboBox_unit_miles.Visible = true;
                    label_unit_miles.Visible = true;
                    label_minus_image.Visible = true;
                    label_imageDecimalPoint.Visible = false;
                }
                else
                {
                    label_minus_image.Visible = false;
                }

                if (!OptionalSymbol_mode && !Distance_mode && !Sunrise_mode && !Weather_mode)
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y - checkBox_location_offset);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y - checkBox_location_offset);
                    checkBox_unit_in_alignment.Location = location;
                }
                else
                {
                    Point location = new Point(location_addZero.X, location_addZero.Y);
                    checkBox_addZero.Location = location;
                    location = new Point(location_unit_in_alignment.X, location_unit_in_alignment.Y);
                    checkBox_unit_in_alignment.Location = location;
                }
            }
        }

        /// <summary>Отображение чекбокса учитывания единиц измерения при выравнивании</summary>
        [Description("Отображение чекбокса учитывания единиц измерения при выравнивании")]
        public virtual bool Unit_alignment
        {
            get
            {
                return Unit_alignment_mode;
            }
            set
            {
                Unit_alignment_mode = value;
                checkBox_unit_in_alignment.Visible = Unit_alignment_mode;
            }
        }

        /// <summary>Доступность разделителя</summary>
        [Description("Доступность разделителя")]
        public virtual bool Separator
        {
            get
            {
                return Separator_mode;
            }
            set
            {
                Separator_mode = value;

                label_separator.Visible = value;
                comboBox_separator.Visible = value;
            }
        }

        /// <summary>Доступность имперских единиц измерения</summary>
        [Description("Доступность имперских единиц измерения")]
        public virtual bool Imperial_unit
        {
            get
            {
                return Imperial_unit_mode;
            }
            set
            {
                Imperial_unit_mode = value;

                if (Weather_mode)
                {
                    label_unit_miles.Enabled = Imperial_unit_mode;
                    comboBox_unit_miles.Enabled = Imperial_unit_mode; 
                }
                else
                {
                    label_unit_miles.Enabled = true;
                    comboBox_unit_miles.Enabled = true;
                }
            }
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при копировании свойст виджета")]
        public event WidgetProperty_Copy_Handler WidgetProperty_Copy;
        public delegate void WidgetProperty_Copy_Handler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при вставке свойст виджета")]
        public event WidgetProperty_Paste_Handler WidgetProperty_Paste;
        public delegate void WidgetProperty_Paste_Handler(object sender, EventArgs eventArgs);

        private void checkBox_Click(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
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
            comboBox_unit.Items.Clear();
            comboBox_unit_miles.Items.Clear();
            comboBox_imageError.Items.Clear();
            comboBox_imageDecimalPoint.Items.Clear();
            comboBox_separator.Items.Clear();

            comboBox_image.Items.AddRange(ListImages.ToArray());
            comboBox_unit.Items.AddRange(ListImages.ToArray());
            comboBox_unit_miles.Items.AddRange(ListImages.ToArray());

            comboBox_imageError.Items.AddRange(ListImages.ToArray());
            comboBox_imageDecimalPoint.Items.AddRange(ListImages.ToArray());
            comboBox_separator.Items.AddRange(ListImages.ToArray());

            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_image.DropDownHeight = 1;
                comboBox_unit.DropDownHeight = 1;
                comboBox_unit_miles.DropDownHeight = 1;
                comboBox_imageError.DropDownHeight = 1;
                comboBox_imageDecimalPoint.DropDownHeight = 1;
                comboBox_separator.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_image.DropDownHeight = 35 * count + 1;
                comboBox_unit.DropDownHeight = 35 * count + 1;
                comboBox_unit_miles.DropDownHeight = 35 * count + 1;
                comboBox_imageError.DropDownHeight = 35 * count + 1;
                comboBox_imageDecimalPoint.DropDownHeight = 35 * count + 1;
                comboBox_separator.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_image.DropDownHeight = 106;
                comboBox_unit.DropDownHeight = 106;
                comboBox_unit_miles.DropDownHeight = 106;
                comboBox_imageError.DropDownHeight = 106;
                comboBox_imageDecimalPoint.DropDownHeight = 106;
                comboBox_separator.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            ImageError = false;
            OptionalSymbol = false;
            PaddingZero = false;
            Distance = false;
            Year = false;
            Sunrise = false;
            Weather = false;
            Separator = false;

            comboBox_image.Text = null;
            comboBox_unit.Text = null;
            comboBox_imageError.Text = null;
            comboBox_imageDecimalPoint.Text = null;
            comboBox_separator.Text = null;

            numericUpDown_imageX.Value = 0;
            numericUpDown_imageY.Value = 0;

            numericUpDown_spacing.Value = 0;
            numericUpDown_angle.Value = 0;

            comboBox_alignment.SelectedIndex = 0;
            checkBox_addZero.Checked = false;
            checkBox_unit_in_alignment.Checked = false;

            comboBox_imageDecimalPoint.Location = location_imageDecimalPoint;
            comboBox_imageError.Location = location_imageError;
            comboBox_unit_miles.Location = location_unit_miles;

            label_imageDecimalPoint.Location = location_imageDecimalPoint_label;
            label_imageError.Location = location_imageError_label;
            label_unit_miles.Location = location_unit_miles_label;
            comboBox_unit_miles.Visible = false;
            label_unit_miles.Visible = false;
            label_minus_image.Visible = false;

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

        private void numericUpDown_image_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_imageX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_imageX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_imageY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_imageY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_imageX" || numericUpDown.Name == "numericUpDown_imageY"))
                    numericUpDown_imageX.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_imageX" || numericUpDown.Name == "numericUpDown_imageY"))
                    numericUpDown_imageX.UpButton();

                e.Handled = true;
            }
        }

        private void context_WidgetProperty_Opening(object sender, CancelEventArgs e)
        {
            if (WidgetProperty.ContainsKey("hmUI_widget_IMG_NUMBER")) context_WidgetProperty.Items[1].Enabled = true;
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
    }
}
