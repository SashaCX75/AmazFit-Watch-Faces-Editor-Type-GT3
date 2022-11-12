using ImageMagick;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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
        /// <param name="showAnimation">Показывать анимацию при предпросмотре</param>
        /// <param name="showProgressArea">Подсвечивать круговую шкалу при наличии фонового изображения</param>
        /// <param name="showCentrHend">Подсвечивать центр стрелки</param>
        /// <param name="showWidgetsArea">Подсвечивать область виджетов</param>
        /// <param name="link">0 - основной экран; 1 - AOD</param>
        /// <param name="Shortcuts_In_Gif">Подсвечивать область с ярлыками (для gif файла)</param>
        /// <param name="time_value_sec">Время от начала анимации, сек</param>
        /// <param name="showEeditMode">Отображение в режиме редактирования</param>
        /// <param name="edit_mode">Выбор отображаемого режима редактирования. 
        /// 1 - редактируемый задний фон
        /// 2 - редактируемые элементы
        /// 3 - редактируемые стрелки</param>
        public void Preview_screen(Graphics gPanel, float scale, bool crop, bool WMesh, bool BMesh, bool BBorder,
            bool showShortcuts, bool showShortcutsArea, bool showShortcutsBorder, bool showShortcutsImage, 
            bool showAnimation, bool showProgressArea, bool showCentrHend, 
            bool showWidgetsArea, int link, bool Shortcuts_In_Gif, float time_value_sec, bool showEeditMode, int edit_mode)
        {
            if (showEeditMode && edit_mode > 0) 
            {
                Preview_edit_screen(gPanel, edit_mode, scale, crop);
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
            src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3.png");
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
            }
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
                    background.Editable_Background.BackgroundList.Count > 0 && background.visible)
                {
                    int index = background.Editable_Background.selected_background;
                    if (index >= 0 && index < background.Editable_Background.BackgroundList.Count &&
                        background.Editable_Background.BackgroundList[index].path != null &&
                        background.Editable_Background.BackgroundList[index].path.Length > 0)
                    {
                        src = OpenFileStream(background.Editable_Background.BackgroundList[index].path);
                        gPanel.DrawImage(src, 0, 0);
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
                    gPanel.DrawImage(src, x, y);
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

            #region Elements
            List<Object> Elements = null;
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
            if (Watch_Face != null && Watch_Face.Editable_Elements != null && Watch_Face.Editable_Elements.Watchface_edit_group != null &&
                Watch_Face.Editable_Elements.Watchface_edit_group.Count > 0)
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
                        gPanel.DrawImage(src, pos_x, pos_y);
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
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
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
                }
                mask = FormColor(mask);
                gPanel.DrawImage(mask, 0, 0);
                //gPanel.DrawImage(mask, new Rectangle(0, 0, mask.Width, mask.Height));
                mask.Dispose();
            }
            src.Dispose();
        }

        public void Draw_elements(Object element, Graphics gPanel, float scale, bool crop, bool WMesh, bool BMesh, bool BBorder,
            bool showShortcuts, bool showShortcutsArea, bool showShortcutsBorder, bool showShortcutsImage,
            bool showAnimation, bool showProgressArea, bool showCentrHend,
            bool showWidgetsArea, int link, bool Shortcuts_In_Gif, float time_value_sec, bool showEeditMode, int edit_mode)
        {
            Bitmap src = new Bitmap(1, 1);
            hmUI_widget_IMG_LEVEL img_level = null;
            hmUI_widget_IMG_PROGRESS img_prorgess = null;
            hmUI_widget_IMG_NUMBER img_number = null;
            hmUI_widget_IMG_NUMBER img_number_target = null;
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
                #region ElementDigitalTime
                case "ElementDigitalTime":
                    ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                    if (!DigitalTime.visible) return;
                    int time_offsetX = -1;
                    int time_offsetY = -1;
                    int time_spasing = 0;
                    bool am_pm = false;

                    // определяем формат времени fm/pm
                    if (DigitalTime.AmPm != null && DigitalTime.AmPm.am_img != null
                            && DigitalTime.AmPm.am_img.Length > 0 && DigitalTime.AmPm.pm_img != null
                            && DigitalTime.AmPm.pm_img.Length > 0 &&
                            DigitalTime.AmPm.visible && checkBox_ShowIn12hourFormat.Checked) am_pm = true;

                    for (int index = 1; index <= 4; index++)
                    {
                        if (DigitalTime.Hour != null && DigitalTime.Hour.img_First != null
                            && DigitalTime.Hour.img_First.Length > 0 &&
                            index == DigitalTime.Hour.position && DigitalTime.Hour.visible)
                        {
                            int imageIndex = ListImages.IndexOf(DigitalTime.Hour.img_First);
                            int x = DigitalTime.Hour.imageX;
                            int y = DigitalTime.Hour.imageY;
                            time_offsetY = y;
                            int spasing = DigitalTime.Hour.space;
                            time_spasing = spasing;
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

                            time_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y,
                                                spasing, alignment, value, addZero, 2, separator_index, BBorder);

                            if (DigitalTime.Hour.icon != null && DigitalTime.Hour.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DigitalTime.Hour.icon);
                                x = DigitalTime.Hour.iconPosX;
                                y = DigitalTime.Hour.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                gPanel.DrawImage(src, x, y);
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
                            int alignment = AlignmentToInt(DigitalTime.Minute.align);
                            bool addZero = DigitalTime.Minute.zero;
                            //addZero = true;
                            if (DigitalTime.Minute.follow && time_offsetX > -1 &&
                                DigitalTime.Minute.position > DigitalTime.Hour.position)
                            {
                                x = time_offsetX;
                                alignment = 0;
                                y = time_offsetY;
                                spasing = time_spasing;
                            }
                            time_offsetY = y;
                            int value = WatchFacePreviewSet.Time.Minutes;
                            int separator_index = -1;
                            if (DigitalTime.Minute.unit != null && DigitalTime.Minute.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DigitalTime.Minute.unit);

                            time_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y,
                                                spasing, alignment, value, addZero, 2, separator_index, BBorder);

                            if (DigitalTime.Minute.icon != null && DigitalTime.Minute.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DigitalTime.Minute.icon);
                                x = DigitalTime.Minute.iconPosX;
                                y = DigitalTime.Minute.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                gPanel.DrawImage(src, x, y);
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
                            int alignment = AlignmentToInt(DigitalTime.Second.align);
                            bool addZero = DigitalTime.Second.zero;
                            //addZero = true;
                            if (DigitalTime.Second.follow && time_offsetX > -1 &&
                                DigitalTime.Second.position > DigitalTime.Minute.position)
                            {
                                x = time_offsetX;
                                alignment = 0;
                                y = time_offsetY;
                                spasing = time_spasing;
                            }
                            time_offsetY = y;
                            int value = WatchFacePreviewSet.Time.Seconds;
                            int separator_index = -1;
                            if (DigitalTime.Second.unit != null && DigitalTime.Second.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DigitalTime.Second.unit);


                            time_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y,
                                                spasing, alignment, value, addZero, 2, separator_index, BBorder);

                            if (DigitalTime.Second.icon != null && DigitalTime.Second.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DigitalTime.Second.icon);
                                x = DigitalTime.Second.iconPosX;
                                y = DigitalTime.Second.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                gPanel.DrawImage(src, x, y);
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
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
                            else
                            {
                                int imageIndex = ListImages.IndexOf(DigitalTime.AmPm.am_img);
                                int x = DigitalTime.AmPm.am_x;
                                int y = DigitalTime.AmPm.am_y;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                                gPanel.DrawImage(src, x, y);
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
                                gPanel.DrawImage(src, x, y);
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
                                gPanel.DrawImage(src, x, y);
                            }
                        }
                    }

                    break;
                #endregion

                #region ElementEditablePointers
                /*case "ElementEditablePointers":
                    ElementEditablePointers EditablePointers = (ElementEditablePointers)element;
                    if (!EditablePointers.visible) continue;
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
                            time_spasing = spasing;
                            int alignment = AlignmentToInt(DateDay.Number.align);
                            bool addZero = DateDay.Number.zero;
                            //addZero = true;
                            int value = WatchFacePreviewSet.Date.Day;
                            int separator_index = -1;
                            if (DateDay.Number.unit != null && DateDay.Number.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DateDay.Number.unit);

                            Draw_dagital_text(gPanel, imageIndex, x, y,
                                spasing, alignment, value, addZero, 2, separator_index, BBorder);

                            if (DateDay.Number.icon != null && DateDay.Number.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DateDay.Number.icon);
                                x = DateDay.Number.iconPosX;
                                y = DateDay.Number.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
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
                                gPanel.DrawImage(src, x_scale, y_scale);
                            }

                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (DateDay.Pointer.cover_path != null && DateDay.Pointer.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(DateDay.Pointer.cover_path);
                                x = DateDay.Pointer.cover_x;
                                y = DateDay.Pointer.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                gPanel.DrawImage(src, x, y);
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
                            int value = WatchFacePreviewSet.Date.Month;
                            int separator_index = -1;
                            if (DateMonth.Number.unit != null && DateMonth.Number.unit.Length > 0)
                                separator_index = ListImages.IndexOf(DateMonth.Number.unit);

                            Draw_dagital_text(gPanel, imageIndex, x, y,
                                spasing, alignment, value, addZero, 2, separator_index, BBorder);

                            if (DateMonth.Number.icon != null && DateMonth.Number.icon.Length > 0)
                            {
                                imageIndex = ListImages.IndexOf(DateMonth.Number.icon);
                                x = DateMonth.Number.iconPosX;
                                y = DateMonth.Number.iconPosY;

                                src = OpenFileStream(ListImagesFullName[imageIndex]);
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                            }
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
                                gPanel.DrawImage(src, x_scale, y_scale);
                            }

                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (DateMonth.Pointer.cover_path != null && DateMonth.Pointer.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(DateMonth.Pointer.cover_path);
                                x = DateMonth.Pointer.cover_x;
                                y = DateMonth.Pointer.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                gPanel.DrawImage(src, x, y);
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
                                gPanel.DrawImage(src, x, y);
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

                    if (DateYear.Number != null && DateYear.Number.img_First != null
                            && DateYear.Number.img_First.Length > 0)
                    {
                        int imageIndex = ListImages.IndexOf(DateYear.Number.img_First);
                        int x = DateYear.Number.imageX;
                        int y = DateYear.Number.imageY;
                        int spasing = DateYear.Number.space;
                        //int alignment = AlignmentToInt(DateYear.Number.align);
                        int alignment = 0;
                        bool addZero = DateYear.Number.zero;
                        int value = WatchFacePreviewSet.Date.Year;
                        if (!addZero) value = value % 100;
                        int separator_index = -1;
                        if (DateYear.Number.unit != null && DateYear.Number.unit.Length > 0)
                            separator_index = ListImages.IndexOf(DateYear.Number.unit);

                        Draw_dagital_text(gPanel, imageIndex, x, y,
                            spasing, alignment, value, addZero, 4, separator_index, BBorder);

                        if (DateYear.Number.icon != null && DateYear.Number.icon.Length > 0)
                        {
                            imageIndex = ListImages.IndexOf(DateYear.Number.icon);
                            x = DateYear.Number.iconPosX;
                            y = DateYear.Number.iconPosY;

                            src = OpenFileStream(ListImagesFullName[imageIndex]);
                            gPanel.DrawImage(src, x, y);
                            //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                                gPanel.DrawImage(src, x_scale, y_scale);
                            }

                            DrawPointer(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

                            if (DateWeek.Pointer.cover_path != null && DateWeek.Pointer.cover_path.Length > 0)
                            {
                                image_index = ListImages.IndexOf(DateWeek.Pointer.cover_path);
                                x = DateWeek.Pointer.cover_x;
                                y = DateWeek.Pointer.cover_y;

                                src = OpenFileStream(ListImagesFullName[image_index]);
                                gPanel.DrawImage(src, x, y);
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
                                gPanel.DrawImage(src, x, y);
                                //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                                    gPanel.DrawImage(src, x, y);
                                    //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                                    gPanel.DrawImage(src, x, y);
                                    //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                                    gPanel.DrawImage(src, x, y);
                                    //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                                    gPanel.DrawImage(src, x, y);
                                    //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                                }
                            }
                        }
                    }
                    break;
                #endregion

                #region ElementShortcuts
                case "ElementShortcuts":
                    ElementShortcuts shortcutsElement = (ElementShortcuts)element;
                    if (!shortcutsElement.visible && !Shortcuts_In_Gif) return;

                    hmUI_widget_IMG_CLICK img_click_step = shortcutsElement.Step;
                    hmUI_widget_IMG_CLICK img_click_heart = shortcutsElement.Heart;
                    hmUI_widget_IMG_CLICK img_click_spo2 = shortcutsElement.SPO2;
                    hmUI_widget_IMG_CLICK img_click_pai = shortcutsElement.PAI;
                    hmUI_widget_IMG_CLICK img_click_stress = shortcutsElement.Stress;
                    hmUI_widget_IMG_CLICK img_click_weather = shortcutsElement.Weather;
                    hmUI_widget_IMG_CLICK img_click_altimeter = shortcutsElement.Altimeter;
                    hmUI_widget_IMG_CLICK img_click_sunrise = shortcutsElement.Sunrise;
                    hmUI_widget_IMG_CLICK img_click_alarm = shortcutsElement.Alarm;
                    hmUI_widget_IMG_CLICK img_click_sleep = shortcutsElement.Sleep;
                    hmUI_widget_IMG_CLICK img_click_countdown = shortcutsElement.Countdown;
                    hmUI_widget_IMG_CLICK img_click_stopwatch = shortcutsElement.Stopwatch;

                    for (int index = 1; index <= 15; index++)
                    {
                        if (img_click_step != null && index == img_click_step.position)
                        {
                            DrawShortcuts(gPanel, img_click_step, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                        }
                        if (img_click_heart != null && index == img_click_heart.position)
                        {
                            DrawShortcuts(gPanel, img_click_heart, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                        }
                        if (img_click_spo2 != null && index == img_click_spo2.position)
                        {
                            DrawShortcuts(gPanel, img_click_spo2, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                        }
                        if (img_click_pai != null && index == img_click_pai.position)
                        {
                            DrawShortcuts(gPanel, img_click_pai, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                        }
                        if (img_click_stress != null && index == img_click_stress.position)
                        {
                            DrawShortcuts(gPanel, img_click_stress, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                        }
                        if (img_click_weather != null && index == img_click_weather.position)
                        {
                            DrawShortcuts(gPanel, img_click_weather, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                        }
                        if (img_click_altimeter != null && index == img_click_altimeter.position)
                        {
                            DrawShortcuts(gPanel, img_click_altimeter, showShortcuts,
                                showShortcutsArea, showShortcutsBorder, showShortcutsImage, Shortcuts_In_Gif);
                        }
                        if (img_click_sunrise != null && index == img_click_sunrise.position)
                        {
                            DrawShortcuts(gPanel, img_click_sunrise, showShortcuts,
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
                    img_number_target = activityElementSteps.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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
                    //img_number_target = activityElementBattery.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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
                    img_number_target = activityElementCalories.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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
                    img_number_target = activityElementPAI.Number_Target;
                    img_pointer = activityElementPAI.Pointer;
                    circle_scale = activityElementPAI.Circle_Scale;
                    linear_scale = activityElementPAI.Linear_Scale;
                    icon = activityElementPAI.Icon;

                    elementValue = 5;
                    value_lenght = 3;
                    goal = WatchFacePreviewSet.Activity.PAI;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementPAI");


                    break;
                #endregion

                #region ElementDistance
                case "ElementDistance":
                    ElementDistance activityElementDistance = (ElementDistance)element;
                    if (!activityElementDistance.visible) return;
                    if (activityElementDistance.Number == null ||
                        activityElementDistance.Number.img_First == null ||
                        activityElementDistance.Number.img_First.Length == 0) return;

                    img_number = activityElementDistance.Number;

                    elementValue = WatchFacePreviewSet.Activity.Distance;
                    double distance_value = elementValue / 1000f;
                    value_lenght = 4;
                    int image_Index = ListImages.IndexOf(img_number.img_First);
                    int pos_x = img_number.imageX;
                    int pos_y = img_number.imageY;
                    int distance_spasing = img_number.space;
                    int distance_alignment = AlignmentToInt(img_number.align);
                    //bool distance_addZero = img_number.zero;
                    bool distance_addZero = false;
                    int distance_separator_index = -1;
                    if (img_number.unit != null && img_number.unit.Length > 0)
                        distance_separator_index = ListImages.IndexOf(img_number.unit);
                    int decumalPoint_index = -1;
                    if (img_number.dot_image != null && img_number.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(img_number.dot_image);

                    Draw_dagital_text_dacumal(gPanel, image_Index, pos_x, pos_y,
                        distance_spasing, distance_alignment, distance_value, distance_addZero, value_lenght,
                        distance_separator_index, decumalPoint_index, 2, BBorder);

                    if (img_number.icon != null && img_number.icon.Length > 0)
                    {
                        image_Index = ListImages.IndexOf(img_number.icon);
                        pos_x = img_number.iconPosX;
                        pos_y = img_number.iconPosY;

                        src = OpenFileStream(ListImagesFullName[image_Index]);
                        gPanel.DrawImage(src, pos_x, pos_y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }

                    break;
                #endregion

                #region ElementStand
                case "ElementStand":
                    ElementStand activityElementStand = (ElementStand)element;
                    if (!activityElementStand.visible) return;

                    img_level = activityElementStand.Images;
                    img_prorgess = activityElementStand.Segments;
                    img_number = activityElementStand.Number;
                    img_number_target = activityElementStand.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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
                    img_number_target = activityElementActivity.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementActivity");


                    break;
                #endregion

                #region ElementSpO2
                case "ElementSpO2":
                    ElementSpO2 activityElementSpO2 = (ElementSpO2)element;
                    if (!activityElementSpO2.visible) return;

                    img_number = activityElementSpO2.Number;

                    elementValue = 97;
                    value_lenght = 3;
                    goal = 100;
                    progress = 0;


                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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
                    img_number_target = activityElementFatBurning.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementFatBurning");


                    break;
                #endregion



                #region ElementWeather
                case "ElementWeather":
                    ElementWeather activityElementWeather = (ElementWeather)element;
                    if (!activityElementWeather.visible) return;

                    img_level = activityElementWeather.Images;
                    img_number = activityElementWeather.Number;
                    hmUI_widget_IMG_NUMBER img_number_min = activityElementWeather.Number_Min;
                    hmUI_widget_IMG_NUMBER img_number_max = activityElementWeather.Number_Max;
                    hmUI_widget_TEXT city_name = activityElementWeather.City_Name;
                    icon = activityElementWeather.Icon;

                    int value_current = WatchFacePreviewSet.Weather.Temperature;
                    int value_min = WatchFacePreviewSet.Weather.TemperatureMin;
                    int value_max = WatchFacePreviewSet.Weather.TemperatureMax;
                    int icon_index = WatchFacePreviewSet.Weather.Icon;
                    bool showTemperature = WatchFacePreviewSet.Weather.showTemperature;

                    DrawWeather(gPanel, img_level, img_number, img_number_min, img_number_max,
                        city_name, icon, value_current, value_min, value_max, value_lenght, icon_index,
                        BBorder, showTemperature);


                    break;
                #endregion

                #region ElementUVIndex
                case "ElementUVIndex":
                    ElementUVIndex activityElementUVIndex = (ElementUVIndex)element;
                    if (!activityElementUVIndex.visible) return;

                    img_level = activityElementUVIndex.Images;
                    img_prorgess = activityElementUVIndex.Segments;
                    img_number = activityElementUVIndex.Number;
                    //img_number_target = activityElementBattery.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
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
                    //img_number_target = activityElementBattery.Number_Target;
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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementHumidity");


                    break;
                #endregion

                #region ElementAltimeter
                case "ElementAltimeter":
                    ElementAltimeter activityElementAltimeter = (ElementAltimeter)element;
                    if (!activityElementAltimeter.visible) return;

                    img_number = activityElementAltimeter.Number;
                    img_pointer = activityElementAltimeter.Pointer;
                    icon = activityElementAltimeter.Icon;

                    elementValue = WatchFacePreviewSet.Weather.AirPressure;
                    value_lenght = 4;
                    goal = 1170 - 195;
                    progress = (WatchFacePreviewSet.Weather.AirPressure - 195) / 975f;

                    if (img_level != null && img_level.image_length > 0)
                    {
                        imgCount = img_level.image_length;
                        valueImgIndex = (int)((imgCount - 1) * progress);
                        if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                    }
                    if (img_prorgess != null && img_prorgess.image_length > 0)
                    {
                        segmentCount = img_prorgess.image_length;
                        valueSegmentIndex = (int)((segmentCount - 1) * progress);
                        if (valueSegmentIndex >= segmentCount) valueSegmentIndex = (int)(segmentCount - 1);
                    }

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementAltimeter");


                    break;
                #endregion

                #region ElementSunrise
                case "ElementSunrise":
                    ElementSunrise activityElementSunrise = (ElementSunrise)element;
                    if (!activityElementSunrise.visible) return;

                    img_level = activityElementSunrise.Images;
                    img_prorgess = activityElementSunrise.Segments;
                    img_number = activityElementSunrise.Sunrise;
                    img_number_target = activityElementSunrise.Sunset;
                    img_pointer = activityElementSunrise.Pointer;
                    icon = activityElementSunrise.Icon;

                    int minSunrise = WatchFacePreviewSet.Time.Minutes;
                    int hourSunrise = WatchFacePreviewSet.Time.Hours;

                    DrawSunrise(gPanel, img_level, img_prorgess, img_number, img_number_target, activityElementSunrise.Sunset_Sunrise,
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
                    img_pointer = activityElementWind.Pointer;
                    icon = activityElementWind.Icon;

                    elementValue = WatchFacePreviewSet.Weather.WindForce;
                    value_lenght = 1;
                    goal = 12;
                    progress = (float)WatchFacePreviewSet.Weather.WindForce / 12f;

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

                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementWind");


                    break;
                #endregion

                #region ElementMoon
                case "ElementMoon":
                    ElementMoon activityElementMoon = (ElementMoon)element;
                    if (!activityElementMoon.visible) return;

                    img_level = activityElementMoon.Images;

                    elementValue = 100;
                    value_lenght = 3;
                    goal = 100;
                    //progress = 0;

                    int year = WatchFacePreviewSet.Date.Year;
                    int month = WatchFacePreviewSet.Date.Month;
                    int day = WatchFacePreviewSet.Date.Day;
                    double moon_age = MoonAge(day, month, year);
                    //int moonPhase = (int)(8 * moon_age / 29);

                    imgCount = img_level.image_length;
                    valueImgIndex = (int)Math.Round((imgCount - 1) * moon_age / 29);
                    //valueImgIndex = (int)Math.Round((imgCount - 1) * moon_age / 29.53f);
                    //valueImgIndex = moonPhase - 1;
                    if (valueImgIndex < 0) valueImgIndex = (int)(imgCount - 1);
                    if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);


                    DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                        img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                        progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                        showCentrHend, "ElementMoon");


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
            src.Dispose();
        }
            
        public void Preview_edit_screen(Graphics gPanel, int edit_mode, float scale, bool crop)
        {
            Bitmap src = new Bitmap(1, 1);

            #region Black background
            Logger.WriteLine("Preview_edit_screen (Black background)");
            src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3.png");
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
            }
            gPanel.DrawImage(src, 0, 0);
            #endregion

            Editable_Background editable_background = null;
            EditableElements editable_elements = null;
            ElementEditablePointers editable_pointers = null;

            #region Background
            Background background = null;
            if (Watch_Face != null && Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                background = Watch_Face.ScreenNormal.Background;
            if (background != null)
            {
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
            if (editable_elements != null)
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
                        if(element_index == i) // рамка выделеного элемента
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
            if (editable_pointers != null)
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

            if (edit_mode == 1 && editable_background != null)
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

                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr,
                        align_h, align_v, text_style, false);
                }
            }

            if (edit_mode == 2 && editable_elements != null)
            {
                if (editable_elements.fg_mask != null && editable_elements.fg_mask.Length > 0)
                {
                    src = OpenFileStream(editable_elements.fg_mask);
                    gPanel.DrawImage(src, 0, 0);
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
                                case "PAI_DAILY":
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

                                default:
                                    valueStr = "Error";
                                    break;
                            }

                                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr,
                            align_h, align_v, text_style, false); 
                        }
                    } 
                }
            }

            if (edit_mode == 3 && editable_pointers != null)
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

                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr,
                        align_h, align_v, text_style, false);
                }
            }

            if (crop)
            {
                Logger.WriteLine("Preview_edit_screen (crop)");
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
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
                }
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
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
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
                }
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
        /// <param name="numberTarget">Параметры цифрового значения цели</param>
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
            hmUI_widget_IMG_NUMBER number, hmUI_widget_IMG_NUMBER numberTarget,
            hmUI_widget_IMG_POINTER pointer, Circle_Scale circleScale, Linear_Scale linearScale,
            hmUI_widget_IMG icon, float value, int value_lenght, int goal, float progress,
            int valueImgIndex, int valueSegmentIndex, bool BBorder, bool showProgressArea, bool showCentrHend, string elementName,
            bool ActivityGoal_Calories = false)
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
                    int alignment = AlignmentToInt(number.align);
                    bool addZero = number.zero;
                    int separator_index = -1;
                    if (number.unit != null && number.unit.Length > 0)
                        separator_index = ListImages.IndexOf(number.unit);

                    Draw_dagital_text(gPanel, imageIndex, x, y,
                        spasing, alignment, (int)value, addZero, value_lenght, separator_index, BBorder, elementName);

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

                if (numberTarget != null && numberTarget.img_First != null && numberTarget.img_First.Length > 0 &&
                    index == numberTarget.position && numberTarget.visible)
                {
                    int imageIndex = ListImages.IndexOf(numberTarget.img_First);
                    int x = numberTarget.imageX;
                    int y = numberTarget.imageY;
                    int spasing = numberTarget.space;
                    int alignment = AlignmentToInt(numberTarget.align);
                    bool addZero = numberTarget.zero;
                    int separator_index = -1;
                    if (numberTarget.unit != null && numberTarget.unit.Length > 0)
                        separator_index = ListImages.IndexOf(numberTarget.unit);

                    Draw_dagital_text(gPanel, imageIndex, x, y,
                        spasing, alignment, (int)goal, addZero, value_lenght, separator_index, BBorder);

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

                    DrawScaleCircle(gPanel, x, y, radius, width, 0, startAngle, fullAngle, progress,
                        color, inversion, showProgressArea);

                    if(mirror) DrawScaleCircle(gPanel, x, y, radius, width, 0, startAngle, -fullAngle, progress,
                         color, inversion, showProgressArea);
                }

                if (linearScale != null && index == linearScale.position && linearScale.visible)
                {
                    int x = linearScale.start_x;
                    int y = linearScale.start_y;
                    int lenght = linearScale.lenght;
                    int width = linearScale.line_width;
                    bool mirror = linearScale.mirror;
                    bool inversion = linearScale.inversion;
                    bool vertical = linearScale.vertical;
                    Color color = StringToColor(linearScale.color);
                    int pointer_index = -1;
                    if (linearScale.pointer != null && linearScale.pointer.Length > 0)
                        pointer_index = ListImages.IndexOf(linearScale.pointer);

                    DrawScaleLinear(gPanel, x, y, lenght, width, pointer_index, vertical, progress,
                        color, inversion, showProgressArea);

                    if (mirror) DrawScaleLinear(gPanel, x, y, -lenght, width, pointer_index, vertical, progress,
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
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем все параметры элемента погода</summary>
        /// <param name="gPanel">Поверхность для рисования</param>
        /// <param name="images">Параметры для изображения</param>
        /// <param name="number">Параметры для текущей температуры</param>
        /// <param name="numberMin">Параметры для минимальной температуры</param>
        /// <param name="numberMax">Параметры для максимальной температуры</param>
        /// <param name="cityName">Параметры для названия города</param>
        /// <param name="icon">Параметры для иконки</param>
        /// <param name="value">Текущая температура</param>
        /// <param name="valueMin">Минимальная температура</param>
        /// <param name="valueMax">Максимальная температура</param>
        /// <param name="value_lenght">Максимальная длина для отображения значения</param>
        /// <param name="icon_index">Номер иконки погоды</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="showTemperature">Показывать температуру</param>
        private void DrawWeather(Graphics gPanel, hmUI_widget_IMG_LEVEL images, 
            hmUI_widget_IMG_NUMBER number, hmUI_widget_IMG_NUMBER numberMin, hmUI_widget_IMG_NUMBER numberMax,
            hmUI_widget_TEXT cityName, hmUI_widget_IMG icon, int value, int valueMin, int valueMax, int value_lenght,
            int icon_index, bool BBorder, bool showTemperature)
        {
            Bitmap src = new Bitmap(1, 1);

            for (int index = 1; index <= 10; index++)
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
                            gPanel.DrawImage(src, x, y);
                            //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                        Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, value, addZero, 
                            imageMinus_index, separator_index, BBorder, -1, false);
                    }
                    else if (imageError_index >= 0)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y,
                                        spasing, alignment, value, addZero, imageMinus_index, separator_index,
                                        BBorder, imageError_index, true);
                    }

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

                if (numberMin != null && numberMin.img_First != null && numberMin.img_First.Length > 0 &&
                    index == numberMin.position && numberMin.visible)
                {
                    int imageIndex = ListImages.IndexOf(numberMin.img_First);
                    int x = numberMin.imageX;
                    int y = numberMin.imageY;
                    int spasing = numberMin.space;
                    int alignment = AlignmentToInt(numberMin.align);
                    bool addZero = false;
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
                        Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, valueMin, addZero,
                            imageMinus_index, separator_index, BBorder, -1, false);
                    }
                    else if (imageError_index >= 0)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y,
                                        spasing, alignment, valueMin, addZero, imageMinus_index, separator_index,
                                        BBorder, imageError_index, true);
                    }

                    if (numberMin.icon != null && numberMin.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(numberMin.icon);
                        x = numberMin.iconPosX;
                        y = numberMin.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                    }
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
                        Draw_weather_text(gPanel, imageIndex, x, y, spasing, alignment, valueMax, addZero,
                            imageMinus_index, separator_index, BBorder, -1, false);
                    }
                    else if (imageError_index >= 0)
                    {
                        Draw_weather_text(gPanel, imageIndex, x, y,
                                        spasing, alignment, valueMax, addZero, imageMinus_index, separator_index,
                                        BBorder, imageError_index, true);
                    }

                    if (numberMax.icon != null && numberMax.icon.Length > 0)
                    {
                        imageIndex = ListImages.IndexOf(numberMax.icon);
                        x = numberMax.iconPosX;
                        y = numberMax.iconPosY;

                        src = OpenFileStream(ListImagesFullName[imageIndex]);
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                    //int align_h = AlignmentToInt(cityName.align_h);
                    //int align_v = AlignmentVerticalToInt(cityName.align_v);
                    string align_h = cityName.align_h;
                    string align_v = cityName.align_v;
                    string text_style = cityName.text_style;
                    string valueStr = "City Name";

                    Draw_text(gPanel, x, y, w, h, size, space_h, space_v, color, valueStr,
                        align_h, align_v, text_style, BBorder);
                }

            }

            src.Dispose();
        }

        /// <summary>Рисуем восход, звкат</summary>
        private void DrawSunrise(Graphics gPanel, hmUI_widget_IMG_LEVEL images, hmUI_widget_IMG_PROGRESS segments,
            hmUI_widget_IMG_NUMBER sunrise, hmUI_widget_IMG_NUMBER sunset, hmUI_widget_IMG_NUMBER sunset_sunrise, hmUI_widget_IMG_POINTER pointer,
            hmUI_widget_IMG icon, int hour, int minute, bool BBorder, bool showProgressArea, bool showCentrHend)
        {
            TimeSpan time_now = new TimeSpan(hour, minute, 0);
            TimeSpan time_sunrise = new TimeSpan(5, 30, 0);
            TimeSpan time_sunset = new TimeSpan(19, 30, 0);
            TimeSpan day_lenght = time_sunset - time_sunrise;
            TimeSpan day_progress = time_now - time_sunrise;

            bool sun = false;
            if(time_now >= time_sunrise && time_now <= time_sunset) sun = true;

            float progress = (float)(day_progress.TotalSeconds / day_lenght.TotalSeconds);
            if (progress > 1) progress = 1;
            if (progress < 0) progress = 0;
            Bitmap src = new Bitmap(1, 1);

            for (int index = 1; index <= 10; index++)
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
                            gPanel.DrawImage(src, x, y);
                            //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
                    float sunrise_value = 5.30f;
                    int image_Index = ListImages.IndexOf(sunrise.img_First);
                    int pos_x = sunrise.imageX;
                    int pos_y = sunrise.imageY;
                    int sunrise_spasing = sunrise.space;
                    int sunrise_alignment = AlignmentToInt(sunrise.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunrise_addZero = true;
                    int sunrise_separator_index = -1;
                    if (sunrise.unit != null && sunrise.unit.Length > 0)
                        sunrise_separator_index = ListImages.IndexOf(sunrise.unit);
                    int decumalPoint_index = -1;
                    if (sunrise.dot_image != null && sunrise.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunrise.dot_image);

                    Draw_dagital_text_dacumal(gPanel, image_Index, pos_x, pos_y,
                        sunrise_spasing, sunrise_alignment, sunrise_value, sunrise_addZero, 4,
                        sunrise_separator_index, decumalPoint_index, 2, BBorder, "ElementSunrise");

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
                
                if (sunset != null && sunset.img_First != null && sunset.img_First.Length > 0 &&
                    index == sunset.position && sunset.visible)
                {
                    float sunset_value = 19.30f;
                    int image_Index = ListImages.IndexOf(sunset.img_First);
                    int pos_x = sunset.imageX;
                    int pos_y = sunset.imageY;
                    int sunset_spasing = sunset.space;
                    int sunset_alignment = AlignmentToInt(sunset.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunset_addZero = true;
                    int sunset_separator_index = -1;
                    if (sunset.unit != null && sunset.unit.Length > 0)
                        sunset_separator_index = ListImages.IndexOf(sunset.unit);
                    int decumalPoint_index = -1;
                    if (sunset.dot_image != null && sunset.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunset.dot_image);

                    Draw_dagital_text_dacumal(gPanel, image_Index, pos_x, pos_y,
                        sunset_spasing, sunset_alignment, sunset_value, sunset_addZero, 4,
                        sunset_separator_index, decumalPoint_index, 2, BBorder, "ElementSunrise");

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

                if (sunset_sunrise != null && sunset_sunrise.img_First != null && sunset_sunrise.img_First.Length > 0 &&
                    index == sunset_sunrise.position && sunset_sunrise.visible)
                {
                    float sunset_sunrise_value = 5.30f;
                    if (time_now > time_sunrise && time_now < time_sunset) sunset_sunrise_value = 19.30f;
                    int image_Index = ListImages.IndexOf(sunset_sunrise.img_First);
                    int pos_x = sunset_sunrise.imageX;
                    int pos_y = sunset_sunrise.imageY;
                    int sunset_sunrise_spasing = sunset_sunrise.space;
                    int sunset_sunrise_alignment = AlignmentToInt(sunset_sunrise.align);
                    //bool distance_addZero = img_number.zero;
                    bool sunset_sunrise_addZero = true;
                    int sunset_sunrise_separator_index = -1;
                    if (sunset_sunrise.unit != null && sunset_sunrise.unit.Length > 0)
                        sunset_sunrise_separator_index = ListImages.IndexOf(sunset_sunrise.unit);
                    int decumalPoint_index = -1;
                    if (sunset_sunrise.dot_image != null && sunset_sunrise.dot_image.Length > 0)
                        decumalPoint_index = ListImages.IndexOf(sunset_sunrise.dot_image);

                    Draw_dagital_text_dacumal(gPanel, image_Index, pos_x, pos_y,
                        sunset_sunrise_spasing, sunset_sunrise_alignment, sunset_sunrise_value, sunset_sunrise_addZero, 4,
                        sunset_sunrise_separator_index, decumalPoint_index, 2, BBorder, "ElementSunrise");

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
                        gPanel.DrawImage(src, x, y);
                        //gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
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
        /// <param name="addZero">Отображать начальные нули</param>
        /// <param name="value_lenght">Количество отображаемых символов</param>
        /// <param name="separator_index">Символ разделителя (единиц измерения)</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="elementName">Название элемента (при необходимости)</param>
        private int Draw_dagital_text(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, int value, bool addZero, int value_lenght, int separator_index, bool BBorder, 
            string elementName = "")
        {
            if (image_index < 0 || image_index >= ListImagesFullName.Count) return 0;
            //while (spacing > 127)
            //{
            //    spacing = spacing - 256;
            //}
            //while (spacing < -128)
            //{
            //    spacing = spacing + 256;
            //}

            int result = 0;
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
            if (elementName == "ElementStand") value_lenght = 5;
            if (elementName == "ElementUVIndex") value_lenght = 2;
            if (elementName == "ElementHumidity") value_lenght = 3;
            if (elementName == "ElementWind") value_lenght = 2;
            int DateLenght = width * value_lenght;
            //int DateLenght = width * value_lenght + 1;
            if (spacing != 0) DateLenght = DateLenght + spacing * (value_lenght - 1);
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
            DateLenghtReal = DateLenghtReal - spacing;

            int PointX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    PointX = x;
                    break;
                case 1:
                    PointX = x + DateLenght / 2 - DateLenghtReal / 2;
                    break;
                case 2:
                    PointX = x + DateLenght - DateLenghtReal;
                    break;
                default:
                    PointX = x;
                    break;
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
                        graphics.DrawImage(src, PointX, PointY);
                        //PointX = PointX + src.Width + spacing;
                        PointX = PointX + width + spacing;
                        //src.Dispose();
                    }
                }

            }
            //result = PointX - spacing;
            result = PointX;
            if (separator_index > -1)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                graphics.DrawImage(src, PointX, PointY);
                //graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
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

            Logger.WriteLine("* Draw_dagital_text (end)");
            return result;
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
         /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
         /// <param name="imageError_index">Иконка ошибки данны</param>
         /// <param name="errorData">отображать ошибку данный</param>
        private int Draw_weather_text(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, int value, bool addZero, int image_minus_index, int unit_index, bool BBorder,
            int imageError_index = -1, bool errorData = false)
        {
            //while (spacing > 127)
            //{
            //    spacing = spacing - 255;
            //}
            //while (spacing < -127)
            //{
            //    spacing = spacing + 255;
            //}
            int result = 0;
            Logger.WriteLine("* Draw_weather_text");
            var src = new Bitmap(1, 1);
            int _number;
            int i;
            string value_S = value.ToString();
            if (addZero)
            {
                //while (value_S.Length < value_lenght)
                while (value_S.Length < 2)
                {
                    value_S = "0" + value_S;
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
            int DateLenght = widthD * 3 + widthCF + 1;
            //if (alignment == 2 && AvailabilityIcon) DateLenght = DateLenght - widthCF;
            if (spacing != 0) DateLenght = DateLenght + 3 * spacing;
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

            int PointX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    PointX = x;
                    break;
                case 1:
                    PointX = x + DateLenght / 2 - DateLenghtReal / 2;
                    break;
                case 2:
                    PointX = x + DateLenght - DateLenghtReal;
                    break;
                default:
                    PointX = x;
                    break;
            }

            Logger.WriteLine("Draw value");
            if (!errorData)
            {
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
                            graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
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
                            graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
                            PointX = PointX + src.Width + spacing;
                            //src.Dispose();
                        }
                    }

                }
                result = PointX - spacing;
                if (unit_index > -1)
                {
                    src = OpenFileStream(ListImagesFullName[unit_index]);
                    graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
                    result = result + src.Width + spacing;
                }
            }
            else if (imageError_index >= 0)
            {
                src = OpenFileStream(ListImagesFullName[imageError_index]);
                switch (alignment)
                {
                    case 0:
                        PointX = x;
                        break;
                    case 1:
                        PointX = x + (DateLenght - src.Width) / 2;
                        break;
                    case 2:
                        PointX = x + DateLenght - src.Width;
                        break;
                    default:
                        PointX = x;
                        break;
                }
                graphics.DrawImage(src, PointX, PointY);
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
        /// <param name="align_h">Горизонтальное выравнивание</param>
        /// <param name="align_v">Вертикальное выравнивание</param>
        /// <param name="text_style">Стиль вписывания текста</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        private void Draw_text(Graphics graphics, int x, int y, int w, int h, float size, int spacing_h, int spacing_v, 
            Color color, string value, string align_h, string align_v, string text_style, bool BBorder)
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

                    foreach (char ch in draw_string)
                    {
                        string str = ch.ToString();
                        Size strSize = TextRenderer.MeasureText(graphics, str, drawFont);
                        gPanel.DrawString(str, drawFont, drawBrush, posX, posY, strFormat);

                        posX = posX + strSize.Width + spacing_h - offsetX;
                    }
                    PointY += (float)(1.47 * size + 0.55 * spacing_v);
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
                //graphics.RotateTransform(-angle);
                //graphics.TranslateTransform(-x, -y);
                graphics.ResetTransform();
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
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        private int Draw_dagital_text_dacumal(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, double value, bool addZero, int value_lenght, int separator_index,
            int decimalPoint_index, int decCount, bool BBorder, string elementName = "")
        {
            Logger.WriteLine("* Draw_dagital_text");
            value = Math.Round(value, decCount, MidpointRounding.AwayFromZero);
            //while (spacing > 127)
            //{
            //    spacing = spacing - 255;
            //}
            //while (spacing < -127)
            //{
            //    spacing = spacing + 255;
            //}
            //var Digit = new Bitmap(ListImagesFullName[image_index]);
            //var Delimit = new Bitmap(1, 1);
            //if (dec >= 0) Delimit = new Bitmap(ListImagesFullName[dec]);
            string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string data_numberS = value.ToString();
            if (decCount > 0)
            {
                if (data_numberS.IndexOf(decimalSeparator) < 0) data_numberS = data_numberS + decimalSeparator;
                while (data_numberS.IndexOf(decimalSeparator) > data_numberS.Length - decCount - 1)
                {
                    data_numberS = data_numberS + "0";
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

            int DateLenght = 4 * width + 2 * spacing;
            if(elementName == "ElementSunrise") DateLenght = 5 * width + 3 * spacing;
            if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                DateLenght = DateLenght + src.Width + spacing;
            }
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                if (comboBox_watch_model.Text == "T-Rex 2" || comboBox_watch_model.Text == "GTR 4" ||
                    comboBox_watch_model.Text == "GTS 4")
                {
                    DateLenght = DateLenght + src.Width + spacing + spacing; 
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
                        //src = OpenFileStream(ListImagesFullName[i]);
                        //DateLenghtReal = DateLenghtReal + src.Width + spacing;
                        DateLenghtReal = DateLenghtReal + width + spacing;
                    }
                }
                else
                {
                    if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                        DateLenghtReal = DateLenghtReal + src.Width + spacing;
                        //src.Dispose();
                    }
                }

            }
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                DateLenghtReal = DateLenghtReal + src.Width + spacing;
            }
            DateLenghtReal = DateLenghtReal - spacing;


            int PointX = 0;
            int PointY = y;
            switch (alignment)
            {
                case 0:
                    PointX = x;
                    break;
                case 1:
                    PointX = x + DateLenght / 2 - DateLenghtReal / 2;
                    break;
                case 2:
                    PointX = x + DateLenght - DateLenghtReal;
                    break;
                default:
                    PointX = x;
                    break;
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
                        graphics.DrawImage(src, PointX, PointY);
                        //PointX = PointX + src.Width + spacing;
                        PointX = PointX + width + spacing;
                    }
                }
                else
                {
                    if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
                    {
                        src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                        graphics.DrawImage(src, PointX, PointY);
                        PointX = PointX + src.Width + spacing;
                    }
                }

            }
            int result = PointX;
            if (separator_index > -1)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
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
            bool inversion, bool showProgressArea)
        {
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
                case 1:
                    pen.EndCap = LineCap.Triangle;
                    pen.StartCap = LineCap.Triangle;
                    break;
                case 2:
                    pen.EndCap = LineCap.Flat;
                    pen.StartCap = LineCap.Flat;
                    break;
                case 90:
                    pen.EndCap = LineCap.Triangle;
                    pen.StartCap = LineCap.Triangle;
                    break;
                case 180:
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
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth, startAngle, valueAngle);
                int s = Math.Sign(valueAngle);
                float start_angl = (float)(startAngle + 0.08 * s * width);
                float end_angl = (float)(valueAngle - 0.16 * s * width);
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth,
                //    (float)(startAngle - 0.007 * s * width), (float)(valueAngle + 0.015 * s * width));
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
            Logger.WriteLine("* DrawScaleCircle (end)");

        }

        /// <summary>Линейная шкала</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="lenght">Длина шкалы</param>
        /// <param name="width">Толщина линии</param>
        /// <param name="pointer">Изображение указателя</param>
        /// <param name="vertical">Вертикальная или горизонтальная</param>
        /// <param name="position">Отображаемая величина от 0 до 1</param>
        /// <param name="color">Цвет шкалы</param>
        /// <param name="inversion">Инверсия шкалы</param>
        /// <param name="showProgressArea">Подсвечивать полную длину шкалы</param>
        private void DrawScaleLinear(Graphics graphics, int x, int y, int lenght, int width, int pointer_index,
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

        /// <summary>формируем изображение на панедли Graphics</summary>
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
            if (shortcut != null && shortcut.src != null && shortcut.src.Length > 0 && shortcut.visible)
            {
                int imageIndex = ListImages.IndexOf(shortcut.src);
                int x = shortcut.x;
                int y = shortcut.y;
                int width = shortcut.w;
                int height = shortcut.h;

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
                        //Rectangle cropRect = new Rectangle(...);
                        //Bitmap src = Image.FromFile(fileName) as Bitmap;
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

                    /*if (showShortcutsImage)
                    {
                        if (imageIndex < ListImagesFullName.Count)
                        {
                            Bitmap src = OpenFileStream(ListImagesFullName[imageIndex]);
                            int pos_x = x + width / 2 - src.Width / 2;
                            int pos_y = y + height / 2 - src.Height / 2;
                            if (pos_x >= x && pos_y >= y)
                            {
                                graphics.DrawImage(src, pos_x, pos_y);
                            }
                            else
                            {
                                Rectangle cropRect = new Rectangle(x - pos_x, y - pos_y, width, height);
                                //Rectangle cropRect = new Rectangle(...);
                                //Bitmap src = Image.FromFile(fileName) as Bitmap;
                                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                                using (Graphics g = Graphics.FromImage(target))
                                {
                                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                                     cropRect,
                                                     GraphicsUnit.Pixel);
                                }
                                graphics.DrawImage(target, x, y);
                            }
                            src.Dispose();
                        }
                    } */
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
                fileName = FullFileDir + @"\assets\" + fileName + ".png"; 
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
            ImageMagick.MagickImage image = new ImageMagick.MagickImage(bitmap);
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
            ImageMagick.MagickImage image = new ImageMagick.MagickImage(inputImage);
            ImageMagick.MagickImage combineMask = new ImageMagick.MagickImage(mask);

            image.Composite(combineMask, ImageMagick.CompositeOperator.In, ImageMagick.Channels.Alpha);

            Logger.WriteLine("* ApplyMask (end)");
            return image.ToBitmap();
        }

        public Bitmap ApplyWidgetMask(Bitmap inputImage, string fg_mask)
        {
            Logger.WriteLine("* ApplyWidgetMask");
            ImageMagick.MagickImage image = new ImageMagick.MagickImage(inputImage);
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
            string Watch_Skin_file_name = textBox_WatchSkin_Path.Text;
            if (!File.Exists(Watch_Skin_file_name))
                Watch_Skin_file_name = Application.StartupPath + Watch_Skin_file_name;

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

            Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
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
            if (ProgramSettings.Watch_Model == "GTS 4")
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");

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
    }
}
