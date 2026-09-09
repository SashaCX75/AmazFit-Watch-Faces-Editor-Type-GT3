using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    public class Program_Settings
    {
        public bool Settings_Unpack_Dialog = true;
        public bool Settings_Unpack_Save = false;
        public bool Settings_Unpack_Replace = false;

        public bool Settings_Pack_Dialog = false;
        public bool Settings_Pack_GoToFile = true;
        public bool Settings_Pack_DoNotning = false;

        public bool Settings_AfterUnpack_Dialog = false;
        public bool Settings_AfterUnpack_Download = true;
        public bool Settings_AfterUnpack_DoNothing = false;

        public bool Settings_Open_Dialog = false;
        public bool Settings_Open_Download = true;
        public bool Settings_Open_DoNotning = false;
        public bool Settings_Open_Download_Your_File = false;

        public string PreviewStates_Path = "";

        public string Watch_Model = "Balance 2";

        public bool ShowBorder = false;
        public bool Crop = true;
        public bool Pointer_Center_marker = true;
        //public bool Show_Warnings = true;
        public bool Show_Shortcuts = true;
        public bool Show_Buttons = true;
        public bool Show_CircleScale_Area = false;
        public bool Show_Widgets_Area = true;

        public bool Shortcuts_Area = true;
        public bool Shortcuts_Border = true;
        //public bool Shortcuts_Image = false;
        public bool Shortcuts_In_Gif = true;

        public bool Buttons_Area = true;
        public bool Buttons_Border = true;
        //public bool Buttons_Image = false;
        public bool Buttons_In_Gif = true;

        public bool Use_ARGB_encoding = false;
        public bool ARGB_encoding_color = false;
        public bool ARGB_encoding_forced = true;
        public int ARGB_encoding_color_count = 255;

        public float Scale = 1f;
        public float Gif_Speed = 1f;
        public int Animation_Preview_Speed = 4;

        public bool DrawAllWidgets = false;

        public string City_Name = "City Name";

        public bool ShowIn12hourFormat = true;

        public bool CreateZPK = false;
        public bool DelConfirm = false;
        public bool AutoSave = false;
        public int AutoSaveTime = 0;
        public bool DevelopmentMode = false;

        public string ZeppPlayerPath = "";
        public string FilePost_API_key = "";
        public string GitHub_owner = "";
        public string GitHub_repoName = "";
        public string GitHub_filePath = "/";
        public string GitHub_token = "";
        public bool GitHub_AskConfirmation = false;

        public int[] CustomColors = { };

        public string language { get; set; }

        public bool WatchSkin_Use = false;
        public bool SendSystemInfo = true;

        public string model_config = @"\model_config\configurations.json";

        public string CacheFonts_light = "0123456789 _+-.,:;`'%°\\\\/";
        public string CacheFonts_full = "0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " +
                "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя  ҐЄІЇґєії " + "_+-.,:;`'%°\\\\/";
    }
}
