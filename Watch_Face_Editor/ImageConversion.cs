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
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine +
                        exp);
                }
            }
            return path;
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
                    if (exp.Message != "Отсутствует карта цветов")
                    {
                        MessageBox.Show("Не верный формат исходного файла." + Environment.NewLine +
                                        "Файл \"" + Path.GetFileName(targetFile) + "\" не был сохранен." +
                                        Environment.NewLine + exp);
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
                            IMagickColor<ushort>[,] colorMap = new IMagickColor<ushort>[16, 16];
                            int index = 0;
                            for (int x = 0; x < 16; x++)
                            {
                                for (int y = 0; y < 16; y++)
                                {
                                    // TODO :: Kartun, check x2
                                    // colorMap[x, y] = image.GetColormap(index);
                                    colorMap[x, y] = image.GetColormapColor(index);
                                    IMagickColor<ushort> colorMap1 = image.GetColormapColor(index);
                                    index++;
                                }
                            }
                            index = 0;
                            for (int x = 0; x < 16; x++)
                            {
                                for (int y = 0; y < 16; y++)
                                {
                                    // TODO :: Kartun, check
                                    // image.SetColormap(index, colorMap[y, x]);
                                    image.SetColormapColor(index, colorMap[y, x]);
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
                                    // colorMap[x, y] = image.GetColormap(index);
                                    colorMap[x, y] = image.GetColormapColor(index);
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
                    if (exp.Message != "Отсутствует карта цветов")
                    {
                        MessageBox.Show("Не верный формат исходного файла." + Environment.NewLine +
                                        "Файл \"" + Path.GetFileName(targetFile) + "\" не был сохранен." +
                                        Environment.NewLine + exp);
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
                    // TODO :: Kartun, check
                    // ImageMagick.Pixel pixel = image.GetPixels().GetPixel(0, 0);
                    IPixel<ushort> pixel = image.GetPixels().GetPixel(0, 0);
                    
                    //pixel = new ImageMagick.Pixel(0, 0, 4);
                    bool transparent = false;
                    //if (pixel.Channels == 4 && pixel[3] < 256) transparent = true;

                    // пытаеммся преобразовать в формат 32bit но с картой цветов
                    image.ColorType = ImageMagick.ColorType.Palette;
                    if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                    {
                        image = image_temp;
                        ushort[] p;
                        if (pixel[2] > 256)
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256), pixel[3] };
                            else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256) };
                        }
                        else
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256), pixel[3] };
                            else
                            {
                                p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256) };
                                transparent = true;
                            }
                        }
                        image.GetPixels().SetPixel(0, 0, p);
                        // TODO :: Kartun, check
                        pixel = image.GetPixels().GetPixel(0, 0);
                        image.ColorType = ImageMagick.ColorType.Palette;
                        // TODO :: Kartun, check
                        pixel = image.GetPixels().GetPixel(0, 0);
                        if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                        {
                            MessageBox.Show("Изображение не должно быть монохромным и должно быть в формате 32bit" +
                                Environment.NewLine + fileNameFull);
                            return null;
                        }
                    }


                    for (int i = 0; i < image.ColormapSize; i++)
                    {
                        //colorMapList.Add(image.GetColormap(i));
                        // TODO :: Kartun, Confirm
                        colorMapList.Add(image.GetColormapColor(i).ToColor());
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
                    MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine +
                            exp);
                }
            }
            return null;
        }
    }
}
