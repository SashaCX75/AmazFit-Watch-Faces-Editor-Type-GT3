using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    class WATCH_FACE
    {
        /// <summary>Модель часов и ID циферблата</summary>
        public WatchFace_Info WatchFace_Info { get; set; }

        /// <summary>Основной экран</summary>
        public ScreenNormal ScreenNormal { get; set; }

        /// <summary>Редактируемые стрелки</summary>
        public ScreenAOD ScreenAOD { get; set; }

        /// <summary>Редактируемые элементы</summary>
        public EditableElements Editable_Elements { get; set; }

        /// <summary>AOD экран</summary>
        public ElementEditablePointers ElementEditablePointers { get; set; }

        public DisconnectAlert DisconnectAlert { get; set; }

        public RepeatAlert RepeatAlert { get; set; }

        public TopImage TopImage { get; set; }

        public ElementShortcuts Shortcuts { get; set; }

        public ElementButtons Buttons { get; set; }
    }

    public class WatchFace_Info
    {
        /// <summary> Название модели часов</summary>
        public string DeviceName { get; set; }

        /// <summary>Id циферблата</summary>
        public int WatchFaceId { get; set; } = 1234567;

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

        /// <summary>Редактируемый задний фон</summary>
        public Editable_Background Editable_Background { get; set; }

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
                    //enable = this.BackgroundImage.enable,
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

    public class Editable_Background
    {
        /// <summary>Использовать ли в циферблате редактируемый фон</summary>
        public bool enable_edit_bg = true;

        /// <summary>Выбраный задний фон</summary>
        public int selected_background = 0;

        /// <summary>Координаты фона (на всякий случай)</summary>
        public int x = 0;

        /// <summary>Координаты фона (на всякий случай)</summary>
        public int y = 0;

        /// <summary>Размер фона (для определения модели)</summary>
        public int h = 0;

        /// <summary>Размер фона (для определения модели)</summary>
        public int w = 0;

        /// <summary>Перечень картинок для фона</summary>
        public List<BackgroundList> BackgroundList { get; set; }

        /// <summary>Перечень картинок для предпросмотра фона</summary>
        //public List<string> BackgroundPreviewList { get; set; }

        /// <summary>Фон пояснительной надписи</summary>
        public string tips_bg { get; set; }

        /// <summary>Координаты пояснительной надписи</summary>
        public int tips_x { get; set; }

        /// <summary>Координаты пояснительной надписи</summary>
        public int tips_y { get; set; }

        /// <summary>Рамка выделения</summary>
        public string fg { get; set; }

        /// <summary>Отображать редактируемый фон в режиме АОД</summary>
        public bool AOD_show { get; set; } = false;

        /// <summary>Отображать в режиме редактирования</summary>
        public bool showEeditMode { get; set; } = false;
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

        public hmUI_widget_TEXT Second_Font { get; set; }
        public hmUI_widget_TEXT Minute_Font { get; set; }
        public hmUI_widget_TEXT Hour_Font { get; set; }
        public hmUI_widget_TEXT Hour_min_Font { get; set; }
        public hmUI_widget_TEXT Hour_min_sec_Font { get; set; }

        public hmUI_widget_IMG_NUMBER Second_rotation { get; set; }
        public hmUI_widget_IMG_NUMBER Minute_rotation { get; set; }
        public hmUI_widget_IMG_NUMBER Hour_rotation { get; set; }
        public Text_Circle Second_circle { get; set; }
        public Text_Circle Minute_circle { get; set; }
        public Text_Circle Hour_circle { get; set; }

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
                    angle = this.Second.angle,
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

            hmUI_widget_TEXT Second_Font = null;
            if (this.Second_Font != null)
            {
                Second_Font = new hmUI_widget_TEXT
                {
                    x = this.Second_Font.x,
                    y = this.Second_Font.y,
                    w = this.Second_Font.w,
                    h = this.Second_Font.h,
                    color = this.Second_Font.color,
                    align_h = this.Second_Font.align_h,
                    align_v = this.Second_Font.align_v,
                    text_size = this.Second_Font.text_size,
                    text_style = this.Second_Font.text_style,
                    line_space = this.Second_Font.line_space,
                    char_space = this.Second_Font.char_space,
                    font = this.Second_Font.font,
                    padding = this.Second_Font.padding,
                    unit_type = this.Second_Font.unit_type,

                    position = this.Second_Font.position,
                    visible = this.Second_Font.visible,
                    show_level = this.Second_Font.show_level,
                    type = this.Second_Font.type,
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
                    angle = this.Minute.angle,
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

            hmUI_widget_TEXT Minute_Font = null;
            if (this.Minute_Font != null)
            {
                Minute_Font = new hmUI_widget_TEXT
                {
                    x = this.Minute_Font.x,
                    y = this.Minute_Font.y,
                    w = this.Minute_Font.w,
                    h = this.Minute_Font.h,
                    color = this.Minute_Font.color,
                    align_h = this.Minute_Font.align_h,
                    align_v = this.Minute_Font.align_v,
                    text_size = this.Minute_Font.text_size,
                    text_style = this.Minute_Font.text_style,
                    line_space = this.Minute_Font.line_space,
                    char_space = this.Minute_Font.char_space,
                    font = this.Minute_Font.font,
                    padding = this.Minute_Font.padding,
                    unit_type = this.Minute_Font.unit_type,

                    position = this.Minute_Font.position,
                    visible = this.Minute_Font.visible,
                    show_level = this.Minute_Font.show_level,
                    type = this.Minute_Font.type,
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
                    angle = this.Hour.angle,
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

            hmUI_widget_TEXT Hour_Font = null;
            if (this.Hour_Font != null)
            {
                Hour_Font = new hmUI_widget_TEXT
                {
                    x = this.Hour_Font.x,
                    y = this.Hour_Font.y,
                    w = this.Hour_Font.w,
                    h = this.Hour_Font.h,
                    color = this.Hour_Font.color,
                    align_h = this.Hour_Font.align_h,
                    align_v = this.Hour_Font.align_v,
                    text_size = this.Hour_Font.text_size,
                    text_style = this.Hour_Font.text_style,
                    line_space = this.Hour_Font.line_space,
                    char_space = this.Hour_Font.char_space,
                    font = this.Hour_Font.font,
                    padding = this.Hour_Font.padding,
                    unit_type = this.Hour_Font.unit_type,

                    position = this.Hour_Font.position,
                    visible = this.Hour_Font.visible,
                    show_level = this.Hour_Font.show_level,
                    type = this.Hour_Font.type,
                };
            }

            hmUI_widget_TEXT Hour_min_Font = null;
            if (this.Hour_min_Font != null)
            {
                Hour_min_Font = new hmUI_widget_TEXT
                {
                    x = this.Hour_min_Font.x,
                    y = this.Hour_min_Font.y,
                    w = this.Hour_min_Font.w,
                    h = this.Hour_min_Font.h,
                    color = this.Hour_min_Font.color,
                    align_h = this.Hour_min_Font.align_h,
                    align_v = this.Hour_min_Font.align_v,
                    text_size = this.Hour_min_Font.text_size,
                    text_style = this.Hour_min_Font.text_style,
                    line_space = this.Hour_min_Font.line_space,
                    char_space = this.Hour_min_Font.char_space,
                    font = this.Hour_min_Font.font,
                    padding = this.Hour_min_Font.padding,
                    unit_type = this.Hour_min_Font.unit_type,
                    unit_end = this.Hour_min_Font.unit_end,

                    position = this.Hour_min_Font.position,
                    visible = this.Hour_min_Font.visible,
                    show_level = this.Hour_min_Font.show_level,
                    type = this.Hour_min_Font.type,
                };
            }

            hmUI_widget_TEXT Hour_min_sec_Font = null;
            if (this.Hour_min_sec_Font != null)
            {
                Hour_min_sec_Font = new hmUI_widget_TEXT
                {
                    x = this.Hour_min_sec_Font.x,
                    y = this.Hour_min_sec_Font.y,
                    w = this.Hour_min_sec_Font.w,
                    h = this.Hour_min_sec_Font.h,
                    color = this.Hour_min_sec_Font.color,
                    align_h = this.Hour_min_sec_Font.align_h,
                    align_v = this.Hour_min_sec_Font.align_v,
                    text_size = this.Hour_min_sec_Font.text_size,
                    text_style = this.Hour_min_sec_Font.text_style,
                    line_space = this.Hour_min_sec_Font.line_space,
                    char_space = this.Hour_min_sec_Font.char_space,
                    font = this.Hour_min_sec_Font.font,
                    padding = this.Hour_min_sec_Font.padding,
                    unit_type = this.Hour_min_sec_Font.unit_type,
                    unit_end = this.Hour_min_Font.unit_end,

                    position = this.Hour_min_sec_Font.position,
                    visible = this.Hour_min_sec_Font.visible,
                    show_level = this.Hour_min_sec_Font.show_level,
                    type = this.Hour_min_sec_Font.type,
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

            hmUI_widget_IMG_NUMBER Second_rotation = null;
            if (this.Second_rotation != null)
            {
                Second_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Second_rotation.imageX,
                    imageY = this.Second_rotation.imageY,
                    space = this.Second_rotation.space,
                    angle = this.Second_rotation.angle,
                    zero = this.Second_rotation.zero,
                    align = this.Second_rotation.align,
                    img_First = this.Second_rotation.img_First,
                    unit = this.Second_rotation.unit,
                    imperial_unit = this.Second_rotation.imperial_unit,
                    icon = this.Second_rotation.icon,
                    iconPosX = this.Second_rotation.iconPosX,
                    iconPosY = this.Second_rotation.iconPosY,
                    negative_image = this.Second_rotation.negative_image,
                    invalid_image = this.Second_rotation.invalid_image,
                    dot_image = this.Second_rotation.dot_image,
                    unit_in_alignment = this.Second_rotation.unit_in_alignment,

                    position = this.Second_rotation.position,
                    visible = this.Second_rotation.visible,
                    show_level = this.Second_rotation.show_level,
                    type = this.Second_rotation.type,
                };
            }

            hmUI_widget_IMG_NUMBER Minute_rotation = null;
            if (this.Minute_rotation != null)
            {
                Minute_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Minute_rotation.imageX,
                    imageY = this.Minute_rotation.imageY,
                    space = this.Minute_rotation.space,
                    angle = this.Minute_rotation.angle,
                    zero = this.Minute_rotation.zero,
                    align = this.Minute_rotation.align,
                    img_First = this.Minute_rotation.img_First,
                    unit = this.Minute_rotation.unit,
                    imperial_unit = this.Minute_rotation.imperial_unit,
                    icon = this.Minute_rotation.icon,
                    iconPosX = this.Minute_rotation.iconPosX,
                    iconPosY = this.Minute_rotation.iconPosY,
                    negative_image = this.Minute_rotation.negative_image,
                    invalid_image = this.Minute_rotation.invalid_image,
                    dot_image = this.Minute_rotation.dot_image,
                    unit_in_alignment = this.Minute_rotation.unit_in_alignment,

                    position = this.Minute_rotation.position,
                    visible = this.Minute_rotation.visible,
                    show_level = this.Minute_rotation.show_level,
                    type = this.Minute_rotation.type,
                };
            }

            hmUI_widget_IMG_NUMBER Hour_rotation = null;
            if (this.Hour_rotation != null)
            {
                Hour_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Hour_rotation.imageX,
                    imageY = this.Hour_rotation.imageY,
                    space = this.Hour_rotation.space,
                    angle = this.Hour_rotation.angle,
                    zero = this.Hour_rotation.zero,
                    align = this.Hour_rotation.align,
                    img_First = this.Hour_rotation.img_First,
                    unit = this.Hour_rotation.unit,
                    imperial_unit = this.Hour_rotation.imperial_unit,
                    icon = this.Hour_rotation.icon,
                    iconPosX = this.Hour_rotation.iconPosX,
                    iconPosY = this.Hour_rotation.iconPosY,
                    negative_image = this.Hour_rotation.negative_image,
                    invalid_image = this.Hour_rotation.invalid_image,
                    dot_image = this.Hour_rotation.dot_image,
                    unit_in_alignment = this.Hour_rotation.unit_in_alignment,

                    position = this.Hour_rotation.position,
                    visible = this.Hour_rotation.visible,
                    show_level = this.Hour_rotation.show_level,
                    type = this.Hour_rotation.type,
                };
            }

            Text_Circle Second_circle = null;
            if (this.Second_circle != null)
            {
                Second_circle = new Text_Circle
                {
                    circle_center_X = this.Second_circle.circle_center_X,
                    circle_center_Y = this.Second_circle.circle_center_Y,
                    radius = this.Second_circle.radius,
                    angle = this.Second_circle.angle,
                    char_space_angle = this.Second_circle.char_space_angle,
                    zero = this.Second_circle.zero,
                    img_First = this.Second_circle.img_First,
                    unit = this.Second_circle.unit,
                    imperial_unit = this.Second_circle.imperial_unit,
                    dot_image = this.Second_circle.dot_image,
                    error_image = this.Second_circle.error_image,
                    //error_width = this.Second_circle.error_width,
                    vertical_alignment = this.Second_circle.vertical_alignment,
                    horizontal_alignment = this.Second_circle.horizontal_alignment,
                    reverse_direction = this.Second_circle.reverse_direction,
                    unit_in_alignment = this.Second_circle.unit_in_alignment,

                    position = this.Second_circle.position,
                    visible = this.Second_circle.visible,
                    show_level = this.Second_circle.show_level,
                    type = this.Second_circle.type,
                };
            }

            Text_Circle Minute_circle = null;
            if (this.Minute_circle != null)
            {
                Minute_circle = new Text_Circle
                {
                    circle_center_X = this.Minute_circle.circle_center_X,
                    circle_center_Y = this.Minute_circle.circle_center_Y,
                    radius = this.Minute_circle.radius,
                    angle = this.Minute_circle.angle,
                    char_space_angle = this.Minute_circle.char_space_angle,
                    zero = this.Minute_circle.zero,
                    img_First = this.Minute_circle.img_First,
                    unit = this.Minute_circle.unit,
                    imperial_unit = this.Minute_circle.imperial_unit,
                    dot_image = this.Minute_circle.dot_image,
                    error_image = this.Minute_circle.error_image,
                    //error_width = this.Minute_circle.error_width,
                    vertical_alignment = this.Minute_circle.vertical_alignment,
                    horizontal_alignment = this.Minute_circle.horizontal_alignment,
                    reverse_direction = this.Minute_circle.reverse_direction,
                    unit_in_alignment = this.Minute_circle.unit_in_alignment,

                    position = this.Minute_circle.position,
                    visible = this.Minute_circle.visible,
                    show_level = this.Second_circle.show_level,
                    type = this.Minute_circle.type,
                };
            }

            Text_Circle Hour_circle = null;
            if (this.Hour_circle != null)
            {
                Hour_circle = new Text_Circle
                {
                    circle_center_X = this.Hour_circle.circle_center_X,
                    circle_center_Y = this.Hour_circle.circle_center_Y,
                    radius = this.Hour_circle.radius,
                    angle = this.Hour_circle.angle,
                    char_space_angle = this.Hour_circle.char_space_angle,
                    zero = this.Hour_circle.zero,
                    img_First = this.Hour_circle.img_First,
                    unit = this.Hour_circle.unit,
                    imperial_unit = this.Hour_circle.imperial_unit,
                    dot_image = this.Hour_circle.dot_image,
                    error_image = this.Hour_circle.error_image,
                    //error_width = this.Hour_circle.error_width,
                    vertical_alignment = this.Hour_circle.vertical_alignment,
                    horizontal_alignment = this.Hour_circle.horizontal_alignment,
                    reverse_direction = this.Hour_circle.reverse_direction,
                    unit_in_alignment = this.Hour_circle.unit_in_alignment,

                    position = this.Hour_circle.position,
                    visible = this.Hour_circle.visible,
                    show_level = this.Second_circle.show_level,
                    type = this.Hour_circle.type,
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

                Second_Font = Second_Font,
                Minute_Font = Minute_Font,
                Hour_Font = Hour_Font,
                Hour_min_Font = Hour_min_Font,
                Hour_min_sec_Font = Hour_min_sec_Font,

                Second_rotation = Second_rotation,
                Minute_rotation = Minute_rotation,
                Hour_rotation = Hour_rotation,

                Second_circle = Second_circle,
                Minute_circle = Minute_circle,
                Hour_circle = Hour_circle,
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

        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

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

    public class ElementAnalogTimePro : ICloneable
    {
        public string elementName = "AnalogTimePro";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_POINTER Second { get; set; }
        public hmUI_widget_IMG_POINTER Minute { get; set; }
        public hmUI_widget_IMG_POINTER Hour { get; set; }

        public Smooth_Second SmoothSecond { get; set; }

        public bool Format_24hour { get; set; } = false;

        public int Format_24hour_position = -1;


        /// <summary>Основной экран или AOD</summary>
        public string show_level = "";

        public object Clone()
        {
            Smooth_Second SmoothSecond = null;
            if (this.SmoothSecond != null)
            {
                SmoothSecond = new Smooth_Second
                {
                    position = this.SmoothSecond.position,
                    enable = this.SmoothSecond.enable,
                    type = this.SmoothSecond.type,
                    fps = this.SmoothSecond.fps,
                };
            }

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

            return new ElementAnalogTimePro
            {
                elementName = this.elementName,
                visible = this.visible,
                Format_24hour = this.Format_24hour,
                Format_24hour_position = this.Format_24hour_position,
                SmoothSecond = SmoothSecond,
                Second = Second,
                Minute = Minute,
                Hour = Hour,
            };
        }
    }

    public class ElementEditablePointers
    {
        public string elementName = "ElementEditablePointers";

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        /// <summary>Выбраный набор стрелок</summary>
        public int selected_pointers = 0;

        /// <summary>Перечень наборов стрелок</summary>
        public List<PointersList> config { get; set; }

        /// <summary>Перечень картинок для предпросмотра стрелок</summary>
        //public List<string> PointersPreviewList { get; set; }

        /// <summary>Фон пояснительной даписи</summary>
        public string tips_bg { get; set; }

        /// <summary>Координаты пояснительной даписи</summary>
        public int tips_x { get; set; }

        /// <summary>Координаты пояснительной даписи</summary>
        public int tips_y { get; set; }

        /// <summary>Рамка выделения</summary>
        public string fg { get; set; }

        /// <summary>Отображать секунды в режиме АОД</summary>
        public bool AOD_show { get; set; } = false;

        /// <summary>Отображать в режиме редактирования</summary>
        public bool showEeditMode { get; set; } = false;

        /// <summary>Отображать в режиме редактирования</summary>
        public hmUI_widget_IMG cover { get; set; }
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_TEXT Day_Month_Font { get; set; }
        public hmUI_widget_TEXT Day_Month_Year_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_TEXT Day_Month_Font = null;
            if (this.Day_Month_Font != null)
            {
                Day_Month_Font = new hmUI_widget_TEXT
                {
                    x = this.Day_Month_Font.x,
                    y = this.Day_Month_Font.y,
                    w = this.Day_Month_Font.w,
                    h = this.Day_Month_Font.h,
                    color = this.Day_Month_Font.color,
                    align_h = this.Day_Month_Font.align_h,
                    align_v = this.Day_Month_Font.align_v,
                    text_size = this.Day_Month_Font.text_size,
                    text_style = this.Day_Month_Font.text_style,
                    line_space = this.Day_Month_Font.line_space,
                    char_space = this.Day_Month_Font.char_space,
                    font = this.Day_Month_Font.font,
                    padding = this.Day_Month_Font.padding,
                    unit_type = this.Day_Month_Font.unit_type,

                    position = this.Day_Month_Font.position,
                    visible = this.Day_Month_Font.visible,
                    show_level = this.Day_Month_Font.show_level,
                    type = this.Day_Month_Font.type,
                };
            }

            hmUI_widget_TEXT Day_Month_Year_Font = null;
            if (this.Day_Month_Year_Font != null)
            {
                Day_Month_Year_Font = new hmUI_widget_TEXT
                {
                    x = this.Day_Month_Year_Font.x,
                    y = this.Day_Month_Year_Font.y,
                    w = this.Day_Month_Year_Font.w,
                    h = this.Day_Month_Year_Font.h,
                    color = this.Day_Month_Year_Font.color,
                    align_h = this.Day_Month_Year_Font.align_h,
                    align_v = this.Day_Month_Year_Font.align_v,
                    text_size = this.Day_Month_Year_Font.text_size,
                    text_style = this.Day_Month_Year_Font.text_style,
                    line_space = this.Day_Month_Year_Font.line_space,
                    char_space = this.Day_Month_Year_Font.char_space,
                    font = this.Day_Month_Year_Font.font,
                    padding = this.Day_Month_Year_Font.padding,
                    unit_type = this.Day_Month_Year_Font.unit_type,

                    position = this.Day_Month_Year_Font.position,
                    visible = this.Day_Month_Year_Font.visible,
                    show_level = this.Day_Month_Year_Font.show_level,
                    type = this.Day_Month_Year_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
                };
            }

            return new ElementDateDay
            {
                elementName = this.elementName,
                visible = this.visible,
                Pointer = Pointer,
                Number = Number,
                Number_Font = Number_Font,
                Day_Month_Font = Day_Month_Font,
                Day_Month_Year_Font = Day_Month_Year_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_TEXT Month_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_TEXT Month_Font = null;
            if (this.Month_Font != null)
            {
                Month_Font = new hmUI_widget_TEXT
                {
                    x = this.Month_Font.x,
                    y = this.Month_Font.y,
                    w = this.Month_Font.w,
                    h = this.Month_Font.h,
                    color = this.Month_Font.color,
                    align_h = this.Month_Font.align_h,
                    align_v = this.Month_Font.align_v,
                    text_size = this.Month_Font.text_size,
                    text_style = this.Month_Font.text_style,
                    line_space = this.Month_Font.line_space,
                    char_space = this.Month_Font.char_space,
                    font = this.Month_Font.font,
                    padding = this.Month_Font.padding,
                    unit_type = this.Month_Font.unit_type,
                    unit_string = this.Month_Font.unit_string,

                    position = this.Month_Font.position,
                    visible = this.Month_Font.visible,
                    show_level = this.Month_Font.show_level,
                    type = this.Month_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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
                Number_Font = Number_Font,
                Month_Font = Month_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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

            return new ElementDateYear
            {
                elementName = this.elementName,
                visible = this.visible,
                Number = Number,
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Icon = Icon,
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
        public hmUI_widget_TEXT DayOfWeek_Font { get; set; }

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

            hmUI_widget_TEXT DayOfWeek_Font = null;
            if (this.DayOfWeek_Font != null)
            {
                DayOfWeek_Font = new hmUI_widget_TEXT
                {
                    x = this.DayOfWeek_Font.x,
                    y = this.DayOfWeek_Font.y,
                    w = this.DayOfWeek_Font.w,
                    h = this.DayOfWeek_Font.h,
                    color = this.DayOfWeek_Font.color,
                    align_h = this.DayOfWeek_Font.align_h,
                    align_v = this.DayOfWeek_Font.align_v,
                    text_size = this.DayOfWeek_Font.text_size,
                    text_style = this.DayOfWeek_Font.text_style,
                    line_space = this.DayOfWeek_Font.line_space,
                    char_space = this.DayOfWeek_Font.char_space,
                    font = this.DayOfWeek_Font.font,
                    padding = this.DayOfWeek_Font.padding,
                    unit_type = this.DayOfWeek_Font.unit_type,
                    unit_string = this.DayOfWeek_Font.unit_string,

                    position = this.DayOfWeek_Font.position,
                    visible = this.DayOfWeek_Font.visible,
                    show_level = this.DayOfWeek_Font.show_level,
                    type = this.DayOfWeek_Font.type,
                };
            }


            return new ElementDateWeek
            {
                elementName = this.elementName,
                visible = this.visible,
                Pointer = Pointer,
                Images = Images,
                DayOfWeek_Font = DayOfWeek_Font,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_TEXT Number_Target_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation_Target { get; set; }
        public Text_Circle Text_circle_Target { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding=this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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
                    angle = this.Number_Target.angle,
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

            hmUI_widget_TEXT Number_Target_Font = null;
            if (this.Number_Target_Font != null)
            {
                Number_Target_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Target_Font.x,
                    y = this.Number_Target_Font.y,
                    w = this.Number_Target_Font.w,
                    h = this.Number_Target_Font.h,
                    color = this.Number_Target_Font.color,
                    align_h = this.Number_Target_Font.align_h,
                    align_v = this.Number_Target_Font.align_v,
                    text_size = this.Number_Target_Font.text_size,
                    text_style = this.Number_Target_Font.text_style,
                    line_space = this.Number_Target_Font.line_space,
                    char_space = this.Number_Target_Font.char_space,
                    font = this.Number_Target_Font.font,
                    padding = this.Number_Target_Font.padding,
                    unit_type = this.Number_Target_Font.unit_type,

                    position = this.Number_Target_Font.position,
                    visible = this.Number_Target_Font.visible,
                    show_level = this.Number_Target_Font.show_level,
                    type = this.Number_Target_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation_Target = null;
            if (this.Text_rotation_Target != null)
            {
                Text_rotation_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation_Target.imageX,
                    imageY = this.Text_rotation_Target.imageY,
                    space = this.Text_rotation_Target.space,
                    angle = this.Text_rotation_Target.angle,
                    zero = this.Text_rotation_Target.zero,
                    align = this.Text_rotation_Target.align,
                    img_First = this.Text_rotation_Target.img_First,
                    unit = this.Text_rotation_Target.unit,
                    imperial_unit = this.Text_rotation_Target.imperial_unit,
                    icon = this.Text_rotation_Target.icon,
                    iconPosX = this.Text_rotation_Target.iconPosX,
                    iconPosY = this.Text_rotation_Target.iconPosY,
                    negative_image = this.Text_rotation_Target.negative_image,
                    invalid_image = this.Text_rotation_Target.invalid_image,
                    dot_image = this.Text_rotation_Target.dot_image,
                    unit_in_alignment = this.Text_rotation_Target.unit_in_alignment,

                    position = this.Text_rotation_Target.position,
                    visible = this.Text_rotation_Target.visible,
                    show_level = this.Text_rotation_Target.show_level,
                    type = this.Text_rotation_Target.type,
                };
            }

            Text_Circle Text_circle_Target = null;
            if (this.Text_circle_Target != null)
            {
                Text_circle_Target = new Text_Circle
                {
                    circle_center_X = this.Text_circle_Target.circle_center_X,
                    circle_center_Y = this.Text_circle_Target.circle_center_Y,
                    radius = this.Text_circle_Target.radius,
                    angle = this.Text_circle_Target.angle,
                    char_space_angle = this.Text_circle_Target.char_space_angle,
                    zero = this.Text_circle_Target.zero,
                    img_First = this.Text_circle_Target.img_First,
                    unit = this.Text_circle_Target.unit,
                    imperial_unit = this.Text_circle_Target.imperial_unit,
                    dot_image = this.Text_circle_Target.dot_image,
                    error_image = this.Text_circle_Target.error_image,
                    vertical_alignment = this.Text_circle_Target.vertical_alignment,
                    horizontal_alignment = this.Text_circle_Target.horizontal_alignment,
                    reverse_direction = this.Text_circle_Target.reverse_direction,
                    unit_in_alignment = this.Text_circle_Target.unit_in_alignment,

                    position = this.Text_circle_Target.position,
                    visible = this.Text_circle_Target.visible,
                    show_level = this.Text_circle_Target.show_level,
                    type = this.Text_circle_Target.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Number_Target = Number_Target,
                Number_Target_Font = Number_Target_Font,
                Text_rotation_Target = Text_rotation_Target,
                Text_circle_Target = Text_circle_Target,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_TEXT Number_Target_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation_Target { get; set; }
        public Text_Circle Text_circle_Target { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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
                    angle = this.Number_Target.angle,
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

            hmUI_widget_TEXT Number_Target_Font = null;
            if (this.Number_Target_Font != null)
            {
                Number_Target_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Target_Font.x,
                    y = this.Number_Target_Font.y,
                    w = this.Number_Target_Font.w,
                    h = this.Number_Target_Font.h,
                    color = this.Number_Target_Font.color,
                    align_h = this.Number_Target_Font.align_h,
                    align_v = this.Number_Target_Font.align_v,
                    text_size = this.Number_Target_Font.text_size,
                    text_style = this.Number_Target_Font.text_style,
                    line_space = this.Number_Target_Font.line_space,
                    char_space = this.Number_Target_Font.char_space,
                    font = this.Number_Target_Font.font,
                    padding = this.Number_Target_Font.padding,
                    unit_type = this.Number_Target_Font.unit_type,

                    position = this.Number_Target_Font.position,
                    visible = this.Number_Target_Font.visible,
                    show_level = this.Number_Target_Font.show_level,
                    type = this.Number_Target_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation_Target = null;
            if (this.Text_rotation_Target != null)
            {
                Text_rotation_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation_Target.imageX,
                    imageY = this.Text_rotation_Target.imageY,
                    space = this.Text_rotation_Target.space,
                    angle = this.Text_rotation_Target.angle,
                    zero = this.Text_rotation_Target.zero,
                    align = this.Text_rotation_Target.align,
                    img_First = this.Text_rotation_Target.img_First,
                    unit = this.Text_rotation_Target.unit,
                    imperial_unit = this.Text_rotation_Target.imperial_unit,
                    icon = this.Text_rotation_Target.icon,
                    iconPosX = this.Text_rotation_Target.iconPosX,
                    iconPosY = this.Text_rotation_Target.iconPosY,
                    negative_image = this.Text_rotation_Target.negative_image,
                    invalid_image = this.Text_rotation_Target.invalid_image,
                    dot_image = this.Text_rotation_Target.dot_image,
                    unit_in_alignment = this.Text_rotation_Target.unit_in_alignment,

                    position = this.Text_rotation_Target.position,
                    visible = this.Text_rotation_Target.visible,
                    show_level = this.Text_rotation_Target.show_level,
                    type = this.Text_rotation_Target.type,
                };
            }

            Text_Circle Text_circle_Target = null;
            if (this.Text_circle_Target != null)
            {
                Text_circle_Target = new Text_Circle
                {
                    circle_center_X = this.Text_circle_Target.circle_center_X,
                    circle_center_Y = this.Text_circle_Target.circle_center_Y,
                    radius = this.Text_circle_Target.radius,
                    angle = this.Text_circle_Target.angle,
                    char_space_angle = this.Text_circle_Target.char_space_angle,
                    zero = this.Text_circle_Target.zero,
                    img_First = this.Text_circle_Target.img_First,
                    unit = this.Text_circle_Target.unit,
                    imperial_unit = this.Text_circle_Target.imperial_unit,
                    dot_image = this.Text_circle_Target.dot_image,
                    error_image = this.Text_circle_Target.error_image,
                    vertical_alignment = this.Text_circle_Target.vertical_alignment,
                    horizontal_alignment = this.Text_circle_Target.horizontal_alignment,
                    reverse_direction = this.Text_circle_Target.reverse_direction,
                    unit_in_alignment = this.Text_circle_Target.unit_in_alignment,

                    position = this.Text_circle_Target.position,
                    visible = this.Text_circle_Target.visible,
                    show_level = this.Text_circle_Target.show_level,
                    type = this.Text_circle_Target.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Number_Target = Number_Target,
                Number_Target_Font = Number_Target_Font,
                Text_rotation_Target = Text_rotation_Target,
                Text_circle_Target = Text_circle_Target,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
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
        //public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_TEXT Number_Target_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation_Target { get; set; }
        public Text_Circle Text_circle_Target { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            //hmUI_widget_TEXT Number_Font = null;
            //if (this.Number_Font != null)
            //{
            //    Number_Font = new hmUI_widget_TEXT
            //    {
            //        x = this.Number_Font.x,
            //        y = this.Number_Font.y,
            //        w = this.Number_Font.w,
            //        h = this.Number_Font.h,
            //        color = this.Number_Font.color,
            //        align_h = this.Number_Font.align_h,
            //        align_v = this.Number_Font.align_v,
            //        text_size = this.Number_Font.text_size,
            //        text_style = this.Number_Font.text_style,
            //        line_space = this.Number_Font.line_space,
            //        char_space = this.Number_Font.char_space,
            //        font = this.Number_Font.font,
            //        padding = this.Number_Font.padding,
            //        unit_type = this.Number_Font.unit_type,

            //        position = this.Number_Font.position,
            //        visible = this.Number_Font.visible,
            //        show_level = this.Number_Font.show_level,
            //        type = this.Number_Font.type,
            //    };
            //}

            hmUI_widget_IMG_NUMBER Number_Target = null;
            if (this.Number_Target != null)
            {
                Number_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Number_Target.imageX,
                    imageY = this.Number_Target.imageY,
                    space = this.Number_Target.space,
                    angle = this.Number_Target.angle,
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

            hmUI_widget_TEXT Number_Target_Font = null;
            if (this.Number_Target_Font != null)
            {
                Number_Target_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Target_Font.x,
                    y = this.Number_Target_Font.y,
                    w = this.Number_Target_Font.w,
                    h = this.Number_Target_Font.h,
                    color = this.Number_Target_Font.color,
                    align_h = this.Number_Target_Font.align_h,
                    align_v = this.Number_Target_Font.align_v,
                    text_size = this.Number_Target_Font.text_size,
                    text_style = this.Number_Target_Font.text_style,
                    line_space = this.Number_Target_Font.line_space,
                    char_space = this.Number_Target_Font.char_space,
                    font = this.Number_Target_Font.font,
                    padding = this.Number_Target_Font.padding,
                    unit_type = this.Number_Target_Font.unit_type,

                    position = this.Number_Target_Font.position,
                    visible = this.Number_Target_Font.visible,
                    show_level = this.Number_Target_Font.show_level,
                    type = this.Number_Target_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation_Target = null;
            if (this.Text_rotation_Target != null)
            {
                Text_rotation_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation_Target.imageX,
                    imageY = this.Text_rotation_Target.imageY,
                    space = this.Text_rotation_Target.space,
                    angle = this.Text_rotation_Target.angle,
                    zero = this.Text_rotation_Target.zero,
                    align = this.Text_rotation_Target.align,
                    img_First = this.Text_rotation_Target.img_First,
                    unit = this.Text_rotation_Target.unit,
                    imperial_unit = this.Text_rotation_Target.imperial_unit,
                    icon = this.Text_rotation_Target.icon,
                    iconPosX = this.Text_rotation_Target.iconPosX,
                    iconPosY = this.Text_rotation_Target.iconPosY,
                    negative_image = this.Text_rotation_Target.negative_image,
                    invalid_image = this.Text_rotation_Target.invalid_image,
                    dot_image = this.Text_rotation_Target.dot_image,
                    unit_in_alignment = this.Text_rotation_Target.unit_in_alignment,

                    position = this.Text_rotation_Target.position,
                    visible = this.Text_rotation_Target.visible,
                    show_level = this.Text_rotation_Target.show_level,
                    type = this.Text_rotation_Target.type,
                };
            }

            Text_Circle Text_circle_Target = null;
            if (this.Text_circle_Target != null)
            {
                Text_circle_Target = new Text_Circle
                {
                    circle_center_X = this.Text_circle_Target.circle_center_X,
                    circle_center_Y = this.Text_circle_Target.circle_center_Y,
                    radius = this.Text_circle_Target.radius,
                    angle = this.Text_circle_Target.angle,
                    char_space_angle = this.Text_circle_Target.char_space_angle,
                    zero = this.Text_circle_Target.zero,
                    img_First = this.Text_circle_Target.img_First,
                    unit = this.Text_circle_Target.unit,
                    imperial_unit = this.Text_circle_Target.imperial_unit,
                    dot_image = this.Text_circle_Target.dot_image,
                    error_image = this.Text_circle_Target.error_image,
                    vertical_alignment = this.Text_circle_Target.vertical_alignment,
                    horizontal_alignment = this.Text_circle_Target.horizontal_alignment,
                    reverse_direction = this.Text_circle_Target.reverse_direction,
                    unit_in_alignment = this.Text_circle_Target.unit_in_alignment,

                    position = this.Text_circle_Target.position,
                    visible = this.Text_circle_Target.visible,
                    show_level = this.Text_circle_Target.show_level,
                    type = this.Text_circle_Target.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                //Number_Font = Number_Font,
                Number_Target = Number_Target,
                Number_Target_Font = Number_Target_Font,
                Text_rotation_Target = Text_rotation_Target,
                Text_circle_Target = Text_circle_Target,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    //image_width = this.Text_circle.image_width,
                    //image_height = this.Text_circle.image_height,
                    unit = this.Text_circle.unit,
                    //unit_width = this.Text_circle.unit_width,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    //dot_image_width = this.Text_circle.dot_image_width,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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

            return new ElementDistance
            {
                elementName = this.elementName,
                visible = this.visible,

                Number = Number,
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Icon = Icon,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation_Target { get; set; }
        public hmUI_widget_TEXT Number_Target_Font { get; set; }
        public Text_Circle Text_circle_Target { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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
                    angle = this.Number_Target.angle,
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

            hmUI_widget_TEXT Number_Target_Font = null;
            if (this.Number_Target_Font != null)
            {
                Number_Target_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Target_Font.x,
                    y = this.Number_Target_Font.y,
                    w = this.Number_Target_Font.w,
                    h = this.Number_Target_Font.h,
                    color = this.Number_Target_Font.color,
                    align_h = this.Number_Target_Font.align_h,
                    align_v = this.Number_Target_Font.align_v,
                    text_size = this.Number_Target_Font.text_size,
                    text_style = this.Number_Target_Font.text_style,
                    line_space = this.Number_Target_Font.line_space,
                    char_space = this.Number_Target_Font.char_space,
                    font = this.Number_Target_Font.font,
                    padding = this.Number_Target_Font.padding,
                    unit_type = this.Number_Target_Font.unit_type,

                    position = this.Number_Target_Font.position,
                    visible = this.Number_Target_Font.visible,
                    show_level = this.Number_Target_Font.show_level,
                    type = this.Number_Target_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation_Target = null;
            if (this.Text_rotation_Target != null)
            {
                Text_rotation_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation_Target.imageX,
                    imageY = this.Text_rotation_Target.imageY,
                    space = this.Text_rotation_Target.space,
                    angle = this.Text_rotation_Target.angle,
                    zero = this.Text_rotation_Target.zero,
                    align = this.Text_rotation_Target.align,
                    img_First = this.Text_rotation_Target.img_First,
                    unit = this.Text_rotation_Target.unit,
                    imperial_unit = this.Text_rotation_Target.imperial_unit,
                    icon = this.Text_rotation_Target.icon,
                    iconPosX = this.Text_rotation_Target.iconPosX,
                    iconPosY = this.Text_rotation_Target.iconPosY,
                    negative_image = this.Text_rotation_Target.negative_image,
                    invalid_image = this.Text_rotation_Target.invalid_image,
                    dot_image = this.Text_rotation_Target.dot_image,
                    unit_in_alignment = this.Text_rotation_Target.unit_in_alignment,

                    position = this.Text_rotation_Target.position,
                    visible = this.Text_rotation_Target.visible,
                    show_level = this.Text_rotation_Target.show_level,
                    type = this.Text_rotation_Target.type,
                };
            }

            Text_Circle Text_circle_Target = null;
            if (this.Text_circle_Target != null)
            {
                Text_circle_Target = new Text_Circle
                {
                    circle_center_X = this.Text_circle_Target.circle_center_X,
                    circle_center_Y = this.Text_circle_Target.circle_center_Y,
                    radius = this.Text_circle_Target.radius,
                    angle = this.Text_circle_Target.angle,
                    char_space_angle = this.Text_circle_Target.char_space_angle,
                    zero = this.Text_circle_Target.zero,
                    img_First = this.Text_circle_Target.img_First,
                    unit = this.Text_circle_Target.unit,
                    imperial_unit = this.Text_circle_Target.imperial_unit,
                    dot_image = this.Text_circle_Target.dot_image,
                    error_image = this.Text_circle_Target.error_image,
                    vertical_alignment = this.Text_circle_Target.vertical_alignment,
                    horizontal_alignment = this.Text_circle_Target.horizontal_alignment,
                    reverse_direction = this.Text_circle_Target.reverse_direction,
                    unit_in_alignment = this.Text_circle_Target.unit_in_alignment,

                    position = this.Text_circle_Target.position,
                    visible = this.Text_circle_Target.visible,
                    show_level = this.Text_circle_Target.show_level,
                    type = this.Text_circle_Target.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Number_Target = Number_Target,
                Number_Target_Font = Number_Target_Font,
                Text_rotation_Target = Text_rotation_Target,
                Text_circle_Target = Text_circle_Target,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_TEXT Number_Target_Font { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
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
                    angle = this.Number_Target.angle,
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

            hmUI_widget_TEXT Number_Target_Font = null;
            if (this.Number_Target_Font != null)
            {
                Number_Target_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Target_Font.x,
                    y = this.Number_Target_Font.y,
                    w = this.Number_Target_Font.w,
                    h = this.Number_Target_Font.h,
                    color = this.Number_Target_Font.color,
                    align_h = this.Number_Target_Font.align_h,
                    align_v = this.Number_Target_Font.align_v,
                    text_size = this.Number_Target_Font.text_size,
                    text_style = this.Number_Target_Font.text_style,
                    line_space = this.Number_Target_Font.line_space,
                    char_space = this.Number_Target_Font.char_space,
                    font = this.Number_Target_Font.font,
                    padding = this.Number_Target_Font.padding,
                    unit_type = this.Number_Target_Font.unit_type,

                    position = this.Number_Target_Font.position,
                    visible = this.Number_Target_Font.visible,
                    show_level = this.Number_Target_Font.show_level,
                    type = this.Number_Target_Font.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                Number_Font = Number_Font,
                Number_Target = Number_Target,
                Number_Target_Font = Number_Target_Font,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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

            return new ElementSpO2
            {
                elementName = this.elementName,
                visible = this.visible,

                Number = Number,
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Icon = Icon,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
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
                Number_Font = Number_Font,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_TEXT Number_Target_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation_Target { get; set; }
        public Text_Circle Text_circle_Target { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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
                    angle = this.Number_Target.angle,
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

            hmUI_widget_TEXT Number_Target_Font = null;
            if (this.Number_Target_Font != null)
            {
                Number_Target_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Target_Font.x,
                    y = this.Number_Target_Font.y,
                    w = this.Number_Target_Font.w,
                    h = this.Number_Target_Font.h,
                    color = this.Number_Target_Font.color,
                    align_h = this.Number_Target_Font.align_h,
                    align_v = this.Number_Target_Font.align_v,
                    text_size = this.Number_Target_Font.text_size,
                    text_style = this.Number_Target_Font.text_style,
                    line_space = this.Number_Target_Font.line_space,
                    char_space = this.Number_Target_Font.char_space,
                    font = this.Number_Target_Font.font,
                    padding = this.Number_Target_Font.padding,
                    unit_type = this.Number_Target_Font.unit_type,

                    position = this.Number_Target_Font.position,
                    visible = this.Number_Target_Font.visible,
                    show_level = this.Number_Target_Font.show_level,
                    type = this.Number_Target_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation_Target = null;
            if (this.Text_rotation_Target != null)
            {
                Text_rotation_Target = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation_Target.imageX,
                    imageY = this.Text_rotation_Target.imageY,
                    space = this.Text_rotation_Target.space,
                    angle = this.Text_rotation_Target.angle,
                    zero = this.Text_rotation_Target.zero,
                    align = this.Text_rotation_Target.align,
                    img_First = this.Text_rotation_Target.img_First,
                    unit = this.Text_rotation_Target.unit,
                    imperial_unit = this.Text_rotation_Target.imperial_unit,
                    icon = this.Text_rotation_Target.icon,
                    iconPosX = this.Text_rotation_Target.iconPosX,
                    iconPosY = this.Text_rotation_Target.iconPosY,
                    negative_image = this.Text_rotation_Target.negative_image,
                    invalid_image = this.Text_rotation_Target.invalid_image,
                    dot_image = this.Text_rotation_Target.dot_image,
                    unit_in_alignment = this.Text_rotation_Target.unit_in_alignment,

                    position = this.Text_rotation_Target.position,
                    visible = this.Text_rotation_Target.visible,
                    show_level = this.Text_rotation_Target.show_level,
                    type = this.Text_rotation_Target.type,
                };
            }

            Text_Circle Text_circle_Target = null;
            if (this.Text_circle_Target != null)
            {
                Text_circle_Target = new Text_Circle
                {
                    circle_center_X = this.Text_circle_Target.circle_center_X,
                    circle_center_Y = this.Text_circle_Target.circle_center_Y,
                    radius = this.Text_circle_Target.radius,
                    angle = this.Text_circle_Target.angle,
                    char_space_angle = this.Text_circle_Target.char_space_angle,
                    zero = this.Text_circle_Target.zero,
                    img_First = this.Text_circle_Target.img_First,
                    unit = this.Text_circle_Target.unit,
                    imperial_unit = this.Text_circle_Target.imperial_unit,
                    dot_image = this.Text_circle_Target.dot_image,
                    error_image = this.Text_circle_Target.error_image,
                    vertical_alignment = this.Text_circle_Target.vertical_alignment,
                    horizontal_alignment = this.Text_circle_Target.horizontal_alignment,
                    reverse_direction = this.Text_circle_Target.reverse_direction,
                    unit_in_alignment = this.Text_circle_Target.unit_in_alignment,

                    position = this.Text_circle_Target.position,
                    visible = this.Text_circle_Target.visible,
                    show_level = this.Text_circle_Target.show_level,
                    type = this.Text_circle_Target.type,
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
                    line_cap = this.Circle_Scale.line_cap,
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
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Number_Target = Number_Target,
                Number_Target_Font = Number_Target_Font,
                Text_rotation_Target = Text_rotation_Target,
                Text_circle_Target = Text_circle_Target,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Min { get; set; }
        public hmUI_widget_TEXT Number_Min_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_Min_rotation { get; set; }
        public Text_Circle Text_Min_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Max { get; set; }
        public hmUI_widget_TEXT Number_Max_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_Max_rotation { get; set; }
        public Text_Circle Text_Max_circle { get; set; }
        public hmUI_widget_TEXT Number_Min_Max_Font { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
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
                    angle = this.Number_Min.angle,
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

            hmUI_widget_TEXT Number_Min_Font = null;
            if (this.Number_Min_Font != null)
            {
                Number_Min_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Min_Font.x,
                    y = this.Number_Min_Font.y,
                    w = this.Number_Min_Font.w,
                    h = this.Number_Min_Font.h,
                    color = this.Number_Min_Font.color,
                    align_h = this.Number_Min_Font.align_h,
                    align_v = this.Number_Min_Font.align_v,
                    text_size = this.Number_Min_Font.text_size,
                    text_style = this.Number_Min_Font.text_style,
                    line_space = this.Number_Min_Font.line_space,
                    char_space = this.Number_Min_Font.char_space,
                    font = this.Number_Min_Font.font,
                    padding = this.Number_Min_Font.padding,
                    unit_type = this.Number_Min_Font.unit_type,

                    position = this.Number_Min_Font.position,
                    visible = this.Number_Min_Font.visible,
                    show_level = this.Number_Min_Font.show_level,
                    type = this.Number_Min_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_Min_rotation = null;
            if (this.Text_Min_rotation != null)
            {
                Text_Min_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_Min_rotation.imageX,
                    imageY = this.Text_Min_rotation.imageY,
                    space = this.Text_Min_rotation.space,
                    angle = this.Text_Min_rotation.angle,
                    zero = this.Text_Min_rotation.zero,
                    align = this.Text_Min_rotation.align,
                    img_First = this.Text_Min_rotation.img_First,
                    unit = this.Text_Min_rotation.unit,
                    imperial_unit = this.Text_Min_rotation.imperial_unit,
                    icon = this.Text_Min_rotation.icon,
                    iconPosX = this.Text_Min_rotation.iconPosX,
                    iconPosY = this.Text_Min_rotation.iconPosY,
                    negative_image = this.Text_Min_rotation.negative_image,
                    invalid_image = this.Text_Min_rotation.invalid_image,
                    dot_image = this.Text_Min_rotation.dot_image,
                    unit_in_alignment = this.Text_Min_rotation.unit_in_alignment,

                    position = this.Text_Min_rotation.position,
                    visible = this.Text_Min_rotation.visible,
                    show_level = this.Text_Min_rotation.show_level,
                    type = this.Text_Min_rotation.type,
                };
            }

            Text_Circle Text_Min_circle = null;
            if (this.Text_Min_circle != null)
            {
                Text_Min_circle = new Text_Circle
                {
                    circle_center_X = this.Text_Min_circle.circle_center_X,
                    circle_center_Y = this.Text_Min_circle.circle_center_Y,
                    radius = this.Text_Min_circle.radius,
                    angle = this.Text_Min_circle.angle,
                    char_space_angle = this.Text_Min_circle.char_space_angle,
                    zero = this.Text_Min_circle.zero,
                    img_First = this.Text_Min_circle.img_First,
                    //image_width = this.Text_Min_circle.image_width,
                    //image_height = this.Text_Min_circle.image_height,
                    unit = this.Text_Min_circle.unit,
                    //unit_width = this.Text_Min_circle.unit_width,
                    imperial_unit = this.Text_Min_circle.imperial_unit,
                    dot_image = this.Text_Min_circle.dot_image,
                    //dot_image_width = this.Text_Min_circle.dot_image_width,
                    error_image = this.Text_Min_circle.error_image,
                    //error_width = this.Text_Min_circle.error_width,
                    vertical_alignment = this.Text_Min_circle.vertical_alignment,
                    horizontal_alignment = this.Text_Min_circle.horizontal_alignment,
                    reverse_direction = this.Text_Min_circle.reverse_direction,
                    unit_in_alignment = this.Text_Min_circle.unit_in_alignment,

                    position = this.Text_Min_circle.position,
                    visible = this.Text_Min_circle.visible,
                    show_level = this.Text_Min_circle.show_level,
                    type = this.Text_Min_circle.type,
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
                    angle = this.Number_Max.angle,
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

            hmUI_widget_TEXT Number_Max_Font = null;
            if (this.Number_Max_Font != null)
            {
                Number_Max_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Max_Font.x,
                    y = this.Number_Max_Font.y,
                    w = this.Number_Max_Font.w,
                    h = this.Number_Max_Font.h,
                    color = this.Number_Max_Font.color,
                    align_h = this.Number_Max_Font.align_h,
                    align_v = this.Number_Max_Font.align_v,
                    text_size = this.Number_Max_Font.text_size,
                    text_style = this.Number_Max_Font.text_style,
                    line_space = this.Number_Max_Font.line_space,
                    char_space = this.Number_Max_Font.char_space,
                    font = this.Number_Max_Font.font,
                    padding = this.Number_Max_Font.padding,
                    unit_type = this.Number_Max_Font.unit_type,

                    position = this.Number_Max_Font.position,
                    visible = this.Number_Max_Font.visible,
                    show_level = this.Number_Max_Font.show_level,
                    type = this.Number_Max_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_Max_rotation = null;
            if (this.Text_Max_rotation != null)
            {
                Text_Max_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_Max_rotation.imageX,
                    imageY = this.Text_Max_rotation.imageY,
                    space = this.Text_Max_rotation.space,
                    angle = this.Text_Max_rotation.angle,
                    zero = this.Text_Max_rotation.zero,
                    align = this.Text_Max_rotation.align,
                    img_First = this.Text_Max_rotation.img_First,
                    unit = this.Text_Max_rotation.unit,
                    imperial_unit = this.Text_Max_rotation.imperial_unit,
                    icon = this.Text_Max_rotation.icon,
                    iconPosX = this.Text_Max_rotation.iconPosX,
                    iconPosY = this.Text_Max_rotation.iconPosY,
                    negative_image = this.Text_Max_rotation.negative_image,
                    invalid_image = this.Text_Max_rotation.invalid_image,
                    dot_image = this.Text_Max_rotation.dot_image,
                    unit_in_alignment = this.Text_Max_rotation.unit_in_alignment,

                    position = this.Text_Max_rotation.position,
                    visible = this.Text_Max_rotation.visible,
                    show_level = this.Text_Max_rotation.show_level,
                    type = this.Text_Max_rotation.type,
                };
            }

            Text_Circle Text_Max_circle = null;
            if (this.Text_Max_circle != null)
            {
                Text_Max_circle = new Text_Circle
                {
                    circle_center_X = this.Text_Max_circle.circle_center_X,
                    circle_center_Y = this.Text_Max_circle.circle_center_Y,
                    radius = this.Text_Max_circle.radius,
                    angle = this.Text_Max_circle.angle,
                    char_space_angle = this.Text_Max_circle.char_space_angle,
                    zero = this.Text_Max_circle.zero,
                    img_First = this.Text_Max_circle.img_First,
                    //image_width = this.Text_Max_circle.image_width,
                    //image_height = this.Text_Max_circle.image_height,
                    unit = this.Text_Max_circle.unit,
                    //unit_width = this.Text_Max_circle.unit_width,
                    imperial_unit = this.Text_Max_circle.imperial_unit,
                    dot_image = this.Text_Max_circle.dot_image,
                    //dot_image_width = this.Text_Max_circle.dot_image_width,
                    error_image = this.Text_Max_circle.error_image,
                    //error_width = this.Text_Max_circle.error_width,
                    vertical_alignment = this.Text_Max_circle.vertical_alignment,
                    horizontal_alignment = this.Text_Max_circle.horizontal_alignment,
                    reverse_direction = this.Text_Max_circle.reverse_direction,
                    unit_in_alignment = this.Text_Max_circle.unit_in_alignment,

                    position = this.Text_Max_circle.position,
                    visible = this.Text_Max_circle.visible,
                    show_level = this.Text_Max_circle.show_level,
                    type = this.Text_Max_circle.type,
                };
            }

            hmUI_widget_TEXT Number_Min_Max_Font = null;
            if (this.Number_Min_Max_Font != null)
            {
                Number_Min_Max_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Min_Max_Font.x,
                    y = this.Number_Min_Max_Font.y,
                    w = this.Number_Min_Max_Font.w,
                    h = this.Number_Min_Max_Font.h,
                    color = this.Number_Min_Max_Font.color,
                    align_h = this.Number_Min_Max_Font.align_h,
                    align_v = this.Number_Min_Max_Font.align_v,
                    text_size = this.Number_Min_Max_Font.text_size,
                    text_style = this.Number_Min_Max_Font.text_style,
                    line_space = this.Number_Min_Max_Font.line_space,
                    char_space = this.Number_Min_Max_Font.char_space,
                    font = this.Number_Min_Max_Font.font,
                    padding = this.Number_Min_Max_Font.padding,
                    unit_type = this.Number_Min_Max_Font.unit_type,

                    position = this.Number_Min_Max_Font.position,
                    visible = this.Number_Min_Max_Font.visible,
                    show_level = this.Number_Min_Max_Font.show_level,
                    type = this.Number_Min_Max_Font.type,
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
                    font = this.City_Name.font,

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
                Number_Font = Number_Font,
                Number_Min = Number_Min,
                Number_Min_Font = Number_Min_Font,
                Text_Min_rotation = Text_Min_rotation,
                Text_Min_circle = Text_Min_circle,
                Number_Max = Number_Max,
                Number_Max_Font = Number_Max_Font,
                Text_Max_rotation = Text_Max_rotation,
                Text_Max_circle = Text_Max_circle,
                Number_Min_Max_Font = Number_Min_Max_Font,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
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

            return new ElementUVIndex
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Font = Number_Font,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
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

            return new ElementHumidity
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Segments = Segments,
                Number = Number,
                Number_Font = Number_Font,
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

        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Number_Target { get; set; }
        public hmUI_widget_TEXT Number_Target_Font { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG Icon { get; set; }

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
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
                    angle = this.Number_Target.angle,
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

            hmUI_widget_TEXT Number_Target_Font = null;
            if (this.Number_Target_Font != null)
            {
                Number_Target_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Target_Font.x,
                    y = this.Number_Target_Font.y,
                    w = this.Number_Target_Font.w,
                    h = this.Number_Target_Font.h,
                    color = this.Number_Target_Font.color,
                    align_h = this.Number_Target_Font.align_h,
                    align_v = this.Number_Target_Font.align_v,
                    text_size = this.Number_Target_Font.text_size,
                    text_style = this.Number_Target_Font.text_style,
                    line_space = this.Number_Target_Font.line_space,
                    char_space = this.Number_Target_Font.char_space,
                    font = this.Number_Target_Font.font,
                    padding = this.Number_Target_Font.padding,
                    unit_type = this.Number_Target_Font.unit_type,

                    position = this.Number_Target_Font.position,
                    visible = this.Number_Target_Font.visible,
                    show_level = this.Number_Target_Font.show_level,
                    type = this.Number_Target_Font.type,
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

                Number = Number,
                Number_Font = Number_Font,
                Number_Target = Number_Target,
                Number_Target_Font = Number_Target_Font,
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
        public hmUI_widget_TEXT Sunrise_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Sunrise_rotation { get; set; }
        public Text_Circle Sunrise_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Sunset { get; set; }
        public hmUI_widget_TEXT Sunset_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Sunset_rotation { get; set; }
        public Text_Circle Sunset_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Sunset_Sunrise { get; set; }
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
                    shortcut = this.Images.shortcut,

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

            hmUI_widget_IMG_NUMBER Sunrise = null;
            if (this.Sunrise != null)
            {
                Sunrise = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunrise.imageX,
                    imageY = this.Sunrise.imageY,
                    space = this.Sunrise.space,
                    angle = this.Sunrise.angle,
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

            hmUI_widget_TEXT Sunrise_Font = null;
            if (this.Sunrise_Font != null)
            {
                Sunrise_Font = new hmUI_widget_TEXT
                {
                    x = this.Sunrise_Font.x,
                    y = this.Sunrise_Font.y,
                    w = this.Sunrise_Font.w,
                    h = this.Sunrise_Font.h,
                    color = this.Sunrise_Font.color,
                    align_h = this.Sunrise_Font.align_h,
                    align_v = this.Sunrise_Font.align_v,
                    text_size = this.Sunrise_Font.text_size,
                    text_style = this.Sunrise_Font.text_style,
                    line_space = this.Sunrise_Font.line_space,
                    char_space = this.Sunrise_Font.char_space,
                    font = this.Sunrise_Font.font,
                    padding = this.Sunrise_Font.padding,
                    unit_type = this.Sunrise_Font.unit_type,

                    position = this.Sunrise_Font.position,
                    visible = this.Sunrise_Font.visible,
                    show_level = this.Sunrise_Font.show_level,
                    type = this.Sunrise_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Sunrise_rotation = null;
            if (this.Sunrise_rotation != null)
            {
                Sunrise_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunrise_rotation.imageX,
                    imageY = this.Sunrise_rotation.imageY,
                    space = this.Sunrise_rotation.space,
                    angle = this.Sunrise_rotation.angle,
                    zero = this.Sunrise_rotation.zero,
                    align = this.Sunrise_rotation.align,
                    img_First = this.Sunrise_rotation.img_First,
                    unit = this.Sunrise_rotation.unit,
                    imperial_unit = this.Sunrise_rotation.imperial_unit,
                    icon = this.Sunrise_rotation.icon,
                    iconPosX = this.Sunrise_rotation.iconPosX,
                    iconPosY = this.Sunrise_rotation.iconPosY,
                    negative_image = this.Sunrise_rotation.negative_image,
                    invalid_image = this.Sunrise_rotation.invalid_image,
                    dot_image = this.Sunrise_rotation.dot_image,
                    unit_in_alignment = this.Sunrise_rotation.unit_in_alignment,

                    position = this.Sunrise_rotation.position,
                    visible = this.Sunrise_rotation.visible,
                    show_level = this.Sunrise_rotation.show_level,
                    type = this.Sunrise_rotation.type,
                };
            }

            Text_Circle Sunrise_circle = null;
            if (this.Sunrise_circle != null)
            {
                Sunrise_circle = new Text_Circle
                {
                    circle_center_X = this.Sunrise_circle.circle_center_X,
                    circle_center_Y = this.Sunrise_circle.circle_center_Y,
                    radius = this.Sunrise_circle.radius,
                    angle = this.Sunrise_circle.angle,
                    char_space_angle = this.Sunrise_circle.char_space_angle,
                    zero = this.Sunrise_circle.zero,
                    img_First = this.Sunrise_circle.img_First,
                    //image_width = this.Sunrise_circle.image_width,
                    //image_height = this.Sunrise_circle.image_height,
                    unit = this.Sunrise_circle.unit,
                    //unit_width = this.Sunrise_circle.unit_width,
                    imperial_unit = this.Sunrise_circle.imperial_unit,
                    dot_image = this.Sunrise_circle.dot_image,
                    //dot_image_width = this.Sunrise_circle.dot_image_width,
                    error_image = this.Sunrise_circle.error_image,
                    //error_width = this.Sunrise_circle.error_width,
                    vertical_alignment = this.Sunrise_circle.vertical_alignment,
                    horizontal_alignment = this.Sunrise_circle.horizontal_alignment,
                    reverse_direction = this.Sunrise_circle.reverse_direction,
                    unit_in_alignment = this.Sunrise_circle.unit_in_alignment,

                    position = this.Sunrise_circle.position,
                    visible = this.Sunrise_circle.visible,
                    show_level = this.Sunrise_circle.show_level,
                    type = this.Sunrise_circle.type,
                };
            }

            hmUI_widget_IMG_NUMBER Sunset = null;
            if (this.Sunset != null)
            {
                Sunset = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunset.imageX,
                    imageY = this.Sunset.imageY,
                    space = this.Sunset.space,
                    angle = this.Sunset.angle,
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

            hmUI_widget_TEXT Sunset_Font = null;
            if (this.Sunset_Font != null)
            {
                Sunset_Font = new hmUI_widget_TEXT
                {
                    x = this.Sunset_Font.x,
                    y = this.Sunset_Font.y,
                    w = this.Sunset_Font.w,
                    h = this.Sunset_Font.h,
                    color = this.Sunset_Font.color,
                    align_h = this.Sunset_Font.align_h,
                    align_v = this.Sunset_Font.align_v,
                    text_size = this.Sunset_Font.text_size,
                    text_style = this.Sunset_Font.text_style,
                    line_space = this.Sunset_Font.line_space,
                    char_space = this.Sunset_Font.char_space,
                    font = this.Sunset_Font.font,
                    padding = this.Sunset_Font.padding,
                    unit_type = this.Sunset_Font.unit_type,

                    position = this.Sunset_Font.position,
                    visible = this.Sunset_Font.visible,
                    show_level = this.Sunset_Font.show_level,
                    type = this.Sunset_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Sunset_rotation = null;
            if (this.Sunset_rotation != null)
            {
                Sunset_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunset_rotation.imageX,
                    imageY = this.Sunset_rotation.imageY,
                    space = this.Sunset_rotation.space,
                    angle = this.Sunset_rotation.angle,
                    zero = this.Sunset_rotation.zero,
                    align = this.Sunset_rotation.align,
                    img_First = this.Sunset_rotation.img_First,
                    unit = this.Sunset_rotation.unit,
                    imperial_unit = this.Sunset_rotation.imperial_unit,
                    icon = this.Sunset_rotation.icon,
                    iconPosX = this.Sunset_rotation.iconPosX,
                    iconPosY = this.Sunset_rotation.iconPosY,
                    negative_image = this.Sunset_rotation.negative_image,
                    invalid_image = this.Sunset_rotation.invalid_image,
                    dot_image = this.Sunset_rotation.dot_image,
                    unit_in_alignment = this.Sunset_rotation.unit_in_alignment,

                    position = this.Sunset_rotation.position,
                    visible = this.Sunset_rotation.visible,
                    show_level = this.Sunset_rotation.show_level,
                    type = this.Sunset_rotation.type,
                };
            }

            Text_Circle Sunset_circle = null;
            if (this.Sunset_circle != null)
            {
                Sunset_circle = new Text_Circle
                {
                    circle_center_X = this.Sunset_circle.circle_center_X,
                    circle_center_Y = this.Sunset_circle.circle_center_Y,
                    radius = this.Sunset_circle.radius,
                    angle = this.Sunset_circle.angle,
                    char_space_angle = this.Sunset_circle.char_space_angle,
                    zero = this.Sunset_circle.zero,
                    img_First = this.Sunset_circle.img_First,
                    //image_width = this.Sunset_circle.image_width,
                    //image_height = this.Sunset_circle.image_height,
                    unit = this.Sunset_circle.unit,
                    //unit_width = this.Sunset_circle.unit_width,
                    imperial_unit = this.Sunset_circle.imperial_unit,
                    dot_image = this.Sunset_circle.dot_image,
                    //dot_image_width = this.Sunset_circle.dot_image_width,
                    error_image = this.Sunset_circle.error_image,
                    //error_width = this.Sunset_circle.error_width,
                    vertical_alignment = this.Sunset_circle.vertical_alignment,
                    horizontal_alignment = this.Sunset_circle.horizontal_alignment,
                    reverse_direction = this.Sunset_circle.reverse_direction,
                    unit_in_alignment = this.Sunset_circle.unit_in_alignment,

                    position = this.Sunset_circle.position,
                    visible = this.Sunset_circle.visible,
                    show_level = this.Sunset_circle.show_level,
                    type = this.Sunset_circle.type,
                };
            }

            hmUI_widget_IMG_NUMBER Sunset_Sunrise = null;
            if (this.Sunset_Sunrise != null)
            {
                Sunset_Sunrise = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunset_Sunrise.imageX,
                    imageY = this.Sunset_Sunrise.imageY,
                    space = this.Sunset_Sunrise.space,
                    angle = this.Sunset_Sunrise.angle,
                    zero = this.Sunset_Sunrise.zero,
                    align = this.Sunset_Sunrise.align,
                    img_First = this.Sunset_Sunrise.img_First,
                    unit = this.Sunset_Sunrise.unit,
                    imperial_unit = this.Sunset_Sunrise.imperial_unit,
                    icon = this.Sunset_Sunrise.icon,
                    iconPosX = this.Sunset_Sunrise.iconPosX,
                    iconPosY = this.Sunset_Sunrise.iconPosY,
                    negative_image = this.Sunset_Sunrise.negative_image,
                    invalid_image = this.Sunset_Sunrise.invalid_image,
                    dot_image = this.Sunset_Sunrise.dot_image,
                    follow = this.Sunset_Sunrise.follow,

                    position = this.Sunset_Sunrise.position,
                    visible = this.Sunset_Sunrise.visible,
                    show_level = this.Sunset_Sunrise.show_level,
                    type = this.Sunset_Sunrise.type,
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
                Sunrise_Font = Sunrise_Font,
                Sunrise_rotation = Sunrise_rotation,
                Sunrise_circle = Sunrise_circle,
                Sunset = Sunset,
                Sunset_Font = Sunset_Font,
                Sunset_rotation = Sunset_rotation,
                Sunset_circle = Sunset_circle,
                Sunset_Sunrise = Sunset_Sunrise,
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
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_POINTER Pointer { get; set; }
        public hmUI_widget_IMG_LEVEL Direction { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
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

            hmUI_widget_IMG_LEVEL Direction = null;
            if (this.Direction != null)
            {
                Direction = new hmUI_widget_IMG_LEVEL
                {
                    X = this.Direction.X,
                    Y = this.Direction.Y,
                    img_First = this.Direction.img_First,
                    image_length = this.Direction.image_length,
                    shortcut = this.Images.shortcut,

                    position = this.Direction.position,
                    visible = this.Direction.visible,
                    show_level = this.Direction.show_level,
                    type = this.Direction.type,
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
                Number_Font = Number_Font,
                Pointer = Pointer,
                Direction = Direction,
                Icon = Icon,
            };
        }
    }

    //public class ElementMoon : ICloneable
    //{
    //    public string elementName = "ElementMoon";

    //    ///// <summary>Позиция в наборе элементов</summary>
    //    //public int position = -1;

    //    /// <summary>Видимость элемента</summary>
    //    public bool visible = true;

    //    public hmUI_widget_IMG_LEVEL Images { get; set; }

    //    public object Clone()
    //    {

    //        hmUI_widget_IMG_LEVEL Images = null;
    //        if (this.Images != null)
    //        {
    //            Images = new hmUI_widget_IMG_LEVEL
    //            {
    //                X = this.Images.X,
    //                Y = this.Images.Y,
    //                img_First = this.Images.img_First,
    //                image_length = this.Images.image_length,
    //                shortcut = this.Images.shortcut,

    //                position = this.Images.position,
    //                visible = this.Images.visible,
    //                show_level = this.Images.show_level,
    //                type = this.Images.type,
    //            };
    //        }

    //        return new ElementMoon
    //        {
    //            elementName = this.elementName,
    //            visible = this.visible,

    //            Images = Images,
    //        };
    //    }
    //}

    public class ElementMoon : ICloneable
    {
        public string elementName = "ElementMoon";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        //public hmUI_widget_IMG_PROGRESS Segments { get; set; }
        public hmUI_widget_IMG_NUMBER Sunrise { get; set; }
        public hmUI_widget_TEXT Sunrise_Font { get; set; }
        //public hmUI_widget_IMG_NUMBER Sunrise_rotation { get; set; }
        //public Text_Circle Sunrise_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Sunset { get; set; }
        public hmUI_widget_TEXT Sunset_Font { get; set; }
        //public hmUI_widget_IMG_NUMBER Sunset_rotation { get; set; }
        //public Text_Circle Sunset_circle { get; set; }
        public hmUI_widget_IMG_NUMBER Sunset_Sunrise { get; set; }
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
                    shortcut = this.Images.shortcut,

                    position = this.Images.position,
                    visible = this.Images.visible,
                    show_level = this.Images.show_level,
                    type = this.Images.type,
                };
            }

            //hmUI_widget_IMG_PROGRESS Segments = null;
            //if (this.Segments != null)
            //{
            //    Segments = new hmUI_widget_IMG_PROGRESS
            //    {
            //        X = this.Segments.X,
            //        Y = this.Segments.Y,
            //        img_First = this.Segments.img_First,
            //        image_length = this.Segments.image_length,

            //        position = this.Segments.position,
            //        visible = this.Segments.visible,
            //        show_level = this.Segments.show_level,
            //        type = this.Segments.type,
            //    };
            //}

            hmUI_widget_IMG_NUMBER Sunrise = null;
            if (this.Sunrise != null)
            {
                Sunrise = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunrise.imageX,
                    imageY = this.Sunrise.imageY,
                    space = this.Sunrise.space,
                    angle = this.Sunrise.angle,
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

            hmUI_widget_TEXT Sunrise_Font = null;
            if (this.Sunrise_Font != null)
            {
                Sunrise_Font = new hmUI_widget_TEXT
                {
                    x = this.Sunrise_Font.x,
                    y = this.Sunrise_Font.y,
                    w = this.Sunrise_Font.w,
                    h = this.Sunrise_Font.h,
                    color = this.Sunrise_Font.color,
                    align_h = this.Sunrise_Font.align_h,
                    align_v = this.Sunrise_Font.align_v,
                    text_size = this.Sunrise_Font.text_size,
                    text_style = this.Sunrise_Font.text_style,
                    line_space = this.Sunrise_Font.line_space,
                    char_space = this.Sunrise_Font.char_space,
                    font = this.Sunrise_Font.font,
                    padding = this.Sunrise_Font.padding,
                    unit_type = this.Sunrise_Font.unit_type,

                    position = this.Sunrise_Font.position,
                    visible = this.Sunrise_Font.visible,
                    show_level = this.Sunrise_Font.show_level,
                    type = this.Sunrise_Font.type,
                };
            }

            //hmUI_widget_IMG_NUMBER Sunrise_rotation = null;
            //if (this.Sunrise_rotation != null)
            //{
            //    Sunrise_rotation = new hmUI_widget_IMG_NUMBER
            //    {
            //        imageX = this.Sunrise_rotation.imageX,
            //        imageY = this.Sunrise_rotation.imageY,
            //        space = this.Sunrise_rotation.space,
            //        angle = this.Sunrise_rotation.angle,
            //        zero = this.Sunrise_rotation.zero,
            //        align = this.Sunrise_rotation.align,
            //        img_First = this.Sunrise_rotation.img_First,
            //        unit = this.Sunrise_rotation.unit,
            //        imperial_unit = this.Sunrise_rotation.imperial_unit,
            //        icon = this.Sunrise_rotation.icon,
            //        iconPosX = this.Sunrise_rotation.iconPosX,
            //        iconPosY = this.Sunrise_rotation.iconPosY,
            //        negative_image = this.Sunrise_rotation.negative_image,
            //        invalid_image = this.Sunrise_rotation.invalid_image,
            //        dot_image = this.Sunrise_rotation.dot_image,
            //        unit_in_alignment = this.Sunrise_rotation.unit_in_alignment,

            //        position = this.Sunrise_rotation.position,
            //        visible = this.Sunrise_rotation.visible,
            //        show_level = this.Sunrise_rotation.show_level,
            //        type = this.Sunrise_rotation.type,
            //    };
            //}

            //Text_Circle Sunrise_circle = null;
            //if (this.Sunrise_circle != null)
            //{
            //    Sunrise_circle = new Text_Circle
            //    {
            //        circle_center_X = this.Sunrise_circle.circle_center_X,
            //        circle_center_Y = this.Sunrise_circle.circle_center_Y,
            //        radius = this.Sunrise_circle.radius,
            //        angle = this.Sunrise_circle.angle,
            //        char_space_angle = this.Sunrise_circle.char_space_angle,
            //        zero = this.Sunrise_circle.zero,
            //        img_First = this.Sunrise_circle.img_First,
            //        //image_width = this.Sunrise_circle.image_width,
            //        //image_height = this.Sunrise_circle.image_height,
            //        unit = this.Sunrise_circle.unit,
            //        //unit_width = this.Sunrise_circle.unit_width,
            //        imperial_unit = this.Sunrise_circle.imperial_unit,
            //        dot_image = this.Sunrise_circle.dot_image,
            //        //dot_image_width = this.Sunrise_circle.dot_image_width,
            //        error_image = this.Sunrise_circle.error_image,
            //        //error_width = this.Sunrise_circle.error_width,
            //        vertical_alignment = this.Sunrise_circle.vertical_alignment,
            //        horizontal_alignment = this.Sunrise_circle.horizontal_alignment,
            //        reverse_direction = this.Sunrise_circle.reverse_direction,
            //        unit_in_alignment = this.Sunrise_circle.unit_in_alignment,

            //        position = this.Sunrise_circle.position,
            //        visible = this.Sunrise_circle.visible,
            //        show_level = this.Sunrise_circle.show_level,
            //        type = this.Sunrise_circle.type,
            //    };
            //}

            hmUI_widget_IMG_NUMBER Sunset = null;
            if (this.Sunset != null)
            {
                Sunset = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunset.imageX,
                    imageY = this.Sunset.imageY,
                    space = this.Sunset.space,
                    angle = this.Sunset.angle,
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

            hmUI_widget_TEXT Sunset_Font = null;
            if (this.Sunset_Font != null)
            {
                Sunset_Font = new hmUI_widget_TEXT
                {
                    x = this.Sunset_Font.x,
                    y = this.Sunset_Font.y,
                    w = this.Sunset_Font.w,
                    h = this.Sunset_Font.h,
                    color = this.Sunset_Font.color,
                    align_h = this.Sunset_Font.align_h,
                    align_v = this.Sunset_Font.align_v,
                    text_size = this.Sunset_Font.text_size,
                    text_style = this.Sunset_Font.text_style,
                    line_space = this.Sunset_Font.line_space,
                    char_space = this.Sunset_Font.char_space,
                    font = this.Sunset_Font.font,
                    padding = this.Sunset_Font.padding,
                    unit_type = this.Sunset_Font.unit_type,

                    position = this.Sunset_Font.position,
                    visible = this.Sunset_Font.visible,
                    show_level = this.Sunset_Font.show_level,
                    type = this.Sunset_Font.type,
                };
            }

            //hmUI_widget_IMG_NUMBER Sunset_rotation = null;
            //if (this.Sunset_rotation != null)
            //{
            //    Sunset_rotation = new hmUI_widget_IMG_NUMBER
            //    {
            //        imageX = this.Sunset_rotation.imageX,
            //        imageY = this.Sunset_rotation.imageY,
            //        space = this.Sunset_rotation.space,
            //        angle = this.Sunset_rotation.angle,
            //        zero = this.Sunset_rotation.zero,
            //        align = this.Sunset_rotation.align,
            //        img_First = this.Sunset_rotation.img_First,
            //        unit = this.Sunset_rotation.unit,
            //        imperial_unit = this.Sunset_rotation.imperial_unit,
            //        icon = this.Sunset_rotation.icon,
            //        iconPosX = this.Sunset_rotation.iconPosX,
            //        iconPosY = this.Sunset_rotation.iconPosY,
            //        negative_image = this.Sunset_rotation.negative_image,
            //        invalid_image = this.Sunset_rotation.invalid_image,
            //        dot_image = this.Sunset_rotation.dot_image,
            //        unit_in_alignment = this.Sunset_rotation.unit_in_alignment,

            //        position = this.Sunset_rotation.position,
            //        visible = this.Sunset_rotation.visible,
            //        show_level = this.Sunset_rotation.show_level,
            //        type = this.Sunset_rotation.type,
            //    };
            //}

            //Text_Circle Sunset_circle = null;
            //if (this.Sunset_circle != null)
            //{
            //    Sunset_circle = new Text_Circle
            //    {
            //        circle_center_X = this.Sunset_circle.circle_center_X,
            //        circle_center_Y = this.Sunset_circle.circle_center_Y,
            //        radius = this.Sunset_circle.radius,
            //        angle = this.Sunset_circle.angle,
            //        char_space_angle = this.Sunset_circle.char_space_angle,
            //        zero = this.Sunset_circle.zero,
            //        img_First = this.Sunset_circle.img_First,
            //        //image_width = this.Sunset_circle.image_width,
            //        //image_height = this.Sunset_circle.image_height,
            //        unit = this.Sunset_circle.unit,
            //        //unit_width = this.Sunset_circle.unit_width,
            //        imperial_unit = this.Sunset_circle.imperial_unit,
            //        dot_image = this.Sunset_circle.dot_image,
            //        //dot_image_width = this.Sunset_circle.dot_image_width,
            //        error_image = this.Sunset_circle.error_image,
            //        //error_width = this.Sunset_circle.error_width,
            //        vertical_alignment = this.Sunset_circle.vertical_alignment,
            //        horizontal_alignment = this.Sunset_circle.horizontal_alignment,
            //        reverse_direction = this.Sunset_circle.reverse_direction,
            //        unit_in_alignment = this.Sunset_circle.unit_in_alignment,

            //        position = this.Sunset_circle.position,
            //        visible = this.Sunset_circle.visible,
            //        show_level = this.Sunset_circle.show_level,
            //        type = this.Sunset_circle.type,
            //    };
            //}

            hmUI_widget_IMG_NUMBER Sunset_Sunrise = null;
            if (this.Sunset_Sunrise != null)
            {
                Sunset_Sunrise = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Sunset_Sunrise.imageX,
                    imageY = this.Sunset_Sunrise.imageY,
                    space = this.Sunset_Sunrise.space,
                    angle = this.Sunset_Sunrise.angle,
                    zero = this.Sunset_Sunrise.zero,
                    align = this.Sunset_Sunrise.align,
                    img_First = this.Sunset_Sunrise.img_First,
                    unit = this.Sunset_Sunrise.unit,
                    imperial_unit = this.Sunset_Sunrise.imperial_unit,
                    icon = this.Sunset_Sunrise.icon,
                    iconPosX = this.Sunset_Sunrise.iconPosX,
                    iconPosY = this.Sunset_Sunrise.iconPosY,
                    negative_image = this.Sunset_Sunrise.negative_image,
                    invalid_image = this.Sunset_Sunrise.invalid_image,
                    dot_image = this.Sunset_Sunrise.dot_image,
                    follow = this.Sunset_Sunrise.follow,

                    position = this.Sunset_Sunrise.position,
                    visible = this.Sunset_Sunrise.visible,
                    show_level = this.Sunset_Sunrise.show_level,
                    type = this.Sunset_Sunrise.type,
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

            return new ElementMoon
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                //Segments = Segments,
                Sunrise = Sunrise,
                Sunrise_Font = Sunrise_Font,
                //Sunrise_rotation = Sunrise_rotation,
                //Sunrise_circle = Sunrise_circle,
                Sunset = Sunset,
                Sunset_Font = Sunset_Font,
                //Sunset_rotation = Sunset_rotation,
                //Sunset_circle = Sunset_circle,
                Sunset_Sunrise = Sunset_Sunrise,
                Pointer = Pointer,
                Icon = Icon,
            };
        }
    }

    public class ElementCompass : ICloneable
    {
        public string elementName = "ElementCompass";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_LEVEL Images { get; set; }
        public hmUI_widget_IMG_NUMBER Number { get; set; }
        public hmUI_widget_TEXT Number_Font { get; set; }
        public hmUI_widget_IMG_NUMBER Text_rotation { get; set; }
        public Text_Circle Text_circle { get; set; }
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
                    shortcut = this.Images.shortcut,

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
                    angle = this.Number.angle,
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

            hmUI_widget_TEXT Number_Font = null;
            if (this.Number_Font != null)
            {
                Number_Font = new hmUI_widget_TEXT
                {
                    x = this.Number_Font.x,
                    y = this.Number_Font.y,
                    w = this.Number_Font.w,
                    h = this.Number_Font.h,
                    color = this.Number_Font.color,
                    align_h = this.Number_Font.align_h,
                    align_v = this.Number_Font.align_v,
                    text_size = this.Number_Font.text_size,
                    text_style = this.Number_Font.text_style,
                    line_space = this.Number_Font.line_space,
                    char_space = this.Number_Font.char_space,
                    font = this.Number_Font.font,
                    padding = this.Number_Font.padding,
                    unit_type = this.Number_Font.unit_type,

                    position = this.Number_Font.position,
                    visible = this.Number_Font.visible,
                    show_level = this.Number_Font.show_level,
                    type = this.Number_Font.type,
                };
            }

            hmUI_widget_IMG_NUMBER Text_rotation = null;
            if (this.Text_rotation != null)
            {
                Text_rotation = new hmUI_widget_IMG_NUMBER
                {
                    imageX = this.Text_rotation.imageX,
                    imageY = this.Text_rotation.imageY,
                    space = this.Text_rotation.space,
                    angle = this.Text_rotation.angle,
                    zero = this.Text_rotation.zero,
                    align = this.Text_rotation.align,
                    img_First = this.Text_rotation.img_First,
                    unit = this.Text_rotation.unit,
                    imperial_unit = this.Text_rotation.imperial_unit,
                    icon = this.Text_rotation.icon,
                    iconPosX = this.Text_rotation.iconPosX,
                    iconPosY = this.Text_rotation.iconPosY,
                    negative_image = this.Text_rotation.negative_image,
                    invalid_image = this.Text_rotation.invalid_image,
                    dot_image = this.Text_rotation.dot_image,
                    unit_in_alignment = this.Text_rotation.unit_in_alignment,

                    position = this.Text_rotation.position,
                    visible = this.Text_rotation.visible,
                    show_level = this.Text_rotation.show_level,
                    type = this.Text_rotation.type,
                };
            }

            Text_Circle Text_circle = null;
            if (this.Text_circle != null)
            {
                Text_circle = new Text_Circle
                {
                    circle_center_X = this.Text_circle.circle_center_X,
                    circle_center_Y = this.Text_circle.circle_center_Y,
                    radius = this.Text_circle.radius,
                    angle = this.Text_circle.angle,
                    char_space_angle = this.Text_circle.char_space_angle,
                    zero = this.Text_circle.zero,
                    img_First = this.Text_circle.img_First,
                    unit = this.Text_circle.unit,
                    imperial_unit = this.Text_circle.imperial_unit,
                    dot_image = this.Text_circle.dot_image,
                    error_image = this.Text_circle.error_image,
                    //error_width = this.Text_circle.error_width,
                    vertical_alignment = this.Text_circle.vertical_alignment,
                    horizontal_alignment = this.Text_circle.horizontal_alignment,
                    reverse_direction = this.Text_circle.reverse_direction,
                    unit_in_alignment = this.Text_circle.unit_in_alignment,

                    position = this.Text_circle.position,
                    visible = this.Text_circle.visible,
                    show_level = this.Text_circle.show_level,
                    type = this.Text_circle.type,
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

            return new ElementCompass
            {
                elementName = this.elementName,
                visible = this.visible,

                Images = Images,
                Number = Number,
                Number_Font = Number_Font,
                Text_rotation = Text_rotation,
                Text_circle = Text_circle,
                Pointer = Pointer,
                Icon = Icon,
            };
        }
    }

    public class ElementImage : ICloneable
    {
        public string elementName = "ElementImage";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG Icon { get; set; }

        public object Clone()
        {

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

            return new ElementImage
            {
                elementName = this.elementName,
                visible = this.visible,

                Icon = Icon,
            };
        }
    }

    public class ElementScript
    {
        public string elementName = "ElementScript";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Использование скриптов</summary>
        public bool enable = true;

        public bool user_functions;
        public bool user_script_start;
        public bool user_script;
        public bool user_script_beforeShortcuts;
        public bool user_script_end;
        public bool resume_call;
        public bool pause_call;

    }



    public class ElementAnimation : ICloneable
    {
        public string elementName = "ElementAnimation";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG_ANIM_List Frame_Animation_List { get; set; }
        public Motion_Animation_List Motion_Animation_List { get; set; }
        public Rotate_Animation_List Rotate_Animation_List { get; set; }

        public object Clone()
        {
            hmUI_widget_IMG_ANIM_List Frame_Animation_list = null;
            if (this.Frame_Animation_List != null) 
                Frame_Animation_list = (hmUI_widget_IMG_ANIM_List)this.Frame_Animation_List.Clone();

            Motion_Animation_List Motion_Animation_list = null;
            if (this.Motion_Animation_List != null)
                Motion_Animation_List = (Motion_Animation_List)this.Motion_Animation_List.Clone();

            Rotate_Animation_List Rotate_Animation_list = null;
            if (this.Rotate_Animation_List != null)
                Rotate_Animation_List = (Rotate_Animation_List)this.Rotate_Animation_List.Clone();


            return new ElementAnimation
            {
                elementName = this.elementName,
                visible = this.visible,

                Frame_Animation_List = Frame_Animation_list,
                Motion_Animation_List = Motion_Animation_list,
                Rotate_Animation_List = Rotate_Animation_list,
            };
        }


    }


    public class EditableElements
    {
        /// <summary>Выбраная зона</summary>
        public int selected_zone = -1;
        public List<WATCHFACE_EDIT_GROUP> Watchface_edit_group { get; set; }
        public string mask { get; set; } //100%mask
        public string fg_mask { get; set; } //70%mask

        /// <summary>Отображать элементы в режиме АОД</summary>
        public bool AOD_show { get; set; } = false;

        /// <summary>Отображать элементы перед остальными</summary>
        public bool display_first { get; set; } = false;

        /// <summary>Отображать в режиме редактирования</summary>
        public bool showEeditMode { get; set; } = false;

        /// <summary>Видимость элемента</summary>
        public bool visible = false;
    }

    public class DisconnectAlert
    {
        /// <summary>Оповещение при разрыве связи</summary>
        public BluetoothStateAlert BluetoothOff { get; set; }

        /// <summary>Оповещение при востановление связи</summary>
        public BluetoothStateAlert BluetoothOn { get; set; }

        /// <summary>Использование элемента</summary>
        public bool enable = true;
    }

    public class RepeatAlert
    {
        public PeriodicAlert EveryHour { get; set; }
        public PeriodicAlert RepeatingAlert { get; set; }

        /// <summary>Использование элемента</summary>
        public bool enable = true;
    }

    public class TopImage
    {
        public string elementName = "TopImage";

        ///// <summary>Позиция в наборе элементов</summary>
        //public int position = -1;

        /// <summary>Видимость элемента</summary>
        public bool visible = true;

        public hmUI_widget_IMG Icon { get; set; }

        /// <summary>Отображать изображение на эеране АОД</summary>
        public bool showInAOD = false;
    }

    public class ElementButtons
    {
        public List<Button> Button { get; set; }

        /// <summary>Использование элемента</summary>
        public bool enable = true;
    }
}


