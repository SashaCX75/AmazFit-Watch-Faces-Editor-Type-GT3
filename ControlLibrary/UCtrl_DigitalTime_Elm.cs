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
    public partial class UCtrl_DigitalTime_Elm : UserControl
    {
        bool highlight_hours = false;
        bool highlight_minutes = false;
        bool highlight_seconds = false;

        bool highlight_hours_font = false;
        bool highlight_minutes_font = false;
        bool highlight_seconds_font = false;

        bool highlight_am_pm = false;

        bool visibility_elements = false; // развернут список с элементами
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        public int position = -1; // позиция в наборе элеменетов
        public UCtrl_DigitalTime_Elm()
        {
            InitializeComponent();

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
        [Description("Происходит при изменении параметров")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event SelectChangedHandler SelectChanged;
        public delegate void SelectChangedHandler(object sender, EventArgs eventArgs, string selectElement);

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
            bool highlight_font = highlight_hours_font || highlight_minutes_font || highlight_seconds_font;
            highlight = highlight || highlight_font;
            return highlight;
        }

        public void ResetHighlightState()
        {
            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = false;

            highlight_hours_font = false;
            highlight_minutes_font = false;
            highlight_seconds_font = false;


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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }
        }

        private void panel_Hours_Click(object sender, EventArgs e)
        {
            highlight_hours = true;
            highlight_minutes = false;
            highlight_seconds = false;

            highlight_hours_font = false;
            highlight_minutes_font = false;
            highlight_seconds_font = false;

            highlight_am_pm = false;

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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_am_pm)
            {
                panel_AmPm.BackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_AmPm.BackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs, "hour");
            }
        }

        private void panel_Minutes_Click(object sender, EventArgs e)
        {
            highlight_hours = false;
            highlight_minutes = true;
            highlight_seconds = false;

            highlight_hours_font = false;
            highlight_minutes_font = false;
            highlight_seconds_font = false;

            highlight_am_pm = false;

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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_am_pm)
            {
                panel_AmPm.BackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_AmPm.BackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs, "minute");
            }
        }

        private void panel_Seconds_Click(object sender, EventArgs e)
        {
            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = true;

            highlight_hours_font = false;
            highlight_minutes_font = false;
            highlight_seconds_font = false;

            highlight_am_pm = false;

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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_am_pm)
            {
                panel_AmPm.BackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_AmPm.BackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs, "second");
            }
        }

        private void panel_Hours_Font_Click(object sender, EventArgs e)
        {
            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = false;

            highlight_hours_font = true;
            highlight_minutes_font = false;
            highlight_seconds_font = false;

            highlight_am_pm = false;

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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_am_pm)
            {
                panel_AmPm.BackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_AmPm.BackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs, "hours_font");
            }
        }

        private void panel_Minutes_Font_Click(object sender, EventArgs e)
        {
            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = false;

            highlight_hours_font = false;
            highlight_minutes_font = true;
            highlight_seconds_font = false;

            highlight_am_pm = false;

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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_am_pm)
            {
                panel_AmPm.BackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_AmPm.BackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs, "minutes_font");
            }
        }

        private void panel_Seconds_Font_Click(object sender, EventArgs e)
        {
            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = false;

            highlight_hours_font = false;
            highlight_minutes_font = false;
            highlight_seconds_font = true;

            highlight_am_pm = false;

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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_am_pm)
            {
                panel_AmPm.BackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_AmPm.BackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs, "seconds_font");
            }
        }

        private void panel_AmPm_Click(object sender, EventArgs e)
        {
            highlight_hours = false;
            highlight_minutes = false;
            highlight_seconds = false;

            highlight_hours_font = false;
            highlight_minutes_font = false;
            highlight_seconds_font = false;

            highlight_am_pm = true;

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


            if (highlight_hours_font)
            {
                panel_Hours_Font.BackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Hours_Font.BackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Hours_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_minutes_font)
            {
                panel_Minutes_Font.BackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Minutes_Font.BackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Minutes_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_seconds_font)
            {
                panel_Seconds_Font.BackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Seconds_Font.BackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Seconds_Font.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_am_pm)
            {
                panel_AmPm.BackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_AmPm.BackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_AmPm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs, "seconds_font");
            }
        }


        #region перетягиваем элементы
        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null) panel.Tag = new object();

            //((Control)sender).Tag = new object();
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null && panel.Tag != null)
                panel.DoDragDrop(sender, DragDropEffects.Move);

            //Control control = (Control)sender;
            //if (control.Tag != null)
            //    control.DoDragDrop(sender, DragDropEffects.Move);
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
            if(draggedPanel == null)
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
                if (pos.Row == 6) return;
                //tableLayoutPanel1.Controls.Add(draggedButton, pos.Column, pos.Row);

                if (pos != posOld)
                {
                    if (pt.Y < control.Location.Y + draggedPanel.Height * 0.9)
                    {
                        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                        if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                    }
                }

                //if (pos != posOld && pos.Row < posOld.Row)
                //{
                //    if (pt.Y < control.Location.Y + draggedPanel.Height * 0.9)
                //    {
                //        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                //        if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                //        else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                //    }
                //}
                //if (pos != posOld && pos.Row > posOld.Row)
                //{
                //    if (pt.Y > control.Location.Y + control.Height * 0.6)
                //    {
                //        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                //        if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                //        else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                //    }
                //}
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
        }

        private void pictureBox_Show_Click(object sender, EventArgs e)
        {
            visibilityElement = false;
            pictureBox_Show.Visible = false;
            pictureBox_NotShow.Visible = true;

            if (ValueChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void pictureBox_NotShow_Click(object sender, EventArgs e)
        {
            visibilityElement = false;
            pictureBox_Show.Visible = true;
            pictureBox_NotShow.Visible = false;

            if (ValueChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }
    }
}
