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

    /// <summary>Стрелочный указатель</summary>
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

    /// <summary>Редактируемые стрелки часов</summary>
    public class EDITABLE_POINTER
    {

        /// <summary>Изображение указателя (стрелки)</summary>
        public string path { get; set; }

        /// <summary>Координата Х центра вращения на циферблате</summary>
        public int centerX { get; set; }

        /// <summary>Координата Y центра вращения на циферблате</summary>
        public int centerY { get; set; }

        /// <summary>Координата Х центра вращения на указателе (стрелке)</summary>
        public int posX { get; set; }

        /// <summary>Координата Y центра вращения на указателе (стрелке)</summary>
        public int posY { get; set; }

    }

    /// <summary>Редактируемые элементы</summary>
    public class WATCHFACE_EDIT_GROUP
    {
        /// <summary>Выбраный элемент</summary>
        public int selected_element = -1;

        /// <summary>Координаты элемента</summary>
        public int x = 0;

        /// <summary>Координаты элемента</summary>
        public int y = 0;

        /// <summary>Размер элемента</summary>
        public int h = 0;

        /// <summary>Размер элемента</summary>
        public int w = 0;

        /// <summary>Перечень элементов</summary>
        public List<Optional_Types_List> optional_types_list { get; set; }

        /// <summary>Набор элементов</summary>
        public List<Object> Elements { get; set; }

        /// <summary>Фон пояснительной надписи</summary>
        public string tips_BG { get; set; }

        /// <summary>Координаты пояснительной надписи</summary>
        public int tips_x { get; set; }

        /// <summary>Координаты пояснительной надписи</summary>
        public int tips_y { get; set; }

        /// <summary>Ширина пояснительной надписи</summary>
        public int tips_width { get; set; }

        /// <summary>Отступы для пояснительной надписи</summary>
        public int tips_margin { get; set; }

        /// <summary>Рамка невыделенного элемента</summary>
        public string un_select_image { get; set; }

        /// <summary>Рамка выделенного элемента</summary>
        public string select_image { get; set; }

    }

    /// <summary>Набор параметров для настраиваемого фона</summary>
    public class BackgroundList
    {
        public string path { get; set; }
        public string preview { get; set; }
    }

    /// <summary>Набор стрелок</summary>
    public class PointersList
    {
        public EDITABLE_POINTER hour { get; set; }
        public EDITABLE_POINTER minute { get; set; }
        public EDITABLE_POINTER second { get; set; }
        public string preview { get; set; }
    }

    /// <summary>Набор параметров для настраиваемого фона</summary>
    public class Optional_Types_List
    {
        public string type { get; set; }
        public string preview { get; set; }
    }

    /// <summary>Набор независимых картинок</summary>
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

    /// <summary>Круговая шкала</summary>
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

    /// <summary>Линейная шкала</summary>
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

    /// <summary>Набор покадровой анимация</summary>
    public class hmUI_widget_IMG_ANIM_List : ICloneable
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;
        /// <summary>Выбраная анимация</summary>
        public int selected_animation = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        public List<hmUI_widget_IMG_ANIM> Frame_Animation { get; set; }

        public object Clone()
        {
            List<hmUI_widget_IMG_ANIM> Frame_Animation = null;
            if (this.Frame_Animation != null)
            {
                foreach (hmUI_widget_IMG_ANIM frame_fnimation in this.Frame_Animation)
                {
                    Frame_Animation.Add((hmUI_widget_IMG_ANIM)frame_fnimation.Clone());
                }
            }

            return new hmUI_widget_IMG_ANIM_List
            {
                position = this.position,
                selected_animation = this.selected_animation,
                visible = this.visible,

                Frame_Animation = Frame_Animation,
            };
        }
    }

    /// <summary>Покадровая анимация</summary>
    public class hmUI_widget_IMG_ANIM : ICloneable
    {
        /*/// <summary>Позиция в наборе элементов</summary>
        public int position = -1;*/

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public int x { get; set; }
        public int y { get; set; }

        ///// <summary>Путь к изображениям</summary>
        //public string anim_path { get; set; }

        /// <summary>Имя изображения</summary>
        public string anim_src { get; set; }

        /// <summary>Префикс изображений</summary>
        public string anim_prefix { get; set; }

        ///// <summary>Расширение изображений</summary>
        //public string anim_ext { get; set; }

        /// <summary>Количество кадров в секунду</summary>
        public int anim_fps { get; set; } = 15;

        ///// <summary>Количество повторений</summary>
        //public int repeat_count { get; set; }

        /// <summary>Повторять анимацию</summary>
        public bool anim_repeat { get; set; } = true;

        /// <summary>Номер кадра отображаемого на предпросмотре</summary>
        public int preview_frame { get; set; } = 1;

        /// <summary>Количество изображений для анимации</summary>
        public int anim_size { get; set; } = 1;

        ///// <summary>Статус анимации</summary>
        //public string anim_status { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

        public object Clone()
        {
            return new hmUI_widget_IMG_ANIM
            {
                //position = this.position,
                visible = this.visible,

                x = this.x,
                y = this.y,

                //anim_path = this.anim_path,
                anim_src = this.anim_src,
                anim_prefix = this.anim_prefix,
                //anim_path = this.anim_path,
                anim_fps = this.anim_fps,
                //repeat_count = this.repeat_count,
                anim_repeat = this.anim_repeat,
                //display_on_restart = this.display_on_restart,
                anim_size = this.anim_size,
            };
        }
}

    /// <summary>Набор анимации движения</summary>
    public class Motion_Animation_List : ICloneable
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;
        /// <summary>Выбраная анимация</summary>
        public int selected_animation = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        public List<Motion_Animation> Motion_Animation { get; set; }

        public object Clone()
        {
            List<Motion_Animation> Motion_Animation = null;
            if (this.Motion_Animation != null)
            {
                foreach (Motion_Animation motion_animation in this.Motion_Animation)
                {
                    Motion_Animation.Add((Motion_Animation)motion_animation.Clone());
                }
            }

            return new Motion_Animation_List
            {
                position = this.position,
                selected_animation = this.selected_animation,
                visible = this.visible,

                Motion_Animation = Motion_Animation,
            };
        }
    }

    /// <summary>Анимация движения</summary>
    public class Motion_Animation : ICloneable
        {
            /*/// <summary>Позиция в наборе элементов</summary>
            public int position = -1;*/

            /// <summary>Видимость элемента</summary>
            public bool visible = true;

            /// <summary>Стартовая координата X</summary>
            public int x_start { get; set; }

            /// <summary>Стартовая координата Y</summary>
            public int y_start { get; set; }

            /// <summary>Конечная координата X</summary>
            public int x_end { get; set; }

            /// <summary>Конечная координата Y</summary>
            public int y_end { get; set; }


            /// <summary>Файл изображения</summary>
            public string src { get; set; }

            /// <summary>Количество кадров в секунду</summary>
            public int anim_fps { get; set; } = 15;

            /// <summary>Длительность цикла анимации, мс</summary>
            public int anim_duration { get; set; } = 10000;

            /// <summary>Количество повторений</summary>
            public int repeat_count { get; set; }

            ///// <summary>Повторять анимацию</summary>
            //public bool anim_repeat { get; set; }

            /// <summary>Движение в обе стороны</summary>
            public bool anim_two_sides { get; set; } = true;

            /// <summary>Показывать положение элемента в начальной позиции</summary>
            public bool show_in_start { get; set; } = true;

        ///// <summary>Статус анимации</summary>
        //public string anim_status { get; set; }

        ///// <summary>Ключь анимации</summary>
        //public string anim_key { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

        public object Clone()
            {
                return new Motion_Animation
                {
                    //position = this.position,
                    visible = this.visible,

                    x_start = this.x_start,
                    y_start = this.y_start,
                    x_end = this.x_end,
                    y_end = this.y_end,

                    src = this.src,
                    anim_fps = this.anim_fps,
                    anim_duration = this.anim_duration,
                    repeat_count = this.repeat_count,
                    //anim_repeat = this.anim_repeat,
                    anim_two_sides = this.anim_two_sides,
                    show_in_start = this.show_in_start,
                };
            }
    }

