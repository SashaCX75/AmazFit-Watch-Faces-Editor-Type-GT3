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
    public partial class FormFileExists : Form
    {
        int result = 0;
        public int Data
        {
            get
            {
                return result;
            }
        }
        public FormFileExists()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = 0;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = 1;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            result = 2;
            this.Close();
        }
    }
}
