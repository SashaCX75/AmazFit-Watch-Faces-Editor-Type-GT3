using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{

    /// <summary>Отображение am/pm</summary>
    public class hmUI_widget_IMG_TIME_am_pm
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата am</summary>
        public int am_x { get; set; }

        /// <summary>Y координата am</summary>
        public int am_y { get; set; }

        /// <summary>картинка am</summary>
        public string am_img { get; set; }


        /// <summary>X координата am/pm</summary>
        public int pm_x { get; set; }

        /// <summary>Y координата am/pm</summary>
        public int pm_y { get; set; }

        /// <summary>картинка pm</summary>
        public string pm_img { get; set; }


        /// <summary>Основной экран или AOD</summary>
        //public string show_level { get; set; }
    }

    /// <summary>Данные цифрами</summary>
    public class hmUI_widget_IMG_NUMBER
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата</summary>
        public int imageX { get; set; }

        /// <summary>Y координата</summary>
        public int imageY { get; set; }

        /// <summary>Отступы</summary>
        public int space { get; set; }

        ///// <summary>Отображение ведущих нулей. 0-не отображать; 1-отображать</summary>        
        /// <summary>Отображение ведущих нулей</summary>
        public bool zero { get; set; }

        /// <summary>Выравнивание</summary>
        public string align = "LEFT";

        /// <summary>Первая картинка из набора для отображения</summary>
        public string img_First { get; set; }

        /// <summary>Разделитель / единици измерения</summary>
        public string unit { get; set; }

        /// <summary>Иконка / разделитель</summary>
        public string icon { get; set; }

        /// <summary>X координата иконки</summary>
        public int iconPosX { get; set; }

        /// <summary>Y координата иконки</summary>
        public int iconPosY { get; set; }

        /// <summary>Высота иконки</summary>
        //public int iconHeight { get; set; }

        /// <summary>Ширина иконки</summary>
        //public int iconWidth { get; set; }

        ///// <summary>Следовать за часами. 0-не следовать; 1-следовать</summary>
        /// <summary>Следовать за предшественником</summary>
        public bool follow { get; set; }

        /// <summary>Основной экран или AOD</summary>
        //public string show_level { get; set; }

        /// <summary>Тип активности</summary>
        public string type { get; set; }
    }

    /// <summary>Данные набором изображения (отображается только одно изображение</summary>
    public class hmUI_widget_IMG_LEVEL
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата</summary>
        public int X { get; set; }

        /// <summary>Y координата</summary>
        public int Y { get; set; }

        /// <summary>Первая картинка из набора для отображения</summary>
        public string img_First { get; set; }

        /// <summary>Количество картинок в наборе</summary>
        public int image_length { get; set; }

        /// <summary>Тип активности</summary>
        public string type { get; set; }
    }

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
        public int? w { get; set; }

        /// <summary>Высота</summary>
        public int? h { get; set; }

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

    public class hmUI_widget_IMG_POINTER
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>Изображение указателя (стрелки)</summary>
        public string src { get; set; }

        /// <summary>Координата Х центра вращения на циферблате</summary>
        public int center_x { get; set; }

        /// <summary>Координата Y центра вращения на циферблате</summary>
        public int center_y { get; set; }

        /// <summary>Координата Х центра вращения на указателе (стрелке)</summary>
        public int pos_x { get; set; }

        /// <summary>Координата Y центра вращения на указателе (стрелке)</summary>
        public int pos_y { get; set; }

        /// <summary>Начальный угол</summary>
        public int start_angle { get; set; }

        /// <summary>Конечный угол</summary>
        public int end_angle = 360;

        /// <summary>Центральное изображение</summary>
        public string cover_path { get; set; }

        /// <summary>Координата Х центрального изображения</summary>
        public int cover_x { get; set; }

        /// <summary>Координата Y центрального изображения</summary>
        public int cover_y { get; set; }

        /// <summary>Фоновое изображение изображение</summary>
        public string scale { get; set; }

        /// <summary>Координата Х фонового изображения</summary>
        public int scale_x { get; set; }

        /// <summary>Координата Y фонового изображения</summary>
        public int scale_y { get; set; }

        /// <summary>Тип активности</summary>
        public string type { get; set; }

    }

}
