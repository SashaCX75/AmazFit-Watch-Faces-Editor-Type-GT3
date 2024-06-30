using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        private void JSON_Scale(float scale, string DeviceName)
        {
            if (Watch_Face == null) return;
            if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
            Watch_Face.WatchFace_Info.DeviceName = DeviceName;
            if (Watch_Face.ScreenNormal != null)
            {
                if (Watch_Face.ScreenNormal.Background != null)
                {
                    Scale_IMG(Watch_Face.ScreenNormal.Background.BackgroundImage, scale);
                    Scale_FILL_RECT(Watch_Face.ScreenNormal.Background.BackgroundColor, scale);
                    Scale_Editable_Background(Watch_Face.ScreenNormal.Background.Editable_Background, scale);
                }
                if (Watch_Face.ScreenNormal.Elements != null)
                {
                    foreach (object elements in Watch_Face.ScreenNormal.Elements)
                    {
                        ScaleElements(elements, scale);
                    }
                }
            }
            if (Watch_Face.ScreenAOD != null)
            {
                if (Watch_Face.ScreenAOD.Background != null)
                {
                    Scale_IMG(Watch_Face.ScreenAOD.Background.BackgroundImage, scale);
                    Scale_FILL_RECT(Watch_Face.ScreenAOD.Background.BackgroundColor, scale);
                }
                if (Watch_Face.ScreenAOD.Elements != null)
                {
                    foreach (object elements in Watch_Face.ScreenAOD.Elements)
                    {
                        ScaleElements(elements, scale);
                    }
                }
            }
            if (Watch_Face.ElementEditablePointers != null)
            {
                Scale_EditablePointers(Watch_Face.ElementEditablePointers, scale);
            }
            if (Watch_Face.Editable_Elements != null && Watch_Face.Editable_Elements.Watchface_edit_group != null)
            {
                foreach(WATCHFACE_EDIT_GROUP edit_group in Watch_Face.Editable_Elements.Watchface_edit_group)
                {
                    edit_group.x = (int)Math.Round(edit_group.x * scale, MidpointRounding.AwayFromZero);
                    edit_group.y = (int)Math.Round(edit_group.y * scale, MidpointRounding.AwayFromZero);
                    edit_group.h = (int)Math.Round(edit_group.h * scale, MidpointRounding.AwayFromZero);
                    edit_group.w = (int)Math.Round(edit_group.w * scale, MidpointRounding.AwayFromZero);

                    edit_group.tips_x = (int)Math.Round(edit_group.tips_x * scale, MidpointRounding.AwayFromZero);
                    edit_group.tips_y = (int)Math.Round(edit_group.tips_y * scale, MidpointRounding.AwayFromZero);
                    edit_group.tips_width = (int)Math.Round(edit_group.tips_width * scale, MidpointRounding.AwayFromZero);
                    edit_group.tips_margin = (int)Math.Round(edit_group.tips_margin * scale, MidpointRounding.AwayFromZero);

                    foreach (object elements in edit_group.Elements)
                    {
                        ScaleElements(elements, scale);
                    }
                }
            }
            if (Watch_Face.Shortcuts != null)
            {
                ElementShortcuts elementShortcuts = Watch_Face.Shortcuts;
                Scale_IMG_CLICK(elementShortcuts.Step, scale);
                Scale_IMG_CLICK(elementShortcuts.Cal, scale);
                Scale_IMG_CLICK(elementShortcuts.Heart, scale);
                Scale_IMG_CLICK(elementShortcuts.PAI, scale);
                Scale_IMG_CLICK(elementShortcuts.Battery, scale);
                Scale_IMG_CLICK(elementShortcuts.Sunrise, scale);
                Scale_IMG_CLICK(elementShortcuts.Moon, scale);
                Scale_IMG_CLICK(elementShortcuts.BodyTemp, scale);
                Scale_IMG_CLICK(elementShortcuts.Weather, scale);
                Scale_IMG_CLICK(elementShortcuts.Stand, scale);
                Scale_IMG_CLICK(elementShortcuts.SPO2, scale);
                Scale_IMG_CLICK(elementShortcuts.Altimeter, scale);
                Scale_IMG_CLICK(elementShortcuts.Stress, scale);
                Scale_IMG_CLICK(elementShortcuts.Countdown, scale);
                Scale_IMG_CLICK(elementShortcuts.Stopwatch, scale);
                Scale_IMG_CLICK(elementShortcuts.Alarm, scale);
                Scale_IMG_CLICK(elementShortcuts.Sleep, scale);
                Scale_IMG_CLICK(elementShortcuts.Altitude, scale);
                Scale_IMG_CLICK(elementShortcuts.Readiness, scale);
                Scale_IMG_CLICK(elementShortcuts.OutdoorRunning, scale);
                Scale_IMG_CLICK(elementShortcuts.Walking, scale);
                Scale_IMG_CLICK(elementShortcuts.OutdoorCycling, scale);
                Scale_IMG_CLICK(elementShortcuts.FreeTraining, scale);
                Scale_IMG_CLICK(elementShortcuts.PoolSwimming, scale);
                Scale_IMG_CLICK(elementShortcuts.OpenWaterSwimming, scale);
                Scale_IMG_CLICK(elementShortcuts.TrainingLoad, scale);
                Scale_IMG_CLICK(elementShortcuts.VO2max, scale);
                Scale_IMG_CLICK(elementShortcuts.RecoveryTime, scale);
                Scale_IMG_CLICK(elementShortcuts.BreathTrain, scale);
                Scale_IMG_CLICK(elementShortcuts.FatBurning, scale);
            }
            if (Watch_Face.TopImage != null && Watch_Face.TopImage.Icon != null)
            {
                Scale_IMG(Watch_Face.TopImage.Icon, scale);
            }
            if (Watch_Face.Buttons != null && Watch_Face.Buttons.Button != null && Watch_Face.Buttons.Button.Count > 0)
            {
                for (int index = 0; index < Watch_Face.Buttons.Button.Count; index++)
                {
                    Button button = Watch_Face.Buttons.Button[index];
                    Scale_Button(button, scale);
                }
            }
        }

        private void Scale_FILL_RECT(hmUI_widget_FILL_RECT fill_rect, float scale)
        {
            if (fill_rect == null) return;
            fill_rect.x = (int)Math.Round(fill_rect.x * scale, MidpointRounding.AwayFromZero);
            fill_rect.y = (int)Math.Round(fill_rect.y * scale, MidpointRounding.AwayFromZero);
            fill_rect.w = (int)Math.Round((fill_rect.w * scale), MidpointRounding.AwayFromZero);
            fill_rect.h = (int)Math.Round((fill_rect.h * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_IMG(hmUI_widget_IMG img, float scale)
        {
            if (img == null) return;
            img.x = (int)Math.Round(img.x * scale, MidpointRounding.AwayFromZero);
            img.y = (int)Math.Round(img.y * scale, MidpointRounding.AwayFromZero);
            if(img.w != null) img.w = (int)Math.Round((double)(img.w * scale), MidpointRounding.AwayFromZero);
            if (img.h != null) img.h = (int)Math.Round((double)(img.h * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_Editable_Background(Editable_Background edit_bg, float scale)
        {
            if (edit_bg == null) return;
            edit_bg.w = (int)Math.Round((double)(edit_bg.w * scale), MidpointRounding.AwayFromZero);
            edit_bg.h = (int)Math.Round((double)(edit_bg.h * scale), MidpointRounding.AwayFromZero);

            edit_bg.tips_x = (int)Math.Round((double)(edit_bg.tips_x * scale), MidpointRounding.AwayFromZero);
            edit_bg.tips_y = (int)Math.Round((double)(edit_bg.tips_y * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_EditablePointers(ElementEditablePointers edit_pointers, float scale)
        {
            if (edit_pointers == null) return;

            edit_pointers.tips_x = (int)Math.Round((double)(edit_pointers.tips_x * scale), MidpointRounding.AwayFromZero);
            edit_pointers.tips_y = (int)Math.Round((double)(edit_pointers.tips_y * scale), MidpointRounding.AwayFromZero);

            foreach(PointersList pointers in edit_pointers.config)
            {
                Scale_EDITABLE_POINTER(pointers.second, scale);
                Scale_EDITABLE_POINTER(pointers.minute, scale);
                Scale_EDITABLE_POINTER(pointers.hour, scale);
            }
            Scale_IMG(edit_pointers.cover, scale);
        }

        private void Scale_Frame_Animation(hmUI_widget_IMG_ANIM anim_frame, float scale)
        {
            if (anim_frame == null) return;
            anim_frame.x = (int)Math.Round(anim_frame.x * scale, MidpointRounding.AwayFromZero);
            anim_frame.y = (int)Math.Round(anim_frame.y * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_Motion_Animation(Motion_Animation anim_motion, float scale)
        {
            if (anim_motion == null) return;
            anim_motion.x_start = (int)Math.Round(anim_motion.x_start * scale, MidpointRounding.AwayFromZero);
            anim_motion.y_start = (int)Math.Round(anim_motion.y_start * scale, MidpointRounding.AwayFromZero);
            anim_motion.x_end = (int)Math.Round(anim_motion.x_end * scale, MidpointRounding.AwayFromZero);
            anim_motion.y_end = (int)Math.Round(anim_motion.y_end * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_Rotate_Animation(Rotate_Animation anim_rotate, float scale)
        {
            if (anim_rotate == null) return;
            anim_rotate.center_x = (int)Math.Round(anim_rotate.center_x * scale, MidpointRounding.AwayFromZero);
            anim_rotate.center_y = (int)Math.Round(anim_rotate.center_y * scale, MidpointRounding.AwayFromZero);
            anim_rotate.pos_x = (int)Math.Round(anim_rotate.pos_x * scale, MidpointRounding.AwayFromZero);
            anim_rotate.pos_y = (int)Math.Round(anim_rotate.pos_y * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_Animation(ElementAnimation anim, float scale)
        {
            if(anim == null) return;
            if (anim.Frame_Animation_List != null && anim.Frame_Animation_List.Frame_Animation != null &&
                anim.Frame_Animation_List.Frame_Animation.Count > 0)
            {
                for (int i = 0; i < anim.Frame_Animation_List.Frame_Animation.Count; i++)
                {
                    Scale_Frame_Animation(anim.Frame_Animation_List.Frame_Animation[i], scale);
                }
            }
            if (anim.Motion_Animation_List != null && anim.Motion_Animation_List.Motion_Animation != null && 
                anim.Motion_Animation_List.Motion_Animation.Count > 0)
            {
                for(int i =0; i < anim.Motion_Animation_List.Motion_Animation.Count; i++)
                {
                    Scale_Motion_Animation(anim.Motion_Animation_List.Motion_Animation[i], scale);
                }
            }
            if (anim.Rotate_Animation_List != null && anim.Rotate_Animation_List.Rotate_Animation != null &&
                anim.Rotate_Animation_List.Rotate_Animation.Count > 0)
            {
                for (int i = 0; i < anim.Rotate_Animation_List.Rotate_Animation.Count; i++)
                {
                    Scale_Rotate_Animation(anim.Rotate_Animation_List.Rotate_Animation[i], scale);
                }
            }
        }

        private void ScaleElements(object elements, float scale)
        {
            string www = elements.GetType().Name;
            switch (elements.GetType().Name)
            {
                case "ElementDigitalTime":
                    ElementDigitalTime elementDigitalTime = (ElementDigitalTime)elements;
                    Scale_AmPm(elementDigitalTime.AmPm, scale);
                    Scale_IMG_NUMBER(elementDigitalTime.Hour, scale);
                    Scale_IMG_NUMBER(elementDigitalTime.Minute, scale);
                    Scale_IMG_NUMBER(elementDigitalTime.Second, scale);

                    Scale_TEXT(elementDigitalTime.Hour_Font, scale);
                    Scale_TEXT(elementDigitalTime.Minute_Font, scale);
                    Scale_TEXT(elementDigitalTime.Second_Font, scale);

                    Scale_TEXT(elementDigitalTime.Hour_min_Font, scale);
                    Scale_TEXT(elementDigitalTime.Hour_min_sec_Font, scale);

                    Scale_IMG_NUMBER(elementDigitalTime.Hour_rotation, scale);
                    Scale_IMG_NUMBER(elementDigitalTime.Minute_rotation, scale);
                    Scale_IMG_NUMBER(elementDigitalTime.Second_rotation, scale);

                    Scale_Text_Circle(elementDigitalTime.Hour_circle, scale);
                    Scale_Text_Circle(elementDigitalTime.Minute_circle, scale);
                    Scale_Text_Circle(elementDigitalTime.Second_circle, scale);
                    break;
                case "ElementDigitalTime_v2":
                    ElementDigitalTime_v2 elementDigitalTime_v2 = (ElementDigitalTime_v2)elements;
                    if (elementDigitalTime_v2.Group_Hour != null)
                    {
                        Scale_IMG_NUMBER(elementDigitalTime_v2.Group_Hour.Number, scale);
                        Scale_TEXT(elementDigitalTime_v2.Group_Hour.Number_Font, scale);
                        Scale_IMG_NUMBER(elementDigitalTime_v2.Group_Hour.Text_rotation, scale);
                        Scale_Text_Circle(elementDigitalTime_v2.Group_Hour.Text_circle, scale);
                    }
                    if (elementDigitalTime_v2.Group_Minute != null)
                    {
                        Scale_IMG_NUMBER(elementDigitalTime_v2.Group_Minute.Number, scale);
                        Scale_TEXT(elementDigitalTime_v2.Group_Minute.Number_Font, scale);
                        Scale_IMG_NUMBER(elementDigitalTime_v2.Group_Minute.Text_rotation, scale);
                        Scale_Text_Circle(elementDigitalTime_v2.Group_Minute.Text_circle, scale);
                    }
                    if (elementDigitalTime_v2.Group_Second != null)
                    {
                        Scale_IMG_NUMBER(elementDigitalTime_v2.Group_Second.Number, scale);
                        Scale_TEXT(elementDigitalTime_v2.Group_Second.Number_Font, scale);
                        Scale_IMG_NUMBER(elementDigitalTime_v2.Group_Second.Text_rotation, scale);
                        Scale_Text_Circle(elementDigitalTime_v2.Group_Second.Text_circle, scale);
                    }
                    Scale_AmPm(elementDigitalTime_v2.AmPm, scale);
                    Scale_TEXT(elementDigitalTime_v2.Hour_Min_Font, scale);
                    Scale_TEXT(elementDigitalTime_v2.Hour_Min_Sec_Font, scale);
                    break;
                case "ElementAnalogTime":
                    ElementAnalogTime elementAnalogTime = (ElementAnalogTime)elements;
                    Scale_IMG_POINTER(elementAnalogTime.Hour, scale);
                    Scale_IMG_POINTER(elementAnalogTime.Minute, scale);
                    Scale_IMG_POINTER(elementAnalogTime.Second, scale);
                    break;
                case "ElementAnalogTimePro":
                    ElementAnalogTimePro elementAnalogTimePro = (ElementAnalogTimePro)elements;
                    Scale_IMG_POINTER(elementAnalogTimePro.Hour, scale);
                    Scale_IMG_POINTER(elementAnalogTimePro.Minute, scale);
                    Scale_IMG_POINTER(elementAnalogTimePro.Second, scale);
                    break;
                case "ElementDateDay":
                    ElementDateDay elementDateDay = (ElementDateDay)elements;
                    Scale_IMG_NUMBER(elementDateDay.Number, scale);
                    Scale_TEXT(elementDateDay.Number_Font, scale);
                    Scale_TEXT(elementDateDay.Day_Month_Font, scale);
                    Scale_TEXT(elementDateDay.Day_Month_Year_Font, scale);
                    Scale_IMG_NUMBER(elementDateDay.Text_rotation, scale);
                    Scale_Text_Circle(elementDateDay.Text_circle, scale);
                    Scale_IMG_POINTER(elementDateDay.Pointer, scale);
                    break;
                case "ElementDateMonth":
                    ElementDateMonth elementDateMonth = (ElementDateMonth)elements;
                    Scale_IMG_NUMBER(elementDateMonth.Number, scale);
                    Scale_TEXT(elementDateMonth.Number_Font, scale);
                    Scale_TEXT(elementDateMonth.Month_Font, scale);
                    Scale_IMG_NUMBER(elementDateMonth.Text_rotation, scale);
                    Scale_Text_Circle(elementDateMonth.Text_circle, scale);
                    Scale_IMG_POINTER(elementDateMonth.Pointer, scale);
                    Scale_IMG_LEVEL(elementDateMonth.Images, scale);
                    break;
                case "ElementDateYear":
                    ElementDateYear elementDateYear = (ElementDateYear)elements;
                    Scale_IMG_NUMBER(elementDateYear.Number, scale);
                    Scale_TEXT(elementDateYear.Number_Font, scale);
                    Scale_IMG_NUMBER(elementDateYear.Text_rotation, scale);
                    Scale_Text_Circle(elementDateYear.Text_circle, scale);
                    Scale_IMG(elementDateYear.Icon, scale);
                    break;
                case "ElementDateWeek":
                    ElementDateWeek elementDateWeek = (ElementDateWeek)elements;
                    Scale_IMG_POINTER(elementDateWeek.Pointer, scale);
                    Scale_IMG_LEVEL(elementDateWeek.Images, scale);
                    Scale_TEXT(elementDateWeek.DayOfWeek_Font, scale);
                    break;
                case "ElementStatuses":
                    ElementStatuses elementStatuses = (ElementStatuses)elements;
                    Scale_IMG_STATUS(elementStatuses.Alarm, scale);
                    Scale_IMG_STATUS(elementStatuses.Bluetooth, scale);
                    Scale_IMG_STATUS(elementStatuses.DND, scale);
                    Scale_IMG_STATUS(elementStatuses.Lock, scale);
                    break;
                case "ElementSteps":
                    ElementSteps elementSteps = (ElementSteps)elements;
                    Scale_IMG_LEVEL(elementSteps.Images, scale);
                    Scale_IMG_PROGRESS(elementSteps.Segments, scale);
                    Scale_IMG_NUMBER(elementSteps.Number, scale);
                    Scale_TEXT(elementSteps.Number_Font, scale);
                    Scale_IMG_NUMBER(elementSteps.Text_rotation, scale);
                    Scale_Text_Circle(elementSteps.Text_circle, scale);
                    Scale_IMG_NUMBER(elementSteps.Number_Target, scale);
                    Scale_TEXT(elementSteps.Number_Target_Font, scale);
                    Scale_IMG_NUMBER(elementSteps.Text_rotation_Target, scale);
                    Scale_Text_Circle(elementSteps.Text_circle_Target, scale);
                    Scale_IMG_POINTER(elementSteps.Pointer, scale);
                    Scale_Circle_Scale(elementSteps.Circle_Scale, scale);
                    Scale_Linear_Scale(elementSteps.Linear_Scale, scale);
                    Scale_IMG(elementSteps.Icon, scale);
                    break;
                case "ElementBattery":
                    ElementBattery elementBattery = (ElementBattery)elements;
                    Scale_IMG_LEVEL(elementBattery.Images, scale);
                    Scale_IMG_PROGRESS(elementBattery.Segments, scale);
                    Scale_IMG_NUMBER(elementBattery.Number, scale);
                    Scale_TEXT(elementBattery.Number_Font, scale);
                    Scale_IMG_NUMBER(elementBattery.Text_rotation, scale);
                    Scale_Text_Circle(elementBattery.Text_circle, scale);
                    Scale_IMG_POINTER(elementBattery.Pointer, scale);
                    Scale_Circle_Scale(elementBattery.Circle_Scale, scale);
                    Scale_Linear_Scale(elementBattery.Linear_Scale, scale);
                    Scale_IMG(elementBattery.Icon, scale);
                    break;
                case "ElementCalories":
                    ElementCalories elementCalories = (ElementCalories)elements;
                    Scale_IMG_LEVEL(elementCalories.Images, scale);
                    Scale_IMG_PROGRESS(elementCalories.Segments, scale);
                    Scale_IMG_NUMBER(elementCalories.Number, scale);
                    Scale_TEXT(elementCalories.Number_Font, scale);
                    Scale_IMG_NUMBER(elementCalories.Text_rotation, scale);
                    Scale_Text_Circle(elementCalories.Text_circle, scale);
                    Scale_IMG_NUMBER(elementCalories.Number_Target, scale);
                    Scale_TEXT(elementCalories.Number_Target_Font, scale);
                    Scale_IMG_NUMBER(elementCalories.Text_rotation_Target, scale);
                    Scale_Text_Circle(elementCalories.Text_circle_Target, scale);
                    Scale_IMG_POINTER(elementCalories.Pointer, scale);
                    Scale_Circle_Scale(elementCalories.Circle_Scale, scale);
                    Scale_Linear_Scale(elementCalories.Linear_Scale, scale);
                    Scale_IMG(elementCalories.Icon, scale);
                    break;
                case "ElementHeart":
                    ElementHeart elementHeart = (ElementHeart)elements;
                    Scale_IMG_LEVEL(elementHeart.Images, scale);
                    Scale_IMG_PROGRESS(elementHeart.Segments, scale);
                    Scale_IMG_NUMBER(elementHeart.Number, scale);
                    Scale_TEXT(elementHeart.Number_Font, scale);
                    Scale_IMG_NUMBER(elementHeart.Text_rotation, scale);
                    Scale_Text_Circle(elementHeart.Text_circle, scale);
                    Scale_IMG_POINTER(elementHeart.Pointer, scale);
                    Scale_Circle_Scale(elementHeart.Circle_Scale, scale);
                    Scale_Linear_Scale(elementHeart.Linear_Scale, scale);
                    Scale_IMG(elementHeart.Icon, scale);
                    break;
                case "ElementPAI":
                    ElementPAI elementPAI = (ElementPAI)elements;
                    Scale_IMG_LEVEL(elementPAI.Images, scale);
                    Scale_IMG_PROGRESS(elementPAI.Segments, scale);
                    Scale_IMG_NUMBER(elementPAI.Number, scale);
                    //Scale_TEXT(elementPAI.Number_Font, scale);
                    Scale_IMG_NUMBER(elementPAI.Number_Target, scale);
                    Scale_TEXT(elementPAI.Number_Target_Font, scale);
                    Scale_IMG_NUMBER(elementPAI.Text_rotation_Target, scale);
                    Scale_Text_Circle(elementPAI.Text_circle_Target, scale);
                    Scale_IMG_POINTER(elementPAI.Pointer, scale);
                    Scale_Circle_Scale(elementPAI.Circle_Scale, scale);
                    Scale_Linear_Scale(elementPAI.Linear_Scale, scale);
                    Scale_IMG(elementPAI.Icon, scale);
                    break;
                case "ElementDistance":
                    ElementDistance elementDistance = (ElementDistance)elements;
                    Scale_IMG_NUMBER(elementDistance.Number, scale);
                    Scale_TEXT(elementDistance.Number_Font, scale);
                    Scale_IMG_NUMBER(elementDistance.Text_rotation, scale);
                    Scale_Text_Circle(elementDistance.Text_circle, scale);
                    Scale_IMG(elementDistance.Icon, scale);
                    break;
                case "ElementStand":
                    ElementStand elementStand = (ElementStand)elements;
                    Scale_IMG_LEVEL(elementStand.Images, scale);
                    Scale_IMG_PROGRESS(elementStand.Segments, scale);
                    Scale_IMG_NUMBER(elementStand.Number, scale);
                    Scale_TEXT(elementStand.Number_Font, scale);
                    Scale_IMG_NUMBER(elementStand.Text_rotation, scale);
                    Scale_Text_Circle(elementStand.Text_circle, scale);
                    Scale_IMG_NUMBER(elementStand.Number_Target, scale);
                    Scale_TEXT(elementStand.Number_Target_Font, scale);
                    Scale_IMG_NUMBER(elementStand.Text_rotation_Target, scale);
                    Scale_Text_Circle(elementStand.Text_circle_Target, scale);
                    Scale_IMG_POINTER(elementStand.Pointer, scale);
                    Scale_Circle_Scale(elementStand.Circle_Scale, scale);
                    Scale_Linear_Scale(elementStand.Linear_Scale, scale);
                    Scale_IMG(elementStand.Icon, scale);
                    break;
                case "ElementActivity":
                    ElementActivity elementActivity = (ElementActivity)elements;
                    Scale_IMG_LEVEL(elementActivity.Images, scale);
                    Scale_IMG_PROGRESS(elementActivity.Segments, scale);
                    Scale_IMG_NUMBER(elementActivity.Number, scale);
                    Scale_TEXT(elementActivity.Number_Font, scale);
                    Scale_IMG_NUMBER(elementActivity.Number_Target, scale);
                    Scale_TEXT(elementActivity.Number_Target_Font, scale);
                    Scale_IMG_POINTER(elementActivity.Pointer, scale);
                    Scale_Circle_Scale(elementActivity.Circle_Scale, scale);
                    Scale_Linear_Scale(elementActivity.Linear_Scale, scale);
                    Scale_IMG(elementActivity.Icon, scale);
                    break;
                case "ElementSpO2":
                    ElementSpO2 elementSpO2 = (ElementSpO2)elements;
                    Scale_IMG_NUMBER(elementSpO2.Number, scale);
                    Scale_TEXT(elementSpO2.Number_Font, scale);
                    Scale_IMG_NUMBER(elementSpO2.Text_rotation, scale);
                    Scale_Text_Circle(elementSpO2.Text_circle, scale);
                    Scale_IMG(elementSpO2.Icon, scale);
                    break;
                case "ElementStress":
                    ElementStress elementStress = (ElementStress)elements;
                    Scale_IMG_LEVEL(elementStress.Images, scale);
                    Scale_IMG_PROGRESS(elementStress.Segments, scale);
                    Scale_IMG_NUMBER(elementStress.Number, scale);
                    Scale_TEXT(elementStress.Number_Font, scale);
                    Scale_IMG_POINTER(elementStress.Pointer, scale);
                    Scale_IMG(elementStress.Icon, scale);
                    break;
                case "ElementFatBurning":
                    ElementFatBurning elementFatBurning = (ElementFatBurning)elements;
                    Scale_IMG_LEVEL(elementFatBurning.Images, scale);
                    Scale_IMG_PROGRESS(elementFatBurning.Segments, scale);
                    Scale_IMG_NUMBER(elementFatBurning.Number, scale);
                    Scale_TEXT(elementFatBurning.Number_Font, scale);
                    Scale_IMG_NUMBER(elementFatBurning.Text_rotation, scale);
                    Scale_Text_Circle(elementFatBurning.Text_circle, scale);
                    Scale_IMG_NUMBER(elementFatBurning.Number_Target, scale);
                    Scale_TEXT(elementFatBurning.Number_Target_Font, scale);
                    Scale_IMG_NUMBER(elementFatBurning.Text_rotation_Target, scale);
                    Scale_Text_Circle(elementFatBurning.Text_circle_Target, scale);
                    Scale_IMG_POINTER(elementFatBurning.Pointer, scale);
                    Scale_Circle_Scale(elementFatBurning.Circle_Scale, scale);
                    Scale_Linear_Scale(elementFatBurning.Linear_Scale, scale);
                    Scale_IMG(elementFatBurning.Icon, scale);
                    break;

                case "ElementWeather":
                    ElementWeather elementWeather = (ElementWeather)elements;
                    Scale_IMG_LEVEL(elementWeather.Images, scale);
                    Scale_IMG_NUMBER(elementWeather.Number, scale);
                    Scale_TEXT(elementWeather.Number_Font, scale);
                    Scale_IMG_NUMBER(elementWeather.Number_Min, scale);
                    Scale_TEXT(elementWeather.Number_Min_Font, scale);
                    Scale_IMG_NUMBER(elementWeather.Number_Max, scale);
                    Scale_TEXT(elementWeather.Number_Max_Font, scale);
                    Scale_IMG_NUMBER(elementWeather.Text_Min_rotation, scale);
                    Scale_Text_Circle(elementWeather.Text_Min_circle, scale);
                    Scale_IMG_NUMBER(elementWeather.Text_Max_rotation, scale);
                    Scale_Text_Circle(elementWeather.Text_Max_circle, scale);
                    Scale_TEXT(elementWeather.Number_Min_Max_Font, scale);
                    Scale_TEXT(elementWeather.City_Name, scale);
                    Scale_IMG(elementWeather.Icon, scale);
                    break;
                case "ElementWeather_v2":
                    ElementWeather_v2 elementWeather_v2 = (ElementWeather_v2)elements;
                    Scale_WeatherGroup(elementWeather_v2.Group_Current, scale);
                    Scale_WeatherGroup(elementWeather_v2.Group_Min, scale);
                    Scale_WeatherGroup(elementWeather_v2.Group_Max, scale);
                    Scale_WeatherGroup(elementWeather_v2.Group_Max_Min, scale);
                    Scale_IMG_LEVEL(elementWeather_v2.Images, scale);
                    Scale_TEXT(elementWeather_v2.City_Name, scale);
                    Scale_IMG(elementWeather_v2.Icon, scale);
                    break;
                case "Element_Weather_FewDays":
                    Element_Weather_FewDays elementWeather_FewDays = (Element_Weather_FewDays)elements;

                    Scale_Diagram(elementWeather_FewDays.Diagram, scale);
                    Scale_IMG_NUMBER(elementWeather_FewDays.Number_Max, scale);
                    Scale_TEXT(elementWeather_FewDays.Number_Font_Max, scale);
                    Scale_IMG_NUMBER(elementWeather_FewDays.Number_Min, scale);
                    Scale_TEXT(elementWeather_FewDays.Number_Font_Min, scale);
                    Scale_IMG_NUMBER(elementWeather_FewDays.Number_MaxMin, scale);
                    Scale_TEXT(elementWeather_FewDays.Number_Font_MaxMin, scale);
                    Scale_IMG_NUMBER(elementWeather_FewDays.Number_Average, scale);
                    Scale_TEXT(elementWeather_FewDays.Number_Font_Average, scale);
                    Scale_IMG_LEVEL(elementWeather_FewDays.DayOfWeek_Images, scale);
                    Scale_TEXT(elementWeather_FewDays.DayOfWeek_Font, scale);
                    Scale_IMG_LEVEL(elementWeather_FewDays.Images, scale);
                    Scale_IMG(elementWeather_FewDays.Icon, scale);
                    break;
                case "ElementUVIndex":
                    ElementUVIndex elementUVIndex = (ElementUVIndex)elements;
                    Scale_IMG_LEVEL(elementUVIndex.Images, scale);
                    Scale_IMG_PROGRESS(elementUVIndex.Segments, scale);
                    Scale_IMG_NUMBER(elementUVIndex.Number, scale);
                    Scale_TEXT(elementUVIndex.Number_Font, scale);
                    Scale_IMG_POINTER(elementUVIndex.Pointer, scale);
                    Scale_IMG(elementUVIndex.Icon, scale);
                    break;
                case "ElementHumidity":
                    ElementHumidity elementHumidity = (ElementHumidity)elements;
                    Scale_IMG_LEVEL(elementHumidity.Images, scale);
                    Scale_IMG_PROGRESS(elementHumidity.Segments, scale);
                    Scale_IMG_NUMBER(elementHumidity.Number, scale);
                    Scale_TEXT(elementHumidity.Number_Font, scale);
                    Scale_IMG_POINTER(elementHumidity.Pointer, scale);
                    Scale_IMG(elementHumidity.Icon, scale);
                    break;
                case "ElementAltimeter":
                    ElementAltimeter elementAltimeter = (ElementAltimeter)elements;
                    Scale_IMG_NUMBER(elementAltimeter.Number, scale);
                    Scale_TEXT(elementAltimeter.Number_Font, scale);
                    Scale_IMG_NUMBER(elementAltimeter.Number_Target, scale);
                    Scale_TEXT(elementAltimeter.Number_Target_Font, scale);
                    Scale_IMG_POINTER(elementAltimeter.Pointer, scale);
                    Scale_IMG(elementAltimeter.Icon, scale);
                    break;
                case "ElementSunrise":
                    ElementSunrise elementSunrise = (ElementSunrise)elements;
                    Scale_IMG_LEVEL(elementSunrise.Images, scale);
                    Scale_IMG_PROGRESS(elementSunrise.Segments, scale);

                    Scale_IMG_NUMBER(elementSunrise.Sunrise, scale);
                    Scale_TEXT(elementSunrise.Sunrise_Font, scale);
                    Scale_IMG_NUMBER(elementSunrise.Sunrise_rotation, scale);
                    Scale_Text_Circle(elementSunrise.Sunrise_circle, scale);

                    Scale_IMG_NUMBER(elementSunrise.Sunset, scale);
                    Scale_TEXT(elementSunrise.Sunset_Font, scale);
                    Scale_IMG_NUMBER(elementSunrise.Sunset_rotation, scale);
                    Scale_Text_Circle(elementSunrise.Sunset_circle, scale);

                    Scale_IMG_NUMBER(elementSunrise.Sunset_Sunrise, scale);

                    Scale_IMG_POINTER(elementSunrise.Pointer, scale);
                    Scale_IMG(elementSunrise.Icon, scale);
                    break;
                case "ElementWind":
                    ElementWind elementWind = (ElementWind)elements;
                    Scale_IMG_LEVEL(elementWind.Images, scale);
                    Scale_IMG_PROGRESS(elementWind.Segments, scale);
                    Scale_IMG_NUMBER(elementWind.Number, scale);
                    Scale_TEXT(elementWind.Number_Font, scale);
                    Scale_IMG_POINTER(elementWind.Pointer, scale);
                    Scale_IMG_LEVEL(elementWind.Direction, scale);
                    Scale_IMG(elementWind.Icon, scale);
                    break;
                case "ElementMoon":
                    ElementMoon elementMoon = (ElementMoon)elements;
                    Scale_IMG_LEVEL(elementMoon.Images, scale);
                    //Scale_IMG_PROGRESS(elementMoon.Segments, scale);

                    Scale_IMG_NUMBER(elementMoon.Sunrise, scale);
                    Scale_TEXT(elementMoon.Sunrise_Font, scale);
                    //Scale_IMG_NUMBER(elementMoon.Sunrise_rotation, scale);
                    //Scale_Text_Circle(elementMoon.Sunrise_circle, scale);

                    Scale_IMG_NUMBER(elementMoon.Sunset, scale);
                    Scale_TEXT(elementMoon.Sunset_Font, scale);
                    //Scale_IMG_NUMBER(elementMoon.Sunset_rotation, scale);
                    //Scale_Text_Circle(elementMoon.Sunset_circle, scale);

                    Scale_IMG_NUMBER(elementMoon.Sunset_Sunrise, scale);

                    Scale_IMG_POINTER(elementMoon.Pointer, scale);
                    Scale_IMG(elementMoon.Icon, scale);
                    break;
                case "ElementAnimation":
                    ElementAnimation elementAnimation = (ElementAnimation)elements;
                    Scale_Animation(elementAnimation, scale);
                    break;
                case "ElementImage":
                    ElementImage elementImage = (ElementImage)elements;
                    Scale_IMG(elementImage.Icon, scale);
                    break;
                case "ElementCompass":
                    ElementCompass elementCompass = (ElementCompass)elements;
                    Scale_IMG_LEVEL(elementCompass.Images, scale);
                    Scale_IMG_NUMBER(elementCompass.Number, scale);
                    Scale_TEXT(elementCompass.Number_Font, scale);
                    Scale_IMG_NUMBER(elementCompass.Text_rotation, scale);
                    Scale_Text_Circle(elementCompass.Text_circle, scale);
                    Scale_IMG_POINTER(elementCompass.Pointer, scale);
                    Scale_IMG(elementCompass.Icon, scale);
                    break;
            }
        }

        private void Scale_IMG_STATUS(hmUI_widget_IMG_STATUS status, float scale)
        {
            if (status == null) return;
            status.x = (int)Math.Round(status.x * scale, MidpointRounding.AwayFromZero);
            status.y = (int)Math.Round(status.y * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_IMG_CLICK(hmUI_widget_IMG_CLICK shortcut, float scale)
        {
            if (shortcut == null) return;
            shortcut.x = (int)Math.Round(shortcut.x * scale, MidpointRounding.AwayFromZero);
            shortcut.y = (int)Math.Round(shortcut.y * scale, MidpointRounding.AwayFromZero);
            shortcut.h = (int)Math.Round(shortcut.h * scale, MidpointRounding.AwayFromZero);
            shortcut.w = (int)Math.Round(shortcut.w * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_Linear_Scale(Linear_Scale linear_scale, float scale)
        {
            if (linear_scale == null) return;
            linear_scale.start_x = (int)Math.Round(linear_scale.start_x * scale, MidpointRounding.AwayFromZero);
            linear_scale.start_y = (int)Math.Round(linear_scale.start_y * scale, MidpointRounding.AwayFromZero);
            linear_scale.line_width = (int)Math.Round(linear_scale.line_width * scale, MidpointRounding.AwayFromZero);
            linear_scale.lenght = (int)Math.Round(linear_scale.lenght * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_Circle_Scale(Circle_Scale circle_scale, float scale)
        {
            if (circle_scale == null) return;
            circle_scale.center_x = (int)Math.Round(circle_scale.center_x * scale, MidpointRounding.AwayFromZero); 
            circle_scale.center_y = (int)Math.Round(circle_scale.center_y * scale, MidpointRounding.AwayFromZero);
            circle_scale.line_width = (int)Math.Round(circle_scale.line_width * scale, MidpointRounding.AwayFromZero);
            circle_scale.radius = (int)Math.Round(circle_scale.radius * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_IMG_PROGRESS(hmUI_widget_IMG_PROGRESS segments, float scale)
        {
            if (segments == null) return;
            if (segments.X != null) for (int i = 0; i < segments.X.Count; i++)
                {
                    segments.X[i] = (int)Math.Round(segments.X[i] * scale, MidpointRounding.AwayFromZero);
                    segments.Y[i] = (int)Math.Round(segments.Y[i] * scale, MidpointRounding.AwayFromZero);
                }
        }

        private void Scale_IMG_LEVEL(hmUI_widget_IMG_LEVEL images, float scale)
        {
            if (images == null) return;
            images.X = (int)Math.Round(images.X * scale, MidpointRounding.AwayFromZero);
            images.Y = (int)Math.Round(images.Y * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_IMG_POINTER(hmUI_widget_IMG_POINTER img_pointer, float scale)
        {
            if (img_pointer == null) return;
            img_pointer.center_x = (int)Math.Round(img_pointer.center_x * scale, MidpointRounding.AwayFromZero);
            img_pointer.center_y = (int)Math.Round(img_pointer.center_y * scale, MidpointRounding.AwayFromZero);
            img_pointer.pos_x = (int)Math.Round((img_pointer.pos_x * scale), MidpointRounding.AwayFromZero);
            img_pointer.pos_y = (int)Math.Round((img_pointer.pos_y * scale), MidpointRounding.AwayFromZero);
            img_pointer.cover_x = (int)Math.Round((img_pointer.cover_x * scale), MidpointRounding.AwayFromZero);
            img_pointer.cover_y = (int)Math.Round((img_pointer.cover_y * scale), MidpointRounding.AwayFromZero);
            img_pointer.scale_x = (int)Math.Round((img_pointer.scale_x * scale), MidpointRounding.AwayFromZero);
            img_pointer.scale_y = (int)Math.Round((img_pointer.scale_y * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_EDITABLE_POINTER(EDITABLE_POINTER edit_pointer, float scale)
        {
            if (edit_pointer == null) return;
            edit_pointer.centerX = (int)Math.Round(edit_pointer.centerX * scale, MidpointRounding.AwayFromZero);
            edit_pointer.centerY = (int)Math.Round(edit_pointer.centerY * scale, MidpointRounding.AwayFromZero);
            edit_pointer.posX = (int)Math.Round((edit_pointer.posX * scale), MidpointRounding.AwayFromZero);
            edit_pointer.posY = (int)Math.Round((edit_pointer.posY * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_IMG_NUMBER(hmUI_widget_IMG_NUMBER img_number, float scale)
        {
            if (img_number == null) return;
            img_number.imageX = (int)Math.Round(img_number.imageX * scale, MidpointRounding.AwayFromZero);
            img_number.imageY = (int)Math.Round(img_number.imageY * scale, MidpointRounding.AwayFromZero);
            img_number.iconPosX = (int)Math.Round((img_number.iconPosX * scale), MidpointRounding.AwayFromZero);
            img_number.iconPosY = (int)Math.Round((img_number.iconPosY * scale), MidpointRounding.AwayFromZero);
            img_number.space = (int)Math.Round((img_number.space * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_Text_Circle(Text_Circle text_circle, float scale)
        {
            if (text_circle == null) return;
            text_circle.circle_center_X = (int)Math.Round(text_circle.circle_center_X * scale, MidpointRounding.AwayFromZero);
            text_circle.circle_center_Y = (int)Math.Round(text_circle.circle_center_Y * scale, MidpointRounding.AwayFromZero);
            text_circle.radius = (int)Math.Round((text_circle.radius * scale), MidpointRounding.AwayFromZero);
            text_circle.char_space_angle = (int)Math.Round((text_circle.char_space_angle * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_TEXT(hmUI_widget_TEXT text, float scale)
        {
            if (text == null) return;
            text.x = (int)Math.Round(text.x * scale, MidpointRounding.AwayFromZero);
            text.y = (int)Math.Round(text.y * scale, MidpointRounding.AwayFromZero);
            text.w = (int)Math.Round((text.w * scale), MidpointRounding.AwayFromZero);
            text.h = (int)Math.Round((text.h * scale), MidpointRounding.AwayFromZero);
            text.text_size = (int)Math.Round((text.text_size * scale), MidpointRounding.AwayFromZero);
            text.line_space = (int)Math.Round((text.line_space * scale), MidpointRounding.AwayFromZero);
            text.char_space = (int)Math.Round((text.char_space * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_AmPm(hmUI_widget_IMG_TIME_am_pm am_pm, float scale)
        {
            if (am_pm == null) return;
            am_pm.am_x = (int)Math.Round(am_pm.am_x * scale, MidpointRounding.AwayFromZero);
            am_pm.am_y = (int)Math.Round(am_pm.am_y * scale, MidpointRounding.AwayFromZero);
            am_pm.pm_x = (int)Math.Round((am_pm.pm_x * scale), MidpointRounding.AwayFromZero);
            am_pm.pm_y = (int)Math.Round((am_pm.pm_y * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_Button(Button button, float scale)
        {
            if (button == null) return;
            button.x = (int)Math.Round(button.x * scale, MidpointRounding.AwayFromZero);
            button.y = (int)Math.Round(button.y * scale, MidpointRounding.AwayFromZero);
            if (button.w != -1) button.w = (int)Math.Round((double)(button.w * scale), MidpointRounding.AwayFromZero);
            if (button.h != -1) button.h = (int)Math.Round((double)(button.h * scale), MidpointRounding.AwayFromZero);
            button.text_size = (int)Math.Round((double)(button.text_size * scale), MidpointRounding.AwayFromZero);
            button.radius = (int)Math.Round((double)(button.radius * scale), MidpointRounding.AwayFromZero);
        }

        private void Scale_WeatherGroup(WeatherGroup weatherGroup, float scale)
        {
            if (weatherGroup == null) return;
            Scale_IMG_NUMBER(weatherGroup.Number, scale);
            Scale_TEXT(weatherGroup.Number_Font, scale);
            Scale_IMG_NUMBER(weatherGroup.Text_rotation, scale);
            Scale_Text_Circle(weatherGroup.Text_circle, scale);
        }

        private void Scale_WeatherFewDays(FewDays fewDays, float scale)
        {
            if (fewDays == null) return;
            fewDays.X = (int)Math.Round(fewDays.X * scale, MidpointRounding.AwayFromZero);
            fewDays.Y = (int)Math.Round(fewDays.Y * scale, MidpointRounding.AwayFromZero);
            fewDays.ColumnWidth = (int)Math.Round(fewDays.ColumnWidth * scale, MidpointRounding.AwayFromZero);
        }

        private void Scale_Diagram(Weather_Diagram diagram, float scale)
        {
            if (diagram == null) return;
            diagram.Height = (int)Math.Round(diagram.Height * scale, MidpointRounding.AwayFromZero);
            diagram.Max_offsetX = (int)Math.Round(diagram.Max_offsetX * scale, MidpointRounding.AwayFromZero);
            diagram.Min_offsetX = (int)Math.Round(diagram.Min_offsetX * scale, MidpointRounding.AwayFromZero);
            diagram.Y = (int)Math.Round(diagram.Y * scale, MidpointRounding.AwayFromZero);
            diagram.Max_pointSize = (int)Math.Round(diagram.Max_pointSize * scale, MidpointRounding.AwayFromZero);
            diagram.Min_pointSize = (int)Math.Round(diagram.Min_pointSize * scale, MidpointRounding.AwayFromZero);
            diagram.Max_lineWidth = (int)Math.Round(diagram.Max_lineWidth * scale, MidpointRounding.AwayFromZero);
            diagram.Min_lineWidth = (int)Math.Round(diagram.Min_lineWidth * scale, MidpointRounding.AwayFromZero);
        }
    }
}
