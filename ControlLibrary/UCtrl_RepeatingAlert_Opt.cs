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
    public partial class UCtrl_RepeatingAlert_Opt : UserControl
    {
        private bool setValue; // режим задания параметров

        public UCtrl_RepeatingAlert_Opt()
        {
            InitializeComponent();
            setValue = true;
            comboBox_hour_vibrate_type.SelectedIndex = 0;
            comboBox_repeat_period_vibrate_type.SelectedIndex = 0;
            comboBox_repeat_period_time.SelectedIndex = 0;
            setValue = false;
        }

        /// <summary>Устанавливает номер типа вибрации каждый час 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public void SetVibratetHour(int type)
        {
            int result;
            switch (type)
            {
                case 25:
                    result = 0;
                    break;
                case 0:
                    result = 1;
                    break;
                case 9:
                    result = 2;
                    break;
                case 1:
                case 27:
                    result = 3;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_hour_vibrate_type.SelectedIndex = result;
        }

        /// <summary>Возвращает номер типа вибрации каждый час 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public int GetVibratetHour()
        {
            int result;
            switch (comboBox_hour_vibrate_type.SelectedIndex)
            {
                case 0:
                    result = 25;
                    break;
                case 1:
                    result = 0;
                    break;
                case 2:
                    result = 9;
                    break;
                case 3:
                    result = 1;
                    //result = 27;
                    break;

                default:
                    result = 25;
                    break;

            }
            return result;
        }

        /// <summary>Устанавливает номер типа вибрации для повторяющихся сигналов 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public void SetVibratetRepeatPeriod(int type)
        {
            int result;
            switch (type)
            {
                case 25:
                    result = 0;
                    break;
                case 0:
                    result = 1;
                    break;
                case 9:
                    result = 2;
                    break;
                case 1:
                case 27:
                    result = 3;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_repeat_period_vibrate_type.SelectedIndex = result;
        }

        /// <summary>Возвращает номер типа вибрации для повторяющихся сигналов 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public int GetVibratetRepeatPeriod()
        {
            int result;
            switch (comboBox_repeat_period_vibrate_type.SelectedIndex)
            {
                case 0:
                    result = 25;
                    break;
                case 1:
                    result = 0;
                    break;
                case 2:
                    result = 9;
                    break;
                case 3:
                    result = 1;
                    //result = 27;
                    break;

                default:
                    result = 25;
                    break;

            }
            return result;
        }



        /// <summary>Устанавливает номер типа вибрации каждые 30 минут 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public void SetVibratetRepeatPeriodTime(int type)
        {
            int result;
            switch (type)
            {
                case 30:
                    result = 0;
                    break;
                case 20:
                    result = 1;
                    break;
                case 15:
                    result = 2;
                    break;
                case 10:
                    result = 3;
                    break;
                case 5:
                    result = 4;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_repeat_period_time.SelectedIndex = result;
        }

        /// <summary>Возвращает номер типа вибрации каждые 30 минут 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public int GetVibratetRepeatPeriodTime()
        {
            int result;
            switch (comboBox_repeat_period_time.SelectedIndex)
            {
                case 0:
                    result = 30;
                    break;
                case 1:
                    result = 20;
                    break;
                case 2:
                    result = 15;
                    break;
                case 3:
                    result = 10;
                    break;
                case 4:
                    result = 5;
                    break;

                default:
                    result = 30;
                    break;

            }
            return result;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        #region Standard events

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }
        #endregion

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

        #region Settings Set/Clear

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            textBox_hour_toast_text.Text = "";
            checkBox_hour_toast.Checked = false;
            comboBox_hour_vibrate_type.SelectedIndex = 2;
            checkBox_hour_vibrate.Checked = false;

            textBox_repeat_period_toast_text.Text = "";
            checkBox_repeat_period_toast.Checked = false;
            comboBox_repeat_period_vibrate_type.SelectedIndex = 2;
            checkBox_repeat_period_vibrate.Checked = false;
            comboBox_repeat_period_time.SelectedIndex = 0;

            setValue = false;
        }

        #endregion

        private void checkBox_hour_vibrate_CheckStateChanged(object sender, EventArgs e)
        {
            label_hour_vibrate.Enabled = checkBox_hour_vibrate.Checked;
            comboBox_hour_vibrate_type.Enabled = checkBox_hour_vibrate.Checked;
        }

        private void checkBox_hour_toast_CheckStateChanged(object sender, EventArgs e)
        {
            textBox_hour_toast_text.Enabled = checkBox_hour_toast.Checked;
        }

        private void checkBox_repeat_period_vibrate_CheckStateChanged(object sender, EventArgs e)
        {
            label_repeat_period_vibrate.Enabled = checkBox_repeat_period_vibrate.Checked;
            comboBox_repeat_period_vibrate_type.Enabled = checkBox_repeat_period_vibrate.Checked;
        }

        private void checkBox_repeat_period_toast_CheckStateChanged(object sender, EventArgs e)
        {
            textBox_repeat_period_toast_text.Enabled = checkBox_repeat_period_toast.Checked;
        }
    }
}
