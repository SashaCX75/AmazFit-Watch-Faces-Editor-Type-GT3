using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    break;
                case "ElementAnalogTime":
                    ElementAnalogTime elementAnalogTime = (ElementAnalogTime)elements;
                    Scale_IMG_POINTER(elementAnalogTime.Hour, scale);
                    Scale_IMG_POINTER(elementAnalogTime.Minute, scale);
                    Scale_IMG_POINTER(elementAnalogTime.Second, scale);
                    break;
                case "ElementDateDay":
                    ElementDateDay elementDateDay = (ElementDateDay)elements;
                    Scale_IMG_NUMBER(elementDateDay.Number, scale);
                    Scale_IMG_POINTER(elementDateDay.Pointer, scale);
                    break;
                case "ElementDateMonth":
                    ElementDateMonth elementDateMonth = (ElementDateMonth)elements;
                    Scale_IMG_NUMBER(elementDateMonth.Number, scale);
                    Scale_IMG_POINTER(elementDateMonth.Pointer, scale);
                    Scale_IMG_LEVEL(elementDateMonth.Images, scale);
                    break;
                case "ElementDateYear":
                    ElementDateYear elementDateYear = (ElementDateYear)elements;
                    Scale_IMG_NUMBER(elementDateYear.Number, scale);
                    break;
                case "ElementDateWeek":
                    ElementDateWeek elementDateWeek = (ElementDateWeek)elements;
                    Scale_IMG_POINTER(elementDateWeek.Pointer, scale);
                    Scale_IMG_LEVEL(elementDateWeek.Images, scale);
                    break;
                case "ElementStatuses":
                    ElementStatuses elementStatuses = (ElementStatuses)elements;
                    Scale_IMG_STATUS(elementStatuses.Alarm, scale);
                    Scale_IMG_STATUS(elementStatuses.Bluetooth, scale);
                    Scale_IMG_STATUS(elementStatuses.DND, scale);
                    Scale_IMG_STATUS(elementStatuses.Lock, scale);
                    break;
                case "ElementShortcuts":
                    ElementShortcuts elementShortcuts = (ElementShortcuts)elements;
                    Scale_IMG_CLICK(elementShortcuts.Step, scale);
                    Scale_IMG_CLICK(elementShortcuts.Heart, scale);
                    Scale_IMG_CLICK(elementShortcuts.SPO2, scale);
                    Scale_IMG_CLICK(elementShortcuts.PAI, scale);
                    Scale_IMG_CLICK(elementShortcuts.Stress, scale);
                    Scale_IMG_CLICK(elementShortcuts.Weather, scale);
                    Scale_IMG_CLICK(elementShortcuts.Altimeter, scale);
                    Scale_IMG_CLICK(elementShortcuts.Sunrise, scale);
                    Scale_IMG_CLICK(elementShortcuts.Alarm, scale);
                    Scale_IMG_CLICK(elementShortcuts.Sleep, scale);
                    Scale_IMG_CLICK(elementShortcuts.Countdown, scale);
                    Scale_IMG_CLICK(elementShortcuts.Stopwatch, scale);
                    break;

                case "ElementSteps":
                    ElementSteps elementSteps = (ElementSteps)elements;
                    Scale_IMG_LEVEL(elementSteps.Images, scale);
                    Scale_IMG_PROGRESS(elementSteps.Segments, scale);
                    Scale_IMG_NUMBER(elementSteps.Number, scale);
                    Scale_IMG_NUMBER(elementSteps.Number_Target, scale);
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
                    Scale_IMG_NUMBER(elementCalories.Number_Target, scale);
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
                    Scale_IMG_NUMBER(elementPAI.Number_Target, scale);
                    Scale_IMG_POINTER(elementPAI.Pointer, scale);
                    Scale_Circle_Scale(elementPAI.Circle_Scale, scale);
                    Scale_Linear_Scale(elementPAI.Linear_Scale, scale);
                    Scale_IMG(elementPAI.Icon, scale);
                    break;
                case "ElementDistance":
                    ElementDistance elementDistance = (ElementDistance)elements;
                    Scale_IMG_NUMBER(elementDistance.Number, scale);
                    break;
                case "ElementStand":
                    ElementStand elementStand = (ElementStand)elements;
                    Scale_IMG_LEVEL(elementStand.Images, scale);
                    Scale_IMG_PROGRESS(elementStand.Segments, scale);
                    Scale_IMG_NUMBER(elementStand.Number, scale);
                    Scale_IMG_NUMBER(elementStand.Number_Target, scale);
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
                    Scale_IMG_NUMBER(elementActivity.Number_Target, scale);
                    Scale_IMG_POINTER(elementActivity.Pointer, scale);
                    Scale_Circle_Scale(elementActivity.Circle_Scale, scale);
                    Scale_Linear_Scale(elementActivity.Linear_Scale, scale);
                    Scale_IMG(elementActivity.Icon, scale);
                    break;
                case "ElementSpO2":
                    ElementSpO2 elementSpO2 = (ElementSpO2)elements;
                    Scale_IMG_NUMBER(elementSpO2.Number, scale);
                    break;
                case "ElementStress":
                    ElementStress elementStress = (ElementStress)elements;
                    Scale_IMG_LEVEL(elementStress.Images, scale);
                    Scale_IMG_PROGRESS(elementStress.Segments, scale);
                    Scale_IMG_NUMBER(elementStress.Number, scale);
                    Scale_IMG_POINTER(elementStress.Pointer, scale);
                    Scale_IMG(elementStress.Icon, scale);
                    break;
                case "ElementFatBurning":
                    ElementFatBurning elementFatBurning = (ElementFatBurning)elements;
                    Scale_IMG_LEVEL(elementFatBurning.Images, scale);
                    Scale_IMG_PROGRESS(elementFatBurning.Segments, scale);
                    Scale_IMG_NUMBER(elementFatBurning.Number, scale);
                    Scale_IMG_NUMBER(elementFatBurning.Number_Target, scale);
                    Scale_IMG_POINTER(elementFatBurning.Pointer, scale);
                    Scale_Circle_Scale(elementFatBurning.Circle_Scale, scale);
                    Scale_Linear_Scale(elementFatBurning.Linear_Scale, scale);
                    Scale_IMG(elementFatBurning.Icon, scale);
                    break;

                case "ElementWeather":
                    ElementWeather elementWeather = (ElementWeather)elements;
                    Scale_IMG_LEVEL(elementWeather.Images, scale);
                    Scale_IMG_NUMBER(elementWeather.Number, scale);
                    Scale_IMG_NUMBER(elementWeather.Number_Min, scale);
                    Scale_IMG_NUMBER(elementWeather.Number_Max, scale);
                    Scale_TEXT(elementWeather.City_Name, scale);
                    Scale_IMG(elementWeather.Icon, scale);
                    break;
                case "ElementUVIndex":
                    ElementUVIndex elementUVIndex = (ElementUVIndex)elements;
                    Scale_IMG_LEVEL(elementUVIndex.Images, scale);
                    Scale_IMG_PROGRESS(elementUVIndex.Segments, scale);
                    Scale_IMG_NUMBER(elementUVIndex.Number, scale);
                    Scale_IMG_POINTER(elementUVIndex.Pointer, scale);
                    Scale_IMG(elementUVIndex.Icon, scale);
                    break;
                case "ElementHumidity":
                    ElementHumidity elementHumidity = (ElementHumidity)elements;
                    Scale_IMG_LEVEL(elementHumidity.Images, scale);
                    Scale_IMG_PROGRESS(elementHumidity.Segments, scale);
                    Scale_IMG_NUMBER(elementHumidity.Number, scale);
                    Scale_IMG_POINTER(elementHumidity.Pointer, scale);
                    Scale_IMG(elementHumidity.Icon, scale);
                    break;
                case "ElementAltimeter":
                    ElementAltimeter Altimeter = (ElementAltimeter)elements;
                    Scale_IMG_NUMBER(Altimeter.Number, scale);
                    Scale_IMG_POINTER(Altimeter.Pointer, scale);
                    Scale_IMG(Altimeter.Icon, scale);
                    break;
                case "ElementSunrise":
                    ElementSunrise elementSunrise = (ElementSunrise)elements;
                    Scale_IMG_LEVEL(elementSunrise.Images, scale);
                    Scale_IMG_PROGRESS(elementSunrise.Segments, scale);
                    Scale_IMG_NUMBER(elementSunrise.Sunrise, scale);
                    Scale_IMG_NUMBER(elementSunrise.Sunset, scale);
                    Scale_IMG_POINTER(elementSunrise.Pointer, scale);
                    Scale_IMG(elementSunrise.Icon, scale);
                    break;
                case "ElementWind":
                    ElementWind elementWind = (ElementWind)elements;
                    Scale_IMG_LEVEL(elementWind.Images, scale);
                    Scale_IMG_PROGRESS(elementWind.Segments, scale);
                    Scale_IMG_NUMBER(elementWind.Number, scale);
                    Scale_IMG_POINTER(elementWind.Pointer, scale);
                    Scale_IMG(elementWind.Icon, scale);
                    break;
                case "ElementMoon":
                    ElementMoon elementMoon = (ElementMoon)elements;
                    Scale_IMG_LEVEL(elementMoon.Images, scale);
                    break;
                case "ElementAnimation":
                    ElementAnimation elementAnimation = (ElementAnimation)elements;
                    Scale_Animation(elementAnimation, scale);
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

        private void Scale_IMG_NUMBER(hmUI_widget_IMG_NUMBER img_number, float scale)
        {
            if (img_number == null) return;
            img_number.imageX = (int)Math.Round(img_number.imageX * scale, MidpointRounding.AwayFromZero);
            img_number.imageY = (int)Math.Round(img_number.imageY * scale, MidpointRounding.AwayFromZero);
            img_number.iconPosX = (int)Math.Round((img_number.iconPosX * scale), MidpointRounding.AwayFromZero);
            img_number.iconPosY = (int)Math.Round((img_number.iconPosY * scale), MidpointRounding.AwayFromZero);
            img_number.space = (int)Math.Round((img_number.space * scale), MidpointRounding.AwayFromZero);
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
    }
}
