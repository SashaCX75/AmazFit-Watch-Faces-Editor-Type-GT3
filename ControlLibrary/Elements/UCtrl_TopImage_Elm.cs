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
    public partial class UCtrl_TopImage_Elm : UserControl
    {
        private bool setValue; // режим задания параметров
        bool highlight_element = false;
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        public UCtrl_TopImage_Elm()
        {
            InitializeComponent();
            setValue = false;

            button_ElementName.Controls.Add(pictureBox_NotShow);
            button_ElementName.Controls.Add(pictureBox_Show);
            button_ElementName.Controls.Add(pictureBox_Del);

            pictureBox_NotShow.BackColor = Color.Transparent;
            pictureBox_Show.BackColor = Color.Transparent;

            pictureBox_Del.BackColor = Color.Transparent;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event SelectChangedHandler SelectChanged;
        public delegate void SelectChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении видимости элемента")]
        public event VisibleElementChangedHandler VisibleElementChanged;
        public delegate void VisibleElementChangedHandler(object sender, EventArgs eventArgs, bool visible);

        [Browsable(true)]
        [Description("Происходит при удалении элемента")]
        public event DelElementHandler DelElement;
        public delegate void DelElementHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        #region Standard events

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }
        #endregion

        private void button_ElementName_Click(object sender, EventArgs e)
        {
            highlight_element = true;
            panel2.Visible = true;
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
            panel2.Visible = false;
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

        private void button_ElementName_SizeChanged(object sender, EventArgs e)
        {
            pictureBox_NotShow.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6, 2);
            pictureBox_Show.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6, 2);

            pictureBox_Del.Location = new Point(button_ElementName.Width - pictureBox_Del.Width - 4, 2);

            if (button_ElementName.Height > 35)
            {
                float currentDPI = button_ElementName.Height / 28f;
                button_ElementName.Image = (Image)(new Bitmap(button_ElementName.Image,
                    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));

                Control.ControlCollection controlCollection = button_ElementName.Controls;
                for (int i = 0; i < controlCollection.Count; i++)
                {
                    string name = controlCollection[i].GetType().Name;
                    if (name == "PictureBox")
                    {
                        PictureBox pb = (PictureBox)controlCollection[i];
                        pb.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }

            }
        }

        private void pictureBox_Show_Click(object sender, EventArgs e)
        {
            visibilityElement = !visibilityElement;
            pictureBox_NotShow.Visible = !visibilityElement;
            pictureBox_Show.Visible = visibilityElement;

            if (VisibleElementChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleElementChanged(this, eventArgs, visibilityElement);
            }
        }

        public void SetVisibilityElementStatus(bool status)
        {
            visibilityElement = status;
            pictureBox_NotShow.Visible = !visibilityElement;
            pictureBox_Show.Visible = visibilityElement;
        }

        private void pictureBox_Del_Click(object sender, EventArgs e)
        {
            if (DelElement != null)
            {
                EventArgs eventArgs = new EventArgs();
                DelElement(this, eventArgs);
            }
        }

        public void SettingsClear()
        {
            setValue = true;

            visibilityElement = true;
            pictureBox_NotShow.Visible = !visibilityElement;
            pictureBox_Show.Visible = visibilityElement;

            setValue = false;
        }

        private void UCtrl_TopImage_Elm_Load(object sender, EventArgs e)
        {
            fitText(button_ElementName);
        }

        public void fitText(Button control)
        {
            Graphics graphics = control.CreateGraphics();
            Font drawFont = control.Font;
            //Font drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
            StringFormat strFormat = new StringFormat();
            strFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Near;
            Size strSize = TextRenderer.MeasureText(graphics, control.Text, drawFont);
            //double controlWidth = control.Width - control.Margin.Left - control.Margin.Right - control.Padding.Left - 65;
            double controlWidth = pictureBox_Show.Location.X - control.Margin.Left - control.Padding.Left - control.Image.Width - 8;
            double scale = controlWidth / strSize.Width;
            if (scale < 1)
            {
                Font newFont = new Font(control.Font.FontFamily, (float)(control.Font.Size * scale), control.Font.Style);
                control.Font = newFont;
            }

        }
    }
}
