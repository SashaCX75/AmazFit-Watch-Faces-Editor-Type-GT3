using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    /// <summary>Фоновое изображение или иконка</summary>
    public class hmUI_widget_IMG
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        public int x { get; set; }
        public int y { get; set; }

        /// <summary>Ширина</summary>
        public int w { get; set; }

        /// <summary>Высота</summary>
        public int h { get; set; }

        /// <summary>Изображение</summary>
        public string src { get; set; }

        /// <summary>Основной экран или AOD</summary>
        //public string show_level { get; set; }
    }

    /// <summary>Заливка фона цветом</summary>
    public class hmUI_widget_FILL_RECT
    {
        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        ///// <summary>Видимость элемента</summary>
        //public bool visible = false;

        public int x { get; set; }
        public int y { get; set; }

        /// <summary>Ширина</summary>
        public int w { get; set; }

        /// <summary>Высота</summary>
        public int h { get; set; }

        /// <summary>Цвет</summary>
        public string color { get; set; }

        /// <summary>Основной экран или AOD</summary>
        //public string show_level { get; set; }
    }



}
