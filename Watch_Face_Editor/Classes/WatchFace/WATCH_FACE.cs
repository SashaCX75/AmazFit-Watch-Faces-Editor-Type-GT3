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
                    dot_path = this.Second.dot_path,
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
                    dot_path = this.Minute.dot_path,
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
                    dot_path = this.Hour.dot_path,
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
                    dot_path = this.Number.dot_path,
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
                    dot_path = this.Number.dot_path,
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
                    dot_path = this.Number.dot_path,
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
                    type = this.Number.type,
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
                    dot_path = this.Number.dot_path,
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
                    dot_path = this.Number_Target.dot_path,
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
}


