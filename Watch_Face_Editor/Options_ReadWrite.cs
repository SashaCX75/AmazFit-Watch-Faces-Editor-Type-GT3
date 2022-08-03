using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        private WATCH_FACE TextToJson(string text)
        {
            WATCH_FACE Watch_Face_return = null;
            WATCH_FACE Watch_Face_temp = null;
            try
            {
                Watch_Face_temp = JsonConvert.DeserializeObject<WATCH_FACE>(text, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                    Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (Watch_Face_temp == null) return Watch_Face_return;
            
            Watch_Face_return = new WATCH_FACE();
            if (Watch_Face_temp.WatchFace_Info != null) Watch_Face_return.WatchFace_Info = Watch_Face_temp.WatchFace_Info;
            if (Watch_Face_temp.ScreenNormal != null)
            {
                Watch_Face_return.ScreenNormal = new ScreenNormal();
                if (Watch_Face_temp.ScreenNormal.Background != null)
                    Watch_Face_return.ScreenNormal.Background = Watch_Face_temp.ScreenNormal.Background;
                if (Watch_Face_temp.ScreenNormal.Elements != null)
                {
                    Watch_Face_return.ScreenNormal.Elements = new List<object>();
                    List<object> NewElements = ObjectsToElements(Watch_Face_temp.ScreenNormal.Elements);
                    Watch_Face_return.ScreenNormal.Elements = NewElements;
                }
            }

            if (Watch_Face_temp.ScreenAOD != null)
            {
                Watch_Face_return.ScreenAOD = new ScreenAOD();
                if (Watch_Face_temp.ScreenAOD.Background != null)
                    Watch_Face_return.ScreenAOD.Background = Watch_Face_temp.ScreenAOD.Background;
                if (Watch_Face_temp.ScreenAOD.Elements != null)
                {
                    Watch_Face_return.ScreenAOD.Elements = new List<object>();
                    List<object> NewElements = ObjectsToElements(Watch_Face_temp.ScreenAOD.Elements);
                    Watch_Face_return.ScreenAOD.Elements = NewElements;
                }
            }

            return Watch_Face_return;
        }

        /// <summary>Распознаем конкретный тип объекта</summary>
        private List<object> ObjectsToElements(List<object> elements)
        {
            List<object> NewElements = new List<object>();
            foreach (object element in elements)
            {
                string elementStr = element.ToString();
                string type = GetTypeFromSring(elementStr);
                switch (type)
                {
                    #region DigitalTime
                    case "DigitalTime":
                        ElementDigitalTime DigitalTime = null;
                        try
                        {
                            DigitalTime = JsonConvert.DeserializeObject<ElementDigitalTime>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DigitalTime != null) NewElements.Add(DigitalTime);
                        break;
                    #endregion

                    #region AnalogTime
                    case "AnalogTime":
                        ElementAnalogTime AnalogTime = null;
                        try
                        {
                            AnalogTime = JsonConvert.DeserializeObject<ElementAnalogTime>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (AnalogTime != null) NewElements.Add(AnalogTime);
                        break;
                    #endregion

                    #region DateDay
                    case "DateDay":
                        ElementDateDay DateDay = null;
                        try
                        {
                            DateDay = JsonConvert.DeserializeObject<ElementDateDay>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateDay != null) NewElements.Add(DateDay);
                        break;
                    #endregion

                    #region DateMonth
                    case "DateMonth":
                        ElementDateMonth DateMonth = null;
                        try
                        {
                            DateMonth = JsonConvert.DeserializeObject<ElementDateMonth>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateMonth != null) NewElements.Add(DateMonth);
                        break;
                    #endregion

                    #region DateYear
                    case "DateYear":
                        ElementDateYear DateYear = null;
                        try
                        {
                            DateYear = JsonConvert.DeserializeObject<ElementDateYear>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateYear != null) NewElements.Add(DateYear);
                        break;
                    #endregion

                    #region DateWeek
                    case "DateWeek":
                        ElementDateWeek DateWeek = null;
                        try
                        {
                            DateWeek = JsonConvert.DeserializeObject<ElementDateWeek>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateWeek != null) NewElements.Add(DateWeek);
                        break;
                    #endregion

                    #region ElementStatuses
                    case "ElementStatuses":
                        ElementStatuses Statuses = null;
                        try
                        {
                            Statuses = JsonConvert.DeserializeObject<ElementStatuses>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Statuses != null) NewElements.Add(Statuses);
                        break;
                    #endregion

                    #region ElementShortcuts
                    case "ElementShortcuts":
                        ElementShortcuts Shortcuts = null;
                        try
                        {
                            Shortcuts = JsonConvert.DeserializeObject<ElementShortcuts>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Shortcuts != null) NewElements.Add(Shortcuts);
                        break;
                    #endregion

                    #region ElementSteps
                    case "ElementSteps":
                        ElementSteps Steps = null;
                        try
                        {
                            Steps = JsonConvert.DeserializeObject<ElementSteps>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Steps != null) NewElements.Add(Steps);
                        break;
                    #endregion

                    #region ElementBattery
                    case "ElementBattery":
                        ElementBattery Battery = null;
                        try
                        {
                            Battery = JsonConvert.DeserializeObject<ElementBattery>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Battery != null) NewElements.Add(Battery);
                        break;
                    #endregion

                    #region ElementCalories
                    case "ElementCalories":
                        ElementCalories Calories = null;
                        try
                        {
                            Calories = JsonConvert.DeserializeObject<ElementCalories>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Calories != null) NewElements.Add(Calories);
                        break;
                    #endregion

                    #region ElementHeart
                    case "ElementHeart":
                        ElementHeart Heart = null;
                        try
                        {
                            Heart = JsonConvert.DeserializeObject<ElementHeart>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Heart != null) NewElements.Add(Heart);
                        break;
                    #endregion

                    #region ElementPAI
                    case "ElementPAI":
                        ElementPAI PAI = null;
                        try
                        {
                            PAI = JsonConvert.DeserializeObject<ElementPAI>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (PAI != null) NewElements.Add(PAI);
                        break;
                    #endregion

                    #region ElementDistance
                    case "ElementDistance":
                        ElementDistance Distance = null;
                        try
                        {
                            Distance = JsonConvert.DeserializeObject<ElementDistance>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Distance != null) NewElements.Add(Distance);
                        break;
                    #endregion

                    #region ElementStand
                    case "ElementStand":
                        ElementStand Stand = null;
                        try
                        {
                            Stand = JsonConvert.DeserializeObject<ElementStand>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Stand != null) NewElements.Add(Stand);
                        break;
                    #endregion

                    #region ElementActivity
                    case "ElementActivity":
                        ElementActivity Activity = null;
                        try
                        {
                            Activity = JsonConvert.DeserializeObject<ElementActivity>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Activity != null) NewElements.Add(Activity);
                        break;
                    #endregion

                    #region ElementSpO2
                    case "ElementSpO2":
                        ElementSpO2 SpO2 = null;
                        try
                        {
                            SpO2 = JsonConvert.DeserializeObject<ElementSpO2>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (SpO2 != null) NewElements.Add(SpO2);
                        break;
                    #endregion

                    #region ElementStress
                    case "ElementStress":
                        ElementStress Stress = null;
                        try
                        {
                            Stress = JsonConvert.DeserializeObject<ElementStress>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Stress != null) NewElements.Add(Stress);
                        break;
                    #endregion

                    #region ElementFatBurning
                    case "ElementFatBurning":
                        ElementFatBurning FatBurning = null;
                        try
                        {
                            FatBurning = JsonConvert.DeserializeObject<ElementFatBurning>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (FatBurning != null) NewElements.Add(FatBurning);
                        break;
                    #endregion


                    #region ElementWeather
                    case "ElementWeather":
                        ElementWeather Weather = null;
                        try
                        {
                            Weather = JsonConvert.DeserializeObject<ElementWeather>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Weather != null) NewElements.Add(Weather);
                        break;
                    #endregion

                    #region ElementUVIndex
                    case "ElementUVIndex":
                        ElementUVIndex UVIndex = null;
                        try
                        {
                            UVIndex = JsonConvert.DeserializeObject<ElementUVIndex>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (UVIndex != null) NewElements.Add(UVIndex);
                        break;
                    #endregion

                    #region ElementHumidity
                    case "ElementHumidity":
                        ElementHumidity Humidity = null;
                        try
                        {
                            Humidity = JsonConvert.DeserializeObject<ElementHumidity>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Humidity != null) NewElements.Add(Humidity);
                        break;
                    #endregion

                    #region ElementAltimeter
                    case "ElementAltimeter":
                        ElementAltimeter Altimeter = null;
                        try
                        {
                            Altimeter = JsonConvert.DeserializeObject<ElementAltimeter>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Altimeter != null) NewElements.Add(Altimeter);
                        break;
                    #endregion

                    #region ElementSunrise
                    case "ElementSunrise":
                        ElementSunrise Sunrise = null;
                        try
                        {
                            Sunrise = JsonConvert.DeserializeObject<ElementSunrise>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Sunrise != null) NewElements.Add(Sunrise);
                        break;
                    #endregion

                    #region ElementWind
                    case "ElementWind":
                        ElementWind Wind = null;
                        try
                        {
                            Wind = JsonConvert.DeserializeObject<ElementWind>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Wind != null) NewElements.Add(Wind);
                        break;
                    #endregion

                    #region ElementMoon
                    case "ElementMoon":
                        ElementMoon Moon = null;
                        try
                        {
                            Moon = JsonConvert.DeserializeObject<ElementMoon>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Moon != null) NewElements.Add(Moon);
                        break;
                    #endregion


                    #region ElementAnimation
                    case "ElementAnimation":
                        ElementAnimation Animation = null;
                        try
                        {
                            Animation = JsonConvert.DeserializeObject<ElementAnimation>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (Animation != null) NewElements.Add(Animation);
                        break;
                        #endregion
                }
            }
            return NewElements;
        }

        /// <summary>Выделяем тип элемента из строки с параметрами</summary>
        private string GetTypeFromSring(string str)
        {
            string name = "elementName";
            string returnStr = "";
            int indexOf = str.IndexOf("elementName");
            if (indexOf >= 0)
            {
                str = str.Substring(indexOf + name.Length + 2);
                indexOf = str.IndexOf("\"");
                str = str.Substring(indexOf + 1);
                indexOf = str.IndexOf("\"");
                str = str.Substring(0, indexOf);
                returnStr = str;
            }
            return returnStr;
        }

        /// <summary>Читаем настройки для фона</summary>
        private void Read_Background_Options(Background background, string preview = "", int id = 0)
        {
            PreviewView = false;
            userCtrl_Background_Options.SettingsClear();

            if (preview != null && preview.Length > 0) userCtrl_Background_Options.SetPreview(preview);
            if (id > 999 && id < 10000000) userCtrl_Background_Options.SetID(id);
            if (background == null) 
            {
                PreviewView = true;
                return;
            }
            userCtrl_Background_Options.Visible = true;
            if (background == null)
            {
                PreviewView = true;
                return;
            }
            if (background.BackgroundColor != null)
            {
                userCtrl_Background_Options.SetColorBackground(StringToColor(background.BackgroundColor.color));
                userCtrl_Background_Options.Switch_ImageColor(1);
            }
            if (background.BackgroundImage != null)
            {
                userCtrl_Background_Options.SetBackground(background.BackgroundImage.src);
                userCtrl_Background_Options.Switch_ImageColor(0);
            }
            userCtrl_Background_Options._Background = background;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения числа картинками</summary>
        private void Read_ImgNumber_Options(hmUI_widget_IMG_NUMBER img_number, bool _dastance, bool _follow, string _followText,
            bool _imageError, bool _optionalSymbol, bool _padingZero, bool _sunrise = false)
        {
            PreviewView = false;

            uCtrl_Text_Opt.SettingsClear();

            uCtrl_Text_Opt.Distance = _dastance;
            uCtrl_Text_Opt.Sunrise = _sunrise;
            uCtrl_Text_Opt.Follow = _follow;
            uCtrl_Text_Opt.FollowText = _followText;
            uCtrl_Text_Opt.ImageError = _imageError;
            uCtrl_Text_Opt.OptionalSymbol = _optionalSymbol;
            uCtrl_Text_Opt.PaddingZero = _padingZero;
            uCtrl_Text_Opt.Visible = true;

            uCtrl_Text_Opt._ElementWithText = img_number;

            //userCtrl_Background_Options.SettingsClear();

            if (img_number == null)
            {
                PreviewView = true; 
                return; 
            }
            if (img_number.img_First != null) 
                uCtrl_Text_Opt.SetImage(img_number.img_First);
            uCtrl_Text_Opt.numericUpDown_imageX.Value = img_number.imageX;
            uCtrl_Text_Opt.numericUpDown_imageY.Value = img_number.imageY;

            uCtrl_Text_Opt.SetIcon(img_number.icon);
            uCtrl_Text_Opt.numericUpDown_iconX.Value = img_number.iconPosX;
            uCtrl_Text_Opt.numericUpDown_iconY.Value = img_number.iconPosY;

            uCtrl_Text_Opt.SetUnit(img_number.unit);
            uCtrl_Text_Opt.SetUnitMile(img_number.imperial_unit);
            uCtrl_Text_Opt.SetImageError(img_number.invalid_image);
            uCtrl_Text_Opt.SetImageDecimalPoint(img_number.dot_image);
            //uCtrl_Text_Opt.SetImageDecimalPointOrMinus
            uCtrl_Text_Opt.numericUpDown_spacing.Value = img_number.space;

            uCtrl_Text_Opt.SetAlignment(img_number.align);
            //uCtrl_Text_Opt.SetImageError

            uCtrl_Text_Opt.checkBox_addZero.Checked = img_number.zero;
            uCtrl_Text_Opt.checkBox_follow.Checked = img_number.follow;

            //uCtrl_Text_Opt.SetUnitMile

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения температуры</summary>
        private void Read_ImgNumberWeather_Options(hmUI_widget_IMG_NUMBER img_number, bool _follow, string _followText,
            bool _imageError, bool _padingZero)
        {
            PreviewView = false;

            uCtrl_Text_Weather_Opt.SettingsClear();

            uCtrl_Text_Weather_Opt.Follow = _follow;
            uCtrl_Text_Weather_Opt.FollowText = _followText;
            uCtrl_Text_Weather_Opt.ImageError = _imageError;
            uCtrl_Text_Weather_Opt.PaddingZero = _padingZero;
            uCtrl_Text_Weather_Opt.Visible = true;

            uCtrl_Text_Weather_Opt._ElementWithText = img_number;

            //userCtrl_Background_Options.SettingsClear();

            if (img_number == null)
            {
                PreviewView = true;
                return;
            }
            if (img_number.img_First != null)
                uCtrl_Text_Weather_Opt.SetImage(img_number.img_First);
            uCtrl_Text_Weather_Opt.numericUpDown_imageX.Value = img_number.imageX;
            uCtrl_Text_Weather_Opt.numericUpDown_imageY.Value = img_number.imageY;

            uCtrl_Text_Weather_Opt.SetIcon(img_number.icon);
            uCtrl_Text_Weather_Opt.numericUpDown_iconX.Value = img_number.iconPosX;
            uCtrl_Text_Weather_Opt.numericUpDown_iconY.Value = img_number.iconPosY;

            uCtrl_Text_Weather_Opt.SetUnit_C(img_number.unit);
            uCtrl_Text_Weather_Opt.SetUnit_F(img_number.imperial_unit);
            uCtrl_Text_Weather_Opt.SetImageError(img_number.invalid_image);
            uCtrl_Text_Weather_Opt.SetImageMinus(img_number.negative_image);
            uCtrl_Text_Weather_Opt.numericUpDown_spacing.Value = img_number.space;

            uCtrl_Text_Weather_Opt.SetAlignment(img_number.align);

            uCtrl_Text_Weather_Opt.checkBox_addZero.Checked = img_number.zero;
            uCtrl_Text_Weather_Opt.checkBox_follow.Checked = img_number.follow;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения иконки</summary>
        private void Read_Icon_Options(hmUI_widget_IMG img)
        {
            PreviewView = false;

            uCtrl_Icon_Opt.SettingsClear();

            uCtrl_Icon_Opt._Icon = img;

            //userCtrl_Background_Options.SettingsClear();

            if (img == null)
            {
                PreviewView = true;
                return;
            }

            if (img.src != null)
                uCtrl_Icon_Opt.SetIcon(img.src);
            uCtrl_Icon_Opt.numericUpDown_iconX.Value = img.x;
            uCtrl_Icon_Opt.numericUpDown_iconY.Value = img.y;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения статусов</summary>
        private void Read_Statuses_Options(hmUI_widget_IMG_STATUS img_status)
        {
            PreviewView = false;

            uCtrl_Icon_Opt.SettingsClear();

            uCtrl_Icon_Opt._Icon = img_status;

            //userCtrl_Background_Options.SettingsClear();

            if (img_status == null)
            {
                PreviewView = true;
                return;
            }

            if (img_status.src != null)
                uCtrl_Icon_Opt.SetIcon(img_status.src);
            uCtrl_Icon_Opt.numericUpDown_iconX.Value = img_status.x;
            uCtrl_Icon_Opt.numericUpDown_iconY.Value = img_status.y;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения статусов</summary>
        private void Read_Shortcuts_Options(hmUI_widget_IMG_CLICK img_click)
        {
            PreviewView = false;

            uCtrl_Shortcut_Opt.SettingsClear();

            uCtrl_Shortcut_Opt._Shortcut = img_click;

            //userCtrl_Background_Options.SettingsClear();

            if (img_click == null)
            {
                PreviewView = true;
                return;
            }

            if (img_click.src != null)
                uCtrl_Shortcut_Opt.SetImage(img_click.src);
            uCtrl_Shortcut_Opt.numericUpDown_imageX.Value = img_click.x;
            uCtrl_Shortcut_Opt.numericUpDown_imageY.Value = img_click.y;
            uCtrl_Shortcut_Opt.numericUpDown_width.Value = img_click.w;
            uCtrl_Shortcut_Opt.numericUpDown_height.Value = img_click.h;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения покадровой анимации</summary>
        private void Read_FrameAnimation_Options(hmUI_widget_IMG_ANIM_List frame_animation_list)
        {
            PreviewView = false;

            uCtrl_Animation_Frame_Opt.SettingsClear();

            uCtrl_Animation_Frame_Opt._AnimationFrame = frame_animation_list;

            //userCtrl_Background_Options.SettingsClear();

            if (frame_animation_list == null || frame_animation_list.Frame_Animation == null)
            {
                uCtrl_Animation_Frame_Opt.SetAnimationCount(0);
                PreviewView = true;
                return;
            }

            uCtrl_Animation_Frame_Opt.SetAnimationCount(frame_animation_list.Frame_Animation.Count);
            int selected_animation = frame_animation_list.selected_animation;
            if (selected_animation < 0 || selected_animation > frame_animation_list.Frame_Animation.Count-1)
            {
                PreviewView = true;
                return;
            }

            hmUI_widget_IMG_ANIM frame_animation = frame_animation_list.Frame_Animation[selected_animation];
            //uCtrl_Animation_Frame_Opt.SetAnimationCount(frame_animation_list.Frame_Animation.Count);
            uCtrl_Animation_Frame_Opt.SetAnimationIndex(selected_animation);
            if (frame_animation.anim_src != null)
                uCtrl_Animation_Frame_Opt.SetImage(frame_animation.anim_src);
            uCtrl_Animation_Frame_Opt.numericUpDown_imageX.Value = frame_animation.x;
            uCtrl_Animation_Frame_Opt.numericUpDown_imageY.Value = frame_animation.y;
            uCtrl_Animation_Frame_Opt.numericUpDown_images_count.Value = frame_animation.anim_size;

            uCtrl_Animation_Frame_Opt.numericUpDown_fps.Value = frame_animation.anim_fps;
            uCtrl_Animation_Frame_Opt.checkBox_anim_repeat.Checked = frame_animation.anim_repeat;
            //uCtrl_Animation_Frame_Opt.checkBox_anim_restart.Checked = frame_animation.display_on_restart;

            uCtrl_Animation_Frame_Opt.label_prefix.Text = frame_animation.anim_prefix;
            uCtrl_Animation_Frame_Opt.checkBox_visible.Checked = frame_animation.visible;
            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения анимации движения</summary>
        private void Read_MotionAnimation_Options(Motion_Animation_List motion_animation_list)
        {
            PreviewView = false;

            uCtrl_Animation_Motion_Opt.SettingsClear();

            uCtrl_Animation_Motion_Opt._AnimationMotion = motion_animation_list;

            //userCtrl_Background_Options.SettingsClear();

            if (motion_animation_list == null || motion_animation_list.Motion_Animation == null)
            {
                uCtrl_Animation_Motion_Opt.SetAnimationCount(0);
                PreviewView = true;
                return;
            }

            uCtrl_Animation_Motion_Opt.SetAnimationCount(motion_animation_list.Motion_Animation.Count);
            int selected_animation = motion_animation_list.selected_animation;
            if (selected_animation < 0 || selected_animation > motion_animation_list.Motion_Animation.Count - 1)
            {
                PreviewView = true;
                return;
            }

            Motion_Animation motion_animation = motion_animation_list.Motion_Animation[selected_animation];
            uCtrl_Animation_Motion_Opt.SetAnimationIndex(selected_animation);
            if (motion_animation.src != null)
                uCtrl_Animation_Motion_Opt.SetImage(motion_animation.src);
            uCtrl_Animation_Motion_Opt.numericUpDown_start_x.Value = motion_animation.x_start;
            uCtrl_Animation_Motion_Opt.numericUpDown_start_y.Value = motion_animation.y_start;
            uCtrl_Animation_Motion_Opt.numericUpDown_end_x.Value = motion_animation.x_end;
            uCtrl_Animation_Motion_Opt.numericUpDown_end_y.Value = motion_animation.y_end;

            uCtrl_Animation_Motion_Opt.numericUpDown_fps.Value = motion_animation.anim_fps;
            uCtrl_Animation_Motion_Opt.numericUpDown_anim_duration.Value = (decimal)(motion_animation.anim_duration/1000f);
            uCtrl_Animation_Motion_Opt.numericUpDown_repeat_count.Value = motion_animation.repeat_count;

            uCtrl_Animation_Motion_Opt.checkBox_anim_two_sides.Checked = motion_animation.anim_two_sides;
            uCtrl_Animation_Motion_Opt.checkBox_show_in_startPos.Checked = motion_animation.show_in_start;
            uCtrl_Animation_Motion_Opt.checkBox_visible.Checked = motion_animation.visible;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения анимации движения </summary>
        private void Read_RotateAnimation_Options(Rotate_Animation_List rotate_animation_list)
        {
            PreviewView = false;

            uCtrl_Animation_Rotate_Opt.SettingsClear();

            uCtrl_Animation_Rotate_Opt._AnimationRotate = rotate_animation_list;

            //userCtrl_Background_Options.SettingsClear();

            if (rotate_animation_list == null || rotate_animation_list.Rotate_Animation == null)
            {
                uCtrl_Animation_Rotate_Opt.SetAnimationCount(0);
                PreviewView = true;
                return;
            }

            uCtrl_Animation_Rotate_Opt.SetAnimationCount(rotate_animation_list.Rotate_Animation.Count);
            int selected_animation = rotate_animation_list.selected_animation;
            if (selected_animation < 0 || selected_animation > rotate_animation_list.Rotate_Animation.Count - 1)
            {
                PreviewView = true;
                return;
            }

            Rotate_Animation rotate_animation = rotate_animation_list.Rotate_Animation[selected_animation];
            uCtrl_Animation_Rotate_Opt.SetAnimationIndex(selected_animation);
            if (rotate_animation.src != null)
                uCtrl_Animation_Rotate_Opt.SetImage(rotate_animation.src);
            uCtrl_Animation_Rotate_Opt.numericUpDown_pos_x.Value = rotate_animation.pos_x;
            uCtrl_Animation_Rotate_Opt.numericUpDown_pos_y.Value = rotate_animation.pos_y;
            uCtrl_Animation_Rotate_Opt.numericUpDown_center_x.Value = rotate_animation.center_x;
            uCtrl_Animation_Rotate_Opt.numericUpDown_center_y.Value = rotate_animation.center_y;
            uCtrl_Animation_Rotate_Opt.numericUpDown_start_angle.Value = rotate_animation.start_angle;
            uCtrl_Animation_Rotate_Opt.numericUpDown_end_angle.Value = rotate_animation.end_angle;

            uCtrl_Animation_Rotate_Opt.numericUpDown_fps.Value = rotate_animation.anim_fps;
            uCtrl_Animation_Rotate_Opt.numericUpDown_anim_duration.Value = (decimal)(rotate_animation.anim_duration/1000f);
            uCtrl_Animation_Rotate_Opt.numericUpDown_repeat_count.Value = rotate_animation.repeat_count;

            uCtrl_Animation_Rotate_Opt.checkBox_anim_two_sides.Checked = rotate_animation.anim_two_sides;
            uCtrl_Animation_Rotate_Opt.checkBox_show_in_startPos.Checked = rotate_animation.show_in_start;
            uCtrl_Animation_Rotate_Opt.checkBox_visible.Checked = rotate_animation.visible;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения стрелочного указателя</summary>
        private void Read_ImgPointer_Options(hmUI_widget_IMG_POINTER img_pointer, bool _showBackground)
        {
            PreviewView = false;

            uCtrl_Pointer_Opt.SettingsClear();

            uCtrl_Pointer_Opt.ShowBackground = _showBackground;
            uCtrl_Pointer_Opt.Visible = true;

            uCtrl_Pointer_Opt._ElementWithPointer = img_pointer;

            //userCtrl_Background_Options.SettingsClear();

            if (img_pointer == null)
            {
                PreviewView = true;
                return;
            }
            if (img_pointer.src != null)
                uCtrl_Pointer_Opt.SetPointerImage(img_pointer.src);
            uCtrl_Pointer_Opt.numericUpDown_pointer_X.Value = img_pointer.center_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_Y.Value = img_pointer.center_y;
            uCtrl_Pointer_Opt.numericUpDown_pointer_offset_X.Value = img_pointer.pos_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_offset_Y.Value = img_pointer.pos_y;

            if (img_pointer.cover_path != null)
                uCtrl_Pointer_Opt.SetPointerImageCentr(img_pointer.cover_path);
            uCtrl_Pointer_Opt.numericUpDown_pointer_centr_X.Value = img_pointer.cover_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_centr_Y.Value = img_pointer.cover_y;

            if (img_pointer.scale != null)
                uCtrl_Pointer_Opt.SetPointerImageBackground(img_pointer.scale);
            uCtrl_Pointer_Opt.numericUpDown_pointer_background_X.Value = img_pointer.scale_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_background_Y.Value = img_pointer.scale_y;

            uCtrl_Pointer_Opt.numericUpDown_pointer_startAngle.Value = img_pointer.start_angle;
            uCtrl_Pointer_Opt.numericUpDown_pointer_endAngle.Value = img_pointer.end_angle;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения круговой шкалы</summary>
        private void Read_CircleScale_Options(Circle_Scale circle_scale)
        {
            PreviewView = false;

            uCtrl_Circle_Scale_Opt.SettingsClear();

            uCtrl_Circle_Scale_Opt.Visible = true;

            uCtrl_Circle_Scale_Opt._CircleScale = circle_scale;

            if (circle_scale == null)
            {
                PreviewView = true;
                return;
            }
            uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircleX.Value = circle_scale.center_x;
            uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircleY.Value = circle_scale.center_y;
            uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_radius.Value = circle_scale.radius;
            uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_width.Value = circle_scale.line_width;

            uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_startAngle.Value = circle_scale.start_angle;
            uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_endAngle.Value = circle_scale.end_angle;

            uCtrl_Circle_Scale_Opt.SetColorScale(StringToColor(circle_scale.color));

            uCtrl_Circle_Scale_Opt.checkBox_mirror.Checked = circle_scale.mirror;
            uCtrl_Circle_Scale_Opt.checkBox_inversion.Checked = circle_scale.inversion;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения линейной шкалы</summary>
        private void Read_LinearScale_Options(Linear_Scale linear_scale)
        {
            PreviewView = false;

            uCtrl_Linear_Scale_Opt.SettingsClear();

            uCtrl_Linear_Scale_Opt.Visible = true;

            uCtrl_Linear_Scale_Opt._LinearScale = linear_scale;

            if (linear_scale == null)
            {
                PreviewView = true;
                return;
            }

            if (linear_scale.color != null)
            {
                uCtrl_Linear_Scale_Opt.SetColorScale(StringToColor(linear_scale.color));
            }
            if (linear_scale.pointer != null)
            {
                uCtrl_Linear_Scale_Opt.SetImagePointer(linear_scale.pointer);
            }

            uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinearX.Value = linear_scale.start_x;
            uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinearY.Value = linear_scale.start_y;

            uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinear_length.Value = linear_scale.lenght;
            uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinear_width.Value = linear_scale.line_width;

            uCtrl_Linear_Scale_Opt.SetColorScale(StringToColor(linear_scale.color));

            uCtrl_Linear_Scale_Opt.checkBox_mirror.Checked = linear_scale.mirror;
            uCtrl_Linear_Scale_Opt.checkBox_inversion.Checked = linear_scale.inversion;

            uCtrl_Linear_Scale_Opt.radioButton_horizontal.Checked = !linear_scale.vertical;
            uCtrl_Linear_Scale_Opt.radioButton_vertical.Checked = linear_scale.vertical;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения набора картинок</summary>
        private void Read_ImgLevel_Options(hmUI_widget_IMG_LEVEL img_level, int imagesCount, bool imagesCountEnable)
        {
            PreviewView = false;

            uCtrl_Images_Opt.SettingsClear();

            uCtrl_Images_Opt.ImagesCount = imagesCount;
            uCtrl_Images_Opt.ImagesCountEnable = imagesCountEnable;

            uCtrl_Images_Opt._ElementWithImages = img_level;

            uCtrl_Images_Opt.Visible = true;

            //userCtrl_Background_Options.SettingsClear();

            if (img_level == null)
            {
                PreviewView = true;
                return;
            }
            if (img_level.img_First != null)
                uCtrl_Images_Opt.SetImage(img_level.img_First);
            uCtrl_Images_Opt.numericUpDown_imageX.Value = img_level.X;
            uCtrl_Images_Opt.numericUpDown_imageY.Value = img_level.Y;
            if (img_level.image_length > 0)
                uCtrl_Images_Opt.numericUpDown_pictures_count.Value = img_level.image_length;
            if (!imagesCountEnable) uCtrl_Images_Opt.numericUpDown_pictures_count.Value = imagesCount;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения картинок сегментами</summary>
        private void Read_ImgProrgess_Options(hmUI_widget_IMG_PROGRESS img_prorgess, int imagesCount, bool fixedRowsCount)
        {
            PreviewView = false;

            uCtrl_Segments_Opt.SettingsClear();

            uCtrl_Segments_Opt.ImagesCount = imagesCount;
            uCtrl_Segments_Opt.FixedRowsCount = fixedRowsCount;
            uCtrl_Segments_Opt.UpdateTable();

            uCtrl_Segments_Opt._ElementWithSegments = img_prorgess;

            uCtrl_Segments_Opt.Visible = true;

            //userCtrl_Background_Options.SettingsClear();

            if (img_prorgess == null)
            {
                PreviewView = true;
                return;
            }
            if (img_prorgess.img_First != null)
                uCtrl_Segments_Opt.SetImage(img_prorgess.img_First);

            uCtrl_Segments_Opt.SetCoordinates(img_prorgess.X, img_prorgess.Y);

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения AM PM</summary>
        private void Read_AM_PM_Options(hmUI_widget_IMG_TIME_am_pm am_pm)
        {
            PreviewView = false;

            uCtrl_AmPm_Opt.SettingsClear();

            uCtrl_AmPm_Opt._AmPm = am_pm;

            if (am_pm == null)
            {
                PreviewView = true;
                return;
            }
            if (am_pm.am_img != null)
                uCtrl_AmPm_Opt.Set_AM_Image(am_pm.am_img);
            uCtrl_AmPm_Opt.numericUpDown_AM_X.Value = am_pm.am_x;
            uCtrl_AmPm_Opt.numericUpDown_AM_Y.Value = am_pm.am_y;

            if (am_pm.pm_img != null)
                uCtrl_AmPm_Opt.Set_PM_Image(am_pm.pm_img);
            uCtrl_AmPm_Opt.numericUpDown_PM_X.Value = am_pm.pm_x;
            uCtrl_AmPm_Opt.numericUpDown_PM_Y.Value = am_pm.pm_y;

            PreviewView = true;
        }

        private void Read_Text_Options(hmUI_widget_TEXT system_font)
        {
            PreviewView = false;

            uCtrl_Text_SystemFont_Opt.SettingsClear();

            uCtrl_Text_SystemFont_Opt._ElementWithSystemFont = system_font;

            uCtrl_Text_SystemFont_Opt.numericUpDown_X.Value = system_font.x;
            uCtrl_Text_SystemFont_Opt.numericUpDown_Y.Value = system_font.y;
            uCtrl_Text_SystemFont_Opt.numericUpDown_Width.Value = system_font.w;
            uCtrl_Text_SystemFont_Opt.numericUpDown_Height.Value = system_font.h;

            uCtrl_Text_SystemFont_Opt.numericUpDown_Size.Value = system_font.text_size;
            uCtrl_Text_SystemFont_Opt.numericUpDown_Spacing.Value = system_font.char_space;
            uCtrl_Text_SystemFont_Opt.numericUpDown_LineSpace.Value = system_font.line_space;

            uCtrl_Text_SystemFont_Opt.SetColorText(StringToColor(system_font.color));

            uCtrl_Text_SystemFont_Opt.SetHorizontalAlignment(system_font.align_h);
            uCtrl_Text_SystemFont_Opt.SetVerticalAlignment(system_font.align_v);
            uCtrl_Text_SystemFont_Opt.SetTextStyle(system_font.text_style);

            PreviewView = true;
        }

        /// <summary>Меняем настройки фона</summary>
        private void userCtrl_Background_Options_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            Background background = (Background)userCtrl_Background_Options._Background;
            string backgroundImg = userCtrl_Background_Options.GetBackground();
            string preview = userCtrl_Background_Options.GetPreview();
            if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
            if (preview.Length > 0) Watch_Face.WatchFace_Info.Preview = preview;
            else Watch_Face.WatchFace_Info.Preview = null;
            Watch_Face.WatchFace_Info.WatchFaceId = userCtrl_Background_Options.GetID();

            if (userCtrl_Background_Options.radioButton_Background_image.Checked)
            {
                if (backgroundImg.Length > 0)
                {
                    if (background == null) 
                    {
                        if (radioButton_ScreenNormal.Checked)
                        {
                            if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                            if (Watch_Face.ScreenNormal.Background == null)
                                Watch_Face.ScreenNormal.Background = new Background();
                            background = Watch_Face.ScreenNormal.Background;
                        }
                        else
                        {
                            if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                            if (Watch_Face.ScreenAOD.Background == null)
                                Watch_Face.ScreenAOD.Background = new Background();
                            background = Watch_Face.ScreenAOD.Background;
                        }
                    }

                    //background = new Background();
                    if (background.BackgroundImage == null)
                        background.BackgroundImage = new hmUI_widget_IMG();
                    background.BackgroundImage.src = backgroundImg;
                    background.BackgroundImage.x = 0;
                    background.BackgroundImage.y = 0;
                    background.BackgroundImage.h = 454;
                    background.BackgroundImage.w = 454;
                    if (Form_Preview.Watch_Model == "GTR 3 Pro")
                    {
                        background.BackgroundImage.h = 480;
                        background.BackgroundImage.w = 480;
                    }
                    if (Form_Preview.Watch_Model == "GTS 3")
                    {
                        background.BackgroundImage.h = 450;
                        background.BackgroundImage.w = 390;
                    }
                    //background.BackgroundImage.show_level = "ONLY_NORMAL";
                    background.BackgroundColor = null;
                }
            }
            else
            {
                if (background == null)
                {
                    if (radioButton_ScreenNormal.Checked)
                    {
                        if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                        if (Watch_Face.ScreenNormal.Background == null)
                            Watch_Face.ScreenNormal.Background = new Background();
                        background = Watch_Face.ScreenNormal.Background;
                    }
                    else
                    {
                        if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                        if (Watch_Face.ScreenAOD.Background == null)
                            Watch_Face.ScreenAOD.Background = new Background();
                        background = Watch_Face.ScreenAOD.Background;
                    }
                }

                if (background.BackgroundColor == null)
                    background.BackgroundColor = new hmUI_widget_FILL_RECT();
                background.BackgroundColor.color = ColorToString(userCtrl_Background_Options.GetColorBackground());
                background.BackgroundColor.x = 0;
                background.BackgroundColor.y = 0;
                background.BackgroundColor.h = 454;
                background.BackgroundColor.w = 454;
                if (ProgramSettings.Watch_Model == "GTR 3 Pro")
                {
                    background.BackgroundColor.h = 480;
                    background.BackgroundColor.w = 480;
                }
                if (ProgramSettings.Watch_Model == "GTS 3")
                {
                    background.BackgroundColor.h = 450;
                    background.BackgroundColor.w = 390;
                }
                background.BackgroundImage = null;
            }
            background.visible = userCtrl_Background_Options.Visible;

            JSON_Modified = true;
            PreviewImage();
            FormText();

            // отображение кнопок создания картинки предпросмотра
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null && Watch_Face.WatchFace_Info.Preview != null)
            {
                button_RefreshPreview.Visible = true;
                button_CreatePreview.Visible = false;
            }
            else
            {
                button_RefreshPreview.Visible = false;
                if (FileName != null && FullFileDir != null)
                {
                    button_CreatePreview.Visible = true;
                }
                else
                {
                    button_CreatePreview.Visible = false;
                }
            }
        }

        private void uCtrl_Text_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_NUMBER img_number = (hmUI_widget_IMG_NUMBER)uCtrl_Text_Opt._ElementWithText;
            if (img_number == null) return;

            img_number.align = uCtrl_Text_Opt.GetAlignment();
            img_number.follow = uCtrl_Text_Opt.checkBox_follow.Checked;
            img_number.icon = uCtrl_Text_Opt.GetIcon();
            img_number.iconPosX = (int)uCtrl_Text_Opt.numericUpDown_iconX.Value;
            img_number.iconPosY = (int)uCtrl_Text_Opt.numericUpDown_iconY.Value;
            img_number.img_First = uCtrl_Text_Opt.GetImage();
            img_number.imageX = (int)uCtrl_Text_Opt.numericUpDown_imageX.Value;
            img_number.imageY = (int)uCtrl_Text_Opt.numericUpDown_imageY.Value;
            img_number.space = (int)uCtrl_Text_Opt.numericUpDown_spacing.Value;
            img_number.unit = uCtrl_Text_Opt.GetUnit();
            img_number.imperial_unit = uCtrl_Text_Opt.GetUnitMile();
            img_number.dot_image = uCtrl_Text_Opt.GetImageDecimalPoint();
            img_number.invalid_image = uCtrl_Text_Opt.GetImageError();
            img_number.zero = uCtrl_Text_Opt.checkBox_addZero.Checked;


            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Text_Weather_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_NUMBER img_number = (hmUI_widget_IMG_NUMBER)uCtrl_Text_Weather_Opt._ElementWithText;
            if (img_number == null) return;

            img_number.align = uCtrl_Text_Weather_Opt.GetAlignment();
            img_number.follow = uCtrl_Text_Weather_Opt.checkBox_follow.Checked;
            img_number.icon = uCtrl_Text_Weather_Opt.GetIcon();
            img_number.iconPosX = (int)uCtrl_Text_Weather_Opt.numericUpDown_iconX.Value;
            img_number.iconPosY = (int)uCtrl_Text_Weather_Opt.numericUpDown_iconY.Value;
            img_number.img_First = uCtrl_Text_Weather_Opt.GetImage();
            img_number.imageX = (int)uCtrl_Text_Weather_Opt.numericUpDown_imageX.Value;
            img_number.imageY = (int)uCtrl_Text_Weather_Opt.numericUpDown_imageY.Value;
            img_number.space = (int)uCtrl_Text_Weather_Opt.numericUpDown_spacing.Value;
            img_number.unit = uCtrl_Text_Weather_Opt.GetUnit_C();
            img_number.imperial_unit = uCtrl_Text_Weather_Opt.GetUnit_F();
            img_number.negative_image = uCtrl_Text_Weather_Opt.GetImageMinus();
            img_number.invalid_image = uCtrl_Text_Weather_Opt.GetImageError();
            img_number.zero = uCtrl_Text_Weather_Opt.checkBox_addZero.Checked;


            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_AmPm_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_TIME_am_pm am_pm = (hmUI_widget_IMG_TIME_am_pm)uCtrl_AmPm_Opt._AmPm;
            if (am_pm == null) return;

            am_pm.am_img = uCtrl_AmPm_Opt.Get_AM_Image();
            am_pm.am_x = (int)uCtrl_AmPm_Opt.numericUpDown_AM_X.Value;
            am_pm.am_y = (int)uCtrl_AmPm_Opt.numericUpDown_AM_Y.Value;

            am_pm.pm_img = uCtrl_AmPm_Opt.Get_PM_Image();
            am_pm.pm_x = (int)uCtrl_AmPm_Opt.numericUpDown_PM_X.Value;
            am_pm.pm_y = (int)uCtrl_AmPm_Opt.numericUpDown_PM_Y.Value;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Pointer_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_POINTER img_pointer = (hmUI_widget_IMG_POINTER)uCtrl_Pointer_Opt._ElementWithPointer;
            if (img_pointer == null) return;

            img_pointer.src = uCtrl_Pointer_Opt.GetPointerImage();
            img_pointer.center_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_X.Value;
            img_pointer.center_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_Y.Value;

            img_pointer.pos_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_offset_X.Value;
            img_pointer.pos_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_offset_Y.Value;

            img_pointer.start_angle = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_startAngle.Value;
            img_pointer.end_angle = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_endAngle.Value;

            img_pointer.cover_path = uCtrl_Pointer_Opt.GetPointerImageCentr();
            img_pointer.cover_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_centr_X.Value;
            img_pointer.cover_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_centr_Y.Value;

            img_pointer.scale = uCtrl_Pointer_Opt.GetPointerImageBackground();
            img_pointer.scale_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_background_X.Value;
            img_pointer.scale_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_background_Y.Value;


            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Images_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_LEVEL img_level = (hmUI_widget_IMG_LEVEL)uCtrl_Images_Opt._ElementWithImages;
            if (img_level == null) return;

            img_level.img_First = uCtrl_Images_Opt.GetImage();
            img_level.X = (int)uCtrl_Images_Opt.numericUpDown_imageX.Value;
            img_level.Y = (int)uCtrl_Images_Opt.numericUpDown_imageY.Value;
            img_level.image_length = (int)uCtrl_Images_Opt.numericUpDown_pictures_count.Value;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Segments_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_PROGRESS img_progress = (hmUI_widget_IMG_PROGRESS)uCtrl_Segments_Opt._ElementWithSegments;
            if (img_progress == null) return;

            img_progress.img_First = uCtrl_Segments_Opt.GetImage();

            List<int> coordinatesX = new List<int>();
            List<int> coordinatesY = new List<int>();
            uCtrl_Segments_Opt.GetCoordinates(out coordinatesX, out coordinatesY);
            img_progress.X = coordinatesX;
            img_progress.Y = coordinatesY;
            int image_length = 0;
            if (coordinatesX != null) image_length = coordinatesX.Count;
            img_progress.image_length = image_length;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Circle_Scale_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            Circle_Scale circle_scale = (Circle_Scale)uCtrl_Circle_Scale_Opt._CircleScale;
            if (circle_scale == null) return;

            circle_scale.center_x = (int)uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircleX.Value;
            circle_scale.center_y = (int)uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircleY.Value;

            circle_scale.radius = (int)uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_radius.Value;
            circle_scale.line_width = (int)uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_width.Value;

            circle_scale.start_angle = (int)uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_startAngle.Value;
            circle_scale.end_angle = (int)uCtrl_Circle_Scale_Opt.numericUpDown_scaleCircle_endAngle.Value;

            circle_scale.color = ColorToString(uCtrl_Circle_Scale_Opt.GetColorScale());

            circle_scale.mirror = uCtrl_Circle_Scale_Opt.checkBox_mirror.Checked;
            circle_scale.inversion = uCtrl_Circle_Scale_Opt.checkBox_inversion.Checked;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Linear_Scale_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            Linear_Scale linear_scale = (Linear_Scale)uCtrl_Linear_Scale_Opt._LinearScale;
            if (linear_scale == null) return;

            linear_scale.pointer = uCtrl_Linear_Scale_Opt.GetImagePointer();
            linear_scale.color = ColorToString(uCtrl_Linear_Scale_Opt.GetColorScale());

            linear_scale.start_x = (int)uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinearX.Value;
            linear_scale.start_y = (int)uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinearY.Value;

            linear_scale.lenght = (int)uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinear_length.Value;
            linear_scale.line_width = (int)uCtrl_Linear_Scale_Opt.numericUpDown_scaleLinear_width.Value;

            linear_scale.vertical = uCtrl_Linear_Scale_Opt.radioButton_vertical.Checked;

            linear_scale.mirror = uCtrl_Linear_Scale_Opt.checkBox_mirror.Checked;
            linear_scale.inversion = uCtrl_Linear_Scale_Opt.checkBox_inversion.Checked;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Icon_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            string type = uCtrl_Icon_Opt._Icon.GetType().Name;
            if (type == "hmUI_widget_IMG")
            {
                hmUI_widget_IMG icon = (hmUI_widget_IMG)uCtrl_Icon_Opt._Icon;
                if (icon == null) return;

                icon.src = uCtrl_Icon_Opt.GetIcon();
                icon.x = (int)uCtrl_Icon_Opt.numericUpDown_iconX.Value;
                icon.y = (int)uCtrl_Icon_Opt.numericUpDown_iconY.Value; 
            }
            else if (type == "hmUI_widget_IMG_STATUS")
            {
                hmUI_widget_IMG_STATUS status = (hmUI_widget_IMG_STATUS)uCtrl_Icon_Opt._Icon;
                if (status == null) return;

                status.src = uCtrl_Icon_Opt.GetIcon();
                status.x = (int)uCtrl_Icon_Opt.numericUpDown_iconX.Value;
                status.y = (int)uCtrl_Icon_Opt.numericUpDown_iconY.Value;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Shortcut_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_CLICK shortcut = (hmUI_widget_IMG_CLICK)uCtrl_Shortcut_Opt._Shortcut;
            if (shortcut == null) return;

            shortcut.src = uCtrl_Shortcut_Opt.GetImage();
            shortcut.x = (int)uCtrl_Shortcut_Opt.numericUpDown_imageX.Value;
            shortcut.y = (int)uCtrl_Shortcut_Opt.numericUpDown_imageY.Value;
            shortcut.h = (int)uCtrl_Shortcut_Opt.numericUpDown_height.Value;
            shortcut.w = (int)uCtrl_Shortcut_Opt.numericUpDown_width.Value;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Text_SystemFont_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_TEXT systemFont = (hmUI_widget_TEXT)uCtrl_Text_SystemFont_Opt._ElementWithSystemFont;
            if (systemFont == null) return;

            systemFont.x = (int)uCtrl_Text_SystemFont_Opt.numericUpDown_X.Value;
            systemFont.y = (int)uCtrl_Text_SystemFont_Opt.numericUpDown_Y.Value;
            systemFont.h = (int)uCtrl_Text_SystemFont_Opt.numericUpDown_Height.Value;
            systemFont.w = (int)uCtrl_Text_SystemFont_Opt.numericUpDown_Width.Value;

            systemFont.text_size = (int)uCtrl_Text_SystemFont_Opt.numericUpDown_Size.Value;
            systemFont.char_space = (int)uCtrl_Text_SystemFont_Opt.numericUpDown_Spacing.Value;
            systemFont.line_space = (int)uCtrl_Text_SystemFont_Opt.numericUpDown_LineSpace.Value;

            systemFont.color = ColorToString(uCtrl_Text_SystemFont_Opt.GetColorText());

            systemFont.align_h = uCtrl_Text_SystemFont_Opt.GetHorizontalAlignment();
            systemFont.align_v = uCtrl_Text_SystemFont_Opt.GetVerticalAlignment();
            systemFont.text_style = uCtrl_Text_SystemFont_Opt.GetTextStyle();

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Frame_Opt_ValueChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            hmUI_widget_IMG_ANIM_List animation_frame = (hmUI_widget_IMG_ANIM_List)uCtrl_Animation_Frame_Opt._AnimationFrame;
            if (animation_frame == null) return;

            if (animation_frame.Frame_Animation == null) animation_frame.Frame_Animation = new List<hmUI_widget_IMG_ANIM>();
            List<hmUI_widget_IMG_ANIM> frameAnimation = animation_frame.Frame_Animation;
            if (frameAnimation == null) return;
            if (frameAnimation.Count < index + 1) return;

            hmUI_widget_IMG_ANIM img_anim = new hmUI_widget_IMG_ANIM();

            img_anim.visible = uCtrl_Animation_Frame_Opt.checkBox_visible.Checked;
            img_anim.x = (int)uCtrl_Animation_Frame_Opt.numericUpDown_imageX.Value;
            img_anim.y = (int)uCtrl_Animation_Frame_Opt.numericUpDown_imageY.Value;

            img_anim.anim_src = uCtrl_Animation_Frame_Opt.GetImage();
            img_anim.anim_prefix = uCtrl_Animation_Frame_Opt.GetPrefix();
            img_anim.anim_fps = (int)uCtrl_Animation_Frame_Opt.numericUpDown_fps.Value;
            img_anim.anim_size = (int)uCtrl_Animation_Frame_Opt.numericUpDown_images_count.Value;
            img_anim.anim_repeat = uCtrl_Animation_Frame_Opt.checkBox_anim_repeat.Checked;

            //rotate_anim.display_on_restart = uCtrl_Animation_Frame_Opt.checkBox_anim_restart.Checked;

            if (index + 1 <= frameAnimation.Count && index >= 0) frameAnimation[index] = img_anim;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Motion_Opt_ValueChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Motion_Animation_List animation_motion = (Motion_Animation_List)uCtrl_Animation_Motion_Opt._AnimationMotion;
            if (animation_motion == null) return;

            if (animation_motion.Motion_Animation == null) animation_motion.Motion_Animation = new List<Motion_Animation>();
            List<Motion_Animation> motionAnimation = animation_motion.Motion_Animation;
            if (motionAnimation == null) return;
            if (motionAnimation.Count < index + 1) return;

            Motion_Animation motion_anim = new Motion_Animation();

            motion_anim.visible = uCtrl_Animation_Motion_Opt.checkBox_visible.Checked;
            motion_anim.x_start = (int)uCtrl_Animation_Motion_Opt.numericUpDown_start_x.Value;
            motion_anim.y_start = (int)uCtrl_Animation_Motion_Opt.numericUpDown_start_y.Value;
            motion_anim.x_end = (int)uCtrl_Animation_Motion_Opt.numericUpDown_end_x.Value;
            motion_anim.y_end = (int)uCtrl_Animation_Motion_Opt.numericUpDown_end_y.Value;

            motion_anim.src = uCtrl_Animation_Motion_Opt.GetImage();
            motion_anim.anim_fps = (int)uCtrl_Animation_Motion_Opt.numericUpDown_fps.Value;
            motion_anim.anim_duration = (int)(uCtrl_Animation_Motion_Opt.numericUpDown_anim_duration.Value * 1000);
            motion_anim.repeat_count = (int)uCtrl_Animation_Motion_Opt.numericUpDown_repeat_count.Value;

            motion_anim.anim_two_sides = uCtrl_Animation_Motion_Opt.checkBox_anim_two_sides.Checked;
            motion_anim.show_in_start = uCtrl_Animation_Motion_Opt.checkBox_show_in_startPos.Checked;

            if (index + 1 <= motionAnimation.Count && index >= 0) motionAnimation[index] = motion_anim;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Rotate_Opt_ValueChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Rotate_Animation_List animation_rotate = (Rotate_Animation_List)uCtrl_Animation_Rotate_Opt._AnimationRotate;
            if (animation_rotate == null) return;

            if (animation_rotate.Rotate_Animation == null) animation_rotate.Rotate_Animation = new List<Rotate_Animation>();
            List<Rotate_Animation> motionAnimation = animation_rotate.Rotate_Animation;
            if (motionAnimation == null) return;
            if (motionAnimation.Count < index + 1) return;

            Rotate_Animation rotate_anim = new Rotate_Animation();

            rotate_anim.visible = uCtrl_Animation_Rotate_Opt.checkBox_visible.Checked;
            rotate_anim.center_x = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_center_x.Value;
            rotate_anim.center_y = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_center_y.Value;
            rotate_anim.pos_x = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_pos_x.Value;
            rotate_anim.pos_y = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_pos_y.Value;

            rotate_anim.start_angle = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_start_angle.Value;
            rotate_anim.end_angle = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_end_angle.Value;

            rotate_anim.src = uCtrl_Animation_Rotate_Opt.GetImage();
            rotate_anim.anim_fps = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_fps.Value;
            rotate_anim.anim_duration = (int)(uCtrl_Animation_Rotate_Opt.numericUpDown_anim_duration.Value*1000);
            rotate_anim.repeat_count = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_repeat_count.Value;

            rotate_anim.anim_two_sides = uCtrl_Animation_Rotate_Opt.checkBox_anim_two_sides.Checked;
            rotate_anim.show_in_start = uCtrl_Animation_Rotate_Opt.checkBox_show_in_startPos.Checked;

            if (index + 1 <= motionAnimation.Count && index >= 0) motionAnimation[index] = rotate_anim;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Frame_Opt_AnimationAdd(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            hmUI_widget_IMG_ANIM_List animation_frame = (hmUI_widget_IMG_ANIM_List)uCtrl_Animation_Frame_Opt._AnimationFrame;
            if (animation_frame == null) return;

            if (animation_frame.Frame_Animation == null) animation_frame.Frame_Animation = new List<hmUI_widget_IMG_ANIM>();
            List<hmUI_widget_IMG_ANIM> frameAnimation = animation_frame.Frame_Animation;

            hmUI_widget_IMG_ANIM img_anim = new hmUI_widget_IMG_ANIM();
            img_anim.x = (int)uCtrl_Animation_Frame_Opt.numericUpDown_imageX.Value;
            img_anim.y = (int)uCtrl_Animation_Frame_Opt.numericUpDown_imageY.Value;

            img_anim.anim_src = uCtrl_Animation_Frame_Opt.GetImage();
            img_anim.anim_prefix = uCtrl_Animation_Frame_Opt.GetPrefix();
            img_anim.anim_fps = (int)uCtrl_Animation_Frame_Opt.numericUpDown_fps.Value;
            img_anim.anim_size = (int)uCtrl_Animation_Frame_Opt.numericUpDown_images_count.Value;
            img_anim.anim_repeat = uCtrl_Animation_Frame_Opt.checkBox_anim_repeat.Checked;

            //rotate_anim.display_on_restart = uCtrl_Animation_Frame_Opt.checkBox_anim_restart.Checked;

            if (frameAnimation.Count > index) frameAnimation.Add(img_anim);
            else frameAnimation.Insert(index, img_anim);
            animation_frame.selected_animation = ++index;
            //uCtrl_Animation_Frame_Opt.SetAnimationIndex(index);
            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Motion_Opt_AnimationAdd(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Motion_Animation_List animation_motion = (Motion_Animation_List)uCtrl_Animation_Motion_Opt._AnimationMotion;
            if (animation_motion == null) return;

            if (animation_motion.Motion_Animation == null) animation_motion.Motion_Animation = new List<Motion_Animation>();
            List<Motion_Animation> motionAnimation = animation_motion.Motion_Animation;

            Motion_Animation motion_anim = new Motion_Animation();
            motion_anim.x_start = (int)uCtrl_Animation_Motion_Opt.numericUpDown_start_x.Value;
            motion_anim.x_end = (int)uCtrl_Animation_Motion_Opt.numericUpDown_end_x.Value;
            motion_anim.y_start = (int)uCtrl_Animation_Motion_Opt.numericUpDown_start_y.Value;
            motion_anim.y_end = (int)uCtrl_Animation_Motion_Opt.numericUpDown_end_y.Value;


            motion_anim.src = uCtrl_Animation_Motion_Opt.GetImage();
            motion_anim.anim_fps = (int)uCtrl_Animation_Motion_Opt.numericUpDown_fps.Value;
            motion_anim.anim_duration = (int)(uCtrl_Animation_Motion_Opt.numericUpDown_anim_duration.Value*1000);
            motion_anim.repeat_count = (int)uCtrl_Animation_Motion_Opt.numericUpDown_repeat_count.Value;

            motion_anim.anim_two_sides = uCtrl_Animation_Motion_Opt.checkBox_anim_two_sides.Checked;
            motion_anim.show_in_start = uCtrl_Animation_Motion_Opt.checkBox_show_in_startPos.Checked;

            if (motionAnimation.Count > index) motionAnimation.Add(motion_anim);
            else motionAnimation.Insert(index, motion_anim);
            animation_motion.selected_animation = ++index;
            //uCtrl_Animation_Motion_Opt.SetAnimationIndex(index);
            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Rotate_Opt_AnimationAdd(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Rotate_Animation_List animation_rotate = (Rotate_Animation_List)uCtrl_Animation_Rotate_Opt._AnimationRotate;
            if (animation_rotate == null) return;

            if (animation_rotate.Rotate_Animation == null) animation_rotate.Rotate_Animation = new List<Rotate_Animation>();
            List<Rotate_Animation> frameAnimation = animation_rotate.Rotate_Animation;

            Rotate_Animation rotate_anim = new Rotate_Animation();
            rotate_anim.center_x = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_center_x.Value;
            rotate_anim.center_y = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_center_y.Value;
            rotate_anim.pos_x = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_pos_x.Value;
            rotate_anim.pos_y = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_pos_y.Value;

            rotate_anim.src = uCtrl_Animation_Rotate_Opt.GetImage();

            rotate_anim.start_angle = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_start_angle.Value;
            rotate_anim.end_angle = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_end_angle.Value;

            rotate_anim.anim_fps = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_fps.Value;
            rotate_anim.anim_duration = (int)(uCtrl_Animation_Rotate_Opt.numericUpDown_anim_duration.Value * 1000);
            rotate_anim.repeat_count = (int)uCtrl_Animation_Rotate_Opt.numericUpDown_repeat_count.Value;

            rotate_anim.anim_two_sides = uCtrl_Animation_Rotate_Opt.checkBox_anim_two_sides.Checked;
            rotate_anim.show_in_start = uCtrl_Animation_Rotate_Opt.checkBox_show_in_startPos.Checked;

            if (frameAnimation.Count > index) frameAnimation.Add(rotate_anim);
            else frameAnimation.Insert(index, rotate_anim);
            animation_rotate.selected_animation = ++index;
            //uCtrl_Animation_Rotate_Opt.SetAnimationIndex(index);
            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Frame_Opt_AnimationDel(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            hmUI_widget_IMG_ANIM_List animation_frame = (hmUI_widget_IMG_ANIM_List)uCtrl_Animation_Frame_Opt._AnimationFrame;
            if (animation_frame == null) return;

            if (animation_frame.Frame_Animation == null) animation_frame.Frame_Animation = new List<hmUI_widget_IMG_ANIM>();
            List<hmUI_widget_IMG_ANIM> frameAnimation = animation_frame.Frame_Animation;

            if (frameAnimation.Count > index) frameAnimation.RemoveAt(index);
            animation_frame.selected_animation = --index;
            if (index < 0 && animation_frame.Frame_Animation != null && animation_frame.Frame_Animation.Count > 0)
                animation_frame.selected_animation = 0;
            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Motion_Opt_AnimationDel(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Motion_Animation_List animation_motion = (Motion_Animation_List)uCtrl_Animation_Motion_Opt._AnimationMotion;
            if (animation_motion == null) return;

            if (animation_motion.Motion_Animation == null) animation_motion.Motion_Animation = new List<Motion_Animation>();
            List<Motion_Animation> motionAnimation = animation_motion.Motion_Animation;

            if (motionAnimation.Count > index) motionAnimation.RemoveAt(index);
            animation_motion.selected_animation = --index;
            if (index < 0 && animation_motion.Motion_Animation != null && animation_motion.Motion_Animation.Count > 0)
                animation_motion.selected_animation = 0;
            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Rotate_Opt_AnimationDel(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Rotate_Animation_List animation_rotate = (Rotate_Animation_List)uCtrl_Animation_Rotate_Opt._AnimationRotate;
            if (animation_rotate == null) return;

            if (animation_rotate.Rotate_Animation == null) animation_rotate.Rotate_Animation = new List<Rotate_Animation>();
            List<Rotate_Animation> rotateAnimation = animation_rotate.Rotate_Animation;

            if (rotateAnimation.Count > index) rotateAnimation.RemoveAt(index);
            animation_rotate.selected_animation = --index;
            if (index < 0 && animation_rotate.Rotate_Animation != null && animation_rotate.Rotate_Animation.Count > 0)
                animation_rotate.selected_animation = 0;
            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Frame_Opt_AnimIndexChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            hmUI_widget_IMG_ANIM_List animation_frame = (hmUI_widget_IMG_ANIM_List)uCtrl_Animation_Frame_Opt._AnimationFrame;
            if (animation_frame == null) return;
            animation_frame.selected_animation = index;

            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);
        }

        private void uCtrl_Animation_Motion_Opt_AnimIndexChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Motion_Animation_List animation_motion = (Motion_Animation_List)uCtrl_Animation_Motion_Opt._AnimationMotion;
            if (animation_motion == null) return;
            animation_motion.selected_animation = index;

            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);
        }

        private void uCtrl_Animation_Rotate_Opt_AnimIndexChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            //if (index < 0) return;
            Rotate_Animation_List animation_rotate = (Rotate_Animation_List)uCtrl_Animation_Rotate_Opt._AnimationRotate;
            if (animation_rotate == null) return;
            animation_rotate.selected_animation = index;

            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);
        }

        private Color StringToColor(string color)
        {
            Color new_color = Color.Black;
            if (color != null)
            {
                if (color.Length == 18) color = color.Remove(2, 8);
                Color old_color = ColorTranslator.FromHtml(color);
                new_color = Color.FromArgb(255, old_color.R, old_color.G, old_color.B); 
            }
            return new_color;
        }

        private string ColorToString(Color color)
        {
            Color new_color = Color.FromArgb(0, color.R, color.G, color.B);
            string colorStr = ColorTranslator.ToHtml(new_color);
            colorStr = colorStr.Replace("#", "0xFF");
            return colorStr;
        }

    }
}
