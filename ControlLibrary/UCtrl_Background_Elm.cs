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
    public partial class UCtrl_Background_Elm : UserControl
    {
        bool highlight_element = false;

        public UCtrl_Background_Elm()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event SelectChangedHandler SelectChanged;
        public delegate void SelectChangedHandler(object sender, EventArgs eventArgs);

        private void button_ElementName_Click(object sender, EventArgs e)
        {
            highlight_element = true;
            if (highlight_element) button_ElementName.BackColor = SystemColors.ActiveCaption;
            else button_ElementName.BackColor = SystemColors.Control;

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        public bool GetHighlightState()
        {
            return highlight_element;
        }

        public void ResetHighlightState()
        {
            highlight_element = false;
            if (highlight_element) button_ElementName.BackColor = SystemColors.ActiveCaption;
            else button_ElementName.BackColor = SystemColors.Control;
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
    }
}
