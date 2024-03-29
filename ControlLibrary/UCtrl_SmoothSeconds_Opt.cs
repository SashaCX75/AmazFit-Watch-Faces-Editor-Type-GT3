﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_SmoothSeconds_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        private bool AODmode;
        public Object _SmoothSeconds;

        [Description("Отображается на экране AOD")]
        public bool AOD
        {
            get
            {
                return AODmode;
            }
            set
            {
                AODmode = value;
                radioButton_type1.Enabled = !AODmode;
                radioButton_type3.Enabled = !AODmode;
            }
        }

        /// <summary>Получаем тип плавной секундной стрелки</summary>
        public int GetSmothType()
        {
            int type = 1;
            if (radioButton_type1.Checked) type = 1;
            if (radioButton_type2.Checked) type = 2;
            if (radioButton_type3.Checked) type = 3;
            if (radioButton_type4.Checked) type = 4;
            return type;
        }

        public UCtrl_SmoothSeconds_Opt()
        {
            InitializeComponent();
            setValue = false;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        #region Settings Set/Clear

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            radioButton_type1.Checked = true;
            numericUpDown_fps.Value = 15;

            setValue = false;
        }

        #endregion

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (ValueChanged != null && !setValue && radioButton.Checked)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        #region numericUpDown

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        #endregion
    }
}
