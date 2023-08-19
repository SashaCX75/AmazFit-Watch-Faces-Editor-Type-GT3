using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    class Header
    {
        byte ImageIDLength = 5;
        const byte HeaderSize = 18;
        public byte[] _header;
        int ColorMapCount;
        byte ColorMapEntrySize;
        byte ExistsColorMap;
        byte ImageType;
        public int Width { get; }
        public int Height { get; }

        public Header(byte[] streamBuffer, string fileNameFull = "", string targetFileName = "")
        {
            _header = new byte[HeaderSize];
            Array.Copy(streamBuffer, 0, _header, 0, HeaderSize);
            ImageIDLength = _header[0];
            ExistsColorMap = _header[1];
            ImageType = _header[2];
            ColorMapCount = BitConverter.ToUInt16(_header, 5);
            ColorMapEntrySize = _header[7];
            Width = BitConverter.ToUInt16(_header, 12);
            Height = BitConverter.ToUInt16(_header, 14);

            if (!(_header[1] == 0 && _header[2] == 2) && !(_header[1] == 1 && _header[2] == 1)) 
            {
                if (MessageBox.Show(Properties.FormStrings.Img_Convert_Error_ReadErr + " \"" + Path.GetFileName(fileNameFull) + "\"." + Environment.NewLine +
                                Properties.FormStrings.Img_Convert_Error_SaveImg, Properties.FormStrings.Message_Error_Caption,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                { 
                    if((_header[1] == (int)'P' && _header[2] == (int)'N' && _header[3] == (int)'G') || 
                        (_header[1] == (int)'p' && _header[2] == (int)'n' && _header[3] == (int)'g'))
                    {
                        if(File.Exists(fileNameFull)) 
                        {
                            File.Copy(fileNameFull, targetFileName, true);
                        }
                    }
                }
                else 
                { 
                    throw new Exception(Properties.FormStrings.Img_Convert_Error_NoPalette); 
                }
            }
        }

        public byte GetImageIDLength()
        {
            return ImageIDLength;
        }

        public void SetImageIDLength(byte value)
        {
            ImageIDLength = value;
            _header[0] = value;
        }

        public int GetColorMapCount()
        {
            return ColorMapCount;
        }

        public int GetExistsColorMap()
        {
            return ExistsColorMap;
        }
        public int GetImageType()
        {
            return ImageType;
        }

        public void SetColorMapCount(int value)
        {
            ColorMapCount = value;
            Array.Copy(IntToByte(value), 0, _header, 5, 2);
        }

        private byte[] IntToByte(int value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            return intBytes;
        }

        public byte GetColorMapEntrySize()
        {
            return ColorMapEntrySize;
        }

        public void SetColorMapEntrySize(byte value)
        {
            ColorMapEntrySize = value;
            _header[7] = value;
        }

    }
}
