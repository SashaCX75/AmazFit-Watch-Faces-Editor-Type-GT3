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
    public partial class UCtrl_DisconnectAlert_Opt : UserControl
    {
        private bool setValue; // режим задания параметров

        public UCtrl_DisconnectAlert_Opt()
        {
            InitializeComponent();
            setValue = true;
            comboBox_disconneсnt_vibrate_type.SelectedIndex = 0;
            comboBox_conneсnt_vibrate_type.SelectedIndex = 0;
            setValue = false;
        }

        /// <summary>Устанавливает номер типа вибрации при обрыве связи 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public void SetVibratetDisconneсnt(int type)
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
                case 27:
                    result = 3;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_disconneсnt_vibrate_type.SelectedIndex = result;
        }

        /// <summary>Возвращает номер типа вибрации при обрыве связи 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public int GetVibratetDisconneсnt()
        {
            int result;
            switch (comboBox_disconneсnt_vibrate_type.SelectedIndex)
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
                    result = 27;
                    break;

                default:
                    result = 25;
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexVibratetDisconneсnt()
        {
            return comboBox_disconneсnt_vibrate_type.SelectedIndex;
        }

        /// <summary>Устанавливает номер типа вибрации при востановлении связи 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public void SetVibratetConneсnt(int type)
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
                case 27:
                    result = 3;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_conneсnt_vibrate_type.SelectedIndex = result;
        }

        /// <summary>Возвращает номер типа вибрации при востановлении связи 25=короткая; 0=средняя; 9=длинная; 27=длинная непрерывная</summary>
        public int GetVibratetConneсnt()
        {
            int result;
            switch (comboBox_conneсnt_vibrate_type.SelectedIndex)
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
                    result = 27;
                    break;

                default:
                    result = 25;
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexVibratetConneсnt()
        {
            return comboBox_conneсnt_vibrate_type.SelectedIndex;
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

        private void checkBox_disconneсnt_vibrate_CheckStateChanged(object sender, EventArgs e)
        {
            label_disconneсnt_vibrate.Enabled = checkBox_disconneсnt_vibrate.Checked;
            comboBox_disconneсnt_vibrate_type.Enabled = checkBox_disconneсnt_vibrate.Checked;
        }

        private void checkBox_conneсnt_vibrate_CheckStateChanged(object sender, EventArgs e)
        {
            label_conneсnt_vibrate.Enabled = checkBox_conneсnt_vibrate.Checked;
            comboBox_conneсnt_vibrate_type.Enabled = checkBox_conneсnt_vibrate.Checked;
        }

        private void checkBox_disconneсnt_toast_CheckStateChanged(object sender, EventArgs e)
        {
            textBox_disconneсnt_toast_text.Enabled = checkBox_disconneсnt_toast.Checked;
        }

        private void checkBox_conneсnt_toast_CheckStateChanged(object sender, EventArgs e)
        {
            textBox_conneсnt_toast_text.Enabled = checkBox_conneсnt_toast.Checked;
        }

        #region Settings Set/Clear

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            textBox_disconneсnt_toast_text.Text = "";
            checkBox_disconneсnt_toast.Checked = false;
            comboBox_disconneсnt_vibrate_type.SelectedIndex = 2;
            checkBox_disconneсnt_vibrate.Checked = false;

            textBox_conneсnt_toast_text.Text = "";
            checkBox_conneсnt_toast.Checked = false;
            comboBox_conneсnt_vibrate_type.SelectedIndex = 2;
            checkBox_conneсnt_vibrate.Checked = false;

            setValue = false;
        }

        #endregion
    }
}
