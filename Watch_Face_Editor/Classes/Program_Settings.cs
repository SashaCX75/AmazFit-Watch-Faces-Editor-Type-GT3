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

        //public bool Model_GTR3 = true;
        //public bool Model_GTR3_Pro = false;
        //public bool Model_GTS3 = false;
        public string Watch_Model = "GTR 3";

        public bool ShowBorder = false;
        public bool Crop = true;
        public bool Shortcuts_Center_marker = true;
        public bool Show_Warnings = true;
        public bool Show_Shortcuts = true;
        public bool Show_CircleScale_Area = false;
        public bool Show_Widgets_Area = true;

        public bool Shortcuts_Area = true;
        public bool Shortcuts_Border = true;
        public bool Shortcuts_Image = false;
        public bool Shortcuts_In_Gif = false;

        public float Scale = 1f;
        public float Gif_Speed = 1f;
        public int Animation_Preview_Speed = 4;

        public bool DrawAllWidgets = false;

        public bool ShowIn12hourFormat = true;

        public int[] CustomColors = { };

        public string language { get; set; }

        public int Splitter_Pos = 0;

        public string WatchSkin_GTR_3 = @"\Skin\WatchSkin_GTR_3.json";
        public string WatchSkin_GTR_3_Pro = @"\Skin\WatchSkin_GTR_3_Pro.json";
        public string WatchSkin_GTS_3 = @"\Skin\WatchSkin_GTS_3.json";
        public string WatchSkin_T_Rex_2 = @"\Skin\WatchSkin_T_Rex_2.json";
        public string WatchSkin_GTR_4 = @"\Skin\WatchSkin_GTR_4.json";
        public string WatchSkin_GTS_4_mini = @"\Skin\WatchSkin_GTS_4_mini.json";
        public string WatchSkin_Amazfit_Band_7 = @"\Skin\WatchSkin_Amazfit_Band_7.json";
        public bool WatchSkin_Use = false;
    }
}
