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
    public partial class UCtrl_Shortcuts_Elm : UserControl
    {
        private bool setValue; // режим задания параметров
        bool highlight_Step = false;
        bool highlight_Cal = false;
        bool highlight_Heart = false;
        bool highlight_PAI = false;
        bool highlight_Battery = false;
        bool highlight_Sunrise = false;
        bool highlight_Moon= false;
        bool highlight_BodyTemp = false;
        bool highlight_Weather = false;
        bool highlight_Stand = false;
        bool highlight_SPO2 = false;
        bool highlight_Altimeter = false;
        bool highlight_Stress = false;
        bool highlight_Countdown = false;
        bool highlight_Stopwatch = false;
        bool highlight_Alarm = false;
        bool highlight_Sleep = false;
        bool highlight_Altitude = false;
        bool highlight_Readiness = false;
        bool highlight_OutdoorRunning = false;
        bool highlight_Walking = false;
        bool highlight_OutdoorCycling = false;
        bool highlight_FreeTraining = false;
        bool highlight_PoolSwimming = false;
        bool highlight_OpenWaterSwimming = false;
        bool highlight_TrainingLoad = false;
        bool highlight_VO2max = false;
        bool highlight_RecoveryTime = false;
        bool highlight_BreathTrain = false;
        bool highlight_FatBurning = false;

        bool visibility_elements = false; // развернут список с элементами
        bool visibilityElement = true; // элемент оторажается на предпросмотре

        bool visibility_activities = false; // развернут список ярлыков с ативностями
        bool visibility_health = false; // развернут список ярлыков со здоровьем
        bool visibility_air = false; // развернут список ярлыков с атмосферой
        bool visibility_system = false; // развернут список системных ярлыков 
        bool visibility_trainings = false; // развернут список ярлыков с тренировками

        public int position = -1; // позиция в наборе элеменетов
        public string selectedElement; // название выбраного элемента

        Point cursorPos = new Point(0, 0);

        public UCtrl_Shortcuts_Elm()
        {
            InitializeComponent();
            setValue = false;

            button_ElementName.Controls.Add(pictureBox_Arrow_Right);
            button_ElementName.Controls.Add(pictureBox_Arrow_Down);
            button_ElementName.Controls.Add(pictureBox_NotShow);
            button_ElementName.Controls.Add(pictureBox_Show);
            button_ElementName.Controls.Add(pictureBox_Del);

            button_Activities.Controls.Add(pictureBox_Arrow_Right_Activities);
            button_Activities.Controls.Add(pictureBox_Arrow_Down_Activities);

            button_Health.Controls.Add(pictureBox_Arrow_Right_Health);
            button_Health.Controls.Add(pictureBox_Arrow_Down_Health);

            button_System.Controls.Add(pictureBox_Arrow_Right_System);
            button_System.Controls.Add(pictureBox_Arrow_Down_System);

            button_Air.Controls.Add(pictureBox_Arrow_Right_Air);
            button_Air.Controls.Add(pictureBox_Arrow_Down_Air);

            button_Trainings.Controls.Add(pictureBox_Arrow_Right_Trainings);
            button_Trainings.Controls.Add(pictureBox_Arrow_Down_Trainings);

            pictureBox_Arrow_Right.Location = new Point(1, 2);
            pictureBox_Arrow_Right.BackColor = Color.Transparent;

            pictureBox_Arrow_Down.Location = new Point(1, 2);
            pictureBox_Arrow_Down.BackColor = Color.Transparent;

            //pictureBox_Show.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6 , 2);
            pictureBox_NotShow.BackColor = Color.Transparent;
            pictureBox_Show.BackColor = Color.Transparent;

            //pictureBox_Del.Location = new Point(button_ElementName.Width - pictureBox_Del.Width - 8, 2);
            pictureBox_Del.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Activities.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Activities.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Activities.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Activities.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Health.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Health.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Health.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Health.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Air.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Air.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Air.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Air.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_System.Location = new Point(1, 2);
            pictureBox_Arrow_Right_System.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_System.Location = new Point(1, 2);
            pictureBox_Arrow_Down_System.BackColor = Color.Transparent;

            pictureBox_Arrow_Right_Trainings.Location = new Point(1, 2);
            pictureBox_Arrow_Right_Trainings.BackColor = Color.Transparent;
            pictureBox_Arrow_Down_Trainings.Location = new Point(1, 2);
            pictureBox_Arrow_Down_Trainings.BackColor = Color.Transparent;
        }

        [Browsable(true)]
        [Description("Происходит при изменении видимости элемента")]
        public event VisibleElementChangedHandler VisibleElementChanged;
        public delegate void VisibleElementChangedHandler(object sender, EventArgs eventArgs, bool visible);

        [Browsable(true)]
        [Description("Происходит при изменении видимости отдельного параметра в элементе")]
        public event VisibleOptionsChangedHandler VisibleOptionsChanged;
        public delegate void VisibleOptionsChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении положения параметров в элементе")]
        public event OptionsMovedHandler OptionsMoved;
        public delegate void OptionsMovedHandler(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions);

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event SelectChangedHandler SelectChanged;
        public delegate void SelectChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при удалении элемента")]
        public event DelElementHandler DelElement;
        public delegate void DelElementHandler(object sender, EventArgs eventArgs);

        private void button_ElementName_Click(object sender, EventArgs e)
        {
            visibility_elements = !visibility_elements;
            tableLayoutPanel1.Visible = visibility_elements;
            pictureBox_Arrow_Down.Visible = visibility_elements;
            pictureBox_Arrow_Right.Visible = !visibility_elements;
        }

        private void button_Activities_Click(object sender, EventArgs e)
        {
            visibility_activities = !visibility_activities;
            tableLayoutPanel_Activities.Visible = visibility_activities;
            pictureBox_Arrow_Down_Activities.Visible = visibility_activities;
            pictureBox_Arrow_Right_Activities.Visible = !visibility_activities;
        }

        private void button_Health_Click(object sender, EventArgs e)
        {
            visibility_health = !visibility_health;
            tableLayoutPanel_Health.Visible = visibility_health;
            pictureBox_Arrow_Down_Health.Visible = visibility_health;
            pictureBox_Arrow_Right_Health.Visible = !visibility_health;
        }

        private void button_Air_Click(object sender, EventArgs e)
        {
            visibility_air = !visibility_air;
            tableLayoutPanel_Air.Visible = visibility_air;
            pictureBox_Arrow_Down_Air.Visible = visibility_air;
            pictureBox_Arrow_Right_Air.Visible = !visibility_air;
        }

        private void button_System_Click(object sender, EventArgs e)
        {
            visibility_system = !visibility_system;
            tableLayoutPanel_System.Visible = visibility_system;
            pictureBox_Arrow_Down_System.Visible = visibility_system;
            pictureBox_Arrow_Right_System.Visible = !visibility_system;
        }

        private void button_Trainings_Click(object sender, EventArgs e)
        {
            visibility_trainings = !visibility_trainings;
            tableLayoutPanel_Trainings.Visible = visibility_trainings;
            pictureBox_Arrow_Down_Trainings.Visible = visibility_trainings;
            pictureBox_Arrow_Right_Trainings.Visible = !visibility_trainings;
        }

        public void ResetHighlightState()
        {
            selectedElement = "";

            highlight_Step = false;
            highlight_Cal = false;
            highlight_Heart = false;
            highlight_PAI = false;
            highlight_Battery = false;
            highlight_Sunrise = false;
            highlight_Moon = false;
            highlight_BodyTemp = false;
            highlight_Weather = false;
            highlight_Stand = false;
            highlight_SPO2 = false;
            highlight_Altimeter = false;
            highlight_Stress = false;
            highlight_Countdown = false;
            highlight_Stopwatch = false;
            highlight_Alarm = false;
            highlight_Sleep = false;
            highlight_Altitude = false;
            highlight_Readiness = false;
            highlight_OutdoorRunning = false;
            highlight_Walking = false;
            highlight_OutdoorCycling = false;
            highlight_FreeTraining = false;
            highlight_PoolSwimming = false;
            highlight_OpenWaterSwimming = false;
            highlight_TrainingLoad = false;
            highlight_VO2max = false;
            highlight_RecoveryTime = false;
            highlight_BreathTrain = false;
            highlight_FatBurning = false;

            SelectElement();
        }

        private void SelectElement()
        {
            if (highlight_Step)
            {
                panel_Step.BackColor = SystemColors.ActiveCaption;
                button_Step.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Step.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Step.BackColor = SystemColors.Control;
                button_Step.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Step.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Cal)
            {
                panel_Cal.BackColor = SystemColors.ActiveCaption;
                button_Cal.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Cal.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Cal.BackColor = SystemColors.Control;
                button_Cal.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Cal.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Heart)
            {
                panel_Heart.BackColor = SystemColors.ActiveCaption;
                button_Heart.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Heart.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Heart.BackColor = SystemColors.Control;
                button_Heart.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Heart.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_PAI)
            {
                panel_PAI.BackColor = SystemColors.ActiveCaption;
                button_PAI.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_PAI.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_PAI.BackColor = SystemColors.Control;
                button_PAI.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_PAI.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Battery)
            {
                panel_Battery.BackColor = SystemColors.ActiveCaption;
                button_Battery.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Battery.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Battery.BackColor = SystemColors.Control;
                button_Battery.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Battery.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Sunrise)
            {
                panel_Sunrise.BackColor = SystemColors.ActiveCaption;
                button_Sunrise.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Sunrise.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Sunrise.BackColor = SystemColors.Control;
                button_Sunrise.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Sunrise.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Moon)
            {
                panel_Moon.BackColor = SystemColors.ActiveCaption;
                button_Moon.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Moon.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Moon.BackColor = SystemColors.Control;
                button_Moon.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Moon.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_BodyTemp)
            {
                panel_BodyTemp.BackColor = SystemColors.ActiveCaption;
                button_BodyTemp.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_BodyTemp.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_BodyTemp.BackColor = SystemColors.Control;
                button_BodyTemp.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_BodyTemp.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Weather)
            {
                panel_Weather.BackColor = SystemColors.ActiveCaption;
                button_Weather.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Weather.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Weather.BackColor = SystemColors.Control;
                button_Weather.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Weather.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Stand)
            {
                panel_Stand.BackColor = SystemColors.ActiveCaption;
                button_Stand.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Stand.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Stand.BackColor = SystemColors.Control;
                button_Stand.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Stand.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_SPO2)
            {
                panel_SPO2.BackColor = SystemColors.ActiveCaption;
                button_SPO2.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_SPO2.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_SPO2.BackColor = SystemColors.Control;
                button_SPO2.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_SPO2.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Altimeter)
            {
                panel_Altimeter.BackColor = SystemColors.ActiveCaption;
                button_Altimeter.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Altimeter.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Altimeter.BackColor = SystemColors.Control;
                button_Altimeter.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Altimeter.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Stress)
            {
                panel_Stress.BackColor = SystemColors.ActiveCaption;
                button_Stress.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Stress.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Stress.BackColor = SystemColors.Control;
                button_Stress.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Stress.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Countdown)
            {
                panel_Countdown.BackColor = SystemColors.ActiveCaption;
                button_Countdown.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Countdown.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Countdown.BackColor = SystemColors.Control;
                button_Countdown.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Countdown.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Stopwatch)
            {
                panel_Stopwatch.BackColor = SystemColors.ActiveCaption;
                button_Stopwatch.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Stopwatch.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Stopwatch.BackColor = SystemColors.Control;
                button_Stopwatch.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Stopwatch.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Alarm)
            {
                panel_Alarm.BackColor = SystemColors.ActiveCaption;
                button_Alarm.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Alarm.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Alarm.BackColor = SystemColors.Control;
                button_Alarm.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Alarm.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Sleep)
            {
                panel_Sleep.BackColor = SystemColors.ActiveCaption;
                button_Sleep.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Sleep.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Sleep.BackColor = SystemColors.Control;
                button_Sleep.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Sleep.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Altitude)
            {
                panel_Altitude.BackColor = SystemColors.ActiveCaption;
                button_Altitude.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Altitude.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Altitude.BackColor = SystemColors.Control;
                button_Altitude.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Altitude.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Readiness)
            {
                panel_Readiness.BackColor = SystemColors.ActiveCaption;
                button_Readiness.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Readiness.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Readiness.BackColor = SystemColors.Control;
                button_Readiness.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Readiness.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_OutdoorRunning)
            {
                panel_OutdoorRunning.BackColor = SystemColors.ActiveCaption;
                button_OutdoorRunning.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_OutdoorRunning.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_OutdoorRunning.BackColor = SystemColors.Control;
                button_OutdoorRunning.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_OutdoorRunning.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_Walking)
            {
                panel_Walking.BackColor = SystemColors.ActiveCaption;
                button_Walking.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_Walking.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_Walking.BackColor = SystemColors.Control;
                button_Walking.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_Walking.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_OutdoorCycling)
            {
                panel_OutdoorCycling.BackColor = SystemColors.ActiveCaption;
                button_OutdoorCycling.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_OutdoorCycling.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_OutdoorCycling.BackColor = SystemColors.Control;
                button_OutdoorCycling.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_OutdoorCycling.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_FreeTraining)
            {
                panel_FreeTraining.BackColor = SystemColors.ActiveCaption;
                button_FreeTraining.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_FreeTraining.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_FreeTraining.BackColor = SystemColors.Control;
                button_FreeTraining.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_FreeTraining.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_PoolSwimming)
            {
                panel_PoolSwimming.BackColor = SystemColors.ActiveCaption;
                button_PoolSwimming.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_PoolSwimming.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_PoolSwimming.BackColor = SystemColors.Control;
                button_PoolSwimming.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_PoolSwimming.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_OpenWaterSwimming)
            {
                panel_OpenWaterSwimming.BackColor = SystemColors.ActiveCaption;
                button_OpenWaterSwimming.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_OpenWaterSwimming.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_OpenWaterSwimming.BackColor = SystemColors.Control;
                button_OpenWaterSwimming.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_OpenWaterSwimming.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_TrainingLoad)
            {
                panel_TrainingLoad.BackColor = SystemColors.ActiveCaption;
                button_TrainingLoad.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_TrainingLoad.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_TrainingLoad.BackColor = SystemColors.Control;
                button_TrainingLoad.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_TrainingLoad.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_VO2max)
            {
                panel_VO2max.BackColor = SystemColors.ActiveCaption;
                button_VO2max.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_VO2max.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_VO2max.BackColor = SystemColors.Control;
                button_VO2max.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_VO2max.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_RecoveryTime)
            {
                panel_RecoveryTime.BackColor = SystemColors.ActiveCaption;
                button_RecoveryTime.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_RecoveryTime.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_RecoveryTime.BackColor = SystemColors.Control;
                button_RecoveryTime.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_RecoveryTime.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_BreathTrain)
            {
                panel_BreathTrain.BackColor = SystemColors.ActiveCaption;
                button_BreathTrain.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_BreathTrain.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_BreathTrain.BackColor = SystemColors.Control;
                button_BreathTrain.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_BreathTrain.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }

            if (highlight_FatBurning)
            {
                panel_FatBurning.BackColor = SystemColors.ActiveCaption;
                button_FatBurning.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
                button_FatBurning.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
            }
            else
            {
                panel_FatBurning.BackColor = SystemColors.Control;
                button_FatBurning.FlatAppearance.MouseOverBackColor = SystemColors.Control;
                button_FatBurning.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            }
        }


        private void panel_Step_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Step";
            highlight_Step = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Cal_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Cal";
            highlight_Cal = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Heart_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Heart";
            highlight_Heart = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_PAI_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "PAI";
            highlight_PAI = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Battery_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Battery";
            highlight_Battery = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Sunrise_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Sunrise";
            highlight_Sunrise = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Moon_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Moon";
            highlight_Moon = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_BodyTemp_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "BodyTemp";
            highlight_BodyTemp = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Weather_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Weather";
            highlight_Weather = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Stand_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Stand";
            highlight_Stand = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_SPO2_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "SPO2";
            highlight_SPO2 = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Altimeter_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Altimeter";
            highlight_Altimeter = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Stress_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Stress";
            highlight_Stress = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Countdown_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Countdown";
            highlight_Countdown = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Stopwatch_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Stopwatch";
            highlight_Stopwatch = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Alarm_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Alarm";
            highlight_Alarm = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Sleep_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Sleep";
            highlight_Sleep = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Altitude_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Altitude";
            highlight_Altitude = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Readiness_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Readiness";
            highlight_Readiness = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_BreathTrain_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "BreathTrain";
            highlight_BreathTrain = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_FatBurning_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "FatBurning";
            highlight_FatBurning = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_OutdoorRunning_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "OutdoorRunning";
            highlight_OutdoorRunning = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_Walking_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "Walking";
            highlight_Walking = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_OutdoorCycling_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "OutdoorCycling";
            highlight_OutdoorCycling = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_FreeTraining_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "FreeTraining";
            highlight_FreeTraining = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_PoolSwimming_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "PoolSwimming";
            highlight_PoolSwimming = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_OpenWaterSwimming_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "OpenWaterSwimming";
            highlight_OpenWaterSwimming = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_TrainingLoad_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "TrainingLoad";
            highlight_TrainingLoad = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_VO2max_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "VO2max";
            highlight_VO2max = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }

        private void panel_RecoveryTime_Click(object sender, EventArgs e)
        {
            ResetHighlightState();
            selectedElement = "RecoveryTime";
            highlight_RecoveryTime = true;

            SelectElement();

            if (SelectChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                SelectChanged(this, eventArgs);
            }
        }


        #region перетягиваем элементы
       /* private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null)
            {
                panel.Tag = new object();
                cursorPos = Cursor.Position;
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null && panel.Tag != null)
            {
                int cursorX = Cursor.Position.X;
                int cursorY = Cursor.Position.Y;
                int dX = Math.Abs(cursorX - cursorPos.X);
                int dY = Math.Abs(cursorY - cursorPos.Y);
                if (dX > 5 || dY > 5)
                    panel.DoDragDrop(sender, DragDropEffects.Move);
                //  panel.DoDragDrop(sender, DragDropEffects.Move);
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            Control control = (Control)sender;
            Panel panel;
            if (control.GetType().Name == "Panel") panel = (Panel)sender;
            else panel = (Panel)control.Parent;
            if (panel != null && panel.Tag != null) panel.Tag = null;

            //((Control)sender).Tag = null;
        }

        private void tableLayoutPanel1_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Button)) && !e.Data.GetDataPresent(typeof(Panel)))
                return;

            e.Effect = e.AllowedEffect;
            Panel draggedPanel = (Panel)e.Data.GetData(typeof(Panel));
            if (draggedPanel == null)
            {
                Button draggedButton = (Button)e.Data.GetData(typeof(Button));
                if (draggedButton != null) draggedPanel = (Panel)draggedButton.Parent;
            }
            if (draggedPanel == null) return;

            Point pt = tableLayoutPanel1.PointToClient(new Point(e.X, e.Y));
            Control control = tableLayoutPanel1.GetChildAtPoint(pt);


            if (control != null)
            {
                var pos = tableLayoutPanel1.GetPositionFromControl(control);
                var posOld = tableLayoutPanel1.GetPositionFromControl(draggedPanel);
                if (pos != posOld && pos.Row < posOld.Row)
                {
                    if (pt.Y < control.Location.Y + draggedPanel.Height * 0.4)
                    {
                        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                        tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        //if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        //else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                    }
                }
                if (pos != posOld && pos.Row > posOld.Row)
                {
                    if (pt.Y > control.Location.Y + control.Height * 0.6)
                    {
                        tableLayoutPanel1.SetRow(control, pos.Row - 1);
                        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                        //if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                        //else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                    }
                }
                draggedPanel.Tag = null;
            }
        }

        private void tableLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            int cursorX = Cursor.Position.X;
            int cursorY = Cursor.Position.Y;
            int dX = Math.Abs(cursorX - cursorPos.X);
            int dY = Math.Abs(cursorY - cursorPos.Y);
            if (dX > 5 || dY > 5)
            {
                if (OptionsMoved != null)
                {
                    EventArgs eventArgs = new EventArgs();
                    OptionsMoved(this, eventArgs, GetOptionsPosition());
                }
            }
        }*/

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

        #endregion

        private void button_ElementName_SizeChanged(object sender, EventArgs e)
        {
            pictureBox_NotShow.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6, 2);
            pictureBox_Show.Location = new Point(button_ElementName.Width - pictureBox_Show.Width * 2 - 6, 2);

            pictureBox_Del.Location = new Point(button_ElementName.Width - pictureBox_Del.Width - 4, 2);

            if (tableLayoutPanel1.Height > 1065)
            {
                float currentDPI = tableLayoutPanel1.Height / 851f;
                button_ElementName.Image = (Image)(new Bitmap(button_ElementName.Image,
                    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));

                Control.ControlCollection controlCollection = tableLayoutPanel1.Controls;
                for (int i = 0; i < controlCollection.Count; i++)
                {
                    string name = controlCollection[i].GetType().Name;
                    if (name == "Panel")
                    {
                        Control.ControlCollection panelCollection = controlCollection[i].Controls;
                        for (int j = 0; j < panelCollection.Count; j++)
                        {
                            string nameButton = panelCollection[j].GetType().Name;
                            if (nameButton == "Button")
                            {
                                Button btn = (Button)panelCollection[j];
                                btn.Image = (Image)(new Bitmap(btn.Image,
                                    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));
                            }
                        }
                    }
                }

                controlCollection = button_ElementName.Controls;
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
            visibilityElement = false;
            pictureBox_Show.Visible = false;
            pictureBox_NotShow.Visible = true;
            SetColorActive();

            if (VisibleElementChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleElementChanged(this, eventArgs, visibilityElement);
            }
        }

        private void pictureBox_NotShow_Click(object sender, EventArgs e)
        {
            visibilityElement = true;
            pictureBox_Show.Visible = true;
            pictureBox_NotShow.Visible = false;
            SetColorActive();

            if (VisibleElementChanged != null)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleElementChanged(this, eventArgs, visibilityElement);
            }
        }

        private void pictureBox_Del_Click(object sender, EventArgs e)
        {
            if (DelElement != null)
            {
                EventArgs eventArgs = new EventArgs();
                DelElement(this, eventArgs);
            }
        }

        private void checkBox_Elements_CheckedChanged(object sender, EventArgs e)
        {
            if (VisibleOptionsChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                VisibleOptionsChanged(sender, eventArgs);
            }
        }

        /// <summary>Устанавливаем статус видимости для всего элемента</summary>
        public void SetVisibilityElementStatus(bool status)
        {
            visibilityElement = status;
            pictureBox_NotShow.Visible = !visibilityElement;
            pictureBox_Show.Visible = visibilityElement;
            SetColorActive();

        }

        /*/// <summary>Устанавливаем порядок опций в элементе</summary>
        public void SetOptionsPosition(Dictionary<int, string> elementOptions)
        {
            int elementCount = tableLayoutPanel1.RowCount;
            for (int key = elementCount - 1; key >= 0; key--)
            {
                Control panel = null;
                if (elementOptions.ContainsKey(elementCount - key))
                {
                    string name = elementOptions[elementCount - key];
                    switch (name)
                    {
                        case "Step":
                            panel = panel_Step;
                            break;
                        case "Heart":
                            panel = panel_Heart;
                            break;
                        case "SPO2":
                            panel = panel_SPO2;
                            break;
                        case "PAI":
                            panel = panel_PAI;
                            break;
                        case "Stress":
                            panel = panel_Stress;
                            break;
                        case "Weather":
                            panel = panel_Weather;
                            break;
                        case "Altimeter":
                            panel = panel_Altimeter;
                            break;
                        case "Sunrise":
                            panel = panel_Sunrise;
                            break;
                        case "Alarm":
                            panel = panel_Alarm;
                            break;
                        case "Sleep":
                            panel = panel_Sleep;
                            break;
                        case "Countdown":
                            panel = panel_Countdown;
                            break;
                        case "Stopwatch":
                            panel = panel_Stopwatch;
                            break;
                    }
                }
                position = key;
                if (panel == null)
                    continue;
                int realPos = tableLayoutPanel1.GetRow(panel);
                if (realPos == position)
                    continue;
                if (realPos < position)
                {
                    for (int i = realPos; i < position; i++)
                    {
                        Control panel2 = tableLayoutPanel1.GetControlFromPosition(0, i + 1);
                        if (panel2 == null) return;
                        tableLayoutPanel1.SetRow(panel2, i);
                        tableLayoutPanel1.SetRow(panel, i + 1);
                    }
                }
                else
                {
                    for (int i = realPos; i > position; i--)
                    {
                        Control panel2 = tableLayoutPanel1.GetControlFromPosition(0, i - 1);
                        if (panel2 == null)
                            return;
                        tableLayoutPanel1.SetRow(panel, i - 1);
                        tableLayoutPanel1.SetRow(panel2, i);
                    }
                }
            }
        }*/

        /*/// <summary>Получаем порядок опций в элементе</summary>
        public Dictionary<string, int> GetOptionsPosition()
        {
            Dictionary<string, int> elementOptions = new Dictionary<string, int>();
            int count = tableLayoutPanel1.RowCount;
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                Control panel = tableLayoutPanel1.GetControlFromPosition(0, i);
                switch (panel.Name)
                {
                    case "panel_Step":
                        elementOptions.Add("Step", count - i);
                        break;
                    case "panel_Heart":
                        elementOptions.Add("Heart", count - i);
                        break;
                    case "panel_SPO2":
                        elementOptions.Add("SPO2", count - i);
                        break;
                    case "panel_PAI":
                        elementOptions.Add("PAI", count - i);
                        break;
                    case "panel_Stress":
                        elementOptions.Add("Stress", count - i);
                        break;
                    case "panel_Weather":
                        elementOptions.Add("Weather", count - i);
                        break;
                    case "panel_Altimeter":
                        elementOptions.Add("Altimeter", count - i);
                        break;
                    case "panel_Sunrise":
                        elementOptions.Add("Sunrise", count - i);
                        break;
                    case "panel_Alarm":
                        elementOptions.Add("Alarm", count - i);
                        break;
                    case "panel_Sleep":
                        elementOptions.Add("Sleep", count - i);
                        break;
                    case "panel_Countdown":
                        elementOptions.Add("Countdown", count - i);
                        break;
                    case "panel_Stopwatch":
                        elementOptions.Add("Stopwatch", count - i);
                        break;
                }
            }
            return elementOptions;
        }*/

        public void SettingsClear()
        {
            setValue = true;

            /*Dictionary<int, string> elementOptions = new Dictionary<int, string>();
            elementOptions.Add(1, "Stopwatch");
            elementOptions.Add(2, "Countdown");
            elementOptions.Add(3, "Sleep");
            elementOptions.Add(4, "Alarm");
            elementOptions.Add(5, "Sunrise");
            elementOptions.Add(6, "Altimeter");
            elementOptions.Add(7, "Weather");
            elementOptions.Add(8, "Stress");
            elementOptions.Add(9, "PAI");
            elementOptions.Add(10, "SPO2");
            elementOptions.Add(11, "Heart");
            elementOptions.Add(12, "Step");
            SetOptionsPosition(elementOptions);*/

            checkBox_Step.Checked = false;
            checkBox_Cal.Checked = false;
            checkBox_Heart.Checked = false;
            checkBox_PAI.Checked = false;
            checkBox_Battery.Checked = false;
            checkBox_Sunrise.Checked = false;
            checkBox_Moon.Checked = false;
            checkBox_BodyTemp.Checked = false;
            checkBox_Weather.Checked = false;
            checkBox_Stand.Checked = false;
            checkBox_SPO2.Checked = false;
            checkBox_Altimeter.Checked = false;
            checkBox_Stress.Checked = false;
            checkBox_Countdown.Checked = false;
            checkBox_Stopwatch.Checked = false;
            checkBox_Alarm.Checked = false;
            checkBox_Sleep.Checked = false;
            checkBox_Altitude.Checked = false;
            checkBox_Readiness.Checked = false;
            checkBox_OutdoorRunning.Checked = false;
            checkBox_Walking.Checked = false;
            checkBox_OutdoorCycling.Checked = false;
            checkBox_FreeTraining.Checked = false;
            checkBox_PoolSwimming.Checked = false;
            checkBox_OpenWaterSwimming.Checked = false;
            checkBox_TrainingLoad.Checked = false;
            checkBox_VO2max.Checked = false;
            checkBox_RecoveryTime.Checked = false;
            checkBox_BreathTrain.Checked = false;
            checkBox_FatBurning.Checked = false;

            visibility_elements = false;
            tableLayoutPanel1.Visible = visibility_elements;
            pictureBox_Arrow_Down.Visible = visibility_elements;
            pictureBox_Arrow_Right.Visible = !visibility_elements;

            visibility_activities = false;
            tableLayoutPanel_Activities.Visible = visibility_activities;
            pictureBox_Arrow_Down_Activities.Visible = visibility_activities;
            pictureBox_Arrow_Right_Activities.Visible = !visibility_activities;

            visibility_health = false;
            tableLayoutPanel_Health.Visible = visibility_health;
            pictureBox_Arrow_Down_Health.Visible = visibility_health;
            pictureBox_Arrow_Right_Health.Visible = !visibility_health;

            visibility_air = false;
            tableLayoutPanel_Air.Visible = visibility_air;
            pictureBox_Arrow_Down_Air.Visible = visibility_air;
            pictureBox_Arrow_Right_Air.Visible = !visibility_air;

            visibility_system = false;
            tableLayoutPanel_System.Visible = visibility_system;
            pictureBox_Arrow_Down_System.Visible = visibility_system;
            pictureBox_Arrow_Right_System.Visible = !visibility_system;

            visibility_trainings = false;
            tableLayoutPanel_Trainings.Visible = visibility_trainings;
            pictureBox_Arrow_Down_Trainings.Visible = visibility_trainings;
            pictureBox_Arrow_Right_Trainings.Visible = !visibility_trainings;

            visibilityElement = true;
            pictureBox_Show.Visible = visibilityElement;
            pictureBox_NotShow.Visible = !visibilityElement;
            SetColorActive();

            setValue = false;
        }

        private void SetColorActive()
        {
            if (visibilityElement)
            {
                button_ElementName.ForeColor = SystemColors.ControlText;
                button_ElementName.BackColor = SystemColors.Control;
            }
            else
            {
                button_ElementName.ForeColor = SystemColors.GrayText;
                button_ElementName.BackColor = SystemColors.ControlLight;
            }
        }
    }
}
