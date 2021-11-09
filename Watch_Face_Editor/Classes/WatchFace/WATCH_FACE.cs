using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    class WATCH_FACE
    {
        /// <summary>Модель часов и ID циферблата</summary>
        public WatchFace_Info WatchFace_Info { get; set; }

        /// <summary>Основной экран</summary>
        public ScreenNormal ScreenNormal { get; set; }

        /// <summary>AOD экран</summary>
        public ScreenAOD ScreenAOD { get; set; }

        
    }

    public class WatchFace_Info
    {
        /// <summary> Название модели часов</summary>
        public string DeviceName { get; set; }

        /// <summary>Id циферблата</summary>
        public int WatchFaceId { get; set; }

        /// <summary>Изображение предпросмотра</summary>
        public string Preview { get; set; }
    }

    public class ScreenNormal
    {
        /// <summary>Задний фон</summary>
        public Background Background { get; set; }

        /// <summary>Набор элементов</summary>
        public List<Object> Elements { get; set; }
    }

    public class ScreenAOD
    {
        /// <summary>Задний фон</summary>
        public Background Background { get; set; }

        /// <summary>Набор элементов</summary>
        public List<Object> Elements { get; set; }
    }

    public class Background
    {
        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        /// <summary>Изображение заднего фона</summary>
        public hmUI_widget_IMG BackgroundImage { get; set; }

        /// <summary>Цвет фона</summary>
        public hmUI_widget_FILL_RECT BackgroundColor { get; set; }
    }

    public class ElementDigitalTime
    {
        public string elementName = "DigitalTime";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_TIME_hour hour { get; set; }
        public hmUI_widget_IMG_TIME_minute minute { get; set; }
        public hmUI_widget_IMG_TIME_second second { get; set; }
        public hmUI_widget_IMG_TIME_am_pm am_pm { get; set; }


        public hmUI_widget_TEXT_TIME_hour hour_text { get; set; }
        public hmUI_widget_TEXT_TIME_minute minute_text { get; set; }
        public hmUI_widget_TEXT_TIME_second second_text { get; set; }
    }
}
