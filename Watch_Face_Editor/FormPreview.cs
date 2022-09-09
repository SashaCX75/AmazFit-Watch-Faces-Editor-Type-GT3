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
                switch (Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_Preview.Size = new Size(230, 230);
                        this.Size = new Size(230 + (int)(22 * currentDPI), 230 + (int)(66 * currentDPI));
                        break;
                    case "GTR 3 Pro":
                        pictureBox_Preview.Size = new Size(243, 243);
                        this.Size = new Size(243 + (int)(22 * currentDPI), 243 + (int)(66 * currentDPI));
                        break;
                    case "GTS 3":
                        pictureBox_Preview.Size = new Size(198, 228);
                        this.Size = new Size(198 + (int)(22 * currentDPI), 228 + (int)(66 * currentDPI));
                        break;
                    case "GTR 4":
                        pictureBox_Preview.Size = new Size(236, 236);
                        this.Size = new Size(236 + (int)(22 * currentDPI), 236 + (int)(66 * currentDPI));
                        break;
                    case "Amazfit Band 7":
                        pictureBox_Preview.Size = new Size(100, 187);
                        this.Size = new Size(100 + (int)(22 * currentDPI), 187 + (int)(66 * currentDPI));
                        break;
                    case "GTS 4 mini":
                        pictureBox_Preview.Size = new Size(171, 195);
                        this.Size = new Size(171 + (int)(22 * currentDPI), 195 + (int)(66 * currentDPI));
                        break;
                }
                scale = 0.5f;
            }

            if (radioButton_normal.Checked)
            {
                switch (Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_Preview.Size = new Size(456, 456);
                        this.Size = new Size(456 + (int)(22 * currentDPI), 456 + (int)(66 * currentDPI));
                        break;
                    case "GTR 3 Pro":
                        pictureBox_Preview.Size = new Size(482, 482);
                        this.Size = new Size(482 + (int)(22 * currentDPI), 482 + (int)(66 * currentDPI));
                        break;
                    case "GTS 3":
                        pictureBox_Preview.Size = new Size(392, 452);
                        this.Size = new Size(392 + (int)(22 * currentDPI), 452 + (int)(66 * currentDPI));
                        break;
                    case "GTR 4":
                        pictureBox_Preview.Size = new Size(468, 468);
                        this.Size = new Size(468 + (int)(22 * currentDPI), 468 + (int)(66 * currentDPI));
                        break;
                    case "Amazfit Band 7":
                        pictureBox_Preview.Size = new Size(196, 370);
                        this.Size = new Size(196 + (int)(22 * currentDPI), 370 + (int)(66 * currentDPI));
                        break;
                    case "GTS 4 mini":
                        pictureBox_Preview.Size = new Size(338, 386);
                        this.Size = new Size(338 + (int)(22 * currentDPI), 386 + (int)(66 * currentDPI));
                        break;
                }
                scale = 1f;
            }

            if (radioButton_large.Checked)
            {
                switch (Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_Preview.Size = new Size(683, 683);
                        this.Size = new Size(683 + (int)(22 * currentDPI), 683 + (int)(66 * currentDPI));
                        break;
                    case "GTR 3 Pro":
                        pictureBox_Preview.Size = new Size(722, 722);
                        this.Size = new Size(722 + (int)(22 * currentDPI), 722 + (int)(66 * currentDPI));
                        break;
                    case "GTS 3":
                        pictureBox_Preview.Size = new Size(587, 677);
                        this.Size = new Size(587 + (int)(22 * currentDPI), 677 + (int)(66 * currentDPI));
                        break;
                    case "GTR 4":
                        pictureBox_Preview.Size = new Size(701, 701);
                        this.Size = new Size(701 + (int)(22 * currentDPI), 701 + (int)(66 * currentDPI));
                        break;
                    case "Amazfit Band 7":
                        pictureBox_Preview.Size = new Size(293, 554);
                        this.Size = new Size(293 + (int)(22 * currentDPI), 554 + (int)(66 * currentDPI));
                        break;
                    case "GTS 4 mini":
                        pictureBox_Preview.Size = new Size(506, 578);
                        this.Size = new Size(506 + (int)(22 * currentDPI), 578 + (int)(66 * currentDPI));
                        break;
                }
                scale = 1.5f;
            }

            if (radioButton_xlarge.Checked)
            {
                switch (Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_Preview.Size = new Size(909, 909);
                        this.Size = new Size(909 + (int)(22 * currentDPI), 909 + (int)(66 * currentDPI));
                        break;
                    case "GTR 3 Pro":
                        pictureBox_Preview.Size = new Size(961, 961);
                        this.Size = new Size(961 + (int)(22 * currentDPI), 961 + (int)(66 * currentDPI));
                        break;
                    case "GTS 3":
                        pictureBox_Preview.Size = new Size(781, 901);
                        this.Size = new Size(781 + (int)(22 * currentDPI), 901 + (int)(66 * currentDPI));
                        break;
                    case "GTR 4":
                        pictureBox_Preview.Size = new Size(933, 933);
                        this.Size = new Size(933 + (int)(22 * currentDPI), 933 + (int)(66 * currentDPI));
                        break;
                    case "Amazfit Band 7":
                        pictureBox_Preview.Size = new Size(389, 737);
                        this.Size = new Size(389 + (int)(22 * currentDPI), 737 + (int)(66 * currentDPI));
                        break;
                    case "GTS 4 mini":
                        pictureBox_Preview.Size = new Size(673, 769);
                        this.Size = new Size(673 + (int)(22 * currentDPI), 769 + (int)(66 * currentDPI));
                        break;
                }
                scale = 2f;
            }

            if (radioButton_xxlarge.Checked)
            {
                switch (Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_Preview.Size = new Size(1136, 1136);
                        this.Size = new Size(1136 + (int)(22 * currentDPI), 1136 + (int)(66 * currentDPI));
                        break;
                    case "GTR 3 Pro":
                        pictureBox_Preview.Size = new Size(1201, 1201);
                        this.Size = new Size(1201 + (int)(22 * currentDPI), 1201 + (int)(66 * currentDPI));
                        break;
                    case "GTS 3":
                        pictureBox_Preview.Size = new Size(976, 1126);
                        this.Size = new Size(976 + (int)(22 * currentDPI), 1126 + (int)(66 * currentDPI));
                        break;
                    case "GTR 4":
                        pictureBox_Preview.Size = new Size(1166, 1166);
                        this.Size = new Size(1166 + (int)(22 * currentDPI), 1166 + (int)(66 * currentDPI));
                        break;
                    case "Amazfit Band 7":
                        pictureBox_Preview.Size = new Size(486, 921);
                        this.Size = new Size(486 + (int)(22 * currentDPI), 921 + (int)(66 * currentDPI));
                        break;
                    case "GTS 4 mini":
                        pictureBox_Preview.Size = new Size(841, 961);
                        this.Size = new Size(841 + (int)(22 * currentDPI), 961 + (int)(66 * currentDPI));
                        break;
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
