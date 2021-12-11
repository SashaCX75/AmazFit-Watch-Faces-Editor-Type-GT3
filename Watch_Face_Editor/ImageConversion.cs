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
        /// <summary>Преобразуем Tga в Png</summary>
        private string TgaToPng(string file, string targetFile)
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

                        Header header = new Header(_streamBuffer);
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
                    image.Composite(Red, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Blue);
                    image.Composite(Blue, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Red);

                    image.Write(targetFile);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine +
                        exp);
                }
            }
            return path;
        }

        /// <summary>Преобразуем Png в Tga</summary>
        private string PngToTga(string fileNameFull, string targetFolder)
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
                        image_temp = new ImageMagick.MagickImage(bitmapNew);
                    }
                    ImageMagick.Pixel pixel = image.GetPixels().GetPixel(0, 0);
                    //pixel = new ImageMagick.Pixel(0, 0, 4);
                    bool transparent = false;
                    //if (pixel.Channels == 4 && pixel[3] < 256) transparent = true;

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
                        pixel = image.GetPixels().GetPixel(0, 0);
                        image.ColorType = ImageMagick.ColorType.Palette;
                        pixel = image.GetPixels().GetPixel(0, 0);
                        if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                        {
                            MessageBox.Show("Изображение не должно быть монохромным и должно быть в формате 32bit" +
                                Environment.NewLine + fileNameFull);
                            return null;
                        }
                    }


                    //image.Format = ImageMagick.MagickFormat.Tga;
                    //List<string> colorMapList = new List<string>();
                    for (int i = 0; i < image.ColormapSize; i++)
                    {
                        colorMapList.Add(image.GetColormap(i));
                    }
                    if (transparent && colorMapList.Count == 2)
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
