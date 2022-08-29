using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class Form_Preview : Form
    {
        float scale = 1;
        float currentDPI; // масштаб экрана

        public Form_Preview(float cDPI)
        {
            InitializeComponent();
            //currentDPI = (int)Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "LogPixels", 96) / 96f;
            currentDPI = cDPI;
        }

        public void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            //Form1 f1 = this.Owner as Form1;//Получаем ссылку на первую форму
            //f1.button1.PerformClick();
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && !radioButton.Checked) return;
            pictureBox_Preview.BackgroundImageLayout = ImageLayout.Zoom;
            if (radioButton_small.Checked)
            {
                if (Watch_Model == "GTR 3" || Watch_Model == "T-Rex 2")
                {
                    pictureBox_Preview.Size = new Size(230, 230);
                    this.Size = new Size(230 + (int)(22 * currentDPI), 230 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTR 3 Pro")
                {
                    pictureBox_Preview.Size = new Size(243, 243);
                    this.Size = new Size(243 + (int)(22 * currentDPI), 243 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTS 3")
                {
                    pictureBox_Preview.Size = new Size(198, 228);
                    this.Size = new Size(198 + (int)(22 * currentDPI), 228 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "Amazfit Band 7")
                {
                    pictureBox_Preview.Size = new Size(100, 187);
                    this.Size = new Size(100 + (int)(22 * currentDPI), 187 + (int)(66 * currentDPI));
                }
                scale = 0.5f;
            }

            if (radioButton_normal.Checked)
            {
                pictureBox_Preview.BackgroundImageLayout = ImageLayout.None;
                if (Watch_Model == "GTR 3" || Watch_Model == "T-Rex 2")
                {
                    pictureBox_Preview.Size = new Size(456, 456);
                    this.Size = new Size(456 + (int)(22 * currentDPI), 456 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTR 3 Pro")
                {
                    pictureBox_Preview.Size = new Size(482, 482);
                    this.Size = new Size(482 + (int)(22 * currentDPI), 482 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTS 3")
                {
                    pictureBox_Preview.Size = new Size(392, 452);
                    this.Size = new Size(392 + (int)(22 * currentDPI), 452 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "Amazfit Band 7")
                {
                    pictureBox_Preview.Size = new Size(194, 368);
                    this.Size = new Size(194 + (int)(22 * currentDPI), 368 + (int)(66 * currentDPI));
                }
                scale = 1f;
            }

            if (radioButton_large.Checked)
            {
                if (Watch_Model == "GTR 3" || Watch_Model == "T-Rex 2")
                {
                    pictureBox_Preview.Size = new Size(683, 683);
                    this.Size = new Size(683 + (int)(22 * currentDPI), 683 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTR 3 Pro")
                {
                    pictureBox_Preview.Size = new Size(722, 722);
                    this.Size = new Size(722 + (int)(22 * currentDPI), 722 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTS 3")
                {
                    pictureBox_Preview.Size = new Size(587, 677);
                    this.Size = new Size(587 + (int)(22 * currentDPI), 677 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "Amazfit Band 7")
                {
                    pictureBox_Preview.Size = new Size(294, 555);
                    this.Size = new Size(294 + (int)(22 * currentDPI), 555 + (int)(66 * currentDPI));
                }
                scale = 1.5f;
            }

            if (radioButton_xlarge.Checked)
            {
                if (Watch_Model == "GTR 3" || Watch_Model == "T-Rex 2")
                {
                    pictureBox_Preview.Size = new Size(909, 909);
                    this.Size = new Size(909 + (int)(22 * currentDPI), 909 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTR 3 Pro")
                {
                    pictureBox_Preview.Size = new Size(961, 961);
                    this.Size = new Size(961 + (int)(22 * currentDPI), 961 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTS 3")
                {
                    pictureBox_Preview.Size = new Size(781, 901);
                    this.Size = new Size(781 + (int)(22 * currentDPI), 901 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "Amazfit Band 7")
                {
                    pictureBox_Preview.Size = new Size(391, 739);
                    this.Size = new Size(391 + (int)(22 * currentDPI), 739 + (int)(66 * currentDPI));
                }
                scale = 2f;
            }

            if (radioButton_xxlarge.Checked)
            {
                if (Watch_Model == "GTR 3" || Watch_Model == "T-Rex 2")
                {
                    pictureBox_Preview.Size = new Size(1136, 1136);
                    this.Size = new Size(1136 + (int)(22 * currentDPI), 1136 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTR 3 Pro")
                {
                    pictureBox_Preview.Size = new Size(1201, 1201);
                    this.Size = new Size(1201 + (int)(22 * currentDPI), 1201 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "GTS 3")
                {
                    pictureBox_Preview.Size = new Size(976, 1126);
                    this.Size = new Size(976 + (int)(22 * currentDPI), 1126 + (int)(66 * currentDPI));
                }
                else if (Watch_Model == "Amazfit Band 7")
                {
                    pictureBox_Preview.Size = new Size(488, 923);
                    this.Size = new Size(488 + (int)(22 * currentDPI), 923 + (int)(66 * currentDPI));
                }
                scale = 2.5f;
            }
        }

        //public class Watch_Model
        //{
        //    public static bool model_GTR3 { get; set; } = true;
        //    public static bool model_GTR3_Pro { get; set; }
        //    public static bool model_GTS3 { get; set; }

        //}

        public static string Watch_Model { get; set; } = "GTR 3";
        

        private void pictureBox_Preview_MouseMove(object sender, MouseEventArgs e)
        {
            int CursorX = (int)Math.Round(e.X / scale, 0);
            int CursorY = (int)Math.Round(e.Y / scale, 0);

            this.Text = Properties.FormStrings.Form_PreviewX + CursorX.ToString() +
                ";  Y=" + CursorY.ToString() + "]";
        }

        private void pictureBox_Preview_MouseLeave(object sender, EventArgs e)
        {
            this.Text = Properties.FormStrings.Form_Preview;
        }
        private void pictureBox_Preview_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseClickСoordinates.X = (int)Math.Round(e.X / scale, 0);
            MouseClickСoordinates.Y = (int)Math.Round(e.Y / scale, 0);
            toolTip1.Show(Properties.FormStrings.Message_CopyCoord, this, e.X - 5, e.Y - 7, 2000);
        }

        private void Form_Preview_Load(object sender, EventArgs e)
        {
            this.Text = Properties.FormStrings.Form_Preview;
        }

        private void Form_Preview_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                radioButton_normal.Checked = true;
            }
        }
    }
}
