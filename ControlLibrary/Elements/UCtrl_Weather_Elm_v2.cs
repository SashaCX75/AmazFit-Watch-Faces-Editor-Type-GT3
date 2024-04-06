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
    public partial class UCtrl_Weather_Elm_v2 : UserControl
    {
        private bool setValue; // режим задания параметров
        bool highlight_images = false;

        bool highlight_number = false;
        bool highlight_number_font = false;
        bool highlight_text_rotation = false;
        bool highlight_text_circle = false;

        bool highlight_number_min = false;
        bool highlight_number_min_font = false;
        bool highlight_text_min_rotation = false;
        bool highlight_text_min_circle = false;

        bool highlight_number_max = false;
        bool highlight_number_max_font = false;
        bool highlight_text_max_rotation = false;
        bool highlight_text_max_circle = false;

        bool highlight_number_max_min = false;
        bool highlight_number_max_min_font = false;
        bool highlight_text_max_min_rotation = false;
        bool highlight_text_max_min_circle = false;

        bool highlight_city_name = false;
        bool highlight_icon = false;

        bool visibility_elements = false; // развернут список с элементами
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        bool visibility_current = false; // развернут список с текущей температурой
        bool visibility_min = false; // развернут список с минимальной температурой
        bool visibility_max = false; // развернут список с максимальной температурой
        bool visibility_max_min = false; // развернут список с максимальной и минимальной температурой

        public int position = -1; // позиция в наборе элеменетов
        public string selectedElement; // название выбраного элемента

        Point cursorPos = new Point(0, 0);
        public UCtrl_Weather_Elm_v2()
        {
            InitializeComponent();
            setValue = false;

            button_ElementName.Controls.Add(pictureBox_Arrow_Right);
            button_ElementName.Controls.Add(pictureBox_Arrow_Down);
            button_ElementName.Controls.Add(pictureBox_NotShow);
            button_ElementName.Controls.Add(pictureBox_Show);
            button_ElementName.Controls.Add(pictureBox_Del);

            button_Current.Controls.Add(pictureBox_Arrow_Right_Current);
            button_Current.Controls.Add(pictureBox_Arrow_Down_Current);

            button_Min.Controls.Add(pictureBox_Arrow_Right_Min);
            button_Min.Controls.Add(pictureBox_Arrow_Down_Min);

            button_Max_Min.Controls.Add(pictureBox_Arrow_Right_Max_Min);
            button_Max_Min.Controls.Add(pictureBox_Arrow_Down_Max_Min);

            button_Max.Controls.Add(pictureBox_Arrow_Right_Max);
            button_Max.Controls.Add(pictureBox_Arrow_Down_Max);

            pictureBox_Arrow_Right.Location = new Point(1, 2);
            pictureBox_Arrow_Right.BackColor = Color.Transparent;

            pictureBox_Arrow_Down.Location = new Point(1, 2);
            pictureBox_Arrow_Down.BackColor = Color.Transparent;

            //pictureBox_Show.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6 , 2);
            pictureBox_NotShow.BackColor = Color.Transparent;
            pictureBox_Show.BackColor = Color.Transparent;

            //pictureBox_Del.Location = new Point(button_ElementName.Width - pictureBox_Del.Width - 8, 2);
            pictureBox_Del.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Current.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Current.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Current.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Current.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Min.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Min.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Min.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Min.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Max.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Max.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Max.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Max.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Max_Min.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Max_Min.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Max_Min.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Max_Min.BackColor = Color.Transparent;
        }

        [Browsable(true)]
        [Description("Происходит при изменении видимости элемента")]
        public event VisibleElementChangedHandler VisibleElementChanged;
        public delegate void VisibleElementChangedHandler(object sender, EventArgs eventArgs, bool visible);

        [Browsable(true)]
        [Description("Происходит при изменении видимости отдельного параметра в элементе")]
        public event VisibleOptionsChangedHandler VisibleOptionsChanged;
        public delegate void VisibleOptionsChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении положения параметров в элементе")]
        public event OptionsMovedHandler OptionsMoved;
        public delegate void OptionsMovedHandler(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions);

        //[Browsable(true)]
        //[Description("Происходит при изменении положения параметров в текущей температуре")]
        //public event CurrentOptionsMovedHandler CurrentOptionsMoved;
        //public delegate void CurrentOptionsMovedHandler(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions);

        //[Browsable(true)]
        //[Description("Происходит при изменении положения параметров в минимальной температуре")]
        //public event MinOptionsMovedHandler MinOptionsMoved;
        //public delegate void MinOptionsMovedHandler(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions);

        //[Browsable(true)]
        //[Description("Происходит при изменении положения параметров в максимальной температуре")]
        //public event MaxOptionsMovedHandler MaxOptionsMoved;
        //public delegate void MaxOptionsMovedHandler(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions);

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event SelectChangedHandler SelectChanged;
        public delegate void SelectChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при удалении элемента")]
        public event DelElementHandler DelElement;
        public delegate void DelElementHandler(object sender, EventArgs eventArgs);

        private void button_ElementName_Click(object sender, EventArgs e)
        {
            visibility_elements = !visibility_elements;
            tableLayoutPanel1.Visible = visibility_elements;
            pictureBox_Arrow_Down.Visible = visibility_elements;
            pictureBox_Arrow_Right.Visible = !visibility_elements;
        }

        private void button_Current_Click(object sender, EventArgs e)
        {
            visibility_current = !visibility_current;
            tableLayoutPanel_Current.Visible = visibility_current;
            pictureBox_Arrow_Down_Current.Visible = visibility_current;
            pictureBox_Arrow_Right_Current.Visible = !visibility_current;
        }

        private void button_Min_Click(object sender, EventArgs e)
        {
            visibility_min = !visibility_min;
            tableLayoutPanel_Min.Visible = visibility_min;
            pictureBox_Arrow_Down_Min.Visible = visibility_min;
            pictureBox_Arrow_Right_Min.Visible = !visibility_min;
        }

        private void button_Max_Click(object sender, EventArgs e)
        {
            visibility_max = !visibility_max;
            tableLayoutPanel_Max.Visible = visibility_max;
            pictureBox_Arrow_Down_Max.Visible = visibility_max;
            pictureBox_Arrow_Right_Max.Visible = !visibility_max;
        }

        private void button_Max_Min_Click(object sender, EventArgs e)
        {
            visibility_max_min = !visibility_max_min;
            tableLayoutPanel_Max_Min.Visible = visibility_max_min;
            pictureBox_Arrow_Down_Max_Min.Visible = visibility_max_min;
            pictureBox_Arrow_Right_Max_Min.Visible = !visibility_max_min;
        }

        public void ResetHighlightState()
        {
            selectedElement = "";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            //highlight_linear_scale = false;
            highlight_city_name = false;
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

            if (highlight_number)
            {
                panel_Number.BackColor = SystemColors.ActiveCaption;
                button_Number_Current.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Current.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number.BackColor = SystemColors.Control;
                button_Number_Current.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Current.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_font)
            {
                panel_Number_Font.BackColor = SystemColors.ActiveCaption;
                button_Number_Current_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Current_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Font.BackColor = SystemColors.Control;
                button_Number_Current_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Current_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_rotation)
            {
                panel_Text_rotation.BackColor = SystemColors.ActiveCaption;
                button_Text_Current_rotation.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Current_rotation.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_rotation.BackColor = SystemColors.Control;
                button_Text_Current_rotation.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Current_rotation.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_circle)
            {
                panel_Text_circle.BackColor = SystemColors.ActiveCaption;
                button_Text_Current_circle.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Current_circle.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_circle.BackColor = SystemColors.Control;
                button_Text_Current_circle.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Current_circle.FlatAppearance.MouseDownBackColor = SystemColors.Control;
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

            if (highlight_number_min_font)
            {
                panel_Number_Min_Font.BackColor = SystemColors.ActiveCaption;
                button_Number_Min_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Min_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Min_Font.BackColor = SystemColors.Control;
                button_Number_Min_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Min_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_min_rotation)
            {
                panel_Text_Min_rotation.BackColor = SystemColors.ActiveCaption;
                button_Text_Min_rotation.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Min_rotation.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_Min_rotation.BackColor = SystemColors.Control;
                button_Text_Min_rotation.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Min_rotation.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_min_circle)
            {
                panel_Text_Min_circle.BackColor = SystemColors.ActiveCaption;
                button_Text_Min_circle.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Min_circle.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_Min_circle.BackColor = SystemColors.Control;
                button_Text_Min_circle.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Min_circle.FlatAppearance.MouseDownBackColor = SystemColors.Control;
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

            if (highlight_number_max_font)
            {
                panel_Number_Max_Font.BackColor = SystemColors.ActiveCaption;
                button_Number_Max_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Max_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Max_Font.BackColor = SystemColors.Control;
                button_Number_Max_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Max_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_max_rotation)
            {
                panel_Text_Max_rotation.BackColor = SystemColors.ActiveCaption;
                button_Text_Max_rotation.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Max_rotation.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_Max_rotation.BackColor = SystemColors.Control;
                button_Text_Max_rotation.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Max_rotation.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_max_circle)
            {
                panel_Text_Max_circle.BackColor = SystemColors.ActiveCaption;
                button_Text_Max_circle.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Max_circle.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_Max_circle.BackColor = SystemColors.Control;
                button_Text_Max_circle.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Max_circle.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_max_min)
            {
                panel_Number_Max_Min.BackColor = SystemColors.ActiveCaption;
                button_Number_Max_Min.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Max_Min.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Max_Min.BackColor = SystemColors.Control;
                button_Number_Max_Min.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Max_Min.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_max_min_font)
            {
                panel_Number_Max_Min_Font.BackColor = SystemColors.ActiveCaption;
                button_Number_Max_Min_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Max_Min_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Max_Min_Font.BackColor = SystemColors.Control;
                button_Number_Max_Min_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Max_Min_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_max_min_rotation)
            {
                panel_Text_Max_Min_rotation.BackColor = SystemColors.ActiveCaption;
                button_Text_Max_Min_rotation.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Max_Min_rotation.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_Max_Min_rotation.BackColor = SystemColors.Control;
                button_Text_Max_Min_rotation.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Max_Min_rotation.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_text_max_min_circle)
            {
                panel_Text_Max_Min_circle.BackColor = SystemColors.ActiveCaption;
                button_Text_Max_Min_circle.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Text_Max_Min_circle.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Text_Max_Min_circle.BackColor = SystemColors.Control;
                button_Text_Max_Min_circle.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Text_Max_Min_circle.FlatAppearance.MouseDownBackColor = SystemColors.Control;
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

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        /////// Current
        private void panel_Number_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Current";

            highlight_images = false;

            highlight_number = true;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Current_Font";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = true;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_rotation_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Current_rotation";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = true;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_circle_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Current_circle";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = true;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        ///////// Min
        private void panel_Number_Min_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Min";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = true;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Min_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Min_Font";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = true;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_Min_rotation_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Min_rotation";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = true;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_Min_circle_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Min_circle";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = true;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        ////// Max
        private void panel_Number_Max_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Max";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = true;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Max_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Max_Font";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = true;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_Max_rotation_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Max_rotation";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = true;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_Max_circle_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Max_circle";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = true;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        ////// Max-Min
        private void panel_Number_Max_Min_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Max_Min";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = true;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Max_Min_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Max_Min_Font";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = true;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_Max_Min_rotation_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Max_Min_rotation";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = true;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Text_Max_Min_circle_Click(object sender, EventArgs e)
        {
            selectedElement = "Text_Max_Min_circle";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = true;

            highlight_city_name = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        ////////
        private void panel_Text_CityName_Click(object sender, EventArgs e)
        {
            selectedElement = "CityName";

            highlight_images = false;

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

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

            highlight_number = false;
            highlight_number_min = false;
            highlight_number_max = false;
            highlight_number_max_min = false;

            highlight_number_font = false;
            highlight_number_min_font = false;
            highlight_number_max_font = false;
            highlight_number_max_min_font = false;

            highlight_text_rotation = false;
            highlight_text_min_rotation = false;
            highlight_text_max_rotation = false;
            highlight_text_max_min_rotation = false;

            highlight_text_circle = false;
            highlight_text_min_circle = false;
            highlight_text_max_circle = false;
            highlight_text_max_min_circle = false;

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

        private void tableLayoutPanel1_DragDrop(object sender, DragEventArgs e)
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

        private void tableLayoutPanel1_DragOver(object sender, DragEventArgs e)
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
            if (draggedPanel == null) 
                return;

            Point pt = tableLayoutPanel1.PointToClient(new Point(e.X, e.Y));
            Control control = tableLayoutPanel1.GetChildAtPoint(pt);

            if (control != null)
            {
                TableLayoutPanelCellPosition pos = tableLayoutPanel1.GetPositionFromControl(control);
                //if (pos.Column < 0 && pos.Row < 0)
                //{
                //    control = control.Parent;
                //}
                TableLayoutPanelCellPosition posOld = tableLayoutPanel1.GetPositionFromControl(draggedPanel);
                if (pos != posOld && pos.Row < posOld.Row)
                {
                    if (posOld.Row - pos.Row == 1)
                    {
                        if (pt.Y < control.Location.Y + draggedPanel.Height * 0.4)
                        {
                            tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                            tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        }
                    }
                    else
                    {
                        for (int rowIndex = pos.Row; rowIndex < posOld.Row; rowIndex++)
                        {
                            Control controlOld = tableLayoutPanel1.GetControlFromPosition(0, rowIndex);
                            Control controlNew = tableLayoutPanel1.GetControlFromPosition(0, rowIndex + 1);

                            tableLayoutPanel1.SetRow(controlNew, rowIndex);
                            tableLayoutPanel1.SetRow(controlOld, rowIndex + 1);
                        }
                    }

                }
                if (pos != posOld && pos.Row > posOld.Row)
                {
                    if (pt.Y > control.Location.Y + control.Height * 0.6)
                    {
                        if (pos.Row - posOld.Row == 1)
                        {
                            tableLayoutPanel1.SetRow(control, pos.Row - 1);
                            tableLayoutPanel1.SetRow(draggedPanel, pos.Row); 
                        }
                        else
                        {
                            for (int rowIndex = posOld.Row; rowIndex < pos.Row; rowIndex++)
                            {
                                Control controlOld = tableLayoutPanel1.GetControlFromPosition(0, rowIndex);
                                Control controlNew = tableLayoutPanel1.GetControlFromPosition(0, rowIndex + 1);

                                tableLayoutPanel1.SetRow(controlNew, rowIndex);
                                tableLayoutPanel1.SetRow(controlOld, rowIndex + 1);
                            }
                        }
                    }
                }
                draggedPanel.Tag = null;
            }
        }

        private void button_ElementName_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void button_ElementName_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        private void button_ElementName_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #endregion

        private void button_ElementName_SizeChanged(object sender, EventArgs e)
        {
            pictureBox_NotShow.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6, 2);
            pictureBox_Show.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6, 2);

            pictureBox_Del.Location = new Point(button_ElementName.Width - pictureBox_Del.Width - 4, 2);

            //if (tableLayoutPanel1.Height > 490)
            if (tableLayoutPanel1.Height > 745)
            {
                float currentDPI = tableLayoutPanel1.Height / 596f;
                button_ElementName.Image = (Image)(new Bitmap(button_ElementName.Image,
                    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));

                Control.ControlCollection controlCollection = tableLayoutPanel1.Controls;
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

                controlCollection = button_ElementName.Controls;
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

        private void pictureBox_Show_Click(object sender, EventArgs e)
        {
            visibilityElement = false;
            pictureBox_Show.Visible = false;
            pictureBox_NotShow.Visible = true;
            SetColorActive();

            if (VisibleElementChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleElementChanged(this, eventArgs, visibilityElement);
            }
        }

        private void pictureBox_NotShow_Click(object sender, EventArgs e)
        {
            visibilityElement = true;
            pictureBox_Show.Visible = true;
            pictureBox_NotShow.Visible = false;
            SetColorActive();

            if (VisibleElementChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleElementChanged(this, eventArgs, visibilityElement);
            }
        }

        private void pictureBox_Del_Click(object sender, EventArgs e)
        {
            if (DelElement != null)
            {
                EventArgs eventArgs = new EventArgs();
                DelElement(this, eventArgs);
            }
        }

        private void checkBox_Elements_CheckedChanged(object sender, EventArgs e)
        {
            if (VisibleOptionsChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleOptionsChanged(sender, eventArgs);
            }
        }

        /// <summary>Устанавливаем статус видимости для всего элемента</summary>
        public void SetVisibilityElementStatus(bool status)
        {
            visibilityElement = status;
            pictureBox_NotShow.Visible = !visibilityElement;
            pictureBox_Show.Visible = visibilityElement;
            SetColorActive();

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
                case "Number":
                    checkBox_Number_Current.Checked = status;
                    break;
                case "Number_Font":
                    checkBox_Number_Current_Font.Checked = status;
                    break;
                case "Text_rotation":
                    checkBox_Text_Current_rotation.Checked = status;
                    break;
                case "Text_circle":
                    checkBox_Text_Current_circle.Checked = status;
                    break;
                case "Number_Min":
                    checkBox_Number_Min.Checked = status;
                    break;
                case "Number_Min_Font":
                    checkBox_Number_Min_Font.Checked = status;
                    break;
                case "Text_Min_rotation":
                    checkBox_Text_Min_rotation.Checked = status;
                    break;
                case "Text_Min_circle":
                    checkBox_Text_Min_circle.Checked = status;
                    break;
                case "Number_Max":
                    checkBox_Number_Max.Checked = status;
                    break;
                case "Number_Max_Font":
                    checkBox_Number_Max_Font.Checked = status;
                    break;
                case "Text_Max_rotation":
                    checkBox_Text_Max_rotation.Checked = status;
                    break;
                case "Text_Max_circle":
                    checkBox_Text_Max_circle.Checked = status;
                    break;
                case "Number_Max_Min":
                    checkBox_Number_Max_Min.Checked = status;
                    break;
                case "Number_Max_Min_Font":
                    checkBox_Number_Max_Min_Font.Checked = status;
                    break;
                case "Text_Max_Min_rotation":
                    checkBox_Text_Max_Min_rotation.Checked = status;
                    break;
                case "Text_Max_Min_circle":
                    checkBox_Text_Max_Min_circle.Checked = status;
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
            int elementCount = tableLayoutPanel1.RowCount;
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
                        case "Group_Current":
                            panel = panel_Current;
                            break;
                        case "Group_Min":
                            panel = panel_Min;
                            break;
                        case "Group_Max":
                            panel = panel_Max;
                            break;
                        case "Group_Max_Min":
                            panel = panel_Max_Min;
                            break;
                            //case "Number":
                            //    panel = panel_Number;
                            //    break;
                            //case "Number_Font":
                            //    panel = panel_Number_Font;
                            //    break;
                            //case "Text_rotation":
                            //    panel = panel_Text_rotation;
                            //    break;
                            //case "Text_circle":
                            //    panel = panel_Text_circle;
                            //    break;
                            //case "Number_Min":
                            //    panel = panel_Number_Min;
                            //    break;
                            //case "Number_Min_Font":
                            //    panel = panel_Number_Min_Font;
                            //    break;
                            //case "Text_Min_rotation":
                            //    panel = panel_Text_Min_rotation;
                            //    break;
                            //case "Text_Min_circle":
                            //    panel = panel_Text_Min_circle;
                            //    break;
                            //case "Number_Max":
                            //    panel = panel_Number_Max;
                            //    break;
                            //case "Number_Max_Font":
                            //    panel = panel_Number_Max_Font;
                            //    break;
                            //case "Text_Max_rotation":
                            //    panel = panel_Text_Max_rotation;
                            //    break;
                            //case "Text_Max_circle":
                            //    panel = panel_Text_Max_circle;
                            //    break;
                            //case "Number_Max_Min":
                            //    panel = panel_Number_Max_Min;
                            //    break;
                            //case "Number_Max_Min_Font":
                            //    panel = panel_Number_Max_Min_Font;
                            //    break;
                            //case "Text_Max_Min_rotation":
                            //    panel = panel_Text_Max_Min_rotation;
                            //    break;
                            //case "Text_Max_Min_circle":
                            //    panel = panel_Text_Max_Min_circle;
                            //break;
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
                int realPos = tableLayoutPanel1.GetRow(panel);
                if (realPos == position)
                    continue;
                if (realPos < position)
                {
                    for (int i = realPos; i < position; i++)
                    {
                        Control panel2 = tableLayoutPanel1.GetControlFromPosition(0, i + 1);
                        if (panel2 == null) return;
                        tableLayoutPanel1.SetRow(panel2, i);
                        tableLayoutPanel1.SetRow(panel, i + 1);
                    }
                }
                else
                {
                    for (int i = realPos; i > position; i--)
                    {
                        Control panel2 = tableLayoutPanel1.GetControlFromPosition(0, i - 1);
                        if (panel2 == null)
                            return;
                        tableLayoutPanel1.SetRow(panel, i - 1);
                        tableLayoutPanel1.SetRow(panel2, i);
                    }
                }
            }
        }

        /// <summary>Получаем порядок опций в элементе</summary>
        public Dictionary<string, int> GetOptionsPosition()
        {
            Dictionary<string, int> elementOptions = new Dictionary<string, int>();
            int count = tableLayoutPanel1.RowCount;
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                Control panel = tableLayoutPanel1.GetControlFromPosition(0, i);
                switch (panel.Name)
                {
                    case "panel_Images":
                        elementOptions.Add("Images", count - i);
                        break;
                    case "panel_Current":
                        elementOptions.Add("Group_Current", count - i);
                        break;
                    case "panel_Min":
                        elementOptions.Add("Group_Min", count - i);
                        break;
                    case "panel_Max":
                        elementOptions.Add("Group_Min", count - i);
                        break;
                    case "panel_Max_Min":
                        elementOptions.Add("Group_Min", count - i);
                        break;
                    //case "panel_Number":
                    //    elementOptions.Add("Number", count - i);
                    //    break;
                    //case "panel_Number_Font":
                    //    elementOptions.Add("Number_Font", count - i);
                    //    break;
                    //case "panel_Text_rotation":
                    //    elementOptions.Add("Text_rotation", count - i);
                    //    break;
                    //case "panel_Text_circle":
                    //    elementOptions.Add("Text_circle", count - i);
                    //    break;
                    //case "panel_Number_Min":
                    //    elementOptions.Add("Number_Min", count - i);
                    //    break;
                    //case "panel_Number_Min_Font":
                    //    elementOptions.Add("Number_Min_Font", count - i);
                    //    break;
                    //case "panel_Text_Min_rotation":
                    //    elementOptions.Add("Text_Min_rotation", count - i);
                    //    break;
                    //case "panel_Text_Min_circle":
                    //    elementOptions.Add("Text_Min_circle", count - i);
                    //    break;
                    //case "panel_Number_Max":
                    //    elementOptions.Add("Number_Max", count - i);
                    //    break;
                    //case "panel_Number_Max_Font":
                    //    elementOptions.Add("Number_Max_Font", count - i);
                    //    break;
                    //case "panel_Text_Max_rotation":
                    //    elementOptions.Add("Text_Max_rotation", count - i);
                    //    break;
                    //case "panel_Text_Max_circle":
                    //    elementOptions.Add("Text_Max_circle", count - i);
                    //    break;
                    //case "panel_Number_Max_Min":
                    //    elementOptions.Add("Number_Max_Min", count - i);
                    //    break;
                    //case "panel_Number_Max_Min_Font":
                    //    elementOptions.Add("Number_Max_Min_Font", count - i);
                    //    break;
                    //case "panel_Text_Max_Min_rotation":
                    //    elementOptions.Add("Text_Max_Min_rotation", count - i);
                    //    break;
                    //case "panel_Text_Max_Min_circle":
                    //    elementOptions.Add("Text_Max_Min_circle", count - i);
                    //    break;
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

        public void SettingsClear()
        {
            setValue = true;

            Dictionary<int, string> elementOptions = new Dictionary<int, string>();
            int index = 1;
            elementOptions.Add(index++, "Icon");
            elementOptions.Add(index++, "CityName");
            elementOptions.Add(index++, "Images");

            elementOptions.Add(index++, "Group_Max_Min");
            elementOptions.Add(index++, "Group_Max");
            elementOptions.Add(index++, "Group_Min");
            elementOptions.Add(index++, "Group_Current");

            //elementOptions.Add(index++, "Text_Max_Min_circle");
            //elementOptions.Add(index++, "Text_Max_Min_rotation");
            //elementOptions.Add(index++, "Number_Max_Min_Font");
            //elementOptions.Add(index++, "Number_Max_Min");

            //elementOptions.Add(index++, "Text_Max_circle");
            //elementOptions.Add(index++, "Text_Max_rotation");
            //elementOptions.Add(index++, "Number_Max_Font");
            //elementOptions.Add(index++, "Number_Max");

            //elementOptions.Add(index++, "Text_Min_circle");
            //elementOptions.Add(index++, "Text_Min_rotation");
            //elementOptions.Add(index++, "Number_Min_Font");
            //elementOptions.Add(index++, "Number_Min");

            //elementOptions.Add(index++, "Text_circle");
            //elementOptions.Add(index++, "Text_rotation");
            //elementOptions.Add(index++, "Number_Font");
            //elementOptions.Add(index++, "Number");

            //elementOptions.Add(index++, "Images");
            SetOptionsPosition(elementOptions);

            checkBox_Images.Checked = false;

            checkBox_Number_Current.Checked = false;
            checkBox_Number_Current_Font.Checked = false;
            checkBox_Text_Current_rotation.Checked = false;
            checkBox_Text_Current_circle.Checked = false;

            checkBox_Number_Min.Checked = false;
            checkBox_Number_Min_Font.Checked = false;
            checkBox_Text_Min_rotation.Checked = false;
            checkBox_Text_Min_circle.Checked = false;

            checkBox_Number_Max.Checked = false;
            checkBox_Number_Max_Font.Checked = false;
            checkBox_Text_Max_rotation.Checked = false;
            checkBox_Text_Max_circle.Checked = false;

            checkBox_Number_Max_Min.Checked = false;
            checkBox_Number_Max_Min_Font.Checked = false;
            checkBox_Text_Max_Min_rotation.Checked = false;
            checkBox_Text_Max_Min_circle.Checked = false;

            checkBox_Text_CityName.Checked = false;
            checkBox_Icon.Checked = false;

            visibility_elements = false;
            tableLayoutPanel1.Visible = visibility_elements;
            pictureBox_Arrow_Down.Visible = visibility_elements;
            pictureBox_Arrow_Right.Visible = !visibility_elements;

            visibility_current = false;
            tableLayoutPanel_Current.Visible = visibility_current;
            pictureBox_Arrow_Down_Current.Visible = visibility_current;
            pictureBox_Arrow_Right_Current.Visible = !visibility_current;

            visibility_min = false;
            tableLayoutPanel_Min.Visible = visibility_min;
            pictureBox_Arrow_Down_Min.Visible = visibility_min;
            pictureBox_Arrow_Right_Min.Visible = !visibility_min;

            visibility_max = false;
            tableLayoutPanel_Max.Visible = visibility_max;
            pictureBox_Arrow_Down_Max.Visible = visibility_max;
            pictureBox_Arrow_Right_Max.Visible = !visibility_max;

            visibilityElement = true;
            pictureBox_Show.Visible = visibilityElement;
            pictureBox_NotShow.Visible = !visibilityElement;
            SetColorActive();

            setValue = false;
        }

        private void SetColorActive()
        {
            if (visibilityElement)
            {
                button_ElementName.ForeColor = SystemColors.ControlText;
                button_ElementName.BackColor = SystemColors.Control;
            }
            else
            {
                button_ElementName.ForeColor = SystemColors.GrayText;
                button_ElementName.BackColor = SystemColors.ControlLight;
            }
        }
    }
}
