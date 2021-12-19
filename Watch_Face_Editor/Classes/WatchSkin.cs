using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    public class WatchSkin
    {
        /// <summary>Изображение заднего фона</summary>
        public WatchSkinBackground Background { get; set; }

        /// <summary>Изображение циферблата</summary>
        public WatchSkinImage Image { get; set; }

        /// <summary>Картинка поверх изображения</summary>
        public WatchSkinForeground Foreground { get; set; }
    }

    public class WatchSkinBackground
    {
        /// <summary>Путь к файлу изображения</summary>
        public string Path { get; set; }

        /// <summary>Высота изображения</summary>
        public int? ImageHeight { get; set; }
    }

    public class WatchSkinImage
    {
        /// <summary>Высота изображения</summary>
        public int? ImageHeight { get; set; }

        /// <summary>Координаты изображения</summary>
        public PositionForeground Position { get; set; }
    }

    public class WatchSkinForeground
    {
        /// <summary>Путь к файлу изображения</summary>
        public string Path { get; set; }

        /// <summary>Высота изображения</summary>
        public int? ImageHeight { get; set; }

        /// <summary>Координаты изображения</summary>
        public PositionForeground Position { get; set; }
    }

    public class PositionForeground
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
