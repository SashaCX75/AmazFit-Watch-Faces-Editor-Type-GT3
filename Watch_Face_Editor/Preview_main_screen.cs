using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public void Preview_screen(Graphics gPanel, float scale, bool crop, bool WMesh, bool BMesh, bool BBorder,
            bool showShortcuts, bool showShortcutsArea, bool showShortcutsBorder, bool showShortcutsImage, 
            bool showAnimation, bool showProgressArea,
            bool showCentrHend, bool showWidgetsArea, int link)
        {
            int offSet_X = 227;
            int offSet_Y = 227;

            Bitmap src = new Bitmap(1, 1);
            gPanel.ScaleTransform(scale, scale, MatrixOrder.Prepend);
            int i;
            //gPanel.SmoothingMode = SmoothingMode.AntiAlias;
            //if (link == 2) goto AnimationEnd;

            #region Black background
            Logger.WriteLine("Preview_screen (Black background)");
            src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3.png");
            if (radioButton_GTR3_Pro.Checked)
            {
                src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
            }
            if (radioButton_GTS3.Checked)
            {
                src = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_3.png");
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
            }
            if (background != null)
            {
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
                    string type = element.GetType().Name;
                    switch (type)
                    {
                        #region ElementDigitalTime
                        case "ElementDigitalTime":
                            ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                            if(!DigitalTime.visible) continue;
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
                                    if(WatchFacePreviewSet.Time.Hours > 11)
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
                            if (!AnalogTime.visible) continue;

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
                                    DrawAnalogClock(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

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
                                    DrawAnalogClock(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

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
                                    DrawAnalogClock(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

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

                        #region ElementDateDay
                        case "ElementDateDay":
                            ElementDateDay DateDay = (ElementDateDay)element;
                            if (!DateDay.visible) continue;

                            for (int index = 1; index <= 2; index++)
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
                                    Day--;
                                    float angle = startAngle + Day * (endAngle - startAngle) / 30f;

                                    if (DateDay.Pointer.scale != null && DateDay.Pointer.scale.Length > 0)
                                    {
                                        image_index = ListImages.IndexOf(DateDay.Pointer.scale);
                                        x = DateDay.Pointer.scale_x;
                                        y = DateDay.Pointer.scale_y;

                                        src = OpenFileStream(ListImagesFullName[image_index]);
                                        gPanel.DrawImage(src, x, y);
                                    }

                                    DrawAnalogClock(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

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
                            if (!DateMonth.visible) continue;

                            for (int index = 1; index <= 3; index++)
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
                                    Month--;
                                    float angle = startAngle + Month * (endAngle - startAngle) / 11f;

                                    if (DateMonth.Pointer.scale != null && DateMonth.Pointer.scale.Length > 0)
                                    {
                                        image_index = ListImages.IndexOf(DateMonth.Pointer.scale);
                                        x = DateMonth.Pointer.scale_x;
                                        y = DateMonth.Pointer.scale_y;

                                        src = OpenFileStream(ListImagesFullName[image_index]);
                                        gPanel.DrawImage(src, x, y);
                                    }

                                    DrawAnalogClock(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

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
                            if (!DateYear.visible) continue;

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
                            if (!DateWeek.visible) continue;

                            for (int index = 1; index <= 2; index++)
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
                                    int Month = WatchFacePreviewSet.Date.Month;
                                    Month--;
                                    float angle = startAngle + Month * (endAngle - startAngle) / 11f;

                                    if (DateWeek.Pointer.scale != null && DateWeek.Pointer.scale.Length > 0)
                                    {
                                        image_index = ListImages.IndexOf(DateWeek.Pointer.scale);
                                        x = DateWeek.Pointer.scale_x;
                                        y = DateWeek.Pointer.scale_y;

                                        src = OpenFileStream(ListImagesFullName[image_index]);
                                        gPanel.DrawImage(src, x, y);
                                    }

                                    DrawAnalogClock(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

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
                            if (!statusElement.visible) continue;

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
                            if (!shortcutsElement.visible || !showShortcuts) continue;

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
                                    DrawShortcuts(gPanel, img_click_step, 
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_heart != null && index == img_click_heart.position)
                                {
                                    DrawShortcuts(gPanel, img_click_heart,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_spo2 != null && index == img_click_spo2.position)
                                {
                                    DrawShortcuts(gPanel, img_click_spo2,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_pai != null && index == img_click_pai.position)
                                {
                                    DrawShortcuts(gPanel, img_click_pai,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_stress != null && index == img_click_stress.position)
                                {
                                    DrawShortcuts(gPanel, img_click_stress,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_weather != null && index == img_click_weather.position)
                                {
                                    DrawShortcuts(gPanel, img_click_weather,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_altimeter != null && index == img_click_altimeter.position)
                                {
                                    DrawShortcuts(gPanel, img_click_altimeter,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_sunrise != null && index == img_click_sunrise.position)
                                {
                                    DrawShortcuts(gPanel, img_click_sunrise,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_alarm != null && index == img_click_alarm.position)
                                {
                                    DrawShortcuts(gPanel, img_click_alarm,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_sleep != null && index == img_click_sleep.position)
                                {
                                    DrawShortcuts(gPanel, img_click_sleep,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_countdown != null && index == img_click_countdown.position)
                                {
                                    DrawShortcuts(gPanel, img_click_countdown,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                                if (img_click_stopwatch != null && index == img_click_stopwatch.position)
                                {
                                    DrawShortcuts(gPanel, img_click_stopwatch,
                                        showShortcutsArea, showShortcutsBorder, showShortcutsImage);
                                }
                            }
                            break;
                        #endregion

                        #region ElementSteps
                        case "ElementSteps":
                            ElementSteps activityElement = (ElementSteps)element;
                            if (!activityElement.visible) continue;

                            hmUI_widget_IMG_LEVEL img_level = activityElement.Images;
                            hmUI_widget_IMG_PROGRESS img_prorgess = activityElement.Segments;
                            hmUI_widget_IMG_NUMBER img_number = activityElement.Number;
                            hmUI_widget_IMG_NUMBER img_number_target = activityElement.Number_Target;
                            hmUI_widget_IMG_POINTER img_pointer = activityElement.Pointer;
                            Circle_Scale circle_scale = activityElement.Circle_Scale;
                            Linear_Scale linear_scale = activityElement.Linear_Scale;
                            hmUI_widget_IMG icon = activityElement.Icon;

                            int elementValue = WatchFacePreviewSet.Activity.Steps;
                            int value_lenght = 5;
                            int goal = WatchFacePreviewSet.Activity.StepsGoal;
                            float progress = (float)WatchFacePreviewSet.Activity.Steps / WatchFacePreviewSet.Activity.StepsGoal;

                            int valueImgIndex = -1;
                            int valueSegmentIndex = -1;
                            int imgCount = 0;
                            int segmentCount = 0;
                            if (img_level != null && img_level.image_length > 0)
                            {
                                imgCount = img_level.image_length;
                                valueImgIndex = (int)((imgCount-1) * progress);
                                if (progress < 0.01) valueImgIndex = -1;
                                if (valueImgIndex >= imgCount) valueImgIndex = (int)(imgCount - 1);
                            }
                            if (img_prorgess != null && img_prorgess.image_length > 0)
                            {
                                segmentCount = img_prorgess.image_length;
                                valueSegmentIndex = (int)((segmentCount-1) * progress);
                                if (progress < 0.01) valueSegmentIndex = -1;
                                if (valueSegmentIndex >= segmentCount) valueImgIndex = (int)(segmentCount - 1);
                            }

                            DrawActivity(gPanel, img_level, img_prorgess, img_number, img_number_target,
                                img_pointer, circle_scale, linear_scale, icon, elementValue, value_lenght, goal,
                                progress, valueImgIndex, valueSegmentIndex, BBorder, showProgressArea,
                                showCentrHend, "ElementSteps");


                            break;
                            #endregion
                    }
                }
            }
            #endregion
            //src.Dispose();

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
                if (radioButton_GTR3_Pro.Checked)
                {
                    mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                }
                if (radioButton_GTS3.Checked)
                {
                    mask = OpenFileStream(Application.StartupPath + @"\Mask\mask_gts_3.png");
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
            if (progress > 1) progress = 1;
            Bitmap src = new Bitmap(1, 1);

            for (int index = 1; index <= 10; index++)
            {
                if (images != null && images.img_First != null && images.img_First.Length > 0 &&
                    index == images.position && images.visible)
                {
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
                        spasing, alignment, (int)value, addZero, value_lenght, separator_index, BBorder);

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

                    float angle = startAngle + progress * (endAngle - startAngle);

                    if (pointer.scale != null && pointer.scale.Length > 0)
                    {
                        int image_index_scale = ListImages.IndexOf(pointer.scale);
                        int x_scale = pointer.scale_x;
                        int y_scale = pointer.scale_y;

                        src = OpenFileStream(ListImagesFullName[image_index_scale]);
                        gPanel.DrawImage(src, x_scale, y_scale);
                    }

                    DrawAnalogClock(gPanel, x, y, offsetX, offsetY, image_index, angle, showCentrHend);

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

        /// <summary>Рисует стрелки</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Центр стрелки X</param>
        /// <param name="y">Центр стрелки Y</param>
        /// <param name="offsetX">Смещение от центра по X</param>
        /// <param name="offsetY">Смещение от центра по Y</param>
        /// <param name="image_index">Номер изображения</param>
        /// <param name="angle">Угол поворота стрелки в градусах</param>
        /// <param name="center_marker">Отображать маркер на точке вращения</param>
        public void DrawAnalogClock(Graphics graphics, int x, int y, int offsetX, int offsetY, int image_index, float angle, bool showCentrHend)
        {
            //int centerX = 227;
            //int centerY = 227;
            //if (radioButton_GTS2.Checked)
            //{
            //    centerX = 174;
            //    centerY = 221;
            //}
            //if (radioButton_TRex_pro.Checked)
            //{
            //    centerX = 180;
            //    centerY = 180;
            //}
            //if (radioButton_ZeppE.Checked)
            //{
            //    centerX = 208;
            //    centerY = 208;
            //}
            //if (x == 0) x = centerX;
            //if (y == 0) y = centerY;

            Logger.WriteLine("* DrawAnalogClock");
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
            Logger.WriteLine("* DrawAnalogClock (end)");
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
        /// <param name="ActivityType">Номер активности (при необходимости)</param>
        private int Draw_dagital_text(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, int value, bool addZero, int value_lenght, int separator_index, bool BBorder,
            int ActivityType = 0)
        {
            while (spacing > 127)
            {
                spacing = spacing - 256;
            }
            while (spacing < -128)
            {
                spacing = spacing + 256;
            }

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
            char[] CH = value_S.ToCharArray();
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

            }
            DateLenghtReal = DateLenghtReal - spacing;

            src = OpenFileStream(ListImagesFullName[image_index]);
            int width = src.Width;
            int height = src.Height;
            //int DateLenght = width * value_lenght + spacing * (value_lenght - 1);
            if (ActivityType == 17) value_lenght = 5;
            if (ActivityType == 11) value_lenght = 3;
            int DateLenght = width * value_lenght;
            //int DateLenght = width * value_lenght + 1;
            if (spacing > 0) DateLenght = DateLenght + spacing * (value_lenght - 1);
            //else DateLenght = DateLenght - spacing;

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
                        PointX = PointX + src.Width + spacing;
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

        /// <summary>Пишем число системным шрифтом</summary>
        /// <param name="graphics">Поверхность для рисования</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата y</param>
        /// <param name="size">Размер шрифта</param>
        /// <param name="spacing">Величина отступа</param>
        /// <param name="color">Цвет шрифта</param>
        /// <param name="angle">Угол поворота надписи в градусах</param>
        /// <param name="value">Отображаемая величина</param>
        /// <param name="addZero">Отображать начальные нули</param>
        /// <param name="value_lenght">Количество отображаемых символов</param>
        /// <param name="BBorder">Рисовать рамку по координатам, вокруг элементов с выравниванием</param>
        /// <param name="ActivityType">Номер активности (при необходимости)</param>
        private int Draw_text(Graphics graphics, int x, int y, float size, int spacing, Color color,
            float angle, string value, bool BBorder, int ActivityType = 0)
        {
            while (spacing > 127)
            {
                spacing = spacing - 255;
            }
            while (spacing < -127)
            {
                spacing = spacing + 255;
            }

            int result = 0;
            size = size * 0.9f;
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
            float offsetY = 1.1f * (strSize1.Height - size);

            Logger.WriteLine("* Draw_text");
            char[] CH = value.ToCharArray();

            int PointX = (int)(-0.3 * offsetX);

            Logger.WriteLine("Draw value");
            SolidBrush drawBrush = new SolidBrush(color);

            graphics.TranslateTransform(x, y);
            graphics.RotateTransform(angle);
            try
            {
                foreach (char ch in CH)
                {
                    string str = ch.ToString();
                    Size strSize = TextRenderer.MeasureText(graphics, str, drawFont);
                    //SizeF stringSize = graphics.MeasureString(str, drawFont, 10000, strFormat);
                    //graphics.FillRectangle(new SolidBrush(Color.White), x + PointX, y + offsetY - strSize.Height, strSize.Width, strSize.Height);
                    graphics.DrawString(str, drawFont, drawBrush, PointX, offsetY, strFormat);

                    PointX = PointX + strSize.Width + spacing - offsetX;
                }

                result = x + TextRenderer.MeasureText(graphics, value, drawFont).Width - offsetX + (value.Length - 1) * spacing;
                if (BBorder)
                {
                    Logger.WriteLine("DrawBorder");
                    Rectangle rect = new Rectangle(0, (int)(-0.75 * size), result - x - 1, (int)(0.75 * size));
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
            finally
            {
                //graphics.RotateTransform(-angle);
                //graphics.TranslateTransform(-x, -y);
                graphics.ResetTransform();
            }

            Logger.WriteLine("* Draw_text (end)");
            return result;
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
        private int Draw_dagital_text(Graphics graphics, int image_index, int x, int y, int spacing,
            int alignment, double value, bool addZero, int value_lenght, int separator_index,
            int decimalPoint_index, int decCount, bool BBorder)
        {
            Logger.WriteLine("* Draw_dagital_text");
            value = Math.Round(value, decCount, MidpointRounding.AwayFromZero);
            while (spacing > 127)
            {
                spacing = spacing - 255;
            }
            while (spacing < -127)
            {
                spacing = spacing + 255;
            }
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


            src = OpenFileStream(ListImagesFullName[image_index]);
            int width = src.Width;
            int height = src.Height;
            int DateLenght = 4 * width;
            if (spacing > 0) DateLenght = DateLenght + 3 * spacing;
            if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                DateLenght = DateLenght + src.Width;
                if (spacing > 0) DateLenght = DateLenght + spacing;
            }
            if (separator_index >= 0 && separator_index < ListImagesFullName.Count)
            {
                src = OpenFileStream(ListImagesFullName[separator_index]);
                DateLenght = DateLenght + src.Width;
                if (spacing > 0) DateLenght = DateLenght + spacing;
            }

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
                        //src = new Bitmap(ListImagesFullName[i]);
                        src = OpenFileStream(ListImagesFullName[i]);
                        graphics.DrawImage(src, PointX, PointY);
                        //graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
                        PointX = PointX + src.Width + spacing;
                        //src.Dispose();
                    }
                }
                else
                {
                    if (decimalPoint_index >= 0 && decimalPoint_index < ListImagesFullName.Count)
                    {
                        //src = new Bitmap(ListImagesFullName[dec]);
                        src = OpenFileStream(ListImagesFullName[decimalPoint_index]);
                        graphics.DrawImage(src, PointX, PointY);
                        //graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
                        PointX = PointX + src.Width + spacing;
                        //src.Dispose();
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
                graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth,
                    (float)(startAngle - 0.007 * s * width), (float)(valueAngle + 0.015 * s * width));
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
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth, startAngle, endAngle);
                graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth,
                    (float)(startAngle - 0.007 * s * width), (float)(fullAngle + 0.015 * s * width));
                myHatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.Transparent);
                pen.Brush = myHatchBrush;
                //graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth, startAngle, endAngle);
                graphics.DrawArc(pen, arcX, arcY, CircleWidth, CircleWidth,
                    (float)(startAngle - 0.007 * s * width), (float)(fullAngle + 0.015 * s * width));

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
        /// <param name="showShortcutsArea">Подсвечивать область ярлыков рамкой</param>
        /// <param name="showShortcutsBorder">Подсвечивать область ярлыков заливкой</param>
        /// <param name="showShortcutsImage">Подсвечивать изображение, отображаемое при нажатии ярлыка</param>
        private void DrawShortcuts(Graphics graphics, hmUI_widget_IMG_CLICK shortcut,
             bool showShortcutsArea, bool showShortcutsBorder, bool showShortcutsImage)
        {
            if (shortcut != null && shortcut.src != null && shortcut.src.Length > 0 && shortcut.visible)
            {
                int imageIndex = ListImages.IndexOf(shortcut.src);
                int x = shortcut.x;
                int y = shortcut.y;
                int width = shortcut.w;
                int height = shortcut.h;

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

                if (showShortcutsImage)
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
            if (radioButton_GTR3_Pro.Checked)
            {
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
            }
            if (radioButton_GTS3.Checked)
            {
                mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
            }

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
    }
}
