using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        /// <summary>Определяем тип файла и способ его конвертации в Png</summary>
        private string ImageAutoDetectReadFormat(string fileNameFull, string targetFileName, int fix_color)
        {
            string path = "";
            if (File.Exists(fileNameFull))
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    //path = Path.GetDirectoryName(fileNameFull);
                    string targetFolder = Path.Combine(Path.GetDirectoryName(fileNameFull), "Png");
                    if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);
                    //string targetFileName = Path.Combine(targetFolder, fileName + ".png");
                    //path = Path.GetDirectoryName(fileNameFull);
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        byte[] _streamBuffer;
                        _streamBuffer = new byte[fileStream.Length];
                        fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                        Header header = new Header(_streamBuffer, fileNameFull, targetFileName);
                        if (header.GetExistsColorMap() == 1 && header.GetImageType() == 1) path = TgaToPng(fileNameFull, targetFileName, fix_color);
                        if (header.GetExistsColorMap() == 0 && header.GetImageType() == 2) path = TgaARGBToPng(fileNameFull, targetFileName, fix_color);
                        if (header.GetExistsColorMap() == 1 && header.GetImageType() == 9) path = TgaToPng(fileNameFull, targetFileName, fix_color);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(Properties.FormStrings.Img_Convert_Error_WrongImg + Environment.NewLine +
                        exp);
                }
            }
            return path;
        }

        /// <summary>Определяем способ конвертации в Tga</summary>
        private void ImageAutoDetectWriteFormat(string fileNameFull, string targetFolder, bool fix_size, int fix_color)
        {
            if (!File.Exists(fileNameFull)) return;
            if (!ProgramSettings.Use_ARGB_encoding)
            {
                fileNameFull = PngToTga(fileNameFull, targetFolder, fix_color, fix_size);
                if (fileNameFull != null) ImageFix(fileNameFull, fix_color);
                return;
            }
            else if (ProgramSettings.ARGB_encoding_forced)
            {
                ImageMagick.MagickImage image;
                using (var fileStream = File.OpenRead(fileNameFull))
                {
                    image = new ImageMagick.MagickImage(fileStream);
                }
                ImageWidth = image.Width;
                int imageWidth = ImageWidth;
                int newWidth = ImageWidth;
                int newHeight = image.Height;

                if (fix_size)
                {
                    while (newWidth % 16 != 0)
                    {
                        newWidth++;
                    }

                    if (ImageWidth != newWidth)
                    {
                        Bitmap bitmap = image.ToBitmap();
                        Bitmap bitmapNew = new Bitmap(newWidth, newHeight);
                        Graphics gfx = Graphics.FromImage(bitmapNew);
                        gfx.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                        image = new ImageMagick.MagickImage(bitmapNew);
                        //image_temp = new ImageMagick.MagickImage(bitmapNew);
                    }
                }

                if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

                fileNameFull = PngToTgaARGB(fileNameFull, targetFolder, image, fix_color);
                ImageFix_ARGB(fileNameFull, imageWidth);
                return;
            }
            else
            {
                try
                {
                    //string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    //string path = Path.GetDirectoryName(fileNameFull);
                    ImageMagick.MagickImage image;

                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        image = new ImageMagick.MagickImage(fileStream);
                    }
                    ImageWidth = image.Width;
                    int imageWidth = ImageWidth;
                    int newWidth = ImageWidth;
                    int newHeight = image.Height;

                    if (fix_size)
                    {
                        while (newWidth % 16 != 0)
                        {
                            newWidth++;
                        }

                        if (ImageWidth != newWidth)
                        {
                            Bitmap bitmap = image.ToBitmap();
                            Bitmap bitmapNew = new Bitmap(newWidth, newHeight);
                            Graphics gfx = Graphics.FromImage(bitmapNew);
                            gfx.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            image = new ImageMagick.MagickImage(bitmapNew);
                            //image_temp = new ImageMagick.MagickImage(bitmapNew);
                        }
                    }

                    if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

                    if (image.TotalColors >= ProgramSettings.ARGB_encoding_color_count)
                    {
                        fileNameFull = PngToTgaARGB(fileNameFull, targetFolder, image, fix_color);
                        ImageFix_ARGB(fileNameFull, imageWidth);
                        return ;
                    }
                    else
                    {
                        fileNameFull = PngToTga(fileNameFull, targetFolder, fix_color, fix_size);
                        if (fileNameFull != null) ImageFix(fileNameFull, fix_color);
                        return ;
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine + exp);
                }
                return;
            }
        }

        /// <summary>Преобразуем ARGB Tga в Png</summary>
        private string TgaARGBToPng(string file, string targetFile, int fix_color)
        {
            string path = "";
            if (File.Exists(file))
            {
                try
                {
                    //string fileNameFull = openFileDialog.FileName;
                    ImageMagick.MagickImage image;
                    string fileNameFull = file;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    path = Path.GetDirectoryName(fileNameFull);
                    //fileName = Path.Combine(path, fileName);
                    int RealWidth = -1;
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        byte[] _streamBuffer;
                        _streamBuffer = new byte[fileStream.Length];
                        fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                        Header header = new Header(_streamBuffer, Path.GetFileName(fileNameFull));
                        ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());
                        RealWidth = imageDescription.GetRealWidth();
                    }

                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        image = new ImageMagick.MagickImage(fileStream, ImageMagick.MagickFormat.Tga);
                    }

                    image.Format = ImageMagick.MagickFormat.Png32;
                    if (RealWidth > 0 && RealWidth != image.Width)
                    {
                        int height = image.Height;
                        image = (ImageMagick.MagickImage)image.Clone(RealWidth, height);
                    }

                    //ImageMagick.IMagickImage Blue = image.Separate(ImageMagick.Channels.Blue).First();
                    //ImageMagick.IMagickImage Red = image.Separate(ImageMagick.Channels.Red).First();
                    //ImageMagick.IMagickImage Alpha = image.Separate(ImageMagick.Channels.Red).First();
                    //if (fix_color)
                    //{
                    //    image.Composite(Red, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Blue);
                    //    image.Composite(Blue, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Red);
                    //}
                    image.Write(targetFile);
                }
                catch (Exception exp)
                {
                    if (exp.Message != Properties.FormStrings.Img_Convert_Error_NoPalette)
                    {
                        MessageBox.Show(Properties.FormStrings.Img_Convert_Error_WrongImg + Environment.NewLine +
                                        Properties.FormStrings.Img_Convert_Error_NotSave1 + " \"" + Path.GetFileName(targetFile) + "\" " +
                                        Properties.FormStrings.Img_Convert_Error_NotSave2 + Environment.NewLine + exp);
                    }
                }
            }
            path = Path.GetDirectoryName(targetFile);
            return path;
        }

        /// <summary>Преобразуем Tga в Png</summary>
        private string TgaToPng(string file, string targetFile, int fix_color)
        {
            string path = "";
            if (File.Exists(file))
            {
                try
                {
                    //string fileNameFull = openFileDialog.FileName;
                    ImageMagick.MagickImage image;
                    string fileNameFull = file;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    path = Path.GetDirectoryName(fileNameFull);
                    //fileName = Path.Combine(path, fileName);
                    int RealWidth = -1;
                    bool colored = true;
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        byte[] _streamBuffer;
                        _streamBuffer = new byte[fileStream.Length];
                        fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                        Header header = new Header(_streamBuffer, Path.GetFileName(fileNameFull));
                        if (header.GetColorMapCount() == 0) colored = false;
                        ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());
                        RealWidth = imageDescription.GetRealWidth();
                    }

                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        image = new ImageMagick.MagickImage(fileStream, ImageMagick.MagickFormat.Tga);
                    }

                    image.Format = ImageMagick.MagickFormat.Png32;
                    if (RealWidth > 0 && RealWidth != image.Width)
                    {
                        int height = image.Height;
                        image = (ImageMagick.MagickImage)image.Clone(RealWidth, height);
                    }

                    ImageMagick.IMagickImage Blue = image.Separate(ImageMagick.Channels.Blue).First();
                    ImageMagick.IMagickImage Red = image.Separate(ImageMagick.Channels.Red).First();
                    ImageMagick.IMagickImage Alpha = image.Separate(ImageMagick.Channels.Red).First();
                    if (fix_color == 1)
                    {
                        image.Composite(Red, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Blue);
                        image.Composite(Blue, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Red);
                        if (!colored)
                        {
                            image.Composite(Alpha, ImageMagick.CompositeOperator.CopyAlpha, ImageMagick.Channels.Alpha);
                        }
                        image.Write(targetFile, MagickFormat.Png32);
                    }
                    if (fix_color == 2)
                    {
                        if (!colored)
                        {
                            image.Composite(Alpha, ImageMagick.CompositeOperator.CopyAlpha, ImageMagick.Channels.Alpha);
                        }
                        image.Write(targetFile, MagickFormat.Png32);
                    }
                    if (fix_color == 3)
                    {
                        if (image.ColormapSize == 256)
                        {
                            //IMagickColor<ushort>[,] colorMap = new IMagickColor<ushort>[16, 16];
                            MagickColor[,] colorMap = new MagickColor[16, 16];
                            int index = 0;
                            for (int x = 0; x < 16; x++)
                            {
                                for (int y = 0; y < 16; y++)
                                {
                                    // TODO :: Kartun, check x2
                                    colorMap[x, y] = image.GetColormap(index);
                                    //colorMap[x, y] = image.GetColormapColor(index);
                                    index++;
                                }
                            }
                            index = 0;
                            for (int x = 0; x < 16; x++)
                            {
                                for (int y = 0; y < 16; y++)
                                {
                                    // TODO :: Kartun, check
                                    image.SetColormap(index, colorMap[y, x]);
                                    //image.SetColormapColor(index, colorMap[y, x]);
                                    index++;
                                }
                            }
#if DEBUG
                            index = 0;
                            for (int x = 0; x < 16; x++)
                            {
                                for (int y = 0; y < 16; y++)
                                {
                                    // TODO :: Kartun, check
                                    colorMap[x, y] = image.GetColormap(index);
                                    //colorMap[x, y] = image.GetColormapColor(index);
                                    index++;
                                }
                            }
#endif
                        }
                        if (!colored)
                        {
                            image.Composite(Alpha, ImageMagick.CompositeOperator.CopyAlpha, ImageMagick.Channels.Alpha);
                        }

                        image.Format = MagickFormat.Png32;
                        image.Write(targetFile + "_temp", MagickFormat.Png);

                        using (var fileStream = File.OpenRead(targetFile + "_temp"))
                        {
                            image = new ImageMagick.MagickImage(fileStream, ImageMagick.MagickFormat.Png32);
                            image.Write(targetFile, ImageMagick.MagickFormat.Png32);
                        }
                        File.Delete(targetFile + "_temp");
                    }
                }
                catch (Exception exp)
                {
                    if (exp.Message != Properties.FormStrings.Img_Convert_Error_NoPalette)
                    {
                        MessageBox.Show(Properties.FormStrings.Img_Convert_Error_WrongImg + Environment.NewLine +
                                        Properties.FormStrings.Img_Convert_Error_NotSave1 + " \"" + Path.GetFileName(targetFile) + "\" " + 
                                        Properties.FormStrings.Img_Convert_Error_NotSave2 + Environment.NewLine + exp);
                    }
                }
            }
            path = Path.GetDirectoryName(targetFile);
            return path;
        }

        /// <summary>Преобразуем Png в Tga</summary>
        private string PngToTga(string fileNameFull, string targetFolder, int fix_color, bool fix_size)
        {
            if (File.Exists(fileNameFull))
            {
                colorMapList.Clear();
                try
                {
                    //string fileNameFull = openFileDialog.FileName;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    //fileName = Path.Combine(path, fileName);
                    ImageMagick.MagickImage image;
                    ImageMagick.MagickImage image_temp;

                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        image = new ImageMagick.MagickImage(fileStream);
                    }
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        //image = new ImageMagick.MagickImage(fileStream);
                        image_temp = new ImageMagick.MagickImage(fileStream);
                    }
                    ImageWidth = image.Width;
                    int newWidth = ImageWidth;
                    int newHeight = image.Height;
                    if (fix_size)
                    //if (model != "Amazfit Band 7" && model != "GTS 4 mini")
                    {
                        while (newWidth % 16 != 0)
                        {
                            newWidth++;
                        }

                        if (ImageWidth != newWidth)
                        {
                            Bitmap bitmap = image.ToBitmap();
                            Bitmap bitmapNew = new Bitmap(newWidth, newHeight);
                            Graphics gfx = Graphics.FromImage(bitmapNew);
                            gfx.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            image = new MagickImage(ImgConvert.CopyImageToByteArray(bitmapNew));
                            image_temp = new ImageMagick.MagickImage(ImgConvert.CopyImageToByteArray(bitmapNew));
                        }
                    }
                    if (fix_color == 3)
                    {
                        newWidth = image.Width;
                        newHeight = image.Height + 1;

                        Bitmap bitmap = image.ToBitmap();
                        Color pixel1 = bitmap.GetPixel(0, 0);
                        Color pixel2 = bitmap.GetPixel(image.Width - 1, 0);
                        Color pixel3 = bitmap.GetPixel(0, image.Height - 1);
                        Color pixel4 = bitmap.GetPixel(image.Width - 1, image.Height - 1);
                        if(pixel1.A == 255 && pixel2.A == 255 && pixel3.A == 255 && pixel4.A == 255)
                        {
                            //if (pixel1.R < 5 && pixel1.G < 5 && pixel4.G < 5) bitmap.SetPixel(0, 0, Color.FromArgb(250, 5, 5, 5));
                            //else bitmap.SetPixel(image.Width - 1, image.Height - 1, Color.FromArgb(250, pixel4.R, pixel4.G, pixel4.B));
                            //image = new MagickImage(ImgConvert.CopyImageToByteArray(bitmap));
                            //image_temp = new ImageMagick.MagickImage(ImgConvert.CopyImageToByteArray(bitmap));

                            Bitmap bitmapNew = new Bitmap(newWidth, newHeight);
                            Graphics gfx = Graphics.FromImage(bitmapNew);
                            gfx.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                            image = new MagickImage(ImgConvert.CopyImageToByteArray(bitmapNew));
                            image_temp = new ImageMagick.MagickImage(ImgConvert.CopyImageToByteArray(bitmapNew));
                        }
                    }
                    // TODO :: Kartun, check
                    ImageMagick.Pixel pixel = image.GetPixels().GetPixel(0, 0);
                    //IPixel<ushort> pixel = image.GetPixels().GetPixel(0, 0);

                    //pixel = new ImageMagick.Pixel(0, 0, 4);
                    bool transparent = false;
                    //if (pixel.Channels == 4 && pixel[3] < 256) transparent = true;

                    // пытаеммся преобразовать в формат 32bit но с картой цветов
                    image.ColorType = ImageMagick.ColorType.Palette;
                    if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                    {
                        //image = image_temp;
                        //ushort[] p;
                        //if (pixel[2] > 256)
                        //{
                        //    if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256), pixel[3] };
                        //    else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256) };
                        //}
                        //else
                        //{
                        //    if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256), pixel[3] };
                        //    else
                        //    {
                        //        p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256) };
                        //        transparent = true;
                        //    }
                        //}

                        image = image_temp;
                        ushort[] p;
                        int pixel_offset = 256;
                        if (pixel[2] > 256) pixel_offset = -256;
                        if (pixel.Channels == 5) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + pixel_offset), pixel[3], pixel[4] };
                        else if(pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + pixel_offset), pixel[3] };
                        else
                        {
                            p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + pixel_offset) };
                            if (pixel_offset == 256) transparent = true;
                        }
                        image.GetPixels().SetPixel(0, 0, p);

                        image.ColorType = ImageMagick.ColorType.Palette;
                        // TODO :: Kartun, check
                        //pixel = image.GetPixels().GetPixel(0, 0);
                        if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                        {
                            MessageBox.Show(Properties.FormStrings.Img_Convert_Error_Not32bit +
                                Environment.NewLine + fileNameFull);
                            return null;
                        }
                    }


                    for (int i = 0; i < image.ColormapSize; i++)
                    {
                        MagickColor ee = image.GetColormap(i);
                        colorMapList.Add(image.GetColormap(i));
                        // TODO :: Kartun, Confirm
                        //colorMapList.Add(image.GetColormapColor(i).ToColor());
                    }
                    while (fix_color == 3 && colorMapList.Count < 256)
                    {
                        colorMapList.Add(Color.FromArgb(0, 0, 0, 0));
                    }
                    if (transparent && colorMapList.Count == 2) // если двухцветное изображение
                    {
                        colorMapList[0] = Color.FromArgb(0, colorMapList[0].R, colorMapList[0].G, colorMapList[0].B);
                        colorMapList[1] = Color.FromArgb(0, colorMapList[1].R, colorMapList[1].G, colorMapList[1].B);
                    }
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }
                    string newFileName = Path.Combine(targetFolder, fileName + ".tga");
                    image.Write(newFileName, ImageMagick.MagickFormat.Tga);
                    return newFileName;

                }
                catch (Exception exp)
                {
                    MessageBox.Show(Properties.FormStrings.Img_Convert_Error_WrongImg + Environment.NewLine +
                            exp);
                }
            }
            return null;
        }

        private void ImageFix(string fileNameFull, int fix_color)
        {
            if (File.Exists(fileNameFull))
            {
                try
                {
                    byte[] _streamBuffer;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    //fileName = Path.Combine(path, fileName);

                    //ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);

                    // читаем картинку в массив
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        _streamBuffer = new byte[fileStream.Length];
                        fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                        Header header = new Header(_streamBuffer);
                        ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());

                        int ColorMapCount = header.GetColorMapCount(); // количество цветов в карте
                        byte ColorMapEntrySize = header.GetColorMapEntrySize(); // битность цвета
                        byte ImageIDLength = header.GetImageIDLength(); // длина описания
                        ColorMap ColorMap = new ColorMap(_streamBuffer, ColorMapCount, ColorMapEntrySize, 18 + ImageIDLength);

                        int ColorMapLength = ColorMap._colorMap.Length;
                        Image_data imageData = new Image_data(_streamBuffer, 18 + ImageIDLength + ColorMapLength);

                        Footer footer = new Footer();

                        #region fix
                        header.SetImageIDLength(46);
                        imageDescription.SetSize(46, ImageWidth);
                        //imageDescription.SetSize(46, header.Width);

                        int colorMapCount = ColorMap.ColorMapCount;
                        //if (checkBox_Color256.Checked && !checkBox_32bit.Checked)
                        //{
                        //    colorMapCount = 256;
                        //    header.SetColorMapCount(colorMapCount);
                        //    if (!checkBox_32bit.Checked) ColorMap.SetColorCount(colorMapCount);
                        //}
                        bool argb_brga = true;
                        colorMapCount = 256;
                        header.SetColorMapCount(colorMapCount);
                        byte colorMapEntrySize = 32;

                        ColorMap.RestoreColor(colorMapList);
                        ColorMap.ColorsFix(argb_brga, colorMapCount, colorMapEntrySize, fix_color);
                        header.SetColorMapEntrySize(32);
                        #endregion

                        int newLength = 18 + header.GetImageIDLength() + ColorMap._colorMap.Length + imageData._imageData.Length;
                        //if (checkBox_Footer.Checked) newLength = newLength + footer._footer.Length;
                        byte[] newTGA = new byte[newLength];

                        header._header.CopyTo(newTGA, 0);
                        int offset = header._header.Length;

                        imageDescription._imageDescription.CopyTo(newTGA, offset);
                        offset = offset + imageDescription._imageDescription.Length;

                        ColorMap._colorMap.CopyTo(newTGA, offset);
                        offset = offset + ColorMap._colorMap.Length;

                        imageData._imageData.CopyTo(newTGA, offset);
                        offset = offset + imageData._imageData.Length;

                        //if (checkBox_Footer.Checked) footer._footer.CopyTo(newTGA, offset);

                        if (newTGA != null && newTGA.Length > 0)
                        {
                            string newFileName = Path.Combine(path, fileName + ".png");

                            using (var fileStreamTGA = File.OpenWrite(newFileName))
                            {
                                fileStreamTGA.Write(newTGA, 0, newTGA.Length);
                                fileStreamTGA.Flush();
                            }
                        }
                    }

                    try
                    {
                        File.Delete(fileNameFull);
                    }
                    catch (Exception)
                    {
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageFix_Error + Environment.NewLine + exp,
                        Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private string PngToTgaARGB(string fileNameFull, string targetFolder, ImageMagick.MagickImage image, int fix_color)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                //string path = Path.GetDirectoryName(fileNameFull);
                //ImageMagick.MagickImage image_temp = new ImageMagick.MagickImage(image);

                if (image.ChannelCount != 4)
                {
                    image.Format = MagickFormat.Png32;
                    image.Alpha(AlphaOption.On);
                }
                if (image.ColorSpace != ImageMagick.ColorSpace.sRGB || image.ChannelCount != 4)
                {
                    MessageBox.Show("Изображение не должно быть монохромным и должно быть в формате 32bit" +
                        Environment.NewLine + fileNameFull);
                    return null;
                }

                if (fix_color == 1)
                {
                    IPixelCollection pixels = image.GetPixels();
                    for (int w = 0; w < image.Width; w++)
                    {
                        for (int h = 0; h < image.Height; h++)
                        {
                            ushort red = pixels[w, h].GetChannel(0);
                            ushort green = pixels[w, h].GetChannel(1);
                            ushort blue = pixels[w, h].GetChannel(2);
                            ushort alpha = pixels[w, h].GetChannel(3);
                            float scale = (float)alpha / ushort.MaxValue;

                            red = (ushort)(red * scale);
                            green = (ushort)(green * scale);
                            blue = (ushort)(blue * scale);

                            pixels[w, h].SetChannel(0, red);
                            pixels[w, h].SetChannel(1, green);
                            pixels[w, h].SetChannel(2, blue);
                            pixels[w, h].SetChannel(3, alpha);
                        }
                    }
                }


                //path = Path.Combine(path, "Fix");
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }
                string newFileName = Path.Combine(targetFolder, fileName + ".tga");
                image.Write(newFileName, ImageMagick.MagickFormat.Tga);
                return newFileName;

            }
            catch (Exception exp)
            {
                MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine + exp);
            }
            return null;
        }

        private void ImageFix_ARGB(string fileNameFull, int imageWidth)
        {
            if (File.Exists(fileNameFull))
            {
                try
                {
                    byte[] _streamBuffer;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);

                    // читаем картинку в массив
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        _streamBuffer = new byte[fileStream.Length];
                        fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                        Header header = new Header(_streamBuffer);
                        ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());

                        byte ImageIDLength = header.GetImageIDLength(); // длина описания
                        Image_data imageData = new Image_data(_streamBuffer, 18 + ImageIDLength);

                        Footer footer = new Footer();

                        #region fix
                        header.SetImageIDLength(46);
                        imageDescription.SetSize(46, imageWidth);

                        header.SetColorMapCount(0);
                        header.SetColorMapEntrySize(0);
                        #endregion

                        int newLength = 18 + header.GetImageIDLength() + imageData._imageData.Length;
                        //if (checkBox_Footer.Checked) newLength = newLength + footer._footer.Length;
                        byte[] newTGA = new byte[newLength];

                        header._header.CopyTo(newTGA, 0);
                        int offset = header._header.Length;

                        imageDescription._imageDescription.CopyTo(newTGA, offset);
                        offset = offset + imageDescription._imageDescription.Length;

                        imageData._imageData.CopyTo(newTGA, offset);
                        offset = offset + imageData._imageData.Length;

                        //if (checkBox_Footer.Checked) footer._footer.CopyTo(newTGA, offset);

                        if (newTGA != null && newTGA.Length > 0)
                        {
                            string newFileName = Path.Combine(path, fileName + ".png");

                            using (var fileStreamTGA = File.OpenWrite(newFileName))
                            {
                                fileStreamTGA.Write(newTGA, 0, newTGA.Length);
                                fileStreamTGA.Flush();
                            }
                        }
                    }

                    try
                    {
                        File.Delete(fileNameFull);
                    }
                    catch (Exception)
                    {
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageFix_Error + Environment.NewLine + exp,
                        Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