/// <summary>Набор анимации вращения</summary>
    public class Rotate_Animation_List : ICloneable
    {
        /// <summary>Позиция в наборе элементов</summary>
        public int position = -1;
        /// <summary>Выбраная анимация</summary>
        public int selected_animation = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;

        public List<Rotate_Animation> Rotate_Animation { get; set; }

        public object Clone()
        {
            List<Rotate_Animation> Rotate_Animation = null;
            if (this.Rotate_Animation != null)
            {
                foreach (Rotate_Animation rotate_animation in this.Rotate_Animation)
                {
                    Rotate_Animation.Add((Rotate_Animation)rotate_animation.Clone());
                }
            }

            return new Rotate_Animation_List
            {
                position = this.position,
                selected_animation = this.selected_animation,
                visible = this.visible,

                Rotate_Animation = Rotate_Animation,
            };
        }
    }

    /// <summary>Анимация вращения</summary>
    public class Rotate_Animation : ICloneable
        {
            /*/// <summary>Позиция в наборе элементов</summary>
            public int position = -1;*/

            /// <summary>Видимость элемента</summary>
            public bool visible = true;

            /// <summary>Файл изображения</summary>
            public string src { get; set; }

            /// <summary>Координата Х центра вращения на циферблате</summary>
            public int center_x { get; set; }

            /// <summary>Координата Y центра вращения на циферблате</summary>
            public int center_y { get; set; }

            /// <summary>Координата Х начального положения изображения</summary>
            public int pos_x { get; set; }

            /// <summary>Координата Y начального положения изображения</summary>
            public int pos_y { get; set; }

            /// <summary>Начальный угол</summary>
            public int start_angle { get; set; }

            /// <summary>Конечный угол</summary>
            public int end_angle { get; set; } = 360;

            /// <summary>Количество кадров в секунду</summary>
            public int anim_fps { get; set; } = 15;

            /// <summary>Длительность цикла анимации, мс</summary>
            public int anim_duration { get; set; } = 10000;

            /// <summary>Количество повторений</summary>
            public int repeat_count { get; set; }

            ///// <summary>Повторять анимацию</summary>
            //public bool anim_repeat { get; set; }

            /// <summary>Движение в обе стороны</summary>
            public bool anim_two_sides { get; set; }

            /// <summary>Показывать положение элемента в начальной позиции</summary>
            public bool show_in_start { get; set; } = true;

        ///// <summary>Статус анимации</summary>
        //public string anim_status { get; set; }

        ///// <summary>Ключь анимации</summary>
        //public string anim_key { get; set; }

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

        public object Clone()
            {
                return new Rotate_Animation
                {
                    //position = this.position,
                    visible = this.visible,

                    center_x = this.center_x,
                    center_y = this.center_y,
                    pos_x = this.pos_x,
                    pos_y = this.pos_y,

                    src = this.src,
                    start_angle = this.start_angle,
                    end_angle = this.end_angle,
                    anim_fps = this.anim_fps,
                    anim_duration = this.anim_duration,
                    repeat_count = this.repeat_count,
                    //anim_repeat = this.anim_repeat,
                    anim_two_sides = this.anim_two_sides,
                    show_in_start = this.show_in_start,
                };
            }
        }

    /// <summary>Оповещение при изменении состояния связи</summary>
    public class BluetoothStateAlert
    {
        /// <summary>Вибрация при изменении статуса</summary>
        public bool vibrate = false;
        /// <summary>Тип вибрации</summary>
        public int vibrateType = 9;
        /// <summary>Оповещение при изменении статуса</summary>
        public bool toastShow = false;
        /// <summary>Текст оповещения</summary>
        public string toastText = "";
    }

}
