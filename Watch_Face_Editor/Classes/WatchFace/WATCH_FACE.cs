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

    public class Background : ICloneable
    {
        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        /// <summary>Изображение заднего фона</summary>
        public hmUI_widget_IMG BackgroundImage { get; set; }

        /// <summary>Цвет фона</summary>
        public hmUI_widget_FILL_RECT BackgroundColor { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG BackgroundImage = null;
            if (this.BackgroundImage != null)
            {
                BackgroundImage = new hmUI_widget_IMG
                {
                    src = this.BackgroundImage.src,
                    x = this.BackgroundImage.x,
                    y = this.BackgroundImage.y,
                    h = this.BackgroundImage.h,
                    w = this.BackgroundImage.w,

                    position = this.BackgroundImage.position,
                    visible = this.BackgroundImage.visible,
                    show_level = this.BackgroundImage.show_level,
                }; 
            }

            hmUI_widget_FILL_RECT BackgroundColor = null;
            if (this.BackgroundColor != null)
            {
                BackgroundColor = new hmUI_widget_FILL_RECT
                {
                    x = this.BackgroundColor.x,
                    y = this.BackgroundColor.y,
                    h = this.BackgroundColor.h,
                    w = this.BackgroundColor.w,

                    //position = this.BackgroundImage.position,
                    //visible = this.BackgroundImage.visible,
                    color = this.BackgroundColor.color,
                    show_level = this.BackgroundColor.show_level,
                }; 
            }
            
            return new Background
            {
                visible = this.visible,
                BackgroundImage = BackgroundImage,
                BackgroundColor = BackgroundColor
            };
        }
    }

    public class ElementDigitalTime : ICloneable
    {
        public string elementName = "DigitalTime";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_NUMBER Second { get; set; }
        public hmUI_widget_IMG_NUMBER Minute { get; set; }
        public hmUI_widget_IMG_NUMBER Hour { get; set; }
        public hmUI_widget_IMG_TIME_am_pm AmPm { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_NUMBER Second = null;
            if (this.Second != null)
            {
                Second = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Second.imageX,
                    imageY = this.Second.imageY,
                    space = this.Second.space,
                    zero = this.Second.zero,
                    align = this.Second.align,
                    img_First = this.Second.img_First,
                    unit = this.Second.unit,
                    imperial_unit = this.Second.imperial_unit,
                    icon = this.Second.icon,
                    iconPosX = this.Second.iconPosX,
                    iconPosY = this.Second.iconPosY,
                    negative_image = this.Second.negative_image,
                    invalid_image = this.Second.invalid_image,
                    dot_image = this.Second.dot_image,
                    follow = this.Second.follow,

                    position = this.Second.position,
                    visible = this.Second.visible,
                    show_level = this.Second.show_level,
                    type = this.Second.type,
                };
            }

            hmUI_widget_IMG_NUMBER Minute = null;
            if (this.Minute != null)
            {
                Minute = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Minute.imageX,
                    imageY = this.Minute.imageY,
                    space = this.Minute.space,
                    zero = this.Minute.zero,
                    align = this.Minute.align,
                    img_First = this.Minute.img_First,
                    unit = this.Minute.unit,
                    imperial_unit = this.Minute.imperial_unit,
                    icon = this.Minute.icon,
                    iconPosX = this.Minute.iconPosX,
                    iconPosY = this.Minute.iconPosY,
                    negative_image = this.Minute.negative_image,
                    invalid_image = this.Minute.invalid_image,
                    dot_image = this.Minute.dot_image,
                    follow = this.Minute.follow,

                    position = this.Minute.position,
                    visible = this.Minute.visible,
                    show_level = this.Minute.show_level,
                    type = this.Minute.type,
                };
            }

            hmUI_widget_IMG_NUMBER Hour = null;
            if (this.Hour != null)
            {
                Hour = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Hour.imageX,
                    imageY = this.Hour.imageY,
                    space = this.Hour.space,
                    zero = this.Hour.zero,
                    align = this.Hour.align,
                    img_First = this.Hour.img_First,
                    unit = this.Hour.unit,
                    imperial_unit = this.Hour.imperial_unit,
                    icon = this.Hour.icon,
                    iconPosX = this.Hour.iconPosX,
                    iconPosY = this.Hour.iconPosY,
                    negative_image = this.Hour.negative_image,
                    invalid_image = this.Hour.invalid_image,
                    dot_image = this.Hour.dot_image,
                    follow = this.Hour.follow,

                    position = this.Hour.position,
                    visible = this.Hour.visible,
                    show_level = this.Hour.show_level,
                    type = this.Hour.type,
                };
            }

            hmUI_widget_IMG_TIME_am_pm AmPm = null;
            if (this.AmPm != null)
            {
                AmPm = new hmUI_widget_IMG_TIME_am_pm
                {
                    am_x = this.AmPm.am_x,
                    am_y = this.AmPm.am_y,
                    am_img = this.AmPm.am_img,
                    pm_x = this.AmPm.pm_x,
                    pm_y = this.AmPm.pm_y,
                    pm_img = this.AmPm.pm_img,

                    position = this.AmPm.position,
                    visible = this.AmPm.visible,
                    show_level = this.AmPm.show_level,
                };
            }

            return new ElementDigitalTime
            {
                elementName = this.elementName,
                visible = this.visible,
                Second = Second,
                Minute = Minute,
                Hour = Hour,
                AmPm = AmPm,
            };
        }
    }

    public class ElementAnalogTime : ICloneable
    {
        public string elementName = "AnalogTime";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_POINTER Second { get; set; }
        public hmUI_widget_IMG_POINTER Minute { get; set; }
        public hmUI_widget_IMG_POINTER Hour { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_POINTER Second = null;
            if (this.Second != null)
            {
                Second = new hmUI_widget_IMG_POINTER
                {
                    src = this.Second.src,
                    center_x = this.Second.center_x,
                    center_y = this.Second.center_y,
                    pos_x = this.Second.pos_x,
                    pos_y = this.Second.pos_y,
                    start_angle = this.Second.start_angle,
                    end_angle = this.Second.end_angle,
                    cover_path = this.Second.cover_path,
                    cover_x = this.Second.cover_x,
                    cover_y = this.Second.cover_y,
                    scale = this.Second.scale,
                    scale_x = this.Second.scale_x,
                    scale_y = this.Second.scale_y,

                    position = this.Second.position,
                    visible = this.Second.visible,
                    show_level = this.Second.show_level,
                    type = this.Second.type,
                };
            }

            hmUI_widget_IMG_POINTER Minute = null;
            if (this.Minute != null)
            {
                Minute = new hmUI_widget_IMG_POINTER
                {
                    src = this.Minute.src,
                    center_x = this.Minute.center_x,
                    center_y = this.Minute.center_y,
                    pos_x = this.Minute.pos_x,
                    pos_y = this.Minute.pos_y,
                    start_angle = this.Minute.start_angle,
                    end_angle = this.Minute.end_angle,
                    cover_path = this.Minute.cover_path,
                    cover_x = this.Minute.cover_x,
                    cover_y = this.Minute.cover_y,
                    scale = this.Minute.scale,
                    scale_x = this.Minute.scale_x,
                    scale_y = this.Minute.scale_y,

                    position = this.Minute.position,
                    visible = this.Minute.visible,
                    show_level = this.Minute.show_level,
                    type = this.Minute.type,
                };
            }

            hmUI_widget_IMG_POINTER Hour = null;
            if (this.Hour != null)
            {
                Hour = new hmUI_widget_IMG_POINTER
                {
                    src = this.Hour.src,
                    center_x = this.Hour.center_x,
                    center_y = this.Hour.center_y,
                    pos_x = this.Hour.pos_x,
                    pos_y = this.Hour.pos_y,
                    start_angle = this.Hour.start_angle,
                    end_angle = this.Hour.end_angle,
                    cover_path = this.Hour.cover_path,
                    cover_x = this.Hour.cover_x,
                    cover_y = this.Hour.cover_y,
                    scale = this.Hour.scale,
                    scale_x = this.Hour.scale_x,
                    scale_y = this.Hour.scale_y,

                    position = this.Hour.position,
                    visible = this.Hour.visible,
                    show_level = this.Hour.show_level,
                    type = this.Hour.type,
                };
            }

            return new ElementAnalogTime
            {
                elementName = this.elementName,
                visible = this.visible,
                Second = Second,
                Minute = Minute,
                Hour = Hour,
            };
        }
    }

    public class ElementDateDay : ICloneable
    {
        public string elementName = "DateDay";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            return new ElementDateDay
            {
                elementName = this.elementName,
                visible = this.visible,
                Pointer = Pointer,
                Number = Number,
            };
        }
    }

    public class ElementDateMonth : ICloneable
    {
        public string elementName = "DateMonth";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_LEVEL Images { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            return new ElementDateMonth
            {
                elementName = this.elementName,
                visible = this.visible,
                Pointer = Pointer,
                Number = Number,
                Images = Images,
            };
        }
    }

    public class ElementDateYear : ICloneable
    {
        public string elementName = "DateYear";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_NUMBER Number { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            return new ElementDateYear
            {
                elementName = this.elementName,
                visible = this.visible,
                Number = Number,
            };
        }
    }

    public class ElementDateWeek : ICloneable
    {
        public string elementName = "DateWeek";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG_LEVEL Images { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            return new ElementDateWeek
            {
                elementName = this.elementName,
                visible = this.visible,
                Pointer = Pointer,
                Images = Images,
            };
        }
    }

    public class ElementStatuses : ICloneable
    {
        public string elementName = "ElementStatuses";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_STATUS Alarm { get; set; }
        public hmUI_widget_IMG_STATUS Bluetooth { get; set; }
        public hmUI_widget_IMG_STATUS DND { get; set; }
        public hmUI_widget_IMG_STATUS Lock { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_STATUS Alarm = null;
            if (this.Alarm != null)
            {
                Alarm = new hmUI_widget_IMG_STATUS
                {
                    x = this.Alarm.x,
                    y = this.Alarm.y,
                    src = this.Alarm.src,

                    position = this.Alarm.position,
                    visible = this.Alarm.visible,

                    show_level = this.Alarm.show_level,
                    type = this.Alarm.type,
                };
            }

            hmUI_widget_IMG_STATUS Bluetooth = null;
            if (this.Bluetooth != null)
            {
                Bluetooth = new hmUI_widget_IMG_STATUS
                {
                    x = this.Bluetooth.x,
                    y = this.Bluetooth.y,
                    src = this.Bluetooth.src,

                    position = this.Bluetooth.position,
                    visible = this.Bluetooth.visible,

                    show_level = this.Bluetooth.show_level,
                    type = this.Bluetooth.type,
                };
            }

            hmUI_widget_IMG_STATUS DND = null;
            if (this.DND != null)
            {
                DND = new hmUI_widget_IMG_STATUS
                {
                    x = this.DND.x,
                    y = this.DND.y,
                    src = this.DND.src,

                    position = this.DND.position,
                    visible = this.DND.visible,

                    show_level = this.DND.show_level,
                    type = this.DND.type,
                };
            }

            hmUI_widget_IMG_STATUS Lock = null;
            if (this.Lock != null)
            {
                Lock = new hmUI_widget_IMG_STATUS
                {
                    x = this.Lock.x,
                    y = this.Lock.y,
                    src = this.Lock.src,

                    position = this.Lock.position,
                    visible = this.Lock.visible,

                    show_level = this.Lock.show_level,
                    type = this.Lock.type,
                };
            }

            return new ElementStatuses
            {
                elementName = this.elementName,
                visible = this.visible,

                Alarm = Alarm,
                Bluetooth = Bluetooth,
                DND = DND,
                Lock = Lock,
            };
        }
    }

    public class ElementShortcuts : ICloneable
    {
        public string elementName = "ElementShortcuts";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_CLICK Step { get; set; }
        public hmUI_widget_IMG_CLICK Heart { get; set; }
        public hmUI_widget_IMG_CLICK SPO2 { get; set; }
        public hmUI_widget_IMG_CLICK PAI { get; set; }
        public hmUI_widget_IMG_CLICK Stress { get; set; }
        public hmUI_widget_IMG_CLICK Weather { get; set; }
        public hmUI_widget_IMG_CLICK Altimeter { get; set; }
        public hmUI_widget_IMG_CLICK Sunrise { get; set; }
        public hmUI_widget_IMG_CLICK Alarm { get; set; }
        public hmUI_widget_IMG_CLICK Sleep { get; set; }
        public hmUI_widget_IMG_CLICK Countdown { get; set; }
        public hmUI_widget_IMG_CLICK Stopwatch { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_CLICK Step = null;
            if (this.Step != null)
            {
                Step = new hmUI_widget_IMG_CLICK
                {
                    x = this.Step.x,
                    y = this.Step.y,
                    w = this.Step.w,
                    h = this.Step.h,
                    src = this.Step.src,

                    position = this.Step.position,
                    visible = this.Step.visible,

                    show_level = this.Step.show_level,
                    type = this.Step.type,
                };
            }

            hmUI_widget_IMG_CLICK Heart = null;
            if (this.Heart != null)
            {
                Heart = new hmUI_widget_IMG_CLICK
                {
                    x = this.Heart.x,
                    y = this.Heart.y,
                    w = this.Heart.w,
                    h = this.Heart.h,
                    src = this.Heart.src,

                    position = this.Heart.position,
                    visible = this.Heart.visible,

                    show_level = this.Heart.show_level,
                    type = this.Heart.type,
                };
            }

            hmUI_widget_IMG_CLICK SPO2 = null;
            if (this.SPO2 != null)
            {
                SPO2 = new hmUI_widget_IMG_CLICK
                {
                    x = this.SPO2.x,
                    y = this.SPO2.y,
                    w = this.SPO2.w,
                    h = this.SPO2.h,
                    src = this.SPO2.src,

                    position = this.SPO2.position,
                    visible = this.SPO2.visible,

                    show_level = this.SPO2.show_level,
                    type = this.SPO2.type,
                };
            }

            hmUI_widget_IMG_CLICK PAI = null;
            if (this.PAI != null)
            {
                PAI = new hmUI_widget_IMG_CLICK
                {
                    x = this.PAI.x,
                    y = this.PAI.y,
                    w = this.PAI.w,
                    h = this.PAI.h,
                    src = this.PAI.src,

                    position = this.PAI.position,
                    visible = this.PAI.visible,

                    show_level = this.PAI.show_level,
                    type = this.PAI.type,
                };
            }

            hmUI_widget_IMG_CLICK Stress = null;
            if (this.Stress != null)
            {
                Stress = new hmUI_widget_IMG_CLICK
                {
                    x = this.Stress.x,
                    y = this.Stress.y,
                    w = this.Stress.w,
                    h = this.Stress.h,
                    src = this.Stress.src,

                    position = this.Stress.position,
                    visible = this.Stress.visible,

                    show_level = this.Stress.show_level,
                    type = this.Stress.type,
                };
            }

            hmUI_widget_IMG_CLICK Weather = null;
            if (this.Weather != null)
            {
                Weather = new hmUI_widget_IMG_CLICK
                {
                    x = this.Weather.x,
                    y = this.Weather.y,
                    w = this.Weather.w,
                    h = this.Weather.h,
                    src = this.Weather.src,

                    position = this.Weather.position,
                    visible = this.Weather.visible,

                    show_level = this.Weather.show_level,
                    type = this.Weather.type,
                };
            }

            hmUI_widget_IMG_CLICK Altimeter = null;
            if (this.Altimeter != null)
            {
                Altimeter = new hmUI_widget_IMG_CLICK
                {
                    x = this.Altimeter.x,
                    y = this.Altimeter.y,
                    w = this.Altimeter.w,
                    h = this.Altimeter.h,
                    src = this.Altimeter.src,

                    position = this.Altimeter.position,
                    visible = this.Altimeter.visible,

                    show_level = this.Altimeter.show_level,
                    type = this.Altimeter.type,
                };
            }

            hmUI_widget_IMG_CLICK Sunrise = null;
            if (this.Sunrise != null)
            {
                Sunrise = new hmUI_widget_IMG_CLICK
                {
                    x = this.Sunrise.x,
                    y = this.Sunrise.y,
                    w = this.Sunrise.w,
                    h = this.Sunrise.h,
                    src = this.Sunrise.src,

                    position = this.Sunrise.position,
                    visible = this.Sunrise.visible,

                    show_level = this.Sunrise.show_level,
                    type = this.Sunrise.type,
                };
            }

            hmUI_widget_IMG_CLICK Alarm = null;
            if (this.Alarm != null)
            {
                Alarm = new hmUI_widget_IMG_CLICK
                {
                    x = this.Alarm.x,
                    y = this.Alarm.y,
                    w = this.Alarm.w,
                    h = this.Alarm.h,
                    src = this.Alarm.src,

                    position = this.Alarm.position,
                    visible = this.Alarm.visible,

                    show_level = this.Alarm.show_level,
                    type = this.Alarm.type,
                };
            }

            hmUI_widget_IMG_CLICK Sleep = null;
            if (this.Sleep != null)
            {
                Sleep = new hmUI_widget_IMG_CLICK
                {
                    x = this.Sleep.x,
                    y = this.Sleep.y,
                    w = this.Sleep.w,
                    h = this.Sleep.h,
                    src = this.Sleep.src,

                    position = this.Sleep.position,
                    visible = this.Sleep.visible,

                    show_level = this.Sleep.show_level,
                    type = this.Sleep.type,
                };
            }

            hmUI_widget_IMG_CLICK Countdown = null;
            if (this.Countdown != null)
            {
                Countdown = new hmUI_widget_IMG_CLICK
                {
                    x = this.Countdown.x,
                    y = this.Countdown.y,
                    w = this.Countdown.w,
                    h = this.Countdown.h,
                    src = this.Countdown.src,

                    position = this.Countdown.position,
                    visible = this.Countdown.visible,

                    show_level = this.Countdown.show_level,
                    type = this.Countdown.type,
                };
            }

            hmUI_widget_IMG_CLICK Stopwatch = null;
            if (this.Stopwatch != null)
            {
                Stopwatch = new hmUI_widget_IMG_CLICK
                {
                    x = this.Stopwatch.x,
                    y = this.Stopwatch.y,
                    w = this.Stopwatch.w,
                    h = this.Stopwatch.h,
                    src = this.Stopwatch.src,

                    position = this.Stopwatch.position,
                    visible = this.Stopwatch.visible,

                    show_level = this.Stopwatch.show_level,
                    type = this.Stopwatch.type,
                };
            }

            return new ElementShortcuts
            {
                elementName = this.elementName,
                visible = this.visible,

                Step = Step,
                Heart = Heart,
                SPO2 = SPO2,
                PAI = PAI,
                Stress = Stress,
                Weather = Weather,
                Altimeter = Altimeter,
                Sunrise = Sunrise,
                Alarm = Alarm,
                Sleep = Sleep,
                Countdown = Countdown,
                Stopwatch = Stopwatch,
            };
        }
    }

    public class ElementSteps : ICloneable
    {
        public string elementName = "ElementSteps";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Number_Target != null)
            {
                Number_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Target.imageX,
                    imageY = this.Number_Target.imageY,
                    space = this.Number_Target.space,
                    zero = this.Number_Target.zero,
                    align = this.Number_Target.align,
                    img_First = this.Number_Target.img_First,
                    unit = this.Number_Target.unit,
                    imperial_unit = this.Number_Target.imperial_unit,
                    icon = this.Number_Target.icon,
                    iconPosX = this.Number_Target.iconPosX,
                    iconPosY = this.Number_Target.iconPosY,
                    negative_image = this.Number_Target.negative_image,
                    invalid_image = this.Number_Target.invalid_image,
                    dot_image = this.Number_Target.dot_image,
                    follow = this.Number_Target.follow,

                    position = this.Number_Target.position,
                    visible = this.Number_Target.visible,
                    show_level = this.Number_Target.show_level,
                    type = this.Number_Target.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementSteps
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Target = Number_Target,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementBattery : ICloneable
    {
        public string elementName = "ElementBattery";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementBattery
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementCalories : ICloneable
    {
        public string elementName = "ElementCalories";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Number_Target != null)
            {
                Number_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Target.imageX,
                    imageY = this.Number_Target.imageY,
                    space = this.Number_Target.space,
                    zero = this.Number_Target.zero,
                    align = this.Number_Target.align,
                    img_First = this.Number_Target.img_First,
                    unit = this.Number_Target.unit,
                    imperial_unit = this.Number_Target.imperial_unit,
                    icon = this.Number_Target.icon,
                    iconPosX = this.Number_Target.iconPosX,
                    iconPosY = this.Number_Target.iconPosY,
                    negative_image = this.Number_Target.negative_image,
                    invalid_image = this.Number_Target.invalid_image,
                    dot_image = this.Number_Target.dot_image,
                    follow = this.Number_Target.follow,

                    position = this.Number_Target.position,
                    visible = this.Number_Target.visible,
                    show_level = this.Number_Target.show_level,
                    type = this.Number_Target.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementCalories
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Target = Number_Target,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementHeart : ICloneable
    {
        public string elementName = "ElementHeart";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementHeart
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementPAI : ICloneable
    {
        public string elementName = "ElementPAI";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Number_Target != null)
            {
                Number_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Target.imageX,
                    imageY = this.Number_Target.imageY,
                    space = this.Number_Target.space,
                    zero = this.Number_Target.zero,
                    align = this.Number_Target.align,
                    img_First = this.Number_Target.img_First,
                    unit = this.Number_Target.unit,
                    imperial_unit = this.Number_Target.imperial_unit,
                    icon = this.Number_Target.icon,
                    iconPosX = this.Number_Target.iconPosX,
                    iconPosY = this.Number_Target.iconPosY,
                    negative_image = this.Number_Target.negative_image,
                    dot_image = this.Number_Target.dot_image,
                    follow = this.Number_Target.follow,

                    position = this.Number_Target.position,
                    visible = this.Number_Target.visible,
                    show_level = this.Number_Target.show_level,
                    type = this.Number_Target.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementPAI
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Target = Number_Target,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementDistance : ICloneable
    {
        public string elementName = "ElementDistance";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_NUMBER Number { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            return new ElementDistance
            {
                elementName = this.elementName,
                visible = this.visible,

                Number = Number,
            };
        }
    }

    public class ElementStand : ICloneable
    {
        public string elementName = "ElementStand";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Number_Target != null)
            {
                Number_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Target.imageX,
                    imageY = this.Number_Target.imageY,
                    space = this.Number_Target.space,
                    zero = this.Number_Target.zero,
                    align = this.Number_Target.align,
                    img_First = this.Number_Target.img_First,
                    unit = this.Number_Target.unit,
                    imperial_unit = this.Number_Target.imperial_unit,
                    icon = this.Number_Target.icon,
                    iconPosX = this.Number_Target.iconPosX,
                    iconPosY = this.Number_Target.iconPosY,
                    negative_image = this.Number_Target.negative_image,
                    invalid_image = this.Number_Target.invalid_image,
                    dot_image = this.Number_Target.dot_image,
                    follow = this.Number_Target.follow,

                    position = this.Number_Target.position,
                    visible = this.Number_Target.visible,
                    show_level = this.Number_Target.show_level,
                    type = this.Number_Target.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementStand
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Target = Number_Target,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementActivity : ICloneable
    {
        public string elementName = "ElementActivity";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        /// <summary>Отображать элемент как калории</summary>
        public bool showCalories = false;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Number_Target != null)
            {
                Number_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Target.imageX,
                    imageY = this.Number_Target.imageY,
                    space = this.Number_Target.space,
                    zero = this.Number_Target.zero,
                    align = this.Number_Target.align,
                    img_First = this.Number_Target.img_First,
                    unit = this.Number_Target.unit,
                    imperial_unit = this.Number_Target.imperial_unit,
                    icon = this.Number_Target.icon,
                    iconPosX = this.Number_Target.iconPosX,
                    iconPosY = this.Number_Target.iconPosY,
                    negative_image = this.Number_Target.negative_image,
                    invalid_image = this.Number_Target.invalid_image,
                    dot_image = this.Number_Target.dot_image,
                    follow = this.Number_Target.follow,

                    position = this.Number_Target.position,
                    visible = this.Number_Target.visible,
                    show_level = this.Number_Target.show_level,
                    type = this.Number_Target.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementActivity
            {
                elementName = this.elementName,
                visible = this.visible,
                showCalories = this.showCalories,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Target = Number_Target,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementSpO2 : ICloneable
    {
        public string elementName = "ElementSpO2";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_NUMBER Number { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            return new ElementSpO2
            {
                elementName = this.elementName,
                visible = this.visible,

                Number = Number,
            };
        }
    }

    public class ElementStress : ICloneable
    {
        public string elementName = "ElementStress";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementStress
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Pointer = Pointer,
                Icon = Icon,
            };
        }
    }

    public class ElementFatBurning : ICloneable
    {
        public string elementName = "ElementFatBurning";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public Circle_Scale Circle_Scale { get; set; }
        public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Number_Target != null)
            {
                Number_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Target.imageX,
                    imageY = this.Number_Target.imageY,
                    space = this.Number_Target.space,
                    zero = this.Number_Target.zero,
                    align = this.Number_Target.align,
                    img_First = this.Number_Target.img_First,
                    unit = this.Number_Target.unit,
                    imperial_unit = this.Number_Target.imperial_unit,
                    icon = this.Number_Target.icon,
                    iconPosX = this.Number_Target.iconPosX,
                    iconPosY = this.Number_Target.iconPosY,
                    negative_image = this.Number_Target.negative_image,
                    invalid_image = this.Number_Target.invalid_image,
                    dot_image = this.Number_Target.dot_image,
                    follow = this.Number_Target.follow,

                    position = this.Number_Target.position,
                    visible = this.Number_Target.visible,
                    show_level = this.Number_Target.show_level,
                    type = this.Number_Target.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementFatBurning
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Target = Number_Target,
                Pointer = Pointer,
                Circle_Scale = Circle_Scale,
                Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }



    public class ElementWeather : ICloneable
    {
        public string elementName = "ElementWeather";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Min { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Max { get; set; }
        public hmUI_widget_TEXT City_Name { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Min = null;
            if (this.Number_Min != null)
            {
                Number_Min = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Min.imageX,
                    imageY = this.Number_Min.imageY,
                    space = this.Number_Min.space,
                    zero = this.Number_Min.zero,
                    align = this.Number_Min.align,
                    img_First = this.Number_Min.img_First,
                    unit = this.Number_Min.unit,
                    imperial_unit = this.Number_Min.imperial_unit,
                    icon = this.Number_Min.icon,
                    iconPosX = this.Number_Min.iconPosX,
                    iconPosY = this.Number_Min.iconPosY,
                    negative_image = this.Number_Min.negative_image,
                    invalid_image = this.Number_Min.invalid_image,
                    dot_image = this.Number_Min.dot_image,
                    follow = this.Number_Min.follow,

                    position = this.Number_Min.position,
                    visible = this.Number_Min.visible,
                    show_level = this.Number_Min.show_level,
                    type = this.Number_Min.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Max = null;
            if (this.Number_Max != null)
            {
                Number_Max = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Max.imageX,
                    imageY = this.Number_Max.imageY,
                    space = this.Number_Max.space,
                    zero = this.Number_Max.zero,
                    align = this.Number_Max.align,
                    img_First = this.Number_Max.img_First,
                    unit = this.Number_Max.unit,
                    imperial_unit = this.Number_Max.imperial_unit,
                    icon = this.Number_Max.icon,
                    iconPosX = this.Number_Max.iconPosX,
                    iconPosY = this.Number_Max.iconPosY,
                    negative_image = this.Number_Max.negative_image,
                    invalid_image = this.Number_Max.invalid_image,
                    dot_image = this.Number_Max.dot_image,
                    follow = this.Number_Max.follow,

                    position = this.Number_Max.position,
                    visible = this.Number_Max.visible,
                    show_level = this.Number_Max.show_level,
                    type = this.Number_Max.type,
                };
            }

            hmUI_widget_TEXT City_Name = null;
            if (this.City_Name != null)
            {
                City_Name = new hmUI_widget_TEXT
                {
                    x = this.City_Name.x,
                    y = this.City_Name.y,
                    w = this.City_Name.w,
                    h = this.City_Name.h,
                    color = this.City_Name.color,
                    align_h = this.City_Name.align_h,
                    align_v = this.City_Name.align_v,
                    text_size = this.City_Name.text_size,
                    text_style = this.City_Name.text_style,
                    line_space = this.City_Name.line_space,
                    char_space = this.City_Name.char_space,

                    position = this.City_Name.position,
                    visible = this.City_Name.visible,
                    show_level = this.City_Name.show_level,
                    type = this.City_Name.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementWeather
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Number = Number,
                Number_Min = Number_Min,
                Number_Max = Number_Max,
                City_Name = City_Name,
                Icon = Icon,
            };
        }
    }

    public class ElementUVIndex : ICloneable
    {
        public string elementName = "ElementUVIndex";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        //public Circle_Scale Circle_Scale { get; set; }
        //public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

/*            Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }*/

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementUVIndex
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Pointer = Pointer,
                //Circle_Scale = Circle_Scale,
                //Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementHumidity : ICloneable
    {
        public string elementName = "ElementHumidity";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        //public Circle_Scale Circle_Scale { get; set; }
        //public Linear_Scale Linear_Scale { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            /*Circle_Scale Circle_Scale = null;
            if (this.Circle_Scale != null)
            {
                Circle_Scale = new Circle_Scale
                {
                    center_x = this.Circle_Scale.center_x,
                    center_y = this.Circle_Scale.center_y,
                    start_angle = this.Circle_Scale.start_angle,
                    end_angle = this.Circle_Scale.end_angle,
                    color = this.Circle_Scale.color,
                    radius = this.Circle_Scale.radius,
                    line_width = this.Circle_Scale.line_width,
                    mirror = this.Circle_Scale.mirror,
                    inversion = this.Circle_Scale.inversion,

                    position = this.Circle_Scale.position,
                    visible = this.Circle_Scale.visible,
                    show_level = this.Circle_Scale.show_level,
                    type = this.Circle_Scale.type,
                };
            }

            Linear_Scale Linear_Scale = null;
            if (this.Linear_Scale != null)
            {
                Linear_Scale = new Linear_Scale
                {
                    start_x = this.Linear_Scale.start_x,
                    start_y = this.Linear_Scale.start_y,
                    color = this.Linear_Scale.color,
                    pointer = this.Linear_Scale.pointer,
                    lenght = this.Linear_Scale.lenght,
                    line_width = this.Linear_Scale.line_width,
                    mirror = this.Linear_Scale.mirror,
                    inversion = this.Linear_Scale.inversion,
                    vertical = this.Linear_Scale.vertical,

                    position = this.Linear_Scale.position,
                    visible = this.Linear_Scale.visible,
                    show_level = this.Linear_Scale.show_level,
                    type = this.Linear_Scale.type,
                };
            }*/

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementHumidity
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Pointer = Pointer,
                //Circle_Scale = Circle_Scale,
                //Linear_Scale = Linear_Scale,
                Icon = Icon,
            };
        }
    }

    public class ElementAltimeter : ICloneable
    {
        public string elementName = "ElementAltimeter";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementAltimeter
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Pointer = Pointer,
                Icon = Icon,
            };
        }
    }

    public class ElementSunrise : ICloneable
    {
        public string elementName = "ElementSunrise";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Sunrise { get; set; }
        public hmUI_widget_IMG_NUMBER Sunset { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Sunrise != null)
            {
                Sunrise = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunrise.imageX,
                    imageY = this.Sunrise.imageY,
                    space = this.Sunrise.space,
                    zero = this.Sunrise.zero,
                    align = this.Sunrise.align,
                    img_First = this.Sunrise.img_First,
                    unit = this.Sunrise.unit,
                    imperial_unit = this.Sunrise.imperial_unit,
                    icon = this.Sunrise.icon,
                    iconPosX = this.Sunrise.iconPosX,
                    iconPosY = this.Sunrise.iconPosY,
                    negative_image = this.Sunrise.negative_image,
                    invalid_image = this.Sunrise.invalid_image,
                    dot_image = this.Sunrise.dot_image,
                    follow = this.Sunrise.follow,

                    position = this.Sunrise.position,
                    visible = this.Sunrise.visible,
                    show_level = this.Sunrise.show_level,
                    type = this.Sunrise.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Sunset != null)
            {
                Sunset = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunset.imageX,
                    imageY = this.Sunset.imageY,
                    space = this.Sunset.space,
                    zero = this.Sunset.zero,
                    align = this.Sunset.align,
                    img_First = this.Sunset.img_First,
                    unit = this.Sunset.unit,
                    imperial_unit = this.Sunset.imperial_unit,
                    icon = this.Sunset.icon,
                    iconPosX = this.Sunset.iconPosX,
                    iconPosY = this.Sunset.iconPosY,
                    negative_image = this.Sunset.negative_image,
                    invalid_image = this.Sunset.invalid_image,
                    dot_image = this.Sunset.dot_image,
                    follow = this.Sunset.follow,

                    position = this.Sunset.position,
                    visible = this.Sunset.visible,
                    show_level = this.Sunset.show_level,
                    type = this.Sunset.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementSunrise
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Sunrise = Sunrise,
                Sunset = Sunset,
                Pointer = Pointer,
                Icon = Icon,
            };
        }
    }

    public class ElementWind : ICloneable
    {
        public string elementName = "ElementWind";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            hmUI_widget_IMG_PROGRESS Segments = null;
            if (this.Segments != null)
            {
                Segments = new hmUI_widget_IMG_PROGRESS
                {
                    X = this.Segments.X,
                    Y = this.Segments.Y,
                    img_First = this.Segments.img_First,
                    image_length = this.Segments.image_length,

                    position = this.Segments.position,
                    visible = this.Segments.visible,
                    show_level = this.Segments.show_level,
                    type = this.Segments.type,
                };
            }

            hmUI_widget_IMG_NUMBER Number = null;
            if (this.Number != null)
            {
                Number = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number.imageX,
                    imageY = this.Number.imageY,
                    space = this.Number.space,
                    zero = this.Number.zero,
                    align = this.Number.align,
                    img_First = this.Number.img_First,
                    unit = this.Number.unit,
                    imperial_unit = this.Number.imperial_unit,
                    icon = this.Number.icon,
                    iconPosX = this.Number.iconPosX,
                    iconPosY = this.Number.iconPosY,
                    negative_image = this.Number.negative_image,
                    invalid_image = this.Number.invalid_image,
                    dot_image = this.Number.dot_image,
                    follow = this.Number.follow,

                    position = this.Number.position,
                    visible = this.Number.visible,
                    show_level = this.Number.show_level,
                    type = this.Number.type,
                };
            }

            hmUI_widget_IMG_POINTER Pointer = null;
            if (this.Pointer != null)
            {
                Pointer = new hmUI_widget_IMG_POINTER
                {
                    src = this.Pointer.src,
                    center_x = this.Pointer.center_x,
                    center_y = this.Pointer.center_y,
                    pos_x = this.Pointer.pos_x,
                    pos_y = this.Pointer.pos_y,
                    start_angle = this.Pointer.start_angle,
                    end_angle = this.Pointer.end_angle,
                    cover_path = this.Pointer.cover_path,
                    cover_x = this.Pointer.cover_x,
                    cover_y = this.Pointer.cover_y,
                    scale = this.Pointer.scale,
                    scale_x = this.Pointer.scale_x,
                    scale_y = this.Pointer.scale_y,

                    position = this.Pointer.position,
                    visible = this.Pointer.visible,
                    show_level = this.Pointer.show_level,
                    type = this.Pointer.type,
                };
            }

            hmUI_widget_IMG Icon = null;
            if (this.Icon != null)
            {
                Icon = new hmUI_widget_IMG
                {
                    x = this.Icon.x,
                    y = this.Icon.y,
                    w = this.Icon.w,
                    h = this.Icon.h,
                    src = this.Icon.src,

                    position = this.Icon.position,
                    visible = this.Icon.visible,
                    show_level = this.Icon.show_level,
                };
            }

            return new ElementWind
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Pointer = Pointer,
                Icon = Icon,
            };
        }
    }

    public class ElementMoon : ICloneable
    {
        public string elementName = "ElementMoon";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }

        public object Clone()
        {

            hmUI_widget_IMG_LEVEL Images = null;
            if (this.Images != null)
            {
                Images = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Images.X,
                    Y = this.Images.Y,
                    img_First = this.Images.img_First,
                    image_length = this.Images.image_length,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            return new ElementMoon
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
            };
        }
    }
}


