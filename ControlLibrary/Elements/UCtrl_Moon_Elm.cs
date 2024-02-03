﻿using System;
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
    public partial class UCtrl_Moon_Elm : UserControl
    {
        private bool setValue; // режим задания параметров
        bool highlight_images = false;
        //bool highlight_segments = false;
        bool highlight_pointer = false;
        bool highlight_sunset = false;
        bool highlight_sunrise = false;
        bool highlight_sunset_sunrise = false;
        bool highlight_sunset_font = false;
        //bool highlight_sunset_rotation = false;
        //bool highlight_sunset_circle = false;
        bool highlight_sunrise_font = false;
        //bool highlight_sunrise_rotation = false;
        //bool highlight_sunrise_circle = false;
        bool highlight_icon = false;

        bool visibility_elements = false; // развернут список с элементами
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        public int position = -1; // позиция в наборе элеменетов
        public string selectedElement; // название выбраного элемента

        Point cursorPos = new Point(0, 0);

        public UCtrl_Moon_Elm()
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

            highlight_images = false;
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
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

            //if (highlight_segments)
            //{
            //    panel_Segments.BackColor = SystemColors.ActiveCaption;
            //    button_Segments.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            //    button_Segments.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            //}
            //else
            //{
            //    panel_Segments.BackColor = SystemColors.Control;
            //    button_Segments.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            //    button_Segments.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            //}

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

            if (highlight_sunset_font)
            {
                panel_Sunset_Font.BackColor = SystemColors.ActiveCaption;
                button_Sunset_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Sunset_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Sunset_Font.BackColor = SystemColors.Control;
                button_Sunset_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Sunset_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            //if (highlight_sunset_rotation)
            //{
            //    panel_Sunset_rotation.BackColor = SystemColors.ActiveCaption;
            //    button_Sunset_rotation.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            //    button_Sunset_rotation.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            //}
            //else
            //{
            //    panel_Sunset_rotation.BackColor = SystemColors.Control;
            //    button_Sunset_rotation.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            //    button_Sunset_rotation.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            //}

            //if (highlight_sunrise_rotation)
            //{
            //    panel_Sunrise_rotation.BackColor = SystemColors.ActiveCaption;
            //    button_Sunrise_rotation.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            //    button_Sunrise_rotation.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            //}
            //else
            //{
            //    panel_Sunrise_rotation.BackColor = SystemColors.Control;
            //    button_Sunrise_rotation.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            //    button_Sunrise_rotation.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            //}

            if (highlight_sunrise_font)
            {
                panel_Sunrise_Font.BackColor = SystemColors.ActiveCaption;
                button_Sunrise_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Sunrise_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Sunrise_Font.BackColor = SystemColors.Control;
                button_Sunrise_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Sunrise_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            //if (highlight_sunset_circle)
            //{
            //    panel_Sunset_circle.BackColor = SystemColors.ActiveCaption;
            //    button_Sunset_circle.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            //    button_Sunset_circle.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            //}
            //else
            //{
            //    panel_Sunset_circle.BackColor = SystemColors.Control;
            //    button_Sunset_circle.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            //    button_Sunset_circle.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            //}

            //if (highlight_sunrise_circle)
            //{
            //    panel_Sunrise_circle.BackColor = SystemColors.ActiveCaption;
            //    button_Sunrise_circle.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            //    button_Sunrise_circle.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            //}
            //else
            //{
            //    panel_Sunrise_circle.BackColor = SystemColors.Control;
            //    button_Sunrise_circle.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            //    button_Sunrise_circle.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            //}

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
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        //private void panel_Segments_Click(object sender, EventArgs e)
        //{
        //    selectedElement = "Segments";

        //    highlight_images = false;
        //    highlight_segments = true;
        //    highlight_pointer = false;
        //    highlight_sunset = false;
        //    highlight_sunrise = false;
        //    highlight_sunset_sunrise = false;
        //    highlight_sunset_font = false;
        //    //highlight_sunset_rotation = false;
        //    //highlight_sunset_circle = false;
        //    highlight_sunrise_font = false;
        //    //highlight_sunrise_rotation = false;
        //    //highlight_sunrise_circle = false;
        //    highlight_icon = false;

        //    SelectElement();

        //    if (SelectChanged != null)
        //    {
        //        EventArgs eventArgs = new EventArgs();
        //        SelectChanged(this, eventArgs);
        //    }
        //}

        private void panel_Pointer_Click(object sender, EventArgs e)
        {
            selectedElement = "Pointer";

            highlight_images = false;
            //highlight_segments = false;
            highlight_pointer = true;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
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
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = true;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
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
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = false;
            highlight_sunrise = true;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
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
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = true;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Sunset_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Sunset_Font";

            highlight_images = false;
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = true;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        //private void panel_Sunset_rotation_Click(object sender, EventArgs e)
        //{
        //    selectedElement = "Sunset_rotation";

        //    highlight_images = false;
        //    highlight_segments = false;
        //    highlight_pointer = false;
        //    highlight_sunset = false;
        //    highlight_sunrise = false;
        //    highlight_sunset_sunrise = false;
        //    highlight_sunset_font = false;
        //    highlight_sunset_rotation = true;
        //    highlight_sunset_circle = false;
        //    highlight_sunrise_font = false;
        //    highlight_sunrise_rotation = false;
        //    highlight_sunrise_circle = false;
        //    highlight_icon = false;

        //    SelectElement();

        //    if (SelectChanged != null)
        //    {
        //        EventArgs eventArgs = new EventArgs();
        //        SelectChanged(this, eventArgs);
        //    }
        //}

        //private void panel_Sunset_circle_Click(object sender, EventArgs e)
        //{
        //    selectedElement = "Sunset_circle";

        //    highlight_images = false;
        //    highlight_segments = false;
        //    highlight_pointer = false;
        //    highlight_sunset = false;
        //    highlight_sunrise = false;
        //    highlight_sunset_sunrise = false;
        //    highlight_sunset_font = false;
        //    highlight_sunset_rotation = false;
        //    highlight_sunset_circle = true;
        //    highlight_sunrise_font = false;
        //    highlight_sunrise_rotation = false;
        //    highlight_sunrise_circle = false;
        //    highlight_icon = false;

        //    SelectElement();

        //    if (SelectChanged != null)
        //    {
        //        EventArgs eventArgs = new EventArgs();
        //        SelectChanged(this, eventArgs);
        //    }
        //}

        private void panel_Sunrise_Font_Click(object sender, EventArgs e)
        {
            selectedElement = "Sunrise_Font";

            highlight_images = false;
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = true;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
            highlight_icon = false;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        //private void panel_Sunrise_rotation_Click(object sender, EventArgs e)
        //{
        //    selectedElement = "Sunrise_rotation";

        //    highlight_images = false;
        //    highlight_segments = false;
        //    highlight_pointer = false;
        //    highlight_sunset = false;
        //    highlight_sunrise = false;
        //    highlight_sunset_sunrise = false;
        //    highlight_sunset_font = false;
        //    highlight_sunset_rotation = false;
        //    highlight_sunset_circle = false;
        //    highlight_sunrise_font = false;
        //    highlight_sunrise_rotation = true;
        //    highlight_sunrise_circle = false;
        //    highlight_icon = false;

        //    SelectElement();

        //    if (SelectChanged != null)
        //    {
        //        EventArgs eventArgs = new EventArgs();
        //        SelectChanged(this, eventArgs);
        //    }
        //}

        //private void panel_Sunrise_circle_Click(object sender, EventArgs e)
        //{
        //    selectedElement = "Sunrise_circle";

        //    highlight_images = false;
        //    highlight_segments = false;
        //    highlight_pointer = false;
        //    highlight_sunset = false;
        //    highlight_sunrise = false;
        //    highlight_sunset_sunrise = false;
        //    highlight_sunset_font = false;
        //    highlight_sunset_rotation = false;
        //    highlight_sunset_circle = false;
        //    highlight_sunrise_font = false;
        //    highlight_sunrise_rotation = false;
        //    highlight_sunrise_circle = true;
        //    highlight_icon = false;

        //    SelectElement();

        //    if (SelectChanged != null)
        //    {
        //        EventArgs eventArgs = new EventArgs();
        //        SelectChanged(this, eventArgs);
        //    }
        //}

        private void panel_Icon_Click(object sender, EventArgs e)
        {
            selectedElement = "Icon";

            highlight_images = false;
            //highlight_segments = false;
            highlight_pointer = false;
            highlight_sunset = false;
            highlight_sunrise = false;
            highlight_sunset_sunrise = false;
            highlight_sunset_font = false;
            //highlight_sunset_rotation = false;
            //highlight_sunset_circle = false;
            highlight_sunrise_font = false;
            //highlight_sunrise_rotation = false;
            //highlight_sunrise_circle = false;
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


            if (control != null)
            {
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

            if (tableLayoutPanel1.Height > 280)
            {
                float currentDPI = tableLayoutPanel1.Height / 226f;
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
                //case "Segments":
                //    checkBox_Segments.Checked = status;
                //    break;
                case "Pointer":
                    checkBox_Pointer.Checked = status;
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
                case "Sunset_Font":
                    checkBox_Sunset_Font.Checked = status;
                    break;
                case "Sunrise_Font":
                    checkBox_Sunrise_Font.Checked = status;
                    break;
                //case "Sunset_rotation":
                //    checkBox_Sunset_rotation.Checked = status;
                //    break;
                //case "Sunrise_rotation":
                //    checkBox_Sunrise_rotation.Checked = status;
                //    break;
                //case "Sunset_circle":
                //    checkBox_Sunset_circle.Checked = status;
                //    break;
                //case "Sunrise_circle":
                //    checkBox_Sunrise_circle.Checked = status;
                //    break;
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
                        //case "Segments":
                        //    panel = panel_Segments;
                        //    break;
                        case "Pointer":
                            panel = panel_Pointer;
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
                        case "Sunset_Font":
                            panel = panel_Sunset_Font;
                            break;
                        case "Sunrise_Font":
                            panel = panel_Sunrise_Font;
                            break;
                        //case "Sunset_rotation":
                        //    panel = panel_Sunset_rotation;
                        //    break;
                        //case "Sunrise_rotation":
                        //    panel = panel_Sunrise_rotation;
                        //    break;
                        //case "Sunset_circle":
                        //    panel = panel_Sunset_circle;
                        //    break;
                        //case "Sunrise_circle":
                        //    panel = panel_Sunrise_circle;
                        //    break;
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
                    //case "panel_Segments":
                    //    elementOptions.Add("Segments", count - i);
                    //    break;
                    case "panel_Pointer":
                        elementOptions.Add("Pointer", count - i);
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
                    case "panel_Sunset_Font":
                        elementOptions.Add("Sunset_Font", count - i);
                        break;
                    case "panel_Sunrise_Font":
                        elementOptions.Add("Sunrise_Font", count - i);
                        break;
                    //case "panel_Sunset_rotation":
                    //    elementOptions.Add("Sunset_rotation", count - i);
                    //    break;
                    //case "panel_Sunrise_rotation":
                    //    elementOptions.Add("Sunrise_rotation", count - i);
                    //    break;
                    //case "panel_Sunset_circle":
                    //    elementOptions.Add("Sunset_circle", count - i);
                    //    break;
                    //case "panel_Sunrise_circle":
                    //    elementOptions.Add("Sunrise_circle", count - i);
                    //    break;
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
            elementOptions.Add(index++, "Sunset_Sunrise");
            //elementOptions.Add(index++, "Sunrise_circle");
            //elementOptions.Add(index++, "Sunrise_rotation");
            elementOptions.Add(index++, "Sunrise_Font");
            elementOptions.Add(index++, "Sunrise");
            //elementOptions.Add(index++, "Sunset_circle");
            //elementOptions.Add(index++, "Sunset_rotation");
            elementOptions.Add(index++, "Sunset_Font");
            elementOptions.Add(index++, "Sunset");
            elementOptions.Add(index++, "Pointer");
            //elementOptions.Add(index++, "Segments");
            elementOptions.Add(index++, "Images");
            SetOptionsPosition(elementOptions);

            checkBox_Images.Checked = false;
            //checkBox_Segments.Checked = false;
            checkBox_Pointer.Checked = false;
            checkBox_Sunset.Checked = false;
            checkBox_Sunset_Font.Checked = false;
            //checkBox_Sunset_rotation.Checked = false;
            //checkBox_Sunset_circle.Checked = false;
            checkBox_Sunrise.Checked = false;
            checkBox_Sunrise_Font.Checked = false;
            //checkBox_Sunrise_rotation.Checked = false;
            //checkBox_Sunrise_circle.Checked = false;
            checkBox_Sunset_Sunrise.Checked = false;
            checkBox_Icon.Checked = false;

            visibility_elements = false;
            tableLayoutPanel1.Visible = visibility_elements;
            pictureBox_Arrow_Down.Visible = visibility_elements;
            pictureBox_Arrow_Right.Visible = !visibility_elements;

            visibilityElement = true;
            pictureBox_Show.Visible = visibilityElement;
            pictureBox_NotShow.Visible = !visibilityElement;

            setValue = false;
        }

    }
}
