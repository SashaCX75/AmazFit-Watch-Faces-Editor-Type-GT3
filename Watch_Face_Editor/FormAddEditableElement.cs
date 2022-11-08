using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Watch_Face_Editor
{
    public partial class FormAddEditableElement : Form
    {
        string type = "";
        public string Type
        {
            get
            {
                return type;
            }
        }
        public FormAddEditableElement()
        {
            InitializeComponent();
        }

        // объединяем контролы из разных groupBox
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                string name = radioButton.Name;
                Control control = this;
                foreach (Control ctrl in control.Controls)
                {
                    // проверяем количество дочерних контролов
                    if (ctrl.Controls.Count > 0)
                        // если кол-во дочерних контролов больше 0, то запускаем более глубокую проверку
                        foreach (Control rbControl in ctrl.Controls)
                        {
                            // проверяем контрол на принадлежность к RadioButton
                            if (rbControl is RadioButton)
                            {
                                if (rbControl.Name != name)
                                {
                                    RadioButton rb = rbControl as RadioButton;
                                    rb.Checked = false;
                                }
                            }
                        }
                }
                //switch (name)
                //{
                //    case "radioButton_date":
                //        type = "Date";
                //        break;
                //    case "radioButton_month":
                //        type = "Month";
                //        break;
                //    case "radioButton_year":
                //        type = "Year";
                //        break;
                //    case "radioButton_week":
                //        type = "Week";
                //        break;

                //    case "radioButton_steps":
                //        type = "Steps";
                //        break;
                //    case "radioButton_calories":
                //        type = "Calories";
                //        break;
                //    case "radioButton_heart":
                //        type = "Heart";
                //        break;
                //    case "radioButton_PAI":
                //        type = "PAI";
                //        break;
                //    case "radioButton_distance":
                //        type = "Distance";
                //        break;
                //    case "radioButton_stand":
                //        type = "Stand";
                //        break;
                //    case "radioButton_stress":
                //        type = "Stress";
                //        break;
                //    case "radioButton_fat_burning":
                //        type = "FatBurning";
                //        break;
                //    case "radioButton_SpO2":
                //        type = "SpO2";
                //        break;

                //    case "radioButton_weather":
                //        type = "Weather";
                //        break;
                //    case "radioButton_UVI":
                //        type = "UVI";
                //        break;
                //    case "radioButton_humidity":
                //        type = "Humidity";
                //        break;
                //    case "radioButton_sunrise":
                //        type = "Sunrise";
                //        break;
                //    case "radioButton_wind":
                //        type = "Wind";
                //        break;
                //    case "Altimeter":
                //        type = "SpO2";
                //        break;
                //    case "radioButton_moon":
                //        type = "Moon";
                //        break;

                //    default:
                //        type = "";
                //        break;
                //}
            }
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

        private void button_add_Click(object sender, EventArgs e)
        {
            Control control = this;
            foreach (Control ctrl in control.Controls)
            {
                // проверяем количество дочерних контролов
                if (ctrl.Controls.Count > 0)
                    // если кол-во дочерних контролов больше 0, то запускаем более глубокую проверку
                    foreach (Control rbControl in ctrl.Controls)
                    {
                        // проверяем контрол на принадлежность к RadioButton
                        if (rbControl is RadioButton)
                        {
                            RadioButton rb = rbControl as RadioButton;
                            if (rb.Checked)
                            {
                                string name = rb.Name;
                                switch (name)
                                {
                                    case "radioButton_date":
                                        type = "Date";
                                        break;
                                    case "radioButton_month":
                                        type = "Month";
                                        break;
                                    case "radioButton_year":
                                        type = "Year";
                                        break;
                                    case "radioButton_week":
                                        type = "Week";
                                        break;

                                    case "radioButton_steps":
                                        type = "Steps";
                                        break;
                                    case "radioButton_calories":
                                        type = "Calories";
                                        break;
                                    case "radioButton_heart":
                                        type = "Heart";
                                        break;
                                    case "radioButton_PAI":
                                        type = "PAI";
                                        break;
                                    case "radioButton_distance":
                                        type = "Distance";
                                        break;
                                    case "radioButton_stand":
                                        type = "Stand";
                                        break;
                                    case "radioButton_stress":
                                        type = "Stress";
                                        break;
                                    case "radioButton_fat_burning":
                                        type = "FatBurning";
                                        break;
                                    case "radioButton_SpO2":
                                        type = "SpO2";
                                        break;

                                    case "radioButton_weather":
                                        type = "Weather";
                                        break;
                                    case "radioButton_UVI":
                                        type = "UVI";
                                        break;
                                    case "radioButton_humidity":
                                        type = "Humidity";
                                        break;
                                    case "radioButton_sunrise":
                                        type = "Sunrise";
                                        break;
                                    case "radioButton_wind":
                                        type = "Wind";
                                        break;
                                    case "Altimeter":
                                        type = "SpO2";
                                        break;
                                    case "radioButton_moon":
                                        type = "Moon";
                                        break;

                                    case "radioButton_battery":
                                        type = "Battery";
                                        break;

                                    default:
                                        type = "";
                                        break;
                                }
                            }
                        }
                    }
            }
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            type = "";
            this.Close();
        }
    }
}
