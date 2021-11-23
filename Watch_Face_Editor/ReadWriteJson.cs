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
        }

        private void AddElementToJS(Object element, string show_level, out string variables, out string items)
        {
            variables = "";
            items = "";
            string options = "";
            string type = element.GetType().Name;
            switch (type)
            {
                case "ElementDigitalTime":
                    ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                    int hourPosition = 99;
                    int minutePosition = 99;
                    int secondPosition = 99;
                    string optionsHour = "";
                    string optionsMinute = "";
                    string optionsSecond = "";
                    if (DigitalTime.Hour != null && DigitalTime.Hour.visible)
                    {
                        hourPosition = DigitalTime.Hour.position;
                        hmUI_widget_IMG_NUMBER img_number_hour = DigitalTime.Hour;
                        optionsHour = IMG_NUMBER_Hour_Options(img_number_hour, "");
                    }
                    if (DigitalTime.Minute != null && DigitalTime.Minute.visible)
                    {
                        minutePosition = DigitalTime.Minute.position;
                        hmUI_widget_IMG_NUMBER img_number_minute = DigitalTime.Minute;
                        optionsMinute = IMG_NUMBER_Minute_Options(img_number_minute, "");
                    }
                    if (DigitalTime.Second != null && DigitalTime.Second.visible)
                    {
                        secondPosition = DigitalTime.Second.position;
                        hmUI_widget_IMG_NUMBER img_number_second = DigitalTime.Second;
                        optionsSecond = IMG_NUMBER_Second_Options(img_number_second, "");
                    }

                    for (int index = 1; index <= 7; index++)
                    {
                        if (index == hourPosition && hourPosition + 1 == minutePosition && minutePosition + 1 == secondPosition)
                        {
                            variables += TabInString(4) + "let normal_digital_clock_img_time = ''" + Environment.NewLine;
                            options = optionsHour + optionsMinute + optionsSecond + Environment.NewLine +
                                TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                            items += Environment.NewLine + TabInString(6) +
                                "normal_digital_clock_img_time = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                    options + TabInString(6) + "});" + Environment.NewLine;
                        }
                        else
                        {
                            if (index == hourPosition)
                            {
                                variables += TabInString(4) + "let normal_digital_clock_img_time_hour = ''" + Environment.NewLine;
                                optionsHour += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    "normal_digital_clock_img_time_hour = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsHour + TabInString(6) + "});" + Environment.NewLine; 
                            }

                            if (index == minutePosition)
                            {
                                variables += TabInString(4) + "let normal_digital_clock_img_time_minute = ''" + Environment.NewLine;
                                optionsMinute += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    "normal_digital_clock_img_time_minute = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsMinute + TabInString(6) + "});" + Environment.NewLine; 
                            }

                            if (index == secondPosition)
                            {
                                variables += TabInString(4) + "let normal_digital_clock_img_time_second = ''" + Environment.NewLine;
                                optionsSecond += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
                                items += Environment.NewLine + TabInString(6) +
                                    "normal_digital_clock_img_time_second = hmUI.createWidget(hmUI.widget.IMG_TIME, {" +
                                        optionsSecond + TabInString(6) + "});" + Environment.NewLine; 
                            }
                        } 
                    }
                    break;
            }
        }

        private string FILL_RECT_Options(hmUI_widget_FILL_RECT fill_rect, string show_level)
        {
            string options = Environment.NewLine;
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
            options += TabInString(7) + "x: " + img.x.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "y: " + img.y.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "w: " + img.w.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "h: " + img.h.ToString() + "," + Environment.NewLine;
            options += TabInString(7) + "src: '" + img.src + ".png'," + Environment.NewLine;
            options += TabInString(7) + "show_level: hmUI.show_level." + show_level + "," + Environment.NewLine;
            return options;
        }

        private string IMG_NUMBER_Hour_Options(hmUI_widget_IMG_NUMBER img_number_hour, string show_level)
        {
            string options = Environment.NewLine;
            string img = img_number_hour.img_First;
            int imgPosition = ListImages.IndexOf(img);
            if(imgPosition+10 > ListImages.Count)
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
            return options;
        }

        private string IMG_NUMBER_Minute_Options(hmUI_widget_IMG_NUMBER img_number_minute, string show_level)
        {
            string options = Environment.NewLine;
            string img = img_number_minute.img_First;
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
            return options;
        }

        private string IMG_NUMBER_Second_Options(hmUI_widget_IMG_NUMBER img_number_second, string show_level)
        {
            string options = Environment.NewLine;
            string img = img_number_second.img_First;
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
            }
            if (parametrs.ContainsKey("x") && Int32.TryParse(parametrs["x"], out value)) img.x = value;
            if (parametrs.ContainsKey("y") && Int32.TryParse(parametrs["y"], out value)) img.y = value;
            if (parametrs.ContainsKey("h") && Int32.TryParse(parametrs["h"], out value)) img.h = value;
            if (parametrs.ContainsKey("w") && Int32.TryParse(parametrs["w"], out value)) img.w = value;
            img.visible = true;

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
                //if (parametrs.ContainsKey("second_follow"))
                //{
                //    if (parametrs["second_follow"] == "1") elementDigitalTime.Hour.zero = true;
                //    else elementDigitalTime.Hour.zero = false;
                //}
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
                    elementDigitalTime.Second.unit = parametrs["second_unit_en"].Replace("'", "");
                }
                if (parametrs.ContainsKey("second_follow"))
                {
                    if (parametrs["second_follow"] == "1") elementDigitalTime.Second.follow = true;
                    else elementDigitalTime.Second.follow = false;
                }
                elementDigitalTime.Second.visible = true;
                elementDigitalTime.Second.position = index;
                //index++;
            }

            return elementDigitalTime;
        }
    }
}
