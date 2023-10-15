using ControlLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {

        private void uCtrl_EditableElements_Opt_ZoneAdd(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;

            if (editableElements.Watchface_edit_group == null) editableElements.Watchface_edit_group = new List<WATCHFACE_EDIT_GROUP>();
            WATCHFACE_EDIT_GROUP watchface_edit_group = new WATCHFACE_EDIT_GROUP();
            watchface_edit_group.x = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_X.Value;
            watchface_edit_group.y = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_Y.Value;
            watchface_edit_group.w = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_W.Value;
            watchface_edit_group.h = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_H.Value;

            watchface_edit_group.tips_BG = uCtrl_EditableElements_Opt.GetTip();
            watchface_edit_group.tips_x = (int)uCtrl_EditableElements_Opt.numericUpDown_tipX.Value;
            watchface_edit_group.tips_y = (int)uCtrl_EditableElements_Opt.numericUpDown_tipY.Value;
            watchface_edit_group.tips_width = (int)uCtrl_EditableElements_Opt.numericUpDown_tips_width.Value;
            watchface_edit_group.tips_margin = (int)uCtrl_EditableElements_Opt.numericUpDown_tips_margin.Value;

            watchface_edit_group.select_image = uCtrl_EditableElements_Opt.GetSelectImage();
            watchface_edit_group.un_select_image = uCtrl_EditableElements_Opt.GetUnSelectImage();

            editableElements.mask = uCtrl_EditableElements_Opt.GetMask();
            editableElements.fg_mask = uCtrl_EditableElements_Opt.GetFgMask();

            //editableElements.AOD_show = uCtrl_EditableElements_Opt.checkBox_showInAOD.Checked;

            if ((editableElements.Watchface_edit_group.Count <= index || index < 0) ||
                (editableElements.Watchface_edit_group.Count == index + 1 && index >= 0))
            {
                editableElements.Watchface_edit_group.Add(watchface_edit_group);
                ++index;
            }
            else
            {
                editableElements.Watchface_edit_group.Insert(++index, watchface_edit_group);
            }
            editableElements.selected_zone = index;
            Read_EditableElements_Options(editableElements);
            Read_EditableElements_ElementOptions(editableElements);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Opt_ZoneDel(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            if (index < 0) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;

            if (editableElements.Watchface_edit_group == null) editableElements.Watchface_edit_group = new List<WATCHFACE_EDIT_GROUP>();
            List<WATCHFACE_EDIT_GROUP> watchface_edit_group = editableElements.Watchface_edit_group;

            if (watchface_edit_group.Count > index) watchface_edit_group.RemoveAt(index);
            editableElements.selected_zone = --index;
            if (index < 0 && watchface_edit_group != null && watchface_edit_group.Count > 0)
                editableElements.selected_zone = 0;
            Read_EditableElements_Options(editableElements);
            Read_EditableElements_ElementOptions(editableElements);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Opt_ZoneIndexChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            editableElements.selected_zone = index;

            Read_EditableElements_Options(editableElements);
            Read_EditableElements_ElementOptions(editableElements);
            PreviewImage();
        }

        private void uCtrl_EditableElements_Opt_ZoneValueChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;

            WATCHFACE_EDIT_GROUP watchface_edit_group = null;
            if (editableElements.Watchface_edit_group != null && index >= 0 && editableElements.Watchface_edit_group.Count > index)
                watchface_edit_group = editableElements.Watchface_edit_group[index];
            if (watchface_edit_group != null)
            {
                watchface_edit_group.x = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_X.Value;
                watchface_edit_group.y = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_Y.Value;
                watchface_edit_group.w = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_W.Value;
                watchface_edit_group.h = (int)uCtrl_EditableElements_Opt.numericUpDown_zone_H.Value;

                watchface_edit_group.tips_BG = uCtrl_EditableElements_Opt.GetTip();
                watchface_edit_group.tips_x = (int)uCtrl_EditableElements_Opt.numericUpDown_tipX.Value;
                watchface_edit_group.tips_y = (int)uCtrl_EditableElements_Opt.numericUpDown_tipY.Value;
                watchface_edit_group.tips_width = (int)uCtrl_EditableElements_Opt.numericUpDown_tips_width.Value;
                watchface_edit_group.tips_margin = (int)uCtrl_EditableElements_Opt.numericUpDown_tips_margin.Value;

                watchface_edit_group.select_image = uCtrl_EditableElements_Opt.GetSelectImage();
                watchface_edit_group.un_select_image = uCtrl_EditableElements_Opt.GetUnSelectImage();
            }

            editableElements.mask = uCtrl_EditableElements_Opt.GetMask();
            editableElements.fg_mask = uCtrl_EditableElements_Opt.GetFgMask();

            editableElements.display_first = uCtrl_EditableElements_Opt.checkBox_display_first.Checked;
            editableElements.AOD_show = uCtrl_EditableElements_Opt.checkBox_showInAOD.Checked;
            editableElements.showEeditMode = uCtrl_EditableElements_Opt.checkBox_edit_mode.Checked;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Opt_ElementAdd(object sender, EventArgs eventArgs, int index)
        {
            if (Watch_Face == null) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            if (editableElements.selected_zone < 0 || editableElements.Watchface_edit_group == null ||
                editableElements.Watchface_edit_group.Count <= editableElements.selected_zone) return;
            WATCHFACE_EDIT_GROUP watchface_edit_group = editableElements.Watchface_edit_group[editableElements.selected_zone];
            if (watchface_edit_group.Elements == null) watchface_edit_group.Elements = new List<object>();
            if (watchface_edit_group.optional_types_list == null) watchface_edit_group.optional_types_list = new List<Optional_Types_List>();

            FormAddEditableElement f = new FormAddEditableElement();
            f.ShowDialog();
            string dialogResult = f.Type;

            Optional_Types_List optional_types = null;
            Object newElement = null;
            string newElementName = "";

            switch (dialogResult)
            {
                case "Date":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "DATE";
                    newElement = new ElementDateDay();
                    newElementName = "ElementDateDay";
                    break;
                case "Month":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "DATE";
                    newElement = new ElementDateMonth();
                    newElementName = "ElementDateMonth";
                    break;
                case "Year":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "DATE";
                    newElement = new ElementDateYear();
                    newElementName = "ElementDateYear";
                    break;
                case "Week":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "WEEK";
                    newElement = new ElementDateWeek();
                    newElementName = "ElementDateWeek";
                    break;

                case "Battery":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "BATTERY";
                    newElement = new ElementBattery();
                    newElementName = "ElementBattery";
                    break;

                case "Steps":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "STEP";
                    newElement = new ElementSteps();
                    newElementName = "ElementSteps";
                    break;
                case "Calories":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "CAL";
                    newElement = new ElementCalories();
                    newElementName = "ElementCalories";
                    break;
                case "Heart":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "HEART";
                    newElement = new ElementHeart();
                    newElementName = "ElementHeart";
                    break;
                case "PAI":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "PAI";
                    newElement = new ElementPAI();
                    newElementName = "ElementPAI";
                    break;
                case "Distance":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "DISTANCE";
                    newElement = new ElementDistance();
                    newElementName = "ElementDistance";
                    break;
                case "Stand":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "STAND";
                    newElement = new ElementStand();
                    newElementName = "ElementStand";
                    break;
                case "Stress":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "STRESS";
                    newElement = new ElementStress();
                    newElementName = "ElementStress";
                    break;
                case "FatBurning":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "FAT_BURNING";
                    newElement = new ElementFatBurning();
                    newElementName = "ElementFatBurning";
                    break;
                case "SpO2":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "SPO2";
                    newElement = new ElementSpO2();
                    newElementName = "ElementSpO2";
                    break;

                case "Weather":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "WEATHER";
                    //optional_types.type = "TEMPERATURE";
                    newElement = new ElementWeather();
                    newElementName = "ElementWeather";
                    break;
                case "UVI":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "UVI";
                    newElement = new ElementUVIndex();
                    newElementName = "ElementUVIndex";
                    break;
                case "Humidity":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "HUMIDITY";
                    newElement = new ElementHumidity();
                    newElementName = "ElementHumidity";
                    break;
                case "Sunrise":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "SUN";
                    newElement = new ElementSunrise();
                    newElementName = "ElementSunrise";
                    break;
                case "Altimeter":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "ALTIMETER";
                    newElement = new ElementAltimeter();
                    newElementName = "ElementAltimeter";
                    break;
                case "Wind":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "WIND";
                    newElement = new ElementWind();
                    newElementName = "ElementWind";
                    break;
                case "Moon":
                    optional_types = new Optional_Types_List();
                    optional_types.type = "MOON";
                    newElement = new ElementMoon();
                    newElementName = "ElementMoon";
                    break;
            }

            if (optional_types == null || newElement == null) return;

            bool exists = watchface_edit_group.Elements.Exists(e => e.GetType().Name == newElementName); // проверяем что такого элемента нет
            //if (!exists) Elements.Add(dateDay);
            if (exists)
            {
                MessageBox.Show(Properties.ElementsString.ElementExists, Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((watchface_edit_group.Elements.Count <= index || index < 0) || (watchface_edit_group.Elements.Count == index + 1 && index >= 0))
            {
                watchface_edit_group.Elements.Add(newElement);
                watchface_edit_group.optional_types_list.Add(optional_types);
                ++index;
            }
            else
            {
                watchface_edit_group.Elements.Insert(++index, newElement);
                watchface_edit_group.optional_types_list.Insert(index, optional_types);
            }
            watchface_edit_group.selected_element = index;

            List<string> elementName = GetElementsNameList(watchface_edit_group.Elements);
            uCtrl_EditableElements_Opt.SettingsElementClear();
            uCtrl_EditableElements_Opt.SetElementsCount(elementName);
            uCtrl_EditableElements_Opt.SetElementsIndex(index);
        }

        private void uCtrl_EditableElements_Opt_ElementDel(object sender, EventArgs eventArgs, int index)
        {
            if (Watch_Face == null) return;
            if (index < 0) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            if (editableElements.selected_zone < 0 || editableElements.Watchface_edit_group == null ||
                editableElements.Watchface_edit_group.Count <= editableElements.selected_zone) return;
            WATCHFACE_EDIT_GROUP watchface_edit_group = editableElements.Watchface_edit_group[editableElements.selected_zone];
            if (watchface_edit_group.Elements == null || watchface_edit_group.Elements.Count <= index) return;


            if (watchface_edit_group.Elements.Count > index) 
            {
                watchface_edit_group.Elements.RemoveAt(index);
                watchface_edit_group.optional_types_list.RemoveAt(index);
            }
            --index;
            if (index < 0 && watchface_edit_group.Elements != null && watchface_edit_group.Elements.Count > 0)
                index = 0;
            watchface_edit_group.selected_element = index;

            List<string> elementName = GetElementsNameList(watchface_edit_group.Elements);
            uCtrl_EditableElements_Opt.SettingsElementClear();
            uCtrl_EditableElements_Opt.SetElementsCount(elementName);
            if(index < 0) uCtrl_EditableElements_Opt.SettingsElementClear();
            else uCtrl_EditableElements_Opt.SetElementsIndex(index);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Opt_ElementIndexChanged(object sender, EventArgs eventArgs, int index)
        {
            uCtrl_EditableElements_Opt.SettingsElementClear();
            uCtrl_EditableElements_Opt.ResetHighlightState();
            ShowElemenrOptions("EditableElements");
            if (Watch_Face == null) return;
            if (index < 0) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            if (editableElements.selected_zone < 0 || editableElements.Watchface_edit_group == null ||
                editableElements.Watchface_edit_group.Count <= editableElements.selected_zone) return;
            WATCHFACE_EDIT_GROUP watchface_edit_group = editableElements.Watchface_edit_group[editableElements.selected_zone];
            if (watchface_edit_group.Elements == null || watchface_edit_group.Elements.Count <= index) return;

            PreviewView = false;
            watchface_edit_group.selected_element = index;
            List<string> subElements = new List<string>();
            Object element = watchface_edit_group.Elements[index];
            string type = element.GetType().Name;
            switch (type)
            {
                #region ElementDateDay
                case "ElementDateDay":
                    //subElements.Add("Images");
                    //subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    //subElements.Add("Icon");

                    ElementDateDay dateDay = (ElementDateDay)element;
                    if (dateDay.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = dateDay.Number.visible;
                    if (dateDay.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = dateDay.Pointer.visible;
                    break;
                #endregion

                #region ElementDateMonth
                case "ElementDateMonth":
                    subElements.Add("Images");
                    //subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    //subElements.Add("Icon");

                    ElementDateMonth dateMonth = (ElementDateMonth)element;
                    if (dateMonth.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = dateMonth.Images.visible;
                    if (dateMonth.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = dateMonth.Number.visible;
                    if (dateMonth.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = dateMonth.Pointer.visible;
                    break;
                #endregion

                #region ElementDateYear
                case "ElementDateYear":
                    //subElements.Add("Images");
                    //subElements.Add("Segments");
                    //subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    //subElements.Add("Icon");

                    ElementDateYear dateYear = (ElementDateYear)element;
                    if (dateYear.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = dateYear.Number.visible;
                    break;
                #endregion

                #region ElementDateWeek
                case "ElementDateWeek":
                    subElements.Add("Images");
                    //subElements.Add("Segments");
                    subElements.Add("Pointer");
                    //subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    //subElements.Add("Icon");

                    ElementDateWeek dateWeek = (ElementDateWeek)element;
                    if (dateWeek.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = dateWeek.Images.visible;
                    if (dateWeek.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = dateWeek.Pointer.visible;
                    break;
                #endregion


                #region ElementSteps
                case "ElementSteps":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementSteps steps = (ElementSteps)element;
                    if (steps.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = steps.Images.visible;
                    if (steps.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = steps.Segments.visible;
                    if (steps.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = steps.Number.visible;
                    if (steps.Number_Target != null) uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked = steps.Number_Target.visible;
                    if (steps.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = steps.Pointer.visible;
                    if (steps.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = steps.Circle_Scale.visible;
                    if (steps.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = steps.Linear_Scale.visible;
                    if (steps.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = steps.Icon.visible;
                    break;
                #endregion

                #region ElementBattery
                case "ElementBattery":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementBattery battery = (ElementBattery)element;
                    if (battery.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = battery.Images.visible;
                    if (battery.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = battery.Segments.visible;
                    if (battery.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = battery.Number.visible;
                    if (battery.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = battery.Pointer.visible;
                    if (battery.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = battery.Circle_Scale.visible;
                    if (battery.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = battery.Linear_Scale.visible;
                    if (battery.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = battery.Icon.visible;
                    break;
                #endregion

                #region ElementCalories
                case "ElementCalories":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementCalories calories = (ElementCalories)element;
                    if (calories.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = calories.Images.visible;
                    if (calories.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = calories.Segments.visible;
                    if (calories.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = calories.Number.visible;
                    if (calories.Number_Target != null) uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked = calories.Number_Target.visible;
                    if (calories.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = calories.Pointer.visible;
                    if (calories.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = calories.Circle_Scale.visible;
                    if (calories.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = calories.Linear_Scale.visible;
                    if (calories.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = calories.Icon.visible;
                    break;
                #endregion

                #region ElementHeart
                case "ElementHeart":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementHeart heart = (ElementHeart)element;
                    if (heart.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = heart.Images.visible;
                    if (heart.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = heart.Segments.visible;
                    if (heart.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = heart.Number.visible;
                    if (heart.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = heart.Pointer.visible;
                    if (heart.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = heart.Circle_Scale.visible;
                    if (heart.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = heart.Linear_Scale.visible;
                    if (heart.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = heart.Icon.visible;
                    break;
                #endregion

                #region ElementPAI
                case "ElementPAI":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementPAI pai = (ElementPAI)element;
                    if (pai.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = pai.Images.visible;
                    if (pai.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = pai.Segments.visible;
                    if (pai.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = pai.Number.visible;
                    //if (pai.Number_Target != null) uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked = pai.Number_Target.enable;
                    if (pai.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = pai.Pointer.visible;
                    if (pai.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = pai.Circle_Scale.visible;
                    if (pai.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = pai.Linear_Scale.visible;
                    if (pai.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = pai.Icon.visible;
                    break;
                #endregion

                #region ElementDistance
                case "ElementDistance":
                    //subElements.Add("Images");
                    //subElements.Add("Segments");
                    //subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    //subElements.Add("Icon");

                    ElementDistance distance = (ElementDistance)element;
                    if (distance.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = distance.Number.visible;
                    break;
                #endregion

                #region ElementStand
                case "ElementStand":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementStand stand = (ElementStand)element;
                    if (stand.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = stand.Images.visible;
                    if (stand.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = stand.Segments.visible;
                    if (stand.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = stand.Number.visible;
                    if (stand.Number_Target != null) uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked = stand.Number_Target.visible;
                    if (stand.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = stand.Pointer.visible;
                    if (stand.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = stand.Circle_Scale.visible;
                    if (stand.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = stand.Linear_Scale.visible;
                    if (stand.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = stand.Icon.visible;
                    break;
                #endregion

                #region ElementActivity
                case "ElementActivity":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementActivity activity = (ElementActivity)element;
                    if (activity.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = activity.Images.visible;
                    if (activity.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = activity.Segments.visible;
                    if (activity.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = activity.Number.visible;
                    if (activity.Number_Target != null) uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked = activity.Number_Target.visible;
                    if (activity.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = activity.Pointer.visible;
                    if (activity.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = activity.Circle_Scale.visible;
                    if (activity.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = activity.Linear_Scale.visible;
                    if (activity.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = activity.Icon.visible;
                    break;
                #endregion

                #region ElementSpO2
                case "ElementSpO2":
                    //subElements.Add("Images");
                    //subElements.Add("Segments");
                    //subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    //subElements.Add("Icon");

                    ElementSpO2 spo2 = (ElementSpO2)element;
                    if (spo2.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = spo2.Number.visible;
                    break;
                #endregion

                #region ElementStress
                case "ElementStress":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementStress stress = (ElementStress)element;
                    if (stress.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = stress.Images.visible;
                    if (stress.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = stress.Segments.visible;
                    if (stress.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = stress.Number.visible;
                    if (stress.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = stress.Pointer.visible;
                    if (stress.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = stress.Icon.visible;
                    break;
                #endregion

                #region ElementFatBurning
                case "ElementFatBurning":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    subElements.Add("Circle_scale");
                    subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementFatBurning fat_burning = (ElementFatBurning)element;
                    if (fat_burning.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = fat_burning.Images.visible;
                    if (fat_burning.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = fat_burning.Segments.visible;
                    if (fat_burning.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = fat_burning.Number.visible;
                    if (fat_burning.Number_Target != null) uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked = fat_burning.Number_Target.visible;
                    if (fat_burning.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = fat_burning.Pointer.visible;
                    if (fat_burning.Circle_Scale != null) uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked = fat_burning.Circle_Scale.visible;
                    if (fat_burning.Linear_Scale != null) uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked = fat_burning.Linear_Scale.visible;
                    if (fat_burning.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = fat_burning.Icon.visible;
                    break;
                #endregion



                #region ElementWeather
                case "ElementWeather":
                    subElements.Add("Images");
                    //subElements.Add("Segments");
                    //subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    subElements.Add("Number_min");
                    subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    subElements.Add("CityName");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementWeather weather = (ElementWeather)element;
                    if (weather.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = weather.Images.visible;
                    if (weather.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = weather.Number.visible;
                    if (weather.Number_Max != null) uCtrl_EditableElements_Opt.checkBox_Number_Max.Checked = weather.Number_Max.visible;
                    if (weather.Number_Min != null) uCtrl_EditableElements_Opt.checkBox_Number_Min.Checked = weather.Number_Min.visible;
                    if (weather.City_Name != null) uCtrl_EditableElements_Opt.checkBox_Text_CityName.Checked = weather.City_Name.visible;
                    if (weather.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = weather.Icon.visible;
                    break;
                #endregion

                #region ElementUVIndex
                case "ElementUVIndex":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementUVIndex uv_index = (ElementUVIndex)element;
                    if (uv_index.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = uv_index.Images.visible;
                    if (uv_index.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = uv_index.Segments.visible;
                    if (uv_index.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = uv_index.Number.visible;
                    if (uv_index.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = uv_index.Pointer.visible;
                    if (uv_index.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = uv_index.Icon.visible;
                    break;
                #endregion

                #region ElementHumidity
                case "ElementHumidity":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementHumidity humidity = (ElementHumidity)element;
                    if (humidity.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = humidity.Images.visible;
                    if (humidity.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = humidity.Segments.visible;
                    if (humidity.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = humidity.Number.visible;
                    if (humidity.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = humidity.Pointer.visible;
                    if (humidity.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = humidity.Icon.visible;
                    break;
                #endregion

                #region ElementAltimeter
                case "ElementAltimeter":
                    //subElements.Add("Images");
                    //subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementAltimeter altimeter = (ElementAltimeter)element;
                    if (altimeter.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = altimeter.Number.visible;
                    if (altimeter.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = altimeter.Pointer.visible;
                    if (altimeter.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = altimeter.Icon.visible;
                    break;
                #endregion

                #region ElementSunrise
                case "ElementSunrise":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    //subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    subElements.Add("Sunset");
                    subElements.Add("Sunrise");
                    subElements.Add("Sunset_Sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    subElements.Add("Icon");

                    ElementSunrise sunrise = (ElementSunrise)element;
                    if (sunrise.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = sunrise.Images.visible;
                    if (sunrise.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = sunrise.Segments.visible;
                    if (sunrise.Sunrise != null) uCtrl_EditableElements_Opt.checkBox_Sunrise.Checked = sunrise.Sunrise.visible;
                    if (sunrise.Sunset != null) uCtrl_EditableElements_Opt.checkBox_Sunset.Checked = sunrise.Sunset.visible;
                    if (sunrise.Sunset_Sunrise != null) uCtrl_EditableElements_Opt.checkBox_Sunset_Sunrise.Checked = sunrise.Sunset_Sunrise.visible;
                    if (sunrise.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = sunrise.Pointer.visible;
                    if (sunrise.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = sunrise.Icon.visible;
                    break;
                #endregion

                #region ElementWind
                case "ElementWind":
                    subElements.Add("Images");
                    subElements.Add("Segments");
                    subElements.Add("Pointer");
                    subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    subElements.Add("Wind_Direction");
                    subElements.Add("Icon");

                    ElementWind wind = (ElementWind)element;
                    if (wind.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = wind.Images.visible;
                    if (wind.Segments != null) uCtrl_EditableElements_Opt.checkBox_Segments.Checked = wind.Segments.visible;
                    if (wind.Number != null) uCtrl_EditableElements_Opt.checkBox_Number.Checked = wind.Number.visible;
                    if (wind.Pointer != null) uCtrl_EditableElements_Opt.checkBox_Pointer.Checked = wind.Pointer.visible;
                    if (wind.Direction != null) uCtrl_EditableElements_Opt.checkBox_Direction.Checked = wind.Direction.visible;
                    if (wind.Icon != null) uCtrl_EditableElements_Opt.checkBox_Icon.Checked = wind.Icon.visible;
                    break;
                #endregion

                #region ElementMoon
                case "ElementMoon":
                    subElements.Add("Images");
                    //subElements.Add("Segments");
                    //subElements.Add("Pointer");
                    //subElements.Add("Number");
                    //subElements.Add("Number_target");
                    //subElements.Add("Number_min");
                    //subElements.Add("Number_max");
                    //subElements.Add("Sunset");
                    //subElements.Add("Sunrise");
                    //subElements.Add("Sunset_sunrise");
                    //subElements.Add("Sity_name");
                    //subElements.Add("Circle_scale");
                    //subElements.Add("Linear_scale");
                    //subElements.Add("Icon");

                    ElementMoon moon = (ElementMoon)element;
                    if (moon.Images != null) uCtrl_EditableElements_Opt.checkBox_Images.Checked = moon.Images.visible;
                    break;
                    #endregion
            }

            Dictionary<int, string> optionsPosition = ReadElementPos(element);
            uCtrl_EditableElements_Opt.SetOptionsPosition(optionsPosition);
            uCtrl_EditableElements_Opt.SetPreviewElement(watchface_edit_group.optional_types_list[watchface_edit_group.selected_element].preview);
            uCtrl_EditableElements_Opt.SetVisibilityOptions(subElements);

            PreviewView = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Opt_ElementValueChanged(object sender, EventArgs eventArgs, int index)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            if (index < 0) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            if (editableElements.selected_zone < 0 || editableElements.Watchface_edit_group == null ||
                editableElements.Watchface_edit_group.Count <= editableElements.selected_zone) return;
            WATCHFACE_EDIT_GROUP watchface_edit_group = editableElements.Watchface_edit_group[editableElements.selected_zone];
            if (watchface_edit_group.Elements == null || watchface_edit_group.Elements.Count <= index) return;

            Optional_Types_List optional_types_list = watchface_edit_group.optional_types_list[index];
            optional_types_list.preview = uCtrl_EditableElements_Opt.GetPreviewElement();

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Opt_SelectChanged(object sender, EventArgs eventArgs)
        {
            ShowElemenrOptions("EditableElements");
            //string selectElement = uCtrl_EditableElements_Opt.selectedElement;

            if (!PreviewView) return;
            if (Watch_Face == null) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            if (editableElements.selected_zone < 0 || editableElements.Watchface_edit_group == null ||
                editableElements.Watchface_edit_group.Count <= editableElements.selected_zone) return;
            WATCHFACE_EDIT_GROUP watchface_edit_group = editableElements.Watchface_edit_group[editableElements.selected_zone];
            if (watchface_edit_group.selected_element < 0 || watchface_edit_group.Elements == null ||
                watchface_edit_group.Elements.Count <= watchface_edit_group.selected_element) return;

            List<string> subElements = new List<string>();
            Object element = watchface_edit_group.Elements[watchface_edit_group.selected_element];
            string type = element.GetType().Name;
            switch (type)
            {
                #region ElementDateDay
                case "ElementDateDay":
                    ElementDateDay dateDay = (ElementDateDay)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        //case "Images":
                        //    if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                        //    {
                        //        img_level = dateDay.Images;
                        //        Read_ImgLevel_Options(img_level, 12, false);
                        //        ShowElemenrOptions("Images");
                        //    }
                        //    else HideAllElemenrOptions();
                        //    break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = dateDay.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = dateDay.Pointer;
                                Read_ImgPointer_Options(img_pointer, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementDateMonth
                case "ElementDateMonth":
                    ElementDateMonth dateMonth = (ElementDateMonth)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = dateMonth.Images;
                                Read_ImgLevel_Options(img_level, 12, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = dateMonth.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = dateMonth.Pointer;
                                Read_ImgPointer_Options(img_pointer, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementDateYear
                case "ElementDateYear":
                    ElementDateYear dateYear = (ElementDateYear)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = dateYear.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, false);
                                uCtrl_Text_Opt.Year = true;
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementDateWeek
                case "ElementDateWeek":
                    ElementDateWeek dateWeek = (ElementDateWeek)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = dateWeek.Images;
                                Read_ImgLevel_Options(img_level, 12, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        //case "Number":
                        //    if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                        //    {
                        //        hmUI_widget_IMG_NUMBER img_number = dateWeek.Number;
                        //        Read_ImgNumber_Options(img_number, false, false, "", false, false, true);
                        //    }
                        //    break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = dateWeek.Pointer;
                                Read_ImgPointer_Options(img_pointer, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion


                #region ElementSteps
                case "ElementSteps":
                    ElementSteps steps = (ElementSteps)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = steps.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = steps.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = steps.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number_Target":
                            if (uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = steps.Number_Target;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = steps.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Circle_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked)
                            {
                                Circle_Scale circle_scale = steps.Circle_Scale;
                                Read_CircleScale_Options(circle_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Linear_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked)
                            {
                                Linear_Scale linear_scale = steps.Linear_Scale;
                                Read_LinearScale_Options(linear_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = steps.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementBattery
                case "ElementBattery":
                    ElementBattery battery = (ElementBattery)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = battery.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = battery.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = battery.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = battery.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Circle_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked)
                            {
                                Circle_Scale circle_scale = battery.Circle_Scale;
                                Read_CircleScale_Options(circle_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Linear_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked)
                            {
                                Linear_Scale linear_scale = battery.Linear_Scale;
                                Read_LinearScale_Options(linear_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = battery.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementCalories
                case "ElementCalories":
                    ElementCalories calories = (ElementCalories)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = calories.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = calories.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = calories.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number_Target":
                            if (uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = calories.Number_Target;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = calories.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Circle_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked)
                            {
                                Circle_Scale circle_scale = calories.Circle_Scale;
                                Read_CircleScale_Options(circle_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Linear_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked)
                            {
                                Linear_Scale linear_scale = calories.Linear_Scale;
                                Read_LinearScale_Options(linear_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = calories.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementHeart
                case "ElementHeart":
                    ElementHeart heart = (ElementHeart)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = heart.Images;
                                Read_ImgLevel_Options(img_level, 6, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = heart.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 6, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = heart.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = heart.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Circle_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked)
                            {
                                Circle_Scale circle_scale = heart.Circle_Scale;
                                Read_CircleScale_Options(circle_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Linear_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked)
                            {
                                Linear_Scale linear_scale = heart.Linear_Scale;
                                Read_LinearScale_Options(linear_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = heart.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementPAI
                case "ElementPAI":
                    ElementPAI pai = (ElementPAI)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = pai.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = pai.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = pai.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        //case "Number_Target":
                        //    if (uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked)
                        //    {
                        //        hmUI_widget_IMG_NUMBER img_number = pai.Number_Target;
                        //        Read_ImgNumber_Options(img_number, false, false, "", false, false, true);
                        //        uCtrl_EditableElements_Opt.Collapse = true;
                        //    }
                        //    break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = pai.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Circle_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked)
                            {
                                Circle_Scale circle_scale = pai.Circle_Scale;
                                Read_CircleScale_Options(circle_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Linear_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked)
                            {
                                Linear_Scale linear_scale = pai.Linear_Scale;
                                Read_LinearScale_Options(linear_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = pai.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementDistance
                case "ElementDistance":
                    ElementDistance distance = (ElementDistance)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = distance.Number;
                                Read_ImgNumber_Options(img_number, true, false, "", false, true, false, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementStand
                case "ElementStand":
                    ElementStand stand = (ElementStand)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = stand.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = stand.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = stand.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number_Target":
                            if (uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = stand.Number_Target;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = stand.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Circle_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked)
                            {
                                Circle_Scale circle_scale = stand.Circle_Scale;
                                Read_CircleScale_Options(circle_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Linear_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked)
                            {
                                Linear_Scale linear_scale = stand.Linear_Scale;
                                Read_LinearScale_Options(linear_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = stand.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementSpO2
                case "ElementSpO2":
                    ElementSpO2 spo2 = (ElementSpO2)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = spo2.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementStress
                case "ElementStress":
                    ElementStress stress = (ElementStress)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = stress.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = stress.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = stress.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = stress.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = stress.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementFatBurning
                case "ElementFatBurning":
                    ElementFatBurning fat_burning = (ElementFatBurning)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = fat_burning.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = fat_burning.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = fat_burning.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number_Target":
                            if (uCtrl_EditableElements_Opt.checkBox_Number_Target.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = fat_burning.Number_Target;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = fat_burning.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Circle_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Circle_Scale.Checked)
                            {
                                Circle_Scale circle_scale = fat_burning.Circle_Scale;
                                Read_CircleScale_Options(circle_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Linear_Scale":
                            if (uCtrl_EditableElements_Opt.checkBox_Linear_Scale.Checked)
                            {
                                Linear_Scale linear_scale = fat_burning.Linear_Scale;
                                Read_LinearScale_Options(linear_scale);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = fat_burning.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion



                #region ElementWeather
                case "ElementWeather":
                    ElementWeather weather = (ElementWeather)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = weather.Images;
                                Read_ImgLevel_Options(img_level, 29, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = weather.Number;
                                Read_ImgNumberWeather_Options(img_number, false, "", true, false);
                                //ShowElemenrOptions("Text_Weather");
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            //else HideAllElemenrOptions();
                            break;
                        case "Number_Min":
                            if (uCtrl_EditableElements_Opt.checkBox_Number_Min.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = weather.Number_Min;
                                Read_ImgNumberWeather_Options(img_number, false, "", true, false);
                                //ShowElemenrOptions("Text_Weather");
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            //else HideAllElemenrOptions();
                            break;
                        case "Number_Max":
                            if (uCtrl_EditableElements_Opt.checkBox_Number_Max.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = weather.Number_Max;
                                Read_ImgNumberWeather_Options(img_number, false, "", true, false);
                                //ShowElemenrOptions("Text_Weather");
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            //else HideAllElemenrOptions();
                            break;
                        case "CityName":
                            if (uCtrl_EditableElements_Opt.checkBox_Text_CityName.Checked)
                            {
                                hmUI_widget_TEXT text = weather.City_Name;
                                Read_Text_Options(text);
                                //ShowElemenrOptions("SystemFont");
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            //else HideAllElemenrOptions();
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = weather.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementUVIndex
                case "ElementUVIndex":
                    ElementUVIndex uv_index = (ElementUVIndex)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = uv_index.Images;
                                Read_ImgLevel_Options(img_level, 5, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = uv_index.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 5, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = uv_index.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = uv_index.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = uv_index.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementHumidity
                case "ElementHumidity":
                    ElementHumidity humidity = (ElementHumidity)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = humidity.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = humidity.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = humidity.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = humidity.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = humidity.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementAltimeter
                case "ElementAltimeter":
                    ElementAltimeter altimeter = (ElementAltimeter)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = altimeter.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = altimeter.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = altimeter.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementSunrise
                case "ElementSunrise":
                    ElementSunrise sunrise = (ElementSunrise)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = sunrise.Images;
                                Read_ImgLevel_Options(img_level, 2, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = sunrise.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 2, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Sunrise":
                            if (uCtrl_EditableElements_Opt.checkBox_Sunrise.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = sunrise.Sunrise;
                                Read_ImgNumber_Options(img_number, false, false, "", true, true, false, true, true);
                                //ShowElemenrOptions("Text");
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            //else HideAllElemenrOptions();
                            break;
                        case "Sunset":
                            if (uCtrl_EditableElements_Opt.checkBox_Sunset.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = sunrise.Sunset;
                                Read_ImgNumber_Options(img_number, false, false, "", true, true, false, true, true);
                                //ShowElemenrOptions("Text");
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            //else HideAllElemenrOptions();
                            break;
                        case "Sunset_Sunrise":
                            if (uCtrl_EditableElements_Opt.checkBox_Sunset_Sunrise.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = sunrise.Sunset_Sunrise;
                                Read_ImgNumber_Options(img_number, false, false, "", true, true, false, true, true);
                                //ShowElemenrOptions("Text");
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            //else HideAllElemenrOptions();
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = sunrise.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = sunrise.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementWind
                case "ElementWind":
                    ElementWind wind = (ElementWind)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = wind.Images;
                                Read_ImgLevel_Options(img_level, 10, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Segments":
                            if (uCtrl_EditableElements_Opt.checkBox_Segments.Checked)
                            {
                                hmUI_widget_IMG_PROGRESS img_prorgess = wind.Segments;
                                Read_ImgProrgess_Options(img_prorgess, 10, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Number":
                            if (uCtrl_EditableElements_Opt.checkBox_Number.Checked)
                            {
                                hmUI_widget_IMG_NUMBER img_number = wind.Number;
                                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Pointer":
                            if (uCtrl_EditableElements_Opt.checkBox_Pointer.Checked)
                            {
                                hmUI_widget_IMG_POINTER img_pointer = wind.Pointer;
                                Read_ImgPointer_Options(img_pointer, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Wind_Direction":
                            if (uCtrl_EditableElements_Opt.checkBox_Direction.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = wind.Direction;
                                Read_ImgLevel_Options(img_level, 8, false);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                        case "Icon":
                            if (uCtrl_EditableElements_Opt.checkBox_Icon.Checked)
                            {
                                hmUI_widget_IMG icon = wind.Icon;
                                Read_Icon_Options(icon);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                #endregion

                #region ElementMoon
                case "ElementMoon":
                    ElementMoon moon = (ElementMoon)element;
                    switch (uCtrl_EditableElements_Opt.selectedElement)
                    {
                        case "Images":
                            if (uCtrl_EditableElements_Opt.checkBox_Images.Checked)
                            {
                                hmUI_widget_IMG_LEVEL img_level = moon.Images;
                                Read_ImgLevel_Options(img_level, 30, true);
                                uCtrl_EditableElements_Opt.Collapse = true;
                            }
                            break;
                    }
                    break;
                    #endregion
            }

            PreviewImage();
        }

        private void uCtrl_EditableElements_Opt_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            if (editableElements.selected_zone < 0 || editableElements.Watchface_edit_group == null ||
                editableElements.Watchface_edit_group.Count <= editableElements.selected_zone) return;
            WATCHFACE_EDIT_GROUP watchface_edit_group = editableElements.Watchface_edit_group[editableElements.selected_zone];
            if (watchface_edit_group.selected_element < 0 || watchface_edit_group.Elements == null ||
                watchface_edit_group.Elements.Count <= watchface_edit_group.selected_element) return;

            List<string> subElements = new List<string>();
            Dictionary<string, int> elementOptions = uCtrl_EditableElements_Opt.GetOptionsPosition2();
            CheckBox checkBox = (CheckBox)sender;
            string name = checkBox.Name;

            Object element = watchface_edit_group.Elements[watchface_edit_group.selected_element];
            string type = element.GetType().Name;
            switch (type)
            {
                #region ElementDateDay
                case "ElementDateDay":
                    ElementDateDay dateDay = (ElementDateDay)element;
                    if (dateDay.Number == null) dateDay.Number = new hmUI_widget_IMG_NUMBER();
                    if (dateDay.Pointer == null) dateDay.Pointer = new hmUI_widget_IMG_POINTER();

                    if (elementOptions.ContainsKey("Number")) dateDay.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) dateDay.Pointer.position = elementOptions["Pointer"];

                    switch (name)
                    {
                        case "checkBox_Number":
                            dateDay.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            dateDay.Pointer.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementDateMonth
                case "ElementDateMonth":
                    ElementDateMonth dateMonth = (ElementDateMonth)element;
                    if (dateMonth.Images == null) dateMonth.Images = new hmUI_widget_IMG_LEVEL();
                    if (dateMonth.Number == null) dateMonth.Number = new hmUI_widget_IMG_NUMBER();
                    if (dateMonth.Pointer == null) dateMonth.Pointer = new hmUI_widget_IMG_POINTER();

                    if (elementOptions.ContainsKey("Images")) dateMonth.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Number")) dateMonth.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) dateMonth.Pointer.position = elementOptions["Pointer"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            dateMonth.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            dateMonth.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            dateMonth.Pointer.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementDateYear
                case "ElementDateYear":
                    ElementDateYear dateYear = (ElementDateYear)element;
                    if (dateYear.Number == null) dateYear.Number = new hmUI_widget_IMG_NUMBER();

                    if (elementOptions.ContainsKey("Number")) dateYear.Number.position = elementOptions["Number"];

                    switch (name)
                    {
                        case "checkBox_Number":
                            dateYear.Number.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementDateWeek
                case "ElementDateWeek":
                    ElementDateWeek dateWeek = (ElementDateWeek)element;
                    if (dateWeek.Images == null) dateWeek.Images = new hmUI_widget_IMG_LEVEL();
                    if (dateWeek.Pointer == null) dateWeek.Pointer = new hmUI_widget_IMG_POINTER();

                    if (elementOptions.ContainsKey("Images")) dateWeek.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Pointer")) dateWeek.Pointer.position = elementOptions["Pointer"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            dateWeek.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            dateWeek.Pointer.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion


                #region ElementSteps
                case "ElementSteps":
                    ElementSteps steps = (ElementSteps)element;
                    if (steps.Images == null) steps.Images = new hmUI_widget_IMG_LEVEL();
                    if (steps.Segments == null) steps.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (steps.Number == null) steps.Number = new hmUI_widget_IMG_NUMBER();
                    if (steps.Number_Target == null) steps.Number_Target = new hmUI_widget_IMG_NUMBER();
                    if (steps.Pointer == null) steps.Pointer = new hmUI_widget_IMG_POINTER();
                    if (steps.Circle_Scale == null) steps.Circle_Scale = new Circle_Scale();
                    if (steps.Linear_Scale == null) steps.Linear_Scale = new Linear_Scale();
                    if (steps.Icon == null) steps.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) steps.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) steps.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) steps.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Number_Target")) steps.Number_Target.position = elementOptions["Number_Target"];
                    if (elementOptions.ContainsKey("Pointer")) steps.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) steps.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) steps.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) steps.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            steps.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            steps.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            steps.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number_Target":
                            steps.Number_Target.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            steps.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            steps.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            steps.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            steps.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementBattery
                case "ElementBattery":
                    ElementBattery battery = (ElementBattery)element;
                    if (battery.Images == null) battery.Images = new hmUI_widget_IMG_LEVEL();
                    if (battery.Segments == null) battery.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (battery.Number == null) battery.Number = new hmUI_widget_IMG_NUMBER();
                    if (battery.Pointer == null) battery.Pointer = new hmUI_widget_IMG_POINTER();
                    if (battery.Circle_Scale == null) battery.Circle_Scale = new Circle_Scale();
                    if (battery.Linear_Scale == null) battery.Linear_Scale = new Linear_Scale();
                    if (battery.Icon == null) battery.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) battery.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) battery.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) battery.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) battery.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) battery.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) battery.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) battery.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            battery.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            battery.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            battery.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            battery.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            battery.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            battery.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            battery.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementCalories
                case "ElementCalories":
                    ElementCalories calories = (ElementCalories)element;
                    if (calories.Images == null) calories.Images = new hmUI_widget_IMG_LEVEL();
                    if (calories.Segments == null) calories.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (calories.Number == null) calories.Number = new hmUI_widget_IMG_NUMBER();
                    if (calories.Number_Target == null) calories.Number_Target = new hmUI_widget_IMG_NUMBER();
                    if (calories.Pointer == null) calories.Pointer = new hmUI_widget_IMG_POINTER();
                    if (calories.Circle_Scale == null) calories.Circle_Scale = new Circle_Scale();
                    if (calories.Linear_Scale == null) calories.Linear_Scale = new Linear_Scale();
                    if (calories.Icon == null) calories.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) calories.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) calories.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) calories.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Number_Target")) calories.Number_Target.position = elementOptions["Number_Target"];
                    if (elementOptions.ContainsKey("Pointer")) calories.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) calories.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) calories.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) calories.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            calories.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            calories.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            calories.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number_Target":
                            calories.Number_Target.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            calories.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            calories.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            calories.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            calories.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementHeart
                case "ElementHeart":
                    ElementHeart heart = (ElementHeart)element;
                    if (heart.Images == null) heart.Images = new hmUI_widget_IMG_LEVEL();
                    if (heart.Segments == null) heart.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (heart.Number == null) heart.Number = new hmUI_widget_IMG_NUMBER();
                    if (heart.Pointer == null) heart.Pointer = new hmUI_widget_IMG_POINTER();
                    if (heart.Circle_Scale == null) heart.Circle_Scale = new Circle_Scale();
                    if (heart.Linear_Scale == null) heart.Linear_Scale = new Linear_Scale();
                    if (heart.Icon == null) heart.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) heart.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) heart.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) heart.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) heart.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) heart.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) heart.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) heart.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            heart.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            heart.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            heart.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            heart.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            heart.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            heart.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            heart.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementPAI
                case "ElementPAI":
                    ElementPAI pai = (ElementPAI)element;
                    if (pai.Images == null) pai.Images = new hmUI_widget_IMG_LEVEL();
                    if (pai.Segments == null) pai.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (pai.Number == null) pai.Number = new hmUI_widget_IMG_NUMBER();
                    //if (pai.Number_Target == null) pai.Number_Target = new hmUI_widget_IMG_NUMBER();
                    if (pai.Pointer == null) pai.Pointer = new hmUI_widget_IMG_POINTER();
                    if (pai.Circle_Scale == null) pai.Circle_Scale = new Circle_Scale();
                    if (pai.Linear_Scale == null) pai.Linear_Scale = new Linear_Scale();
                    if (pai.Icon == null) pai.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) pai.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) pai.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) pai.Number.position = elementOptions["Number"];
                    //if (elementOptions.ContainsKey("Number_Target")) pai.Number_Target.position = elementOptions["Number_Target"];
                    if (elementOptions.ContainsKey("Pointer")) pai.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) pai.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) pai.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) pai.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            pai.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            pai.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            pai.Number.visible = checkBox.Checked;
                            break;
                        //case "checkBox_Number_Target":
                        //    pai.Number_Target.enable = checkBox.Checked;
                        //    break;
                        case "checkBox_Pointer":
                            pai.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            pai.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            pai.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            pai.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementDistance
                case "ElementDistance":
                    ElementDistance distance = (ElementDistance)element;
                    if (distance.Number == null) distance.Number = new hmUI_widget_IMG_NUMBER();

                    if (elementOptions.ContainsKey("Number")) distance.Number.position = elementOptions["Number"];

                    switch (name)
                    {
                        case "checkBox_Number":
                            distance.Number.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementStand
                case "ElementStand":
                    ElementStand stand = (ElementStand)element;
                    if (stand.Images == null) stand.Images = new hmUI_widget_IMG_LEVEL();
                    if (stand.Segments == null) stand.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (stand.Number == null) stand.Number = new hmUI_widget_IMG_NUMBER();
                    if (stand.Number_Target == null) stand.Number_Target = new hmUI_widget_IMG_NUMBER();
                    if (stand.Pointer == null) stand.Pointer = new hmUI_widget_IMG_POINTER();
                    if (stand.Circle_Scale == null) stand.Circle_Scale = new Circle_Scale();
                    if (stand.Linear_Scale == null) stand.Linear_Scale = new Linear_Scale();
                    if (stand.Icon == null) stand.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) stand.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) stand.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) stand.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Number_Target")) stand.Number_Target.position = elementOptions["Number_Target"];
                    if (elementOptions.ContainsKey("Pointer")) stand.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) stand.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) stand.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) stand.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            stand.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            stand.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            stand.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number_Target":
                            stand.Number_Target.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            stand.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            stand.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            stand.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            stand.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementActivity
                case "ElementActivity":
                    ElementActivity activity = (ElementActivity)element;
                    if (activity.Images == null) activity.Images = new hmUI_widget_IMG_LEVEL();
                    if (activity.Segments == null) activity.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (activity.Number == null) activity.Number = new hmUI_widget_IMG_NUMBER();
                    if (activity.Number_Target == null) activity.Number_Target = new hmUI_widget_IMG_NUMBER();
                    if (activity.Pointer == null) activity.Pointer = new hmUI_widget_IMG_POINTER();
                    if (activity.Circle_Scale == null) activity.Circle_Scale = new Circle_Scale();
                    if (activity.Linear_Scale == null) activity.Linear_Scale = new Linear_Scale();
                    if (activity.Icon == null) activity.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) activity.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) activity.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) activity.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Number_Target")) activity.Number_Target.position = elementOptions["Number_Target"];
                    if (elementOptions.ContainsKey("Pointer")) activity.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) activity.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) activity.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) activity.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            activity.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            activity.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            activity.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number_Target":
                            activity.Number_Target.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            activity.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            activity.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            activity.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            activity.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementSpO2
                case "ElementSpO2":
                    ElementSpO2 spo2 = (ElementSpO2)element;
                    if (spo2.Number == null) spo2.Number = new hmUI_widget_IMG_NUMBER();

                    if (elementOptions.ContainsKey("Number")) spo2.Number.position = elementOptions["Number"];

                    switch (name)
                    {
                        case "checkBox_Number":
                            spo2.Number.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementStress
                case "ElementStress":
                    ElementStress stress = (ElementStress)element;
                    if (stress.Images == null) stress.Images = new hmUI_widget_IMG_LEVEL();
                    if (stress.Segments == null) stress.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (stress.Number == null) stress.Number = new hmUI_widget_IMG_NUMBER();
                    if (stress.Pointer == null) stress.Pointer = new hmUI_widget_IMG_POINTER();
                    if (stress.Icon == null) stress.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) stress.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) stress.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) stress.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) stress.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Icon")) stress.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            stress.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            stress.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            stress.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            stress.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            stress.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementFatBurning
                case "ElementFatBurning":
                    ElementFatBurning fat_burning = (ElementFatBurning)element;
                    if (fat_burning.Images == null) fat_burning.Images = new hmUI_widget_IMG_LEVEL();
                    if (fat_burning.Segments == null) fat_burning.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (fat_burning.Number == null) fat_burning.Number = new hmUI_widget_IMG_NUMBER();
                    if (fat_burning.Number_Target == null) fat_burning.Number_Target = new hmUI_widget_IMG_NUMBER();
                    if (fat_burning.Pointer == null) fat_burning.Pointer = new hmUI_widget_IMG_POINTER();
                    if (fat_burning.Circle_Scale == null) fat_burning.Circle_Scale = new Circle_Scale();
                    if (fat_burning.Linear_Scale == null) fat_burning.Linear_Scale = new Linear_Scale();
                    if (fat_burning.Icon == null) fat_burning.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) fat_burning.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) fat_burning.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) fat_burning.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Number_Target")) fat_burning.Number_Target.position = elementOptions["Number_Target"];
                    if (elementOptions.ContainsKey("Pointer")) fat_burning.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Circle_Scale")) fat_burning.Circle_Scale.position = elementOptions["Circle_Scale"];
                    if (elementOptions.ContainsKey("Linear_Scale")) fat_burning.Linear_Scale.position = elementOptions["Linear_Scale"];
                    if (elementOptions.ContainsKey("Icon")) fat_burning.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            fat_burning.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            fat_burning.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            fat_burning.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number_Target":
                            fat_burning.Number_Target.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            fat_burning.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Circle_Scale":
                            fat_burning.Circle_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Linear_Scale":
                            fat_burning.Linear_Scale.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            fat_burning.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion



                #region ElementWeather
                case "ElementWeather":
                    ElementWeather weather = (ElementWeather)element;
                    if (weather.Images == null) weather.Images = new hmUI_widget_IMG_LEVEL();
                    if (weather.Number == null) weather.Number = new hmUI_widget_IMG_NUMBER();
                    if (weather.Number_Max == null) weather.Number_Max = new hmUI_widget_IMG_NUMBER();
                    if (weather.Number_Min == null) weather.Number_Min = new hmUI_widget_IMG_NUMBER();
                    if (weather.City_Name == null) weather.City_Name = new hmUI_widget_TEXT();
                    if (weather.Icon == null) weather.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) weather.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Number")) weather.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Number_Max")) weather.Number_Max.position = elementOptions["Number_Max"];
                    if (elementOptions.ContainsKey("Number_Min")) weather.Number_Min.position = elementOptions["Number_Min"];
                    if (elementOptions.ContainsKey("CityName")) weather.City_Name.position = elementOptions["CityName"];
                    if (elementOptions.ContainsKey("Icon")) weather.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            weather.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            weather.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number_Max":
                            weather.Number_Max.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number_Min":
                            weather.Number_Min.visible = checkBox.Checked;
                            break;
                        case "checkBox_Text_CityName":
                            weather.City_Name.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            weather.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementUVIndex
                case "ElementUVIndex":
                    ElementUVIndex uv_index = (ElementUVIndex)element;
                    if (uv_index.Images == null) uv_index.Images = new hmUI_widget_IMG_LEVEL();
                    if (uv_index.Segments == null) uv_index.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (uv_index.Number == null) uv_index.Number = new hmUI_widget_IMG_NUMBER();
                    if (uv_index.Pointer == null) uv_index.Pointer = new hmUI_widget_IMG_POINTER();
                    if (uv_index.Icon == null) uv_index.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) uv_index.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) uv_index.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) uv_index.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) uv_index.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Icon")) uv_index.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            uv_index.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            uv_index.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            uv_index.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            uv_index.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            uv_index.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementHumidity
                case "ElementHumidity":
                    ElementHumidity humidity = (ElementHumidity)element;
                    if (humidity.Images == null) humidity.Images = new hmUI_widget_IMG_LEVEL();
                    if (humidity.Segments == null) humidity.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (humidity.Number == null) humidity.Number = new hmUI_widget_IMG_NUMBER();
                    if (humidity.Pointer == null) humidity.Pointer = new hmUI_widget_IMG_POINTER();
                    if (humidity.Icon == null) humidity.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) humidity.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) humidity.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) humidity.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) humidity.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Icon")) humidity.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            humidity.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            humidity.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            humidity.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            humidity.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            humidity.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementAltimeter
                case "ElementAltimeter":
                    ElementAltimeter altimeter = (ElementAltimeter)element;
                    if (altimeter.Number == null) altimeter.Number = new hmUI_widget_IMG_NUMBER();
                    if (altimeter.Pointer == null) altimeter.Pointer = new hmUI_widget_IMG_POINTER();
                    if (altimeter.Icon == null) altimeter.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Number")) altimeter.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) altimeter.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Icon")) altimeter.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Number":
                            altimeter.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            altimeter.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            altimeter.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementSunrise
                case "ElementSunrise":
                    ElementSunrise sunrise = (ElementSunrise)element;
                    if (sunrise.Images == null) sunrise.Images = new hmUI_widget_IMG_LEVEL();
                    if (sunrise.Segments == null) sunrise.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (sunrise.Sunrise == null) sunrise.Sunrise = new hmUI_widget_IMG_NUMBER();
                    if (sunrise.Sunset == null) sunrise.Sunset = new hmUI_widget_IMG_NUMBER();
                    if (sunrise.Sunset_Sunrise == null) sunrise.Sunset_Sunrise = new hmUI_widget_IMG_NUMBER();
                    if (sunrise.Pointer == null) sunrise.Pointer = new hmUI_widget_IMG_POINTER();
                    if (sunrise.Icon == null) sunrise.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) sunrise.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) sunrise.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Sunrise")) sunrise.Sunrise.position = elementOptions["Sunrise"];
                    if (elementOptions.ContainsKey("Sunset")) sunrise.Sunset.position = elementOptions["Sunset"];
                    if (elementOptions.ContainsKey("Sunset_Sunrise")) sunrise.Sunset_Sunrise.position = elementOptions["Sunset_Sunrise"];
                    if (elementOptions.ContainsKey("Pointer")) sunrise.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Icon")) sunrise.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            sunrise.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            sunrise.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Sunrise":
                            sunrise.Sunrise.visible = checkBox.Checked;
                            break;
                        case "checkBox_Sunset":
                            sunrise.Sunset.visible = checkBox.Checked;
                            break;
                        case "checkBox_Sunset_Sunrise":
                            sunrise.Sunset_Sunrise.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            sunrise.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            sunrise.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementWind
                case "ElementWind":
                    ElementWind wind = (ElementWind)element;
                    if (wind.Images == null) wind.Images = new hmUI_widget_IMG_LEVEL();
                    if (wind.Segments == null) wind.Segments = new hmUI_widget_IMG_PROGRESS();
                    if (wind.Number == null) wind.Number = new hmUI_widget_IMG_NUMBER();
                    if (wind.Pointer == null) wind.Pointer = new hmUI_widget_IMG_POINTER();
                    if (wind.Direction == null) wind.Direction = new hmUI_widget_IMG_LEVEL();
                    if (wind.Icon == null) wind.Icon = new hmUI_widget_IMG();

                    if (elementOptions.ContainsKey("Images")) wind.Images.position = elementOptions["Images"];
                    if (elementOptions.ContainsKey("Segments")) wind.Segments.position = elementOptions["Segments"];
                    if (elementOptions.ContainsKey("Number")) wind.Number.position = elementOptions["Number"];
                    if (elementOptions.ContainsKey("Pointer")) wind.Pointer.position = elementOptions["Pointer"];
                    if (elementOptions.ContainsKey("Wind_Direction")) wind.Direction.position = elementOptions["Wind_Direction"];
                    if (elementOptions.ContainsKey("Icon")) wind.Icon.position = elementOptions["Icon"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            wind.Images.visible = checkBox.Checked;
                            break;
                        case "checkBox_Segments":
                            wind.Segments.visible = checkBox.Checked;
                            break;
                        case "checkBox_Number":
                            wind.Number.visible = checkBox.Checked;
                            break;
                        case "checkBox_Pointer":
                            wind.Pointer.visible = checkBox.Checked;
                            break;
                        case "checkBox_Direction":
                            wind.Direction.visible = checkBox.Checked;
                            break;
                        case "checkBox_Icon":
                            wind.Icon.visible = checkBox.Checked;
                            break;
                    }
                    break;
                #endregion

                #region ElementMoon
                case "ElementMoon":
                    ElementMoon moon = (ElementMoon)element;
                    if (moon.Images == null) moon.Images = new hmUI_widget_IMG_LEVEL();

                    if (elementOptions.ContainsKey("Images")) moon.Images.position = elementOptions["Images"];

                    switch (name)
                    {
                        case "checkBox_Images":
                            moon.Images.visible = checkBox.Checked;
                            break;
                    }
                    break;
                    #endregion
            }


            uCtrl_EditableElements_Opt_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Opt_PreviewElementAdd(object sender, EventArgs eventArgs, int index)
        {
            if (index < 0) return;
            if (Watch_Face == null || Watch_Face.Editable_Elements == null ||
                Watch_Face.Editable_Elements.Watchface_edit_group == null)
            {
                return;
            }
            int selected_zone = Watch_Face.Editable_Elements.selected_zone;
            if (selected_zone < 0) return;
            if (selected_zone >= Watch_Face.Editable_Elements.Watchface_edit_group.Count ||
                Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone].optional_types_list == null ||
                index >= Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone].optional_types_list.Count)
            {
                return;
            }

            Optional_Types_List optional_types_list = Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone].optional_types_list[index];
            if (FileName != null && FullFileDir != null) // проект уже сохранен
            {
                // формируем картинку для предпросмотра
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        break;
                    case "GTR 4":
                        bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        break;
                    case "Amazfit Band 7":
                        bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
                        break;
                    case "GTS 4 mini":
                        bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        break;
                    case "Falcon":
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, false, false, false, true, false,
                    false, false, link, false, false, -1, false, 0);
                if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);

                // обрезаем по маске
                if (checkBox_crop.Checked && Watch_Face.Editable_Elements.fg_mask != null && Watch_Face.Editable_Elements.fg_mask.Length > 0)
                {
                    int maskIndex = ListImages.LastIndexOf(Watch_Face.Editable_Elements.fg_mask);
                    string fg_mask = ListImagesFullName[maskIndex];
                    if (File.Exists(fg_mask)) bitmap = ApplyWidgetMask(bitmap, fg_mask);
                }

                // обрезаем по размеру зоны
                WATCHFACE_EDIT_GROUP watchface_edit_group = Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone];
                int x = watchface_edit_group.x;
                int y = watchface_edit_group.y;
                int width = watchface_edit_group.w;
                int height = watchface_edit_group.h;
                if (width > 1 && height > 1)
                {
                    Rectangle cropRect = new Rectangle(x, y, width, height);
                    Bitmap tempBitmap = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(tempBitmap))
                    {
                        g.DrawImage(bitmap, new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                                         cropRect, GraphicsUnit.Pixel);
                    }
                    bitmap = tempBitmap;
                }

                // определяем имя файла для сохранения и сохраняем файл
                int i = 1;
                string tempName = "ez(" + (selected_zone + 1).ToString() + ")_" + optional_types_list.type;
                string NamePreview = tempName + ".png";
                string PathPreview = FullFileDir + @"\assets\" + NamePreview;
                while (File.Exists(PathPreview) && i < 10)
                {
                    NamePreview = tempName + "_" + i.ToString() + ".png";
                    PathPreview = FullFileDir + @"\assets\" + NamePreview;
                    i++;
                    if (i > 9)
                    {
                        MessageBox.Show(Properties.FormStrings.Message_Wrong_Preview_Exists,
                            Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                bitmap.Save(PathPreview, ImageFormat.Png);
                string fileNameOnly = Path.GetFileNameWithoutExtension(PathPreview);

                PreviewView = false;

                LoadImage(Path.GetDirectoryName(PathPreview) + @"\");

                optional_types_list.preview = fileNameOnly;
                PreviewView = true;
                JSON_Modified = true;
                FormText();
                Read_EditableElements_Options(Watch_Face.Editable_Elements);
                //HideAllElemenrOptions();
                //ResetHighlightState("");

                bitmap.Dispose();

                PreviewImage();
            }
        }

        private void uCtrl_EditableElements_Opt_PreviewElementRefresh(object sender, EventArgs eventArgs, int index)
        {
            if (index < 0) return;
            if (Watch_Face == null || Watch_Face.Editable_Elements == null ||
                Watch_Face.Editable_Elements.Watchface_edit_group == null)
            {
                return;
            }
            int selected_zone = Watch_Face.Editable_Elements.selected_zone;
            if (selected_zone < 0) return;
            if (selected_zone >= Watch_Face.Editable_Elements.Watchface_edit_group.Count ||
                Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone].optional_types_list == null ||
                index >= Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone].optional_types_list.Count)
            {
                return;
            }

            Optional_Types_List optional_types_list = Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone].optional_types_list[index];
            if (optional_types_list.preview == null || optional_types_list.preview.Length < 1)
            {
                uCtrl_EditableElements_Opt_PreviewElementAdd(null, null, index);
                return;
            }

            if (optional_types_list.preview.Length > 0)
            {
                string preview = FullFileDir + @"\assets\" +
                    optional_types_list.preview + ".png";
                if (!File.Exists(preview))
                {
                    optional_types_list.preview = "";
                    uCtrl_EditableElements_Opt_PreviewElementAdd(null, null, index);
                    return;
                }
                // формируем картинку для предпросмотра
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        break;
                    case "GTR 4":
                        bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        break;
                    case "Amazfit Band 7":
                        bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
                        break;
                    case "GTS 4 mini":
                        bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        break;
                    case "Falcon":
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, false, false, false, true, false,
                    false, false, link, false, false, -1, false, 0);
                if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);

                // обрезаем по маске
                if (checkBox_crop.Checked && Watch_Face.Editable_Elements.fg_mask != null && Watch_Face.Editable_Elements.fg_mask.Length > 0)
                {
                    int maskIndex = ListImages.LastIndexOf(Watch_Face.Editable_Elements.fg_mask);
                    string fg_mask = ListImagesFullName[maskIndex];
                    if (File.Exists(fg_mask)) bitmap = ApplyWidgetMask(bitmap, fg_mask);
                }

                // обрезаем по размеру зоны
                WATCHFACE_EDIT_GROUP watchface_edit_group = Watch_Face.Editable_Elements.Watchface_edit_group[selected_zone];
                int x = watchface_edit_group.x;
                int y = watchface_edit_group.y;
                int width = watchface_edit_group.w;
                int height = watchface_edit_group.h;
                if (width > 1 && height > 1)
                {
                    Rectangle cropRect = new Rectangle(x, y, width, height);
                    Bitmap tempBitmap = new Bitmap(width, height);
                    using (Graphics g = Graphics.FromImage(tempBitmap))
                    {
                        g.DrawImage(bitmap, new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                                         cropRect, GraphicsUnit.Pixel);
                    }
                    bitmap = tempBitmap;
                }

                bitmap.Save(preview, ImageFormat.Png);

                bitmap.Dispose();

                LoadImage(Path.GetDirectoryName(preview) + @"\");
                Read_EditableElements_Options(Watch_Face.Editable_Elements);

                PreviewImage();
            }
        }

        private void uCtrl_EditableElements_Opt_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            EditableElements editableElements = (EditableElements)uCtrl_EditableElements_Opt._EditableElemets;
            if (editableElements == null) return;
            if (editableElements.selected_zone < 0 || editableElements.Watchface_edit_group == null ||
                editableElements.Watchface_edit_group.Count <= editableElements.selected_zone) return;
            WATCHFACE_EDIT_GROUP watchface_edit_group = editableElements.Watchface_edit_group[editableElements.selected_zone];
            if (watchface_edit_group.selected_element < 0 || watchface_edit_group.Elements == null ||
                watchface_edit_group.Elements.Count <= watchface_edit_group.selected_element) return;

            Object element = watchface_edit_group.Elements[watchface_edit_group.selected_element];
            string type = element.GetType().Name;
            switch (type)
            {
                #region ElementDateDay
                case "ElementDateDay":
                    ElementDateDay dateDay = (ElementDateDay)element;
                    if (dateDay != null)
                    {
                        if (dateDay.Number == null) dateDay.Number = new hmUI_widget_IMG_NUMBER();
                        if (dateDay.Pointer == null) dateDay.Pointer = new hmUI_widget_IMG_POINTER();

                        //Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                        if (elementOptions.ContainsKey("Number")) dateDay.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) dateDay.Pointer.position = elementOptions["Pointer"];

                    }
                    break;
                #endregion

                #region ElementDateMonth
                case "ElementDateMonth":
                    ElementDateMonth dateMonth = (ElementDateMonth)element;
                    if (dateMonth != null)
                    {
                        if (dateMonth.Number == null) dateMonth.Number = new hmUI_widget_IMG_NUMBER();
                        if (dateMonth.Pointer == null) dateMonth.Pointer = new hmUI_widget_IMG_POINTER();
                        if (dateMonth.Images == null) dateMonth.Images = new hmUI_widget_IMG_LEVEL();

                        //Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                        if (elementOptions.ContainsKey("Number")) dateMonth.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) dateMonth.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Images")) dateMonth.Images.position = elementOptions["Images"];

                    }
                    break;
                #endregion

                #region ElementDateWeek
                case "ElementDateWeek":
                    ElementDateWeek dateWeek = (ElementDateWeek)element;
                    if (dateWeek != null)
                    {
                        if (dateWeek.Pointer == null) dateWeek.Pointer = new hmUI_widget_IMG_POINTER();
                        if (dateWeek.Images == null) dateWeek.Images = new hmUI_widget_IMG_LEVEL();

                        if (elementOptions.ContainsKey("Pointer")) dateWeek.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Images")) dateWeek.Images.position = elementOptions["Images"];

                    }
                    break;
                #endregion


                #region ElementSteps
                case "ElementSteps":
                    ElementSteps steps = (ElementSteps)element;
                    if (steps != null)
                    {
                        if (steps.Images == null) steps.Images = new hmUI_widget_IMG_LEVEL();
                        if (steps.Segments == null) steps.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (steps.Number == null) steps.Number = new hmUI_widget_IMG_NUMBER();
                        if (steps.Number_Target == null) steps.Number_Target = new hmUI_widget_IMG_NUMBER();
                        if (steps.Pointer == null) steps.Pointer = new hmUI_widget_IMG_POINTER();
                        if (steps.Circle_Scale == null) steps.Circle_Scale = new Circle_Scale();
                        if (steps.Linear_Scale == null) steps.Linear_Scale = new Linear_Scale();
                        if (steps.Icon == null) steps.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) steps.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) steps.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) steps.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Number_Target")) steps.Number_Target.position = elementOptions["Number_Target"];
                        if (elementOptions.ContainsKey("Pointer")) steps.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) steps.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) steps.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) steps.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementBattery
                case "ElementBattery":
                    ElementBattery battery = (ElementBattery)element;
                    if (battery != null)
                    {
                        if (battery.Images == null) battery.Images = new hmUI_widget_IMG_LEVEL();
                        if (battery.Segments == null) battery.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (battery.Number == null) battery.Number = new hmUI_widget_IMG_NUMBER();
                        if (battery.Pointer == null) battery.Pointer = new hmUI_widget_IMG_POINTER();
                        if (battery.Circle_Scale == null) battery.Circle_Scale = new Circle_Scale();
                        if (battery.Linear_Scale == null) battery.Linear_Scale = new Linear_Scale();
                        if (battery.Icon == null) battery.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) battery.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) battery.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) battery.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) battery.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) battery.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) battery.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) battery.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementCalories
                case "ElementCalories":
                    ElementCalories calories = (ElementCalories)element;
                    if (calories != null)
                    {
                        if (calories.Images == null) calories.Images = new hmUI_widget_IMG_LEVEL();
                        if (calories.Segments == null) calories.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (calories.Number == null) calories.Number = new hmUI_widget_IMG_NUMBER();
                        if (calories.Number_Target == null) calories.Number_Target = new hmUI_widget_IMG_NUMBER();
                        if (calories.Pointer == null) calories.Pointer = new hmUI_widget_IMG_POINTER();
                        if (calories.Circle_Scale == null) calories.Circle_Scale = new Circle_Scale();
                        if (calories.Linear_Scale == null) calories.Linear_Scale = new Linear_Scale();
                        if (calories.Icon == null) calories.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) calories.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) calories.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) calories.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Number_Target")) calories.Number_Target.position = elementOptions["Number_Target"];
                        if (elementOptions.ContainsKey("Pointer")) calories.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) calories.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) calories.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) calories.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementHeart
                case "ElementHeart":
                    ElementHeart heart = (ElementHeart)element;
                    if (heart != null)
                    {
                        if (heart.Images == null) heart.Images = new hmUI_widget_IMG_LEVEL();
                        if (heart.Segments == null) heart.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (heart.Number == null) heart.Number = new hmUI_widget_IMG_NUMBER();
                        if (heart.Pointer == null) heart.Pointer = new hmUI_widget_IMG_POINTER();
                        if (heart.Circle_Scale == null) heart.Circle_Scale = new Circle_Scale();
                        if (heart.Linear_Scale == null) heart.Linear_Scale = new Linear_Scale();
                        if (heart.Icon == null) heart.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) heart.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) heart.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) heart.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) heart.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) heart.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) heart.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) heart.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementPAI
                case "ElementPAI":
                    ElementPAI pai = (ElementPAI)element;
                    if (pai != null)
                    {
                        if (pai.Images == null) pai.Images = new hmUI_widget_IMG_LEVEL();
                        if (pai.Segments == null) pai.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (pai.Number == null) pai.Number = new hmUI_widget_IMG_NUMBER();
                        if (pai.Number_Target == null) pai.Number_Target = new hmUI_widget_IMG_NUMBER();
                        if (pai.Pointer == null) pai.Pointer = new hmUI_widget_IMG_POINTER();
                        if (pai.Circle_Scale == null) pai.Circle_Scale = new Circle_Scale();
                        if (pai.Linear_Scale == null) pai.Linear_Scale = new Linear_Scale();
                        if (pai.Icon == null) pai.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) pai.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) pai.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) pai.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Number_Target")) pai.Number_Target.position = elementOptions["Number_Target"];
                        if (elementOptions.ContainsKey("Pointer")) pai.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) pai.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) pai.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) pai.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementStand
                case "ElementStand":
                    ElementStand stand = (ElementStand)element;
                    if (stand != null)
                    {
                        if (stand.Images == null) stand.Images = new hmUI_widget_IMG_LEVEL();
                        if (stand.Segments == null) stand.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (stand.Number == null) stand.Number = new hmUI_widget_IMG_NUMBER();
                        if (stand.Number_Target == null) stand.Number_Target = new hmUI_widget_IMG_NUMBER();
                        if (stand.Pointer == null) stand.Pointer = new hmUI_widget_IMG_POINTER();
                        if (stand.Circle_Scale == null) stand.Circle_Scale = new Circle_Scale();
                        if (stand.Linear_Scale == null) stand.Linear_Scale = new Linear_Scale();
                        if (stand.Icon == null) stand.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) stand.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) stand.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) stand.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Number_Target")) stand.Number_Target.position = elementOptions["Number_Target"];
                        if (elementOptions.ContainsKey("Pointer")) stand.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) stand.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) stand.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) stand.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementActivity
                case "ElementActivity":
                    ElementActivity activity = (ElementActivity)element;
                    if (activity != null)
                    {
                        if (activity.Images == null) activity.Images = new hmUI_widget_IMG_LEVEL();
                        if (activity.Segments == null) activity.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (activity.Number == null) activity.Number = new hmUI_widget_IMG_NUMBER();
                        if (activity.Number_Target == null) activity.Number_Target = new hmUI_widget_IMG_NUMBER();
                        if (activity.Pointer == null) activity.Pointer = new hmUI_widget_IMG_POINTER();
                        if (activity.Circle_Scale == null) activity.Circle_Scale = new Circle_Scale();
                        if (activity.Linear_Scale == null) activity.Linear_Scale = new Linear_Scale();
                        if (activity.Icon == null) activity.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) activity.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) activity.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) activity.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Number_Target")) activity.Number_Target.position = elementOptions["Number_Target"];
                        if (elementOptions.ContainsKey("Pointer")) activity.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) activity.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) activity.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) activity.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementSpO2
                case "ElementSpO2":
                    ElementSpO2 spo2 = (ElementSpO2)element;
                    if (spo2 != null)
                    {
                        if (spo2.Number == null) spo2.Number = new hmUI_widget_IMG_NUMBER();

                        if (elementOptions.ContainsKey("Number")) spo2.Number.position = elementOptions["Number"];
                    }
                    break;
                #endregion

                #region ElementStress
                case "ElementStress":
                    ElementStress stress = (ElementStress)element;
                    if (stress != null)
                    {
                        if (stress.Images == null) stress.Images = new hmUI_widget_IMG_LEVEL();
                        if (stress.Segments == null) stress.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (stress.Number == null) stress.Number = new hmUI_widget_IMG_NUMBER();
                        if (stress.Pointer == null) stress.Pointer = new hmUI_widget_IMG_POINTER();
                        if (stress.Icon == null) stress.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) stress.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) stress.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) stress.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) stress.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Icon")) stress.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementFatBurning
                case "ElementFatBurning":
                    ElementFatBurning fat_burning = (ElementFatBurning)element;
                    if (fat_burning != null)
                    {
                        if (fat_burning.Images == null) fat_burning.Images = new hmUI_widget_IMG_LEVEL();
                        if (fat_burning.Segments == null) fat_burning.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (fat_burning.Number == null) fat_burning.Number = new hmUI_widget_IMG_NUMBER();
                        if (fat_burning.Number_Target == null) fat_burning.Number_Target = new hmUI_widget_IMG_NUMBER();
                        if (fat_burning.Pointer == null) fat_burning.Pointer = new hmUI_widget_IMG_POINTER();
                        if (fat_burning.Circle_Scale == null) fat_burning.Circle_Scale = new Circle_Scale();
                        if (fat_burning.Linear_Scale == null) fat_burning.Linear_Scale = new Linear_Scale();
                        if (fat_burning.Icon == null) fat_burning.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) fat_burning.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) fat_burning.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) fat_burning.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Number_Target")) fat_burning.Number_Target.position = elementOptions["Number_Target"];
                        if (elementOptions.ContainsKey("Pointer")) fat_burning.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Circle_Scale")) fat_burning.Circle_Scale.position = elementOptions["Circle_Scale"];
                        if (elementOptions.ContainsKey("Linear_Scale")) fat_burning.Linear_Scale.position = elementOptions["Linear_Scale"];
                        if (elementOptions.ContainsKey("Icon")) fat_burning.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion



                #region ElementWeather
                case "ElementWeather":
                    ElementWeather weather = (ElementWeather)element;
                    if (weather != null)
                    {
                        if (weather.Images == null) weather.Images = new hmUI_widget_IMG_LEVEL();
                        if (weather.Number == null) weather.Number = new hmUI_widget_IMG_NUMBER();
                        if (weather.Number_Max == null) weather.Number_Max = new hmUI_widget_IMG_NUMBER();
                        if (weather.Number_Min == null) weather.Number_Min = new hmUI_widget_IMG_NUMBER();
                        if (weather.City_Name == null) weather.City_Name = new hmUI_widget_TEXT()
;                        if (weather.Icon == null) weather.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) weather.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Number")) weather.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Number_Max")) weather.Number_Max.position = elementOptions["Number_Max"];
                        if (elementOptions.ContainsKey("Number_Min")) weather.Number_Min.position = elementOptions["Number_Min"];
                        if (elementOptions.ContainsKey("CityName")) weather.City_Name.position = elementOptions["CityName"];
                        if (elementOptions.ContainsKey("Icon")) weather.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementUVIndex
                case "ElementUVIndex":
                    ElementUVIndex uv_index = (ElementUVIndex)element;
                    if (uv_index != null)
                    {
                        if (uv_index.Images == null) uv_index.Images = new hmUI_widget_IMG_LEVEL();
                        if (uv_index.Segments == null) uv_index.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (uv_index.Number == null) uv_index.Number = new hmUI_widget_IMG_NUMBER();
                        if (uv_index.Pointer == null) uv_index.Pointer = new hmUI_widget_IMG_POINTER();
                        if (uv_index.Icon == null) uv_index.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) uv_index.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) uv_index.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) uv_index.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) uv_index.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Icon")) uv_index.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementHumidity
                case "ElementHumidity":
                    ElementHumidity humidity = (ElementHumidity)element;
                    if (humidity != null)
                    {
                        if (humidity.Images == null) humidity.Images = new hmUI_widget_IMG_LEVEL();
                        if (humidity.Segments == null) humidity.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (humidity.Number == null) humidity.Number = new hmUI_widget_IMG_NUMBER();
                        if (humidity.Pointer == null) humidity.Pointer = new hmUI_widget_IMG_POINTER();
                        if (humidity.Icon == null) humidity.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) humidity.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) humidity.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) humidity.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) humidity.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Icon")) humidity.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementAltimeter
                case "ElementAltimeter":
                    ElementAltimeter altimeter = (ElementAltimeter)element;
                    if (altimeter != null)
                    {
                        if (altimeter.Number == null) altimeter.Number = new hmUI_widget_IMG_NUMBER();
                        if (altimeter.Pointer == null) altimeter.Pointer = new hmUI_widget_IMG_POINTER();
                        if (altimeter.Icon == null) altimeter.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Number")) altimeter.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) altimeter.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Icon")) altimeter.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementSunrise
                case "ElementSunrise":
                    ElementSunrise sunrise = (ElementSunrise)element;
                    if (sunrise != null)
                    {
                        if (sunrise.Images == null) sunrise.Images = new hmUI_widget_IMG_LEVEL();
                        if (sunrise.Segments == null) sunrise.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (sunrise.Sunrise == null) sunrise.Sunrise = new hmUI_widget_IMG_NUMBER();
                        if (sunrise.Sunset == null) sunrise.Sunset = new hmUI_widget_IMG_NUMBER();
                        if (sunrise.Sunset_Sunrise == null) sunrise.Sunset_Sunrise = new hmUI_widget_IMG_NUMBER();
                        if (sunrise.Pointer == null) sunrise.Pointer = new hmUI_widget_IMG_POINTER();
                        if (sunrise.Icon == null) sunrise.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) sunrise.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) sunrise.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Sunrise")) sunrise.Sunrise.position = elementOptions["Sunrise"];
                        if (elementOptions.ContainsKey("Sunset")) sunrise.Sunset.position = elementOptions["Sunset"];
                        if (elementOptions.ContainsKey("Sunset_Sunrise")) sunrise.Sunset_Sunrise.position = elementOptions["Sunset_Sunrise"];
                        if (elementOptions.ContainsKey("Pointer")) sunrise.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Icon")) sunrise.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementWind
                case "ElementWind":
                    ElementWind wind = (ElementWind)element;
                    if (wind != null)
                    {
                        if (wind.Images == null) wind.Images = new hmUI_widget_IMG_LEVEL();
                        if (wind.Segments == null) wind.Segments = new hmUI_widget_IMG_PROGRESS();
                        if (wind.Number == null) wind.Number = new hmUI_widget_IMG_NUMBER();
                        if (wind.Pointer == null) wind.Pointer = new hmUI_widget_IMG_POINTER();
                        if (wind.Direction == null) wind.Direction = new hmUI_widget_IMG_LEVEL();
                        if (wind.Icon == null) wind.Icon = new hmUI_widget_IMG();

                        if (elementOptions.ContainsKey("Images")) wind.Images.position = elementOptions["Images"];
                        if (elementOptions.ContainsKey("Segments")) wind.Segments.position = elementOptions["Segments"];
                        if (elementOptions.ContainsKey("Number")) wind.Number.position = elementOptions["Number"];
                        if (elementOptions.ContainsKey("Pointer")) wind.Pointer.position = elementOptions["Pointer"];
                        if (elementOptions.ContainsKey("Wind_Direction")) wind.Direction.position = elementOptions["Wind_Direction"];
                        if (elementOptions.ContainsKey("Icon")) wind.Icon.position = elementOptions["Icon"];
                    }
                    break;
                #endregion

                #region ElementMoon
                case "ElementMoon":
                    ElementMoon moon = (ElementMoon)element;
                    if (moon != null)
                    {
                        if (moon.Images == null) moon.Images = new hmUI_widget_IMG_LEVEL();

                        if (elementOptions.ContainsKey("Images")) moon.Images.position = elementOptions["Images"];
                    }
                    break;
                    #endregion
            }

            

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private Dictionary<int, string> ReadElementPos(Object element)
        {
            Dictionary<int, string> elementOptions = new Dictionary<int, string>();
            if (element != null)
            {
                string type = element.GetType().Name;
                switch (type)
                {
                    #region ElementDigitalTime
                    case "ElementDigitalTime":
                        ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                        if (DigitalTime.Second != null && !elementOptions.ContainsKey(DigitalTime.Second.position) &&
                            !elementOptions.ContainsValue("Second"))
                        {
                            elementOptions.Add(DigitalTime.Second.position, "Second");
                        }
                        if (DigitalTime.Minute != null && !elementOptions.ContainsKey(DigitalTime.Minute.position) &&
                            !elementOptions.ContainsValue("Minute"))
                        {
                            elementOptions.Add(DigitalTime.Minute.position, "Minute");
                        }
                        if (DigitalTime.Hour != null && !elementOptions.ContainsKey(DigitalTime.Hour.position) &&
                            !elementOptions.ContainsValue("Hour"))
                        {
                            elementOptions.Add(DigitalTime.Hour.position, "Hour");
                        }
                        if (DigitalTime.AmPm != null && !elementOptions.ContainsKey(DigitalTime.AmPm.position) &&
                            !elementOptions.ContainsValue("AmPm"))
                        {
                            elementOptions.Add(DigitalTime.AmPm.position, "AmPm");
                        }
                        break;
                    #endregion

                    #region ElementAnalogTime
                    case "ElementAnalogTime":
                        ElementAnalogTime AnalogTime = (ElementAnalogTime)element;
                        if (AnalogTime.Second != null) elementOptions.Add(AnalogTime.Second.position, "Second");
                        if (AnalogTime.Minute != null) elementOptions.Add(AnalogTime.Minute.position, "Minute");
                        if (AnalogTime.Hour != null) elementOptions.Add(AnalogTime.Hour.position, "Hour");
                        break;
                    #endregion

                    #region ElementDateDay
                    case "ElementDateDay":
                        ElementDateDay DateDay = (ElementDateDay)element;
                        if (DateDay.Number != null) elementOptions.Add(DateDay.Number.position, "Number");
                        if (DateDay.Pointer != null) elementOptions.Add(DateDay.Pointer.position, "Pointer");
                        break;
                    #endregion

                    #region ElementDateMonth
                    case "ElementDateMonth":
                        ElementDateMonth DateMonth = (ElementDateMonth)element;
                        if (DateMonth.Number != null) elementOptions.Add(DateMonth.Number.position, "Number");
                        if (DateMonth.Pointer != null) elementOptions.Add(DateMonth.Pointer.position, "Pointer");
                        if (DateMonth.Images != null) elementOptions.Add(DateMonth.Images.position, "Images");
                        break;
                    #endregion

                    #region ElementDateYear
                    //case "ElementDateYear":
                    //    ElementDateYear DateYear = (ElementDateYear)element;
                    //    break;
                    #endregion

                    #region ElementDateWeek
                    case "ElementDateWeek":
                        ElementDateWeek DateWeek = (ElementDateWeek)element;
                        if (DateWeek.Pointer != null) elementOptions.Add(DateWeek.Pointer.position, "Pointer");
                        if (DateWeek.Images != null) elementOptions.Add(DateWeek.Images.position, "Images");
                        break;
                    #endregion



                    #region ElementShortcuts
                    //case "ElementShortcuts":
                    //    ElementShortcuts Shortcuts = (ElementShortcuts)element;
                    //    uCtrl_Shortcuts_Elm.SetVisibilityElementStatus(Shortcuts.enable);
                    //    break;
                    #endregion


                    #region ElementSteps
                    case "ElementSteps":
                        ElementSteps Steps = (ElementSteps)element;
                        if (Steps.Images != null) elementOptions.Add(Steps.Images.position, "Images");
                        if (Steps.Segments != null) elementOptions.Add(Steps.Segments.position, "Segments");
                        if (Steps.Number != null) elementOptions.Add(Steps.Number.position, "Number");
                        if (Steps.Number_Target != null) elementOptions.Add(Steps.Number_Target.position, "Number_Target");
                        if (Steps.Pointer != null) elementOptions.Add(Steps.Pointer.position, "Pointer");
                        if (Steps.Circle_Scale != null) elementOptions.Add(Steps.Circle_Scale.position, "Circle_Scale");
                        if (Steps.Linear_Scale != null) elementOptions.Add(Steps.Linear_Scale.position, "Linear_Scale");
                        if (Steps.Icon != null) elementOptions.Add(Steps.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementBattery
                    case "ElementBattery":
                        ElementBattery Battery = (ElementBattery)element;
                        if (Battery.Images != null) elementOptions.Add(Battery.Images.position, "Images");
                        if (Battery.Segments != null) elementOptions.Add(Battery.Segments.position, "Segments");
                        if (Battery.Number != null) elementOptions.Add(Battery.Number.position, "Number");
                        if (Battery.Pointer != null) elementOptions.Add(Battery.Pointer.position, "Pointer");
                        if (Battery.Circle_Scale != null) elementOptions.Add(Battery.Circle_Scale.position, "Circle_Scale");
                        if (Battery.Linear_Scale != null) elementOptions.Add(Battery.Linear_Scale.position, "Linear_Scale");
                        if (Battery.Icon != null) elementOptions.Add(Battery.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementCalories
                    case "ElementCalories":
                        ElementCalories Calories = (ElementCalories)element;
                        if (Calories.Images != null) elementOptions.Add(Calories.Images.position, "Images");
                        if (Calories.Segments != null) elementOptions.Add(Calories.Segments.position, "Segments");
                        if (Calories.Number != null) elementOptions.Add(Calories.Number.position, "Number");
                        if (Calories.Number_Target != null) elementOptions.Add(Calories.Number_Target.position, "Number_Target");
                        if (Calories.Pointer != null) elementOptions.Add(Calories.Pointer.position, "Pointer");
                        if (Calories.Circle_Scale != null) elementOptions.Add(Calories.Circle_Scale.position, "Circle_Scale");
                        if (Calories.Linear_Scale != null) elementOptions.Add(Calories.Linear_Scale.position, "Linear_Scale");
                        if (Calories.Icon != null) elementOptions.Add(Calories.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementHeart
                    case "ElementHeart":
                        ElementHeart Heart = (ElementHeart)element;
                        if (Heart.Images != null) elementOptions.Add(Heart.Images.position, "Images");
                        if (Heart.Segments != null) elementOptions.Add(Heart.Segments.position, "Segments");
                        if (Heart.Number != null) elementOptions.Add(Heart.Number.position, "Number");
                        if (Heart.Pointer != null) elementOptions.Add(Heart.Pointer.position, "Pointer");
                        if (Heart.Circle_Scale != null) elementOptions.Add(Heart.Circle_Scale.position, "Circle_Scale");
                        if (Heart.Linear_Scale != null) elementOptions.Add(Heart.Linear_Scale.position, "Linear_Scale");
                        if (Heart.Icon != null) elementOptions.Add(Heart.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementPAI
                    case "ElementPAI":
                        ElementPAI PAI = (ElementPAI)element;
                        if (PAI.Images != null) elementOptions.Add(PAI.Images.position, "Images");
                        if (PAI.Segments != null) elementOptions.Add(PAI.Segments.position, "Segments");
                        if (PAI.Number != null) elementOptions.Add(PAI.Number.position, "Number");
                        if (PAI.Number_Target != null) elementOptions.Add(PAI.Number_Target.position, "Number_Target");
                        if (PAI.Pointer != null) elementOptions.Add(PAI.Pointer.position, "Pointer");
                        if (PAI.Circle_Scale != null) elementOptions.Add(PAI.Circle_Scale.position, "Circle_Scale");
                        if (PAI.Linear_Scale != null) elementOptions.Add(PAI.Linear_Scale.position, "Linear_Scale");
                        if (PAI.Icon != null) elementOptions.Add(PAI.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementDistance
                    //case "ElementDistance":
                    //    ElementDistance Distance = (ElementDistance)element;
                    //    break;
                    #endregion

                    #region ElementStand
                    case "ElementStand":
                        ElementStand Stand = (ElementStand)element;
                        if (Stand.Images != null) elementOptions.Add(Stand.Images.position, "Images");
                        if (Stand.Segments != null) elementOptions.Add(Stand.Segments.position, "Segments");
                        if (Stand.Number != null) elementOptions.Add(Stand.Number.position, "Number");
                        if (Stand.Number_Target != null) elementOptions.Add(Stand.Number_Target.position, "Number_Target");
                        if (Stand.Pointer != null) elementOptions.Add(Stand.Pointer.position, "Pointer");
                        if (Stand.Circle_Scale != null) elementOptions.Add(Stand.Circle_Scale.position, "Circle_Scale");
                        if (Stand.Linear_Scale != null) elementOptions.Add(Stand.Linear_Scale.position, "Linear_Scale");
                        if (Stand.Icon != null) elementOptions.Add(Stand.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementActivity
                    case "ElementActivity":
                        ElementActivity Activity = (ElementActivity)element;
                        if (Activity.Images != null) elementOptions.Add(Activity.Images.position, "Images");
                        if (Activity.Segments != null) elementOptions.Add(Activity.Segments.position, "Segments");
                        if (Activity.Number != null) elementOptions.Add(Activity.Number.position, "Number");
                        if (Activity.Number_Target != null) elementOptions.Add(Activity.Number_Target.position, "Number_Target");
                        if (Activity.Pointer != null) elementOptions.Add(Activity.Pointer.position, "Pointer");
                        if (Activity.Circle_Scale != null) elementOptions.Add(Activity.Circle_Scale.position, "Circle_Scale");
                        if (Activity.Linear_Scale != null) elementOptions.Add(Activity.Linear_Scale.position, "Linear_Scale");
                        if (Activity.Icon != null) elementOptions.Add(Activity.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementSpO2
                    //case "ElementSpO2":
                    //    ElementSpO2 SpO2 = (ElementSpO2)element;
                    //    break;
                    #endregion

                    #region ElementStress
                    case "ElementStress":
                        ElementStress Stress = (ElementStress)element;
                        if (Stress.Images != null) elementOptions.Add(Stress.Images.position, "Images");
                        if (Stress.Segments != null) elementOptions.Add(Stress.Segments.position, "Segments");
                        if (Stress.Number != null) elementOptions.Add(Stress.Number.position, "Number");
                        if (Stress.Pointer != null) elementOptions.Add(Stress.Pointer.position, "Pointer");
                        if (Stress.Icon != null) elementOptions.Add(Stress.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementFatBurning
                    case "ElementFatBurning":
                        ElementFatBurning FatBurning = (ElementFatBurning)element;
                        if (FatBurning.Images != null) elementOptions.Add(FatBurning.Images.position, "Images");
                        if (FatBurning.Segments != null) elementOptions.Add(FatBurning.Segments.position, "Segments");
                        if (FatBurning.Number != null) elementOptions.Add(FatBurning.Number.position, "Number");
                        if (FatBurning.Number_Target != null) elementOptions.Add(FatBurning.Number_Target.position, "Number_Target");
                        if (FatBurning.Pointer != null) elementOptions.Add(FatBurning.Pointer.position, "Pointer");
                        if (FatBurning.Circle_Scale != null) elementOptions.Add(FatBurning.Circle_Scale.position, "Circle_Scale");
                        if (FatBurning.Linear_Scale != null) elementOptions.Add(FatBurning.Linear_Scale.position, "Linear_Scale");
                        if (FatBurning.Icon != null) elementOptions.Add(FatBurning.Icon.position, "Icon");
                        break;
                    #endregion



                    #region ElementWeather
                    case "ElementWeather":
                        ElementWeather Weather = (ElementWeather)element;
                        if (Weather.Images != null) elementOptions.Add(Weather.Images.position, "Images");
                        if (Weather.Number != null) elementOptions.Add(Weather.Number.position, "Number");
                        if (Weather.Number_Min != null) elementOptions.Add(Weather.Number_Min.position, "Number_Min");
                        if (Weather.Number_Max != null) elementOptions.Add(Weather.Number_Max.position, "Number_Max");
                        if (Weather.City_Name != null) elementOptions.Add(Weather.City_Name.position, "CityName");
                        if (Weather.Icon != null) elementOptions.Add(Weather.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementUVIndex
                    case "ElementUVIndex":
                        ElementUVIndex UVIndex = (ElementUVIndex)element;
                        if (UVIndex.Images != null) elementOptions.Add(UVIndex.Images.position, "Images");
                        if (UVIndex.Segments != null) elementOptions.Add(UVIndex.Segments.position, "Segments");
                        if (UVIndex.Number != null) elementOptions.Add(UVIndex.Number.position, "Number");
                        if (UVIndex.Pointer != null) elementOptions.Add(UVIndex.Pointer.position, "Pointer");
                        if (UVIndex.Icon != null) elementOptions.Add(UVIndex.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementHumidity
                    case "ElementHumidity":
                        ElementHumidity Humidity = (ElementHumidity)element;
                        if (Humidity.Images != null) elementOptions.Add(Humidity.Images.position, "Images");
                        if (Humidity.Segments != null) elementOptions.Add(Humidity.Segments.position, "Segments");
                        if (Humidity.Number != null) elementOptions.Add(Humidity.Number.position, "Number");
                        if (Humidity.Pointer != null) elementOptions.Add(Humidity.Pointer.position, "Pointer");
                        if (Humidity.Icon != null) elementOptions.Add(Humidity.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementAltimeter
                    case "ElementAltimeter":
                        ElementAltimeter Altimeter = (ElementAltimeter)element;
                        if (Altimeter.Number != null) elementOptions.Add(Altimeter.Number.position, "Number");
                        if (Altimeter.Pointer != null) elementOptions.Add(Altimeter.Pointer.position, "Pointer");
                        if (Altimeter.Icon != null) elementOptions.Add(Altimeter.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementSunrise
                    case "ElementSunrise":
                        ElementSunrise Sunrise = (ElementSunrise)element;
                        if (Sunrise.Images != null) elementOptions.Add(Sunrise.Images.position, "Images");
                        if (Sunrise.Segments != null) elementOptions.Add(Sunrise.Segments.position, "Segments");
                        if (Sunrise.Sunrise != null) elementOptions.Add(Sunrise.Sunrise.position, "Sunrise");
                        if (Sunrise.Sunset != null) elementOptions.Add(Sunrise.Sunset.position, "Sunset");
                        if (Sunrise.Sunset_Sunrise != null) elementOptions.Add(Sunrise.Sunset_Sunrise.position, "Sunset_Sunrise");
                        if (Sunrise.Pointer != null) elementOptions.Add(Sunrise.Pointer.position, "Pointer");
                        if (Sunrise.Icon != null) elementOptions.Add(Sunrise.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementWind
                    case "ElementWind":
                        ElementWind Wind = (ElementWind)element;
                        uCtrl_Wind_Elm.SetVisibilityElementStatus(Wind.visible);
                        if (Wind.Images != null) elementOptions.Add(Wind.Images.position, "Images");
                        if (Wind.Segments != null) elementOptions.Add(Wind.Segments.position, "Segments");
                        if (Wind.Number != null) elementOptions.Add(Wind.Number.position, "Number");
                        if (Wind.Pointer != null) elementOptions.Add(Wind.Pointer.position, "Pointer");
                        if (Wind.Direction != null) elementOptions.Add(Wind.Direction.position, "Wind_Direction");
                        if (Wind.Icon != null) elementOptions.Add(Wind.Icon.position, "Icon");
                        break;
                    #endregion

                    #region ElementMoon
                    //case "ElementMoon":
                    //    ElementMoon Moon = (ElementMoon)element;
                    //    break;
                        #endregion
                }
            }
            return elementOptions;
        }
    }
}
