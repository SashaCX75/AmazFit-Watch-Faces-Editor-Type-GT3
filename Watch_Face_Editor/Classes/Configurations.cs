using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using static Newtonsoft.Json.JsonConvert;
//using Newtonsoft.Json.Linq;

namespace Watch_Face_Editor.Classes
{
    public struct PlatformBackground
    {
        public int w;
        public int h;

        public PlatformBackground(int iWidth, int iHeight)
        {
            w = iWidth;
            h = iHeight;
        }

        public override string ToString() => $"(Width:{w}, Height: {h})";

    }

    public struct AmazfitPlatform
    {
        // Внутренний идентификатор, используемый внутри программы
        //public String int_id;  // Излишний параметр
        // Название
        public String name;
        // Нужное
        public int designWidth;
        public int[] deviceSource_ids;
        // Размеры фона
        public PlatformBackground background;

        // Размер pictureBox при масштабе 0.5
        public PlatformBackground scaling_0_5;

        // Размер pictureBox при масштабе 1.0
        public PlatformBackground scaling_1_0;

        // Размер pictureBox при масштабе 1.5
        public PlatformBackground scaling_1_5;

        // Размер pictureBox при масштабе 2.0
        public PlatformBackground scaling_2_0;

        // Размер pictureBox при масштабе 2.5
        public PlatformBackground scaling_2_5;

        // Маска
        public string maskImage;

        // Размер preview
        public int previewHeight;

        // путь к скину часов
        public string watchSkin;

        // тип кодирования изображений
        public int colorScheme;

        // необходимость корректировки ширины изображения
        public bool fixSize;

        public float versionOS;

        public override string ToString() =>
        $"Name: {name}; designWidth: {designWidth}; Background: w: {background.w}, h: {background.h}; versionOS: {versionOS};";

        public AmazfitPlatform(string iName, string iIintId, int iDesignWidth, int[] iIds, PlatformBackground iBackground, 
                               PlatformBackground iScaling_0_5, PlatformBackground iScaling_1_0, PlatformBackground iScaling_1_5, PlatformBackground iScaling_2_0,
                               PlatformBackground iScaling_2_5, string iMaskImage, int iPreviewHeight, string iWatchSkin, int iColorScheme, bool iFixSize, int iVersionOS)
        {
            name = iName;
            //int_id = iIintId;
            designWidth = iDesignWidth;
            deviceSource_ids = iIds;
            background = iBackground;
            scaling_0_5 = iScaling_0_5;
            scaling_1_0 = iScaling_1_0;
            scaling_1_5 = iScaling_1_5;
            scaling_2_0 = iScaling_2_0;
            scaling_2_5 = iScaling_2_5;
            maskImage = iMaskImage;
            previewHeight = iPreviewHeight;
            watchSkin = iWatchSkin;
            colorScheme = iColorScheme;
            fixSize = iFixSize;
            versionOS = iVersionOS;
        }
    }


    public class Configurations
    {
        //private l_platform[] AvaialblePlatforms { get; set; }
        //public l_platform[] AvaialblePlatforms { get; set; }
        public Dictionary<string, AmazfitPlatform> AvaialblePlatforms { get; set; }


        public static Dictionary<string, AmazfitPlatform> LoadFromFile(string Filename = @"\model_config\configurations.json")
        {
            Dictionary<string, AmazfitPlatform> result;
            try
            {

                result = new Dictionary<string, AmazfitPlatform>();
                //result.Add("GTR 3 Pro", new l_platform("test1", 500, new int[] { 1, 2, 3 }, new l_background(543, 345)));
                //result.Add("GTS 3", new l_platform("test2", 600, new int[] { 4, 5, 6 }, new l_background(111, 222)));

                //string JSON_String = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
                //{
                //    //DefaultValueHandling = DefaultValueHandling.Ignore,
                //    NullValueHandling = NullValueHandling.Ignore
                //});
                //File.WriteAllText(Filename, JSON_String, System.Text.Encoding.UTF8);

                if (File.Exists(Filename))
                {
                    result = DeserializeObject<Dictionary<string, AmazfitPlatform>>
                    (File.ReadAllText(Filename), new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    //foreach (var config in result)
                    //{
                    //    Logger.WriteLine($"{config.Key}: {config.Value}");
                    //}
                    return result;
                }
                else
                {
                    Logger.WriteLine("No file for device config:" + Filename);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Ошибка при чтении глобальных конфигураций:" + ex);
            }

            // TODO :: Proparation !
            return null;
        }
    }
}
