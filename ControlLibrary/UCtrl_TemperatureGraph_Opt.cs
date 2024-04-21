using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_TemperatureGraph_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        public Object _Diagram;

        public UCtrl_TemperatureGraph_Opt()
        {
            InitializeComponent();
            setValue = false;
            comboBox_max_pointType.SelectedIndex = 0;
            comboBox_min_pointType.SelectedIndex = 0;
        }

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

        private void comboBox_color_Click(object sender, EventArgs e)
        {
            Program_Settings ProgramSettings = new Program_Settings();
            ColorDialog colorDialog = new ColorDialog();
            ComboBox comboBox_color = sender as ComboBox;
            colorDialog.Color = comboBox_color.BackColor;
            colorDialog.FullOpen = true;

            // читаем пользовательские цвета из настроек
            if (File.Exists(Application.StartupPath + @"\Settings.json"))
            {
                ProgramSettings = JsonConvert.DeserializeObject<Program_Settings>
                            (File.ReadAllText(Application.StartupPath + @"\Settings.json"), new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
            }
            colorDialog.CustomColors = ProgramSettings.CustomColors;


            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета формы
            comboBox_color.BackColor = colorDialog.Color;
            if (ProgramSettings.CustomColors != colorDialog.CustomColors)
            {
                ProgramSettings.CustomColors = colorDialog.CustomColors;

                string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
            }

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        public void SetMaxColor(Color color)
        {
            comboBox_maxColor.BackColor = color;
        }

        public Color GetMaxColor()
        {
            return comboBox_maxColor.BackColor;
        }

        /// <summary>Устанавливает тип точки минимальной температуры</summary>
        public void SetMaxPointType(int type)
        {
            comboBox_max_pointType.SelectedIndex = type;
        }

        /// <summary>Возвращает тип точки минимальной температуры</summary>
        public int GetMaxPointType()
        {
            return comboBox_max_pointType.SelectedIndex;
        }



        public void SetMinColor(Color color)
        {
            comboBox_minColor.BackColor = color;
        }

        public Color GetMinColor()
        {
            return comboBox_minColor.BackColor;
        }

        /// <summary>Устанавливает тип точки минимальной температуры</summary>
        public void SetMinPointType(int type)
        {
            comboBox_min_pointType.SelectedIndex = type;
        }

        /// <summary>Возвращает тип точки минимальной температуры</summary>
        public int GetMinPointType()
        {
            return comboBox_min_pointType.SelectedIndex;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        #region Standard events

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

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

        #region Settings Set/Clear

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            numericUpDown_posY.Value = 30;
            numericUpDown_height.Value = 200;

            numericUpDown_max_lineWidth.Value = 5;
            numericUpDown_max_pointSize.Value = 12;
            numericUpDown_max_offsetX.Value = 0;
            comboBox_max_pointType.SelectedIndex = 0;

            numericUpDown_min_lineWidth.Value = 5;
            numericUpDown_min_pointSize.Value = 12;
            numericUpDown_min_offsetX.Value = 0;
            comboBox_min_pointType.SelectedIndex = 0;

            checkBox_use_max.Checked = true;
            checkBox_use_min.Checked = true;

            setValue = false;
        }

        #endregion

        private void checkBox_use_max_CheckedChanged(object sender, EventArgs e)
        {
            bool cheked = checkBox_use_max.Checked;
            comboBox_maxColor.Enabled = cheked;
            comboBox_max_pointType.Enabled = cheked;
            numericUpDown_max_lineWidth.Enabled = cheked;
            numericUpDown_max_pointSize.Enabled = cheked;
            numericUpDown_max_offsetX.Enabled = cheked;

            label11.Enabled = cheked;
            label10.Enabled = cheked;
            label9.Enabled = cheked;
            label8.Enabled = cheked;
            label1.Enabled = cheked;

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void checkBox_use_min_CheckedChanged(object sender, EventArgs e)
        {
            bool cheked = checkBox_use_min.Checked;
            comboBox_minColor.Enabled = cheked;
            comboBox_min_pointType.Enabled = cheked;
            numericUpDown_min_lineWidth.Enabled = cheked;
            numericUpDown_min_pointSize.Enabled = cheked;
            numericUpDown_min_offsetX.Enabled = cheked;

            label5.Enabled = cheked;
            label4.Enabled = cheked;
            label06.Enabled = cheked;
            label7.Enabled = cheked;
            label6.Enabled = cheked;

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }
    }
}
