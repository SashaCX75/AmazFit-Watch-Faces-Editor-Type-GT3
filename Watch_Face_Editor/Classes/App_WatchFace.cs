using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watch_Face_Editor
{
    public class App_WatchFace
    {
        public string configVersion { get; set; } = "v2";
        public App app { get; set; }=new App();
        public List<string> permissions { get; set; } =new List<string> { "gps" };
        public Runtime runtime { get; set; }= new Runtime();
        public I18n i18n { get; set; } = new I18n();
        public string defaultLanguage { get; set; } = "en-US";
        public bool debug { get; set; } = false;
        public Module module { get; set; } = new Module();
        public List<Platform> platforms { get; set; } = new List<Platform>();
        //public Platform[] platforms { get; set; }
        public int designWidth { get; set; } = 454;
        public Packageinfo packageInfo { get; set; } = new Packageinfo();
    }

    public class App
    {
        public int appIdType { get; set; } = 0;
        public int appId { get; set; } = 1234567;
        public string appName { get; set; } = "AppName";
        public string appType { get; set; } = "watchface";
        public Version version { get; set; } = new Version();
        public string vender { get; set; } = "zepp";
        public string description { get; set; } = "";
        public string icon { get; set; } = "";
        public List<string> cover { get; set; } = new List<string>();
        public Extrainfo extraInfo { get; set; } = new Extrainfo();
    }

    public class Version
    {
        public int code { get; set; } = 2;
        public string name { get; set; } = "1.0.1";
    }

    public class Extrainfo
    {
        public int madeBy { get; set; } = 1;
        public bool fromZoom { get; set; } = false;
    }

    public class Runtime
    {
        public Apiversion apiVersion { get; set; } = new Apiversion();
    }

    public class Apiversion
    {
        public string compatible { get; set; } = "1.0.0";
        public string target { get; set; } = "1.0.1";
        public string minVersion { get; set; } = "1.0.0";
    }

    public class I18n
    {
        public EnUS enUS { get; set; } = new EnUS();
    }

    public class EnUS
    {
        public string icon { get; set; } = "";
        public string appName { get; set; } = "AppName";
    }

    public class Module
    {
        public Watchface watchface { get; set; } = new Watchface();
    }

    public class Watchface
    {
        public string path { get; set; } = "watchface/index";
        public int main { get; set; } = 1;
        public int editable { get; set; } = 0;
        public int lockscreen { get; set; } = 0;
        public int hightCost { get; set; } = 0;
    }

    public class Packageinfo
    {
        public string mode { get; set; } = "production";
        public int timeStamp { get; set; } = 1671657100;
        public int expiredTime { get; set; } = 172800;
        public string zpm { get; set; } = "2.8.2";
    }

    public class Platform
    {
        public string name { get; set; } = "";
        public int deviceSource { get; set; }
    }

}
