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
    public partial class UCtrl_Weather_FewDay_Elm : UserControl
    {
        private bool setValue; // режим задания параметров
        bool highlight_settings = false;
        bool highlight_images = false;
        bool highlight_diagram = false;
        bool highlight_number_max = false;
        bool highlight_number_max_font = false;
        bool highlight_number_min = false;
        bool highlight_number_min_font = false;
        bool highlight_number_max_min = false;
        bool highlight_number_max_min_font = false;
        bool highlight_number_average = false;
        bool highlight_number_average_font = false;
        bool highlight_images_dow = false;
        bool highlight_font_dow = false;
        bool highlight_icon = false;

        private bool Graph_use = false;

        bool visibility_elements = false; // развернут список с элементами
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        public int position = -1; // позиция в наборе элеменетов
        public string selectedElement; // название выбраного элемента

        Point cursorPos = new Point(0, 0);

        public UCtrl_Weather_FewDay_Elm()
        {
            InitializeComponent();
            setValue = false;

            button_ElementName.Controls.Add(pictureBox_Arrow_Right);
            button_ElementName.Controls.Add(pictureBox_Arrow_Down);
            button_ElementName.Controls.Add(pictureBox_NotShow);
            button_ElementName.Controls.Add(pictureBox_Show);
            button_ElementName.Controls.Add(pictureBox_Del);

            pictureBox_Arrow_Right.Location = new Point(1, 2);
            pictureBox_Arrow_Right.BackColor = Color.Transparent;

            pictureBox_Arrow_Down.Location = new Point(1, 2);
            pictureBox_Arrow_Down.BackColor = Color.Transparent;

            //pictureBox_Show.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6 , 2);
            pictureBox_NotShow.BackColor = Color.Transparent;
            pictureBox_Show.BackColor = Color.Transparent;

            //pictureBox_Del.Location = new Point(button_ElementName.Width - pictureBox_Del.Width - 8, 2);
            pictureBox_Del.BackColor = Color.Transparent;
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

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event SelectChangedHandler SelectChanged;
        public delegate void SelectChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при удалении элемента")]
        public event DelElementHandler DelElement;
        public delegate void DelElementHandler(object sender, EventArgs eventArgs);

        /// <summary>Возможность отображения графика температуры</summary>
        [Description("Возможность отображения графика температуры")]
        public virtual bool GraphUse
        {
            get
            {
                return Graph_use;
            }
            set
            {
                Graph_use = value;
                bool tempSate = setValue;
                setValue = true;
                panel_Diagram.Enabled = Graph_use;
                if (!Graph_use) checkBox_Diagram.Checked = false;
                setValue = tempSate;
            }
        }

        private void button_ElementName_Click(object sender, EventArgs e)
        {
            visibility_elements = !visibility_elements;
            tableLayoutPanel1.Visible = visibility_elements;
            pictureBox_Arrow_Down.Visible = visibility_elements;
            pictureBox_Arrow_Right.Visible = !visibility_elements;
        }

        public void ResetHighlightState()
        {
            selectedElement = "";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();
        }

        private void SelectElement()
        {
            if (highlight_settings)
            {
                panel_Settings.BackColor = SystemColors.ActiveCaption;
                button_Settings.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Settings.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Settings.BackColor = SystemColors.Control;
                button_Settings.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Settings.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

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

            if (highlight_diagram)
            {
                panel_Diagram.BackColor = SystemColors.ActiveCaption;
                button_Diagram.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Diagram.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Diagram.BackColor = SystemColors.Control;
                button_Diagram.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Diagram.FlatAppearance.MouseDownBackColor = SystemColors.Control;
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

            if (highlight_number_max_min)
            {
                panel_Number_MaxMin.BackColor = SystemColors.ActiveCaption;
                button_Number_MaxMin.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_MaxMin.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_MaxMin.BackColor = SystemColors.Control;
                button_Number_MaxMin.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_MaxMin.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_max_min_font)
            {
                panel_Number_MaxMin_Font.BackColor = SystemColors.ActiveCaption;
                button_Number_MaxMin_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_MaxMin_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_MaxMin_Font.BackColor = SystemColors.Control;
                button_Number_MaxMin_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_MaxMin_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }
            
            if (highlight_number_average)
            {
                panel_Number_Average.BackColor = SystemColors.ActiveCaption;
                button_Number_Average.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Average.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Average.BackColor = SystemColors.Control;
                button_Number_Average.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Average.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_number_average_font)
            {
                panel_Number_Average_Font.BackColor = SystemColors.ActiveCaption;
                button_Number_Average_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Number_Average_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Number_Average_Font.BackColor = SystemColors.Control;
                button_Number_Average_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Number_Average_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_images_dow)
            {
                panel_Images_DOW.BackColor = SystemColors.ActiveCaption;
                button_Images_DOW.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Images_DOW.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Images_DOW.BackColor = SystemColors.Control;
                button_Images_DOW.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Images_DOW.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_font_dow)
            {
                panel_Font_DOW.BackColor = SystemColors.ActiveCaption;
                button_Font_DOW.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Font_DOW.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Font_DOW.BackColor = SystemColors.Control;
                button_Font_DOW.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Font_DOW.FlatAppearance.MouseDownBackColor = SystemColors.Control;
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

        private void panel_Settings_Click(object sender, EventArgs e)
        {
            selectedElement = "Settings";

            highlight_settings = true;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Images_Click(object sender, EventArgs e)
        {
            selectedElement = "Images";

            highlight_settings = false;
            highlight_images = true;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Diagram_Click(object sender, EventArgs e)
        {
            selectedElement = "Diagram";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = true;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
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

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = true;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
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

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = true;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
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

            highlight_settings = false;
            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = true;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
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

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = true;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_MaxMin_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_MaxMin";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = true;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_MaxMin_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_MaxMin_Font";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = true;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Average_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Average";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = true;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Number_Average_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Number_Average_Font";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = true;
            highlight_images_dow = false;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Images_DOW_Click(object sender, EventArgs e)
        {
            selectedElement = "Images_DOW";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = true;
            highlight_font_dow = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Font_DOW_Click(object sender, EventArgs e)
        {
            selectedElement = "Font_DOW";

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = true;
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

            highlight_settings = false;
            highlight_images = false;
            highlight_diagram = false;
            highlight_number_max = false;
            highlight_number_max_font = false;
            highlight_number_min = false;
            highlight_number_min_font = false;
            highlight_number_max_min = false;
            highlight_number_max_min_font = false;
            highlight_number_average = false;
            highlight_number_average_font = false;
            highlight_images_dow = false;
            highlight_font_dow = false;
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
            if (draggedPanel == null) return;

            Point pt = tableLayoutPanel1.PointToClient(new Point(e.X, e.Y));
            Control control = tableLayoutPanel1.GetChildAtPoint(pt);

            if (control != null /*&& control.Name != "panel_Settings"*/)
            {
                if (control.Name == "panel_Settings")
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                else e.Effect = DragDropEffects.Move;

                var pos = tableLayoutPanel1.GetPositionFromControl(control);
                var posOld = tableLayoutPanel1.GetPositionFromControl(draggedPanel);
                if (pos != posOld && pos.Row < posOld.Row)
                {
                    if (pt.Y < control.Location.Y + draggedPanel.Height * 0.4)
                    {
                        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                        tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        //if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        //else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                    }
                }
                if (pos != posOld && pos.Row > posOld.Row)
                {
                    if (pt.Y > control.Location.Y + control.Height * 0.6)
                    {
                        tableLayoutPanel1.SetRow(control, pos.Row - 1);
                        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                        //if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        //else tableLayoutPanel1.SetRow(control, pos.Row - 1);
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

            if (tableLayoutPanel1.Height > 440)
            {
                float currentDPI = tableLayoutPanel1.Height / 351f;
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

        /// <summary>Устанавливаем статус видимости для всего элемента</summary>DayOfWeek_Images
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
                case "Diagram":
                    checkBox_Diagram.Checked = status;
                    break;
                case "Number_Max":
                    checkBox_Number_Max.Checked = status;
                    break;
                case "Number_Max_Font":
                    checkBox_Number_Max_Font.Checked = status;
                    break;
                case "Number_Min":
                    checkBox_Number_Min.Checked = status;
                    break;
                case "Number_Min_Font":
                    checkBox_Number_Min_Font.Checked = status;
                    break;
                case "Number_MaxMin":
                    checkBox_Number_MaxMin.Checked = status;
                    break;
                case "Number_MaxMin_Font":
                    checkBox_Number_MaxMin_Font.Checked = status;
                    break;
                case "Number_Average":
                    checkBox_Number_Average.Checked = status;
                    break;
                case "Number_Average_Font":
                    checkBox_Number_Average_Font.Checked = status;
                    break;
                case "Images_DOW":
                    checkBox_Images_DOW.Checked = status;
                    break;
                case "Font_DOW":
                    checkBox_Font_DOW.Checked = status;
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
                        case "Diagram":
                            panel = panel_Diagram;
                            break;
                        case "Number_Max":
                            panel = panel_Number_Max;
                            break;
                        case "Number_Max_Font":
                            panel = panel_Number_Max_Font;
                            break;
                        case "Number_Min":
                            panel = panel_Number_Min;
                            break;
                        case "Number_Min_Font":
                            panel = panel_Number_Min_Font;
                            break;
                        case "Number_MaxMin":
                            panel = panel_Number_MaxMin;
                            break;
                        case "Number_MaxMin_Font":
                            panel = panel_Number_MaxMin_Font;
                            break;
                        case "Number_Average":
                            panel = panel_Number_Average;
                            break;
                        case "Number_Average_Font":
                            panel = panel_Number_Average_Font;
                            break;
                        case "Images_DOW":
                            panel = panel_Images_DOW;
                            break;
                        case "Font_DOW":
                            panel = panel_Font_DOW;
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
                    case "panel_Diagram":
                        elementOptions.Add("Diagram", count - i);
                        break;
                    case "panel_Number_Max":
                        elementOptions.Add("Number_Max", count - i);
                        break;
                    case "panel_Number_Max_Font":
                        elementOptions.Add("Number_Max_Font", count - i);
                        break;
                    case "panel_Number_Min":
                        elementOptions.Add("Number_Min", count - i);
                        break;
                    case "panel_Number_Min_Font":
                        elementOptions.Add("Number_Min_Font", count - i);
                        break;
                    case "panel_Number_MaxMin":
                        elementOptions.Add("Number_MaxMin", count - i);
                        break;
                    case "panel_Number_MaxMin_Font":
                        elementOptions.Add("Number_MaxMin_Font", count - i);
                        break;
                    case "panel_Number_Average":
                        elementOptions.Add("Number_Average", count - i);
                        break;
                    case "panel_Number_Average_Font":
                        elementOptions.Add("Number_Average_Font", count - i);
                        break;
                    case "panel_Images_DOW":
                        elementOptions.Add("Images_DOW", count - i);
                        break;
                    case "panel_Font_DOW":
                        elementOptions.Add("Font_DOW", count - i);
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
            elementOptions.Add(index++, "Font_DOW");
            elementOptions.Add(index++, "Images_DOW");
            elementOptions.Add(index++, "Number_Average_Font");
            elementOptions.Add(index++, "Number_Average");
            elementOptions.Add(index++, "Number_MaxMin_Font");
            elementOptions.Add(index++, "Number_MaxMin");
            elementOptions.Add(index++, "Number_Min_Font");
            elementOptions.Add(index++, "Number_Min");
            elementOptions.Add(index++, "Number_Max_Font");
            elementOptions.Add(index++, "Number_Max");
            elementOptions.Add(index++, "Diagram");
            elementOptions.Add(index++, "Images");
            SetOptionsPosition(elementOptions);

            checkBox_Images.Checked = false;
            checkBox_Diagram.Checked = false;
            checkBox_Number_Max.Checked = false;
            checkBox_Number_Max_Font.Checked = false;
            checkBox_Number_Min.Checked = false;
            checkBox_Number_Min_Font.Checked = false;
            checkBox_Number_MaxMin.Checked = false;
            checkBox_Number_MaxMin_Font.Checked = false;
            checkBox_Number_Average.Checked = false;
            checkBox_Number_Average_Font.Checked = false;
            checkBox_Images_DOW.Checked = false;
            checkBox_Font_DOW.Checked = false;
            checkBox_Icon.Checked = false;

            GraphUse = true;

            visibility_elements = false;
            tableLayoutPanel1.Visible = visibility_elements;
            pictureBox_Arrow_Down.Visible = visibility_elements;
            pictureBox_Arrow_Right.Visible = !visibility_elements;

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
