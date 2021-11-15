using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    /// <summary>Цифровое время(часы)</summary>
    //public class hmUI_widget_IMG_TIME_hour
    //{
    //    / <summary>Позиция в наборе элементов</summary>
    //    public int position = -1;

    //    / <summary>Видимость элемента</summary>
    //    public bool visible = false;

    //    / <summary>X координата часов</summary>
    //    public int hour_startX { get; set; }

    //    / <summary>Y координата часов</summary>
    //    public int hour_startY { get; set; }

    //    / <summary>Отступы</summary>
    //    public int hour_space { get; set; }

    //    / <summary>Отображение ведущих нулей. 0-не отображать; 1-отображать</summary>
    //    public int hour_zero { get; set; }

    //    / <summary>Выравнивание</summary>
    //    public string hour_align { get; set; }

    //    / <summary>Набор картинок для цифр</summary>
    //    public List<string> hour_array { get; set; }

    //    / <summary>Разделитель</summary>
    //    public string hour_unit { get; set; }

    //    / <summary>Основной экран или AOD</summary>
    //    public string show_level { get; set; }
    //}

    /// <summary>Отображение am/pm</summary>
    public class hmUI_widget_IMG_TIME_am_pm
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата am</summary>
        public string am_x { get; set; }

        /// <summary>Y координата am</summary>
        public string am_y { get; set; }

        /// <summary>картинка am</summary>
        public string am_img { get; set; }


        /// <summary>X координата am/pm</summary>
        public string pm_x { get; set; }

        /// <summary>Y координата am/pm</summary>
        public string pm_y { get; set; }

        /// <summary>картинка pm</summary>
        public string pm_img { get; set; }


        /// <summary>Основной экран или AOD</summary>
        public string show_level { get; set; }
    }


    /// <summary>Цифровое время</summary>
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
        public string align { get; set; }

        /// <summary>Набор картинок для цифр</summary>
        public string img_First { get; set; }

        /// <summary>Разделитель / единици измерения</summary>
        public string unit { get; set; }

        /// <summary>Иконка / разделитель</summary>
        public string icon { get; set; }

        /// <summary>X координата иконки</summary>
        public int iconPosX { get; set; }

        /// <summary>Y координата иконки</summary>
        public int iconPosY { get; set; }

        ///// <summary>Следовать за часами. 0-не следовать; 1-следовать</summary>
        /// <summary>Следовать за предшественником</summary>
        public bool follow { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level { get; set; }
    }


    /// <summary>Цифровое время (секунды)</summary>
    //public class hmUI_widget_IMG_TIME_second
    //{
    //    /// <summary>Позиция в наборе элементов</summary>
    //    public int position = -1;

    //    /// <summary>Видимость элемента</summary>
    //    public bool visible = false;

    //    /// <summary>X координата секунд</summary>
    //    public int second_startX { get; set; }

    //    /// <summary>Y координата секунд</summary>
    //    public int second_startY { get; set; }

    //    /// <summary>Отступы</summary>
    //    public int second_space { get; set; }

    //    /// <summary>Отображение ведущих нулей. 0-не отображать; 1-отображать</summary>
    //    public int second_zero { get; set; }

    //    /// <summary>Выравнивание</summary>
    //    public string second_align { get; set; }

    //    /// <summary>Набор картинок для цифр</summary>
    //    public List<string> second_array { get; set; }

    //    /// <summary>Разделитель</summary>
    //    public string second_unit { get; set; }

    //    /// <summary>Следовать за минутами. 0-не следовать; 1-следовать</summary>
    //    public int second_follow { get; set; }

    //    /// <summary>Основной экран или AOD</summary>
    //    public string show_level { get; set; }
    //}


    /// <summary>Цифровое время шрифтом (часы)</summary>
    //public class hmUI_widget_TEXT_TIME_hour
    //{
    //    /// <summary>Позиция в наборе элементов</summary>
    //    public int position = -1;

    //    /// <summary>Видимость элемента</summary>
    //    public bool visible = false;

    //    /// <summary>X координата секунд</summary>
    //    public int hour_startX { get; set; }

    //    /// <summary>Y координата секунд</summary>
    //    public int hour_startY { get; set; }

    //    /// <summary>Отступы</summary>
    //    public int hour_space { get; set; }

    //    /// <summary>Отображение ведущих нулей. 0-не отображать; 1-отображать</summary>
    //    public int hour_zero { get; set; }

    //    /// <summary>Выравнивание</summary>
    //    public string hour_align { get; set; }

    //    /// <summary>Цвет шрифта</summary>
    //    public string hour_color { get; set; }

    //    /// <summary>Размер шрифта</summary>
    //    public int hour_font_size { get; set; }

    //    /// <summary>Следовать за минутами. 0-не следовать; 1-следовать</summary>
    //    public int hour_follow { get; set; }

    //    /// <summary>Основной экран или AOD</summary>
    //    public string show_level { get; set; }
    //}

    /// <summary>Цифровое время шрифтом (минуты)</summary>
    public class hmUI_widget_TEXT
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата секунд</summary>
        public int minute_startX { get; set; }

        /// <summary>Y координата секунд</summary>
        public int minute_startY { get; set; }

        /// <summary>Отступы</summary>
        public int minute_space { get; set; }

        /// <summary>Отображение ведущих нулей. 0-не отображать; 1-отображать</summary>
        public int minute_zero { get; set; }

        /// <summary>Выравнивание</summary>
        public string minute_align { get; set; }

        /// <summary>Цвет шрифта</summary>
        public string minute_color { get; set; }

        /// <summary>Размер шрифта</summary>
        public int minute_font_size { get; set; }

        /// <summary>Следовать за минутами. 0-не следовать; 1-следовать</summary>
        public int minute_follow { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level { get; set; }
    }

    /// <summary>Цифровое время шрифтом (секунды)</summary>
    //public class hmUI_widget_TEXT_TIME_second
    //{
    //    /// <summary>Позиция в наборе элементов</summary>
    //    public int position = -1;

    //    /// <summary>Видимость элемента</summary>
    //    public bool visible = false;

    //    /// <summary>X координата секунд</summary>
    //    public int second_startX { get; set; }

    //    /// <summary>Y координата секунд</summary>
    //    public int second_startY { get; set; }

    //    /// <summary>Отступы</summary>
    //    public int second_space { get; set; }

    //    /// <summary>Отображение ведущих нулей. 0-не отображать; 1-отображать</summary>
    //    public int second_zero { get; set; }

    //    /// <summary>Выравнивание</summary>
    //    public string second_align { get; set; }

    //    /// <summary>Цвет шрифта</summary>
    //    public string second_color { get; set; }

    //    /// <summary>Размер шрифта</summary>
    //    public int second_font_size { get; set; }

    //    /// <summary>Следовать за минутами. 0-не следовать; 1-следовать</summary>
    //    public int second_follow { get; set; }

    //    /// <summary>Основной экран или AOD</summary>
    //    public string show_level { get; set; }
    //}
}
