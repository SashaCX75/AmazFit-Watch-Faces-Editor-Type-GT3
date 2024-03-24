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
    public partial class UCtrl_AnalogTime_Elm : UserControl
    {
        private bool setValue; // режим задания параметров
        bool highlight_hours = false;
        bool highlight_minutes = false;
        bool highlight_seconds = false;

        bool visibility_elements = false; // развернут список с элементами
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        public int position = -1; // позиция в наборе элеменетов
        public string selectedElement; // название выбраного элемента

        Point cursorPos = new Point(0, 0);

        public UCtrl_AnalogTime_Elm()
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

        public bool GetHighlightState()
        {
            bool highlight = highlight_hours || highlight_minutes || highlight_seconds;
            return highlight;
        }

        public void ResetHighlightState()
        {
            selectedElement = "";

            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = false;

            if (highlight_hours)
            {
                panel_Hours.BackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours.BackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes)
            {
                panel_Minutes.BackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes.BackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds)
            {
                panel_Seconds.BackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds.BackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }
        }

        private void panel_Hours_Click(object sender, EventArgs e)
        {
            selectedElement = "Hour";

            highlight_hours = true;
            highlight_minutes = false;
            highlight_seconds = false;

            if (highlight_hours)
            {
                panel_Hours.BackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours.BackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes)
            {
                panel_Minutes.BackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes.BackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds)
            {
                panel_Seconds.BackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds.BackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Minutes_Click(object sender, EventArgs e)
        {
            selectedElement = "Minute";

            highlight_hours = false;
            highlight_minutes = true;
            highlight_seconds = false;

            if (highlight_hours)
            {
                panel_Hours.BackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours.BackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes)
            {
                panel_Minutes.BackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes.BackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds)
            {
                panel_Seconds.BackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds.BackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Seconds_Click(object sender, EventArgs e)
        {
            selectedElement = "Second";

            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = true;

            if (highlight_hours)
            {
                panel_Hours.BackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours.BackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes)
            {
                panel_Minutes.BackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes.BackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds)
            {
                panel_Seconds.BackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds.BackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

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

            if (tableLayoutPanel1.Height > 100)
            {
                float currentDPI = tableLayoutPanel1.Height / 76f;
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
                case "Hour":
                    checkBox_Hours.Checked = status;
                    break;
                case "Minute":
                    checkBox_Minutes.Checked = status;
                    break;
                case "Second":
                    checkBox_Seconds.Checked = status;
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
                        case "Hour":
                            panel = panel_Hours;
                            break;
                        case "Minute":
                            panel = panel_Minutes;
                            break;
                        case "Second":
                            panel = panel_Seconds;
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
                    case "panel_Hours":
                        elementOptions.Add("Hour", count - i);
                        break;
                    case "panel_Minutes":
                        elementOptions.Add("Minute", count - i);
                        break;
                    case "panel_Seconds":
                        elementOptions.Add("Second", count - i);
                        break;
                }
            }
            return elementOptions;
        }

        public void SettingsClear()
        {
            setValue = true;

            Dictionary<int, string> elementOptions = new Dictionary<int, string>();
            elementOptions.Add(3, "Second");
            elementOptions.Add(2, "Minute");
            elementOptions.Add(1, "Hour");
            SetOptionsPosition(elementOptions);

            checkBox_Hours.Checked = false;
            checkBox_Minutes.Checked = false;
            checkBox_Seconds.Checked = false;

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
