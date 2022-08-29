using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    class ColorMap
    {
        public int ColorMapCount;
        private byte ColorMapEntrySize;
        public byte[] _colorMap;
        List<Color> Colors = new List<Color>();

        /// <summary>Формируем карту цветов</summary>
        /// <param name="streamBuffer">Набор байт файла</param>
        /// <param name="colorMapCount">Количество цветов в карте</param>
        /// <param name="colorMapEntrySize">Битность цвета (24, 32)</param>
        /// <param name="offset">Смещение от начала файла до карты цветов</param>
        public ColorMap(byte[] streamBuffer, int colorMapCount, byte colorMapEntrySize, int offset)
        {
            ColorMapCount = colorMapCount;
            ColorMapEntrySize = colorMapEntrySize;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            Array.Copy(streamBuffer, offset, _colorMap, 0, lenght);

            ArrayToColors();
        }

        private void ArrayToColors()
        {
            Colors.Clear();
            if (ColorMapEntrySize == 24)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    if (i * 3 + 2 < _colorMap.Length)
                    {
                        Colors.Add(Color.FromArgb(_colorMap[i * 3], _colorMap[i * 3 + 1], _colorMap[i * 3 + 2]));
                    }
                }
            }
            if (ColorMapEntrySize == 32)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    if (i * 4 + 3 < _colorMap.Length)
                    {
                        Colors.Add(Color.FromArgb(_colorMap[i * 4], _colorMap[i * 4 + 1], _colorMap[i * 4 + 2], _colorMap[i * 4 + 3]));
                    }
                }
            }
        }

        private void ARGB_BGRA(string model)
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                byte A = Colors[i].A;
                byte R = Colors[i].R;
                byte G = Colors[i].G;
                byte B = Colors[i].B;
                float scale = A / 255f;
                R = (byte)(R * scale);
                G = (byte)(G * scale);
                B = (byte)(B * scale);
                if (model == "Amazfit Band 7") Colors[i] = Color.FromArgb(B, G, R, A);
                else Colors[i] = Color.FromArgb(R, G, B, A);
            }
        }

        public void ColorsFix(bool argb_brga, int colorMapCount, byte colorMapEntrySize, string model)
        {
            if (argb_brga) ARGB_BGRA(model);

            ColorMapCount = colorMapCount;
            ColorMapEntrySize = colorMapEntrySize;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            for (int i = 0; i < ColorMapCount; i++)
            {
                Color color = Color.FromArgb(0, 0, 0, 0);
                if (i < Colors.Count) color = Colors[i];
                _colorMap[i * bit] = color.A;
                _colorMap[i * bit + 1] = color.R;
                _colorMap[i * bit + 2] = color.G;
                _colorMap[i * bit + 3] = color.B;
            }
            ArrayToColors();
        }

        public void RestoreColor(List<Color> colorMapList)
        {
            ColorMapCount = colorMapList.Count;
            ColorMapEntrySize = 32;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            for (int i = 0; i < ColorMapCount; i++)
            {
                Color color = Color.FromArgb(0, 0, 0, 0);
                if (i < colorMapList.Count) color = colorMapList[i];
                _colorMap[i * bit] = color.A;
                _colorMap[i * bit + 1] = color.R;
                _colorMap[i * bit + 2] = color.G;
                _colorMap[i * bit + 3] = color.B;
            }
            ArrayToColors();
        }
        public void SetColorCount(int newCount)
        {
            ColorMapCount = newCount;
            int bit = ColorMapEntrySize / 8;
            int lenght = bit * ColorMapCount;
            _colorMap = new byte[lenght];
            if (ColorMapEntrySize == 24)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    Color color = Color.FromArgb(0, 0, 0, 0);
                    if (i < Colors.Count) color = Colors[i];
                    //_colorMap[i * bit] = color.A;
                    _colorMap[i * bit] = color.R;
                    _colorMap[i * bit + 1] = color.G;
                    _colorMap[i * bit + 2] = color.B;
                }
            }
            if (ColorMapEntrySize == 32)
            {
                for (int i = 0; i < ColorMapCount; i++)
                {
                    Color color = Color.FromArgb(0, 0, 0, 0);
                    if (i < Colors.Count) color = Colors[i];
                    _colorMap[i * bit] = color.A;
                    _colorMap[i * bit + 1] = color.R;
                    _colorMap[i * bit + 2] = color.G;
                    _colorMap[i * bit + 3] = color.B;
                }
            }
            ArrayToColors();
        }
    }
}
