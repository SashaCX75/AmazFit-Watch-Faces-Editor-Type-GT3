using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    //public class App_WatchFace
    //{

        public class App_WatchFace
        {
            public string configVersion { get; set; }
            public App app { get; set; }
            public string[] permissions { get; set; }
            public Runtime runtime { get; set; }
            public Device device { get; set; }
            public Module module { get; set; }
            public I18n i18n { get; set; }
            public string defaultLanguage { get; set; }
        }

        public class App
        {
            public int appId { get; set; }
            public string appName { get; set; }
            public string appType { get; set; }
            public Version version { get; set; }
            public string icon { get; set; }
            public string vender { get; set; }
            public string description { get; set; }
        }

        public class Version
        {
            public int code { get; set; }
            public string name { get; set; }
        }

        public class Runtime
        {
            public Apiversion apiVersion { get; set; }
        }

        public class Apiversion
        {
            public string compatible { get; set; }
            public string target { get; set; }
            public string minVersion { get; set; }
        }

        public class Device
        {
            public string[] targets { get; set; }
            public string[] platforms { get; set; }
        }

        public class Module
        {
            public Page page { get; set; }
            public AppWidget appwidget { get; set; }
            public WatchWidget watchwidget { get; set; }
            public Watchface watchface { get; set; }
            public AppSide appside { get; set; }
            public Setting setting { get; set; }
        }

        public class Page
        {
            public string[] pages { get; set; }
            public Window window { get; set; }
        }

        public class Window
        {
            public string navigationBarBackgroundColor { get; set; }
            public string navigationBarTextStyle { get; set; }
            public string navigationBarTitleText { get; set; }
            public string backgroundColor { get; set; }
            public string backgroundTextStyle { get; set; }
        }

        public class AppWidget
        {
            public string[] widgets { get; set; }
        }

        public class WatchWidget
        {
            public object[] widgets { get; set; }
        }

        public class Watchface
        {
            public string path { get; set; }
            public int main { get; set; }
            public int editable { get; set; }
            public int lockscrreen { get; set; }
        }

        public class AppSide
        {
            public string path { get; set; }
        }

        public class Setting
        {
            public string path { get; set; }
        }

        public class I18n
        {
            public EnUS enUS { get; set; }
        }

        public class EnUS
        {
            public string name { get; set; }
        }

    //}
}
