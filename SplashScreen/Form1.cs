using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SplashScreen
{
    public partial class SplashScreen : Form
    {
        int imageIndex = 0;
        public SplashScreen()
        {
            InitializeComponent();
            pictureBox_SplashScreen.Image = imageList1.Images[imageIndex];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            imageIndex++;
            if (imageIndex >= imageList1.Images.Count) imageIndex = 0;
            pictureBox_SplashScreen.Image = imageList1.Images[imageIndex];
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
