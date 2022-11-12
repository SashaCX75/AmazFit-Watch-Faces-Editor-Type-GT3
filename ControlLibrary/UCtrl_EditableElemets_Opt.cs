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
using System.Xml.Linq;

namespace ControlLibrary
{
    public partial class UCtrl_EditableElemets_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _EditableElemets;
        private bool _collapse = false;

        bool highlight_images = false;
        bool highlight_segments = false;
        bool highlight_pointer = false;
        bool highlight_number = false;
        bool highlight_number_target = false;
        bool highlight_number_min = false;
        bool highlight_number_max = false;
        bool highlight_sunset = false;
        bool highlight_sunrise = false;
        bool highlight_sunset_sunrise = false;
        bool highlight_city_name = false;
        bool highlight_circle_scale = false;
        bool highlight_linear_scale = false;
        bool highlight_icon = false;

        float currentDPI; // масштаб экрана
        //bool visibilityElement = true; // элемент оторажается на предпросмотре

        public int position = -1; // позиция в наборе элеменетов
        public string selectedElement; // название выбраного элемента

        Point cursorPos = new Point(0, 0);

        public UCtrl_EditableElemets_Opt()
        {
            Logger.WriteLine("* UCtrl_EditableElemets_Opt");
            InitializeComponent();
            setValue = false;
            currentDPI = tabControl1.Height / 462f;
            Logger.WriteLine("* currentDPI = " + currentDPI.ToString());

            button_collapse.Controls.Add(pictureBox_Arrow_Right); 
            button_collapse.Controls.Add(pictureBox_Arrow_Down);

            pictureBox_Arrow_Right.Location = new Point(1, 2);
            pictureBox_Arrow_Right.BackColor = Color.Transparent;

            pictureBox_Arrow_Down.Location = new Point(1, 2);
            pictureBox_Arrow_Down.BackColor = Color.Transparent;
        }
        
        /// <summary>Сворачиваение элемента</summary>
        [Description("Сворачиваение элемента")]
        public virtual bool Collapse
        {
            get
            {
                return _collapse;
            }
            set
            {
                _collapse = value;
                //button_collapse.Visible = _collapse;
                tabControl1.Visible = !_collapse;
                pictureBox_Arrow_Right.Visible = _collapse;
                pictureBox_Arrow_Down.Visible = !_collapse;
            }
        }

        /// <summary>Задает индекс выбраноq зоны</summary>
        public void SetZoneIndex(int index)
        {
            setValue = true;
            comboBox_select_zone.SelectedIndex = index;
            setValue = false;
        }

        /// <summary>Задает количество зон в выпадающем списке</summary>
        public void SetZoneCount(int count)
        {
            setValue = true;
            comboBox_select_zone.Items.Clear();
            for (int i = 1; i < count + 1; i++)
            {
                comboBox_select_zone.Items.Add(i.ToString());
            }
            if (count >= 9) button_zoneAdd.Enabled = false;
            else button_zoneAdd.Enabled = true;
            setValue = false;
        }

        /// <summary>Задает индекс выбраного элемента в зоне</summary>
        public void SetElementsIndex(int index)
        {
            //setValue = true;
            comboBox_select_element.SelectedIndex = index;
            //setValue = false;
        }

        /// <summary>Задает количество элементов в выбранной зоне</summary>
        public void SetElementsCount(int count)
        {
            setValue = true;
            comboBox_select_element.Items.Clear();
            for (int i = 1; i < count + 1; i++)
            {
                comboBox_select_element.Items.Add(i.ToString());
            }
            if (comboBox_select_zone.SelectedIndex >= 0)
            {
                button_elementAdd.Enabled = true; 
                if (comboBox_select_element.SelectedIndex >= 0) button_elementDel.Enabled = true;
                else button_elementDel.Enabled = false;
            }
            else
            {
                button_elementAdd.Enabled = false;
                button_elementDel.Enabled = false;
            }
            setValue = false;
        }

