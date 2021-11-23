using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    class Header
    {
        byte ImageIDLength = 5;
        const byte HeaderSize = 18;
        public byte[] _header;
        int ColorMapCount;
        byte ColorMapEntrySize;
        public int Width { get; }
        public int Height { get; }

        public Header(byte[] streamBuffer)
        {
            _header = new byte[HeaderSize];
            Array.Copy(streamBuffer, 0, _header, 0, HeaderSize);
            ImageIDLength = _header[0];
            ColorMapCount = BitConverter.ToUInt16(_header, 5);
            ColorMapEntrySize = _header[7];
            Width = BitConverter.ToUInt16(_header, 12);
            Height = BitConverter.ToUInt16(_header, 14);

            if (_header[1] != 1 || _header[2] != 1) throw new Exception("Не верный формат файла");
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
