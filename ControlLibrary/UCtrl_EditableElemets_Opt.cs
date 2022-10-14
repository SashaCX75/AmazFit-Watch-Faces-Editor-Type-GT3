using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_EditableElemets_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _EditableTimePointer;

        bool highlight_images = false;
        bool highlight_segments = false;
        bool highlight_pointer = false;
        bool highlight_number = false;
        bool highlight_number_min = false;
        bool highlight_number_max = false;
        bool highlight_sunset = false;
        bool highlight_sunrise = false;
        bool highlight_sunset_sunrise = false;
        bool highlight_city_name = false;
        bool highlight_circle_scale = false;
        bool highlight_linear_scale = false;
        bool highlight_icon = false;

        bool visibility_elements = false; // развернут список с элементами
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        public int position = -1; // позиция в наборе элеменетов
        public string selectedElement; // название выбраного элемента

        Point cursorPos = new Point(0, 0);

        public UCtrl_EditableElemets_Opt()
        {
            InitializeComponent();
            setValue = false;
        }

        /// <summary>Задает индекс выбраного набора стрелок</summary>
        public void SetZoneIndex(int index)
        {
            setValue = true;
            comboBox_select_zone.SelectedIndex = index;
            setValue = false;
        }

        /// <summary>Задает количество наборов стрелок в выпадающем списке</summary>
        public void SetZoneCount(int count)
        {
            setValue = true;
            comboBox_select_zone.Items.Clear();
            for (int i = 1; i < count + 1; i++)
            {
                comboBox_select_zone.Items.Add(i.ToString());
            }
            if (count >= 9) button_add.Enabled = false;
            else button_add.Enabled = true;
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

        [Browsable(true)]
        [Description("Происходит при изменении общих настроек редактируемых элементов")]
        public event EditableElementsChangedHandler EditableElementsChanged;
        public delegate void EditableElementsChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при изменении выбора редактируемых элементов")]
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
        [Description("Происходит при изменении редактируемой зоны")]
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
        [Description("Происходит при изменении редактируемой зоны")]
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


        public void ResetHighlightState()
        {
            selectedElement = "";

            highlight_images = false;
            highlight_segments = false;
            highlight_pointer = false;
            highlight_number = false;
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
            highlight_number = false;
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
            highlight_number = false;
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
            highlight_number = false;
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
            int count = tableLayoutPanel_element.RowCount;
            for (int i = 0; i < tableLayoutPanel_element.RowCount; i++)
            {
                Control panel = tableLayoutPanel_element.GetControlFromPosition(0, i);
                switch (panel.Name)
                {
                    case "panel_Images":
                        elementOptions.Add("Images", count - i);
                        break;
                    case "panel_Segments":
                        elementOptions.Add("Segments", count - i);
                        break;
                    case "panel_Number":
                        elementOptions.Add("Number", count - i);
                        break;
                    case "panel_Number_Min":
                        elementOptions.Add("Number_Min", count - i);
                        break;
                    case "panel_Number_Max":
                        elementOptions.Add("Number_Max", count - i);
                        break;
                    case "panel_Sunset":
                        elementOptions.Add("Sunset", count - i);
                        break;
                    case "panel_Sunrise":
                        elementOptions.Add("Sunrise", count - i);
                        break;
                    case "panel_Sunset_Sunrise":
                        elementOptions.Add("Sunset_Sunrise", count - i);
                        break;
                    case "panel_Pointer":
                        elementOptions.Add("Pointer", count - i);
                        break;
                    case "panel_Circle_Scale":
                        elementOptions.Add("Circle_Scale", count - i);
                        break;
                    case "panel_Linear_Scale":
                        elementOptions.Add("Linear_Scale", count - i);
                        break;
                    case "panel_Text_CityName":
                        elementOptions.Add("CityName", count - i);
                        break;
                    case "panel_Icon":
                        elementOptions.Add("Icon", count - i);
                        break;
                }
            }
            return elementOptions;
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
            elementOptions.Add(11, "Number");
            elementOptions.Add(12, "Segments");
            elementOptions.Add(13, "Images");
            SetOptionsPosition(elementOptions);

            checkBox_Images.Checked = false;
            checkBox_Segments.Checked = false;
            checkBox_Number.Checked = false;
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

            visibility_elements = false;
            tableLayoutPanel_element.Visible = visibility_elements;

            visibilityElement = true;

            setValue = setValueTemp;
        }
    }
}
