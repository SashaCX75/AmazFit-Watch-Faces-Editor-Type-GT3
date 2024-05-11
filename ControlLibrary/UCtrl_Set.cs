using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ControlLibrary
{
    public partial class UCtrl_Set: UserControl
    {
        private int setNumber;
        private bool setValue;
        Dictionary<string, List<int>> ForecastData = new Dictionary<string, List<int>>();
        public UCtrl_Set()
        {
            InitializeComponent();
            comboBox_WeatherSet_Icon.SelectedIndex = 0;
            comboBox_WindDirection.SelectedIndex = 0;
            setValue = false;
        }

        // меняем цвет текста и рамки для groupBox
        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            DrawGroupBox(box, e.Graphics, Color.Black, Color.DarkGray);
        }
        private void DrawGroupBox(GroupBox box, Graphics g, Color textColor, Color borderColor)
        {
            if (box != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                Rectangle rect = new Rectangle(box.ClientRectangle.X,
                                               box.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               box.ClientRectangle.Width - 1,
                                               box.ClientRectangle.Height - (int)(strSize.Height / 2) - 5);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(box.Text, box.Font, textBrush, box.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }


        [Browsable(true)]
        public event CollapseHandler Collapse;
        public delegate void CollapseHandler(object sender, EventArgs eventArgs, int setNumber);

        [Browsable(true)]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs, int setNumber);

        private void button_Set_Click(object sender, EventArgs e)
        {
            bool v = groupBox_Activity.Visible;
            groupBox_Air.Visible = !v;
            groupBox_Activity.Visible = !v;

            if (Collapse != null)
            {
                EventArgs eventArgs = new EventArgs();
                Collapse(this, eventArgs, setNumber);
            }

        }

        /// <summary>Возвращает true если панель свернута</summary>
        public bool Collapsed
        {
            get
            {
                return !groupBox_Activity.Visible;
            }
            set
            {
                groupBox_Air.Visible = !value;
                groupBox_Activity.Visible = !value;
            }
        }

        /// <summary>Устанавливает номер панели</summary>
        public int SetNumber
        {
            get
            {
                return setNumber;
            }
            set
            {
                setNumber = value;
            }
        }

        /// <summary>Устанавливает надпись на кнопке</summary>
        [Localizable(true)]
        public string ButtonText
        {
            get
            {
                return button_Set.Text;
            }
            set
            {
                button_Set.Text = value;
            }
        }

        private void numericUpDown_Set_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, setNumber);
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, setNumber);
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, setNumber);
            }
        }

        private void checkBox_Click(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, setNumber);
            }
        }

        /// <summary>Возвращает данные для предпросмотра из выбранного набора параметров</summary>
        /// <param name="Activity">Данные активностей (год, число, день, день недели, часы, минуты, секунды,
        /// заряд, калории, ЧСС,  путь, шаги, цель шагов, PAI, StandUp, стресс, жиросжигание)</param>
        /// <param name="Air">Атмосферные данные (иконка погоды, текущая температура, максимальная температура, 
        /// минимальная температура, УФ индекс, качество воздуха, влажность, сила ветра,
        /// высота, давление)</param>
        /// <param name="checkValue">Дначение переключателей (Bluetooth, будильник, блокировка, DND, показ температуры)</param>
        public void GetValue(out Dictionary<string, int> Activity, out Dictionary<string, int> Air, out Dictionary<string, List<int>> ForecastData,
            out Dictionary<string, bool> checkValue)
        {
            Activity = new Dictionary<string, int>();
            Air = new Dictionary<string, int>();
            ForecastData = this.ForecastData;
            checkValue = new Dictionary<string, bool>();

            Activity.Add("Year", dateTimePicker_Date_Set.Value.Year);
            Activity.Add("Month", dateTimePicker_Date_Set.Value.Month);
            Activity.Add("Day", dateTimePicker_Date_Set.Value.Day);
            int WeekDay = (int)dateTimePicker_Date_Set.Value.DayOfWeek;
            if (WeekDay == 0) WeekDay = 7;
            Activity.Add("WeekDay", WeekDay);

            Activity.Add("Hour", dateTimePicker_Time_Set.Value.Hour);
            Activity.Add("Minute", dateTimePicker_Time_Set.Value.Minute);
            Activity.Add("Second", dateTimePicker_Time_Set.Value.Second);

            Activity.Add("Battery", (int)numericUpDown_Battery_Set.Value);
            Activity.Add("Calories", (int)numericUpDown_Calories_Set.Value);
            Activity.Add("HeartRate", (int)numericUpDown_HeartRate_Set.Value);
            Activity.Add("Distance", (int)numericUpDown_Distance_Set.Value);
            Activity.Add("Steps", (int)numericUpDown_Steps_Set.Value);
            Activity.Add("StepsGoal", (int)numericUpDown_Goal_Set.Value);

            Activity.Add("PAI", (int)numericUpDown_PAI_Set.Value);
            Activity.Add("StandUp", (int)numericUpDown_StandUp_Set.Value);
            Activity.Add("Stress", (int)numericUpDown_Stress_Set.Value);
            //Activity.Add("ActivityGoal", (int)numericUpDown_ActivityGoal_Set.Value);
            Activity.Add("FatBurning", (int)numericUpDown_FatBurning_Set.Value);


            Air.Add("Weather_Icon", comboBox_WeatherSet_Icon.SelectedIndex);
            Air.Add("Temperature", (int)numericUpDown_WeatherSet_Temp.Value);
            Air.Add("TemperatureMax", (int)numericUpDown_WeatherSet_MaxTemp.Value);
            Air.Add("TemperatureMin", (int)numericUpDown_WeatherSet_MinTemp.Value);

            Air.Add("UVindex", (int)numericUp_UVindex_Set.Value);
            Air.Add("AirQuality", (int)numericUpDown_AirQuality_Set.Value);
            Air.Add("Humidity", (int)numericUpDown_Humidity_Set.Value);
            Air.Add("WindForce", (int)numericUpDown_WindForce.Value);
            Air.Add("WindDirection", comboBox_WindDirection.SelectedIndex);
            Air.Add("CompassDirection", (int)numericUpDown_Compass_Set.Value);
            Air.Add("Altitude", (int)numericUpDown_Altitude_Set.Value);
            Air.Add("AirPressure", (int)numericUpDown_AirPressure_Set.Value);


            checkValue.Add("Bluetooth", checkBox_Bluetooth_Set.Checked);
            checkValue.Add("Alarm", checkBox_Alarm_Set.Checked);
            checkValue.Add("Lock", checkBox_Lock_Set.Checked);
            checkValue.Add("DND", checkBox_DND_Set.Checked);

            checkValue.Add("ShowTemperature", checkBox_WeatherSet_Temp.Checked);

        }

        /// <summary>Устанавливает данные для предпросмотра из выбранного набора параметров</summary>
        /// <param name="Activity">Данные активностей (год, число, день, часы, минуты, секунды,
        /// заряд, калории, ЧСС,  путь, шаги, цель шагов, PAI, StandUp, стресс, жиросжигание)</param>
        /// <param name="Air">Атмосферные данные (иконка погоды, текущая температура, максимальная температура, 
        /// минимальная температура, УФ индекс, качество воздуха, влажность, сила ветра,
        /// высота, давление)</param>
        /// <param name="checkValue">Дначение переключателей (Bluetooth, будильник, блокировка, DND, показ температуры)</param>
        public void SetValue(Dictionary<string, int> Activity, Dictionary<string, int> Air, Dictionary<string, List<int>> ForecastData,
            Dictionary<string, bool> checkValue)
        {
            int year;
            Activity.TryGetValue("Year", out year);
            int month;
            Activity.TryGetValue("Month", out month);
            int day;
            Activity.TryGetValue("Day", out day);
            int hour;
            Activity.TryGetValue("Hour", out hour);
            int min;
            Activity.TryGetValue("Minute", out min);
            int sec;
            Activity.TryGetValue("Second", out sec);

            int battery;
            Activity.TryGetValue("Battery", out battery);
            int calories;
            Activity.TryGetValue("Calories", out calories);
            int heartRate;
            Activity.TryGetValue("HeartRate", out heartRate);
            int distance;
            Activity.TryGetValue("Distance", out distance);
            int steps;
            Activity.TryGetValue("Steps", out steps);
            int stepsGoal;
            Activity.TryGetValue("StepsGoal", out stepsGoal);

            int PAI;
            Activity.TryGetValue("PAI", out PAI);
            int standUp;
            Activity.TryGetValue("StandUp", out standUp);
            int stress;
            Activity.TryGetValue("Stress", out stress);
            //int activityGoal;
            //Activity.TryGetValue("ActivityGoal", out activityGoal);
            int fatBurning;
            Activity.TryGetValue("FatBurning", out fatBurning);

            int weather_Icon;
            Air.TryGetValue("Weather_Icon", out weather_Icon);
            int temperature;
            Air.TryGetValue("Temperature", out temperature);
            int temperatureMax;
            Air.TryGetValue("TemperatureMax", out temperatureMax);
            int temperatureMin;
            Air.TryGetValue("TemperatureMin", out temperatureMin);

            int UVindex;
            Air.TryGetValue("UVindex", out UVindex);
            int airQuality;
            Air.TryGetValue("AirQuality", out airQuality);
            int humidity;
            Air.TryGetValue("Humidity", out humidity);
            int windForce;
            Air.TryGetValue("WindForce", out windForce);
            int windDirection;
            Air.TryGetValue("WindDirection", out windDirection);
            int compassDirection;
            Air.TryGetValue("CompassDirection", out compassDirection);
            int altitude;
            Air.TryGetValue("Altitude", out altitude);
            int airPressure;
            Air.TryGetValue("AirPressure", out airPressure);

            bool bluetooth;
            checkValue.TryGetValue("Bluetooth", out bluetooth);
            bool alarm;
            checkValue.TryGetValue("Alarm", out alarm);
            bool Lock;
            checkValue.TryGetValue("Lock", out Lock);
            bool DND;
            checkValue.TryGetValue("DND", out DND);

            bool showTemperature;
            checkValue.TryGetValue("ShowTemperature", out showTemperature);

            try
            {
                setValue = true;

                dateTimePicker_Date_Set.Value = new DateTime(year, month, day, hour, min, sec);
                dateTimePicker_Time_Set.Value = new DateTime(year, month, day, hour, min, sec);

                numericUpDown_Battery_Set.Value = battery;
                numericUpDown_Calories_Set.Value = calories;
                numericUpDown_HeartRate_Set.Value = heartRate;
                numericUpDown_Distance_Set.Value = distance;
                numericUpDown_Steps_Set.Value = steps;
                numericUpDown_Goal_Set.Value = stepsGoal;

                numericUpDown_PAI_Set.Value = PAI;
                numericUpDown_StandUp_Set.Value = standUp;
                numericUpDown_Stress_Set.Value = stress;
                //numericUpDown_ActivityGoal_Set.Value = activityGoal;
                numericUpDown_FatBurning_Set.Value = fatBurning;


                comboBox_WeatherSet_Icon.SelectedIndex = weather_Icon;
                numericUpDown_WeatherSet_Temp.Value = temperature;
                numericUpDown_WeatherSet_MaxTemp.Value = temperatureMax;
                numericUpDown_WeatherSet_MinTemp.Value = temperatureMin;


                numericUp_UVindex_Set.Value = UVindex;
                numericUpDown_AirQuality_Set.Value = airQuality;
                numericUpDown_Humidity_Set.Value = humidity;
                numericUpDown_WindForce.Value = windForce;
                comboBox_WindDirection.SelectedIndex = windDirection;
                numericUpDown_Compass_Set.Value = compassDirection;
                numericUpDown_Altitude_Set.Value = altitude;
                numericUpDown_AirPressure_Set.Value = airPressure;

                checkBox_Bluetooth_Set.Checked = bluetooth;
                checkBox_Alarm_Set.Checked = alarm;
                checkBox_Lock_Set.Checked = Lock;
                checkBox_DND_Set.Checked = DND;

                checkBox_WeatherSet_Temp.Checked = showTemperature;

                this.ForecastData = ForecastData;
            }
            finally
            {
                setValue = false;
            }

        }

        /// <summary>Устанавливает случайные данные для значений</summary>
        public void RandomValue(Random rnd)
        {
            setValue = true;

            DateTime now = DateTime.Now;
            //Random rnd = new Random();

            int year = now.Year;
            int month = rnd.Next(0, 12) + 1;
            int day = rnd.Next(0, 28) + 1;
            int hour = rnd.Next(0, 24);
            int min = rnd.Next(0, 60);
            int sec = rnd.Next(0, 60);
            dateTimePicker_Date_Set.Value = new DateTime(year, month, day, hour, min, sec);
            dateTimePicker_Time_Set.Value = new DateTime(year, month, day, hour, min, sec);

            numericUpDown_Battery_Set.Value = rnd.Next(0, 101);
            numericUpDown_Calories_Set.Value = rnd.Next(0, 500);
            numericUpDown_HeartRate_Set.Value = rnd.Next(45, 180);
            numericUpDown_Distance_Set.Value = rnd.Next(0, 15000);
            numericUpDown_Steps_Set.Value = rnd.Next(0, 15000);
            numericUpDown_Goal_Set.Value = rnd.Next(0, 15000);

            numericUpDown_PAI_Set.Value = rnd.Next(0, 150);
            numericUpDown_StandUp_Set.Value = rnd.Next(0, 13);
            numericUpDown_Stress_Set.Value = rnd.Next(0, 101);
            //numericUpDown_ActivityGoal_Set.Value = rnd.Next(0, 13);
            numericUpDown_FatBurning_Set.Value = rnd.Next(0, 35);


            comboBox_WeatherSet_Icon.SelectedIndex = rnd.Next(0, 29);
            numericUpDown_WeatherSet_Temp.Value = rnd.Next(-25, 35) + 1;
            numericUpDown_WeatherSet_MaxTemp.Value = rnd.Next(-25, 35) + 1;
            numericUpDown_WeatherSet_MinTemp.Value = numericUpDown_WeatherSet_MaxTemp.Value - rnd.Next(3, 10);


            numericUp_UVindex_Set.Value = rnd.Next(0, 13);
            numericUpDown_AirQuality_Set.Value = rnd.Next(0, 650);
            numericUpDown_Humidity_Set.Value = rnd.Next(30, 100);
            numericUpDown_WindForce.Value = rnd.Next(0, 13);
            comboBox_WindDirection.SelectedIndex = rnd.Next(0, 8);
            numericUpDown_Compass_Set.Value = rnd.Next(0, 360);
            numericUpDown_Altitude_Set.Value = rnd.Next(-100, 300);
            numericUpDown_AirPressure_Set.Value = rnd.Next(800, 1200);

            checkBox_Bluetooth_Set.Checked = rnd.Next(2) == 0 ? false : true;
            checkBox_Alarm_Set.Checked = rnd.Next(2) == 0 ? false : true;
            checkBox_Lock_Set.Checked = rnd.Next(2) == 0 ? false : true;
            checkBox_DND_Set.Checked = rnd.Next(2) == 0 ? false : true;

            checkBox_WeatherSet_Temp.Checked = rnd.Next(7) == 0 ? false : true;

            Dictionary<string, List<int>> ForecastData = new Dictionary<string, List<int>>();
            List<int> high = new List<int>();
            List<int> low = new List<int>();
            List<int> index = new List<int>(); 
            int tempOffset = rnd.Next(-10, 20);
            for (int i = 0; i < 9; i++)
            {
                int maxTemp = rnd.Next(-5, 5) + tempOffset;
                int minTemp = maxTemp - rnd.Next(3, 7);
                int iconIndex = rnd.Next(0, 25);

                high.Add(maxTemp);
                low.Add(minTemp);
                index.Add(iconIndex);
            }
            ForecastData.Add("high", high);
            ForecastData.Add("low", low);
            ForecastData.Add("index", index);
            this.ForecastData = ForecastData;

            setValue = false;
        }

        private void checkBox_WeatherSet_Temp_CheckedChanged(object sender, EventArgs e)
        {
            bool b = checkBox_WeatherSet_Temp.Checked;
            numericUpDown_WeatherSet_Temp.Enabled = b;
            numericUpDown_WeatherSet_MaxTemp.Enabled = b;
            numericUpDown_WeatherSet_MinTemp.Enabled = b;
        }
    }
}
