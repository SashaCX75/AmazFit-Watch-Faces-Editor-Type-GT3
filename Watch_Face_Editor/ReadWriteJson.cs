using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        public void JsonToJS(out string variables, out string items)
        {
            variables = Environment.NewLine;
            items = Environment.NewLine;
            string scale_call = "";
            string options = "";
            if (Watch_Face == null) return;
            if (Watch_Face.ScreenNormal != null)
            {
                if (Watch_Face.ScreenNormal.Background != null && Watch_Face.ScreenNormal.Background.visible)
                {
                    if(Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                    {
                        variables += TabInString(4) + "let normal_background_bg = ''" + Environment.NewLine;
                        hmUI_widget_FILL_RECT backgroundColor = Watch_Face.ScreenNormal.Background.BackgroundColor;
                        //if (backgroundColor.show_level == null) backgroundColor.show_level = "ONLY_NORMAL";
                        options = FILL_RECT_Options(backgroundColor, "ONLY_NORMAL");
                        items += TabInString(6) + "normal_background_bg = hmUI.createWidget(hmUI.widget.FILL_RECT, {" +
                            options + TabInString(6) + "});" + Environment.NewLine;
                    }
                    if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                    {
                        variables += TabInString(4) + "let normal_background_bg_img = ''" + Environment.NewLine;
                        hmUI_widget_IMG backgroundImage = Watch_Face.ScreenNormal.Background.BackgroundImage;
                        //if (backgroundImage.show_level == null) backgroundImage.show_level = "ONLY_NORMAL";
                        options = IMG_Options(backgroundImage, "ONLY_NORMAL");
                        items += TabInString(6) + "normal_background_bg_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                            options + TabInString(6) + "});" + Environment.NewLine;
                    }
                }
                if (Watch_Face.ScreenNormal.Elements != null && Watch_Face.ScreenNormal.Elements.Count > 0)
                {
                    foreach(Object element in Watch_Face.ScreenNormal.Elements)
                    {
                        string outVariables = "";
                        string outItems = "";
                        string out_resume_call = "";
                        AddElementToJS(element, "ONLY_NORMAL", out outVariables, out outItems, out out_resume_call,
                            items, scale_call);
                        variables += outVariables;
                        items += outItems;
                        scale_call += out_resume_call;
                    }
                }
            }

            items = items + Environment.NewLine + Environment.NewLine; 
            if (Watch_Face.ScreenAOD != null)
            {
                if (Watch_Face.ScreenAOD.Background != null && Watch_Face.ScreenAOD.Background.visible)
                {
                    if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                    {
                        variables += TabInString(4) + "let idle_background_bg = ''" + Environment.NewLine;
                        hmUI_widget_FILL_RECT backgroundColor = Watch_Face.ScreenAOD.Background.BackgroundColor;
                        //if (backgroundColor.show_level == null) backgroundColor.show_level = "ONLY_NORMAL";
                        options = FILL_RECT_Options(backgroundColor, "ONAL_AOD");
                        items += TabInString(6) + "idle_background_bg = hmUI.createWidget(hmUI.widget.FILL_RECT, {" +
                            options + TabInString(6) + "});" + Environment.NewLine;
                    }
                    if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                    {
                        variables += TabInString(4) + "let idle_background_bg_img = ''" + Environment.NewLine;
                        hmUI_widget_IMG backgroundImage = Watch_Face.ScreenAOD.Background.BackgroundImage;
                        //if (backgroundImage.show_level == null) backgroundImage.show_level = "ONLY_NORMAL";
                        options = IMG_Options(backgroundImage, "ONAL_AOD");
                        items += TabInString(6) + "idle_background_bg_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                            options + TabInString(6) + "});" + Environment.NewLine;
                    }
                }
                if (Watch_Face.ScreenAOD.Elements != null && Watch_Face.ScreenAOD.Elements.Count > 0)
                {
                    foreach (Object element in Watch_Face.ScreenAOD.Elements)
                    {
                        string outVariables = "";
                        string outItems = "";
                        string out_resume_call = "";
                        AddElementToJS(element, "ONAL_AOD", out outVariables, out outItems, out out_resume_call,
                            items, scale_call);
                        variables += outVariables;
                        items += outItems;
                        scale_call += out_resume_call;
                    }
                }
            }

            if (scale_call.Length > 5)
            {
                items += Environment.NewLine + TabInString(6) + "function scale_call() {";
                items += Environment.NewLine + scale_call;
                items += Environment.NewLine + TabInString(6) + "};" + Environment.NewLine;
                items += Environment.NewLine + TabInString(6) +
                    "const widgetDelegate = hmUI.createWidget(hmUI.widget.WIDGET_DELEGATE, {";
                items += Environment.NewLine + TabInString(7) + "resume_call: (function () {";
                //items += Environment.NewLine + resume_call;
                items += Environment.NewLine + TabInString(8) + "scale_call();";
                items += Environment.NewLine + TabInString(7) + "}),";
                items += Environment.NewLine + TabInString(6) + "});";
            }

            int firstPos = items.IndexOf("let screenType = hmSetting.getScreenType();");
            int lastPos = items.LastIndexOf("let screenType = hmSetting.getScreenType();");
            while (firstPos > 0 && firstPos < lastPos)
            {
                int lenghtRemove = "let screenType = hmSetting.getScreenType();".Length;
                items = items.Remove(lastPos, lenghtRemove);
                firstPos = items.IndexOf("let screenType = hmSetting.getScreenType();");
                lastPos = items.LastIndexOf("let screenType = hmSetting.getScreenType();");
            }
        }

        private void AddElementToJS(Object element, string show_level, out string variables, out string items,
            out string scale_call,  string exist_items, string exist_scale_call)
        {
            string optionNameStart = "normal_";
            if (show_level == "ONAL_AOD") optionNameStart = "idle_";
            variables = "";
            items = "";
            scale_call = "";
            string options = "";
            string type = element.GetType().Name;

            int imagesPosition = 99;
            int segmentsPosition = 99;
            int numberPosition = 99;
            int numberTargetPosition = 99;
            int pointerPosition= 99;
            int circleScalePosition = 99;
            int linearScalePosition = 99;
            int iconPosition = 99;

            string imagesOptions = "";
            string segmentsOptions = "";
            string numberOptions = "";
            string numberTargetOptions = "";
            string pointerOptions = "";
            string circleScaleOptions = "";
            string linearScaleOptions = "";
            Circle_Scale circle_scale = null;
            Linear_Scale linear_scale = null;
            string iconOptions = "";

            string numberOptions_separator = "";
            string numberTargetOptions_separator = "";

            switch (type)
            {
                #region ElementDigitalTime
                case "ElementDigitalTime":
                    ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                    if(!DigitalTime.visible) return;
                    int hourPosition = 99;
                    int minutePosition = 99;
                    int secondPosition = 99;
                    int AmPmPosition = 99;
                    string optionsHour = "";
                    string optionsMinute = "";
                    string optionsSecond = "";
                    string optionsHour_separator = "";
                    string optionsMinute_separator = "";
                    string optionsSecond_separator = "";
                    string optionsAmPm = "";
                    if (DigitalTime.Hour != null && DigitalTime.Hour.visible)
                    {
                        hourPosition = DigitalTime.Hour.position;
                        hmUI_widget_IMG_NUMBER img_number_hour = DigitalTime.Hour;
                        optionsHour = IMG_NUMBER_Hour_Options(img_number_hour, "");

                        optionsHour_separator = IMG_Separator_Options(img_number_hour, show_level);
                    }
                    if (DigitalTime.Minute != null && DigitalTime.Minute.visible)
                    {
                        minutePosition = DigitalTime.Minute.position;
                        hmUI_widget_IMG_NUMBER img_number_minute = DigitalTime.Minute;
                        optionsMinute = IMG_NUMBER_Minute_Options(img_number_minute, "");

                        optionsMinute_separator = IMG_Separator_Options(img_number_minute, show_level);
                    }
                    if (DigitalTime.Second != null && DigitalTime.Second.visible)
                    {
                        secondPosition = DigitalTime.Second.position;
                        hmUI_widget_IMG_NUMBER img_number_second = DigitalTime.Second;
                        optionsSecond = IMG_NUMBER_Second_Options(img_number_second, "");

                        optionsSecond_separator = IMG_Separator_Options(img_number_second, show_level);
                    }
                    if (DigitalTime.AmPm != null && DigitalTime.AmPm.visible)
                    {
                        AmPmPosition = DigitalTime.AmPm.position;
                        hmUI_widget_IMG_TIME_am_pm am_pm = DigitalTime.AmPm;
                        optionsAmPm = AmPm_Options(am_pm, show_level);
                    }

                    bool fullTime = false;
                    for (int index = 1; index <= 4; index++)
                    {
                        if (index == hourPosition && hourPosition < minutePosition && minutePosition < secondPosition)
                        {
                            if (optionsHour.Length > 5 && optionsMinute.Length > 5 )
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                                        "digital_clock_img_time = ''" + Environment.NewLine;
                                options = optionsHour + optionsMinute + optionsSecond + Environment.NewLine +
                                    TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "digital_clock_img_time = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        options + TabInString(6) + "});" + Environment.NewLine;

                                if (optionsHour_separator.Length > 5)
                                {
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "digital_clock_hour_separator_img = ''" + Environment.NewLine;
                                    items += Environment.NewLine + TabInString(6) +
                                        optionNameStart + "digital_clock_hour_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                            optionsHour_separator + TabInString(6) + "});" + Environment.NewLine;
                                }

                                if (optionsMinute_separator.Length > 5)
                                {
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "digital_clock_minute_separator_img = ''" + Environment.NewLine;
                                    items += Environment.NewLine + TabInString(6) +
                                        optionNameStart + "digital_clock_minute_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                            optionsMinute_separator + TabInString(6) + "});" + Environment.NewLine;
                                }

                                if (optionsSecond_separator.Length > 5)
                                {
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "digital_clock_second_separator_img = ''" + Environment.NewLine;
                                    items += Environment.NewLine + TabInString(6) +
                                        optionNameStart + "digital_clock_second_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                            optionsSecond_separator + TabInString(6) + "});" + Environment.NewLine;
                                }

                                fullTime = true;
                            }
                        }
                        if(!fullTime)
                        {
                            if (index == hourPosition && optionsHour.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "digital_clock_img_time_hour = ''" + Environment.NewLine;
                                optionsHour += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "digital_clock_img_time_hour = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsHour + TabInString(6) + "});" + Environment.NewLine;

                                if (optionsHour_separator.Length > 5)
                                {
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "digital_clock_hour_separator_img = ''" + Environment.NewLine;
                                    items += Environment.NewLine + TabInString(6) +
                                        optionNameStart + "digital_clock_hour_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                            optionsHour_separator + TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (index == minutePosition && optionsMinute.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "digital_clock_img_time_minute = ''" + Environment.NewLine;
                                optionsMinute += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "digital_clock_img_time_minute = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsMinute + TabInString(6) + "});" + Environment.NewLine;

                                if (optionsMinute_separator.Length > 5)
                                {
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "digital_clock_minute_separator_img = ''" + Environment.NewLine;
                                    items += Environment.NewLine + TabInString(6) +
                                        optionNameStart + "digital_clock_minute_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                            optionsMinute_separator + TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (index == secondPosition && optionsSecond.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "digital_clock_img_time_second = ''" + Environment.NewLine;
                                optionsSecond += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "digital_clock_img_time_second = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsSecond + TabInString(6) + "});" + Environment.NewLine;

                                if (optionsSecond_separator.Length > 5)
                                {
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "digital_clock_second_separator_img = ''" + Environment.NewLine;
                                    items += Environment.NewLine + TabInString(6) +
                                        optionNameStart + "digital_clock_second_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                            optionsSecond_separator + TabInString(6) + "});" + Environment.NewLine;
                                }
                            }
                        }

                        if (index == AmPmPosition && optionsAmPm.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "digital_clock_img_time_AmPm = ''" + Environment.NewLine;
                            //optionsAmPm += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "digital_clock_img_time_AmPm = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                    optionsAmPm + TabInString(6) + "});" + Environment.NewLine;
                        }
                    }
                    break;
                #endregion

                #region ElementAnalogTime
                case "ElementAnalogTime":
                    ElementAnalogTime AnalogTime = (ElementAnalogTime)element;
                    if (!AnalogTime.visible) return;
                    int hourPointerPosition = 99;
                    int minutePointerPosition = 99;
                    int secondPointerPosition = 99;
                    string optionsPointerHour = "";
                    string optionsPointerMinute = "";
                    string optionsPointerSecond = "";
                    if (AnalogTime.Hour != null && AnalogTime.Hour.visible)
                    {
                        hourPointerPosition = AnalogTime.Hour.position;
                        hmUI_widget_IMG_POINTER img_pointer_hour = AnalogTime.Hour;
                        optionsPointerHour = IMG_POINTER_Hour_Options(img_pointer_hour, show_level);
                    }
                    if (AnalogTime.Minute != null && AnalogTime.Minute.visible)
                    {
                        minutePointerPosition = AnalogTime.Minute.position;
                        hmUI_widget_IMG_POINTER img_pointer_minute = AnalogTime.Minute;
                        optionsPointerMinute = IMG_POINTER_Minute_Options(img_pointer_minute, show_level);
                    }
                    if (AnalogTime.Second != null && AnalogTime.Second.visible)
                    {
                        secondPointerPosition = AnalogTime.Second.position;
                        hmUI_widget_IMG_POINTER img_pointer_second = AnalogTime.Second;
                        optionsPointerSecond = IMG_POINTER_Second_Options(img_pointer_second, show_level);
                    }

                    for (int index = 1; index <= 3; index++)
                    {
                        if (index == hourPointerPosition && optionsPointerHour.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "analog_clock_time_pointer_hour = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "analog_clock_time_pointer_hour = hmUI.createWidget(hmUI.widget.TIME_POINTER, {" +
                                    optionsPointerHour + TabInString(6) + "});" + Environment.NewLine;
                        }

                        if (index == minutePointerPosition && optionsPointerMinute.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "analog_clock_time_pointer_minute = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "analog_clock_time_pointer_minute = hmUI.createWidget(hmUI.widget.TIME_POINTER, {" +
                                    optionsPointerMinute + TabInString(6) + "});" + Environment.NewLine;
                        }
                        
                        if (index == secondPointerPosition && optionsPointerSecond.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "analog_clock_time_pointer_second = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "analog_clock_time_pointer_second = hmUI.createWidget(hmUI.widget.TIME_POINTER, {" +
                                    optionsPointerSecond + TabInString(6) + "});" + Environment.NewLine;
                        }
                    }
                    break;
                #endregion

                #region ElementDateDay
                case "ElementDateDay":
                    ElementDateDay DateDay = (ElementDateDay)element;
                    if (!DateDay.visible) return;
                    int pointerPositionDay = 99;
                    int numberPositionDay = 99;
                    string optionsPointerDay = "";
                    string optionsNumberDay = "";
                    string optionsNumberDay_separator = "";
                    if (DateDay.Pointer != null && DateDay.Pointer.visible)
                    {
                        pointerPositionDay = DateDay.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = DateDay.Pointer;
                        optionsPointerDay = DATE_POINTER_Options(img_pointer, "DAY", show_level);
                    }
                    if (DateDay.Number != null && DateDay.Number.visible)
                    {
                        numberPositionDay = DateDay.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = DateDay.Number;
                        optionsNumberDay = IMG_NUMBER_Day_Options(img_number, show_level);

                        optionsNumberDay_separator = IMG_Separator_Options(img_number, show_level);
                    }

                    for (int index = 1; index <= 3; index++)
                    {
                        if (index == pointerPositionDay && optionsPointerDay.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "day_pointer_progress_date_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "day_pointer_progress_date_pointer = hmUI.createWidget(hmUI.widget.DATE_POINTER, {" +
                                    optionsPointerDay + TabInString(6) + "});" + Environment.NewLine;
                        }

                        if (index == numberPositionDay && optionsNumberDay.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "date_img_date_day = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "date_img_date_day = hmUI.createWidget(hmUI.widget.IMG_DATE, {" +
                                    optionsNumberDay + TabInString(6) + "});" + Environment.NewLine;

                            if (optionsNumberDay_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "date_day_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "date_day_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        optionsNumberDay_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }
                    }
                    break;
                #endregion

                #region ElementDateMonth
                case "ElementDateMonth":
                    ElementDateMonth DateMonth = (ElementDateMonth)element;
                    if (!DateMonth.visible) return;
                    int pointerPositionMonth = 99;
                    int numberPositionMonth = 99;
                    int imagesPositionMonth = 99;
                    string optionsPointerMonth = "";
                    string optionsNumberMonth = "";
                    string optionsNumberMonth_separator = "";
                    string optionsImagesMonth = "";
                    if (DateMonth.Pointer != null && DateMonth.Pointer.visible)
                    {
                        pointerPositionMonth = DateMonth.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = DateMonth.Pointer;
                        optionsPointerMonth = DATE_POINTER_Options(img_pointer, "MONTH", show_level);
                    }
                    if (DateMonth.Number != null && DateMonth.Number.visible)
                    {
                        numberPositionMonth = DateMonth.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = DateMonth.Number;
                        optionsNumberMonth = IMG_NUMBER_Month_Options(img_number, show_level);

                        optionsNumberMonth_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (DateMonth.Images != null && DateMonth.Images.visible)
                    {
                        imagesPositionMonth = DateMonth.Images.position;
                        hmUI_widget_IMG_LEVEL img_images = DateMonth.Images;
                        optionsImagesMonth = IMG_IMAGES_Month_Options(img_images, show_level);
                    }

                    for (int index = 1; index <= 3; index++)
                    {
                        if (index == pointerPositionMonth && optionsPointerMonth.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "month_pointer_progress_date_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "month_pointer_progress_date_pointer = hmUI.createWidget(hmUI.widget.DATE_POINTER, {" +
                                    optionsPointerMonth + TabInString(6) + "});" + Environment.NewLine;
                        }

                        if (index == numberPositionMonth && optionsNumberMonth.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "date_img_date_month = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "date_img_date_month = hmUI.createWidget(hmUI.widget.IMG_DATE, {" +
                                    optionsNumberMonth + TabInString(6) + "});" + Environment.NewLine;

                            if (optionsNumberMonth_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "date_month_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "date_month_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        optionsNumberMonth_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        if (index == imagesPositionMonth && optionsImagesMonth.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "date_img_date_month_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "date_img_date_month_img = hmUI.createWidget(hmUI.widget.IMG_DATE, {" +
                                    optionsImagesMonth + TabInString(6) + "});" + Environment.NewLine;
                        }
                    }
                    break;
                #endregion

                #region ElementDateYear
                case "ElementDateYear":
                    ElementDateYear DateYear = (ElementDateYear)element;
                    if (!DateYear.visible) return;
                    string optionsNumberYear = "";
                    string optionsNumberYear_separator = "";
                    if (DateYear.Number != null)
                    {
                        hmUI_widget_IMG_NUMBER img_number = DateYear.Number;
                        optionsNumberYear = IMG_NUMBER_Year_Options(img_number, show_level);

                        optionsNumberYear_separator = IMG_Separator_Options(img_number, show_level);
                    }

                    if (optionsNumberYear.Length > 5)
                    {
                        variables += TabInString(4) + "let " + optionNameStart +
                            "date_img_date_year = ''" + Environment.NewLine;
                        items += Environment.NewLine + TabInString(6) +
                            optionNameStart + "date_img_date_year = hmUI.createWidget(hmUI.widget.IMG_DATE, {" +
                                optionsNumberYear + TabInString(6) + "});" + Environment.NewLine;

                        if (optionsNumberYear_separator.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "date_year_separator_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "date_year_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                    optionsNumberYear_separator + TabInString(6) + "});" + Environment.NewLine;
                        }
                    }
                    break;
                #endregion

                #region ElementDateWeek
                case "ElementDateWeek":
                    ElementDateWeek DateWeek = (ElementDateWeek)element;
                    if (!DateWeek.visible) return;
                    int pointerPositionWeek = 99;
                    int imagesPositionWeek = 99;
                    string optionsPointerWeek = "";
                    string optionsImagesWeek = "";
                    if (DateWeek.Pointer != null && DateWeek.Pointer.visible)
                    {
                        pointerPositionWeek = DateWeek.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = DateWeek.Pointer;
                        optionsPointerWeek = DATE_POINTER_Options(img_pointer, "WEEK", show_level);
                    }
                    if (DateWeek.Images != null && DateWeek.Images.visible)
                    {
                        imagesPositionWeek = DateWeek.Images.position;
                        hmUI_widget_IMG_LEVEL img_images = DateWeek.Images;
                        optionsImagesWeek = IMG_IMAGES_Week_Options(img_images, show_level);
                    }

                    for (int index = 1; index <= 2; index++)
                    {
                        if (index == pointerPositionWeek && optionsPointerWeek.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "week_pointer_progress_date_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "week_pointer_progress_date_pointer = hmUI.createWidget(hmUI.widget.DATE_POINTER, {" +
                                    optionsPointerWeek + TabInString(6) + "});" + Environment.NewLine;
                        }

                        if (index == imagesPositionWeek && optionsImagesWeek.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "date_img_date_week_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "date_img_date_week_img = hmUI.createWidget(hmUI.widget.IMG_WEEK, {" +
                                    optionsImagesWeek + TabInString(6) + "});" + Environment.NewLine;
                        }
                    }
                    break;
                #endregion

                #region ElementStatuses
                case "ElementStatuses":
                    ElementStatuses Statuses = (ElementStatuses)element;

                    if (!Statuses.visible) return;
                    int positionStatusAlarm = 99;
                    int positionStatusBluetooth = 99;
                    int positionStatusDND = 99;
                    int positionStatusLock = 99;
                    string optionsStatusAlarm = "";
                    string optionsStatusBluetooth = "";
                    string optionsStatusDND = "";
                    string optionsStatusLock = "";

                    if (Statuses.Alarm != null && Statuses.Alarm.visible)
                    {
                        positionStatusAlarm = Statuses.Alarm.position;
                        hmUI_widget_IMG_STATUS img_status = Statuses.Alarm;
                        optionsStatusAlarm = IMG_STATUS_Options(img_status, "CLOCK", show_level);
                    }
                    if (Statuses.Bluetooth != null && Statuses.Bluetooth.visible)
                    {
                        positionStatusBluetooth = Statuses.Bluetooth.position;
                        hmUI_widget_IMG_STATUS img_bluetooth = Statuses.Bluetooth;
                        optionsStatusBluetooth = IMG_STATUS_Options(img_bluetooth, "DISCONNECT", show_level);
                    }
                    if (Statuses.DND != null && Statuses.DND.visible)
                    {
                        positionStatusDND = Statuses.DND.position;
                        hmUI_widget_IMG_STATUS img_DND = Statuses.DND;
                        optionsStatusDND = IMG_STATUS_Options(img_DND, "DISTURB", show_level);
                    }
                    if (Statuses.Lock != null && Statuses.Lock.visible)
                    {
                        positionStatusLock = Statuses.Lock.position;
                        hmUI_widget_IMG_STATUS img_lock = Statuses.Lock;
                        optionsStatusLock = IMG_STATUS_Options(img_lock, "LOCK", show_level);
                    }

                    for (int index = 1; index <= 4; index++)
                    {
                        // Alarm
                        if (index == positionStatusAlarm && optionsStatusAlarm.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "system_clock_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "system_clock_img = hmUI.createWidget(hmUI.widget.IMG_STATUS, {" +
                                    optionsStatusAlarm + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Bluetooth
                        if (index == positionStatusBluetooth && optionsStatusBluetooth.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "system_disconnect_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "system_disconnect_img = hmUI.createWidget(hmUI.widget.IMG_STATUS, {" +
                                    optionsStatusBluetooth + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // DND
                        if (index == positionStatusDND && optionsStatusDND.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "system_dnd_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "system_dnd_img = hmUI.createWidget(hmUI.widget.IMG_STATUS, {" +
                                    optionsStatusDND + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Lock
                        if (index == positionStatusLock && optionsStatusLock.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "system_lock_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "system_lock_img = hmUI.createWidget(hmUI.widget.IMG_STATUS, {" +
                                    optionsStatusLock + TabInString(6) + "});" + Environment.NewLine;
                        }


                    }
                    break;
                #endregion

                #region ElementShortcuts
                case "ElementShortcuts":
                    ElementShortcuts Shortcuts = (ElementShortcuts)element;

                    if (!Shortcuts.visible) return;
                    int positionShortcutsStep = 99;
                    int positionShortcutsHeart = 99;
                    int positionShortcutsSPO2 = 99;
                    int positionShortcutsPAI = 99;
                    int positionShortcutsStress = 99;
                    int positionShortcutsWeather = 99;
                    int positionShortcutsAltimeter = 99;
                    int positionShortcutsSunrise = 99;
                    int positionShortcutsAlarm = 99;
                    int positionShortcutsSleep = 99;
                    int positionShortcutsCountdown = 99;
                    int positionShortcutsStopwatch = 99;

                    string optionsShortcutsStep = "";
                    string optionsShortcutsHeart = "";
                    string optionsShortcutsSPO2 = "";
                    string optionsShortcutsPAI = "";
                    string optionsShortcutsStress = "";
                    string optionsShortcutsWeather = "";
                    string optionsShortcutsAltimeter = "";
                    string optionsShortcutsSunrise = "";
                    string optionsShortcutsAlarm = "";
                    string optionsShortcutsSleep = "";
                    string optionsShortcutsCountdown = "";
                    string optionsShortcutsStopwatch = "";

                    if (Shortcuts.Step != null && Shortcuts.Step.visible)
                    {
                        positionShortcutsStep = Shortcuts.Step.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Step;
                        optionsShortcutsStep = IMG_CLICK_Options(img_clic, "STEP", show_level);
                    }
                    if (Shortcuts.Heart != null && Shortcuts.Heart.visible)
                    {
                        positionShortcutsHeart = Shortcuts.Heart.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Heart;
                        optionsShortcutsHeart = IMG_CLICK_Options(img_clic, "HEART", show_level);
                    }
                    if (Shortcuts.SPO2 != null && Shortcuts.SPO2.visible)
                    {
                        positionShortcutsSPO2 = Shortcuts.SPO2.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.SPO2;
                        optionsShortcutsSPO2 = IMG_CLICK_Options(img_clic, "SPO2", show_level);
                    }
                    if (Shortcuts.PAI != null && Shortcuts.PAI.visible)
                    {
                        positionShortcutsPAI = Shortcuts.PAI.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.PAI;
                        optionsShortcutsPAI = IMG_CLICK_Options(img_clic, "PAI_WEEKLY", show_level);
                    }
                    if (Shortcuts.Stress != null && Shortcuts.Stress.visible)
                    {
                        positionShortcutsStress = Shortcuts.Stress.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Stress;
                        optionsShortcutsStress = IMG_CLICK_Options(img_clic, "STRESS", show_level);
                    }
                    if (Shortcuts.Weather != null && Shortcuts.Weather.visible)
                    {
                        positionShortcutsWeather = Shortcuts.Weather.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Weather;
                        optionsShortcutsWeather = IMG_CLICK_Options(img_clic, "WEATHER_CURRENT", show_level);
                    }
                    if (Shortcuts.Altimeter != null && Shortcuts.Altimeter.visible)
                    {
                        positionShortcutsAltimeter = Shortcuts.Altimeter.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Altimeter;
                        optionsShortcutsAltimeter = IMG_CLICK_Options(img_clic, "ALTIMETER", show_level);
                    }
                    if (Shortcuts.Sunrise != null && Shortcuts.Sunrise.visible)
                    {
                        positionShortcutsSunrise = Shortcuts.Sunrise.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Sunrise;
                        optionsShortcutsSunrise = IMG_CLICK_Options(img_clic, "SUN_CURRENT", show_level);
                    }
                    if (Shortcuts.Alarm != null && Shortcuts.Alarm.visible)
                    {
                        positionShortcutsAlarm = Shortcuts.Alarm.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Alarm;
                        optionsShortcutsAlarm = IMG_CLICK_Options(img_clic, "ALARM_CLOCK", show_level);
                    }
                    if (Shortcuts.Sleep != null && Shortcuts.Sleep.visible)
                    {
                        positionShortcutsSleep = Shortcuts.Sleep.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Sleep;
                        optionsShortcutsSleep = IMG_CLICK_Options(img_clic, "SLEEP", show_level);
                    }
                    if (Shortcuts.Countdown != null && Shortcuts.Countdown.visible)
                    {
                        positionShortcutsCountdown = Shortcuts.Countdown.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Countdown;
                        optionsShortcutsCountdown = IMG_CLICK_Options(img_clic, "COUNT_DOWN", show_level);
                    }
                    if (Shortcuts.Stopwatch != null && Shortcuts.Stopwatch.visible)
                    {
                        positionShortcutsStopwatch = Shortcuts.Stopwatch.position;
                        hmUI_widget_IMG_CLICK img_clic = Shortcuts.Stopwatch;
                        optionsShortcutsStopwatch = IMG_CLICK_Options(img_clic, "STOP_WATCH", show_level);
                    }

                    for (int index = 1; index <= 12; index++)
                    {
                        // Step
                        if (index == positionShortcutsStep && optionsShortcutsStep.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "step_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsStep + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Heart
                        if (index == positionShortcutsHeart && optionsShortcutsHeart.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "heart_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsHeart + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // SPO2
                        if (index == positionShortcutsSPO2 && optionsShortcutsSPO2.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "spo2_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "spo2_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsSPO2 + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // PAI
                        if (index == positionShortcutsPAI && optionsShortcutsPAI.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "pai_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsPAI + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Stress
                        if (index == positionShortcutsStress && optionsShortcutsStress.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "stress_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "stress_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsStress + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Weather
                        if (index == positionShortcutsWeather && optionsShortcutsWeather.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "temperature_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "temperature_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsWeather + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Altimeter
                        if (index == positionShortcutsAltimeter && optionsShortcutsAltimeter.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "altimeter_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "altimeter_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsAltimeter + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Sunrise
                        if (index == positionShortcutsSunrise && optionsShortcutsSunrise.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "sunrise_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "sunrise_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsSunrise + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Alarm
                        if (index == positionShortcutsAlarm && optionsShortcutsAlarm.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "alarm_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "alarm_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsAlarm + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Sleep
                        if (index == positionShortcutsSleep && optionsShortcutsSleep.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "sleep_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "sleep_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsSleep + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Countdown
                        if (index == positionShortcutsCountdown && optionsShortcutsCountdown.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "countdown_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "countdown_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsCountdown + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Stopwatch
                        if (index == positionShortcutsStopwatch && optionsShortcutsStopwatch.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "stopwatch_jumpable_img_click = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "stopwatch_jumpable_img_click = hmUI.createWidget(hmUI.widget.IMG_CLICK, {" +
                                    optionsShortcutsStopwatch + TabInString(6) + "});" + Environment.NewLine;
                        }


                    }
                    break;
                #endregion

                #region ElementSteps
                case "ElementSteps":
                    ElementSteps Steps = (ElementSteps)element;

                    if (!Steps.visible) return;
                    if (Steps.Images != null && Steps.Images.visible)
                    {
                        imagesPosition = Steps.Images.position;
                        hmUI_widget_IMG_LEVEL img_images = Steps.Images;
                        imagesOptions = IMG_IMAGES_Options(img_images, "STEP", show_level);
                    }
                    if (Steps.Segments != null && Steps.Segments.visible)
                    {
                        segmentsPosition = Steps.Segments.position;
                        hmUI_widget_IMG_PROGRESS img_progress = Steps.Segments;
                        segmentsOptions = IMG_PROGRESS_Options(img_progress, "STEP", show_level);
                    }
                    if (Steps.Number != null && Steps.Number.visible)
                    {
                        numberPosition = Steps.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = Steps.Number;
                        numberOptions = IMG_NUMBER_Options(img_number, "STEP", show_level);

                        numberOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (Steps.Number_Target != null && Steps.Number_Target.visible)
                    {
                        numberTargetPosition = Steps.Number_Target.position;
                        hmUI_widget_IMG_NUMBER img_number = Steps.Number_Target;
                        numberTargetOptions = IMG_NUMBER_Options(img_number, "STEP_TARGET", show_level);

                        numberTargetOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (Steps.Pointer != null && Steps.Pointer.visible)
                    {
                        pointerPosition = Steps.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = Steps.Pointer;
                        pointerOptions = IMG_POINTER_Options(img_pointer, "STEP", show_level);
                    }

                    if (Steps.Circle_Scale != null && Steps.Circle_Scale.visible)
                    {
                        circleScalePosition = Steps.Circle_Scale.position;
                        circle_scale = Steps.Circle_Scale;

                        circleScaleOptions = Circle_Scale_Options(circle_scale, optionNameStart, "STEP", show_level);
                        //circleScaleMirrorOptions = Circle_Scale_Options(linear_scale, true);
                    }
                    if (Steps.Linear_Scale != null && Steps.Linear_Scale.visible)
                    {
                        linearScalePosition = Steps.Linear_Scale.position;
                        linear_scale = Steps.Linear_Scale;

                        linearScaleOptions = Linear_Scale_Options(linear_scale, optionNameStart, "STEP", "step", show_level);
                    }

                    if (Steps.Icon != null && Steps.Icon.visible)
                    {
                        iconPosition = Steps.Icon.position;
                        hmUI_widget_IMG img_icon = Steps.Icon;
                        iconOptions = IMG_Options(img_icon, show_level);
                    }

                    for (int index = 1; index <= 10; index++)
                    {
                        // Images
                        if (index == imagesPosition && imagesOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_image_progress_img_level = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "step_image_progress_img_level = hmUI.createWidget(hmUI.widget.IMG_LEVEL, {" +
                                    imagesOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Segments
                        if (index == segmentsPosition && segmentsOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_image_progress_img_progress = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "step_image_progress_img_progress = hmUI.createWidget(hmUI.widget.IMG_PROGRESS, {" +
                                    segmentsOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Number
                        if (index == numberPosition && numberOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_current_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "step_current_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "step_current_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "step_current_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Number_Target
                        if (index == numberTargetPosition && numberTargetOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_target_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "step_target_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberTargetOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberTargetOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "step_target_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "step_target_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberTargetOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Pointer
                        if (index == pointerPosition && pointerOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_pointer_progress_img_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "step_pointer_progress_img_pointer = hmUI.createWidget(hmUI.widget.IMG_POINTER, {" +
                                    pointerOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Circle_Scale
                        if (index == circleScalePosition && circle_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_circle_scale = ''" + Environment.NewLine;

                            items += circleScaleOptions;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            //items += Environment.NewLine + TabInString(7) +
                            //    optionNameStart + "step_circle_scale = hmUI.createWidget(hmUI.widget.ARC_PROGRESS, {" +
                            //        circleScaleOptions + TabInString(7) + "});" + Environment.NewLine;

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "step_circle_scale = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;

                            if (circle_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "step_circle_scale_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "step_circle_scale_mirror = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;

                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales STEP');" + Environment.NewLine;
                            if (items.IndexOf("const step = hmSensor.createSensor(hmSensor.id.STEP);") < 0 &&
                                exist_items.IndexOf("const step = hmSensor.createSensor(hmSensor.id.STEP);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const step = hmSensor.createSensor(hmSensor.id.STEP);" + Environment.NewLine;
                                if (exist_items.IndexOf("step.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "step.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressStep") < 0 &&
                                exist_scale_call.IndexOf("progressStep") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueStep = step.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetStep = step.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressStep = valueStep/targetStep;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressStep > 1) progressStep = 1;" + Environment.NewLine;
                            }

                            if (circle_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "step = 1 - progressStep;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "step = progressStep;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) + 
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "step");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) + 
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "step");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Linear_Scale
                        if (index == linearScalePosition && linear_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_linear_scale = ''" + Environment.NewLine;
                            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "step_linear_scale_pointer_img = ''" + Environment.NewLine;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "step_linear_scale = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;

                            if (linear_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "step_linear_scale_mirror = ''" + Environment.NewLine;
                                if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "step_linear_scale_pointer_img_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "step_linear_scale_mirror = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;

                            items += linearScaleOptions;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales STEP');" + Environment.NewLine;
                            if (items.IndexOf("const step = hmSensor.createSensor(hmSensor.id.STEP);") < 0 &&
                                exist_items.IndexOf("const step = hmSensor.createSensor(hmSensor.id.STEP);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const step = hmSensor.createSensor(hmSensor.id.STEP);" + Environment.NewLine;
                                if (exist_items.IndexOf("step.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "step.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressStep") < 0 &&
                                exist_scale_call.IndexOf("progressStep") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueStep = step.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetStep = step.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressStep = valueStep/targetStep;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressStep > 1) progressStep = 1;" + Environment.NewLine;
                            }
                            if (linear_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "step = 1 - progressStep;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "step = progressStep;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) + 
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "step", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) + 
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "step", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Icon
                        if (index == iconPosition && iconOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "step_icon_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "step_icon_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                    iconOptions + TabInString(6) + "});" + Environment.NewLine;
                        }


                    }
                    break;
                #endregion

                #region ElementBattery
                case "ElementBattery":
                    ElementBattery Battery = (ElementBattery)element;

                    if (!Battery.visible) return;
                    if (Battery.Images != null && Battery.Images.visible)
                    {
                        imagesPosition = Battery.Images.position;
                        hmUI_widget_IMG_LEVEL img_images = Battery.Images;
                        imagesOptions = IMG_IMAGES_Options(img_images, "BATTERY", show_level);
                    }
                    if (Battery.Segments != null && Battery.Segments.visible)
                    {
                        segmentsPosition = Battery.Segments.position;
                        hmUI_widget_IMG_PROGRESS img_progress = Battery.Segments;
                        segmentsOptions = IMG_PROGRESS_Options(img_progress, "BATTERY", show_level);
                    }
                    if (Battery.Number != null && Battery.Number.visible)
                    {
                        numberPosition = Battery.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = Battery.Number;
                        numberOptions = IMG_NUMBER_Options(img_number, "BATTERY", show_level);

                        numberOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (Battery.Pointer != null && Battery.Pointer.visible)
                    {
                        pointerPosition = Battery.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = Battery.Pointer;
                        pointerOptions = IMG_POINTER_Options(img_pointer, "BATTERY", show_level);
                    }

                    if (Battery.Circle_Scale != null && Battery.Circle_Scale.visible)
                    {
                        circleScalePosition = Battery.Circle_Scale.position;
                        circle_scale = Battery.Circle_Scale;

                        circleScaleOptions = Circle_Scale_Options(circle_scale, optionNameStart, "BATTERY", show_level);
                        //circleScaleMirrorOptions = Circle_Scale_Options(linear_scale, true);
                    }
                    if (Battery.Linear_Scale != null && Battery.Linear_Scale.visible)
                    {
                        linearScalePosition = Battery.Linear_Scale.position;
                        linear_scale = Battery.Linear_Scale;

                        linearScaleOptions = Linear_Scale_Options(linear_scale, optionNameStart, "BATTERY", "battery", show_level);
                    }

                    if (Battery.Icon != null && Battery.Icon.visible)
                    {
                        iconPosition = Battery.Icon.position;
                        hmUI_widget_IMG img_icon = Battery.Icon;
                        iconOptions = IMG_Options(img_icon, show_level);
                    }

                    for (int index = 1; index <= 10; index++)
                    {
                        // Images
                        if (index == imagesPosition && imagesOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "battery_image_progress_img_level = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "battery_image_progress_img_level = hmUI.createWidget(hmUI.widget.IMG_LEVEL, {" +
                                    imagesOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Segments
                        if (index == segmentsPosition && segmentsOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "battery_image_progress_img_progress = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "battery_image_progress_img_progress = hmUI.createWidget(hmUI.widget.IMG_PROGRESS, {" +
                                    segmentsOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Number
                        if (index == numberPosition && numberOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "battery_text_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "battery_text_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "battery_text_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "battery_text_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Pointer
                        if (index == pointerPosition && pointerOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "battery_pointer_progress_img_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "battery_pointer_progress_img_pointer = hmUI.createWidget(hmUI.widget.IMG_POINTER, {" +
                                    pointerOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Circle_Scale
                        if (index == circleScalePosition && circle_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "battery_circle_scale = ''" + Environment.NewLine;

                            items += circleScaleOptions;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "battery_circle_scale = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;

                            if (circle_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "battery_circle_scale_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "battery_circle_scale_mirror = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales BATTERY');" + Environment.NewLine;
                            if (items.IndexOf("const battery = hmSensor.createSensor(hmSensor.id.BATTERY);") < 0 &&
                                exist_items.IndexOf("const battery = hmSensor.createSensor(hmSensor.id.BATTERY);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const battery = hmSensor.createSensor(hmSensor.id.BATTERY);" + Environment.NewLine;
                                if (exist_items.IndexOf("battery.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "battery.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressBattery") < 0 &&
                                exist_scale_call.IndexOf("progressBattery") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueBattery = battery.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetBattery = 100;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let targetBattery = battery.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressBattery = valueBattery/targetBattery;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressBattery > 1) progressBattery = 1;" + Environment.NewLine;
                            }

                            if (circle_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "battery = 1 - progressBattery;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "battery = progressBattery;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "battery");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "battery");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Linear_Scale
                        if (index == linearScalePosition && linear_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "battery_linear_scale = ''" + Environment.NewLine;
                            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "battery_linear_scale_pointer_img = ''" + Environment.NewLine;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "battery_linear_scale = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;

                            if (linear_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "battery_linear_scale_mirror = ''" + Environment.NewLine;
                                if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "battery_linear_scale_pointer_img_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "battery_linear_scale_mirror = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;

                            items += linearScaleOptions;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales BATTERY');" + Environment.NewLine;
                            if (items.IndexOf("const battery = hmSensor.createSensor(hmSensor.id.BATTERY);") < 0 &&
                                exist_items.IndexOf("const battery = hmSensor.createSensor(hmSensor.id.BATTERY);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const battery = hmSensor.createSensor(hmSensor.id.BATTERY);" + Environment.NewLine;
                                if (exist_items.IndexOf("battery.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "battery.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressBattery") < 0 &&
                                exist_scale_call.IndexOf("progressBattery") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueBattery = battery.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetBattery = 100;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let targetBattery = battery.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressBattery = valueBattery/targetBattery;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressBattery > 1) progressBattery = 1;" + Environment.NewLine;
                            }

                            if (linear_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "battery = 1 - progressBattery;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "battery = progressBattery;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "battery", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "battery", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Icon
                        if (index == iconPosition && iconOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "battery_icon_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "battery_icon_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                    iconOptions + TabInString(6) + "});" + Environment.NewLine;
                        }


                    }
                    break;
                #endregion

                #region ElementCalories
                case "ElementCalories":
                    ElementCalories Calories = (ElementCalories)element;

                    if (!Calories.visible) return;
                    if (Calories.Images != null && Calories.Images.visible)
                    {
                        imagesPosition = Calories.Images.position;
                        hmUI_widget_IMG_LEVEL img_images = Calories.Images;
                        imagesOptions = IMG_IMAGES_Options(img_images, "CAL", show_level);
                    }
                    if (Calories.Segments != null && Calories.Segments.visible)
                    {
                        segmentsPosition = Calories.Segments.position;
                        hmUI_widget_IMG_PROGRESS img_progress = Calories.Segments;
                        segmentsOptions = IMG_PROGRESS_Options(img_progress, "CAL", show_level);
                    }
                    if (Calories.Number != null && Calories.Number.visible)
                    {
                        numberPosition = Calories.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = Calories.Number;
                        numberOptions = IMG_NUMBER_Options(img_number, "CAL", show_level);

                        numberOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (Calories.Number_Target != null && Calories.Number_Target.visible)
                    {
                        numberTargetPosition = Calories.Number_Target.position;
                        hmUI_widget_IMG_NUMBER img_number = Calories.Number_Target;
                        numberTargetOptions = IMG_NUMBER_Options(img_number, "CAL_TARGET", show_level);

                        numberTargetOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (Calories.Pointer != null && Calories.Pointer.visible)
                    {
                        pointerPosition = Calories.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = Calories.Pointer;
                        pointerOptions = IMG_POINTER_Options(img_pointer, "CAL", show_level);
                    }

                    if (Calories.Circle_Scale != null && Calories.Circle_Scale.visible)
                    {
                        circleScalePosition = Calories.Circle_Scale.position;
                        circle_scale = Calories.Circle_Scale;

                        circleScaleOptions = Circle_Scale_Options(circle_scale, optionNameStart, "CAL", show_level);
                        //circleScaleMirrorOptions = Circle_Scale_Options(linear_scale, true);
                    }
                    if (Calories.Linear_Scale != null && Calories.Linear_Scale.visible)
                    {
                        linearScalePosition = Calories.Linear_Scale.position;
                        linear_scale = Calories.Linear_Scale;

                        linearScaleOptions = Linear_Scale_Options(linear_scale, optionNameStart, "CAL", "calorie", show_level);
                    }

                    if (Calories.Icon != null && Calories.Icon.visible)
                    {
                        iconPosition = Calories.Icon.position;
                        hmUI_widget_IMG img_icon = Calories.Icon;
                        iconOptions = IMG_Options(img_icon, show_level);
                    }

                    for (int index = 1; index <= 10; index++)
                    {
                        // Images
                        if (index == imagesPosition && imagesOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_image_progress_img_level = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "calorie_image_progress_img_level = hmUI.createWidget(hmUI.widget.IMG_LEVEL, {" +
                                    imagesOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Segments
                        if (index == segmentsPosition && segmentsOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_image_progress_img_progress = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "calorie_image_progress_img_progress = hmUI.createWidget(hmUI.widget.IMG_PROGRESS, {" +
                                    segmentsOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Number
                        if (index == numberPosition && numberOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_current_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "calorie_current_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "calorie_current_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "calorie_current_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Number_Target
                        if (index == numberTargetPosition && numberTargetOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_target_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "calorie_target_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberTargetOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberTargetOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "calorie_target_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "calorie_target_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberTargetOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Pointer
                        if (index == pointerPosition && pointerOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_pointer_progress_img_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "calorie_pointer_progress_img_pointer = hmUI.createWidget(hmUI.widget.IMG_POINTER, {" +
                                    pointerOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Circle_Scale
                        if (index == circleScalePosition && circle_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_circle_scale = ''" + Environment.NewLine;

                            items += circleScaleOptions;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "calorie_circle_scale = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;

                            if (circle_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_circle_scale_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "calorie_circle_scale_mirror = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales CALORIE');" + Environment.NewLine;
                            if (items.IndexOf("const calorie = hmSensor.createSensor(hmSensor.id.CALORIE);") < 0 &&
                                exist_items.IndexOf("const calorie = hmSensor.createSensor(hmSensor.id.CALORIE);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const calorie = hmSensor.createSensor(hmSensor.id.CALORIE);" + Environment.NewLine;
                                if (exist_items.IndexOf("calorie.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "calorie.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressCalories") < 0 &&
                                exist_scale_call.IndexOf("progressCalories") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueCalories = calorie.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetCalories = calorie.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressCalories = valueCalories/targetCalories;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressCalories > 1) progressCalories = 1;" + Environment.NewLine;
                            }

                            if (circle_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "calorie = 1 - progressCalories;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "calorie = progressCalories;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "calorie");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "calorie");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Linear_Scale
                        if (index == linearScalePosition && linear_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_linear_scale = ''" + Environment.NewLine;
                            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "calorie_linear_scale_pointer_img = ''" + Environment.NewLine;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "calorie_linear_scale = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;

                            if (linear_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_linear_scale_mirror = ''" + Environment.NewLine;
                                if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "calorie_linear_scale_pointer_img_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "calorie_linear_scale_mirror = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;

                            items += linearScaleOptions;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales CALORIE');" + Environment.NewLine;
                            if (items.IndexOf("const calorie = hmSensor.createSensor(hmSensor.id.CALORIE);") < 0 &&
                                exist_items.IndexOf("const calorie = hmSensor.createSensor(hmSensor.id.CALORIE);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const calorie = hmSensor.createSensor(hmSensor.id.CALORIE);" + Environment.NewLine;
                                if (exist_items.IndexOf("calorie.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "calorie.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressCalories") < 0 &&
                                exist_scale_call.IndexOf("progressCalories") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueCalories = calorie.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetCalories = calorie.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressCalories = valueCalories/targetCalories;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressCalories > 1) progressCalories = 1;" + Environment.NewLine;
                            }

                            if (linear_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "calorie = 1 - progressCalories;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "calorie = progressCalories;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "calorie", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "calorie", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Icon
                        if (index == iconPosition && iconOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "calorie_icon_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "calorie_icon_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                    iconOptions + TabInString(6) + "});" + Environment.NewLine;
                        }


                    }
                    break;
                #endregion

                #region ElementHeart
                case "ElementHeart":
                    ElementHeart Heart = (ElementHeart)element;

                    if (!Heart.visible) return;
                    if (Heart.Images != null && Heart.Images.visible)
                    {
                        imagesPosition = Heart.Images.position;
                        hmUI_widget_IMG_LEVEL img_images = Heart.Images;
                        imagesOptions = IMG_IMAGES_Options(img_images, "HEART", show_level);
                    }
                    if (Heart.Segments != null && Heart.Segments.visible)
                    {
                        segmentsPosition = Heart.Segments.position;
                        hmUI_widget_IMG_PROGRESS img_progress = Heart.Segments;
                        segmentsOptions = IMG_PROGRESS_Options(img_progress, "HEART", show_level);
                    }
                    if (Heart.Number != null && Heart.Number.visible)
                    {
                        numberPosition = Heart.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = Heart.Number;
                        numberOptions = IMG_NUMBER_Options(img_number, "HEART", show_level);

                        numberOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (Heart.Pointer != null && Heart.Pointer.visible)
                    {
                        pointerPosition = Heart.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = Heart.Pointer;
                        pointerOptions = IMG_POINTER_Options(img_pointer, "HEART", show_level);
                    }

                    if (Heart.Circle_Scale != null && Heart.Circle_Scale.visible)
                    {
                        circleScalePosition = Heart.Circle_Scale.position;
                        circle_scale = Heart.Circle_Scale;

                        circleScaleOptions = Circle_Scale_Options(circle_scale, optionNameStart, "HEART", show_level);
                        //circleScaleMirrorOptions = Circle_Scale_Options(linear_scale, true);
                    }
                    if (Heart.Linear_Scale != null && Heart.Linear_Scale.visible)
                    {
                        linearScalePosition = Heart.Linear_Scale.position;
                        linear_scale = Heart.Linear_Scale;

                        linearScaleOptions = Linear_Scale_Options(linear_scale, optionNameStart, "HEART", "heart_rate", show_level);
                    }

                    if (Heart.Icon != null && Heart.Icon.visible)
                    {
                        iconPosition = Heart.Icon.position;
                        hmUI_widget_IMG img_icon = Heart.Icon;
                        iconOptions = IMG_Options(img_icon, show_level);
                    }

                    for (int index = 1; index <= 10; index++)
                    {
                        // Images
                        if (index == imagesPosition && imagesOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_image_progress_img_level = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "heart_rate_image_progress_img_level = hmUI.createWidget(hmUI.widget.IMG_LEVEL, {" +
                                    imagesOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Segments
                        if (index == segmentsPosition && segmentsOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_image_progress_img_progress = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "heart_rate_image_progress_img_progress = hmUI.createWidget(hmUI.widget.IMG_PROGRESS, {" +
                                    segmentsOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Number
                        if (index == numberPosition && numberOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_text_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "heart_rate_text_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "heart_rate_text_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "heart_rate_text_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Pointer
                        if (index == pointerPosition && pointerOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_pointer_progress_img_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "heart_rate_pointer_progress_img_pointer = hmUI.createWidget(hmUI.widget.IMG_POINTER, {" +
                                    pointerOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Circle_Scale
                        if (index == circleScalePosition && circle_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_circle_scale = ''" + Environment.NewLine;

                            items += circleScaleOptions;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            //items += Environment.NewLine + TabInString(7) +
                            //    optionNameStart + "heart_rate_circle_scale = hmUI.createWidget(hmUI.widget.ARC_PROGRESS, {" +
                            //        circleScaleOptions + TabInString(7) + "});" + Environment.NewLine;

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "heart_rate_circle_scale = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;

                            if (circle_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_circle_scale_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "heart_rate_circle_scale_mirror = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales HEART');" + Environment.NewLine;
                            if (items.IndexOf("const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);") < 0 &&
                                exist_items.IndexOf("const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);" + Environment.NewLine;
                                if (exist_items.IndexOf("heart_rate.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "heart_rate.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine; 
                                }
                            }
                            if (scale_call.IndexOf("progressHeartRate") < 0 &&
                                exist_scale_call.IndexOf("progressHeartRate") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                //resume_call += TabInString(8) + "const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);" + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueHeartRate = heart_rate.last;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let valueHeartRate = heart_rate.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetHeartRate = 179;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let targetHeartRate = heart_rate.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressHeartRate = (valueHeartRate - 71)/(targetHeartRate - 71);" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressHeartRate < 0) progressHeartRate = 0;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressHeartRate > 1) progressHeartRate = 1;" + Environment.NewLine;
                            }
                            if (circle_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "heart_rate = 1 - progressHeartRate;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "heart_rate = progressHeartRate;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "heart_rate");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "heart_rate");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Linear_Scale
                        if (index == linearScalePosition && linear_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_linear_scale = ''" + Environment.NewLine;
                            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "heart_rate_linear_scale_pointer_img = ''" + Environment.NewLine;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "heart_rate_linear_scale = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;

                            if (linear_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_linear_scale_mirror = ''" + Environment.NewLine;
                                if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "heart_rate_linear_scale_pointer_img_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "heart_rate_linear_scale_mirror = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;

                            items += linearScaleOptions;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales HEART');" + Environment.NewLine;
                            if (items.IndexOf("const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);") < 0 &&
                                exist_items.IndexOf("const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);" + Environment.NewLine;
                                if (exist_items.IndexOf("heart_rate.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "heart_rate.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }
                            
                            if (scale_call.IndexOf("progressHeartRate") < 0 &&
                                exist_scale_call.IndexOf("progressHeartRate") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                //resume_call += TabInString(8) + "const heart_rate = hmSensor.createSensor(hmSensor.id.HEART);" + Environment.NewLine;
                                scale_call += TabInString(8) + "let valueHeartRate = heart_rate.last;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let valueHeartRate = heart_rate.current;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetHeartRate = 179;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let targetHeartRate = heart_rate.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressHeartRate = (valueHeartRate - 71)/(targetHeartRate - 71);" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressHeartRate < 0) progressHeartRate = 0;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressHeartRate > 1) progressHeartRate = 1;" + Environment.NewLine;
                            }
                            
                            if (linear_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "heart_rate = 1 - progressHeartRate;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "heart_rate = progressHeartRate;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "heart_rate", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "heart_rate", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Icon
                        if (index == iconPosition && iconOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "heart_rate_icon_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "heart_rate_icon_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                    iconOptions + TabInString(6) + "});" + Environment.NewLine;
                        }


                    }
                    break;
                #endregion

                #region ElementPAI
                case "ElementPAI":
                    ElementPAI PAI = (ElementPAI)element;

                    if (!PAI.visible) return;
                    if (PAI.Images != null && PAI.Images.visible)
                    {
                        imagesPosition = PAI.Images.position;
                        hmUI_widget_IMG_LEVEL img_images = PAI.Images;
                        imagesOptions = IMG_IMAGES_Options(img_images, "PAI_WEEKLY", show_level);
                    }
                    if (PAI.Segments != null && PAI.Segments.visible)
                    {
                        segmentsPosition = PAI.Segments.position;
                        hmUI_widget_IMG_PROGRESS img_progress = PAI.Segments;
                        segmentsOptions = IMG_PROGRESS_Options(img_progress, "PAI_WEEKLY", show_level);
                    }
                    if (PAI.Number != null && PAI.Number.visible)
                    {
                        numberPosition = PAI.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = PAI.Number;
                        numberOptions = IMG_NUMBER_Options(img_number, "PAI_DAILY", show_level);

                        numberOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (PAI.Number_Target != null && PAI.Number_Target.visible)
                    {
                        numberTargetPosition = PAI.Number_Target.position;
                        hmUI_widget_IMG_NUMBER img_number = PAI.Number_Target;
                        numberTargetOptions = IMG_NUMBER_Options(img_number, "PAI_WEEKLY", show_level);

                        numberTargetOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }
                    if (PAI.Pointer != null && PAI.Pointer.visible)
                    {
                        pointerPosition = PAI.Pointer.position;
                        hmUI_widget_IMG_POINTER img_pointer = PAI.Pointer;
                        pointerOptions = IMG_POINTER_Options(img_pointer, "PAI_WEEKLY", show_level);
                    }

                    if (PAI.Circle_Scale != null && PAI.Circle_Scale.visible)
                    {
                        circleScalePosition = PAI.Circle_Scale.position;
                        circle_scale = PAI.Circle_Scale;

                        circleScaleOptions = Circle_Scale_Options(circle_scale, optionNameStart, "PAI_WEEKLY", show_level);
                        //circleScaleMirrorOptions = Circle_Scale_Options(linear_scale, true);
                    }
                    if (PAI.Linear_Scale != null && PAI.Linear_Scale.visible)
                    {
                        linearScalePosition = PAI.Linear_Scale.position;
                        linear_scale = PAI.Linear_Scale;

                        linearScaleOptions = Linear_Scale_Options(linear_scale, optionNameStart, "PAI_WEEKLY", "pai", show_level);
                    }

                    if (PAI.Icon != null && PAI.Icon.visible)
                    {
                        iconPosition = PAI.Icon.position;
                        hmUI_widget_IMG img_icon = PAI.Icon;
                        iconOptions = IMG_Options(img_icon, show_level);
                    }

                    for (int index = 1; index <= 10; index++)
                    {
                        // Images
                        if (index == imagesPosition && imagesOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_image_progress_img_level = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "pai_image_progress_img_level = hmUI.createWidget(hmUI.widget.IMG_LEVEL, {" +
                                    imagesOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Segments
                        if (index == segmentsPosition && segmentsOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_image_progress_img_progress = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "pai_image_progress_img_progress = hmUI.createWidget(hmUI.widget.IMG_PROGRESS, {" +
                                    segmentsOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Number
                        if (index == numberPosition && numberOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_day_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "pai_day_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "pai_day_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "pai_day_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Number_Target
                        if (index == numberTargetPosition && numberTargetOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_weekly_text_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "pai_weekly_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                    numberTargetOptions + TabInString(6) + "});" + Environment.NewLine;

                            if (numberTargetOptions_separator.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "pai_weekly_separator_img = ''" + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "pai_weekly_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                        numberTargetOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                            }
                        }

                        // Pointer
                        if (index == pointerPosition && pointerOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_pointer_progress_img_pointer = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "pai_pointer_progress_img_pointer = hmUI.createWidget(hmUI.widget.IMG_POINTER, {" +
                                    pointerOptions + TabInString(6) + "});" + Environment.NewLine;
                        }

                        // Circle_Scale
                        if (index == circleScalePosition && circle_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_circle_scale = ''" + Environment.NewLine;

                            items += circleScaleOptions;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            //items += Environment.NewLine + TabInString(7) +
                            //    optionNameStart + "pai_circle_scale = hmUI.createWidget(hmUI.widget.ARC_PROGRESS, {" +
                            //        circleScaleOptions + TabInString(7) + "});" + Environment.NewLine;

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "pai_circle_scale = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;

                            if (circle_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "pai_circle_scale_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "pai_circle_scale_mirror = hmUI.createWidget(hmUI.widget.ARC);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales PAI');" + Environment.NewLine;
                            if (items.IndexOf("const pai = hmSensor.createSensor(hmSensor.id.PAI);") < 0 &&
                                exist_items.IndexOf("const pai = hmSensor.createSensor(hmSensor.id.PAI);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const pai = hmSensor.createSensor(hmSensor.id.PAI);" + Environment.NewLine;
                                if (exist_items.IndexOf("calorie.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "pai.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressPAI") < 0 &&
                                exist_scale_call.IndexOf("progressPAI") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valuePAI = pai.totalpai;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetPAI = 100;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let targetPAI = pai.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressPAI = valuePAI/targetPAI;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressPAI > 1) progressPAI = 1;" + Environment.NewLine;
                            }

                            if (circle_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "pai = 1 - progressPAI;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_cs_" + optionNameStart +
                                "pai = progressPAI;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "pai");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Circle_Scale_WidgetDelegate_Options(circle_scale, optionNameStart, "pai");
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Linear_Scale
                        if (index == linearScalePosition && linear_scale != null)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_linear_scale = ''" + Environment.NewLine;
                            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "pai_linear_scale_pointer_img = ''" + Environment.NewLine;

                            if (items.IndexOf("let screenType = hmSetting.getScreenType();") < 0)
                                items += Environment.NewLine + TabInString(6) + "let screenType = hmSetting.getScreenType();";
                            if (optionNameStart == "normal_")
                                items += Environment.NewLine + TabInString(6) + "if (screenType != hmSetting.screen_type.AOD) {";
                            else items += Environment.NewLine + TabInString(6) + "if (screenType == hmSetting.screen_type.AOD) {";

                            items += Environment.NewLine + TabInString(7) +
                                optionNameStart + "pai_linear_scale = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;

                            if (linear_scale.mirror)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                "pai_linear_scale_mirror = ''" + Environment.NewLine;
                                if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                                    variables += TabInString(4) + "let " + optionNameStart +
                                        "pai_linear_scale_pointer_img_mirror = ''" + Environment.NewLine;

                                items += TabInString(7) + optionNameStart +
                                    "pai_linear_scale_mirror = hmUI.createWidget(hmUI.widget.FILL_RECT);" + Environment.NewLine;
                            }

                            items += TabInString(6) + "};" + Environment.NewLine;

                            items += linearScaleOptions;


                            scale_call += Environment.NewLine + TabInString(8) + "console.log('update scales PAI');" + Environment.NewLine;
                            if (items.IndexOf("const pai = hmSensor.createSensor(hmSensor.id.PAI);") < 0 &&
                                exist_items.IndexOf("const pai = hmSensor.createSensor(hmSensor.id.PAI);") < 0)
                            {
                                items += TabInString(6) + Environment.NewLine;
                                items += TabInString(6) + "const pai = hmSensor.createSensor(hmSensor.id.PAI);" + Environment.NewLine;
                                if (exist_items.IndexOf("calorie.addEventListener") < 0)
                                {
                                    items += TabInString(6) + "pai.addEventListener(hmSensor.event.CHANGE, function() {" + Environment.NewLine;
                                    items += TabInString(7) + "scale_call();" + Environment.NewLine;
                                    items += TabInString(6) + "});" + Environment.NewLine;
                                }
                            }

                            if (scale_call.IndexOf("progressPAI") < 0 &&
                                exist_scale_call.IndexOf("progressPAI") < 0)
                            {
                                scale_call += TabInString(8) + Environment.NewLine;
                                scale_call += TabInString(8) + "let valuePAI = pai.totalpai;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let targetPAI = 100;" + Environment.NewLine;
                                //resume_call += TabInString(8) + "let targetPAI = pai.target;" + Environment.NewLine;
                                scale_call += TabInString(8) + "let progressPAI = valuePAI/targetPAI;" + Environment.NewLine;
                                scale_call += TabInString(8) + "if (progressPAI > 1) progressPAI = 1;" + Environment.NewLine;
                            }

                            if (linear_scale.inversion)
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "pai = 1 - progressPAI;" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += TabInString(8) + "let progress_ls_" + optionNameStart +
                                "pai = progressPAI;" + Environment.NewLine;
                            }
                            if (optionNameStart == "normal_")
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType != hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "pai", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }
                            else
                            {
                                scale_call += Environment.NewLine + TabInString(8) +
                                    "if (screenType == hmSetting.screen_type.AOD) {" + Environment.NewLine;
                                scale_call += Linear_Scale_WidgetDelegate_Options(linear_scale, optionNameStart, "pai", show_level);
                                scale_call += TabInString(8) + "};" + Environment.NewLine;
                            }




                        }

                        // Icon
                        if (index == iconPosition && iconOptions.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "pai_icon_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "pai_icon_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                    iconOptions + TabInString(6) + "});" + Environment.NewLine;
                        }


                    }
                    break;
                #endregion

                #region ElementDistance
                case "ElementDistance":
                    ElementDistance Distance = (ElementDistance)element;

                    if (!Distance.visible) return;
                    if (Distance.Number != null)
                    {
                        numberPosition = Distance.Number.position;
                        hmUI_widget_IMG_NUMBER img_number = Distance.Number;
                        numberOptions = IMG_NUMBER_Options(img_number, "DISTANCE", show_level);

                        numberOptions_separator = IMG_Separator_Options(img_number, show_level);
                    }

                    // Number
                    if ( numberOptions.Length > 5)
                    {
                        variables += TabInString(4) + "let " + optionNameStart +
                            "distance_text_text_img = ''" + Environment.NewLine;
                        items += Environment.NewLine + TabInString(6) +
                            optionNameStart + "distance_text_text_img = hmUI.createWidget(hmUI.widget.TEXT_IMG, {" +
                                numberOptions + TabInString(6) + "});" + Environment.NewLine;

                        if (numberOptions_separator.Length > 5)
                        {
                            variables += TabInString(4) + "let " + optionNameStart +
                                "distance_text_separator_img = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "distance_text_separator_img = hmUI.createWidget(hmUI.widget.IMG, {" +
                                    numberOptions_separator + TabInString(6) + "});" + Environment.NewLine;
                        }
                    }
                    break;
                    #endregion
            }
        }

        private string FILL_RECT_Options(hmUI_widget_FILL_RECT fill_rect, string show_level)
        {
            string options = Environment.NewLine;
            if (fill_rect == null) return options;
            options += TabInString(7) + "x: " + fill_rect.x.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "y: " + fill_rect.y.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "w: " + fill_rect.w.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "h: " + fill_rect.h.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "color: '" + fill_rect.color + "'," + Environment.NewLine;
            options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
            return options;
        }

        private string IMG_Options(hmUI_widget_IMG img, string show_level)
        {
            string options = Environment.NewLine;
            if (img == null) return options;
            if (img.src == null) return options;
            if (img.src.Length > 0)
            {
                options += TabInString(7) + "x: " + img.x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img.y.ToString() + "," + Environment.NewLine;
                if (img.w != null) options += TabInString(7) + "w: " + img.w.ToString() + "," + Environment.NewLine;
                if (img.h != null) options += TabInString(7) + "h: " + img.h.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "src: '" + img.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine; 
            }
            return options;
        }

        private string IMG_Separator_Options(hmUI_widget_IMG_NUMBER img_number, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number == null) return options;
            if (img_number.icon != null && img_number.icon.Length > 0)
            {
                options += TabInString(7) + "x: " + img_number.iconPosX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img_number.iconPosY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "src: '" + img_number.icon + ".png'," + Environment.NewLine;
                options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
            }
            return options;
        }

        private string IMG_NUMBER_Options(hmUI_widget_IMG_NUMBER img_number, string type, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number == null) return options;
            if (img_number.img_First == null) return options;
            string img = img_number.img_First;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + 10 > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string img_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    img_array += file_name;
                    if (i < imgPosition + 9) img_array += ",";
                }
                img_array += "]";
                options += TabInString(7) + "x: " + img_number.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img_number.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "font_array: " + img_array + "," + Environment.NewLine;
                if (img_number.zero) options += TabInString(7) + "padding: true," + Environment.NewLine;
                else options += TabInString(7) + "padding: false," + Environment.NewLine;

                //options += TabInString(7) + "padding: " + img_number.zero.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "h_space: " + img_number.space.ToString() + "," + Environment.NewLine;
                if (img_number.unit != null && img_number.unit.Length > 0)
                {
                    string unit = "'" + img_number.unit + ".png'";
                    options += TabInString(7) + "unit_sc: " + unit + "," + Environment.NewLine;
                    options += TabInString(7) + "unit_tc: " + unit + "," + Environment.NewLine;
                    options += TabInString(7) + "unit_en: " + unit + "," + Environment.NewLine;
                }

                if (img_number.negative_image != null && img_number.negative_image.Length > 0)
                {
                    string negative_image = "'" + img_number.negative_image + ".png'";
                    options += TabInString(7) + "negative_image: " + negative_image + "," + Environment.NewLine;
                }

                if (img_number.invalid_image != null && img_number.invalid_image.Length > 0)
                {
                    string invalid_image = "'" + img_number.invalid_image + ".png'";
                    options += TabInString(7) + "invalid_image: " + invalid_image + "," + Environment.NewLine;
                }

                if (img_number.dot_image != null && img_number.dot_image.Length > 0)
                {
                    string dot_path = "'" + img_number.dot_image + ".png'";
                    options += TabInString(7) + "dot_image: " + dot_path + "," + Environment.NewLine;
                }

                options += TabInString(7) + "align_h: hmUI.align." + img_number.align.ToUpper() + "," + Environment.NewLine;


                if (type.Length > 0)
                {
                    options += TabInString(7) + "type: hmUI.data_type." + type + "," + Environment.NewLine;
                }

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_NUMBER_Hour_Options(hmUI_widget_IMG_NUMBER img_number_hour, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number_hour == null) return options;
            if (img_number_hour.img_First == null) return options;
            string img = img_number_hour.img_First;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + 10 > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string hour_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    hour_array += file_name;
                    if (i < imgPosition + 9) hour_array += ",";
                }
                hour_array += "]";
                options += TabInString(7) + "hour_startX: " + img_number_hour.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "hour_startY: " + img_number_hour.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "hour_array: " + hour_array + "," + Environment.NewLine;
                if (img_number_hour.zero) options += TabInString(7) + "hour_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "hour_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "hour_space: " + img_number_hour.space.ToString() + "," + Environment.NewLine;
                if (img_number_hour.unit != null && img_number_hour.unit.Length > 0)
                {
                    string hour_unit = "'" + img_number_hour.unit + ".png'";
                    options += TabInString(7) + "hour_unit_sc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "hour_unit_tc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "hour_unit_en: " + hour_unit + "," + Environment.NewLine;
                }
                options += TabInString(7) + "hour_align: hmUI.align." + img_number_hour.align.ToUpper() + "," + Environment.NewLine;

                //options += TabInString(7) + "show_level: hmUI.show_level." + img_number_hour.show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                } 
            }
            return options;
        }

        private string IMG_NUMBER_Minute_Options(hmUI_widget_IMG_NUMBER img_number_minute, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number_minute == null) return options;
            if (img_number_minute.img_First == null) return options;
            string img = img_number_minute.img_First;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + 10 > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string minute_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    minute_array += file_name;
                    if (i < imgPosition + 9) minute_array += ",";
                }
                minute_array += "]";
                options += TabInString(7) + "minute_startX: " + img_number_minute.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "minute_startY: " + img_number_minute.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "minute_array: " + minute_array + "," + Environment.NewLine;
                if (img_number_minute.zero) options += TabInString(7) + "minute_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "minute_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "minute_space: " + img_number_minute.space.ToString() + "," + Environment.NewLine;
                if (img_number_minute.follow) options += TabInString(7) + "minute_follow: 1," + Environment.NewLine;
                else options += TabInString(7) + "minute_follow: 0," + Environment.NewLine;
                if (img_number_minute.unit != null && img_number_minute.unit.Length > 0)
                {
                    string minute_unit = "'" + img_number_minute.unit + ".png'";
                    options += TabInString(7) + "minute_unit_en: " + minute_unit + "," + Environment.NewLine;
                }
                options += TabInString(7) + "minute_align: hmUI.align." + img_number_minute.align.ToUpper() + "," + Environment.NewLine;

                //options += TabInString(7) + "show_level: hmUI.show_level." + img_number_hour.show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                } 
            }
            return options;
        }

        private string IMG_NUMBER_Second_Options(hmUI_widget_IMG_NUMBER img_number_second, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number_second == null) return options;
            if (img_number_second.img_First == null) return options;
            string img = img_number_second.img_First;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + 10 > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string second_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    second_array += file_name;
                    if (i < imgPosition + 9) second_array += ",";
                }
                second_array += "]";
                options += TabInString(7) + "second_startX: " + img_number_second.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "second_startY: " + img_number_second.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "second_array: " + second_array + "," + Environment.NewLine;
                if (img_number_second.zero) options += TabInString(7) + "second_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "second_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "second_space: " + img_number_second.space.ToString() + "," + Environment.NewLine;
                if (img_number_second.follow) options += TabInString(7) + "second_follow: 1," + Environment.NewLine;
                else options += TabInString(7) + "second_follow: 0," + Environment.NewLine;
                if (img_number_second.unit != null && img_number_second.unit.Length > 0)
                {
                    string second_unit = "'" + img_number_second.unit + ".png'";
                    options += TabInString(7) + "second_unit_en: " + second_unit + "," + Environment.NewLine;
                }
                options += TabInString(7) + "second_align: hmUI.align." + img_number_second.align.ToUpper() + "," + Environment.NewLine;

                //options += TabInString(7) + "show_level: hmUI.show_level." + img_number_hour.show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                } 
            }
            return options;
        }

        private string AmPm_Options(hmUI_widget_IMG_TIME_am_pm am_pm, string show_level)
        {
            string options = Environment.NewLine;
            if (am_pm == null) return options;
            if (am_pm.am_img != null && am_pm.am_img.Length > 0 && am_pm.pm_img != null && am_pm.pm_img.Length > 0)
            {
                options += TabInString(7) + "am_x: " + am_pm.am_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "am_y: " + am_pm.am_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "am_sc_path: '" + am_pm.am_img + ".png'," + Environment.NewLine;
                options += TabInString(7) + "am_en_path: '" + am_pm.am_img + ".png'," + Environment.NewLine;

                options += TabInString(7) + "pm_x: " + am_pm.pm_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "pm_y: " + am_pm.pm_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "pm_sc_path: '" + am_pm.pm_img + ".png'," + Environment.NewLine;
                options += TabInString(7) + "pm_en_path: '" + am_pm.pm_img + ".png'," + Environment.NewLine;

                options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine; 
            }

            return options;
        }

        private string IMG_POINTER_Hour_Options(hmUI_widget_IMG_POINTER img_pointer_hour, string show_level)
        {
            string options = Environment.NewLine;
            if (img_pointer_hour == null) return options;
            if (img_pointer_hour.src != null && img_pointer_hour.src.Length > 0)
            {
                options += TabInString(7) + "hour_path: '" + img_pointer_hour.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "hour_centerX: " + img_pointer_hour.center_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "hour_centerY: " + img_pointer_hour.center_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "hour_posX: " + img_pointer_hour.pos_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "hour_posY: " + img_pointer_hour.pos_y.ToString() + "," + Environment.NewLine;

                if (img_pointer_hour.cover_path != null && img_pointer_hour.cover_path.Length > 0)
                {
                    options += TabInString(7) + "hour_cover_path: '" + img_pointer_hour.cover_path + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "hour_cover_x: " + img_pointer_hour.cover_x.ToString() + "," + Environment.NewLine;
                    options += TabInString(7) + "hour_cover_y: " + img_pointer_hour.cover_y.ToString() + "," + Environment.NewLine; 
                }

                //options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                } 
            }
            return options;
        }

        private string IMG_POINTER_Minute_Options(hmUI_widget_IMG_POINTER img_pointer_minute, string show_level)
        {
            string options = Environment.NewLine;
            if (img_pointer_minute == null) return options;
            if (img_pointer_minute.src != null && img_pointer_minute.src.Length > 0)
            {
                options += TabInString(7) + "minute_path: '" + img_pointer_minute.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "minute_centerX: " + img_pointer_minute.center_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "minute_centerY: " + img_pointer_minute.center_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "minute_posX: " + img_pointer_minute.pos_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "minute_posY: " + img_pointer_minute.pos_y.ToString() + "," + Environment.NewLine;

                if (img_pointer_minute.cover_path != null && img_pointer_minute.cover_path.Length > 0)
                {
                    options += TabInString(7) + "minute_cover_path: '" + img_pointer_minute.cover_path + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "minute_cover_x: " + img_pointer_minute.cover_x.ToString() + "," + Environment.NewLine;
                    options += TabInString(7) + "minute_cover_y: " + img_pointer_minute.cover_y.ToString() + "," + Environment.NewLine; 
                }

                //options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                } 
            }
            return options;
        }

        private string IMG_POINTER_Second_Options(hmUI_widget_IMG_POINTER img_pointer_second, string show_level)
        {
            string options = Environment.NewLine;
            if(img_pointer_second == null) return options;
            if (img_pointer_second.src != null && img_pointer_second.src.Length > 0)
            {
                options += TabInString(7) + "second_path: '" + img_pointer_second.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "second_centerX: " + img_pointer_second.center_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "second_centerY: " + img_pointer_second.center_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "second_posX: " + img_pointer_second.pos_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "second_posY: " + img_pointer_second.pos_y.ToString() + "," + Environment.NewLine;

                if (img_pointer_second.cover_path != null && img_pointer_second.cover_path.Length > 0)
                {
                    options += TabInString(7) + "second_cover_path: '" + img_pointer_second.cover_path + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "second_cover_x: " + img_pointer_second.cover_x.ToString() + "," + Environment.NewLine;
                    options += TabInString(7) + "second_cover_y: " + img_pointer_second.cover_y.ToString() + "," + Environment.NewLine; 
                }

                //options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                } 
            }
            return options;
        }

        private string DATE_POINTER_Options(hmUI_widget_IMG_POINTER img_pointer, string type, string show_level)
        {
            string options = Environment.NewLine;
            if (img_pointer == null) return options;
            if (img_pointer.src != null && img_pointer.src.Length > 0)
            {
                options += TabInString(7) + "src: '" + img_pointer.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "center_x: " + img_pointer.center_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "center_y: " + img_pointer.center_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "posX: " + img_pointer.pos_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "posY: " + img_pointer.pos_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "start_angle: " + img_pointer.start_angle.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "end_angle: " + img_pointer.end_angle.ToString() + "," + Environment.NewLine;

                if (img_pointer.scale != null && img_pointer.scale.Length > 0)
                {
                    options += TabInString(7) + "scale_sc: '" + img_pointer.scale + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "scale_tc: '" + img_pointer.scale + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "scale_en: '" + img_pointer.scale + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "scale_x: " + img_pointer.scale_x.ToString() + "," + Environment.NewLine;
                    options += TabInString(7) + "scale_y: " + img_pointer.scale_y.ToString() + "," + Environment.NewLine;
                }

                if (img_pointer.cover_path != null && img_pointer.cover_path.Length > 0)
                {
                    options += TabInString(7) + "cover_path: '" + img_pointer.cover_path + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "cover_x: " + img_pointer.cover_x.ToString() + "," + Environment.NewLine;
                    options += TabInString(7) + "cover_y: " + img_pointer.cover_y.ToString() + "," + Environment.NewLine;
                }
                //else
                //{
                //    options += TabInString(7) + "cover_path: ''," + Environment.NewLine;
                //    options += TabInString(7) + "cover_x: 0," + Environment.NewLine;
                //    options += TabInString(7) + "cover_y: 0," + Environment.NewLine;
                //}

                if (type.Length > 0)
                {
                    options += TabInString(7) + "type: hmUI.date." + type + "," + Environment.NewLine;
                }

                //options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_POINTER_Options(hmUI_widget_IMG_POINTER img_pointer, string type, string show_level)
        {
            string options = Environment.NewLine;
            if(img_pointer == null) return options;
            if (img_pointer.src != null && img_pointer.src.Length > 0)
            {
                options += TabInString(7) + "src: '" + img_pointer.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "center_x: " + img_pointer.center_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "center_y: " + img_pointer.center_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "x: " + img_pointer.pos_x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img_pointer.pos_y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "start_angle: " + img_pointer.start_angle.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "end_angle: " + img_pointer.end_angle.ToString() + "," + Environment.NewLine;

                if (img_pointer.scale != null && img_pointer.scale.Length > 0)
                {
                    options += TabInString(7) + "scale_sc: '" + img_pointer.scale + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "scale_tc: '" + img_pointer.scale + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "scale_en: '" + img_pointer.scale + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "scale_x: " + img_pointer.scale_x.ToString() + "," + Environment.NewLine;
                    options += TabInString(7) + "scale_y: " + img_pointer.scale_y.ToString() + "," + Environment.NewLine;
                }

                if (img_pointer.cover_path != null && img_pointer.cover_path.Length > 0)
                {
                    options += TabInString(7) + "cover_path: '" + img_pointer.cover_path + ".png'," + Environment.NewLine;
                    options += TabInString(7) + "cover_x: " + img_pointer.cover_x.ToString() + "," + Environment.NewLine;
                    options += TabInString(7) + "cover_y: " + img_pointer.cover_y.ToString() + "," + Environment.NewLine;
                }
                //else
                //{
                //    options += TabInString(7) + "cover_path: ''," + Environment.NewLine;
                //    options += TabInString(7) + "cover_x: 0," + Environment.NewLine;
                //    options += TabInString(7) + "cover_y: 0," + Environment.NewLine;
                //}

                if (type.Length > 0)
                {
                    options += TabInString(7) + "type: hmUI.data_type." + type + "," + Environment.NewLine;
                }

                //options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_NUMBER_Day_Options(hmUI_widget_IMG_NUMBER img_number_day, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number_day == null) return options;
            if (img_number_day.img_First == null) return options;
            string img = img_number_day.img_First;
            int count = 10;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + count > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string day_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    day_array += file_name;
                    if (i < imgPosition + count - 1) day_array += ",";
                }
                day_array += "]";
                options += TabInString(7) + "day_startX: " + img_number_day.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "day_startY: " + img_number_day.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "day_sc_array: " + day_array + "," + Environment.NewLine;
                options += TabInString(7) + "day_tc_array: " + day_array + "," + Environment.NewLine;
                options += TabInString(7) + "day_en_array: " + day_array + "," + Environment.NewLine;

                if (img_number_day.zero) options += TabInString(7) + "day_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "day_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "day_space: " + img_number_day.space.ToString() + "," + Environment.NewLine;
                if (img_number_day.unit != null && img_number_day.unit.Length > 0)
                {
                    string day_unit = "'" + img_number_day.unit + ".png'";
                    options += TabInString(7) + "day_unit_sc: " + day_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "day_unit_tc: " + day_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "day_unit_en: " + day_unit + "," + Environment.NewLine;
                }
                options += TabInString(7) + "day_align: hmUI.align." + img_number_day.align.ToUpper() + "," + Environment.NewLine;
                options += TabInString(7) + "day_is_character: false," + Environment.NewLine;

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_NUMBER_Month_Options(hmUI_widget_IMG_NUMBER img_number_month, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number_month == null) return options;
            if (img_number_month.img_First == null) return options;
            string img = img_number_month.img_First;
            int count = 10;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + count > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string month_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    month_array += file_name;
                    if (i < imgPosition + count - 1) month_array += ",";
                }
                month_array += "]";
                options += TabInString(7) + "month_startX: " + img_number_month.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "month_startY: " + img_number_month.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "month_sc_array: " + month_array + "," + Environment.NewLine;
                options += TabInString(7) + "month_tc_array: " + month_array + "," + Environment.NewLine;
                options += TabInString(7) + "month_en_array: " + month_array + "," + Environment.NewLine;

                if (img_number_month.zero) options += TabInString(7) + "month_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "month_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "month_space: " + img_number_month.space.ToString() + "," + Environment.NewLine;
                if (img_number_month.unit != null && img_number_month.unit.Length > 0)
                {
                    string month_unit = "'" + img_number_month.unit + ".png'";
                    options += TabInString(7) + "month_unit_sc: " + month_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "month_unit_tc: " + month_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "month_unit_en: " + month_unit + "," + Environment.NewLine;
                }
                options += TabInString(7) + "month_align: hmUI.align." + img_number_month.align.ToUpper() + "," + Environment.NewLine;
                options += TabInString(7) + "month_is_character: false," + Environment.NewLine;

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_NUMBER_Year_Options(hmUI_widget_IMG_NUMBER img_number_year, string show_level)
        {
            string options = Environment.NewLine;
            if (img_number_year == null) return options;
            if (img_number_year.img_First == null) return options;
            string img = img_number_year.img_First;
            int count = 10;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + count > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string year_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    year_array += file_name;
                    if (i < imgPosition + count - 1) year_array += ",";
                }
                year_array += "]";
                options += TabInString(7) + "year_startX: " + img_number_year.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "year_startY: " + img_number_year.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "year_sc_array: " + year_array + "," + Environment.NewLine;
                options += TabInString(7) + "year_tc_array: " + year_array + "," + Environment.NewLine;
                options += TabInString(7) + "year_en_array: " + year_array + "," + Environment.NewLine;

                if (img_number_year.zero) options += TabInString(7) + "year_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "year_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "year_space: " + img_number_year.space.ToString() + "," + Environment.NewLine;
                if (img_number_year.unit != null && img_number_year.unit.Length > 0)
                {
                    string year_unit = "'" + img_number_year.unit + ".png'";
                    options += TabInString(7) + "year_unit_sc: " + year_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "year_unit_tc: " + year_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "year_unit_en: " + year_unit + "," + Environment.NewLine;
                }
                options += TabInString(7) + "year_align: hmUI.align." + img_number_year.align.ToUpper() + "," + Environment.NewLine;
                options += TabInString(7) + "year_is_character: false," + Environment.NewLine;

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_IMAGES_Month_Options(hmUI_widget_IMG_LEVEL img_month, string show_level)
        {
            string options = Environment.NewLine;
            if (img_month == null) return options;
            if (img_month.img_First == null) return options;
            string img = img_month.img_First;
            int count = 12;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + count > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string month_array = "[";
                for (int i = imgPosition; i < imgPosition + 12; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    month_array += file_name;
                    if (i < imgPosition + count - 1) month_array += ",";
                }
                month_array += "]";
                options += TabInString(7) + "month_startX: " + img_month.X.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "month_startY: " + img_month.Y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "month_sc_array: " + month_array + "," + Environment.NewLine;
                options += TabInString(7) + "month_tc_array: " + month_array + "," + Environment.NewLine;
                options += TabInString(7) + "month_en_array: " + month_array + "," + Environment.NewLine;

                options += TabInString(7) + "month_is_character: true ," + Environment.NewLine;

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_IMAGES_Week_Options(hmUI_widget_IMG_LEVEL img_week, string show_level)
        {
            string options = Environment.NewLine;
            if (img_week == null) return options;
            if (img_week.img_First == null) return options;
            string img = img_week.img_First;
            int count = 7;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + count > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string week_array = "[";
                for (int i = imgPosition; i < imgPosition + 7; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    week_array += file_name;
                    if (i < imgPosition + count-1) week_array += ",";
                }
                week_array += "]";
                options += TabInString(7) + "x: " + img_week.X.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img_week.Y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "week_en: " + week_array + "," + Environment.NewLine;
                options += TabInString(7) + "week_tc: " + week_array + "," + Environment.NewLine;
                options += TabInString(7) + "week_sc: " + week_array + "," + Environment.NewLine;

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_IMAGES_Options(hmUI_widget_IMG_LEVEL img_level, string type, string show_level)
        {
            string options = Environment.NewLine;
            if (img_level == null) return options;
            if (img_level.img_First == null) return options;
            string img = img_level.img_First;
            int count = img_level.image_length;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + count > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }
                string img_array = "[";
                for (int i = imgPosition; i < imgPosition + count; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    img_array += file_name;
                    if (i < imgPosition + count - 1) img_array += ",";
                }
                img_array += "]";
                options += TabInString(7) + "x: " + img_level.X.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img_level.Y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "image_array: " + img_array + "," + Environment.NewLine;
                options += TabInString(7) + "image_length: " + img_level.image_length.ToString() + "," + Environment.NewLine;

                if (type.Length > 0)
                {
                    options += TabInString(7) + "type: hmUI.data_type." + type + "," + Environment.NewLine;
                }

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string IMG_PROGRESS_Options(hmUI_widget_IMG_PROGRESS img_progress, string type, string show_level)
        {
            string options = Environment.NewLine;
            if (img_progress == null) return options;
            if (img_progress.img_First == null) return options;
            if (img_progress.image_length == 0) return options;
            string img = img_progress.img_First;
            int count = img_progress.image_length;
            if (count != img_progress.X.Count || count != img_progress.Y.Count) return options;
            if (img.Length > 0)
            {
                int imgPosition = ListImages.IndexOf(img);
                if (imgPosition + count > ListImages.Count)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageCount_Error, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return options;
                }

                string img_array = "[";
                for (int i = imgPosition; i < imgPosition + count; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    img_array += file_name;
                    if (i < imgPosition + count - 1) img_array += ",";
                }
                img_array += "]";

                string x_array = "[";
                string y_array = "[";
                for (int i = 0; i < count; i++)
                {
                    x_array += img_progress.X[i].ToString();
                    y_array += img_progress.Y[i].ToString();
                    if (i < count - 1)
                    {
                        x_array += ",";
                        y_array += ",";
                    }
                }
                x_array += "]";
                y_array += "]";


                options += TabInString(7) + "x: " + x_array + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + y_array + "," + Environment.NewLine;
                options += TabInString(7) + "image_array: " + img_array + "," + Environment.NewLine;
                options += TabInString(7) + "image_length: " + img_progress.image_length.ToString() + "," + Environment.NewLine;

                if (type.Length > 0)
                {
                    options += TabInString(7) + "type: hmUI.data_type." + type + "," + Environment.NewLine;
                }

                if (show_level.Length > 0)
                {
                    options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                }
            }
            return options;
        }

        private string Circle_Scale_Options(Circle_Scale circle_scale, string optionNameStart, string type, string show_level)
        {
            string options = Environment.NewLine;
            options += TabInString(6) + "// " + optionNameStart + type.ToLower() + 
                "_circle_scale = hmUI.createWidget(hmUI.widget.Circle_Scale, {" + Environment.NewLine;
            options += TabInString(7) + "// center_x: " + circle_scale.center_x.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// center_y: " + circle_scale.center_y.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// start_angle: " + circle_scale.start_angle.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// end_angle: " + circle_scale.end_angle.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// radius: " + circle_scale.radius.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// line_width: " + circle_scale.line_width.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// color: " + circle_scale.color + "," + Environment.NewLine;
            options += TabInString(7) + "// mirror: " + circle_scale.mirror.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// inversion: " + circle_scale.inversion.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// type: hmUI.data_type." + type.ToUpper() + "," + Environment.NewLine; 
            options += TabInString(7) + "// show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
            options += TabInString(6) + "// });" + Environment.NewLine;

            return options;
        }

        private string Linear_Scale_Options(Linear_Scale linear_scale, string optionNameStart, string type, 
            string typeCaption, string show_level)
        {
            string options = Environment.NewLine;
            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
            {
                options += TabInString(6) + "" + optionNameStart + typeCaption.ToLower() +
                "_linear_scale_pointer_img = hmUI.createWidget(hmUI.widget.IMG);" + Environment.NewLine;
                if (linear_scale.mirror)
                {
                    options += TabInString(6) + "" + optionNameStart + typeCaption.ToLower() +
                "_linear_scale_pointer_img_mirror = hmUI.createWidget(hmUI.widget.IMG);" + Environment.NewLine;
                }
            }
            options += TabInString(6) + "// " + optionNameStart + typeCaption.ToLower() +
                "_linear_scale = hmUI.createWidget(hmUI.widget.Linear_Scale, {" + Environment.NewLine;
            options += TabInString(7) + "// start_x: " + linear_scale.start_x.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// start_y: " + linear_scale.start_y.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// color: " + linear_scale.color + "," + Environment.NewLine;
            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                options += TabInString(7) + "// pointer: '" + linear_scale.pointer + ".png'," + Environment.NewLine;
            options += TabInString(7) + "// lenght: " + linear_scale.lenght.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// line_width: " + linear_scale.line_width.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// vertical: " + linear_scale.vertical.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// mirror: " + linear_scale.mirror.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// inversion: " + linear_scale.inversion.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "// type: hmUI.data_type." + type.ToUpper() + "," + Environment.NewLine;
            options += TabInString(7) + "// show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
            options += TabInString(6) + "// });" + Environment.NewLine;

            return options;
        }

        private string Circle_Scale_WidgetDelegate_Options(Circle_Scale circle_scale, string optionNameStart, string type)
        {
            string options = Environment.NewLine;
            options += TabInString(9) + "// " + optionNameStart + type + "_circle_scale" + Environment.NewLine;

            // исходные параметры
            int start_angle = circle_scale.start_angle - 90;
            int end_angle = circle_scale.end_angle - 90;
            if (circle_scale.inversion)
            {
                start_angle = circle_scale.end_angle - 90;
                end_angle = circle_scale.start_angle - 90;
            }
            int center_x = circle_scale.center_x;
            int center_y = circle_scale.center_y;
            int radius = circle_scale.radius;
            int line_width = circle_scale.line_width;
            string color = circle_scale.color;

            options += TabInString(9) + "// initial parameters" + Environment.NewLine;
            options += TabInString(9) + "let start_angle_" + optionNameStart + type + " = " +
                start_angle.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let end_angle_" + optionNameStart + type + " = " +
                end_angle.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let center_x_" + optionNameStart + type + " = " +
                center_x.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let center_y_" + optionNameStart + type + " = " +
                center_y.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let radius_" + optionNameStart + type + " = " +
                radius.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let line_width_cs_" + optionNameStart + type + " = " +
                line_width.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let color_cs_" + optionNameStart + type + " = " +
                color + ";" + Environment.NewLine;

            // расчетные параметры
            options += TabInString(9) + Environment.NewLine;
            options += TabInString(9) + "// calculated parameters" + Environment.NewLine;
            options += TabInString(9) + "let arcX_" + optionNameStart + type + " = center_x_" + optionNameStart + 
                type + " - radius_" + optionNameStart + type + ";" + Environment.NewLine;
            options += TabInString(9) + "let arcY_" + optionNameStart + type + " = center_y_" + optionNameStart +
                type + " - radius_" + optionNameStart + type + ";" + Environment.NewLine;
            options += TabInString(9) + "let CircleWidth_" + optionNameStart + type + " = 2 * radius_" + 
                optionNameStart + type + ";" + Environment.NewLine;

            options += TabInString(9) + "let angle_offset_" + optionNameStart + type + " = end_angle_" + optionNameStart + 
                type + " - start_angle_" + optionNameStart + type + ";" + Environment.NewLine;
            options += TabInString(9) + "angle_offset_" + optionNameStart + type + " = angle_offset_" + optionNameStart + 
                type + " * progress_cs_" + optionNameStart + type + ";" + Environment.NewLine;
            options += TabInString(9) + "let end_angle_" + optionNameStart + type + "_draw = start_angle_" + optionNameStart + 
                type + " + angle_offset_" + optionNameStart + type + ";" + Environment.NewLine;
            options += TabInString(9) + Environment.NewLine;
            options += TabInString(9) + optionNameStart + type + "_circle_scale.setProperty(hmUI.prop.MORE, {" + Environment.NewLine;
            options += TabInString(10) + "x: arcX_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(10) + "y: arcY_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(10) + "w: CircleWidth_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(10) + "h: CircleWidth_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(10) + "start_angle: start_angle_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(10) + "end_angle: end_angle_" + optionNameStart + type + "_draw," + Environment.NewLine;
            options += TabInString(10) + "color: color_cs_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(10) + "line_width: line_width_cs_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(9) + "});" + Environment.NewLine;

            if (circle_scale.mirror)
            {
                options += TabInString(9) + Environment.NewLine;
                options += TabInString(9) + "// " + optionNameStart + type + "_circle_scale_mirror" + Environment.NewLine;

                // исходные параметры
                start_angle = circle_scale.start_angle - 90;
                end_angle = circle_scale.end_angle - 90;
                int anglOffset = end_angle - start_angle;
                end_angle = start_angle - anglOffset;
                if (circle_scale.inversion)
                {
                    int temp = start_angle;
                    start_angle = end_angle;
                    end_angle = temp;
                }

                options += TabInString(9) + "// initial parameters mirror" + Environment.NewLine;
                options += TabInString(9) + "let start_angle_" + optionNameStart + type + "_mirror = " +
                    start_angle.ToString() + ";" + Environment.NewLine;
                options += TabInString(9) + "let end_angle_" + optionNameStart + type + "_mirror = " +
                    end_angle.ToString() + ";" + Environment.NewLine;

                // расчетные параметры
                options += TabInString(9) + Environment.NewLine;
                options += TabInString(9) + "// calculated parameters mirror" + Environment.NewLine;

                options += TabInString(9) + "let angle_offset_" + optionNameStart + type + "_mirror = end_angle_" + 
                    optionNameStart + type + "_mirror - start_angle_" + optionNameStart + type + "_mirror;" + 
                    Environment.NewLine;
                options += TabInString(9) + "angle_offset_" + optionNameStart + type + "_mirror = angle_offset_" + 
                    optionNameStart + type + "_mirror * progress_cs_" + optionNameStart + type + ";" + Environment.NewLine;
                options += TabInString(9) + "let end_angle_" + optionNameStart + type + "_draw_mirror = start_angle_" + 
                    optionNameStart + type + "_mirror + angle_offset_" + optionNameStart + type + "_mirror;" + 
                    Environment.NewLine;
                options += TabInString(9) + Environment.NewLine;
                options += TabInString(9) + optionNameStart + type + "_circle_scale_mirror.setProperty(hmUI.prop.MORE, {" + Environment.NewLine;
                options += TabInString(10) + "x: arcX_" + optionNameStart + type + "," + Environment.NewLine;
                options += TabInString(10) + "y: arcY_" + optionNameStart + type + "," + Environment.NewLine;
                options += TabInString(10) + "w: CircleWidth_" + optionNameStart + type + "," + Environment.NewLine;
                options += TabInString(10) + "h: CircleWidth_" + optionNameStart + type + "," + Environment.NewLine;
                options += TabInString(10) + "start_angle: start_angle_" + optionNameStart + type + "_mirror," + Environment.NewLine;
                options += TabInString(10) + "end_angle: end_angle_" + optionNameStart + type + "_draw_mirror," + Environment.NewLine;
                options += TabInString(10) + "color: color_cs_" + optionNameStart + type + "," + Environment.NewLine;
                options += TabInString(10) + "line_width: line_width_cs_" + optionNameStart + type + "," + Environment.NewLine;
                options += TabInString(9) + "});" + Environment.NewLine;
            }

            return options;
        }

        private string Linear_Scale_WidgetDelegate_Options(Linear_Scale linear_scale, string optionNameStart, string type, string show_level)
        {
            string options = Environment.NewLine;
            options += TabInString(9) + "// " + optionNameStart + type + "_linear_scale" + Environment.NewLine;

            // исходные параметры
            int start_x = linear_scale.start_x;
            int start_y = linear_scale.start_y;
            int lenght = linear_scale.lenght;
            if (linear_scale.inversion)
            {
                if (linear_scale.vertical)
                {
                    start_y = start_y + lenght;
                    lenght = -lenght;
                }
                else
                {
                    start_x = start_x + lenght;
                    lenght = -lenght;
                }
            }
            int line_width = linear_scale.line_width;
            string color = linear_scale.color;

            options += TabInString(9) + "// initial parameters" + Environment.NewLine;
            options += TabInString(9) + "let start_x_" + optionNameStart + type + " = " +
                start_x.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let start_y_" + optionNameStart + type + " = " +
                start_y.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let lenght_ls_" + optionNameStart + type + " = " +
                lenght.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let line_width_ls_" + optionNameStart + type + " = " +
                line_width.ToString() + ";" + Environment.NewLine;
            options += TabInString(9) + "let color_ls_" + optionNameStart + type + " = " +
                color + ";" + Environment.NewLine;

            // расчетные параметры
            options += TabInString(9) + Environment.NewLine;
            options += TabInString(9) + "// calculated parameters" + Environment.NewLine;
            options += TabInString(9) + "let start_x_" + optionNameStart + type +
                "_draw = start_x_" + optionNameStart + type + ";" + Environment.NewLine;
            options += TabInString(9) + "let start_y_" + optionNameStart + type +
                "_draw = start_y_" + optionNameStart + type + ";" + Environment.NewLine;
            options += TabInString(9) + "lenght_ls_" + optionNameStart + type +
                " = lenght_ls_" + optionNameStart + type + " * progress_ls_" + optionNameStart + type + ";" + Environment.NewLine;

            if (linear_scale.vertical)
            {
                options += TabInString(9) + "let lenght_ls_" + optionNameStart + type +
                "_draw = line_width_ls_" + optionNameStart + type + ";" + Environment.NewLine;
                options += TabInString(9) + "let line_width_ls_" + optionNameStart + type +
                "_draw = lenght_ls_" + optionNameStart + type + ";" + Environment.NewLine;

                options += TabInString(9) + "if (lenght_ls_" + optionNameStart + type + " < 0){" + Environment.NewLine;
                options += TabInString(10) + "line_width_ls_" + optionNameStart + type + "_draw = -lenght_ls_" + 
                    optionNameStart + type + ";" + Environment.NewLine;
                options += TabInString(10) + "start_y_" + optionNameStart + type +
                    "_draw = start_y_" + optionNameStart + type + "_draw - line_width_ls_" + 
                    optionNameStart + type + "_draw;" + Environment.NewLine;
                options += TabInString(9) + "};" + Environment.NewLine;
            }
            else
            {
                options += TabInString(9) + "let lenght_ls_" + optionNameStart + type +
                    "_draw = lenght_ls_" + optionNameStart + type + ";" + Environment.NewLine;
                options += TabInString(9) + "let line_width_ls_" + optionNameStart + type +
                    "_draw = line_width_ls_" + optionNameStart + type + ";" + Environment.NewLine;

                options += TabInString(9) + "if (lenght_ls_" + optionNameStart + type + " < 0){" + Environment.NewLine;
                options += TabInString(10) + "lenght_ls_" + optionNameStart + type +
                    "_draw = -lenght_ls_" + optionNameStart + type + ";" + Environment.NewLine;
                options += TabInString(10) + "start_x_" + optionNameStart + type + "_draw = start_x_" + 
                    optionNameStart + type + " - lenght_ls_" + optionNameStart + type + "_draw;" + Environment.NewLine;
                options += TabInString(9) + "};" + Environment.NewLine;
            }

            options += TabInString(9) + Environment.NewLine;
            options += TabInString(9) + optionNameStart + type + "_linear_scale.setProperty(hmUI.prop.MORE, {" + Environment.NewLine;
            options += TabInString(10) + "x: start_x_" + optionNameStart + type + "_draw," + Environment.NewLine;
            options += TabInString(10) + "y: start_y_" + optionNameStart + type + "_draw," + Environment.NewLine;
            options += TabInString(10) + "w: lenght_ls_" + optionNameStart + type + "_draw," + Environment.NewLine;
            options += TabInString(10) + "h: line_width_ls_" + optionNameStart + type + "_draw," + Environment.NewLine;
            options += TabInString(10) + "color: color_ls_" + optionNameStart + type + "," + Environment.NewLine;
            options += TabInString(9) + "});" + Environment.NewLine;


            if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
            {
                int pointer_index = ListImages.IndexOf(linear_scale.pointer);
                if (pointer_index >= 0)
                {
                    System.Drawing.Bitmap src = OpenFileStream(ListImagesFullName[pointer_index]);
                    int pointer_offset_x = src.Width / 2;
                    int pointer_offset_y = src.Height / 2;

                    options += TabInString(9) + Environment.NewLine;
                    options += TabInString(9) + "// pointers parameters" + Environment.NewLine;
                    options += TabInString(9) + "let pointer_offset_x_ls_" + optionNameStart + type + " = " +
                        pointer_offset_x.ToString() + ";" + Environment.NewLine;
                    options += TabInString(9) + "let pointer_offset_y_ls_" + optionNameStart + type + " = " +
                        pointer_offset_y.ToString() + ";" + Environment.NewLine;


                    options += TabInString(9) + "" + optionNameStart + type +
                        "_linear_scale_pointer_img.setProperty(hmUI.prop.MORE, {" + Environment.NewLine;
                    if (linear_scale.vertical)
                    {
                        options += TabInString(10) + "x: start_x_" + optionNameStart + type + 
                            "_draw + line_width_ls_" + optionNameStart + type + 
                            " / 2 - pointer_offset_x_ls_" + optionNameStart + type + "," + Environment.NewLine;
                        options += TabInString(10) + "y: start_y_" + optionNameStart + type +
                            " + lenght_ls_" + optionNameStart + type + " - pointer_offset_y_ls_" + 
                            optionNameStart + type + "," + Environment.NewLine;
                    }
                    else
                    {

                        options += TabInString(10) + "x: start_x_" + optionNameStart + type +
                            " + lenght_ls_" + optionNameStart + type + " - pointer_offset_x_ls_" + 
                            optionNameStart + type + "," + Environment.NewLine;
                        options += TabInString(10) + "y: start_y_" + optionNameStart + type +
                            "_draw + line_width_ls_" + optionNameStart + type + " / 2 - pointer_offset_y_ls_" + 
                            optionNameStart + type + "," + Environment.NewLine;
                    }
                    options += TabInString(10) + "src: '" + linear_scale.pointer + ".png'," + Environment.NewLine;
                    if (show_level.Length > 0)
                    {
                        options += TabInString(10) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                    }
                    options += TabInString(9) + "});" + Environment.NewLine;
                }
            }

            // зеркальная шкала
            if (linear_scale.mirror)
            {
                options += TabInString(9) + Environment.NewLine;
                options += TabInString(9) + "// " + optionNameStart + type + "_linear_scale_mirror" + Environment.NewLine;

                // исходные параметры
                start_x = linear_scale.start_x;
                start_y = linear_scale.start_y;
                lenght = - linear_scale.lenght;
                if (linear_scale.inversion)
                {
                    if (linear_scale.vertical)
                    {
                        start_y = start_y + lenght;
                        lenght = -lenght;
                    }
                    else
                    {
                        start_x = start_x + lenght;
                        lenght = -lenght;
                    }
                }
                //line_width = linear_scale.line_width;
                //color = linear_scale.color;

                options += TabInString(9) + "// initial parameters" + Environment.NewLine;
                options += TabInString(9) + "let start_x_" + optionNameStart + type + "_mirror = " +
                    start_x.ToString() + ";" + Environment.NewLine;
                options += TabInString(9) + "let start_y_" + optionNameStart + type + "_mirror = " +
                    start_y.ToString() + ";" + Environment.NewLine;
                options += TabInString(9) + "let lenght_ls_" + optionNameStart + type + "_mirror = " +
                    lenght.ToString() + ";" + Environment.NewLine;
                options += TabInString(9) + "let line_width_ls_" + optionNameStart + type + "_mirror ="  +
                    line_width.ToString() + ";" + Environment.NewLine;
                options += TabInString(9) + "let color_ls_" + optionNameStart + type + "_mirror = " +
                    color + ";" + Environment.NewLine;

                // расчетные параметры
                options += TabInString(9) + Environment.NewLine;
                options += TabInString(9) + "// calculated parameters" + Environment.NewLine;
                options += TabInString(9) + "let start_x_" + optionNameStart + type +
                    "_draw_mirror = start_x_" + optionNameStart + type + "_mirror;" + Environment.NewLine;
                options += TabInString(9) + "let start_y_" + optionNameStart + type +
                    "_draw_mirror = start_y_" + optionNameStart + type + "_mirror;" + Environment.NewLine;
                options += TabInString(9) + "lenght_ls_" + optionNameStart + type +
                    "_mirror = lenght_ls_" + optionNameStart + type + "_mirror * progress_ls_" + optionNameStart + type + ";" + Environment.NewLine;

                if (linear_scale.vertical)
                {
                    options += TabInString(9) + "let lenght_ls_" + optionNameStart + type +
                    "_draw_mirror = line_width_ls_" + optionNameStart + type + "_mirror;" + Environment.NewLine;
                    options += TabInString(9) + "let line_width_ls_" + optionNameStart + type +
                    "_draw_mirror = lenght_ls_" + optionNameStart + type + "_mirror;" + Environment.NewLine;

                    options += TabInString(9) + "if (lenght_ls_" + optionNameStart + type + "_mirror < 0){" + Environment.NewLine;
                    options += TabInString(10) + "line_width_ls_" + optionNameStart + type + "_draw_mirror = -lenght_ls_" +
                        optionNameStart + type + "_mirror;" + Environment.NewLine;
                    options += TabInString(10) + "start_y_" + optionNameStart + type +
                        "_draw_mirror = start_y_" + optionNameStart + type + "_draw - line_width_ls_" +
                        optionNameStart + type + "_draw_mirror;" + Environment.NewLine;
                    options += TabInString(9) + "};" + Environment.NewLine;
                }
                else
                {
                    options += TabInString(9) + "let lenght_ls_" + optionNameStart + type +
                        "_draw_mirror = lenght_ls_" + optionNameStart + type + "_mirror;" + Environment.NewLine;
                    options += TabInString(9) + "let line_width_ls_" + optionNameStart + type +
                        "_draw_mirror = line_width_ls_" + optionNameStart + type + "_mirror;" + Environment.NewLine;

                    options += TabInString(9) + "if (lenght_ls_" + optionNameStart + type + "_mirror < 0){" + Environment.NewLine;
                    options += TabInString(10) + "lenght_ls_" + optionNameStart + type +
                        "_draw_mirror = -lenght_ls_" + optionNameStart + type + "_mirror;" + Environment.NewLine;
                    options += TabInString(10) + "start_x_" + optionNameStart + type + "_draw_mirror = start_x_" +
                        optionNameStart + type + " - lenght_ls_" + optionNameStart + type + "_draw_mirror;" + Environment.NewLine;
                    options += TabInString(9) + "};" + Environment.NewLine;
                }

                options += TabInString(9) + Environment.NewLine;
                options += TabInString(9) + optionNameStart + type + "_linear_scale_mirror.setProperty(hmUI.prop.MORE, {" + Environment.NewLine;
                options += TabInString(10) + "x: start_x_" + optionNameStart + type + "_draw_mirror," + Environment.NewLine;
                options += TabInString(10) + "y: start_y_" + optionNameStart + type + "_draw_mirror," + Environment.NewLine;
                options += TabInString(10) + "w: lenght_ls_" + optionNameStart + type + "_draw_mirror," + Environment.NewLine;
                options += TabInString(10) + "h: line_width_ls_" + optionNameStart + type + "_draw_mirror," + Environment.NewLine;
                options += TabInString(10) + "color: color_ls_" + optionNameStart + type + "," + Environment.NewLine;
                options += TabInString(9) + "});" + Environment.NewLine;


                if (linear_scale.pointer != null && linear_scale.pointer.Length > 0)
                {
                    int pointer_index = ListImages.IndexOf(linear_scale.pointer);
                    if (pointer_index >= 0)
                    {
                        System.Drawing.Bitmap src = OpenFileStream(ListImagesFullName[pointer_index]);
                        int pointer_offset_x = src.Width / 2;
                        int pointer_offset_y = src.Height / 2;

                        options += TabInString(9) + Environment.NewLine;
                        options += TabInString(9) + "// pointers parameters" + Environment.NewLine;
                        options += TabInString(9) + "let pointer_offset_x_ls_" + optionNameStart + type + "_mirror = " +
                            pointer_offset_x.ToString() + ";" + Environment.NewLine;
                        options += TabInString(9) + "let pointer_offset_y_ls_" + optionNameStart + type + "_mirror = " +
                            pointer_offset_y.ToString() + ";" + Environment.NewLine;


                        options += TabInString(9) + "" + optionNameStart + type +
                            "_linear_scale_pointer_img_mirror.setProperty(hmUI.prop.MORE, {" + Environment.NewLine;
                        if (linear_scale.vertical)
                        {
                            options += TabInString(10) + "x: start_x_" + optionNameStart + type +
                                "_draw_mirror + line_width_ls_" + optionNameStart + type +
                                "_mirror / 2 - pointer_offset_x_ls_" + optionNameStart + type + "_mirror," + Environment.NewLine;
                            options += TabInString(10) + "y: start_y_" + optionNameStart + type +
                                "_mirror + lenght_ls_" + optionNameStart + type + "_mirror - pointer_offset_y_ls_" +
                                optionNameStart + type + "_mirror," + Environment.NewLine;
                        }
                        else
                        {

                            options += TabInString(10) + "x: start_x_" + optionNameStart + type +
                                "_mirror + lenght_ls_" + optionNameStart + type + "_mirror - pointer_offset_x_ls_" +
                                optionNameStart + type + "_mirror," + Environment.NewLine;
                            options += TabInString(10) + "y: start_y_" + optionNameStart + type +
                                "_draw_mirror + line_width_ls_" + optionNameStart + type + "_mirror / 2 - pointer_offset_y_ls_" +
                                optionNameStart + type + "_mirror," + Environment.NewLine;
                        }
                        options += TabInString(10) + "src: '" + linear_scale.pointer + ".png'," + Environment.NewLine;
                        if (show_level.Length > 0)
                        {
                            options += TabInString(10) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                        }
                        options += TabInString(9) + "});" + Environment.NewLine;
                    }
                }
            }

            return options;
        }

        private string IMG_STATUS_Options(hmUI_widget_IMG_STATUS img_status, string type, string show_level)
        {
            string options = Environment.NewLine;
            if (img_status == null) return options;
            if (img_status.src == null) return options;
            if (img_status.src.Length > 0)
            {
                options += TabInString(7) + "x: " + img_status.x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img_status.y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "src: '" + img_status.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "type: hmUI.system_status." + type + "," + Environment.NewLine;
                options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
            }
            return options;
        }

        private string IMG_CLICK_Options(hmUI_widget_IMG_CLICK img_click, string type, string show_level)
        {
            string options = Environment.NewLine;
            if (img_click == null) return options;
            if (img_click.src == null) return options;
            if (img_click.src.Length > 0)
            {
                options += TabInString(7) + "x: " + img_click.x.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "y: " + img_click.y.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "w: " + img_click.w.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "h: " + img_click.h.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "src: '" + img_click.src + ".png'," + Environment.NewLine;
                options += TabInString(7) + "type: hmUI.data_type." + type + "," + Environment.NewLine;
                options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
            }
            return options;
        }


        private string TabInString(int count)
        {
            string returnStr = "";
            returnStr = new String(' ', count * 2);
            return returnStr;
        }

        private void JSToJson(string fileHame)
        {
            Watch_Face = null;
            if (!File.Exists(fileHame)) return;
            string functionText = File.ReadAllText(fileHame);
            functionText = functionText.Replace("\r", "");
            functionText = functionText.Replace("\n", Environment.NewLine);
            string functionName = "";
            while (functionName != "init_view()" && functionText.Length > 10)
            {
                functionText = GetFunction(functionText, out functionName);
            }
            List<string> functionsList = GetFunctionsList(functionText);

            Watch_Face = new WATCH_FACE();
            Watch_Face.WatchFace_Info = new WatchFace_Info();
            Watch_Face.ScreenNormal = new ScreenNormal();
            Watch_Face.ScreenAOD = new ScreenAOD();

            foreach (string parametrString in functionsList)
            {
                Dictionary<string, string> parametrs = ParseParametrsInString(parametrString);
                if (parametrs.ContainsKey("ObjectType") && parametrs.ContainsKey("ObjectName"))
                {
                    string objectType = parametrs["ObjectType"];
                    string objectName = parametrs["ObjectName"];
                    List<object> elementsList = null;
                    switch (objectType)
                    {
                        #region IMG
                        case "IMG":
                            hmUI_widget_IMG img = Object_IMG(parametrs);
                            if (objectName.EndsWith("background_bg") || objectName.EndsWith("background_bg_img"))
                            {
                                if (img.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                                {
                                    if (Watch_Face.ScreenNormal.Background == null)
                                        Watch_Face.ScreenNormal.Background = new Background();
                                    Watch_Face.ScreenNormal.Background.BackgroundImage = img;
                                }
                                else if (img.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                                {
                                    if (Watch_Face.ScreenAOD.Background == null)
                                        Watch_Face.ScreenAOD.Background = new Background();
                                    Watch_Face.ScreenAOD.Background.BackgroundImage = img;
                                }
                            }
                            else
                            {
                                elementsList = null;
                                if (img.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                                {
                                    if (Watch_Face.ScreenNormal.Elements == null)
                                        Watch_Face.ScreenNormal.Elements = new List<object>();
                                    elementsList = Watch_Face.ScreenNormal.Elements;
                                }
                                else if (img.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                                {
                                    if (Watch_Face.ScreenAOD.Elements == null)
                                        Watch_Face.ScreenAOD.Elements = new List<object>();
                                    elementsList = Watch_Face.ScreenAOD.Elements;
                                }
                                //if (elementsList != null) elementsList.Add(img);
                            }

                            if (objectName.EndsWith("separator_img"))
                            {
                                if (objectName.EndsWith("hour_separator_img"))
                                {
                                    ElementDigitalTime digitalTime = null;
                                    digitalTime = (ElementDigitalTime)elementsList.Find(e => e.GetType().Name == "ElementDigitalTime");
                                    if (digitalTime != null && digitalTime.Hour != null)
                                    {
                                        digitalTime.Hour.icon = img.src;
                                        digitalTime.Hour.iconPosX = img.x;
                                        digitalTime.Hour.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("minute_separator_img"))
                                {
                                    ElementDigitalTime digitalTime = null;
                                    digitalTime = (ElementDigitalTime)elementsList.Find(e => e.GetType().Name == "ElementDigitalTime");
                                    if (digitalTime != null && digitalTime.Minute != null)
                                    {
                                        digitalTime.Minute.icon = img.src;
                                        digitalTime.Minute.iconPosX = img.x;
                                        digitalTime.Minute.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("second_separator_img"))
                                {
                                    ElementDigitalTime digitalTime = null;
                                    digitalTime = (ElementDigitalTime)elementsList.Find(e => e.GetType().Name == "ElementDigitalTime");
                                    if (digitalTime != null && digitalTime.Second != null)
                                    {
                                        digitalTime.Second.icon = img.src;
                                        digitalTime.Second.iconPosX = img.x;
                                        digitalTime.Second.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("day_separator_img"))
                                {
                                    ElementDateDay dateDay = null;
                                    dateDay = (ElementDateDay)elementsList.Find(e => e.GetType().Name == "ElementDateDay");
                                    if (dateDay != null && dateDay.Number != null)
                                    {
                                        dateDay.Number.icon = img.src;
                                        dateDay.Number.iconPosX = img.x;
                                        dateDay.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("month_separator_img"))
                                {
                                    ElementDateMonth dateMonth = null;
                                    dateMonth = (ElementDateMonth)elementsList.Find(e => e.GetType().Name == "ElementDateMonth");
                                    if (dateMonth != null && dateMonth.Number != null)
                                    {
                                        dateMonth.Number.icon = img.src;
                                        dateMonth.Number.iconPosX = img.x;
                                        dateMonth.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("year_separator_img"))
                                {
                                    ElementDateYear dateYear = null;
                                    dateYear = (ElementDateYear)elementsList.Find(e => e.GetType().Name == "ElementDateYear");
                                    if (dateYear != null && dateYear.Number != null)
                                    {
                                        dateYear.Number.icon = img.src;
                                        dateYear.Number.iconPosX = img.x;
                                        dateYear.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("step_current_separator_img"))
                                {
                                    ElementSteps steps = null;
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                    if (steps != null && steps.Number != null)
                                    {
                                        steps.Number.icon = img.src;
                                        steps.Number.iconPosX = img.x;
                                        steps.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("step_target_separator_img"))
                                {
                                    ElementSteps steps = null;
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                    if (steps != null && steps.Number_Target != null)
                                    {
                                        steps.Number_Target.icon = img.src;
                                        steps.Number_Target.iconPosX = img.x;
                                        steps.Number_Target.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("battery_text_separator_img"))
                                {
                                    ElementBattery battery = null;
                                    battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                    if (battery != null && battery.Number != null)
                                    {
                                        battery.Number.icon = img.src;
                                        battery.Number.iconPosX = img.x;
                                        battery.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("calorie_current_separator_img"))
                                {
                                    ElementCalories calorie = null;
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                    if (calorie != null && calorie.Number != null)
                                    {
                                        calorie.Number.icon = img.src;
                                        calorie.Number.iconPosX = img.x;
                                        calorie.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("calorie_target_separator_img"))
                                {
                                    ElementCalories calorie = null;
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                    if (calorie != null && calorie.Number_Target != null)
                                    {
                                        calorie.Number_Target.icon = img.src;
                                        calorie.Number_Target.iconPosX = img.x;
                                        calorie.Number_Target.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("heart_rate_text_separator_img"))
                                {
                                    ElementHeart heart = null;
                                    heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                    if (heart != null && heart.Number != null)
                                    {
                                        heart.Number.icon = img.src;
                                        heart.Number.iconPosX = img.x;
                                        heart.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("pai_day_separator_img"))
                                {
                                    ElementPAI pai_day = null;
                                    pai_day = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                    if (pai_day != null && pai_day.Number != null)
                                    {
                                        pai_day.Number.icon = img.src;
                                        pai_day.Number.iconPosX = img.x;
                                        pai_day.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("pai_weekly_separator_img"))
                                {
                                    ElementPAI pai_weekly = null;
                                    pai_weekly = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                    if (pai_weekly != null && pai_weekly.Number != null)
                                    {
                                        pai_weekly.Number.icon = img.src;
                                        pai_weekly.Number.iconPosX = img.x;
                                        pai_weekly.Number.iconPosY = img.y;
                                    }
                                }

                                if (objectName.EndsWith("distance_text_separator_img"))
                                {
                                    ElementDistance distance = null;
                                    distance = (ElementDistance)elementsList.Find(e => e.GetType().Name == "ElementDistance");
                                    if (distance != null && distance.Number != null)
                                    {
                                        distance.Number.icon = img.src;
                                        distance.Number.iconPosX = img.x;
                                        distance.Number.iconPosY = img.y;
                                    }
                                }
                            }

                            if (objectName.EndsWith("icon_img"))
                            {

                                if (objectName.EndsWith("step_icon_img"))
                                {
                                    ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                    if(steps == null)
                                    {
                                        elementsList.Add(new ElementSteps()); 
                                        steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                    }
                                    if (steps != null)
                                    {
                                        int offset = 1;
                                        if (steps.Images != null) offset++;
                                        if (steps.Segments != null) offset++;
                                        if (steps.Number != null) offset++;
                                        if (steps.Number_Target != null) offset++;
                                        if (steps.Pointer != null) offset++;
                                        if (steps.Circle_Scale != null) offset++;
                                        if (steps.Linear_Scale != null) offset++;

                                        steps.Icon = new hmUI_widget_IMG();
                                        steps.Icon.src = img.src;
                                        steps.Icon.x = img.x;
                                        steps.Icon.y = img.y;
                                        steps.Icon.visible = true;
                                        steps.Icon.position = offset;
                                    }
                                }
                                
                                if (objectName.EndsWith("battery_icon_img"))
                                {
                                    ElementBattery battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                    if (battery == null)
                                    {
                                        elementsList.Add(new ElementBattery());
                                        battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                    }
                                    if (battery != null)
                                    {
                                        int offset = 1;
                                        if (battery.Images != null) offset++;
                                        if (battery.Segments != null) offset++;
                                        if (battery.Number != null) offset++;
                                        if (battery.Pointer != null) offset++;
                                        if (battery.Circle_Scale != null) offset++;
                                        if (battery.Linear_Scale != null) offset++;

                                        battery.Icon = new hmUI_widget_IMG();
                                        battery.Icon.src = img.src;
                                        battery.Icon.x = img.x;
                                        battery.Icon.y = img.y;
                                        battery.Icon.visible = true;
                                        battery.Icon.position = offset;
                                    }
                                }

                                if (objectName.EndsWith("calorie_icon_img"))
                                {
                                    ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                    if (calorie == null)
                                    {
                                        elementsList.Add(new ElementCalories());
                                        calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                    }
                                    if (calorie != null)
                                    {
                                        int offset = 1;
                                        if (calorie.Images != null) offset++;
                                        if (calorie.Segments != null) offset++;
                                        if (calorie.Number != null) offset++;
                                        if (calorie.Number_Target != null) offset++;
                                        if (calorie.Pointer != null) offset++;
                                        if (calorie.Circle_Scale != null) offset++;
                                        if (calorie.Linear_Scale != null) offset++;

                                        calorie.Icon = new hmUI_widget_IMG();
                                        calorie.Icon.src = img.src;
                                        calorie.Icon.x = img.x;
                                        calorie.Icon.y = img.y;
                                        calorie.Icon.visible = true;
                                        calorie.Icon.position = offset;
                                    }
                                }

                                if (objectName.EndsWith("heart_rate_icon_img"))
                                {
                                    ElementHeart heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                    if (heart == null)
                                    {
                                        elementsList.Add(new ElementHeart());
                                        heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                    }
                                    if (heart != null)
                                    {
                                        int offset = 1;
                                        if (heart.Images != null) offset++;
                                        if (heart.Segments != null) offset++;
                                        if (heart.Number != null) offset++;
                                        if (heart.Pointer != null) offset++;
                                        if (heart.Circle_Scale != null) offset++;
                                        if (heart.Linear_Scale != null) offset++;

                                        heart.Icon = new hmUI_widget_IMG();
                                        heart.Icon.src = img.src;
                                        heart.Icon.x = img.x;
                                        heart.Icon.y = img.y;
                                        heart.Icon.visible = true;
                                        heart.Icon.position = offset;
                                    }
                                }

                                if (objectName.EndsWith("pai_icon_img"))
                                {
                                    ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                    if (pai == null)
                                    {
                                        elementsList.Add(new ElementPAI());
                                        pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                    }
                                    if (pai != null)
                                    {
                                        int offset = 1;
                                        if (pai.Images != null) offset++;
                                        if (pai.Segments != null) offset++;
                                        if (pai.Number != null) offset++;
                                        if (pai.Number_Target != null) offset++;
                                        if (pai.Pointer != null) offset++;
                                        if (pai.Circle_Scale != null) offset++;
                                        if (pai.Linear_Scale != null) offset++;

                                        pai.Icon = new hmUI_widget_IMG();
                                        pai.Icon.src = img.src;
                                        pai.Icon.x = img.x;
                                        pai.Icon.y = img.y;
                                        pai.Icon.visible = true;
                                        pai.Icon.position = offset;
                                    }
                                }
                            }


                        break;
                        #endregion

                        #region FILL_RECT
                        case "FILL_RECT":
                            hmUI_widget_FILL_RECT fill_rect = Object_FILL_RECT(parametrs);
                            if (objectName.IndexOf("background") >= 0)
                            {
                                if (fill_rect.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                                {
                                    if (Watch_Face.ScreenNormal.Background == null)
                                        Watch_Face.ScreenNormal.Background = new Background();
                                    Watch_Face.ScreenNormal.Background.BackgroundColor = fill_rect;
                                }
                                else if (fill_rect.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                                {
                                    if (Watch_Face.ScreenAOD.Background == null)
                                        Watch_Face.ScreenAOD.Background = new Background();
                                    Watch_Face.ScreenAOD.Background.BackgroundColor = fill_rect;
                                }
                            }

                            break;
                        #endregion

                        #region IMG_TIME
                        case "IMG_TIME":
                            ElementDigitalTime img_time = Object_DigitalTime(parametrs);
                            elementsList = null;
                            if (objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }
                            if (elementsList != null)
                            {
                                ElementDigitalTime digitalTime = (ElementDigitalTime)elementsList.Find(e => e.GetType().Name == "ElementDigitalTime");
                                if(digitalTime == null) elementsList.Add(img_time);
                                else
                                {
                                    int offset = 0;
                                    if (digitalTime.Hour != null) offset++;
                                    if (digitalTime.Minute != null) offset++;
                                    if (digitalTime.Second != null) offset++;
                                    if (digitalTime.AmPm != null) offset++;

                                    if(img_time.Hour != null)
                                    {
                                        img_time.Hour.position = img_time.Hour.position + offset;
                                        digitalTime.Hour = img_time.Hour;
                                    }
                                    if (img_time.Minute != null)
                                    {
                                        img_time.Minute.position = img_time.Minute.position + offset;
                                        digitalTime.Minute = img_time.Minute;
                                    }
                                    if (img_time.Second != null)
                                    {
                                        img_time.Second.position = img_time.Second.position + offset;
                                        digitalTime.Second = img_time.Second;
                                    }
                                    if (img_time.AmPm != null)
                                    {
                                        img_time.AmPm.position = img_time.AmPm.position + offset;
                                        digitalTime.AmPm = img_time.AmPm;
                                    }
                                }
                            }


                            break;
                        #endregion

                        #region TIME_POINTER
                        case "TIME_POINTER":
                            ElementAnalogTime pointer_time = Object_AnalogTime(parametrs);
                            elementsList = null;
                            if (objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }
                            if (elementsList != null)
                            {
                                ElementAnalogTime analogTime = (ElementAnalogTime)elementsList.Find(e => e.GetType().Name == "ElementAnalogTime");
                                if (analogTime == null) elementsList.Add(pointer_time);
                                else
                                {
                                    int offset = 0;
                                    if (analogTime.Hour != null) offset++;
                                    if (analogTime.Minute != null) offset++;
                                    if (analogTime.Second != null) offset++;

                                    if (pointer_time.Hour != null)
                                    {
                                        pointer_time.Hour.position = pointer_time.Hour.position + offset;
                                        analogTime.Hour = pointer_time.Hour;
                                    }
                                    if (pointer_time.Minute != null)
                                    {
                                        pointer_time.Minute.position = pointer_time.Minute.position + offset;
                                        analogTime.Minute = pointer_time.Minute;
                                    }
                                    if (pointer_time.Second != null)
                                    {
                                        pointer_time.Second.position = pointer_time.Second.position + offset;
                                        analogTime.Second = pointer_time.Second;
                                    }
                                }
                            }


                            break;
                        #endregion

                        #region IMG_DATE
                        case "IMG_DATE":
                            List<hmUI_widget_IMG_NUMBER> img_number_list = Object_ImgDate(parametrs);
                            List<hmUI_widget_IMG_LEVEL> img_level_list = Object_ImgLevelDate(parametrs);
                            elementsList = null;
                            if ((img_number_list.Count > 0 && img_number_list[0].show_level == "ONLY_NORMAL") || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if ((img_number_list.Count > 0 && img_number_list[0].show_level == "ONAL_AOD") || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            if (elementsList != null && img_number_list != null)
                            {
                                foreach(hmUI_widget_IMG_NUMBER img_number in img_number_list)
                                {
                                    if(img_number.type == "DAY")
                                    {
                                        int offsetNumber = 1;
                                        ElementDateDay dateDay = (ElementDateDay)elementsList.Find(e => e.GetType().Name == "ElementDateDay");
                                        if (dateDay == null) elementsList.Add(new ElementDateDay());
                                        dateDay = (ElementDateDay)elementsList.Find(e => e.GetType().Name == "ElementDateDay");
                                        if (dateDay != null)
                                        {
                                            if (dateDay.Pointer != null) offsetNumber++;
                                            dateDay.Number = img_number;
                                            dateDay.Number.position = offsetNumber;
                                            dateDay.Number.visible = true;
                                            //dateDay.Pointer = new hmUI_widget_IMG_POINTER();
                                            //newPointer = dateDay.Pointer;
                                        }
                                    }

                                    if (img_number.type == "MONTH")
                                    {
                                        int offsetNumber = 1;
                                        ElementDateMonth dateMonth = (ElementDateMonth)elementsList.Find(e => e.GetType().Name == "ElementDateMonth");
                                        if (dateMonth == null) elementsList.Add(new ElementDateMonth());
                                        dateMonth = (ElementDateMonth)elementsList.Find(e => e.GetType().Name == "ElementDateMonth");
                                        if (dateMonth != null)
                                        {
                                            if (dateMonth.Pointer != null) offsetNumber++;
                                            if (dateMonth.Images != null) offsetNumber++;
                                            dateMonth.Number = img_number;
                                            dateMonth.Number.position = offsetNumber;
                                            dateMonth.Number.visible = true;
                                            //dateDay.Pointer = new hmUI_widget_IMG_POINTER();
                                            //newPointer = dateDay.Pointer;
                                        }
                                    }

                                    if (img_number.type == "YEAR")
                                    {
                                        ElementDateYear dateYear = (ElementDateYear)elementsList.Find(e => e.GetType().Name == "ElementDateYear");
                                        if (dateYear == null) elementsList.Add(new ElementDateYear());
                                        dateYear = (ElementDateYear)elementsList.Find(e => e.GetType().Name == "ElementDateYear");
                                        if (dateYear != null)
                                        {
                                            dateYear.Number = img_number;
                                            dateYear.Number.visible = true;
                                        }
                                    }
                                }

                            }

                            if (elementsList != null && img_level_list != null)
                            {
                                foreach (hmUI_widget_IMG_LEVEL img_level in img_level_list)
                                {
                                    if (img_level.type == "MONTH")
                                    {
                                        int offsetNumber = 1;
                                        ElementDateMonth dateMonth = (ElementDateMonth)elementsList.Find(e => e.GetType().Name == "ElementDateMonth");
                                        if (dateMonth == null) elementsList.Add(new ElementDateMonth());
                                        dateMonth = (ElementDateMonth)elementsList.Find(e => e.GetType().Name == "ElementDateMonth");
                                        if (dateMonth != null)
                                        {
                                            if (dateMonth.Pointer != null) offsetNumber++;
                                            if (dateMonth.Number != null) offsetNumber++;
                                            dateMonth.Images = img_level;
                                            dateMonth.Images.position = offsetNumber;
                                            dateMonth.Images.visible = true;
                                            //dateDay.Pointer = new hmUI_widget_IMG_POINTER();
                                            //newPointer = dateDay.Pointer;
                                        }
                                    }
                                }

                            }


                            break;
                        #endregion

                        #region DATE_POINTER
                        case "DATE_POINTER":
                            hmUI_widget_IMG_POINTER img_pointer = Object_DATE_POINTER(parametrs);
                            elementsList = null;
                            if (img_pointer.show_level == "ONLY_NORMAL" || img_pointer.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (img_pointer.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            //int offsetPointer = 1;
                            //hmUI_widget_IMG_POINTER newPointer = null;
                            if (elementsList != null && img_pointer.type == "DAY")
                            {
                                int offsetPointer = 1;
                                ElementDateDay dateDay = (ElementDateDay)elementsList.Find(e => e.GetType().Name == "ElementDateDay");
                                if (dateDay == null) elementsList.Add(new ElementDateDay());
                                dateDay = (ElementDateDay)elementsList.Find(e => e.GetType().Name == "ElementDateDay");
                                if (dateDay != null)
                                {
                                    if (dateDay.Number != null) offsetPointer++;
                                    dateDay.Pointer = img_pointer;
                                    dateDay.Pointer.position = offsetPointer;
                                    dateDay.Pointer.visible = true;
                                    //dateDay.Pointer = new hmUI_widget_IMG_POINTER();
                                    //newPointer = dateDay.Pointer;
                                }
                            }
                            if (elementsList != null && img_pointer.type == "MONTH")
                            {
                                int offsetPointer = 1;
                                ElementDateMonth dateMonth = (ElementDateMonth)elementsList.Find(e => e.GetType().Name == "ElementDateMonth");
                                if (dateMonth == null) elementsList.Add(new ElementDateMonth());
                                dateMonth = (ElementDateMonth)elementsList.Find(e => e.GetType().Name == "ElementDateMonth");
                                if (dateMonth != null)
                                {
                                    if (dateMonth.Number != null) offsetPointer++;
                                    if (dateMonth.Images != null) offsetPointer++;
                                    dateMonth.Pointer = img_pointer;
                                    dateMonth.Pointer.position = offsetPointer;
                                    dateMonth.Pointer.visible = true;
                                    //dateMonth.Pointer = new hmUI_widget_IMG_POINTER();
                                    //newPointer = dateMonth.Pointer;
                                }
                            }
                            if (elementsList != null && img_pointer.type == "WEEK")
                            {
                                int offsetPointer = 1;
                                ElementDateWeek dateWeek = (ElementDateWeek)elementsList.Find(e => e.GetType().Name == "ElementDateWeek");
                                if (dateWeek == null) elementsList.Add(new ElementDateWeek());
                                dateWeek = (ElementDateWeek)elementsList.Find(e => e.GetType().Name == "ElementDateWeek");
                                if (dateWeek != null)
                                {
                                    if (dateWeek.Images != null) offsetPointer++;
                                    dateWeek.Pointer = img_pointer;
                                    dateWeek.Pointer.position = offsetPointer;
                                    dateWeek.Pointer.visible = true;
                                    //dateWeek.Pointer = new hmUI_widget_IMG_POINTER();
                                    //newPointer = dateWeek.Pointer;
                                }
                            }
                            break;
                        #endregion

                        #region IMG_WEEK
                        case "IMG_WEEK":
                            hmUI_widget_IMG_LEVEL imgWeek = Object_IMG_WEEK(parametrs);
                            elementsList = null;
                            if (imgWeek.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (imgWeek.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }
                            if (elementsList != null)
                            {
                                ElementDateWeek dateWeek = new ElementDateWeek();
                                dateWeek.Images = imgWeek;
                                elementsList.Add(dateWeek);
                            }
                            else
                            {
                                ElementDateWeek dateWeek = null;
                                dateWeek = (ElementDateWeek)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                                if (dateWeek != null)
                                {
                                    dateWeek.Images = imgWeek;
                                }
                            }

                            break;
                        #endregion

                        #region IMG_LEVEL
                        case "IMG_LEVEL":
                            hmUI_widget_IMG_LEVEL imgLevel = Object_IMG_LEVEL(parametrs);
                            elementsList = null;
                            if (imgLevel.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (imgLevel.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            if (elementsList != null && imgLevel.type == "STEP")
                            {
                                ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                if (steps == null)
                                {
                                    elementsList.Add(new ElementSteps());
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                }
                                if (steps != null)
                                {
                                    int offset = 1;
                                    //if (steps.Images != null) offset++;
                                    if (steps.Segments != null) offset++;
                                    if (steps.Number != null) offset++;
                                    if (steps.Number_Target != null) offset++;
                                    if (steps.Pointer != null) offset++;
                                    if (steps.Circle_Scale != null) offset++;
                                    if (steps.Linear_Scale != null) offset++;
                                    if (steps.Icon != null) offset++;

                                    steps.Images = new hmUI_widget_IMG_LEVEL();
                                    steps.Images.img_First = imgLevel.img_First;
                                    steps.Images.image_length = imgLevel.image_length;
                                    steps.Images.X = imgLevel.X;
                                    steps.Images.Y = imgLevel.Y;
                                    steps.Images.visible = true;
                                    steps.Images.position = offset;
                                }
                            }

                            if (elementsList != null && imgLevel.type == "BATTERY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementBattery battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                if (battery == null)
                                {
                                    elementsList.Add(new ElementBattery());
                                    battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                }
                                if (battery != null)
                                {
                                    int offset = 1;
                                    //if (steps.Images != null) offset++;
                                    if (battery.Segments != null) offset++;
                                    if (battery.Number != null) offset++;
                                    if (battery.Pointer != null) offset++;
                                    if (battery.Circle_Scale != null) offset++;
                                    if (battery.Linear_Scale != null) offset++;
                                    if (battery.Icon != null) offset++;

                                    battery.Images = new hmUI_widget_IMG_LEVEL();
                                    battery.Images.img_First = imgLevel.img_First;
                                    battery.Images.image_length = imgLevel.image_length;
                                    battery.Images.X = imgLevel.X;
                                    battery.Images.Y = imgLevel.Y;
                                    battery.Images.visible = true;
                                    battery.Images.position = offset;
                                }
                            }

                            if (elementsList != null && imgLevel.type == "CAL")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                if (calorie == null)
                                {
                                    elementsList.Add(new ElementCalories());
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                }
                                if (calorie != null)
                                {
                                    int offset = 1;
                                    //if (steps.Images != null) offset++;
                                    if (calorie.Segments != null) offset++;
                                    if (calorie.Number != null) offset++;
                                    if (calorie.Number_Target != null) offset++;
                                    if (calorie.Pointer != null) offset++;
                                    if (calorie.Circle_Scale != null) offset++;
                                    if (calorie.Linear_Scale != null) offset++;
                                    if (calorie.Icon != null) offset++;

                                    calorie.Images = new hmUI_widget_IMG_LEVEL();
                                    calorie.Images.img_First = imgLevel.img_First;
                                    calorie.Images.image_length = imgLevel.image_length;
                                    calorie.Images.X = imgLevel.X;
                                    calorie.Images.Y = imgLevel.Y;
                                    calorie.Images.visible = true;
                                    calorie.Images.position = offset;
                                }
                            }

                            if (elementsList != null && imgLevel.type == "HEART")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementHeart heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                if (heart == null)
                                {
                                    elementsList.Add(new ElementHeart());
                                    heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                }
                                if (heart != null)
                                {
                                    int offset = 1;
                                    //if (steps.Images != null) offset++;
                                    if (heart.Segments != null) offset++;
                                    if (heart.Number != null) offset++;
                                    if (heart.Pointer != null) offset++;
                                    if (heart.Circle_Scale != null) offset++;
                                    if (heart.Linear_Scale != null) offset++;
                                    if (heart.Icon != null) offset++;

                                    heart.Images = new hmUI_widget_IMG_LEVEL();
                                    heart.Images.img_First = imgLevel.img_First;
                                    heart.Images.image_length = imgLevel.image_length;
                                    heart.Images.image_length = 6;
                                    heart.Images.X = imgLevel.X;
                                    heart.Images.Y = imgLevel.Y;
                                    heart.Images.visible = true;
                                    heart.Images.position = offset;
                                    if(imgLevel.image_length!=6)
                                        MessageBox.Show("Количество изображений для отображения пульса должно быть равным 6.", 
                                            Properties.FormStrings.Message_Warning_Caption,MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            if (elementsList != null && imgLevel.type == "PAI_WEEKLY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                if (pai == null)
                                {
                                    elementsList.Add(new ElementPAI());
                                    pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                }
                                if (pai != null)
                                {
                                    int offset = 1;
                                    //if (steps.Images != null) offset++;
                                    if (pai.Segments != null) offset++;
                                    if (pai.Number != null) offset++;
                                    if (pai.Number_Target != null) offset++;
                                    if (pai.Pointer != null) offset++;
                                    if (pai.Circle_Scale != null) offset++;
                                    if (pai.Linear_Scale != null) offset++;
                                    if (pai.Icon != null) offset++;

                                    pai.Images = new hmUI_widget_IMG_LEVEL();
                                    pai.Images.img_First = imgLevel.img_First;
                                    pai.Images.image_length = imgLevel.image_length;
                                    pai.Images.X = imgLevel.X;
                                    pai.Images.Y = imgLevel.Y;
                                    pai.Images.visible = true;
                                    pai.Images.position = offset;
                                }
                            }



                            break;
                        #endregion

                        #region IMG_PROGRESS
                        case "IMG_PROGRESS":
                            hmUI_widget_IMG_PROGRESS imgProgress = Object_IMG_PROGRESS(parametrs);
                            elementsList = null;
                            if (imgProgress.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (imgProgress.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            if (elementsList != null && imgProgress.type == "STEP")
                            {
                                ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                if (steps == null)
                                {
                                    elementsList.Add(new ElementSteps());
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                }
                                if (steps != null)
                                {
                                    int offset = 1;
                                    if (steps.Images != null) offset++;
                                    //if (steps.Segments != null) offset++;
                                    if (steps.Number != null) offset++;
                                    if (steps.Number_Target != null) offset++;
                                    if (steps.Pointer != null) offset++;
                                    if (steps.Circle_Scale != null) offset++;
                                    if (steps.Linear_Scale != null) offset++;
                                    if (steps.Icon != null) offset++;

                                    steps.Segments = new hmUI_widget_IMG_PROGRESS();
                                    steps.Segments.img_First = imgProgress.img_First;
                                    steps.Segments.image_length = imgProgress.image_length;
                                    steps.Segments.X = imgProgress.X;
                                    steps.Segments.Y = imgProgress.Y;
                                    steps.Segments.visible = true;
                                    steps.Segments.position = offset;
                                }
                            }

                            if (elementsList != null && imgProgress.type == "BATTERY")
                            {
                                ElementBattery battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                if (battery == null)
                                {
                                    elementsList.Add(new ElementBattery());
                                    battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                }
                                if (battery != null)
                                {
                                    int offset = 1;
                                    if (battery.Images != null) offset++;
                                    //if (steps.Segments != null) offset++;
                                    if (battery.Number != null) offset++;
                                    if (battery.Pointer != null) offset++;
                                    if (battery.Circle_Scale != null) offset++;
                                    if (battery.Linear_Scale != null) offset++;
                                    if (battery.Icon != null) offset++;

                                    battery.Segments = new hmUI_widget_IMG_PROGRESS();
                                    battery.Segments.img_First = imgProgress.img_First;
                                    battery.Segments.image_length = imgProgress.image_length;
                                    battery.Segments.X = imgProgress.X;
                                    battery.Segments.Y = imgProgress.Y;
                                    battery.Segments.visible = true;
                                    battery.Segments.position = offset;
                                }
                            }

                            if (elementsList != null && imgProgress.type == "CAL")
                            {
                                ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                if (calorie == null)
                                {
                                    elementsList.Add(new ElementCalories());
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                }
                                if (calorie != null)
                                {
                                    int offset = 1;
                                    if (calorie.Images != null) offset++;
                                    //if (steps.Segments != null) offset++;
                                    if (calorie.Number != null) offset++;
                                    if (calorie.Number_Target != null) offset++;
                                    if (calorie.Pointer != null) offset++;
                                    if (calorie.Circle_Scale != null) offset++;
                                    if (calorie.Linear_Scale != null) offset++;
                                    if (calorie.Icon != null) offset++;

                                    calorie.Segments = new hmUI_widget_IMG_PROGRESS();
                                    calorie.Segments.img_First = imgProgress.img_First;
                                    calorie.Segments.image_length = imgProgress.image_length;
                                    calorie.Segments.X = imgProgress.X;
                                    calorie.Segments.Y = imgProgress.Y;
                                    calorie.Segments.visible = true;
                                    calorie.Segments.position = offset;
                                }
                            }

                            if (elementsList != null && imgProgress.type == "HEART")
                            {
                                ElementHeart heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                if (heart == null)
                                {
                                    elementsList.Add(new ElementHeart());
                                    heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                }
                                if (heart != null)
                                {
                                    int offset = 1;
                                    if (heart.Images != null) offset++;
                                    //if (steps.Segments != null) offset++;
                                    if (heart.Number != null) offset++;
                                    if (heart.Pointer != null) offset++;
                                    if (heart.Circle_Scale != null) offset++;
                                    if (heart.Linear_Scale != null) offset++;
                                    if (heart.Icon != null) offset++;

                                    heart.Segments = new hmUI_widget_IMG_PROGRESS();
                                    heart.Segments.img_First = imgProgress.img_First;
                                    //heart.Segments.image_length = imgProgress.image_length;
                                    heart.Segments.image_length = 6;
                                    heart.Segments.X = imgProgress.X;
                                    heart.Segments.Y = imgProgress.Y;
                                    heart.Segments.visible = true;
                                    heart.Segments.position = offset;
                                    if (imgProgress.image_length != 6)
                                        MessageBox.Show("Количество изображений для отображения пульса должно быть равным 6.",
                                            Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            if (elementsList != null && imgProgress.type == "PAI_WEEKLY")
                            {
                                ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                if (pai == null)
                                {
                                    elementsList.Add(new ElementPAI());
                                    pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                }
                                if (pai != null)
                                {
                                    int offset = 1;
                                    if (pai.Images != null) offset++;
                                    //if (steps.Segments != null) offset++;
                                    if (pai.Number != null) offset++;
                                    if (pai.Number_Target != null) offset++;
                                    if (pai.Pointer != null) offset++;
                                    if (pai.Circle_Scale != null) offset++;
                                    if (pai.Linear_Scale != null) offset++;
                                    if (pai.Icon != null) offset++;

                                    pai.Segments = new hmUI_widget_IMG_PROGRESS();
                                    pai.Segments.img_First = imgProgress.img_First;
                                    pai.Segments.image_length = imgProgress.image_length;
                                    pai.Segments.X = imgProgress.X;
                                    pai.Segments.Y = imgProgress.Y;
                                    pai.Segments.visible = true;
                                    pai.Segments.position = offset;
                                }
                            }



                            break;
                        #endregion

                        #region IMG_NUMBER
                        case "TEXT_IMG":
                            hmUI_widget_IMG_NUMBER imgNumber = Object_IMG_NUMBER(parametrs);
                            elementsList = null;
                            if (imgNumber.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (imgNumber.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            if (elementsList != null && imgNumber.type == "STEP")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                if (steps == null)
                                {
                                    elementsList.Add(new ElementSteps());
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                }
                                if (steps != null)
                                {
                                    int offset = 1;
                                    if (steps.Images != null) offset++;
                                    if (steps.Segments != null) offset++;
                                    //if (steps.Number != null) offset++;
                                    if (steps.Number_Target != null) offset++;
                                    if (steps.Pointer != null) offset++;
                                    if (steps.Circle_Scale != null) offset++;
                                    if (steps.Linear_Scale != null) offset++;
                                    if (steps.Icon != null) offset++;

                                    steps.Number = new hmUI_widget_IMG_NUMBER();
                                    steps.Number.img_First = imgNumber.img_First;
                                    steps.Number.imageX = imgNumber.imageX;
                                    steps.Number.imageY = imgNumber.imageY;
                                    steps.Number.space = imgNumber.space;
                                    steps.Number.zero = imgNumber.zero;
                                    steps.Number.unit = imgNumber.unit;
                                    steps.Number.imperial_unit = imgNumber.imperial_unit;
                                    steps.Number.negative_image = imgNumber.negative_image;
                                    steps.Number.invalid_image = imgNumber.invalid_image;
                                    steps.Number.dot_image = imgNumber.dot_image;
                                    steps.Number.align = imgNumber.align;
                                    steps.Number.visible = true;
                                    steps.Number.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "STEP_TARGET")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                if (steps == null)
                                {
                                    elementsList.Add(new ElementSteps());
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                }
                                if (steps != null)
                                {
                                    int offset = 1;
                                    if (steps.Images != null) offset++;
                                    if (steps.Segments != null) offset++;
                                    if (steps.Number != null) offset++;
                                    //if (steps.Number_Target != null) offset++;
                                    if (steps.Pointer != null) offset++;
                                    if (steps.Circle_Scale != null) offset++;
                                    if (steps.Linear_Scale != null) offset++;
                                    if (steps.Icon != null) offset++;

                                    steps.Number_Target = new hmUI_widget_IMG_NUMBER();
                                    steps.Number_Target.img_First = imgNumber.img_First;
                                    steps.Number_Target.imageX = imgNumber.imageX;
                                    steps.Number_Target.imageY = imgNumber.imageY;
                                    steps.Number_Target.space = imgNumber.space;
                                    steps.Number_Target.zero = imgNumber.zero;
                                    steps.Number_Target.unit = imgNumber.unit;
                                    steps.Number_Target.imperial_unit = imgNumber.imperial_unit;
                                    steps.Number_Target.negative_image = imgNumber.negative_image;
                                    steps.Number.invalid_image = imgNumber.invalid_image;
                                    steps.Number_Target.dot_image = imgNumber.dot_image;
                                    steps.Number_Target.align = imgNumber.align;
                                    steps.Number_Target.visible = true;
                                    steps.Number_Target.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "BATTERY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementBattery battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                if (battery == null)
                                {
                                    elementsList.Add(new ElementBattery());
                                    battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                }
                                if (battery != null)
                                {
                                    int offset = 1;
                                    if (battery.Images != null) offset++;
                                    if (battery.Segments != null) offset++;
                                    //if (steps.Number != null) offset++;
                                    if (battery.Pointer != null) offset++;
                                    if (battery.Circle_Scale != null) offset++;
                                    if (battery.Linear_Scale != null) offset++;
                                    if (battery.Icon != null) offset++;

                                    battery.Number = new hmUI_widget_IMG_NUMBER();
                                    battery.Number.img_First = imgNumber.img_First;
                                    battery.Number.imageX = imgNumber.imageX;
                                    battery.Number.imageY = imgNumber.imageY;
                                    battery.Number.space = imgNumber.space;
                                    battery.Number.zero = imgNumber.zero;
                                    battery.Number.unit = imgNumber.unit;
                                    battery.Number.imperial_unit = imgNumber.imperial_unit;
                                    battery.Number.negative_image = imgNumber.negative_image;
                                    battery.Number.invalid_image = imgNumber.invalid_image;
                                    battery.Number.dot_image = imgNumber.dot_image;
                                    battery.Number.align = imgNumber.align;
                                    battery.Number.visible = true;
                                    battery.Number.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "CAL")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                if (calorie == null)
                                {
                                    elementsList.Add(new ElementCalories());
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                }
                                if (calorie != null)
                                {
                                    int offset = 1;
                                    if (calorie.Images != null) offset++;
                                    if (calorie.Segments != null) offset++;
                                    //if (steps.Number != null) offset++;
                                    if (calorie.Number_Target != null) offset++;
                                    if (calorie.Pointer != null) offset++;
                                    if (calorie.Circle_Scale != null) offset++;
                                    if (calorie.Linear_Scale != null) offset++;
                                    if (calorie.Icon != null) offset++;

                                    calorie.Number = new hmUI_widget_IMG_NUMBER();
                                    calorie.Number.img_First = imgNumber.img_First;
                                    calorie.Number.imageX = imgNumber.imageX;
                                    calorie.Number.imageY = imgNumber.imageY;
                                    calorie.Number.space = imgNumber.space;
                                    calorie.Number.zero = imgNumber.zero;
                                    calorie.Number.unit = imgNumber.unit;
                                    calorie.Number.imperial_unit = imgNumber.imperial_unit;
                                    calorie.Number.negative_image = imgNumber.negative_image;
                                    calorie.Number.invalid_image = imgNumber.invalid_image;
                                    calorie.Number.dot_image = imgNumber.dot_image;
                                    calorie.Number.align = imgNumber.align;
                                    calorie.Number.visible = true;
                                    calorie.Number.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "CAL_TARGET")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                if (calorie == null)
                                {
                                    elementsList.Add(new ElementCalories());
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                }
                                if (calorie != null)
                                {
                                    int offset = 1;
                                    if (calorie.Images != null) offset++;
                                    if (calorie.Segments != null) offset++;
                                    if (calorie.Number != null) offset++;
                                    //if (steps.Number_Target != null) offset++;
                                    if (calorie.Pointer != null) offset++;
                                    if (calorie.Circle_Scale != null) offset++;
                                    if (calorie.Linear_Scale != null) offset++;
                                    if (calorie.Icon != null) offset++;

                                    calorie.Number_Target = new hmUI_widget_IMG_NUMBER();
                                    calorie.Number_Target.img_First = imgNumber.img_First;
                                    calorie.Number_Target.imageX = imgNumber.imageX;
                                    calorie.Number_Target.imageY = imgNumber.imageY;
                                    calorie.Number_Target.space = imgNumber.space;
                                    calorie.Number_Target.zero = imgNumber.zero;
                                    calorie.Number_Target.unit = imgNumber.unit;
                                    calorie.Number_Target.imperial_unit = imgNumber.imperial_unit;
                                    calorie.Number_Target.negative_image = imgNumber.negative_image;
                                    calorie.Number.invalid_image = imgNumber.invalid_image;
                                    calorie.Number_Target.dot_image = imgNumber.dot_image;
                                    calorie.Number_Target.align = imgNumber.align;
                                    calorie.Number_Target.visible = true;
                                    calorie.Number_Target.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "HEART")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementHeart heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                if (heart == null)
                                {
                                    elementsList.Add(new ElementHeart());
                                    heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                }
                                if (heart != null)
                                {
                                    int offset = 1;
                                    if (heart.Images != null) offset++;
                                    if (heart.Segments != null) offset++;
                                    //if (steps.Number != null) offset++;
                                    if (heart.Pointer != null) offset++;
                                    if (heart.Circle_Scale != null) offset++;
                                    if (heart.Linear_Scale != null) offset++;
                                    if (heart.Icon != null) offset++;

                                    heart.Number = new hmUI_widget_IMG_NUMBER();
                                    heart.Number.img_First = imgNumber.img_First;
                                    heart.Number.imageX = imgNumber.imageX;
                                    heart.Number.imageY = imgNumber.imageY;
                                    heart.Number.space = imgNumber.space;
                                    heart.Number.zero = imgNumber.zero;
                                    heart.Number.unit = imgNumber.unit;
                                    heart.Number.imperial_unit = imgNumber.imperial_unit;
                                    heart.Number.negative_image = imgNumber.negative_image;
                                    heart.Number.invalid_image = imgNumber.invalid_image;
                                    heart.Number.dot_image = imgNumber.dot_image;
                                    heart.Number.align = imgNumber.align;
                                    heart.Number.visible = true;
                                    heart.Number.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "PAI_DAILY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                if (pai == null)
                                {
                                    elementsList.Add(new ElementPAI());
                                    pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                }
                                if (pai != null)
                                {
                                    int offset = 1;
                                    if (pai.Images != null) offset++;
                                    if (pai.Segments != null) offset++;
                                    //if (steps.Number != null) offset++;
                                    if (pai.Number_Target != null) offset++;
                                    if (pai.Pointer != null) offset++;
                                    if (pai.Circle_Scale != null) offset++;
                                    if (pai.Linear_Scale != null) offset++;
                                    if (pai.Icon != null) offset++;

                                    pai.Number = new hmUI_widget_IMG_NUMBER();
                                    pai.Number.img_First = imgNumber.img_First;
                                    pai.Number.imageX = imgNumber.imageX;
                                    pai.Number.imageY = imgNumber.imageY;
                                    pai.Number.space = imgNumber.space;
                                    pai.Number.zero = imgNumber.zero;
                                    pai.Number.unit = imgNumber.unit;
                                    pai.Number.imperial_unit = imgNumber.imperial_unit;
                                    pai.Number.negative_image = imgNumber.negative_image;
                                    pai.Number.invalid_image = imgNumber.invalid_image;
                                    pai.Number.dot_image = imgNumber.dot_image;
                                    pai.Number.align = imgNumber.align;
                                    pai.Number.visible = true;
                                    pai.Number.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "PAI_WEEKLY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                if (pai == null)
                                {
                                    elementsList.Add(new ElementPAI());
                                    pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                }
                                if (pai != null)
                                {
                                    int offset = 1;
                                    if (pai.Images != null) offset++;
                                    if (pai.Segments != null) offset++;
                                    if (pai.Number != null) offset++;
                                    //if (steps.Number_Target != null) offset++;
                                    if (pai.Pointer != null) offset++;
                                    if (pai.Circle_Scale != null) offset++;
                                    if (pai.Linear_Scale != null) offset++;
                                    if (pai.Icon != null) offset++;

                                    pai.Number_Target = new hmUI_widget_IMG_NUMBER();
                                    pai.Number_Target.img_First = imgNumber.img_First;
                                    pai.Number_Target.imageX = imgNumber.imageX;
                                    pai.Number_Target.imageY = imgNumber.imageY;
                                    pai.Number_Target.space = imgNumber.space;
                                    pai.Number_Target.zero = imgNumber.zero;
                                    pai.Number_Target.unit = imgNumber.unit;
                                    pai.Number_Target.imperial_unit = imgNumber.imperial_unit;
                                    pai.Number_Target.negative_image = imgNumber.negative_image;
                                    pai.Number.invalid_image = imgNumber.invalid_image;
                                    pai.Number_Target.dot_image = imgNumber.dot_image;
                                    pai.Number_Target.align = imgNumber.align;
                                    pai.Number_Target.visible = true;
                                    pai.Number_Target.position = offset;
                                }
                            }

                            if (elementsList != null && imgNumber.type == "DISTANCE")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementDistance distance = (ElementDistance)elementsList.Find(e => e.GetType().Name == "ElementDistance");
                                if (distance == null)
                                {
                                    elementsList.Add(new ElementDistance());
                                    distance = (ElementDistance)elementsList.Find(e => e.GetType().Name == "ElementDistance");
                                }
                                if (distance != null)
                                {
                                    distance.Number = new hmUI_widget_IMG_NUMBER();
                                    distance.Number.img_First = imgNumber.img_First;
                                    distance.Number.imageX = imgNumber.imageX;
                                    distance.Number.imageY = imgNumber.imageY;
                                    distance.Number.space = imgNumber.space;
                                    distance.Number.zero = imgNumber.zero;
                                    distance.Number.unit = imgNumber.unit;
                                    distance.Number.imperial_unit = imgNumber.imperial_unit;
                                    distance.Number.negative_image = imgNumber.negative_image;
                                    distance.Number.invalid_image = imgNumber.invalid_image;
                                    distance.Number.dot_image = imgNumber.dot_image;
                                    distance.Number.align = imgNumber.align;
                                    distance.Number.visible = true;
                                    distance.Number.position = 1;
                                }
                            }



                            break;
                        #endregion

                        #region IMG_POINTER
                        case "IMG_POINTER":
                            hmUI_widget_IMG_POINTER imgPointer = Object_IMG_POINTER(parametrs);
                            elementsList = null;
                            if (imgPointer.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (imgPointer.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            if (elementsList != null && imgPointer.type == "STEP")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                if (steps == null)
                                {
                                    elementsList.Add(new ElementSteps());
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                }
                                if (steps != null)
                                {
                                    int offset = 1;
                                    if (steps.Images != null) offset++;
                                    if (steps.Segments != null) offset++;
                                    if (steps.Number != null) offset++;
                                    if (steps.Number_Target != null) offset++;
                                    //if (steps.Pointer != null) offset++;
                                    if (steps.Circle_Scale != null) offset++;
                                    if (steps.Linear_Scale != null) offset++;
                                    if (steps.Icon != null) offset++;

                                    steps.Pointer = new hmUI_widget_IMG_POINTER();
                                    steps.Pointer.src = imgPointer.src;
                                    steps.Pointer.center_x = imgPointer.center_x;
                                    steps.Pointer.center_y = imgPointer.center_y;
                                    steps.Pointer.pos_x = imgPointer.pos_x;
                                    steps.Pointer.pos_y = imgPointer.pos_y;
                                    steps.Pointer.start_angle = imgPointer.start_angle;
                                    steps.Pointer.end_angle = imgPointer.end_angle;
                                    steps.Pointer.cover_path = imgPointer.cover_path;
                                    steps.Pointer.cover_x = imgPointer.cover_x;
                                    steps.Pointer.cover_y = imgPointer.cover_y;
                                    steps.Pointer.scale = imgPointer.scale;
                                    steps.Pointer.scale_x = imgPointer.scale_x;
                                    steps.Pointer.scale_y = imgPointer.scale_y;
                                    steps.Pointer.visible = true;
                                    steps.Pointer.position = offset;
                                }
                            }

                            if (elementsList != null && imgPointer.type == "BATTERY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementBattery battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                if (battery == null)
                                {
                                    elementsList.Add(new ElementBattery());
                                    battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                }
                                if (battery != null)
                                {
                                    int offset = 1;
                                    if (battery.Images != null) offset++;
                                    if (battery.Segments != null) offset++;
                                    if (battery.Number != null) offset++;
                                    //if (steps.Pointer != null) offset++;
                                    if (battery.Circle_Scale != null) offset++;
                                    if (battery.Linear_Scale != null) offset++;
                                    if (battery.Icon != null) offset++;

                                    battery.Pointer = new hmUI_widget_IMG_POINTER();
                                    battery.Pointer.src = imgPointer.src;
                                    battery.Pointer.center_x = imgPointer.center_x;
                                    battery.Pointer.center_y = imgPointer.center_y;
                                    battery.Pointer.pos_x = imgPointer.pos_x;
                                    battery.Pointer.pos_y = imgPointer.pos_y;
                                    battery.Pointer.start_angle = imgPointer.start_angle;
                                    battery.Pointer.end_angle = imgPointer.end_angle;
                                    battery.Pointer.cover_path = imgPointer.cover_path;
                                    battery.Pointer.cover_x = imgPointer.cover_x;
                                    battery.Pointer.cover_y = imgPointer.cover_y;
                                    battery.Pointer.scale = imgPointer.scale;
                                    battery.Pointer.scale_x = imgPointer.scale_x;
                                    battery.Pointer.scale_y = imgPointer.scale_y;
                                    battery.Pointer.visible = true;
                                    battery.Pointer.position = offset;
                                }
                            }

                            if (elementsList != null && imgPointer.type == "CAL")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                if (calorie == null)
                                {
                                    elementsList.Add(new ElementCalories());
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                }
                                if (calorie != null)
                                {
                                    int offset = 1;
                                    if (calorie.Images != null) offset++;
                                    if (calorie.Segments != null) offset++;
                                    if (calorie.Number != null) offset++;
                                    if (calorie.Number_Target != null) offset++;
                                    //if (steps.Pointer != null) offset++;
                                    if (calorie.Circle_Scale != null) offset++;
                                    if (calorie.Linear_Scale != null) offset++;
                                    if (calorie.Icon != null) offset++;

                                    calorie.Pointer = new hmUI_widget_IMG_POINTER();
                                    calorie.Pointer.src = imgPointer.src;
                                    calorie.Pointer.center_x = imgPointer.center_x;
                                    calorie.Pointer.center_y = imgPointer.center_y;
                                    calorie.Pointer.pos_x = imgPointer.pos_x;
                                    calorie.Pointer.pos_y = imgPointer.pos_y;
                                    calorie.Pointer.start_angle = imgPointer.start_angle;
                                    calorie.Pointer.end_angle = imgPointer.end_angle;
                                    calorie.Pointer.cover_path = imgPointer.cover_path;
                                    calorie.Pointer.cover_x = imgPointer.cover_x;
                                    calorie.Pointer.cover_y = imgPointer.cover_y;
                                    calorie.Pointer.scale = imgPointer.scale;
                                    calorie.Pointer.scale_x = imgPointer.scale_x;
                                    calorie.Pointer.scale_y = imgPointer.scale_y;
                                    calorie.Pointer.visible = true;
                                    calorie.Pointer.position = offset;
                                }
                            }

                            if (elementsList != null && imgPointer.type == "HEART")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementHeart heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                if (heart == null)
                                {
                                    elementsList.Add(new ElementHeart());
                                    heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                }
                                if (heart != null)
                                {
                                    int offset = 1;
                                    if (heart.Images != null) offset++;
                                    if (heart.Segments != null) offset++;
                                    if (heart.Number != null) offset++;
                                    //if (steps.Pointer != null) offset++;
                                    if (heart.Circle_Scale != null) offset++;
                                    if (heart.Linear_Scale != null) offset++;
                                    if (heart.Icon != null) offset++;

                                    heart.Pointer = new hmUI_widget_IMG_POINTER();
                                    heart.Pointer.src = imgPointer.src;
                                    heart.Pointer.center_x = imgPointer.center_x;
                                    heart.Pointer.center_y = imgPointer.center_y;
                                    heart.Pointer.pos_x = imgPointer.pos_x;
                                    heart.Pointer.pos_y = imgPointer.pos_y;
                                    heart.Pointer.start_angle = imgPointer.start_angle;
                                    heart.Pointer.end_angle = imgPointer.end_angle;
                                    heart.Pointer.cover_path = imgPointer.cover_path;
                                    heart.Pointer.cover_x = imgPointer.cover_x;
                                    heart.Pointer.cover_y = imgPointer.cover_y;
                                    heart.Pointer.scale = imgPointer.scale;
                                    heart.Pointer.scale_x = imgPointer.scale_x;
                                    heart.Pointer.scale_y = imgPointer.scale_y;
                                    heart.Pointer.visible = true;
                                    heart.Pointer.position = offset;
                                }
                            }

                            if (elementsList != null && imgPointer.type == "PAI_WEEKLY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                if (pai == null)
                                {
                                    elementsList.Add(new ElementPAI());
                                    pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                }
                                if (pai != null)
                                {
                                    int offset = 1;
                                    if (pai.Images != null) offset++;
                                    if (pai.Segments != null) offset++;
                                    if (pai.Number != null) offset++;
                                    if (pai.Number_Target != null) offset++;
                                    //if (steps.Pointer != null) offset++;
                                    if (pai.Circle_Scale != null) offset++;
                                    if (pai.Linear_Scale != null) offset++;
                                    if (pai.Icon != null) offset++;

                                    pai.Pointer = new hmUI_widget_IMG_POINTER();
                                    pai.Pointer.src = imgPointer.src;
                                    pai.Pointer.center_x = imgPointer.center_x;
                                    pai.Pointer.center_y = imgPointer.center_y;
                                    pai.Pointer.pos_x = imgPointer.pos_x;
                                    pai.Pointer.pos_y = imgPointer.pos_y;
                                    pai.Pointer.start_angle = imgPointer.start_angle;
                                    pai.Pointer.end_angle = imgPointer.end_angle;
                                    pai.Pointer.cover_path = imgPointer.cover_path;
                                    pai.Pointer.cover_x = imgPointer.cover_x;
                                    pai.Pointer.cover_y = imgPointer.cover_y;
                                    pai.Pointer.scale = imgPointer.scale;
                                    pai.Pointer.scale_x = imgPointer.scale_x;
                                    pai.Pointer.scale_y = imgPointer.scale_y;
                                    pai.Pointer.visible = true;
                                    pai.Pointer.position = offset;
                                }
                            }

                            break;
                        #endregion

                        #region Circle_Scale
                        case "Circle_Scale":
                            Circle_Scale img_Circle_Scale = Object_Circle_Scale(parametrs);
                            elementsList = null;
                            if (img_Circle_Scale.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (img_Circle_Scale.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            if (elementsList != null && img_Circle_Scale.type == "STEP")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                if (steps == null)
                                {
                                    elementsList.Add(new ElementSteps());
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                }
                                if (steps != null)
                                {
                                    int offset = 1;
                                    if (steps.Images != null) offset++;
                                    if (steps.Segments != null) offset++;
                                    if (steps.Number != null) offset++;
                                    if (steps.Number_Target != null) offset++;
                                    if (steps.Pointer != null) offset++;
                                    //if (steps.Circle_Scale != null) offset++;
                                    if (steps.Linear_Scale != null) offset++;
                                    if (steps.Icon != null) offset++;

                                    steps.Circle_Scale = new Circle_Scale();
                                    steps.Circle_Scale.center_x = img_Circle_Scale.center_x;
                                    steps.Circle_Scale.center_y = img_Circle_Scale.center_y;
                                    steps.Circle_Scale.start_angle = img_Circle_Scale.start_angle;
                                    steps.Circle_Scale.end_angle = img_Circle_Scale.end_angle;
                                    steps.Circle_Scale.color = img_Circle_Scale.color;
                                    steps.Circle_Scale.radius = img_Circle_Scale.radius;
                                    steps.Circle_Scale.line_width = img_Circle_Scale.line_width;
                                    steps.Circle_Scale.mirror = img_Circle_Scale.mirror;
                                    steps.Circle_Scale.inversion = img_Circle_Scale.inversion;
                                    steps.Circle_Scale.visible = true;
                                    steps.Circle_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Circle_Scale.type == "BATTERY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementBattery battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                if (battery == null)
                                {
                                    elementsList.Add(new ElementBattery());
                                    battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                }
                                if (battery != null)
                                {
                                    int offset = 1;
                                    if (battery.Images != null) offset++;
                                    if (battery.Segments != null) offset++;
                                    if (battery.Number != null) offset++;
                                    if (battery.Pointer != null) offset++;
                                    //if (steps.Circle_Scale != null) offset++;
                                    if (battery.Linear_Scale != null) offset++;
                                    if (battery.Icon != null) offset++;

                                    battery.Circle_Scale = new Circle_Scale();
                                    battery.Circle_Scale.center_x = img_Circle_Scale.center_x;
                                    battery.Circle_Scale.center_y = img_Circle_Scale.center_y;
                                    battery.Circle_Scale.start_angle = img_Circle_Scale.start_angle;
                                    battery.Circle_Scale.end_angle = img_Circle_Scale.end_angle;
                                    battery.Circle_Scale.color = img_Circle_Scale.color;
                                    battery.Circle_Scale.radius = img_Circle_Scale.radius;
                                    battery.Circle_Scale.line_width = img_Circle_Scale.line_width;
                                    battery.Circle_Scale.mirror = img_Circle_Scale.mirror;
                                    battery.Circle_Scale.inversion = img_Circle_Scale.inversion;
                                    battery.Circle_Scale.visible = true;
                                    battery.Circle_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Circle_Scale.type == "CAL")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                if (calorie == null)
                                {
                                    elementsList.Add(new ElementCalories());
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                }
                                if (calorie != null)
                                {
                                    int offset = 1;
                                    if (calorie.Images != null) offset++;
                                    if (calorie.Segments != null) offset++;
                                    if (calorie.Number != null) offset++;
                                    if (calorie.Number_Target != null) offset++;
                                    if (calorie.Pointer != null) offset++;
                                    //if (steps.Circle_Scale != null) offset++;
                                    if (calorie.Linear_Scale != null) offset++;
                                    if (calorie.Icon != null) offset++;

                                    calorie.Circle_Scale = new Circle_Scale();
                                    calorie.Circle_Scale.center_x = img_Circle_Scale.center_x;
                                    calorie.Circle_Scale.center_y = img_Circle_Scale.center_y;
                                    calorie.Circle_Scale.start_angle = img_Circle_Scale.start_angle;
                                    calorie.Circle_Scale.end_angle = img_Circle_Scale.end_angle;
                                    calorie.Circle_Scale.color = img_Circle_Scale.color;
                                    calorie.Circle_Scale.radius = img_Circle_Scale.radius;
                                    calorie.Circle_Scale.line_width = img_Circle_Scale.line_width;
                                    calorie.Circle_Scale.mirror = img_Circle_Scale.mirror;
                                    calorie.Circle_Scale.inversion = img_Circle_Scale.inversion;
                                    calorie.Circle_Scale.visible = true;
                                    calorie.Circle_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Circle_Scale.type == "HEART")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementHeart heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                if (heart == null)
                                {
                                    elementsList.Add(new ElementHeart());
                                    heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                }
                                if (heart != null)
                                {
                                    int offset = 1;
                                    if (heart.Images != null) offset++;
                                    if (heart.Segments != null) offset++;
                                    if (heart.Number != null) offset++;
                                    if (heart.Pointer != null) offset++;
                                    //if (steps.Circle_Scale != null) offset++;
                                    if (heart.Linear_Scale != null) offset++;
                                    if (heart.Icon != null) offset++;

                                    heart.Circle_Scale = new Circle_Scale();
                                    heart.Circle_Scale.center_x = img_Circle_Scale.center_x;
                                    heart.Circle_Scale.center_y = img_Circle_Scale.center_y;
                                    heart.Circle_Scale.start_angle = img_Circle_Scale.start_angle;
                                    heart.Circle_Scale.end_angle = img_Circle_Scale.end_angle;
                                    heart.Circle_Scale.color = img_Circle_Scale.color;
                                    heart.Circle_Scale.radius = img_Circle_Scale.radius;
                                    heart.Circle_Scale.line_width = img_Circle_Scale.line_width;
                                    heart.Circle_Scale.mirror = img_Circle_Scale.mirror;
                                    heart.Circle_Scale.inversion = img_Circle_Scale.inversion;
                                    heart.Circle_Scale.visible = true;
                                    heart.Circle_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Circle_Scale.type == "PAI_WEEKLY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                if (pai == null)
                                {
                                    elementsList.Add(new ElementPAI());
                                    pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                }
                                if (pai != null)
                                {
                                    int offset = 1;
                                    if (pai.Images != null) offset++;
                                    if (pai.Segments != null) offset++;
                                    if (pai.Number != null) offset++;
                                    if (pai.Number_Target != null) offset++;
                                    if (pai.Pointer != null) offset++;
                                    //if (steps.Circle_Scale != null) offset++;
                                    if (pai.Linear_Scale != null) offset++;
                                    if (pai.Icon != null) offset++;

                                    pai.Circle_Scale = new Circle_Scale();
                                    pai.Circle_Scale.center_x = img_Circle_Scale.center_x;
                                    pai.Circle_Scale.center_y = img_Circle_Scale.center_y;
                                    pai.Circle_Scale.start_angle = img_Circle_Scale.start_angle;
                                    pai.Circle_Scale.end_angle = img_Circle_Scale.end_angle;
                                    pai.Circle_Scale.color = img_Circle_Scale.color;
                                    pai.Circle_Scale.radius = img_Circle_Scale.radius;
                                    pai.Circle_Scale.line_width = img_Circle_Scale.line_width;
                                    pai.Circle_Scale.mirror = img_Circle_Scale.mirror;
                                    pai.Circle_Scale.inversion = img_Circle_Scale.inversion;
                                    pai.Circle_Scale.visible = true;
                                    pai.Circle_Scale.position = offset;
                                }
                            }

                            break;
                        #endregion

                        #region Linear_Scale
                        case "Linear_Scale":
                            Linear_Scale img_Linear_Scale = Object_Linear_Scale(parametrs);
                            elementsList = null;
                            if (img_Linear_Scale.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (img_Linear_Scale.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            if (elementsList != null && img_Linear_Scale.type == "STEP")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementSteps steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                if (steps == null)
                                {
                                    elementsList.Add(new ElementSteps());
                                    steps = (ElementSteps)elementsList.Find(e => e.GetType().Name == "ElementSteps");
                                }
                                if (steps != null)
                                {
                                    int offset = 1;
                                    if (steps.Images != null) offset++;
                                    if (steps.Segments != null) offset++;
                                    if (steps.Number != null) offset++;
                                    if (steps.Number_Target != null) offset++;
                                    if (steps.Pointer != null) offset++;
                                    if (steps.Circle_Scale != null) offset++;
                                    //if (steps.Linear_Scale != null) offset++;
                                    if (steps.Icon != null) offset++;

                                    steps.Linear_Scale = new Linear_Scale();
                                    steps.Linear_Scale.start_x = img_Linear_Scale.start_x;
                                    steps.Linear_Scale.start_y = img_Linear_Scale.start_y;
                                    steps.Linear_Scale.color = img_Linear_Scale.color;
                                    steps.Linear_Scale.lenght = img_Linear_Scale.lenght;
                                    steps.Linear_Scale.line_width = img_Linear_Scale.line_width;
                                    steps.Linear_Scale.mirror = img_Linear_Scale.mirror;
                                    steps.Linear_Scale.inversion = img_Linear_Scale.inversion;
                                    steps.Linear_Scale.vertical = img_Linear_Scale.vertical;
                                    steps.Linear_Scale.pointer = img_Linear_Scale.pointer;
                                    steps.Linear_Scale.visible = true;
                                    steps.Linear_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Linear_Scale.type == "BATTERY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementBattery battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                if (battery == null)
                                {
                                    elementsList.Add(new ElementBattery());
                                    battery = (ElementBattery)elementsList.Find(e => e.GetType().Name == "ElementBattery");
                                }
                                if (battery != null)
                                {
                                    int offset = 1;
                                    if (battery.Images != null) offset++;
                                    if (battery.Segments != null) offset++;
                                    if (battery.Number != null) offset++;
                                    if (battery.Pointer != null) offset++;
                                    if (battery.Circle_Scale != null) offset++;
                                    //if (steps.Linear_Scale != null) offset++;
                                    if (battery.Icon != null) offset++;

                                    battery.Linear_Scale = new Linear_Scale();
                                    battery.Linear_Scale.start_x = img_Linear_Scale.start_x;
                                    battery.Linear_Scale.start_y = img_Linear_Scale.start_y;
                                    battery.Linear_Scale.color = img_Linear_Scale.color;
                                    battery.Linear_Scale.lenght = img_Linear_Scale.lenght;
                                    battery.Linear_Scale.line_width = img_Linear_Scale.line_width;
                                    battery.Linear_Scale.mirror = img_Linear_Scale.mirror;
                                    battery.Linear_Scale.inversion = img_Linear_Scale.inversion;
                                    battery.Linear_Scale.vertical = img_Linear_Scale.vertical;
                                    battery.Linear_Scale.pointer = img_Linear_Scale.pointer;
                                    battery.Linear_Scale.visible = true;
                                    battery.Linear_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Linear_Scale.type == "CAL")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementCalories calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                if (calorie == null)
                                {
                                    elementsList.Add(new ElementCalories());
                                    calorie = (ElementCalories)elementsList.Find(e => e.GetType().Name == "ElementCalories");
                                }
                                if (calorie != null)
                                {
                                    int offset = 1;
                                    if (calorie.Images != null) offset++;
                                    if (calorie.Segments != null) offset++;
                                    if (calorie.Number != null) offset++;
                                    if (calorie.Number_Target != null) offset++;
                                    if (calorie.Pointer != null) offset++;
                                    if (calorie.Circle_Scale != null) offset++;
                                    //if (steps.Linear_Scale != null) offset++;
                                    if (calorie.Icon != null) offset++;

                                    calorie.Linear_Scale = new Linear_Scale();
                                    calorie.Linear_Scale.start_x = img_Linear_Scale.start_x;
                                    calorie.Linear_Scale.start_y = img_Linear_Scale.start_y;
                                    calorie.Linear_Scale.color = img_Linear_Scale.color;
                                    calorie.Linear_Scale.lenght = img_Linear_Scale.lenght;
                                    calorie.Linear_Scale.line_width = img_Linear_Scale.line_width;
                                    calorie.Linear_Scale.mirror = img_Linear_Scale.mirror;
                                    calorie.Linear_Scale.inversion = img_Linear_Scale.inversion;
                                    calorie.Linear_Scale.vertical = img_Linear_Scale.vertical;
                                    calorie.Linear_Scale.pointer = img_Linear_Scale.pointer;
                                    calorie.Linear_Scale.visible = true;
                                    calorie.Linear_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Linear_Scale.type == "HEART")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementHeart heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                if (heart == null)
                                {
                                    elementsList.Add(new ElementHeart());
                                    heart = (ElementHeart)elementsList.Find(e => e.GetType().Name == "ElementHeart");
                                }
                                if (heart != null)
                                {
                                    int offset = 1;
                                    if (heart.Images != null) offset++;
                                    if (heart.Segments != null) offset++;
                                    if (heart.Number != null) offset++;
                                    if (heart.Pointer != null) offset++;
                                    if (heart.Circle_Scale != null) offset++;
                                    //if (steps.Linear_Scale != null) offset++;
                                    if (heart.Icon != null) offset++;

                                    heart.Linear_Scale = new Linear_Scale();
                                    heart.Linear_Scale.start_x = img_Linear_Scale.start_x;
                                    heart.Linear_Scale.start_y = img_Linear_Scale.start_y;
                                    heart.Linear_Scale.color = img_Linear_Scale.color;
                                    heart.Linear_Scale.lenght = img_Linear_Scale.lenght;
                                    heart.Linear_Scale.line_width = img_Linear_Scale.line_width;
                                    heart.Linear_Scale.mirror = img_Linear_Scale.mirror;
                                    heart.Linear_Scale.inversion = img_Linear_Scale.inversion;
                                    heart.Linear_Scale.vertical = img_Linear_Scale.vertical;
                                    heart.Linear_Scale.pointer = img_Linear_Scale.pointer;
                                    heart.Linear_Scale.visible = true;
                                    heart.Linear_Scale.position = offset;
                                }
                            }

                            if (elementsList != null && img_Linear_Scale.type == "PAI_WEEKLY")
                            //if (objectName.EndsWith("step_image_progress_img_level"))
                            {
                                ElementPAI pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                if (pai == null)
                                {
                                    elementsList.Add(new ElementPAI());
                                    pai = (ElementPAI)elementsList.Find(e => e.GetType().Name == "ElementPAI");
                                }
                                if (pai != null)
                                {
                                    int offset = 1;
                                    if (pai.Images != null) offset++;
                                    if (pai.Segments != null) offset++;
                                    if (pai.Number != null) offset++;
                                    if (pai.Number_Target != null) offset++;
                                    if (pai.Pointer != null) offset++;
                                    if (pai.Circle_Scale != null) offset++;
                                    //if (steps.Linear_Scale != null) offset++;
                                    if (pai.Icon != null) offset++;

                                    pai.Linear_Scale = new Linear_Scale();
                                    pai.Linear_Scale.start_x = img_Linear_Scale.start_x;
                                    pai.Linear_Scale.start_y = img_Linear_Scale.start_y;
                                    pai.Linear_Scale.color = img_Linear_Scale.color;
                                    pai.Linear_Scale.lenght = img_Linear_Scale.lenght;
                                    pai.Linear_Scale.line_width = img_Linear_Scale.line_width;
                                    pai.Linear_Scale.mirror = img_Linear_Scale.mirror;
                                    pai.Linear_Scale.inversion = img_Linear_Scale.inversion;
                                    pai.Linear_Scale.vertical = img_Linear_Scale.vertical;
                                    pai.Linear_Scale.pointer = img_Linear_Scale.pointer;
                                    pai.Linear_Scale.visible = true;
                                    pai.Linear_Scale.position = offset;
                                }
                            }

                            break;
                        #endregion

                        #region IMG_STATUS
                        case "IMG_STATUS":
                            hmUI_widget_IMG_STATUS imgStatus = Object_IMG_STATUS(parametrs);
                            elementsList = null;
                            if (imgStatus.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (imgStatus.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            ElementStatuses elementStatuses = (ElementStatuses)elementsList.Find(e => e.GetType().Name == "ElementStatuses");
                            if (elementStatuses == null)
                            {
                                elementsList.Add(new ElementStatuses());
                                elementStatuses = (ElementStatuses)elementsList.Find(e => e.GetType().Name == "ElementStatuses");
                            }
                            if (elementStatuses != null)
                            {
                                int index = 1;
                                switch (imgStatus.type) 
                                {
                                    case "CLOCK":
                                        //if (elementStatuses.Alarm != null) index++;
                                        if (elementStatuses.Bluetooth != null) index++;
                                        if (elementStatuses.DND != null) index++;
                                        if (elementStatuses.Lock != null) index++;

                                        imgStatus.type = "Alarm";
                                        imgStatus.position = index;
                                        elementStatuses.Alarm = imgStatus;
                                        break;

                                    case "DISCONNECT":
                                        if (elementStatuses.Alarm != null) index++;
                                        //if (elementStatuses.Bluetooth != null) index++;
                                        if (elementStatuses.DND != null) index++;
                                        if (elementStatuses.Lock != null) index++;

                                        imgStatus.type = "Bluetooth";
                                        imgStatus.position = index;
                                        elementStatuses.Bluetooth = imgStatus;
                                        break;

                                    case "DISTURB":
                                        if (elementStatuses.Alarm != null) index++;
                                        if (elementStatuses.Bluetooth != null) index++;
                                        //if (elementStatuses.DND != null) index++;
                                        if (elementStatuses.Lock != null) index++;

                                        imgStatus.type = "DND";
                                        imgStatus.position = index;
                                        elementStatuses.DND = imgStatus;
                                        break;

                                    case "LOCK":
                                        if (elementStatuses.Alarm != null) index++;
                                        if (elementStatuses.Bluetooth != null) index++;
                                        if (elementStatuses.DND != null) index++;
                                        //if (elementStatuses.Lock != null) index++;

                                        imgStatus.type = "Lock";
                                        imgStatus.position = index;
                                        elementStatuses.Lock = imgStatus;
                                        break;
                                }
                            }
                            break;
                        #endregion

                        #region IMG_CLICK
                        case "IMG_CLICK":
                            hmUI_widget_IMG_CLICK imgShortcut = Object_IMG_CLICK(parametrs);
                            elementsList = null;
                            if (imgShortcut.show_level == "ONLY_NORMAL" || objectName.StartsWith("normal"))
                            {
                                if (Watch_Face.ScreenNormal.Elements == null)
                                    Watch_Face.ScreenNormal.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenNormal.Elements;
                            }
                            else if (imgShortcut.show_level == "ONAL_AOD" || objectName.StartsWith("idle"))
                            {
                                if (Watch_Face.ScreenAOD.Elements == null)
                                    Watch_Face.ScreenAOD.Elements = new List<object>();
                                elementsList = Watch_Face.ScreenAOD.Elements;
                            }

                            ElementShortcuts elementShortcuts = (ElementShortcuts)elementsList.Find(e => e.GetType().Name == "ElementShortcuts");
                            if (elementShortcuts == null)
                            {
                                elementsList.Add(new ElementShortcuts());
                                elementShortcuts = (ElementShortcuts)elementsList.Find(e => e.GetType().Name == "ElementShortcuts");
                            }
                            if (elementShortcuts != null)
                            {
                                int index = 1;
                                switch (imgShortcut.type)
                                {
                                    case "STEP":
                                        //if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Step = imgShortcut;
                                        break;

                                    case "HEART":
                                        if (elementShortcuts.Step != null) index++;
                                        //if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Heart = imgShortcut;
                                        break;

                                    case "SPO2":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        //if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.SPO2 = imgShortcut;
                                        break;

                                    case "PAI":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        //if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.PAI = imgShortcut;
                                        break;

                                    case "STRESS":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        //if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Stress = imgShortcut;
                                        break;

                                    case "WEATHER_CURRENT":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        //if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Weather = imgShortcut;
                                        break;

                                    case "ALTIMETER":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        //if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Altimeter = imgShortcut;
                                        break;

                                    case "SUN_CURRENT":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        //if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Sunrise = imgShortcut;
                                        break;

                                    case "ALARM_CLOCK":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        //if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Alarm = imgShortcut;
                                        break;

                                    case "SLEEP":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        //if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Sleep = imgShortcut;
                                        break;

                                    case "COUNT_DOWN":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        //if (elementShortcuts.Countdown != null) index++;
                                        if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Countdown = imgShortcut;
                                        break;

                                    case "STOP_WATCH":
                                        if (elementShortcuts.Step != null) index++;
                                        if (elementShortcuts.Heart != null) index++;
                                        if (elementShortcuts.SPO2 != null) index++;
                                        if (elementShortcuts.PAI != null) index++;
                                        if (elementShortcuts.Stress != null) index++;
                                        if (elementShortcuts.Weather != null) index++;
                                        if (elementShortcuts.Altimeter != null) index++;
                                        if (elementShortcuts.Sunrise != null) index++;
                                        if (elementShortcuts.Alarm != null) index++;
                                        if (elementShortcuts.Sleep != null) index++;
                                        if (elementShortcuts.Countdown != null) index++;
                                        //if (elementShortcuts.Stopwatch != null) index++;

                                        imgShortcut.position = index;
                                        elementShortcuts.Stopwatch = imgShortcut;
                                        break;

                                }
                            }
                            break;
                            #endregion
                    }
                }

            }
        }


        /// <summary>Возвращает имя функции и все его содержимое</summary>
        private string GetFunction(string str, out string parametrName)
        {
            string returnString = "";
            parametrName = "";
            returnString = str.Trim(new char[] { '{', '}' });
            //returnString = str.TrimStart(char.Parse("{"));
            //returnString = str.TrimEnd(char.Parse("{"));
            int firstIndex = str.IndexOf("{");
            int lastIndex = str.IndexOf("}");
            if (lastIndex > firstIndex)
            {
                returnString = str.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
                int openCount = new Regex("{").Matches(returnString).Count;
                int closingPCount = new Regex("}").Matches(returnString).Count;
                while (openCount != closingPCount)
                {
                    lastIndex = str.IndexOf("}", lastIndex + 1);
                    returnString = str.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
                    openCount = new Regex("{").Matches(returnString).Count;
                    closingPCount = new Regex("}").Matches(returnString).Count;
                }

                parametrName = str.Remove(firstIndex);
                int stringStartIndex = parametrName.LastIndexOf("\n");
                if(stringStartIndex > 0) parametrName = parametrName.Remove(0, stringStartIndex);
                parametrName = parametrName.Trim();
                int i1 = str.IndexOf(Environment.NewLine);
                //returnString = str.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
                //int i = returnString.Length;

                //int i4 = new Regex("{").Matches(returnString).Count;
                //int i3 = new Regex("}").Matches(returnString).Count;
            }
            else returnString = "";

            return returnString;
        }

        /// <summary>Возвращает по отдельности все функции из строки вместе с их содержимым</summary>
        private List<string> GetFunctionsList(string str)
        {
            List<string> GetParametrsList = new List<string>();


            int valueLenght = str.IndexOf("});") + 2;
            while (valueLenght > 0)
            {
                string valueStr = str.Remove(valueLenght);
                //str = str.Remove(0, valueLenght + 1);


                //int firstIndex = valueStr.IndexOf("(");
                int breackLineIndex = valueStr.IndexOf(";");
                while (breackLineIndex > 0 )
                {
                    valueStr = valueStr.Remove(0, breackLineIndex + 1);
                    breackLineIndex = valueStr.IndexOf(";");
                }
                int firstIndex = valueStr.IndexOf("(");
                //string TempStr = valueStr.Remove(firstIndex);
                if (firstIndex >= 0)
                {
                    int stringStartIndex = valueStr.Remove(firstIndex).LastIndexOf("\n");
                    valueStr = valueStr.Remove(0, stringStartIndex);
                    valueStr = valueStr.TrimStart();

                    GetParametrsList.Add(valueStr); 
                }
                str = str.Remove(0, valueLenght + 1);
                valueLenght = str.IndexOf("});");
                int posIf = str.IndexOf("if (screenType");
                if (posIf >= 0 && posIf < valueLenght) valueLenght = str.IndexOf("};")-1;
                if (valueLenght >= 0) valueLenght = valueLenght + 2;
            }

            return GetParametrsList;
        }

        /// <summary>Возвращает все параметры функции и их значения в виде строк</summary>
        private Dictionary<string, string> ParseParametrsInString(string str)
        {
            Dictionary<string, string> returnParametrs = new Dictionary<string, string>();

            int endIndex = str.IndexOf("=");
            if (endIndex < 0)
                return returnParametrs;
            string valueNameStr = str.Remove(endIndex);
            valueNameStr = valueNameStr.Replace("$", "");
            valueNameStr = valueNameStr.Trim();

            int startIndex = str.IndexOf("hmUI.widget.") + "hmUI.widget.".Length;
            endIndex = str.IndexOf(",", startIndex);
            if(str.IndexOf(")", startIndex) < endIndex) 
            {
                endIndex = str.IndexOf(")", startIndex);
                if (startIndex < 12 || endIndex < 0)
                    return returnParametrs;
                string valueStrType = str.Substring(startIndex, endIndex - startIndex);
                returnParametrs.Add("ObjectName", valueNameStr);
                returnParametrs.Add("ObjectType", valueStrType);
                return returnParametrs;
            }
            if (startIndex < 12 || endIndex < 0)
                return returnParametrs;
            string valueStr = str.Substring(startIndex, endIndex - startIndex);
            returnParametrs.Add("ObjectName", valueNameStr);
            returnParametrs.Add("ObjectType", valueStr);

            startIndex = str.IndexOf("{") + 1;
            endIndex = str.IndexOf("}", startIndex);
            str = str.Substring(startIndex, endIndex - startIndex);
            str = str.Trim();
            str = str + Environment.NewLine;

            endIndex = str.IndexOf(Environment.NewLine);
            while (endIndex > 0)
            {
                valueStr = str.Substring(0, endIndex);
                //int tempInt = valueStr.IndexOf("//");
                valueStr = valueStr.Trim();
                startIndex = valueStr.IndexOf(",");
                if (startIndex > 0) valueStr = valueStr.Remove(startIndex, valueStr.Length- startIndex);
                //valueStr = valueStr.TrimEnd(',');
                startIndex = valueStr.IndexOf(":");
                if (startIndex > 0)
                {
                    string valueName = valueStr.Substring(0, startIndex);
                    valueStr = valueStr.Remove(0, startIndex + 1);
                    valueStr = valueStr.Trim();

                    if (returnParametrs.ContainsKey(valueName)) returnParametrs.Remove(valueName);
                    returnParametrs.Add(valueName, valueStr); 
                }

                str = str.Remove(0, endIndex);
                str = str.TrimStart();
                //str = str.Trim();
                //str = str.TrimEnd(',');
                endIndex = str.IndexOf(Environment.NewLine);
            }

            return returnParametrs;
        }

        private hmUI_widget_IMG Object_IMG(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG img = new hmUI_widget_IMG();
            int value;
            if (parametrs.ContainsKey("src"))
            {
                string imgName = parametrs["src"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                img.src = imgName;

                if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) img.x = value;
                if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) img.y = value;
                if (parametrs.ContainsKey("h") && Int32.TryParse(parametrs["h"], out value)) img.h = value;
                if (parametrs.ContainsKey("w") && Int32.TryParse(parametrs["w"], out value)) img.w = value;

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    img.show_level = imgName;
                }
                img.visible = true;
            }

            return img;
        }

        private hmUI_widget_FILL_RECT Object_FILL_RECT(Dictionary<string, string> parametrs)
        {
            hmUI_widget_FILL_RECT fill_rect = new hmUI_widget_FILL_RECT();
            int value;
            if (parametrs.ContainsKey("color")) fill_rect.color = parametrs["color"].Replace("'", "");
            if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) fill_rect.x = value;
            if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) fill_rect.y = value;
            if (parametrs.ContainsKey("h") && Int32.TryParse(parametrs["h"], out value)) fill_rect.h = value;
            if (parametrs.ContainsKey("w") && Int32.TryParse(parametrs["w"], out value)) fill_rect.w = value;

            if (parametrs.ContainsKey("show_level"))
            {
                string valueStr = parametrs["show_level"].Replace("hmUI.show_level.", "");
                fill_rect.show_level = valueStr;
            }

            return fill_rect;
        }

        private ElementDigitalTime Object_DigitalTime(Dictionary<string, string> parametrs)
        {
            ElementDigitalTime elementDigitalTime = new ElementDigitalTime();
            int value;
            int index = 1;
            if (parametrs.ContainsKey("hour_array"))
            {
                elementDigitalTime.Hour = new hmUI_widget_IMG_NUMBER();
                string[] hour_array = parametrs["hour_array"].Split(',');
                string imgName = hour_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementDigitalTime.Hour.img_First = imgName;
                if (parametrs.ContainsKey("hour_startX") && Int32.TryParse(parametrs["hour_startX"], out value))
                    elementDigitalTime.Hour.imageX = value;
                if (parametrs.ContainsKey("hour_startY") && Int32.TryParse(parametrs["hour_startY"], out value))
                    elementDigitalTime.Hour.imageY = value;
                if (parametrs.ContainsKey("hour_space") && Int32.TryParse(parametrs["hour_space"], out value))
                    elementDigitalTime.Hour.space = value;
                if (parametrs.ContainsKey("hour_zero"))
                {
                    //if (parametrs["hour_zero"] == "1") elementDigitalTime.Hour.zero = true;
                    //else elementDigitalTime.Hour.zero = false;
                    elementDigitalTime.Hour.zero = StringToBool(parametrs["hour_zero"]);
                }
                if (parametrs.ContainsKey("hour_align")) 
                    elementDigitalTime.Hour.align = parametrs["hour_align"].Replace("hmUI.align.", "");
                if (parametrs.ContainsKey("hour_unit_en"))
                {
                    imgName = parametrs["hour_unit_en"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementDigitalTime.Hour.unit = imgName;
                }
                elementDigitalTime.Hour.visible = true;
                elementDigitalTime.Hour.position = index;
                index++;
            }

            if (parametrs.ContainsKey("minute_array"))
            {
                elementDigitalTime.Minute = new hmUI_widget_IMG_NUMBER();
                string[] minute_array = parametrs["minute_array"].Split(',');
                string imgName = minute_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementDigitalTime.Minute.img_First = imgName;
                if (parametrs.ContainsKey("minute_startX") && Int32.TryParse(parametrs["minute_startX"], out value))
                    elementDigitalTime.Minute.imageX = value;
                if (parametrs.ContainsKey("minute_startY") && Int32.TryParse(parametrs["minute_startY"], out value))
                    elementDigitalTime.Minute.imageY = value;
                if (parametrs.ContainsKey("minute_space") && Int32.TryParse(parametrs["minute_space"], out value))
                    elementDigitalTime.Minute.space = value;
                if (parametrs.ContainsKey("minute_zero"))
                {
                    //if (parametrs["minute_zero"] == "1") elementDigitalTime.Minute.zero = true;
                    //else elementDigitalTime.Minute.zero = false;
                    elementDigitalTime.Minute.zero = StringToBool(parametrs["minute_zero"]);
                }
                if (parametrs.ContainsKey("minute_align"))
                    elementDigitalTime.Minute.align = parametrs["minute_align"].Replace("hmUI.align.", "");
                if (parametrs.ContainsKey("minute_unit_en"))
                {
                    imgName = parametrs["minute_unit_en"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementDigitalTime.Minute.unit = imgName;
                }
                if (parametrs.ContainsKey("minute_follow"))
                {
                    //if (parametrs["minute_follow"] == "1") elementDigitalTime.Minute.follow = true;
                    //else elementDigitalTime.Minute.follow = false;
                    elementDigitalTime.Minute.follow = StringToBool(parametrs["minute_follow"]);
                }
                elementDigitalTime.Minute.visible = true;
                elementDigitalTime.Minute.position = index;
                index++;
            }

            if (parametrs.ContainsKey("second_array"))
            {
                elementDigitalTime.Second = new hmUI_widget_IMG_NUMBER();
                string[] second_array = parametrs["second_array"].Split(',');
                string imgName = second_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementDigitalTime.Second.img_First = imgName;
                if (parametrs.ContainsKey("second_startX") && Int32.TryParse(parametrs["second_startX"], out value))
                    elementDigitalTime.Second.imageX = value;
                if (parametrs.ContainsKey("second_startY") && Int32.TryParse(parametrs["second_startY"], out value))
                    elementDigitalTime.Second.imageY = value;
                if (parametrs.ContainsKey("second_space") && Int32.TryParse(parametrs["second_space"], out value))
                    elementDigitalTime.Second.space = value;
                if (parametrs.ContainsKey("second_zero"))
                {
                    //if (parametrs["second_zero"] == "1") elementDigitalTime.Second.zero = true;
                    //else elementDigitalTime.Second.zero = false;
                    elementDigitalTime.Second.zero = StringToBool(parametrs["second_zero"]);
                }
                if (parametrs.ContainsKey("second_align"))
                    elementDigitalTime.Second.align = parametrs["second_align"].Replace("hmUI.align.", "");
                if (parametrs.ContainsKey("second_unit_en"))
                {
                    imgName = parametrs["second_unit_en"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementDigitalTime.Second.unit = imgName;
                }
                if (parametrs.ContainsKey("second_follow"))
                {
                    //if (parametrs["second_follow"] == "1") elementDigitalTime.Second.follow = true;
                    //else elementDigitalTime.Second.follow = false;
                    elementDigitalTime.Second.follow = StringToBool(parametrs["second_follow"]);
                }
                elementDigitalTime.Second.visible = true;
                elementDigitalTime.Second.position = index;
                index++;
            }

            if (parametrs.ContainsKey("am_en_path"))
            {
                elementDigitalTime.AmPm = new hmUI_widget_IMG_TIME_am_pm();
                string imgName = parametrs["am_en_path"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementDigitalTime.AmPm.am_img = imgName;
                if (parametrs.ContainsKey("am_x") && Int32.TryParse(parametrs["am_x"], out value))
                    elementDigitalTime.AmPm.am_x = value;
                if (parametrs.ContainsKey("am_y") && Int32.TryParse(parametrs["am_y"], out value))
                    elementDigitalTime.AmPm.am_y = value;

                if (parametrs.ContainsKey("pm_en_path"))
                {
                    imgName = parametrs["pm_en_path"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementDigitalTime.AmPm.pm_img = imgName;
                }
                if (parametrs.ContainsKey("pm_x") && Int32.TryParse(parametrs["pm_x"], out value))
                    elementDigitalTime.AmPm.pm_x = value;
                if (parametrs.ContainsKey("pm_y") && Int32.TryParse(parametrs["pm_y"], out value))
                    elementDigitalTime.AmPm.pm_y = value;

                elementDigitalTime.AmPm.visible = true;
                elementDigitalTime.AmPm.position = index;
                //index++;
            }

            return elementDigitalTime;
        }

        private ElementAnalogTime Object_AnalogTime(Dictionary<string, string> parametrs)
        {
            ElementAnalogTime elementAnalogTime = new ElementAnalogTime();
            int value;
            int index = 1;
            if (parametrs.ContainsKey("hour_path"))
            {
                elementAnalogTime.Hour = new hmUI_widget_IMG_POINTER();
                string imgName = parametrs["hour_path"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementAnalogTime.Hour.src = imgName;

                if (parametrs.ContainsKey("hour_centerX") && Int32.TryParse(parametrs["hour_centerX"], out value))
                    elementAnalogTime.Hour.center_x = value;
                if (parametrs.ContainsKey("hour_centerY") && Int32.TryParse(parametrs["hour_centerY"], out value))
                    elementAnalogTime.Hour.center_y = value;

                if (parametrs.ContainsKey("hour_posX") && Int32.TryParse(parametrs["hour_posX"], out value))
                    elementAnalogTime.Hour.pos_x = value;
                if (parametrs.ContainsKey("hour_posY") && Int32.TryParse(parametrs["hour_posY"], out value))
                    elementAnalogTime.Hour.pos_y = value;

                if (parametrs.ContainsKey("hour_cover_path"))
                {
                    imgName = parametrs["hour_cover_path"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementAnalogTime.Hour.cover_path = imgName;

                    if (parametrs.ContainsKey("hour_cover_x") && Int32.TryParse(parametrs["hour_cover_x"], out value))
                        elementAnalogTime.Hour.cover_x = value;
                    if (parametrs.ContainsKey("hour_cover_y") && Int32.TryParse(parametrs["hour_cover_y"], out value))
                        elementAnalogTime.Hour.cover_y = value;
                }

                elementAnalogTime.Hour.visible = true;
                elementAnalogTime.Hour.position = index;
                index++;
            }

            if (parametrs.ContainsKey("minute_path"))
            {
                elementAnalogTime.Minute = new hmUI_widget_IMG_POINTER();
                string imgName = parametrs["minute_path"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementAnalogTime.Minute.src = imgName;

                if (parametrs.ContainsKey("minute_centerX") && Int32.TryParse(parametrs["minute_centerX"], out value))
                    elementAnalogTime.Minute.center_x = value;
                if (parametrs.ContainsKey("minute_centerY") && Int32.TryParse(parametrs["minute_centerY"], out value))
                    elementAnalogTime.Minute.center_y = value;

                if (parametrs.ContainsKey("minute_posX") && Int32.TryParse(parametrs["minute_posX"], out value))
                    elementAnalogTime.Minute.pos_x = value;
                if (parametrs.ContainsKey("minute_posY") && Int32.TryParse(parametrs["minute_posY"], out value))
                    elementAnalogTime.Minute.pos_y = value;

                if (parametrs.ContainsKey("minute_cover_path"))
                {
                    imgName = parametrs["minute_cover_path"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementAnalogTime.Minute.cover_path = imgName;

                    if (parametrs.ContainsKey("minute_cover_x") && Int32.TryParse(parametrs["minute_cover_x"], out value))
                        elementAnalogTime.Minute.cover_x = value;
                    if (parametrs.ContainsKey("minute_cover_y") && Int32.TryParse(parametrs["minute_cover_y"], out value))
                        elementAnalogTime.Minute.cover_y = value;
                }

                elementAnalogTime.Minute.visible = true;
                elementAnalogTime.Minute.position = index;
                index++;
            }

            if (parametrs.ContainsKey("second_path"))
            {
                elementAnalogTime.Second = new hmUI_widget_IMG_POINTER();
                string imgName = parametrs["second_path"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementAnalogTime.Second.src = imgName;

                if (parametrs.ContainsKey("second_centerX") && Int32.TryParse(parametrs["second_centerX"], out value))
                    elementAnalogTime.Second.center_x = value;
                if (parametrs.ContainsKey("second_centerY") && Int32.TryParse(parametrs["second_centerY"], out value))
                    elementAnalogTime.Second.center_y = value;

                if (parametrs.ContainsKey("second_posX") && Int32.TryParse(parametrs["second_posX"], out value))
                    elementAnalogTime.Second.pos_x = value;
                if (parametrs.ContainsKey("second_posY") && Int32.TryParse(parametrs["second_posY"], out value))
                    elementAnalogTime.Second.pos_y = value;

                if (parametrs.ContainsKey("second_cover_path"))
                {
                    imgName = parametrs["second_cover_path"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementAnalogTime.Second.cover_path = imgName;

                    if (parametrs.ContainsKey("second_cover_x") && Int32.TryParse(parametrs["second_cover_x"], out value))
                        elementAnalogTime.Second.cover_x = value;
                    if (parametrs.ContainsKey("second_cover_y") && Int32.TryParse(parametrs["second_cover_y"], out value))
                        elementAnalogTime.Second.cover_y = value;
                }

                elementAnalogTime.Second.visible = true;
                elementAnalogTime.Second.position = index;
                index++;
            }

            return elementAnalogTime;
        }

        private hmUI_widget_IMG_POINTER Object_DATE_POINTER(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_POINTER elementPointer = new hmUI_widget_IMG_POINTER();
            int value;
            //int index = 1;
            if (parametrs.ContainsKey("src"))
            {
                string imgName = parametrs["src"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementPointer.src = imgName;

                if (parametrs.ContainsKey("center_x") && Int32.TryParse(parametrs["center_x"], out value))
                    elementPointer.center_x = value;
                if (parametrs.ContainsKey("center_y") && Int32.TryParse(parametrs["center_y"], out value))
                    elementPointer.center_y = value;

                if (parametrs.ContainsKey("posX") && Int32.TryParse(parametrs["posX"], out value))
                    elementPointer.pos_x = value;
                if (parametrs.ContainsKey("posY") && Int32.TryParse(parametrs["posY"], out value))
                    elementPointer.pos_y = value;

                if (parametrs.ContainsKey("start_angle") && Int32.TryParse(parametrs["start_angle"], out value))
                    elementPointer.start_angle = value;
                if (parametrs.ContainsKey("end_angle") && Int32.TryParse(parametrs["end_angle"], out value))
                    elementPointer.end_angle = value;

                if (parametrs.ContainsKey("cover_path"))
                {
                    imgName = parametrs["cover_path"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementPointer.cover_path = imgName;

                    if (parametrs.ContainsKey("cover_x") && Int32.TryParse(parametrs["cover_x"], out value))
                        elementPointer.cover_x = value;
                    if (parametrs.ContainsKey("cover_y") && Int32.TryParse(parametrs["cover_y"], out value))
                        elementPointer.cover_y = value;
                }

                if (parametrs.ContainsKey("scale_en"))
                {
                    imgName = parametrs["scale_en"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementPointer.scale = imgName;

                    if (parametrs.ContainsKey("scale_x") && Int32.TryParse(parametrs["scale_x"], out value))
                        elementPointer.scale_x = value;
                    if (parametrs.ContainsKey("scale_y") && Int32.TryParse(parametrs["scale_y"], out value))
                        elementPointer.scale_y = value;
                }

                if (parametrs.ContainsKey("type"))
                {
                    imgName = parametrs["type"].Replace("hmUI.date.", "");
                    elementPointer.type = imgName;
                }

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    elementPointer.show_level = imgName;
                }

                elementPointer.visible = true;
                elementPointer.position = 1;
                //index++;
            }

            return elementPointer;
        }

        private hmUI_widget_IMG_POINTER Object_IMG_POINTER(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_POINTER elementPointer = new hmUI_widget_IMG_POINTER();
            int value;
            //int index = 1;
            if (parametrs.ContainsKey("src"))
            {
                string imgName = parametrs["src"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                elementPointer.src = imgName;

                if (parametrs.ContainsKey("center_x") && Int32.TryParse(parametrs["center_x"], out value))
                    elementPointer.center_x = value;
                if (parametrs.ContainsKey("center_y") && Int32.TryParse(parametrs["center_y"], out value))
                    elementPointer.center_y = value;

                if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value))
                    elementPointer.pos_x = value;
                if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value))
                    elementPointer.pos_y = value;

                if (parametrs.ContainsKey("start_angle") && Int32.TryParse(parametrs["start_angle"], out value))
                    elementPointer.start_angle = value;
                if (parametrs.ContainsKey("end_angle") && Int32.TryParse(parametrs["end_angle"], out value))
                    elementPointer.end_angle = value;

                if (parametrs.ContainsKey("cover_path"))
                {
                    imgName = parametrs["cover_path"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementPointer.cover_path = imgName;

                    if (parametrs.ContainsKey("cover_x") && Int32.TryParse(parametrs["cover_x"], out value))
                        elementPointer.cover_x = value;
                    if (parametrs.ContainsKey("cover_y") && Int32.TryParse(parametrs["cover_y"], out value))
                        elementPointer.cover_y = value;
                }

                if (parametrs.ContainsKey("scale_en"))
                {
                    imgName = parametrs["scale_en"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    elementPointer.scale = imgName;

                    if (parametrs.ContainsKey("scale_x") && Int32.TryParse(parametrs["scale_x"], out value))
                        elementPointer.scale_x = value;
                    if (parametrs.ContainsKey("scale_y") && Int32.TryParse(parametrs["scale_y"], out value))
                        elementPointer.scale_y = value;
                }

                if (parametrs.ContainsKey("type"))
                {
                    imgName = parametrs["type"].Replace("hmUI.data_type.", "");
                    elementPointer.type = imgName;
                }

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    elementPointer.show_level = imgName;
                }

                elementPointer.visible = true;
                elementPointer.position = 1;
                //index++;
            }

            return elementPointer;
        }

        private List<hmUI_widget_IMG_NUMBER> Object_ImgDate(Dictionary<string, string> parametrs)
        {
            List<hmUI_widget_IMG_NUMBER> elementImgDate = new List<hmUI_widget_IMG_NUMBER>();
            int value;
            int index = 1;
            if (parametrs.ContainsKey("day_en_array"))
            {
                if (!parametrs.ContainsKey("day_is_character") || parametrs["day_is_character"] == "false")
                {
                    hmUI_widget_IMG_NUMBER dayNumber = new hmUI_widget_IMG_NUMBER();
                    string[] day_array = parametrs["day_en_array"].Split(',');
                    string imgName = day_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    dayNumber.img_First = imgName;
                    if (parametrs.ContainsKey("day_startX") && Int32.TryParse(parametrs["day_startX"], out value))
                        dayNumber.imageX = value;
                    if (parametrs.ContainsKey("day_startY") && Int32.TryParse(parametrs["day_startY"], out value))
                        dayNumber.imageY = value;
                    if (parametrs.ContainsKey("day_space") && Int32.TryParse(parametrs["day_space"], out value))
                        dayNumber.space = value;
                    if (parametrs.ContainsKey("day_zero"))
                    {
                        //if (parametrs["day_zero"] == "1") dayNumber.zero = true;
                        //else dayNumber.zero = false;
                        dayNumber.zero = StringToBool(parametrs["day_zero"]);
                    }
                    if (parametrs.ContainsKey("day_align"))
                        dayNumber.align = parametrs["day_align"].Replace("hmUI.align.", "");
                    if (parametrs.ContainsKey("day_unit_en"))
                    {
                        imgName = parametrs["day_unit_en"].Replace("'", "");
                        imgName = Path.GetFileNameWithoutExtension(imgName);
                        dayNumber.unit = imgName;
                    }

                    if (parametrs.ContainsKey("show_level"))
                    {
                        imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                        dayNumber.show_level = imgName;
                    }
                    dayNumber.visible = true;
                    dayNumber.position = index;
                    dayNumber.type = "DAY";
                    index++;

                    elementImgDate.Add(dayNumber);
                }
            }

            if (parametrs.ContainsKey("month_en_array"))
            {
                if (!parametrs.ContainsKey("month_is_character") || parametrs["month_is_character"] == "false")
                {
                    hmUI_widget_IMG_NUMBER monthNumber = new hmUI_widget_IMG_NUMBER();
                    string[] month_array = parametrs["month_en_array"].Split(',');
                    string imgName = month_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    monthNumber.img_First = imgName;
                    if (parametrs.ContainsKey("month_startX") && Int32.TryParse(parametrs["month_startX"], out value))
                        monthNumber.imageX = value;
                    if (parametrs.ContainsKey("month_startY") && Int32.TryParse(parametrs["month_startY"], out value))
                        monthNumber.imageY = value;
                    if (parametrs.ContainsKey("month_space") && Int32.TryParse(parametrs["month_space"], out value))
                        monthNumber.space = value;
                    if (parametrs.ContainsKey("month_zero"))
                    {
                        //if (parametrs["month_zero"] == "1") monthNumber.zero = true;
                        //else monthNumber.zero = false;
                        monthNumber.zero = StringToBool(parametrs["month_zero"]);
                    }
                    if (parametrs.ContainsKey("month_align"))
                        monthNumber.align = parametrs["month_align"].Replace("hmUI.align.", "");
                    if (parametrs.ContainsKey("month_unit_en"))
                    {
                        imgName = parametrs["month_unit_en"].Replace("'", "");
                        imgName = Path.GetFileNameWithoutExtension(imgName);
                        monthNumber.unit = imgName;
                    }
                    monthNumber.visible = true;
                    monthNumber.position = index;
                    monthNumber.type = "MONTH";
                    index++;

                    elementImgDate.Add(monthNumber);
                }
            }

            if (parametrs.ContainsKey("year_en_array"))
            {
                if (!parametrs.ContainsKey("year_is_character") || parametrs["year_is_character"] == "false")
                {
                    hmUI_widget_IMG_NUMBER yearNumber = new hmUI_widget_IMG_NUMBER();
                    string[] year_array = parametrs["year_en_array"].Split(',');
                    string imgName = year_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    yearNumber.img_First = imgName;
                    if (parametrs.ContainsKey("year_startX") && Int32.TryParse(parametrs["year_startX"], out value))
                        yearNumber.imageX = value;
                    if (parametrs.ContainsKey("year_startY") && Int32.TryParse(parametrs["year_startY"], out value))
                        yearNumber.imageY = value;
                    if (parametrs.ContainsKey("year_space") && Int32.TryParse(parametrs["year_space"], out value))
                        yearNumber.space = value;
                    if (parametrs.ContainsKey("year_zero"))
                    {
                        //if (parametrs["year_zero"] == "1") yearNumber.zero = true;
                        //else yearNumber.zero = false;
                        yearNumber.zero = StringToBool(parametrs["year_zero"]);
                    }
                    if (parametrs.ContainsKey("year_align"))
                        yearNumber.align = parametrs["year_align"].Replace("hmUI.align.", "");
                    if (parametrs.ContainsKey("year_unit_en"))
                    {
                        imgName = parametrs["year_unit_en"].Replace("'", "");
                        imgName = Path.GetFileNameWithoutExtension(imgName);
                        yearNumber.unit = imgName;
                    }
                    yearNumber.visible = true;
                    yearNumber.position = index;
                    yearNumber.type = "YEAR";
                    index++;

                    elementImgDate.Add(yearNumber);
                }
            }


            return elementImgDate;
        }

        private List<hmUI_widget_IMG_LEVEL> Object_ImgLevelDate(Dictionary<string, string> parametrs)
        {
            List<hmUI_widget_IMG_LEVEL> elementImgLevelDate = new List<hmUI_widget_IMG_LEVEL>();
            int value;
            int index = 1;
            if (parametrs.ContainsKey("day_en_array"))
            {
                if (parametrs.ContainsKey("day_is_character") && parametrs["day_is_character"] == "true")
                {
                    hmUI_widget_IMG_LEVEL dayNumber = new hmUI_widget_IMG_LEVEL();
                    string[] day_array = parametrs["day_en_array"].Split(',');
                    string imgName = day_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    dayNumber.img_First = imgName;
                    if (parametrs.ContainsKey("day_startX") && Int32.TryParse(parametrs["day_startX"], out value))
                        dayNumber.X = value;
                    if (parametrs.ContainsKey("day_startY") && Int32.TryParse(parametrs["day_startY"], out value))
                        dayNumber.Y = value;

                    if (parametrs.ContainsKey("show_level"))
                    {
                        imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                        dayNumber.show_level = imgName;
                    }
                    dayNumber.visible = true;
                    dayNumber.position = index;
                    dayNumber.type = "DAY";
                    index++;

                    elementImgLevelDate.Add(dayNumber);
                }
            }

            if (parametrs.ContainsKey("month_en_array"))
            {
                if (parametrs.ContainsKey("month_is_character") && parametrs["month_is_character"] == "true")
                {
                    hmUI_widget_IMG_LEVEL monthNumber = new hmUI_widget_IMG_LEVEL();
                    string[] month_array = parametrs["month_en_array"].Split(',');
                    string imgName = month_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    monthNumber.img_First = imgName;
                    if (parametrs.ContainsKey("month_startX") && Int32.TryParse(parametrs["month_startX"], out value))
                        monthNumber.X = value;
                    if (parametrs.ContainsKey("month_startY") && Int32.TryParse(parametrs["month_startY"], out value))
                        monthNumber.Y = value;
                    monthNumber.visible = true;
                    monthNumber.position = index;
                    monthNumber.type = "MONTH";
                    index++;

                    elementImgLevelDate.Add(monthNumber);
                }
            }

            if (parametrs.ContainsKey("year_en_array"))
            {
                if (parametrs.ContainsKey("year_is_character") && parametrs["year_is_character"] == "true")
                {
                    hmUI_widget_IMG_LEVEL yearNumber = new hmUI_widget_IMG_LEVEL();
                    string[] year_array = parametrs["year_en_array"].Split(',');
                    string imgName = year_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    yearNumber.img_First = imgName;
                    if (parametrs.ContainsKey("year_startX") && Int32.TryParse(parametrs["year_startX"], out value))
                        yearNumber.X = value;
                    if (parametrs.ContainsKey("year_startY") && Int32.TryParse(parametrs["year_startY"], out value))
                        yearNumber.Y = value;
                    yearNumber.visible = true;
                    yearNumber.position = index;
                    yearNumber.type = "YEAR";
                    index++;

                    elementImgLevelDate.Add(yearNumber);
                }
            }


            return elementImgLevelDate;
        }

        private hmUI_widget_IMG_LEVEL Object_IMG_WEEK(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_LEVEL imgWeek = new hmUI_widget_IMG_LEVEL();
            int value;
            if (parametrs.ContainsKey("week_en"))
            {
                string[] day__array = parametrs["week_en"].Split(',');
                string imgName = day__array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                imgWeek.img_First = imgName;

                if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) imgWeek.X = value;
                if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) imgWeek.Y = value;

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    imgWeek.show_level = imgName;
                }

                imgWeek.visible = true;
                imgWeek.position = 1;
            }

            return imgWeek;
        }

        private hmUI_widget_IMG_LEVEL Object_IMG_LEVEL(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_LEVEL imgLevel = new hmUI_widget_IMG_LEVEL();
            int value;
            if (parametrs.ContainsKey("image_array"))
            {
                string[] image_array = parametrs["image_array"].Split(',');
                string imgName = image_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                imgLevel.img_First = imgName;

                if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) imgLevel.X = value;
                if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) imgLevel.Y = value;
                if (parametrs.ContainsKey("image_length") && Int32.TryParse(parametrs["image_length"], out value)) imgLevel.image_length = value;

                if (parametrs.ContainsKey("type"))
                {
                    imgName = parametrs["type"].Replace("hmUI.data_type.", "");
                    imgLevel.type = imgName;
                }

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    imgLevel.show_level = imgName;
                }

                imgLevel.visible = true;
                imgLevel.position = 1;
            }

            return imgLevel;
        }

        private hmUI_widget_IMG_PROGRESS Object_IMG_PROGRESS(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_PROGRESS imgProgress = new hmUI_widget_IMG_PROGRESS();
            int value;
            if (parametrs.ContainsKey("image_array"))
            {
                string[] image_array = parametrs["image_array"].Split(',');
                string imgName = image_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                imgProgress.img_First = imgName;
                if (parametrs.ContainsKey("image_length") && Int32.TryParse(parametrs["image_length"], out value)) imgProgress.image_length = value;

                List<int> X = new List<int>();
                if (parametrs.ContainsKey("x"))
                {
                    string[] x_array = parametrs["x"].Split(',');
                    foreach (string valueStr in x_array)
                    {
                        string str = valueStr.Replace("[", "").Replace("]", "");
                        if (Int32.TryParse(str, out value)) X.Add(value);
                    }
                }
                List<int> Y = new List<int>();
                if (parametrs.ContainsKey("y"))
                {
                    string[] y_array = parametrs["y"].Split(',');
                    foreach (string valueStr in y_array)
                    {
                        string str = valueStr.Replace("[", "").Replace("]", "");
                        if (Int32.TryParse(str, out value)) Y.Add(value);
                    }
                }

                value = -1;
                if (parametrs.ContainsKey("image_length")) Int32.TryParse(parametrs["image_length"], out value);
                if (X.Count == 0) X.Add(0);
                while (X.Count < value)
                {
                    int v = X[X.Count - 1];
                    X.Add(v);
                }
                value = -1;
                if (Y.Count == 0) Y.Add(0);
                while (Y.Count < value)
                {
                    int v = Y[Y.Count - 1];
                    Y.Add(v);
                }

                if (X.Count > 0) imgProgress.X = X;
                if (Y.Count > 0) imgProgress.Y = Y;

                if (parametrs.ContainsKey("type"))
                {
                    imgName = parametrs["type"].Replace("hmUI.data_type.", "");
                    imgProgress.type = imgName;
                }

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    imgProgress.show_level = imgName;
                }

                imgProgress.visible = true;
                imgProgress.position = 1;
            }

            return imgProgress;
        }

        private hmUI_widget_IMG_NUMBER Object_IMG_NUMBER(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_NUMBER imgNumber = new hmUI_widget_IMG_NUMBER();
            int value;
            if (parametrs.ContainsKey("font_array"))
            {
                string[] image_array = parametrs["font_array"].Split(',');
                string imgName = image_array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                imgNumber.img_First = imgName;

                if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) imgNumber.imageX = value;
                if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) imgNumber.imageY = value;
                if (parametrs.ContainsKey("h_space") && Int32.TryParse(parametrs["h_space"], out value)) imgNumber.space = value;
                if (parametrs.ContainsKey("zero")) imgNumber.zero = StringToBool(parametrs["zero"]);
                if (parametrs.ContainsKey("align_h")) imgNumber.align = parametrs["align_h"].Replace("hmUI.align.","");
                if (parametrs.ContainsKey("unit_en") && parametrs["unit_en"].Length > 0) 
                {
                    imgName = parametrs["unit_en"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    imgNumber.unit = imgName;
                }
                if (parametrs.ContainsKey("imperial_unit_en") && parametrs["imperial_unit_en"].Length > 0)
                {
                    imgName = parametrs["imperial_unit_en"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    imgNumber.unit = imgName;
                }
                if (parametrs.ContainsKey("negative_image") && parametrs["negative_image"].Length > 0)
                {
                    imgName = parametrs["negative_image"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    imgNumber.negative_image = imgName;
                }
                if (parametrs.ContainsKey("invalid_image") && parametrs["invalid_image"].Length > 0)
                {
                    imgName = parametrs["invalid_image"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    imgNumber.invalid_image = imgName;
                }
                if (parametrs.ContainsKey("dot_image") && parametrs["dot_image"].Length > 0)
                {
                    imgName = parametrs["dot_image"].Replace("'", "");
                    imgName = Path.GetFileNameWithoutExtension(imgName);
                    imgNumber.dot_image = imgName;
                }

                if (parametrs.ContainsKey("type"))
                {
                    imgName = parametrs["type"].Replace("hmUI.data_type.", "");
                    imgNumber.type = imgName;
                }

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    imgNumber.show_level = imgName;
                }

                imgNumber.visible = true;
                imgNumber.position = 1;
            }

            return imgNumber;
        }

        private Circle_Scale Object_Circle_Scale(Dictionary<string, string> parametrs)
        {
            Circle_Scale element_Circle_Scale = new Circle_Scale(); 
            int value;
            string paramName = "";

            if (parametrs.ContainsKey("// center_x") && Int32.TryParse(parametrs["// center_x"], out value))
                element_Circle_Scale.center_x = value;
            if (parametrs.ContainsKey("// center_y") && Int32.TryParse(parametrs["// center_y"], out value))
                element_Circle_Scale.center_y = value;

            if (parametrs.ContainsKey("// start_angle") && Int32.TryParse(parametrs["// start_angle"], out value))
                element_Circle_Scale.start_angle = value;
            if (parametrs.ContainsKey("// end_angle") && Int32.TryParse(parametrs["// end_angle"], out value))
                element_Circle_Scale.end_angle = value;

            if (parametrs.ContainsKey("// color") && parametrs["// color"].Length > 3) 
                element_Circle_Scale.color = parametrs["// color"];

            if (parametrs.ContainsKey("// radius") && Int32.TryParse(parametrs["// radius"], out value))
                element_Circle_Scale.radius = value;
            if (parametrs.ContainsKey("// line_width") && Int32.TryParse(parametrs["// line_width"], out value))
                element_Circle_Scale.line_width = value;

            if (parametrs.ContainsKey("// mirror")) element_Circle_Scale.mirror = StringToBool(parametrs["// mirror"]);
            if (parametrs.ContainsKey("// inversion")) element_Circle_Scale.inversion = StringToBool(parametrs["// inversion"]);

            if (parametrs.ContainsKey("// type"))
            {
                paramName = parametrs["// type"].Replace("hmUI.data_type.", "");
                element_Circle_Scale.type = paramName;
            }

            if (parametrs.ContainsKey("// show_level"))
            {
                paramName = parametrs["// show_level"].Replace("hmUI.show_level.", "");
                element_Circle_Scale.show_level = paramName;
            }

            element_Circle_Scale.visible = true;
            element_Circle_Scale.position = 1;

            return element_Circle_Scale;
        }

        private Linear_Scale Object_Linear_Scale(Dictionary<string, string> parametrs)
        {
            Linear_Scale element_Linear_Scale = new Linear_Scale();
            int value;
            string paramName = "";

            if (parametrs.ContainsKey("// start_x") && Int32.TryParse(parametrs["// start_x"], out value))
                element_Linear_Scale.start_x = value;
            if (parametrs.ContainsKey("// start_y") && Int32.TryParse(parametrs["// start_y"], out value))
                element_Linear_Scale.start_y = value;

            if (parametrs.ContainsKey("// color") && parametrs["// color"].Length > 3)
                element_Linear_Scale.color = parametrs["// color"];

            if (parametrs.ContainsKey("// lenght") && Int32.TryParse(parametrs["// lenght"], out value))
                element_Linear_Scale.lenght = value;
            if (parametrs.ContainsKey("// line_width") && Int32.TryParse(parametrs["// line_width"], out value))
                element_Linear_Scale.line_width = value;

            if (parametrs.ContainsKey("// mirror")) element_Linear_Scale.mirror = StringToBool(parametrs["// mirror"]);
            if (parametrs.ContainsKey("// inversion")) element_Linear_Scale.inversion = StringToBool(parametrs["// inversion"]);
            if (parametrs.ContainsKey("// vertical")) element_Linear_Scale.vertical = StringToBool(parametrs["// vertical"]);

            if (parametrs.ContainsKey("// pointer") && parametrs["// pointer"].Length > 0)
            {
                paramName = parametrs["// pointer"].Replace("'", "");
                paramName = Path.GetFileNameWithoutExtension(paramName);
                element_Linear_Scale.pointer = paramName;
            }

            if (parametrs.ContainsKey("// type"))
            {
                paramName = parametrs["// type"].Replace("hmUI.data_type.", "");
                element_Linear_Scale.type = paramName;
            }

            if (parametrs.ContainsKey("// show_level"))
            {
                paramName = parametrs["// show_level"].Replace("hmUI.show_level.", "");
                element_Linear_Scale.show_level = paramName;
            }

            element_Linear_Scale.visible = true;
            element_Linear_Scale.position = 1;

            return element_Linear_Scale;
        }

        private hmUI_widget_IMG_STATUS Object_IMG_STATUS(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_STATUS img_status = new hmUI_widget_IMG_STATUS();
            int value;
            if (parametrs.ContainsKey("src"))
            {
                string imgName = parametrs["src"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                img_status.src = imgName;

                if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) img_status.x = value;
                if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) img_status.y = value;

                if (parametrs.ContainsKey("type"))
                {
                    imgName = parametrs["type"].Replace("hmUI.system_status.", "");
                    img_status.type = imgName;
                }

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    img_status.show_level = imgName;
                }
                img_status.visible = true;
                img_status.position = 1;
            }

            return img_status;
        }

        private hmUI_widget_IMG_CLICK Object_IMG_CLICK(Dictionary<string, string> parametrs)
        {
            hmUI_widget_IMG_CLICK img_shortcut = new hmUI_widget_IMG_CLICK();
            int value;
            if (parametrs.ContainsKey("src"))
            {
                string imgName = parametrs["src"].Replace("'", "");
                imgName = Path.GetFileNameWithoutExtension(imgName);
                img_shortcut.src = imgName;

                if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) img_shortcut.x = value;
                if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) img_shortcut.y = value;
                if (parametrs.ContainsKey("w") && Int32.TryParse(parametrs["w"], out value)) img_shortcut.w = value;
                if (parametrs.ContainsKey("h") && Int32.TryParse(parametrs["h"], out value)) img_shortcut.h = value;

                if (parametrs.ContainsKey("type"))
                {
                    imgName = parametrs["type"].Replace("hmUI.data_type.", "");
                    img_shortcut.type = imgName;
                }

                if (parametrs.ContainsKey("show_level"))
                {
                    imgName = parametrs["show_level"].Replace("hmUI.show_level.", "");
                    img_shortcut.show_level = imgName;
                }
                img_shortcut.visible = true;
                img_shortcut.position = 1;
            }

            return img_shortcut;
        }

        private bool StringToBool(string str)
        {
            bool returnValue = false;
            str = str.ToLower();
            if (str == "1") returnValue = true;
            if (str == "true") returnValue = true;
            return returnValue;
        }
    }
}
