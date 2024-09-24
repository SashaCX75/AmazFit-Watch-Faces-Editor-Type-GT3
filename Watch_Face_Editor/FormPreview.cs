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
            Form1 form1 = this.Owner as Form1;//Получаем ссылку на первую форму
            Classes.AmazfitPlatform selectedModel = form1.SelectedModel;

            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && !radioButton.Checked) return;
            pictureBox_Preview.BackgroundImageLayout = ImageLayout.Zoom;
            if (radioButton_small.Checked)
            {
                pictureBox_Preview.Size = new Size(selectedModel.scaling.scaling_0_5.w, selectedModel.scaling.scaling_0_5.h);
                this.Size = new Size(selectedModel.scaling.scaling_0_5.w + (int)(22 * currentDPI), selectedModel.scaling.scaling_0_5.h + (int)(66 * currentDPI));
                scale = 0.5f;
            }

            if (radioButton_normal.Checked)
            {
                pictureBox_Preview.Size = new Size(selectedModel.scaling.scaling_1_0.w, selectedModel.scaling.scaling_1_0.h);
                this.Size = new Size(selectedModel.scaling.scaling_1_0.w + (int)(22 * currentDPI), selectedModel.scaling.scaling_1_0.h + (int)(66 * currentDPI));
                scale = 1f;
            }

            if (radioButton_large.Checked)
            {
                pictureBox_Preview.Size = new Size(selectedModel.scaling.scaling_1_5.w, selectedModel.scaling.scaling_1_5.h);
                this.Size = new Size(selectedModel.scaling.scaling_1_5.w + (int)(22 * currentDPI), selectedModel.scaling.scaling_1_5.h + (int)(66 * currentDPI));
                scale = 1.5f;
            }

            if (radioButton_xlarge.Checked)
            {
                pictureBox_Preview.Size = new Size(selectedModel.scaling.scaling_2_0.w, selectedModel.scaling.scaling_2_0.h);
                this.Size = new Size(selectedModel.scaling.scaling_2_0.w + (int)(22 * currentDPI), selectedModel.scaling.scaling_2_0.h + (int)(66 * currentDPI));
                scale = 2f;
            }

            if (radioButton_xxlarge.Checked)
            {
                pictureBox_Preview.Size = new Size(selectedModel.scaling.scaling_2_5.w, selectedModel.scaling.scaling_2_5.h);
                this.Size = new Size(selectedModel.scaling.scaling_2_5.w + (int)(22 * currentDPI), selectedModel.scaling.scaling_2_5.h + (int)(66 * currentDPI));
                scale = 2.5f;
            }
        }

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
