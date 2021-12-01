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

    /// <summary>Цифровое время шрифтом</summary>
    public class hmUI_widget_TEXT
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        /// <summary>X координата секунд</summary>
        public int startX { get; set; }

        /// <summary>Y координата секунд</summary>
        public int startY { get; set; }

        /// <summary>Отступы</summary>
        public int space { get; set; }

        /// <summary>Отображение ведущих нулей. 0-не отображать; 1-отображать</summary>
        public int zero { get; set; }

        /// <summary>Выравнивание</summary>
        public string align { get; set; }

        /// <summary>Цвет шрифта</summary>
        public string color { get; set; }

        /// <summary>Размер шрифта</summary>
        public int font_size { get; set; }

        /// <summary>Следовать за минутами. 0-не следовать; 1-следовать</summary>
        public int follow { get; set; }

        /// <summary>Основной экран или AOD</summary>
        //public string show_level { get; set; }
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
