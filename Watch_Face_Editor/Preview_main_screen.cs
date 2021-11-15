using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        /// <param name="showAnimation">Показывать анимацию при предпросмотре</param>
        /// <param name="showProgressArea">Подсвечивать круговую шкалу при наличии фонового изображения</param>
        /// <param name="showCentrHend">Подсвечивать центр стрелки</param>
        /// <param name="showWidgetsArea">Подсвечивать область виджетов</param>
        /// <param name="link">0 - основной экран; 1 - AOD</param>
        public void Preview_screen(Graphics gPanel, float scale, bool crop, bool WMesh, bool BMesh, bool BBorder,
            bool showShortcuts, bool showShortcutsArea, bool showShortcutsBorder, bool showAnimation, bool showProgressArea,
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
            gPanel.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height));
            //src.Dispose();
            #endregion

            #region Background
            Background background = null;
            if (radioButton_ScreenNormal.Checked)
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
                if (background.BackgroundImage != null && background.BackgroundImage.src.Length > 0 &&
                    background.visible)
                {
                    src = OpenFileStream(background.BackgroundImage.src);
                    int x = background.BackgroundImage.x;
                    int y = background.BackgroundImage.y;
                    int w = background.BackgroundImage.w;
                    int h = background.BackgroundImage.h;
                    gPanel.DrawImage(src, new Rectangle(x, y, w, h));
                }
                if (background.BackgroundColor != null && background.visible)
                {
                    Color color = StringToColor(background.BackgroundColor.color);
                    int x = background.BackgroundColor.x;
                    int y = background.BackgroundColor.y;
                    int w = background.BackgroundColor.w;
                    int h = background.BackgroundColor.h;
                    gPanel.Clear(color);
                }
            }
            #endregion

            #region Elements
            List<Object> Elements = null;
            if (radioButton_ScreenNormal.Checked)
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
                        case "ElementDigitalTime":
                            ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                            int time_offsetX = -1;
                            int time_offsetY = -1;
                            int time_spasing = 0;
                            bool _pm = false;

                            if (DigitalTime.Hour != null && DigitalTime.Hour.img_First != null
                                && DigitalTime.Hour.img_First.Length > 0 && DigitalTime.Hour.visible)
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
                                    if (DigitalTime.AmPm.am_img != null && DigitalTime.AmPm.am_img.Length > 0 &&
                                        DigitalTime.AmPm.pm_img != null && DigitalTime.AmPm.pm_img.Length > 0)
                                    {
                                        if (value > 11)
                                        {
                                            _pm = true;
                                            value -= 12;
                                        }
                                        if (value == 0) value = 12; 
                                    }
                                }

                                time_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y,
                                                    spasing, alignment, value, addZero, 2, separator_index, BBorder);

                                if (DigitalTime.Hour.icon.Length > 0)
                                {
                                    imageIndex = ListImages.IndexOf(DigitalTime.Hour.icon);
                                    x = DigitalTime.Hour.iconPosX;
                                    y = DigitalTime.Hour.iconPosY;

                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                                }
                            }

                            if (DigitalTime.Minute != null && DigitalTime.Minute.img_First != null
                                && DigitalTime.Minute.img_First.Length > 0 && DigitalTime.Minute.visible)
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

                                if (DigitalTime.Minute.icon.Length > 0)
                                {
                                    imageIndex = ListImages.IndexOf(DigitalTime.Minute.icon);
                                    x = DigitalTime.Minute.iconPosX;
                                    y = DigitalTime.Minute.iconPosY;

                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                                }
                            }
                            else time_offsetX = -1;

                            if (DigitalTime.Second != null && DigitalTime.Second.img_First != null
                                && DigitalTime.Second.img_First.Length > 0 && DigitalTime.Second.visible)
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
                                int value = WatchFacePreviewSet.Time.Minutes;
                                int separator_index = -1;
                                if (DigitalTime.Second.unit != null && DigitalTime.Second.unit.Length > 0)
                                    separator_index = ListImages.IndexOf(DigitalTime.Second.unit);


                                time_offsetX = Draw_dagital_text(gPanel, imageIndex, x, y,
                                                    spasing, alignment, value, addZero, 2, separator_index, BBorder);

                                if (DigitalTime.Second.icon.Length > 0)
                                {
                                    imageIndex = ListImages.IndexOf(DigitalTime.Second.icon);
                                    x = DigitalTime.Second.iconPosX;
                                    y = DigitalTime.Second.iconPosY;

                                    src = OpenFileStream(ListImagesFullName[imageIndex]);
                                    gPanel.DrawImage(src, new Rectangle(x, y, src.Width, src.Height));
                                }
                            }

                            break;
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
                    gPanel.DrawLine(pen, new Point(offSet_X + i * LineDistance, 0), new Point(offSet_X + i * LineDistance, 454));
                    gPanel.DrawLine(pen, new Point(offSet_X - i * LineDistance, 0), new Point(offSet_X - i * LineDistance, 454));

                    gPanel.DrawLine(pen, new Point(0, offSet_Y + i * LineDistance), new Point(454, offSet_Y + i * LineDistance));
                    gPanel.DrawLine(pen, new Point(0, offSet_Y - i * LineDistance), new Point(454, offSet_Y - i * LineDistance));

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
                    gPanel.DrawLine(pen, new Point(offSet_X + i * LineDistance, 0), new Point(offSet_X + i * LineDistance, 454));
                    gPanel.DrawLine(pen, new Point(offSet_X - i * LineDistance, 0), new Point(offSet_X - i * LineDistance, 454));

                    gPanel.DrawLine(pen, new Point(0, offSet_Y + i * LineDistance), new Point(454, offSet_Y + i * LineDistance));
                    gPanel.DrawLine(pen, new Point(0, offSet_Y - i * LineDistance), new Point(454, offSet_Y - i * LineDistance));

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
                mask = AplyMask(mask);
                gPanel.DrawImage(mask, new Rectangle(0, 0, mask.Width, mask.Height));
                mask.Dispose();
            }
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
            int DateLenght = width * value_lenght + 1;
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
                        graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
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
                        graphics.DrawImage(src, new Rectangle(PointX, PointY, src.Width, src.Height));
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

        private Bitmap AplyMask(Bitmap bitmap)
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

        private int AlignmentToInt(string alignment)
        {
            int result;
            switch (alignment)
            {
                case "Left":
                    result = 0;
                    break;
                case "Center":
                    result = 1;
                    break;
                case "Right":
                    result = 2;
                    break;

                default:
                    result = 0;
                    break;

            }
            return result;
        }
    }
}
