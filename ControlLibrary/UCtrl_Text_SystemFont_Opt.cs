using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_Text_SystemFont_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        public Object _ElementWithSystemFont;

        public UCtrl_Text_SystemFont_Opt()
        {
            InitializeComponent();
            setValue = true;
            comboBox_alignmentHorizontal.SelectedIndex = 0;
            comboBox_alignmentVertical.SelectedIndex = 0;
            comboBox_textStyle.SelectedIndex = 0;
            setValue = false;
        }

        public void SetHorizontalAlignment(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "LEFT":
                    result = 0;
                    break;
                case "CENTER_H":
                    result = 1;
                    break;
                case "RIGHT":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_alignmentHorizontal.SelectedIndex = result;
        }

        /// <summary>Возвращает выравнивание строкой "LEFT", "RIGHT", "CENTER_H"</summary>
        public string GetHorizontalAlignment()
        {
            string result;
            switch (comboBox_alignmentHorizontal.SelectedIndex)
            {
                case 0:
                    result = "LEFT";
                    break;
                case 1:
                    result = "CENTER_H";
                    break;
                case 2:
                    result = "RIGHT";
                    break;

                default:
                    result = "Left";
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexHorizontalAlignment()
        {
            return comboBox_alignmentHorizontal.SelectedIndex;
        }

        public void SetVerticalAlignment(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "TOP":
                    result = 0;
                    break;
                case "CENTER_V":
                    result = 1;
                    break;
                case "BOTTOM":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_alignmentVertical.SelectedIndex = result;
        }

        /// <summary>Возвращает выравнивание строкой "LEFT", "RIGHT", "CENTER_H"</summary>
        public string GetVerticalAlignment()
        {
            string result;
            switch (comboBox_alignmentVertical.SelectedIndex)
            {
                case 0:
                    result = "TOP";
                    break;
                case 1:
                    result = "CENTER_V";
                    break;
                case 2:
                    result = "BOTTOM";
                    break;

                default:
                    result = "TOP";
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexVerticalAlignment()
        {
            return comboBox_alignmentVertical.SelectedIndex;
        }

        public void SetTextStyle(string style)
        {
            int result;
            switch (style)
            {
                case "NONE":
                    result = 0;
                    break;
                case "WRAP":
                    result = 1;
                    break;
                //case "CHAR_WRAP":
                //    result = 2;
                //    break;
                case "ELLIPSIS":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            comboBox_textStyle.SelectedIndex = result;
        }

        /// <summary>Возвращает выравнивание строкой "ELLIPSIS", "WRAP", "CHAR_WRAP", "NONE"</summary>
        public string GetTextStyle()
        {
            string result;
            switch (comboBox_textStyle.SelectedIndex)
            {
                case 0:
                    result = "NONE";
                    break;
                case 1:
                    result = "WRAP";
                    break;
                //case 2:
                //    result = "CHAR_WRAP";
                //    break;
                case 2:
                    result = "ELLIPSIS";
                    break;

                default:
                    result = "NONE";
                    break;

            }
            return result;
        }
        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexTextStyle()
        {
            return comboBox_textStyle.SelectedIndex;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        private void checkBox_Click(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        #region Standard events

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
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
        #endregion

        #region contextMenu
        private void contextMenuStrip_X_Opening(object sender, CancelEventArgs e)
        {
            if ((MouseСoordinates.X < 0) || (MouseСoordinates.Y < 0))
            {
                contextMenuStrip_X.Items[0].Enabled = false;
            }
            else
            {
                contextMenuStrip_X.Items[0].Enabled = true;
            }
            decimal i = 0;
            if ((Clipboard.ContainsText() == true) && (decimal.TryParse(Clipboard.GetText(), out i)))
            {
                contextMenuStrip_X.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip_X.Items[2].Enabled = false;
            }
        }

        private void contextMenuStrip_Y_Opening(object sender, CancelEventArgs e)
        {
            if ((MouseСoordinates.X < 0) || (MouseСoordinates.Y < 0))
            {
                contextMenuStrip_Y.Items[0].Enabled = false;
            }
            else
            {
                contextMenuStrip_Y.Items[0].Enabled = true;
            }
            decimal i = 0;
            if ((Clipboard.ContainsText() == true) && (decimal.TryParse(Clipboard.GetText(), out i)))
            {
                contextMenuStrip_Y.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip_Y.Items[2].Enabled = false;
            }
        }

        private void вставитьКоординатуХToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    numericUpDown.Value = MouseСoordinates.X;
                }
            }
        }

        private void вставитьКоординатуYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    numericUpDown.Value = MouseСoordinates.Y;
                }
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    Clipboard.SetText(numericUpDown.Value.ToString());
                }
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    //Если в буфере обмен содержится текст
                    if (Clipboard.ContainsText() == true)
                    {
                        //Извлекаем (точнее копируем) его и сохраняем в переменную
                        decimal i = 0;
                        if (decimal.TryParse(Clipboard.GetText(), out i))
                        {
                            if (i > numericUpDown.Maximum) i = numericUpDown.Maximum;
                            if (i < numericUpDown.Minimum) i = numericUpDown.Minimum;
                            numericUpDown.Value = i;
                        }
                    }

                }
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

        private void numericUpDown_Width_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.X < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                if ((MouseСoordinates.X - numericUpDown_X.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.X - numericUpDown_X.Value;
                }
                else numericUpDown.Value = 1;
            }
        }

        private void numericUpDown_Height_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.Y < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                if ((MouseСoordinates.Y - numericUpDown_Y.Value) > 0)
                {
                    numericUpDown.Value = MouseСoordinates.Y - numericUpDown_Y.Value;
                }
                else numericUpDown.Value = 1;
            }
        }

        private void comboBox_Color_Click(object sender, EventArgs e)
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

        public void SetColorText(Color color)
        {
            comboBox_Color.BackColor = color;
        }

        public Color GetColorText()
        {
            return comboBox_Color.BackColor;
        }

        #region Settings Set/Clear

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            comboBox_alignmentHorizontal.SelectedIndex = 0;
            comboBox_alignmentVertical.SelectedIndex = 0;
            comboBox_textStyle.SelectedIndex = 0;

            setValue = false;
        }
        #endregion

        private void numericUpDown_Pos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_X")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_Y")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Y.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_X" || numericUpDown.Name == "numericUpDown_Y"))
                    numericUpDown_X.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_X" || numericUpDown.Name == "numericUpDown_Y"))
                    numericUpDown_X.UpButton();

                e.Handled = true;
            }
        }

        private void numericUpDown_size_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_Width")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_Width")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_Height")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_Height")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_Height.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_Width" || numericUpDown.Name == "numericUpDown_Height"))
                    numericUpDown_Width.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_Width" || numericUpDown.Name == "numericUpDown_Height"))
                    numericUpDown_Width.UpButton();

                e.Handled = true;
            }
        }
    }
}
