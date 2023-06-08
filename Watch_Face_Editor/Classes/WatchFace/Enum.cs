namespace Watch_Face_Editor
{
    public enum hmUI_show_level
    {
        ONLY_NORMAL = 0,
        ONLY_AOD = 1,
        ALL = 2
    }

    public enum hmUI_align
    {
        LEFT = 0,
        CENTER_H = 1,
        RIGHT = 2,
        CENTER_V = 3
    }

    public enum text_style
    {
        CHAR_WRAP = 0, // перенос по буквам
        WRAP = 1, // перенос по словам
        ELLIPSIS = 2, // без переноса
        NONE = 3 // бегущая строка
    }
}
