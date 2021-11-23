using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    class ImageDescription
    {
        public byte[] _imageDescription;
        public ImageDescription(byte[] streamBuffer, byte lenght)
        {
            _imageDescription = new byte[lenght];
            Array.Copy(streamBuffer, 18, _imageDescription, 0, lenght);
        }

        public void SetSize(int newSize, int Width)
        {
            Array.Resize(ref _imageDescription, newSize);
            if (newSize > 6)
            {
                _imageDescription[0] = 0x53;
                _imageDescription[1] = 0x4F;
                _imageDescription[2] = 0x4D;
                _imageDescription[3] = 0x48;
                Array.Copy(IntToByte(Width), 0, _imageDescription, 4, 2);
            }
        }

        private byte[] IntToByte(int value)
        {
            byte[] intBytes = BitConverter.GetBytes(value);
            return intBytes;
        }

        public int GetRealWidth()
        {
            if (_imageDescription.Length < 6) return -1;
            int Width = BitConverter.ToUInt16(_imageDescription, 4);
            return Width;
        }
    }
}
