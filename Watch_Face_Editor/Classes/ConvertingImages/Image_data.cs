using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    class Image_data
    {
        public byte[] _imageData;

        /// <summary>Запоминаем оставшуюся часть файла, после карты цветов</summary>
        /// <param name="streamBuffer">Набор байт файла</param>
        /// <param name="offset">Смещение от начала файла до конца карты цветов</param>
        public Image_data(byte[] streamBuffer, int offset)
        {
            int lenght = streamBuffer.Length - offset;
            _imageData = new byte[lenght];
            Array.Copy(streamBuffer, offset, _imageData, 0, lenght);
        }
    }
}