        /// <summary>Задает количество элементов в выбранной зоне и прописывает названия элементов</summary>
        public void SetElementsCount(List<string> elementName)
        {
            setValue = true;
            int count = elementName.Count;
            comboBox_select_element.Items.Clear();
            for (int i = 1; i < count + 1; i++)
            {
                comboBox_select_element.Items.Add(i.ToString() + " " + elementName[i - 1]);
            }
            if (comboBox_select_zone.SelectedIndex >= 0)
            {
                button_elementAdd.Enabled = true;
                if (comboBox_select_element.SelectedIndex >= 0) button_elementDel.Enabled = true;
                else button_elementDel.Enabled = false;
            }
            else
            {
                button_elementAdd.Enabled = false;
                button_elementDel.Enabled = false;
            }
            setValue = false;
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


        /// <summary>Задает название рамки выделения невыбранного элемента</summary>
        public void SetUnSelectImage(string value)
        {
            comboBox_un_select_image.Text = value;
            if (comboBox_un_select_image.SelectedIndex < 0) comboBox_un_select_image.Text = "";
        }

        /// <summary>Возвращает название рамки выделения невыбранного элемента</summary>
        public string GetUnSelectImage()
        {
            if (comboBox_un_select_image.SelectedIndex < 0) return "";
            return comboBox_un_select_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка рамки выделения невыбранного элемента</summary>
        public int GetSelectedIndexUnSelectImage()
        {
            return comboBox_un_select_image.SelectedIndex;
        }


        /// <summary>Задает название рамки выделения выбранного элемента</summary>
        public void SetSelectImage(string value)
        {
            comboBox_select_image.Text = value;
            if (comboBox_select_image.SelectedIndex < 0) comboBox_select_image.Text = "";
        }

        /// <summary>Возвращает название рамки выделения выбранного элемента</summary>
        public string GetSelectImage()
        {
            if (comboBox_select_image.SelectedIndex < 0) return "";
            return comboBox_select_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка рамки выделения выбранного элемента</summary>
        public int GetSelectedIndexSelectImage()
        {
            return comboBox_select_image.SelectedIndex;
        }


        /// <summary>Задает название верхней маски</summary>
        public void SetFgMask(string value)
        {
            comboBox_fg_mask.Text = value;
            if (comboBox_fg_mask.SelectedIndex < 0) comboBox_fg_mask.Text = "";
        }

        /// <summary>Возвращает название верхней маски</summary>
        public string GetFgMask()
        {
            if (comboBox_fg_mask.SelectedIndex < 0) return "";
            return comboBox_fg_mask.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка верхней маски</summary>
        public int GetSelectedIndexFgMask()
        {
            return comboBox_fg_mask.SelectedIndex;
        }


        /// <summary>Задает название нижней маски</summary>
        public void SetMask(string value)
        {
            comboBox_mask.Text = value;
            if (comboBox_mask.SelectedIndex < 0) comboBox_mask.Text = "";
        }

        /// <summary>Возвращает название нижней маски</summary>
        public string GetMask()
        {
            if (comboBox_mask.SelectedIndex < 0) return "";
            return comboBox_mask.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка нижней маски</summary>
        public int GetSelectedIndexMask()
        {
            return comboBox_mask.SelectedIndex;
        }


        /// <summary>Задает название предпросмотра элемента</summary>
        public void SetPreviewElement(string value)
        {
            comboBox_Preview_image.Text = value;
            if (comboBox_Preview_image.SelectedIndex < 0) comboBox_Preview_image.Text = "";
        }

        /// <summary>Возвращает название предпросмотра предпросмотра элемента</summary>
        public string GetPreviewElement()
        {
            if (comboBox_Preview_image.SelectedIndex < 0) return "";
            return comboBox_Preview_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка предпросмотра элемента</summary>
        public int GetSelectedIndexPreviewElement()
        {
            return comboBox_Preview_image.SelectedIndex;
        }

        [Browsable(true)]
        [Description("Происходит при изменении общих настроек редактируемых элементов")]
        public event ZoneValueChangedHandler ZoneValueChanged;
        public delegate void ZoneValueChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при изменении настроек элемента в выбраной зоне")]
        public event ElementValueChangedHandler ElementValueChanged;
        public delegate void ElementValueChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при удалении редактируемой зоны")]
        public event ZoneDelHandler ZoneDel;
        public delegate void ZoneDelHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при добавлении редактируемой зоны")]
        public event ZoneAddHandler ZoneAdd;
        public delegate void ZoneAddHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при изменении выбора редактируемой зоны")]
        public event ZoneIndexChangedHandler ZoneIndexChanged;
        public delegate void ZoneIndexChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при удалении редактируемой зоны")]
        public event ElementDelHandler ElementDel;
        public delegate void ElementDelHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при добавлении редактируемой зоны")]
        public event ElementAddHandler ElementAdd;
        public delegate void ElementAddHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при изменении выбора редактируемой зоны")]
        public event ElementIndexChangedHandler ElementIndexChanged;
        public delegate void ElementIndexChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при обновлении предпросмотра элемента")]
        public event PreviewElementRefreshHandler PreviewElementRefresh;
        public delegate void PreviewElementRefreshHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при добавлении предпросмотра элемента")]
        public event PreviewElementAddHandler PreviewElementAdd;
        public delegate void PreviewElementAddHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при изменении положения параметров в элементе")]
        public event OptionsMovedHandler OptionsMoved;
        public delegate void OptionsMovedHandler(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions);

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event SelectChangedHandler SelectChanged;
        public delegate void SelectChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении видимости отдельного параметра в выбраном элнменте зоны")]
        public event VisibleOptionsChangedHandler VisibleOptionsChanged;
        public delegate void VisibleOptionsChangedHandler(object sender, EventArgs eventArgs);

        private void checkBox_Click(object sender, EventArgs e)
        {
            if (ZoneValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ZoneValueChanged(this, eventArgs, comboBox_select_zone.SelectedIndex);
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
                if (ZoneValueChanged != null && !setValue)
                {
                    EventArgs eventArgs = new EventArgs();
                    ZoneValueChanged(this, eventArgs, comboBox_select_zone.SelectedIndex);
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
            if (ZoneValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ZoneValueChanged(this, eventArgs, comboBox_select_zone.SelectedIndex);
            }
        }

        private void comboBox_select_zone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_select_zone.SelectedIndex >= 0)
            {
                button_zoneDel.Enabled = true;
            }
            else
            {
                button_zoneDel.Enabled = false;
            }

            if (comboBox_select_zone.SelectedIndex >= 0)
            {
                button_elementAdd.Enabled = true;
                if (comboBox_select_element.SelectedIndex >= 0) button_elementDel.Enabled = true;
                else button_elementDel.Enabled = false;
            }
            else
            {
                button_elementAdd.Enabled = false;
                button_elementDel.Enabled = false;
            }

            if (ZoneIndexChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ZoneIndexChanged(this, eventArgs, comboBox_select_zone.SelectedIndex);
            }
        }

        private void comboBox_Preview_image_SelectedIndexChanged(object sender, EventArgs e)
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

            if (ElementValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ElementValueChanged(this, eventArgs, comboBox_select_element.SelectedIndex);
            }
        }
        #endregion


        public void ResetHighlightState()
        {
            selectedElement = "";

            highlight_images = false;
            highlight_segments = false;
            highlight_pointer = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_city_name = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_icon = false;

            SelectElement();
        }

        private void SelectElement()
        {
            if (highlight_images)
            {
                panel_Images.BackColor = SystemColors.ActiveCaption;
                button_Images.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Images.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Images.BackColor = SystemColors.Control;
                button_Images.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Images.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_segments)
            {
                panel_Segments.BackColor = SystemColors.ActiveCaption;
                button_Segments.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Segments.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Segments.BackColor = SystemColors.Control;
                button_Segments.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Segments.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_pointer)
            {
                panel_Pointer.BackColor = SystemColors.ActiveCaption;
                button_Pointer.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Pointer.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Pointer.BackColor = SystemColors.Control;
                button_Pointer.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Pointer.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number)
            {
                panel_Number.BackColor = SystemColors.ActiveCaption;
                button_Number.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number.BackColor = SystemColors.Control;
                button_Number.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_target)
            {
                panel_Number_Target.BackColor = SystemColors.ActiveCaption;
                button_Number_Target.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Target.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Target.BackColor = SystemColors.Control;
                button_Number_Target.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Target.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_min)
            {
                panel_Number_Min.BackColor = SystemColors.ActiveCaption;
                button_Number_Min.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Min.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Min.BackColor = SystemColors.Control;
                button_Number_Min.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Min.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_max)
            {
                panel_Number_Max.BackColor = SystemColors.ActiveCaption;
                button_Number_Max.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Max.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Max.BackColor = SystemColors.Control;
                button_Number_Max.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Max.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }


            if (highlight_sunset)
            {
                panel_Sunset.BackColor = SystemColors.ActiveCaption;
                button_Sunset.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Sunset.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Sunset.BackColor = SystemColors.Control;
                button_Sunset.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Sunset.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_sunrise)
            {
                panel_Sunrise.BackColor = SystemColors.ActiveCaption;
                button_Sunrise.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Sunrise.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Sunrise.BackColor = SystemColors.Control;
                button_Sunrise.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Sunrise.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_sunset_sunrise)
            {
                panel_Sunset_Sunrise.BackColor = SystemColors.ActiveCaption;
                button_Sunset_Sunrise.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Sunset_Sunrise.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Sunset_Sunrise.BackColor = SystemColors.Control;
                button_Sunset_Sunrise.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Sunset_Sunrise.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_city_name)
            {
                panel_Text_CityName.BackColor = SystemColors.ActiveCaption;
                button_Text_CityName.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_CityName.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_CityName.BackColor = SystemColors.Control;
                button_Text_CityName.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_CityName.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_circle_scale)
            {
                panel_Circle_Scale.BackColor = SystemColors.ActiveCaption;
                button_Circle_Scale.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Circle_Scale.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Circle_Scale.BackColor = SystemColors.Control;
                button_Circle_Scale.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Circle_Scale.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_linear_scale)
            {
                panel_Linear_Scale.BackColor = SystemColors.ActiveCaption;
                button_Linear_Scale.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Linear_Scale.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Linear_Scale.BackColor = SystemColors.Control;
                button_Linear_Scale.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Linear_Scale.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_icon)
            {
                panel_Icon.BackColor = SystemColors.ActiveCaption;
                button_Icon.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Icon.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Icon.BackColor = SystemColors.Control;
                button_Icon.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Icon.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }
        }

        private void panel_Images_Click(object sender, EventArgs e)
        {
            selectedElement = "Images";

            highlight_images = true;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Segments_Click(object sender, EventArgs e)
        {
            selectedElement = "Segments";

            highlight_images = false;
            highlight_segments = true;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Click(object sender, EventArgs e)
        {
            selectedElement = "Number";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = true;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Target_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Target";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = true;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Min_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Min";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = true;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Max_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Max";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = true;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Sunset_Click(object sender, EventArgs e)
        {
            selectedElement = "Sunset";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = true;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Sunrise_Click(object sender, EventArgs e)
        {
            selectedElement = "Sunrise";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = true;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Sunset_Sunrise_Click(object sender, EventArgs e)
        {
            selectedElement = "Sunset_Sunrise";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = true;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Pointer_Click(object sender, EventArgs e)
        {
            selectedElement = "Pointer";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = true;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Circle_Scale_Click(object sender, EventArgs e)
        {
            selectedElement = "Circle_Scale";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = true;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Linear_Scale_Click(object sender, EventArgs e)
        {
            selectedElement = "Linear_Scale";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = true;
            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_CityName_Click(object sender, EventArgs e)
        {
            selectedElement = "CityName";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = true;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Icon_Click(object sender, EventArgs e)
        {
            selectedElement = "Icon";

            highlight_images = false;
            highlight_segments = false;
            highlight_number = false;
            highlight_number_target = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_pointer = false;
            highlight_circle_scale = false;
            highlight_linear_scale = false;
            highlight_city_name = false;
            highlight_icon = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }


        #region перетягиваем элементы
        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null)
            {
                panel.Tag = new object();
                cursorPos = Cursor.Position;
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null && panel.Tag != null)
            {
                int cursorX = Cursor.Position.X;
                int cursorY = Cursor.Position.Y;
                int dX = Math.Abs(cursorX - cursorPos.X);
                int dY = Math.Abs(cursorY - cursorPos.Y);
                if (dX > 5 || dY > 5)
                    panel.DoDragDrop(sender, DragDropEffects.Move);
                //panel.DoDragDrop(sender, DragDropEffects.Move);
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null && panel.Tag != null) panel.Tag = null;

            //((Control)sender).Tag = null;
        }

        private void tableLayoutPanel_element_DragDrop(object sender, DragEventArgs e)
        {
            int cursorX = Cursor.Position.X;
            int cursorY = Cursor.Position.Y;
            int dX = Math.Abs(cursorX - cursorPos.X);
            int dY = Math.Abs(cursorY - cursorPos.Y);
            if (dX > 5 || dY > 5)
            {
                if (OptionsMoved != null)
                {
                    EventArgs eventArgs = new EventArgs();
                    OptionsMoved(this, eventArgs, GetOptionsPosition());
                }
            }
        }

        private void tableLayoutPanel_element_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Button)) && !e.Data.GetDataPresent(typeof(Panel)))
                return;

            e.Effect = e.AllowedEffect;
            Panel draggedPanel = (Panel)e.Data.GetData(typeof(Panel));
            if (draggedPanel == null)
            {
                Button draggedButton = (Button)e.Data.GetData(typeof(Button));
                if (draggedButton != null) draggedPanel = (Panel)draggedButton.Parent;
            }
            if (draggedPanel == null) return;

            Point pt = tableLayoutPanel_element.PointToClient(new Point(e.X, e.Y));
            Control control = tableLayoutPanel_element.GetChildAtPoint(pt);


            if (control != null)
            {
                var pos = tableLayoutPanel_element.GetPositionFromControl(control);
                var posOld = tableLayoutPanel_element.GetPositionFromControl(draggedPanel);
                if (pos != posOld && pos.Row < posOld.Row)
                {
                    if (pt.Y < control.Location.Y + draggedPanel.Height * 0.4)
                    {
                        tableLayoutPanel_element.SetRow(draggedPanel, pos.Row);
                        tableLayoutPanel_element.SetRow(control, pos.Row + 1);
                        //if (pos.Row < posOld.Row) tableLayoutPanel_element.SetRow(control, pos.Row + 1);
                        //else tableLayoutPanel_element.SetRow(control, pos.Row - 1);
                    }
                }
                if (pos != posOld && pos.Row > posOld.Row)
                {
                    if (pt.Y > control.Location.Y + control.Height * 0.6)
                    {
                        tableLayoutPanel_element.SetRow(control, pos.Row - 1);
                        tableLayoutPanel_element.SetRow(draggedPanel, pos.Row);
                        //if (pos.Row < posOld.Row) tableLayoutPanel_element.SetRow(control, pos.Row + 1);
                        //else tableLayoutPanel_element.SetRow(control, pos.Row - 1);
                    }
                }
                draggedPanel.Tag = null;
            }
        }

        #endregion

        private void checkBox_Elements_CheckedChanged(object sender, EventArgs e)
        {
            if (VisibleOptionsChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleOptionsChanged(sender, eventArgs);
            }
        }

        /// <summary>Устанавливаем статус вкл/выкл для отдельной опции в элементе</summary>
        public void SetVisibilityOptionsStatus(string name, bool status)
        {
            setValue = true;
            switch (name)
            {
                case "Images":
                    checkBox_Images.Checked = status;
                    break;
                case "Segments":
                    checkBox_Segments.Checked = status;
                    break;
                case "Number":
                    checkBox_Number.Checked = status;
                    break;
                case "Number_Target":
                    checkBox_Number_Target.Checked = status;
                    break;
                case "Number_Min":
                    checkBox_Number_Min.Checked = status;
                    break;
                case "Number_Max":
                    checkBox_Number_Max.Checked = status;
                    break;
                case "Sunset":
                    checkBox_Sunset.Checked = status;
                    break;
                case "Sunrise":
                    checkBox_Sunrise.Checked = status;
                    break;
                case "Sunset_Sunrise":
                    checkBox_Sunset_Sunrise.Checked = status;
                    break;
                case "Pointer":
                    checkBox_Pointer.Checked = status;
                    break;
                case "Circle_Scale":
                    checkBox_Circle_Scale.Checked = status;
                    break;
                case "Linear_Scale":
                    checkBox_Linear_Scale.Checked = status;
                    break;
                case "CityName":
                    checkBox_Text_CityName.Checked = status;
                    break;
                case "Icon":
                    checkBox_Icon.Checked = status;
                    break;
            }
            setValue = false;
        }

        /// <summary>Устанавливаем порядок опций в элементе</summary>
        public void SetOptionsPosition(Dictionary<int, string> elementOptions)
        {
            int elementCount = tableLayoutPanel_element.RowCount;
            for (int key = elementCount - 1; key >= 0; key--)
            {
                Control panel = null;
                if (elementOptions.ContainsKey(elementCount - key))
                {
                    string name = elementOptions[elementCount - key];
                    switch (name)
                    {
                        case "Images":
                            panel = panel_Images;
                            break;
                        case "Segments":
                            panel = panel_Segments;
                            break;
                        case "Number":
                            panel = panel_Number;
                            break;
                        case "Number_Target":
                            panel = panel_Number_Target;
                            break;
                        case "Number_Min":
                            panel = panel_Number_Min;
                            break;
                        case "Number_Max":
                            panel = panel_Number_Max;
                            break;
                        case "Sunset":
                            panel = panel_Sunset;
                            break;
                        case "Sunrise":
                            panel = panel_Sunrise;
                            break;
                        case "Sunset_Sunrise":
                            panel = panel_Sunset_Sunrise;
                            break;
                        case "Pointer":
                            panel = panel_Pointer;
                            break;
                        case "Circle_Scale":
                            panel = panel_Circle_Scale;
                            break;
                        case "Linear_Scale":
                            panel = panel_Linear_Scale;
                            break;
                        case "CityName":
                            panel = panel_Text_CityName;
                            break;
                        case "Icon":
                            panel = panel_Icon;
                            break;
                    }
                }
                position = key;
                if (panel == null)
                    continue;
                int realPos = tableLayoutPanel_element.GetRow(panel);
                if (realPos == position)
                    continue;
                if (realPos < position)
                {
                    for (int i = realPos; i < position; i++)
                    {
                        Control panel2 = tableLayoutPanel_element.GetControlFromPosition(0, i + 1);
                        if (panel2 == null) return;
                        tableLayoutPanel_element.SetRow(panel2, i);
                        tableLayoutPanel_element.SetRow(panel, i + 1);
                    }
                }
                else
                {
                    for (int i = realPos; i > position; i--)
                    {
                        Control panel2 = tableLayoutPanel_element.GetControlFromPosition(0, i - 1);
                        if (panel2 == null)
                            return;
                        tableLayoutPanel_element.SetRow(panel, i - 1);
                        tableLayoutPanel_element.SetRow(panel2, i);
                    }
                }
            }
        }

        /// <summary>Получаем порядок опций в элементе</summary>
        public Dictionary<string, int> GetOptionsPosition()
        {
            Dictionary<string, int> elementOptions = new Dictionary<string, int>();
            int index = 1;
            for (int i = tableLayoutPanel_element.RowCount - 1; i >= 0; i--)
            {
                Control panel = tableLayoutPanel_element.GetControlFromPosition(0, i);
                if (panel != null)
                {
                    switch (panel.Name)
                    {
                        case "panel_Images":
                            elementOptions.Add("Images", index);
                            break;
                        case "panel_Segments":
                            elementOptions.Add("Segments", index);
                            break;
                        case "panel_Number":
                            elementOptions.Add("Number", index);
                            break;
                        case "panel_Number_Target":
                            elementOptions.Add("Number_Target", index);
                            break;
                        case "panel_Number_Min":
                            elementOptions.Add("Number_Min", index);
                            break;
                        case "panel_Number_Max":
                            elementOptions.Add("Number_Max", index);
                            break;
                        case "panel_Sunset":
                            elementOptions.Add("Sunset", index);
                            break;
                        case "panel_Sunrise":
                            elementOptions.Add("Sunrise", index);
                            break;
                        case "panel_Sunset_Sunrise":
                            elementOptions.Add("Sunset_Sunrise", index);
                            break;
                        case "panel_Pointer":
                            elementOptions.Add("Pointer", index);
                            break;
                        case "panel_Circle_Scale":
                            elementOptions.Add("Circle_Scale", index);
                            break;
                        case "panel_Linear_Scale":
                            elementOptions.Add("Linear_Scale", index);
                            break;
                        case "panel_Text_CityName":
                            elementOptions.Add("CityName", index);
                            break;
                        case "panel_Icon":
                            elementOptions.Add("Icon", index);
                            break;
                    } 
                    index++;
                }
            }
            return elementOptions;
        }

        /// <summary>Устанавливаем порядок опций в элементе</summary>
        public void SetVisibilityOptions(List<string> elementOptions)
        {
            if (elementOptions == null) elementOptions = new List<string>();
            panel_Images.Visible = elementOptions.Contains("Images") ? true : false;
            panel_Segments.Visible = elementOptions.Contains("Segments") ? true : false;
            panel_Number.Visible = elementOptions.Contains("Number") ? true : false;
            panel_Number_Target.Visible = elementOptions.Contains("Number_target") ? true : false;
            panel_Number_Min.Visible = elementOptions.Contains("Number_min") ? true : false;
            panel_Number_Max.Visible = elementOptions.Contains("Number_max") ? true : false;
            panel_Sunset.Visible = elementOptions.Contains("Sunset") ? true : false;
            panel_Sunrise.Visible = elementOptions.Contains("Sunrise") ? true : false;
            panel_Sunset_Sunrise.Visible = elementOptions.Contains("Sunset_Sunrise") ? true : false;
            panel_Pointer.Visible = elementOptions.Contains("Pointer") ? true : false;
            panel_Circle_Scale.Visible = elementOptions.Contains("Circle_scale") ? true : false;
            panel_Linear_Scale.Visible = elementOptions.Contains("Linear_scale") ? true : false;
            panel_Text_CityName.Visible = elementOptions.Contains("CityName") ? true : false;
            panel_Icon.Visible = elementOptions.Contains("Icon") ? true : false;

            //for (int i = 0; i < elementOptions.Count; i++)
            //{
            //    switch (elementOptions[i])
            //    {
            //        case "Images":
            //            panel_Images.Visible = true;
            //            break;
            //        case "Segments":
            //            panel_Segments.Visible = true;
            //            break;
            //        case "Number":
            //            panel_Number.Visible = true;
            //            break;
            //        case "Number_Min":
            //            panel_Number_Min.Visible = true;
            //            break;
            //        case "Number_Max":
            //            panel_Number_Max.Visible = true;
            //            break;
            //        case "Sunset":
            //            panel_Sunset.Visible = true;
            //            break;
            //        case "Sunrise":
            //            panel_Sunrise.Visible = true;
            //            break;
            //        case "Sunset_Sunrise":
            //            panel_Sunset_Sunrise.Visible = true;
            //            break;
            //        case "Pointer":
            //            panel_Pointer.Visible = true;
            //            break;
            //        case "Circle_Scale":
            //            panel_Circle_Scale.Visible = true;
            //            break;
            //        case "Linear_Scale":
            //            panel_Linear_Scale.Visible = true;
            //            break;
            //        case "CityName":
            //            panel_Text_CityName.Visible = true;
            //            break;
            //        case "Icon":
            //            panel_Icon.Visible = true;
            //            break;
            //    }
            //}

            if (tabControl1.SelectedTab.Name == "tabPageElementSettings")
            {
                tabControl1.Height = (int)(panel3.Location.Y + panel3.Height + 26 * currentDPI);
            }
            else
            {
                tabControl1.Height = (int)(462 * currentDPI);
            }
        }

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_un_select_image.Items.Clear();
            comboBox_select_image.Items.Clear();
            comboBox_fg_mask.Items.Clear();
            comboBox_mask.Items.Clear();
            comboBox_tip.Items.Clear();
            comboBox_Preview_image.Items.Clear();

            comboBox_un_select_image.Items.AddRange(ListImages.ToArray());
            comboBox_select_image.Items.AddRange(ListImages.ToArray());
            comboBox_fg_mask.Items.AddRange(ListImages.ToArray());
            comboBox_mask.Items.AddRange(ListImages.ToArray());
            comboBox_tip.Items.AddRange(ListImages.ToArray());
            comboBox_Preview_image.Items.AddRange(ListImages.ToArray());

            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_un_select_image.DropDownHeight = 1;
                comboBox_select_image.DropDownHeight = 1;
                comboBox_fg_mask.DropDownHeight = 1;
                comboBox_mask.DropDownHeight = 1;
                comboBox_tip.DropDownHeight = 1;
                comboBox_Preview_image.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_un_select_image.DropDownHeight = 35 * count + 1;
                comboBox_select_image.DropDownHeight = 35 * count + 1;
                comboBox_fg_mask.DropDownHeight = 35 * count + 1;
                comboBox_mask.DropDownHeight = 35 * count + 1;
                comboBox_tip.DropDownHeight = 35 * count + 1;
                comboBox_Preview_image.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_un_select_image.DropDownHeight = 106;
                comboBox_select_image.DropDownHeight = 106;
                comboBox_fg_mask.DropDownHeight = 106;
                comboBox_mask.DropDownHeight = 106;
                comboBox_tip.DropDownHeight = 106;
                comboBox_Preview_image.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;
            comboBox_select_zone.Items.Clear();
            comboBox_select_element.Items.Clear();
            button_elementAdd.Enabled = false;
            button_zoneDel.Enabled = false;
            button_PreviewAdd.Enabled = false;
            //button_PreviewRefresh.Enabled = false;

            comboBox_un_select_image.Text = null;
            comboBox_select_image.Text = null;
            comboBox_fg_mask.Text = null;
            comboBox_mask.Text = null;
            comboBox_tip.Text = null;
            comboBox_Preview_image.Text = null;

            numericUpDown_tipX.Value = 0;
            numericUpDown_tipY.Value = 0;
            numericUpDown_tips_width.Value = 0;
            numericUpDown_tips_margin.Value = 0;

            numericUpDown_zone_X.Value = 0;
            numericUpDown_zone_Y.Value = 0;
            numericUpDown_zone_W.Value = 100;
            numericUpDown_zone_H.Value = 100;

            checkBox_showInAOD.Checked = false;
            checkBox_edit_mode.Checked = false;

            //visibilityElement = true;
            //button_collapse.Visible = false;
            Collapse = false;
            SettingsElementClear();

            setValue = false;
        }

        public void SettingsElementClear()
        {
            bool setValueTemp = setValue;
            setValue = true;

            Dictionary<int, string> elementOptions = new Dictionary<int, string>();
            elementOptions.Add(1, "Icon");
            elementOptions.Add(2, "Linear_Scale");
            elementOptions.Add(3, "Circle_Scale");
            elementOptions.Add(4, "CityName");
            elementOptions.Add(5, "Pointer");
            elementOptions.Add(6, "Sunset_Sunrise");
            elementOptions.Add(7, "Sunrise");
            elementOptions.Add(8, "Sunset");
            elementOptions.Add(9, "Number_Max");
            elementOptions.Add(10, "Number_Min");
            elementOptions.Add(11, "Number_Target");
            elementOptions.Add(12, "Number");
            elementOptions.Add(13, "Segments");
            elementOptions.Add(14, "Images");
            SetOptionsPosition(elementOptions);

            checkBox_Images.Checked = false;
            checkBox_Segments.Checked = false;
            checkBox_Number.Checked = false;
            checkBox_Number_Target.Checked = false;
            checkBox_Number_Min.Checked = false;
            checkBox_Number_Max.Checked = false;
            checkBox_Sunset.Checked = false;
            checkBox_Sunrise.Checked = false;
            checkBox_Sunset_Sunrise.Checked = false;
            checkBox_Pointer.Checked = false;
            checkBox_Circle_Scale.Checked = false;
            checkBox_Linear_Scale.Checked = false;
            checkBox_Text_CityName.Checked = false;
            checkBox_Icon.Checked = false;

            button_PreviewAdd.Enabled = false;
            //button_PreviewRefresh.Enabled = false;

            SetVisibilityOptions(null);
            comboBox_Preview_image.Text = null;

            setValue = setValueTemp;
        }

        public void SetElementsType(string type)
        {
            panel_Images.Visible = false;
            panel_Segments.Visible = false;
            panel_Number.Visible = false;
            panel_Number_Min.Visible = false;
            panel_Number_Max.Visible = false;
            panel_Sunset.Visible = false;
            panel_Sunrise.Visible = false;
            panel_Sunset_Sunrise.Visible = false;
            panel_Pointer.Visible = false;
            panel_Text_CityName.Visible = false;
            panel_Circle_Scale.Visible = false;
            panel_Linear_Scale.Visible = false;
            panel_Icon.Visible = false;

            switch (type)
            {
                case "ElementBattery":
                    panel_Images.Visible = true;
                    panel_Segments.Visible = true;
                    panel_Number.Visible = true;
                    panel_Number_Min.Visible = false;
                    panel_Number_Max.Visible = false;
                    panel_Sunset.Visible = false;
                    panel_Sunrise.Visible = false;
                    panel_Sunset_Sunrise.Visible = false;
                    panel_Pointer.Visible = true;
                    panel_Text_CityName.Visible = false;
                    panel_Circle_Scale.Visible = true;
                    panel_Linear_Scale.Visible = true;
                    panel_Icon.Visible = true;
                    break;
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
            if (ZoneValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ZoneValueChanged(this, eventArgs, comboBox_select_zone.SelectedIndex);
            }
        }

        private void numericUpDown_height_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.X < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.X - numericUpDown_zone_X.Value;
            }
        }

        private void numericUpDown_width_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.Y < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.Y - numericUpDown_zone_Y.Value;
            }
        }

        #endregion

        private void button_add_Click(object sender, EventArgs e)
        {
            if (ZoneAdd != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ZoneAdd(this, eventArgs, comboBox_select_zone.SelectedIndex);
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if (ZoneDel != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ZoneDel(this, eventArgs, comboBox_select_zone.SelectedIndex);
            }
        }

        private void UCtrl_EditableElemets_Opt_Load(object sender, EventArgs e)
        {
            button_PreviewAdd.Location = button_PreviewRefresh.Location;
        }

        private void button_elementAdd_Click(object sender, EventArgs e)
        {
            if (ElementAdd != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ElementAdd(this, eventArgs, comboBox_select_element.SelectedIndex);
            }
        }

        private void button_elementDel_Click(object sender, EventArgs e)
        {
            if (ElementDel != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ElementDel(this, eventArgs, comboBox_select_element.SelectedIndex);
            }
        }

        private void comboBox_select_element_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ElementIndexChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ElementIndexChanged(this, eventArgs, comboBox_select_element.SelectedIndex);
            }

            if (comboBox_select_element.SelectedIndex >= 0)
            {
                button_elementDel.Enabled = true;
                button_PreviewAdd.Enabled = true;
            }
            else
            {
                button_elementDel.Enabled = false;
                button_PreviewAdd.Enabled = false;
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Name == "tabPageElementSettings")
            {
                tabControl1.Height = (int)(panel3.Location.Y + panel3.Height + 26 * currentDPI);
                button_collapse.Visible = true;
            }
            else
            {
                tabControl1.Height = (int)(462 * currentDPI);
                button_collapse.Visible = false;
            }
        }

        private void panel3_LocationChanged(object sender, EventArgs e)
        {
            if (currentDPI == 0) return;
            //if (tabControl1.SelectedTab.Name == "tabPageElementSettings")
            //{
            //    tabControl1.Height = (int)(panel3.Location.Y + panel3.Height + 26 * currentDPI);
            //}
            //else
            //{
            //    tabControl1.Height = (int)(462 * currentDPI);
            //}
        }

        private void button_PreviewAdd_Click(object sender, EventArgs e)
        {
            if (PreviewElementAdd != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                PreviewElementAdd(this, eventArgs, comboBox_select_element.SelectedIndex);
            }
        }

        private void button_PreviewRefresh_Click(object sender, EventArgs e)
        {
            if (PreviewElementRefresh != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                PreviewElementRefresh(this, eventArgs, comboBox_select_element.SelectedIndex);
            }
        }

        private void button_collapse_Click(object sender, EventArgs e)
        {
            Collapse = !Collapse;
        }

        private void button_collapse_SizeChanged(object sender, EventArgs e)
        {
            Logger.WriteLine("* currentDPI = " + currentDPI.ToString());
            if (tabControl1.Height > 580) //if (tabControl1.Height > 580)
            {
                float currentDPI = tabControl1.Height / 462f;
                Logger.WriteLine("* currentDPI = " + currentDPI.ToString());
                //button_collapse.Image = (Image)(new Bitmap(button_collapse.Image,
                //    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));

                Control.ControlCollection controlCollection = tableLayoutPanel_element.Controls;
                for (int i = 0; i < controlCollection.Count; i++)
                {
                    string name = controlCollection[i].GetType().Name;
                    if (name == "Panel")
                    {
                        Control.ControlCollection panelCollection = controlCollection[i].Controls;
                        for (int j = 0; j < panelCollection.Count; j++)
                        {
                            string nameButton = panelCollection[j].GetType().Name;
                            if (nameButton == "Button")
                            {
                                Button btn = (Button)panelCollection[j];
                                btn.Image = (Image)(new Bitmap(btn.Image,
                                    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));
                            }
                        }
                    }
                }

                controlCollection = button_collapse.Controls;
                for (int i = 0; i < controlCollection.Count; i++)
                {
                    string name = controlCollection[i].GetType().Name;
                    if (name == "PictureBox")
                    {
                        PictureBox pb = (PictureBox)controlCollection[i];
                        pb.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }

            }
        }
    }

    static class Logger
    {
        //----------------------------------------------------------
        // Статический метод записи строки в файл лога без переноса
        //----------------------------------------------------------
        public static void Write(string text)
        {
            try
            {
                //using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Program log.txt", true))
                //{
                //    sw.Write(text);
                //}
            }
            catch (Exception)
            {
            }
        }

        //---------------------------------------------------------
        // Статический метод записи строки в файл лога с переносом
        //---------------------------------------------------------
        public static void WriteLine(string message)
        {
            try
            {
                //using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\EditableElemets_Opt log.txt", true))
                //{
                //    sw.WriteLine(String.Format("{0,-23} {1}", DateTime.Now.ToString() + ":", message));
                //}
            }
            catch (Exception)
            {
            }
        }
    }
}
