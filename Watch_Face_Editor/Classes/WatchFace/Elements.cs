using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public string show_level = "";
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

        /// <summary>Разделитель / единици измерения (американские?)</summary>
        public string imperial_unit { get; set; }

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

        /// <summary>Символ "-"</summary>
        public string negative_image { get; set; }

        /// <summary>Символ ошибки</summary>
        public string invalid_image { get; set; }

        /// <summary>Десятичный разделитель</summary>
        public string dot_image { get; set; }

        ///// <summary>Следовать за часами. 0-не следовать; 1-следовать</summary>
        /// <summary>Следовать за предшественником</summary>
        public bool follow { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

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


        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";
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
        public string show_level = "";
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
        public string show_level = "";
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
        public int end_angle { get; set; } = 360;

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


        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

    }

    public class hmUI_widget_IMG_PROGRESS 
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата</summary>
        public List<int> X { get; set; }

        /// <summary>Y координата</summary>
        public List<int> Y { get; set; }

        /// <summary>Первая картинка из набора для отображения</summary>
        public string img_First { get; set; }

        /// <summary>Количество картинок в наборе</summary>
        public int image_length { get; set; }

        /// <summary>Тип активности</summary>
        public string type { get; set; }


        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";
    }

    public class Circle_Scale
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>Координата Х центра шкалы</summary>
        public int center_x { get; set; }

        /// <summary>Координата Y центра шкалы</summary>
        public int center_y { get; set; }

        /// <summary>Начальный угол</summary>
        public int start_angle { get; set; }

        /// <summary>Конечный угол</summary>
        public int end_angle = 180;

        /// <summary>Цвет</summary>
        public string color { get; set; } = "0xFFFF8C00";

        /// <summary>Радиус</summary>
        public int radius { get; set; } = 100;

        /// <summary>Толщина линии</summary>
        public int line_width { get; set; } = 5;

        /// <summary>Тип активности</summary>
        public string type { get; set; }

        /// <summary>Направление от центра</summary>
        public bool mirror { get; set; }

        /// <summary>Инверсия шкалы</summary>
        public bool inversion { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

    }

    public class Linear_Scale
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>Координата Х начала шкалы</summary>
        public int start_x { get; set; }

        /// <summary>Координата Y начала шкалы</summary>
        public int start_y { get; set; }

        /// <summary>Цвет</summary>
        public string color { get; set; } = "0xFFFF8C00";

        /// <summary>Указатель на шкале</summary>
        public string pointer { get; set; }

        /// <summary>Длина шкалы</summary>
        public int lenght { get; set; } = 100;

        /// <summary>Толщина линии</summary>
        public int line_width { get; set; } = 5;

        /// <summary>Тип активности</summary>
        public string type { get; set; }

        /// <summary>Направление от центра</summary>
        public bool mirror { get; set; }

        /// <summary>Инверсия шкалы</summary>
        public bool inversion { get; set; }

        /// <summary>Горизонтальная или вертикальная шкала</summary>
        public bool vertical { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";
    }

    /// <summary>Статусы</summary>
    public class hmUI_widget_IMG_STATUS
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        public int x { get; set; }
        public int y { get; set; }

        /// <summary>Изображение</summary>
        public string src { get; set; }

        /// <summary>Тип статуса</summary>
        public string type { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";
    }

    /// <summary>Ярлык</summary>
    public class hmUI_widget_IMG_CLICK
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        public int x { get; set; }
        public int y { get; set; }

        /// <summary>Ширина</summary>
        public int w { get; set; } = 100;

        /// <summary>Высота</summary>
        public int h { get; set; } = 100;

        /// <summary>Изображение</summary>
        public string src { get; set; }

        /// <summary>Тип ярлыка</summary>
        public string type { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";
    }

    /// <summary>Надпись системным шрифтом</summary>
    public class hmUI_widget_TEXT
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата</summary>
        public int x { get; set; }

        /// <summary>Y координата</summary>
        public int y { get; set; }

        /// <summary>Ширина</summary>
        public int w { get; set; } = 150;

        /// <summary>Высота</summary>
        public int h { get; set; } = 30;

        /// <summary>Цвет</summary>
        public string color { get; set; } = "0xFFFF8C00";

        /// <summary>Горизонтальное выравнивание</summary>
        public string align_h { get; set; } = "LEFT";

        /// <summary>Вертикальное выравнивание</summary>
        public string align_v { get; set; } = "TOP";

        /// <summary>Размер шрифта</summary>
        public int text_size { get; set; } = 20;

        /// <summary>Стиль переноса слов
        /// WRAP - перенос по словам
        /// CHAR_WRAP - перенос по символам
        /// ELLIPSIS - бегущая строка
        /// NONE - без переноса</summary>
        public string text_style { get; set; } = "ELLIPSIS";

        /// <summary>межстрочный интервал</summary>
        public int line_space { get; set; }

        /// <summary>Отступы</summary>
        public int char_space { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

        /// <summary>Тип активности</summary>
        public string type { get; set; }
    }

}
