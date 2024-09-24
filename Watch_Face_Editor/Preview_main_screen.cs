using ImageMagick;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using Watch_Face_Editor.Classes;
using static System.Windows.Forms.AxHost;
using LineCap = System.Drawing.Drawing2D.LineCap;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        /// <summary>формируем изображение на панедли Graphics</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="scale">Масштаб прорисовки</param>
        /// <param name="crop">Обрезать по форме экрана</param>
        /// <param name="WMesh">Рисовать белую сетку</param>
        /// <param name="BMesh">Рисовать черную сетку</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showShortcuts">Подсвечивать область ярлыков</param>
        /// <param name="showShortcutsArea">Подсвечивать область ярлыков рамкой</param>
        /// <param name="showShortcutsBorder">Подсвечивать область ярлыков заливкой</param>
        /// <param name="showShortcutsImage">Подсвечивать изображение, отображаемое при нажатии ярлыка</param>
        /// <param name="showButtons">Подсвечивать область кнопок</param>
        /// <param name="showButtonsArea">Подсвечивать область кнопок рамкой</param>
        /// <param name="showButtonsBorder">Подсвечивать область кнопок заливкой</param>
        /// <param name="showAnimation">Показывать анимацию при предпросмотре</param>
        /// <param name="showProgressArea">Подсвечивать круговую шкалу при наличии фонового изображения</param>
        /// <param name="showCentrHend">Подсвечивать центр стрелки</param>
        /// <param name="showWidgetsArea">Подсвечивать область виджетов</param>
        /// <param name="link">0 - основной экран; 1 - AOD</param>
        /// <param name="Shortcuts_In_Gif">Подсвечивать область с ярлыками (для gif файла)</param>
        /// <param name="Buttons_In_Gif">Подсвечивать область с ярлыками (для gif файла)</param>
        /// <param name="time_value_sec">Время от начала анимации, сек</param>
        /// <param name="showEeditMode">Отображение в режиме редактирования</param>
        /// <param name="edit_mode">Выбор отображаемого режима редактирования. 
        /// 1 - редактируемый задний фон
        /// 2 - редактируемые элементы
        /// 3 - редактируемые стрелки</param>
        public void Preview_screen(Graphics gPanel, float scale, bool crop, bool WMesh, bool BMesh, bool BBorder,
            bool showShortcuts, bool showShortcutsArea, bool showShortcutsBorder, bool showShortcutsImage,
            bool showButtons, bool showButtonsArea, bool showButtonsBorder, 
            bool showAnimation, bool showProgressArea, bool showCentrHend, 
            bool showWidgetsArea, int link, bool Shortcuts_In_Gif, bool Buttons_In_Gif, float time_value_sec, bool showEeditMode, int edit_mode)
        {
            if (showEeditMode && edit_mode > 0) 
            {
                Preview_edit_screen(gPanel, edit_mode, scale, crop, WMesh, BMesh);
                return;
            }
            int offSet_X = 227;
            int offSet_Y = 227;
            showShortcutsImage = true;

            Bitmap src = new Bitmap(1, 1);
            gPanel.ScaleTransform(scale, scale, MatrixOrder.Prepend);
            int i;
            //gPanel.SmoothingMode = SmoothingMode.AntiAlias;
            //if (link == 2) goto AnimationEnd;

            #region Black background
            Logger.WriteLine("Preview_screen (Black background)");
            /*src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3.png");
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3 Pro":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                    break;
                case "GTS 3":
                case "GTS 4":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_3.png");
                    break;
                case "GTR 4":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                    break;
                case "Amazfit Band 7":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_band_7.png");
                    break;
                case "GTS 4 mini":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                    break;
                case "Falcon":
                case "GTR mini":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_falcon.png");
                    break;
            }*/
            src = OpenFileStream(Application.StartupPath + @"\Mask\" + SelectedModel.maskImage);
            offSet_X = src.Width / 2;
            offSet_Y = src.Height / 2;
            gPanel.DrawImage(src, 0, 0);
            //gPanel.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height));
            //src.Dispose();
            #endregion

            #region Background
            Background background = null;
            if (link == 0)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    background = Watch_Face.ScreenNormal.Background;
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    background = Watch_Face.ScreenAOD.Background;

                if (Watch_Face != null && Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null &&
                    Watch_Face.ScreenNormal.Background.Editable_Background != null &&
                    Watch_Face.ScreenNormal.Background.Editable_Background.enable_edit_bg &&
                    /*Watch_Face.ScreenNormal.Background.Editable_Background.fg != null && Watch_Face.ScreenNormal.Background.Editable_Background.fg.Length > 0 &&
                     Watch_Face.ScreenNormal.Background.Editable_Background.tips_bg != null && Watch_Face.ScreenNormal.Background.Editable_Background.tips_bg.Length > 0 &&*/
                    (Watch_Face.ScreenNormal.Background.Editable_Background.AOD_show || background == null))
                {
                    Background backgroundAOD = Watch_Face.ScreenNormal.Background;
                    if (backgroundAOD.Editable_Background.BackgroundList != null &&
                                backgroundAOD.Editable_Background.BackgroundList.Count > 0 && backgroundAOD.visible)
                    {
                        int index = backgroundAOD.Editable_Background.selected_background;
                        if (index >= 0 && index < backgroundAOD.Editable_Background.BackgroundList.Count &&
                            backgroundAOD.Editable_Background.BackgroundList[index].path != null &&
                            backgroundAOD.Editable_Background.BackgroundList[index].path.Length > 0)
                        {
                            src = OpenFileStream(backgroundAOD.Editable_Background.BackgroundList[index].path);
                            gPanel.DrawImage(src, 0, 0);
                        }
                    }
                }
            }
            if (background != null)
            {
                if (background.Editable_Background != null && background.Editable_Background.enable_edit_bg &&
                    background.Editable_Background.BackgroundList != null &&
                    background.Editable_Background.BackgroundList.Count > 0 && background.visible /*&&
                    Watch_Face.ScreenNormal.Background.Editable_Background.fg != null && Watch_Face.ScreenNormal.Background.Editable_Background.fg.Length > 0 &&
                    Watch_Face.ScreenNormal.Background.Editable_Background.tips_bg != null && Watch_Face.ScreenNormal.Background.Editable_Background.tips_bg.Length > 0*/)
                {
                    int index = background.Editable_Background.selected_background;
                    if (index >= 0 && index < background.Editable_Background.BackgroundList.Count &&
                        background.Editable_Background.BackgroundList[index].path != null &&
                        background.Editable_Background.BackgroundList[index].path.Length > 0)
                    {
                        src = OpenFileStream(background.Editable_Background.BackgroundList[index].path);
                        if (src != null) gPanel.DrawImage(src, 0, 0);
                    }
                }
                if (background.BackgroundImage != null && background.BackgroundImage.src != null && 
                    background.BackgroundImage.src.Length > 0 && background.visible)
                {
                    src = OpenFileStream(background.BackgroundImage.src);
                    int x = background.BackgroundImage.x;
                    int y = background.BackgroundImage.y;
                    //int w = background.BackgroundImage.w;
                    //int h = background.BackgroundImage.h;
                    if (src != null) gPanel.DrawImage(src, x, y);
                }
                if (background.BackgroundColor != null && background.visible)
                {
                    Color color = StringToColor(background.BackgroundColor.color);
                    //int x = background.BackgroundColor.x;
                    //int y = background.BackgroundColor.y;
                    //int w = background.BackgroundColor.w;
                    //int h = background.BackgroundColor.h;
                    gPanel.Clear(color);
                }
            }
            #endregion

            List<Object> Elements = null;
            #region EditableElements
            Elements = null;
            if (Watch_Face != null && Watch_Face.Editable_Elements != null && Watch_Face.Editable_Elements.display_first &&
                Watch_Face.Editable_Elements.visible && Watch_Face.Editable_Elements.Watchface_edit_group != null &&
                Watch_Face.Editable_Elements.Watchface_edit_group.Count > 0 && (link == 0 || Watch_Face.Editable_Elements.AOD_show))
            {
                foreach (WATCHFACE_EDIT_GROUP edit_group in Watch_Face.Editable_Elements.Watchface_edit_group)
                {
                    int selected_element = edit_group.selected_element;
                    if (selected_element >= 0 && edit_group.Elements != null && selected_element < edit_group.Elements.Count)
                    {
                        string type = edit_group.Elements[selected_element].GetType().Name;
                        //bool showDate = true;
                        if (type == "ElementDateDay" || type == "ElementDateMonth" || type == "ElementDateYear")
                        {
                            foreach (Object element in edit_group.Elements)
                            {
                                type = element.GetType().Name;
                                if (type == "ElementDateDay" || type == "ElementDateMonth" || type == "ElementDateYear")
                                {
                                    Draw_elements(element, gPanel, scale, crop, WMesh, BMesh, BBorder, showShortcuts,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage, showAnimation, showProgressArea, showCentrHend,
                                        showWidgetsArea, link, Shortcuts_In_Gif, time_value_sec, showEeditMode, edit_mode);
                                }
                            }
                        }
                        else
                        {
                            Draw_elements(edit_group.Elements[selected_element], gPanel, scale, crop, WMesh, BMesh, BBorder, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, showAnimation, showProgressArea, showCentrHend,
                                showWidgetsArea, link, Shortcuts_In_Gif, time_value_sec, showEeditMode, edit_mode);
                        }

                    }
                }
            }
            #endregion

            #region Elements
            Elements = null;
            if (link == 0)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Elements != null)
                    Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Elements != null)
                    Elements = Watch_Face.ScreenAOD.Elements;
            }
            if (Elements != null && Elements.Count > 0)
            {
                foreach (Object element in Elements)
                {
                    Draw_elements(element, gPanel, scale, crop, WMesh, BMesh, BBorder, showShortcuts, showShortcutsArea, showShortcutsBorder,
                        showShortcutsImage, showAnimation, showProgressArea, showCentrHend, showWidgetsArea, link, Shortcuts_In_Gif, time_value_sec,
                        showEeditMode, edit_mode);
                }
            }
            #endregion
            //src.Dispose();

            #region EditableElements
            Elements = null;
            if (Watch_Face != null && Watch_Face.Editable_Elements != null && !Watch_Face.Editable_Elements.display_first && 
                Watch_Face.Editable_Elements.visible && Watch_Face.Editable_Elements.Watchface_edit_group != null && 
                Watch_Face.Editable_Elements.Watchface_edit_group.Count > 0 && (link == 0 || Watch_Face.Editable_Elements.AOD_show))
            {
                foreach(WATCHFACE_EDIT_GROUP edit_group in Watch_Face.Editable_Elements.Watchface_edit_group)
                {
                    int selected_element = edit_group.selected_element;
                    if (selected_element >= 0 && edit_group.Elements != null && selected_element < edit_group.Elements.Count)
                    {
                        string type = edit_group.Elements[selected_element].GetType().Name;
                        //bool showDate = true;
                        if(type == "ElementDateDay" || type == "ElementDateMonth" || type == "ElementDateYear")
                        {
                            foreach(Object element in edit_group.Elements)
                            {
                                type = element.GetType().Name;
                                if (type == "ElementDateDay" || type == "ElementDateMonth" || type == "ElementDateYear")
                                {
                                    Draw_elements(element, gPanel, scale, crop, WMesh, BMesh, BBorder, showShortcuts,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage, showAnimation, showProgressArea, showCentrHend,
                                        showWidgetsArea, link, Shortcuts_In_Gif, time_value_sec, showEeditMode, edit_mode);
                                }
                            }
                        }
                        else
                        {
                            Draw_elements(edit_group.Elements[selected_element], gPanel, scale, crop, WMesh, BMesh, BBorder, showShortcuts, 
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, showAnimation, showProgressArea, showCentrHend,
                                showWidgetsArea, link, Shortcuts_In_Gif, time_value_sec, showEeditMode, edit_mode);
                        }
         
                    }
                }
            }
            #endregion

            #region EditablePointers
            if (Watch_Face != null && Watch_Face.ElementEditablePointers != null &&
                Watch_Face.ElementEditablePointers.visible)
            {
                ElementEditablePointers EditablePointers = Watch_Face.ElementEditablePointers;

                if (EditablePointers.config != null && EditablePointers.config.Count > 0 &&
                    EditablePointers.selected_pointers >= 0 &&
                    EditablePointers.selected_pointers < EditablePointers.config.Count)
                {
                    PointersList pointers_list = EditablePointers.config[EditablePointers.selected_pointers];

                    if (pointers_list.hour != null && pointers_list.hour.path != null
                            && pointers_list.hour.path.Length > 0)
                    {
                        int x = pointers_list.hour.centerX;
                        int y = pointers_list.hour.centerY;
                        int offsetX = pointers_list.hour.posX;
                        int offsetY = pointers_list.hour.posY;
                        int image_index = ListImages.IndexOf(pointers_list.hour.path);
                        int hour = WatchFacePreviewSet.Time.Hours;
                        int min = WatchFacePreviewSet.Time.Minutes;
                        //int sec = Watch_Face_Preview_Set.TimeW.Seconds;
                        if (hour >= 12) hour = hour - 12;
                        float angle = 360 * hour / 12 + 360 * min / (60 * 12);
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);
                    }

                    if (pointers_list.minute != null && pointers_list.minute.path != null
                        && pointers_list.minute.path.Length > 0)
                    {
                        int x = pointers_list.minute.centerX;
                        int y = pointers_list.minute.centerY;
                        int offsetX = pointers_list.minute.posX;
                        int offsetY = pointers_list.minute.posY;
                        int image_index = ListImages.IndexOf(pointers_list.minute.path);
                        int min = WatchFacePreviewSet.Time.Minutes;
                        float angle = 360 * min / 60;
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);
                    }

                    if (pointers_list.second != null && pointers_list.second.path != null
                        && pointers_list.second.path.Length > 0 &&
                        (radioButton_ScreenNormal.Checked || EditablePointers.AOD_show))
                    {
                        int x = pointers_list.second.centerX;
                        int y = pointers_list.second.centerY;
                        int offsetX = pointers_list.second.posX;
                        int offsetY = pointers_list.second.posY;
                        int image_index = ListImages.IndexOf(pointers_list.second.path);
                        int sec = WatchFacePreviewSet.Time.Seconds;
                        float angle = 360 * sec / 60;
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);
                    }

                    if (EditablePointers.cover != null && EditablePointers.cover.src != null && 
                        EditablePointers.cover.src.Length > 0)
                    {
                        int image_Index = ListImages.IndexOf(EditablePointers.cover.src);
                        int pos_x = EditablePointers.cover.x;
                        int pos_y = EditablePointers.cover.y;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        if (src != null) gPanel.DrawImage(src, pos_x, pos_y);
                    }
                }


            }
            #endregion

            #region TopImage
            if (Watch_Face != null && Watch_Face.TopImage != null && Watch_Face.TopImage.visible && Watch_Face.TopImage.Icon != null)
            {
                if (link == 0 || Watch_Face.TopImage.showInAOD)
                {
                    hmUI_widget_IMG icon = Watch_Face.TopImage.Icon;
                    if (icon != null && icon.src != null && icon.src.Length > 0)
                    {
                        int imageIndex = ListImages.IndexOf(icon.src);
                        int x = icon.x;
                        int y = icon.y;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                            //gPanel.DrawImage(src, x, y);
                        }
                    } 
                }
            }
            #endregion

            #region ElementShortcuts
            if (radioButton_ScreenNormal.Checked && Watch_Face != null && Watch_Face.Shortcuts != null && Watch_Face.Shortcuts.visible)
            {
                ElementShortcuts shortcutsElement = Watch_Face.Shortcuts;

                hmUI_widget_IMG_CLICK img_click_step = shortcutsElement.Step;
                hmUI_widget_IMG_CLICK img_click_cal = shortcutsElement.Cal;
                hmUI_widget_IMG_CLICK img_click_heart = shortcutsElement.Heart;
                hmUI_widget_IMG_CLICK img_click_pai = shortcutsElement.PAI;
                hmUI_widget_IMG_CLICK img_click_battery = shortcutsElement.Battery;
                hmUI_widget_IMG_CLICK img_click_sunrise = shortcutsElement.Sunrise;
                hmUI_widget_IMG_CLICK img_click_moon = shortcutsElement.Moon;
                hmUI_widget_IMG_CLICK img_click_bodyTemp = shortcutsElement.BodyTemp;
                hmUI_widget_IMG_CLICK img_click_weather = shortcutsElement.Weather;
                hmUI_widget_IMG_CLICK img_click_stand = shortcutsElement.Stand;
                hmUI_widget_IMG_CLICK img_click_spo2 = shortcutsElement.SPO2;
                hmUI_widget_IMG_CLICK img_click_altimeter = shortcutsElement.Altimeter;
                hmUI_widget_IMG_CLICK img_click_stress = shortcutsElement.Stress;
                hmUI_widget_IMG_CLICK img_click_countdown = shortcutsElement.Countdown;
                hmUI_widget_IMG_CLICK img_click_stopwatch = shortcutsElement.Stopwatch;
                hmUI_widget_IMG_CLICK img_click_alarm = shortcutsElement.Alarm;
                hmUI_widget_IMG_CLICK img_click_sleep = shortcutsElement.Sleep;
                hmUI_widget_IMG_CLICK img_click_altitude = shortcutsElement.Altitude;
                hmUI_widget_IMG_CLICK img_click_readiness = shortcutsElement.Readiness;
                hmUI_widget_IMG_CLICK img_click_outdoorRunning = shortcutsElement.OutdoorRunning;
                hmUI_widget_IMG_CLICK img_click_walking = shortcutsElement.Walking;
                hmUI_widget_IMG_CLICK img_click_outdoorCycling = shortcutsElement.OutdoorCycling;
                hmUI_widget_IMG_CLICK img_click_freeTraining = shortcutsElement.FreeTraining;
                hmUI_widget_IMG_CLICK img_click_poolSwimming = shortcutsElement.PoolSwimming;
                hmUI_widget_IMG_CLICK img_click_openWaterSwimming = shortcutsElement.OpenWaterSwimming;
                hmUI_widget_IMG_CLICK img_click_trainingLoad = shortcutsElement.TrainingLoad;
                hmUI_widget_IMG_CLICK img_click_vo2max = shortcutsElement.VO2max;
                hmUI_widget_IMG_CLICK img_click_recoveryTime = shortcutsElement.RecoveryTime;
                hmUI_widget_IMG_CLICK img_click_breathTrain = shortcutsElement.BreathTrain;
                hmUI_widget_IMG_CLICK img_click_fatBurning = shortcutsElement.FatBurning;

                for (int index = -1; index <= 35; index++)
                {
                    if (img_click_step != null && index == img_click_step.position)
                    {
                        DrawShortcuts(gPanel, img_click_step, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_cal != null && index == img_click_cal.position)
                    {
                        DrawShortcuts(gPanel, img_click_cal, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_heart != null && index == img_click_heart.position)
                    {
                        DrawShortcuts(gPanel, img_click_heart, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_pai != null && index == img_click_pai.position)
                    {
                        DrawShortcuts(gPanel, img_click_pai, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_battery != null && index == img_click_battery.position)
                    {
                        DrawShortcuts(gPanel, img_click_battery, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_sunrise != null && index == img_click_sunrise.position)
                    {
                        DrawShortcuts(gPanel, img_click_sunrise, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_moon != null && index == img_click_moon.position)
                    {
                        DrawShortcuts(gPanel, img_click_moon, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_bodyTemp != null && index == img_click_bodyTemp.position)
                    {
                        DrawShortcuts(gPanel, img_click_bodyTemp, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_weather != null && index == img_click_weather.position)
                    {
                        DrawShortcuts(gPanel, img_click_weather, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_stand != null && index == img_click_stand.position)
                    {
                        DrawShortcuts(gPanel, img_click_stand, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_spo2 != null && index == img_click_spo2.position)
                    {
                        DrawShortcuts(gPanel, img_click_spo2, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_altimeter != null && index == img_click_altimeter.position)
                    {
                        DrawShortcuts(gPanel, img_click_altimeter, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_stress != null && index == img_click_stress.position)
                    {
                        DrawShortcuts(gPanel, img_click_stress, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_countdown != null && index == img_click_countdown.position)
                    {
                        DrawShortcuts(gPanel, img_click_countdown, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_stopwatch != null && index == img_click_stopwatch.position)
                    {
                        DrawShortcuts(gPanel, img_click_stopwatch, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_alarm != null && index == img_click_alarm.position)
                    {
                        DrawShortcuts(gPanel, img_click_alarm, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_sleep != null && index == img_click_sleep.position)
                    {
                        DrawShortcuts(gPanel, img_click_sleep, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_altitude != null && index == img_click_altitude.position)
                    {
                        DrawShortcuts(gPanel, img_click_altitude, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_readiness != null && index == img_click_readiness.position)
                    {
                        DrawShortcuts(gPanel, img_click_readiness, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_outdoorRunning != null && index == img_click_outdoorRunning.position)
                    {
                        DrawShortcuts(gPanel, img_click_outdoorRunning, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_walking != null && index == img_click_walking.position)
                    {
                        DrawShortcuts(gPanel, img_click_walking, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_outdoorCycling != null && index == img_click_outdoorCycling.position)
                    {
                        DrawShortcuts(gPanel, img_click_outdoorCycling, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_freeTraining != null && index == img_click_freeTraining.position)
                    {
                        DrawShortcuts(gPanel, img_click_freeTraining, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_poolSwimming != null && index == img_click_poolSwimming.position)
                    {
                        DrawShortcuts(gPanel, img_click_poolSwimming, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_openWaterSwimming != null && index == img_click_openWaterSwimming.position)
                    {
                        DrawShortcuts(gPanel, img_click_openWaterSwimming, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_trainingLoad != null && index == img_click_trainingLoad.position)
                    {
                        DrawShortcuts(gPanel, img_click_trainingLoad, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_vo2max != null && index == img_click_vo2max.position)
                    {
                        DrawShortcuts(gPanel, img_click_vo2max, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_recoveryTime != null && index == img_click_recoveryTime.position)
                    {
                        DrawShortcuts(gPanel, img_click_recoveryTime, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_breathTrain != null && index == img_click_breathTrain.position)
                    {
                        DrawShortcuts(gPanel, img_click_breathTrain, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                    if (img_click_fatBurning != null && index == img_click_fatBurning.position)
                    {
                        DrawShortcuts(gPanel, img_click_fatBurning, showShortcuts,
                            showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                    }
                }
            }
            #endregion

            #region ElementButtons
            if (radioButton_ScreenNormal.Checked && Watch_Face != null && Watch_Face.Buttons != null && Watch_Face.Buttons.enable)
            {
                if (Watch_Face.Buttons.Button != null && Watch_Face.Buttons.Button.Count > 0)
                {
                    foreach (Button button in Watch_Face.Buttons.Button)
                    {
                        DrawButton(gPanel, button, false, showButtons, showButtonsArea, showButtonsBorder, Buttons_In_Gif);
                    } 
                }
            }
            #endregion

            #region Mesh
            Logger.WriteLine("PreviewToBitmap (Mesh)");

            if (WMesh)
            {
                Pen pen = new Pen(Color.White, 2);
                int LineDistance = 30;
                if (scale >= 1) LineDistance = 15;
                if (scale >= 2) LineDistance = 10;
                if (scale >= 2) pen.Width = 1;
                //if (panel_Preview.Height > 300) LineDistance = 15;
                //if (panel_Preview.Height > 690) LineDistance = 10;
                for (i = 0; i < 30; i++)
                {
                    gPanel.DrawLine(pen, new Point(offSet_X + i * LineDistance, 0), new Point(offSet_X + i * LineDistance, 480));
                    gPanel.DrawLine(pen, new Point(offSet_X - i * LineDistance, 0), new Point(offSet_X - i * LineDistance, 480));

                    gPanel.DrawLine(pen, new Point(0, offSet_Y + i * LineDistance), new Point(480, offSet_Y + i * LineDistance));
                    gPanel.DrawLine(pen, new Point(0, offSet_Y - i * LineDistance), new Point(480, offSet_Y - i * LineDistance));

                    if (i == 0) pen.Width = pen.Width / 3f;
                }
            }

            if (BMesh)
            {
                Pen pen = new Pen(Color.Black, 2);
                int LineDistance = 30;
                if (scale >= 1) LineDistance = 15;
                if (scale >= 2) LineDistance = 10;
                if (scale >= 2) pen.Width = 1;
                //if (panel_Preview.Height > 300) LineDistance = 15;
                //if (panel_Preview.Height > 690) LineDistance = 10;
                for (i = 0; i < 30; i++)
                {
                    gPanel.DrawLine(pen, new Point(offSet_X + i * LineDistance, 0), new Point(offSet_X + i * LineDistance, 480));
                    gPanel.DrawLine(pen, new Point(offSet_X - i * LineDistance, 0), new Point(offSet_X - i * LineDistance, 480));

                    gPanel.DrawLine(pen, new Point(0, offSet_Y + i * LineDistance), new Point(480, offSet_Y + i * LineDistance));
                    gPanel.DrawLine(pen, new Point(0, offSet_Y - i * LineDistance), new Point(480, offSet_Y - i * LineDistance));

                    if (i == 0) pen.Width = pen.Width / 3f;
                }
            }
            #endregion

            if (crop)
            {
                Logger.WriteLine("PreviewToBitmap (crop)");
                /*Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        break;
                    case "GTR 4":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        break;
                    case "Amazfit Band 7":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_band_7.png");
                        break;
                    case "GTS 4 mini":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        break;
                    case "Falcon":
                    case "GTR mini":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }*/
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\" + SelectedModel.maskImage);
                mask = FormColor(mask);
                gPanel.DrawImage(mask, 0, 0);
                //gPanel.DrawImage(mask, new Rectangle(0, 0, mask.Width, mask.Height));
                mask.Dispose();
            }
            if (src != null) src.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="gPanel"></param>
        /// <param name="scale"></param>
        /// <param name="crop"></param>
        /// <param name="WMesh"></param>
        /// <param name="BMesh"></param>
        /// <param name="BBorder"></param>
        /// <param name="showShortcuts"></param>
        /// <param name="showShortcutsArea"></param>
        /// <param name="showShortcutsBorder"></param>
        /// <param name="showShortcutsImage"></param>
        /// <param name="showAnimation"></param>
        /// <param name="showProgressArea"></param>
        /// <param name="showCentrHend"></param>
        /// <param name="showWidgetsArea"></param>
        /// <param name="link"></param>
        /// <param name="Shortcuts_In_Gif"></param>
        /// <param name="time_value_sec"></param>
        /// <param name="showEeditMode"></param>
        /// <param name="edit_mode"></param>
        public void Draw_elements(Object element, Graphics gPanel, float scale, bool crop, bool WMesh, bool BMesh, bool BBorder,
            bool showShortcuts, bool showShortcutsArea, bool showShortcutsBorder, bool showShortcutsImage,
            bool showAnimation, bool showProgressArea, bool showCentrHend,
            bool showWidgetsArea, int link, bool Shortcuts_In_Gif, float time_value_sec, bool showEeditMode, int edit_mode)
        {
            Bitmap src = new Bitmap(1, 1);
            hmUI_widget_IMG_LEVEL img_level = null;
            hmUI_widget_IMG_PROGRESS img_prorgess = null;
            hmUI_widget_IMG_NUMBER img_number = null;
            hmUI_widget_TEXT font_number = null;
            hmUI_widget_IMG_NUMBER text_rotation = null;
            Text_Circle text_circle = null;
            hmUI_widget_IMG_NUMBER img_number_target = null;
            hmUI_widget_TEXT font_number_target = null;
            hmUI_widget_IMG_NUMBER text_rotation_target = null;
            Text_Circle text_circle_target = null;
            hmUI_widget_IMG_POINTER img_pointer = null;
            Circle_Scale circle_scale = null;
            Linear_Scale linear_scale = null;
            hmUI_widget_IMG icon = null;

            int elementValue = 0;
            int value_lenght = 3;
            int goal = 10000;
            float progress = 0;

            int valueImgIndex = -1;
            int valueSegmentIndex = -1;
            int imgCount = 0;
            int segmentCount = 0;

            string type = element.GetType().Name;
            switch (type)
            {
                /*#region ElementDigitalTime
                case "ElementDigitalTime":
                    ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                    if (!DigitalTime.visible) return;
                    int time_hour_offsetX = -1;
                    int time_hour_offsetY = -1;
                    int time_minute_offsetX = -1;
                    int time_minute_offsetY = -1;
                    int time_spasing = 0;
                    bool am_pm = false;
                    value_lenght = 2;

                    // определяем формат времени m/pm
                    if (DigitalTime.AmPm != null && DigitalTime.AmPm.visible && checkBox_ShowIn12hourFormat.Checked && 
                        DigitalTime.AmPm.am_img != null && DigitalTime.AmPm.am_img.Length > 0 && 
                        DigitalTime.AmPm.pm_img != null && DigitalTime.AmPm.pm_img.Length > 0) am_pm = true;

                    for (int index = 1; index <= 20; index++)
                    {
                        if (DigitalTime.Hour != null && DigitalTime.Hour.img_First != null
                            && DigitalTime.Hour.img_First.Length > 0 &&
                            index == DigitalTime.Hour.position && DigitalTime.Hour.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DigitalTime.Hour.img_First);
                            int x = DigitalTime.Hour.imageX;
                            int y = DigitalTime.Hour.imageY;
                            time_hour_offsetY = y;
                            int spasing = DigitalTime.Hour.space;
                            time_spasing = spasing;
                            int angle = DigitalTime.Hour.angle;
                            int alignment = AlignmentToInt(DigitalTime.Hour.align);
                            bool addZero = DigitalTime.Hour.zero;
                            //addZero = true;
                            int value = WatchFacePreviewSet.Time.Hours;
                            int separator_index = -1;
                            if (DigitalTime.Hour.unit != null && DigitalTime.Hour.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DigitalTime.Hour.unit);

                            if (ProgramSettings.ShowIn12hourFormat && DigitalTime.AmPm != null)
                            {
                                if (am_pm)
                                {
                                    if (value > 11) value -= 12;
                                    if (value == 0) value = 12;
                                }
                            }

                            time_hour_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y, spasing, alignment, value, addZero, 2, separator_index, angle, BBorder, "ElementDigitalTime");
                            time_minute_offsetX = -1;
                            time_minute_offsetY = -1;
                            if (spasing != 0 && separator_index >= 0) time_hour_offsetX -= spasing;

                            if (DigitalTime.Hour.icon != null && DigitalTime.Hour.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DigitalTime.Hour.icon);
                                x = DigitalTime.Hour.iconPosX;
                                y = DigitalTime.Hour.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }

                        if (DigitalTime.Minute != null && DigitalTime.Minute.img_First != null
                            && DigitalTime.Minute.img_First.Length > 0 &&
                            index == DigitalTime.Minute.position && DigitalTime.Minute.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DigitalTime.Minute.img_First);
                            int x = DigitalTime.Minute.imageX;
                            int y = DigitalTime.Minute.imageY;
                            int spasing = DigitalTime.Minute.space;
                            time_spasing = spasing;
                            int angle = DigitalTime.Minute.angle;
                            int alignment = AlignmentToInt(DigitalTime.Minute.align);
                            bool addZero = DigitalTime.Minute.zero;
                            //addZero = true;
                            if (DigitalTime.Minute.follow && time_hour_offsetX > -1 &&
                                DigitalTime.Minute.position > DigitalTime.Hour.position)
                            {
                                x = time_hour_offsetX;
                                alignment = 0;
                                y = time_hour_offsetY;
                                spasing = time_spasing;
                            }
                            time_minute_offsetY = y;
                            int value = WatchFacePreviewSet.Time.Minutes;
                            int separator_index = -1;
                            if (DigitalTime.Minute.unit != null && DigitalTime.Minute.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DigitalTime.Minute.unit);

                            time_minute_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y, spasing, alignment, value, addZero, 2, separator_index, angle, BBorder, "ElementDigitalTime");
                            time_hour_offsetX = -1;
                            time_hour_offsetY = -1;
                            if (spasing != 0 && separator_index >= 0) time_minute_offsetX -= spasing;

                            if (DigitalTime.Minute.icon != null && DigitalTime.Minute.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DigitalTime.Minute.icon);
                                x = DigitalTime.Minute.iconPosX;
                                y = DigitalTime.Minute.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }

                        if (DigitalTime.Second != null && DigitalTime.Second.img_First != null
                            && DigitalTime.Second.img_First.Length > 0 &&
                            index == DigitalTime.Second.position && DigitalTime.Second.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DigitalTime.Second.img_First);
                            int x = DigitalTime.Second.imageX;
                            int y = DigitalTime.Second.imageY;
                            int spasing = DigitalTime.Second.space;
                            time_spasing = spasing;
                            int angle = DigitalTime.Second.angle;
                            int alignment = AlignmentToInt(DigitalTime.Second.align);
                            bool addZero = DigitalTime.Second.zero;
                            //addZero = true;
                            if (DigitalTime.Second.follow && time_minute_offsetX > -1 &&
                                DigitalTime.Second.position > DigitalTime.Minute.position)
                            {
                                x = time_minute_offsetX;
                                alignment = 0;
                                y = time_hour_offsetY;
                                spasing = time_spasing;
                            }
                            time_hour_offsetY = y;
                            int value = WatchFacePreviewSet.Time.Seconds;
                            int separator_index = -1;
                            if (DigitalTime.Second.unit != null && DigitalTime.Second.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DigitalTime.Second.unit);


                            Draw_dagital_text(gPanel, imageIndex, x, y,
                                                spasing, alignment, value, addZero, 2, separator_index, angle, BBorder, "ElementDigitalTime");

                            if (DigitalTime.Second.icon != null && DigitalTime.Second.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DigitalTime.Second.icon);
                                x = DigitalTime.Second.iconPosX;
                                y = DigitalTime.Second.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }

                        if (am_pm && index == DigitalTime.AmPm.position)
                        {
                            if (WatchFacePreviewSet.Time.Hours > 11)
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime.AmPm.pm_img);
                                int x = DigitalTime.AmPm.pm_x;
                                int y = DigitalTime.AmPm.pm_y;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                            else
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime.AmPm.am_img);
                                int x = DigitalTime.AmPm.am_x;
                                int y = DigitalTime.AmPm.am_y;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }


                        if (DigitalTime.Hour_Font != null && index == DigitalTime.Hour_Font.position && DigitalTime.Hour_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DigitalTime.Hour_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            int value = WatchFacePreviewSet.Time.Hours;
                            if (ProgramSettings.ShowIn12hourFormat && DigitalTime.AmPm != null)
                            {
                                if (value > 11) value -= 12;
                                if (value == 0) value = 12;
                            }
                            string valueStr = value.ToString();
                            string unitStr = "hour";
                            if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (number_font.unit_type > 0)
                            {
                                if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                valueStr += unitStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DigitalTime.Minute_Font != null && index == DigitalTime.Minute_Font.position && DigitalTime.Minute_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DigitalTime.Minute_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string valueStr = WatchFacePreviewSet.Time.Minutes.ToString();
                            string unitStr = "min";
                            if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (number_font.unit_type > 0)
                            {
                                if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                valueStr += unitStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DigitalTime.Second_Font != null && index == DigitalTime.Second_Font.position && DigitalTime.Second_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DigitalTime.Second_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string valueStr = WatchFacePreviewSet.Time.Seconds.ToString();
                            string unitStr = "sec";
                            if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (number_font.unit_type > 0)
                            {
                                if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                valueStr += unitStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DigitalTime.Hour_min_Font != null && index == DigitalTime.Hour_min_Font.position && DigitalTime.Hour_min_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DigitalTime.Hour_min_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string unitStr = "Am";
                            int value = WatchFacePreviewSet.Time.Hours;
                            if (ProgramSettings.ShowIn12hourFormat)
                            {
                                if (value > 11)
                                {
                                    value -= 12;
                                    unitStr = "Pm";
                                }
                                if (value == 0) value = 12;
                            }
                            string valueHourStr = value.ToString();
                            if (number_font.padding) valueHourStr = valueHourStr.PadLeft(value_lenght, '0');
                            string valueMinStr = WatchFacePreviewSet.Time.Minutes.ToString();
                            valueMinStr = valueMinStr.PadLeft(value_lenght, '0');

                            string delimeter = ":";
                            if (number_font.unit_string != null && number_font.unit_string.Length > 0) delimeter = number_font.unit_string;

                            string valueStr = "";
                            if (number_font.unit_type == 0) unitStr = unitStr.ToLower();
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr = valueHourStr + delimeter + valueMinStr;
                            if (checkBox_ShowIn12hourFormat.Checked)
                            {
                                if (number_font.unit_end) valueStr = valueStr + " " + unitStr;
                                else valueStr = unitStr + " " + valueStr; 
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DigitalTime.Hour_min_sec_Font != null && index == DigitalTime.Hour_min_sec_Font.position && DigitalTime.Hour_min_sec_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DigitalTime.Hour_min_sec_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string unitStr = "Am";
                            int value = WatchFacePreviewSet.Time.Hours;
                            if (ProgramSettings.ShowIn12hourFormat)
                            {
                                if (value > 11)
                                {
                                    value -= 12;
                                    unitStr = "Pm";
                                }
                                if (value == 0) value = 12;
                            }
                            string valueHourStr = value.ToString();
                            if (number_font.padding) valueHourStr = valueHourStr.PadLeft(value_lenght, '0');
                            string valueMinStr = WatchFacePreviewSet.Time.Minutes.ToString();
                            valueMinStr = valueMinStr.PadLeft(value_lenght, '0');
                            string valueSecStr = WatchFacePreviewSet.Time.Seconds.ToString();
                            valueSecStr = valueSecStr.PadLeft(value_lenght, '0');

                            string delimeter = ":";
                            if (number_font.unit_string != null && number_font.unit_string.Length > 0) delimeter = number_font.unit_string;

                            string valueStr = "";
                            if (number_font.unit_type == 0) unitStr = unitStr.ToLower();
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr = valueHourStr + delimeter + valueMinStr + delimeter + valueSecStr;
                            if (checkBox_ShowIn12hourFormat.Checked)
                            {
                                if (number_font.unit_end) valueStr = valueStr + " " + unitStr;
                                else valueStr = unitStr + " " + valueStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }


                        if (DigitalTime.Hour_rotation != null && DigitalTime.Hour_rotation.img_First != null && DigitalTime.Hour_rotation.img_First.Length > 0 &&
                            index == DigitalTime.Hour_rotation.position && DigitalTime.Hour_rotation.visible)
                        {
                            int pos_x = DigitalTime.Hour_rotation.imageX;
                            int pos_y = DigitalTime.Hour_rotation.imageY;
                            int spacing = DigitalTime.Hour_rotation.space;
                            float angle = DigitalTime.Hour_rotation.angle;
                            bool addZero = DigitalTime.Hour_rotation.zero;
                            int image_index = ListImages.IndexOf(DigitalTime.Hour_rotation.img_First);
                            int unit_index = ListImages.IndexOf(DigitalTime.Hour_rotation.unit);
                            int dot_image_index = ListImages.IndexOf(DigitalTime.Hour_rotation.dot_image);
                            string horizontal_alignment = DigitalTime.Hour_rotation.align;
                            bool unit_in_alignment = DigitalTime.Hour_rotation.unit_in_alignment;

                            int value = WatchFacePreviewSet.Time.Hours;
                            if (ProgramSettings.ShowIn12hourFormat && DigitalTime.AmPm != null)
                            {
                                if (am_pm)
                                {
                                    if (value > 11) value -= 12;
                                    if (value == 0) value = 12;
                                }
                            }
                            string valueStr = value.ToString();
                            if (addZero) valueStr = valueStr.PadLeft(2, '0');

                            Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                valueStr, 2, BBorder, -1, -1, false, "ElementDigitalTime");
                        }

                        if (DigitalTime.Minute_rotation != null && DigitalTime.Minute_rotation.img_First != null && DigitalTime.Minute_rotation.img_First.Length > 0 &&
                            index == DigitalTime.Minute_rotation.position && DigitalTime.Minute_rotation.visible)
                        {
                            int pos_x = DigitalTime.Minute_rotation.imageX;
                            int pos_y = DigitalTime.Minute_rotation.imageY;
                            int spacing = DigitalTime.Minute_rotation.space;
                            float angle = DigitalTime.Minute_rotation.angle;
                            bool addZero = DigitalTime.Minute_rotation.zero;
                            int image_index = ListImages.IndexOf(DigitalTime.Minute_rotation.img_First);
                            int unit_index = ListImages.IndexOf(DigitalTime.Minute_rotation.unit);
                            int dot_image_index = ListImages.IndexOf(DigitalTime.Minute_rotation.dot_image);
                            string horizontal_alignment = DigitalTime.Minute_rotation.align;
                            bool unit_in_alignment = DigitalTime.Minute_rotation.unit_in_alignment;

                            int value = WatchFacePreviewSet.Time.Minutes;
                            string valueStr = value.ToString();
                            if (addZero) valueStr = valueStr.PadLeft(2, '0');

                            Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                valueStr, 2, BBorder, -1, -1, false, "ElementDigitalTime");
                        }

                        if (DigitalTime.Second_rotation != null && DigitalTime.Second_rotation.img_First != null && DigitalTime.Second_rotation.img_First.Length > 0 &&
                            index == DigitalTime.Second_rotation.position && DigitalTime.Second_rotation.visible)
                        {
                            int pos_x = DigitalTime.Second_rotation.imageX;
                            int pos_y = DigitalTime.Second_rotation.imageY;
                            int spacing = DigitalTime.Second_rotation.space;
                            float angle = DigitalTime.Second_rotation.angle;
                            bool addZero = DigitalTime.Second_rotation.zero;
                            int image_index = ListImages.IndexOf(DigitalTime.Second_rotation.img_First);
                            int unit_index = ListImages.IndexOf(DigitalTime.Second_rotation.unit);
                            int dot_image_index = ListImages.IndexOf(DigitalTime.Second_rotation.dot_image);
                            string horizontal_alignment = DigitalTime.Second_rotation.align;
                            bool unit_in_alignment = DigitalTime.Second_rotation.unit_in_alignment;

                            int value = WatchFacePreviewSet.Time.Seconds;
                            string valueStr = value.ToString();
                            if (addZero) valueStr = valueStr.PadLeft(2, '0');

                            Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                valueStr, 2, BBorder, -1, -1, false, "ElementDigitalTime");
                        }

                        if (DigitalTime.Hour_circle != null && DigitalTime.Hour_circle.img_First != null && DigitalTime.Hour_circle.img_First.Length > 0 &&
                            index == DigitalTime.Hour_circle.position && DigitalTime.Hour_circle.visible)
                        {
                            int centr_x = DigitalTime.Hour_circle.circle_center_X;
                            int centr_y = DigitalTime.Hour_circle.circle_center_Y;
                            int radius = DigitalTime.Hour_circle.radius;
                            int spacing = DigitalTime.Hour_circle.char_space_angle;
                            float angle = DigitalTime.Hour_circle.angle;
                            bool addZero = DigitalTime.Hour_circle.zero;
                            int image_index = ListImages.IndexOf(DigitalTime.Hour_circle.img_First);
                            int unit_index = ListImages.IndexOf(DigitalTime.Hour_circle.unit);
                            int dot_image_index = ListImages.IndexOf(DigitalTime.Hour_circle.dot_image);
                            string vertical_alignment = DigitalTime.Hour_circle.vertical_alignment;
                            string horizontal_alignment = DigitalTime.Hour_circle.horizontal_alignment;
                            bool reverse_direction = DigitalTime.Hour_circle.reverse_direction;
                            bool unit_in_alignment = DigitalTime.Hour_circle.unit_in_alignment;

                            int value = WatchFacePreviewSet.Time.Hours;
                            if (ProgramSettings.ShowIn12hourFormat && DigitalTime.AmPm != null)
                            {
                                if (am_pm)
                                {
                                    if (value > 11) value -= 12;
                                    if (value == 0) value = 12;
                                }
                            }
                            string valueStr = value.ToString();
                            if (addZero) valueStr = valueStr.PadLeft(2, '0');

                            Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                image_index, *//*int image_width, int image_height,*//* unit_index, *//*int unit_width,*//* dot_image_index, *//*int dot_image_width,*//*
                                vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                                valueStr, 2, BBorder, showCentrHend, -1, -1, false, "ElementDigitalTime");
                        }

                        if (DigitalTime.Minute_circle != null && DigitalTime.Minute_circle.img_First != null && DigitalTime.Minute_circle.img_First.Length > 0 &&
                            index == DigitalTime.Minute_circle.position && DigitalTime.Minute_circle.visible)
                        {
                            int centr_x = DigitalTime.Minute_circle.circle_center_X;
                            int centr_y = DigitalTime.Minute_circle.circle_center_Y;
                            int radius = DigitalTime.Minute_circle.radius;
                            int spacing = DigitalTime.Minute_circle.char_space_angle;
                            float angle = DigitalTime.Minute_circle.angle;
                            bool addZero = DigitalTime.Minute_circle.zero;
                            int image_index = ListImages.IndexOf(DigitalTime.Minute_circle.img_First);
                            int unit_index = ListImages.IndexOf(DigitalTime.Minute_circle.unit);
                            int dot_image_index = ListImages.IndexOf(DigitalTime.Minute_circle.dot_image);
                            string vertical_alignment = DigitalTime.Minute_circle.vertical_alignment;
                            string horizontal_alignment = DigitalTime.Minute_circle.horizontal_alignment;
                            bool reverse_direction = DigitalTime.Minute_circle.reverse_direction;
                            bool unit_in_alignment = DigitalTime.Minute_circle.unit_in_alignment;

                            int value = WatchFacePreviewSet.Time.Minutes;
                            string valueStr = value.ToString();
                            if (addZero) valueStr = valueStr.PadLeft(2, '0');

                            Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                image_index, *//*int image_width, int image_height,*//* unit_index, *//*int unit_width,*//* dot_image_index, *//*int dot_image_width,*//*
                                vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                                valueStr, 2, BBorder, showCentrHend, -1, -1, false, "ElementDigitalTime");
                        }

                        if (DigitalTime.Second_circle != null && DigitalTime.Second_circle.img_First != null && DigitalTime.Second_circle.img_First.Length > 0 &&
                            index == DigitalTime.Second_circle.position && DigitalTime.Second_circle.visible)
                        {
                            int centr_x = DigitalTime.Second_circle.circle_center_X;
                            int centr_y = DigitalTime.Second_circle.circle_center_Y;
                            int radius = DigitalTime.Second_circle.radius;
                            int spacing = DigitalTime.Second_circle.char_space_angle;
                            float angle = DigitalTime.Second_circle.angle;
                            bool addZero = DigitalTime.Second_circle.zero;
                            int image_index = ListImages.IndexOf(DigitalTime.Second_circle.img_First);
                            int unit_index = ListImages.IndexOf(DigitalTime.Second_circle.unit);
                            int dot_image_index = ListImages.IndexOf(DigitalTime.Second_circle.dot_image);
                            string vertical_alignment = DigitalTime.Second_circle.vertical_alignment;
                            string horizontal_alignment = DigitalTime.Second_circle.horizontal_alignment;
                            bool reverse_direction = DigitalTime.Second_circle.reverse_direction;
                            bool unit_in_alignment = DigitalTime.Second_circle.unit_in_alignment;

                            int value = WatchFacePreviewSet.Time.Seconds;
                            string valueStr = value.ToString();
                            if (addZero) valueStr = valueStr.PadLeft(2, '0');

                            Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                image_index, *//*int image_width, int image_height,*//* unit_index, *//*int unit_width,*//* dot_image_index, *//*int dot_image_width,*//*
                                vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                                valueStr, 2, BBorder, showCentrHend, -1, -1, false, "ElementDigitalTime");
                        }
                    }

                    break;
                #endregion*/

                #region ElementDigitalTime_v2
                case "ElementDigitalTime_v2":
                    ElementDigitalTime_v2 DigitalTime_v2 = (ElementDigitalTime_v2)element;
                    if (!DigitalTime_v2.visible) return;
                    int time_v2_hour_offsetX = -1;
                    int time_v2_hour_offsetY = -1;
                    int time_v2_minute_offsetX = -1;
                    int time_v2_minute_offsetY = -1;
                    int time_v2_spasing = 0;
                    bool am_pm_v2 = false;
                    value_lenght = 2;

                    // определяем формат времени am/pm
                    if (DigitalTime_v2.AmPm != null && DigitalTime_v2.AmPm.visible && checkBox_ShowIn12hourFormat.Checked &&
                        DigitalTime_v2.AmPm.am_img != null && DigitalTime_v2.AmPm.am_img.Length > 0 &&
                        DigitalTime_v2.AmPm.pm_img != null && DigitalTime_v2.AmPm.pm_img.Length > 0) am_pm_v2 = true;

                    for (int index = 1; index <= 10; index++)
                    {
                        if (DigitalTime_v2.Group_Hour != null && index == DigitalTime_v2.Group_Hour.position)
                        {
                            if (DigitalTime_v2.Group_Hour.Text_circle != null && DigitalTime_v2.Group_Hour.Text_circle.img_First != null && 
                                DigitalTime_v2.Group_Hour.Text_circle.img_First.Length > 0 && DigitalTime_v2.Group_Hour.Text_circle.visible)
                            {
                                int centr_x = DigitalTime_v2.Group_Hour.Text_circle.circle_center_X;
                                int centr_y = DigitalTime_v2.Group_Hour.Text_circle.circle_center_Y;
                                int radius = DigitalTime_v2.Group_Hour.Text_circle.radius;
                                int spacing = DigitalTime_v2.Group_Hour.Text_circle.char_space_angle;
                                float angle = DigitalTime_v2.Group_Hour.Text_circle.angle;
                                bool addZero = DigitalTime_v2.Group_Hour.Text_circle.zero;
                                int image_index = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Text_circle.img_First);
                                int unit_index = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Text_circle.unit);
                                int dot_image_index = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Text_circle.dot_image);
                                string vertical_alignment = DigitalTime_v2.Group_Hour.Text_circle.vertical_alignment;
                                string horizontal_alignment = DigitalTime_v2.Group_Hour.Text_circle.horizontal_alignment;
                                bool reverse_direction = DigitalTime_v2.Group_Hour.Text_circle.reverse_direction;
                                bool unit_in_alignment = DigitalTime_v2.Group_Hour.Text_circle.unit_in_alignment;

                                int value = WatchFacePreviewSet.Time.Hours;
                                if (ProgramSettings.ShowIn12hourFormat && DigitalTime_v2.AmPm != null)
                                {
                                    if (am_pm_v2)
                                    {
                                        if (value > 11) value -= 12;
                                        if (value == 0) value = 12;
                                    }
                                }
                                string valueStr = value.ToString();
                                if (addZero) valueStr = valueStr.PadLeft(2, '0');

                                Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                    image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                                    vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                                    valueStr, 2, BBorder, showCentrHend, -1, -1, false, "ElementDigitalTime");
                            }

                            if (DigitalTime_v2.Group_Hour.Text_rotation != null && DigitalTime_v2.Group_Hour.Text_rotation.img_First != null && 
                                DigitalTime_v2.Group_Hour.Text_rotation.img_First.Length > 0 && DigitalTime_v2.Group_Hour.Text_rotation.visible)
                            {
                                int pos_x = DigitalTime_v2.Group_Hour.Text_rotation.imageX;
                                int pos_y = DigitalTime_v2.Group_Hour.Text_rotation.imageY;
                                int spacing = DigitalTime_v2.Group_Hour.Text_rotation.space;
                                float angle = DigitalTime_v2.Group_Hour.Text_rotation.angle;
                                bool addZero = DigitalTime_v2.Group_Hour.Text_rotation.zero;
                                int image_index = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Text_rotation.img_First);
                                int unit_index = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Text_rotation.unit);
                                int dot_image_index = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Text_rotation.dot_image);
                                string horizontal_alignment = DigitalTime_v2.Group_Hour.Text_rotation.align;
                                bool unit_in_alignment = DigitalTime_v2.Group_Hour.Text_rotation.unit_in_alignment;

                                int value = WatchFacePreviewSet.Time.Hours;
                                if (ProgramSettings.ShowIn12hourFormat && DigitalTime_v2.AmPm != null)
                                {
                                    if (am_pm_v2)
                                    {
                                        if (value > 11) value -= 12;
                                        if (value == 0) value = 12;
                                    }
                                }
                                string valueStr = value.ToString();
                                if (addZero) valueStr = valueStr.PadLeft(2, '0');

                                Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                    image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                    valueStr, 2, BBorder, -1, -1, false, "ElementDigitalTime");
                            }

                            if (DigitalTime_v2.Group_Hour.Number_Font != null && DigitalTime_v2.Group_Hour.Number_Font.visible)
                            {
                                hmUI_widget_TEXT number_font = DigitalTime_v2.Group_Hour.Number_Font;
                                int x = number_font.x;
                                int y = number_font.y;
                                int h = number_font.h;
                                int w = number_font.w;

                                int size = number_font.text_size;
                                int space_h = number_font.char_space;
                                int space_v = number_font.line_space;

                                Color color = StringToColor(number_font.color);
                                int alpha = number_font.alpha;
                                //int align_h = AlignmentToInt(number_font.align_h);
                                //int align_v = AlignmentVerticalToInt(number_font.align_v);
                                string align_h = number_font.align_h;
                                string align_v = number_font.align_v;
                                string text_style = number_font.text_style;
                                int value = WatchFacePreviewSet.Time.Hours;
                                if (ProgramSettings.ShowIn12hourFormat && DigitalTime_v2.AmPm != null)
                                {
                                    if (value > 11) value -= 12;
                                    if (value == 0) value = 12;
                                }
                                string valueStr = value.ToString();
                                string unitStr = "hour";
                                if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                                if (number_font.unit_type > 0)
                                {
                                    if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                    valueStr += unitStr;
                                }

                                if (number_font.centreHorizontally)
                                {
                                    x = (SelectedModel.background.w - w) / 2;
                                    align_h = "CENTER_H";
                                }
                                if (number_font.centreVertically)
                                {
                                    y = (SelectedModel.background.h - h) / 2;
                                    align_v = "CENTER_V";
                                }

                                if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                                {
                                    string font_fileName = FontsList[number_font.font];
                                    //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                    if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                    {
                                        Font drawFont = null;
                                        using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                        {
                                            fonts.AddFontFile(font_fileName);
                                            drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                        }

                                        Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                        align_h, align_v, text_style, BBorder);
                                    }
                                    else
                                    {
                                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                    }

                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }
                            }

                            if (DigitalTime_v2.Group_Hour.Number != null && DigitalTime_v2.Group_Hour.Number.img_First != null && 
                                DigitalTime_v2.Group_Hour.Number.img_First.Length > 0 && DigitalTime_v2.Group_Hour.Number.visible)
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Number.img_First);
                                int x = DigitalTime_v2.Group_Hour.Number.imageX;
                                int y = DigitalTime_v2.Group_Hour.Number.imageY;
                                time_v2_hour_offsetY = y;
                                int spasing = DigitalTime_v2.Group_Hour.Number.space;
                                time_v2_spasing = spasing;
                                int angle = DigitalTime_v2.Group_Hour.Number.angle;
                                int alignment = AlignmentToInt(DigitalTime_v2.Group_Hour.Number.align);
                                bool addZero = DigitalTime_v2.Group_Hour.Number.zero;
                                //addZero = true;
                                int value = WatchFacePreviewSet.Time.Hours;
                                int alpha = DigitalTime_v2.Group_Hour.Number.alpha;
                                int separator_index = -1;
                                if (DigitalTime_v2.Group_Hour.Number.unit != null && DigitalTime_v2.Group_Hour.Number.unit.Length > 0)
                                    separator_index = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Number.unit);

                                if (ProgramSettings.ShowIn12hourFormat && DigitalTime_v2.AmPm != null)
                                {
                                    if (am_pm_v2)
                                    {
                                        if (value > 11) value -= 12;
                                        if (value == 0) value = 12;
                                    }
                                }

                                time_v2_hour_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y, spasing, alignment, value, alpha, addZero, 2, separator_index, angle, BBorder, "ElementDigitalTime");
                                time_v2_minute_offsetX = -1;
                                time_v2_minute_offsetY = -1;
                                if (spasing != 0 && separator_index >= 0) time_v2_hour_offsetX -= spasing;

                                if (DigitalTime_v2.Group_Hour.Number.icon != null && DigitalTime_v2.Group_Hour.Number.icon.Length > 0)
                                {
                                    imageIndex = ListImages.IndexOf(DigitalTime_v2.Group_Hour.Number.icon);
                                    x = DigitalTime_v2.Group_Hour.Number.iconPosX;
                                    y = DigitalTime_v2.Group_Hour.Number.iconPosY;
                                    alpha = DigitalTime_v2.Group_Hour.Number.icon_alpha;

                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    if (SelectedModel.versionOS >= 2.1 && alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else if (src != null) gPanel.DrawImage(src, x, y);
                                }
                            }
                        }

                        if (DigitalTime_v2.Group_Minute != null && index == DigitalTime_v2.Group_Minute.position)
                        {
                            if (DigitalTime_v2.Group_Minute.Text_circle != null && DigitalTime_v2.Group_Minute.Text_circle.img_First != null && 
                                DigitalTime_v2.Group_Minute.Text_circle.img_First.Length > 0 && DigitalTime_v2.Group_Minute.Text_circle.visible)
                            {
                                int centr_x = DigitalTime_v2.Group_Minute.Text_circle.circle_center_X;
                                int centr_y = DigitalTime_v2.Group_Minute.Text_circle.circle_center_Y;
                                int radius = DigitalTime_v2.Group_Minute.Text_circle.radius;
                                int spacing = DigitalTime_v2.Group_Minute.Text_circle.char_space_angle;
                                float angle = DigitalTime_v2.Group_Minute.Text_circle.angle;
                                bool addZero = DigitalTime_v2.Group_Minute.Text_circle.zero;
                                int image_index = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Text_circle.img_First);
                                int unit_index = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Text_circle.unit);
                                int dot_image_index = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Text_circle.dot_image);
                                string vertical_alignment = DigitalTime_v2.Group_Minute.Text_circle.vertical_alignment;
                                string horizontal_alignment = DigitalTime_v2.Group_Minute.Text_circle.horizontal_alignment;
                                bool reverse_direction = DigitalTime_v2.Group_Minute.Text_circle.reverse_direction;
                                bool unit_in_alignment = DigitalTime_v2.Group_Minute.Text_circle.unit_in_alignment;

                                int value = WatchFacePreviewSet.Time.Minutes;
                                string valueStr = value.ToString();
                                if (addZero) valueStr = valueStr.PadLeft(2, '0');

                                Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                    image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                                    vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                                    valueStr, 2, BBorder, showCentrHend, -1, -1, false, "ElementDigitalTime");
                            }

                            if (DigitalTime_v2.Group_Minute.Text_rotation != null && DigitalTime_v2.Group_Minute.Text_rotation.img_First != null && 
                                DigitalTime_v2.Group_Minute.Text_rotation.img_First.Length > 0 && DigitalTime_v2.Group_Minute.Text_rotation.visible)
                            {
                                int pos_x = DigitalTime_v2.Group_Minute.Text_rotation.imageX;
                                int pos_y = DigitalTime_v2.Group_Minute.Text_rotation.imageY;
                                int spacing = DigitalTime_v2.Group_Minute.Text_rotation.space;
                                float angle = DigitalTime_v2.Group_Minute.Text_rotation.angle;
                                bool addZero = DigitalTime_v2.Group_Minute.Text_rotation.zero;
                                int image_index = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Text_rotation.img_First);
                                int unit_index = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Text_rotation.unit);
                                int dot_image_index = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Text_rotation.dot_image);
                                string horizontal_alignment = DigitalTime_v2.Group_Minute.Text_rotation.align;
                                bool unit_in_alignment = DigitalTime_v2.Group_Minute.Text_rotation.unit_in_alignment;

                                int value = WatchFacePreviewSet.Time.Minutes;
                                string valueStr = value.ToString();
                                if (addZero) valueStr = valueStr.PadLeft(2, '0');

                                Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                    image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                    valueStr, 2, BBorder, -1, -1, false, "ElementDigitalTime");
                            }

                            if (DigitalTime_v2.Group_Minute.Number_Font != null && DigitalTime_v2.Group_Minute.Number_Font.visible)
                            {
                                hmUI_widget_TEXT number_font = DigitalTime_v2.Group_Minute.Number_Font;
                                int x = number_font.x;
                                int y = number_font.y;
                                int h = number_font.h;
                                int w = number_font.w;

                                int size = number_font.text_size;
                                int space_h = number_font.char_space;
                                int space_v = number_font.line_space;

                                Color color = StringToColor(number_font.color);
                                int alpha = number_font.alpha;
                                //int align_h = AlignmentToInt(number_font.align_h);
                                //int align_v = AlignmentVerticalToInt(number_font.align_v);
                                string align_h = number_font.align_h;
                                string align_v = number_font.align_v;
                                string text_style = number_font.text_style;
                                string valueStr = WatchFacePreviewSet.Time.Minutes.ToString();
                                string unitStr = "min";
                                if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                                if (number_font.unit_type > 0)
                                {
                                    if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                    valueStr += unitStr;
                                }

                                if (number_font.centreHorizontally)
                                {
                                    x = (SelectedModel.background.w - w) / 2;
                                    align_h = "CENTER_H";
                                }
                                if (number_font.centreVertically)
                                {
                                    y = (SelectedModel.background.h - h) / 2;
                                    align_v = "CENTER_V";
                                }

                                if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                                {
                                    string font_fileName = FontsList[number_font.font];
                                    //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                    if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                    {
                                        Font drawFont = null;
                                        using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                        {
                                            fonts.AddFontFile(font_fileName);
                                            drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                        }

                                        Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                        align_h, align_v, text_style, BBorder);
                                    }
                                    else
                                    {
                                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                    }

                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }
                            }

                            if (DigitalTime_v2.Group_Minute.Number != null && DigitalTime_v2.Group_Minute.Number.img_First != null && 
                                DigitalTime_v2.Group_Minute.Number.img_First.Length > 0 && DigitalTime_v2.Group_Minute.Number.visible)
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Number.img_First);
                                int x = DigitalTime_v2.Group_Minute.Number.imageX;
                                int y = DigitalTime_v2.Group_Minute.Number.imageY;
                                int spasing = DigitalTime_v2.Group_Minute.Number.space;
                                //time_v2_spasing = spasing;
                                int angle = DigitalTime_v2.Group_Minute.Number.angle;
                                int alignment = AlignmentToInt(DigitalTime_v2.Group_Minute.Number.align);
                                bool addZero = DigitalTime_v2.Group_Minute.Number.zero;
                                //addZero = true;
                                int alpha = DigitalTime_v2.Group_Minute.Number.alpha;
                                if (DigitalTime_v2.Group_Minute.Number.follow && time_v2_hour_offsetX > -1 &&
                                    DigitalTime_v2.Group_Minute.position > DigitalTime_v2.Group_Hour.position &&
                                    DigitalTime_v2.Group_Second.position > DigitalTime_v2.Group_Minute.position)
                                {
                                    x = time_v2_hour_offsetX;
                                    alignment = 0;
                                    y = time_v2_hour_offsetY;
                                    spasing = time_v2_spasing;
                                }
                                time_v2_minute_offsetY = y;
                                int value = WatchFacePreviewSet.Time.Minutes;
                                int separator_index = -1;
                                if (DigitalTime_v2.Group_Minute.Number.unit != null && DigitalTime_v2.Group_Minute.Number.unit.Length > 0)
                                    separator_index = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Number.unit);

                                time_v2_minute_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y, spasing, alignment, value, alpha, addZero, 2, separator_index, angle, BBorder, "ElementDigitalTime");
                                time_v2_hour_offsetX = -1;
                                time_v2_hour_offsetY = -1;
                                if (spasing != 0 && separator_index >= 0) time_v2_minute_offsetX -= spasing;

                                if (DigitalTime_v2.Group_Minute.Number.icon != null && DigitalTime_v2.Group_Minute.Number.icon.Length > 0)
                                {
                                    imageIndex = ListImages.IndexOf(DigitalTime_v2.Group_Minute.Number.icon);
                                    x = DigitalTime_v2.Group_Minute.Number.iconPosX;
                                    y = DigitalTime_v2.Group_Minute.Number.iconPosY;
                                    alpha = DigitalTime_v2.Group_Minute.Number.icon_alpha;

                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    if (SelectedModel.versionOS >= 2.1 && alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else if (src != null) gPanel.DrawImage(src, x, y);
                                }
                            } 
                        }

                        if (DigitalTime_v2.Group_Second != null && index == DigitalTime_v2.Group_Second.position)
                        {
                            if (DigitalTime_v2.Group_Second.Text_circle != null && DigitalTime_v2.Group_Second.Text_circle.img_First != null && 
                                DigitalTime_v2.Group_Second.Text_circle.img_First.Length > 0 && DigitalTime_v2.Group_Second.Text_circle.visible)
                            {
                                int centr_x = DigitalTime_v2.Group_Second.Text_circle.circle_center_X;
                                int centr_y = DigitalTime_v2.Group_Second.Text_circle.circle_center_Y;
                                int radius = DigitalTime_v2.Group_Second.Text_circle.radius;
                                int spacing = DigitalTime_v2.Group_Second.Text_circle.char_space_angle;
                                float angle = DigitalTime_v2.Group_Second.Text_circle.angle;
                                bool addZero = DigitalTime_v2.Group_Second.Text_circle.zero;
                                int image_index = ListImages.IndexOf(DigitalTime_v2.Group_Second.Text_circle.img_First);
                                int unit_index = ListImages.IndexOf(DigitalTime_v2.Group_Second.Text_circle.unit);
                                int dot_image_index = ListImages.IndexOf(DigitalTime_v2.Group_Second.Text_circle.dot_image);
                                string vertical_alignment = DigitalTime_v2.Group_Second.Text_circle.vertical_alignment;
                                string horizontal_alignment = DigitalTime_v2.Group_Second.Text_circle.horizontal_alignment;
                                bool reverse_direction = DigitalTime_v2.Group_Second.Text_circle.reverse_direction;
                                bool unit_in_alignment = DigitalTime_v2.Group_Second.Text_circle.unit_in_alignment;

                                int value = WatchFacePreviewSet.Time.Seconds;
                                string valueStr = value.ToString();
                                if (addZero) valueStr = valueStr.PadLeft(2, '0');

                                Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                    image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                                    vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                                    valueStr, 2, BBorder, showCentrHend, -1, -1, false, "ElementDigitalTime");
                            }

                            if (DigitalTime_v2.Group_Second.Text_rotation != null && DigitalTime_v2.Group_Second.Text_rotation.img_First != null && 
                                DigitalTime_v2.Group_Second.Text_rotation.img_First.Length > 0 && DigitalTime_v2.Group_Second.Text_rotation.visible)
                            {
                                int pos_x = DigitalTime_v2.Group_Second.Text_rotation.imageX;
                                int pos_y = DigitalTime_v2.Group_Second.Text_rotation.imageY;
                                int spacing = DigitalTime_v2.Group_Second.Text_rotation.space;
                                float angle = DigitalTime_v2.Group_Second.Text_rotation.angle;
                                bool addZero = DigitalTime_v2.Group_Second.Text_rotation.zero;
                                int image_index = ListImages.IndexOf(DigitalTime_v2.Group_Second.Text_rotation.img_First);
                                int unit_index = ListImages.IndexOf(DigitalTime_v2.Group_Second.Text_rotation.unit);
                                int dot_image_index = ListImages.IndexOf(DigitalTime_v2.Group_Second.Text_rotation.dot_image);
                                string horizontal_alignment = DigitalTime_v2.Group_Second.Text_rotation.align;
                                bool unit_in_alignment = DigitalTime_v2.Group_Second.Text_rotation.unit_in_alignment;

                                int value = WatchFacePreviewSet.Time.Seconds;
                                string valueStr = value.ToString();
                                if (addZero) valueStr = valueStr.PadLeft(2, '0');

                                Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                    image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                    valueStr, 2, BBorder, -1, -1, false, "ElementDigitalTime");
                            }

                            if (DigitalTime_v2.Group_Second.Number_Font != null && DigitalTime_v2.Group_Second.Number_Font.visible)
                            {
                                hmUI_widget_TEXT number_font = DigitalTime_v2.Group_Second.Number_Font;
                                int x = number_font.x;
                                int y = number_font.y;
                                int h = number_font.h;
                                int w = number_font.w;

                                int size = number_font.text_size;
                                int space_h = number_font.char_space;
                                int space_v = number_font.line_space;

                                Color color = StringToColor(number_font.color);
                                int alpha = number_font.alpha;
                                //int align_h = AlignmentToInt(number_font.align_h);
                                //int align_v = AlignmentVerticalToInt(number_font.align_v);
                                string align_h = number_font.align_h;
                                string align_v = number_font.align_v;
                                string text_style = number_font.text_style;
                                string valueStr = WatchFacePreviewSet.Time.Seconds.ToString();
                                string unitStr = "sec";
                                if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                                if (number_font.unit_type > 0)
                                {
                                    if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                    valueStr += unitStr;
                                }

                                if (number_font.centreHorizontally)
                                {
                                    x = (SelectedModel.background.w - w) / 2;
                                    align_h = "CENTER_H";
                                }
                                if (number_font.centreVertically)
                                {
                                    y = (SelectedModel.background.h - h) / 2;
                                    align_v = "CENTER_V";
                                }

                                if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                                {
                                    string font_fileName = FontsList[number_font.font];
                                    //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                    if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                    {
                                        Font drawFont = null;
                                        using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                        {
                                            fonts.AddFontFile(font_fileName);
                                            drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                        }

                                        Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                        align_h, align_v, text_style, BBorder);
                                    }
                                    else
                                    {
                                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                    }

                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }
                            }

                            if (DigitalTime_v2.Group_Second.Number != null && DigitalTime_v2.Group_Second.Number.img_First != null && 
                                DigitalTime_v2.Group_Second.Number.img_First.Length > 0 && DigitalTime_v2.Group_Second.Number.visible)
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime_v2.Group_Second.Number.img_First);
                                int x = DigitalTime_v2.Group_Second.Number.imageX;
                                int y = DigitalTime_v2.Group_Second.Number.imageY;
                                int spasing = DigitalTime_v2.Group_Second.Number.space;
                                //time_v2_spasing = spasing;
                                int angle = DigitalTime_v2.Group_Second.Number.angle;
                                int alignment = AlignmentToInt(DigitalTime_v2.Group_Second.Number.align);
                                bool addZero = DigitalTime_v2.Group_Second.Number.zero;
                                //addZero = true;
                                int alpha = DigitalTime_v2.Group_Second.Number.alpha;
                                if (DigitalTime_v2.Group_Second.Number.follow && time_v2_minute_offsetX > -1 &&
                                    DigitalTime_v2.Group_Minute.position > DigitalTime_v2.Group_Hour.position &&
                                    DigitalTime_v2.Group_Second.position > DigitalTime_v2.Group_Minute.position)
                                {
                                    x = time_v2_minute_offsetX;
                                    alignment = 0;
                                    y = time_v2_minute_offsetY;
                                    spasing = time_v2_spasing;
                                }
                                //time_v2_hour_offsetY = y;
                                int value = WatchFacePreviewSet.Time.Seconds;
                                int separator_index = -1;
                                if (DigitalTime_v2.Group_Second.Number.unit != null && DigitalTime_v2.Group_Second.Number.unit.Length > 0)
                                    separator_index = ListImages.IndexOf(DigitalTime_v2.Group_Second.Number.unit);


                                Draw_dagital_text(gPanel, imageIndex, x, y,
                                                    spasing, alignment, value, alpha, addZero, 2, separator_index, angle, BBorder, "ElementDigitalTime");

                                if (DigitalTime_v2.Group_Second.Number.icon != null && DigitalTime_v2.Group_Second.Number.icon.Length > 0)
                                {
                                    imageIndex = ListImages.IndexOf(DigitalTime_v2.Group_Second.Number.icon);
                                    x = DigitalTime_v2.Group_Second.Number.iconPosX;
                                    y = DigitalTime_v2.Group_Second.Number.iconPosY;
                                    alpha = DigitalTime_v2.Group_Second.Number.icon_alpha;

                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    if (SelectedModel.versionOS >= 2.1 && alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else if (src != null) gPanel.DrawImage(src, x, y);
                                }
                            } 
                        }


                        if (am_pm_v2 && index == DigitalTime_v2.AmPm.position)
                        {
                            if (WatchFacePreviewSet.Time.Hours > 11)
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime_v2.AmPm.pm_img);
                                int x = DigitalTime_v2.AmPm.pm_x;
                                int y = DigitalTime_v2.AmPm.pm_y;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                            else
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime_v2.AmPm.am_img);
                                int x = DigitalTime_v2.AmPm.am_x;
                                int y = DigitalTime_v2.AmPm.am_y;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }


                        if (DigitalTime_v2.Hour_Min_Font != null && index == DigitalTime_v2.Hour_Min_Font.position && DigitalTime_v2.Hour_Min_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DigitalTime_v2.Hour_Min_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            int alpha = number_font.alpha;
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string unitStr = "Am";
                            int value = WatchFacePreviewSet.Time.Hours;
                            if (ProgramSettings.ShowIn12hourFormat)
                            {
                                if (value > 11)
                                {
                                    value -= 12;
                                    unitStr = "Pm";
                                }
                                if (value == 0) value = 12;
                            }
                            string valueHourStr = value.ToString();
                            if (number_font.padding) valueHourStr = valueHourStr.PadLeft(value_lenght, '0');
                            string valueMinStr = WatchFacePreviewSet.Time.Minutes.ToString();
                            valueMinStr = valueMinStr.PadLeft(value_lenght, '0');

                            string delimeter = ":";
                            if (number_font.unit_string != null && number_font.unit_string.Length > 0) delimeter = number_font.unit_string;

                            string valueStr = "";
                            if (number_font.unit_type == 0) unitStr = unitStr.ToLower();
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr = valueHourStr + delimeter + valueMinStr;
                            if (checkBox_ShowIn12hourFormat.Checked)
                            {
                                if (number_font.unit_end) valueStr = valueStr + " " + unitStr;
                                else valueStr = unitStr + " " + valueStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DigitalTime_v2.Hour_Min_Sec_Font != null && index == DigitalTime_v2.Hour_Min_Sec_Font.position && DigitalTime_v2.Hour_Min_Sec_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DigitalTime_v2.Hour_Min_Sec_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            int alpha = number_font.alpha;
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string unitStr = "Am";
                            int value = WatchFacePreviewSet.Time.Hours;
                            if (ProgramSettings.ShowIn12hourFormat)
                            {
                                if (value > 11)
                                {
                                    value -= 12;
                                    unitStr = "Pm";
                                }
                                if (value == 0) value = 12;
                            }
                            string valueHourStr = value.ToString();
                            if (number_font.padding) valueHourStr = valueHourStr.PadLeft(value_lenght, '0');
                            string valueMinStr = WatchFacePreviewSet.Time.Minutes.ToString();
                            valueMinStr = valueMinStr.PadLeft(value_lenght, '0');
                            string valueSecStr = WatchFacePreviewSet.Time.Seconds.ToString();
                            valueSecStr = valueSecStr.PadLeft(value_lenght, '0');

                            string delimeter = ":";
                            if (number_font.unit_string != null && number_font.unit_string.Length > 0) delimeter = number_font.unit_string;

                            string valueStr = "";
                            if (number_font.unit_type == 0) unitStr = unitStr.ToLower();
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr = valueHourStr + delimeter + valueMinStr + delimeter + valueSecStr;
                            if (checkBox_ShowIn12hourFormat.Checked)
                            {
                                if (number_font.unit_end) valueStr = valueStr + " " + unitStr;
                                else valueStr = unitStr + " " + valueStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha,valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                    }

                    break;
                #endregion

                #region ElementAnalogTime
                case "ElementAnalogTime":
                    ElementAnalogTime AnalogTime = (ElementAnalogTime)element;
                    if (!AnalogTime.visible) return;

                    for (int index = 1; index <= 3; index++)
                    {
                        if (AnalogTime.Hour != null && AnalogTime.Hour.src != null
                            && AnalogTime.Hour.src.Length > 0 &&
                            index == AnalogTime.Hour.position && AnalogTime.Hour.visible)
                        {
                            int x = AnalogTime.Hour.center_x;
                            int y = AnalogTime.Hour.center_y;
                            int offsetX = AnalogTime.Hour.pos_x;
                            int offsetY = AnalogTime.Hour.pos_y;
                            int image_index = ListImages.IndexOf(AnalogTime.Hour.src);
                            int hour = WatchFacePreviewSet.Time.Hours;
                            int min = WatchFacePreviewSet.Time.Minutes;
                            //int sec = Watch_Face_Preview_Set.TimeW.Seconds;
                            if (hour >= 12) hour = hour - 12;
                            float angle = 360 * hour / 12 + 360 * min / (60 * 12);
                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (AnalogTime.Hour.cover_path != null && AnalogTime.Hour.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(AnalogTime.Hour.cover_path);
                                x = AnalogTime.Hour.cover_x;
                                y = AnalogTime.Hour.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (AnalogTime.Minute != null && AnalogTime.Minute.src != null
                            && AnalogTime.Minute.src.Length > 0 &&
                            index == AnalogTime.Minute.position && AnalogTime.Minute.visible)
                        {
                            int x = AnalogTime.Minute.center_x;
                            int y = AnalogTime.Minute.center_y;
                            int offsetX = AnalogTime.Minute.pos_x;
                            int offsetY = AnalogTime.Minute.pos_y;
                            int image_index = ListImages.IndexOf(AnalogTime.Minute.src);
                            int min = WatchFacePreviewSet.Time.Minutes;
                            float angle = 360 * min / 60;
                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (AnalogTime.Minute.cover_path != null && AnalogTime.Minute.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(AnalogTime.Minute.cover_path);
                                x = AnalogTime.Minute.cover_x;
                                y = AnalogTime.Minute.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (AnalogTime.Second != null && AnalogTime.Second.src != null
                            && AnalogTime.Second.src.Length > 0 &&
                            index == AnalogTime.Second.position && AnalogTime.Second.visible)
                        {
                            int x = AnalogTime.Second.center_x;
                            int y = AnalogTime.Second.center_y;
                            int offsetX = AnalogTime.Second.pos_x;
                            int offsetY = AnalogTime.Second.pos_y;
                            int image_index = ListImages.IndexOf(AnalogTime.Second.src);
                            int sec = WatchFacePreviewSet.Time.Seconds;
                            float angle = 360 * sec / 60;
                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (AnalogTime.Second.cover_path != null && AnalogTime.Second.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(AnalogTime.Second.cover_path);
                                x = AnalogTime.Second.cover_x;
                                y = AnalogTime.Second.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }
                    }

                    break;
                #endregion

                #region ElementAnalogTimePro
                case "ElementAnalogTimePro":
                    ElementAnalogTimePro AnalogTimePro = (ElementAnalogTimePro)element;
                    if (!AnalogTimePro.visible) return;

                    for (int index = 1; index <= 5; index++)
                    {
                        if (AnalogTimePro.Hour != null && AnalogTimePro.Hour.src != null
                            && AnalogTimePro.Hour.src.Length > 0 &&
                            index == AnalogTimePro.Hour.position && AnalogTimePro.Hour.visible)
                        {
                            int x = AnalogTimePro.Hour.center_x;
                            int y = AnalogTimePro.Hour.center_y;
                            int offsetX = AnalogTimePro.Hour.pos_x;
                            int offsetY = AnalogTimePro.Hour.pos_y;
                            int image_index = ListImages.IndexOf(AnalogTimePro.Hour.src);
                            int hour = WatchFacePreviewSet.Time.Hours;
                            int min = WatchFacePreviewSet.Time.Minutes;
                            //int sec = Watch_Face_Preview_Set.TimeW.Seconds;
                            int startAngl = AnalogTimePro.Hour.start_angle;
                            int endAngl = AnalogTimePro.Hour.end_angle;
                            int fullAngl = endAngl - startAngl;
                            int fullHour = 24;
                            if (!AnalogTimePro.Format_24hour)
                            {
                                if (hour >= 12) hour = hour - 12;
                                fullHour = 12;
                            }
                            float angle = startAngl + fullAngl * hour / fullHour + fullAngl * min / (60 * fullHour);
                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (AnalogTimePro.Hour.cover_path != null && AnalogTimePro.Hour.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(AnalogTimePro.Hour.cover_path);
                                x = AnalogTimePro.Hour.cover_x;
                                y = AnalogTimePro.Hour.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (AnalogTimePro.Minute != null && AnalogTimePro.Minute.src != null
                            && AnalogTimePro.Minute.src.Length > 0 &&
                            index == AnalogTimePro.Minute.position && AnalogTimePro.Minute.visible)
                        {
                            int x = AnalogTimePro.Minute.center_x;
                            int y = AnalogTimePro.Minute.center_y;
                            int offsetX = AnalogTimePro.Minute.pos_x;
                            int offsetY = AnalogTimePro.Minute.pos_y;
                            int startAngl = AnalogTimePro.Minute.start_angle;
                            int endAngl = AnalogTimePro.Minute.end_angle;
                            int fullAngl = endAngl - startAngl;
                            int image_index = ListImages.IndexOf(AnalogTimePro.Minute.src);
                            int min = WatchFacePreviewSet.Time.Minutes;
                            float angle = startAngl + fullAngl * min / 60;
                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (AnalogTimePro.Minute.cover_path != null && AnalogTimePro.Minute.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(AnalogTimePro.Minute.cover_path);
                                x = AnalogTimePro.Minute.cover_x;
                                y = AnalogTimePro.Minute.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (AnalogTimePro.Second != null && AnalogTimePro.Second.src != null
                            && AnalogTimePro.Second.src.Length > 0 &&
                            index == AnalogTimePro.Second.position && AnalogTimePro.Second.visible)
                        {
                            int x = AnalogTimePro.Second.center_x;
                            int y = AnalogTimePro.Second.center_y;
                            int offsetX = AnalogTimePro.Second.pos_x;
                            int offsetY = AnalogTimePro.Second.pos_y;
                            int startAngl = AnalogTimePro.Second.start_angle;
                            int endAngl = AnalogTimePro.Second.end_angle;
                            int fullAngl = endAngl - startAngl;
                            int image_index = ListImages.IndexOf(AnalogTimePro.Second.src);
                            int sec = WatchFacePreviewSet.Time.Seconds;
                            float angle = startAngl + fullAngl * sec / 60;
                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (AnalogTimePro.Second.cover_path != null && AnalogTimePro.Second.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(AnalogTimePro.Second.cover_path);
                                x = AnalogTimePro.Second.cover_x;
                                y = AnalogTimePro.Second.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }
                    }

                    break;
                #endregion

                #region ElementEditablePointers
                /*case "ElementEditablePointers":
                    ElementEditablePointers EditablePointers = (ElementEditablePointers)element;
                    if (!EditablePointers.enable) continue;
                    if (EditablePointers.config == null || EditablePointers.config.Count == 0 ||
                        EditablePointers.selected_pointers < 0 || 
                        EditablePointers.selected_pointers >= EditablePointers.config.Count) continue;

                    PointersList pointers_list = EditablePointers.config[EditablePointers.selected_pointers];

                    if (pointers_list.hour != null && pointers_list.hour.src != null
                            && pointers_list.hour.src.Length > 0)
                    {
                        int x = pointers_list.hour.center_x;
                        int y = pointers_list.hour.center_y;
                        int offsetX = pointers_list.hour.pos_x;
                        int offsetY = pointers_list.hour.pos_y;
                        int image_index = ListImages.IndexOf(pointers_list.hour.src);
                        int hour = WatchFacePreviewSet.Time.Hours;
                        int min = WatchFacePreviewSet.Time.Minutes;
                        //int sec = Watch_Face_Preview_Set.TimeW.Seconds;
                        if (hour >= 12) hour = hour - 12;
                        float angle = 360 * hour / 12 + 360 * min / (60 * 12);
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                        if (pointers_list.hour.cover_path != null && pointers_list.hour.cover_path.Length > 0)
                        {
                            image_index = ListImages.IndexOf(pointers_list.hour.cover_path);
                            x = pointers_list.hour.cover_x;
                            y = pointers_list.hour.cover_y;

                            src = OpenFileStream(ListImagesFullName[image_index]);
                            gPanel.DrawImage(src, x, y);
                        }
                    }

                    if (pointers_list.minute != null && pointers_list.minute.src != null
                        && pointers_list.minute.src.Length > 0)
                    {
                        int x = pointers_list.minute.center_x;
                        int y = pointers_list.minute.center_y;
                        int offsetX = pointers_list.minute.pos_x;
                        int offsetY = pointers_list.minute.pos_y;
                        int image_index = ListImages.IndexOf(pointers_list.minute.src);
                        int min = WatchFacePreviewSet.Time.Minutes;
                        float angle = 360 * min / 60;
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                        if (pointers_list.minute.cover_path != null && pointers_list.minute.cover_path.Length > 0)
                        {
                            image_index = ListImages.IndexOf(pointers_list.minute.cover_path);
                            x = pointers_list.minute.cover_x;
                            y = pointers_list.minute.cover_y;

                            src = OpenFileStream(ListImagesFullName[image_index]);
                            gPanel.DrawImage(src, x, y);
                        }
                    }

                    if (pointers_list.second != null && pointers_list.second.src != null
                        && pointers_list.second.src.Length > 0 &&
                        (radioButton_ScreenNormal.Checked || EditablePointers.AOD_show))
                    {
                        int x = pointers_list.second.center_x;
                        int y = pointers_list.second.center_y;
                        int offsetX = pointers_list.second.pos_x;
                        int offsetY = pointers_list.second.pos_y;
                        int image_index = ListImages.IndexOf(pointers_list.second.src);
                        int sec = WatchFacePreviewSet.Time.Seconds;
                        float angle = 360 * sec / 60;
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                        if (pointers_list.second.cover_path != null && pointers_list.second.cover_path.Length > 0)
                        {
                            image_index = ListImages.IndexOf(pointers_list.second.cover_path);
                            x = pointers_list.second.cover_x;
                            y = pointers_list.second.cover_y;

                            src = OpenFileStream(ListImagesFullName[image_index]);
                            gPanel.DrawImage(src, x, y);
                        }
                    }

                    break;*/
                #endregion

                #region ElementDateDay
                case "ElementDateDay":
                    ElementDateDay DateDay = (ElementDateDay)element;
                    if (!DateDay.visible) return;

                    for (int index = 1; index <= 15; index++)
                    {
                        if (DateDay.Number != null && DateDay.Number.img_First != null
                            && DateDay.Number.img_First.Length > 0 &&
                            index == DateDay.Number.position && DateDay.Number.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DateDay.Number.img_First);
                            int x = DateDay.Number.imageX;
                            int y = DateDay.Number.imageY;
                            int spasing = DateDay.Number.space;
                            time_v2_spasing = spasing;
                            int alignment = AlignmentToInt(DateDay.Number.align);
                            bool addZero = DateDay.Number.zero;
                            //addZero = true;
                            int alpha = DateDay.Number.alpha;
                            int value = WatchFacePreviewSet.Date.Day;
                            int separator_index = -1;
                            if (DateDay.Number.unit != null && DateDay.Number.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DateDay.Number.unit);

                            Draw_dagital_text(gPanel, imageIndex, x, y,
                                spasing, alignment, value, alpha, addZero, 2, separator_index, 0, BBorder, "ElementDateDay");

                            if (DateDay.Number.icon != null && DateDay.Number.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DateDay.Number.icon);
                                x = DateDay.Number.iconPosX;
                                y = DateDay.Number.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (SelectedModel.versionOS >= 2.1 && DateDay.Number.alpha != 255)
                                {
                                    int w = src.Width;
                                    int h = src.Height;
                                    // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                    ColorMatrix colorMatrix = new ColorMatrix();
                                    colorMatrix.Matrix33 = DateDay.Number.alpha / 255f; // значение от 0 до 1

                                    // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                    ImageAttributes imgAttributes = new ImageAttributes();
                                    imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                    // Указываем прямоугольник, куда будет помещено изображение
                                    Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                    gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                }
                                else if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (DateDay.Number_Font != null && index == DateDay.Number_Font.position && DateDay.Number_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DateDay.Number_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            int alpha = number_font.alpha;
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string valueStr = WatchFacePreviewSet.Date.Day.ToString();
                            string unitStr = "day";
                            if (number_font.padding) valueStr = valueStr.PadLeft(2, '0');
                            if (number_font.unit_type > 0)
                            {
                                if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                valueStr += unitStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DateDay.Day_Month_Font != null && index == DateDay.Day_Month_Font.position && DateDay.Day_Month_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DateDay.Day_Month_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            int alpha = number_font.alpha;
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string valueDayStr = WatchFacePreviewSet.Date.Day.ToString();
                            string valueMonthStr = WatchFacePreviewSet.Date.Month.ToString();
                            string delimeter = "/";
                            if (number_font.unit_string != null && number_font.unit_string.Length > 0) delimeter = number_font.unit_string;

                            if (number_font.padding)
                            {
                                valueDayStr = valueDayStr.PadLeft(2, '0');
                                valueMonthStr = valueMonthStr.PadLeft(2, '0');
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            string valueStr = valueDayStr + delimeter + valueMonthStr;

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DateDay.Day_Month_Year_Font != null && index == DateDay.Day_Month_Year_Font.position && DateDay.Day_Month_Year_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DateDay.Day_Month_Year_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;
                            bool yearFull = false;
                            if (number_font.unit_type == 1) yearFull = true;

                            Color color = StringToColor(number_font.color);
                            int alpha = number_font.alpha;
                            //int align_h = AlignmentToInt(number_font.align_h);
                            //int align_v = AlignmentVerticalToInt(number_font.align_v);
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string valueDayStr = WatchFacePreviewSet.Date.Day.ToString();
                            string valueMonthStr = WatchFacePreviewSet.Date.Month.ToString();
                            string valueYearStr = (WatchFacePreviewSet.Date.Year % 100).ToString();
                            string delimeter = "/";
                            if (number_font.unit_string != null && number_font.unit_string.Length > 0) delimeter = number_font.unit_string;

                            if (number_font.padding) 
                            {
                                valueDayStr = valueDayStr.PadLeft(2, '0');
                                valueMonthStr = valueMonthStr.PadLeft(2, '0');
                                if (number_font.unit_type == 2) yearFull = true;
                            }
                            if (yearFull) valueYearStr = WatchFacePreviewSet.Date.Year.ToString();

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            string valueStr = valueDayStr + delimeter + valueMonthStr + delimeter + valueYearStr;

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DateDay.Text_rotation != null && DateDay.Text_rotation.img_First != null && DateDay.Text_rotation.img_First.Length > 0 &&
                            index == DateDay.Text_rotation.position && DateDay.Text_rotation.visible)
                        {
                            value_lenght = 2;
                            int pos_x = DateDay.Text_rotation.imageX;
                            int pos_y = DateDay.Text_rotation.imageY;
                            int spacing = DateDay.Text_rotation.space;
                            float angle = DateDay.Text_rotation.angle;
                            bool addZero = DateDay.Text_rotation.zero;
                            int image_index = ListImages.IndexOf(DateDay.Text_rotation.img_First);
                            int unit_index = ListImages.IndexOf(DateDay.Text_rotation.unit);
                            int dot_image_index = ListImages.IndexOf(DateDay.Text_rotation.dot_image);
                            string horizontal_alignment = DateDay.Text_rotation.align;
                            bool unit_in_alignment = DateDay.Text_rotation.unit_in_alignment;

                            string valueStr = (WatchFacePreviewSet.Date.Day).ToString();
                            if (addZero) valueStr = valueStr.PadLeft(value_lenght, '0');

                            Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                valueStr, value_lenght, BBorder, -1, -1, false, "ElementDateDay");
                        }

                        if (DateDay.Text_circle != null && DateDay.Text_circle.img_First != null && DateDay.Text_circle.img_First.Length > 0 &&
                            index == DateDay.Text_circle.position && DateDay.Text_circle.visible)
                        {
                            value_lenght = 2;
                            int centr_x = DateDay.Text_circle.circle_center_X;
                            int centr_y = DateDay.Text_circle.circle_center_Y;
                            int radius = DateDay.Text_circle.radius;
                            int spacing = DateDay.Text_circle.char_space_angle;
                            float angle = DateDay.Text_circle.angle;
                            bool addZero = DateDay.Text_circle.zero;
                            int image_index = ListImages.IndexOf(DateDay.Text_circle.img_First);
                            int unit_index = ListImages.IndexOf(DateDay.Text_circle.unit);
                            int dot_image_index = ListImages.IndexOf(DateDay.Text_circle.dot_image);
                            string vertical_alignment = DateDay.Text_circle.vertical_alignment;
                            string horizontal_alignment = DateDay.Text_circle.horizontal_alignment;
                            bool reverse_direction = DateDay.Text_circle.reverse_direction;
                            bool unit_in_alignment = DateDay.Text_circle.unit_in_alignment;

                            string valueStr = (WatchFacePreviewSet.Date.Day).ToString();
                            if (addZero) valueStr = valueStr.PadLeft(value_lenght, '0');

                            Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                image_index,  unit_index, dot_image_index, vertical_alignment, horizontal_alignment, 
                                reverse_direction, unit_in_alignment, valueStr, value_lenght, BBorder, showCentrHend, -1, -1, false, "ElementDateDay");
                        }

                        if (DateDay.Pointer != null && DateDay.Pointer.src != null
                            && DateDay.Pointer.src.Length > 0 &&
                            index == DateDay.Pointer.position && DateDay.Pointer.visible)
                        {
                            int x = DateDay.Pointer.center_x;
                            int y = DateDay.Pointer.center_y;
                            int offsetX = DateDay.Pointer.pos_x;
                            int offsetY = DateDay.Pointer.pos_y;
                            int startAngle = DateDay.Pointer.start_angle;
                            int endAngle = DateDay.Pointer.end_angle;
                            int image_index = ListImages.IndexOf(DateDay.Pointer.src);
                            int Day = WatchFacePreviewSet.Date.Day;
                            //Day--;
                            int angle = (int)(startAngle + Day * (endAngle - startAngle) / 31f);

                            if (DateDay.Pointer.scale != null && DateDay.Pointer.scale.Length > 0)
                            {
                                int image_index_scale = ListImages.IndexOf(DateDay.Pointer.scale);
                                int x_scale = DateDay.Pointer.scale_x;
                                int y_scale = DateDay.Pointer.scale_y;

                                src = OpenFileStream(ListImagesFullName[image_index_scale]);
                                if (src != null) gPanel.DrawImage(src, x_scale, y_scale);
                            }

                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (DateDay.Pointer.cover_path != null && DateDay.Pointer.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(DateDay.Pointer.cover_path);
                                x = DateDay.Pointer.cover_x;
                                y = DateDay.Pointer.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }
                    }

                    break;
                #endregion

                #region ElementDateMonth
                case "ElementDateMonth":
                    ElementDateMonth DateMonth = (ElementDateMonth)element;
                    if (!DateMonth.visible) return;

                    for (int index = 1; index <= 15; index++)
                    {
                        if (DateMonth.Number != null && DateMonth.Number.img_First != null
                            && DateMonth.Number.img_First.Length > 0 &&
                            index == DateMonth.Number.position && DateMonth.Number.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DateMonth.Number.img_First);
                            int x = DateMonth.Number.imageX;
                            int y = DateMonth.Number.imageY;
                            int spasing = DateMonth.Number.space;
                            int alignment = AlignmentToInt(DateMonth.Number.align);
                            bool addZero = DateMonth.Number.zero;
                            //addZero = true;
                            int alpha = DateMonth.Number.alpha;
                            int value = WatchFacePreviewSet.Date.Month;
                            int separator_index = -1;
                            if (DateMonth.Number.unit != null && DateMonth.Number.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DateMonth.Number.unit);

                            Draw_dagital_text(gPanel, imageIndex, x, y,
                                spasing, alignment, value, alpha, addZero, 2, separator_index, 0, BBorder, "ElementDateMonth");

                            if (DateMonth.Number.icon != null && DateMonth.Number.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DateMonth.Number.icon);
                                x = DateMonth.Number.iconPosX;
                                y = DateMonth.Number.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                                {
                                    int w = src.Width;
                                    int h = src.Height;
                                    // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                    ColorMatrix colorMatrix = new ColorMatrix();
                                    colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                    // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                    ImageAttributes imgAttributes = new ImageAttributes();
                                    imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                    // Указываем прямоугольник, куда будет помещено изображение
                                    Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                    gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                }
                                else if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (DateMonth.Number_Font != null && index == DateMonth.Number_Font.position && DateMonth.Number_Font.visible)
                        {
                            hmUI_widget_TEXT number_font = DateMonth.Number_Font;
                            int x = number_font.x;
                            int y = number_font.y;
                            int h = number_font.h;
                            int w = number_font.w;

                            int size = number_font.text_size;
                            int space_h = number_font.char_space;
                            int space_v = number_font.line_space;

                            Color color = StringToColor(number_font.color);
                            int alpha = number_font.alpha;
                            string align_h = number_font.align_h;
                            string align_v = number_font.align_v;
                            string text_style = number_font.text_style;
                            string valueStr = WatchFacePreviewSet.Date.Month.ToString();
                            string unitStr = "month";
                            if (number_font.padding) valueStr = valueStr.PadLeft(2, '0');
                            if (number_font.unit_type > 0)
                            {
                                if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                                valueStr += unitStr;
                            }

                            if (number_font.centreHorizontally)
                            {
                                x = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (number_font.centreVertically)
                            {
                                y = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                            {
                                string font_fileName = FontsList[number_font.font];
                                //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }
                        }

                        if (DateMonth.Month_Font != null && index == DateMonth.Month_Font.position && DateMonth.Month_Font.visible)
                        {
                            hmUI_widget_TEXT month_font = DateMonth.Month_Font;
                            string[] dowArrey = month_font.unit_string.Split(',');

                            if (dowArrey.Length == 12)
                            {
                                int strIndex = WatchFacePreviewSet.Date.Month - 1;
                                string valueStr = dowArrey[strIndex].Trim();

                                int x = month_font.x;
                                int y = month_font.y;
                                int h = month_font.h;
                                int w = month_font.w;

                                int size = month_font.text_size;
                                int space_h = month_font.char_space;
                                int space_v = month_font.line_space;

                                Color color = StringToColor(month_font.color);
                                int alpha = month_font.alpha;
                                string align_h = month_font.align_h;
                                string align_v = month_font.align_v;
                                string text_style = month_font.text_style;

                                if (month_font.centreHorizontally)
                                {
                                    x = (SelectedModel.background.w - w) / 2;
                                    align_h = "CENTER_H";
                                }
                                if (month_font.centreVertically)
                                {
                                    y = (SelectedModel.background.h - h) / 2;
                                    align_v = "CENTER_V";
                                }

                                if (month_font.font != null && month_font.font.Length > 3 && FontsList.ContainsKey(month_font.font))
                                {
                                    string font_fileName = FontsList[month_font.font];
                                    //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                    if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                    {
                                        Font drawFont = null;
                                        using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                        {
                                            fonts.AddFontFile(font_fileName);
                                            drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                        }

                                        Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                        align_h, align_v, text_style, BBorder);
                                    }
                                    else
                                    {
                                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                    }

                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }
                            }
                        }

                        if (DateMonth.Text_rotation != null && DateMonth.Text_rotation.img_First != null && DateMonth.Text_rotation.img_First.Length > 0 &&
                            index == DateMonth.Text_rotation.position && DateMonth.Text_rotation.visible)
                        {
                            value_lenght = 2;
                            int pos_x = DateMonth.Text_rotation.imageX;
                            int pos_y = DateMonth.Text_rotation.imageY;
                            int spacing = DateMonth.Text_rotation.space;
                            float angle = DateMonth.Text_rotation.angle;
                            bool addZero = DateMonth.Text_rotation.zero;
                            int image_index = ListImages.IndexOf(DateMonth.Text_rotation.img_First);
                            int unit_index = ListImages.IndexOf(DateMonth.Text_rotation.unit);
                            int dot_image_index = ListImages.IndexOf(DateMonth.Text_rotation.dot_image);
                            string horizontal_alignment = DateMonth.Text_rotation.align;
                            bool unit_in_alignment = DateMonth.Text_rotation.unit_in_alignment;

                            string valueStr = (WatchFacePreviewSet.Date.Month).ToString();
                            if (addZero) valueStr = valueStr.PadLeft(value_lenght, '0');

                            Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                                image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                                valueStr, value_lenght, BBorder, -1, -1, false, "ElementDateMonth");
                        }

                        if (DateMonth.Text_circle != null && DateMonth.Text_circle.img_First != null && DateMonth.Text_circle.img_First.Length > 0 &&
                            index == DateMonth.Text_circle.position && DateMonth.Text_circle.visible)
                        {
                            value_lenght = 2;
                            int centr_x = DateMonth.Text_circle.circle_center_X;
                            int centr_y = DateMonth.Text_circle.circle_center_Y;
                            int radius = DateMonth.Text_circle.radius;
                            int spacing = DateMonth.Text_circle.char_space_angle;
                            float angle = DateMonth.Text_circle.angle;
                            bool addZero = DateMonth.Text_circle.zero;
                            int image_index = ListImages.IndexOf(DateMonth.Text_circle.img_First);
                            int unit_index = ListImages.IndexOf(DateMonth.Text_circle.unit);
                            int dot_image_index = ListImages.IndexOf(DateMonth.Text_circle.dot_image);
                            string vertical_alignment = DateMonth.Text_circle.vertical_alignment;
                            string horizontal_alignment = DateMonth.Text_circle.horizontal_alignment;
                            bool reverse_direction = DateMonth.Text_circle.reverse_direction;
                            bool unit_in_alignment = DateMonth.Text_circle.unit_in_alignment;

                            string valueStr = (WatchFacePreviewSet.Date.Month).ToString();
                            if (addZero) valueStr = valueStr.PadLeft(value_lenght, '0');

                            Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                                image_index, unit_index, dot_image_index, vertical_alignment, horizontal_alignment,
                                reverse_direction, unit_in_alignment, valueStr, value_lenght, BBorder, showCentrHend, -1, -1, false, "ElementDateMonth");
                        }

                        if (DateMonth.Pointer != null && DateMonth.Pointer.src != null
                            && DateMonth.Pointer.src.Length > 0 &&
                            index == DateMonth.Pointer.position && DateMonth.Pointer.visible)
                        {
                            int x = DateMonth.Pointer.center_x;
                            int y = DateMonth.Pointer.center_y;
                            int offsetX = DateMonth.Pointer.pos_x;
                            int offsetY = DateMonth.Pointer.pos_y;
                            int startAngle = DateMonth.Pointer.start_angle;
                            int endAngle = DateMonth.Pointer.end_angle;
                            int image_index = ListImages.IndexOf(DateMonth.Pointer.src);
                            int Month = WatchFacePreviewSet.Date.Month;
                            //Month--;
                            int angle = (int)(startAngle + Month * (endAngle - startAngle) / 12f);

                            if (DateMonth.Pointer.scale != null && DateMonth.Pointer.scale.Length > 0)
                            {
                                int image_index_scale = ListImages.IndexOf(DateMonth.Pointer.scale);
                                int x_scale = DateMonth.Pointer.scale_x;
                                int y_scale = DateMonth.Pointer.scale_y;

                                src = OpenFileStream(ListImagesFullName[image_index_scale]);
                                if (src != null) gPanel.DrawImage(src, x_scale, y_scale);
                            }

                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (DateMonth.Pointer.cover_path != null && DateMonth.Pointer.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(DateMonth.Pointer.cover_path);
                                x = DateMonth.Pointer.cover_x;
                                y = DateMonth.Pointer.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (DateMonth.Images != null && DateMonth.Images.img_First != null
                            && DateMonth.Images.img_First.Length > 0 &&
                            index == DateMonth.Images.position && DateMonth.Images.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DateMonth.Images.img_First);
                            int x = DateMonth.Images.X;
                            int y = DateMonth.Images.Y;
                            imageIndex = imageIndex + WatchFacePreviewSet.Date.Month - 1;

                            if (imageIndex < ListImagesFullName.Count)
                            {
                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }

                    }

                    break;
                #endregion

                #region ElementDateYear
                case "ElementDateYear":
                    ElementDateYear DateYear = (ElementDateYear)element;
                    if (!DateYear.visible) return;

                    icon = DateYear.Icon;

                    for (int index = 1; index <= 15; index++)
                    {
                        if (DateYear.Number != null && DateYear.Number.img_First != null
                            && DateYear.Number.img_First.Length > 0 &&
                            index == DateYear.Number.position && DateYear.Number.visible)
                    {
                        int imageIndex = ListImages.IndexOf(DateYear.Number.img_First);
                        int x = DateYear.Number.imageX;
                        int y = DateYear.Number.imageY;
                        int spasing = DateYear.Number.space;
                        //int alignment = AlignmentToInt(DateYear.Number.align);
                        int alignment = 0;
                        bool addZero = DateYear.Number.zero;
                        int alpha = DateYear.Number.alpha;
                        int value = WatchFacePreviewSet.Date.Year;
                        if (!addZero) value = value % 100;
                        int separator_index = -1;
                        if (DateYear.Number.unit != null && DateYear.Number.unit.Length > 0)
                            separator_index = ListImages.IndexOf(DateYear.Number.unit);

                        Draw_dagital_text(gPanel, imageIndex, x, y,
                            spasing, alignment, value, alpha, addZero, 4, separator_index, 0, BBorder, "ElementDateYear");

                        if (DateYear.Number.icon != null && DateYear.Number.icon.Length > 0)
                        {
                            imageIndex = ListImages.IndexOf(DateYear.Number.icon);
                            x = DateYear.Number.iconPosX;
                            y = DateYear.Number.iconPosY;

                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                                {
                                    int w = src.Width;
                                    int h = src.Height;
                                    // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                    ColorMatrix colorMatrix = new ColorMatrix();
                                    colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                    // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                    ImageAttributes imgAttributes = new ImageAttributes();
                                    imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                    // Указываем прямоугольник, куда будет помещено изображение
                                    Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                    gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                }
                                else if (src != null) gPanel.DrawImage(src, x, y);
                        }
                     }

                    if (DateYear.Number_Font != null && index == DateYear.Number_Font.position && DateYear.Number_Font.visible)
                    {
                        hmUI_widget_TEXT number_font = DateYear.Number_Font;
                        int x = number_font.x;
                        int y = number_font.y;
                        int h = number_font.h;
                        int w = number_font.w;

                        int size = number_font.text_size;
                        int space_h = number_font.char_space;
                        int space_v = number_font.line_space;

                        Color color = StringToColor(number_font.color);
                        int alpha = number_font.alpha;
                        string align_h = number_font.align_h;
                        string align_v = number_font.align_v;
                        string text_style = number_font.text_style; 
                        int value = WatchFacePreviewSet.Date.Year;
                        if (!number_font.padding) value = value % 100;
                        string valueStr = value.ToString().PadLeft(2, '0');
                        string unitStr = "year";
                        //if (number_font.padding) valueStr = valueStr.PadLeft(2, '0');
                        if (number_font.unit_type > 0)
                        {
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr += unitStr;
                        }

                        if (number_font.centreHorizontally)
                        {
                            x = (SelectedModel.background.w - w) / 2;
                            align_h = "CENTER_H";
                        }
                        if (number_font.centreVertically)
                        {
                            y = (SelectedModel.background.h - h) / 2;
                            align_v = "CENTER_V";
                        }

                        if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                        {
                            string font_fileName = FontsList[number_font.font];
                            //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                            if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                            {
                                Font drawFont = null;
                                using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                {
                                    fonts.AddFontFile(font_fileName);
                                    drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                }

                                Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                align_h, align_v, text_style, BBorder);
                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }

                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }
                    }

                    if (DateYear.Text_rotation != null && DateYear.Text_rotation.img_First != null && DateYear.Text_rotation.img_First.Length > 0 &&
                                index == DateYear.Text_rotation.position && DateYear.Text_rotation.visible)
                    {
                        value_lenght = 4;
                        int pos_x = DateYear.Text_rotation.imageX;
                        int pos_y = DateYear.Text_rotation.imageY;
                        int spacing = DateYear.Text_rotation.space;
                        float angle = DateYear.Text_rotation.angle;
                        bool addZero = DateYear.Text_rotation.zero;
                        int image_index = ListImages.IndexOf(DateYear.Text_rotation.img_First);
                        int unit_index = ListImages.IndexOf(DateYear.Text_rotation.unit);
                        int dot_image_index = ListImages.IndexOf(DateYear.Text_rotation.dot_image);
                        string horizontal_alignment = DateYear.Text_rotation.align;
                        bool unit_in_alignment = DateYear.Text_rotation.unit_in_alignment;

                        string valueStr = (WatchFacePreviewSet.Date.Year).ToString();
                        if (!addZero) valueStr = (WatchFacePreviewSet.Date.Year % 100).ToString();
                        //if (addZero) valueStr = valueStr.PadLeft(value_lenght, '0');

                        Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                            image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                            valueStr, value_lenght, BBorder, -1, -1, false, "ElementDateYear");
                    }

                    if (DateYear.Text_circle != null && DateYear.Text_circle.img_First != null && DateYear.Text_circle.img_First.Length > 0 &&
                        index == DateYear.Text_circle.position && DateYear.Text_circle.visible)
                    {
                        value_lenght = 4;
                        int centr_x = DateYear.Text_circle.circle_center_X;
                        int centr_y = DateYear.Text_circle.circle_center_Y;
                        int radius = DateYear.Text_circle.radius;
                        int spacing = DateYear.Text_circle.char_space_angle;
                        float angle = DateYear.Text_circle.angle;
                        bool addZero = DateYear.Text_circle.zero;
                        int image_index = ListImages.IndexOf(DateYear.Text_circle.img_First);
                        int unit_index = ListImages.IndexOf(DateYear.Text_circle.unit);
                        int dot_image_index = ListImages.IndexOf(DateYear.Text_circle.dot_image);
                        string vertical_alignment = DateYear.Text_circle.vertical_alignment;
                        string horizontal_alignment = DateYear.Text_circle.horizontal_alignment;
                        bool reverse_direction = DateYear.Text_circle.reverse_direction;
                        bool unit_in_alignment = DateYear.Text_circle.unit_in_alignment;

                        string valueStr = (WatchFacePreviewSet.Date.Year).ToString();
                        if (!addZero) valueStr = (WatchFacePreviewSet.Date.Year % 100).ToString();
                        //if (addZero) valueStr = valueStr.PadLeft(value_lenght, '0');

                        Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                            image_index, unit_index, dot_image_index, vertical_alignment, horizontal_alignment,
                            reverse_direction, unit_in_alignment, valueStr, value_lenght, BBorder, showCentrHend, -1, -1, false, "ElementDateYear");
                    }

                    if (icon != null && icon.src != null && icon.src.Length > 0 &&
                        index == icon.position && icon.visible)
                    {
                        int imageIndex = ListImages.IndexOf(icon.src);
                        int x = icon.x;
                        int y = icon.y;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                                {
                                    int w = src.Width;
                                    int h = src.Height;
                                    // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                    ColorMatrix colorMatrix = new ColorMatrix();
                                    colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                    // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                    ImageAttributes imgAttributes = new ImageAttributes();
                                    imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                    // Указываем прямоугольник, куда будет помещено изображение
                                    Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                    gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                }
                                else gPanel.DrawImage(src, x, y);
                        }
                    }
                    }

                    break;
                #endregion

                #region ElementDateWeek
                case "ElementDateWeek":
                    ElementDateWeek DateWeek = (ElementDateWeek)element;
                    if (!DateWeek.visible) return;

                    for (int index = 1; index <= 15; index++)
                    {
                        if (DateWeek.Pointer != null && DateWeek.Pointer.src != null
                            && DateWeek.Pointer.src.Length > 0 &&
                            index == DateWeek.Pointer.position && DateWeek.Pointer.visible)
                        {
                            int x = DateWeek.Pointer.center_x;
                            int y = DateWeek.Pointer.center_y;
                            int offsetX = DateWeek.Pointer.pos_x;
                            int offsetY = DateWeek.Pointer.pos_y;
                            int startAngle = DateWeek.Pointer.start_angle;
                            int endAngle = DateWeek.Pointer.end_angle;
                            int image_index = ListImages.IndexOf(DateWeek.Pointer.src);
                            int WeekDay = WatchFacePreviewSet.Date.WeekDay;
                            //WeekDay++;
                            //if (WeekDay < 0) WeekDay = 6;
                            //if (WeekDay > 7) WeekDay = 1;
                            int angle = (int)(startAngle + WeekDay * (endAngle - startAngle) / 7f);

                            if (DateWeek.Pointer.scale != null && DateWeek.Pointer.scale.Length > 0)
                            {
                                int image_index_scale = ListImages.IndexOf(DateWeek.Pointer.scale);
                                int x_scale = DateWeek.Pointer.scale_x;
                                int y_scale = DateWeek.Pointer.scale_y;

                                src = OpenFileStream(ListImagesFullName[image_index_scale]);
                                if (src != null) gPanel.DrawImage(src, x_scale, y_scale);
                            }

                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (DateWeek.Pointer.cover_path != null && DateWeek.Pointer.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(DateWeek.Pointer.cover_path);
                                x = DateWeek.Pointer.cover_x;
                                y = DateWeek.Pointer.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (DateWeek.Images != null && DateWeek.Images.img_First != null
                            && DateWeek.Images.img_First.Length > 0 &&
                            index == DateWeek.Images.position && DateWeek.Images.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DateWeek.Images.img_First);
                            int x = DateWeek.Images.X;
                            int y = DateWeek.Images.Y;
                            imageIndex = imageIndex + WatchFacePreviewSet.Date.WeekDay - 1;

                            if (imageIndex < ListImagesFullName.Count)
                            {
                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                if (SelectedModel.versionOS >= 2.1 && DateWeek.Images.alpha != 255)
                                {
                                    int w = src.Width;
                                    int h = src.Height;
                                    // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                    ColorMatrix colorMatrix = new ColorMatrix();
                                    colorMatrix.Matrix33 = DateWeek.Images.alpha / 255f; // значение от 0 до 1

                                    // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                    ImageAttributes imgAttributes = new ImageAttributes();
                                    imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                    // Указываем прямоугольник, куда будет помещено изображение
                                    Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                    gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                }
                                else if (src != null) gPanel.DrawImage(src, x, y);
                            }
                        }

                        if (DateWeek.DayOfWeek_Font != null && index == DateWeek.DayOfWeek_Font.position && DateWeek.DayOfWeek_Font.visible)
                        {
                            hmUI_widget_TEXT dow_font = DateWeek.DayOfWeek_Font;
                            string[] dowArrey = dow_font.unit_string.Split(',');

                            if (dowArrey.Length == 7)
                            {
                                int strIndex = WatchFacePreviewSet.Date.WeekDay - 1;
                                string valueStr = dowArrey[strIndex].Trim();

                                int x = dow_font.x;
                                int y = dow_font.y;
                                int h = dow_font.h;
                                int w = dow_font.w;

                                int size = dow_font.text_size;
                                int space_h = dow_font.char_space;
                                int space_v = dow_font.line_space;

                                Color color = StringToColor(dow_font.color);
                                int alpha = dow_font.alpha;
                                if (dow_font.use_color_2 && strIndex >= 5) color = StringToColor(dow_font.color_2);
                                string align_h = dow_font.align_h;
                                string align_v = dow_font.align_v;
                                string text_style = dow_font.text_style;

                                if (dow_font.centreHorizontally)
                                {
                                    x = (SelectedModel.background.w - w) / 2;
                                    align_h = "CENTER_H";
                                }
                                if (dow_font.centreVertically)
                                {
                                    y = (SelectedModel.background.h - h) / 2;
                                    align_v = "CENTER_V";
                                }

                                if (dow_font.font != null && dow_font.font.Length > 3 && FontsList.ContainsKey(dow_font.font))
                                {
                                    string font_fileName = FontsList[dow_font.font];
                                    //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                                    if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                    {
                                        Font drawFont = null;
                                        using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                        {
                                            fonts.AddFontFile(font_fileName);
                                            drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                        }

                                        Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                        align_h, align_v, text_style, BBorder);
                                    }
                                    else
                                    {
                                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                    }

                                }
                                else
                                {
                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                } 
                            }
                        }
                    }

                    break;
                #endregion

                #region ElementStatuses
                case "ElementStatuses":
                    ElementStatuses statusElement = (ElementStatuses)element;
                    if (!statusElement.visible) return;

                    hmUI_widget_IMG_STATUS img_status_alarm = statusElement.Alarm;
                    hmUI_widget_IMG_STATUS img_status_bluetooth = statusElement.Bluetooth;
                    hmUI_widget_IMG_STATUS img_status_dnd = statusElement.DND;
                    hmUI_widget_IMG_STATUS img_status_lock = statusElement.Lock;

                    for (int index = 1; index <= 4; index++)
                    {
                        if (img_status_alarm != null && img_status_alarm.src != null &&
                        img_status_alarm.src.Length > 0 && index == img_status_alarm.position &&
                        img_status_alarm.visible)
                        {
                            if (WatchFacePreviewSet.Status.Alarm)
                            {
                                int imageIndex = ListImages.IndexOf(img_status_alarm.src);
                                int x = img_status_alarm.x;
                                int y = img_status_alarm.y;

                                if (imageIndex < ListImagesFullName.Count)
                                {
                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    if (SelectedModel.versionOS >= 2.1 && img_status_alarm.alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = img_status_alarm.alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else gPanel.DrawImage(src, x, y);
                                    //if (src != null) gPanel.DrawImage(src, x, y);
                                }
                            }
                        }

                        if (img_status_bluetooth != null && img_status_bluetooth.src != null &&
                        img_status_bluetooth.src.Length > 0 && index == img_status_bluetooth.position &&
                        img_status_bluetooth.visible)
                        {
                            if (!WatchFacePreviewSet.Status.Bluetooth)
                            {
                                int imageIndex = ListImages.IndexOf(img_status_bluetooth.src);
                                int x = img_status_bluetooth.x;
                                int y = img_status_bluetooth.y;

                                if (imageIndex < ListImagesFullName.Count)
                                {
                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    if (SelectedModel.versionOS >= 2.1 && img_status_bluetooth.alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = img_status_bluetooth.alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else gPanel.DrawImage(src, x, y);
                                    //if (src != null) gPanel.DrawImage(src, x, y);
                                }
                            }
                        }

                        if (img_status_dnd != null && img_status_dnd.src != null &&
                        img_status_dnd.src.Length > 0 && index == img_status_dnd.position &&
                        img_status_dnd.visible)
                        {
                            if (WatchFacePreviewSet.Status.DoNotDisturb)
                            {
                                int imageIndex = ListImages.IndexOf(img_status_dnd.src);
                                int x = img_status_dnd.x;
                                int y = img_status_dnd.y;

                                if (imageIndex < ListImagesFullName.Count)
                                {
                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    if (SelectedModel.versionOS >= 2.1 && img_status_dnd.alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = img_status_dnd.alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else gPanel.DrawImage(src, x, y);
                                    //if (src != null) gPanel.DrawImage(src, x, y);
                                }
                            }
                        }

                        if (img_status_lock != null && img_status_lock.src != null &&
                        img_status_lock.src.Length > 0 && index == img_status_lock.position &&
                        img_status_lock.visible)
                        {
                            if (WatchFacePreviewSet.Status.Lock)
                            {
                                int imageIndex = ListImages.IndexOf(img_status_lock.src);
                                int x = img_status_lock.x;
                                int y = img_status_lock.y;

                                if (imageIndex < ListImagesFullName.Count)
                                {
                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    if (SelectedModel.versionOS >= 2.1 && img_status_lock.alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = img_status_lock.alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else gPanel.DrawImage(src, x, y);
                                    //if (src != null) gPanel.DrawImage(src, x, y);
                                }
                            }
                        }
                    }
                    break;
                #endregion



                #region ElementSteps
                case "ElementSteps":
                    ElementSteps activityElementSteps = (ElementSteps)element;
                    if (!activityElementSteps.visible) return;

                    img_level = activityElementSteps.Images;
                    img_prorgess = activityElementSteps.Segments;
                    img_number = activityElementSteps.Number;
                    font_number = activityElementSteps.Number_Font;
                    text_rotation = activityElementSteps.Text_rotation;
                    text_circle = activityElementSteps.Text_circle;
                    img_number_target = activityElementSteps.Number_Target;
                    font_number_target = activityElementSteps.Number_Target_Font;
                    text_rotation_target = activityElementSteps.Text_rotation_Target;
                    text_circle_target = activityElementSteps.Text_circle_Target;
                    img_pointer = activityElementSteps.Pointer;
                    circle_scale = activityElementSteps.Circle_Scale;
                    linear_scale = activityElementSteps.Linear_Scale;
                    icon = activityElementSteps.Icon;

                    elementValue = WatchFacePreviewSet.Activity.Steps;
                    value_lenght = 5;
                    goal = WatchFacePreviewSet.Activity.StepsGoal;
                    progress = (float)WatchFacePreviewSet.Activity.Steps / WatchFacePreviewSet.Activity.StepsGoal;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        if (progress < 0.01) valueImgIndex = -1;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        if (progress < 0.01) valueSegmentIndex = -1;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target,img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementSteps");


                    break;
                #endregion

                #region ElementBattery
                case "ElementBattery":
                    ElementBattery activityElementBattery = (ElementBattery)element;
                    if (!activityElementBattery.visible) return;

                    img_level = activityElementBattery.Images;
                    img_prorgess = activityElementBattery.Segments;
                    img_number = activityElementBattery.Number;
                    font_number = activityElementBattery.Number_Font;
                    text_rotation = activityElementBattery.Text_rotation;
                    text_circle = activityElementBattery.Text_circle;
                    img_pointer = activityElementBattery.Pointer;
                    circle_scale = activityElementBattery.Circle_Scale;
                    linear_scale = activityElementBattery.Linear_Scale;
                    icon = activityElementBattery.Icon;

                    elementValue = WatchFacePreviewSet.Battery;
                    value_lenght = 3;
                    goal = 100;
                    progress = (float)WatchFacePreviewSet.Battery / 100f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        float imgIndex = imgCount * progress;
                        valueImgIndex = (int)imgIndex;
                        valueImgIndex--;
                        if (valueImgIndex < 0) valueImgIndex = 0;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        float imgIndex = segmentCount * progress;
                        valueSegmentIndex = (int)imgIndex;
                        valueSegmentIndex--;
                        if (valueSegmentIndex < 0) valueSegmentIndex = 0;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementBattery");


                    break;
                #endregion

                #region ElementCalories
                case "ElementCalories":
                    ElementCalories activityElementCalories = (ElementCalories)element;
                    if (!activityElementCalories.visible) return;

                    img_level = activityElementCalories.Images;
                    img_prorgess = activityElementCalories.Segments;
                    img_number = activityElementCalories.Number;
                    font_number = activityElementCalories.Number_Font;
                    text_rotation = activityElementCalories.Text_rotation;
                    text_circle = activityElementCalories.Text_circle;
                    img_number_target = activityElementCalories.Number_Target;
                    font_number_target = activityElementCalories.Number_Target_Font;
                    text_rotation_target = activityElementCalories.Text_rotation_Target;
                    text_circle_target = activityElementCalories.Text_circle_Target;
                    img_pointer = activityElementCalories.Pointer;
                    circle_scale = activityElementCalories.Circle_Scale;
                    linear_scale = activityElementCalories.Linear_Scale;
                    icon = activityElementCalories.Icon;

                    elementValue = WatchFacePreviewSet.Activity.Calories;
                    value_lenght = 4;
                    goal = 300;
                    progress = (float)WatchFacePreviewSet.Activity.Calories / 300f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        //if (progress < 0.01) valueImgIndex = -1;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        //if (progress < 0.01) valueSegmentIndex = -1;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementCalories");


                    break;
                #endregion

                #region ElementHeart
                case "ElementHeart":
                    ElementHeart activityElementHeart = (ElementHeart)element;
                    if (!activityElementHeart.visible) return;

                    img_level = activityElementHeart.Images;
                    img_prorgess = activityElementHeart.Segments;
                    img_number = activityElementHeart.Number;
                    font_number = activityElementHeart.Number_Font;
                    text_rotation = activityElementHeart.Text_rotation;
                    text_circle = activityElementHeart.Text_circle;
                    img_pointer = activityElementHeart.Pointer;
                    circle_scale = activityElementHeart.Circle_Scale;
                    linear_scale = activityElementHeart.Linear_Scale;
                    icon = activityElementHeart.Icon;

                    elementValue = WatchFacePreviewSet.Activity.HeartRate;
                    value_lenght = 3;
                    goal = 179;
                    progress = (WatchFacePreviewSet.Activity.HeartRate - 71) / (179f - 71);

                    //if (img_level != null && img_level.image_length > 0)
                    //{
                    //    imgCount = img_level.image_length;
                    //    valueImgIndex = (int)((imgCount - 1) * progress);
                    //    if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    //}
                    //if (img_prorgess != null && img_prorgess.image_length > 0)
                    //{
                    //    segmentCount = img_prorgess.image_length;
                    //    valueSegmentIndex = (int)((segmentCount - 1) * progress);
                    //    if (progress < 0.01) valueSegmentIndex = -1;
                    //    if (valueSegmentIndex >= segmentCount) valueImgIndex = (int)(segmentCount - 1);
                    //}
                    if (elementValue < 90)
                    {
                        valueImgIndex = 0;
                        valueSegmentIndex = 0;
                    }
                    if (elementValue >= 90 && elementValue < 108)
                    {
                        valueImgIndex = 1;
                        valueSegmentIndex = 1;
                    }
                    if (elementValue >= 108 && elementValue < 126)
                    {
                        valueImgIndex = 2;
                        valueSegmentIndex = 2;
                    }
                    if (elementValue >= 126 && elementValue < 144)
                    {
                        valueImgIndex = 3;
                        valueSegmentIndex = 3;
                    }
                    if (elementValue >= 144 && elementValue < 162)
                    {
                        valueImgIndex = 4;
                        valueSegmentIndex = 4;
                    }
                    if (elementValue >= 162)
                    {
                        valueImgIndex = 5;
                        valueSegmentIndex = 5;
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementHeart");


                    break;
                #endregion

                #region ElementPAI
                case "ElementPAI":
                    ElementPAI activityElementPAI = (ElementPAI)element;
                    if (!activityElementPAI.visible) return;

                    img_level = activityElementPAI.Images;
                    img_prorgess = activityElementPAI.Segments;
                    img_number = activityElementPAI.Number;
                    //font_number = activityElementPA-*21Й  Ё
                    //-I.Number_Font;
                    img_number_target = activityElementPAI.Number_Target;
                    font_number_target = activityElementPAI.Number_Target_Font;
                    text_rotation_target = activityElementPAI.Text_rotation_Target;
                    text_circle_target = activityElementPAI.Text_circle_Target;
                    img_pointer = activityElementPAI.Pointer;
                    circle_scale = activityElementPAI.Circle_Scale;
                    linear_scale = activityElementPAI.Linear_Scale;
                    icon = activityElementPAI.Icon;

                    //elementValue = WatchFacePreviewSet.Activity.PAI;
                    value_lenght = 3;
                    goal = WatchFacePreviewSet.Activity.PAI;
                    elementValue = goal / 7;
                    //value_altitude = 100;
                    progress = (float)WatchFacePreviewSet.Activity.PAI / 100f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        //if (progress < 0.01) valueImgIndex = -1;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        //if (progress < 0.01) valueSegmentIndex = -1;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementPAI");


                    break;
                #endregion

                #region ElementDistance
                case "ElementDistance":
                    ElementDistance activityElementDistance = (ElementDistance)element;
                    if (!activityElementDistance.visible) return;

                    img_number = activityElementDistance.Number;
                    font_number = activityElementDistance.Number_Font;
                    text_rotation = activityElementDistance.Text_rotation;
                    text_circle = activityElementDistance.Text_circle;
                    icon = activityElementDistance.Icon;

                    elementValue = WatchFacePreviewSet.Activity.Distance;
                    float distance_value = (float)Math.Round(elementValue / 1000f, 2);

                    DrawDistance(gPanel, img_number, font_number, text_rotation, text_circle, icon, distance_value, BBorder, showCentrHend);

                    break;
                #endregion

                #region ElementStand
                case "ElementStand":
                    ElementStand activityElementStand = (ElementStand)element;
                    if (!activityElementStand.visible) return;

                    img_level = activityElementStand.Images;
                    img_prorgess = activityElementStand.Segments;
                    img_number = activityElementStand.Number;
                    font_number = activityElementStand.Number_Font;
                    text_rotation = activityElementStand.Text_rotation;
                    text_circle = activityElementStand.Text_circle;
                    img_number_target = activityElementStand.Number_Target;
                    font_number_target = activityElementStand.Number_Target_Font;
                    text_rotation_target = activityElementStand.Text_rotation_Target;
                    text_circle_target = activityElementStand.Text_circle_Target;
                    img_pointer = activityElementStand.Pointer;
                    circle_scale = activityElementStand.Circle_Scale;
                    linear_scale = activityElementStand.Linear_Scale;
                    icon = activityElementStand.Icon;

                    elementValue = WatchFacePreviewSet.Activity.StandUp;
                    value_lenght = 2;
                    goal = 12;
                    progress = (float)WatchFacePreviewSet.Activity.StandUp / 12f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        //if (progress < 0.01) valueImgIndex = -1;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        //if (progress < 0.01) valueSegmentIndex = -1;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementStand");


                    break;
                #endregion

                #region ElementActivity
                case "ElementActivity":
                    ElementActivity activityElementActivity = (ElementActivity)element;
                    if (!activityElementActivity.visible) return;

                    img_level = activityElementActivity.Images;
                    img_prorgess = activityElementActivity.Segments;
                    img_number = activityElementActivity.Number;
                    font_number = activityElementActivity.Number_Font;
                    img_number_target = activityElementActivity.Number_Target;
                    font_number_target = activityElementActivity.Number_Target_Font;
                    img_pointer = activityElementActivity.Pointer;
                    circle_scale = activityElementActivity.Circle_Scale;
                    linear_scale = activityElementActivity.Linear_Scale;
                    icon = activityElementActivity.Icon;

                    elementValue = WatchFacePreviewSet.Activity.Steps;
                    value_lenght = 5;
                    goal = WatchFacePreviewSet.Activity.StepsGoal;
                    progress = (float)WatchFacePreviewSet.Activity.Steps / WatchFacePreviewSet.Activity.StepsGoal;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        if (progress < 0.01) valueImgIndex = -1;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        if (progress < 0.01) valueSegmentIndex = -1;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    // пересчитываем данные если отображаем как калории
                    if (activityElementActivity.showCalories)
                    {
                        elementValue = WatchFacePreviewSet.Activity.Calories;
                        value_lenght = 4;
                        goal = 300;
                        progress = (float)WatchFacePreviewSet.Activity.Calories / 300f;

                        if (img_level != null && img_level.image_length > 0)
                        {
                            imgCount = img_level.image_length;
                            valueImgIndex = (int)((imgCount - 1) * progress);
                            //if (progress < 0.01) valueImgIndex = -1;
                            if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                        }
                        if (img_prorgess != null && img_prorgess.image_length > 0)
                        {
                            segmentCount = img_prorgess.image_length;
                            valueSegmentIndex = (int)((segmentCount - 1) * progress);
                            //if (progress < 0.01) valueSegmentIndex = -1;
                            if (valueSegmentIndex >= segmentCount) valueImgIndex = (int)(segmentCount - 1);
                        }
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementActivity");


                    break;
                #endregion

                #region ElementSpO2
                case "ElementSpO2":
                    ElementSpO2 activityElementSpO2 = (ElementSpO2)element;
                    if (!activityElementSpO2.visible) return;

                    img_number = activityElementSpO2.Number;
                    font_number = activityElementSpO2.Number_Font;
                    text_rotation = activityElementSpO2.Text_rotation;
                    text_circle = activityElementSpO2.Text_circle;
                    icon = activityElementSpO2.Icon;

                    elementValue = 97;
                    value_lenght = 3;
                    goal = 100;
                    progress = 0;


                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementSpO2");


                    break;
                #endregion

                #region ElementStress
                case "ElementStress":
                    ElementStress activityElementStress = (ElementStress)element;
                    if (!activityElementStress.visible) return;

                    img_level = activityElementStress.Images;
                    img_prorgess = activityElementStress.Segments;
                    img_number = activityElementStress.Number;
                    font_number = activityElementStress.Number_Font;
                    img_pointer = activityElementStress.Pointer;
                    icon = activityElementStress.Icon;

                    elementValue = WatchFacePreviewSet.Activity.Stress;
                    value_lenght = 3;
                    goal = 100;
                    progress = (float)WatchFacePreviewSet.Activity.Stress / 100f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                        if (elementValue == 0) valueImgIndex = -1;
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                        if (elementValue == 0) valueSegmentIndex = -1;
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementStress");


                    break;
                #endregion

                #region ElementFatBurning
                case "ElementFatBurning":
                    ElementFatBurning activityElementFatBurning = (ElementFatBurning)element;
                    if (!activityElementFatBurning.visible) return;

                    img_level = activityElementFatBurning.Images;
                    img_prorgess = activityElementFatBurning.Segments;
                    img_number = activityElementFatBurning.Number;
                    font_number = activityElementFatBurning.Number_Font;
                    text_rotation = activityElementFatBurning.Text_rotation;
                    text_circle = activityElementFatBurning.Text_circle;
                    img_number_target = activityElementFatBurning.Number_Target;
                    font_number_target = activityElementFatBurning.Number_Target_Font;
                    text_rotation_target = activityElementFatBurning.Text_rotation_Target;
                    text_circle_target = activityElementFatBurning.Text_circle_Target;
                    img_pointer = activityElementFatBurning.Pointer;
                    circle_scale = activityElementFatBurning.Circle_Scale;
                    linear_scale = activityElementFatBurning.Linear_Scale;
                    icon = activityElementFatBurning.Icon;

                    elementValue = WatchFacePreviewSet.Activity.FatBurning;
                    value_lenght = 3;
                    goal = 30;
                    progress = (float)WatchFacePreviewSet.Activity.FatBurning / 30f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                        if (elementValue == 0) valueImgIndex = -1;
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                        if (elementValue == 0) valueSegmentIndex = -1;
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementFatBurning");


                    break;
                #endregion



                /*#region ElementWeather
                case "ElementWeather":
                    ElementWeather activityElementWeather = (ElementWeather)element;
                    if (!activityElementWeather.visible) return;

                    value_lenght = 3;
                    img_level = activityElementWeather.Images;
                    img_number = activityElementWeather.Number;
                    font_number = activityElementWeather.Number_Font;

                    hmUI_widget_IMG_NUMBER img_number_min = activityElementWeather.Number_Min;
                    hmUI_widget_TEXT font_number_min = activityElementWeather.Number_Min_Font;
                    hmUI_widget_IMG_NUMBER text_Min_rotation = activityElementWeather.Text_Min_rotation;
                    Text_Circle text_Min_circle = activityElementWeather.Text_Min_circle;

                    hmUI_widget_IMG_NUMBER img_number_max = activityElementWeather.Number_Max;
                    hmUI_widget_TEXT font_number_max = activityElementWeather.Number_Max_Font;
                    hmUI_widget_IMG_NUMBER text_Max_rotation = activityElementWeather.Text_Max_rotation;
                    Text_Circle text_Max_circle = activityElementWeather.Text_Max_circle;

                    hmUI_widget_TEXT font_number_min_max = activityElementWeather.Number_Min_Max_Font;

                    hmUI_widget_TEXT city_name = activityElementWeather.City_Name;
                    icon = activityElementWeather.Icon;

                    int value_current = WatchFacePreviewSet.Weather.Temperature;
                    int value_min = WatchFacePreviewSet.Weather.TemperatureMin;
                    int value_max = WatchFacePreviewSet.Weather.TemperatureMax;
                    int icon_index = WatchFacePreviewSet.Weather.Icon;
                    bool showTemperature = WatchFacePreviewSet.Weather.showTemperature;

                    DrawWeather(gPanel, img_level, img_number, font_number, img_number_min, font_number_min, text_Min_rotation, text_Min_circle,
                        img_number_max, font_number_max, text_Max_rotation, text_Max_circle, font_number_min_max, city_name, icon, value_current, value_min, value_max, value_lenght, icon_index,
                        BBorder, showTemperature, showCentrHend);


                    break;
                #endregion*/

                #region ElementWeather_v2
                case "ElementWeather_v2":
                    ElementWeather_v2 activityElementWeather_v2 = (ElementWeather_v2)element;
                    if (!activityElementWeather_v2.visible) return;

                    value_lenght = 3;
                    WeatherGroup group_current = activityElementWeather_v2.Group_Current;
                    WeatherGroup group_min = activityElementWeather_v2.Group_Min;
                    WeatherGroup group_max = activityElementWeather_v2.Group_Max;
                    WeatherGroup group_max_min = activityElementWeather_v2.Group_Max_Min;

                    img_level = activityElementWeather_v2.Images;
                    hmUI_widget_TEXT city_name_v2 = activityElementWeather_v2.City_Name;
                    icon = activityElementWeather_v2.Icon;

                    int value_current_v2 = WatchFacePreviewSet.Weather.Temperature;
                    int value_min_v2 = WatchFacePreviewSet.Weather.TemperatureMin;
                    int value_max_v2 = WatchFacePreviewSet.Weather.TemperatureMax;
                    int icon_index_v2 = WatchFacePreviewSet.Weather.Icon;
                    bool showTemperature_v2 = WatchFacePreviewSet.Weather.showTemperature;

                    DrawWeather_v2(gPanel, group_current, group_min, group_max, group_max_min, img_level, city_name_v2, icon,
                        value_current_v2, value_min_v2, value_max_v2, value_lenght, icon_index_v2,
                        BBorder, showTemperature_v2, showCentrHend);


                    break;
                #endregion

                #region Element_Weather_FewDays
                case "Element_Weather_FewDays":
                    Element_Weather_FewDays activityElement_Weather_FewDays = (Element_Weather_FewDays)element;
                    if (!activityElement_Weather_FewDays.visible) return;

                    bool showTemperature_FewDays = WatchFacePreviewSet.Weather.showTemperature;

                    DrawWeatherFewDays(gPanel, activityElement_Weather_FewDays, WatchFacePreviewSet.Weather.forecastData, BBorder, showTemperature_FewDays);


                    break;
                #endregion

                #region ElementUVIndex
                case "ElementUVIndex":
                    ElementUVIndex activityElementUVIndex = (ElementUVIndex)element;
                    if (!activityElementUVIndex.visible) return;

                    img_level = activityElementUVIndex.Images;
                    img_prorgess = activityElementUVIndex.Segments;
                    img_number = activityElementUVIndex.Number;
                    font_number = activityElementUVIndex.Number_Font;
                    img_pointer = activityElementUVIndex.Pointer;
                    icon = activityElementUVIndex.Icon;

                    elementValue = WatchFacePreviewSet.Weather.UVindex;
                    value_lenght = 1;
                    goal = 5;
                    progress = (float)WatchFacePreviewSet.Weather.UVindex / 5f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)(imgCount * progress);
                        valueImgIndex--;
                        if (valueImgIndex < 0) valueImgIndex = 0;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                        if (imgCount == 5)
                        {
                            switch (elementValue)
                            {
                                case 0:
                                case 1:
                                case 2:
                                    valueImgIndex = 0;
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                    valueImgIndex = 1;
                                    break;
                                case 6:
                                case 7:
                                    valueImgIndex = 2;
                                    break;
                                case 8:
                                case 9:
                                case 10:
                                    valueImgIndex = 3;
                                    break;
                                default:
                                    valueImgIndex = 4;
                                    break;
                            }
                        }
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)(segmentCount * progress);
                        valueSegmentIndex--;
                        if (valueSegmentIndex < 0) valueSegmentIndex = 0;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                        if (segmentCount == 5)
                        {
                            switch (elementValue)
                            {
                                case 0:
                                case 1:
                                case 2:
                                    valueSegmentIndex = 0;
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                    valueSegmentIndex = 1;
                                    break;
                                case 6:
                                case 7:
                                    valueSegmentIndex = 2;
                                    break;
                                case 8:
                                case 9:
                                case 10:
                                    valueSegmentIndex = 3;
                                    break;
                                default:
                                    valueSegmentIndex = 4;
                                    break;
                            }
                        }
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementUVIndex");


                    break;
                #endregion

                #region ElementHumidity
                case "ElementHumidity":
                    ElementHumidity activityElementHumidity = (ElementHumidity)element;
                    if (!activityElementHumidity.visible) return;

                    img_level = activityElementHumidity.Images;
                    img_prorgess = activityElementHumidity.Segments;
                    img_number = activityElementHumidity.Number;
                    font_number = activityElementHumidity.Number_Font;
                    img_pointer = activityElementHumidity.Pointer;
                    icon = activityElementHumidity.Icon;

                    elementValue = WatchFacePreviewSet.Weather.Humidity;
                    value_lenght = 1;
                    goal = 100;
                    progress = (float)WatchFacePreviewSet.Weather.Humidity / 100f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)(imgCount * progress);
                        valueImgIndex--;
                        if (valueImgIndex < 0) valueImgIndex = 0;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)(segmentCount * progress);
                        valueSegmentIndex--;
                        if (valueSegmentIndex < 0) valueSegmentIndex = 0;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementHumidity");


                    break;
                #endregion

                #region ElementAltimeter
                case "ElementAltimeter":
                    ElementAltimeter activityElementAltimeter = (ElementAltimeter)element;
                    if (!activityElementAltimeter.visible) return;

                    img_number = activityElementAltimeter.Number;
                    font_number = activityElementAltimeter.Number_Font;
                    img_number_target = activityElementAltimeter.Number_Target;
                    font_number_target = activityElementAltimeter.Number_Target_Font;
                    img_pointer = activityElementAltimeter.Pointer;
                    icon = activityElementAltimeter.Icon;

                    elementValue = WatchFacePreviewSet.Weather.AirPressure;
                    //value_lenght = 4;
                    goal = 1200;
                    //value_altitude = 1170 - 195;
                    //progress = (WatchFacePreviewSet.Weather.AirPressure - 195) / 975f;
                    progress = WatchFacePreviewSet.Weather.AirPressure / 1200f;
                    progress = (int)(progress * 100);
                    progress = progress / 100f;
                    int value_altitude = WatchFacePreviewSet.Weather.Altitude;

                    //DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                    //    text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                    //    progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                    //    showCentrHend, "ElementAltimeter");

                    DrawAltimeter(gPanel, img_number, font_number, img_number_target, font_number_target,
                        img_pointer, icon, elementValue, value_altitude, progress, BBorder, showProgressArea, showCentrHend);


                    break;
                #endregion

                #region ElementSunrise
                case "ElementSunrise":
                    ElementSunrise activityElementSunrise = (ElementSunrise)element;
                    if (!activityElementSunrise.visible) return;

                    img_level = activityElementSunrise.Images;
                    img_prorgess = activityElementSunrise.Segments;

                    img_number = activityElementSunrise.Sunrise;
                    font_number = activityElementSunrise.Sunrise_Font;
                    hmUI_widget_IMG_NUMBER sunrise_rotation = activityElementSunrise.Sunrise_rotation;
                    Text_Circle sunrise_circle = activityElementSunrise.Sunrise_circle;

                    img_number_target = activityElementSunrise.Sunset;
                    font_number_target = activityElementSunrise.Sunset_Font;
                    hmUI_widget_IMG_NUMBER sunset_rotation = activityElementSunrise.Sunset_rotation;
                    Text_Circle sunset_circle = activityElementSunrise.Sunset_circle;

                    img_pointer = activityElementSunrise.Pointer;
                    icon = activityElementSunrise.Icon;

                    int minSunrise = WatchFacePreviewSet.Time.Minutes;
                    int hourSunrise = WatchFacePreviewSet.Time.Hours;

                    DrawSunrise(gPanel, img_level, img_prorgess, img_number, font_number, sunrise_rotation, sunrise_circle,
                        img_number_target, font_number_target, sunset_rotation, sunset_circle,
                        activityElementSunrise.Sunset_Sunrise,
                        img_pointer, icon, hourSunrise, minSunrise, BBorder, showProgressArea, showCentrHend);


                    break;
                #endregion

                #region ElementWind
                case "ElementWind":
                    ElementWind activityElementWind = (ElementWind)element;
                    if (!activityElementWind.visible) return;

                    img_level = activityElementWind.Images;
                    img_prorgess = activityElementWind.Segments;
                    img_number = activityElementWind.Number;
                    font_number = activityElementWind.Number_Font;
                    img_pointer = activityElementWind.Pointer;
                    hmUI_widget_IMG_LEVEL img_direction = activityElementWind.Direction;
                    icon = activityElementWind.Icon;

                    elementValue = WatchFacePreviewSet.Weather.WindForce;
                    value_lenght = 1;
                    goal = 12;
                    progress = (float)WatchFacePreviewSet.Weather.WindForce / 12f;
                    int valueImgDirectionIndex = WatchFacePreviewSet.Weather.WindDirection;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        valueImgIndex = elementValue - 2;
                        if (valueImgIndex < 0) valueImgIndex = 0;
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        valueSegmentIndex = elementValue - 2;
                        if (valueSegmentIndex < 0) valueSegmentIndex = 0;
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawWind(gPanel, img_level, img_prorgess, img_number, font_number, img_pointer, img_direction, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, valueImgDirectionIndex, BBorder, showProgressArea, showCentrHend);


                    break;
                #endregion

                #region ElementMoon
                case "ElementMoon":
                    ElementMoon activityElementMoon = (ElementMoon)element;
                    if (!activityElementMoon.visible) return;

                    img_level = activityElementMoon.Images;
                    //img_prorgess = activityElementMoon.Segments;

                    img_number = activityElementMoon.Sunrise;
                    font_number = activityElementMoon.Sunrise_Font;
                    //hmUI_widget_IMG_NUMBER sunrise_rotation = activityElementMoon.Sunrise_rotation;
                    //Text_Circle sunrise_circle = activityElementMoon.Sunrise_circle;

                    img_number_target = activityElementMoon.Sunset;
                    font_number_target = activityElementMoon.Sunset_Font;
                    //hmUI_widget_IMG_NUMBER sunset_rotation = activityElementMoon.Sunset_rotation;
                    //Text_Circle sunset_circle = activityElementMoon.Sunset_circle;

                    img_pointer = activityElementMoon.Pointer;
                    icon = activityElementMoon.Icon;

                    int minSunriseMoon = WatchFacePreviewSet.Time.Minutes;
                    int hourSunriseMoon = WatchFacePreviewSet.Time.Hours;

                    DrawMoon(gPanel, img_level, /*img_prorgess,*/ img_number, font_number, null, null,
                        img_number_target, font_number_target, null, null,
                        activityElementMoon.Sunset_Sunrise,
                        img_pointer, icon, hourSunriseMoon, minSunriseMoon, BBorder, showProgressArea, showCentrHend);


                    break;
                #endregion

                #region ElementImage
                case "ElementImage":
                    ElementImage activityElementImage = (ElementImage)element;
                    if (!activityElementImage.visible) return;

                    icon = activityElementImage.Icon;

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementImage");


                    break;
                #endregion

                #region ElementCompass
                case "ElementCompass":
                    ElementCompass activityElementCompass = (ElementCompass)element;
                    if (!activityElementCompass.visible) return;
                    if (SelectedModel.versionOS < 2) return;

                    img_level = activityElementCompass.Images;
                    img_number = activityElementCompass.Number;
                    font_number = activityElementCompass.Number_Font;
                    text_rotation = activityElementCompass.Text_rotation;
                    text_circle = activityElementCompass.Text_circle;
                    img_pointer = activityElementCompass.Pointer;
                    icon = activityElementCompass.Icon;

                    elementValue = WatchFacePreviewSet.Weather.CompassDirection;
                    value_lenght = 3;
                    goal = 360;
                    progress = (float)elementValue / 360;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        int temp_value = 45/2 + elementValue;
                        valueImgIndex = (int)(temp_value / 45);
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, font_number, text_rotation, text_circle, img_number_target, font_number_target,
                        text_rotation_target, text_circle_target, img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementCompass");


                    break;
                #endregion

                #region ElementAnimation
                case "ElementAnimation":
                    ElementAnimation elementAnimation = (ElementAnimation)element;
                    if (!elementAnimation.visible) return;

                    for (int indexAnim = 1; indexAnim <= 3; indexAnim++)
                    {
                        hmUI_widget_IMG_ANIM_List frame_Animation_List = elementAnimation.Frame_Animation_List;
                        if (frame_Animation_List != null && frame_Animation_List.visible &&
                            frame_Animation_List.position == indexAnim && frame_Animation_List.Frame_Animation != null)
                        {
                            foreach (hmUI_widget_IMG_ANIM animation_frame in frame_Animation_List.Frame_Animation)
                            {
                                if (animation_frame.visible) DrawAnimationFrame(gPanel, animation_frame, time_value_sec);
                            }
                        }

                        Motion_Animation_List motion_Animation_List = elementAnimation.Motion_Animation_List;
                        if (motion_Animation_List != null && motion_Animation_List.visible &&
                            motion_Animation_List.position == indexAnim && motion_Animation_List.Motion_Animation != null)
                        {
                            foreach (Motion_Animation motion_Animation in motion_Animation_List.Motion_Animation)
                            {
                                if (motion_Animation.visible) DrawAnimationMotion(gPanel, motion_Animation, time_value_sec);
                            }
                        }

                        Rotate_Animation_List rotate_Animation_List = elementAnimation.Rotate_Animation_List;
                        if (rotate_Animation_List != null && rotate_Animation_List.visible &&
                            rotate_Animation_List.position == indexAnim && rotate_Animation_List.Rotate_Animation != null)
                        {
                            foreach (Rotate_Animation rotate_Animation in rotate_Animation_List.Rotate_Animation)
                            {
                                if (rotate_Animation.visible) DrawAnimationRotate(gPanel, rotate_Animation, time_value_sec, showCentrHend);
                            }
                        }
                    }


                    break;
                    #endregion

            }
            if (src != null) src.Dispose();
        }

        /// <param name="edit_mode">Выбор отображаемого режима редактирования. 
        /// 1 - редактируемый задний фон
        /// 2 - редактируемые элементы
        /// 3 - редактируемые стрелки</param>
        public void Preview_edit_screen(Graphics gPanel, int edit_mode, float scale, bool crop, bool WMesh, bool BMesh)
        {
            Bitmap src = new Bitmap(1, 1);

            #region Black background
            Logger.WriteLine("Preview_edit_screen (Black background)");
            /*src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3.png");
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3 Pro":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                    break;
                case "GTS 3":
                case "GTS 4":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_3.png");
                    break;
                case "GTR 4":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                    break;
                case "Amazfit Band 7":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_band_7.png");
                    break;
                case "GTS 4 mini":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                    break;
                case "Falcon":
                case "GTR mini":
                    src = OpenFileStream(Application.StartupPath + @"\Mask\mask_falcon.png");
                    break;
            }*/
            src = OpenFileStream(Application.StartupPath + @"\Mask\" + SelectedModel.maskImage);
            gPanel.DrawImage(src, 0, 0);
            int offSet_X = src.Width / 2;
            int offSet_Y = src.Height / 2;
            #endregion

            Editable_Background editable_background = null;
            EditableElements editable_elements = null;
            ElementEditablePointers editable_pointers = null;

            #region Background
            Background background = null;
            if (Watch_Face != null && Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                background = Watch_Face.ScreenNormal.Background;
            if (background != null && background.visible)
            {
                if (background.BackgroundColor != null && background.visible)
                {
                    Color color = StringToColor(background.BackgroundColor.color);
                    gPanel.Clear(color);
                }
                if (background != null)
                {
                    if (background.BackgroundImage != null && background.BackgroundImage.src != null &&
                        background.BackgroundImage.src.Length > 0 && background.visible)
                    {
                        src = OpenFileStream(background.BackgroundImage.src);
                        int x = background.BackgroundImage.x;
                        int y = background.BackgroundImage.y;
                        if (src != null) gPanel.DrawImage(src, x, y);
                    }
                }
                if (background.Editable_Background != null && background.Editable_Background.enable_edit_bg &&
                    background.Editable_Background.BackgroundList != null &&
                    background.Editable_Background.BackgroundList.Count > 0 && background.visible)
                {
                    int index = background.Editable_Background.selected_background;
                    editable_background = background.Editable_Background;
                    if (index >= 0 && index < editable_background.BackgroundList.Count &&
                        editable_background.BackgroundList[index].preview != null &&
                        editable_background.BackgroundList[index].preview.Length > 0)
                    {
                        src = OpenFileStream(editable_background.BackgroundList[index].preview);
                        gPanel.DrawImage(src, 0, 0);
                    }
                }
            }
            #endregion

            #region EditableElements
            if (Watch_Face != null && Watch_Face.Editable_Elements != null)
                editable_elements = Watch_Face.Editable_Elements;
            if (editable_elements != null && editable_elements.visible)
            {
                if (editable_elements.visible &&
                    editable_elements.Watchface_edit_group != null &&
                    editable_elements.Watchface_edit_group.Count > 0)
                {
                    // предпросмотр элемента
                    for(int i = 0; i < editable_elements.Watchface_edit_group.Count; i++)
                    {
                        int element_index = editable_elements.Watchface_edit_group[i].selected_element;
                        if (element_index >= 0 && element_index < editable_elements.Watchface_edit_group[i].optional_types_list.Count &&
                            editable_elements.Watchface_edit_group[i].optional_types_list[element_index].preview != null &&
                            editable_elements.Watchface_edit_group[i].optional_types_list[element_index].preview.Length > 0)
                        {
                            int h = 0;
                            int w = 0;
                            src = OpenFileStream(editable_elements.Watchface_edit_group[i].optional_types_list[0].preview);
                            if (src != null)
                            {
                                h = src.Height;
                                w = src.Width; 
                            }
                            src = OpenFileStream(editable_elements.Watchface_edit_group[i].optional_types_list[element_index].preview);
                            if (src != null)
                            {
                                int x = editable_elements.Watchface_edit_group[i].x + editable_elements.Watchface_edit_group[i].w / 2 - w / 2;
                                int y = editable_elements.Watchface_edit_group[i].y + editable_elements.Watchface_edit_group[i].h / 2 - h / 2;
                                //gPanel.DrawImage(src, x, y);
                                if (w > 0 && h > 0)
                                {
                                    Rectangle cropRect = new Rectangle(0, 0, w, h);
                                    //Rectangle cropRect = new Rectangle(...);
                                    //Bitmap src = Image.FromFile(fileName) as Bitmap;
                                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                                    using (Graphics g = Graphics.FromImage(target))
                                    {
                                        g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                                         cropRect, GraphicsUnit.Pixel);
                                    }
                                    gPanel.DrawImage(target, x, y);
                                } 
                            }
                        }

                        element_index = editable_elements.selected_zone;
                        if (edit_mode == 2) // рисуем рамки элементов
                        {
                            if (element_index == i) // рамка выделеного элемента
                            {
                                if (i >= 0 && i < editable_elements.Watchface_edit_group.Count &&
                                editable_elements.Watchface_edit_group[i].select_image != null &&
                                editable_elements.Watchface_edit_group[i].select_image.Length > 0)
                                {
                                    int x = editable_elements.Watchface_edit_group[i].x;
                                    int y = editable_elements.Watchface_edit_group[i].y;
                                    src = OpenFileStream(editable_elements.Watchface_edit_group[i].select_image);
                                    gPanel.DrawImage(src, x, y);
                                }
                            }
                            else // рамка невыделеного элемента
                            {
                                if (i >= 0 && i < editable_elements.Watchface_edit_group.Count &&
                                editable_elements.Watchface_edit_group[i].un_select_image != null &&
                                editable_elements.Watchface_edit_group[i].un_select_image.Length > 0)
                                {
                                    int x = editable_elements.Watchface_edit_group[i].x;
                                    int y = editable_elements.Watchface_edit_group[i].y;
                                    src = OpenFileStream(editable_elements.Watchface_edit_group[i].un_select_image);
                                    gPanel.DrawImage(src, x, y);
                                }
                            } 
                        }
                    }

                    if (editable_elements.mask != null && editable_elements.mask.Length > 0)
                    {
                        src = OpenFileStream(editable_elements.mask);
                        gPanel.DrawImage(src, 0, 0);
                    }
                }
            }
            #endregion

            #region EditablePointers
            if (Watch_Face != null && Watch_Face.ElementEditablePointers != null)
                editable_pointers = Watch_Face.ElementEditablePointers;
            if (editable_pointers != null && editable_pointers.visible)
            {
                if (editable_pointers.visible &&
                    editable_pointers.config != null &&
                    editable_pointers.config.Count > 0)
                {
                    int index = editable_pointers.selected_pointers;
                    if (index >= 0 && index < editable_pointers.config.Count &&
                        editable_pointers.config[index].preview != null &&
                        editable_pointers.config[index].preview.Length > 0)
                    {
                        src = OpenFileStream(editable_pointers.config[index].preview);
                        gPanel.DrawImage(src, 0, 0);
                    }
                }
            }
            #endregion

            // подсказки на экране редактирования
            if (edit_mode == 1 && editable_background != null && background.visible)
            {
                if (editable_background.fg != null && editable_background.fg.Length > 0)
                {
                    src = OpenFileStream(editable_background.fg);
                    gPanel.DrawImage(src, 0, 0);
                }
                if (editable_background.tips_bg != null && editable_background.tips_bg.Length > 0)
                {
                    int tips_x = editable_background.tips_x;
                    int tips_y = editable_background.tips_y;
                    src = OpenFileStream(editable_background.tips_bg);
                    gPanel.DrawImage(src, tips_x, tips_y);
                    int index = background.Editable_Background.selected_background;

                    int x = tips_x + 5;
                    int y = tips_y;
                    int h = src.Height;
                    int w = src.Width - 10;

                    int size = 19;
                    int space_h = 0;
                    int space_v = 0;

                    Color color = Color.Black;
                    string align_h = "CENTER_H";
                    string align_v = "CENTER_V";
                    string text_style = "ELLIPSIS";
                    string valueStr = Properties.FormStrings.Tip_Background + (index + 1).ToString() +
                        "/" + editable_background.BackgroundList.Count.ToString();
                    //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2") 
                    if (SelectedModel.versionOS >= 1.5) valueStr = Properties.FormStrings.Tip_Background.TrimEnd();

                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, 255, valueStr,
                    align_h, align_v, text_style, false);
                }
            }

            if (edit_mode == 2 && editable_elements != null && editable_elements.visible)
            {
                if (editable_elements.fg_mask != null && editable_elements.fg_mask.Length > 0)
                {
                    //if (ProgramSettings.Watch_Model != "GTR 4" && ProgramSettings.Watch_Model != "GTS 4" && ProgramSettings.Watch_Model != "T-Rex 2")
                    if (SelectedModel.versionOS >= 1.5)
                    {
                        src = OpenFileStream(editable_elements.fg_mask);
                        gPanel.DrawImage(src, 0, 0); 
                    }
                }
                int selected_zone = editable_elements.selected_zone;
                if (selected_zone >= 0 && editable_elements.Watchface_edit_group != null &&
                    selected_zone < editable_elements.Watchface_edit_group.Count)
                {
                    if (editable_elements.Watchface_edit_group[selected_zone].tips_BG != null &&
                        editable_elements.Watchface_edit_group[selected_zone].tips_BG.Length > 0)
                    {
                        int tips_x = editable_elements.Watchface_edit_group[selected_zone].tips_x;
                        int tips_y = editable_elements.Watchface_edit_group[selected_zone].tips_y;
                        int tips_width = editable_elements.Watchface_edit_group[selected_zone].tips_width;
                        src = OpenFileStream(editable_elements.Watchface_edit_group[selected_zone].tips_BG);
                        int width = tips_width;
                        int height = src.Height;
                        if (width > 1 && height > 1)
                        {
                            Rectangle cropRect = new Rectangle(0, 0, width, height);
                            Bitmap tempBitmap = new Bitmap(width, height);
                            using (Graphics g = Graphics.FromImage(tempBitmap))
                            {
                                g.DrawImage(src, new Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                                                 cropRect, GraphicsUnit.Pixel);
                            }
                            src = tempBitmap;
                        }
                        gPanel.DrawImage(src, tips_x, tips_y);

                        int selected_element = editable_elements.Watchface_edit_group[selected_zone].selected_element;
                        if (selected_element >= 0 && editable_elements.Watchface_edit_group[selected_zone].optional_types_list != null &&
                            selected_element < editable_elements.Watchface_edit_group[selected_zone].optional_types_list.Count)
                        {
                            int margin = editable_elements.Watchface_edit_group[selected_zone].tips_margin;
                            int h = src.Height;
                            int w = tips_width - margin;
                            //int w = src.Width - 10;
                            int x = tips_x + margin / 2;
                            int y = tips_y;

                            int size = 19;
                            int space_h = 0;
                            int space_v = 0;

                            Color color = Color.Black;
                            string align_h = "CENTER_H";
                            string align_v = "CENTER_V";
                            string text_style = "ELLIPSIS";
                            string valueStr = "";
                            string type = editable_elements.Watchface_edit_group[selected_zone].optional_types_list[selected_element].type;
                            switch (type)
                            {
                                case "DATE":
                                    valueStr = Properties.ElementsString.TypeNameDate;
                                    break;
                                case "WEEK":
                                    valueStr = Properties.ElementsString.TypeNameWeek;
                                    break;
                                case "BATTERY":
                                    valueStr = Properties.ElementsString.TypeNameBattery;
                                    break;
                                case "STEP":
                                    valueStr = Properties.ElementsString.TypeNameStep;
                                    break;
                                case "CAL":
                                    valueStr = Properties.ElementsString.TypeNameCalories;
                                    break;
                                case "HEART":
                                    valueStr = Properties.ElementsString.TypeNameHeart;
                                    break;
                                case "PAI":
                                    valueStr = Properties.ElementsString.TypeNamePAI;
                                    break;
                                case "DISTANCE":
                                    valueStr = Properties.ElementsString.TypeNameDistance;
                                    break;
                                case "STAND":
                                    valueStr = Properties.ElementsString.TypeNameStand;
                                    break;
                                case "STRESS":
                                    valueStr = Properties.ElementsString.TypeNameStress;
                                    break;
                                case "FAT_BURNING":
                                    valueStr = Properties.ElementsString.TypeNameFatBurning;
                                    break;
                                case "SPO2":
                                    valueStr = Properties.ElementsString.TypeNameSpO2;
                                    break;

                                case "TEMPERATURE":
                                    valueStr = Properties.ElementsString.TypeNameTemperature;
                                    break;
                                case "WEATHER":
                                    valueStr = Properties.ElementsString.TypeNameWeather;
                                    break;
                                case "UVI":
                                    valueStr = Properties.ElementsString.TypeNameUVI;
                                    break;
                                case "HUMIDITY":
                                    valueStr = Properties.ElementsString.TypeNameHumidity;
                                    break;
                                case "ALTIMETER":
                                    valueStr = Properties.ElementsString.TypeNameAltimeter;
                                    break;
                                case "SUN":
                                    valueStr = Properties.ElementsString.TypeNameSunrise;
                                    break;
                                case "WIND":
                                    valueStr = Properties.ElementsString.TypeNameWind;
                                    break;
                                case "MOON":
                                    valueStr = Properties.ElementsString.TypeNameMoon;
                                    break;

                                default:
                                    valueStr = "Error";
                                    break;
                            }

                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, 255, valueStr,
                            align_h, align_v, text_style, false); 
                        }
                    } 
                }
            }

            if (edit_mode == 3 && editable_pointers != null && editable_pointers.visible)
            {
                if (editable_pointers.fg != null && editable_pointers.fg.Length > 0)
                {
                    src = OpenFileStream(editable_pointers.fg);
                    gPanel.DrawImage(src, 0, 0);
                }
                if (editable_pointers.tips_bg != null && editable_pointers.tips_bg.Length > 0)
                {
                    int tips_x = editable_pointers.tips_x;
                    int tips_y = editable_pointers.tips_y;
                    src = OpenFileStream(editable_pointers.tips_bg);
                    gPanel.DrawImage(src, tips_x, tips_y);
                    int index = editable_pointers.selected_pointers;

                    int x = tips_x + 5;
                    int y = tips_y;
                    int h = src.Height;
                    int w = src.Width - 10;

                    int size = 19;
                    int space_h = 0;
                    int space_v = 0;

                    Color color = Color.Black;
                    string align_h = "CENTER_H";
                    string align_v = "CENTER_V";
                    string text_style = "ELLIPSIS";
                    string valueStr = Properties.FormStrings.Tip_Pointer + (index + 1).ToString() +
                        "/" + editable_pointers.config.Count.ToString();
                    //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2")
                    if (SelectedModel.versionOS >= 1.5) valueStr = Properties.FormStrings.Tip_Pointer.TrimEnd();

                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, 255, valueStr,
                        align_h, align_v, text_style, false);
                }
            }

            #region Mesh
            Logger.WriteLine("PreviewToBitmap (Mesh)");

            if (WMesh)
            {
                Pen pen = new Pen(Color.White, 2);
                int LineDistance = 30;
                if (scale >= 1) LineDistance = 15;
                if (scale >= 2) LineDistance = 10;
                if (scale >= 2) pen.Width = 1;
                //if (panel_Preview.Height > 300) LineDistance = 15;
                //if (panel_Preview.Height > 690) LineDistance = 10;
                for (int i = 0; i < 30; i++)
                {
                    gPanel.DrawLine(pen, new Point(offSet_X + i * LineDistance, 0), new Point(offSet_X + i * LineDistance, 480));
                    gPanel.DrawLine(pen, new Point(offSet_X - i * LineDistance, 0), new Point(offSet_X - i * LineDistance, 480));

                    gPanel.DrawLine(pen, new Point(0, offSet_Y + i * LineDistance), new Point(480, offSet_Y + i * LineDistance));
                    gPanel.DrawLine(pen, new Point(0, offSet_Y - i * LineDistance), new Point(480, offSet_Y - i * LineDistance));

                    if (i == 0) pen.Width = pen.Width / 3f;
                }
            }

            if (BMesh)
            {
                Pen pen = new Pen(Color.Black, 2);
                int LineDistance = 30;
                if (scale >= 1) LineDistance = 15;
                if (scale >= 2) LineDistance = 10;
                if (scale >= 2) pen.Width = 1;
                //if (panel_Preview.Height > 300) LineDistance = 15;
                //if (panel_Preview.Height > 690) LineDistance = 10;
                for (int i = 0; i < 30; i++)
                {
                    gPanel.DrawLine(pen, new Point(offSet_X + i * LineDistance, 0), new Point(offSet_X + i * LineDistance, 480));
                    gPanel.DrawLine(pen, new Point(offSet_X - i * LineDistance, 0), new Point(offSet_X - i * LineDistance, 480));

                    gPanel.DrawLine(pen, new Point(0, offSet_Y + i * LineDistance), new Point(480, offSet_Y + i * LineDistance));
                    gPanel.DrawLine(pen, new Point(0, offSet_Y - i * LineDistance), new Point(480, offSet_Y - i * LineDistance));

                    if (i == 0) pen.Width = pen.Width / 3f;
                }
            }
            #endregion

            if (crop)
            {
                Logger.WriteLine("Preview_edit_screen (crop)");
                /* Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                 switch (ProgramSettings.Watch_Model)
                 {
                     case "GTR 3 Pro":
                         mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                         break;
                     case "GTS 3":
                     case "GTS 4":
                         mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_3.png");
                         break;
                     case "GTR 4":
                         mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                         break;
                     case "Amazfit Band 7":
                         mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_band_7.png");
                         break;
                     case "GTS 4 mini":
                         mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                         break;
                     case "Falcon":
                     case "GTR mini":
                         mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_falcon.png");
                         break;
                 }*/
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\" + SelectedModel.maskImage);
                mask = FormColor(mask);
                gPanel.DrawImage(mask, 0, 0);
                //gPanel.DrawImage(mask, new Rectangle(0, 0, mask.Width, mask.Height));
                mask.Dispose();
            }

            src.Dispose();
        }

        /// <summary>создаем предпросмотр для редактируемых стрелок</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="scale">Масштаб прорисовки</param>
        /// <param name="crop">Обрезать по форме экрана</param>
        public void Creat_preview_editable_pointers(Graphics gPanel, float scale, bool crop)
        {
            Bitmap src = new Bitmap(1, 1);
            gPanel.ScaleTransform(scale, scale, MatrixOrder.Prepend);
            //gPanel.SmoothingMode = SmoothingMode.AntiAlias;
            //if (link == 2) goto AnimationEnd;

            #region EditablePointers
            if (Watch_Face != null && Watch_Face.ElementEditablePointers != null &&
                Watch_Face.ElementEditablePointers.visible)
            {
                ElementEditablePointers EditablePointers = Watch_Face.ElementEditablePointers;

                if (EditablePointers.config != null && EditablePointers.config.Count > 0 &&
                    EditablePointers.selected_pointers >= 0 &&
                    EditablePointers.selected_pointers < EditablePointers.config.Count)
                {
                    PointersList pointers_list = EditablePointers.config[EditablePointers.selected_pointers];

                    if (pointers_list.hour != null && pointers_list.hour.path != null
                            && pointers_list.hour.path.Length > 0)
                    {
                        int x = pointers_list.hour.centerX;
                        int y = pointers_list.hour.centerY;
                        int offsetX = pointers_list.hour.posX;
                        int offsetY = pointers_list.hour.posY;
                        int image_index = ListImages.IndexOf(pointers_list.hour.path);
                        int hour = WatchFacePreviewSet.Time.Hours;
                        int min = WatchFacePreviewSet.Time.Minutes;
                        //int sec = Watch_Face_Preview_Set.TimeW.Seconds;
                        if (hour >= 12) hour = hour - 12;
                        float angle = 360 * hour / 12 + 360 * min / (60 * 12);
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, false);
                    }

                    if (pointers_list.minute != null && pointers_list.minute.path != null
                        && pointers_list.minute.path.Length > 0)
                    {
                        int x = pointers_list.minute.centerX;
                        int y = pointers_list.minute.centerY;
                        int offsetX = pointers_list.minute.posX;
                        int offsetY = pointers_list.minute.posY;
                        int image_index = ListImages.IndexOf(pointers_list.minute.path);
                        int min = WatchFacePreviewSet.Time.Minutes;
                        float angle = 360 * min / 60;
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, false);
                    }

                    if (pointers_list.second != null && pointers_list.second.path != null
                        && pointers_list.second.path.Length > 0 &&
                        (radioButton_ScreenNormal.Checked || EditablePointers.AOD_show))
                    {
                        int x = pointers_list.second.centerX;
                        int y = pointers_list.second.centerY;
                        int offsetX = pointers_list.second.posX;
                        int offsetY = pointers_list.second.posY;
                        int image_index = ListImages.IndexOf(pointers_list.second.path);
                        int sec = WatchFacePreviewSet.Time.Seconds;
                        float angle = 360 * sec / 60;
                        DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, false);
                    }
                }


            }
            #endregion

            if (crop)
            {
                Logger.WriteLine("PreviewToBitmap (crop)");
                /*Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        break;
                    case "GTR 4":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        break;
                    case "Amazfit Band 7":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_band_7.png");
                        break;
                    case "GTS 4 mini":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        break;
                    case "Falcon":
                    case "GTR mini":
                        mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }*/
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\" + SelectedModel.maskImage);
                mask = FormColor(mask);
                gPanel.DrawImage(mask, 0, 0);
                //gPanel.DrawImage(mask, new Rectangle(0, 0, mask.Width, mask.Height));
                mask.Dispose();
            }
        }

        /// <summary>Рисуем все параметры элемента</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="images">Параметры для изображения</param>
        /// <param name="segments">Параметры для сегментов</param>
        /// <param name="number">Параметры цифрового значения</param>
        /// <param name="number_font">Параметры отображения данных шрифтом</param>
        /// <param name="text_rotation">Параметры текста под углом</param>
        /// <param name="text_circle">Параметры текста по окружности</param>
        /// <param name="numberTarget">Параметры цифрового значения цели</param>
        /// <param name="numberTarget_font">Параметры отображения цели шрифтом</param>
        /// <param name="text_rotationTarget">Параметры текста цели под углом</param>
        /// <param name="text_circleTarget">Параметры текста цели по окружности</param>
        /// <param name="pointer">Параметры для стрелочного указателя</param>
        /// <param name="circleScale">Параметры для круговой шкалы</param>
        /// <param name="linearScale">Параметры для линейной шкалы</param>
        /// <param name="icon">Параметры для иконки</param>
        /// <param name="value">Значение показателя</param>
        /// <param name="value_lenght">Максимальная длина для отображения значения</param>
        /// <param name="goal">Значение цели для показателя</param>
        /// <param name="progress">Прогресс показателя</param>
        /// <param name="valueImgIndex">Позиция картинки из заданного массива для отображения показателя картинками</param>
        /// <param name="valueSegmentIndex">Позиция картинки из заданного массива для отображения показателя сегментами</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showProgressArea">Подсвечивать круговую шкалу при наличии фонового изображения</param>
        /// <param name="showCentrHend">Подсвечивать центр стрелки</param>
        /// <param name="elementName">Имя отображаемого элемента</param>
        /// <param name="ActivityGoal_Calories">Для активности отображаем шаги ли калории</param>
        private void DrawActivity(Graphics gPanel, hmUI_widget_IMG_LEVEL images, hmUI_widget_IMG_PROGRESS segments,
            hmUI_widget_IMG_NUMBER number, hmUI_widget_TEXT number_font, hmUI_widget_IMG_NUMBER text_rotation, Text_Circle text_circle, hmUI_widget_IMG_NUMBER numberTarget,
            hmUI_widget_TEXT numberTarget_font, hmUI_widget_IMG_NUMBER text_rotationTarget, Text_Circle text_circleTarget, hmUI_widget_IMG_POINTER pointer, Circle_Scale circleScale, Linear_Scale linearScale,
            hmUI_widget_IMG icon, float value, int value_lenght, int goal, float progress,
            int valueImgIndex, int valueSegmentIndex, bool BBorder, bool showProgressArea, bool showCentrHend, string elementName,
            bool ActivityGoal_Calories = false)
        {
            if (progress < 0) progress = 0;
            if (progress > 1) progress = 1;
            Bitmap src = new Bitmap(1, 1);
            string unit = "";
            switch (elementName)
            {
                case "ElementSteps":
                    unit = "steps";
                    break;
                case "ElementBattery":
                    unit = "%";
                    break;
                case "ElementCalories":
                    unit = "kcal";
                    break;
                case "ElementHeart":
                    unit = "bpm";
                    break;
                case "ElementPAI":
                    unit = "pai";
                    break;
                case "ElementDistance":
                    unit = "km";
                    break;
                case "ElementSpO2":
                    unit = "%";
                    break;
                case "ElementFatBurning":
                    unit = "min";
                    break;
                case "ElementHumidity":
                    unit = "%";
                    break;
                case "ElementAltimeter":
                    unit = "hpa";
                    break;
                case "ElementCompass":
                    unit = "°";
                    break;
            }

            for (int index = 1; index <= 25; index++)
            {
                if (images != null && images.img_First != null && images.img_First.Length > 0 &&
                    index == images.position && images.visible)
                {
                    if (valueImgIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(images.img_First);
                        int x = images.X;
                        int y = images.Y;
                        int width = 0;
                        int height = 0;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        width = src.Width;
                        height = src.Height;

                        imageIndex = imageIndex + valueImgIndex;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else if (width > 0 && height > 0) 
                            {
                                Rectangle cropRect = new Rectangle(0, 0, width, height);
                                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                                using (Graphics g = Graphics.FromImage(target))
                                {
                                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                                     cropRect, GraphicsUnit.Pixel);
                                }
                                gPanel.DrawImage(target, x, y);
                            }
                            else
                            {
                                gPanel.DrawImage(src, x, y);

                            }
                        } 
                    }
                }

                if (segments != null && segments.img_First != null && segments.img_First.Length > 0 &&
                    index == segments.position && segments.visible)
                {
                    if (valueSegmentIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(segments.img_First);
                        for (int i = 0; i <= valueSegmentIndex; i++)
                        {
                            int imgIndex = imageIndex + i;

                            if (imgIndex < ListImagesFullName.Count && i < segments.X.Count)
                            {
                                int x = segments.X[i];
                                int y = segments.Y[i];
                                src = OpenFileStream(ListImagesFullName[imgIndex]);
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            } 
                        }
                    }
                }

                if (number != null && number.img_First != null && number.img_First.Length > 0 &&
                    index == number.position && number.visible)
                {
                    int imageIndex = ListImages.IndexOf(number.img_First);
                    int x = number.imageX;
                    int y = number.imageY;
                    int spasing = number.space;
                    int angle = number.angle;
                    int alignment = AlignmentToInt(number.align);
                    bool addZero = number.zero;
                    int alpha = number.alpha;
                    int separator_index = -1;
                    if (number.unit != null && number.unit.Length > 0)
                        separator_index = ListImages.IndexOf(number.unit);

                    Draw_dagital_text(gPanel, imageIndex, x, y,
                        spasing, alignment, (int)value, alpha, addZero, value_lenght, separator_index, angle, BBorder, elementName);

                    if (number.icon != null && number.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(number.icon);
                        x = number.iconPosX;
                        y = number.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                    }
                }

                if (number_font != null && index == number_font.position && number_font.visible)
                {
                    int x = number_font.x;
                    int y = number_font.y;
                    int h = number_font.h;
                    int w = number_font.w;

                    int size = number_font.text_size;
                    int space_h = number_font.char_space;
                    int space_v = number_font.line_space;

                    Color color = StringToColor(number_font.color);
                    int alpha = number_font.alpha;
                    string align_h = number_font.align_h;
                    string align_v = number_font.align_v;
                    string text_style = number_font.text_style;
                    string valueStr = value.ToString();
                    string unitStr = unit;
                    if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                    if (number_font.unit_type > 0)
                    {
                        if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        if (number_font.unit_type == 2 && elementName == "ElementCompass")
                        {
                            switch (valueImgIndex)
                            {
                                case 0:
                                    unitStr += " N";
                                    break;
                                case 1:
                                    unitStr += " NE";
                                    break;
                                case 2:
                                    unitStr += " E";
                                    break;
                                case 3:
                                    unitStr += " SE";
                                    break;
                                case 4:
                                    unitStr += " W";
                                    break;
                                case 5:
                                    unitStr += " SW";
                                    break;
                                case 6:
                                    unitStr += " W";
                                    break;
                                case 7:
                                    unitStr += " NW";
                                    break;
                            }
                        }
                        valueStr += unitStr; 
                    }

                    if (number_font.centreHorizontally) 
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (number_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                    {
                        string font_fileName = FontsList[number_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (text_rotation != null && text_rotation.img_First != null && text_rotation.img_First.Length > 0 &&
                    index == text_rotation.position && text_rotation.visible)
                {
                    int pos_x = text_rotation.imageX;
                    int pos_y = text_rotation.imageY;
                    int spacing = text_rotation.space;
                    float angle = text_rotation.angle;
                    bool addZero = text_rotation.zero;
                    int image_index = ListImages.IndexOf(text_rotation.img_First);
                    int unit_index = ListImages.IndexOf(text_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(text_rotation.dot_image);
                    string horizontal_alignment = text_rotation.align;
                    bool unit_in_alignment = text_rotation.unit_in_alignment;

                    string valueStr = ((int)value).ToString();
                    if (text_rotation.zero) valueStr = valueStr.PadLeft(value_lenght, '0');

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        valueStr, value_lenght, BBorder, -1, -1, false, elementName);
                }

                if (text_circle != null && text_circle.img_First != null && text_circle.img_First.Length > 0 &&
                    index == text_circle.position && text_circle.visible)
                {
                    int centr_x = text_circle.circle_center_X;
                    int centr_y = text_circle.circle_center_Y;
                    int radius = text_circle.radius;
                    int spacing = text_circle.char_space_angle;
                    float angle = text_circle.angle;
                    bool addZero = text_circle.zero;
                    int image_index = ListImages.IndexOf(text_circle.img_First);
                    int unit_index = ListImages.IndexOf(text_circle.unit);
                    int dot_image_index = ListImages.IndexOf(text_circle.dot_image);
                    string vertical_alignment = text_circle.vertical_alignment;
                    string horizontal_alignment = text_circle.horizontal_alignment;
                    bool reverse_direction = text_circle.reverse_direction;
                    bool unit_in_alignment = text_circle.unit_in_alignment;

                    string valueStr = ((int)value).ToString();
                    if (text_circle.zero) valueStr = valueStr.PadLeft(value_lenght, '0');

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        valueStr, value_lenght, BBorder, showCentrHend, -1, -1, false, elementName);
                }

                if (numberTarget != null && numberTarget.img_First != null && numberTarget.img_First.Length > 0 &&
                    index == numberTarget.position && numberTarget.visible)
                {
                    int imageIndex = ListImages.IndexOf(numberTarget.img_First);
                    int x = numberTarget.imageX;
                    int y = numberTarget.imageY;
                    int spasing = numberTarget.space;
                    int angle = numberTarget.angle;
                    int alpha = numberTarget.alpha;
                    int alignment = AlignmentToInt(numberTarget.align);
                    bool addZero = numberTarget.zero;
                    int separator_index = -1;
                    if (numberTarget.unit != null && numberTarget.unit.Length > 0)
                        separator_index = ListImages.IndexOf(numberTarget.unit);

                    if (elementName != "ElementAltimeter") Draw_dagital_text(gPanel, imageIndex, x, y,
                        spasing, alignment, (int)goal, alpha, addZero, value_lenght, separator_index, angle, BBorder);
                    else Draw_dagital_text(gPanel, imageIndex, x, y,
                        spasing, alignment, (int)goal, alpha, addZero, 5, separator_index, angle, BBorder);

                    if (numberTarget.icon != null && numberTarget.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(numberTarget.icon);
                        x = numberTarget.iconPosX;
                        y = numberTarget.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

                if (numberTarget_font != null && index == numberTarget_font.position && numberTarget_font.visible)
                {
                    int x = numberTarget_font.x;
                    int y = numberTarget_font.y;
                    int h = numberTarget_font.h;
                    int w = numberTarget_font.w;

                    int size = numberTarget_font.text_size;
                    int space_h = numberTarget_font.char_space;
                    int space_v = numberTarget_font.line_space;

                    Color color = StringToColor(numberTarget_font.color);
                    int alpha = numberTarget_font.alpha;
                    string align_h = numberTarget_font.align_h;
                    string align_v = numberTarget_font.align_v;
                    string text_style = numberTarget_font.text_style;
                    string valueStr = goal.ToString();
                    string unitStr = unit;
                    if (elementName == "ElementAltimeter") unitStr = "meter";
                    if (numberTarget_font.padding)
                    {
                        if (elementName == "ElementAltimeter")
                        {
                            if (goal >= 0) valueStr = valueStr.PadLeft(5, '0');
                            else
                            {
                                int tempGoal = Math.Abs(goal);
                                valueStr = tempGoal.ToString();
                                valueStr = valueStr.PadLeft(4, '0');
                                valueStr = "-" + valueStr;
                            }
                        }
                        else valueStr = valueStr.PadLeft(value_lenght, '0'); 
                    }
                    if (numberTarget_font.unit_type > 0)
                    {
                        if (numberTarget_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }

                    if (numberTarget_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (numberTarget_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (numberTarget_font.font != null && numberTarget_font.font.Length > 3 && FontsList.ContainsKey(numberTarget_font.font))
                    {
                        string font_fileName = FontsList[numberTarget_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + numberAltitude_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (text_rotationTarget != null && text_rotationTarget.img_First != null && text_rotationTarget.img_First.Length > 0 &&
                    index == text_rotationTarget.position && text_rotationTarget.visible)
                {
                    int pos_x = text_rotationTarget.imageX;
                    int pos_y = text_rotationTarget.imageY;
                    int spacing = text_rotationTarget.space;
                    float angle = text_rotationTarget.angle;
                    bool addZero = text_rotationTarget.zero;
                    int image_index = ListImages.IndexOf(text_rotationTarget.img_First);
                    int unit_index = ListImages.IndexOf(text_rotationTarget.unit);
                    int dot_image_index = ListImages.IndexOf(text_rotationTarget.dot_image);
                    string horizontal_alignment = text_rotationTarget.align;
                    bool unit_in_alignment = text_rotationTarget.unit_in_alignment;

                    string valueStr = goal.ToString();
                    if (text_rotationTarget.zero) valueStr = valueStr.PadLeft(value_lenght, '0');

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        valueStr, value_lenght, BBorder, -1, -1, false, elementName);
                }

                if (text_circleTarget != null && text_circleTarget.img_First != null && text_circleTarget.img_First.Length > 0 &&
                    index == text_circleTarget.position && text_circleTarget.visible)
                {
                    int centr_x = text_circleTarget.circle_center_X;
                    int centr_y = text_circleTarget.circle_center_Y;
                    int radius = text_circleTarget.radius;
                    int spacing = text_circleTarget.char_space_angle;
                    float angle = text_circleTarget.angle;
                    bool addZero = text_circleTarget.zero;
                    int image_index = ListImages.IndexOf(text_circleTarget.img_First);
                    int unit_index = ListImages.IndexOf(text_circleTarget.unit);
                    int dot_image_index = ListImages.IndexOf(text_circleTarget.dot_image);
                    string vertical_alignment = text_circleTarget.vertical_alignment;
                    string horizontal_alignment = text_circleTarget.horizontal_alignment;
                    bool reverse_direction = text_circleTarget.reverse_direction;
                    bool unit_in_alignment = text_circleTarget.unit_in_alignment;

                    string valueStr = goal.ToString();
                    if (text_circleTarget.zero) valueStr = valueStr.PadLeft(value_lenght, '0');

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        valueStr, value_lenght, BBorder, showCentrHend, -1, -1, false, elementName);
                }

                if (pointer != null && pointer.src != null && pointer.src.Length > 0 &&
                    index == pointer.position && pointer.visible)
                {
                    int x = pointer.center_x;
                    int y = pointer.center_y;
                    int offsetX = pointer.pos_x;
                    int offsetY = pointer.pos_y;
                    int startAngle = pointer.start_angle;
                    int endAngle = pointer.end_angle;
                    int image_index = ListImages.IndexOf(pointer.src);
                    float progressHeart = 57 / 360f;
                    if (value == 0) progressHeart = 0;
                    if (value >= 90 && value < 108) progressHeart = 118 / 360f;
                    if (value >= 108 && value < 126) progressHeart = 180 / 360f;
                    if (value >= 126 && value < 144) progressHeart = 237 / 360f;
                    if (value >= 144 && value < 162) progressHeart = 298 / 360f;
                    if (value >= 162) progressHeart = 359 / 360f;

                    float angle = startAngle + progress * (endAngle - startAngle);

                    if (pointer.scale != null && pointer.scale.Length > 0)
                    {
                        int image_index_scale = ListImages.IndexOf(pointer.scale);
                        int x_scale = pointer.scale_x;
                        int y_scale = pointer.scale_y;

                        src = OpenFileStream(ListImagesFullName[image_index_scale]);
                        gPanel.DrawImage(src, x_scale, y_scale);
                    }

                    if (elementName== "ElementHeart") angle = startAngle + progressHeart * (endAngle - startAngle);
                    DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                    if (pointer.cover_path != null && pointer.cover_path.Length > 0)
                    {
                        image_index = ListImages.IndexOf(pointer.cover_path);
                        x = pointer.cover_x;
                        y = pointer.cover_y;

                        src = OpenFileStream(ListImagesFullName[image_index]);
                        gPanel.DrawImage(src, x, y);
                    }
                }

                if (circleScale != null && index == circleScale.position && circleScale.visible)
                {
                    int x = circleScale.center_x;
                    int y = circleScale.center_y;
                    int radius = circleScale.radius;
                    int width = circleScale.line_width;
                    int startAngle = circleScale.start_angle;
                    int endAngle = circleScale.end_angle;
                    bool mirror = circleScale.mirror;
                    bool inversion = circleScale.inversion;
                    Color color = StringToColor(circleScale.color);
                    float fullAngle = endAngle - startAngle;

                    string flatness = circleScale.line_cap;
                    int lineCap = 3;
                    if (inversion)
                    {
                        if (flatness == "Rounded") lineCap = 0;
                    }
                    else
                    {
                        if (flatness == "Rounded")
                        {
                            if (mirror) lineCap = 2;
                            else lineCap = 0;
                        } 
                    }

                    DrawScaleCircle(gPanel, x, y, radius, width, lineCap, startAngle, fullAngle, progress,
                        color, inversion, showProgressArea, showCentrHend);

                    if (mirror) DrawScaleCircle(gPanel, x, y, radius, width, lineCap, startAngle, -fullAngle, progress,
                         color, inversion, showProgressArea, showCentrHend);
                }

                if (linearScale != null && index == linearScale.position && linearScale.visible)
                {
                    int x = linearScale.start_x;
                    int y = linearScale.start_y;
                    int lenght = linearScale.lenght;
                    int width = linearScale.line_width;
                    string line_cap = linearScale.line_cap;
                    bool mirror = linearScale.mirror;
                    bool inversion = linearScale.inversion;
                    bool vertical = linearScale.vertical;
                    Color color = StringToColor(linearScale.color);
                    int pointer_index = -1;
                    if (linearScale.pointer != null && linearScale.pointer.Length > 0)
                        pointer_index = ListImages.IndexOf(linearScale.pointer);

                    DrawScaleLinear(gPanel, x, y, lenght, width, line_cap, pointer_index, vertical, progress,
                        color, inversion, showProgressArea);

                    if (mirror) DrawScaleLinear(gPanel, x, y, -lenght, width, line_cap, pointer_index, vertical, progress,
                        color, inversion, showProgressArea);
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, x, y);
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем дистанцию</summary>
        /// <param name="number">Параметры цифрового значения</param>
        /// <param name="number_font">Параметры отображения данных шрифтом</param>
        /// <param name="text_rotation">Параметры текста под углом</param>
        /// <param name="text_circle">Параметры текста по окружности</param>
        /// <param name="icon">Параметры для иконки</param>
        /// <param name="distance_value">Значение показателя</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showProgressArea">Подсвечивать круговую шкалу при наличии фонового изображения</param>
        /// <param name="showCentrHend">Подсвечивать центр стрелки</param>
        private void DrawDistance(Graphics gPanel, hmUI_widget_IMG_NUMBER number, hmUI_widget_TEXT number_font, hmUI_widget_IMG_NUMBER text_rotation,
            Text_Circle text_circle, hmUI_widget_IMG icon, float distance_value, bool BBorder, bool showCentrHend)
        {
            Bitmap src = new Bitmap(1, 1);
            string unit = "km";

            for (int index = 1; index <= 15; index++)
            {
                if (number != null && number.img_First != null && number.img_First.Length > 0 &&
                    index == number.position && number.visible)
                {
                    int value_lenght = 4;
                    int image_Index = ListImages.IndexOf(number.img_First);
                    int pos_x = number.imageX;
                    int pos_y = number.imageY;
                    int distance_spasing = number.space;
                    int angle = number.angle;
                    int alpha = number.alpha;
                    int distance_alignment = AlignmentToInt(number.align);
                    //bool distance_addZero = img_number.zero;
                    bool distance_addZero = false;
                    int distance_separator_index = -1;
                    if (number.unit != null && number.unit.Length > 0)
                        distance_separator_index = ListImages.IndexOf(number.unit);
                    int decumalPoint_index = -1;
                    if (number.dot_image != null && number.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(number.dot_image);

                    Draw_dagital_text_decimal(gPanel, image_Index, pos_x, pos_y,
                        distance_spasing, distance_alignment, distance_value, distance_addZero, value_lenght,
                        distance_separator_index, decumalPoint_index, 2, angle, alpha, BBorder);

                    if (number.icon != null && number.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(number.icon);
                        pos_x = number.iconPosX;
                        pos_y = number.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(pos_x, pos_y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, pos_x, pos_y);
                        //gPanel.DrawImage(src, pos_x, pos_y);
                    }
                }

                if (number_font != null && index == number_font.position && number_font.visible)
                {
                    int x = number_font.x;
                    int y = number_font.y;
                    int h = number_font.h;
                    int w = number_font.w;

                    int size = number_font.text_size;
                    int space_h = number_font.char_space;
                    int space_v = number_font.line_space;

                    Color color = StringToColor(number_font.color);
                    int alpha = number_font.alpha;
                    string align_h = number_font.align_h;
                    string align_v = number_font.align_v;
                    string text_style = number_font.text_style;
                    string valueStr = distance_value.ToString();

                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (valueStr.IndexOf(decimalSeparator) < 0) valueStr += decimalSeparator;
                    while (valueStr.IndexOf(decimalSeparator) > valueStr.Length - 2 - 1)
                    {
                        valueStr += "0";
                    }
                    string unitStr = unit;
                    if (number_font.padding) valueStr = valueStr.PadLeft(5, '0');
                    if (number_font.unit_type > 0)
                    {
                        if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }


                    if (number_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (number_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                    {
                        string font_fileName = FontsList[number_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (text_rotation != null && text_rotation.img_First != null && text_rotation.img_First.Length > 0 &&
                    text_rotation.dot_image != null && text_rotation.dot_image.Length > 0 && index == text_rotation.position && text_rotation.visible)
                {
                    int pos_x = text_rotation.imageX;
                    int pos_y = text_rotation.imageY;
                    int spacing = text_rotation.space;
                    float angle = text_rotation.angle;
                    bool addZero = text_rotation.zero;
                    int image_index = ListImages.IndexOf(text_rotation.img_First);
                    int unit_index = ListImages.IndexOf(text_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(text_rotation.dot_image);
                    string horizontal_alignment = text_rotation.align;
                    bool unit_in_alignment = text_rotation.unit_in_alignment;

                    string value = distance_value.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (text_rotation.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index,dot_image_index, horizontal_alignment, unit_in_alignment,
                        value, 4, BBorder, -1, -1, false, "ElementDistance");
                }

                if (text_circle != null && text_circle.img_First != null && text_circle.img_First.Length > 0 &&
                    text_circle.dot_image != null && text_circle.dot_image.Length > 0 && index == text_circle.position && text_circle.visible)
                {
                    int centr_x = text_circle.circle_center_X;
                    int centr_y = text_circle.circle_center_Y;
                    int radius = text_circle.radius;
                    int spacing = text_circle.char_space_angle;
                    float angle = text_circle.angle;
                    bool addZero = text_circle.zero;
                    int image_index = ListImages.IndexOf(text_circle.img_First);
                    int unit_index = ListImages.IndexOf(text_circle.unit);
                    int dot_image_index = ListImages.IndexOf(text_circle.dot_image);
                    string vertical_alignment = text_circle.vertical_alignment;
                    string horizontal_alignment = text_circle.horizontal_alignment;
                    bool reverse_direction = text_circle.reverse_direction;
                    bool unit_in_alignment = text_circle.unit_in_alignment;

                    string value = distance_value.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (text_circle.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        value, 4, BBorder, showCentrHend, -1, -1, false, "ElementDistance");
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем все параметры элемента погода</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="images">Параметры для изображения</param>
        /// <param name="number">Параметры для текущей температуры</param>
        /// <param name="number_font">Параметры отображения текущей температуры шрифтом</param>
        /// <param name="numberMin">Параметры для минимальной температуры</param>
        /// <param name="numberMin_font">Параметры отображения минимальной температуры шрифтом</param>
        /// <param name="numberMax">Параметры для максимальной температуры</param>
        /// <param name="numberMax_font">Параметры отображения максимальной температуры шрифтом</param>
        /// <param name="numberMinMax_font">Параметры отображения минимальной и максимальной температуры шрифтом</param>
        /// <param name="cityName">Параметры для названия города</param>
        /// <param name="icon">Параметры для иконки</param>
        /// <param name="value">Текущая температура</param>
        /// <param name="valueMin">Минимальная температура</param>
        /// <param name="valueMax">Максимальная температура</param>
        /// <param name="value_lenght">Максимальная длина для отображения значения</param>
        /// <param name="icon_index">Номер иконки погоды</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showTemperature">Показывать температуру</param>
        private void DrawWeather(Graphics gPanel, hmUI_widget_IMG_LEVEL images, hmUI_widget_IMG_NUMBER number, hmUI_widget_TEXT number_font, 
            hmUI_widget_IMG_NUMBER numberMin, hmUI_widget_TEXT numberMin_font, hmUI_widget_IMG_NUMBER textMin_rotation, Text_Circle textMin_circle, 
            hmUI_widget_IMG_NUMBER numberMax, hmUI_widget_TEXT numberMax_font, hmUI_widget_IMG_NUMBER textMax_rotation, Text_Circle textMax_circle,
            hmUI_widget_TEXT numberMinMax_font, hmUI_widget_TEXT cityName, hmUI_widget_IMG icon, int value, int valueMin, int valueMax, int value_lenght,
            int icon_index, bool BBorder, bool showTemperature, bool showCentrHend)
        {
            Bitmap src = new Bitmap(1, 1);
            string unit = "°";

            for (int index = 1; index <= 25; index++)
            {
                if (images != null && images.img_First != null && images.img_First.Length > 0 &&
                    index == images.position && images.visible)
                {
                    if (icon_index >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(images.img_First);
                        int x = images.X;
                        int y = images.Y;
                        imageIndex = imageIndex + icon_index;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                            //gPanel.DrawImage(src, x, y);
                        }
                    }
                }

                if (number != null && number.img_First != null && number.img_First.Length > 0 &&
                    index == number.position && number.visible)
                {
                    int imageIndex = ListImages.IndexOf(number.img_First);
                    int x = number.imageX;
                    int y = number.imageY;
                    int spasing = number.space;
                    int alignment = AlignmentToInt(number.align);
                    bool addZero = false;
                    int angle = number.angle;
                    int separator_index = -1;
                    if (number.unit != null && number.unit.Length > 0)
                        separator_index = ListImages.IndexOf(number.unit);
                    int imageError_index = -1;
                    if (number.invalid_image != null && number.invalid_image.Length > 0)
                        imageError_index = ListImages.IndexOf(number.invalid_image);
                    int imageMinus_index = -1;
                    if (number.negative_image != null && number.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(number.negative_image);

                    if (showTemperature)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, value, 3, addZero, 
                            imageMinus_index, separator_index, angle, BBorder, -1, false);
                    }
                    else if (imageError_index >= 0)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y,
                                        spasing, alignment, value, 3, addZero, imageMinus_index, separator_index, angle,
                                        BBorder, imageError_index, true);
                    }

                    if (number.icon != null && number.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(number.icon);
                        x = number.iconPosX;
                        y = number.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                    }
                }

                if (number_font != null && index == number_font.position && number_font.visible)
                {
                    int x = number_font.x;
                    int y = number_font.y;
                    int h = number_font.h;
                    int w = number_font.w;

                    int size = number_font.text_size;
                    int space_h = number_font.char_space;
                    int space_v = number_font.line_space;

                    Color color = StringToColor(number_font.color);
                    int alpha = number_font.alpha;
                    string align_h = number_font.align_h;
                    string align_v = number_font.align_v;
                    string text_style = number_font.text_style;
                    string valueStr = value.ToString();
                    string unitStr = unit;
                    //if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                    if (number_font.unit_type > 0)
                    {
                        if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }
                    if (!showTemperature) valueStr = "--";

                    if (number_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (number_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                    {
                        string font_fileName = FontsList[number_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (numberMin != null && numberMin.img_First != null && numberMin.img_First.Length > 0 &&
                    index == numberMin.position && numberMin.visible)
                {
                    int imageIndex = ListImages.IndexOf(numberMin.img_First);
                    int x = numberMin.imageX;
                    int y = numberMin.imageY;
                    int spasing = numberMin.space;
                    int alignment = AlignmentToInt(numberMin.align);
                    bool addZero = false;
                    int angle = numberMin.angle;
                    int separator_index = -1;
                    if (numberMin.unit != null && numberMin.unit.Length > 0)
                        separator_index = ListImages.IndexOf(numberMin.unit);
                    int imageError_index = -1;
                    if (numberMin.invalid_image != null && numberMin.invalid_image.Length > 0)
                        imageError_index = ListImages.IndexOf(numberMin.invalid_image);
                    int imageMinus_index = -1;
                    if (numberMin.negative_image != null && numberMin.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(numberMin.negative_image);

                    if (showTemperature)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, valueMin, 3, addZero,
                            imageMinus_index, separator_index, angle, BBorder, -1, false);
                    }
                    else if (imageError_index >= 0)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y,
                                        spasing, alignment, valueMin, 3, addZero, imageMinus_index, separator_index, angle,
                                        BBorder, imageError_index, true);
                    }

                    if (numberMin.icon != null && numberMin.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(numberMin.icon);
                        x = numberMin.iconPosX;
                        y = numberMin.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                    }
                }

                if (numberMin_font != null && index == numberMin_font.position && numberMin_font.visible)
                {
                    int x = numberMin_font.x;
                    int y = numberMin_font.y;
                    int h = numberMin_font.h;
                    int w = numberMin_font.w;

                    int size = numberMin_font.text_size;
                    int space_h = numberMin_font.char_space;
                    int space_v = numberMin_font.line_space;

                    Color color = StringToColor(numberMin_font.color);
                    int alpha = numberMin_font.alpha;
                    string align_h = numberMin_font.align_h;
                    string align_v = numberMin_font.align_v;
                    string text_style = numberMin_font.text_style;
                    string valueStr = valueMin.ToString();
                    string unitStr = unit;
                    //if (numberMin_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                    if (numberMin_font.unit_type > 0)
                    {
                        if (numberMin_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }
                    if (!showTemperature) valueStr = "--";

                    if (numberMin_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (numberMin_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (numberMin_font.font != null && numberMin_font.font.Length > 3 && FontsList.ContainsKey(numberMin_font.font))
                    {
                        string font_fileName = FontsList[numberMin_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + numberMin_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (textMin_rotation != null && textMin_rotation.img_First != null && textMin_rotation.img_First.Length > 0 &&
                    textMin_rotation.dot_image != null && textMin_rotation.dot_image.Length > 0 && index == textMin_rotation.position && textMin_rotation.visible)
                {
                    int pos_x = textMin_rotation.imageX;
                    int pos_y = textMin_rotation.imageY;
                    int spacing = textMin_rotation.space;
                    float angle = textMin_rotation.angle;
                    bool addZero = textMin_rotation.zero;
                    int image_index = ListImages.IndexOf(textMin_rotation.img_First);
                    int unit_index = ListImages.IndexOf(textMin_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(textMin_rotation.dot_image);
                    string horizontal_alignment = textMin_rotation.align;
                    bool unit_in_alignment = textMin_rotation.unit_in_alignment;

                    int error_index = -1;
                    if (!showTemperature) error_index = ListImages.IndexOf(textMin_rotation.invalid_image);

                    string valueStr = valueMin.ToString();
                    string symbolMinus = "-";
                    if (textMin_rotation.zero) 
                    { 
                        valueStr = valueStr.PadLeft(3, '0'); 
                        if (valueMin < 0) valueStr = symbolMinus +  Math.Abs(valueMin).ToString().PadLeft(3, '0');
                    }

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        valueStr, 3, BBorder, -1, error_index, !showTemperature, "ElementWeather");
                }

                if (textMin_circle != null && textMin_circle.img_First != null && textMin_circle.img_First.Length > 0 &&
                    textMin_circle.dot_image != null && textMin_circle.dot_image.Length > 0 && index == textMin_circle.position && textMin_circle.visible)
                {
                    int centr_x = textMin_circle.circle_center_X;
                    int centr_y = textMin_circle.circle_center_Y;
                    int radius = textMin_circle.radius;
                    int spacing = textMin_circle.char_space_angle;
                    float angle = textMin_circle.angle;
                    bool addZero = textMin_circle.zero;
                    int image_index = ListImages.IndexOf(textMin_circle.img_First);
                    int unit_index = ListImages.IndexOf(textMin_circle.unit);
                    int dot_image_index = ListImages.IndexOf(textMin_circle.dot_image);
                    string vertical_alignment = textMin_circle.vertical_alignment;
                    string horizontal_alignment = textMin_circle.horizontal_alignment;
                    bool reverse_direction = textMin_circle.reverse_direction;
                    bool unit_in_alignment = textMin_circle.unit_in_alignment;

                    string valueStr = valueMin.ToString();
                    string symbolMinus = "-";

                    int error_index = -1;
                    if (!showTemperature) error_index = ListImages.IndexOf(textMin_circle.error_image);

                    if (textMin_circle.zero)
                    {
                        valueStr = valueStr.PadLeft(3, '0');
                        if (valueMin < 0) valueStr = symbolMinus + Math.Abs(valueMin).ToString().PadLeft(3, '0');
                    }

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        valueStr, 3, BBorder, showCentrHend, -1, error_index, !showTemperature, "ElementWeather");
                }

                if (numberMax != null && numberMax.img_First != null && numberMax.img_First.Length > 0 &&
                    index == numberMax.position && numberMax.visible)
                {
                    int imageIndex = ListImages.IndexOf(numberMax.img_First);
                    int x = numberMax.imageX;
                    int y = numberMax.imageY;
                    int spasing = numberMax.space;
                    int alignment = AlignmentToInt(numberMax.align);
                    bool addZero = false;
                    int angle = numberMax.angle;
                    int separator_index = -1;
                    if (numberMax.unit != null && numberMax.unit.Length > 0)
                        separator_index = ListImages.IndexOf(numberMax.unit);
                    int imageError_index = -1;
                    if (numberMax.invalid_image != null && numberMax.invalid_image.Length > 0)
                        imageError_index = ListImages.IndexOf(numberMax.invalid_image);
                    int imageMinus_index = -1;
                    if (numberMax.negative_image != null && numberMax.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(numberMax.negative_image);

                    if (showTemperature)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, valueMax, 3, addZero,
                            imageMinus_index, separator_index, angle, BBorder, -1, false);
                    }
                    else if (imageError_index >= 0)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y,
                                        spasing, alignment, valueMax, 3, addZero, imageMinus_index, separator_index, angle,
                                        BBorder, imageError_index, true);
                    }

                    if (numberMax.icon != null && numberMax.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(numberMax.icon);
                        x = numberMax.iconPosX;
                        y = numberMax.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                    }
                }

                if (numberMax_font != null && index == numberMax_font.position && numberMax_font.visible)
                {
                    int x = numberMax_font.x;
                    int y = numberMax_font.y;
                    int h = numberMax_font.h;
                    int w = numberMax_font.w;

                    int size = numberMax_font.text_size;
                    int space_h = numberMax_font.char_space;
                    int space_v = numberMax_font.line_space;

                    Color color = StringToColor(numberMax_font.color);
                    int alpha = numberMax_font.alpha;
                    string align_h = numberMax_font.align_h;
                    string align_v = numberMax_font.align_v;
                    string text_style = numberMax_font.text_style;
                    string valueStr = valueMax.ToString();
                    string unitStr = unit;
                    //if (numberMax_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                    if (numberMax_font.unit_type > 0)
                    {
                        if (numberMax_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }
                    if (!showTemperature) valueStr = "--";

                    if (numberMax_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (numberMax_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (numberMax_font.font != null && numberMax_font.font.Length > 3 && FontsList.ContainsKey(numberMax_font.font))
                    {
                        string font_fileName = FontsList[numberMax_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + numberMax_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (textMax_rotation != null && textMax_rotation.img_First != null && textMax_rotation.img_First.Length > 0 &&
                    textMax_rotation.dot_image != null && textMax_rotation.dot_image.Length > 0 && index == textMax_rotation.position && textMax_rotation.visible)
                {
                    int pos_x = textMax_rotation.imageX;
                    int pos_y = textMax_rotation.imageY;
                    int spacing = textMax_rotation.space;
                    float angle = textMax_rotation.angle;
                    bool addZero = textMax_rotation.zero;
                    int image_index = ListImages.IndexOf(textMax_rotation.img_First);
                    int unit_index = ListImages.IndexOf(textMax_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(textMax_rotation.dot_image);
                    string horizontal_alignment = textMax_rotation.align;
                    bool unit_in_alignment = textMax_rotation.unit_in_alignment;

                    int error_index = -1;
                    if (!showTemperature) error_index = ListImages.IndexOf(textMax_rotation.invalid_image);

                    string valueStr = valueMax.ToString();
                    string symbolMinus = "-";
                    if (textMax_rotation.zero)
                    {
                        valueStr = valueStr.PadLeft(3, '0');
                        if (valueMax < 0) valueStr = symbolMinus + Math.Abs(valueMax).ToString().PadLeft(3, '0');
                    }

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        valueStr, 3, BBorder, -1, error_index, !showTemperature, "ElementWeather");
                }

                if (textMax_circle != null && textMax_circle.img_First != null && textMax_circle.img_First.Length > 0 &&
                    textMax_circle.dot_image != null && textMax_circle.dot_image.Length > 0 && index == textMax_circle.position && textMax_circle.visible)
                {
                    int centr_x = textMax_circle.circle_center_X;
                    int centr_y = textMax_circle.circle_center_Y;
                    int radius = textMax_circle.radius;
                    int spacing = textMax_circle.char_space_angle;
                    float angle = textMax_circle.angle;
                    bool addZero = textMax_circle.zero;
                    int image_index = ListImages.IndexOf(textMax_circle.img_First);
                    int unit_index = ListImages.IndexOf(textMax_circle.unit);
                    int dot_image_index = ListImages.IndexOf(textMax_circle.dot_image);
                    string vertical_alignment = textMax_circle.vertical_alignment;
                    string horizontal_alignment = textMax_circle.horizontal_alignment;
                    bool reverse_direction = textMax_circle.reverse_direction;
                    bool unit_in_alignment = textMax_circle.unit_in_alignment;

                    int error_index = -1;
                    if (!showTemperature) error_index = ListImages.IndexOf(textMax_circle.error_image);

                    string valueStr = valueMax.ToString();
                    string symbolMinus = "-";
                    if (textMax_circle.zero)
                    {
                        valueStr = valueStr.PadLeft(3, '0');
                        if (valueMax < 0) valueStr = symbolMinus + Math.Abs(valueMax).ToString().PadLeft(3, '0');
                    }

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        valueStr, 3, BBorder, showCentrHend, -1, error_index, !showTemperature, "ElementWeather");
                }

                if (numberMinMax_font != null && index == numberMinMax_font.position && numberMinMax_font.visible)
                {
                    int x = numberMinMax_font.x;
                    int y = numberMinMax_font.y;
                    int h = numberMinMax_font.h;
                    int w = numberMinMax_font.w;

                    int size = numberMinMax_font.text_size;
                    int space_h = numberMinMax_font.char_space;
                    int space_v = numberMinMax_font.line_space;

                    Color color = StringToColor(numberMinMax_font.color);
                    int alpha = numberMinMax_font.alpha;
                    string align_h = numberMinMax_font.align_h;
                    string align_v = numberMinMax_font.align_v;
                    string text_style = numberMinMax_font.text_style;
                    string valueStr = valueMin.ToString() + "/" + valueMax.ToString();
                    string unitStr = unit;
                    //if (numberMinMax_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                    if (numberMinMax_font.unit_type > 0)
                    {
                        if (numberMinMax_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }
                    if (!showTemperature) valueStr = "--";

                    if (numberMinMax_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (numberMinMax_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (numberMinMax_font.font != null && numberMinMax_font.font.Length > 3 && FontsList.ContainsKey(numberMinMax_font.font))
                    {
                        string font_fileName = FontsList[numberMinMax_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + numberMinMax_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                    }
                }

                if (cityName != null && index == cityName.position && cityName.visible)
                {
                    int x = cityName.x;
                    int y = cityName.y;
                    int h = cityName.h;
                    int w = cityName.w;

                    int size = cityName.text_size;
                    int space_h = cityName.char_space;
                    int space_v = cityName.line_space;

                    Color color = StringToColor(cityName.color);
                    int alpha = cityName.alpha;
                    string align_h = cityName.align_h;
                    string align_v = cityName.align_v;
                    string text_style = cityName.text_style;
                    string valueStr = "City Name";

                    if (cityName.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (cityName.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (cityName.font != null && cityName.font.Length > 3 && FontsList.ContainsKey(cityName.font))
                    {
                        string font_fileName = FontsList[cityName.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }
                                
                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем все параметры элемента погода</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="group_current">Группа параметров для текущей температуры</param>
        /// <param name="group_min">Группа параметров для минимальной температуры</param>
        /// <param name="group_max">Группа параметров для максимальной температуры</param>
        /// <param name="group_max_min">Группа параметров для максимальной/минимальной температуры</param>
        /// <param name="images">Параметры для изображения</param>
        /// <param name="cityName">Параметры для названия города</param>
        /// <param name="icon">Параметры для иконки</param>
        /// <param name="value">Текущая температура</param>
        /// <param name="valueMin">Минимальная температура</param>
        /// <param name="valueMax">Максимальная температура</param>
        /// <param name="value_lenght">Максимальная длина для отображения значения</param>
        /// <param name="icon_index">Номер иконки погоды</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showTemperature">Показывать температуру</param>
        private void DrawWeather_v2(Graphics gPanel, WeatherGroup group_current, WeatherGroup group_min,
            WeatherGroup group_max, WeatherGroup group_max_min, hmUI_widget_IMG_LEVEL images, 
            hmUI_widget_TEXT cityName, hmUI_widget_IMG icon, int value, int valueMin, int valueMax, int value_lenght,
            int icon_index, bool BBorder, bool showTemperature, bool showCentrHend)
        {
            Bitmap src = new Bitmap(1, 1);
            string unit = "°";

            for (int index = 1; index <= 10; index++)
            {
                if (group_current != null && index == group_current.position)
                {
                    hmUI_widget_IMG_NUMBER number = group_current.Number;
                    hmUI_widget_TEXT number_font = group_current.Number_Font;
                    hmUI_widget_IMG_NUMBER text_rotation = group_current.Text_rotation;
                    Text_Circle text_circle = group_current.Text_circle;
                    int value_current = value;

                    if (text_circle != null && text_circle.img_First != null && text_circle.img_First.Length > 0 &&
                        text_circle.dot_image != null && text_circle.dot_image.Length > 0 && text_circle.visible)
                    {
                        int centr_x = text_circle.circle_center_X;
                        int centr_y = text_circle.circle_center_Y;
                        int radius = text_circle.radius;
                        int spacing = text_circle.char_space_angle;
                        float angle = text_circle.angle;
                        bool addZero = text_circle.zero;
                        int image_index = ListImages.IndexOf(text_circle.img_First);
                        int unit_index = ListImages.IndexOf(text_circle.unit);
                        int dot_image_index = ListImages.IndexOf(text_circle.dot_image);
                        string vertical_alignment = text_circle.vertical_alignment;
                        string horizontal_alignment = text_circle.horizontal_alignment;
                        bool reverse_direction = text_circle.reverse_direction;
                        bool unit_in_alignment = text_circle.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_circle.error_image);

                        string valueStr = value_current.ToString();
                        string symbolMinus = "-";
                        if (text_circle.zero)
                        {
                            valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (value_current < 0) valueStr = symbolMinus + Math.Abs(value_current).ToString().PadLeft(value_lenght, '0');
                        }

                        Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                            image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                            vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                            valueStr, value_lenght, BBorder, showCentrHend, -1, error_index, !showTemperature, "ElementWeather");
                    }

                    if (text_rotation != null && text_rotation.img_First != null && text_rotation.img_First.Length > 0 &&
                    text_rotation.dot_image != null && text_rotation.dot_image.Length > 0 && text_rotation.visible)
                    {
                        int pos_x = text_rotation.imageX;
                        int pos_y = text_rotation.imageY;
                        int spacing = text_rotation.space;
                        float angle = text_rotation.angle;
                        bool addZero = text_rotation.zero;
                        int image_index = ListImages.IndexOf(text_rotation.img_First);
                        int unit_index = ListImages.IndexOf(text_rotation.unit);
                        int dot_image_index = ListImages.IndexOf(text_rotation.dot_image);
                        string horizontal_alignment = text_rotation.align;
                        bool unit_in_alignment = text_rotation.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_rotation.invalid_image);

                        string valueStr = value_current.ToString();
                        string symbolMinus = "-";
                        if (text_rotation.zero)
                        {
                            valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (value_current < 0) valueStr = symbolMinus + Math.Abs(value_current).ToString().PadLeft(value_lenght, '0');
                        }

                        Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                            image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                            valueStr, value_lenght, BBorder, -1, error_index, !showTemperature, "ElementWeather");
                    }

                    if (number_font != null && number_font.visible)
                    {
                        int x = number_font.x;
                        int y = number_font.y;
                        int h = number_font.h;
                        int w = number_font.w;

                        int size = number_font.text_size;
                        int space_h = number_font.char_space;
                        int space_v = number_font.line_space;

                        Color color = StringToColor(number_font.color);
                        int alpha = number_font.alpha;
                        string align_h = number_font.align_h;
                        string align_v = number_font.align_v;
                        string text_style = number_font.text_style;
                        string valueStr = value_current.ToString();
                        string unitStr = unit;
                        //if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                        if (number_font.unit_type > 0)
                        {
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr += unitStr;
                        }
                        if (!showTemperature) valueStr = "--";

                        if (number_font.centreHorizontally)
                        {
                            x = (SelectedModel.background.w - w) / 2;
                            align_h = "CENTER_H";
                        }
                        if (number_font.centreVertically)
                        {
                            y = (SelectedModel.background.h - h) / 2;
                            align_v = "CENTER_V";
                        }

                        if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                        {
                            string font_fileName = FontsList[number_font.font];
                            //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                            if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                            {
                                Font drawFont = null;
                                using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                {
                                    fonts.AddFontFile(font_fileName);
                                    drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                }

                                Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                align_h, align_v, text_style, BBorder);
                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }

                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }
                    }

                    if (number != null && number.img_First != null && number.img_First.Length > 0 && number.visible)
                    {
                        int imageIndex = ListImages.IndexOf(number.img_First);
                        int x = number.imageX;
                        int y = number.imageY;
                        int spasing = number.space;
                        int alignment = AlignmentToInt(number.align);
                        bool addZero = false;
                        int angle = number.angle;
                        int separator_index = -1;
                        if (number.unit != null && number.unit.Length > 0)
                            separator_index = ListImages.IndexOf(number.unit);
                        int imageError_index = -1;
                        if (number.invalid_image != null && number.invalid_image.Length > 0)
                            imageError_index = ListImages.IndexOf(number.invalid_image);
                        int imageMinus_index = -1;
                        if (number.negative_image != null && number.negative_image.Length > 0)
                            imageMinus_index = ListImages.IndexOf(number.negative_image);

                        if (showTemperature)
                        {
                            Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, value_current, value_lenght, addZero,
                                imageMinus_index, separator_index, angle, BBorder, -1, false);
                        }
                        else if (imageError_index >= 0)
                        {
                            Draw_weather_text(gPanel, imageIndex, x, y,
                                            spasing, alignment, value_current, value_lenght, addZero, imageMinus_index, separator_index, angle,
                                            BBorder, imageError_index, true);
                        }

                        if (number.icon != null && number.icon.Length > 0)
                        {
                            imageIndex = ListImages.IndexOf(number.icon);
                            x = number.iconPosX;
                            y = number.iconPosY;

                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                        }
                    }
                }

                if (group_min != null && index == group_min.position)
                {
                    hmUI_widget_IMG_NUMBER number = group_min.Number;
                    hmUI_widget_TEXT number_font = group_min.Number_Font;
                    hmUI_widget_IMG_NUMBER text_rotation = group_min.Text_rotation;
                    Text_Circle text_circle = group_min.Text_circle;
                    int value_current = valueMin;

                    if (text_circle != null && text_circle.img_First != null && text_circle.img_First.Length > 0 &&
                        text_circle.dot_image != null && text_circle.dot_image.Length > 0 && text_circle.visible)
                    {
                        int centr_x = text_circle.circle_center_X;
                        int centr_y = text_circle.circle_center_Y;
                        int radius = text_circle.radius;
                        int spacing = text_circle.char_space_angle;
                        float angle = text_circle.angle;
                        bool addZero = text_circle.zero;
                        int image_index = ListImages.IndexOf(text_circle.img_First);
                        int unit_index = ListImages.IndexOf(text_circle.unit);
                        int dot_image_index = ListImages.IndexOf(text_circle.dot_image);
                        string vertical_alignment = text_circle.vertical_alignment;
                        string horizontal_alignment = text_circle.horizontal_alignment;
                        bool reverse_direction = text_circle.reverse_direction;
                        bool unit_in_alignment = text_circle.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_circle.error_image);

                        string valueStr = value_current.ToString();
                        string symbolMinus = "-";
                        if (text_circle.zero)
                        {
                            valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (value_current < 0) valueStr = symbolMinus + Math.Abs(value_current).ToString().PadLeft(value_lenght, '0');
                        }

                        Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                            image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                            vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                            valueStr, value_lenght, BBorder, showCentrHend, -1, error_index, !showTemperature, "ElementWeather");
                    }

                    if (text_rotation != null && text_rotation.img_First != null && text_rotation.img_First.Length > 0 &&
                    text_rotation.dot_image != null && text_rotation.dot_image.Length > 0 && text_rotation.visible)
                    {
                        int pos_x = text_rotation.imageX;
                        int pos_y = text_rotation.imageY;
                        int spacing = text_rotation.space;
                        float angle = text_rotation.angle;
                        bool addZero = text_rotation.zero;
                        int image_index = ListImages.IndexOf(text_rotation.img_First);
                        int unit_index = ListImages.IndexOf(text_rotation.unit);
                        int dot_image_index = ListImages.IndexOf(text_rotation.dot_image);
                        string horizontal_alignment = text_rotation.align;
                        bool unit_in_alignment = text_rotation.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_rotation.invalid_image);

                        string valueStr = value_current.ToString();
                        string symbolMinus = "-";
                        if (text_rotation.zero)
                        {
                            valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (value_current < 0) valueStr = symbolMinus + Math.Abs(value_current).ToString().PadLeft(value_lenght, '0');
                        }

                        Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                            image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                            valueStr, value_lenght, BBorder, -1, error_index, !showTemperature, "ElementWeather");
                    }

                    if (number_font != null && number_font.visible)
                    {
                        int x = number_font.x;
                        int y = number_font.y;
                        int h = number_font.h;
                        int w = number_font.w;

                        int size = number_font.text_size;
                        int space_h = number_font.char_space;
                        int space_v = number_font.line_space;

                        Color color = StringToColor(number_font.color);
                        int alpha = number_font.alpha;
                        string align_h = number_font.align_h;
                        string align_v = number_font.align_v;
                        string text_style = number_font.text_style;
                        string valueStr = value_current.ToString();
                        string unitStr = unit;
                        //if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                        if (number_font.unit_type > 0)
                        {
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr += unitStr;
                        }
                        if (!showTemperature) valueStr = "--";

                        if (number_font.centreHorizontally)
                        {
                            x = (SelectedModel.background.w - w) / 2;
                            align_h = "CENTER_H";
                        }
                        if (number_font.centreVertically)
                        {
                            y = (SelectedModel.background.h - h) / 2;
                            align_v = "CENTER_V";
                        }

                        if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                        {
                            string font_fileName = FontsList[number_font.font];
                            //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                            if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                            {
                                Font drawFont = null;
                                using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                {
                                    fonts.AddFontFile(font_fileName);
                                    drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                }

                                Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                align_h, align_v, text_style, BBorder);
                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }

                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }
                    }

                    if (number != null && number.img_First != null && number.img_First.Length > 0 && number.visible)
                    {
                        int imageIndex = ListImages.IndexOf(number.img_First);
                        int x = number.imageX;
                        int y = number.imageY;
                        int spasing = number.space;
                        int alignment = AlignmentToInt(number.align);
                        bool addZero = false;
                        int angle = number.angle;
                        int separator_index = -1;
                        if (number.unit != null && number.unit.Length > 0)
                            separator_index = ListImages.IndexOf(number.unit);
                        int imageError_index = -1;
                        if (number.invalid_image != null && number.invalid_image.Length > 0)
                            imageError_index = ListImages.IndexOf(number.invalid_image);
                        int imageMinus_index = -1;
                        if (number.negative_image != null && number.negative_image.Length > 0)
                            imageMinus_index = ListImages.IndexOf(number.negative_image);

                        if (showTemperature)
                        {
                            Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, value_current, value_lenght, addZero,
                                imageMinus_index, separator_index, angle, BBorder, -1, false);
                        }
                        else if (imageError_index >= 0)
                        {
                            Draw_weather_text(gPanel, imageIndex, x, y,
                                            spasing, alignment, value_current, value_lenght, addZero, imageMinus_index, separator_index, angle,
                                            BBorder, imageError_index, true);
                        }

                        if (number.icon != null && number.icon.Length > 0)
                        {
                            imageIndex = ListImages.IndexOf(number.icon);
                            x = number.iconPosX;
                            y = number.iconPosY;

                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                        }
                    }
                }

                if (group_max != null && index == group_max.position)
                {
                    hmUI_widget_IMG_NUMBER number = group_max.Number;
                    hmUI_widget_TEXT number_font = group_max.Number_Font;
                    hmUI_widget_IMG_NUMBER text_rotation = group_max.Text_rotation;
                    Text_Circle text_circle = group_max.Text_circle;
                    int value_current = valueMax;

                    if (text_circle != null && text_circle.img_First != null && text_circle.img_First.Length > 0 &&
                        text_circle.dot_image != null && text_circle.dot_image.Length > 0 && text_circle.visible)
                    {
                        int centr_x = text_circle.circle_center_X;
                        int centr_y = text_circle.circle_center_Y;
                        int radius = text_circle.radius;
                        int spacing = text_circle.char_space_angle;
                        float angle = text_circle.angle;
                        bool addZero = text_circle.zero;
                        int image_index = ListImages.IndexOf(text_circle.img_First);
                        int unit_index = ListImages.IndexOf(text_circle.unit);
                        int dot_image_index = ListImages.IndexOf(text_circle.dot_image);
                        string vertical_alignment = text_circle.vertical_alignment;
                        string horizontal_alignment = text_circle.horizontal_alignment;
                        bool reverse_direction = text_circle.reverse_direction;
                        bool unit_in_alignment = text_circle.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_circle.error_image);

                        string valueStr = value_current.ToString();
                        string symbolMinus = "-";
                        if (text_circle.zero)
                        {
                            valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (value_current < 0) valueStr = symbolMinus + Math.Abs(value_current).ToString().PadLeft(value_lenght, '0');
                        }

                        Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                            image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                            vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                            valueStr, value_lenght, BBorder, showCentrHend, -1, error_index, !showTemperature, "ElementWeather");
                    }

                    if (text_rotation != null && text_rotation.img_First != null && text_rotation.img_First.Length > 0 &&
                    text_rotation.dot_image != null && text_rotation.dot_image.Length > 0 && text_rotation.visible)
                    {
                        int pos_x = text_rotation.imageX;
                        int pos_y = text_rotation.imageY;
                        int spacing = text_rotation.space;
                        float angle = text_rotation.angle;
                        bool addZero = text_rotation.zero;
                        int image_index = ListImages.IndexOf(text_rotation.img_First);
                        int unit_index = ListImages.IndexOf(text_rotation.unit);
                        int dot_image_index = ListImages.IndexOf(text_rotation.dot_image);
                        string horizontal_alignment = text_rotation.align;
                        bool unit_in_alignment = text_rotation.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_rotation.invalid_image);

                        string valueStr = value_current.ToString();
                        string symbolMinus = "-";
                        if (text_rotation.zero)
                        {
                            valueStr = valueStr.PadLeft(value_lenght, '0');
                            if (value_current < 0) valueStr = symbolMinus + Math.Abs(value_current).ToString().PadLeft(value_lenght, '0');
                        }

                        Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                            image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                            valueStr, value_lenght, BBorder, -1, error_index, !showTemperature, "ElementWeather");
                    }

                    if (number_font != null && number_font.visible)
                    {
                        int x = number_font.x;
                        int y = number_font.y;
                        int h = number_font.h;
                        int w = number_font.w;

                        int size = number_font.text_size;
                        int space_h = number_font.char_space;
                        int space_v = number_font.line_space;

                        Color color = StringToColor(number_font.color);
                        int alpha = number_font.alpha;
                        string align_h = number_font.align_h;
                        string align_v = number_font.align_v;
                        string text_style = number_font.text_style;
                        string valueStr = value_current.ToString();
                        string unitStr = unit;
                        //if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                        if (number_font.unit_type > 0)
                        {
                            if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr += unitStr;
                        }
                        if (!showTemperature) valueStr = "--";

                        if (number_font.centreHorizontally)
                        {
                            x = (SelectedModel.background.w - w) / 2;
                            align_h = "CENTER_H";
                        }
                        if (number_font.centreVertically)
                        {
                            y = (SelectedModel.background.h - h) / 2;
                            align_v = "CENTER_V";
                        }

                        if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                        {
                            string font_fileName = FontsList[number_font.font];
                            //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                            if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                            {
                                Font drawFont = null;
                                using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                {
                                    fonts.AddFontFile(font_fileName);
                                    drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                }

                                Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                align_h, align_v, text_style, BBorder);
                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }

                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }
                    }

                    if (number != null && number.img_First != null && number.img_First.Length > 0 && number.visible)
                    {
                        int imageIndex = ListImages.IndexOf(number.img_First);
                        int x = number.imageX;
                        int y = number.imageY;
                        int spasing = number.space;
                        int alignment = AlignmentToInt(number.align);
                        bool addZero = false;
                        int angle = number.angle;
                        int separator_index = -1;
                        if (number.unit != null && number.unit.Length > 0)
                            separator_index = ListImages.IndexOf(number.unit);
                        int imageError_index = -1;
                        if (number.invalid_image != null && number.invalid_image.Length > 0)
                            imageError_index = ListImages.IndexOf(number.invalid_image);
                        int imageMinus_index = -1;
                        if (number.negative_image != null && number.negative_image.Length > 0)
                            imageMinus_index = ListImages.IndexOf(number.negative_image);

                        if (showTemperature)
                        {
                            Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, value_current, value_lenght, addZero,
                                imageMinus_index, separator_index, angle, BBorder, -1, false);
                        }
                        else if (imageError_index >= 0)
                        {
                            Draw_weather_text(gPanel, imageIndex, x, y,
                                            spasing, alignment, value_current, value_lenght, addZero, imageMinus_index, separator_index, angle,
                                            BBorder, imageError_index, true);
                        }

                        if (number.icon != null && number.icon.Length > 0)
                        {
                            imageIndex = ListImages.IndexOf(number.icon);
                            x = number.iconPosX;
                            y = number.iconPosY;

                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                        }
                    }
                }

                if (group_max_min != null && index == group_max_min.position)
                {
                    hmUI_widget_IMG_NUMBER number = group_max_min.Number;
                    hmUI_widget_TEXT number_font = group_max_min.Number_Font;
                    hmUI_widget_IMG_NUMBER text_rotation = group_max_min.Text_rotation;
                    Text_Circle text_circle = group_max_min.Text_circle;
                    //float value_current = valueMin;
                    //float value_max = valueMax;
                    //while (value_max > 1)
                    //{
                    //    value_max = value_max / 10;
                    //}
                    //value_current += value_max;

                    if (text_circle != null && text_circle.img_First != null && text_circle.img_First.Length > 0 &&
                        text_circle.dot_image != null && text_circle.dot_image.Length > 0 && text_circle.visible)
                    {
                        int centr_x = text_circle.circle_center_X;
                        int centr_y = text_circle.circle_center_Y;
                        int radius = text_circle.radius;
                        int spacing = text_circle.char_space_angle;
                        float angle = text_circle.angle;
                        bool addZero = text_circle.zero;
                        int image_index = ListImages.IndexOf(text_circle.img_First);
                        int unit_index = ListImages.IndexOf(text_circle.unit);
                        int dot_image_index = ListImages.IndexOf(text_circle.dot_image);
                        int separator_index = ListImages.IndexOf(text_circle.separator_image);
                        string vertical_alignment = text_circle.vertical_alignment;
                        string horizontal_alignment = text_circle.horizontal_alignment;
                        bool reverse_direction = text_circle.reverse_direction;
                        bool unit_in_alignment = text_circle.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_circle.error_image);

                        string valueMaxStr = valueMax.ToString();
                        string valueMinStr = valueMin.ToString();
                        string symbolMinus = "-";
                        if (text_circle.zero)
                        {
                            valueMaxStr = valueMaxStr.PadLeft(value_lenght, '0');
                            if (valueMax < 0) valueMaxStr = symbolMinus + Math.Abs(valueMax).ToString().PadLeft(value_lenght, '0');

                            valueMinStr = valueMinStr.PadLeft(value_lenght, '0');
                            if (valueMin < 0) valueMinStr = symbolMinus + Math.Abs(valueMin).ToString().PadLeft(value_lenght, '0');
                        }
                        string valueStr = valueMaxStr + "*" + valueMinStr;

                        Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                            image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                            vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                            valueStr, 6, BBorder, showCentrHend, separator_index, error_index, !showTemperature, "ElementWeather_Max/Min");
                    }

                    if (text_rotation != null && text_rotation.img_First != null && text_rotation.img_First.Length > 0 &&
                    text_rotation.dot_image != null && text_rotation.dot_image.Length > 0 && text_rotation.visible)
                    {
                        int pos_x = text_rotation.imageX;
                        int pos_y = text_rotation.imageY;
                        int spacing = text_rotation.space;
                        float angle = text_rotation.angle;
                        bool addZero = text_rotation.zero;
                        int image_index = ListImages.IndexOf(text_rotation.img_First);
                        int unit_index = ListImages.IndexOf(text_rotation.unit);
                        int dot_image_index = ListImages.IndexOf(text_rotation.dot_image);
                        int separator_index = ListImages.IndexOf(text_rotation.separator_image);
                        string horizontal_alignment = text_rotation.align;
                        bool unit_in_alignment = text_rotation.unit_in_alignment;

                        int error_index = -1;
                        if (!showTemperature) error_index = ListImages.IndexOf(text_rotation.invalid_image);

                        string valueMaxStr = valueMax.ToString();
                        string valueMinStr = valueMin.ToString();
                        string symbolMinus = "-";
                        if (text_rotation.zero)
                        {
                            valueMaxStr = valueMaxStr.PadLeft(value_lenght, '0');
                            if (valueMax < 0) valueMaxStr = symbolMinus + Math.Abs(valueMax).ToString().PadLeft(value_lenght, '0');

                            valueMinStr = valueMinStr.PadLeft(value_lenght, '0');
                            if (valueMin < 0) valueMinStr = symbolMinus + Math.Abs(valueMin).ToString().PadLeft(value_lenght, '0');
                        }
                        string valueStr = valueMaxStr + "*" + valueMinStr;

                        Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                            image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                            valueStr, value_lenght*2+3, BBorder, separator_index, error_index, !showTemperature, "ElementWeather_Max/Min");
                    }

                    if (number_font != null && number_font.visible)
                    {
                        int x = number_font.x;
                        int y = number_font.y;
                        int h = number_font.h;
                        int w = number_font.w;

                        int size = number_font.text_size;
                        int space_h = number_font.char_space;
                        int space_v = number_font.line_space;

                        Color color = StringToColor(number_font.color);
                        int alpha = number_font.alpha;
                        string align_h = number_font.align_h;
                        string align_v = number_font.align_v;
                        string text_style = number_font.text_style;
                        string delimeter = "/";
                        //if (number_font.unit_string != null && number_font.unit_string.Length > 0) delimeter = number_font.unit_string;
                        //string valueStr = valueMax.ToString() + delimeter + valueMin.ToString();
                        string valueStr = valueMin.ToString() + delimeter + valueMax.ToString();
                        string unitStr = unit;
                        //if (number_font.padding) valueStr = valueStr.PadLeft(value_lenght, '0');
                        if (number_font.unit_type > 0)
                        {
                            //if (number_font.unit_type == 2) unitStr = "°C";
                            //if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                            valueStr += unitStr;
                        }
                        if (!showTemperature) valueStr = "--";

                        if (number_font.centreHorizontally)
                        {
                            x = (SelectedModel.background.w - w) / 2;
                            align_h = "CENTER_H";
                        }
                        if (number_font.centreVertically)
                        {
                            y = (SelectedModel.background.h - h) / 2;
                            align_v = "CENTER_V";
                        }

                        if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                        {
                            string font_fileName = FontsList[number_font.font];
                            //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                            if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                            {
                                Font drawFont = null;
                                using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                {
                                    fonts.AddFontFile(font_fileName);
                                    drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                }

                                Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                align_h, align_v, text_style, BBorder);
                            }
                            else
                            {
                                Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            }

                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }
                    }

                    if (number != null && number.img_First != null && number.img_First.Length > 0 && number.visible)
                    {
                        int imageIndex = ListImages.IndexOf(number.img_First);
                        int x = number.imageX;
                        int y = number.imageY;
                        int spasing = number.space;
                        int alignment = AlignmentToInt(number.align);
                        bool addZero = false;
                        int angle = number.angle;
                        int unit_index = -1;
                        if (number.unit != null && number.unit.Length > 0)
                            unit_index = ListImages.IndexOf(number.unit);
                        int separator_index = -1;
                        if (number.dot_image != null && number.dot_image.Length > 0)
                            separator_index = ListImages.IndexOf(number.dot_image);
                        int imageError_index = -1;
                        if (number.invalid_image != null && number.invalid_image.Length > 0)
                            imageError_index = ListImages.IndexOf(number.invalid_image);
                        int imageMinus_index = -1;
                        if (number.negative_image != null && number.negative_image.Length > 0)
                            imageMinus_index = ListImages.IndexOf(number.negative_image);

                        if (showTemperature)
                        {
                            Draw_weather_max_min_text(gPanel, imageIndex, x, y, spasing, alignment, valueMax, valueMin, value_lenght, addZero,
                                imageMinus_index, unit_index, separator_index, angle, BBorder, -1, false);
                        }
                        else if (imageError_index >= 0)
                        {
                            Draw_weather_max_min_text(gPanel, imageIndex, x, y,
                                            spasing, alignment, valueMax, valueMin, value_lenght, addZero, imageMinus_index, separator_index, separator_index, 
                                            angle, BBorder, imageError_index, true);
                        }

                        if (number.icon != null && number.icon.Length > 0)
                        {
                            imageIndex = ListImages.IndexOf(number.icon);
                            x = number.iconPosX;
                            y = number.iconPosY;

                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && number.icon_alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = number.icon_alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                        }
                    }
                }


                if (images != null && images.img_First != null && images.img_First.Length > 0 &&
                   index == images.position && images.visible)
                {
                    if (icon_index >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(images.img_First);
                        int x = images.X;
                        int y = images.Y;
                        imageIndex = imageIndex + icon_index;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                        }
                    }
                }

                if (cityName != null && index == cityName.position && cityName.visible)
                {
                    int x = cityName.x;
                    int y = cityName.y;
                    int h = cityName.h;
                    int w = cityName.w;

                    int size = cityName.text_size;
                    int space_h = cityName.char_space;
                    int space_v = cityName.line_space;

                    Color color = StringToColor(cityName.color);
                    int alpha = cityName.alpha;
                    string align_h = cityName.align_h;
                    string align_v = cityName.align_v;
                    string text_style = cityName.text_style;
                    string valueStr = "City Name";

                    if (cityName.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (cityName.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (cityName.font != null && cityName.font.Length > 3 && FontsList.ContainsKey(cityName.font))
                    {
                        string font_fileName = FontsList[cityName.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, x, y);
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем все параметры элемента погода</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="weather_FewDays">Погода на несколько дней</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showTemperature">Показывать температуру</param>
        private void DrawWeatherFewDays(Graphics gPanel, Element_Weather_FewDays weather_FewDays, List<ForecastData> forecastData,
            bool BBorder, bool showTemperature)
        {
            Logger.WriteLine("* DrawWeatherFewDays");
            if (weather_FewDays == null) return;
            if (!weather_FewDays.visible) return;
            if (weather_FewDays.FewDays == null) return;
            //if (SelectedModel.versionOS < 3) return;

            Bitmap src = new Bitmap(1, 1);
            string unit = "°";
            int x = weather_FewDays.FewDays.X;
            int y = weather_FewDays.FewDays.Y;
            int offsetDayX = weather_FewDays.FewDays.ColumnWidth;
            //int offsetGraphX = 0;
            //int offsetGraphAverageX = 0;
            int offsetGraphY = 0;
            bool useMaxGraphOffset = (weather_FewDays.Diagram != null && weather_FewDays.Diagram.visible && weather_FewDays.Diagram.Use_max_diagram && weather_FewDays.Diagram.PositionOnGraph);
            bool useMinGraphOffset = (weather_FewDays.Diagram != null && weather_FewDays.Diagram.visible && weather_FewDays.Diagram.Use_min_diagram && weather_FewDays.Diagram.PositionOnGraph);
            bool useAverageGraphOffset = (weather_FewDays.Diagram != null && weather_FewDays.Diagram.visible && weather_FewDays.Diagram.Use_average_diagram && weather_FewDays.Diagram.PositionOnGraph);

            if (!radioButton_ScreenNormal.Checked || SelectedModel.versionOS < 3)
            {
                useMaxGraphOffset = false;
                useMinGraphOffset = false;
                useAverageGraphOffset = false;
            }

            if (useMaxGraphOffset || useMinGraphOffset || useAverageGraphOffset) offsetGraphY = weather_FewDays.Diagram.Y;
            //if (useAverageGraphOffset) offsetGraphAverageX = weather_FewDays.Diagram.Average_offsetX;

            if (weather_FewDays.FewDays != null && weather_FewDays.FewDays.Background != null)
            {
                string bg = weather_FewDays.FewDays.Background;
                if (bg != null && bg.Length > 0)
                {
                    int imageIndex = ListImages.IndexOf(bg);

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        gPanel.DrawImage(src, x, y);
                    }
                }
            }
            if (forecastData == null || forecastData.Count == 0) return;

            float scale = 1.0f;
            int offsetY = 0;
            int maximal_temp = 0;
            if (weather_FewDays.Diagram != null)
            {
                scale = DiagramScale(weather_FewDays.Diagram.Height, weather_FewDays.Diagram.Max_lineWidth,
                                weather_FewDays.Diagram.Min_lineWidth, weather_FewDays.Diagram.Max_pointSize,
                                weather_FewDays.Diagram.Min_pointSize, weather_FewDays.Diagram.Max_pointType,
                                weather_FewDays.Diagram.Min_pointType, forecastData, out maximal_temp, weather_FewDays.FewDays.DaysCount);
                offsetY = weather_FewDays.Diagram.Max_lineWidth / 2;
                if (weather_FewDays.Diagram.Max_pointSize > weather_FewDays.Diagram.Max_lineWidth && weather_FewDays.Diagram.Max_pointType > 0) 
                    offsetY = weather_FewDays.Diagram.Max_pointSize / 2; 
            }

            for (int index = 0; index <= 15; index++)
            {
                int offsetGraphX = 0;

                if (weather_FewDays.Images != null && weather_FewDays.Images.img_First != null && weather_FewDays.Images.img_First.Length > 0 &&
                   index == weather_FewDays.Images.position && weather_FewDays.Images.visible)
                {
                    int posY = y + weather_FewDays.Images.Y;
                    int imageIndex = ListImages.IndexOf(weather_FewDays.Images.img_First);

                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int weather_index = forecastData[dayIndex].index;
                            if (weather_index >= 0)
                            {
                                int posX = x + offsetDayX * dayIndex + weather_FewDays.Images.X;
                                int image_index = imageIndex + weather_index;

                                if (image_index < ListImagesFullName.Count)
                                {
                                    src = OpenFileStream(ListImagesFullName[image_index]);
                                    if (SelectedModel.versionOS >= 2.1 && weather_FewDays.Images.alpha != 255)
                                    {
                                        int w = src.Width;
                                        int h = src.Height;
                                        // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                        ColorMatrix colorMatrix = new ColorMatrix();
                                        colorMatrix.Matrix33 = weather_FewDays.Images.alpha / 255f; // значение от 0 до 1

                                        // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                        ImageAttributes imgAttributes = new ImageAttributes();
                                        imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                        // Указываем прямоугольник, куда будет помещено изображение
                                        Rectangle rect_alpha = new Rectangle(posX, posY, w, h);
                                        gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                                    }
                                    else gPanel.DrawImage(src, posX, posY);
                                }
                            } 
                        }
                    }
                }

                if (weather_FewDays.Diagram != null && index == weather_FewDays.Diagram.position && weather_FewDays.Diagram.visible && 
                    /*radioButton_ScreenNormal.Checked &&*/ SelectedModel.versionOS >= 3)
                {
                    Color colorMax = StringToColor(weather_FewDays.Diagram.Max_color);
                    Pen penMax = new Pen(colorMax, weather_FewDays.Diagram.Max_lineWidth);
                    int maxPintSize = weather_FewDays.Diagram.Max_pointSize;
                    if (weather_FewDays.Diagram.Max_pointType == 0) maxPintSize = 0;

                    Color colorMin = StringToColor(weather_FewDays.Diagram.Min_color);
                    Pen penMin = new Pen(colorMin, weather_FewDays.Diagram.Min_lineWidth);
                    int minPintSize = weather_FewDays.Diagram.Min_pointSize;
                    if (weather_FewDays.Diagram.Min_pointType == 0) minPintSize = 0;

                    Color colorAverage = StringToColor(weather_FewDays.Diagram.Average_color);
                    Pen penAverage = new Pen(colorAverage, weather_FewDays.Diagram.Average_lineWidth);
                    int averagePintSize = weather_FewDays.Diagram.Average_pointSize;
                    if (weather_FewDays.Diagram.Average_pointType == 0) averagePintSize = 0;

                    int offsetDiagramY = offsetY;
                    offsetDiagramY += weather_FewDays.FewDays.Y + weather_FewDays.Diagram.Y;
                    //offsetDiagramY++;

                    int Max_offsetX = weather_FewDays.FewDays.X + weather_FewDays.Diagram.Max_offsetX;
                    int Min_offsetX = weather_FewDays.FewDays.X + weather_FewDays.Diagram.Min_offsetX;
                    int Average_offsetX = weather_FewDays.FewDays.X + weather_FewDays.Diagram.Average_offsetX;

                    int maxOldX = Max_offsetX;
                    int minOldX = Min_offsetX;
                    int averageOldX = Average_offsetX;

                    int maxOldY = (int)((maximal_temp - forecastData[0].high) * scale) + offsetDiagramY;
                    int minOldY = (int)((maximal_temp - forecastData[0].low) * scale) + offsetDiagramY;
                    int averageOldY = (int)((maximal_temp - (forecastData[0].high + forecastData[0].low) / 2) * scale) + offsetDiagramY;

                    bool endPointMax = false;
                    bool endPointMin = false;
                    bool endPointAverage = false;

                    SmoothingMode smoothingMode = gPanel.SmoothingMode;
                    gPanel.SmoothingMode = SmoothingMode.AntiAlias;
                    for (int i = 0; i < weather_FewDays.FewDays.DaysCount; i++)
                    {
                        if (i < forecastData.Count)
                        {
                            if (weather_FewDays.Diagram.Use_min_diagram)
                            {
                                Point pointStart = new Point(minOldX, minOldY);
                                minOldX = Min_offsetX + i * weather_FewDays.FewDays.ColumnWidth;
                                minOldY = (int)((maximal_temp - forecastData[i].low) * scale) + offsetDiagramY;
                                Point pointEnd = new Point(minOldX, minOldY);

                                if (pointStart != pointEnd)
                                {
                                    gPanel.DrawLine(penMin, pointStart, pointEnd);
                                    DrawaWeatherPoint(gPanel, pointStart.X, pointStart.Y, minPintSize,
                                        weather_FewDays.Diagram.Min_pointType, StringToColor(weather_FewDays.Diagram.Min_color));
                                    endPointMin = true;
                                }
                            }

                            if (weather_FewDays.Diagram.Use_average_diagram)
                            {
                                Point pointStart = new Point(averageOldX, averageOldY);
                                averageOldX = Average_offsetX + i * weather_FewDays.FewDays.ColumnWidth;
                                averageOldY = (int)((maximal_temp - (forecastData[i].high + forecastData[i].low) / 2) * scale) + offsetDiagramY;
                                Point pointEnd = new Point(averageOldX, averageOldY);

                                if (pointStart != pointEnd)
                                {
                                    gPanel.DrawLine(penAverage, pointStart, pointEnd);
                                    DrawaWeatherPoint(gPanel, pointStart.X, pointStart.Y, averagePintSize,
                                        weather_FewDays.Diagram.Average_pointType, StringToColor(weather_FewDays.Diagram.Average_color));
                                    endPointAverage = true;
                                }
                            }

                            if (weather_FewDays.Diagram.Use_max_diagram)
                            {
                                Point pointStart = new Point(maxOldX, maxOldY);
                                maxOldX = Max_offsetX + i * weather_FewDays.FewDays.ColumnWidth;
                                maxOldY = (int)((maximal_temp - forecastData[i].high) * scale) + offsetDiagramY;
                                Point pointEnd = new Point(maxOldX, maxOldY);

                                if (pointStart != pointEnd)
                                {
                                    gPanel.DrawLine(penMax, pointStart, pointEnd);
                                    DrawaWeatherPoint(gPanel, pointStart.X, pointStart.Y, maxPintSize,
                                        weather_FewDays.Diagram.Max_pointType, StringToColor(weather_FewDays.Diagram.Max_color));
                                    endPointMax = true;
                                }
                            }
                        }
                    }
                    if (endPointMin) DrawaWeatherPoint(gPanel, minOldX, minOldY, minPintSize,
                        weather_FewDays.Diagram.Min_pointType, StringToColor(weather_FewDays.Diagram.Min_color));
                    if (endPointAverage) DrawaWeatherPoint(gPanel, averageOldX, averageOldY, averagePintSize,
                        weather_FewDays.Diagram.Average_pointType, StringToColor(weather_FewDays.Diagram.Average_color));
                    if (endPointMax) DrawaWeatherPoint(gPanel, maxOldX, maxOldY, maxPintSize,
                        weather_FewDays.Diagram.Max_pointType, StringToColor(weather_FewDays.Diagram.Max_color));

                    gPanel.SmoothingMode = smoothingMode;
                }


                if (weather_FewDays.Number_Max != null && index == weather_FewDays.Number_Max.position && 
                    weather_FewDays.Number_Max.img_First != null && weather_FewDays.Number_Max.img_First.Length > 0 && weather_FewDays.Number_Max.visible)
                {
                    if (useMaxGraphOffset) offsetGraphX = weather_FewDays.Diagram.Max_offsetX;
                    int imageIndex = ListImages.IndexOf(weather_FewDays.Number_Max.img_First);
                    int posY = y + weather_FewDays.Number_Max.imageY;
                    int spacing = weather_FewDays.Number_Max.space;
                    int alignment = AlignmentToInt(weather_FewDays.Number_Max.align);
                    bool addZero = false;
                    int angle = weather_FewDays.Number_Max.angle;
                    int separator_index = -1;
                    if (weather_FewDays.Number_Max.unit != null && weather_FewDays.Number_Max.unit.Length > 0)
                        separator_index = ListImages.IndexOf(weather_FewDays.Number_Max.unit);
                    int imageError_index = -1;
                    if (weather_FewDays.Number_Max.invalid_image != null && weather_FewDays.Number_Max.invalid_image.Length > 0)
                        imageError_index = ListImages.IndexOf(weather_FewDays.Number_Max.invalid_image);
                    int imageMinus_index = -1;
                    if (weather_FewDays.Number_Max.negative_image != null && weather_FewDays.Number_Max.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(weather_FewDays.Number_Max.negative_image);


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_Max.imageX + offsetGraphX;
                            if (useMaxGraphOffset)
                            {

                                int offsetDayY = (int)((maximal_temp - forecastData[dayIndex].high) * scale) + offsetY;
                                posDayY += offsetDayY + offsetGraphY;
                            }

                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                int temperature_value = forecastData[dayIndex].high;

                                Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, temperature_value, 5, addZero,
                                    imageMinus_index, separator_index, angle, BBorder, -1, false);
                            }
                            else if (imageError_index >= 0)
                            {
                                Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, 0, 5, addZero,
                                    imageMinus_index, separator_index, angle,
                                                BBorder, imageError_index, true);
                            } 
                        }
                    }
                }

                if (weather_FewDays.Number_Font_Max != null && index == weather_FewDays.Number_Font_Max.position && weather_FewDays.Number_Font_Max.visible)
                {
                    if (useMaxGraphOffset) offsetGraphX = weather_FewDays.Diagram.Max_offsetX;
                    int posY = y + weather_FewDays.Number_Font_Max.y;
                    int h = weather_FewDays.Number_Font_Max.h;
                    int w = weather_FewDays.Number_Font_Max.w;

                    int size = weather_FewDays.Number_Font_Max.text_size;
                    int space_h = weather_FewDays.Number_Font_Max.char_space;
                    int space_v = weather_FewDays.Number_Font_Max.line_space;

                    Color color = StringToColor(weather_FewDays.Number_Font_Max.color);
                    int alpha = weather_FewDays.Number_Font_Max.alpha;
                    string align_h = weather_FewDays.Number_Font_Max.align_h;
                    string align_v = weather_FewDays.Number_Font_Max.align_v;
                    string text_style = weather_FewDays.Number_Font_Max.text_style;


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_Font_Max.x + offsetGraphX;
                            if (useMaxGraphOffset)
                            {

                                int offsetDayY = (int)((maximal_temp - forecastData[dayIndex].high) * scale) + offsetY;
                                posDayY += offsetDayY + offsetGraphY;
                            }

                            string valueStr = "--";
                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                valueStr = forecastData[dayIndex].high.ToString();

                                string unitStr = unit;
                                if (weather_FewDays.Number_Font_Max.unit_type > 0)
                                {
                                    if (weather_FewDays.Number_Font_Max.unit_type == 2) unitStr = unitStr.ToUpper();
                                    valueStr += unitStr;
                                }
                            }

                            if (weather_FewDays.Number_Font_Max.centreHorizontally)
                            {
                                posX = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (weather_FewDays.Number_Font_Max.centreVertically)
                            {
                                posX = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (weather_FewDays.Number_Font_Max.font != null && weather_FewDays.Number_Font_Max.font.Length > 3 && FontsList.ContainsKey(weather_FewDays.Number_Font_Max.font))
                            {
                                string font_fileName = FontsList[weather_FewDays.Number_Font_Max.font];
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, posX, posDayY, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            } 
                        }
                    }
                }


                if (weather_FewDays.Number_Average != null && index == weather_FewDays.Number_Average.position &&
                    weather_FewDays.Number_Average.img_First != null && weather_FewDays.Number_Average.img_First.Length > 0 && weather_FewDays.Number_Average.visible)
                {
                    if (useAverageGraphOffset) offsetGraphX = weather_FewDays.Diagram.Average_offsetX;
                    int imageIndex = ListImages.IndexOf(weather_FewDays.Number_Average.img_First);
                    int posY = y + weather_FewDays.Number_Average.imageY;
                    int spacing = weather_FewDays.Number_Average.space;
                    int alignment = AlignmentToInt(weather_FewDays.Number_Average.align);
                    bool addZero = false;
                    int angle = weather_FewDays.Number_Average.angle;
                    int separator_index = -1;
                    if (weather_FewDays.Number_Average.unit != null && weather_FewDays.Number_Average.unit.Length > 0)
                        separator_index = ListImages.IndexOf(weather_FewDays.Number_Average.unit);
                    int imageError_index = -1;
                    if (weather_FewDays.Number_Average.invalid_image != null && weather_FewDays.Number_Average.invalid_image.Length > 0)
                        imageError_index = ListImages.IndexOf(weather_FewDays.Number_Average.invalid_image);
                    int imageMinus_index = -1;
                    if (weather_FewDays.Number_Average.negative_image != null && weather_FewDays.Number_Average.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(weather_FewDays.Number_Average.negative_image);


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_Average.imageX + offsetGraphX;
                            if (useAverageGraphOffset)
                            {

                                int offsetDayY = (int)((maximal_temp - (forecastData[dayIndex].high + forecastData[dayIndex].low) / 2) * scale) + offsetY;
                                posDayY += offsetDayY + offsetGraphY;
                            }

                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                int temperature_value = (forecastData[dayIndex].high + forecastData[dayIndex].low) / 2;

                                Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, temperature_value, 5, addZero,
                                    imageMinus_index, separator_index, angle, BBorder, -1, false);
                            }
                            else if (imageError_index >= 0)
                            {
                                Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, 0, 5, addZero,
                                    imageMinus_index, separator_index, angle,
                                                BBorder, imageError_index, true);
                            } 
                        }
                    }
                }

                if (weather_FewDays.Number_Font_Average != null && index == weather_FewDays.Number_Font_Average.position && weather_FewDays.Number_Font_Average.visible)
                {
                    if (useAverageGraphOffset) offsetGraphX = weather_FewDays.Diagram.Average_offsetX;
                    int posY = y + weather_FewDays.Number_Font_Average.y;
                    int h = weather_FewDays.Number_Font_Average.h;
                    int w = weather_FewDays.Number_Font_Average.w;

                    int size = weather_FewDays.Number_Font_Average.text_size;
                    int space_h = weather_FewDays.Number_Font_Average.char_space;
                    int space_v = weather_FewDays.Number_Font_Average.line_space;

                    Color color = StringToColor(weather_FewDays.Number_Font_Average.color);
                    int alpha = weather_FewDays.Number_Font_Average.alpha;
                    string align_h = weather_FewDays.Number_Font_Average.align_h;
                    string align_v = weather_FewDays.Number_Font_Average.align_v;
                    string text_style = weather_FewDays.Number_Font_Average.text_style;


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_Font_Average.x + offsetGraphX;
                            if (useAverageGraphOffset)
                            {

                                int offsetDayY = (int)((maximal_temp - (forecastData[dayIndex].high + forecastData[dayIndex].low) / 2) * scale) + offsetY;
                                posDayY += offsetDayY + offsetGraphY;
                            }

                            string valueStr = "--";
                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                int temp = (forecastData[dayIndex].high + forecastData[dayIndex].low) / 2;
                                valueStr = temp.ToString();

                                string unitStr = unit;
                                if (weather_FewDays.Number_Font_Average.unit_type > 0)
                                {
                                    if (weather_FewDays.Number_Font_Average.unit_type == 2) unitStr = unitStr.ToUpper();
                                    valueStr += unitStr;
                                }
                            }

                            if (weather_FewDays.Number_Font_Average.centreHorizontally)
                            {
                                posX = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (weather_FewDays.Number_Font_Average.centreVertically)
                            {
                                posX = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (weather_FewDays.Number_Font_Average.font != null && weather_FewDays.Number_Font_Average.font.Length > 3 && FontsList.ContainsKey(weather_FewDays.Number_Font_Max.font))
                            {
                                string font_fileName = FontsList[weather_FewDays.Number_Font_Average.font];
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, posX, posDayY, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            } 
                        }
                    }
                }


                if (weather_FewDays.Number_Min != null && index == weather_FewDays.Number_Min.position &&
                    weather_FewDays.Number_Min.img_First != null && weather_FewDays.Number_Min.img_First.Length > 0 && weather_FewDays.Number_Min.visible)
                {
                    if (useMinGraphOffset) offsetGraphX = weather_FewDays.Diagram.Min_offsetX;
                    int imageIndex = ListImages.IndexOf(weather_FewDays.Number_Min.img_First);
                    int posY = y + weather_FewDays.Number_Min.imageY;
                    int spacing = weather_FewDays.Number_Min.space;
                    int alignment = AlignmentToInt(weather_FewDays.Number_Min.align);
                    bool addZero = false;
                    int angle = weather_FewDays.Number_Min.angle;
                    int separator_index = -1;
                    if (weather_FewDays.Number_Min.unit != null && weather_FewDays.Number_Min.unit.Length > 0)
                        separator_index = ListImages.IndexOf(weather_FewDays.Number_Min.unit);
                    int imageError_index = -1;
                    if (weather_FewDays.Number_Min.invalid_image != null && weather_FewDays.Number_Min.invalid_image.Length > 0)
                        imageError_index = ListImages.IndexOf(weather_FewDays.Number_Min.invalid_image);
                    int imageMinus_index = -1;
                    if (weather_FewDays.Number_Min.negative_image != null && weather_FewDays.Number_Min.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(weather_FewDays.Number_Min.negative_image);


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_Min.imageX + offsetGraphX;
                            if (useMinGraphOffset)
                            {

                                int offsetDayY = (int)((maximal_temp - forecastData[dayIndex].low) * scale) + offsetY;
                                posDayY += offsetDayY + offsetGraphY;
                            }

                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                int temperature_value = forecastData[dayIndex].low;

                                Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, temperature_value, 5, addZero,
                                    imageMinus_index, separator_index, angle, BBorder, -1, false);
                            }
                            else if (imageError_index >= 0)
                            {
                                Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, 0, 5, addZero,
                                    imageMinus_index, separator_index, angle,
                                                BBorder, imageError_index, true);
                            } 
                        }
                    }
                }

                if (weather_FewDays.Number_Font_Min != null && index == weather_FewDays.Number_Font_Min.position && weather_FewDays.Number_Font_Min.visible)
                {
                    if (useMinGraphOffset) offsetGraphX = weather_FewDays.Diagram.Min_offsetX;
                    int posY = y + weather_FewDays.Number_Font_Min.y;
                    int h = weather_FewDays.Number_Font_Min.h;
                    int w = weather_FewDays.Number_Font_Min.w;

                    int size = weather_FewDays.Number_Font_Min.text_size;
                    int space_h = weather_FewDays.Number_Font_Min.char_space;
                    int space_v = weather_FewDays.Number_Font_Min.line_space;

                    Color color = StringToColor(weather_FewDays.Number_Font_Min.color);
                    int alpha = weather_FewDays.Number_Font_Min.alpha;
                    string align_h = weather_FewDays.Number_Font_Min.align_h;
                    string align_v = weather_FewDays.Number_Font_Min.align_v;
                    string text_style = weather_FewDays.Number_Font_Min.text_style;


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_Font_Min.x + offsetGraphX;
                            if (useMinGraphOffset)
                            {

                                int offsetDayY = (int)((maximal_temp - forecastData[dayIndex].low) * scale) + offsetY;
                                posDayY += offsetDayY + offsetGraphY;
                            }

                            string valueStr = "--";
                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                valueStr = forecastData[dayIndex].low.ToString();

                                string unitStr = unit;
                                if (weather_FewDays.Number_Font_Min.unit_type > 0)
                                {
                                    if (weather_FewDays.Number_Font_Min.unit_type == 2) unitStr = unitStr.ToUpper();
                                    valueStr += unitStr;
                                }
                            }

                            if (weather_FewDays.Number_Font_Min.centreHorizontally)
                            {
                                posX = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (weather_FewDays.Number_Font_Min.centreVertically)
                            {
                                posX = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (weather_FewDays.Number_Font_Min.font != null && weather_FewDays.Number_Font_Min.font.Length > 3 && FontsList.ContainsKey(weather_FewDays.Number_Font_Min.font))
                            {
                                string font_fileName = FontsList[weather_FewDays.Number_Font_Min.font];
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, posX, posDayY, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            } 
                        }
                    }
                }


                if (weather_FewDays.Number_MaxMin != null && index == weather_FewDays.Number_MaxMin.position &&
                    weather_FewDays.Number_MaxMin.img_First != null && weather_FewDays.Number_MaxMin.img_First.Length > 0 && weather_FewDays.Number_MaxMin.visible)
                {
                    int imageIndex = ListImages.IndexOf(weather_FewDays.Number_MaxMin.img_First);
                    int posY = y + weather_FewDays.Number_MaxMin.imageY;
                    int spacing = weather_FewDays.Number_MaxMin.space;
                    int alignment = AlignmentToInt(weather_FewDays.Number_MaxMin.align);
                    bool addZero = false;
                    int angle = weather_FewDays.Number_MaxMin.angle;
                    int unit_index = -1;
                    if (weather_FewDays.Number_MaxMin.unit != null && weather_FewDays.Number_MaxMin.unit.Length > 0)
                        unit_index = ListImages.IndexOf(weather_FewDays.Number_MaxMin.unit);
                    int imageError_index = -1;
                    if (weather_FewDays.Number_MaxMin.invalid_image != null && weather_FewDays.Number_MaxMin.invalid_image.Length > 0)
                        imageError_index = ListImages.IndexOf(weather_FewDays.Number_MaxMin.invalid_image);
                    int imageMinus_index = -1;
                    if (weather_FewDays.Number_MaxMin.negative_image != null && weather_FewDays.Number_MaxMin.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(weather_FewDays.Number_MaxMin.negative_image);
                    int separator_index = -1;
                    if (weather_FewDays.Number_MaxMin.separator_image != null && weather_FewDays.Number_MaxMin.separator_image.Length > 0)
                        separator_index = ListImages.IndexOf(weather_FewDays.Number_MaxMin.separator_image);


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_MaxMin.imageX + offsetGraphX;

                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                int tMax_value = forecastData[dayIndex].high;
                                int tMin_value = forecastData[dayIndex].low;

                                //Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, temperature_value, 5, addZero,
                                //    imageMinus_index, separator_index, angle, BBorder, -1, false);
                                Draw_weather_MaxMin(gPanel, imageIndex, posX, posDayY, spacing, alignment, tMax_value, tMin_value,
                                    imageMinus_index, separator_index, unit_index, angle, BBorder, -1, false);
                            }
                            else if (imageError_index >= 0)
                            {
                                Draw_weather_text(gPanel, imageIndex, posX, posDayY, spacing, alignment, 0, 5, addZero,
                                    imageMinus_index, unit_index, angle,
                                                BBorder, imageError_index, true);
                            } 
                        }
                    }
                }

                if (weather_FewDays.Number_Font_MaxMin != null && index == weather_FewDays.Number_Font_MaxMin.position && weather_FewDays.Number_Font_MaxMin.visible)
                {
                    int posY = y + weather_FewDays.Number_Font_MaxMin.y;
                    int h = weather_FewDays.Number_Font_MaxMin.h;
                    int w = weather_FewDays.Number_Font_MaxMin.w;

                    int size = weather_FewDays.Number_Font_MaxMin.text_size;
                    int space_h = weather_FewDays.Number_Font_MaxMin.char_space;
                    int space_v = weather_FewDays.Number_Font_MaxMin.line_space;

                    Color color = StringToColor(weather_FewDays.Number_Font_MaxMin.color);
                    int alpha = weather_FewDays.Number_Font_MaxMin.alpha;
                    string align_h = weather_FewDays.Number_Font_MaxMin.align_h;
                    string align_v = weather_FewDays.Number_Font_MaxMin.align_v;
                    string text_style = weather_FewDays.Number_Font_MaxMin.text_style;


                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        if (dayIndex < forecastData.Count)
                        {
                            int posDayY = posY;
                            int posX = x + offsetDayX * dayIndex + weather_FewDays.Number_Font_MaxMin.x;

                            string valueStr = "--";
                            if (dayIndex < forecastData.Count && showTemperature)
                            {
                                valueStr = forecastData[dayIndex].high.ToString();
                                if (weather_FewDays.Number_Font_MaxMin.unit_string != null && weather_FewDays.Number_Font_MaxMin.unit_string.Length > 0)
                                    valueStr += weather_FewDays.Number_Font_MaxMin.unit_string;
                                valueStr += forecastData[dayIndex].low.ToString();

                                string unitStr = unit;
                                if (weather_FewDays.Number_Font_MaxMin.unit_type > 0)
                                {
                                    if (weather_FewDays.Number_Font_MaxMin.unit_type == 2) unitStr = unitStr.ToUpper();
                                    valueStr += unitStr;
                                }
                            }

                            if (weather_FewDays.Number_Font_MaxMin.centreHorizontally)
                            {
                                posX = (SelectedModel.background.w - w) / 2;
                                align_h = "CENTER_H";
                            }
                            if (weather_FewDays.Number_Font_MaxMin.centreVertically)
                            {
                                posX = (SelectedModel.background.h - h) / 2;
                                align_v = "CENTER_V";
                            }

                            if (weather_FewDays.Number_Font_MaxMin.font != null && weather_FewDays.Number_Font_MaxMin.font.Length > 3 && FontsList.ContainsKey(weather_FewDays.Number_Font_MaxMin.font))
                            {
                                string font_fileName = FontsList[weather_FewDays.Number_Font_MaxMin.font];
                                if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                {
                                    Font drawFont = null;
                                    using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                    {
                                        fonts.AddFontFile(font_fileName);
                                        drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                    }

                                    Draw_text_userFont(gPanel, posX, posDayY, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                    align_h, align_v, text_style, BBorder);
                                }
                                else
                                {
                                    Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }

                            }
                            else
                            {
                                Draw_text(gPanel, posX, posDayY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                            } 
                        }
                    }
                }


                if (weather_FewDays.DayOfWeek_Images != null && weather_FewDays.DayOfWeek_Images.img_First != null
                    && weather_FewDays.DayOfWeek_Images.img_First.Length > 0 && index == weather_FewDays.DayOfWeek_Images.position &&
                    weather_FewDays.DayOfWeek_Images.visible)
                {
                    int posY = y + weather_FewDays.DayOfWeek_Images.Y;
                    int imageIndex = ListImages.IndexOf(weather_FewDays.DayOfWeek_Images.img_First);

                    for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                    {
                        int dof = WatchFacePreviewSet.Date.WeekDay + dayIndex - 1;
                        while (dof >= 7) { dof -= 7; }
                        int posX = x + offsetDayX * dayIndex + weather_FewDays.DayOfWeek_Images.X;
                        int image_index = imageIndex + dof;

                        if (image_index < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[image_index]);
                            if (SelectedModel.versionOS >= 2.1 && weather_FewDays.DayOfWeek_Images.alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = weather_FewDays.DayOfWeek_Images.alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(posX, posY, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, posX, posY);
                        }
                    }
                }

                if (weather_FewDays.DayOfWeek_Font != null && index == weather_FewDays.DayOfWeek_Font.position && weather_FewDays.DayOfWeek_Font.visible)
                    {
                    hmUI_widget_TEXT dow_font = weather_FewDays.DayOfWeek_Font;
                    string[] dowArrey = dow_font.unit_string.Split(',');

                    int h = dow_font.h;
                    int w = dow_font.w;

                    int size = dow_font.text_size;
                    int space_h = dow_font.char_space;
                    int space_v = dow_font.line_space;

                    Color color_1 = StringToColor(dow_font.color);
                    Color color_2 = StringToColor(dow_font.color_2);
                    int alpha = dow_font.alpha;

                    string align_h = dow_font.align_h;
                        string align_v = dow_font.align_v;
                        string text_style = dow_font.text_style;

                        if (dowArrey.Length == 7)
                        {
                            int posY = y + weather_FewDays.DayOfWeek_Font.y;
                            for (int dayIndex = 0; dayIndex < weather_FewDays.FewDays.DaysCount; dayIndex++)
                            {
                                int dof = WatchFacePreviewSet.Date.WeekDay + dayIndex - 1;
                                while (dof >= 7) { dof -= 7; }
                                int posX = x + offsetDayX * dayIndex + weather_FewDays.DayOfWeek_Font.x;
                                string valueStr = dowArrey[dof].Trim();

                                Color color = color_1;
                                if (dow_font.use_color_2 && dof >= 5) color = color_2;

                                if (dow_font.font != null && dow_font.font.Length > 3 && FontsList.ContainsKey(dow_font.font))
                                {
                                    string font_fileName = FontsList[dow_font.font];
                                    if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                                    {
                                        Font drawFont = null;
                                        using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                                        {
                                            fonts.AddFontFile(font_fileName);
                                            drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                                        }

                                        Draw_text_userFont(gPanel, posX, posY, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                                        align_h, align_v, text_style, BBorder);
                                    }
                                    else
                                    {
                                        Draw_text(gPanel, posX, posY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                    }

                                }
                                else
                                {
                                    Draw_text(gPanel, posX, posY, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                                }
                            }

                        }
                }

                if (weather_FewDays.Icon != null && weather_FewDays.Icon.src != null && weather_FewDays.Icon.src.Length > 0 &&
                    index == weather_FewDays.Icon.position && weather_FewDays.Icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(weather_FewDays.Icon.src);
                    int iconPosX = x + weather_FewDays.Icon.x;
                    int iconPosY = y + weather_FewDays.Icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && weather_FewDays.Icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = weather_FewDays.Icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(iconPosX, iconPosY, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, iconPosX, iconPosY);
                        //gPanel.DrawImage(src, iconPosX, iconPosY);
                    }
                }

            }
            Logger.WriteLine("* DrawWeatherFewDays (End)");
        }

        private float DiagramScale(int heightDiagram, int maxLineWidth, int minLineWidth, int maxPointSize, int minPointSize, 
            int maxPointType, int minPointType, List<ForecastData> forecastData, out int max_temp, int daysCount)
        {
            Logger.WriteLine("* DiagramScale");
            if (maxLineWidth < maxPointSize && maxPointType > 0) maxLineWidth = maxPointSize;
            if (minLineWidth < minPointSize && minPointType > 0) minLineWidth = minPointSize;
            heightDiagram -= (maxLineWidth + minLineWidth) / 2;


            int high = -300;
            int low = 300;
            if (daysCount > forecastData.Count) daysCount = forecastData.Count;

            for(int index = 0; index< daysCount; index++)
            {
                if (index < forecastData.Count)
                {
                    ForecastData item = forecastData[index];
                    if (item.high > high) high = item.high;
                    if (item.low < low) low = item.low; 
                }
            }

            //foreach (ForecastData item in forecastData)
            //{
            //    if (item.high > high) high = item.high;
            //    if (item.low < low) low = item.low;
            //}
            float delta = high - low;
            float scale = heightDiagram / delta;
            max_temp = high;
            //Logger.WriteLine(string.Format("scale = {0}, high = {1}, low = {2}, delta = {3}, heightDiagram = {4}", scale, high, low, delta, heightDiagram));

            Logger.WriteLine("* DiagramScale (End)");

            return scale;
        }

        private void DrawaWeatherPoint(Graphics gPanel, int x, int y, int pointSize, int pointType, Color color)
        {
            if (pointSize == 0) return;
            int fillX = x - pointSize / 4;
            int fillY = y - pointSize / 4;
            x -= pointSize / 2;
            y -= pointSize / 2;

            switch (pointType)
            {
                case 1:
                    gPanel.FillRectangle(new SolidBrush(color), x, y, pointSize, pointSize);
                    break;
                case 2:
                    gPanel.FillRectangle(new SolidBrush(color), x, y, pointSize, pointSize);
                    gPanel.FillRectangle(new SolidBrush(Color.White), fillX, fillY, pointSize/2, pointSize/2);
                    break;
                case 3:
                    gPanel.FillEllipse(new SolidBrush(color), x, y, pointSize, pointSize);
                    break;
                case 4:
                    gPanel.FillEllipse(new SolidBrush(color), x, y, pointSize, pointSize);
                    gPanel.FillEllipse(new SolidBrush(Color.White), fillX, fillY, pointSize / 2, pointSize / 2);
                    break;
            }
        }
        
        /// <summary>Рисуем восход, звкат</summary>
        private void DrawSunrise(Graphics gPanel, hmUI_widget_IMG_LEVEL images, hmUI_widget_IMG_PROGRESS segments,
            hmUI_widget_IMG_NUMBER sunrise, hmUI_widget_TEXT sunrise_font, hmUI_widget_IMG_NUMBER sunrise_rotation, Text_Circle sunrise_circle,
            hmUI_widget_IMG_NUMBER sunset, hmUI_widget_TEXT sunset_font, hmUI_widget_IMG_NUMBER sunset_rotation, Text_Circle sunset_circle, 
            hmUI_widget_IMG_NUMBER sunset_sunrise, hmUI_widget_IMG_POINTER pointer,
            hmUI_widget_IMG icon, int hour, int minute, bool BBorder, bool showProgressArea, bool showCentrHend)
        {
            TimeSpan time_now = new TimeSpan(hour, minute, 0);
            TimeSpan time_sunrise = new TimeSpan(3, 30, 0);
            TimeSpan time_sunset = new TimeSpan(20, 30, 0);
            TimeSpan day_lenght = time_sunset - time_sunrise;
            TimeSpan day_progress = time_now - time_sunrise;

            bool sun = false;
            if(time_now >= time_sunrise && time_now <= time_sunset) sun = true;

            float progress = (float)(day_progress.TotalSeconds / day_lenght.TotalSeconds);
            if (progress > 1) progress = 1;
            if (progress < 0) progress = 0;
            Bitmap src = new Bitmap(1, 1);

            for (int index = 1; index <= 15; index++)
            {
                if (sun && images != null && images.img_First != null && images.img_First.Length > 0 &&
                    index == images.position && images.visible)
                {
                    int imgCount = images.image_length;
                    int valueImgIndex = (int)((imgCount - 1) * progress);
                    if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);

                    if (valueImgIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(images.img_First);
                        int x = images.X;
                        int y = images.Y;
                        imageIndex = imageIndex + valueImgIndex;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                            {
                                int w = src.Width;
                                int h = src.Height;
                                // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                                ColorMatrix colorMatrix = new ColorMatrix();
                                colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                                // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                                ImageAttributes imgAttributes = new ImageAttributes();
                                imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                                // Указываем прямоугольник, куда будет помещено изображение
                                Rectangle rect_alpha = new Rectangle(x, y, w, h);
                                gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                            }
                            else gPanel.DrawImage(src, x, y);
                        }
                    }
                }

                if (sun && segments != null && segments.img_First != null && segments.img_First.Length > 0 &&
                    index == segments.position && segments.visible)
                {
                    int segmentCount = segments.image_length;
                    int valueSegmentIndex = (int)((segmentCount - 1) * progress);
                    if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);

                    if (valueSegmentIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(segments.img_First);
                        for (int i = 0; i <= valueSegmentIndex; i++)
                        {
                            int imgIndex = imageIndex + i;

                            if (imgIndex < ListImagesFullName.Count && i < segments.X.Count)
                            {
                                int x = segments.X[i];
                                int y = segments.Y[i];
                                src = OpenFileStream(ListImagesFullName[imgIndex]);
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }
                    }
                }

                if (sunrise != null && sunrise.img_First != null && sunrise.img_First.Length > 0 &&
                    index == sunrise.position && sunrise.visible)
                {
                    //float sunrise_value = 5.30f;
                    float sunrise_value = (float)Math.Round(time_sunrise.Hours + time_sunrise.Minutes / 100f, 2);
                    int image_Index = ListImages.IndexOf(sunrise.img_First);
                    int pos_x = sunrise.imageX;
                    int pos_y = sunrise.imageY;
                    int sunrise_spasing = sunrise.space;
                    int angl = sunrise.angle;
                    int alpha = sunrise.alpha;
                    int sunrise_alignment = AlignmentToInt(sunrise.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunrise_addZero = true;
                    int sunrise_separator_index = -1;
                    if (sunrise.unit != null && sunrise.unit.Length > 0)
                        sunrise_separator_index = ListImages.IndexOf(sunrise.unit);
                    int decumalPoint_index = -1;
                    if (sunrise.dot_image != null && sunrise.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunrise.dot_image);

                    Draw_dagital_text_decimal(gPanel, image_Index, pos_x, pos_y,
                        sunrise_spasing, sunrise_alignment, sunrise_value, sunrise_addZero, 4,
                        sunrise_separator_index, decumalPoint_index, 2, angl, alpha, BBorder,  "ElementSunrise");

                    if (sunrise.icon != null && sunrise.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(sunrise.icon);
                        pos_x = sunrise.iconPosX;
                        pos_y = sunrise.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        if (SelectedModel.versionOS >= 2.1 && sunrise.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = sunrise.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(pos_x, pos_y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, pos_x, pos_y);
                    }
                }

                if (sunrise_font != null && index == sunrise_font.position && sunrise_font.visible)
                {
                    int x = sunrise_font.x;
                    int y = sunrise_font.y;
                    int h = sunrise_font.h;
                    int w = sunrise_font.w;

                    int size = sunrise_font.text_size;
                    int space_h = sunrise_font.char_space;
                    int space_v = sunrise_font.line_space;

                    Color color = StringToColor(sunrise_font.color);
                    int alpha = sunrise_font.alpha;
                    string align_h = sunrise_font.align_h;
                    string align_v = sunrise_font.align_v;
                    string text_style = sunrise_font.text_style;
                    //string valueStr = value.ToString();
                    string valueStr = "03:30";

                    if (sunrise_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (sunrise_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (sunrise_font.font != null && sunrise_font.font.Length > 3 && FontsList.ContainsKey(sunrise_font.font))
                    {
                        string font_fileName = FontsList[sunrise_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + sunrise_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (sunrise_rotation != null && sunrise_rotation.img_First != null && sunrise_rotation.img_First.Length > 0 &&
                    sunrise_rotation.dot_image != null && sunrise_rotation.dot_image.Length > 0 && index == sunrise_rotation.position && sunrise_rotation.visible)
                {
                    int pos_x = sunrise_rotation.imageX;
                    int pos_y = sunrise_rotation.imageY;
                    int spacing = sunrise_rotation.space;
                    float angle = sunrise_rotation.angle;
                    bool addZero = sunrise_rotation.zero;
                    int image_index = ListImages.IndexOf(sunrise_rotation.img_First);
                    int unit_index = ListImages.IndexOf(sunrise_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(sunrise_rotation.dot_image);
                    string horizontal_alignment = sunrise_rotation.align;
                    bool unit_in_alignment = sunrise_rotation.unit_in_alignment;

                    float sunrise_value = (float)Math.Round(time_sunrise.Hours + time_sunrise.Minutes / 100f, 2);
                    string value = sunrise_value.ToString();
                    //string value = 5.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunrise_rotation.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        value, 4, BBorder, -1, -1, false, "ElementSunrise");
                }

                if (sunrise_circle != null && sunrise_circle.img_First != null && sunrise_circle.img_First.Length > 0 &&
                    sunrise_circle.dot_image != null && sunrise_circle.dot_image.Length > 0 && index == sunrise_circle.position && sunrise_circle.visible)
                {
                    int centr_x = sunrise_circle.circle_center_X;
                    int centr_y = sunrise_circle.circle_center_Y;
                    int radius = sunrise_circle.radius;
                    int spacing = sunrise_circle.char_space_angle;
                    float angle = sunrise_circle.angle;
                    bool addZero = sunrise_circle.zero;
                    int image_index = ListImages.IndexOf(sunrise_circle.img_First);
                    int unit_index = ListImages.IndexOf(sunrise_circle.unit);
                    int dot_image_index = ListImages.IndexOf(sunrise_circle.dot_image);
                    string vertical_alignment = sunrise_circle.vertical_alignment;
                    string horizontal_alignment = sunrise_circle.horizontal_alignment;
                    bool reverse_direction = sunrise_circle.reverse_direction;
                    bool unit_in_alignment = sunrise_circle.unit_in_alignment;

                    float sunrise_value = (float)Math.Round(time_sunrise.Hours + time_sunrise.Minutes / 100f, 2);
                    string value = sunrise_value.ToString();
                    //string value = 5.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunrise_circle.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        value, 4, BBorder, showCentrHend, -1, -1, false, "ElementSunrise");
                }

                if (sunset != null && sunset.img_First != null && sunset.img_First.Length > 0 &&
                    index == sunset.position && sunset.visible)
                {
                    float sunset_value = (float)Math.Round(time_sunset.Hours + time_sunset.Minutes / 100f, 2);
                    //float sunset_value = 19.30f;
                    int image_Index = ListImages.IndexOf(sunset.img_First);
                    int pos_x = sunset.imageX;
                    int pos_y = sunset.imageY;
                    int sunset_spasing = sunset.space;
                    int angl = sunset.angle;
                    int alpha = sunset.alpha;
                    int sunset_alignment = AlignmentToInt(sunset.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunset_addZero = true;
                    int sunset_separator_index = -1;
                    if (sunset.unit != null && sunset.unit.Length > 0)
                        sunset_separator_index = ListImages.IndexOf(sunset.unit);
                    int decumalPoint_index = -1;
                    if (sunset.dot_image != null && sunset.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunset.dot_image);

                    Draw_dagital_text_decimal(gPanel, image_Index, pos_x, pos_y,
                        sunset_spasing, sunset_alignment, sunset_value, sunset_addZero, 4,
                        sunset_separator_index, decumalPoint_index, 2, angl, alpha, BBorder, "ElementSunrise");

                    if (sunset.icon != null && sunset.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(sunset.icon);
                        pos_x = sunset.iconPosX;
                        pos_y = sunset.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        if (SelectedModel.versionOS >= 2.1 && sunset.icon_alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = sunset.icon_alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(pos_x, pos_y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, pos_x, pos_y);
                    }
                }

                if (sunset_font != null && index == sunset_font.position && sunset_font.visible)
                {
                    int x = sunset_font.x;
                    int y = sunset_font.y;
                    int h = sunset_font.h;
                    int w = sunset_font.w;

                    int size = sunset_font.text_size;
                    int space_h = sunset_font.char_space;
                    int space_v = sunset_font.line_space;

                    Color color = StringToColor(sunset_font.color);
                    int alpha = sunset_font.alpha;
                    string align_h = sunset_font.align_h;
                    string align_v = sunset_font.align_v;
                    string text_style = sunset_font.text_style;
                    //string valueStr = value.ToString();
                    string valueStr = "20:30";

                    if (sunset_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (sunset_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (sunset_font.font != null && sunset_font.font.Length > 3 && FontsList.ContainsKey(sunset_font.font))
                    {
                        string font_fileName = FontsList[sunset_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + sunset_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (sunset_rotation != null && sunset_rotation.img_First != null && sunset_rotation.img_First.Length > 0 &&
                    sunset_rotation.dot_image != null && sunset_rotation.dot_image.Length > 0 && index == sunset_rotation.position && sunset_rotation.visible)
                {
                    int pos_x = sunset_rotation.imageX;
                    int pos_y = sunset_rotation.imageY;
                    int spacing = sunset_rotation.space;
                    float angle = sunset_rotation.angle;
                    bool addZero = sunset_rotation.zero;
                    int image_index = ListImages.IndexOf(sunset_rotation.img_First);
                    int unit_index = ListImages.IndexOf(sunset_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(sunset_rotation.dot_image);
                    string horizontal_alignment = sunset_rotation.align;
                    bool unit_in_alignment = sunset_rotation.unit_in_alignment;

                    float sunset_value = (float)Math.Round(time_sunset.Hours + time_sunset.Minutes / 100f, 2);
                    string value = sunset_value.ToString();
                    //string value = 19.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunset_rotation.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        value, 4, BBorder, -1, -1, false, "ElementSunrise");
                }

                if (sunset_circle != null && sunset_circle.img_First != null && sunset_circle.img_First.Length > 0 &&
                    sunset_circle.dot_image != null && sunset_circle.dot_image.Length > 0 && index == sunset_circle.position && sunset_circle.visible)
                {
                    int centr_x = sunset_circle.circle_center_X;
                    int centr_y = sunset_circle.circle_center_Y;
                    int radius = sunset_circle.radius;
                    int spacing = sunset_circle.char_space_angle;
                    float angle = sunset_circle.angle;
                    bool addZero = sunset_circle.zero;
                    int image_index = ListImages.IndexOf(sunset_circle.img_First);
                    int unit_index = ListImages.IndexOf(sunset_circle.unit);
                    int dot_image_index = ListImages.IndexOf(sunset_circle.dot_image);
                    string vertical_alignment = sunset_circle.vertical_alignment;
                    string horizontal_alignment = sunset_circle.horizontal_alignment;
                    bool reverse_direction = sunset_circle.reverse_direction;
                    bool unit_in_alignment = sunset_circle.unit_in_alignment;

                    float sunset_value = (float)Math.Round(time_sunset.Hours + time_sunset.Minutes / 100f, 2);
                    string value = sunset_value.ToString();
                    //string value = 19.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunset_circle.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        value, 4, BBorder, showCentrHend, -1, -1, false, "ElementSunrise");
                }

                if (sunset_sunrise != null && sunset_sunrise.img_First != null && sunset_sunrise.img_First.Length > 0 &&
                    index == sunset_sunrise.position && sunset_sunrise.visible)
                {
                    float sunset_sunrise_value = 5.30f;
                    if (time_now > time_sunrise && time_now < time_sunset) sunset_sunrise_value = 19.30f;
                    int image_Index = ListImages.IndexOf(sunset_sunrise.img_First);
                    int pos_x = sunset_sunrise.imageX;
                    int pos_y = sunset_sunrise.imageY;
                    int sunset_sunrise_spasing = sunset_sunrise.space;
                    int angl = sunset_sunrise.angle;
                    int alpha = sunset_sunrise.alpha;
                    int sunset_sunrise_alignment = AlignmentToInt(sunset_sunrise.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunset_sunrise_addZero = true;
                    int sunset_sunrise_separator_index = -1;
                    if (sunset_sunrise.unit != null && sunset_sunrise.unit.Length > 0)
                        sunset_sunrise_separator_index = ListImages.IndexOf(sunset_sunrise.unit);
                    int decumalPoint_index = -1;
                    if (sunset_sunrise.dot_image != null && sunset_sunrise.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunset_sunrise.dot_image);

                    Draw_dagital_text_decimal(gPanel, image_Index, pos_x, pos_y,
                        sunset_sunrise_spasing, sunset_sunrise_alignment, sunset_sunrise_value, sunset_sunrise_addZero, 4,
                        sunset_sunrise_separator_index, decumalPoint_index, 2, angl, alpha, BBorder, "ElementSunrise");

                    if (sunset_sunrise.icon != null && sunset_sunrise.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(sunset_sunrise.icon);
                        pos_x = sunset_sunrise.iconPosX;
                        pos_y = sunset_sunrise.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        if (SelectedModel.versionOS >= 2.1 && sunset_sunrise.icon_alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = sunset_sunrise.icon_alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(pos_x, pos_y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, pos_x, pos_y);
                    }
                }

                if (sun && pointer != null && pointer.src != null && pointer.src.Length > 0 &&
                    index == pointer.position && pointer.visible)
                {
                    int x = pointer.center_x;
                    int y = pointer.center_y;
                    int offsetX = pointer.pos_x;
                    int offsetY = pointer.pos_y;
                    int startAngle = pointer.start_angle;
                    int endAngle = pointer.end_angle;
                    int image_index = ListImages.IndexOf(pointer.src);

                    float angle = startAngle + progress * (endAngle - startAngle);

                    if (pointer.scale != null && pointer.scale.Length > 0)
                    {
                        int image_index_scale = ListImages.IndexOf(pointer.scale);
                        int x_scale = pointer.scale_x;
                        int y_scale = pointer.scale_y;

                        src = OpenFileStream(ListImagesFullName[image_index_scale]);
                        gPanel.DrawImage(src, x_scale, y_scale);
                    }

                    DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                    if (pointer.cover_path != null && pointer.cover_path.Length > 0)
                    {
                        image_index = ListImages.IndexOf(pointer.cover_path);
                        x = pointer.cover_x;
                        y = pointer.cover_y;

                        src = OpenFileStream(ListImagesFullName[image_index]);
                        gPanel.DrawImage(src, x, y);
                    }
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, x, y);
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем восход, звкат луны</summary>
        private void DrawMoon(Graphics gPanel, hmUI_widget_IMG_LEVEL images, /*hmUI_widget_IMG_PROGRESS segments,*/
            hmUI_widget_IMG_NUMBER sunrise, hmUI_widget_TEXT sunrise_font, hmUI_widget_IMG_NUMBER sunrise_rotation, Text_Circle sunrise_circle,
            hmUI_widget_IMG_NUMBER sunset, hmUI_widget_TEXT sunset_font, hmUI_widget_IMG_NUMBER sunset_rotation, Text_Circle sunset_circle,
            hmUI_widget_IMG_NUMBER sunset_sunrise, hmUI_widget_IMG_POINTER pointer,
            hmUI_widget_IMG icon, int hour, int minute, bool BBorder, bool showProgressArea, bool showCentrHend)
        {
            TimeSpan time_now = new TimeSpan(hour, minute, 0);
            if (time_now <= new TimeSpan(5, 30, 0)) time_now = new TimeSpan(hour + 24, minute, 0);
            TimeSpan time_sunrise = new TimeSpan(18, 30, 0);
            TimeSpan time_sunset = new TimeSpan(24 + 5, 30, 0);
            TimeSpan day_lenght = time_sunset - time_sunrise;
            TimeSpan day_progress = time_now - time_sunrise;

            bool moon = false;
            if (time_now >= time_sunrise && time_now <= time_sunset) moon = true;

            float progress = (float)(day_progress.TotalSeconds / day_lenght.TotalSeconds);
            if (progress > 1) progress = 1;
            if (progress < 0) progress = 0;
            Bitmap src = new Bitmap(1, 1);

            for (int index = 1; index <= 15; index++)
            {
                if (images != null && images.img_First != null && images.img_First.Length > 0 &&
                    index == images.position && images.visible)
                {
                    int year = WatchFacePreviewSet.Date.Year;
                    int month = WatchFacePreviewSet.Date.Month;
                    int day = WatchFacePreviewSet.Date.Day;
                    double moon_age = MoonAge(day, month, year);
                    //int moonPhase = (int)(8 * moon_age / 29);

                    int imgCount = images.image_length;
                    int valueImgIndex = (int)Math.Round((imgCount - 1) * moon_age / 29);
                    //valueImgIndex = (int)Math.Round((imgCount - 1) * moon_age / 29.53f);
                    //valueImgIndex = moonPhase - 1;
                    if (valueImgIndex < 0) valueImgIndex = (int)(imgCount - 1);
                    if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);

                    if (valueImgIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(images.img_First);
                        int x = images.X;
                        int y = images.Y;
                        imageIndex = imageIndex + valueImgIndex;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            gPanel.DrawImage(src, x, y);
                            //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                        }
                    }
                }

                if (sunrise != null && sunrise.img_First != null && sunrise.img_First.Length > 0 &&
                    index == sunrise.position && sunrise.visible)
                {
                    //float sunrise_value = 5.30f;
                    float sunrise_value = (float)Math.Round(time_sunrise.Hours + time_sunrise.Minutes / 100f, 2);
                    int image_Index = ListImages.IndexOf(sunrise.img_First);
                    int pos_x = sunrise.imageX;
                    int pos_y = sunrise.imageY;
                    int sunrise_spasing = sunrise.space;
                    int angl = sunrise.angle;
                    int alpha = sunrise.alpha;
                    int sunrise_alignment = AlignmentToInt(sunrise.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunrise_addZero = true;
                    int sunrise_separator_index = -1;
                    if (sunrise.unit != null && sunrise.unit.Length > 0)
                        sunrise_separator_index = ListImages.IndexOf(sunrise.unit);
                    int decumalPoint_index = -1;
                    if (sunrise.dot_image != null && sunrise.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunrise.dot_image);

                    Draw_dagital_text_decimal(gPanel, image_Index, pos_x, pos_y,
                        sunrise_spasing, sunrise_alignment, sunrise_value, sunrise_addZero, 4,
                        sunrise_separator_index, decumalPoint_index, 2, angl, alpha, BBorder, "ElementSunrise");

                    if (sunrise.icon != null && sunrise.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(sunrise.icon);
                        pos_x = sunrise.iconPosX;
                        pos_y = sunrise.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        gPanel.DrawImage(src, pos_x, pos_y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

                if (sunrise_font != null && index == sunrise_font.position && sunrise_font.visible)
                {
                    int x = sunrise_font.x;
                    int y = sunrise_font.y;
                    int h = sunrise_font.h;
                    int w = sunrise_font.w;

                    int size = sunrise_font.text_size;
                    int space_h = sunrise_font.char_space;
                    int space_v = sunrise_font.line_space;

                    Color color = StringToColor(sunrise_font.color);
                    int alpha = sunrise_font.alpha;
                    string align_h = sunrise_font.align_h;
                    string align_v = sunrise_font.align_v;
                    string text_style = sunrise_font.text_style;
                    //string valueStr = value.ToString();
                    string valueStr = "18:30";

                    if (sunrise_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (sunrise_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (sunrise_font.font != null && sunrise_font.font.Length > 3 && FontsList.ContainsKey(sunrise_font.font))
                    {
                        string font_fileName = FontsList[sunrise_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + sunrise_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (sunrise_rotation != null && sunrise_rotation.img_First != null && sunrise_rotation.img_First.Length > 0 &&
                    sunrise_rotation.dot_image != null && sunrise_rotation.dot_image.Length > 0 && index == sunrise_rotation.position && sunrise_rotation.visible)
                {
                    int pos_x = sunrise_rotation.imageX;
                    int pos_y = sunrise_rotation.imageY;
                    int spacing = sunrise_rotation.space;
                    float angle = sunrise_rotation.angle;
                    bool addZero = sunrise_rotation.zero;
                    int image_index = ListImages.IndexOf(sunrise_rotation.img_First);
                    int unit_index = ListImages.IndexOf(sunrise_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(sunrise_rotation.dot_image);
                    string horizontal_alignment = sunrise_rotation.align;
                    bool unit_in_alignment = sunrise_rotation.unit_in_alignment;

                    float sunrise_value = (float)Math.Round(time_sunrise.Hours + time_sunrise.Minutes / 100f, 2);
                    string value = sunrise_value.ToString();
                    //string value = 5.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunrise_rotation.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        value, 4, BBorder, -1, -1, false, "ElementSunrise");
                }

                if (sunrise_circle != null && sunrise_circle.img_First != null && sunrise_circle.img_First.Length > 0 &&
                    sunrise_circle.dot_image != null && sunrise_circle.dot_image.Length > 0 && index == sunrise_circle.position && sunrise_circle.visible)
                {
                    int centr_x = sunrise_circle.circle_center_X;
                    int centr_y = sunrise_circle.circle_center_Y;
                    int radius = sunrise_circle.radius;
                    int spacing = sunrise_circle.char_space_angle;
                    float angle = sunrise_circle.angle;
                    bool addZero = sunrise_circle.zero;
                    int image_index = ListImages.IndexOf(sunrise_circle.img_First);
                    int unit_index = ListImages.IndexOf(sunrise_circle.unit);
                    int dot_image_index = ListImages.IndexOf(sunrise_circle.dot_image);
                    string vertical_alignment = sunrise_circle.vertical_alignment;
                    string horizontal_alignment = sunrise_circle.horizontal_alignment;
                    bool reverse_direction = sunrise_circle.reverse_direction;
                    bool unit_in_alignment = sunrise_circle.unit_in_alignment;

                    float sunrise_value = (float)Math.Round(time_sunrise.Hours + time_sunrise.Minutes / 100f, 2);
                    string value = sunrise_value.ToString();
                    //string value = 5.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunrise_circle.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        value, 4, BBorder, showCentrHend, -1, -1, false, "ElementSunrise");
                }

                if (sunset != null && sunset.img_First != null && sunset.img_First.Length > 0 &&
                    index == sunset.position && sunset.visible)
                {
                    float sunset_value = (float)Math.Round(time_sunset.Hours + time_sunset.Minutes / 100f, 2);
                    //float sunset_value = 19.30f;
                    int image_Index = ListImages.IndexOf(sunset.img_First);
                    int pos_x = sunset.imageX;
                    int pos_y = sunset.imageY;
                    int sunset_spasing = sunset.space;
                    int angl = sunset.angle;
                    int alpha = sunset.alpha;
                    int sunset_alignment = AlignmentToInt(sunset.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunset_addZero = true;
                    int sunset_separator_index = -1;
                    if (sunset.unit != null && sunset.unit.Length > 0)
                        sunset_separator_index = ListImages.IndexOf(sunset.unit);
                    int decumalPoint_index = -1;
                    if (sunset.dot_image != null && sunset.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunset.dot_image);

                    Draw_dagital_text_decimal(gPanel, image_Index, pos_x, pos_y,
                        sunset_spasing, sunset_alignment, sunset_value, sunset_addZero, 4,
                        sunset_separator_index, decumalPoint_index, 2, angl, alpha, BBorder, "ElementSunrise");

                    if (sunset.icon != null && sunset.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(sunset.icon);
                        pos_x = sunset.iconPosX;
                        pos_y = sunset.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        gPanel.DrawImage(src, pos_x, pos_y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

                if (sunset_font != null && index == sunset_font.position && sunset_font.visible)
                {
                    int x = sunset_font.x;
                    int y = sunset_font.y;
                    int h = sunset_font.h;
                    int w = sunset_font.w;

                    int size = sunset_font.text_size;
                    int space_h = sunset_font.char_space;
                    int space_v = sunset_font.line_space;

                    Color color = StringToColor(sunset_font.color);
                    int alpha = sunset_font.alpha;
                    string align_h = sunset_font.align_h;
                    string align_v = sunset_font.align_v;
                    string text_style = sunset_font.text_style;
                    //string valueStr = value.ToString();
                    string valueStr = "05:30";

                    if (sunset_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (sunset_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (sunset_font.font != null && sunset_font.font.Length > 3 && FontsList.ContainsKey(sunset_font.font))
                    {
                        string font_fileName = FontsList[sunset_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + sunset_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (sunset_rotation != null && sunset_rotation.img_First != null && sunset_rotation.img_First.Length > 0 &&
                    sunset_rotation.dot_image != null && sunset_rotation.dot_image.Length > 0 && index == sunset_rotation.position && sunset_rotation.visible)
                {
                    int pos_x = sunset_rotation.imageX;
                    int pos_y = sunset_rotation.imageY;
                    int spacing = sunset_rotation.space;
                    float angle = sunset_rotation.angle;
                    bool addZero = sunset_rotation.zero;
                    int image_index = ListImages.IndexOf(sunset_rotation.img_First);
                    int unit_index = ListImages.IndexOf(sunset_rotation.unit);
                    int dot_image_index = ListImages.IndexOf(sunset_rotation.dot_image);
                    string horizontal_alignment = sunset_rotation.align;
                    bool unit_in_alignment = sunset_rotation.unit_in_alignment;

                    float sunset_value = (float)Math.Round(time_sunset.Hours + time_sunset.Minutes / 100f, 2);
                    string value = sunset_value.ToString();
                    //string value = 19.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunset_rotation.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_rotate(gPanel, pos_x, pos_y, spacing, angle, addZero,
                        image_index, unit_index, dot_image_index, horizontal_alignment, unit_in_alignment,
                        value, 4, BBorder, -1, -1, false, "ElementSunrise");
                }

                if (sunset_circle != null && sunset_circle.img_First != null && sunset_circle.img_First.Length > 0 &&
                    sunset_circle.dot_image != null && sunset_circle.dot_image.Length > 0 && index == sunset_circle.position && sunset_circle.visible)
                {
                    int centr_x = sunset_circle.circle_center_X;
                    int centr_y = sunset_circle.circle_center_Y;
                    int radius = sunset_circle.radius;
                    int spacing = sunset_circle.char_space_angle;
                    float angle = sunset_circle.angle;
                    bool addZero = sunset_circle.zero;
                    int image_index = ListImages.IndexOf(sunset_circle.img_First);
                    int unit_index = ListImages.IndexOf(sunset_circle.unit);
                    int dot_image_index = ListImages.IndexOf(sunset_circle.dot_image);
                    string vertical_alignment = sunset_circle.vertical_alignment;
                    string horizontal_alignment = sunset_circle.horizontal_alignment;
                    bool reverse_direction = sunset_circle.reverse_direction;
                    bool unit_in_alignment = sunset_circle.unit_in_alignment;

                    float sunset_value = (float)Math.Round(time_sunset.Hours + time_sunset.Minutes / 100f, 2);
                    string value = sunset_value.ToString();
                    //string value = 19.30f.ToString();
                    string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    if (value.IndexOf(decimalSeparator) < 0) value = value + decimalSeparator;
                    while (value.IndexOf(decimalSeparator) > value.Length - 2 - 1)
                    {
                        value = value + "0";
                    }
                    if (sunset_circle.zero) value = value.PadLeft(5, '0');

                    Draw_dagital_text_on_circle(gPanel, centr_x, centr_y, radius, spacing, angle, addZero,
                        image_index, /*int image_width, int image_height,*/ unit_index, /*int unit_width,*/ dot_image_index, /*int dot_image_width,*/
                        vertical_alignment, horizontal_alignment, reverse_direction, unit_in_alignment,
                        value, 4, BBorder, showCentrHend, -1, -1, false, "ElementSunrise");
                }

                if (sunset_sunrise != null && sunset_sunrise.img_First != null && sunset_sunrise.img_First.Length > 0 &&
                    index == sunset_sunrise.position && sunset_sunrise.visible)
                {
                    float sunset_sunrise_value = 5.30f;
                    if (time_now > time_sunrise && time_now < time_sunset) sunset_sunrise_value = 18.30f;
                    int image_Index = ListImages.IndexOf(sunset_sunrise.img_First);
                    int pos_x = sunset_sunrise.imageX;
                    int pos_y = sunset_sunrise.imageY;
                    int sunset_sunrise_spasing = sunset_sunrise.space;
                    int angl = sunset_sunrise.angle;
                    int alpha = sunset_sunrise.alpha;
                    int sunset_sunrise_alignment = AlignmentToInt(sunset_sunrise.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunset_sunrise_addZero = true;
                    int sunset_sunrise_separator_index = -1;
                    if (sunset_sunrise.unit != null && sunset_sunrise.unit.Length > 0)
                        sunset_sunrise_separator_index = ListImages.IndexOf(sunset_sunrise.unit);
                    int decumalPoint_index = -1;
                    if (sunset_sunrise.dot_image != null && sunset_sunrise.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunset_sunrise.dot_image);

                    Draw_dagital_text_decimal(gPanel, image_Index, pos_x, pos_y,
                        sunset_sunrise_spasing, sunset_sunrise_alignment, sunset_sunrise_value, sunset_sunrise_addZero, 4,
                        sunset_sunrise_separator_index, decumalPoint_index, 2, angl, alpha, BBorder, "ElementSunrise");

                    if (sunset_sunrise.icon != null && sunset_sunrise.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(sunset_sunrise.icon);
                        pos_x = sunset_sunrise.iconPosX;
                        pos_y = sunset_sunrise.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        gPanel.DrawImage(src, pos_x, pos_y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

                if (moon && pointer != null && pointer.src != null && pointer.src.Length > 0 &&
                    index == pointer.position && pointer.visible)
                {
                    int x = pointer.center_x;
                    int y = pointer.center_y;
                    int offsetX = pointer.pos_x;
                    int offsetY = pointer.pos_y;
                    int startAngle = pointer.start_angle;
                    int endAngle = pointer.end_angle;
                    int image_index = ListImages.IndexOf(pointer.src);

                    float angle = startAngle + progress * (endAngle - startAngle);

                    if (pointer.scale != null && pointer.scale.Length > 0)
                    {
                        int image_index_scale = ListImages.IndexOf(pointer.scale);
                        int x_scale = pointer.scale_x;
                        int y_scale = pointer.scale_y;

                        src = OpenFileStream(ListImagesFullName[image_index_scale]);
                        gPanel.DrawImage(src, x_scale, y_scale);
                    }

                    DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                    if (pointer.cover_path != null && pointer.cover_path.Length > 0)
                    {
                        image_index = ListImages.IndexOf(pointer.cover_path);
                        x = pointer.cover_x;
                        y = pointer.cover_y;

                        src = OpenFileStream(ListImagesFullName[image_index]);
                        gPanel.DrawImage(src, x, y);
                    }
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, x, y);
                    }
                }


            }

            src.Dispose();
        }

        /// <summary>Рисуем все параметры элемента барометра</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="number">Параметры цифрового значения</param>
        /// <param name="number_font">Параметры отображения данных шрифтом</param>
        /// <param name="numberAltitude">Параметры цифрового значения цели</param>
        /// <param name="numberAltitude_font">Параметры отображения цели шрифтом</param>
        /// <param name="pointer">Параметры для стрелочного указателя</param>
        /// <param name="icon">Параметры для иконки</param>
        /// <param name="value">Значение показателя</param>
        /// <param name="value_altitude">Значение цели для показателя</param>
        /// <param name="progress">Прогресс показателя</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showProgressArea">Подсвечивать круговую шкалу при наличии фонового изображения</param>
        /// <param name="showCentrHend">Подсвечивать центр стрелки</param>
        /// <param name="elementName">Имя отображаемого элемента</param>
        private void DrawAltimeter(Graphics gPanel, hmUI_widget_IMG_NUMBER number, hmUI_widget_TEXT number_font, hmUI_widget_IMG_NUMBER numberAltitude,
            hmUI_widget_TEXT numberAltitude_font, hmUI_widget_IMG_POINTER pointer, hmUI_widget_IMG icon, float value, int value_altitude, float progress,
            bool BBorder, bool showProgressArea, bool showCentrHend)
        {
            if (progress < 0) progress = 0;
            if (progress > 1) progress = 1;
            Bitmap src = new Bitmap(1, 1);
            //string unit = "hpa";

            for (int index = 1; index <= 25; index++)
            {
                if (number != null && number.img_First != null && number.img_First.Length > 0 &&
                    index == number.position && number.visible)
                {
                    int imageIndex = ListImages.IndexOf(number.img_First);
                    int x = number.imageX;
                    int y = number.imageY;
                    int spasing = number.space;
                    int angle = number.angle;
                    int alignment = AlignmentToInt(number.align);
                    bool addZero = number.zero;
                    int alpha = number.alpha;
                    int separator_index = -1;
                    if (number.unit != null && number.unit.Length > 0)
                        separator_index = ListImages.IndexOf(number.unit);

                    Draw_dagital_text(gPanel, imageIndex, x, y,
                        spasing, alignment, (int)value, alpha, addZero, 4, separator_index, angle, BBorder, "ElementAltimeter");

                    if (number.icon != null && number.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(number.icon);
                        x = number.iconPosX;
                        y = number.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

                if (number_font != null && index == number_font.position && number_font.visible)
                {
                    int x = number_font.x;
                    int y = number_font.y;
                    int h = number_font.h;
                    int w = number_font.w;

                    int size = number_font.text_size;
                    int space_h = number_font.char_space;
                    int space_v = number_font.line_space;

                    Color color = StringToColor(number_font.color);
                    int alpha = number_font.alpha;
                    string align_h = number_font.align_h;
                    string align_v = number_font.align_v;
                    string text_style = number_font.text_style;
                    string valueStr = value.ToString();
                    string unitStr = "hpa";
                    if (number_font.padding) valueStr = valueStr.PadLeft(4, '0');
                    if (number_font.unit_type > 0)
                    {
                        if (number_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }

                    if (number_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (number_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                    {
                        string font_fileName = FontsList[number_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (numberAltitude != null && numberAltitude.img_First != null && numberAltitude.img_First.Length > 0 &&
                    index == numberAltitude.position && numberAltitude.visible)
                {
                    int imageIndex = ListImages.IndexOf(numberAltitude.img_First);
                    int x = numberAltitude.imageX;
                    int y = numberAltitude.imageY;
                    int spasing = numberAltitude.space;
                    int angle = numberAltitude.angle;
                    int alignment = AlignmentToInt(numberAltitude.align);
                    bool addZero = numberAltitude.zero;
                    int separator_index = -1;
                    if (numberAltitude.unit != null && numberAltitude.unit.Length > 0)
                        separator_index = ListImages.IndexOf(numberAltitude.unit);
                    int imageMinus_index = -1;
                    if (numberAltitude.negative_image != null && numberAltitude.negative_image.Length > 0)
                        imageMinus_index = ListImages.IndexOf(numberAltitude.negative_image);

                    //Draw_dagital_text(gPanel, imageIndex, x, y, spasing, alignment, value_altitude, addZero, 5, separator_index, angle, BBorder);
                    Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, value_altitude, 4, addZero, imageMinus_index, separator_index, angle, BBorder, -1, false);

                    if (numberAltitude.icon != null && numberAltitude.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(numberAltitude.icon);
                        x = numberAltitude.iconPosX;
                        y = numberAltitude.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

                if (numberAltitude_font != null && index == numberAltitude_font.position && numberAltitude_font.visible)
                {
                    int x = numberAltitude_font.x;
                    int y = numberAltitude_font.y;
                    int h = numberAltitude_font.h;
                    int w = numberAltitude_font.w;

                    int size = numberAltitude_font.text_size;
                    int space_h = numberAltitude_font.char_space;
                    int space_v = numberAltitude_font.line_space;

                    Color color = StringToColor(numberAltitude_font.color);
                    int alpha = numberAltitude_font.alpha;
                    string align_h = numberAltitude_font.align_h;
                    string align_v = numberAltitude_font.align_v;
                    string text_style = numberAltitude_font.text_style;
                    string valueStr = value_altitude.ToString();
                    string unitStr = "meter";
                    if (numberAltitude_font.padding)
                    {
                        if (value_altitude >= 0) valueStr = valueStr.PadLeft(5, '0');
                        else
                        {
                            int tempGoal = Math.Abs(value_altitude);
                            valueStr = tempGoal.ToString();
                            valueStr = valueStr.PadLeft(4, '0');
                            valueStr = "-" + valueStr;
                        }
                    }
                    if (numberAltitude_font.unit_type > 0)
                    {
                        if (numberAltitude_font.unit_type == 2) unitStr = unitStr.ToUpper();
                        valueStr += unitStr;
                    }

                    if (numberAltitude_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (numberAltitude_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (numberAltitude_font.font != null && numberAltitude_font.font.Length > 3 && FontsList.ContainsKey(numberAltitude_font.font))
                    {
                        string font_fileName = FontsList[numberAltitude_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + numberAltitude_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (pointer != null && pointer.src != null && pointer.src.Length > 0 &&
                    index == pointer.position && pointer.visible)
                {
                    int x = pointer.center_x;
                    int y = pointer.center_y;
                    int offsetX = pointer.pos_x;
                    int offsetY = pointer.pos_y;
                    int startAngle = pointer.start_angle;
                    int endAngle = pointer.end_angle;
                    int image_index = ListImages.IndexOf(pointer.src);

                    float angle = startAngle + progress * (endAngle - startAngle);

                    if (pointer.scale != null && pointer.scale.Length > 0)
                    {
                        int image_index_scale = ListImages.IndexOf(pointer.scale);
                        int x_scale = pointer.scale_x;
                        int y_scale = pointer.scale_y;

                        src = OpenFileStream(ListImagesFullName[image_index_scale]);
                        gPanel.DrawImage(src, x_scale, y_scale);
                    }

                    DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                    if (pointer.cover_path != null && pointer.cover_path.Length > 0)
                    {
                        image_index = ListImages.IndexOf(pointer.cover_path);
                        x = pointer.cover_x;
                        y = pointer.cover_y;

                        src = OpenFileStream(ListImagesFullName[image_index]);
                        gPanel.DrawImage(src, x, y);
                    }
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, x, y);
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем все параметры элемента ветер</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="images">Параметры для изображения</param>
        /// <param name="segments">Параметры для сегментов</param>
        /// <param name="number">Параметры цифрового значения</param>
        /// <param name="number_font">Параметры отображения данных шрифтом</param>
        /// <param name="pointer">Параметры для стрелочного указателя</param>
        /// <param name="icon">Параметры для иконки</param>
        /// <param name="value">Значение показателя</param>
        /// <param name="value_lenght">Максимальная длина для отображения значения</param>
        /// <param name="goal">Значение цели для показателя</param>
        /// <param name="progress">Прогресс показателя</param>
        /// <param name="valueImgIndex">Позиция картинки из заданного массива для отображения показателя картинками</param>
        /// <param name="valueSegmentIndex">Позиция картинки из заданного массива для отображения показателя сегментами</param>
        /// <param name="valueImgDirectionIndex">Позиция картинки для направления ветра</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showProgressArea">Подсвечивать круговую шкалу при наличии фонового изображения</param>
        /// <param name="showCentrHend">Подсвечивать центр стрелки</param>
        private void DrawWind(Graphics gPanel, hmUI_widget_IMG_LEVEL images, hmUI_widget_IMG_PROGRESS segments,
            hmUI_widget_IMG_NUMBER number, hmUI_widget_TEXT number_font, hmUI_widget_IMG_POINTER pointer, hmUI_widget_IMG_LEVEL img_direction,
            hmUI_widget_IMG icon, float value, int value_lenght, int goal, float progress, int valueImgIndex, 
            int valueSegmentIndex, int valueImgDirectionIndex, bool BBorder, bool showProgressArea, bool showCentrHend)
        {
            if (progress < 0) progress = 0;
            if (progress > 1) progress = 1;
            Bitmap src = new Bitmap(1, 1);

            for (int index = 1; index <= 15; index++)
            {
                if (images != null && images.img_First != null && images.img_First.Length > 0 &&
                    index == images.position && images.visible)
                {
                    if (valueImgIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(images.img_First);
                        int x = images.X;
                        int y = images.Y;
                        int width = 0;
                        int height = 0;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        width = src.Width;
                        height = src.Height;

                        imageIndex = imageIndex + valueImgIndex;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (width > 0 && height > 0)
                            {
                                Rectangle cropRect = new Rectangle(0, 0, width, height);
                                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                                using (Graphics g = Graphics.FromImage(target))
                                {
                                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                                     cropRect, GraphicsUnit.Pixel);
                                }
                                gPanel.DrawImage(target, x, y);
                            }
                            else
                            {
                                gPanel.DrawImage(src, x, y);

                            }
                        }
                    }
                }

                if (segments != null && segments.img_First != null && segments.img_First.Length > 0 &&
                    index == segments.position && segments.visible)
                {
                    if (valueSegmentIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(segments.img_First);
                        for (int i = 0; i <= valueSegmentIndex; i++)
                        {
                            int imgIndex = imageIndex + i;

                            if (imgIndex < ListImagesFullName.Count && i < segments.X.Count)
                            {
                                int x = segments.X[i];
                                int y = segments.Y[i];
                                src = OpenFileStream(ListImagesFullName[imgIndex]);
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                        }
                    }
                }

                if (number != null && number.img_First != null && number.img_First.Length > 0 &&
                    index == number.position && number.visible)
                {
                    int imageIndex = ListImages.IndexOf(number.img_First);
                    int x = number.imageX;
                    int y = number.imageY;
                    int spasing = number.space;
                    int angle = number.angle;
                    int alignment = AlignmentToInt(number.align);
                    bool addZero = number.zero;
                    int alpha = number.alpha;
                    int separator_index = -1;
                    if (number.unit != null && number.unit.Length > 0)
                        separator_index = ListImages.IndexOf(number.unit);

                    Draw_dagital_text(gPanel, imageIndex, x, y,
                        spasing, alignment, (int)value, alpha, addZero, value_lenght, separator_index, angle, BBorder, "ElementWind");

                    if (number.icon != null && number.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(number.icon);
                        x = number.iconPosX;
                        y = number.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

                if (number_font != null && index == number_font.position && number_font.visible)
                {
                    int x = number_font.x;
                    int y = number_font.y;
                    int h = number_font.h;
                    int w = number_font.w;

                    int size = number_font.text_size;
                    int space_h = number_font.char_space;
                    int space_v = number_font.line_space;

                    Color color = StringToColor(number_font.color);
                    int alpha = number_font.alpha;
                    string align_h = number_font.align_h;
                    string align_v = number_font.align_v;
                    string text_style = number_font.text_style;
                    string valueStr = value.ToString();

                    if (number_font.centreHorizontally)
                    {
                        x = (SelectedModel.background.w - w) / 2;
                        align_h = "CENTER_H";
                    }
                    if (number_font.centreVertically)
                    {
                        y = (SelectedModel.background.h - h) / 2;
                        align_v = "CENTER_V";
                    }

                    if (number_font.font != null && number_font.font.Length > 3 && FontsList.ContainsKey(number_font.font))
                    {
                        string font_fileName = FontsList[number_font.font];
                        //string font_fileName = ProjectDir + @"\assets\fonts\" + number_font.font;
                        if (SelectedModel.versionOS >= 2 && File.Exists(font_fileName))
                        {
                            Font drawFont = null;
                            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
                            {
                                fonts.AddFontFile(font_fileName);
                                drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
                            }

                            Draw_text_userFont(gPanel, x, y, w, h, drawFont, size, space_h, space_v, color, alpha, valueStr,
                                            align_h, align_v, text_style, BBorder);
                        }
                        else
                        {
                            Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                        }

                    }
                    else
                    {
                        Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, alpha, valueStr, align_h, align_v, text_style, BBorder);
                    }
                }

                if (pointer != null && pointer.src != null && pointer.src.Length > 0 &&
                    index == pointer.position && pointer.visible)
                {
                    int x = pointer.center_x;
                    int y = pointer.center_y;
                    int offsetX = pointer.pos_x;
                    int offsetY = pointer.pos_y;
                    int startAngle = pointer.start_angle;
                    int endAngle = pointer.end_angle;
                    int image_index = ListImages.IndexOf(pointer.src);

                    float angle = startAngle + progress * (endAngle - startAngle);

                    if (pointer.scale != null && pointer.scale.Length > 0)
                    {
                        int image_index_scale = ListImages.IndexOf(pointer.scale);
                        int x_scale = pointer.scale_x;
                        int y_scale = pointer.scale_y;

                        src = OpenFileStream(ListImagesFullName[image_index_scale]);
                        gPanel.DrawImage(src, x_scale, y_scale);
                    }

                    DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                    if (pointer.cover_path != null && pointer.cover_path.Length > 0)
                    {
                        image_index = ListImages.IndexOf(pointer.cover_path);
                        x = pointer.cover_x;
                        y = pointer.cover_y;

                        src = OpenFileStream(ListImagesFullName[image_index]);
                        gPanel.DrawImage(src, x, y);
                    }
                }

                if (img_direction != null && img_direction.img_First != null && img_direction.img_First.Length > 0 &&
                   index == img_direction.position && img_direction.visible)
                {
                    if (valueImgDirectionIndex >= 0)
                    {
                        int imageIndex = ListImages.IndexOf(img_direction.img_First);
                        int x = img_direction.X;
                        int y = img_direction.Y;
                        int width = 0;
                        int height = 0;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        width = src.Width;
                        height = src.Height;

                        imageIndex = imageIndex + valueImgDirectionIndex;

                        if (imageIndex < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            if (width > 0 && height > 0)
                            {
                                Rectangle cropRect = new Rectangle(0, 0, width, height);
                                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                                using (Graphics g = Graphics.FromImage(target))
                                {
                                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                                     cropRect, GraphicsUnit.Pixel);
                                }
                                gPanel.DrawImage(target, x, y);
                            }
                            else
                            {
                                gPanel.DrawImage(src, x, y);

                            }
                        }
                    }
                }

                if (icon != null && icon.src != null && icon.src.Length > 0 &&
                    index == icon.position && icon.visible)
                {
                    int imageIndex = ListImages.IndexOf(icon.src);
                    int x = icon.x;
                    int y = icon.y;

                    if (imageIndex < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (SelectedModel.versionOS >= 2.1 && icon.alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = icon.alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(x, y, w, h);
                            gPanel.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else gPanel.DrawImage(src, x, y);
                        gPanel.DrawImage(src, x, y);
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем покадровую анимацию</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="img_anim">Параметры анимации движения </param>
        /// <param name="time_value_sec">Время от начала анимайии, сек</param>
        private void DrawAnimationFrame(Graphics gPanel, hmUI_widget_IMG_ANIM img_anim, float time_value_sec)
        {
            string start_img = img_anim.anim_src;
            if (start_img == null || start_img.Length == 0) return;
            //if (time_value_sec < 0) time_value_sec = 0;

            int x = img_anim.x;
            int y = img_anim.y;

            int fps = img_anim.anim_fps;
            int size = img_anim.anim_size;

            Bitmap src = new Bitmap(1, 1);

            if (time_value_sec < 0) // статичная картинка
            {
                int imageIndex = ListAnimImages.IndexOf(start_img);
                imageIndex += img_anim.preview_frame - 1;
                if (imageIndex > ListAnimImages.Count - 1) imageIndex = ListAnimImages.Count - 1;
                if (imageIndex >= 0)
                {
                    src = OpenFileStream(ListAnimImagesFullName[imageIndex]);
                    gPanel.DrawImage(src, x, y); 
                }
            }
            else
            {

                bool repeat = img_anim.anim_repeat;
                if (repeat)
                {
                    while (time_value_sec > (float)size / fps)
                    {
                        time_value_sec = time_value_sec - (float)size / fps;
                    }
                }
                int imageIndex = ListAnimImages.IndexOf(start_img);
                if (repeat) imageIndex = (int)(imageIndex + time_value_sec * fps);
                else
                {
                    if (time_value_sec > (float)size / fps) imageIndex = (int)(imageIndex + size - 1);
                    else imageIndex = (int)(imageIndex + time_value_sec * fps);
                }
                if (imageIndex < ListAnimImagesFullName.Count && imageIndex >= 0)
                {
                    src = OpenFileStream(ListAnimImagesFullName[imageIndex]);
                    gPanel.DrawImage(src, x, y);
                } 
            }

            src.Dispose();
        }

        /// <summary>Рисуем анимацию движения </summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="motion_anim">Параметры покадровой анимации</param>
        /// <param name="time_value_sec">Время от начала анимайии, сек</param>
        private void DrawAnimationMotion(Graphics gPanel, Motion_Animation motion_anim,
            float time_value_sec)
        {
            string src_name = motion_anim.src;
            if (src_name == null || src_name.Length == 0) return;

            int x_start = motion_anim.x_start;
            int y_start = motion_anim.y_start;
            int x_end = motion_anim.x_end;
            int y_end = motion_anim.y_end;

            float time_anim = motion_anim.anim_duration/1000f;
            int count = motion_anim.repeat_count;

            bool anim_two_sides = motion_anim.anim_two_sides;

            int imageIndex = ListAnimImages.IndexOf(src_name);
            if (imageIndex < 0) return;
            Bitmap src = new Bitmap(1, 1);
            src = OpenFileStream(ListAnimImagesFullName[imageIndex]);

            if (time_value_sec < 0) // статичная картинка
            {
                bool show_in_start = motion_anim.show_in_start;
                if(show_in_start) gPanel.DrawImage(src, x_start, y_start);
                else gPanel.DrawImage(src, x_end, y_end);
            }
            else
            {
                if(anim_two_sides) // зеркальная анимация в обе стороны
                {
                    if (count > 0 & time_value_sec > time_anim * 2 * count) time_value_sec = 0;
                    while (time_value_sec > time_anim * 2)
                    {
                        time_value_sec = time_value_sec - time_anim * 2;
                    }
                    bool morror_anim = false;
                    if(time_value_sec > time_anim)
                    {
                        time_value_sec = time_value_sec - time_anim;
                        morror_anim = true;
                    }
                    float progress = time_value_sec / time_anim;
                    int dx = x_end - x_start;
                    int dy = y_end - y_start;

                    if (!morror_anim)
                    {
                        int x = (int)Math.Round(x_start + dx * progress);
                        int y = (int)Math.Round(y_start + dy * progress);
                        gPanel.DrawImage(src, x, y);
                    }
                    else
                    {
                        int x = (int)Math.Round(x_end - dx * progress);
                        int y = (int)Math.Round(y_end - dy * progress);
                        gPanel.DrawImage(src, x, y);
                    }
                }
                else // одностароняя анимация
                {
                    if (count > 0 & time_value_sec > time_anim * count) time_value_sec = time_anim;
                    while (time_value_sec > time_anim)
                    {
                        time_value_sec = time_value_sec - time_anim;
                    }
                    float progress = time_value_sec / time_anim;
                    int dx = x_end - x_start;
                    int dy = y_end - y_start;

                    int x = (int)Math.Round(x_start + dx * progress);
                    int y = (int)Math.Round(y_start + dy * progress);
                    gPanel.DrawImage(src, x, y);
                }
            }


            src.Dispose();
        }

        /// <summary>Рисуем анимацию вращения </summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="rotate_anim">Параметры анимации вращения</param>
        /// <param name="time_value_sec">Время от начала анимайии, сек</param>
        /// <param name="showCentrHend">Отображать маркер на точке вращения</param>
        private void DrawAnimationRotate(Graphics gPanel, Rotate_Animation rotate_anim,
            float time_value_sec, bool showCentrHend)
        {
            string src_name = rotate_anim.src;
            if (src_name == null || src_name.Length == 0) return;

            int image_index = ListAnimImages.IndexOf(src_name);
            if (image_index < 0) return;
            int x = rotate_anim.center_x;
            int y = rotate_anim.center_y;
            int offsetX = rotate_anim.pos_x;
            int offsetY = rotate_anim.pos_y;
            float angle = 0;

            float start_angle = rotate_anim.start_angle;
            float end_angle = rotate_anim.end_angle;


            float time_anim = rotate_anim.anim_duration / 1000f;
            int count = rotate_anim.repeat_count;

            bool anim_two_sides = rotate_anim.anim_two_sides;

            if (time_value_sec < 0) // статичная картинка
            {
                bool show_in_start = rotate_anim.show_in_start;
                if (show_in_start) angle = start_angle;
                else angle = end_angle;
            }
            else
            {
                if (anim_two_sides) // зеркальная анимация в обе стороны
                {
                    if (count > 0 & time_value_sec > time_anim * 2 * count) time_value_sec = 0;
                    while (time_value_sec > time_anim * 2)
                    {
                        time_value_sec = time_value_sec - time_anim * 2;
                    }
                    bool morror_anim = false;
                    if (time_value_sec > time_anim)
                    {
                        time_value_sec = time_value_sec - time_anim;
                        morror_anim = true;
                    }
                    float progress = time_value_sec / time_anim;
                    float d_angl = end_angle - start_angle;

                    if (!morror_anim)
                    {
                        angle = start_angle + d_angl * progress;
                    }
                    else
                    {
                        angle = end_angle - d_angl * progress;
                    }
                }
                else // одностароняя анимация
                {
                    if (count > 0 & time_value_sec > time_anim * count) time_value_sec = time_anim;
                    while (time_value_sec > time_anim)
                    {
                        time_value_sec = time_value_sec - time_anim;
                    }
                    float progress = time_value_sec / time_anim;
                    float d_angl = end_angle - start_angle;

                    angle = start_angle + d_angl * progress;
                }
            }

            DrawRotateForAnim(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);
        }

        /// <summary>Рисует стрелки</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Центр стрелки X</param>
        /// <param name="y">Центр стрелки Y</param>
        /// <param name="offsetX">Смещение от центра по X</param>
        /// <param name="offsetY">Смещение от центра по Y</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="angle">Угол поворота стрелки в градусах</param>
        /// <param name="showCentrHend">Отображать маркер на точке вращения</param>
        public void DrawPointer(Graphics graphics, int x, int y, int offsetX, int offsetY, int image_index, float angle, bool showCentrHend)
        {
            Logger.WriteLine("* DrawPointer");
            Bitmap src = OpenFileStream(ListImagesFullName[image_index]);
            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(angle);
            graphics.DrawImage(src, new Rectangle(-offsetX, -offsetY, src.Width, src.Height));
            graphics.RotateTransform(-angle);
            graphics.TranslateTransform(-x, -y);
            src.Dispose();

            if (showCentrHend)
            {
                Logger.WriteLine("Draw showCentrHend");
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawLine(pen1, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen1, new Point(x, y - 5), new Point(x, y + 5));
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawLine(pen2, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen2, new Point(x, y - 5), new Point(x, y + 5));
                }
            }
            Logger.WriteLine("* DrawPointer (end)");
        }

        /// <summary>Рисует элемент для анимации вращения</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Центр стрелки X</param>
        /// <param name="y">Центр стрелки Y</param>
        /// <param name="offsetX">Смещение от центра по X</param>
        /// <param name="offsetY">Смещение от центра по Y</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="angle">Угол поворота стрелки в градусах</param>
        /// <param name="showCentrHend">Отображать маркер на точке вращения</param>
        public void DrawRotateForAnim(Graphics graphics, int x, int y, int offsetX, int offsetY, int image_index, float angle, bool showCentrHend)
        {
            Logger.WriteLine("* DrawPointer");
            Bitmap src = OpenFileStream(ListAnimImagesFullName[image_index]);
            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(angle);
            graphics.DrawImage(src, new Rectangle(-offsetX, -offsetY, src.Width, src.Height));
            graphics.RotateTransform(-angle);
            graphics.TranslateTransform(-x, -y);
            src.Dispose();

            if (showCentrHend)
            {
                Logger.WriteLine("Draw showCentrHend");
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawLine(pen1, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen1, new Point(x, y - 5), new Point(x, y + 5));
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawLine(pen2, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen2, new Point(x, y - 5), new Point(x, y + 5));
                }
            }
            Logger.WriteLine("* DrawPointer (end)");
        }


        /// <summary>Рисует число набором картинок</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="spacing">Величина отступа</param>
        /// <param name="alignment">Выравнивание</param>
        /// <param name="value">Отображаемая величина</param>
        /// <param name="alpha">Прозрачность данных</param>
        /// <param name="addZero">Отображать начальные нули</param>
        /// <param name="value_lenght">Количество отображаемых символов</param>
        /// <param name="separator_index">Символ разделителя (единиц измерения)</param>
        /// <param name="angle">Угол наклона текста</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="elementName">Название элемента (при необходимости)</param>
        private int Draw_dagital_text(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, int value, int alpha, bool addZero, int value_lenght, int separator_index, int angle, bool BBorder, 
            string elementName = "")
        {
            if (image_index < 0 || image_index >= ListImagesFullName.Count) return 0;

            int result = 0;
            //if (ProgramSettings.Watch_Model != "GTR 4" && ProgramSettings.Watch_Model != "GTS 4" && 
            //    ProgramSettings.Watch_Model != "GTR mini" && ProgramSettings.Watch_Model != "T-Rex Ultra") angle = 0;
            if (SelectedModel.versionOS < 2) angle = 0;
            Logger.WriteLine("* Draw_dagital_text");
            var src = new Bitmap(1, 1);
            int _number;
            int i;
            string value_S = value.ToString();
            if (addZero)
            {
                while (value_S.Length < value_lenght)
                {
                    value_S = "0" + value_S;
                }
            }

            Logger.WriteLine("DateLenght");
            src = OpenFileStream(ListImagesFullName[image_index]);
            int width = src.Width;
            int height = src.Height;
            //int DateLenght = width * value_lenght + spacing * (value_lenght - 1);ElementHumidity
            if (elementName == "ElementStand") value_lenght = 4;
            if (elementName == "ElementUVIndex") value_lenght = 2;
            if (elementName == "ElementHumidity") value_lenght = 3;
            if (elementName == "ElementWind") value_lenght = 2;
            if (elementName == "ElementCompass") value_lenght = 5;
            int DateLenght = width * value_lenght;
            //int DateLenght = width * value_lenght + 1;
            if (spacing != 0)
            {
                DateLenght += spacing * (value_lenght - 1);
                //if (separator_index > -1) DateLenght -= spacing;
            }
            //else DateLenght = DateLenght - spacing;

            Logger.WriteLine("DateLenghtReal");
            char[] CH = value_S.ToCharArray();
            int DateLenghtReal = 0;
            foreach (char ch in CH)
            {
                _number = 0;
                if (int.TryParse(ch.ToString(), out _number))
                {
                    i = image_index + _number;
                    if (i < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[i]);
                        //DateLenghtReal = DateLenghtReal + src.Width + spacing;
                        DateLenghtReal = DateLenghtReal + width + spacing;
                    }
                }

            }
            DateLenghtReal -= spacing;
            //if (spacing != 0)
            //{
            //    if (separator_index > -1)
            //    {
            //        if (elementName != "ElementDigitalTime" && elementName != "ElementDateDay" &&
            //            elementName != "ElementDateMonth" && elementName != "ElementDateYear") DateLenghtReal += spacing;
            //    }
            //}


            int PointX = x;
            int offsetX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    offsetX = 0;
                    break;
                case 1:
                    offsetX = DateLenght / 2 - DateLenghtReal / 2;
                    break;
                case 2:
                    offsetX = DateLenght - DateLenghtReal;
                    break;
                default:
                    offsetX = 0;
                    break;
            }

            Matrix transformMatrix = graphics.Transform;
            if (SelectedModel.versionOS >= 2)
            {
                int pivot_point_offset_x = 0;
                int pivot_point_offset_y = 0;
                Calculation_pivot_point_offset(width, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                graphics.TranslateTransform(PointX, PointY);
                graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-PointX, -PointY);
            }

            Logger.WriteLine("Draw value");
            foreach (char ch in CH)
            {
                _number = 0;
                if (int.TryParse(ch.ToString(), out _number))
                {
                    i = image_index + _number;
                    if (i < ListImagesFullName.Count)
                    {
                        string s = ListImagesFullName[i];
                        src = OpenFileStream(ListImagesFullName[i]);
                        if (SelectedModel.versionOS >= 2.1 && alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(PointX + offsetX, PointY, w, h);
                            graphics.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else graphics.DrawImage(src, PointX + offsetX, PointY);
                        PointX = PointX + width + spacing;
                        //src.Dispose();
                    }
                }

            }
            //result = PointX - spacing;
            result = PointX + offsetX;
            if (separator_index > -1)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                if (SelectedModel.versionOS >= 2.1 && alpha != 255)
                {
                    int w = src.Width;
                    int h = src.Height;
                    // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                    ColorMatrix colorMatrix = new ColorMatrix();
                    colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                    // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                    ImageAttributes imgAttributes = new ImageAttributes();
                    imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    // Указываем прямоугольник, куда будет помещено изображение
                    Rectangle rect_alpha = new Rectangle(PointX + offsetX, PointY, w, h);
                    graphics.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                }
                else graphics.DrawImage(src, PointX + offsetX, PointY);
                result = result + src.Width + spacing;
            }
            src.Dispose();

            if (BBorder)
            {
                Logger.WriteLine("DrawBorder");
                Rectangle rect = new Rectangle(x, y, DateLenght - 1, height - 1);
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawRectangle(pen1, rect);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen2, rect);
                }
            }
            //graphics.ResetTransform();
            graphics.Transform = transformMatrix;

            Logger.WriteLine("* Draw_dagital_text (end)");
            return result;
        }

        private void Calculation_pivot_point_offset(int width, int height, float angle, out int pivot_point_offset_x, out int pivot_point_offset_y)
        {
            while (angle >= 360)
            {
                angle -= 360;
            }
            while (angle < 0)
            {
                angle += 360;
            }
            pivot_point_offset_x = 0;
            pivot_point_offset_y = 0;
            if ((width == 0 && height == 0) || angle == 0) return;
            double angle_radian = Math.PI * angle / 180.0;
            if (angle > 0 && angle <= 90)
            {
                pivot_point_offset_x = (int)(height * Math.Sin(angle_radian));
                return;
            }
            if (angle > 90 && angle <= 180)
            {
                double d_angle = Math.Atan2(height, width);
                d_angle += angle_radian;
                double diagonal = Math.Sqrt(height * height + width * width); 
                pivot_point_offset_x = (int)(-diagonal * Math.Cos(d_angle));
                pivot_point_offset_y = (int)(-height * Math.Cos(angle_radian));
                return;
            }
            if (angle > 180 && angle <= 270)
            {
                double d_angle = Math.Atan2(height, width);
                d_angle += angle_radian;
                double diagonal = Math.Sqrt(height * height + width * width);
                pivot_point_offset_x = (int)(-width * Math.Cos(angle_radian));
                pivot_point_offset_y = (int)(-diagonal * Math.Sin(d_angle));
                return;
            }
            if (angle > 270 && angle <= 360)
            {
                pivot_point_offset_y = (int)(-width * Math.Sin(angle_radian));
                return;
            }

        }

        /// <summary>Рисует погоду числом</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="spacing">Величина отступа</param>
        /// <param name="alignment">Выравнивание</param>
        /// <param name="value">Отображаемая величина</param>
        /// <param name="addZero">Отображать начальные нули</param>
        /// <param name="image_minus_index">Символ "-"</param>
        /// <param name="unit_index">Символ единиц измерения</param>
        /// <param name="angle">Угол наклона текста</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="imageError_index">Иконка ошибки данны</param>
        /// <param name="errorData">отображать ошибку данный</param>
        private int Draw_weather_text(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, int value, int value_lenght, bool addZero, int image_minus_index, int unit_index, int angle, bool BBorder,
            int imageError_index = -1, bool errorData = false)
        {
            int result = 0;
            //if (ProgramSettings.Watch_Model != "GTR 4" && ProgramSettings.Watch_Model != "GTS 4" &&
            //    ProgramSettings.Watch_Model != "GTR mini" && ProgramSettings.Watch_Model != "T-Rex Ultra") angle = 0;
            if (SelectedModel.versionOS < 2) angle = 0;
            Logger.WriteLine("* Draw_weather_text");
            var src = new Bitmap(1, 1);
            int _number;
            int i;
            string value_S = value.ToString();
            if (addZero)
            {
                //while (value_S.Length < 2)
                //{
                //    value_S = "0" + value_S;
                //}
                if (value >= 0) value_S = value_S.PadLeft(value_lenght, '0');
                else
                {
                    int tempGoal = Math.Abs(value);
                    value_S = tempGoal.ToString();
                    value_S = value_S.PadLeft(value_lenght - 1, '0');
                    value_S = "-" + value_S;
                }
            }
            char[] CH = value_S.ToCharArray();

            src = OpenFileStream(ListImagesFullName[image_index]);
            int widthD = src.Width;
            int height = src.Height;
            int widthM = 0;
            int widthCF = 0;
            if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[image_minus_index]);
                widthM = src.Width;
            }
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[unit_index]);
                widthCF = src.Width;
            }

            //int DateLenght = widthD * 2 + widthM + widthCF + 1;
            int DateLenght = widthD * value_lenght + 1;
            if (spacing != 0) DateLenght = DateLenght + (value_lenght - 1) * spacing;
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count) DateLenght = DateLenght + widthCF + spacing;
            //if (widthM == 0) DateLenght = DateLenght - spacing;
            //if (alignment == 2 && AvailabilityIcon) DateLenght = DateLenght - spacing;

            int DateLenghtReal = 0;
            Logger.WriteLine("DateLenght");
            foreach (char ch in CH)
            {
                _number = 0;
                if (int.TryParse(ch.ToString(), out _number))
                {
                    i = image_index + _number;
                    if (i < ListImagesFullName.Count)
                    {
                        //src = new Bitmap(ListImagesFullName[i]);
                        src = OpenFileStream(ListImagesFullName[i]);
                        DateLenghtReal = DateLenghtReal + src.Width + spacing;
                        //src.Dispose();
                    }
                }
                else
                {
                    if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
                    {
                        DateLenghtReal = DateLenghtReal + widthM + spacing;
                    }
                }
            }
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                DateLenghtReal = DateLenghtReal + widthCF + spacing;
            }

            DateLenghtReal = DateLenghtReal - spacing;

            int PointX = x;
            int offsetX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    offsetX = 0;
                    break;
                case 1:
                    offsetX = (DateLenght - DateLenghtReal) / 2 - 1;
                    break;
                case 2:
                    offsetX = DateLenght - DateLenghtReal - 1;
                    break;
                default:
                    offsetX = 0;
                    break;
            }

            Logger.WriteLine("Draw value");
            Matrix transformMatrix = graphics.Transform;
            if (!errorData)
            {
                //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2")
                if (SelectedModel.versionOS >= 1.5)
                {
                    int pivot_point_offset_x = 0;
                    int pivot_point_offset_y = 0;
                    Calculation_pivot_point_offset(widthD, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                    graphics.TranslateTransform(PointX, PointY);
                    graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-PointX, -PointY);
                }

                foreach (char ch in CH)
                {
                    _number = 0;
                    if (int.TryParse(ch.ToString(), out _number))
                    {
                        i = image_index + _number;
                        if (i < ListImagesFullName.Count)
                        {
                            //src = new Bitmap(ListImagesFullName[i]);
                            src = OpenFileStream(ListImagesFullName[i]);
                            graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                            //PointX = PointX + src.Width + spacing;
                            PointX = PointX + widthD + spacing;
                            //src.Dispose();
                        }
                    }
                    else
                    {
                        if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
                        {
                            //src = new Bitmap(ListImagesFullName[dec]);
                            src = OpenFileStream(ListImagesFullName[image_minus_index]);
                            graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                            PointX = PointX + src.Width + spacing;
                            //src.Dispose();
                        }
                    }

                }
                result = PointX - spacing + offsetX;
                if (unit_index > -1)
                {
                    src = OpenFileStream(ListImagesFullName[unit_index]);
                    graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                    result = result + src.Width + spacing;
                }
            }
            else if (imageError_index >= 0)
            {
                src = OpenFileStream(ListImagesFullName[imageError_index]);
                //switch (alignment)
                //{
                //    case 0:
                //        PointX = x;
                //        break;
                //    case 1:
                //        PointX = x + (DateLenght - src.Width) / 2;
                //        break;
                //    case 2:
                //        PointX = x + DateLenght - src.Width;
                //        break;
                //    default:
                //        PointX = x;
                //        break;
                //}
                switch (alignment)
                {
                    case 0:
                        offsetX = 0;
                        break;
                    case 1:
                        offsetX = (DateLenght - src.Width) / 2;
                        break;
                    case 2:
                        offsetX = DateLenght - src.Width;
                        break;
                    default:
                        offsetX = 0;
                        break;
                }
                //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2")
                if (SelectedModel.versionOS >= 1.5)
                {
                    int pivot_point_offset_x = 0;
                    int pivot_point_offset_y = 0;
                    Calculation_pivot_point_offset(widthD, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                    graphics.TranslateTransform(PointX, PointY);
                    graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-PointX, -PointY);
                }
                graphics.DrawImage(src, PointX + offsetX, PointY);
            }
            src.Dispose();

            if (BBorder)
            {
                Logger.WriteLine("DrawBorder");
                Rectangle rect = new Rectangle(x, y, DateLenght - 1, height - 1);
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawRectangle(pen1, rect);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen2, rect);
                }
            }
            //graphics.ResetTransform();
            graphics.Transform = transformMatrix;

            Logger.WriteLine("* Draw_weather_text (end)");
            return result;
        }

        /// <summary>Рисует погоду числом</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="spacing">Величина отступа</param>
        /// <param name="alignment">Выравнивание</param>
        /// <param name="valueMax">Максимальная температура</param>
        /// <param name="valueMin">минимальная температура</param>
        /// <param name="image_minus_index">Символ "-"</param>
        /// <param name="separtator_index">Символ разделителя</param>
        /// <param name="unit_index">Символ единиц измерения</param>
        /// <param name="angle">Угол наклона текста</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="imageError_index">Иконка ошибки данны</param>
        /// <param name="errorData">отображать ошибку данный</param>
        private int Draw_weather_MaxMin(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, int valueMax, int valueMin, int image_minus_index, int separtator_index, int unit_index, 
            int angle, bool BBorder, int imageError_index = -1, bool errorData = false)
        {
            int result = 0;
            int value_lenght = 5;
            //if (ProgramSettings.Watch_Model != "GTR 4" && ProgramSettings.Watch_Model != "GTS 4" &&
            //    ProgramSettings.Watch_Model != "GTR mini" && ProgramSettings.Watch_Model != "T-Rex Ultra") angle = 0;
            if (SelectedModel.versionOS < 2) angle = 0;
            Logger.WriteLine("* Draw_weather_text");
            var src = new Bitmap(1, 1);
            int _number;
            int i;
            string value_S = valueMax.ToString();
            if (separtator_index >= 0)
            {
                value_S += ".";
            }
            value_S += valueMin.ToString();
            //if (addZero)
            //{
            //    //while (value_S.Length < 2)
            //    //{
            //    //    value_S = "0" + value_S;
            //    //}
            //    if (value >= 0) value_S = value_S.PadLeft(value_lenght, '0');
            //    else
            //    {
            //        int tempGoal = Math.Abs(value);
            //        value_S = tempGoal.ToString();
            //        value_S = value_S.PadLeft(value_lenght - 1, '0');
            //        value_S = "-" + value_S;
            //    }
            //}
            char[] CH = value_S.ToCharArray();

            src = OpenFileStream(ListImagesFullName[image_index]);
            int widthD = src.Width;
            int height = src.Height;
            int widthM = 0;
            int widthCF = 0;
            int widthS = 0;
            if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[image_minus_index]);
                widthM = src.Width;
            }
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[unit_index]);
                widthCF = src.Width;
            }
            if (separtator_index >= 0 && separtator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separtator_index]);
                widthS = src.Width;
            }

            //int DateLenght = widthD * 2 + widthM + widthCF + 1;
            int DateLenght = widthD * value_lenght + 1;
            if (spacing != 0) DateLenght = DateLenght + (value_lenght - 1) * spacing;
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count) DateLenght = DateLenght + widthCF + spacing;
            if (separtator_index >= 0 && separtator_index < ListImagesFullName.Count) DateLenght = DateLenght + widthS + spacing;
            //if (widthM == 0) DateLenght = DateLenght - spacing;
            //if (alignment == 2 && AvailabilityIcon) DateLenght = DateLenght - spacing;

            int DateLenghtReal = 0;
            Logger.WriteLine("DateLenght");
            foreach (char ch in CH)
            {
                _number = 0;
                if (int.TryParse(ch.ToString(), out _number))
                {
                    i = image_index + _number;
                    if (i < ListImagesFullName.Count)
                    {
                        //src = new Bitmap(ListImagesFullName[i]);
                        src = OpenFileStream(ListImagesFullName[i]);
                        DateLenghtReal = DateLenghtReal + src.Width + spacing;
                        //src.Dispose();
                    }
                }
                else
                {
                    if (ch == '.')
                    {
                        if (separtator_index >= 0 && separtator_index < ListImagesFullName.Count)
                        {
                            DateLenghtReal = DateLenghtReal + widthS + spacing;
                        }
                    }
                    else
                    {
                        if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
                        {
                            DateLenghtReal = DateLenghtReal + widthM + spacing;
                        } 
                    }
                }
            }
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                DateLenghtReal = DateLenghtReal + widthCF + spacing;
            }

            DateLenghtReal = DateLenghtReal - spacing;

            int PointX = x;
            int offsetX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    offsetX = 0;
                    break;
                case 1:
                    offsetX = (DateLenght - DateLenghtReal) / 2 - 1;
                    break;
                case 2:
                    offsetX = DateLenght - DateLenghtReal - 1;
                    break;
                default:
                    offsetX = 0;
                    break;
            }

            Logger.WriteLine("Draw value");
            Matrix transformMatrix = graphics.Transform;
            if (!errorData)
            {
                //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2")
                if (SelectedModel.versionOS >= 1.5)
                {
                    int pivot_point_offset_x = 0;
                    int pivot_point_offset_y = 0;
                    Calculation_pivot_point_offset(widthD, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                    graphics.TranslateTransform(PointX, PointY);
                    graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-PointX, -PointY);
                }

                foreach (char ch in CH)
                {
                    _number = 0;
                    if (int.TryParse(ch.ToString(), out _number))
                    {
                        i = image_index + _number;
                        if (i < ListImagesFullName.Count)
                        {
                            //src = new Bitmap(ListImagesFullName[i]);
                            src = OpenFileStream(ListImagesFullName[i]);
                            graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                            //PointX = PointX + src.Width + spacing;
                            PointX = PointX + widthD + spacing;
                            //src.Dispose();
                        }
                    }
                    else
                    {
                        if (ch == '.')
                        {
                            if (separtator_index >= 0 && separtator_index < ListImagesFullName.Count)
                            {
                                //src = new Bitmap(ListImagesFullName[dec]);
                                src = OpenFileStream(ListImagesFullName[separtator_index]);
                                graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                                PointX = PointX + src.Width + spacing;
                                //src.Dispose();
                            }
                        }
                        else
                        {
                            if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
                            {
                                //src = new Bitmap(ListImagesFullName[dec]);
                                src = OpenFileStream(ListImagesFullName[image_minus_index]);
                                graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                                PointX = PointX + src.Width + spacing;
                                //src.Dispose();
                            } 
                        }
                    }

                }
                result = PointX - spacing + offsetX;
                if (unit_index > -1)
                {
                    src = OpenFileStream(ListImagesFullName[unit_index]);
                    graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                    result = result + src.Width + spacing;
                }
            }
            else if (imageError_index >= 0)
            {
                src = OpenFileStream(ListImagesFullName[imageError_index]);
                //switch (alignment)
                //{
                //    case 0:
                //        PointX = x;
                //        break;
                //    case 1:
                //        PointX = x + (DateLenght - src.Width) / 2;
                //        break;
                //    case 2:
                //        PointX = x + DateLenght - src.Width;
                //        break;
                //    default:
                //        PointX = x;
                //        break;
                //}
                switch (alignment)
                {
                    case 0:
                        offsetX = 0;
                        break;
                    case 1:
                        offsetX = (DateLenght - src.Width) / 2;
                        break;
                    case 2:
                        offsetX = DateLenght - src.Width;
                        break;
                    default:
                        offsetX = 0;
                        break;
                }
                //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2")
                if (SelectedModel.versionOS >= 1.5)
                {
                    int pivot_point_offset_x = 0;
                    int pivot_point_offset_y = 0;
                    Calculation_pivot_point_offset(widthD, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                    graphics.TranslateTransform(PointX, PointY);
                    graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-PointX, -PointY);
                }
                graphics.DrawImage(src, PointX + offsetX, PointY);
            }
            src.Dispose();

            if (BBorder)
            {
                Logger.WriteLine("DrawBorder");
                Rectangle rect = new Rectangle(x, y, DateLenght - 1, height - 1);
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawRectangle(pen1, rect);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen2, rect);
                }
            }
            //graphics.ResetTransform();
            graphics.Transform = transformMatrix;

            Logger.WriteLine("* Draw_weather_text (end)");
            return result;
        }

        /// <summary>Рисует погоду числом</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="spacing">Величина отступа</param>
        /// <param name="alignment">Выравнивание</param>
        /// <param name="value_max">Отображаемое максимальное значение</param>
        /// <param name="value_min">Отображаемое минимальное значение</param>
        /// <param name="addZero">Отображать начальные нули</param>
        /// <param name="image_minus_index">Символ "-"</param>
        /// <param name="separator_index">Символ разделителя</param>
        /// <param name="unit_index">Символ единиц измерения</param>
        /// <param name="angle">Угол наклона текста</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="imageError_index">Иконка ошибки данны</param>
        /// <param name="errorData">отображать ошибку данный</param>
        private int Draw_weather_max_min_text(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, int value_max, int value_min, int value_lenght, bool addZero, int image_minus_index, int unit_index, int separator_index,
            int angle, bool BBorder, int imageError_index = -1, bool errorData = false)
        {
            int result = 0;
            //if (ProgramSettings.Watch_Model != "GTR 4" && ProgramSettings.Watch_Model != "GTS 4" &&
            //    ProgramSettings.Watch_Model != "GTR mini" && ProgramSettings.Watch_Model != "T-Rex Ultra") angle = 0;
            if (SelectedModel.versionOS < 2) angle = 0;
            Logger.WriteLine("* Draw_weather_text");
            var src = new Bitmap(1, 1);
            int _number;
            int i;
            string value_max_S = value_max.ToString();
            string value_min_S = value_min.ToString();
            if (addZero)
            {
                if (value_max >= 0) value_max_S = value_max_S.PadLeft(value_lenght, '0');
                else
                {
                    int tempGoal = Math.Abs(value_max);
                    value_max_S = tempGoal.ToString();
                    value_max_S = value_max_S.PadLeft(value_lenght - 1, '0');
                    value_max_S = "-" + value_max_S;
                }

                if (value_min >= 0) value_min_S = value_min_S.PadLeft(value_lenght, '0');
                else
                {
                    int tempGoal = Math.Abs(value_min);
                    value_min_S = tempGoal.ToString();
                    value_min_S = value_min_S.PadLeft(value_lenght - 1, '0');
                    value_min_S = "-" + value_min_S;
                }
            }
            char[] CH = (value_max + "*" + value_min).ToCharArray();

            src = OpenFileStream(ListImagesFullName[image_index]);
            int widthD = src.Width;
            int height = src.Height;
            int widthM = 0;
            int widthCF = 0;
            int widthS = 0;
            if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[image_minus_index]);
                widthM = src.Width;
            }
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[unit_index]);
                widthCF = src.Width;
            }
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                widthS = src.Width;
            }

            int DateLenght = widthD * 6 + 1;
            if (spacing != 0) DateLenght += 2 * spacing;
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count) DateLenght = DateLenght + widthCF + spacing;
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count) DateLenght = DateLenght + widthS + spacing;

            int DateLenghtReal = 0;
            Logger.WriteLine("DateLenght");
            foreach (char ch in CH)
            {
                _number = 0;
                if (int.TryParse(ch.ToString(), out _number))
                {
                    i = image_index + _number;
                    if (i < ListImagesFullName.Count)
                    {
                        //src = new Bitmap(ListImagesFullName[i]);
                        src = OpenFileStream(ListImagesFullName[i]);
                        DateLenghtReal = DateLenghtReal + src.Width + spacing;
                        //src.Dispose();
                    }
                }
                else
                {
                    if (ch == '*')
                    {
                        if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
                            DateLenghtReal = DateLenghtReal + widthS + spacing;
                    }
                    else if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
                    {
                        DateLenghtReal = DateLenghtReal + widthM + spacing;
                    }
                }
            }
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                DateLenghtReal = DateLenghtReal + widthCF + spacing;
            }

            DateLenghtReal = DateLenghtReal - spacing;

            //int PointX = 0;
            //int PointY = y;
            //switch (alignment)
            //{
            //    case 0:
            //        PointX = x;
            //        break;
            //    case 1:
            //        PointX = x + DateLenght / 2 - DateLenghtReal / 2;
            //        break;
            //    case 2:
            //        PointX = x + DateLenght - DateLenghtReal;
            //        break;
            //    default:
            //        PointX = x;
            //        break;
            //}

            int PointX = x;
            int offsetX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    offsetX = 0;
                    break;
                case 1:
                    offsetX = (DateLenght - DateLenghtReal) / 2 - 1;
                    break;
                case 2:
                    offsetX = DateLenght - DateLenghtReal - 1;
                    break;
                default:
                    offsetX = 0;
                    break;
            }

            Logger.WriteLine("Draw value");
            Matrix transformMatrix = graphics.Transform;
            if (!errorData)
            {
                //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2")
                if (SelectedModel.versionOS >= 1.5)
                {
                    int pivot_point_offset_x = 0;
                    int pivot_point_offset_y = 0;
                    Calculation_pivot_point_offset(widthD, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                    graphics.TranslateTransform(PointX, PointY);
                    graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-PointX, -PointY);
                }

                foreach (char ch in CH)
                {
                    _number = 0;
                    if (int.TryParse(ch.ToString(), out _number))
                    {
                        i = image_index + _number;
                        if (i < ListImagesFullName.Count)
                        {
                            //src = new Bitmap(ListImagesFullName[i]);
                            src = OpenFileStream(ListImagesFullName[i]);
                            graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                            //PointX = PointX + src.Width + spacing;
                            PointX = PointX + widthD + spacing;
                            //src.Dispose();
                        }
                    }
                    else
                    {
                        if (ch == '*')
                        {
                            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
                            {
                                src = OpenFileStream(ListImagesFullName[separator_index]);
                                if (src != null)
                                {
                                    graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                                    PointX = PointX + src.Width + spacing;
                                } 
                            }
                        }
                        else if (image_minus_index >= 0 && image_minus_index < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[image_minus_index]);
                            if (src != null)
                            {
                                graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                                PointX = PointX + src.Width + spacing;
                            }
                        }
                    }

                }
                result = PointX - spacing + offsetX;
                if (unit_index > -1)
                {
                    src = OpenFileStream(ListImagesFullName[unit_index]);
                    graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                    result = result + src.Width + spacing;
                }
            }
            else if (imageError_index >= 0)
            {
                src = OpenFileStream(ListImagesFullName[imageError_index]);
                //switch (alignment)
                //{
                //    case 0:
                //        PointX = x;
                //        break;
                //    case 1:
                //        PointX = x + (DateLenght - src.Width) / 2;
                //        break;
                //    case 2:
                //        PointX = x + DateLenght - src.Width;
                //        break;
                //    default:
                //        PointX = x;
                //        break;
                //}
                switch (alignment)
                {
                    case 0:
                        offsetX = 0;
                        break;
                    case 1:
                        offsetX = (DateLenght - src.Width) / 2;
                        break;
                    case 2:
                        offsetX = DateLenght - src.Width;
                        break;
                    default:
                        offsetX = 0;
                        break;
                }
                //if (ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4" || ProgramSettings.Watch_Model == "T-Rex 2")
                if (SelectedModel.versionOS >= 1.5)
                {
                    int pivot_point_offset_x = 0;
                    int pivot_point_offset_y = 0;
                    Calculation_pivot_point_offset(widthD, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                    graphics.TranslateTransform(PointX, PointY);
                    graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                    graphics.RotateTransform(angle);
                    graphics.TranslateTransform(-PointX, -PointY);
                }
                graphics.DrawImage(src, PointX + offsetX, PointY);
            }
            src.Dispose();

            if (BBorder)
            {
                Logger.WriteLine("DrawBorder");
                Rectangle rect = new Rectangle(x, y, DateLenght - 1, height - 1);
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawRectangle(pen1, rect);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen2, rect);
                }
            }
            //graphics.ResetTransform();
            graphics.Transform = transformMatrix;

            Logger.WriteLine("* Draw_weather_text (end)");
            return result;
        }

        /// <summary>Пишем число системным шрифтом</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="w">Ширина</param>
        /// <param name="h">Высота</param>
        /// <param name="size">Размер шрифта</param>
        /// <param name="spacing_h">Величина отступа</param>
        /// <param name="spacing_v">Межстрочный интервал</param>
        /// <param name="color">Цвет шрифта</param>
        /// <param name="alpha">Прозрачность</param>
        /// <param name="value">Отображаемые данные</param>
        /// <param name="align_h">Горизонтальное выравнивание</param>
        /// <param name="align_v">Вертикальное выравнивание</param>
        /// <param name="text_style">Стиль вписывания текста</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        private void Draw_text(Graphics graphics, int x, int y, int w, int h, float size, int spacing_h, int spacing_v, 
            Color color, int alpha, string value, string align_h, string align_v, string text_style, bool BBorder)
        {
            size = size * 0.99f;
            if (w < 5 || h < 5) return;
            Bitmap bitmap = new Bitmap(Convert.ToInt32(w), Convert.ToInt32(h), PixelFormat.Format32bppArgb);
            Graphics gPanel = Graphics.FromImage(bitmap);
            //Font drawFont = new Font("Times New Roman", size, GraphicsUnit.World);
            Font drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
            StringFormat strFormat = new StringFormat();
            strFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Near;
            Size strSize1 = TextRenderer.MeasureText(graphics, "0", drawFont);
            Size strSize2 = TextRenderer.MeasureText(graphics, "00" + Environment.NewLine + "0", drawFont);
            int chWidth = strSize2.Width - strSize1.Width;
            int offsetX = strSize1.Width - chWidth;
            int chHeight = strSize2.Height - strSize1.Height;
            //float offsetY = strSize2.Height - strSize1.Height;
            float offsetY = strSize1.Height - size;
            List<string> text = new List<string>();
            switch (text_style)
            {
                case "NONE":
                    text.Add(value);
                    break;
                case "WRAP":
                    //text.Add(value);
                    string[] words = value.Split(new char[] { ' ' });
                    for (int i = 0; i < words.Length; i++)
                    {
                        string draw_string = words[i];
                        Size strSize = TextRenderer.MeasureText(graphics, draw_string, drawFont);
                        int strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                        //strLenght += (draw_string.Length - 1) * spacing_h;
                        if(strLenght <= w) // слово помещается в рамку
                        {
                            if (i + 1 < words.Length) // есть еще слова
                            {
                                while (i + 1 < words.Length && strLenght < w)
                                {
                                    string new_draw_string = draw_string + " " + words[i + 1];
                                    strSize = TextRenderer.MeasureText(graphics, new_draw_string, drawFont);
                                    strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                                    strLenght += (new_draw_string.Length - 1) * spacing_h;
                                    if (strLenght <= w)
                                    {
                                        i++;
                                        draw_string = new_draw_string;
                                    } 
                                }
                                text.Add(draw_string);
                            }
                            else // последнее слово
                            {
                                text.Add(draw_string);
                            }
                        }
                        else // слово больше рамки
                        {
                            int index = draw_string.Length;
                            while (strLenght > w && index > 1)
                            {
                                index--;
                                string subStr = draw_string.Substring(0, index);
                                strSize = TextRenderer.MeasureText(graphics, subStr, drawFont);
                                strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                                if(strLenght <= w)
                                {
                                    text.Add(subStr);
                                    draw_string = draw_string.Remove(0, index);
                                    index = draw_string.Length;
                                    strSize = TextRenderer.MeasureText(graphics, draw_string, drawFont);
                                    strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                                }
                            }
                            text.Add(draw_string);
                        }
                    }
                    
                    break;
                case "CHAR_WRAP":
                    text.Add(value);
                    break;
                case "ELLIPSIS":
                    text.Add(value);
                    break;

                default:
                    text.Add(value);
                    break;
            }

            Logger.WriteLine("* Draw_text");

            float PointX = (float)(-0.3 * offsetX);
            float PointY = (float)(1.2 * offsetY);

            if (align_v == "BOTTOM")
            {
                float textHeight = (float)((text.Count * 1.47 * size) + ((text.Count - 1) * 0.55 * spacing_v));
                PointY = (float)(h + 1.2 * offsetY - textHeight);
            }
            if (align_v == "CENTER_V")
            {
                float textHeight = (float)((text.Count * 1.47 * size) + ((text.Count - 1) * 0.55 * spacing_v));
                PointY = (float)(h/2 + 1.2 * offsetY - textHeight/2);
            }

            Logger.WriteLine("Draw value");
            //if (SelectedModel.versionOS > 3.5 && alpha != 255) {
            //    color = Color.FromArgb(alpha, color);
            //    // Устанавливаем параметры сглаживания для текста
            //    gPanel.TextRenderingHint = TextRenderingHint.AntiAlias;
            //    gPanel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //}

            SolidBrush drawBrush = new SolidBrush(color);

            try
            {
                foreach (string draw_string in text)
                {
                    //gPanel.DrawString(draw_string, drawFont, drawBrush, PointX, PointY, strFormat);
                    float posX = PointX;
                    float posY = PointY;

                    if (align_h == "RIGHT")
                    {
                        Size strSize = TextRenderer.MeasureText(graphics, draw_string, drawFont);
                        float textLenght = strSize.Width + (draw_string.Length-1) * spacing_h - offsetX;
                        posX = (float)(w - 0.3 * offsetX - textLenght);
                    }
                    if (align_h == "CENTER_H")
                    {
                        Size strSize = TextRenderer.MeasureText(graphics, draw_string, drawFont);
                        float textLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                        posX = (float)(w/2 - 0.3 * offsetX - textLenght/2);
                    }

                    if (spacing_h == 0)
                    {
                        //SizeF strSizeF = graphics.MeasureString(draw_string, drawFont);
                        Size strSize = TextRenderer.MeasureText(graphics, draw_string, drawFont);
                        gPanel.DrawString(draw_string, drawFont, drawBrush, posX, posY, strFormat);

                        posX = posX + strSize.Width - offsetX;
                    }
                    else
                    {
                        foreach (char ch in draw_string)
                        {
                            string str = ch.ToString();
                            Size strSize = TextRenderer.MeasureText(graphics, str, drawFont);
                            gPanel.DrawString(str, drawFont, drawBrush, posX, posY, strFormat);

                            posX = posX + strSize.Width + spacing_h - offsetX;
                        }
                    }
                    PointY += (float)(1.47 * size + 0.55 * spacing_v);
                }
                if (SelectedModel.versionOS >= 3.5 && alpha != 255)
                {
                    // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                    ColorMatrix colorMatrix = new ColorMatrix();
                    colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                    // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                    ImageAttributes imgAttributes = new ImageAttributes();
                    imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    // Указываем прямоугольник, куда будет помещено изображение
                    Rectangle rect_alpha = new Rectangle(x, y, w, h);
                    graphics.DrawImage(bitmap, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes); 
                }
                else graphics.DrawImage(bitmap, x, y);
                //graphics.DrawImage(bitmap, x, y);

                if (BBorder)
                {
                    Rectangle rect = new Rectangle(x, y, w, h);
                    Logger.WriteLine("DrawBorder");
                    //Rectangle rect = new Rectangle(0, (int)(-0.75 * size), result - x - 1, (int)(0.75 * size));
                    using (Pen pen1 = new Pen(Color.White, 1))
                    {
                        graphics.DrawRectangle(pen1, rect);
                    }
                    using (Pen pen2 = new Pen(Color.Black, 1))
                    {
                        pen2.DashStyle = DashStyle.Dot;
                        graphics.DrawRectangle(pen2, rect);
                    }
                }
            }
            catch (Exception)
            {
            }

            Logger.WriteLine("* Draw_text (end)");
        }

        /// <summary>Пишем число внешним шрифтом</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="w">Ширина</param>
        /// <param name="h">Высота</param>
        /// <param name="drawFont">Шрифт</param>
        /// <param name="size">Размер шрифта</param>
        /// <param name="spacing_h">Величина отступа</param>
        /// <param name="spacing_v">Межстрочный интервал</param>
        /// <param name="color">Цвет шрифта</param>
        /// <param name="alpha">Прозрачность</param>
        /// <param name="value">Отображаемые данные</param>
        /// <param name="align_h">Горизонтальное выравнивание</param>
        /// <param name="align_v">Вертикальное выравнивание</param>
        /// <param name="text_style">Стиль вписывания текста</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        private void Draw_text_userFont(Graphics graphics, int x, int y, int w, int h, Font drawFont, float size, int spacing_h, int spacing_v,
            Color color, int alpha, string value, string align_h, string align_v, string text_style, bool BBorder)
        {
            if (w < 5 || h < 5) return;
            Bitmap bitmap = new Bitmap(Convert.ToInt32(w), Convert.ToInt32(h), PixelFormat.Format32bppArgb);
            Graphics gPanel = Graphics.FromImage(bitmap);
            //Font drawFont = new Font("Times New Roman", size, GraphicsUnit.World);
            //Font drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
            StringFormat strFormat = new StringFormat();
            strFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Near;
            //Size strSize1 = TextRenderer.MeasureText(graphics, "0", drawFont);
            //Size strSize2 = TextRenderer.MeasureText(graphics, "00" + Environment.NewLine + "0", drawFont);
            SizeF strSize1 = graphics.MeasureString("0", drawFont);
            SizeF strSize2 = graphics.MeasureString("00" + Environment.NewLine + "0", drawFont);
            
            float chWidth = strSize2.Width - strSize1.Width;
            float offsetX = strSize1.Width - chWidth;
            float chHeight = strSize2.Height - strSize1.Height;
            //float offsetY = strSize2.Height - strSize1.Height;
            //float offsetY = strSize1.Height - size;
            //offsetY = 0;
            List<string> text = new List<string>();
            switch (text_style)
            {
                case "NONE":
                    text.Add(value);
                    break;
                case "WRAP":
                    //text.Add(value);
                    string[] words = value.Split(new char[] { ' ' });
                    for (int i = 0; i < words.Length; i++)
                    {
                        string draw_string = words[i];
                        Size strSize = TextRenderer.MeasureText(graphics, draw_string, drawFont);
                        float strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                        //strLenght += (draw_string.Length - 1) * spacing_h;
                        if (strLenght <= w) // слово помещается в рамку
                        {
                            if (i + 1 < words.Length) // есть еще слова
                            {
                                while (i + 1 < words.Length && strLenght < w)
                                {
                                    string new_draw_string = draw_string + " " + words[i + 1];
                                    strSize = TextRenderer.MeasureText(graphics, new_draw_string, drawFont);
                                    strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                                    strLenght += (new_draw_string.Length - 1) * spacing_h;
                                    if (strLenght <= w)
                                    {
                                        i++;
                                        draw_string = new_draw_string;
                                    }
                                }
                                text.Add(draw_string);
                            }
                            else // последнее слово
                            {
                                text.Add(draw_string);
                            }
                        }
                        else // слово больше рамки
                        {
                            int index = draw_string.Length;
                            while (strLenght > w && index > 1)
                            {
                                index--;
                                string subStr = draw_string.Substring(0, index);
                                strSize = TextRenderer.MeasureText(graphics, subStr, drawFont);
                                strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                                if (strLenght <= w)
                                {
                                    text.Add(subStr);
                                    draw_string = draw_string.Remove(0, index);
                                    index = draw_string.Length;
                                    strSize = TextRenderer.MeasureText(graphics, draw_string, drawFont);
                                    strLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                                }
                            }
                            text.Add(draw_string);
                        }
                    }

                    break;
                case "CHAR_WRAP":
                    text.Add(value);
                    break;
                case "ELLIPSIS":
                    text.Add(value);
                    break;

                default:
                    text.Add(value);
                    break;
            }

            Logger.WriteLine("* Draw_text");

            float PointX = (float)(-0.5 * offsetX);
            //float PointY = (float)(1.2 * offsetY);
            float PointY = 0;

            float textHeight = (float)((text.Count * chHeight) + (text.Count * 0.46 * spacing_v));
            //float textHeight = (float)((text.Count * chHeight) + ((text.Count - 1) * 0.46 * spacing_v));
            if (align_v == "BOTTOM")
            {
                PointY = PointY + h - textHeight;
            }
            if (align_v == "CENTER_V")
            {
                PointY = PointY + (h - textHeight) / 2;
            }

            Logger.WriteLine("Draw value");
            color = Color.FromArgb(alpha, color);
            SolidBrush drawBrush = new SolidBrush(color);

            try
            {
                foreach (string draw_string in text)
                {
                    //gPanel.DrawString(draw_string, drawFont, drawBrush, PointX, PointY, strFormat);
                    float posX = PointX;
                    float posY = PointY;

                    if (align_h == "RIGHT")
                    {
                        SizeF strSize = graphics.MeasureString(draw_string, drawFont);
                        float textLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                        posX = (float)(w - 0.5 * offsetX - textLenght);
                    }
                    if (align_h == "CENTER_H")
                    {
                        SizeF strSize = graphics.MeasureString(draw_string, drawFont);
                        float textLenght = strSize.Width + (draw_string.Length - 1) * spacing_h - offsetX;
                        posX = (float)(w / 2 - 0.5 * offsetX - textLenght / 2);
                    }

                    if(spacing_h == 0)
                    {
                        SizeF textSize = graphics.MeasureString(draw_string, drawFont);
                        float chWidthStr = textSize.Width * 0.98f;

                        gPanel.DrawString(draw_string, drawFont, drawBrush, posX, posY, strFormat);

                        posX = posX + chWidthStr + spacing_h;
                    }
                    else
                    {
                        string lastChar = "0";
                        foreach (char ch in draw_string)
                        {
                            string str = ch.ToString();
                            string drawStr = lastChar + str;
                            Size strSize = TextRenderer.MeasureText(graphics, str, drawFont);

                            Size chSize1 = TextRenderer.MeasureText(lastChar, drawFont);
                            Size chSize2 = TextRenderer.MeasureText(drawStr, drawFont);
                            chWidth = chSize2.Width - chSize1.Width;

                            SizeF textSize1 = graphics.MeasureString(lastChar, drawFont);
                            SizeF textSize2 = graphics.MeasureString(drawStr, drawFont);
                            float chWidth2 = (textSize2.Width - textSize1.Width) * 0.98f;
                            if (str == " ")
                            {
                                textSize2 = graphics.MeasureString(str + lastChar, drawFont);
                                chWidth2 = (textSize2.Width - textSize1.Width) * 0.98f;
                            }
                            if (lastChar == " ")
                            {
                                textSize1 = graphics.MeasureString(lastChar + str, drawFont);
                                textSize2 = graphics.MeasureString(str + lastChar + str, drawFont);
                                chWidth2 = (textSize2.Width - textSize1.Width) * 0.98f;
                            }

                            gPanel.DrawString(str, drawFont, drawBrush, posX, posY, strFormat);
                            lastChar = str;

                            posX = posX + chWidth2 + spacing_h;
                            //posX = posX + strSize.Width + spacing_h - offsetX;
                        } 
                    }
                    PointY += (float)(chHeight + 0.46 * spacing_v);
                }
                graphics.DrawImage(bitmap, x, y);

                if (BBorder)
                {
                    Rectangle rect = new Rectangle(x, y, w, h);
                    Logger.WriteLine("DrawBorder");
                    //Rectangle rect = new Rectangle(0, (int)(-0.75 * size), result - x - 1, (int)(0.75 * size));
                    using (Pen pen1 = new Pen(Color.White, 1))
                    {
                        graphics.DrawRectangle(pen1, rect);
                    }
                    using (Pen pen2 = new Pen(Color.Black, 1))
                    {
                        pen2.DashStyle = DashStyle.Dot;
                        graphics.DrawRectangle(pen2, rect);
                    }
                }
            }
            catch (Exception)
            {
            }

            Logger.WriteLine("* Draw_text (end)");
        }

        /// <summary>Пишем число под угломи</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="spacing">Угол между символами</param>
        /// <param name="angle">Угол поворота надписи в градусах</param>
        /// <param name="zero">Отображать начальные нули</param>
        /// <param name="image_index">Номер начального изображения</param>
        /// <param name="image_width">Ширина символа</param>
        /// <param name="image_height">Высота символа</param>
        /// <param name="unit_index">Номер символа единиц измерения</param>
        /// <param name="unit_width">Ширина символа единиц измерения</param>
        /// <param name="dot_image_index">Номер символа десятичного разделителя</param>
        /// <param name="dot_image_width">Ширина символа десятичного разделителя</param>
        /// <param name="separator_index">Номер символа разделителя</param>
        /// <param name="horizontal_alignment">Выравнивание символов по горизонтали относительно окружности (LEFT, CENTER_H, RIGHT)</param>
        /// <param name="unit_in_alignment">Учитывать единицы измерения при выравнивании</param>
        /// <param name="value">Отображаемая величина</param>
        /// <param name="value_lenght">Количество отображаемых символов</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="elementName">Название элемента</param>
        private void Draw_dagital_text_rotate(Graphics graphics, int x, int y, int spacing, float angle, bool zero,
            int image_index, int unit_index, int dot_image_index, 
            string horizontal_alignment, bool unit_in_alignment,
            string value, int value_lenght, bool BBorder, int separator_index, int error_index, bool errorData, string elementName = "")
        {
            Logger.WriteLine("* Draw_dagital_text_rotate");
            if (errorData)
            {
                value = "0";
                image_index = error_index;
                value_lenght = 1;
                unit_index = -1;
                unit_in_alignment = false;
            }
            //value = "-10";
            //elementName = "ElementWeather";
            if (image_index < 0 || image_index >= ListImagesFullName.Count) return;
            Bitmap src = null;
            src = OpenFileStream(ListImagesFullName[image_index]);
            if (src == null) return;
            int image_width = src.Width;
            int image_height = src.Height;

            int unit_width = -1;
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[unit_index]);
                if (src != null) unit_width = src.Width;
            }

            int dot_image_width = -1;
            if (dot_image_index >= 0 && dot_image_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[dot_image_index]);
                if (src != null) dot_image_width = src.Width;
            }

            int separator_width = -1;
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                if (src != null) separator_width = src.Width;
            }

            Matrix transformMatrix = graphics.Transform;
            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(angle);
            int posOffset = 0;
            int DateLenght = image_width * value.Length + spacing * (value.Length - 1);
            if ((elementName == "ElementDistance" || elementName == "ElementSunrise") && dot_image_width > 0)
                DateLenght = DateLenght - image_width + dot_image_width;
            if (elementName == "ElementWeather" && dot_image_width > 0 && value.StartsWith("-"))
                DateLenght = DateLenght - image_width + dot_image_width;
            if (elementName == "ElementWeather_Max/Min")
            {
                if (dot_image_width > 0)
                {
                    if (value.IndexOf("-") >= 0) DateLenght = DateLenght - image_width + dot_image_width;
                    if (value.LastIndexOf("-") > value.IndexOf("-")) DateLenght = DateLenght - image_width + dot_image_width;
                }
                if (separator_width > 0) DateLenght = DateLenght - image_width + separator_width;
                else DateLenght = DateLenght - image_width;
            }
            if (unit_in_alignment && unit_width > 0) DateLenght = DateLenght + unit_width + spacing;


            switch (horizontal_alignment)
            {
                //case "LEFT":
                //    posOffset = 0;
                //    break;
                case "CENTER_H":
                    posOffset = -DateLenght / 2;
                    break;
                case "RIGHT":
                    posOffset = -DateLenght;
                    break;
            }

            try
            {
                foreach (char ch in value)
                {
                    int index = 0;
                    if (Int32.TryParse(ch.ToString(), out index)) // если число 
                    {
                        src = OpenFileStream(ListImagesFullName[image_index + index]);
                        if (src != null)
                        {
                            graphics.DrawImage(src, posOffset, 0);
                            posOffset += image_width + spacing;
                        }
                    }
                    else // если разделитель 
                    {
                        if (ch == '*') 
                        {
                            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
                            {
                                src = OpenFileStream(ListImagesFullName[separator_index]);
                                if (src != null)
                                {
                                    graphics.DrawImage(src, posOffset, 0);
                                    posOffset += separator_width + spacing;
                                } 
                            }
                        }
                        else if (dot_image_index >= 0 && dot_image_index < ListImagesFullName.Count)
                        {
                            src = OpenFileStream(ListImagesFullName[dot_image_index]);
                            if (src != null)
                            {
                                graphics.DrawImage(src, posOffset, 0);
                                posOffset += dot_image_width + spacing;
                            }
                        }
                    }
                }
                if (unit_width > 0) // единицы измерения
                {
                    src = OpenFileStream(ListImagesFullName[unit_index]);
                    if (src != null)
                    {
                        graphics.DrawImage(src, posOffset, 0);
                    }
                }
            }
            finally
            {
                graphics.Transform = transformMatrix;
                src.Dispose();
            }

            if (BBorder)
            {
                //DateLenght = image_width * value_lenght + spacing * (value_lenght - 1);
                //if ((elementName == "ElementDistance" || elementName == "ElementSunrise") && dot_image_width > 0)
                //    DateLenght = DateLenght + spacing + dot_image_width;
                //if (elementName == "ElementWeather" && dot_image_width > 0/* && value.StartsWith("-")*/)
                //    DateLenght = DateLenght + spacing + dot_image_width;
                //if (unit_in_alignment && unit_width > 0)
                //    DateLenght = DateLenght + spacing + unit_width;

                switch (horizontal_alignment)
                {
                    case "LEFT":
                        posOffset = 0;
                        break;
                    case "CENTER_H":
                        posOffset = -DateLenght / 2;
                        break;
                    case "RIGHT":
                        posOffset = -DateLenght;
                        break;
                }
                graphics.TranslateTransform(x, y);
                graphics.RotateTransform(angle);
                Logger.WriteLine("DrawBorder");
                Rectangle rect = new Rectangle(posOffset, 0, DateLenght - 1, image_height - 1);
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawRectangle(pen1, rect);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen2, rect);
                }
                graphics.Transform = transformMatrix;
            }

            Logger.WriteLine("* Draw_dagital_text_rotate (end)");
        }

        /// <summary>Пишем число по окружности</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="radius">Радиус y</param>
        /// <param name="spacing">Угол между символами</param>
        /// <param name="angle">Угол поворота надписи в градусах</param>
        /// <param name="zero">Отображать начальные нули</param>
        /// <param name="image_index">Номер начального изображения</param>
        /// <param name="image_width">Ширина символа</param>
        /// <param name="image_height">Высота символа</param>
        /// <param name="unit_index">Номер символа единиц измерения</param>
        /// <param name="unit_width">Ширина символа единиц измерения</param>
        /// <param name="dot_image_index">Номер символа десятичного разделителя</param>
        /// <param name="dot_image_width">Ширина символа десятичного разделителя</param>
        /// <param name="separator_index">Номер символа разделителя</param>
        /// <param name="vertical_alignment">Выравнивание символов по вертикали относительно окружности (TOP, CENTER_V, BOTTOM)</param>
        /// <param name="horizontal_alignment">Выравнивание символов по горизонтали относительно окружности (LEFT, CENTER_H, RIGHT)</param>
        /// <param name="reverse_direction">Обратное направление</param>
        /// <param name="unit_in_alignment">Учитывать единицы измерения при выравнивании</param>
        /// 
        /// <param name="value">Отображаемая величина</param>
        /// <param name="value_lenght">Количество отображаемых символов</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="elementName">Название элемента</param>
        private void Draw_dagital_text_on_circle(Graphics graphics, int x, int y, int radius, int spacing, float angle, bool zero,
            int image_index, /*int image_width, int image_height,*/ int unit_index, /*int unit_width,*/ int dot_image_index, /*int dot_image_width,*/
            string vertical_alignment, string horizontal_alignment, bool reverse_direction, bool unit_in_alignment, 
            string value, int value_lenght, bool BBorder, bool showCentrHend, int separator_index, 
            int error_index, bool errorData, string elementName = "")
        {
            Logger.WriteLine("* Draw_dagital_text_on_circle");
            if (errorData)
            {
                value = "0";
                image_index = error_index;
                value_lenght = 1;
                unit_index = -1;
                unit_in_alignment = false;
            }
            //value = "-10";
            //elementName = "ElementWeather";
            if (image_index < 0 || image_index >= ListImagesFullName.Count) return;
            Bitmap src = null;
            src = OpenFileStream(ListImagesFullName[image_index]);
            if (src == null) return;
            int image_width = src.Width;
            int image_height = src.Height;

            int unit_width = -1;
            if (unit_index >= 0 && unit_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[unit_index]);
                if (src != null) unit_width = src.Width; 
            }

            int dot_image_width = -1;
            if (dot_image_index >= 0 && dot_image_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[dot_image_index]);
                if (src != null) dot_image_width = src.Width;
            }

            int separator_width = -1;
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                if (src != null) separator_width = src.Width;
            }

            double image_angle = 0;
            double unit_angle = 0;
            double dot_image_angle = 0;
            double separator_angle = 0;
            //spacing = spacing / 2f;
            image_angle = ToDegree(Math.Atan2(image_width / 2f, radius));
            if (unit_width > 0) unit_angle = ToDegree(Math.Atan2(unit_width / 2f, radius));
            if (dot_image_width > 0) dot_image_angle = ToDegree(Math.Atan2(dot_image_width / 2f, radius));
            if (separator_width > 0) separator_angle = ToDegree(Math.Atan2(separator_width / 2f, radius));

            Matrix transformMatrix = graphics.Transform;
            graphics.TranslateTransform(x, y);
            double angleOffset = image_angle * (value.Length - 1);
            if ((elementName == "ElementDistance" || elementName == "ElementSunrise") && dot_image_width > 0)
                angleOffset = angleOffset - image_angle + dot_image_angle;
            if (elementName == "ElementWeather" && dot_image_width > 0 && value.StartsWith("-"))
                angleOffset = angleOffset - image_angle + dot_image_angle;
            if (elementName == "ElementWeather_Max/Min")
            {
                if (dot_image_width > 0)
                {
                    if (value.IndexOf("-") >= 0) angleOffset = angleOffset - image_angle + dot_image_angle;
                    if (value.LastIndexOf("-") > value.IndexOf("-")) angleOffset = angleOffset - image_angle + dot_image_angle;
                }
                if (separator_width > 0) angleOffset = angleOffset - image_angle + separator_angle;
                else angleOffset = angleOffset - image_angle;
            }

            float startAngle = 0;
            double fullAngle = value_lenght * image_angle * 2 + (value_lenght - 1) * spacing;
            if (unit_width > 0 & unit_in_alignment) fullAngle += unit_angle * 2 + spacing;
            //if (dot_image_width > 0) 
            //{ 
            //    fullAngle += dot_image_angle * 2 + spacing;
            //    if (elementName == "ElementWeather_Max/Min") fullAngle += dot_image_angle * 2 + spacing;
            //}
            if (separator_width > 0) fullAngle += separator_angle * 2 + spacing;
            Pen pen = new Pen(Color.White, image_height);

            switch (horizontal_alignment)
            {
                case "LEFT":
                    graphics.RotateTransform(angle);
                    startAngle = angle;
                    break;
                case "CENTER_H":
                    angleOffset = angleOffset + spacing * (value.Length - 1) / 2f;
                    if (unit_width > 0 && unit_in_alignment) angleOffset = angleOffset + (image_angle + unit_angle + spacing) / 2f;

                    if (elementName == "ElementWeather" && dot_image_width > 0 && value.StartsWith("-"))
                        angleOffset = angleOffset + (dot_image_angle - image_angle) / 2f;

                    if (elementName == "ElementWeather_Max/Min" && dot_image_width > 0 && value.StartsWith("-"))
                        angleOffset = angleOffset + (dot_image_angle - image_angle) / 2f;

                    //if (elementName == "ElementWeather_Max/Min")
                    //{
                    //    if (dot_image_width > 0)
                    //    {
                    //        if (value.IndexOf("-") >= 0) angleOffset = angleOffset + (dot_image_angle - image_angle) / 2f;
                    //        if (value.LastIndexOf("-") > value.IndexOf("-")) angleOffset = angleOffset + (dot_image_angle - image_angle) / 2f;
                    //    }
                    //    if (separator_width > 0) angleOffset = angleOffset + (separator_angle - image_angle) / 2f;
                    //}
                    if (reverse_direction) angleOffset = -angleOffset;
                    graphics.RotateTransform((float)(angle - angleOffset));
                    if (unit_in_alignment && unit_width >= 0)
                    {
                        if (reverse_direction) startAngle = (float)(angle + fullAngle / 2 - (image_angle + unit_angle) / 2);
                        else startAngle = (float)(angle - fullAngle / 2 + (image_angle + unit_angle) / 2); 
                    }
                    else
                    {
                        if (reverse_direction) startAngle = (float)(angle + fullAngle / 2 - image_angle);
                        else startAngle = (float)(angle - fullAngle / 2 + image_angle);
                    }
                    break;
                case "RIGHT":
                    angleOffset = 2 * angleOffset + spacing * (value.Length - 1);
                    if (unit_width > 0 && unit_in_alignment) angleOffset = angleOffset + image_angle + unit_angle + spacing;

                    if (elementName == "ElementWeather" && dot_image_width > 0 && value.StartsWith("-"))
                        angleOffset = angleOffset - dot_image_angle + image_angle;

                    if (elementName == "ElementWeather_Max/Min" && dot_image_width > 0 && value.StartsWith("-"))
                        angleOffset = angleOffset - dot_image_angle + image_angle;

                    //if (elementName == "ElementWeather_Max/Min")
                    //{
                    //    if (dot_image_width > 0)
                    //    {
                    //        if (value.IndexOf("-") >= 0) angleOffset = angleOffset - image_angle + dot_image_angle;
                    //        if (value.LastIndexOf("-") > value.IndexOf("-")) angleOffset = angleOffset - image_angle + dot_image_angle;
                    //    }
                    //    if (separator_width > 0) angleOffset = angleOffset - image_angle + separator_angle;
                    //}

                    if (reverse_direction) angleOffset = -angleOffset;
                    graphics.RotateTransform((float)(angle - angleOffset));
                    if (unit_in_alignment && unit_width >= 0)
                    {
                        if (reverse_direction) startAngle = (float)(angle + fullAngle - image_angle - unit_angle);
                        else startAngle = (float)(angle - fullAngle + image_angle + unit_angle); 
                    }
                    else
                    {
                        if (reverse_direction) startAngle = (float)(angle + fullAngle - 2 * image_angle);
                        else startAngle = (float)(angle - fullAngle + 2 * image_angle);
                    }
                    break;
            }
            if (reverse_direction) spacing = -spacing;

            try
            {
                bool firstSymbol = true;
                if (!reverse_direction)
                {
                    if (vertical_alignment == "CENTER_V") radius += image_height / 2;
                    if (vertical_alignment == "BOTTOM") radius += image_height;

                    foreach (char ch in value)
                    {
                        int index = 0;
                        if (Int32.TryParse(ch.ToString(), out index)) // если число 
                        {
                            src = OpenFileStream(ListImagesFullName[image_index + index]);
                            if (src != null)
                            {
                                if (!firstSymbol) graphics.RotateTransform((float)image_angle);
                                firstSymbol = false;
                                graphics.DrawImage(src, -image_width / 2, -radius);
                                graphics.RotateTransform((float)(image_angle + spacing));
                            }
                        }
                        else // если разделитель 
                        {
                            if (ch == '*')
                            {
                                if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
                                {
                                    src = OpenFileStream(ListImagesFullName[separator_index]);
                                    if (src != null)
                                    {
                                        //if (firstSymbol) graphics.RotateTransform(-(float)image_angle);
                                        if (!firstSymbol) graphics.RotateTransform((float)separator_angle);
                                        firstSymbol = false;
                                        graphics.DrawImage(src, -separator_width / 2, -radius);
                                        graphics.RotateTransform((float)(separator_angle + spacing));
                                    } 
                                }
                            }
                            else if(dot_image_index >= 0 && dot_image_index < ListImagesFullName.Count)
                            {
                                src = OpenFileStream(ListImagesFullName[dot_image_index]);
                                if (src != null)
                                {
                                    //if (firstSymbol) graphics.RotateTransform(-(float)image_angle);
                                    if (!firstSymbol) graphics.RotateTransform((float)dot_image_angle);
                                    firstSymbol = false;
                                    graphics.DrawImage(src, -dot_image_width / 2, -radius);
                                    graphics.RotateTransform((float)(dot_image_angle + spacing));
                                } 
                            }
                        }
                    }
                    if (unit_width > 0) // единицы измерения
                    {
                        src = OpenFileStream(ListImagesFullName[unit_index]);
                        if (src != null)
                        {
                            graphics.RotateTransform((float)unit_angle);
                            graphics.DrawImage(src, -unit_width / 2, -radius);
                            //graphics.RotateTransform((float)dot_image_angle);
                        }
                    } 
                }
                else // обратное направление
                {
                    if (vertical_alignment == "CENTER_V") radius -= image_height / 2;
                    if (vertical_alignment == "BOTTOM") radius -= image_height;

                    graphics.RotateTransform(180);
                    foreach (char ch in value)
                    {
                        int index = 0;
                        if (Int32.TryParse(ch.ToString(), out index)) // если число 
                        {
                            src = OpenFileStream(ListImagesFullName[image_index + index]);
                            if (src != null)
                            {
                                if (!firstSymbol) graphics.RotateTransform(-(float)image_angle);
                                firstSymbol = false;
                                graphics.DrawImage(src, -image_width / 2, radius);
                                graphics.RotateTransform(-(float)(image_angle - spacing));
                            }
                        }
                        else // если разделитель 
                        {
                            if (ch == '*')
                            {
                                if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
                                {
                                    src = OpenFileStream(ListImagesFullName[separator_index]);
                                    if (src != null)
                                    {
                                        //if (firstSymbol) graphics.RotateTransform(-(float)image_angle);
                                        if (!firstSymbol) graphics.RotateTransform(-(float)separator_angle);
                                        firstSymbol = false;
                                        graphics.DrawImage(src, -separator_width / 2, radius);
                                        graphics.RotateTransform(-(float)(separator_angle - spacing));
                                    }
                                }
                            }
                            else if (dot_image_index >= 0 && dot_image_index < ListImagesFullName.Count)
                            {
                                src = OpenFileStream(ListImagesFullName[dot_image_index]);
                                if (src != null)
                                {
                                    if (!firstSymbol) graphics.RotateTransform(-(float)dot_image_angle);
                                    firstSymbol = false;
                                    graphics.DrawImage(src, -dot_image_width / 2, radius);
                                    graphics.RotateTransform(-(float)(dot_image_angle - spacing));
                                }
                            }
                        }
                    }
                    if (unit_width > 0) // единицы измерения
                    {
                        src = OpenFileStream(ListImagesFullName[unit_index]);
                        if (src != null)
                        {
                            graphics.RotateTransform(-(float)unit_angle);
                            graphics.DrawImage(src, -unit_width / 2, radius);
                            //graphics.RotateTransform((float)dot_image_angle);
                        }
                    }
                }
            }
            finally
            {
                graphics.Transform = transformMatrix;
                src.Dispose();
            }

            if (BBorder)
            {
                float radiusArc = radius;
                if (!reverse_direction)
                {
                    startAngle -= 90 + (float)image_angle;
                    radiusArc = radius - image_height;
                    radiusArc += image_height / 2f;
                }
                else
                {
                    startAngle -= 90 - (float)image_angle;
                    radiusArc = radius + image_height;
                    radiusArc -= image_height / 2f;
                    fullAngle = -fullAngle;
                }
                // подсвечивание поле надписи заливкой
                HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent20, Color.White, Color.Transparent);
                pen.Brush = myHatchBrush;
                graphics.DrawArc(pen, x - radiusArc, y - radiusArc, 2 * radiusArc, 2 * radiusArc, startAngle, (float)fullAngle);
                myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.Transparent);
                pen.Brush = myHatchBrush;
                graphics.DrawArc(pen, x - radiusArc, y - radiusArc, 2 * radiusArc, 2 * radiusArc, startAngle, (float)fullAngle);

                // подсвечивание внешней и внутреней дуги
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawArc(pen1, x - radius, y - radius, 2 * radius, 2 * radius, startAngle, (float)fullAngle);
                    int ArcWidth = (int)(radius - image_height);
                    if (reverse_direction) ArcWidth = (int)(radius + image_height);
                    if (ArcWidth < 1) ArcWidth = 1;
                    graphics.DrawArc(pen1, x - ArcWidth, y - ArcWidth, 2 * ArcWidth, 2 * ArcWidth, startAngle, (float)fullAngle);
                }
                using (Pen pen1 = new Pen(Color.Black, 1))
                {
                    pen1.DashStyle = DashStyle.Dash;
                    graphics.DrawArc(pen1, x - radius, y - radius, 2 * radius, 2 * radius, startAngle, (float)fullAngle);
                    int ArcWidth = (int)(radius - image_height);
                    if (reverse_direction) ArcWidth = (int)(radius + image_height);
                    if (ArcWidth < 1) ArcWidth = 1;
                    graphics.DrawArc(pen1, x - ArcWidth, y - ArcWidth, 2 * ArcWidth, 2 * ArcWidth, startAngle, (float)fullAngle);
                }
            }

            if (showCentrHend)
            {
                Logger.WriteLine("Draw showCentrHend");
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawLine(pen1, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen1, new Point(x, y - 5), new Point(x, y + 5));
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawLine(pen2, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen2, new Point(x, y - 5), new Point(x, y + 5));
                }
            }

            Logger.WriteLine("* Draw_dagital_text_on_circle (end)");
        }

        private double ToDegree(double radian)
        {
            return radian * (180 / Math.PI);
        }

        /// <summary>Пишем число системным шрифтом по окружности</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="radius">Радиус y</param>
        /// <param name="size">Размер шрифта</param>
        /// <param name="spacing">Величина отступа</param>
        /// <param name="color">Цвет шрифта</param>
        /// <param name="angle">Угол поворота надписи в градусах</param>
        /// <param name="rotate_direction">Направление текста</param>
        /// <param name="value">Отображаемая величина</param>
        /// <param name="addZero">Отображать начальные нули</param>
        /// <param name="value_lenght">Количество отображаемых символов</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="ActivityType">Номер активности (при необходимости)</param>
        private float Draw_text_rotate(Graphics graphics, int x, int y, int radius, float size, int spacing,
            Color color, float angle, int rotate_direction, string value, bool BBorder, int ActivityType = 0)
        {
            while (spacing > 127)
            {
                spacing = spacing - 255;
            }
            while (spacing < -127)
            {
                spacing = spacing + 255;
            }

            size = size * 0.9f;
            if (radius == 0) radius = 1;
            //Font drawFont = new Font("Times New Roman", size, GraphicsUnit.World);
            Font drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
            StringFormat strFormat = new StringFormat();
            strFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Far;
            Size strSize1 = TextRenderer.MeasureText(graphics, "0", drawFont);
            Size strSize2 = TextRenderer.MeasureText(graphics, "00", drawFont);
            int chLenght = strSize2.Width - strSize1.Width;
            int offsetX = strSize1.Width - chLenght;
            //float offsetY = 1.1f * (strSize1.Height - size);
            float offsetY = (strSize1.Height - size);

            Logger.WriteLine("* Draw_text_rotate");
            char[] CH = value.ToCharArray();

            int PointX = (int)(-0.3 * offsetX);

            Logger.WriteLine("Draw value");
            Matrix transformMatrix = graphics.Transform;
            SolidBrush drawBrush = new SolidBrush(color);
            graphics.TranslateTransform(x, y);

            if (rotate_direction == 1)
            {
                radius = -radius;
                //PointX = (int)(0.7 * offsetX);
                graphics.RotateTransform(-angle + 180);
            }
            else
            {
                graphics.RotateTransform(angle);
            }

            float offset_angle = 0;
            try
            {
                foreach (char ch in CH)
                {
                    string str = ch.ToString();
                    Size strSize = TextRenderer.MeasureText(graphics, str, drawFont);
                    //SizeF stringSize = graphics.MeasureString(str, drawFont, 10000, strFormat);
                    //graphics.FillRectangle(new SolidBrush(Color.White), x + PointX, y + offsetY - strSize.Height, strSize.Width, strSize.Height);
                    graphics.DrawString(str, drawFont, drawBrush, PointX, offsetY - radius, strFormat);

                    double sircle_lenght_relative = (strSize.Width + spacing - offsetX) / (2 * Math.PI * radius);
                    offset_angle = ((float)(sircle_lenght_relative * 360));
                    offset_angle = offset_angle * 1.05f;
                    //if (rotate_direction == 1) offset_angle = -offset_angle;
                    graphics.RotateTransform(offset_angle);
                    //PointX = PointX + strSize.Width + spacing - offsetX;
                }

            }
            finally
            {
                //graphics.ResetTransform();
                graphics.Transform = transformMatrix;
            }

            Logger.WriteLine("* Draw_text_rotate (end)");
            return offset_angle;
        }

        /// <summary>Рисует число с десятичным разделителем набором картинок</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="spacing">Величина отступа</param>
        /// <param name="alignment">Выравнивание</param>
        /// <param name="value">Отображаемая величина</param>
        /// <param name="addZero">Отображать начальные нули</param>
        /// <param name="value_lenght">Количество отображаемых символов</param>
        /// <param name="separator_index">Символ разделителя (единиц измерения)</param>
        /// <param name="decimalPoint_index">Символ десятичного разделителя</param>
        /// <param name="decCount">Число знаков после запятой</param>
        /// <param name="angle">Угол наклона текста</param>
        /// <param name="alpha">Прозрачность</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        private int Draw_dagital_text_decimal(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, float value, bool addZero, int value_lenght, int separator_index,
            int decimalPoint_index, int decCount, int angle,int alpha, bool BBorder, string elementName = "")
        {
            Logger.WriteLine("* Draw_dagital_text");
            if (SelectedModel.versionOS < 2) angle = 0;
            string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string data_numberS = value.ToString();
            if (decCount > 0)
            {
                if (data_numberS.IndexOf(decimalSeparator) < 0) data_numberS += decimalSeparator;
                while (data_numberS.IndexOf(decimalSeparator) > data_numberS.Length - decCount - 1)
                {
                    data_numberS += "0";
                }
            }
            if (addZero)
            {
                while (data_numberS.Length <= value_lenght)
                {
                    data_numberS = "0" + data_numberS;
                }
            }
            int DateLenghtReal = 0;
            int _number;
            int i;
            var src = new Bitmap(1, 1);
            char[] CH = data_numberS.ToCharArray();

            Logger.WriteLine("DateLenght");
            src = OpenFileStream(ListImagesFullName[image_index]);
            int width = src.Width;
            int height = src.Height;

            int DateLenght = 4 * width + 1 * spacing;
            //if(elementName == "ElementSunrise") DateLenght = 5 * width + 2 * spacing;
            if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                DateLenght = DateLenght + src.Width + spacing;
            }
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                if (SelectedModel.versionOS >= 1.5)
                {
                    DateLenght = DateLenght + src.Width + spacing ; 
                }
                else
                {
                    DateLenght = DateLenght + src.Width + src.Width + spacing + spacing;
                }
            }

            Logger.WriteLine("DateLenghtReal");
            foreach (char ch in CH)
            {
                _number = 0;
                if (int.TryParse(ch.ToString(), out _number))
                {
                    i = image_index + _number;
                    if (i < ListImagesFullName.Count)
                    {
                        DateLenghtReal = DateLenghtReal + width + spacing;
                    }
                }
                else
                {
                    if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                        DateLenghtReal = DateLenghtReal + src.Width + spacing;
                    }
                }

            }
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                DateLenghtReal = DateLenghtReal + src.Width + spacing;
            }
            DateLenghtReal = DateLenghtReal - spacing;

            int PointX = x;
            int offsetX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    offsetX = 0;
                    break;
                case 1:
                    offsetX = DateLenght / 2 - DateLenghtReal / 2;
                    break;
                case 2:
                    offsetX = DateLenght - DateLenghtReal;
                    break;
                default:
                    offsetX = 0;
                    break;
            }

            Matrix transformMatrix = graphics.Transform;
            if (SelectedModel.versionOS >= 2)
            {
                int pivot_point_offset_x = 0;
                int pivot_point_offset_y = 0;
                Calculation_pivot_point_offset(width, height, angle, out pivot_point_offset_x, out pivot_point_offset_y);
                graphics.TranslateTransform(PointX, PointY);
                graphics.TranslateTransform(pivot_point_offset_x, pivot_point_offset_y);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-PointX, -PointY);
            }

            Logger.WriteLine("Draw value");
            foreach (char ch in CH)
            {
                _number = 0;
                if (int.TryParse(ch.ToString(), out _number))
                {
                    i = image_index + _number;
                    if (i < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[i]);
                        if (SelectedModel.versionOS >= 2.1 && alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(PointX + offsetX, PointY, w, h);
                            graphics.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else graphics.DrawImage(src, PointX + offsetX, PointY);
                        PointX = PointX + width + spacing;
                    }
                }
                else
                {
                    if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                        if (SelectedModel.versionOS >= 2.1 && alpha != 255)
                        {
                            int w = src.Width;
                            int h = src.Height;
                            // Создаем матрицу цветов для изменения прозрачности (альфа-канал)
                            ColorMatrix colorMatrix = new ColorMatrix();
                            colorMatrix.Matrix33 = alpha / 255f; // значение от 0 до 1

                            // Создаем объект ImageAttributes и применяем к нему матрицу цветов
                            ImageAttributes imgAttributes = new ImageAttributes();
                            imgAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                            // Указываем прямоугольник, куда будет помещено изображение
                            Rectangle rect_alpha = new Rectangle(PointX + offsetX, PointY, w, h);
                            graphics.DrawImage(src, rect_alpha, 0, 0, w, h, GraphicsUnit.Pixel, imgAttributes);
                        }
                        else graphics.DrawImage(src, PointX + offsetX, PointY);
                        PointX = PointX + src.Width + spacing;
                    }
                }

            }
            int result = PointX + offsetX;
            if (separator_index > -1)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                graphics.DrawImage(src, new Rectangle(PointX + offsetX, PointY, src.Width, src.Height));
                result = result + src.Width + spacing;
            }
            src.Dispose();

            if (BBorder)
            {
                Logger.WriteLine("DrawBorder");
                Rectangle rect = new Rectangle(x, y, DateLenght - 1, height - 1);
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawRectangle(pen1, rect);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen2, rect);
                }
            }
            //graphics.ResetTransform();
            graphics.Transform = transformMatrix;

            Logger.WriteLine("* Draw_dagital_text (end)");
            return result;
        }

        /// <summary>круговая шкала</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="radius">Радиус</param>
        /// <param name="width">Толщина линии</param>
        /// <param name="lineCap">Тип окончания линии</param>
        /// <param name="startAngle">Начальный угол</param>
        /// <param name="fullAngle">Общий угол</param>
        /// <param name="position">Отображаемая величина от 0 до 1</param>
        /// <param name="color">Цвет шкалы</param>
        /// <param name="inversion">Инверсия шкалы</param>
        /// <param name="showProgressArea">Подсвечивать полную длину шкалы</param>
        private void DrawScaleCircle(Graphics graphics, int x, int y, float radius, float width,
            int lineCap, float startAngle, float fullAngle, float position, Color color,
            bool inversion, bool showProgressArea, bool showCentrHend)
        {
            if (radius <= width) return;
            Logger.WriteLine("* DrawScaleCircle");
            startAngle = startAngle - 90;
            if (position > 1) position = 1;
            if (inversion)
            {
                startAngle = startAngle + fullAngle;
                fullAngle = -fullAngle;
                position = 1 - position;
            }
            float valueAngle = fullAngle * position;
            //if (valueAngle == 0) return;
            Pen pen = new Pen(color, width);

            switch (lineCap)
            {
                case 0:
                    pen.EndCap = LineCap.Round;
                    pen.StartCap = LineCap.Round;
                    break;
                case 1:
                    pen.EndCap = LineCap.Flat;
                    pen.StartCap = LineCap.Round;
                    break;
                case 2:
                    pen.EndCap = LineCap.Round;
                    pen.StartCap = LineCap.Flat;
                    break;
                case 3:
                    pen.EndCap = LineCap.Flat;
                    pen.StartCap = LineCap.Flat;
                    break;
                default:
                    pen.EndCap = LineCap.Round;
                    pen.StartCap = LineCap.Round;
                    break;
            }

            //int srcX = (int)Math.Round(x - radius - width / 2, MidpointRounding.AwayFromZero);
            //int srcY = (int)Math.Round(y - radius - width / 2, MidpointRounding.AwayFromZero);
            //int srcX = (int)(x - radius - width / 2);
            //int srcY = (int)(y - radius - width / 2);
            int srcX = (int)(x - radius);
            int srcY = (int)(y - radius);
            //int arcX = (int)(x - radius);
            //int arcY = (int)(y - radius);
            int arcX = (int)(x - radius + width / 2);
            int arcY = (int)(y - radius + width / 2);
            float CircleWidth = 2 * radius - width;

            try
            {
                int s = Math.Sign(valueAngle);
                double arcLenght = s * Math.PI * (2 * radius - width) * valueAngle / 360;
                //if (pen.EndCap == LineCap.Round && pen.StartCap == LineCap.Round)
                //{
                    if (arcLenght < width)
                    {
                        pen.EndCap = LineCap.Flat;
                        pen.StartCap = LineCap.Flat;
                    } 
                //}
                //else if (pen.EndCap == LineCap.Round || pen.StartCap == LineCap.Round)
                //{
                //    if (arcLenght < width/2)
                //    {
                //        pen.EndCap = LineCap.Flat;
                //        pen.StartCap = LineCap.Flat;
                //    }
                //}
                double anglOffset = (width / 2) * 360 / (2 * radius * Math.PI);
                float start_angl = startAngle;
                float end_angl = valueAngle;
                if(pen.StartCap == LineCap.Round)
                {
                    start_angl = (float)(start_angl + s * anglOffset);
                    end_angl = (float)(end_angl - s * anglOffset);
                }
                if(pen.EndCap == LineCap.Round) end_angl = (float)(end_angl - s * anglOffset);
                //float start_angl = (float)(startAngle + 0.08 * s * width);
                //float end_angl = (float)(valueAngle - 0.16 * s * width);
                graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth,start_angl, end_angl);
                //TODO исправить отрисовку при большой толщине
            }
            catch (Exception)
            {
            }

            //if (pointerIndex >= 0 && pointerIndex < ListImagesFullName.Count)
            //{
            //    src = OpenFileStream(ListImagesFullName[pointerIndex]);
            //    graphics.DrawImage(src, new Rectangle(srcX, srcX, src.Width, src.Height));
            //}

            if (showProgressArea)
            {
                // подсвечивание шкалы заливкой
                HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent20, Color.White, Color.Transparent);
                pen.Brush = myHatchBrush;
                int s = Math.Sign(fullAngle);
                float start_angl = (float)(startAngle + 0.08 * s * width);
                float end_angl = (float)(fullAngle - 0.16 * s * width);
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth, startAngle, endAngle);
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth,
                //    (float)(startAngle - 0.007 * s * width), (float)(fullAngle + 0.015 * s * width));
                graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth, start_angl, end_angl);
                myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.Transparent);
                pen.Brush = myHatchBrush;
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth, startAngle, endAngle);
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth,
                //    (float)(startAngle - 0.007 * s * width), (float)(fullAngle + 0.015 * s * width));
                graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth, start_angl, end_angl);

                // подсвечивание внешней и внутреней дуги на шкале
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawArc(pen1, srcX, srcY, CircleWidth + width, CircleWidth + width, startAngle, fullAngle);
                    int ArcWidth = (int)(CircleWidth - width);
                    if (ArcWidth < 1) ArcWidth = 1;
                    graphics.DrawArc(pen1, srcX + width, srcY + width, ArcWidth, ArcWidth, startAngle, fullAngle);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dash;
                    graphics.DrawArc(pen2, srcX, srcY, CircleWidth + width, CircleWidth + width, startAngle, fullAngle);
                    int ArcWidth = (int)(CircleWidth - width);
                    if (ArcWidth < 1) ArcWidth = 1;
                    graphics.DrawArc(pen2, srcX + width, srcY + width, ArcWidth, ArcWidth, startAngle, fullAngle);
                }
            }

            if (showCentrHend)
            {
                Logger.WriteLine("Draw showCentrHend");
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawLine(pen1, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen1, new Point(x, y - 5), new Point(x, y + 5));
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawLine(pen2, new Point(x - 5, y), new Point(x + 5, y));
                    graphics.DrawLine(pen2, new Point(x, y - 5), new Point(x, y + 5));
                }
            }
            Logger.WriteLine("* DrawScaleCircle (end)");

        }

        /// <summary>Линейная шкала</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="lenght">Длина шкалы</param>
        /// <param name="width">Толщина линии</param>
        /// <param name="lineCap">Тип окончания линии</param>
        /// <param name="pointer_index">Изображение указателя</param>
        /// <param name="vertical">Вертикальная или горизонтальная</param>
        /// <param name="position">Отображаемая величина от 0 до 1</param>
        /// <param name="color">Цвет шкалы</param>
        /// <param name="inversion">Инверсия шкалы</param>
        /// <param name="showProgressArea">Подсвечивать полную длину шкалы</param>
        private void DrawScaleLinear(Graphics graphics, int x, int y, int lenght, int width, string lineCap, int pointer_index,
            bool vertical, float position, Color color, bool inversion, bool showProgressArea)
        {
            Logger.WriteLine("* DrawScaleLinear");
            if (lineCap == "Rounded")
            {
                DrawScaleLinearRounded(graphics, x, y, lenght, width, lineCap, pointer_index, vertical, position, color, inversion, showProgressArea);
                return;
            }
            var src = new Bitmap(1, 1);
            if (position > 1) position = 1;
            if (!vertical)
            {
                if (inversion)
                {
                    x = x + lenght;
                    lenght = -lenght;
                    position = 1 - position;
                }
                try
                {
                    int realLenght = (int)(lenght * position);
                    Brush br = new SolidBrush(color);
                    Rectangle rc = new Rectangle(x, y, realLenght, width);
                    if (realLenght < 0) rc = new Rectangle(x + realLenght, y, -realLenght, width);
                    graphics.FillRectangle(br, rc);

                    if (pointer_index >= 0 && pointer_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[pointer_index]);
                        int pos_x = x + realLenght - src.Width / 2;
                        int pos_y = y + width/2 - src.Height / 2;
                        graphics.DrawImage(src, pos_x, pos_y);
                    }

                    if (showProgressArea)
                    {
                        HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent20, Color.White, Color.Transparent);

                        rc = new Rectangle(x, y, lenght, width);
                        if (lenght < 0) rc = new Rectangle(x + lenght, y, -lenght, width);
                        graphics.FillRectangle(myHatchBrush, rc);

                        myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.Transparent);
                        graphics.FillRectangle(myHatchBrush, rc);
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                if (inversion)
                {
                    y = y + lenght;
                    lenght = -lenght;
                    position = 1 - position;
                }
                try
                {
                    int realLenght = (int)(lenght * position);
                    Brush br = new SolidBrush(color);
                    Rectangle rc = new Rectangle(x, y, width, realLenght);
                    if (realLenght < 0) rc = new Rectangle(x, y + realLenght, width, -realLenght);
                    graphics.FillRectangle(br, rc);

                    if (pointer_index >= 0 && pointer_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[pointer_index]);
                        int pos_x = x + width / 2 - src.Width / 2;
                        int pos_y = y + realLenght - src.Height / 2;
                        graphics.DrawImage(src, pos_x, pos_y);
                    }

                    if (showProgressArea)
                    {
                        HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent20, Color.White, Color.Transparent);

                        rc = new Rectangle(x, y, width, lenght);
                        if (lenght < 0) rc = new Rectangle(x, y + lenght, width, -lenght);
                        graphics.FillRectangle(myHatchBrush, rc);

                        myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.Transparent);
                        graphics.FillRectangle(myHatchBrush, rc);
                    }
                }
                catch (Exception)
                {
                }
            }

            Logger.WriteLine("* DrawScaleLinear (end)");

        }

        /// <summary>Линейная шкала закругленная</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="lenght">Длина шкалы</param>
        /// <param name="width">Толщина линии</param>
        /// <param name="lineCap">Тип окончания линии</param>
        /// <param name="pointer_index">Изображение указателя</param>
        /// <param name="vertical">Вертикальная или горизонтальная</param>
        /// <param name="position">Отображаемая величина от 0 до 1</param>
        /// <param name="color">Цвет шкалы</param>
        /// <param name="inversion">Инверсия шкалы</param>
        /// <param name="showProgressArea">Подсвечивать полную длину шкалы</param>
        private void DrawScaleLinearRounded(Graphics graphics, int x, int y, int lenght, int width, string lineCap, int pointer_index,
            bool vertical, float position, Color color, bool inversion, bool showProgressArea)
        {
            Logger.WriteLine("* DrawScaleLinear");
            var src = new Bitmap(1, 1);
            if (position > 1) position = 1;
            if (!vertical)
            {
                if (inversion)
                {
                    x = x + lenght;
                    lenght = -lenght;
                    position = 1 - position;
                }
                try
                {
                    int realLenght = (int)(lenght * position);
                    Brush br = new SolidBrush(color); 
                    Rectangle rc = new Rectangle();
                    if (Math.Abs(realLenght) > width)
                    {
                        int radius = width / 2;
                        rc = new Rectangle(x + radius, y, realLenght - width, width);
                        Rectangle rcStart = new Rectangle(x, y, width, width);
                        Rectangle rcEnd = new Rectangle(x + realLenght - width, y, width, width);
                        if (realLenght < 0)
                        {
                            rc = new Rectangle(x + realLenght + radius, y, -realLenght - width, width);
                            rcStart = new Rectangle(x + realLenght, y, width, width);
                            rcEnd = new Rectangle(x - width, y, width, width);
                        }
                        graphics.FillRectangle(br, rc);
                        //br = new SolidBrush(Color.Red);
                        graphics.FillEllipse(br, rcStart);
                        //br = new SolidBrush(Color.Blue);
                        graphics.FillEllipse(br, rcEnd); 
                    }
                    else
                    {
                        rc = new Rectangle(x, y, realLenght, width);
                        //br = new SolidBrush(Color.Green);
                        graphics.FillEllipse(br, rc);
                    }

                    if (pointer_index >= 0 && pointer_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[pointer_index]);
                        int pos_x = x + realLenght - src.Width / 2;
                        int pos_y = y + width / 2 - src.Height / 2;
                        graphics.DrawImage(src, pos_x, pos_y);
                    }

                    if (showProgressArea)
                    {
                        HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent20, Color.White, Color.Transparent);

                        rc = new Rectangle(x, y, lenght, width);
                        if (lenght < 0) rc = new Rectangle(x + lenght, y, -lenght, width);
                        graphics.FillRectangle(myHatchBrush, rc);

                        myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.Transparent);
                        graphics.FillRectangle(myHatchBrush, rc);
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                if (inversion)
                {
                    y = y + lenght;
                    lenght = -lenght;
                    position = 1 - position;
                }
                try
                {
                    int realLenght = (int)(lenght * position);
                    Brush br = new SolidBrush(color);
                    Rectangle rc = new Rectangle();
                    if (Math.Abs(realLenght) > width)
                    {
                        int radius = width / 2;
                        rc = new Rectangle(x, y + radius, width, realLenght - width);
                        Rectangle rcStart = new Rectangle(x, y, width, width);
                        Rectangle rcEnd = new Rectangle(x, y + realLenght - width, width, width);
                        if (realLenght < 0)
                        {
                            rc = new Rectangle(x, y + realLenght + radius, width, -realLenght - width);
                            rcStart = new Rectangle(x, y + realLenght, width, width);
                            rcEnd = new Rectangle(x, y - width, width, width);
                        }
                        graphics.FillRectangle(br, rc);
                        //br = new SolidBrush(Color.Red);
                        graphics.FillEllipse(br, rcStart);
                        //br = new SolidBrush(Color.Blue);
                        graphics.FillEllipse(br, rcEnd);
                    }
                    else
                    {
                        rc = new Rectangle(x, y, width, realLenght);
                        //br = new SolidBrush(Color.Green);
                        graphics.FillEllipse(br, rc);
                    }

                    if (pointer_index >= 0 && pointer_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[pointer_index]);
                        int pos_x = x + width / 2 - src.Width / 2;
                        int pos_y = y + realLenght - src.Height / 2;
                        graphics.DrawImage(src, pos_x, pos_y);
                    }

                    if (showProgressArea)
                    {
                        HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent20, Color.White, Color.Transparent);

                        rc = new Rectangle(x, y, width, lenght);
                        if (lenght < 0) rc = new Rectangle(x, y + lenght, width, -lenght);
                        graphics.FillRectangle(myHatchBrush, rc);

                        myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.Transparent);
                        graphics.FillRectangle(myHatchBrush, rc);
                    }
                }
                catch (Exception)
                {
                }
            }

            Logger.WriteLine("* DrawScaleLinear (end)");

        }

        /// <summary>отображаем ярлыки</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="shortcut">Элемент с отображаемым ярлыклм</param>
        /// <param name="showShortcuts">Отображать ярлыки</param>
        /// <param name="showShortcutsArea">Подсвечивать область ярлыков рамкой</param>
        /// <param name="showShortcutsBorder">Подсвечивать область ярлыков заливкой</param>
        /// <param name="showShortcutsImage">Подсвечивать изображение, отображаемое при нажатии ярлыка</param>
        /// <param name="Shortcuts_In_Gif">Подсвечивать область с ярлыками (для gif файла)</param>
        private void DrawShortcuts(Graphics graphics, hmUI_widget_IMG_CLICK shortcut, bool showShortcuts,
             bool showShortcutsArea, bool showShortcutsBorder, bool showShortcutsImage, bool Shortcuts_In_Gif)
        {
            if (shortcut != null /*&& shortcut.src != null && shortcut.src.Length > 0*/ && shortcut.visible)
            {
                //int imageIndex = ListImages.IndexOf(shortcut.src);
                int x = shortcut.x;
                int y = shortcut.y;
                int width = shortcut.w;
                int height = shortcut.h;

                if (shortcut.src != null && shortcut.src.Length > 0)
                {
                    int imageIndex = ListImages.IndexOf(shortcut.src);
                    if (imageIndex < ListImagesFullName.Count)
                    {
                        Bitmap src = OpenFileStream(ListImagesFullName[imageIndex]);
                        int pos_x = x + width / 2 - src.Width / 2;
                        int pos_y = y + height / 2 - src.Height / 2;
                        if (pos_x >= x && pos_y >= y)
                        {
                            graphics.DrawImage(src, pos_x, pos_y);
                        }
                        else if (width > 0 && height > 0)
                        {
                            Rectangle cropRect = new Rectangle(x - pos_x, y - pos_y, width, height);
                            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                            using (Graphics g = Graphics.FromImage(target))
                            {
                                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                                 cropRect, GraphicsUnit.Pixel);
                            }
                            graphics.DrawImage(target, x, y);
                        }
                        src.Dispose();
                    }
                }

                if (showShortcuts)
                {
                    if (showShortcutsArea)
                    {
                        HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.White, Color.Transparent);
                        Rectangle rect = new Rectangle(x, y, width, height);
                        graphics.FillRectangle(myHatchBrush, rect);
                        myHatchBrush = new HatchBrush(HatchStyle.Percent05, Color.Black, Color.Transparent);
                        graphics.FillRectangle(myHatchBrush, rect);
                    }
                    if (showShortcutsBorder)
                    {
                        Rectangle rect = new Rectangle(x, y, width - 1, height - 1);
                        using (Pen pen1 = new Pen(Color.White, 1))
                        {
                            graphics.DrawRectangle(pen1, rect);
                        }
                        using (Pen pen2 = new Pen(Color.Black, 1))
                        {
                            pen2.DashStyle = DashStyle.Dot;
                            graphics.DrawRectangle(pen2, rect);
                        }
                    }
                }

                if (Shortcuts_In_Gif)
                {
                    HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.White, Color.Transparent);
                    Rectangle rect = new Rectangle(x, y, width, height);
                    graphics.FillRectangle(myHatchBrush, rect);
                    myHatchBrush = new HatchBrush(HatchStyle.Percent05, Color.Black, Color.Transparent);
                    graphics.FillRectangle(myHatchBrush, rect);

                    rect = new Rectangle(x, y, width - 1, height - 1);
                    using (Pen pen1 = new Pen(Color.White, 1))
                    {
                        graphics.DrawRectangle(pen1, rect);
                    }
                    using (Pen pen2 = new Pen(Color.Black, 1))
                    {
                        pen2.DashStyle = DashStyle.Dot;
                        graphics.DrawRectangle(pen2, rect);
                    }

                    Bitmap src = new Bitmap(Application.StartupPath + @"\Mask\shortcut_pointer.png");
                    int pos_x = x + width / 2 - 16;
                    int pos_y = y + height / 2 - 11;
                    graphics.DrawImage(src, pos_x, pos_y);
                }
            }
        }

        /// <summary>отображаем кнопки</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="button">Элемент с отображаемой кнопкой</param>
        /// <param name="showPressButton">Отображать изображение, отображаемое при нажатии кнопки</param>
        /// <param name="showButtons">Отображать кнопки</param>
        /// <param name="showButtonsArea">Подсвечивать область кнопки рамкой</param>
        /// <param name="showButtonsBorder">Подсвечивать область кнопки заливкой</param>
        /// <param name="Buttons_In_Gif">Подсвечивать область с кнопками (для gif файла)</param>
        private void DrawButton(Graphics graphics, Button button,  bool showPressButton, bool showButtons,
             bool showButtonsArea, bool showButtonsBorder, bool Buttons_In_Gif)
        {
            if (button == null) return; 
            if (!button.visible) return;
            int x = button.x;
            int y = button.y;
            int width = button.w;
            int height = button.h;

            if (button.normal_src != null && button.normal_src.Length > 0 && button.press_src != null && button.press_src.Length > 0)
            {
                int imageIndex = ListImages.IndexOf(button.normal_src);
                if (width < 0 || height < 0)
                {
                    if (imageIndex >= 0 && imageIndex < ListImagesFullName.Count)
                    {
                        Bitmap src = OpenFileStream(ListImagesFullName[imageIndex]);
                        if (width < 0) width = src.Width;
                        if (height < 0) height = src.Height;
                        src.Dispose();
                    } 
                }

                if (showPressButton && button.press_src != null && button.press_src.Length > 0)
                    imageIndex = ListImages.IndexOf(button.press_src);
                if (imageIndex >= 0 && imageIndex < ListImagesFullName.Count)
                {
                    Bitmap src = OpenFileStream(ListImagesFullName[imageIndex]);
                    int pos_x = x + width / 2 - src.Width / 2;
                    int pos_y = y + height / 2 - src.Height / 2;
                    if (pos_x >= x && pos_y >= y)
                    {
                        graphics.DrawImage(src, pos_x, pos_y);
                    }
                    else if (width > 0 && height > 0)
                    {
                        Rectangle cropRect = new Rectangle(x - pos_x, y - pos_y, width, height);
                        Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                        using (Graphics g = Graphics.FromImage(target))
                        {
                            g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                             cropRect, GraphicsUnit.Pixel);
                        }
                        graphics.DrawImage(target, x, y);
                    }
                    src.Dispose();
                }
            }
            else
            {
                if (width < 0) width = 100;
                if (height < 0) height = 40;

                string colorStr = button.normal_color;
                if (showPressButton) colorStr = button.press_color;
                Color color = StringToColor(colorStr);
                if (button.radius == 0) graphics.FillRectangle(new SolidBrush(color), new Rectangle(x, y, width, height));
                else
                {
                    // Задаем прямоугольник с закругленными углами
                    int radius = button.radius;
                    if (radius > width / 2) radius = width / 2;
                    if (radius > height / 2) radius = height / 2;
                    GraphicsPath path = new GraphicsPath();
                    path.AddArc(x, y, radius * 2, radius * 2, 180, 90);  // Левый верхний угол
                    path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);  // Правый верхний угол
                    path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);  // Правый нижний угол
                    path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);  // Левый нижний угол
                    path.CloseFigure();  // Закрываем фигуру
                    graphics.FillPath(new SolidBrush(color), path);
                }
            }

            if (button.text.Length > 0) Draw_text(graphics, x, y, width, height, button.text_size, 0, 0, StringToColor(button.color), 255,
                button.text, "CENTER_H", "CENTER_V", "ELLIPSIS", false);

            if (showButtons)
            {
                if (showButtonsArea)
                {
                    HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.White, Color.Transparent);
                    Rectangle rect = new Rectangle(x, y, width, height);
                    graphics.FillRectangle(myHatchBrush, rect);
                    myHatchBrush = new HatchBrush(HatchStyle.Percent05, Color.Black, Color.Transparent);
                    graphics.FillRectangle(myHatchBrush, rect);
                }
                if (showButtonsBorder)
                {
                    Rectangle rect = new Rectangle(x, y, width - 1, height - 1);
                    using (Pen pen1 = new Pen(Color.White, 1))
                    {
                        graphics.DrawRectangle(pen1, rect);
                    }
                    using (Pen pen2 = new Pen(Color.Black, 1))
                    {
                        pen2.DashStyle = DashStyle.Dot;
                        graphics.DrawRectangle(pen2, rect);
                    }
                } 
            }

            if (Buttons_In_Gif)
            {
                HatchBrush myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.White, Color.Transparent);
                Rectangle rect = new Rectangle(x, y, width, height);
                graphics.FillRectangle(myHatchBrush, rect);
                myHatchBrush = new HatchBrush(HatchStyle.Percent05, Color.Black, Color.Transparent);
                graphics.FillRectangle(myHatchBrush, rect);

                rect = new Rectangle(x, y, width - 1, height - 1);
                using (Pen pen1 = new Pen(Color.White, 1))
                {
                    graphics.DrawRectangle(pen1, rect);
                }
                using (Pen pen2 = new Pen(Color.Black, 1))
                {
                    pen2.DashStyle = DashStyle.Dot;
                    graphics.DrawRectangle(pen2, rect);
                }

                Bitmap src = new Bitmap(Application.StartupPath + @"\Mask\shortcut_pointer.png");
                int pos_x = x + width / 2 - 16;
                int pos_y = y + height / 2 - 11;
                graphics.DrawImage(src, pos_x, pos_y);
            }
        }

        private Bitmap OpenFileStream(string fileName)
        {
            Bitmap src = null;
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    src = new Bitmap(Image.FromStream(stream));
                } 
            }
            else
            {
                fileName = ProjectDir + @"\assets\" + fileName + ".png"; 
                if (File.Exists(fileName))
                {
                    using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        src = new Bitmap(Image.FromStream(stream));
                    }
                }
            }
            return src;
        }

        /// <summary>Имитируем обрезку изображения, заливая контур цветом фона</summary>
        private Bitmap FormColor(Bitmap bitmap)
        {
            Logger.WriteLine("* FormColor");
            //int[] bgColors = { 203, 255, 240 };
            Color color = pictureBox_Preview.BackColor;
            ImageMagick.MagickImage image = new ImageMagick.MagickImage(ImgConvert.CopyImageToByteArray(bitmap));
            // меняем прозрачный цвет на цвет фона
            image.Opaque(ImageMagick.MagickColor.FromRgba((byte)0, (byte)0, (byte)0, (byte)0),
                ImageMagick.MagickColor.FromRgb(color.R, color.G, color.B));
            // меняем черный цвет на прозрачный
            image.Opaque(ImageMagick.MagickColor.FromRgb((byte)0, (byte)0, (byte)0),
                ImageMagick.MagickColor.FromRgba((byte)0, (byte)0, (byte)0, (byte)0));

            Logger.WriteLine("* FormColor (end)");
            return image.ToBitmap();
        }

        /// <summary>Обрезаим изображение по маске</summary>
        public Bitmap ApplyMask(Bitmap inputImage, Bitmap mask)
        {
            Logger.WriteLine("* ApplyMask");
            ImageMagick.MagickImage image = new ImageMagick.MagickImage(ImgConvert.CopyImageToByteArray(inputImage));
            ImageMagick.MagickImage combineMask = new ImageMagick.MagickImage(ImgConvert.CopyImageToByteArray(mask));

            image.Composite(combineMask, ImageMagick.CompositeOperator.In, ImageMagick.Channels.Alpha);

            Logger.WriteLine("* ApplyMask (end)");
            return image.ToBitmap();
        }

        public Bitmap ApplyWidgetMask(Bitmap inputImage, string fg_mask)
        {
            Logger.WriteLine("* ApplyWidgetMask");
            ImageMagick.MagickImage image = new ImageMagick.MagickImage(ImgConvert.CopyImageToByteArray(inputImage));
            ImageMagick.MagickImage combineMask = new ImageMagick.MagickImage(fg_mask);
            combineMask.Level(127, 128, Channels.Alpha);
            image.Composite(combineMask, ImageMagick.CompositeOperator.Xor, Channels.Alpha);

            Logger.WriteLine("* ApplyWidgetMask (end)");
            return image.ToBitmap();
        }

        private int AlignmentToInt(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "LEFT":
                    result = 0;
                    break;
                case "CENTER_H":
                    result = 1;
                    break;
                case "RIGHT":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            return result;
        }

        private int AlignmentVerticalToInt(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "TOP":
                    result = 0;
                    break;
                case "CENTER_V":
                    result = 1;
                    break;
                case "BOTTOM":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            return result;
        }

        /// <summary>Масштабирование изображения</summary>
        /// <param name="image">Исходное изображение</param>
        /// <param name="scale">Масштаб</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, float scale)
        {
            if (scale <= 0) return new Bitmap(image);
            int width = (int)Math.Round(image.Width * scale);
            int height = (int)Math.Round(image.Height * scale);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public Bitmap ApplyWatchSkin(Bitmap bitmap)
        {
            string Watch_Skin_file_name = SelectedModel.watchSkin;
            //string Watch_Skin_file_name = textBox_WatchSkin_Path.Text;
            if (!File.Exists(Watch_Skin_file_name))
                Watch_Skin_file_name = Path.Combine(Application.StartupPath, "Skin", Watch_Skin_file_name);
            if (!File.Exists(Watch_Skin_file_name))
                Watch_Skin_file_name = Path.Combine(Application.StartupPath, Watch_Skin_file_name);

            WatchSkin Watch_Skin = new WatchSkin();
            if (File.Exists(Watch_Skin_file_name))
            {
                string text = File.ReadAllText(Watch_Skin_file_name);
                try
                {
                    Watch_Skin = JsonConvert.DeserializeObject<WatchSkin>(text, new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                        Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return bitmap;
                }
            }
            else return bitmap;

            /*Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
            if (ProgramSettings.Watch_Model == "GTR 3 Pro")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
            if (ProgramSettings.Watch_Model == "GTS 3")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
            if (ProgramSettings.Watch_Model == "GTR 4")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
            if (ProgramSettings.Watch_Model == "Amazfit Band 7")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
            if (ProgramSettings.Watch_Model == "GTS 4 mini")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
            if (ProgramSettings.Watch_Model == "Falcon")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
            if (ProgramSettings.Watch_Model == "GTR mini")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
            if (ProgramSettings.Watch_Model == "GTS 4")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");*/
            Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\" + SelectedModel.maskImage);

            bitmap = ApplyMask(bitmap, mask);
            //Graphics gPanel = Graphics.FromImage(bitmap);

            Bitmap BackgroundImage = null;
            Bitmap ForegroundImage = null;

            float BackgroundImageHeight = -1;
            float ImageHeight = -1;
            float ForegroundImageHeight = -1;

            // Background
            if (Watch_Skin.Background != null && Watch_Skin.Background.Path != null)
            {
                string file_name = Watch_Skin.Background.Path;
                if (!File.Exists(file_name)) file_name = Application.StartupPath + file_name;
                if (File.Exists(file_name))
                {
                    BackgroundImage = new Bitmap(file_name);
                    if (Watch_Skin.Background.ImageHeight != null) BackgroundImageHeight =
                            (int)Watch_Skin.Background.ImageHeight;
                    float scale = BackgroundImageHeight / BackgroundImage.Height;
                    BackgroundImage = ResizeImage(BackgroundImage, scale);
                }
            }

            // Image
            if (Watch_Skin.Image != null && Watch_Skin.Image.ImageHeight != null)
            {
                if (Watch_Skin.Image.ImageHeight != null) ImageHeight =
                            (int)Watch_Skin.Image.ImageHeight;
                float scale = ImageHeight / bitmap.Height;
                bitmap = ResizeImage(bitmap, scale);
            }

            Bitmap returnBitmap;
            if (BackgroundImage != null) returnBitmap = BackgroundImage;
            else returnBitmap = bitmap;
            Graphics gPanel = Graphics.FromImage(returnBitmap);

            if (BackgroundImage != null)
            {
                int posX = (int)(BackgroundImage.Width / 2f - bitmap.Width / 2f);
                int posY = (int)(BackgroundImage.Height / 2f - bitmap.Height / 2f);
                if (Watch_Skin.Image != null && Watch_Skin.Image.Position != null)
                {
                    posX = Watch_Skin.Image.Position.X;
                    posY = Watch_Skin.Image.Position.Y;
                }
                gPanel.DrawImage(bitmap, posX, posY, bitmap.Width, bitmap.Height);
            }

            // Foreground
            if (Watch_Skin.Foreground != null && Watch_Skin.Foreground.Path != null)
            {
                string file_name = Watch_Skin.Foreground.Path;
                if (!File.Exists(file_name)) file_name = Application.StartupPath + file_name;
                if (File.Exists(file_name))
                {
                    ForegroundImage = new Bitmap(file_name);
                    if (Watch_Skin.Foreground.ImageHeight != null) ForegroundImageHeight =
                            (int)Watch_Skin.Foreground.ImageHeight;
                    float scale = ForegroundImageHeight / ForegroundImage.Height;
                    ForegroundImage = ResizeImage(ForegroundImage, scale);
                }
            }

            if (ForegroundImage != null)
            {
                int posX = (int)(BackgroundImage.Width / 2f - ForegroundImage.Width / 2f);
                int posY = (int)(BackgroundImage.Height / 2f - ForegroundImage.Height / 2f);
                if (Watch_Skin.Image != null && Watch_Skin.Image.Position != null)
                {
                    posX = Watch_Skin.Foreground.Position.X;
                    posY = Watch_Skin.Foreground.Position.Y;
                }
                gPanel.DrawImage(ForegroundImage, posX, posY, ForegroundImage.Width, ForegroundImage.Height);
            }

            return returnBitmap;
        }

        #region Moom phase
        private int JulianDate(int d, int m, int y)
        {
            int mm, yy;
            int k1, k2, k3;
            int j;

            yy = y - (int)((12 - m) / 10);
            mm = m + 9;
            if (mm >= 12)
            {
                mm = mm - 12;
            }
            k1 = (int)(365.25 * (yy + 4712));
            k2 = (int)(30.6001 * mm + 0.5);
            k3 = (int)((int)((yy / 100) + 49) * 0.75) - 38;
            // 'j' for dates in Julian calendar:
            j = k1 + k2 + d + 59;
            if (j > 2299160)
            {
                // For Gregorian calendar:
                j = j - k3; // 'j' is the Julian date at 12h UT (Universal Time)
            }
            return j;
        }
        private double MoonAge(int d, int m, int y)
        {
            double ag = 0;
            int j = JulianDate(d, m, y);
            //Calculate the approximate phase of the moon
            double ip = (j + 4.867) / 29.53059;
            ip = ip - Math.Floor(ip);
            //After several trials I've seen to add the following lines, 
            //which gave the result was not bad 
            if (ip < 0.5)
                ag = ip * 29.53059 + 29.53059 / 2;
            else
                ag = ip * 29.53059 - 29.53059 / 2;
            // Moon's age in days
            ag = Math.Floor(ag) + 1;
            return ag;
        }
        #endregion

        private Size GetStringSize (string fontPath, int fontSize, string stringCache)
        {
            Size size = new Size(0, 0);
            using (System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection())
            {
                Bitmap bitmap = new Bitmap(55, 55, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                fonts.AddFontFile(fontPath);
                Font drawFont = new Font(fonts.Families[0], fontSize, GraphicsUnit.World);
                StringFormat strFormat = new StringFormat();
                strFormat.FormatFlags = StringFormatFlags.FitBlackBox;
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Near;
                size = TextRenderer.MeasureText(graphics, stringCache, drawFont);
            }
            return size;
        }
    }
}
