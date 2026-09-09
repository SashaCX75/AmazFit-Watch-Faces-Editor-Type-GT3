using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_ChartSleep_Opt : UserControl
    {
        //private bool setValue; // режим задания параметров
        public Object _ChartSleep;
        private int[] CustomColors = { }; // пользовательские цвета

        public UCtrl_ChartSleep_Opt()
        {
            InitializeComponent();
            //setValue = false;
        }

        private void comboBox_color_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            ComboBox comboBox_color = sender as ComboBox;
            colorDialog.Color = comboBox_color.BackColor;
            colorDialog.FullOpen = true;

            colorDialog.CustomColors = CustomColors;


            if (colorDialog.ShowDialog() == DialogResult.Cancel) return;

            // установка цвета формы
            comboBox_color.BackColor = colorDialog.Color;
            if (CustomColors != colorDialog.CustomColors)
            {
                CustomColors = colorDialog.CustomColors;

                if (CustomColorsChanged != null)
                {
                    CustomColorsChanged(CustomColors);
                }
            }

            if (ValueChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void SetDeepSleepColour(Color color)
        {
            comboBox_deepSleep_colour.BackColor = color;
        }
        public Color GetDeepSleepColour()
        {
            return comboBox_deepSleep_colour.BackColor;
        }

        public void SetLightSleepColour(Color color)
        {
            comboBox_lightSleep_colour.BackColor = color;
        }
        public Color GetLightSleepColour()
        {
            return comboBox_lightSleep_colour.BackColor;
        }

        public void SetRemColour(Color color)
        {
            comboBox_REM_colour.BackColor = color;
        }
        public Color GetRemColour()
        {
            return comboBox_REM_colour.BackColor;
        }

        public void SetWakeupColour(Color color)
        {
            comboBox_wakeup_colour.BackColor = color;
        }
        public Color GetWakeupColour()
        {
            return comboBox_wakeup_colour.BackColor;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении пользовательских цветов")]
        public event CustomColorsChangedHandler CustomColorsChanged;
        public delegate void CustomColorsChangedHandler(int[] customColors);

        #region Settings Set/Clear

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear(int[] customColors)
        {
            //setValue = true;
            CustomColors = customColors;

            //setValue = false;
        }
        #endregion
    }
}
