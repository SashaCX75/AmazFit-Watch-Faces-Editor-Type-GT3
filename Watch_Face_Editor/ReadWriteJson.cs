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
                        AddElementToJS(element, "ONLY_NORMAL", out outVariables, out outItems);
                        variables += outVariables;
                        items += outItems;
                    }
                }
            }
            if (Watch_Face.ScreenAOD != null)
            {
                if (Watch_Face.ScreenAOD.Background != null && Watch_Face.ScreenAOD.Background.visible)
                {
                    if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                    {
                        variables += TabInString(4) + "let idle_background_bg = ''" + Environment.NewLine;
                        hmUI_widget_FILL_RECT backgroundColor = Watch_Face.ScreenAOD.Background.BackgroundColor;
                        //if (backgroundColor.show_level == null) backgroundColor.show_level = "ONLY_NORMAL";
                        options = FILL_RECT_Options(backgroundColor, "ONLY_NORMAL");
                        items += TabInString(6) + "idle_background_bg = hmUI.createWidget(hmUI.widget.FILL_RECT, {" +
                            options + TabInString(6) + "});" + Environment.NewLine;
                    }
                    if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                    {
                        variables += TabInString(4) + "let idle_background_bg_img = ''" + Environment.NewLine;
                        hmUI_widget_IMG backgroundImage = Watch_Face.ScreenAOD.Background.BackgroundImage;
                        //if (backgroundImage.show_level == null) backgroundImage.show_level = "ONLY_NORMAL";
                        options = IMG_Options(backgroundImage, "ONLY_NORMAL");
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
                        AddElementToJS(element, "ONAL_AOD", out outVariables, out outItems);
                        variables += outVariables;
                        items += outItems;
                    }
                }
            }
        }

        private void AddElementToJS(Object element, string show_level, out string variables, out string items)
        {
            string optionNameStart = "normal_";
            if (show_level == "ONAL_AOD") optionNameStart = "idle_";
            variables = "";
            items = "";
            string options = "";
            string type = element.GetType().Name;
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

                    for (int index = 1; index <= 4; index++)
                    {
                        if (index == hourPosition && hourPosition + 1 == minutePosition && minutePosition + 1 == secondPosition)
                        {
                            if (optionsHour.Length > 5 && optionsMinute.Length > 5)
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
                            }
                        }
                        else
                        {
                            if (index == hourPosition && optionsHour.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "digital_clock_img_time_hour = ''" + Environment.NewLine;
                                optionsHour += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "digital_clock_img_time_hour = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsHour + TabInString(6) + "});" + Environment.NewLine; 
                            }

                            if (index == minutePosition && optionsMinute.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "digital_clock_img_time_minute = ''" + Environment.NewLine;
                                optionsMinute += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "digital_clock_img_time_minute = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsMinute + TabInString(6) + "});" + Environment.NewLine; 
                            }

                            if (index == secondPosition && optionsSecond.Length > 5)
                            {
                                variables += TabInString(4) + "let " + optionNameStart +
                                    "digital_clock_img_time_second = ''" + Environment.NewLine;
                                optionsSecond += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    optionNameStart + "digital_clock_img_time_second = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsSecond + TabInString(6) + "});" + Environment.NewLine; 
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
                        optionsPointerDay = IMG_POINTER_Options(img_pointer, "DAY", show_level);
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
                                "date_img_date_month = ''" + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                optionNameStart + "date_img_date_month = hmUI.createWidget(hmUI.widget.IMG_DATE, {" +
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
                        optionsPointerMonth = IMG_POINTER_Options(img_pointer, "MONTH", show_level);
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
                    int numberPositionYear = 99;
                    string optionsNumberYear = "";
                    string optionsNumberYear_separator = "";
                    if (DateYear.Number != null && DateYear.Number.visible)
                    {
                        numberPositionYear = DateYear.Number.position;
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
                        optionsPointerWeek = IMG_POINTER_Options(img_pointer, "WEEK", show_level);
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

        private string IMG_POINTER_Options(hmUI_widget_IMG_POINTER img_pointer, string type, string show_level)
        {
            string options = Environment.NewLine;
            if(img_pointer == null) return options;
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
                string hour_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    hour_array += file_name;
                    if (i < imgPosition + count - 1) hour_array += ",";
                }
                hour_array += "]";
                options += TabInString(7) + "day_startX: " + img_number_day.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "day_startY: " + img_number_day.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "day_sc_array: " + hour_array + "," + Environment.NewLine;
                options += TabInString(7) + "day_tc_array: " + hour_array + "," + Environment.NewLine;
                options += TabInString(7) + "day_en_array: " + hour_array + "," + Environment.NewLine;

                if (img_number_day.zero) options += TabInString(7) + "day_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "day_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "day_space: " + img_number_day.space.ToString() + "," + Environment.NewLine;
                if (img_number_day.unit != null && img_number_day.unit.Length > 0)
                {
                    string hour_unit = "'" + img_number_day.unit + ".png'";
                    options += TabInString(7) + "day_unit_sc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "day_unit_tc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "day_unit_en: " + hour_unit + "," + Environment.NewLine;
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
                string hour_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    hour_array += file_name;
                    if (i < imgPosition + count - 1) hour_array += ",";
                }
                hour_array += "]";
                options += TabInString(7) + "month_startX: " + img_number_month.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "month_startY: " + img_number_month.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "month_sc_array: " + hour_array + "," + Environment.NewLine;
                options += TabInString(7) + "month_tc_array: " + hour_array + "," + Environment.NewLine;
                options += TabInString(7) + "month_en_array: " + hour_array + "," + Environment.NewLine;

                if (img_number_month.zero) options += TabInString(7) + "month_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "month_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "month_space: " + img_number_month.space.ToString() + "," + Environment.NewLine;
                if (img_number_month.unit != null && img_number_month.unit.Length > 0)
                {
                    string hour_unit = "'" + img_number_month.unit + ".png'";
                    options += TabInString(7) + "month_unit_sc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "month_unit_tc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "month_unit_en: " + hour_unit + "," + Environment.NewLine;
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
                string hour_array = "[";
                for (int i = imgPosition; i < imgPosition + 10; i++)
                {
                    string file_name = "\"" + ListImages[i] + ".png" + "\"";
                    hour_array += file_name;
                    if (i < imgPosition + count - 1) hour_array += ",";
                }
                hour_array += "]";
                options += TabInString(7) + "year_startX: " + img_number_year.imageX.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "year_startY: " + img_number_year.imageY.ToString() + "," + Environment.NewLine;
                options += TabInString(7) + "year_sc_array: " + hour_array + "," + Environment.NewLine;
                options += TabInString(7) + "year_tc_array: " + hour_array + "," + Environment.NewLine;
                options += TabInString(7) + "year_en_array: " + hour_array + "," + Environment.NewLine;

                if (img_number_year.zero) options += TabInString(7) + "year_zero: 1," + Environment.NewLine;
                else options += TabInString(7) + "year_zero: 0," + Environment.NewLine;
                options += TabInString(7) + "year_space: " + img_number_year.space.ToString() + "," + Environment.NewLine;
                if (img_number_year.unit != null && img_number_year.unit.Length > 0)
                {
                    string hour_unit = "'" + img_number_year.unit + ".png'";
                    options += TabInString(7) + "year_unit_sc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "year_unit_tc: " + hour_unit + "," + Environment.NewLine;
                    options += TabInString(7) + "year_unit_en: " + hour_unit + "," + Environment.NewLine;
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
                                if (objectName.StartsWith("normal"))
                                {
                                    if (Watch_Face.ScreenNormal.Background == null)
                                        Watch_Face.ScreenNormal.Background = new Background();
                                    Watch_Face.ScreenNormal.Background.BackgroundImage = img;
                                }
                                else if (objectName.StartsWith("idle"))
                                {
                                    if (Watch_Face.ScreenAOD.Background == null)
                                        Watch_Face.ScreenAOD.Background = new Background();
                                    Watch_Face.ScreenAOD.Background.BackgroundImage = img;
                                }
                            }
                            else
                            {
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
                                if (elementsList != null) elementsList.Add(img);
                            }

                            if (objectName.EndsWith("hour_separator_img"))
                            {
                                ElementDigitalTime digitalTime = null;
                                digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                                if(digitalTime != null && digitalTime.Hour != null)
                                {
                                    digitalTime.Hour.icon = img.src;
                                    digitalTime.Hour.iconPosX = img.x;
                                    digitalTime.Hour.iconPosY = img.y;
                                }
                            }

                            if (objectName.EndsWith("minute_separator_img"))
                            {
                                ElementDigitalTime digitalTime = null;
                                digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
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
                                digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
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
                                dateDay = (ElementDateDay)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateDay");
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
                                dateMonth = (ElementDateMonth)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
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
                                dateYear = (ElementDateYear)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateYear");
                                if (dateYear != null && dateYear.Number != null)
                                {
                                    dateYear.Number.icon = img.src;
                                    dateYear.Number.iconPosX = img.x;
                                    dateYear.Number.iconPosY = img.y;
                                }
                            }


                            break;
                        #endregion

                        #region FILL_RECT
                        case "FILL_RECT":
                            hmUI_widget_FILL_RECT fill_rect = Object_FILL_RECT(parametrs);
                            if (objectName.IndexOf("background") >= 0)
                            {
                                if (objectName.StartsWith("normal"))
                                {
                                    if (Watch_Face.ScreenNormal.Background == null)
                                        Watch_Face.ScreenNormal.Background = new Background();
                                    Watch_Face.ScreenNormal.Background.BackgroundColor = fill_rect;
                                }
                                else if (objectName.StartsWith("idle"))
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
                            hmUI_widget_IMG_POINTER img_pointer = Object_Pointer(parametrs);
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

                            //if (newPointer != null) 
                            //{
                            //    newPointer.src = img_pointer.src;
                            //    newPointer.center_x = img_pointer.center_x;
                            //    newPointer.center_y = img_pointer.center_y;
                            //    newPointer.pos_x = img_pointer.pos_x;
                            //    newPointer.pos_y = img_pointer.pos_y;

                            //    newPointer.start_angle = img_pointer.start_angle;
                            //    newPointer.end_angle = img_pointer.end_angle;

                            //    newPointer.cover_path = img_pointer.cover_path;
                            //    newPointer.cover_x = img_pointer.cover_x;
                            //    newPointer.cover_y = img_pointer.cover_y;

                            //    newPointer.scale = img_pointer.scale;
                            //    newPointer.scale_x = img_pointer.scale_x;
                            //    newPointer.scale_y = img_pointer.scale_y;

                            //    newPointer.type = img_pointer.type;
                            //    newPointer.position = offsetPointer;
                            //    newPointer.visible = true;
                            //}






                            break;
                        #endregion

                        #region IMG_WEEK
                        case "IMG_WEEK":
                            hmUI_widget_IMG_LEVEL imgWeek = Object_IMG_WEEK(parametrs);
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
                parametrName = parametrName.Remove(0, stringStartIndex);
                parametrName = parametrName.Trim();
                int i1 = str.IndexOf(Environment.NewLine);
                //returnString = str.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
                int i = returnString.Length;

                int i4 = new Regex("{").Matches(returnString).Count;
                int i3 = new Regex("}").Matches(returnString).Count;
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
                valueStr = valueStr.Trim();
                valueStr = valueStr.TrimEnd(',');
                startIndex = valueStr.IndexOf(":");
                string valueName = valueStr.Substring(0, startIndex);
                valueStr = valueStr.Remove(0, startIndex + 1);
                valueStr = valueStr.Trim();

                if (returnParametrs.ContainsKey(valueName)) returnParametrs.Remove(valueName);
                returnParametrs.Add(valueName, valueStr);

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
                    if (parametrs["hour_zero"] == "1") elementDigitalTime.Hour.zero = true;
                    else elementDigitalTime.Hour.zero = false;
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
                    if (parametrs["minute_zero"] == "1") elementDigitalTime.Minute.zero = true;
                    else elementDigitalTime.Minute.zero = false;
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
                    if (parametrs["minute_follow"] == "1") elementDigitalTime.Minute.follow = true;
                    else elementDigitalTime.Minute.follow = false;
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
                    if (parametrs["second_zero"] == "1") elementDigitalTime.Second.zero = true;
                    else elementDigitalTime.Second.zero = false;
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
                    if (parametrs["second_follow"] == "1") elementDigitalTime.Second.follow = true;
                    else elementDigitalTime.Second.follow = false;
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

        private hmUI_widget_IMG_POINTER Object_Pointer(Dictionary<string, string> parametrs)
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
                    string[] day__array = parametrs["day_en_array"].Split(',');
                    string imgName = day__array[0].Replace("\"", "").Replace("[", "").Replace("]", "");
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
                        if (parametrs["day_zero"] == "1") dayNumber.zero = true;
                        else dayNumber.zero = false;
                    }
                    if (parametrs.ContainsKey("day_align"))
                        dayNumber.align = parametrs["day_align"].Replace("hmUI.align.", "");
                    if (parametrs.ContainsKey("day_unit_en"))
                    {
                        imgName = parametrs["day_unit_en"].Replace("'", "");
                        imgName = Path.GetFileNameWithoutExtension(imgName);
                        dayNumber.unit = imgName;
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
                        if (parametrs["month_zero"] == "1") monthNumber.zero = true;
                        else monthNumber.zero = false;
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
                        if (parametrs["year_zero"] == "1") yearNumber.zero = true;
                        else yearNumber.zero = false;
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
                imgWeek.visible = true;
                imgWeek.position = 1;
            }

            return imgWeek;
        }
    }
}
