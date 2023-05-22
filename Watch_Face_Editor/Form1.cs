using ControlLibrary;
using ImageMagick;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        WATCH_FACE Watch_Face;
        public Watch_Face_Preview_Set WatchFacePreviewSet;
        List<string> ListImages = new List<string>(); // перечень имен файлов с картинками без раширений
        List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        List<string> ListAnimImages = new List<string>(); // перечень имен файлов с картинками анимации без раширений
        List<string> ListAnimImagesFullName = new List<string>(); // перечень путей к файлам с картинками анимации
        public bool PreviewView; // включает прорисовку предпросмотра
        bool Settings_Load; // включать при обновлении настроек для выключения перерисовки
        bool JSON_Modified = false; // JSON файл был изменен
        string FileName; // Запоминает имя для диалогов
        string FullFileDir; // Запоминает папку проекта
        public static Program_Settings ProgramSettings;
        string StartFileNameJson; // имя файла из параметров запуска
        string StartFileNameZip; // имя файла из параметров запуска
        float currentDPI; // масштаб экрана
        Point cursorPos = new Point(0, 0); // положение курсора при начале перетягивания элементов
        List<Color> colorMapList = new List<Color>(); // карта цветов для конвертации изображений
        int ImageWidth; // ширина изображения для конвертации изображений

        Form_Preview formPreview;


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        public Form1(string[] args)
        {
            if (File.Exists(Application.StartupPath + "\\Program log.txt")) File.Delete(Application.StartupPath + @"\Program log.txt");
            Logger.WriteLine("* Form1");
            Logger.WriteLine("Application.StartupPath: " + Application.StartupPath);

            SplashScreenStart();

            Point centrPosition = new Point(0, 0);
            centrPosition.X = (SystemInformation.PrimaryMonitorSize.Width - 1100) / 2;
            centrPosition.Y = (SystemInformation.PrimaryMonitorSize.Height - 721) / 2;

            Point centrPositionPreview = new Point(0, 0);
            centrPositionPreview.X = (SystemInformation.PrimaryMonitorSize.Width - 478) / 2;
            centrPositionPreview.Y = (SystemInformation.PrimaryMonitorSize.Height - 524) / 2;

            ProgramSettings = new Program_Settings();
            try
            {
                if (File.Exists(Application.StartupPath + @"\Settings.json"))
                {
                    Logger.WriteLine("Read Settings");
                    ProgramSettings = JsonConvert.DeserializeObject<Program_Settings>
                                (File.ReadAllText(Application.StartupPath + @"\Settings.json"), new JsonSerializerSettings
                                {
                                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                                    NullValueHandling = NullValueHandling.Ignore
                                });
                    //Logger.WriteLine("Чтение Settings.json");
                }
                else
                {
                    Properties.Settings.Default.FormLocation = centrPosition;
                    Properties.Settings.Default.PreviewLocation = centrPositionPreview;
                    Properties.Settings.Default.Save();

                    string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
                    {
                        //DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
                }
                Logger.WriteLine("FormLocation = " + Properties.Settings.Default.FormLocation.ToString());

#if DEBUG
                const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

                using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
                {
                    if (ndpKey != null && ndpKey.GetValue("Release") != null)
                    {
                        Console.WriteLine($".NET Framework Version: {CheckFor45PlusVersion((int)ndpKey.GetValue("Release"))}");
                        Logger.WriteLine($".NET Framework Version: {CheckFor45PlusVersion((int)ndpKey.GetValue("Release"))}");
                    }
                    else
                    {
                        Console.WriteLine(".NET Framework Version 4.5 or later is not detected.");
                        Logger.WriteLine(".NET Framework Version 4.5 or later is not detected.");
                    }
                }
#endif

                if (ProgramSettings == null) ProgramSettings = new Program_Settings();
                Size size = new Size();
                int screenOffsetX = 0;
                //Получаем высоту главного экрана и сумму ширины двух мониторов.
                size.Height = SystemInformation.PrimaryMonitorSize.Height;
                //size.Width = Screen.AllScreens[0].WorkingArea.Width + Screen.AllScreens[1].WorkingArea.Width;
                foreach (Screen screen in Screen.AllScreens)
                {
                    size.Width += screen.WorkingArea.Width;
                    if (screen.WorkingArea.X < screenOffsetX) screenOffsetX = screen.WorkingArea.X;
                }
                size.Width -= screenOffsetX;
                if (Properties.Settings.Default.FormLocation.X >= size.Width || Properties.Settings.Default.FormLocation.Y >= size.Height || 
                    Properties.Settings.Default.FormLocation.X < screenOffsetX - 550)
                    Properties.Settings.Default.FormLocation = centrPosition;
                if (Properties.Settings.Default.PreviewLocation.X >= size.Width || Properties.Settings.Default.PreviewLocation.Y >= size.Height ||
                    Properties.Settings.Default.PreviewLocation.X < screenOffsetX - 240)
                    Properties.Settings.Default.PreviewLocation = centrPositionPreview;

                if ((ProgramSettings.language == null) || (ProgramSettings.language.Length < 2))
                {
                    string language = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                    //int language = System.Globalization.CultureInfo.CurrentCulture.LCID;
                    //ProgramSettings.language = "Русский";
                    ProgramSettings.language = "English";
                    Logger.WriteLine("language = " + language);
                    switch (language)
                    {
                        case "ru": ProgramSettings.language = "Русский"; break;
                        case "en": ProgramSettings.language = "English"; break;
                        case "es": ProgramSettings.language = "Español"; break;
                        case "it": ProgramSettings.language = "Italiano"; break;
                        case "zh": ProgramSettings.language = "Chinese/简体中文"; break;
                        case "uk": ProgramSettings.language = "Українська"; break;
                        case "de": ProgramSettings.language = "Deutsch"; break;
                        case "cs": ProgramSettings.language = "Čeština"; break;
                        case "fr": ProgramSettings.language = "French"; break;
                    }
                  
                }
                //Logger.WriteLine("Определили язык");
                SetLanguage();
            }
            catch (Exception)
            {
                //Logger.WriteLine("Ошибка чтения настроек " + ex);
            }

            InitializeComponent();

            WatchFacePreviewSet = new Watch_Face_Preview_Set();
            WatchFacePreviewSet.Activity = new ActivitySet();
            WatchFacePreviewSet.Date = new DateSet();
            WatchFacePreviewSet.Status = new StatusSet();
            WatchFacePreviewSet.Time = new TimeSet();
            WatchFacePreviewSet.Weather = new WeatherSet();

            PreviewView = true;
            Settings_Load = false;
            currentDPI = tabControl1.Height / 670f;

            #region sistem font
            //byte[] fontData = Properties.Resources.OpenSans_Regular;
            byte[] fontData = Properties.Resources.Roboto_Regular;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            //fonts.AddMemoryFont(fontPtr, Properties.Resources.OpenSans_Regular.Length);
            //AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.OpenSans_Regular.Length, IntPtr.Zero, ref dummy);
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Roboto_Regular.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Roboto_Regular.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            #endregion
            Logger.WriteLine("Создали переменные");

            if (args.Length == 1)
            {
                string fileName = args[0].ToString();
                if ((File.Exists(fileName)) && (Path.GetExtension(fileName) == ".json"))
                {
                    Logger.WriteLine("args[0] - *.json");
                    StartFileNameJson = fileName;
                    Logger.WriteLine("Программа запущена с аргументом: " + fileName);
                }
                if ((File.Exists(fileName)) && (Path.GetExtension(fileName) == ".zip"))
                {
                    Logger.WriteLine("args[0] - *.zip");
                    StartFileNameZip = fileName;
                    Logger.WriteLine("Программа запущена с аргументом: " + fileName);
                }
            }
            Logger.WriteLine("* Form1 (end)");
        }

        private void SplashScreenStart()
        {
            Logger.WriteLine("* SplashScreenStart");
            //SplashScreen.SplashScreen formSplashScreen = new SplashScreen.SplashScreen();
            //formSplashScreen.Show();
            string splashScreenPath = Application.StartupPath + @"\Libs\SplashScreen.exe";
            if (File.Exists(splashScreenPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = splashScreenPath;
                Process exeProcess = Process.Start(startInfo);

                exeProcess.Dispose();
                //exeProcess.CloseMainWindow();
            }
            Logger.WriteLine("* SplashScreenStart (end)");

        }

        /// <summary>
        /// Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.WriteLine("* Form1_Load");
            // закрываем SplashScreen
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "AmazFit WatchFace editor (ZeppOS) SplashScreen");
            if (windowPtr != IntPtr.Zero)
            {
                Logger.WriteLine("* SplashScreen_CLOSE");
                SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }

            /*// Set window location
            if (Properties.Settings.Default.FormLocation != null)
            {
                this.Location = Properties.Settings.Default.FormLocation;
            }*/

            Logger.WriteLine("Form1_Load");

            PreviewView = false;
            Settings_Load = true;

            comboBox_AddBackground.SelectedIndex = 0;
            comboBox_AddTime.SelectedIndex = 0;
            comboBox_AddDate.SelectedIndex = 0;
            comboBox_AddActivity.SelectedIndex = 0;
            comboBox_AddAir.SelectedIndex = 0;
            comboBox_AddSystem.SelectedIndex = 0;
            progressBar1.Width = (int)(650 * currentDPI);

            Logger.WriteLine("Set Model_Watch");
            comboBox_watch_model.Text = ProgramSettings.Watch_Model;
            
            checkBox_WatchSkin_Use.Checked = ProgramSettings.WatchSkin_Use;
            textBox_WatchSkin_Path.Enabled = ProgramSettings.WatchSkin_Use;
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_3;
                    break;
                case "GTR 3 Pro":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_3_Pro;
                    break;
                case "GTS 3":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTS_3;
                    break;
                case "T-Rex 2":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_T_Rex_2;
                    break;
                case "T-Rex Ultra":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_T_Rex_Ultra;
                    break;
                case "GTR 4":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_4;
                    break;
                case "Amazfit Band 7":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_Amazfit_Band_7;
                    break;
                case "GTS 4 mini":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTS_4_mini;
                    break;
                case "Falcon":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_Falcon;
                    break;
                case "GTR mini":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_mini;
                    break;
                case "GTS 4":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTS_3;
                    break;
            }

            textBox_PreviewStates_Path.Text = ProgramSettings.PreviewStates_Path;

            Logger.WriteLine("Set checkBox");
            checkBox_border.Checked = ProgramSettings.ShowBorder;
            checkBox_crop.Checked = ProgramSettings.Crop;
            checkBox_Show_Shortcuts.Checked = ProgramSettings.Show_Shortcuts;
            checkBox_CircleScaleImage.Checked = ProgramSettings.Show_CircleScale_Area;
            checkBox_center_marker.Checked = ProgramSettings.Shortcuts_Center_marker;
            checkBox_WidgetsArea.Checked = ProgramSettings.Show_Widgets_Area;

            label_version.Text = "v " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            label_version_help.Text =
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            //label_version.Text = currentDPI.ToString();

            Logger.WriteLine("Set Settings");
            radioButton_Settings_AfterUnpack_Dialog.Checked = ProgramSettings.Settings_AfterUnpack_Dialog;
            radioButton_Settings_AfterUnpack_DoNothing.Checked = ProgramSettings.Settings_AfterUnpack_DoNothing;
            radioButton_Settings_AfterUnpack_Download.Checked = ProgramSettings.Settings_AfterUnpack_Download;

            radioButton_Settings_Open_Dialog.Checked = ProgramSettings.Settings_Open_Dialog;
            radioButton_Settings_Open_DoNotning.Checked = ProgramSettings.Settings_Open_DoNotning;
            radioButton_Settings_Open_Download.Checked = ProgramSettings.Settings_Open_Download;
            radioButton_Settings_Open_Download_Your_File.Checked = ProgramSettings.Settings_Open_Download_Your_File;

            radioButton_Settings_Pack_Dialog.Checked = ProgramSettings.Settings_Pack_Dialog;
            radioButton_Settings_Pack_DoNotning.Checked = ProgramSettings.Settings_Pack_DoNotning;
            radioButton_Settings_Pack_GoToFile.Checked = ProgramSettings.Settings_Pack_GoToFile;

            radioButton_Settings_Unpack_Dialog.Checked = ProgramSettings.Settings_Unpack_Dialog;
            radioButton_Settings_Unpack_Replace.Checked = ProgramSettings.Settings_Unpack_Replace;
            radioButton_Settings_Unpack_Save.Checked = ProgramSettings.Settings_Unpack_Save;
            numericUpDown_Gif_Speed.Value = (decimal)ProgramSettings.Gif_Speed;
            comboBox_Animation_Preview_Speed.SelectedIndex = ProgramSettings.Animation_Preview_Speed;

            checkBox_Shortcuts_Area.Checked = ProgramSettings.Shortcuts_Area;
            checkBox_Shortcuts_Border.Checked = ProgramSettings.Shortcuts_Border;
            checkBox_Shortcuts_Image.Checked = ProgramSettings.Shortcuts_Image;
            checkBox_Shortcuts_In_Gif.Checked = ProgramSettings.Shortcuts_In_Gif;

            checkBox_ShowIn12hourFormat.Checked = ProgramSettings.ShowIn12hourFormat;
            checkBox_AllWidgetsInGif.Checked = ProgramSettings.DrawAllWidgets;

            if (ProgramSettings.language.Length > 1) comboBox_Language.Text = ProgramSettings.language;


            Settings_Load = false;
            JSON_Modified = false;

            switch(ProgramSettings.language)
            {
                case "Русский":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_ru;
                    break;
                case "Українська":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_uk;
                    break;
                case "Deutsch":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_de;
                    break;
                case "Chinese/简体中文":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_zh;
                    break;
                case "Español":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_es;
                    break;
                case "Čeština":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_cs;
                    break;
                case "French":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_fr;
                    break;
                case "Italiano":
                    richTextBox_Tips.Rtf = Properties.Resources.tips_it;
                    break;
                default:
                    richTextBox_Tips.Rtf = Properties.Resources.tips_en;
                    break;
            }
            
            //richTextBox_Tips.Rtf = richTextBox_Tips.Text;


            StartJsonPreview();
            SetPreferences(userCtrl_Set1);
            PreviewView = true;
            Logger.WriteLine("* Form1_Load (end)");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Logger.WriteLine("* Form1_Shown");
            JSON_Modified = false;
            FormText();

            // изменяем размер панели для предпросмотра если она не влазит
            if (pictureBox_Preview.Top + pictureBox_Preview.Height > label_watch_model.Top)
            {
                float newHeight = label_watch_model.Top - pictureBox_Preview.Top;
                float scale = newHeight / pictureBox_Preview.Height;
                pictureBox_Preview.Size = new Size((int)(pictureBox_Preview.Width * scale), (int)(pictureBox_Preview.Height * scale));
            }

            userCtrl_Background_Options.AutoSize = true;
            userCtrl_Background_Options.Visible = false;
            uCtrl_EditableBackground_Opt.AutoSize = true;
            uCtrl_Text_Opt.AutoSize = true;
            uCtrl_Text_Rotate_Opt.AutoSize = true;
            uCtrl_Text_Circle_Opt.AutoSize = true;
            uCtrl_Text_Weather_Opt.AutoSize = true;
            uCtrl_AmPm_Opt.AutoSize = true;
            uCtrl_Pointer_Opt.AutoSize = true;
            uCtrl_Images_Opt.AutoSize = true;
            uCtrl_Segments_Opt.AutoSize = true;
            uCtrl_Circle_Scale_Opt.AutoSize = true;
            uCtrl_Linear_Scale_Opt.AutoSize = true;
            uCtrl_Icon_Opt.AutoSize = true;
            uCtrl_Shortcut_Opt.AutoSize = true;
            uCtrl_Text_SystemFont_Opt.AutoSize = true;
            uCtrl_Animation_Frame_Opt.AutoSize = true;
            uCtrl_Animation_Motion_Opt.AutoSize = true;
            uCtrl_Animation_Rotate_Opt.AutoSize = true;
            uCtrl_EditableTimePointer_Opt.AutoSize = true;
            uCtrl_EditableElements_Opt.AutoSize = true;
            uCtrl_DisconnectAlert_Opt.AutoSize = true;
            uCtrl_RepeatingAlert_Opt.AutoSize = true;
            uCtrl_SmoothSeconds_Opt.AutoSize = true;

            button_CreatePreview.Location = button_RefreshPreview.Location;

            if (currentDPI > 1.27)
            {
                pictureBox_IconTime.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox_IconActivity.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox_IconDate.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox_IconAir.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox_IconBackground.BackgroundImageLayout = ImageLayout.Zoom;
                pictureBox_IconSystem.BackgroundImageLayout = ImageLayout.Zoom;

                button_unpack_zip.Image = (Image)(new Bitmap(button_unpack_zip.Image, 
                    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));
                button_pack_zip.Image = (Image)(new Bitmap(button_pack_zip.Image,
                    new Size((int)(16 * currentDPI), (int)(16 * currentDPI))));
            }
            fitText(button_SaveGIF);
            fitText(button_SavePNG);
            Logger.WriteLine("* Form1_Shown(end)");

            if (StartFileNameJson != null && StartFileNameJson.Length > 0)
            {
                Logger.WriteLine("Загружаем Json файл из значения аргумента " + StartFileNameJson);
                FileName = Path.GetFileName(StartFileNameJson);
                FullFileDir = Path.GetDirectoryName(StartFileNameJson);
                LoadJson(StartFileNameJson);
                StartFileNameJson = "";
            }
            else if (StartFileNameZip != null && StartFileNameZip.Length > 0)
            {
                Logger.WriteLine("Загружаем zip файл из значения аргумента " + StartFileNameZip);
                Unpack_Zip(StartFileNameZip);
                StartFileNameZip = "";
            }
        }

        public void fitText(Control control)
        {
            Graphics graphics = control.CreateGraphics();
            Font drawFont = control.Font;
            //Font drawFont = new Font(fonts.Families[0], size, GraphicsUnit.World);
            StringFormat strFormat = new StringFormat();
            strFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Near;
            Size strSize = TextRenderer.MeasureText(graphics, control.Text, drawFont);
            double controlWidth = control.Width - control.Margin.Left - control.Margin.Right;
            double scale = controlWidth / strSize.Width;
            if (scale < 1) 
            {
                Font newFont = new Font(control.Font.FontFamily, (float)(control.Font.Size * scale), control.Font.Style);
                control.Font = newFont;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            Logger.WriteLine("* FormClosing");

            /*// Copy window location to app settings
            Properties.Settings.Default.FormLocation = this.Location;*/

            // Save settings
            Properties.Settings.Default.Save();
#if !DEBUG
            if(SaveRequest() == DialogResult.Cancel) e.Cancel = true;
#endif
        }

        private void SetLanguage()
        {
            Logger.WriteLine("* SetLanguage");
            switch (ProgramSettings.language)
            {
                case "Русский":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru");
                    break;
                case "English":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                    break;
                case "Español":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
                    break;
                case "Português":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("pt");
                    break;
                case "Čeština":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("cs");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("cs");
                    break;
                case "Magyar":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("hu");
                    break;
                case "Slovenčina":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("sk");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("sk");
                    break;
                case "French":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fr");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr");
                    break;
                case "Chinese/简体中文":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("zh");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh");
                    break;
                case "Italian":
                case "Italiano":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("it");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("it");
                    break;
                case "Українська":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("uk");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("uk");
                    break;
                case "Deutsch":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de");
                    break;

                default:
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
                    break;
            }
            Logger.WriteLine("* SetLanguage (end)");
        }

        private void radioButton_Settings_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings_Load) return;
            ProgramSettings.Settings_AfterUnpack_Dialog = radioButton_Settings_AfterUnpack_Dialog.Checked;
            ProgramSettings.Settings_AfterUnpack_DoNothing = radioButton_Settings_AfterUnpack_DoNothing.Checked;
            ProgramSettings.Settings_AfterUnpack_Download = radioButton_Settings_AfterUnpack_Download.Checked;

            ProgramSettings.Settings_Open_Dialog = radioButton_Settings_Open_Dialog.Checked;
            ProgramSettings.Settings_Open_DoNotning = radioButton_Settings_Open_DoNotning.Checked;
            ProgramSettings.Settings_Open_Download = radioButton_Settings_Open_Download.Checked;

            ProgramSettings.Settings_Pack_Dialog = radioButton_Settings_Pack_Dialog.Checked;
            ProgramSettings.Settings_Pack_DoNotning = radioButton_Settings_Pack_DoNotning.Checked;
            ProgramSettings.Settings_Pack_GoToFile = radioButton_Settings_Pack_GoToFile.Checked;

            ProgramSettings.Settings_Unpack_Dialog = radioButton_Settings_Unpack_Dialog.Checked;
            ProgramSettings.Settings_Unpack_Replace = radioButton_Settings_Unpack_Replace.Checked;
            ProgramSettings.Settings_Unpack_Save = radioButton_Settings_Unpack_Save.Checked;

            ProgramSettings.ShowIn12hourFormat = checkBox_ShowIn12hourFormat.Checked;
            ProgramSettings.WatchSkin_Use = checkBox_WatchSkin_Use.Checked;
            ProgramSettings.DrawAllWidgets = checkBox_AllWidgetsInGif.Checked;
            ProgramSettings.Shortcuts_Area = checkBox_Shortcuts_Area.Checked;
            ProgramSettings.Shortcuts_Border = checkBox_Shortcuts_Border.Checked;
            ProgramSettings.Shortcuts_Image = checkBox_Shortcuts_Image.Checked;

            ProgramSettings.ShowBorder = checkBox_border.Checked;
            ProgramSettings.Crop = checkBox_crop.Checked;
            ProgramSettings.Show_CircleScale_Area = checkBox_CircleScaleImage.Checked;
            ProgramSettings.Shortcuts_Center_marker = checkBox_center_marker.Checked;
            ProgramSettings.Show_Widgets_Area = checkBox_WidgetsArea.Checked;
            ProgramSettings.Show_Shortcuts = checkBox_Show_Shortcuts.Checked;

            //ProgramSettings.language = comboBox_Language.Text;

            if (comboBox_watch_model.SelectedIndex != -1)
            {
                ProgramSettings.Watch_Model = comboBox_watch_model.Text;

                if (comboBox_watch_model.Text == "GTR 3") ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR 3 Pro") ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 3") ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "T-Rex 2") ProgramSettings.WatchSkin_T_Rex_2 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "T-Rex Ultra") ProgramSettings.WatchSkin_T_Rex_Ultra = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR 4") ProgramSettings.WatchSkin_GTR_4 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "Amazfit Band 7") ProgramSettings.WatchSkin_Amazfit_Band_7 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 4 mini") ProgramSettings.WatchSkin_GTS_4_mini = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "Falcon") ProgramSettings.WatchSkin_Falcon = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR mini") ProgramSettings.WatchSkin_GTR_mini = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 4") ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
            }

            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
        }

        private void comboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Settings_Load) return;
            ProgramSettings.language = comboBox_Language.Text;
            SetLanguage();
            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
            if (!Settings_Load)
            {
                if (MessageBox.Show(Properties.FormStrings.Message_Restart_Text1 + Environment.NewLine +
                                Properties.FormStrings.Message_Restart_Text2, Properties.FormStrings.Message_Restart_Caption,
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }

        private void comboBox_Animation_Preview_Speed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Settings_Load) return;
            ProgramSettings.Animation_Preview_Speed = comboBox_Animation_Preview_Speed.SelectedIndex;
            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
        }

        // устанавливаем заголовок окна
        private void FormText()
        {
            //throw new NotImplementedException(); FileName
            string FormName = "GTR 3 watch face editor";
            string FormNameSufix = "";
            if (FileName != null)
            {
                FormNameSufix = Path.GetFileNameWithoutExtension(FileName);
            }
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3":
                    FormName = "GTR 3 watch face editor";
                    break;
                case "GTR 3 Pro":
                    FormName = "GTR 3 Pro watch face editor";
                    break;
                case "GTS 3":
                    FormName = "GTS 3 watch face editor";
                    break;
                case "T-Rex 2":
                    FormName = "T-Rex 2 watch face editor";
                    break;
                case "T-Rex Ultra":
                    FormName = "T-Rex Ultra watch face editor";
                    break;
                case "GTR 4":
                    FormName = "GTR 4 watch face editor";
                    break;
                case "Amazfit Band 7":
                    FormName = "Amazfit Band 7 watch face editor";
                    break;
                case "GTS 4 mini":
                    FormName = "GTS 4 mini watch face editor";
                    break;
                case "Falcon":
                    FormName = "Falcon watch face editor";
                    break;
                case "GTR mini":
                    FormName = "GTR mini watch face editor";
                    break;
                case "GTS 4":
                    FormName = "GTS 4 watch face editor";
                    break;
                default:
                    FormName = "GTR 3 watch face editor";
                    break;
            }

            if (FormNameSufix.Length == 0)
            {
                this.Text = FormName;
                button_OpenDir.Enabled = false;
                button_SaveJson.Enabled = false;
            }
            else
            {
                if (JSON_Modified) FormNameSufix = FormNameSufix + "*";
                this.Text = FormName + " (" + FormNameSufix + ")";
                button_OpenDir.Enabled = true;
                button_SaveJson.Enabled = true;
            }
        }

        private void checkBox_WatchSkin_Use_Click(object sender, EventArgs e)
        {
            bool b = checkBox_WatchSkin_Use.Checked;
            textBox_WatchSkin_Path.Enabled = b;
            button_WatchSkin_PathGet.Enabled = b;
        }

        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            if (groupBox.Enabled) DrawGroupBox(groupBox, e.Graphics, Color.Black, Color.DarkGray);
            else DrawGroupBox(groupBox, e.Graphics, Color.DarkGray, Color.DarkGray);
        }
        private void DrawGroupBox(GroupBox groupBox, Graphics g, Color textColor, Color borderColor)
        {
            if (groupBox != null)
            {
                Brush textBrush = new SolidBrush(textColor);
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(groupBox.Text, groupBox.Font);
                Rectangle rect = new Rectangle(groupBox.ClientRectangle.X,
                                               groupBox.ClientRectangle.Y + (int)(strSize.Height / 2),
                                               groupBox.ClientRectangle.Width - 1,
                                               groupBox.ClientRectangle.Height - (int)(strSize.Height / 2) - 5);

                // Clear text and border
                g.Clear(this.BackColor);

                // Draw text
                g.DrawString(groupBox.Text, groupBox.Font, textBrush, groupBox.Padding.Left, 0);

                // Drawing Border
                //Left
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                //Right
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Bottom
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                //Top1
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + groupBox.Padding.Left, rect.Y));
                //Top2
                g.DrawLine(borderPen, new Point(rect.X + groupBox.Padding.Left + (int)(strSize.Width), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }

        private void button_WatchSkin_PathGet_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FullFileDir;
            openFileDialog.FileName = FileName;
            openFileDialog.Filter = Properties.FormStrings.FilterJson;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_WatchSkin;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Logger.WriteLine("* WatchSkin_PathGet_Click");
                string fullfilename = openFileDialog.FileName;
                if (fullfilename.IndexOf(Application.StartupPath) == 0)
                    fullfilename = fullfilename.Remove(0, Application.StartupPath.Length);
                textBox_WatchSkin_Path.Text = fullfilename;

                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3":
                        ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
                        break;
                    case "GTR 3 Pro":
                        ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
                        break;
                    case "GTS 3":
                        ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
                        break;
                    case "T-Rex 2":
                        ProgramSettings.WatchSkin_T_Rex_2 = textBox_WatchSkin_Path.Text;
                        break;
                    case "T-Rex Ultra":
                        ProgramSettings.WatchSkin_T_Rex_Ultra = textBox_WatchSkin_Path.Text;
                        break;
                    case "GTR 4":
                        ProgramSettings.WatchSkin_GTR_4 = textBox_WatchSkin_Path.Text;
                        break;
                    case "Amazfit Band 7":
                        ProgramSettings.WatchSkin_Amazfit_Band_7 = textBox_WatchSkin_Path.Text;
                        break;
                    case "GTS 4 mini":
                        ProgramSettings.WatchSkin_GTS_4_mini = textBox_WatchSkin_Path.Text;
                        break;
                    case "Falcon":
                        ProgramSettings.WatchSkin_Falcon = textBox_WatchSkin_Path.Text;
                        break;
                    case "GTR mini":
                        ProgramSettings.WatchSkin_GTR_mini = textBox_WatchSkin_Path.Text;
                        break;
                    case "GTS 4":
                        ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
                        break;
                }

                string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText("Settings.json", JSON_String, Encoding.UTF8);

                Logger.WriteLine("* WatchSkin_PathGet_Click_END");
            }
        }

        #region выбираем данные для предпросмотра

        public void SetPreferences(UCtrl_Set userControl_Set)
        {
            Dictionary<string, int> Activity = new Dictionary<string, int>();
            Dictionary<string, int> Air = new Dictionary<string, int>();
            Dictionary<string, bool> checkValue = new Dictionary<string, bool>();
            userControl_Set.GetValue(out Activity, out Air, out checkValue);

            int Year = Activity["Year"];
            int Month = Activity["Month"];
            int Day = Activity["Day"];
            int WeekDay = Activity["WeekDay"];

            int Hour = Activity["Hour"];
            int Minute = Activity["Minute"];
            int Second = Activity["Second"];

            int Battery = Activity["Battery"];
            int Calories = Activity["Calories"];
            int HeartRate = Activity["HeartRate"];
            int Distance = Activity["Distance"];
            int Steps = Activity["Steps"];
            int StepsGoal = Activity["StepsGoal"];

            int PAI;
            Activity.TryGetValue("PAI", out PAI);
            int StandUp;
            Activity.TryGetValue("StandUp", out StandUp);
            int Stress;
            Activity.TryGetValue("Stress", out Stress);
            int ActivityGoal;
            Activity.TryGetValue("ActivityGoal", out ActivityGoal);
            int FatBurning;
            Activity.TryGetValue("FatBurning", out FatBurning);


            int Weather_Icon;
            Air.TryGetValue("Weather_Icon", out Weather_Icon);
            int Temperature;
            Air.TryGetValue("Temperature", out Temperature);
            int TemperatureMax;
            Air.TryGetValue("TemperatureMax", out TemperatureMax);
            int TemperatureMin;
            Air.TryGetValue("TemperatureMin", out TemperatureMin);

            int UVindex;
            Air.TryGetValue("UVindex", out UVindex);
            int AirQuality;
            Air.TryGetValue("AirQuality", out AirQuality);
            int Humidity;
            Air.TryGetValue("Humidity", out Humidity);
            int WindForce;
            Air.TryGetValue("WindForce", out WindForce);
            int WindDirection;
            Air.TryGetValue("WindDirection", out WindDirection);
            int Altitude;
            Air.TryGetValue("Altitude", out Altitude);
            int AirPressure;
            Air.TryGetValue("AirPressure", out AirPressure);


            bool Bluetooth;
            checkValue.TryGetValue("Bluetooth", out Bluetooth);
            bool Alarm;
            checkValue.TryGetValue("Alarm", out Alarm);
            bool Lock;
            checkValue.TryGetValue("Lock", out Lock);
            bool DND;
            checkValue.TryGetValue("DND", out DND);

            bool ShowTemperature;
            checkValue.TryGetValue("ShowTemperature", out ShowTemperature);

            WatchFacePreviewSet.Date.Year = Year;
            WatchFacePreviewSet.Date.Month = Month;
            WatchFacePreviewSet.Date.Day = Day;
            WatchFacePreviewSet.Date.WeekDay = WeekDay;
            if (WatchFacePreviewSet.Date.WeekDay == 0) WatchFacePreviewSet.Date.WeekDay = 7;

            WatchFacePreviewSet.Time.Hours = Hour;
            WatchFacePreviewSet.Time.Minutes = Minute;
            WatchFacePreviewSet.Time.Seconds = Second;

            WatchFacePreviewSet.Battery = Battery;
            WatchFacePreviewSet.Activity.Calories = Calories;
            WatchFacePreviewSet.Activity.HeartRate = HeartRate;
            WatchFacePreviewSet.Activity.Distance = Distance;
            WatchFacePreviewSet.Activity.Steps = Steps;
            WatchFacePreviewSet.Activity.StepsGoal = StepsGoal;
            WatchFacePreviewSet.Activity.PAI = PAI;
            WatchFacePreviewSet.Activity.StandUp = StandUp;
            WatchFacePreviewSet.Activity.Stress = Stress;
            //WatchFacePreviewSet.Activity.ActivityGoal = ActivityGoal;
            WatchFacePreviewSet.Activity.FatBurning = FatBurning;

            WatchFacePreviewSet.Status.Bluetooth = Bluetooth;
            WatchFacePreviewSet.Status.Alarm = Alarm;
            WatchFacePreviewSet.Status.Lock = Lock;
            WatchFacePreviewSet.Status.DoNotDisturb = DND;

            WatchFacePreviewSet.Weather.Temperature = Temperature;
            WatchFacePreviewSet.Weather.TemperatureMax = TemperatureMax;
            WatchFacePreviewSet.Weather.TemperatureMin = TemperatureMin;
            //WatchFacePreviewSet.Weather.TemperatureNoData = !checkBox_WeatherSet_Temp.Checked;
            //WatchFacePreviewSet.Weather.TemperatureMinMaxNoData = !checkBox_WeatherSet_MaxMinTemp.Checked;
            WatchFacePreviewSet.Weather.Icon = Weather_Icon;

            WatchFacePreviewSet.Weather.showTemperature = ShowTemperature;

            WatchFacePreviewSet.Weather.UVindex = UVindex;
            WatchFacePreviewSet.Weather.AirQuality = AirQuality;
            WatchFacePreviewSet.Weather.Humidity = Humidity;
            WatchFacePreviewSet.Weather.WindForce = WindForce;
            WatchFacePreviewSet.Weather.WindDirection = WindDirection;
            WatchFacePreviewSet.Weather.Altitude = Altitude;
            WatchFacePreviewSet.Weather.AirPressure = AirPressure;
            WatchFacePreviewSet.SetNumber = userControl_Set.SetNumber;

            //SetDigitForPrewiev();
        }

        private void userCtrl_Set1_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set1);
            PreviewImage();
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 1;
        }

        private void userCtrl_Set2_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set2);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 2;
        }

        private void userCtrl_Set3_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set3);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 3;
        }

        private void userCtrl_Set4_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set4);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 4;
        }

        private void userCtrl_Set5_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set5);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 5;
        }

        private void userCtrl_Set6_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set6);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 6;
        }

        private void userCtrl_Set7_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set7);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 7;
        }

        private void userCtrl_Set8_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set8);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 8;
        }

        private void userCtrl_Set9_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set9);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 9;
        }

        private void userCtrl_Set10_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set10);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 10;
        }

        private void userCtrl_Set11_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set11);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set12.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 11;
        }

        private void userCtrl_Set12_Collapse(object sender, EventArgs eventArgs, int setNumber)
        {
            SetPreferences(userCtrl_Set12);
            PreviewImage();
            userCtrl_Set1.Collapsed = true;
            userCtrl_Set2.Collapsed = true;
            userCtrl_Set3.Collapsed = true;
            userCtrl_Set4.Collapsed = true;
            userCtrl_Set5.Collapsed = true;
            userCtrl_Set6.Collapsed = true;
            userCtrl_Set7.Collapsed = true;
            userCtrl_Set8.Collapsed = true;
            userCtrl_Set9.Collapsed = true;
            userCtrl_Set10.Collapsed = true;
            userCtrl_Set11.Collapsed = true;
            WatchFacePreviewSet.SetNumber = 12;
        }

        #endregion


        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Control UControl = (Control)sender;
            Panel panel = (Panel)UControl.Parent;
            if (panel != null) 
            { 
                panel.Tag = new object();
                cursorPos = Cursor.Position;
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Control UControl = (Control)sender;
            Panel panel = (Panel)UControl.Parent;
            if (panel != null && panel.Tag != null)
            {
                int cursorX = Cursor.Position.X;
                int cursorY = Cursor.Position.Y;
                int dX = Math.Abs(cursorX - cursorPos.X);
                int dY = Math.Abs(cursorY - cursorPos.Y);
                if (panel.Name == "panel_UC_Background" || panel.Name == "panel_UC_Shortcuts" || panel.Name == "panel_UC_DisconnectAlert" || 
                    panel.Name == "panel_UC_EditableTimePointer" || panel.Name == "panel_UC_EditableElements" || 
                    panel.Name == "panel_UC_RepeatingAlert" || panel.Name == "panel_UC_TopImage" || 
                    panel.Name == "panel_UC_Image")
                {
                    panel.DoDragDrop(sender, DragDropEffects.None);
                    return;
                }
                if (dX > 5 || dY > 5)
                    panel.DoDragDrop(sender, DragDropEffects.Move);
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            Control UControl = (Control)sender;
            Panel panel = (Panel)UControl.Parent;
            if (panel != null && panel.Tag != null) panel.Tag = null;
        }

        private void tableLayoutPanel1_DragOver(object sender, DragEventArgs e)
        {
            bool typeReturn = true;
            //return;
            //if (e.Data.GetDataPresent(typeof(UCtrl_Background_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_DigitalTime_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_AnalogTime_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_AnalogTimePro_Elm))) typeReturn = false;
            //if (e.Data.GetDataPresent(typeof(UCtrl_EditableTimePointer_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_DateDay_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_DateMonth_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_DateYear_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_DateWeek_Elm))) typeReturn = false;

            if (e.Data.GetDataPresent(typeof(UCtrl_Statuses_Elm))) typeReturn = false;
            //if (e.Data.GetDataPresent(typeof(UCtrl_Shortcuts_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Animation_Elm))) typeReturn = false;

            if (e.Data.GetDataPresent(typeof(UCtrl_Steps_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Battery_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Calories_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Heart_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_PAI_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Distance_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Stand_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Activity_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_SpO2_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Stress_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_FatBurning_Elm))) typeReturn = false;

            if (e.Data.GetDataPresent(typeof(UCtrl_Weather_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_UVIndex_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Humidity_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Altimeter_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Sunrise_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Wind_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Moon_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_Image_Elm))) typeReturn = false;
            //if (typeReturn) return;
            int dY = tableLayoutPanel_ElemetsWatchFace.PointToScreen(Point.Empty).Y;
            int posY = e.Y - dY;
            if (posY < 5) return;

            e.Effect = e.AllowedEffect;
            string[] objectName = e.Data.GetFormats();
            Panel draggedPanel = null;
            UserControl draggedUCtrl_Elm;
            if (objectName.Length > 0)
            {
                List<object> Elements = new List<object>();
                int index = -1;
                if (radioButton_ScreenNormal.Checked)
                {
                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Elements != null)
                        Elements = Watch_Face.ScreenNormal.Elements;
                }
                else
                {
                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Elements != null) 
                        Elements = Watch_Face.ScreenAOD.Elements;
                }

                switch (objectName[0])
                {
                    case "ControlLibrary.UCtrl_DigitalTime_Elm":
                        ElementDigitalTime digitalTime =
                            (ElementDigitalTime)Elements.Find(e1 => e1.GetType().Name == "ElementDigitalTime");
                        index = Elements.IndexOf(digitalTime);
                        draggedUCtrl_Elm = (UCtrl_DigitalTime_Elm)e.Data.GetData(typeof(UCtrl_DigitalTime_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_AnalogTime_Elm":
                        ElementAnalogTime analogTime =
                            (ElementAnalogTime)Elements.Find(e1 => e1.GetType().Name == "ElementAnalogTime");
                        index = Elements.IndexOf(analogTime);
                        draggedUCtrl_Elm = (UCtrl_AnalogTime_Elm)e.Data.GetData(typeof(UCtrl_AnalogTime_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_AnalogTimePro_Elm":
                        ElementAnalogTimePro analogTimePro =
                            (ElementAnalogTimePro)Elements.Find(e1 => e1.GetType().Name == "ElementAnalogTimePro");
                        index = Elements.IndexOf(analogTimePro);
                        draggedUCtrl_Elm = (UCtrl_AnalogTimePro_Elm)e.Data.GetData(typeof(UCtrl_AnalogTimePro_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_EditableTimePointer_Elm":
                        ElementEditablePointers editablePointers =
                            (ElementEditablePointers)Elements.Find(e1 => e1.GetType().Name == "ElementEditablePointers");
                        index = Elements.IndexOf(editablePointers);
                        draggedUCtrl_Elm = (UCtrl_EditableTimePointer_Elm)e.Data.GetData(typeof(UCtrl_EditableTimePointer_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_DateDay_Elm":
                        ElementDateDay dateDay =
                            (ElementDateDay)Elements.Find(e1 => e1.GetType().Name == "ElementDateDay");
                        index = Elements.IndexOf(dateDay);
                        draggedUCtrl_Elm = (UCtrl_DateDay_Elm)e.Data.GetData(typeof(UCtrl_DateDay_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_DateMonth_Elm":
                        ElementDateMonth dateMonth =
                            (ElementDateMonth)Elements.Find(e1 => e1.GetType().Name == "ElementDateMonth");
                        index = Elements.IndexOf(dateMonth);
                        draggedUCtrl_Elm = (UCtrl_DateMonth_Elm)e.Data.GetData(typeof(UCtrl_DateMonth_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_DateYear_Elm":
                        ElementDateYear dateYear =
                            (ElementDateYear)Elements.Find(e1 => e1.GetType().Name == "ElementDateYear");
                        index = Elements.IndexOf(dateYear);
                        draggedUCtrl_Elm = (UCtrl_DateYear_Elm)e.Data.GetData(typeof(UCtrl_DateYear_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_DateWeek_Elm":
                        ElementDateWeek dateWeek =
                            (ElementDateWeek)Elements.Find(e1 => e1.GetType().Name == "ElementDateWeek");
                        index = Elements.IndexOf(dateWeek);
                        draggedUCtrl_Elm = (UCtrl_DateWeek_Elm)e.Data.GetData(typeof(UCtrl_DateWeek_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;



                    case "ControlLibrary.UCtrl_Shortcuts_Elm":
                        //ElementShortcuts shortcuts =
                        //    (ElementShortcuts)Elements.Find(e1 => e1.GetType().Name == "ElementShortcuts");
                        //index = Elements.IndexOf(shortcuts);
                        draggedUCtrl_Elm = (UCtrl_Shortcuts_Elm)e.Data.GetData(typeof(UCtrl_Shortcuts_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Statuses_Elm":
                        ElementStatuses statuses =
                            (ElementStatuses)Elements.Find(e1 => e1.GetType().Name == "ElementStatuses");
                        index = Elements.IndexOf(statuses);
                        draggedUCtrl_Elm = (UCtrl_Statuses_Elm)e.Data.GetData(typeof(UCtrl_Statuses_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Animation_Elm":
                        ElementAnimation animation =
                            (ElementAnimation)Elements.Find(e1 => e1.GetType().Name == "ElementAnimation");
                        index = Elements.IndexOf(animation);
                        draggedUCtrl_Elm = (UCtrl_Animation_Elm)e.Data.GetData(typeof(UCtrl_Animation_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;



                    case "ControlLibrary.UCtrl_Steps_Elm":
                        ElementSteps steps =
                            (ElementSteps)Elements.Find(e1 => e1.GetType().Name == "ElementSteps");
                        index = Elements.IndexOf(steps);
                        draggedUCtrl_Elm = (UCtrl_Steps_Elm)e.Data.GetData(typeof(UCtrl_Steps_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Battery_Elm":
                        ElementBattery battery =
                            (ElementBattery)Elements.Find(e1 => e1.GetType().Name == "ElementBattery");
                        index = Elements.IndexOf(battery);
                        draggedUCtrl_Elm = (UCtrl_Battery_Elm)e.Data.GetData(typeof(UCtrl_Battery_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Calories_Elm":
                        ElementCalories calories =
                            (ElementCalories)Elements.Find(e1 => e1.GetType().Name == "ElementCalories");
                        index = Elements.IndexOf(calories);
                        draggedUCtrl_Elm = (UCtrl_Calories_Elm)e.Data.GetData(typeof(UCtrl_Calories_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Heart_Elm":
                        ElementHeart heart =
                            (ElementHeart)Elements.Find(e1 => e1.GetType().Name == "ElementHeart");
                        index = Elements.IndexOf(heart);
                        draggedUCtrl_Elm = (UCtrl_Heart_Elm)e.Data.GetData(typeof(UCtrl_Heart_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_PAI_Elm":
                        ElementPAI pai =
                            (ElementPAI)Elements.Find(e1 => e1.GetType().Name == "ElementPAI");
                        index = Elements.IndexOf(pai);
                        draggedUCtrl_Elm = (UCtrl_PAI_Elm)e.Data.GetData(typeof(UCtrl_PAI_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Distance_Elm":
                        ElementDistance distance =
                            (ElementDistance)Elements.Find(e1 => e1.GetType().Name == "ElementDistance");
                        index = Elements.IndexOf(distance);
                        draggedUCtrl_Elm = (UCtrl_Distance_Elm)e.Data.GetData(typeof(UCtrl_Distance_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Stand_Elm":
                        ElementStand stand =
                            (ElementStand)Elements.Find(e1 => e1.GetType().Name == "ElementStand");
                        index = Elements.IndexOf(stand);
                        draggedUCtrl_Elm = (UCtrl_Stand_Elm)e.Data.GetData(typeof(UCtrl_Stand_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Activity_Elm":
                        ElementActivity activity =
                            (ElementActivity)Elements.Find(e1 => e1.GetType().Name == "ElementActivity");
                        index = Elements.IndexOf(activity);
                        draggedUCtrl_Elm = (UCtrl_Activity_Elm)e.Data.GetData(typeof(UCtrl_Activity_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_SpO2_Elm":
                        ElementSpO2 spo2 =
                            (ElementSpO2)Elements.Find(e1 => e1.GetType().Name == "ElementSpO2");
                        index = Elements.IndexOf(spo2);
                        draggedUCtrl_Elm = (UCtrl_SpO2_Elm)e.Data.GetData(typeof(UCtrl_SpO2_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Stress_Elm":
                        ElementStress stress =
                            (ElementStress)Elements.Find(e1 => e1.GetType().Name == "ElementStress");
                        index = Elements.IndexOf(stress);
                        draggedUCtrl_Elm = (UCtrl_Stress_Elm)e.Data.GetData(typeof(UCtrl_Stress_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_FatBurning_Elm":
                        ElementFatBurning fat_burning =
                            (ElementFatBurning)Elements.Find(e1 => e1.GetType().Name == "ElementFatBurning");
                        index = Elements.IndexOf(fat_burning);
                        draggedUCtrl_Elm = (UCtrl_FatBurning_Elm)e.Data.GetData(typeof(UCtrl_FatBurning_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;



                    case "ControlLibrary.UCtrl_Weather_Elm":
                        ElementWeather weather =
                            (ElementWeather)Elements.Find(e1 => e1.GetType().Name == "ElementWeather");
                        index = Elements.IndexOf(weather);
                        draggedUCtrl_Elm = (UCtrl_Weather_Elm)e.Data.GetData(typeof(UCtrl_Weather_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_UVIndex_Elm":
                        ElementUVIndex uv_index =
                            (ElementUVIndex)Elements.Find(e1 => e1.GetType().Name == "ElementUVIndex");
                        index = Elements.IndexOf(uv_index);
                        draggedUCtrl_Elm = (UCtrl_UVIndex_Elm)e.Data.GetData(typeof(UCtrl_UVIndex_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Humidity_Elm":
                        ElementHumidity humidity =
                            (ElementHumidity)Elements.Find(e1 => e1.GetType().Name == "ElementHumidity");
                        index = Elements.IndexOf(humidity);
                        draggedUCtrl_Elm = (UCtrl_Humidity_Elm)e.Data.GetData(typeof(UCtrl_Humidity_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Altimeter_Elm":
                        ElementAltimeter altimeter =
                            (ElementAltimeter)Elements.Find(e1 => e1.GetType().Name == "ElementAltimeter");
                        index = Elements.IndexOf(altimeter);
                        draggedUCtrl_Elm = (UCtrl_Altimeter_Elm)e.Data.GetData(typeof(UCtrl_Altimeter_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Sunrise_Elm":
                        ElementSunrise sunrise =
                            (ElementSunrise)Elements.Find(e1 => e1.GetType().Name == "ElementSunrise");
                        index = Elements.IndexOf(sunrise);
                        draggedUCtrl_Elm = (UCtrl_Sunrise_Elm)e.Data.GetData(typeof(UCtrl_Sunrise_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Wind_Elm":
                        ElementWind wind =
                            (ElementWind)Elements.Find(e1 => e1.GetType().Name == "ElementWind");
                        index = Elements.IndexOf(wind);
                        draggedUCtrl_Elm = (UCtrl_Wind_Elm)e.Data.GetData(typeof(UCtrl_Wind_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Moon_Elm":
                        ElementMoon moon =
                            (ElementMoon)Elements.Find(e1 => e1.GetType().Name == "ElementMoon");
                        index = Elements.IndexOf(moon);
                        draggedUCtrl_Elm = (UCtrl_Moon_Elm)e.Data.GetData(typeof(UCtrl_Moon_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Image_Elm":
                        ElementImage image =
                            (ElementImage)Elements.Find(e1 => e1.GetType().Name == "ElementImage");
                        index = Elements.IndexOf(image);
                        draggedUCtrl_Elm = (UCtrl_Image_Elm)e.Data.GetData(typeof(UCtrl_Image_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_EditableElements_Elm":
                        //EditableElements editableElements =
                        //    (EditableElements)Elements.Find(e1 => e1.GetType().Name == "ElementEditablePointers");
                        //index = Elements.IndexOf(editablePointers);
                        draggedUCtrl_Elm = (UCtrl_EditableElements_Elm)e.Data.GetData(typeof(UCtrl_EditableElements_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_DisconnectAlert_Elm":
                        //EditableElements editableElements =
                        //    (EditableElements)Elements.Find(e1 => e1.GetType().Name == "ElementEditablePointers");
                        //index = Elements.IndexOf(editablePointers);
                        draggedUCtrl_Elm = (UCtrl_DisconnectAlert_Elm)e.Data.GetData(typeof(UCtrl_DisconnectAlert_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_Background_Elm":
                        //EditableElements editableElements =
                        //    (EditableElements)Elements.Find(e1 => e1.GetType().Name == "ElementEditablePointers");
                        //index = Elements.IndexOf(editablePointers);
                        draggedUCtrl_Elm = (UCtrl_Background_Elm)e.Data.GetData(typeof(UCtrl_Background_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_RepeatingAlert_Elm":
                        //EditableElements editableElements =
                        //    (EditableElements)Elements.Find(e1 => e1.GetType().Name == "ElementEditablePointers");
                        //index = Elements.IndexOf(editablePointers);
                        draggedUCtrl_Elm = (UCtrl_RepeatingAlert_Elm)e.Data.GetData(typeof(UCtrl_RepeatingAlert_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;

                    case "ControlLibrary.UCtrl_TopImage_Elm":
                        //EditableElements editableElements =
                        //    (EditableElements)Elements.Find(e1 => e1.GetType().Name == "ElementEditablePointers");
                        //index = Elements.IndexOf(editablePointers);
                        draggedUCtrl_Elm = (UCtrl_TopImage_Elm)e.Data.GetData(typeof(UCtrl_TopImage_Elm));
                        if (draggedUCtrl_Elm != null) draggedPanel = (Panel)draggedUCtrl_Elm.Parent;
                        break;
                }

                if (draggedPanel == null) return;
                if (typeReturn || index < 0)
                {
                    draggedPanel.Tag = null;
                    return;
                }

                Point pt = tableLayoutPanel_ElemetsWatchFace.PointToClient(new Point(e.X, e.Y));
                Control control = tableLayoutPanel_ElemetsWatchFace.GetChildAtPoint(pt);

                if (control != null)
                {
                    if (control.Name == "panel_UC_Background") return;
                    if (control.Name == "panel_UC_Shortcuts") return;
                    if (control.Name == "panel_UC_EditableTimePointer") return;
                    if (control.Name == "panel_UC_EditableElements") return;
                    if (control.Name == "panel_UC_DisconnectAlert") return;
                    if (control.Name == "panel_UC_RepeatingAlert") return;
                    var pos = tableLayoutPanel_ElemetsWatchFace.GetPositionFromControl(control);
                    var posOld = tableLayoutPanel_ElemetsWatchFace.GetPositionFromControl(draggedPanel);
                    int indexNew = tableLayoutPanel_ElemetsWatchFace.RowCount - 2 - pos.Row;

                    Console.WriteLine("pos.Row = " + pos.Row.ToString() + "     posOld.Row = " + posOld.Row.ToString());
                    //tableLayoutPanel1.Controls.Add(draggedButton, pos.Column, pos.Row);

                    if (pos != posOld)
                    {
                        if (pt.Y < control.Location.Y + draggedPanel.Height * 0.9)
                        {
                            tableLayoutPanel_ElemetsWatchFace.SetRow(draggedPanel, pos.Row);
                            if (pos.Row < posOld.Row) tableLayoutPanel_ElemetsWatchFace.SetRow(control, pos.Row + 1);
                            else tableLayoutPanel_ElemetsWatchFace.SetRow(control, pos.Row - 1);

                            //DragDropElements(sender, e);
                            if (indexNew >= 0 && indexNew < Elements.Count && index >= 0 && index < Elements.Count && indexNew != index)
                            {
                                if (indexNew > index)
                                {
                                    Elements.Insert(indexNew + 1, Elements[index]);
                                    Elements.RemoveAt(index);
                                }
                                else
                                {
                                    Elements.Insert(indexNew, Elements[index]);
                                    Elements.RemoveAt(index + 1);
                                    //object temp = Elements[index];
                                    //Elements.RemoveAt(index);
                                    //Elements.Insert(indexNew, temp);
                                }
                                JSON_Modified = true;
                                PreviewImage();
                                FormText();
                                //WATCH_FACE www = Watch_Face;
                            }
                        }
                    }
                    draggedPanel.Tag = null;
                }
            }
        }

        private void ShowElemenrOptions(string optionsName)
        {
            bool updatePreview = false;
            if (uCtrl_EditableBackground_Opt.Visible) updatePreview = true;
            if (uCtrl_EditableElements_Opt.Visible) updatePreview = true;
            if (uCtrl_EditableTimePointer_Opt.Visible) updatePreview = true;

            bool AOD = radioButton_ScreenIdle.Checked;
            HideAllElemenrOptions();
            switch (optionsName)
            {
                case "Background":
                    userCtrl_Background_Options.Visible = true;
                    userCtrl_Background_Options.AOD = AOD;
                    break;
                case "Text":
                    uCtrl_Text_Opt.Visible = true;
                    break;
                case "Text_rotation":
                    uCtrl_Text_Rotate_Opt.Visible = true;
                    break;
                case "Text_circle":
                    uCtrl_Text_Circle_Opt.Visible = true;
                    break;
                case "Text_Weather":
                    uCtrl_Text_Weather_Opt.Visible = true;
                    break;
                case "AmPm":
                    uCtrl_AmPm_Opt.Visible = true;
                    break;
                case "Pointer":
                    uCtrl_Pointer_Opt.Visible = true;
                    break;
                case "Images":
                    uCtrl_Images_Opt.Visible = true;
                    break;
                case "Segments":
                    uCtrl_Segments_Opt.Visible = true;
                    break;
                case "Circle_Scale":
                    uCtrl_Circle_Scale_Opt.Visible = true;
                    break;
                case "Linear_Scale":
                    uCtrl_Linear_Scale_Opt.Visible = true;
                    break;
                case "Icon":
                    uCtrl_Icon_Opt.Visible = true;
                    break;
                case "Shortcut":
                    uCtrl_Shortcut_Opt.Visible = true;
                    break;
                case "SystemFont":
                    uCtrl_Text_SystemFont_Opt.Visible = true;
                    break;
                case "FrameAnimation":
                    uCtrl_Animation_Frame_Opt.Visible = true;
                    break;
                case "MotionAnimation":
                    uCtrl_Animation_Motion_Opt.Visible = true;
                    break;
                case "RotateAnimation":
                    uCtrl_Animation_Rotate_Opt.Visible = true;
                    break;
                case "EditableTimePointer":
                    uCtrl_EditableTimePointer_Opt.Visible = true;
                    break;
                case "EditableElements":
                    uCtrl_EditableElements_Opt.Visible = true;
                    break;
                case "DisconnectAlert":
                    uCtrl_DisconnectAlert_Opt.Visible = true;
                    break;
                case "RepeatingAlert":
                    uCtrl_RepeatingAlert_Opt.Visible = true;
                    break;
                case "SmoothSecond":
                    uCtrl_SmoothSeconds_Opt.Visible = true;
                    break;
            }

            if (optionsName == "EditableTimePointer" || optionsName == "EditableElements" ||
                optionsName == "Background") updatePreview = !updatePreview;
            if (updatePreview) PreviewImage();
        }

        /// <summary>Скрывает все панели с настройками элементов</summary>
        private void HideAllElemenrOptions()
        {
            userCtrl_Background_Options.Visible = false;
            uCtrl_EditableBackground_Opt.Visible = false;
            uCtrl_Text_Opt.Visible = false;
            uCtrl_Text_Rotate_Opt.Visible = false;
            uCtrl_Text_Circle_Opt.Visible = false;
            uCtrl_Text_Weather_Opt.Visible = false;
            uCtrl_AmPm_Opt.Visible = false;
            uCtrl_Pointer_Opt.Visible = false;
            uCtrl_Images_Opt.Visible = false;
            uCtrl_Segments_Opt.Visible = false;
            uCtrl_Circle_Scale_Opt.Visible = false;
            uCtrl_Linear_Scale_Opt.Visible = false;
            uCtrl_Icon_Opt.Visible = false;
            uCtrl_Shortcut_Opt.Visible = false;
            uCtrl_Text_SystemFont_Opt.Visible = false;
            uCtrl_Animation_Frame_Opt.Visible = false;
            uCtrl_Animation_Motion_Opt.Visible = false;
            uCtrl_Animation_Rotate_Opt.Visible = false;
            uCtrl_EditableTimePointer_Opt.Visible = false;
            uCtrl_EditableElements_Opt.Visible = false;
            uCtrl_DisconnectAlert_Opt.Visible = false;
            uCtrl_RepeatingAlert_Opt.Visible = false;
            uCtrl_SmoothSeconds_Opt.Visible = false;
        }

        private void ResetHighlightState(string selectElementName)
        {
            if (selectElementName != "Background") uCtrl_Background_Elm.ResetHighlightState();
            if (selectElementName != "DigitalTime") uCtrl_DigitalTime_Elm.ResetHighlightState();
            if (selectElementName != "AnalogTime") uCtrl_AnalogTime_Elm.ResetHighlightState();
            if (selectElementName != "AnalogTimePro") uCtrl_AnalogTimePro_Elm.ResetHighlightState();
            if (selectElementName != "EditablePointers") uCtrl_EditableTimePointer_Elm.ResetHighlightState();
            if (selectElementName != "EditableElements") uCtrl_EditableElements_Elm.ResetHighlightState();
            if (selectElementName != "DateDay") uCtrl_DateDay_Elm.ResetHighlightState();
            if (selectElementName != "DateMonth") uCtrl_DateMonth_Elm.ResetHighlightState();
            if (selectElementName != "DateYear") uCtrl_DateYear_Elm.ResetHighlightState();
            if (selectElementName != "DateWeek") uCtrl_DateWeek_Elm.ResetHighlightState();
            if (selectElementName != "Shortcuts") uCtrl_Shortcuts_Elm.ResetHighlightState();
            if (selectElementName != "Statuses") uCtrl_Statuses_Elm.ResetHighlightState();
            if (selectElementName != "Animation") uCtrl_Animation_Elm.ResetHighlightState();

            if (selectElementName != "Steps") uCtrl_Steps_Elm.ResetHighlightState();
            if (selectElementName != "Battery") uCtrl_Battery_Elm.ResetHighlightState();
            if (selectElementName != "Calories") uCtrl_Calories_Elm.ResetHighlightState();
            if (selectElementName != "Heart") uCtrl_Heart_Elm.ResetHighlightState();
            if (selectElementName != "PAI") uCtrl_PAI_Elm.ResetHighlightState();
            if (selectElementName != "Distance") uCtrl_Distance_Elm.ResetHighlightState();
            if (selectElementName != "Stand") uCtrl_Stand_Elm.ResetHighlightState();
            if (selectElementName != "Activity") uCtrl_Activity_Elm.ResetHighlightState();
            if (selectElementName != "SpO2") uCtrl_SpO2_Elm.ResetHighlightState();
            if (selectElementName != "Stress") uCtrl_Stress_Elm.ResetHighlightState();
            if (selectElementName != "FatBurning") uCtrl_FatBurning_Elm.ResetHighlightState();

            if (selectElementName != "Weather") uCtrl_Weather_Elm.ResetHighlightState();
            if (selectElementName != "UVIndex") uCtrl_UVIndex_Elm.ResetHighlightState();
            if (selectElementName != "Humidity") uCtrl_Humidity_Elm.ResetHighlightState();
            if (selectElementName != "Altimeter") uCtrl_Altimeter_Elm.ResetHighlightState();
            if (selectElementName != "Sunrise") uCtrl_Sunrise_Elm.ResetHighlightState();
            if (selectElementName != "Wind") uCtrl_Wind_Elm.ResetHighlightState();
            if (selectElementName != "Moon") uCtrl_Moon_Elm.ResetHighlightState();
            if (selectElementName != "Image") uCtrl_Image_Elm.ResetHighlightState();

            if (selectElementName != "DisconnectAlert") uCtrl_DisconnectAlert_Elm.ResetHighlightState();
            if (selectElementName != "RepeatingAlert") uCtrl_RepeatingAlert_Elm.ResetHighlightState();
            if (selectElementName != "TopImage") uCtrl_TopImage_Elm.ResetHighlightState();


            if (selectElementName != "Animation") 
            {
                if (button_Add_Anim_Images.Visible)
                {
                    button_Add_Anim_Images.Visible = false;
                    button_Add_Images.Visible = true;
                    dataGridView_AnimImagesList.Visible = false;
                    dataGridView_ImagesList.Visible = true;
                }
            }
            else
            {
                if (!button_Add_Anim_Images.Visible)
                {
                    button_Add_Anim_Images.Visible = true;
                    button_Add_Images.Visible = false;
                    dataGridView_AnimImagesList.Visible = true;
                    dataGridView_ImagesList.Visible = false;
                }
            }
        }

        private void ClearAllElemenrOptions()
        {
            uCtrl_Background_Elm.SettingsClear();
            uCtrl_DigitalTime_Elm.SettingsClear();
            uCtrl_AnalogTime_Elm.SettingsClear();
            uCtrl_AnalogTimePro_Elm.SettingsClear();
            uCtrl_EditableTimePointer_Elm.SettingsClear();
            uCtrl_EditableElements_Elm.SettingsClear();

            uCtrl_DateDay_Elm.SettingsClear();
            uCtrl_DateMonth_Elm.SettingsClear();
            uCtrl_DateYear_Elm.SettingsClear();
            uCtrl_DateWeek_Elm.SettingsClear();

            uCtrl_Statuses_Elm.SettingsClear();
            uCtrl_Shortcuts_Elm.SettingsClear();
            uCtrl_Animation_Elm.SettingsClear();

            uCtrl_Steps_Elm.SettingsClear();
            uCtrl_Battery_Elm.SettingsClear();
            uCtrl_Calories_Elm.SettingsClear();
            uCtrl_Heart_Elm.SettingsClear();
            uCtrl_PAI_Elm.SettingsClear();
            uCtrl_Distance_Elm.SettingsClear();
            uCtrl_Stand_Elm.SettingsClear();
            uCtrl_Activity_Elm.SettingsClear();
            uCtrl_SpO2_Elm.SettingsClear();
            uCtrl_Stress_Elm.SettingsClear();
            uCtrl_FatBurning_Elm.SettingsClear();

            uCtrl_Weather_Elm.SettingsClear();
            uCtrl_UVIndex_Elm.SettingsClear();
            uCtrl_Humidity_Elm.SettingsClear();
            uCtrl_Altimeter_Elm.SettingsClear();
            uCtrl_Sunrise_Elm.SettingsClear();
            uCtrl_Wind_Elm.SettingsClear();
            uCtrl_Moon_Elm.SettingsClear();
            uCtrl_Image_Elm.SettingsClear();

            uCtrl_DisconnectAlert_Elm.SettingsClear();
            uCtrl_RepeatingAlert_Elm.SettingsClear();
            uCtrl_TopImage_Elm.SettingsClear();
        }

        private void uCtrl_Background_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("Background");

            string preview = "";
            int id = 0;
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null) 
            {
                preview = Watch_Face.WatchFace_Info.Preview;
                id = Watch_Face.WatchFace_Info.WatchFaceId;
            }
            bool editable_background = false; 
            bool Editable_background_ShowOnAOD = false;
            Background background = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Background != null) background = Watch_Face.ScreenNormal.Background;
                editable_background = true;
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Background != null) background = Watch_Face.ScreenAOD.Background;
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Background != null && Watch_Face.ScreenNormal.Background.visible &&
                    Watch_Face.ScreenNormal.Background.Editable_Background != null &&
                    Watch_Face.ScreenNormal.Background.Editable_Background.enable_edit_bg) 
                { 
                    editable_background = true;
                    if (Watch_Face.ScreenNormal.Background.Editable_Background.AOD_show) Editable_background_ShowOnAOD = true;
                }
            }
            ShowElemenrOptions("Background");
            Read_Background_Options(background, editable_background, Editable_background_ShowOnAOD, preview, id);
        }

        private void uCtrl_DigitalTime_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_DigitalTime_Elm.selectedElement;
            if(selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("DigitalTime");

            ElementDigitalTime digitalTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) 
                {
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }
            if (digitalTime != null)
            {
                hmUI_widget_IMG_NUMBER img_number = null;

                switch (selectElement)
                {
                    case "Hour":
                        if (uCtrl_DigitalTime_Elm.checkBox_Hours.Checked)
                        {
                            img_number = digitalTime.Hour;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Minute":
                        if (uCtrl_DigitalTime_Elm.checkBox_Minutes.Checked)
                        {
                            img_number = digitalTime.Minute;
                            Read_ImgNumber_Options(img_number, false, true, Properties.FormStrings.FollowMinute, false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Second":
                        if (uCtrl_DigitalTime_Elm.checkBox_Seconds.Checked)
                        {
                            img_number = digitalTime.Second;
                            Read_ImgNumber_Options(img_number, false, true, Properties.FormStrings.FollowSecond, false, false, true, true);
                            ShowElemenrOptions("Text"); 
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "AmPm":
                        if (uCtrl_DigitalTime_Elm.checkBox_AmPm.Checked)
                        {
                            hmUI_widget_IMG_TIME_am_pm am_pm = digitalTime.AmPm;
                            Read_AM_PM_Options(am_pm);
                            ShowElemenrOptions("AmPm");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }

            //JSON_Modified = true;
            //PreviewImage();
            //FormText();
        }

        private void uCtrl_AnalogTime_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_AnalogTime_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("AnalogTime");

            ElementAnalogTime analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }
            if (analogTime != null)
            {
                hmUI_widget_IMG_POINTER img_pointer = null;

                switch (selectElement)
                {
                    case "Hour":
                        if (uCtrl_AnalogTime_Elm.checkBox_Hours.Checked)
                        {
                            img_pointer = analogTime.Hour;
                            Read_ImgPointer_Options(img_pointer, false);
                            uCtrl_Pointer_Opt.TimeMode = true;
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Minute":
                        if (uCtrl_AnalogTime_Elm.checkBox_Minutes.Checked)
                        {
                            img_pointer = analogTime.Minute;
                            Read_ImgPointer_Options(img_pointer, false);
                            uCtrl_Pointer_Opt.TimeMode = true;
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Second":
                        if (uCtrl_AnalogTime_Elm.checkBox_Seconds.Checked)
                        {
                            img_pointer = analogTime.Second;
                            Read_ImgPointer_Options(img_pointer, false);
                            uCtrl_Pointer_Opt.TimeMode = true;
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }
        private void uCtrl_AnalogTimePro_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_AnalogTimePro_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("AnalogTimePro");

            ElementAnalogTimePro analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }
            if (analogTime != null)
            {
                hmUI_widget_IMG_POINTER img_pointer = null;

                switch (selectElement)
                {
                    case "Hour":
                        if (uCtrl_AnalogTimePro_Elm.checkBox_Hours.Checked)
                        {
                            img_pointer = analogTime.Hour;
                            Read_ImgPointer_Options(img_pointer, false);
                            uCtrl_Pointer_Opt.TimeMode = false;
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Minute":
                        if (uCtrl_AnalogTimePro_Elm.checkBox_Minutes.Checked)
                        {
                            img_pointer = analogTime.Minute;
                            Read_ImgPointer_Options(img_pointer, false);
                            uCtrl_Pointer_Opt.TimeMode = false;
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Second":
                        if (uCtrl_AnalogTimePro_Elm.checkBox_Seconds.Checked)
                        {
                            img_pointer = analogTime.Second;
                            Read_ImgPointer_Options(img_pointer, false);
                            uCtrl_Pointer_Opt.TimeMode = false;
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;

                    case "SmoothSecond":
                        if (uCtrl_AnalogTimePro_Elm.checkBox_SmoothSeconds.Checked)
                        {
                            Smooth_Second smoothSecond = analogTime.SmoothSecond;
                            Read_Smooth_Second_Options(smoothSecond);
                            uCtrl_SmoothSeconds_Opt.AOD = radioButton_ScreenIdle.Checked;
                            if (radioButton_ScreenIdle.Checked && smoothSecond == null) uCtrl_SmoothSeconds_Opt.radioButton_type2.Checked = true;
                            ShowElemenrOptions("SmoothSecond");
                        }
                        else HideAllElemenrOptions();
                        break;

                    case "Format_24hour":
                        HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_EditableTimePointer_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("EditablePointers");

            ElementEditablePointers editablePointers = null;
            if (Watch_Face != null && Watch_Face.ElementEditablePointers != null) editablePointers = Watch_Face.ElementEditablePointers;
            
            if (editablePointers != null)
            {
                //hmUI_widget_IMG_NUMBER img_number = null;

                //if (SpO2.Number == null) SpO2.Number = new hmUI_widget_IMG_NUMBER();
                //img_number = SpO2.Number;
                Read_EditablePointers_Options(editablePointers);
                ShowElemenrOptions("EditableTimePointer");

            }
        }

        private void uCtrl_EditableElements_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("EditableElements");

            EditableElements editableElement = null;
            if (Watch_Face != null && Watch_Face.Editable_Elements != null) editableElement = Watch_Face.Editable_Elements;

            if (editableElement != null)
            {
                //hmUI_widget_IMG_NUMBER img_number = null;

                //if (SpO2.Number == null) SpO2.Number = new hmUI_widget_IMG_NUMBER();
                //img_number = SpO2.Number;
                Read_EditableElements_Options(editableElement);
                ShowElemenrOptions("EditableElements");

            }
        }

        private void button_JSON_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* JSON");
            // сохранение если файл не сохранен
            if (SaveRequest() == DialogResult.Cancel) return;

            //string subPath = Application.StartupPath + @"\Watch_face\";
            //if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FullFileDir;
            openFileDialog.FileName = FileName;
            openFileDialog.Filter = Properties.FormStrings.FilterJson;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_Dial_Settings;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = Path.GetFileName(openFileDialog.FileName);
                FullFileDir = Path.GetDirectoryName(openFileDialog.FileName);

                Logger.WriteLine("* JSON_Click");
                string newFullName = openFileDialog.FileName;
                //string dirName = Path.GetDirectoryName(newFullName) + @"\assets\";

                LoadJson(newFullName);

            }
            Logger.WriteLine("* JSON (end)");
        }

        private void LoadJson(string fileName)
        {
            string text = File.ReadAllText(fileName);
            Watch_Face = TextToJson(text);

            // отображение кнопок создания картинки предпросмотра
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null && Watch_Face.WatchFace_Info.Preview != null)
            {
                button_RefreshPreview.Visible = true;
                button_CreatePreview.Visible = false;
            }
            else
            {
                button_RefreshPreview.Visible = false;
                if (FileName != null && FullFileDir != null)
                {
                    button_CreatePreview.Visible = true;
                }
                else
                {
                    button_CreatePreview.Visible = false;
                }
            }

            PreviewView = false;
            string dirName = Path.GetDirectoryName(fileName) + @"\assets\";
            // устанавливаем настройки для предпросмотра
            fileName = Path.Combine(Path.GetDirectoryName(fileName), "Preview.States");
            if (File.Exists(fileName) && (ProgramSettings.Settings_Open_Download || ProgramSettings.Settings_Open_Dialog))
            {
                Logger.WriteLine("Load Preview.States");
                if (ProgramSettings.Settings_Open_Download)
                {
                    JsonPreview_Read(fileName);
                }
                else if (ProgramSettings.Settings_Open_Dialog)
                {
                    if (MessageBox.Show(Properties.FormStrings.Message_LoadPreviewStates_Text,
                        Properties.FormStrings.Message_LoadPreviewStates_Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        JsonPreview_Read(fileName);
                    }
                }
            }
            else if (ProgramSettings.Settings_Open_Download_Your_File &&
                File.Exists(ProgramSettings.PreviewStates_Path))
            {
                JsonPreview_Read(ProgramSettings.PreviewStates_Path);
            }
            else StartJsonPreview(); 

            LoadImage(dirName);
            button_Add_Images.Enabled = true;
            button_Add_Anim_Images.Enabled = true;
            ShowElemetsWatchFace();
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null && Watch_Face.WatchFace_Info.DeviceName != null)
            {
                switch (Watch_Face.WatchFace_Info.DeviceName)
                {
                    case "GTR3":
                        comboBox_watch_model.Text = "GTR 3";
                        break;
                    case "GTR3_Pro":
                        comboBox_watch_model.Text = "GTR 3 Pro";
                        break;
                    case "GTS3":
                        comboBox_watch_model.Text = "GTS 3";
                        break;
                    case "T_Rex_2":
                        comboBox_watch_model.Text = "T-Rex 2";
                        break;
                    case "T_Rex_Ultra":
                        comboBox_watch_model.Text = "T-Rex Ultra";
                        break;
                    case "GTR4":
                        comboBox_watch_model.Text = "GTR 4";
                        break;
                    case "Amazfit_Band_7":
                        comboBox_watch_model.Text = "Amazfit Band 7";
                        break;
                    case "GTS4_mini":
                        comboBox_watch_model.Text = "GTS 4 mini";
                        break;
                    case "GTR_mini":
                        comboBox_watch_model.Text = "GTR mini";
                        break;
                    case "Falcon":
                        comboBox_watch_model.Text = "Falcon";
                        break;
                    case "GTS4":
                        comboBox_watch_model.Text = "GTS 4";
                        break;
                    default:
                        comboBox_watch_model.Text = "GTR 3";
                        break;
                }
            }
            PreviewView = true;

            JSON_Modified = false;
            PreviewImage();
            FormText();

            groupBox_AddElemets.Enabled = true;
        }

        /// <summary>Загружаем файлы изображений в проект и в выпадающие списки</summary>
        /// <param name="dirName">Папка с изображениями</param>
        private void LoadImage(string dirName)
        {
            Logger.WriteLine("* LoadImage");

            dataGridView_ImagesList.Rows.Clear();
            ListImages.Clear();
            ListImagesFullName.Clear();


            dataGridView_AnimImagesList.Rows.Clear();
            ListAnimImages.Clear();
            ListAnimImagesFullName.Clear();

            if (!Directory.Exists(dirName)) return;

            DirectoryInfo Folder;
            Folder = new DirectoryInfo(dirName);
            //FileInfo[] Images;
            //Images = Folder.GetFiles("*.png").OrderBy(p => Path.GetFileNameWithoutExtension(p.Name)).ToArray();
            FileInfo[] Images = Folder.GetFiles("*.png");
            Images = FileInfoSort(Images);
            //Array.Sort(Images, new MyCustomComparer()); выдает ошибку
            Image loadedImage = null; 
            int count = 1;

            //progressBar1.Value = 0;
            //progressBar1.Maximum = Images.Length;
            //progressBar1.Visible = true;
            foreach (FileInfo file in Images)
            {
                try
                {
                    string fileNameOnly = Path.GetFileNameWithoutExtension(file.Name);
                    Logger.WriteLine("loadedImage " + fileNameOnly);
                    //loadedImage = Image.FromFile(file.FullName);
                    using (FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                    {
                        loadedImage = Image.FromStream(stream);
                    }

                    var RowNew = new DataGridViewRow();
                    DataGridViewImageCellLayout ZoomType = DataGridViewImageCellLayout.Zoom;
                    if ((loadedImage.Height < 45) && (loadedImage.Width < 110))
                        ZoomType = DataGridViewImageCellLayout.Normal;
                    RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = count.ToString() });
                    RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = fileNameOnly });
                    RowNew.Cells.Add(new DataGridViewImageCell()
                    {
                        Value = loadedImage,
                        ImageLayout = ZoomType,

                    });
                    RowNew.Cells.Add(new DataGridViewImageCell()
                    {
                        Value = loadedImage,
                        ImageLayout = ZoomType,

                    });
                    //loadedImage.Dispose();
                    RowNew.Height = 45;
                    dataGridView_ImagesList.Rows.Add(RowNew);
                    //progressBar1.Value++;
                    count++;
                    ListImages.Add(fileNameOnly);
                    ListImagesFullName.Add(file.FullName);
                }
                catch
                {
                    // Could not load the image - probably related to Windows file system permissions.
                    MessageBox.Show(Properties.FormStrings.Message_Error_Image_Text1 +
                        file.FullName.Substring(file.FullName.LastIndexOf('\\') + 1) + Properties.FormStrings.Message_Error_Image_Text2);
                }
            }

            userCtrl_Background_Options.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_EditableBackground_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Text_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Text_Rotate_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Text_Circle_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Text_Weather_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_AmPm_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Pointer_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Images_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Segments_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Linear_Scale_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Icon_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_Shortcut_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_EditableElements_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            uCtrl_EditableTimePointer_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            //uCtrl_Animation_Frame_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            //uCtrl_Animation_Motion_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
            //uCtrl_Animation_Rotate_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);

            //progressBar1.Visible = false;
            LoadAnimImage(dirName + @"animation\");
        }

        /// <summary>Загружаем файлы изображений для анимации в проект и в выпадающие списки</summary>
        /// <param name="dirName">Папка с изображениями анимации</param>
        private void LoadAnimImage(string dirName)
        {
            Logger.WriteLine("* LoadAnimImage");
            if (!Directory.Exists(dirName)) return;

            DirectoryInfo Folder;
            Folder = new DirectoryInfo(dirName);
            //FileInfo[] Images;
            //Images = Folder.GetFiles("*.png").OrderBy(p => Path.GetFileNameWithoutExtension(p.Name)).ToArray();
            FileInfo[] Images = Folder.GetFiles("*.png");
            Images = FileInfoSort(Images);
            //Array.Sort(Images, new MyCustomComparer()); выдает ошибку
            Image loadedImage = null;
            int count = 1;

            //progressBar1.Value = 0;
            //progressBar1.Maximum = Images.Length;
            //progressBar1.Visible = true;
            foreach (FileInfo file in Images)
            {
                try
                {
                    string fileNameOnly = Path.GetFileNameWithoutExtension(file.Name);
                    Logger.WriteLine("loadedAnimImage " + fileNameOnly);
                    //loadedImage = Image.FromFile(file.FullName);
                    using (FileStream stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                    {
                        loadedImage = Image.FromStream(stream);
                    }

                    var RowNew = new DataGridViewRow();
                    DataGridViewImageCellLayout ZoomType = DataGridViewImageCellLayout.Zoom;
                    if ((loadedImage.Height < 45) && (loadedImage.Width < 110))
                        ZoomType = DataGridViewImageCellLayout.Normal;
                    RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = count.ToString() });
                    RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = fileNameOnly });
                    RowNew.Cells.Add(new DataGridViewImageCell()
                    {
                        Value = loadedImage,
                        ImageLayout = ZoomType,

                    });
                    RowNew.Cells.Add(new DataGridViewImageCell()
                    {
                        Value = loadedImage,
                        ImageLayout = ZoomType,

                    });
                    //loadedImage.Dispose();
                    RowNew.Height = 45;
                    dataGridView_AnimImagesList.Rows.Add(RowNew);
                    //progressBar1.Value++;
                    count++;
                    ListAnimImages.Add(fileNameOnly);
                    ListAnimImagesFullName.Add(file.FullName);
                }
                catch
                {
                    // Could not load the image - probably related to Windows file system permissions.
                    MessageBox.Show(Properties.FormStrings.Message_Error_Image_Text1 +
                        file.FullName.Substring(file.FullName.LastIndexOf('\\') + 1) + Properties.FormStrings.Message_Error_Image_Text2);
                }
            }

            uCtrl_Animation_Frame_Opt.ComboBoxAddItems(ListAnimImages, ListAnimImagesFullName);
            uCtrl_Animation_Motion_Opt.ComboBoxAddItems(ListAnimImages, ListAnimImagesFullName);
            uCtrl_Animation_Rotate_Opt.ComboBoxAddItems(ListAnimImages, ListAnimImagesFullName);

            //progressBar1.Visible = false;
        }

        private void comboBox_AddElements_Click(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.Items.RemoveAt(0);
        }

        private void button_New_Project_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* New_Project");
            // сохранение если файл не сохранен
            if (SaveRequest() == DialogResult.Cancel) return;

            string subPath = Application.StartupPath + @"\Watch_face\";
            if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);

            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.InitialDirectory = subPath;
            openFileDialog.FileName = "New_Project";
            openFileDialog.Filter = Properties.FormStrings.FilterJson;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            openFileDialog.RestoreDirectory = false;
            //openFileDialog.Multiselect = false;
            //openFileDialog.CheckFileExists = true;
            //openFileDialog.CreatePrompt = true;
            openFileDialog.DefaultExt = "json";
            openFileDialog.ValidateNames = true;
            openFileDialog.OverwritePrompt = true;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_New_Project;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Logger.WriteLine("* New_Project_Click");
                string fullfilename = openFileDialog.FileName;
                string dirName = Path.GetDirectoryName(fullfilename) + @"\assets\";
                if (Directory.Exists(dirName)) 
                {
                    DialogResult dialogResult = MessageBox.Show(Properties.FormStrings.Message_Warning_Assets_Exist1 + 
                        Environment.NewLine + Properties.FormStrings.Message_Warning_Assets_Exist2 + Environment.NewLine +
                        Properties.FormStrings.Message_Warning_Assets_Exist3, Properties.FormStrings.Message_Warning_Caption, 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No) return;
                }
                if (Path.GetExtension(fullfilename) != ".json") fullfilename = fullfilename + ".json";
                FileName = Path.GetFileName(fullfilename);
                FullFileDir = Path.GetDirectoryName(fullfilename);

                Watch_Face = new WATCH_FACE();
                Watch_Face.WatchFace_Info = new WatchFace_Info();
                Random rnd = new Random();
                int rndID = rnd.Next(1000, 10000000);
                Watch_Face.WatchFace_Info.WatchFaceId = rndID;

                Watch_Face.ScreenNormal = new ScreenNormal();
                Watch_Face.ScreenNormal.Background = new Background();
                Watch_Face.ScreenNormal.Background.BackgroundColor = new hmUI_widget_FILL_RECT();

                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                        break;
                    case "GTR 3 Pro":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 480;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 480;
                        break;
                    case "GTS 3":
                        Watch_Face.WatchFace_Info.DeviceName = "GTS3";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                        break;
                    case "T-Rex 2":
                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_2";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                        break;
                    case "T-Rex Ultra":
                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_Ultra";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                        break;
                    case "GTR 4":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 466;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 466;
                        break;
                    case "Amazfit Band 7":
                        Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 368;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 194;
                        break;
                    case "GTS 4 mini":
                        Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 384;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 336;
                        break;
                    case "Falcon":
                        Watch_Face.WatchFace_Info.DeviceName = "Falcon";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 416;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 416;
                        break;
                    case "GTR mini":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR_mini";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 416;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 416;
                        break;
                    case "GTS 4":
                        Watch_Face.WatchFace_Info.DeviceName = "GTS4";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                        break;
                }

                string JSON_String = JsonConvert.SerializeObject(Watch_Face, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(fullfilename, JSON_String, Encoding.UTF8);
                button_Add_Images.Enabled = true;
                button_Add_Anim_Images.Enabled = true;
                Directory.CreateDirectory(dirName);
                LoadImage(dirName);
                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
                groupBox_AddElemets.Enabled = true;

                PreviewImage();
                FormText();
            }
            Logger.WriteLine("* New_Project (end)");
        }

        private void button_New_Project_Click_New(object sender, EventArgs e)
        {
            Logger.WriteLine("* New_Project");
            // сохранение если файл не сохранен
            if (SaveRequest() == DialogResult.Cancel) return;

            string subPath = Application.StartupPath + @"\Watch_face\";
            if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            dialog.InitialDirectory = subPath;
            //dialog.DefaultFileName = "New_Project";
            //dialog.FileName = "New_Project";
            //dialog.Filter = Properties.FormStrings.FilterJson;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            dialog.RestoreDirectory = true;
            //openFileDialog.Multiselect = false;
            //openFileDialog.CheckFileExists = true;
            //openFileDialog.CreatePrompt = true;
            //dialog.DefaultExt = "json";
            dialog.EnsureValidNames = false;
            //dialog.OverwritePrompt = true;
            dialog.Title = Properties.FormStrings.Dialog_Title_New_Project;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Logger.WriteLine("* New_Project_Click");
                string fullfilename =Path.GetFileNameWithoutExtension(dialog.FileName);
                fullfilename = Path.Combine(dialog.FileName, fullfilename)+".json";
                string dirName = Path.GetDirectoryName(fullfilename) + @"\assets\";
                if (Directory.Exists(dirName))
                {
                    DialogResult dialogResult = MessageBox.Show(Properties.FormStrings.Message_Warning_Assets_Exist1 +
                        Environment.NewLine + Properties.FormStrings.Message_Warning_Assets_Exist2 + Environment.NewLine +
                        Properties.FormStrings.Message_Warning_Assets_Exist3, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No) return;
                }
                if (Path.GetExtension(fullfilename) != ".json") fullfilename = fullfilename + ".json";
                FileName = Path.GetFileName(fullfilename);
                FullFileDir = Path.GetDirectoryName(fullfilename);

                Watch_Face = new WATCH_FACE();
                Watch_Face.WatchFace_Info = new WatchFace_Info();
                Random rnd = new Random();
                int rndID = rnd.Next(1000, 10000000);
                Watch_Face.WatchFace_Info.WatchFaceId = rndID;

                Watch_Face.ScreenNormal = new ScreenNormal();
                Watch_Face.ScreenNormal.Background = new Background();
                Watch_Face.ScreenNormal.Background.BackgroundColor = new hmUI_widget_FILL_RECT();

                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                        break;
                    case "GTR 3 Pro":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 480;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 480;
                        break;
                    case "GTS 3":
                        Watch_Face.WatchFace_Info.DeviceName = "GTS3";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                        break;
                    case "T-Rex 2":
                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_2";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                        break;
                    case "T-Rex Ultra":
                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_Ultra";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                        break;
                    case "GTR 4":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 466;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 466;
                        break;
                    case "Amazfit Band 7":
                        Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 368;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 194;
                        break;
                    case "GTS 4 mini":
                        Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 384;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 336;
                        break;
                    case "Falcon":
                        Watch_Face.WatchFace_Info.DeviceName = "Falcon";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 416;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 416;
                        break;
                    case "GTR mini":
                        Watch_Face.WatchFace_Info.DeviceName = "GTR_mini";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 416;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 416;
                        break;
                    case "GTS 4":
                        Watch_Face.WatchFace_Info.DeviceName = "GTS4";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                        break;
                }

                string JSON_String = JsonConvert.SerializeObject(Watch_Face, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(fullfilename, JSON_String, Encoding.UTF8);
                button_Add_Images.Enabled = true;
                button_Add_Anim_Images.Enabled = true;
                Directory.CreateDirectory(dirName);
                LoadImage(dirName);
                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
                groupBox_AddElemets.Enabled = true;

                PreviewImage();
                FormText();
            }
            Logger.WriteLine("* New_Project (end)");
        }

        private void button_Add_Images_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* Add_Images");
            if (FullFileDir == null) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = FullFileDir;
            openFileDialog.FileName = FileName;
            openFileDialog.Filter = Properties.FormStrings.FilterPng;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_Dial_Settings;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //FileName = Path.GetFileName(openFileDialog.FileName);
                //FullFileDir = Path.GetDirectoryName(openFileDialog.FileName);

                Logger.WriteLine("* Add_Images_Click");
                string dirName = FullFileDir + @"\assets\";
                foreach(string fileFullName in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileName(fileFullName);
                    fileName = fileName.Replace(" ", "_");
                    string newFileName = dirName + fileName;
                    if (File.Exists(newFileName))
                    {
                        DialogResult dialogResult = MessageBox.Show(Properties.FormStrings.Message_Warning_Image_Exist1
                            + fileName + Environment.NewLine + Properties.FormStrings.Message_Warning_Image_Exist2, 
                            Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (dialogResult == DialogResult.Yes) File.Copy(fileFullName, newFileName, true); ;
                    }
                    else File.Copy(fileFullName, newFileName, true);
                }
                LoadImage(dirName);
            }
            Logger.WriteLine("* Add_Images (end)");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                // Do what you want here
                if (FileName != null)
                {
                    string fullfilename = Path.Combine(FullFileDir, FileName);
                    if (File.Exists(fullfilename))
                    {
                        save_JSON_File(fullfilename);
                    };

                    JSON_Modified = false;
                    FormText();
                    //if (checkBox_JsonWarnings.Checked) jsonWarnings(fullfilename);
                }
                else
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = FullFileDir;
                    saveFileDialog.FileName = FileName; if (FileName == null || FileName.Length == 0)
                    {
                        if (FullFileDir != null && FullFileDir.Length > 3)
                        {
                            saveFileDialog.FileName = Path.GetFileName(FullFileDir);
                        }
                    }
                    saveFileDialog.Filter = Properties.FormStrings.FilterJson;

                    //openFileDialog.Filter = "Json files (*.json) | *.json";
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.Title = Properties.FormStrings.Dialog_Title_Dial_Settings;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fullfilename = saveFileDialog.FileName;
                        save_JSON_File(fullfilename);

                        FileName = Path.GetFileName(fullfilename);
                        FullFileDir = Path.GetDirectoryName(fullfilename);
                        JSON_Modified = false;
                        FormText();
                        //if (checkBox_JsonWarnings.Checked) jsonWarnings(fullfilename);
                    }
                }
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }

            //if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            //{
            //    if (uCtrl_Text_Opt.Visible) uCtrl_Text_Opt.CursorKeyPressed(e.KeyCode.ToString());
            //}
        }

        private void save_JSON_File(String fullfilename)
        {
            if (Watch_Face == null) return;
            string JSON_String = JsonConvert.SerializeObject(Watch_Face, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText(fullfilename, JSON_String, Encoding.UTF8);
        }

        // формируем изображение для предпросмотра
        private void PreviewImage()
        {
            if (!PreviewView) return;
            Logger.WriteLine("* PreviewImage");
            //Graphics gPanel = panel_Preview.CreateGraphics();
            //gPanel.Clear(panel_Preview.BackColor);
            float scale = 1.0f;
            //if (panel_Preview.Height < 300) scale = 0.5f;
            #region BackgroundImage
            Logger.WriteLine("BackgroundImage");
            Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3 Pro":
                    bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                    break;
                case "GTS 3":
                case "GTS 4":
                    bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                    break;
                case "GTR 4":
                    bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                    break;
                case "Amazfit Band 7":
                    bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                    break;
                case "GTS 4 mini":
                    bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                    break;
                case "Falcon":
                case "GTR mini":
                    bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                    break;
            }
            Graphics gPanel = Graphics.FromImage(bitmap);
            #endregion

            Logger.WriteLine("Preview_screen");
            bool showEeditMode = false;
            int edit_mode = 0;
            if (uCtrl_EditableBackground_Opt.Visible && uCtrl_EditableBackground_Opt.checkBox_edit_mode.Checked) { 
                showEeditMode = true;
                edit_mode = 1;
            }
            if (uCtrl_EditableElements_Opt.Visible && uCtrl_EditableElements_Opt.checkBox_edit_mode.Checked)
            {
                showEeditMode = true;
                edit_mode = 2;
            }
            if (uCtrl_EditableTimePointer_Opt.Visible && uCtrl_EditableTimePointer_Opt.checkBox_edit_mode.Checked)
            {
                showEeditMode = true;
                edit_mode = 3;
            }
            int link = radioButton_ScreenNormal.Checked ? 0 : 1;
            Preview_screen(gPanel, scale, checkBox_crop.Checked, checkBox_WebW.Checked, checkBox_WebB.Checked,
                checkBox_border.Checked, checkBox_Show_Shortcuts.Checked, checkBox_Shortcuts_Area.Checked,
                checkBox_Shortcuts_Border.Checked, checkBox_Shortcuts_Image.Checked, true, checkBox_CircleScaleImage.Checked,
                checkBox_center_marker.Checked, checkBox_WidgetsArea.Checked, link, false, -1, showEeditMode, edit_mode);
            pictureBox_Preview.BackgroundImage = bitmap;
            gPanel.Dispose();

            if ((formPreview != null) && (formPreview.Visible))
            {
                formPreview.pictureBox_Preview.BackgroundImage = bitmap;
            }
            Logger.WriteLine("* PreviewImage (end)");
        }

        private void pictureBox_Preview_Click(object sender, EventArgs e)
        {
            bool showEeditMode = false;
            int edit_mode = 0;
            if ((formPreview == null) || (!formPreview.Visible))
            {
                formPreview = new Form_Preview(currentDPI);
                formPreview.Show(this);

                switch (ProgramSettings.Scale)
                {
                    case 0.5f:
                        formPreview.radioButton_small.Checked = true;
                        break;
                    case 1.5f:
                        formPreview.radioButton_large.Checked = true;
                        break;
                    case 2.0f:
                        formPreview.radioButton_xlarge.Checked = true;
                        break;
                    case 2.5f:
                        formPreview.radioButton_xxlarge.Checked = true;
                        break;
                    default:
                        formPreview.radioButton_normal.Checked = true;
                        break;

                }

                formPreview.pictureBox_Preview.Resize += (object senderResize, EventArgs eResize) =>
                {
                    if (Form_Preview.Watch_Model != comboBox_watch_model.Text)
                    {
                        if (comboBox_watch_model.SelectedIndex == -1) Form_Preview.Watch_Model = "GTR 3";
                        else Form_Preview.Watch_Model = comboBox_watch_model.Text;
                    }
                    float scalePreviewResize = 1.0f;
                    if (formPreview.radioButton_small.Checked) scalePreviewResize = 0.5f;
                    if (formPreview.radioButton_large.Checked) scalePreviewResize = 1.5f;
                    if (formPreview.radioButton_xlarge.Checked) scalePreviewResize = 2.0f;
                    if (formPreview.radioButton_xxlarge.Checked) scalePreviewResize = 2.5f;

                    ProgramSettings.Scale = scalePreviewResize;
                    string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
                    {
                        //DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);

                    #region BackgroundImage 
                    Bitmap bitmapPreviewResize = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                    switch (ProgramSettings.Watch_Model)
                    {
                        case "GTR 3 Pro":
                            bitmapPreviewResize = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                            break;
                        case "GTS 3":
                        case "GTS 4":
                            bitmapPreviewResize = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                            break;
                        case "GTR 4":
                            bitmapPreviewResize = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                            break;
                        case "Amazfit Band 7":
                            bitmapPreviewResize = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                            break;
                        case "GTS 4 mini":
                            bitmapPreviewResize = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                            break;
                        case "Falcon":
                        case "GTR mini":
                            bitmapPreviewResize = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                            break;
                    }
                    Graphics gPanelPreviewResize = Graphics.FromImage(bitmapPreviewResize);
                    #endregion

                    int link_aod = radioButton_ScreenNormal.Checked ? 0 : 1;
                    showEeditMode = false;
                    edit_mode = 0;
                    if (uCtrl_EditableBackground_Opt.Visible && uCtrl_EditableBackground_Opt.checkBox_edit_mode.Checked)
                    {
                        showEeditMode = true;
                        edit_mode = 1;
                    }
                    if (uCtrl_EditableElements_Opt.Visible && uCtrl_EditableElements_Opt.checkBox_edit_mode.Checked)
                    {
                        showEeditMode = true;
                        edit_mode = 2;
                    }
                    if (uCtrl_EditableTimePointer_Opt.Visible && uCtrl_EditableTimePointer_Opt.checkBox_edit_mode.Checked)
                    {
                        showEeditMode = true;
                        edit_mode = 3;
                    }
                    Preview_screen(gPanelPreviewResize, 1, checkBox_crop.Checked,
                        checkBox_WebW.Checked, checkBox_WebB.Checked, checkBox_border.Checked,
                        checkBox_Show_Shortcuts.Checked, checkBox_Shortcuts_Area.Checked, checkBox_Shortcuts_Border.Checked,
                        checkBox_Shortcuts_Image.Checked, true,checkBox_CircleScaleImage.Checked, 
                        checkBox_center_marker.Checked, checkBox_WidgetsArea.Checked, link_aod, false, -1, showEeditMode, edit_mode);
                    formPreview.pictureBox_Preview.BackgroundImage = bitmapPreviewResize;
                    gPanelPreviewResize.Dispose();
                };

                formPreview.FormClosing += (object senderClosing, FormClosingEventArgs eClosing) =>
                {
                    button_PreviewBig.Enabled = true;
                };

                formPreview.KeyDown += (object senderKeyDown, KeyEventArgs eKeyDown) =>
                {
                    this.Form1_KeyDown(senderKeyDown, eKeyDown);
                };

                formPreview.pictureBox_Preview.MouseDoubleClick += (object senderDoubleClick, MouseEventArgs eDoubleClick) =>
                {
                    uCtrl_AmPm_Opt.SetMouseСoordinates(MouseClickСoordinates.X, MouseClickСoordinates.Y);

                };
            }

            if (Form_Preview.Watch_Model != comboBox_watch_model.Text)
            {
                if (comboBox_watch_model.SelectedIndex == -1) Form_Preview.Watch_Model = "GTR 3";
                else Form_Preview.Watch_Model = comboBox_watch_model.Text;
            }
            formPreview.radioButton_CheckedChanged(sender, e);
            float scale = 1.0f;

            #region BackgroundImage 
            Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3 Pro":
                    bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                    break;
                case "GTS 3":
                case "GTS 4":
                    bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                    break;
                case "GTR 4":
                    bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                    break;
                case "Amazfit Band 7":
                    bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                    break;
                case "GTS 4 mini":
                    bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                    break;
                case "Falcon":
                case "GTR mini":
                    bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                    break;
            }
            Graphics gPanel = Graphics.FromImage(bitmap);
            #endregion

            int link = radioButton_ScreenNormal.Checked ? 0 : 1;
            showEeditMode = false;
            edit_mode = 0;
            if (uCtrl_EditableBackground_Opt.Visible && uCtrl_EditableBackground_Opt.checkBox_edit_mode.Checked)
            {
                showEeditMode = true;
                edit_mode = 1;
            }
            if (uCtrl_EditableElements_Opt.Visible && uCtrl_EditableElements_Opt.checkBox_edit_mode.Checked)
            {
                showEeditMode = true;
                edit_mode = 2;
            }
            if (uCtrl_EditableTimePointer_Opt.Visible && uCtrl_EditableTimePointer_Opt.checkBox_edit_mode.Checked)
            {
                showEeditMode = true;
                edit_mode = 3;
            }
            Preview_screen(gPanel, scale, checkBox_crop.Checked, checkBox_WebW.Checked, checkBox_WebB.Checked,
                checkBox_border.Checked, checkBox_Show_Shortcuts.Checked, checkBox_Shortcuts_Area.Checked,
                checkBox_Shortcuts_Border.Checked, checkBox_Shortcuts_Image.Checked, true, checkBox_CircleScaleImage.Checked,
                checkBox_center_marker.Checked, checkBox_WidgetsArea.Checked, link, false, -1, showEeditMode, edit_mode);
            formPreview.pictureBox_Preview.BackgroundImage = bitmap;
            gPanel.Dispose();

            button_PreviewBig.Enabled = false;
        }

        private void button_OpenDir_Click(object sender, EventArgs e)
        {
            if (FullFileDir != null)
            {
                Process.Start(new ProcessStartInfo(FullFileDir));
                //Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + FullFileDir));
            }
        }

        private void button_SaveJson_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = FullFileDir;
            saveFileDialog.FileName = FileName;
            if (FileName == null || FileName.Length == 0)
            {
                if (FullFileDir != null && FullFileDir.Length > 3)
                {
                    saveFileDialog.FileName = Path.GetFileName(FullFileDir);
                }
            }
            saveFileDialog.Filter = Properties.FormStrings.FilterJson;

            //openFileDialog.Filter = "Json files (*.json) | *.json";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = Properties.FormStrings.Dialog_Title_Dial_Settings;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullfilename = saveFileDialog.FileName;
                save_JSON_File(fullfilename);

                FileName = Path.GetFileName(fullfilename);
                FullFileDir = Path.GetDirectoryName(fullfilename);
                JSON_Modified = false;
                FormText();

                if (Watch_Face != null && Watch_Face.WatchFace_Info != null && Watch_Face.WatchFace_Info.Preview != null)
                {
                    button_RefreshPreview.Visible = true;
                    button_CreatePreview.Visible = false;
                }
                else
                {
                    button_RefreshPreview.Visible = false;
                    if (FileName != null && FullFileDir != null)
                    {
                        button_CreatePreview.Visible = true;
                    }
                    else
                    {
                        button_CreatePreview.Visible = false;
                    }
                }

                //if (checkBox_JsonWarnings.Checked) jsonWarnings(fullfilename);
            }
        }

        private void comboBox_AddTime_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox_AddTime.SelectedIndex == 0)
            {
                AddAnalogTime();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddTime.SelectedIndex == 1)
            {
                AddDigitalTime();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddTime.SelectedIndex == 2)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    AddEditableTimePointer();
                    ShowElemetsWatchFace();
                    JSON_Modified = true;
                    FormText();

                    //panel_WatchfaceElements.AutoScrollPosition = new Point(
                    //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    //    panel_WatchfaceElements.VerticalScroll.Maximum); 
                }
                else MessageBox.Show(Properties.FormStrings.Message_EditablePointerAOD_Text, Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (comboBox_AddTime.SelectedIndex == 3)
            {
                AddAnalogTimePro();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum); 
            }
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddTime.Items.Insert(0, Properties.FormStrings.Elemet_Time);
            comboBox_AddTime.SelectedIndex = 0;
            PreviewView = true;
        }

        private void comboBox_AddDate_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox_AddDate.SelectedIndex == 0)
            {
                AddDateDay();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddDate.SelectedIndex == 1)
            {
                AddDateMonth();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddDate.SelectedIndex == 2)
            {
                AddDateYear();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddDate.SelectedIndex == 3)
            {
                AddDateWeek();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddDate.Items.Insert(0, Properties.FormStrings.Elemet_Date);
            comboBox_AddDate.SelectedIndex = 0;
            PreviewView = true;
        }

        private void comboBox_AddActivity_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox_AddActivity.SelectedIndex == 0)
            {
                AddSteps();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddActivity.SelectedIndex == 1)
            {
                AddCalories();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddActivity.SelectedIndex == 2)
            {
                AddHeart();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddActivity.SelectedIndex == 3)
            {
                AddPAI();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddActivity.SelectedIndex == 4)
            {
                AddDistance();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddActivity.SelectedIndex == 5)
            {
                AddStand();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            /*if (comboBox_AddActivity.SelectedIndex == 6)
            {
                AddActivity();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }*/
            if (comboBox_AddActivity.SelectedIndex == 6)
            {
                AddSpO2();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddActivity.SelectedIndex == 7)
            {
                AddFatBurning();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddActivity.SelectedIndex == 8)
            {
                AddStress();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }

            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddActivity.Items.Insert(0, Properties.FormStrings.Elemet_Activity);
            comboBox_AddActivity.SelectedIndex = 0;
            PreviewView = true;
        }

        private void comboBox_AddAir_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox_AddAir.SelectedIndex == 0)
            {
                AddWeather();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddAir.SelectedIndex == 1)
            {
                AddUVIndex();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddAir.SelectedIndex == 2)
            {
                AddHumidity();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddAir.SelectedIndex == 3)
            {
                AddSunrise();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddAir.SelectedIndex == 4)
            {
                AddWind();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddAir.SelectedIndex == 5)
            {
                AddAltimeter();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddAir.SelectedIndex == 6)
            {
                AddMoon();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }

            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddAir.Items.Insert(0, Properties.FormStrings.Elemet_Air);
            comboBox_AddAir.SelectedIndex = 0;
            PreviewView = true;
        }

        private void comboBox_AddSystem_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox_AddSystem.SelectedIndex == 0)
            {
                AddBattery();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 1)
            {
                AddStatuses();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 2)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    AddShortcuts();
                    ShowElemetsWatchFace();
                    JSON_Modified = true;
                    FormText(); 
                }
                else MessageBox.Show(Properties.FormStrings.Message_ShortcutsAOD_Text, Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 3)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    AddAnimation();
                    ShowElemetsWatchFace();
                    JSON_Modified = true;
                    FormText();
                }
                else MessageBox.Show(Properties.FormStrings.Message_AnimationAOD_Text, Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 4)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    AddEditableElements();
                    ShowElemetsWatchFace();
                    JSON_Modified = true;
                    FormText();
                }
                else MessageBox.Show(Properties.FormStrings.Message_EditableElementsAOD_Text, Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 5)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    AddDisconnectAlert();
                    ShowElemetsWatchFace();
                    JSON_Modified = true;
                    FormText();
                }
                else MessageBox.Show(Properties.FormStrings.Message_ElementAOD_Text, Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 6)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    AddRepeatingAlert();
                    ShowElemetsWatchFace();
                    JSON_Modified = true;
                    FormText();
                }
                else MessageBox.Show(Properties.FormStrings.Message_ElementAOD_Text, Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 7)
            {
                AddImage();
                ShowElemetsWatchFace();
                JSON_Modified = true;
                FormText();

                panel_WatchfaceElements.AutoScrollPosition = new Point(
                    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                    panel_WatchfaceElements.VerticalScroll.Maximum);
            }
            if (comboBox_AddSystem.SelectedIndex == 8)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    AddTopImage();
                    ShowElemetsWatchFace();
                    JSON_Modified = true;
                    FormText();
                }
                else MessageBox.Show(Properties.FormStrings.Message_ElementAOD_Text, Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //panel_WatchfaceElements.AutoScrollPosition = new Point(
                //    Math.Abs(panel_WatchfaceElements.AutoScrollPosition.X),
                //    panel_WatchfaceElements.VerticalScroll.Maximum);
            }

            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddSystem.Items.Insert(0, Properties.FormStrings.Elemet_System);
            comboBox_AddSystem.SelectedIndex = 0;
            PreviewView = true;
        }

        private DialogResult SaveRequest()
        {
            // сохранение если файл не сохранен
            if (JSON_Modified)
            {
                if (FileName != null)
                {
                    DialogResult dr = MessageBox.Show(Properties.FormStrings.Message_Save_JSON_Modified_Text1 +
                        Path.GetFileNameWithoutExtension(FileName) + Properties.FormStrings.Message_Save_JSON_Modified_Text2,
                        Properties.FormStrings.Message_Save_JSON_Modified_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        string fullfilename = Path.Combine(FullFileDir, FileName);
                        save_JSON_File(fullfilename);
                        JSON_Modified = false;
                        FormText();
                        //if (checkBox_JsonWarnings.Checked) jsonWarnings(fullfilename);
                        return dr;
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        return dr;
                    }
                }
            }
            return DialogResult.Ignore;
        }

        /// <summary>Добавляем фон в циферблат</summary>
        private void AddBackground()
        {
            if (!PreviewView) return;
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if(Watch_Face.ScreenNormal.Background != null) return;
                Watch_Face.ScreenNormal.Background = new Background();
                Watch_Face.ScreenNormal.Background.BackgroundColor = new hmUI_widget_FILL_RECT();
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                        break;
                    case "GTR 3 Pro":
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 480;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 480;
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                        break;
                    case "GTR 4":
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 466;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 466;
                        break;
                    case "Amazfit Band 7":
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 368;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 194;
                        break;
                    case "GTS 4 mini":
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 384;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 336;
                        break;
                    case "Falcon":
                    case "GTR mini":
                        Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.h = 416;
                        Watch_Face.ScreenNormal.Background.BackgroundColor.w = 416;
                        break;
                }
                Watch_Face.ScreenNormal.Background.visible = true;
                JSON_Modified = true;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Background != null) return;
                Watch_Face.ScreenAOD.Background = new Background();
                Watch_Face.ScreenAOD.Background.BackgroundColor = new hmUI_widget_FILL_RECT();
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.h = 454;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.w = 454;
                        break;
                    case "GTR 3 Pro":
                        Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.h = 480;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.w = 480;
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.h = 450;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.w = 390;
                        break;
                    case "GTR 4":
                        Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.h = 466;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.w = 466;
                        break;
                    case "Amazfit Band 7":
                        Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.h = 368;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.w = 194;
                        break;
                    case "GTS 4 mini":
                        Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.h = 384;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.w = 336;
                        break;
                    case "Falcon":
                    case "GTR mini":
                        Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                        Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.h = 416;
                        Watch_Face.ScreenAOD.Background.BackgroundColor.w = 416;
                        break;
                }
                Watch_Face.ScreenAOD.Background.visible = true;
                JSON_Modified = true;
            }
        }

        /// <summary>Добавляем цифровое время в циферблат</summary>
        private void AddDigitalTime()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementDigitalTime digitalTime = new ElementDigitalTime();
            digitalTime.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime"); // проверяем что такого элемента нет
            bool existsShortcuts = Elements.Exists(e => e.GetType().Name == "ElementShortcuts"); // проверяем что нет ярлыков
            if (!exists) {
                if (!existsShortcuts) Elements.Add(digitalTime);
                else Elements.Insert(Elements.Count - 1, digitalTime);
            }
            //if (!exists) Elements.Insert(0, digitalTime);
            uCtrl_DigitalTime_Elm.SettingsClear();
        }

        /// <summary>Добавляем аналогового время в циферблат</summary>
        private void AddAnalogTime()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementAnalogTime analogTime = new ElementAnalogTime();
            analogTime.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime"); // проверяем что такого элемента нет
            bool existsShortcuts = Elements.Exists(e => e.GetType().Name == "ElementShortcuts"); // проверяем что нет ярлыков
            if (!exists) 
            { 
                if(!existsShortcuts) Elements.Add(analogTime);
                else Elements.Insert(Elements.Count-1, analogTime);
            }
            //if (!exists) Elements.Insert(0, analogTime);
            uCtrl_AnalogTime_Elm.SettingsClear();
        }

        /// <summary>Добавляем аналогового время в циферблат (Pro)</summary>
        private void AddAnalogTimePro()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            ElementAnalogTimePro analogTime = new ElementAnalogTimePro();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;

                if (analogTime.SmoothSecond != null) analogTime.SmoothSecond.type = 2;
            }

            //ElementAnalogTimePro analogTime = new ElementAnalogTimePro();
            analogTime.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTimePro"); // проверяем что такого элемента нет
            bool existsShortcuts = Elements.Exists(e => e.GetType().Name == "ElementShortcuts"); // проверяем что нет ярлыков
            if (!exists)
            {
                if (!existsShortcuts) Elements.Add(analogTime);
                else Elements.Insert(Elements.Count - 1, analogTime);
            }
            //if (!exists) Elements.Insert(0, analogTime);
            uCtrl_AnalogTimePro_Elm.SettingsClear();
        }

        /// <summary>Добавляем редактируемые стрелки</summary>
        private void AddEditableTimePointer()
        {
            if (!PreviewView) return;
            //List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            /*if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                return;
                //if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                //if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                //Elements = Watch_Face.ScreenAOD.Elements;

                //if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                //    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementEditablePointers editablePointers = new ElementEditablePointers();
            editablePointers.enable = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementEditablePointers"); // проверяем что такого элемента нет
            bool existsShortcuts = Elements.Exists(e => e.GetType().Name == "ElementShortcuts"); // проверяем что нет ярлыков
            if (!exists)
            {
                if (!existsShortcuts) Elements.Add(editablePointers);
                else Elements.Insert(Elements.Count - 1, editablePointers);
            }
            //if (!exists) Elements.Insert(0, analogTime);*/
            if (Watch_Face.ElementEditablePointers == null) Watch_Face.ElementEditablePointers = new ElementEditablePointers();
            Watch_Face.ElementEditablePointers.visible = true;
            uCtrl_EditableTimePointer_Opt.SettingsClear();
        }

        /// <summary>Добавляем редактируемые стрелки</summary>
        private void AddEditableElements()
        {
            if (!PreviewView) return;
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();

            if (Watch_Face.Editable_Elements == null) Watch_Face.Editable_Elements = new EditableElements();
            Watch_Face.Editable_Elements.visible = true;
        }

        /// <summary>Добавляем оповещение об обрыве связи</summary>
        private void AddDisconnectAlert()
        {
            if (!PreviewView) return;
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();

            if (Watch_Face.DisconnectAlert == null) Watch_Face.DisconnectAlert = new DisconnectAlert();
            Watch_Face.DisconnectAlert.enable = true;
        }

        /// <summary>Добавляем повторяющиеся оповещение</summary>
        private void AddRepeatingAlert()
        {
            if (!PreviewView) return;
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();

            if (Watch_Face.RepeatAlert == null) Watch_Face.RepeatAlert = new RepeatAlert();
            Watch_Face.RepeatAlert.enable = true;
        }

        /// <summary>Добавляем верхнее изображение оповещение</summary>
        private void AddTopImage()
        {
            if (!PreviewView) return;
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();

            if (Watch_Face.TopImage == null)
            {
                Watch_Face.TopImage = new TopImage();
                Watch_Face.TopImage.Icon = new hmUI_widget_IMG();
            }
            Watch_Face.TopImage.visible = true;
        }

        /// <summary>Добавляем дату в циферблат</summary>
        private void AddDateDay()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementDateDay dateDay = new ElementDateDay();
            dateDay.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementDateDay"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, dateDay);
            uCtrl_DigitalTime_Elm.SettingsClear();
        }

        /// <summary>Добавляем месяц в циферблат</summary>
        private void AddDateMonth()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementDateMonth dateMonth = new ElementDateMonth();
            dateMonth.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementDateMonth"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, dateMonth);
            uCtrl_DigitalTime_Elm.SettingsClear();
        }

        /// <summary>Добавляем год в циферблат</summary>
        private void AddDateYear()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementDateYear dateYear = new ElementDateYear();
            dateYear.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementDateYear"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, dateYear);
            uCtrl_DigitalTime_Elm.SettingsClear();
        }

        /// <summary>Добавляем день недели в циферблат</summary>
        private void AddDateWeek()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementDateWeek dateWeek = new ElementDateWeek();
            dateWeek.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementDateWeek"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, dateWeek);
            uCtrl_DigitalTime_Elm.SettingsClear();
        }

        /// <summary>Добавляем статусы в циферблат</summary>
        private void AddStatuses()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementStatuses statuses = new ElementStatuses();
            statuses.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementStatuses"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, statuses);
            uCtrl_Statuses_Elm.SettingsClear();
        }

        /// <summary>Добавляем ярлыки в циферблат</summary>
        private void AddShortcuts()
        {
            if (!PreviewView) return;
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (Watch_Face.Shortcuts == null) Watch_Face.Shortcuts = new ElementShortcuts();
            Watch_Face.Shortcuts.visible = true;
            uCtrl_Shortcuts_Elm.SettingsClear();
        }

        /// <summary>Добавляем ярлыки в циферблат</summary>
        private void AddAnimation()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementAnimation animation = new ElementAnimation();
            animation.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnimation"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, animation);
            //if (!exists) Elements.Add(animation);
            uCtrl_Animation_Elm.SettingsClear();
        }

        /// <summary>Добавляем шаги в циферблат</summary>
        private void AddSteps()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementSteps steps = new ElementSteps();
            steps.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementSteps"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, steps);
            uCtrl_Steps_Elm.SettingsClear();
        }

        /// <summary>Добавляем заряд в циферблат</summary>
        private void AddBattery()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementBattery battery = new ElementBattery();
            battery.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementBattery"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, battery);
            uCtrl_Battery_Elm.SettingsClear();
        }

        /// <summary>Добавляем калории в циферблат</summary>
        private void AddCalories()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementCalories сalories = new ElementCalories();
            сalories.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementCalories"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, сalories);
            uCtrl_Calories_Elm.SettingsClear();
        }

        /// <summary>Добавляем пульс в циферблат</summary>
        private void AddHeart()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementHeart heart = new ElementHeart();
            heart.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementHeart"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, heart);
            uCtrl_Heart_Elm.SettingsClear();
        }

        /// <summary>Добавляем PAI в циферблат</summary>
        private void AddPAI()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementPAI pai = new ElementPAI();
            pai.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementPAI"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, pai);
            uCtrl_PAI_Elm.SettingsClear();
        }

        /// <summary>Добавляем путь в циферблат</summary>
        private void AddDistance()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementDistance distance = new ElementDistance();
            distance.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementDistance"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, distance);
            uCtrl_Distance_Elm.SettingsClear();
        }

        /// <summary>Добавляем разминку в циферблат</summary>
        private void AddStand()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementStand stand = new ElementStand();
            stand.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementStand"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, stand);
            uCtrl_Stand_Elm.SettingsClear();
        }

        /// <summary>Добавляем активность в циферблат</summary>
        private void AddActivity()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementActivity activity = new ElementActivity();
            activity.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementActivity"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, activity);
            uCtrl_Activity_Elm.SettingsClear();
        }

        /// <summary>Добавляем SpO2 в циферблат</summary>
        private void AddSpO2()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementSpO2 spo2 = new ElementSpO2();
            spo2.visible = true;
            //spo2.Number = new hmUI_widget_IMG_NUMBER();
            //spo2.Number.position = 1;
            //spo2.Number.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementSpO2"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, spo2);
            uCtrl_SpO2_Elm.SettingsClear();
        }

        /// <summary>Добавляем стресс в циферблат</summary>
        private void AddStress()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementStress steps = new ElementStress();
            steps.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementStress"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, steps);
            uCtrl_Stress_Elm.SettingsClear();
        }

        /// <summary>Добавляем жиросжигание в циферблат</summary>
        private void AddFatBurning()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementFatBurning steps = new ElementFatBurning();
            steps.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementFatBurning"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, steps);
            uCtrl_FatBurning_Elm.SettingsClear();
        }



        /// <summary>Добавляем погоду в циферблат</summary>
        private void AddWeather()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementWeather weather = new ElementWeather();
            weather.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementWeather"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, weather);
            uCtrl_Weather_Elm.SettingsClear();
        }

        /// <summary>Добавляем УФ индекс в циферблат</summary>
        private void AddUVIndex()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementUVIndex uv_index = new ElementUVIndex();
            uv_index.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementUVIndex"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, uv_index);
            uCtrl_UVIndex_Elm.SettingsClear();
        }

        /// <summary>Добавляем шаги в циферблат</summary>
        private void AddHumidity()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementHumidity humidity = new ElementHumidity();
            humidity.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementHumidity"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, humidity);
            uCtrl_Humidity_Elm.SettingsClear();
        }

        /// <summary>Добавляем барометр в циферблат</summary>
        private void AddAltimeter()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementAltimeter altimete = new ElementAltimeter();
            altimete.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementAltimeter"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, altimete);
            uCtrl_Altimeter_Elm.SettingsClear();
        }

        /// <summary>Добавляем восход в циферблат</summary>
        private void AddSunrise()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementSunrise sunrise = new ElementSunrise();
            sunrise.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementSunrise"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, sunrise);
            uCtrl_Sunrise_Elm.SettingsClear();
        }

        /// <summary>Добавляем ветер в циферблат</summary>
        private void AddWind()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementWind wind = new ElementWind();
            wind.visible = true;
            //digitalTime.position = Elements.Count;
            bool exists = Elements.Exists(e => e.GetType().Name == "ElementWind"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, wind);
            uCtrl_Wind_Elm.SettingsClear();
        }

        /// <summary>Добавляем луна в циферблат</summary>
        private void AddMoon()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementMoon moon = new ElementMoon();
            moon.visible = true;
            moon.Images = new hmUI_widget_IMG_LEVEL();
            moon.Images.position = 1;
            moon.Images.visible = true;

            bool exists = Elements.Exists(e => e.GetType().Name == "ElementMoon"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, moon);
            uCtrl_Moon_Elm.SettingsClear();
        }

        /// <summary>Добавляем изображение в циферблат</summary>
        private void AddImage()
        {
            if (!PreviewView) return;
            List<object> Elements = new List<object>();
            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                if (Watch_Face.ScreenNormal.Elements == null) Watch_Face.ScreenNormal.Elements = new List<object>();
                Elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                if (Watch_Face.ScreenAOD.Elements == null) Watch_Face.ScreenAOD.Elements = new List<object>();
                Elements = Watch_Face.ScreenAOD.Elements;

                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null) Elements = Watch_Face.ScreenAOD.Elements;
            }

            ElementImage image = new ElementImage();
            image.visible = true;
            image.Icon = new hmUI_widget_IMG();
            image.Icon.position = 1;
            image.Icon.visible = true;

            bool exists = Elements.Exists(e => e.GetType().Name == "ElementImage"); // проверяем что такого элемента нет
            if (!exists) Elements.Insert(0, image);
            uCtrl_Icon_Opt.SettingsClear();
        }



        /// <summary>Отображаем элемынты в соответствии с json файлом</summary>
        private void ShowElemetsWatchFace()
        {
            PreviewView = false;
            HideAllElemenrOptions();
            ResetHighlightState("");
            ClearAllElemenrOptions();

            uCtrl_Background_Elm.Visible = false;
            uCtrl_DigitalTime_Elm.Visible = false;
            uCtrl_AnalogTime_Elm.Visible = false;
            uCtrl_AnalogTimePro_Elm.Visible = false;
            uCtrl_EditableTimePointer_Elm.Visible = false;
            uCtrl_EditableElements_Elm.Visible = false;

            uCtrl_DateDay_Elm.Visible = false;
            uCtrl_DateMonth_Elm.Visible = false;
            uCtrl_DateYear_Elm.Visible = false;
            uCtrl_DateWeek_Elm.Visible = false;

            uCtrl_Shortcuts_Elm.Visible = false;
            uCtrl_Statuses_Elm.Visible = false;
            uCtrl_Animation_Elm.Visible = false;

            uCtrl_Steps_Elm.Visible = false;
            uCtrl_Battery_Elm.Visible = false;
            uCtrl_Calories_Elm.Visible = false;
            uCtrl_Heart_Elm.Visible = false;
            uCtrl_PAI_Elm.Visible = false;
            uCtrl_Distance_Elm.Visible = false;
            uCtrl_Stand_Elm.Visible = false;
            uCtrl_Activity_Elm.Visible = false;
            uCtrl_SpO2_Elm.Visible = false;
            uCtrl_Stress_Elm.Visible = false;
            uCtrl_FatBurning_Elm.Visible = false;

            uCtrl_Weather_Elm.Visible = false;
            uCtrl_UVIndex_Elm.Visible = false;
            uCtrl_Humidity_Elm.Visible = false;
            uCtrl_Altimeter_Elm.Visible = false;
            uCtrl_Sunrise_Elm.Visible = false;
            uCtrl_Wind_Elm.Visible = false;
            uCtrl_Moon_Elm.Visible = false;
            uCtrl_Image_Elm.Visible = false;

            uCtrl_DisconnectAlert_Elm.Visible = false;
            uCtrl_RepeatingAlert_Elm.Visible = false;
            uCtrl_TopImage_Elm.Visible = false;


            int count = tableLayoutPanel_ElemetsWatchFace.RowCount;

            if (Watch_Face == null)
            {
                PreviewView = true;
                return;
            }
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) return;
                if (Watch_Face.ScreenNormal.Background != null) 
                {
                    uCtrl_Background_Elm.Visible_ShowDel(false);
                    uCtrl_Background_Elm.SetVisibilityElementStatus(Watch_Face.ScreenNormal.Background.visible);
                    uCtrl_Background_Elm.Visible = true;
                }
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) return;
                if (Watch_Face.ScreenAOD.Background != null)
                {
                    uCtrl_Background_Elm.Visible_ShowDel(true);
                    uCtrl_Background_Elm.SetVisibilityElementStatus(Watch_Face.ScreenAOD.Background.visible);
                    uCtrl_Background_Elm.Visible = true;
                }

            }

            List<object> elements = new List<object>();
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal.Elements != null && Watch_Face.ScreenNormal.Elements.Count > 0)
                    elements = Watch_Face.ScreenNormal.Elements;
            }
            else
            {
                if (Watch_Face.ScreenAOD.Elements != null && Watch_Face.ScreenAOD.Elements.Count > 0)
                    elements = Watch_Face.ScreenAOD.Elements;
            }

            Dictionary<int, string> elementOptions;
            if (elements.Count > 0)
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    Object element = elements[i];
                    //string elementStr = element.ToString();
                    //string type = GetTypeFromSring(elementStr);
                    string type = element.GetType().Name;
                    switch (type)
                    {
                        #region ElementDigitalTime
                        case "ElementDigitalTime":
                            ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                            uCtrl_DigitalTime_Elm.SetVisibilityElementStatus(DigitalTime.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (DigitalTime.Second != null && !elementOptions.ContainsKey(DigitalTime.Second.position) && 
                                !elementOptions.ContainsValue("Second"))
                            {
                                uCtrl_DigitalTime_Elm.checkBox_Seconds.Checked = DigitalTime.Second.visible;
                                elementOptions.Add(DigitalTime.Second.position, "Second");
                            }
                            if (DigitalTime.Minute != null && !elementOptions.ContainsKey(DigitalTime.Minute.position) &&
                                !elementOptions.ContainsValue("Minute"))
                            {
                                uCtrl_DigitalTime_Elm.checkBox_Minutes.Checked = DigitalTime.Minute.visible;
                                elementOptions.Add(DigitalTime.Minute.position, "Minute");
                            }
                            if (DigitalTime.Hour != null && !elementOptions.ContainsKey(DigitalTime.Hour.position) &&
                                !elementOptions.ContainsValue("Hour"))
                            {
                                uCtrl_DigitalTime_Elm.checkBox_Hours.Checked = DigitalTime.Hour.visible;
                                elementOptions.Add(DigitalTime.Hour.position, "Hour");
                            }
                            if (DigitalTime.AmPm != null && !elementOptions.ContainsKey(DigitalTime.AmPm.position) &&
                                !elementOptions.ContainsValue("AmPm"))
                            {
                                uCtrl_DigitalTime_Elm.checkBox_AmPm.Checked = DigitalTime.AmPm.visible;
                                elementOptions.Add(DigitalTime.AmPm.position, "AmPm");
                            }
                                
                            uCtrl_DigitalTime_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_DigitalTime_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion
                        
                        #region ElementAnalogTime
                        case "ElementAnalogTime":
                            ElementAnalogTime AnalogTime = (ElementAnalogTime)element;
                            uCtrl_AnalogTime_Elm.SetVisibilityElementStatus(AnalogTime.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (AnalogTime.Second != null)
                            {
                                uCtrl_AnalogTime_Elm.checkBox_Seconds.Checked = AnalogTime.Second.visible;
                                elementOptions.Add(AnalogTime.Second.position, "Second");
                            }
                            if (AnalogTime.Minute != null)
                            {
                                uCtrl_AnalogTime_Elm.checkBox_Minutes.Checked = AnalogTime.Minute.visible;
                                elementOptions.Add(AnalogTime.Minute.position, "Minute");
                            }
                            if (AnalogTime.Hour != null)
                            {
                                uCtrl_AnalogTime_Elm.checkBox_Hours.Checked = AnalogTime.Hour.visible;
                                elementOptions.Add(AnalogTime.Hour.position, "Hour");
                            }

                            uCtrl_AnalogTime_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_AnalogTime_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementAnalogTimePro
                        case "ElementAnalogTimePro":
                            ElementAnalogTimePro AnalogTimePro = (ElementAnalogTimePro)element;
                            uCtrl_AnalogTimePro_Elm.SetVisibilityElementStatus(AnalogTimePro.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (AnalogTimePro.Second != null)
                            {
                                uCtrl_AnalogTimePro_Elm.checkBox_Seconds.Checked = AnalogTimePro.Second.visible;
                                elementOptions.Add(AnalogTimePro.Second.position, "Second");
                            }
                            if (AnalogTimePro.Minute != null)
                            {
                                uCtrl_AnalogTimePro_Elm.checkBox_Minutes.Checked = AnalogTimePro.Minute.visible;
                                elementOptions.Add(AnalogTimePro.Minute.position, "Minute");
                            }
                            if (AnalogTimePro.Hour != null)
                            {
                                uCtrl_AnalogTimePro_Elm.checkBox_Hours.Checked = AnalogTimePro.Hour.visible;
                                elementOptions.Add(AnalogTimePro.Hour.position, "Hour");
                            }
                            if (AnalogTimePro.SmoothSecond != null)
                            {
                                uCtrl_AnalogTimePro_Elm.checkBox_SmoothSeconds.Checked = AnalogTimePro.SmoothSecond.enable;
                                elementOptions.Add(AnalogTimePro.SmoothSecond.position, "SmoothSecond");
                            }

                            uCtrl_AnalogTimePro_Elm.checkBox_Format_24hour.Checked = AnalogTimePro.Format_24hour;
                            elementOptions.Add(AnalogTimePro.Format_24hour_position, "Format_24hour");

                            uCtrl_AnalogTimePro_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_AnalogTimePro_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        /*#region ElementEditablePointers
                        case "ElementEditablePointers":
                            ElementEditablePointers EditablePointers = (ElementEditablePointers)element;
                            uCtrl_EditableTimePointer_Elm.SetVisibilityElementStatus(EditablePointers.enable);

                            uCtrl_EditableTimePointer_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion*/

                        #region ElementDateDay
                        case "ElementDateDay":
                            ElementDateDay DateDay = (ElementDateDay)element;
                            uCtrl_DateDay_Elm.SetVisibilityElementStatus(DateDay.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (DateDay.Number != null)
                            {
                                uCtrl_DateDay_Elm.checkBox_Number.Checked = DateDay.Number.visible;
                                elementOptions.Add(DateDay.Number.position, "Number");
                            }
                            if (DateDay.Pointer != null)
                            {
                                uCtrl_DateDay_Elm.checkBox_Pointer.Checked = DateDay.Pointer.visible;
                                elementOptions.Add(DateDay.Pointer.position, "Pointer");
                            }

                            uCtrl_DateDay_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_DateDay_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementDateMonth
                        case "ElementDateMonth":
                            ElementDateMonth DateMonth = (ElementDateMonth)element;
                            uCtrl_DateMonth_Elm.SetVisibilityElementStatus(DateMonth.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (DateMonth.Number != null)
                            {
                                uCtrl_DateMonth_Elm.checkBox_Number.Checked = DateMonth.Number.visible;
                                elementOptions.Add(DateMonth.Number.position, "Number");
                            }
                            if (DateMonth.Pointer != null)
                            {
                                uCtrl_DateMonth_Elm.checkBox_Pointer.Checked = DateMonth.Pointer.visible;
                                elementOptions.Add(DateMonth.Pointer.position, "Pointer");
                            }
                            if (DateMonth.Images != null)
                            {
                                uCtrl_DateMonth_Elm.checkBox_Images.Checked = DateMonth.Images.visible;
                                elementOptions.Add(DateMonth.Images.position, "Images");
                            }

                            //uCtrl_DateMonth_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_DateMonth_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementDateYear
                        case "ElementDateYear":
                            ElementDateYear DateYear = (ElementDateYear)element;
                            uCtrl_DateYear_Elm.SetVisibilityElementStatus(DateYear.visible);

                            uCtrl_DateYear_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementDateWeek
                        case "ElementDateWeek":
                            ElementDateWeek DateWeek = (ElementDateWeek)element;
                            uCtrl_DateWeek_Elm.SetVisibilityElementStatus(DateWeek.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (DateWeek.Pointer != null)
                            {
                                uCtrl_DateWeek_Elm.checkBox_Pointer.Checked = DateWeek.Pointer.visible;
                                elementOptions.Add(DateWeek.Pointer.position, "Pointer");
                            }
                            if (DateWeek.Images != null)
                            {
                                uCtrl_DateWeek_Elm.checkBox_Images.Checked = DateWeek.Images.visible;
                                elementOptions.Add(DateWeek.Images.position, "Images");
                            }

                            uCtrl_DateWeek_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_DateWeek_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion


                        #region ElementStatuses
                        case "ElementStatuses":
                            ElementStatuses Statuses = (ElementStatuses)element;
                            uCtrl_Statuses_Elm.SetVisibilityElementStatus(Statuses.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Statuses.DND != null)
                            {
                                uCtrl_Statuses_Elm.checkBox_DND.Checked = Statuses.DND.visible;
                                elementOptions.Add(Statuses.DND.position, "DND");
                            }
                            if (Statuses.Bluetooth != null)
                            {
                                uCtrl_Statuses_Elm.checkBox_Bluetooth.Checked = Statuses.Bluetooth.visible;
                                elementOptions.Add(Statuses.Bluetooth.position, "Bluetooth");
                            }
                            if (Statuses.Alarm != null)
                            {
                                uCtrl_Statuses_Elm.checkBox_Alarm.Checked = Statuses.Alarm.visible;
                                elementOptions.Add(Statuses.Alarm.position, "Alarm");
                            }
                            if (Statuses.Lock != null)
                            {
                                uCtrl_Statuses_Elm.checkBox_Lock.Checked = Statuses.Lock.visible;
                                elementOptions.Add(Statuses.Lock.position, "Lock");
                            }
                            uCtrl_Statuses_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Statuses_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        /*#region ElementShortcuts
                        case "ElementShortcuts":
                            ElementShortcuts Shortcuts = (ElementShortcuts)element;
                            uCtrl_Shortcuts_Elm.SetVisibilityElementStatus(Shortcuts.enable);
                            elementOptions = new Dictionary<int, string>();
                            if (Shortcuts.Step != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Step.Checked = Shortcuts.Step.enable;
                                elementOptions.Add(Shortcuts.Step.position, "Step");
                            }
                            if (Shortcuts.Heart != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Heart.Checked = Shortcuts.Heart.enable;
                                elementOptions.Add(Shortcuts.Heart.position, "Heart");
                            }
                            if (Shortcuts.SPO2 != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_SPO2.Checked = Shortcuts.SPO2.enable;
                                elementOptions.Add(Shortcuts.SPO2.position, "SPO2");
                            }
                            if (Shortcuts.PAI != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_PAI.Checked = Shortcuts.PAI.enable;
                                elementOptions.Add(Shortcuts.PAI.position, "PAI");
                            }
                            if (Shortcuts.Stress != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Stress.Checked = Shortcuts.Stress.enable;
                                elementOptions.Add(Shortcuts.Stress.position, "Stress");
                            }
                            if (Shortcuts.Weather != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Weather.Checked = Shortcuts.Weather.enable;
                                elementOptions.Add(Shortcuts.Weather.position, "Weather");
                            }
                            if (Shortcuts.Altimeter != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Altimeter.Checked = Shortcuts.Altimeter.enable;
                                elementOptions.Add(Shortcuts.Altimeter.position, "Altimeter");
                            }
                            if (Shortcuts.Sunrise != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Sunrise.Checked = Shortcuts.Sunrise.enable;
                                elementOptions.Add(Shortcuts.Sunrise.position, "Sunrise");
                            }
                            if (Shortcuts.Alarm != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Alarm.Checked = Shortcuts.Alarm.enable;
                                elementOptions.Add(Shortcuts.Alarm.position, "Alarm");
                            }
                            if (Shortcuts.Sleep != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Sleep.Checked = Shortcuts.Sleep.enable;
                                elementOptions.Add(Shortcuts.Sleep.position, "Sleep");
                            }
                            if (Shortcuts.Countdown != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Countdown.Checked = Shortcuts.Countdown.enable;
                                elementOptions.Add(Shortcuts.Countdown.position, "Countdown");
                            }
                            if (Shortcuts.Stopwatch != null)
                            {
                                uCtrl_Shortcuts_Elm.checkBox_Stopwatch.Checked = Shortcuts.Stopwatch.enable;
                                elementOptions.Add(Shortcuts.Stopwatch.position, "Stopwatch");
                            }
                            uCtrl_Shortcuts_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Shortcuts_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion*/

                        #region ElementAnimation
                        case "ElementAnimation":
                            ElementAnimation Animation = (ElementAnimation)element;
                            uCtrl_Animation_Elm.SetVisibilityElementStatus(Animation.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Animation.Frame_Animation_List!= null)
                            {
                                uCtrl_Animation_Elm.checkBox_FrameAnimation.Checked = Animation.Frame_Animation_List.visible;
                                elementOptions.Add(Animation.Frame_Animation_List.position, "FrameAnimation");
                                /*if (ProgramSettings.Watch_Model == "T-Rex 2" *//*ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4"*//*)
                                {
                                    uCtrl_Animation_Elm.MotionAnimation = false;
                                    uCtrl_Animation_Elm.RotateAnimation = false;
                                }
                                else
                                {
                                    uCtrl_Animation_Elm.MotionAnimation = true;
                                    uCtrl_Animation_Elm.RotateAnimation = true;
                                }*/
                            }

                            if (Animation.Motion_Animation_List != null)
                            {
                                uCtrl_Animation_Elm.checkBox_MotionAnimation.Checked = Animation.Motion_Animation_List.visible;
                                elementOptions.Add(Animation.Motion_Animation_List.position, "MotionAnimation");
                            }

                            if (Animation.Rotate_Animation_List != null)
                            {
                                uCtrl_Animation_Elm.checkBox_RotateAnimation.Checked = Animation.Rotate_Animation_List.visible;
                                elementOptions.Add(Animation.Rotate_Animation_List.position, "RotateAnimation");
                            }


                            uCtrl_Animation_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Animation_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion


                        #region ElementSteps
                        case "ElementSteps":
                            ElementSteps Steps = (ElementSteps)element;
                            uCtrl_Steps_Elm.SetVisibilityElementStatus(Steps.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Steps.Images != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Images.Checked = Steps.Images.visible;
                                elementOptions.Add(Steps.Images.position, "Images");
                            }
                            if (Steps.Segments != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Segments.Checked = Steps.Segments.visible;
                                elementOptions.Add(Steps.Segments.position, "Segments");
                            }
                            if (Steps.Number != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Number.Checked = Steps.Number.visible;
                                elementOptions.Add(Steps.Number.position, "Number");
                            }
                            if (Steps.Text_rotation != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Text_rotation.Checked = Steps.Text_rotation.visible;
                                elementOptions.Add(Steps.Text_rotation.position, "Text_rotation");
                            }
                            if (Steps.Text_circle != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Text_circle.Checked = Steps.Text_circle.visible;
                                elementOptions.Add(Steps.Text_circle.position, "Text_circle");
                            }
                            if (Steps.Number_Target != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Number_Target.Checked = Steps.Number_Target.visible;
                                elementOptions.Add(Steps.Number_Target.position, "Number_Target");
                            }
                            if (Steps.Text_rotation_Target != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Text_rotation_Target.Checked = Steps.Text_rotation_Target.visible;
                                elementOptions.Add(Steps.Text_rotation_Target.position, "Text_rotation_Target");
                            }
                            if (Steps.Text_circle_Target != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Text_circle_Target.Checked = Steps.Text_circle_Target.visible;
                                elementOptions.Add(Steps.Text_circle_Target.position, "Text_circle_Target");
                            }
                            if (Steps.Pointer != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Pointer.Checked = Steps.Pointer.visible;
                                elementOptions.Add(Steps.Pointer.position, "Pointer");
                            }
                            if (Steps.Circle_Scale != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Circle_Scale.Checked = Steps.Circle_Scale.visible;
                                elementOptions.Add(Steps.Circle_Scale.position, "Circle_Scale");
                            }
                            if (Steps.Linear_Scale != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Linear_Scale.Checked = Steps.Linear_Scale.visible;
                                elementOptions.Add(Steps.Linear_Scale.position, "Linear_Scale");
                            }
                            if (Steps.Icon != null)
                            {
                                uCtrl_Steps_Elm.checkBox_Icon.Checked = Steps.Icon.visible;
                                elementOptions.Add(Steps.Icon.position, "Icon");
                            }

                            uCtrl_Steps_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Steps_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementBattery
                        case "ElementBattery":
                            ElementBattery Battery = (ElementBattery)element;
                            uCtrl_Battery_Elm.SetVisibilityElementStatus(Battery.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Battery.Images != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Images.Checked = Battery.Images.visible;
                                elementOptions.Add(Battery.Images.position, "Images");
                            }
                            if (Battery.Segments != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Segments.Checked = Battery.Segments.visible;
                                elementOptions.Add(Battery.Segments.position, "Segments");
                            }
                            if (Battery.Number != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Number.Checked = Battery.Number.visible;
                                elementOptions.Add(Battery.Number.position, "Number");
                            }
                            if (Battery.Text_rotation != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Text_rotation.Checked = Battery.Text_rotation.visible;
                                elementOptions.Add(Battery.Text_rotation.position, "Text_rotation");
                            }
                            if (Battery.Text_circle != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Text_circle.Checked = Battery.Text_circle.visible;
                                elementOptions.Add(Battery.Text_circle.position, "Text_circle");
                            }
                            if (Battery.Pointer != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Pointer.Checked = Battery.Pointer.visible;
                                elementOptions.Add(Battery.Pointer.position, "Pointer");
                            }
                            if (Battery.Circle_Scale != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Circle_Scale.Checked = Battery.Circle_Scale.visible;
                                elementOptions.Add(Battery.Circle_Scale.position, "Circle_Scale");
                            }
                            if (Battery.Linear_Scale != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Linear_Scale.Checked = Battery.Linear_Scale.visible;
                                elementOptions.Add(Battery.Linear_Scale.position, "Linear_Scale");
                            }
                            if (Battery.Icon != null)
                            {
                                uCtrl_Battery_Elm.checkBox_Icon.Checked = Battery.Icon.visible;
                                elementOptions.Add(Battery.Icon.position, "Icon");
                            }

                            uCtrl_Battery_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Battery_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementCalories
                        case "ElementCalories":
                            ElementCalories Calories = (ElementCalories)element;
                            uCtrl_Calories_Elm.SetVisibilityElementStatus(Calories.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Calories.Images != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Images.Checked = Calories.Images.visible;
                                elementOptions.Add(Calories.Images.position, "Images");
                            }
                            if (Calories.Segments != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Segments.Checked = Calories.Segments.visible;
                                elementOptions.Add(Calories.Segments.position, "Segments");
                            }
                            if (Calories.Number != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Number.Checked = Calories.Number.visible;
                                elementOptions.Add(Calories.Number.position, "Number");
                            }
                            if (Calories.Text_rotation != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Text_rotation.Checked = Calories.Text_rotation.visible;
                                elementOptions.Add(Calories.Text_rotation.position, "Text_rotation");
                            }
                            if (Calories.Text_circle != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Text_circle.Checked = Calories.Text_circle.visible;
                                elementOptions.Add(Calories.Text_circle.position, "Text_circle");
                            }
                            if (Calories.Number_Target != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Number_Target.Checked = Calories.Number_Target.visible;
                                elementOptions.Add(Calories.Number_Target.position, "Number_Target");
                            }
                            if (Calories.Text_rotation_Target != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Text_rotation_Target.Checked = Calories.Text_rotation_Target.visible;
                                elementOptions.Add(Calories.Text_rotation_Target.position, "Text_rotation_Target");
                            }
                            if (Calories.Text_circle_Target != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Text_circle_Target.Checked = Calories.Text_circle_Target.visible;
                                elementOptions.Add(Calories.Text_circle_Target.position, "Text_circle_Target");
                            }
                            if (Calories.Pointer != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Pointer.Checked = Calories.Pointer.visible;
                                elementOptions.Add(Calories.Pointer.position, "Pointer");
                            }
                            if (Calories.Circle_Scale != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Circle_Scale.Checked = Calories.Circle_Scale.visible;
                                elementOptions.Add(Calories.Circle_Scale.position, "Circle_Scale");
                            }
                            if (Calories.Linear_Scale != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Linear_Scale.Checked = Calories.Linear_Scale.visible;
                                elementOptions.Add(Calories.Linear_Scale.position, "Linear_Scale");
                            }
                            if (Calories.Icon != null)
                            {
                                uCtrl_Calories_Elm.checkBox_Icon.Checked = Calories.Icon.visible;
                                elementOptions.Add(Calories.Icon.position, "Icon");
                            }

                            uCtrl_Calories_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Calories_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementHeart
                        case "ElementHeart":
                            ElementHeart Heart = (ElementHeart)element;
                            uCtrl_Heart_Elm.SetVisibilityElementStatus(Heart.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Heart.Images != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Images.Checked = Heart.Images.visible;
                                elementOptions.Add(Heart.Images.position, "Images");
                            }
                            if (Heart.Segments != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Segments.Checked = Heart.Segments.visible;
                                elementOptions.Add(Heart.Segments.position, "Segments");
                            }
                            if (Heart.Number != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Number.Checked = Heart.Number.visible;
                                elementOptions.Add(Heart.Number.position, "Number");
                            }
                            if (Heart.Text_rotation != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Text_rotation.Checked = Heart.Text_rotation.visible;
                                elementOptions.Add(Heart.Text_rotation.position, "Text_rotation");
                            }
                            if (Heart.Text_circle != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Text_circle.Checked = Heart.Text_circle.visible;
                                elementOptions.Add(Heart.Text_circle.position, "Text_circle");
                            }
                            if (Heart.Pointer != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Pointer.Checked = Heart.Pointer.visible;
                                elementOptions.Add(Heart.Pointer.position, "Pointer");
                            }
                            if (Heart.Circle_Scale != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Circle_Scale.Checked = Heart.Circle_Scale.visible;
                                elementOptions.Add(Heart.Circle_Scale.position, "Circle_Scale");
                            }
                            if (Heart.Linear_Scale != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Linear_Scale.Checked = Heart.Linear_Scale.visible;
                                elementOptions.Add(Heart.Linear_Scale.position, "Linear_Scale");
                            }
                            if (Heart.Icon != null)
                            {
                                uCtrl_Heart_Elm.checkBox_Icon.Checked = Heart.Icon.visible;
                                elementOptions.Add(Heart.Icon.position, "Icon");
                            }

                            uCtrl_Heart_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Heart_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementPAI
                        case "ElementPAI":
                            ElementPAI PAI = (ElementPAI)element;
                            uCtrl_PAI_Elm.SetVisibilityElementStatus(PAI.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (PAI.Images != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Images.Checked = PAI.Images.visible;
                                elementOptions.Add(PAI.Images.position, "Images");
                            }
                            if (PAI.Segments != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Segments.Checked = PAI.Segments.visible;
                                elementOptions.Add(PAI.Segments.position, "Segments");
                            }
                            if (PAI.Number != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Number.Checked = PAI.Number.visible;
                                elementOptions.Add(PAI.Number.position, "Number");
                            }
                            if (PAI.Number_Target != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Number_Target.Checked = PAI.Number_Target.visible;
                                elementOptions.Add(PAI.Number_Target.position, "Number_Target");
                            }
                            if (PAI.Text_rotation_Target != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Text_rotation_Target.Checked = PAI.Text_rotation_Target.visible;
                                elementOptions.Add(PAI.Text_rotation_Target.position, "Text_rotation_Target");
                            }
                            if (PAI.Text_circle_Target != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Text_circle_Target.Checked = PAI.Text_circle_Target.visible;
                                elementOptions.Add(PAI.Text_circle_Target.position, "Text_circle_Target");
                            }
                            if (PAI.Pointer != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Pointer.Checked = PAI.Pointer.visible;
                                elementOptions.Add(PAI.Pointer.position, "Pointer");
                            }
                            if (PAI.Circle_Scale != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Circle_Scale.Checked = PAI.Circle_Scale.visible;
                                elementOptions.Add(PAI.Circle_Scale.position, "Circle_Scale");
                            }
                            if (PAI.Linear_Scale != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Linear_Scale.Checked = PAI.Linear_Scale.visible;
                                elementOptions.Add(PAI.Linear_Scale.position, "Linear_Scale");
                            }
                            if (PAI.Icon != null)
                            {
                                uCtrl_PAI_Elm.checkBox_Icon.Checked = PAI.Icon.visible;
                                elementOptions.Add(PAI.Icon.position, "Icon");
                            }

                            uCtrl_PAI_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_PAI_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementDistance
                        case "ElementDistance":
                            ElementDistance Distance = (ElementDistance)element;
                            uCtrl_Distance_Elm.SetVisibilityElementStatus(Distance.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Distance.Number != null)
                            {
                                uCtrl_Distance_Elm.checkBox_Number.Checked = Distance.Number.visible;
                                elementOptions.Add(Distance.Number.position, "Number");
                            }
                            if (Distance.Text_rotation != null)
                            {
                                uCtrl_Distance_Elm.checkBox_Text_rotation.Checked = Distance.Text_rotation.visible;
                                elementOptions.Add(Distance.Text_rotation.position, "Text_rotation");
                            }
                            if (Distance.Text_circle != null)
                            {
                                uCtrl_Distance_Elm.checkBox_Text_circle.Checked = Distance.Text_circle.visible;
                                elementOptions.Add(Distance.Text_circle.position, "Text_circle");
                            }
                            if (Distance.Icon != null)
                            {
                                uCtrl_Distance_Elm.checkBox_Icon.Checked = Distance.Icon.visible;
                                elementOptions.Add(Distance.Icon.position, "Icon");
                            }

                            uCtrl_Distance_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Distance_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementStand
                        case "ElementStand":
                            ElementStand Stand = (ElementStand)element;
                            uCtrl_Stand_Elm.SetVisibilityElementStatus(Stand.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Stand.Images != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Images.Checked = Stand.Images.visible;
                                elementOptions.Add(Stand.Images.position, "Images");
                            }
                            if (Stand.Segments != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Segments.Checked = Stand.Segments.visible;
                                elementOptions.Add(Stand.Segments.position, "Segments");
                            }
                            if (Stand.Number != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Number.Checked = Stand.Number.visible;
                                elementOptions.Add(Stand.Number.position, "Number");
                            }
                            if (Stand.Text_rotation != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Text_rotation.Checked = Stand.Text_rotation.visible;
                                elementOptions.Add(Stand.Text_rotation.position, "Text_rotation");
                            }
                            if (Stand.Text_circle != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Text_circle.Checked = Stand.Text_circle.visible;
                                elementOptions.Add(Stand.Text_circle.position, "Text_circle");
                            }
                            if (Stand.Number_Target != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Number_Target.Checked = Stand.Number_Target.visible;
                                elementOptions.Add(Stand.Number_Target.position, "Number_Target");
                            }
                            if (Stand.Text_rotation_Target != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Text_rotation_Target.Checked = Stand.Text_rotation_Target.visible;
                                elementOptions.Add(Stand.Text_rotation_Target.position, "Text_rotation_Target");
                            }
                            if (Stand.Text_circle_Target != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Text_circle_Target.Checked = Stand.Text_circle_Target.visible;
                                elementOptions.Add(Stand.Text_circle_Target.position, "Text_circle_Target");
                            }
                            if (Stand.Pointer != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Pointer.Checked = Stand.Pointer.visible;
                                elementOptions.Add(Stand.Pointer.position, "Pointer");
                            }
                            if (Stand.Circle_Scale != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Circle_Scale.Checked = Stand.Circle_Scale.visible;
                                elementOptions.Add(Stand.Circle_Scale.position, "Circle_Scale");
                            }
                            if (Stand.Linear_Scale != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Linear_Scale.Checked = Stand.Linear_Scale.visible;
                                elementOptions.Add(Stand.Linear_Scale.position, "Linear_Scale");
                            }
                            if (Stand.Icon != null)
                            {
                                uCtrl_Stand_Elm.checkBox_Icon.Checked = Stand.Icon.visible;
                                elementOptions.Add(Stand.Icon.position, "Icon");
                            }

                            uCtrl_Stand_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Stand_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementActivity
                        case "ElementActivity":
                            ElementActivity Activity = (ElementActivity)element;
                            uCtrl_Activity_Elm.SetVisibilityElementStatus(Activity.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Activity.Images != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Images.Checked = Activity.Images.visible;
                                elementOptions.Add(Activity.Images.position, "Images");
                            }
                            if (Activity.Segments != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Segments.Checked = Activity.Segments.visible;
                                elementOptions.Add(Activity.Segments.position, "Segments");
                            }
                            if (Activity.Number != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Number.Checked = Activity.Number.visible;
                                elementOptions.Add(Activity.Number.position, "Number");
                            }
                            if (Activity.Number_Target != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Number_Target.Checked = Activity.Number_Target.visible;
                                elementOptions.Add(Activity.Number_Target.position, "Number_Target");
                            }
                            if (Activity.Pointer != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Pointer.Checked = Activity.Pointer.visible;
                                elementOptions.Add(Activity.Pointer.position, "Pointer");
                            }
                            if (Activity.Circle_Scale != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Circle_Scale.Checked = Activity.Circle_Scale.visible;
                                elementOptions.Add(Activity.Circle_Scale.position, "Circle_Scale");
                            }
                            if (Activity.Linear_Scale != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Linear_Scale.Checked = Activity.Linear_Scale.visible;
                                elementOptions.Add(Activity.Linear_Scale.position, "Linear_Scale");
                            }
                            if (Activity.Icon != null)
                            {
                                uCtrl_Activity_Elm.checkBox_Icon.Checked = Activity.Icon.visible;
                                elementOptions.Add(Activity.Icon.position, "Icon");
                            }

                            uCtrl_Activity_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Activity_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementSpO2
                        case "ElementSpO2":
                            ElementSpO2 SpO2 = (ElementSpO2)element;
                            uCtrl_SpO2_Elm.SetVisibilityElementStatus(SpO2.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (SpO2.Number != null)
                            {
                                uCtrl_SpO2_Elm.checkBox_Number.Checked = SpO2.Number.visible;
                                elementOptions.Add(SpO2.Number.position, "Number");
                            }
                            if (SpO2.Text_rotation != null)
                            {
                                uCtrl_SpO2_Elm.checkBox_Text_rotation.Checked = SpO2.Text_rotation.visible;
                                elementOptions.Add(SpO2.Text_rotation.position, "Text_rotation");
                            }
                            if (SpO2.Text_circle != null)
                            {
                                uCtrl_SpO2_Elm.checkBox_Text_circle.Checked = SpO2.Text_circle.visible;
                                elementOptions.Add(SpO2.Text_circle.position, "Text_circle");
                            }
                            if (SpO2.Icon != null)
                            {
                                uCtrl_SpO2_Elm.checkBox_Icon.Checked = SpO2.Icon.visible;
                                elementOptions.Add(SpO2.Icon.position, "Icon");
                            }

                            uCtrl_SpO2_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_SpO2_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);

                            //uCtrl_SpO2_Elm.Visible = true;
                            //SetElementPositionInGUI(type, count - i - 2);
                            break;
                        #endregion

                        #region ElementStress
                        case "ElementStress":
                            ElementStress Stress = (ElementStress)element;
                            uCtrl_Stress_Elm.SetVisibilityElementStatus(Stress.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Stress.Images != null)
                            {
                                uCtrl_Stress_Elm.checkBox_Images.Checked = Stress.Images.visible;
                                elementOptions.Add(Stress.Images.position, "Images");
                            }
                            if (Stress.Segments != null)
                            {
                                uCtrl_Stress_Elm.checkBox_Segments.Checked = Stress.Segments.visible;
                                elementOptions.Add(Stress.Segments.position, "Segments");
                            }
                            if (Stress.Number != null)
                            {
                                uCtrl_Stress_Elm.checkBox_Number.Checked = Stress.Number.visible;
                                elementOptions.Add(Stress.Number.position, "Number");
                            }
                            if (Stress.Pointer != null)
                            {
                                uCtrl_Stress_Elm.checkBox_Pointer.Checked = Stress.Pointer.visible;
                                elementOptions.Add(Stress.Pointer.position, "Pointer");
                            }
                            if (Stress.Icon != null)
                            {
                                uCtrl_Stress_Elm.checkBox_Icon.Checked = Stress.Icon.visible;
                                elementOptions.Add(Stress.Icon.position, "Icon");
                            }

                            uCtrl_Stress_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Stress_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementFatBurning
                        case "ElementFatBurning":
                            ElementFatBurning FatBurning = (ElementFatBurning)element;
                            uCtrl_FatBurning_Elm.SetVisibilityElementStatus(FatBurning.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (FatBurning.Images != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Images.Checked = FatBurning.Images.visible;
                                elementOptions.Add(FatBurning.Images.position, "Images");
                            }
                            if (FatBurning.Segments != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Segments.Checked = FatBurning.Segments.visible;
                                elementOptions.Add(FatBurning.Segments.position, "Segments");
                            }
                            if (FatBurning.Number != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Number.Checked = FatBurning.Number.visible;
                                elementOptions.Add(FatBurning.Number.position, "Number");
                            }
                            if (FatBurning.Text_rotation != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Text_rotation.Checked = FatBurning.Text_rotation.visible;
                                elementOptions.Add(FatBurning.Text_rotation.position, "Text_rotation");
                            }
                            if (FatBurning.Text_circle != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Text_circle.Checked = FatBurning.Text_circle.visible;
                                elementOptions.Add(FatBurning.Text_circle.position, "Text_circle");
                            }
                            if (FatBurning.Number_Target != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Number_Target.Checked = FatBurning.Number_Target.visible;
                                elementOptions.Add(FatBurning.Number_Target.position, "Number_Target");
                            }
                            if (FatBurning.Text_rotation_Target != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Text_rotation_Target.Checked = FatBurning.Text_rotation_Target.visible;
                                elementOptions.Add(FatBurning.Text_rotation_Target.position, "Text_rotation_Target");
                            }
                            if (FatBurning.Text_circle_Target != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Text_circle_Target.Checked = FatBurning.Text_circle_Target.visible;
                                elementOptions.Add(FatBurning.Text_circle_Target.position, "Text_circle_Target");
                            }
                            if (FatBurning.Pointer != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Pointer.Checked = FatBurning.Pointer.visible;
                                elementOptions.Add(FatBurning.Pointer.position, "Pointer");
                            }
                            if (FatBurning.Circle_Scale != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Circle_Scale.Checked = FatBurning.Circle_Scale.visible;
                                elementOptions.Add(FatBurning.Circle_Scale.position, "Circle_Scale");
                            }
                            if (FatBurning.Linear_Scale != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Linear_Scale.Checked = FatBurning.Linear_Scale.visible;
                                elementOptions.Add(FatBurning.Linear_Scale.position, "Linear_Scale");
                            }
                            if (FatBurning.Icon != null)
                            {
                                uCtrl_FatBurning_Elm.checkBox_Icon.Checked = FatBurning.Icon.visible;
                                elementOptions.Add(FatBurning.Icon.position, "Icon");
                            }

                            uCtrl_FatBurning_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_FatBurning_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion



                        #region ElementWeather
                        case "ElementWeather":
                            ElementWeather Weather = (ElementWeather)element;
                            uCtrl_Weather_Elm.SetVisibilityElementStatus(Weather.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Weather.Images != null)
                            {
                                uCtrl_Weather_Elm.checkBox_Images.Checked = Weather.Images.visible;
                                elementOptions.Add(Weather.Images.position, "Images");
                            }
                            if (Weather.Number != null)
                            {
                                uCtrl_Weather_Elm.checkBox_Number.Checked = Weather.Number.visible;
                                elementOptions.Add(Weather.Number.position, "Number");
                            }
                            if (Weather.Number_Min != null)
                            {
                                uCtrl_Weather_Elm.checkBox_Number_Min.Checked = Weather.Number_Min.visible;
                                elementOptions.Add(Weather.Number_Min.position, "Number_Min");
                            }
                            if (Weather.Number_Max != null)
                            {
                                uCtrl_Weather_Elm.checkBox_Number_Max.Checked = Weather.Number_Max.visible;
                                elementOptions.Add(Weather.Number_Max.position, "Number_Max");
                            }
                            if (Weather.City_Name != null)
                            {
                                uCtrl_Weather_Elm.checkBox_Text_CityName.Checked = Weather.City_Name.visible;
                                elementOptions.Add(Weather.City_Name.position, "CityName");
                            }
                            if (Weather.Icon != null)
                            {
                                uCtrl_Weather_Elm.checkBox_Icon.Checked = Weather.Icon.visible;
                                elementOptions.Add(Weather.Icon.position, "Icon");
                            }

                            uCtrl_Weather_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Weather_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementUVIndex
                        case "ElementUVIndex":
                            ElementUVIndex UVIndex = (ElementUVIndex)element;
                            uCtrl_UVIndex_Elm.SetVisibilityElementStatus(UVIndex.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (UVIndex.Images != null)
                            {
                                uCtrl_UVIndex_Elm.checkBox_Images.Checked = UVIndex.Images.visible;
                                elementOptions.Add(UVIndex.Images.position, "Images");
                            }
                            if (UVIndex.Segments != null)
                            {
                                uCtrl_UVIndex_Elm.checkBox_Segments.Checked = UVIndex.Segments.visible;
                                elementOptions.Add(UVIndex.Segments.position, "Segments");
                            }
                            if (UVIndex.Number != null)
                            {
                                uCtrl_UVIndex_Elm.checkBox_Number.Checked = UVIndex.Number.visible;
                                elementOptions.Add(UVIndex.Number.position, "Number");
                            }
                            if (UVIndex.Pointer != null)
                            {
                                uCtrl_UVIndex_Elm.checkBox_Pointer.Checked = UVIndex.Pointer.visible;
                                elementOptions.Add(UVIndex.Pointer.position, "Pointer");
                            }
                            if (UVIndex.Icon != null)
                            {
                                uCtrl_UVIndex_Elm.checkBox_Icon.Checked = UVIndex.Icon.visible;
                                elementOptions.Add(UVIndex.Icon.position, "Icon");
                            }

                            uCtrl_UVIndex_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_UVIndex_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementHumidity
                        case "ElementHumidity":
                            ElementHumidity Humidity = (ElementHumidity)element;
                            uCtrl_Humidity_Elm.SetVisibilityElementStatus(Humidity.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Humidity.Images != null)
                            {
                                uCtrl_Humidity_Elm.checkBox_Images.Checked = Humidity.Images.visible;
                                elementOptions.Add(Humidity.Images.position, "Images");
                            }
                            if (Humidity.Segments != null)
                            {
                                uCtrl_Humidity_Elm.checkBox_Segments.Checked = Humidity.Segments.visible;
                                elementOptions.Add(Humidity.Segments.position, "Segments");
                            }
                            if (Humidity.Number != null)
                            {
                                uCtrl_Humidity_Elm.checkBox_Number.Checked = Humidity.Number.visible;
                                elementOptions.Add(Humidity.Number.position, "Number");
                            }
                            if (Humidity.Pointer != null)
                            {
                                uCtrl_Humidity_Elm.checkBox_Pointer.Checked = Humidity.Pointer.visible;
                                elementOptions.Add(Humidity.Pointer.position, "Pointer");
                            }
                            if (Humidity.Icon != null)
                            {
                                uCtrl_Humidity_Elm.checkBox_Icon.Checked = Humidity.Icon.visible;
                                elementOptions.Add(Humidity.Icon.position, "Icon");
                            }

                            uCtrl_Humidity_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Humidity_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementAltimeter
                        case "ElementAltimeter":
                            ElementAltimeter Altimeter = (ElementAltimeter)element;
                            uCtrl_Altimeter_Elm.SetVisibilityElementStatus(Altimeter.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Altimeter.Number != null)
                            {
                                uCtrl_Altimeter_Elm.checkBox_Number.Checked = Altimeter.Number.visible;
                                elementOptions.Add(Altimeter.Number.position, "Number");
                            }
                            if (Altimeter.Pointer != null)
                            {
                                uCtrl_Altimeter_Elm.checkBox_Pointer.Checked = Altimeter.Pointer.visible;
                                elementOptions.Add(Altimeter.Pointer.position, "Pointer");
                            }
                            if (Altimeter.Icon != null)
                            {
                                uCtrl_Altimeter_Elm.checkBox_Icon.Checked = Altimeter.Icon.visible;
                                elementOptions.Add(Altimeter.Icon.position, "Icon");
                            }

                            uCtrl_Altimeter_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Altimeter_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementSunrise
                        case "ElementSunrise":
                            ElementSunrise Sunrise = (ElementSunrise)element;
                            uCtrl_Sunrise_Elm.SetVisibilityElementStatus(Sunrise.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Sunrise.Images != null)
                            {
                                uCtrl_Sunrise_Elm.checkBox_Images.Checked = Sunrise.Images.visible;
                                elementOptions.Add(Sunrise.Images.position, "Images");
                            }
                            if (Sunrise.Segments != null)
                            {
                                uCtrl_Sunrise_Elm.checkBox_Segments.Checked = Sunrise.Segments.visible;
                                elementOptions.Add(Sunrise.Segments.position, "Segments");
                            }
                            if (Sunrise.Sunrise != null)
                            {
                                uCtrl_Sunrise_Elm.checkBox_Sunrise.Checked = Sunrise.Sunrise.visible;
                                elementOptions.Add(Sunrise.Sunrise.position, "Sunrise");
                            }
                            if (Sunrise.Sunset != null)
                            {
                                uCtrl_Sunrise_Elm.checkBox_Sunset.Checked = Sunrise.Sunset.visible;
                                elementOptions.Add(Sunrise.Sunset.position, "Sunset");
                            }
                            if (Sunrise.Sunset_Sunrise != null)
                            {
                                uCtrl_Sunrise_Elm.checkBox_Sunset_Sunrise.Checked = Sunrise.Sunset_Sunrise.visible;
                                elementOptions.Add(Sunrise.Sunset_Sunrise.position, "Sunset_Sunrise");
                            }
                            if (Sunrise.Pointer != null)
                            {
                                uCtrl_Sunrise_Elm.checkBox_Pointer.Checked = Sunrise.Pointer.visible;
                                elementOptions.Add(Sunrise.Pointer.position, "Pointer");
                            }
                            if (Sunrise.Icon != null)
                            {
                                uCtrl_Sunrise_Elm.checkBox_Icon.Checked = Sunrise.Icon.visible;
                                elementOptions.Add(Sunrise.Icon.position, "Icon");
                            }

                            uCtrl_Sunrise_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Sunrise_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementWind
                        case "ElementWind":
                            ElementWind Wind = (ElementWind)element;
                            uCtrl_Wind_Elm.SetVisibilityElementStatus(Wind.visible);
                            elementOptions = new Dictionary<int, string>();
                            if (Wind.Images != null)
                            {
                                uCtrl_Wind_Elm.checkBox_Images.Checked = Wind.Images.visible;
                                elementOptions.Add(Wind.Images.position, "Images");
                            }
                            if (Wind.Segments != null)
                            {
                                uCtrl_Wind_Elm.checkBox_Segments.Checked = Wind.Segments.visible;
                                elementOptions.Add(Wind.Segments.position, "Segments");
                            }
                            if (Wind.Number != null)
                            {
                                uCtrl_Wind_Elm.checkBox_Number.Checked = Wind.Number.visible;
                                elementOptions.Add(Wind.Number.position, "Number");
                            }
                            if (Wind.Pointer != null)
                            {
                                uCtrl_Wind_Elm.checkBox_Pointer.Checked = Wind.Pointer.visible;
                                elementOptions.Add(Wind.Pointer.position, "Pointer");
                            }
                            if (Wind.Direction != null)
                            {
                                uCtrl_Wind_Elm.checkBox_Direction.Checked = Wind.Direction.visible;
                                elementOptions.Add(Wind.Direction.position, "Direction");
                            }
                            if (Wind.Icon != null)
                            {
                                uCtrl_Wind_Elm.checkBox_Icon.Checked = Wind.Icon.visible;
                                elementOptions.Add(Wind.Icon.position, "Icon");
                            }

                            uCtrl_Wind_Elm.SetOptionsPosition(elementOptions);

                            uCtrl_Wind_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementMoon
                        case "ElementMoon":
                            ElementMoon Moon = (ElementMoon)element;
                            uCtrl_Moon_Elm.SetVisibilityElementStatus(Moon.visible);

                            uCtrl_Moon_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion

                        #region ElementImage
                        case "ElementImage":
                            ElementImage Image = (ElementImage)element;
                            uCtrl_Image_Elm.SetVisibilityElementStatus(Image.visible);

                            uCtrl_Image_Elm.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                        #endregion
                    }
                }
            }

            int elementsCount = elements.Count;


            if (Watch_Face.Editable_Elements != null)
            {
                if (radioButton_ScreenNormal.Checked || Watch_Face.Editable_Elements.AOD_show)
                {
                    EditableElements editableElements = Watch_Face.Editable_Elements;
                    uCtrl_EditableElements_Elm.SetVisibilityElementStatus(editableElements.visible);

                    if (radioButton_ScreenNormal.Checked) uCtrl_EditableElements_Elm.Visible_ShowDel(true);
                    else uCtrl_EditableElements_Elm.Visible_ShowDel(false);

                    uCtrl_EditableElements_Elm.Visible = true;
                    SetElementPositionInGUI(editableElements.GetType().Name, count - elementsCount - 2);
                    elementsCount++; 
                }
            }

            if (Watch_Face.ElementEditablePointers != null)
            {
                ElementEditablePointers EditablePointers = Watch_Face.ElementEditablePointers;
                uCtrl_EditableTimePointer_Elm.SetVisibilityElementStatus(EditablePointers.visible);

                if (radioButton_ScreenNormal.Checked) uCtrl_EditableTimePointer_Elm.Visible_ShowDel(true);
                else uCtrl_EditableTimePointer_Elm.Visible_ShowDel(false);

                uCtrl_EditableTimePointer_Elm.Visible = true;
                SetElementPositionInGUI("ElementEditablePointers", count - elementsCount - 2);
                elementsCount++;
            }

            if (Watch_Face.TopImage != null && (radioButton_ScreenNormal.Checked || Watch_Face.TopImage.showInAOD))
            {
                TopImage Topt_Image = Watch_Face.TopImage;
                uCtrl_TopImage_Elm.SetVisibilityElementStatus(Topt_Image.visible);

                uCtrl_TopImage_Elm.Visible = true;
                uCtrl_TopImage_Elm.checkBox_ShowInAOD.Checked = Watch_Face.TopImage.showInAOD;
                SetElementPositionInGUI("TopImage", count - elementsCount - 2);
                elementsCount++;
            }

            if (Watch_Face.RepeatAlert != null && radioButton_ScreenNormal.Checked)
            {
                RepeatAlert Repeat_Alert = Watch_Face.RepeatAlert;
                uCtrl_RepeatingAlert_Elm.SetVisibilityElementStatus(Repeat_Alert.enable);

                uCtrl_RepeatingAlert_Elm.Visible = true;
                SetElementPositionInGUI("RepeatAlert", count - elementsCount - 2);
                elementsCount++;
            }

            if (Watch_Face.DisconnectAlert != null && radioButton_ScreenNormal.Checked)
            {
                DisconnectAlert Disconnect_Alert = Watch_Face.DisconnectAlert;
                uCtrl_DisconnectAlert_Elm.SetVisibilityElementStatus(Disconnect_Alert.enable);

                uCtrl_DisconnectAlert_Elm.Visible = true;
                SetElementPositionInGUI("DisconnectAlert", count - elementsCount - 2);
                elementsCount++;
            }

            if (Watch_Face.Shortcuts != null && radioButton_ScreenNormal.Checked)
            {
                ElementShortcuts Shortcuts = Watch_Face.Shortcuts;
                uCtrl_Shortcuts_Elm.SetVisibilityElementStatus(Shortcuts.visible);
                elementOptions = new Dictionary<int, string>();
                if (Shortcuts.Step != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Step.Checked = Shortcuts.Step.visible;
                    elementOptions.Add(Shortcuts.Step.position, "Step");
                }
                if (Shortcuts.Heart != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Heart.Checked = Shortcuts.Heart.visible;
                    elementOptions.Add(Shortcuts.Heart.position, "Heart");
                }
                if (Shortcuts.SPO2 != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_SPO2.Checked = Shortcuts.SPO2.visible;
                    elementOptions.Add(Shortcuts.SPO2.position, "SPO2");
                }
                if (Shortcuts.PAI != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_PAI.Checked = Shortcuts.PAI.visible;
                    elementOptions.Add(Shortcuts.PAI.position, "PAI");
                }
                if (Shortcuts.Stress != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Stress.Checked = Shortcuts.Stress.visible;
                    elementOptions.Add(Shortcuts.Stress.position, "Stress");
                }
                if (Shortcuts.Weather != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Weather.Checked = Shortcuts.Weather.visible;
                    elementOptions.Add(Shortcuts.Weather.position, "Weather");
                }
                if (Shortcuts.Altimeter != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Altimeter.Checked = Shortcuts.Altimeter.visible;
                    elementOptions.Add(Shortcuts.Altimeter.position, "Altimeter");
                }
                if (Shortcuts.Sunrise != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Sunrise.Checked = Shortcuts.Sunrise.visible;
                    elementOptions.Add(Shortcuts.Sunrise.position, "Sunrise");
                }
                if (Shortcuts.Alarm != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Alarm.Checked = Shortcuts.Alarm.visible;
                    elementOptions.Add(Shortcuts.Alarm.position, "Alarm");
                }
                if (Shortcuts.Sleep != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Sleep.Checked = Shortcuts.Sleep.visible;
                    elementOptions.Add(Shortcuts.Sleep.position, "Sleep");
                }
                if (Shortcuts.Countdown != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Countdown.Checked = Shortcuts.Countdown.visible;
                    elementOptions.Add(Shortcuts.Countdown.position, "Countdown");
                }
                if (Shortcuts.Stopwatch != null)
                {
                    uCtrl_Shortcuts_Elm.checkBox_Stopwatch.Checked = Shortcuts.Stopwatch.visible;
                    elementOptions.Add(Shortcuts.Stopwatch.position, "Stopwatch");
                }
                uCtrl_Shortcuts_Elm.SetOptionsPosition(elementOptions);

                uCtrl_Shortcuts_Elm.Visible = true;
                SetElementPositionInGUI("ElementShortcuts", count - elementsCount - 2);
            }

            PreviewView = true;
        }

        /// <summary>Перемещаем элемен в нужную позицию</summary>
        private void SetElementPositionInGUI(string type, int position)
        {
            if (position >= tableLayoutPanel_ElemetsWatchFace.RowCount || position < 0) return;
            Control panel = null;
            switch (type)
            {
                case "ElementAnalogTime":
                    panel = panel_UC_AnalogTime;
                    break;
                case "ElementAnalogTimePro":
                    panel = panel_UC_AnalogTimePro;
                    break;
                case "ElementDigitalTime":
                    panel = panel_UC_DigitalTime;
                    break;
                case "ElementEditablePointers":
                    panel = panel_UC_EditableTimePointer;
                    break;
                case "EditableElements":
                    panel = panel_UC_EditableElements;
                    break;
                case "ElementDateDay":
                    panel = panel_UC_DateDay;
                    break;
                case "ElementDateMonth":
                    panel = panel_UC_DateMonth;
                    break;
                case "ElementDateYear":
                    panel = panel_UC_DateYear;
                    break;
                case "ElementDateWeek":
                    panel = panel_UC_DateWeek;
                    break;

                case "ElementStatuses":
                    panel = panel_UC_Statuses;
                    break;
                case "ElementShortcuts":
                    panel = panel_UC_Shortcuts;
                    break;
                case "ElementAnimation":
                    panel = panel_UC_Animation;
                    break;

                case "ElementSteps":
                    panel = panel_UC_Steps;
                    break;
                case "ElementBattery":
                    panel = panel_UC_Battery;
                    break;
                case "ElementCalories":
                    panel = panel_UC_Calories;
                    break;
                case "ElementHeart":
                    panel = panel_UC_Heart;
                    break;
                case "ElementPAI":
                    panel = panel_UC_PAI;
                    break;
                case "ElementDistance":
                    panel = panel_UC_Distance;
                    break;
                case "ElementStand":
                    panel = panel_UC_Stand;
                    break;
                case "ElementActivity":
                    panel = panel_UC_Activity;
                    break;
                case "ElementSpO2":
                    panel = panel_UC_SpO2;
                    break;
                case "ElementStress":
                    panel = panel_UC_Stress;
                    break;
                case "ElementFatBurning":
                    panel = panel_UC_FatBurning;
                    break;


                case "ElementWeather":
                    panel = panel_UC_Weather;
                    break;
                case "ElementUVIndex":
                    panel = panel_UC_UVIndex;
                    break;
                case "ElementHumidity":
                    panel = panel_UC_Humidity;
                    break;
                case "ElementAltimeter":
                    panel = panel_UC_Altimeter;
                    break;
                case "ElementSunrise":
                    panel = panel_UC_Sunrise;
                    break;
                case "ElementWind":
                    panel = panel_UC_Wind;
                    break;
                case "ElementMoon":
                    panel = panel_UC_Moon;
                    break;
                case "ElementImage":
                    panel = panel_UC_Image;
                    break;

                case "DisconnectAlert":
                    panel = panel_UC_DisconnectAlert;
                    break;
                case "RepeatAlert":
                    panel = panel_UC_RepeatingAlert;
                    break;
                case "TopImage":
                    panel = panel_UC_TopImage;
                    break;
            }
            if (panel == null) return;
            int realPos = tableLayoutPanel_ElemetsWatchFace.GetRow(panel);
            if (realPos == position) return;
            if (realPos < position)
            {
                for (int i = realPos; i < position; i++)
                {
                    Control panel2 = tableLayoutPanel_ElemetsWatchFace.GetControlFromPosition(0, i + 1);
                    if (panel2 == null) return;
                    //string n = panel2.Name;
                    tableLayoutPanel_ElemetsWatchFace.SetRow(panel2, i);
                    tableLayoutPanel_ElemetsWatchFace.SetRow(panel, i + 1);
                }
            }
            else
            {
                for (int i = realPos; i > position; i--)
                {
                    Control panel2 = tableLayoutPanel_ElemetsWatchFace.GetControlFromPosition(0, i - 1);
                    if (panel2 == null) return;
                    tableLayoutPanel_ElemetsWatchFace.SetRow(panel, i - 1);
                    tableLayoutPanel_ElemetsWatchFace.SetRow(panel2, i);
                }
            }
        }

        private void radioButton_ScreenNormal_CheckedChanged(object sender, EventArgs e)
        {
            PreviewView = false;
            comboBox_AddBackground.Visible = !radioButton_ScreenNormal.Checked;
            pictureBox_IconBackground.Visible = !radioButton_ScreenNormal.Checked;
            button_CopyAOD.Visible = !radioButton_ScreenNormal.Checked;
            ShowElemetsWatchFace(); 
            PreviewView = true;
            PreviewImage();
            FormText();
        }

        private void checkBox_VisibleSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings_Load) return;
            ProgramSettings.Settings_AfterUnpack_Dialog = radioButton_Settings_AfterUnpack_Dialog.Checked;
            ProgramSettings.Settings_AfterUnpack_DoNothing = radioButton_Settings_AfterUnpack_DoNothing.Checked;
            ProgramSettings.Settings_AfterUnpack_Download = radioButton_Settings_AfterUnpack_Download.Checked;

            ProgramSettings.Settings_Open_Dialog = radioButton_Settings_Open_Dialog.Checked;
            ProgramSettings.Settings_Open_DoNotning = radioButton_Settings_Open_DoNotning.Checked;
            ProgramSettings.Settings_Open_Download = radioButton_Settings_Open_Download.Checked;

            ProgramSettings.Settings_Pack_Dialog = radioButton_Settings_Pack_Dialog.Checked;
            ProgramSettings.Settings_Pack_DoNotning = radioButton_Settings_Pack_DoNotning.Checked;
            ProgramSettings.Settings_Pack_GoToFile = radioButton_Settings_Pack_GoToFile.Checked;

            ProgramSettings.Settings_Unpack_Dialog = radioButton_Settings_Unpack_Dialog.Checked;
            ProgramSettings.Settings_Unpack_Replace = radioButton_Settings_Unpack_Replace.Checked;
            ProgramSettings.Settings_Unpack_Save = radioButton_Settings_Unpack_Save.Checked;

            ProgramSettings.ShowIn12hourFormat = checkBox_ShowIn12hourFormat.Checked;
            ProgramSettings.WatchSkin_Use = checkBox_WatchSkin_Use.Checked;
            ProgramSettings.DrawAllWidgets = checkBox_AllWidgetsInGif.Checked;
            ProgramSettings.Shortcuts_Area = checkBox_Shortcuts_Area.Checked;
            ProgramSettings.Shortcuts_Border = checkBox_Shortcuts_Border.Checked;
            ProgramSettings.Shortcuts_Image = checkBox_Shortcuts_Image.Checked;

            ProgramSettings.ShowBorder = checkBox_border.Checked;
            ProgramSettings.Crop = checkBox_crop.Checked;
            ProgramSettings.Show_CircleScale_Area = checkBox_CircleScaleImage.Checked;
            ProgramSettings.Shortcuts_Center_marker = checkBox_center_marker.Checked;
            ProgramSettings.Show_Widgets_Area = checkBox_WidgetsArea.Checked;
            ProgramSettings.Show_Shortcuts = checkBox_Show_Shortcuts.Checked;
            ProgramSettings.Shortcuts_In_Gif = checkBox_Shortcuts_In_Gif.Checked;

            //ProgramSettings.language = comboBox_Language.Text;
            if (comboBox_watch_model.SelectedIndex != -1)
            {
                ProgramSettings.Watch_Model = comboBox_watch_model.Text;

                if (comboBox_watch_model.Text == "GTR 3") ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR 3 Pro") ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 3") ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "T-Rex 2") ProgramSettings.WatchSkin_T_Rex_2 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "T-Rex Ultra") ProgramSettings.WatchSkin_T_Rex_Ultra = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR 4") ProgramSettings.WatchSkin_GTR_4 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "Amazfit Band 7") ProgramSettings.WatchSkin_Amazfit_Band_7 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 4 mini") ProgramSettings.WatchSkin_GTS_4_mini = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "Falcon") ProgramSettings.WatchSkin_Falcon = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR mini") ProgramSettings.WatchSkin_GTR_mini = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 4") ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
            }

            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);

            PreviewImage();
            FormText();
        }

        private void checkBox_UnvisibleSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings_Load) return;
            ProgramSettings.Settings_AfterUnpack_Dialog = radioButton_Settings_AfterUnpack_Dialog.Checked;
            ProgramSettings.Settings_AfterUnpack_DoNothing = radioButton_Settings_AfterUnpack_DoNothing.Checked;
            ProgramSettings.Settings_AfterUnpack_Download = radioButton_Settings_AfterUnpack_Download.Checked;

            ProgramSettings.Settings_Open_Dialog = radioButton_Settings_Open_Dialog.Checked;
            ProgramSettings.Settings_Open_DoNotning = radioButton_Settings_Open_DoNotning.Checked;
            ProgramSettings.Settings_Open_Download = radioButton_Settings_Open_Download.Checked;

            ProgramSettings.Settings_Pack_Dialog = radioButton_Settings_Pack_Dialog.Checked;
            ProgramSettings.Settings_Pack_DoNotning = radioButton_Settings_Pack_DoNotning.Checked;
            ProgramSettings.Settings_Pack_GoToFile = radioButton_Settings_Pack_GoToFile.Checked;

            ProgramSettings.Settings_Unpack_Dialog = radioButton_Settings_Unpack_Dialog.Checked;
            ProgramSettings.Settings_Unpack_Replace = radioButton_Settings_Unpack_Replace.Checked;
            ProgramSettings.Settings_Unpack_Save = radioButton_Settings_Unpack_Save.Checked;

            ProgramSettings.ShowIn12hourFormat = checkBox_ShowIn12hourFormat.Checked;
            ProgramSettings.WatchSkin_Use = checkBox_WatchSkin_Use.Checked;
            ProgramSettings.DrawAllWidgets = checkBox_AllWidgetsInGif.Checked;
            ProgramSettings.Shortcuts_Area = checkBox_Shortcuts_Area.Checked;
            ProgramSettings.Shortcuts_Border = checkBox_Shortcuts_Border.Checked;
            ProgramSettings.Shortcuts_Image = checkBox_Shortcuts_Image.Checked;
            ProgramSettings.Shortcuts_In_Gif = checkBox_Shortcuts_In_Gif.Checked;

            ProgramSettings.ShowBorder = checkBox_border.Checked;
            ProgramSettings.Crop = checkBox_crop.Checked;
            ProgramSettings.Show_CircleScale_Area = checkBox_CircleScaleImage.Checked;
            ProgramSettings.Shortcuts_Center_marker = checkBox_center_marker.Checked;
            ProgramSettings.Show_Widgets_Area = checkBox_WidgetsArea.Checked;
            ProgramSettings.Show_Shortcuts = checkBox_Show_Shortcuts.Checked;

            //ProgramSettings.language = comboBox_Language.Text;
            if (comboBox_watch_model.SelectedIndex != -1)
            {
                ProgramSettings.Watch_Model = comboBox_watch_model.Text;

                if (comboBox_watch_model.Text == "GTR 3") ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR 3 Pro") ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 3") ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "T-Rex 2") ProgramSettings.WatchSkin_T_Rex_2 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "T-Rex Ultra") ProgramSettings.WatchSkin_T_Rex_Ultra = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR 4") ProgramSettings.WatchSkin_GTR_4 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "Amazfit Band 7") ProgramSettings.WatchSkin_Amazfit_Band_7 = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 4 mini") ProgramSettings.WatchSkin_GTS_4_mini = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "Falcon") ProgramSettings.WatchSkin_Falcon = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTR mini") ProgramSettings.WatchSkin_GTR_mini = textBox_WatchSkin_Path.Text;
                if (comboBox_watch_model.Text == "GTS 4") ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
            }

            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
        }

        private void checkBox_WebW_CheckedChanged(object sender, EventArgs e)
        {
            PreviewImage();
        }

        private void comboBox_AddBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!PreviewView) return;
            AddBackground();
            PreviewView = false;
            ShowElemetsWatchFace();
            PreviewView = true;
            FormText();
        }

        private void uCtrl_Background_Elm_VisibleElemenChanged(object sender, EventArgs eventArgs, bool visible)
        {
            Background background = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Background != null) background = Watch_Face.ScreenNormal.Background;
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Background != null) background = Watch_Face.ScreenAOD.Background;
            }
            if(background != null) background.visible = visible;
            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Background_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            PreviewView = false;
            if (Watch_Face != null && Watch_Face.ScreenAOD != null)
                Watch_Face.ScreenAOD.Background = null;
            JSON_Modified = true;
            ShowElemetsWatchFace(); 
            PreviewView = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DigitalTime_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDigitalTime digitalTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    //digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDigitalTime());
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    //digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDigitalTime());
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }

            if (digitalTime != null)
            {
                if (digitalTime.Hour == null) digitalTime.Hour = new hmUI_widget_IMG_NUMBER();
                if (digitalTime.Minute == null) digitalTime.Minute = new hmUI_widget_IMG_NUMBER();
                if (digitalTime.Second == null) digitalTime.Second = new hmUI_widget_IMG_NUMBER();
                if (digitalTime.AmPm == null) digitalTime.AmPm = new hmUI_widget_IMG_TIME_am_pm();

                Dictionary<string, int> elementOptions = uCtrl_DigitalTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Hour")) digitalTime.Hour.position = elementOptions["Hour"];
                if (elementOptions.ContainsKey("Minute")) digitalTime.Minute.position = elementOptions["Minute"];
                if (elementOptions.ContainsKey("Second")) digitalTime.Second.position = elementOptions["Second"];
                if (elementOptions.ContainsKey("AmPm")) digitalTime.AmPm.position = elementOptions["AmPm"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Hours":
                        digitalTime.Hour.visible = checkBox.Checked;
                        break;
                    case "checkBox_Minutes":
                        digitalTime.Minute.visible = checkBox.Checked;
                        break;
                    case "checkBox_Seconds":
                        digitalTime.Second.visible = checkBox.Checked;
                        break;
                    case "checkBox_AmPm":
                        digitalTime.AmPm.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_DigitalTime_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DigitalTime_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDigitalTime digitalTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    //digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDigitalTime());
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    //digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDigitalTime());
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }

            if (digitalTime != null)
            {
                if (digitalTime.Hour == null) digitalTime.Hour = new hmUI_widget_IMG_NUMBER();
                if (digitalTime.Minute == null) digitalTime.Minute = new hmUI_widget_IMG_NUMBER();
                if (digitalTime.Second == null) digitalTime.Second = new hmUI_widget_IMG_NUMBER();
                if (digitalTime.AmPm == null) digitalTime.AmPm = new hmUI_widget_IMG_TIME_am_pm();

                //Dictionary<string, int> elementOptions = uCtrl_DigitalTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Hour")) digitalTime.Hour.position = elementOptions["Hour"];
                if (elementOptions.ContainsKey("Minute")) digitalTime.Minute.position = elementOptions["Minute"];
                if (elementOptions.ContainsKey("Second")) digitalTime.Second.position = elementOptions["Second"];
                if (elementOptions.ContainsKey("AmPm")) digitalTime.AmPm.position = elementOptions["AmPm"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DigitalTime_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementDigitalTime digitalTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    digitalTime = (ElementDigitalTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDigitalTime");
                }
            }
            if (digitalTime != null)
            {
                digitalTime.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            List<object> Elements = new List<object>();
            string objectName = "";
            objectName = sender.GetType().Name;
            switch (sender.GetType().Name)
            {
                case "UCtrl_DigitalTime_Elm":
                    objectName = "ElementDigitalTime";
                    break;
                case "UCtrl_AnalogTime_Elm":
                    objectName = "ElementAnalogTime";
                    break;
                case "UCtrl_AnalogTimePro_Elm":
                    objectName = "ElementAnalogTimePro";
                    break;
                //case "UCtrl_EditableTimePointer_Elm":
                //    objectName = "ElementEditablePointers";
                //break;
                case "UCtrl_DateDay_Elm":
                    objectName = "ElementDateDay";
                    break;
                case "UCtrl_DateMonth_Elm":
                    objectName = "ElementDateMonth";
                    break;
                case "UCtrl_DateYear_Elm":
                    objectName = "ElementDateYear";
                    break;
                case "UCtrl_DateWeek_Elm":
                    objectName = "ElementDateWeek";
                    break;

                //case "UCtrl_Shortcuts_Elm":
                //    objectName = "ElementShortcuts";
                //    break;
                case "UCtrl_Statuses_Elm":
                    objectName = "ElementStatuses";
                    break;
                case "UCtrl_Animation_Elm":
                    objectName = "ElementAnimation";
                    break;

                case "UCtrl_Steps_Elm":
                    objectName = "ElementSteps";
                    break;
                case "UCtrl_Battery_Elm":
                    objectName = "ElementBattery";
                    break;
                case "UCtrl_Calories_Elm":
                    objectName = "ElementCalories";
                    break;
                case "UCtrl_Heart_Elm":
                    objectName = "ElementHeart";
                    break;
                case "UCtrl_PAI_Elm":
                    objectName = "ElementPAI";
                    break;
                case "UCtrl_Distance_Elm":
                    objectName = "ElementDistance";
                    break;
                case "UCtrl_Stand_Elm":
                    objectName = "ElementStand";
                    break;
                case "UCtrl_Activity_Elm":
                    objectName = "ElementActivity";
                    break;
                case "UCtrl_SpO2_Elm":
                    objectName = "ElementSpO2";
                    break;
                case "UCtrl_Stress_Elm":
                    objectName = "ElementStress";
                    break;
                case "UCtrl_FatBurning_Elm":
                    objectName = "ElementFatBurning";
                    break;

                case "UCtrl_Weather_Elm":
                    objectName = "ElementWeather";
                    break;
                case "UCtrl_UVIndex_Elm":
                    objectName = "ElementUVIndex";
                    break;
                case "UCtrl_Humidity_Elm":
                    objectName = "ElementHumidity";
                    break;
                case "UCtrl_Altimeter_Elm":
                    objectName = "ElementAltimeter";
                    break;
                case "UCtrl_Sunrise_Elm":
                    objectName = "ElementSunrise";
                    break;
                case "UCtrl_Wind_Elm":
                    objectName = "ElementWind";
                    break;
                case "UCtrl_Moon_Elm":
                    objectName = "ElementMoon";
                    break;
                case "UCtrl_Image_Elm":
                    objectName = "ElementImage";
                    break;
            }
            if (objectName.Length > 0)
            {
                if (radioButton_ScreenNormal.Checked)
                {
                    if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                        Watch_Face.ScreenNormal.Elements != null)
                    {
                        Elements = Watch_Face.ScreenNormal.Elements;
                    }
                }
                else
                {
                    if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                        Watch_Face.ScreenAOD.Elements != null)
                    {
                        Elements = Watch_Face.ScreenAOD.Elements;
                    }
                }

                bool exists = Elements.Exists(e => e.GetType().Name == objectName);
                if (exists)
                {
                    int index = Elements.FindIndex(e => e.GetType().Name == objectName);
                    Elements.RemoveAt(index);

                    PreviewView = false;
                    ShowElemetsWatchFace();
                    PreviewView = true;
                }

                JSON_Modified = true;
                PreviewImage();
                FormText(); 
            }
        }

        private void uCtrl_EditableTimePointer_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            if (Watch_Face != null && Watch_Face.ElementEditablePointers != null)
            {
                Watch_Face.ElementEditablePointers = null;

                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            if (Watch_Face != null && Watch_Face.Editable_Elements != null)
            {
                Watch_Face.Editable_Elements = null;

                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Shortcuts_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            if (Watch_Face != null && Watch_Face.Shortcuts != null)
            {
                Watch_Face.Shortcuts = null;

                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DisconnectAlert_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            if (Watch_Face != null && Watch_Face.DisconnectAlert != null)
            {
                Watch_Face.DisconnectAlert = null;

                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_RepeatingAlert_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            if (Watch_Face != null && Watch_Face.RepeatAlert != null)
            {
                Watch_Face.RepeatAlert = null;

                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_TopImage_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            if (Watch_Face != null && Watch_Face.TopImage != null)
            {
                Watch_Face.TopImage = null;

                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private FileInfo[] FileInfoSort(FileInfo[] fileInfo)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = fileInfo.Length;
            progressBar1.Visible = true;
            if (fileInfo.Length < 2)
            {
                progressBar1.Visible = false;
                return fileInfo;
            }
            for (int i = 0; i < fileInfo.Length - 1; i++)
            {
                progressBar1.Value++;
                //progressBar1.Update();
                for (int j = i + 1; j < fileInfo.Length; j++)
                {
                    int compare = FileInfoCompare(fileInfo[i], fileInfo[j]);
                    if (compare > 0)
                    {
                        FileInfo temp = fileInfo[i];
                        fileInfo[i] = fileInfo[j];
                        fileInfo[j] = temp;
                    }
                }
            }
            progressBar1.Visible = false;
            return fileInfo;
        }

        public int FileInfoCompare(FileInfo fileInfo1, FileInfo fileInfo2)
        {
            // разделяем на блоки
            string name1 = fileInfo1.Name;
            string name2 = fileInfo2.Name;
            name1 = Path.GetFileNameWithoutExtension(name1);
            name2 = Path.GetFileNameWithoutExtension(name2);
            int value1 = 0;
            int value2 = 0;
            if (Int32.TryParse(name1, out value1) && Int32.TryParse(name2, out value2))
            {
                if (name1.Length != name2.Length)
                {
                    if (name1.Length < name2.Length) return -1;
                    if (name1.Length > name2.Length) return 1;
                }

                if (value1 < value2) return -1;
                if (value1 > value2) return 1;
                if (value1 == value2)
                    return name1.CompareTo(name2);
            }

            string[] parts1 = name1.Split(new char[] { '-', '_', '.' });
            string[] parts2 = name2.Split(new char[] { '-', '_', '.' });

            // приводим цифровые блоки к одной длине
            for (int i = 0; i < parts1.Length; i++)
            {
                int ruselt;
                if (Int32.TryParse(parts1[i], out ruselt))
                {
                    int toPad = 10 - parts1[i].Length;
                    if (toPad < 0) toPad = 0;
                    parts1[i] = parts1[i].Insert(0, new String('0', toPad));
                }
            }
            for (int i = 0; i < parts2.Length; i++)
            {
                int ruselt;
                if (Int32.TryParse(parts2[i], out ruselt))
                {
                    int toPad = 10 - parts2[i].Length;
                    if (toPad < 0) toPad = 0;
                    parts2[i] = parts2[i].Insert(0, new String('0', toPad));
                }
            }

            // объединяем обратно в строку
            string toCompare1 = string.Join("", parts1);
            string toCompare2 = string.Join("", parts2);

            // сравниваем строки
            int ret = toCompare1.CompareTo(toCompare2);
            //Console.WriteLine("Compare1=" + toCompare1);
            //Console.WriteLine("Compare2=" + toCompare2);
            //Console.WriteLine("return=" + ret.ToString());
            //Console.WriteLine(" ");

            return toCompare1.CompareTo(toCompare2);
        }

        private void userCtrl_Set_ValueChanged(object sender, EventArgs eventArgs, int setNumber)
        {
            switch (setNumber)
            {
                case 1:
                    SetPreferences(userCtrl_Set1);
                    break;
                case 2:
                    SetPreferences(userCtrl_Set2);
                    break;
                case 3:
                    SetPreferences(userCtrl_Set3);
                    break;
                case 4:
                    SetPreferences(userCtrl_Set4);
                    break;
                case 5:
                    SetPreferences(userCtrl_Set5);
                    break;
                case 6:
                    SetPreferences(userCtrl_Set6);
                    break;
                case 7:
                    SetPreferences(userCtrl_Set7);
                    break;
                case 8:
                    SetPreferences(userCtrl_Set8);
                    break;
                case 9:
                    SetPreferences(userCtrl_Set9);
                    break;
                case 10:
                    SetPreferences(userCtrl_Set10);
                    break;
                case 11:
                    SetPreferences(userCtrl_Set11);
                    break;
                case 12:
                    SetPreferences(userCtrl_Set12);
                    break;
            }

            PreviewImage();
        }

        private void button_RandomPreview_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            Random rnd = new Random();
            int year = now.Year;
            int month = rnd.Next(0, 12) + 1;
            int day = rnd.Next(0, 28) + 1;
            int weekDay = rnd.Next(0, 7) + 1;
            int hour = rnd.Next(0, 24);
            int min = rnd.Next(0, 60);
            int sec = rnd.Next(0, 60);
            int battery = rnd.Next(0, 101);
            int calories = rnd.Next(0, 500);
            int pulse = rnd.Next(45, 180);
            int distance = rnd.Next(0, 15000);
            int steps = rnd.Next(0, 15000);
            int goal = rnd.Next(0, 15000);
            int pai = rnd.Next(0, 150);
            int standUp = rnd.Next(0, 13);
            int stress = rnd.Next(0, 101);
            int fatBurning = rnd.Next(0, 35);
            bool bluetooth = rnd.Next(2) == 0 ? false : true;
            bool alarm = rnd.Next(2) == 0 ? false : true;
            bool unlocked = rnd.Next(2) == 0 ? false : true;
            bool dnd = rnd.Next(2) == 0 ? false : true;

            int temperature = rnd.Next(-25, 35);
            int temperatureMax = rnd.Next(-25, 35);
            int temperatureMin = temperatureMax - rnd.Next(3, 10);
            int temperatureIcon = rnd.Next(0, 29);
            bool showTemperature = rnd.Next(7) == 0 ? false : true;

            int airPressure = rnd.Next(800, 1200);
            int airQuality = rnd.Next(0, 650);
            int altitude = rnd.Next(0, 100);
            int humidity = rnd.Next(30, 100);
            int UVindex = rnd.Next(0, 13);
            int windForce = rnd.Next(0, 13);
            int windDirection = rnd.Next(0, 8);

            WatchFacePreviewSet.Date.Year = year;
            WatchFacePreviewSet.Date.Month = month;
            WatchFacePreviewSet.Date.Day = day;
            WatchFacePreviewSet.Date.WeekDay = weekDay;

            WatchFacePreviewSet.Time.Hours = hour;
            WatchFacePreviewSet.Time.Minutes = min;
            WatchFacePreviewSet.Time.Seconds = sec;

            WatchFacePreviewSet.Battery = battery;
            WatchFacePreviewSet.Activity.Steps = steps;
            WatchFacePreviewSet.Activity.StepsGoal = goal;
            WatchFacePreviewSet.Activity.Calories = calories;
            WatchFacePreviewSet.Activity.HeartRate = pulse;
            WatchFacePreviewSet.Activity.PAI = pai;
            WatchFacePreviewSet.Activity.Distance = distance;
            WatchFacePreviewSet.Activity.StandUp = standUp;
            WatchFacePreviewSet.Activity.Stress = stress;
            WatchFacePreviewSet.Activity.FatBurning = fatBurning;

            WatchFacePreviewSet.Status.Bluetooth = bluetooth;
            WatchFacePreviewSet.Status.Alarm = alarm;
            WatchFacePreviewSet.Status.Lock = unlocked;
            WatchFacePreviewSet.Status.DoNotDisturb = dnd;

            WatchFacePreviewSet.Weather.Temperature = temperature;
            WatchFacePreviewSet.Weather.TemperatureMin = temperatureMin;
            WatchFacePreviewSet.Weather.TemperatureMax = temperatureMax;
            WatchFacePreviewSet.Weather.Icon = temperatureIcon;
            WatchFacePreviewSet.Weather.showTemperature = showTemperature;

            WatchFacePreviewSet.Weather.AirPressure = airPressure;
            WatchFacePreviewSet.Weather.AirQuality = airQuality;
            WatchFacePreviewSet.Weather.Altitude = altitude;
            WatchFacePreviewSet.Weather.Humidity = humidity;
            WatchFacePreviewSet.Weather.UVindex = UVindex;
            WatchFacePreviewSet.Weather.WindForce = windForce;
            WatchFacePreviewSet.Weather.WindDirection = windDirection;
            PreviewImage();
        }

        private void StartJsonPreview()
        {
            Random rnd = new Random();
            userCtrl_Set1.RandomValue(rnd);
            userCtrl_Set2.RandomValue(rnd);
            userCtrl_Set3.RandomValue(rnd);
            userCtrl_Set4.RandomValue(rnd);
            userCtrl_Set5.RandomValue(rnd);
            userCtrl_Set6.RandomValue(rnd);
            userCtrl_Set7.RandomValue(rnd);
            userCtrl_Set8.RandomValue(rnd);
            userCtrl_Set9.RandomValue(rnd);
            userCtrl_Set10.RandomValue(rnd);
            userCtrl_Set11.RandomValue(rnd);
            userCtrl_Set12.RandomValue(rnd);

            for (int i = 1; i < 13; i++)
            {
                DateTime date = DateTime.Now;
                int year;
                int month;
                int day;
                int weekDay;
                int offsetDay;

                switch (i)
                {
                    case 1:
                        date = userCtrl_Set1.dateTimePicker_Date_Set.Value;
                        break;
                    case 2:
                        date = userCtrl_Set2.dateTimePicker_Date_Set.Value;
                        break;
                    case 3:
                        date = userCtrl_Set3.dateTimePicker_Date_Set.Value;
                        break;
                    case 4:
                        date = userCtrl_Set5.dateTimePicker_Date_Set.Value;
                        break;
                    case 5:
                        date = userCtrl_Set5.dateTimePicker_Date_Set.Value;
                        break;
                    case 6:
                        date = userCtrl_Set6.dateTimePicker_Date_Set.Value;
                        break;
                    case 7:
                        date = userCtrl_Set7.dateTimePicker_Date_Set.Value;
                        break;
                    case 8:
                        date = userCtrl_Set8.dateTimePicker_Date_Set.Value;
                        break;
                    case 9:
                        date = userCtrl_Set9.dateTimePicker_Date_Set.Value;
                        break;
                    case 10:
                        date = userCtrl_Set10.dateTimePicker_Date_Set.Value;
                        break;
                    case 11:
                        date = userCtrl_Set11.dateTimePicker_Date_Set.Value;
                        break;
                    case 12:
                        date = userCtrl_Set12.dateTimePicker_Date_Set.Value;
                        break;
                }


                year = date.Year;
                month = i;
                //int month = date.Month;
                day = date.Day;
                date = new DateTime(year, month, day);
                weekDay = (int)date.DayOfWeek;
                offsetDay = i - weekDay;
                day = day + offsetDay;
                while (day < 1)
                {
                    day = day + 7;
                }
                while (day > 28)
                {
                    day = day - 7;
                }
                date = new DateTime(year, month, day);

                switch (i)
                {
                    case 1:
                        userCtrl_Set1.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 2:
                        userCtrl_Set2.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 3:
                        userCtrl_Set3.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 4:
                        userCtrl_Set4.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 5:
                        userCtrl_Set5.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 6:
                        userCtrl_Set6.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 7:
                        userCtrl_Set7.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 8:
                        userCtrl_Set8.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 9:
                        userCtrl_Set9.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 10:
                        userCtrl_Set10.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 11:
                        userCtrl_Set11.dateTimePicker_Date_Set.Value = date;
                        break;
                    case 12:
                        userCtrl_Set12.dateTimePicker_Date_Set.Value = date;
                        break;
                }
            }

            SetPreferences(userCtrl_Set12);
            if (!userCtrl_Set1.Collapsed) SetPreferences(userCtrl_Set1);
            if (!userCtrl_Set2.Collapsed) SetPreferences(userCtrl_Set2);
            if (!userCtrl_Set3.Collapsed) SetPreferences(userCtrl_Set3);
            if (!userCtrl_Set4.Collapsed) SetPreferences(userCtrl_Set4);
            if (!userCtrl_Set5.Collapsed) SetPreferences(userCtrl_Set5);
            if (!userCtrl_Set6.Collapsed) SetPreferences(userCtrl_Set6);
            if (!userCtrl_Set7.Collapsed) SetPreferences(userCtrl_Set7);
            if (!userCtrl_Set8.Collapsed) SetPreferences(userCtrl_Set8);
            if (!userCtrl_Set9.Collapsed) SetPreferences(userCtrl_Set9);
            if (!userCtrl_Set10.Collapsed) SetPreferences(userCtrl_Set10);
            if (!userCtrl_Set11.Collapsed) SetPreferences(userCtrl_Set11);
        }

        // считываем параметры из JsonPreview
         void JsonPreview_Read(string fullfilename)
        {
            string text = File.ReadAllText(fullfilename);

            PreviewView = false;
            Prewiev_States_Json ps = new Prewiev_States_Json();
            try
            {
                var objson = JsonConvert.DeserializeObject<object[]>(text);

                int count = objson.Count();

                string JSON_Text = JsonConvert.SerializeObject(objson, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                //richTextBox_JsonText.Text = JSON_Text;

                if (count == 0) return;
                if (count > 12) count = 12;
                for (int i = 0; i < count; i++)
                {
                    ps = JsonConvert.DeserializeObject<Prewiev_States_Json>(objson[i].ToString(), new JsonSerializerSettings
                    {
                        //DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    Dictionary<string, int> Activity = new Dictionary<string, int>();
                    Dictionary<string, int> Air = new Dictionary<string, int>();
                    Dictionary<string, bool> checkValue = new Dictionary<string, bool>();

                    Activity.Add("Year", ps.Time.Year);
                    Activity.Add("Month", ps.Time.Month);
                    Activity.Add("Day", ps.Time.Day);

                    Activity.Add("Hour", ps.Time.Hour);
                    Activity.Add("Minute", ps.Time.Minute);
                    Activity.Add("Second", ps.Time.Second);

                    Activity.Add("Battery", ps.BatteryLevel);
                    Activity.Add("Calories", ps.Calories);
                    Activity.Add("HeartRate", ps.Pulse);
                    Activity.Add("Distance", ps.Distance);
                    Activity.Add("Steps", ps.Steps);
                    Activity.Add("StepsGoal", ps.Goal);

                    Activity.Add("PAI", ps.PAI);
                    Activity.Add("StandUp", ps.Stand);
                    Activity.Add("Stress", ps.Stress);
                    //Activity.Add("ActivityGoal", ps.ActivityGoal);
                    Activity.Add("FatBurning", ps.FatBurning);


                    Air.Add("Weather_Icon", ps.CurrentWeather);
                    Air.Add("Temperature", ps.CurrentTemperature);
                    Air.Add("TemperatureMax", ps.TemperatureMax);
                    Air.Add("TemperatureMin", ps.TemperatureMin);

                    Air.Add("UVindex", ps.UVindex);
                    Air.Add("AirQuality", ps.AirQuality);
                    Air.Add("Humidity", ps.Humidity);
                    Air.Add("WindForce", ps.WindForce);
                    Air.Add("WindDirection", ps.WindDirection);
                    Air.Add("Altitude", ps.Altitude);
                    Air.Add("AirPressure", ps.AirPressure);


                    checkValue.Add("Bluetooth", ps.Bluetooth);
                    checkValue.Add("Alarm", ps.Alarm);
                    checkValue.Add("Lock", ps.Unlocked);
                    checkValue.Add("DND", ps.DoNotDisturb);

                    checkValue.Add("ShowTemperature", ps.ShowTemperature);

                    switch (i)
                    {
                        case 0:
                            userCtrl_Set1.SetValue(Activity, Air, checkValue);
                            break;
                        case 1:
                            userCtrl_Set2.SetValue(Activity, Air, checkValue);
                            break;
                        case 2:
                            userCtrl_Set3.SetValue(Activity, Air, checkValue);
                            break;
                        case 3:
                            userCtrl_Set4.SetValue(Activity, Air, checkValue);
                            break;
                        case 4:
                            userCtrl_Set5.SetValue(Activity, Air, checkValue);
                            break;
                        case 5:
                            userCtrl_Set6.SetValue(Activity, Air, checkValue);
                            break;
                        case 6:
                            userCtrl_Set7.SetValue(Activity, Air, checkValue);
                            break;
                        case 7:
                            userCtrl_Set8.SetValue(Activity, Air, checkValue);
                            break;
                        case 8:
                            userCtrl_Set9.SetValue(Activity, Air, checkValue);
                            break;
                        case 9:
                            userCtrl_Set10.SetValue(Activity, Air, checkValue);
                            break;
                        case 10:
                            userCtrl_Set11.SetValue(Activity, Air, checkValue);
                            break;
                        case 11:
                            userCtrl_Set12.SetValue(Activity, Air, checkValue);
                            break;
                    }
                }

                switch (count)
                {
                    case 1:
                        SetPreferences(userCtrl_Set1);
                        break;
                    case 2:
                        SetPreferences(userCtrl_Set2);
                        break;
                    case 3:
                        SetPreferences(userCtrl_Set3);
                        break;
                    case 4:
                        SetPreferences(userCtrl_Set4);
                        break;
                    case 5:
                        SetPreferences(userCtrl_Set5);
                        break;
                    case 6:
                        SetPreferences(userCtrl_Set6);
                        break;
                    case 7:
                        SetPreferences(userCtrl_Set7);
                        break;
                    case 8:
                        SetPreferences(userCtrl_Set8);
                        break;
                    case 9:
                        SetPreferences(userCtrl_Set9);
                        break;
                    case 10:
                        SetPreferences(userCtrl_Set10);
                        break;
                    case 11:
                        SetPreferences(userCtrl_Set11);
                        break;
                    case 12:
                        SetPreferences(userCtrl_Set12);
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.FormStrings.Message_JsonReadError_Text, Properties.FormStrings.Message_Error_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            SetPreferences(userCtrl_Set12);
            if (!userCtrl_Set1.Collapsed) SetPreferences(userCtrl_Set1);
            if (!userCtrl_Set2.Collapsed) SetPreferences(userCtrl_Set2);
            if (!userCtrl_Set3.Collapsed) SetPreferences(userCtrl_Set3);
            if (!userCtrl_Set4.Collapsed) SetPreferences(userCtrl_Set4);
            if (!userCtrl_Set5.Collapsed) SetPreferences(userCtrl_Set5);
            if (!userCtrl_Set6.Collapsed) SetPreferences(userCtrl_Set6);
            if (!userCtrl_Set7.Collapsed) SetPreferences(userCtrl_Set7);
            if (!userCtrl_Set8.Collapsed) SetPreferences(userCtrl_Set8);
            if (!userCtrl_Set9.Collapsed) SetPreferences(userCtrl_Set9);
            if (!userCtrl_Set10.Collapsed) SetPreferences(userCtrl_Set10);
            if (!userCtrl_Set11.Collapsed) SetPreferences(userCtrl_Set11);

            PreviewView = true;
            //PreviewImage();
        }

        private void button_JsonPreview_Read_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FullFileDir;
            //openFileDialog.Filter = Properties.FormStrings.FilterJson;
            openFileDialog.FileName = "Preview.States";
            openFileDialog.Filter = "PreviewStates file | *.States";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_PreviewStates;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullfilename = openFileDialog.FileName;
                JsonPreview_Read(fullfilename);
                PreviewImage();
            }
        }

        private void button_JsonPreview_Write_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //openFileDialog.InitialDirectory = subPath;
            //saveFileDialog.Filter = Properties.FormStrings.FilterJson;
            saveFileDialog.FileName = "Preview.States";
            saveFileDialog.Filter = "PreviewStates file | *.States";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = Properties.FormStrings.Dialog_Title_PreviewStates;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                object[] objson = new object[] { };
                int count = 0;
                for (int i = 0; i < 12; i++)
                {
                    Prewiev_States_Json ps = new Prewiev_States_Json();
                    ps.Time = new TimePreview();
                    Dictionary<string, int> Activity = new Dictionary<string, int>();
                    Dictionary<string, int> Air = new Dictionary<string, int>();
                    Dictionary<string, bool> checkValue = new Dictionary<string, bool>();
                    switch (i)
                    {
                        case 0:
                            userCtrl_Set1.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 1:
                            userCtrl_Set2.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 2:
                            userCtrl_Set3.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 3:
                            userCtrl_Set4.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 4:
                            userCtrl_Set5.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 5:
                            userCtrl_Set6.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 6:
                            userCtrl_Set7.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 7:
                            userCtrl_Set8.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 8:
                            userCtrl_Set9.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 9:
                            userCtrl_Set10.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 10:
                            userCtrl_Set11.GetValue(out Activity, out Air, out checkValue);
                            break;
                        case 11:
                            userCtrl_Set12.GetValue(out Activity, out Air, out checkValue);
                            break;
                    }

                    ps.Time.Year = Activity["Year"];
                    ps.Time.Month = Activity["Month"];
                    ps.Time.Day = Activity["Day"];

                    ps.Time.Hour = Activity["Hour"];
                    ps.Time.Minute = Activity["Minute"];
                    ps.Time.Second = Activity["Second"];

                    ps.BatteryLevel = Activity["Battery"];
                    ps.Calories = Activity["Calories"];
                    ps.Pulse = Activity["HeartRate"];
                    ps.Distance = Activity["Distance"];
                    ps.Steps = Activity["Steps"];
                    ps.Goal = Activity["StepsGoal"];

                    ps.PAI = Activity["PAI"];
                    ps.Stand = Activity["StandUp"];
                    ps.Stress = Activity["Stress"];
                    //ps.ActivityGoal = Activity["ActivityGoal"];
                    ps.FatBurning = Activity["FatBurning"];


                    ps.CurrentWeather = Air["Weather_Icon"];
                    ps.CurrentTemperature = Air["Temperature"];
                    ps.TemperatureMax = Air["TemperatureMax"];
                    ps.TemperatureMin = Air["TemperatureMin"];

                    ps.UVindex = Air["UVindex"];
                    ps.AirQuality = Air["AirQuality"];
                    ps.Humidity = Air["Humidity"];
                    ps.WindForce = Air["WindForce"];
                    ps.WindDirection = Air["WindDirection"];
                    ps.Altitude = Air["Altitude"];
                    ps.AirPressure = Air["AirPressure"];


                    ps.Bluetooth = checkValue["Bluetooth"];
                    ps.Alarm = checkValue["Alarm"];
                    ps.Unlocked = checkValue["Lock"];
                    ps.DoNotDisturb = checkValue["DND"];

                    ps.ShowTemperature = checkValue["ShowTemperature"];

                    //if (ps.Calories != 1234)
                    //{
                        Array.Resize(ref objson, objson.Length + 1);
                        objson[count] = ps;
                        count++;
                    //}
                }

                string string_json_temp = JsonConvert.SerializeObject(objson, Formatting.None, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                var objsontemp = JsonConvert.DeserializeObject<object[]>(string_json_temp);

                string formatted = JsonConvert.SerializeObject(objsontemp, Formatting.Indented);
                //richTextBox_JsonText.Text = formatted;


                if (formatted.Length < 10)
                {
                    MessageBox.Show(Properties.FormStrings.Message_SaveOnly1234_Text);
                    return;
                }
                //text = text.Replace(@"\", "");
                //text = text.Replace("\"{", "{");
                //text = text.Replace("}\"", "}");
                //text = text.Replace(",", ",\r\n");
                //text = text.Replace(":", ": ");
                //text = text.Replace(": {", ": {\r\n");
                //string formatted = JsonConvert.SerializeObject(text, Formatting.Indented);

                string fullfilename = saveFileDialog.FileName;
                //richTextBox_JsonText.Text = formatted;
                File.WriteAllText(fullfilename, formatted, Encoding.UTF8);
            }
        }

        private void button_JsonPreview_Random_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            userCtrl_Set1.RandomValue(rnd);
            userCtrl_Set2.RandomValue(rnd);
            userCtrl_Set3.RandomValue(rnd);
            userCtrl_Set4.RandomValue(rnd);
            userCtrl_Set5.RandomValue(rnd);
            userCtrl_Set6.RandomValue(rnd);
            userCtrl_Set7.RandomValue(rnd);
            userCtrl_Set8.RandomValue(rnd);
            userCtrl_Set9.RandomValue(rnd);
            userCtrl_Set10.RandomValue(rnd);
            userCtrl_Set11.RandomValue(rnd);
            userCtrl_Set12.RandomValue(rnd);

            //PreviewImage();
            SetPreferences(userCtrl_Set12);
            if (!userCtrl_Set1.Collapsed) SetPreferences(userCtrl_Set1);
            if (!userCtrl_Set2.Collapsed) SetPreferences(userCtrl_Set2);
            if (!userCtrl_Set3.Collapsed) SetPreferences(userCtrl_Set3);
            if (!userCtrl_Set4.Collapsed) SetPreferences(userCtrl_Set4);
            if (!userCtrl_Set5.Collapsed) SetPreferences(userCtrl_Set5);
            if (!userCtrl_Set6.Collapsed) SetPreferences(userCtrl_Set6);
            if (!userCtrl_Set7.Collapsed) SetPreferences(userCtrl_Set7);
            if (!userCtrl_Set8.Collapsed) SetPreferences(userCtrl_Set8);
            if (!userCtrl_Set9.Collapsed) SetPreferences(userCtrl_Set9);
            if (!userCtrl_Set10.Collapsed) SetPreferences(userCtrl_Set10);
            if (!userCtrl_Set11.Collapsed) SetPreferences(userCtrl_Set11);
            //PreviewView = true;
            PreviewImage();
        }

        private void comboBox_watch_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_watch_model.SelectedIndex == -1) return;
            ProgramSettings.Watch_Model = comboBox_watch_model.Text;
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3":
                case "T-Rex 2":
                case "T-Rex Ultra":
                    pictureBox_Preview.Size = new Size((int)(230 * currentDPI), (int)(230 * currentDPI));
                    break;
                case "GTR 3 Pro":
                    pictureBox_Preview.Size = new Size((int)(230 * currentDPI), (int)(230 * currentDPI));
                    break;
                case "GTS 3":
                case "GTS 4":
                    pictureBox_Preview.Size = new Size((int)(198 * currentDPI), (int)(228 * currentDPI));
                    break;
                case "GTR 4":
                    pictureBox_Preview.Size = new Size((int)(230 * currentDPI), (int)(230 * currentDPI));
                    break;
                case "Amazfit Band 7":
                    pictureBox_Preview.Size = new Size((int)(100 * currentDPI), (int)(187 * currentDPI));
                    break;
                case "GTS 4 mini":
                    pictureBox_Preview.Size = new Size((int)(171 * currentDPI), (int)(195 * currentDPI));
                    break;
                case "Falcon":
                case "GTR mini":
                    pictureBox_Preview.Size = new Size((int)(211 * currentDPI), (int)(211 * currentDPI));
                    break;
            }

            // изменяем размер панели для предпросмотра если она не влазит
            if (pictureBox_Preview.Top + pictureBox_Preview.Height > label_watch_model.Top)
            {
                float newHeight = label_watch_model.Top - pictureBox_Preview.Top;
                float scale = newHeight / pictureBox_Preview.Height;
                pictureBox_Preview.Size = new Size((int)(pictureBox_Preview.Width * scale), (int)(pictureBox_Preview.Height * scale));
            }

            if ((formPreview != null) && (formPreview.Visible))
            {
                if (Form_Preview.Watch_Model != comboBox_watch_model.Text)
                {
                    if (comboBox_watch_model.SelectedIndex == -1) Form_Preview.Watch_Model = "GTR 3";
                    else Form_Preview.Watch_Model = comboBox_watch_model.Text;
                }
                formPreview.radioButton_CheckedChanged(sender, e);
            }

            if (Settings_Load) return;

            if (Watch_Face == null) Watch_Face = new WATCH_FACE();
            if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();

            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_3;

                    Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 454;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 454;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 454;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 454;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 454;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 454;
                        }
                    }
                    break;
                case "GTR 3 Pro":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_3_Pro;

                    if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                    Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";

                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 480;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 480;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 480;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 480;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 480;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 480;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 480;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 480;
                        }
                    }
                    break;
                case "GTS 3":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTS_3;

                    if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                    Watch_Face.WatchFace_Info.DeviceName = "GTS3";

                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 390;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 450;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 390;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 450;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 390;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 450;
                        }
                    }
                    break;
                case "T-Rex 2":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_T_Rex_2;

                    Watch_Face.WatchFace_Info.DeviceName = "T_Rex_2";
                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 454;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 454;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 454;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 454;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 454;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 454;
                        }
                    }
                    break;
                case "T-Rex Ultra":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_T_Rex_Ultra;

                    Watch_Face.WatchFace_Info.DeviceName = "T_Rex_Ultra";
                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 454;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 454;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 454;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 454;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 454;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 454;
                        }
                    }
                    break;
                case "GTR 4":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_4;

                    Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 466;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 466;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 466;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 466;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 466;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 466;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 466;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 466;
                        }
                    }
                    break;
                case "Amazfit Band 7":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_Amazfit_Band_7;

                    if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                    Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";

                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 194;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 368;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 194;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 368;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 194;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 368;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 194;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 368;
                        }
                    }
                    break;
                case "GTS 4 mini":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTS_4_mini;

                    if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                    Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";

                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 336;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 384;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 336;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 384;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 336;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 384;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 336;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 384;
                        }
                    }
                    break;
                case "Falcon":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_Falcon;

                    if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                    Watch_Face.WatchFace_Info.DeviceName = "Falcon";

                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 416;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 416;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 416;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 416;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 416;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 416;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 416;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 416;
                        }
                    }
                    break;
                case "GTR mini":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_mini;

                    if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                    Watch_Face.WatchFace_Info.DeviceName = "GTR_mini";

                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 416;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 416;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 416;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 416;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 416;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 416;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 416;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 416;
                        }
                    }
                    break;
                case "GTS 4":
                    textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTS_3;

                    if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                    Watch_Face.WatchFace_Info.DeviceName = "GTS4";

                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                    {
                        if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                            Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                        }
                        if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenNormal.Background.BackgroundImage.w = 390;
                            Watch_Face.ScreenNormal.Background.BackgroundImage.h = 450;
                        }
                    }

                    if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                    {
                        if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundColor.w = 390;
                            Watch_Face.ScreenAOD.Background.BackgroundColor.h = 450;
                        }
                        if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                        {
                            Watch_Face.ScreenAOD.Background.BackgroundImage.w = 390;
                            Watch_Face.ScreenAOD.Background.BackgroundImage.h = 450;
                        }
                    }
                    break;
            }

            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);

            if (Watch_Face != null && Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                ChangSizeBackground(Watch_Face.ScreenNormal.Background);

            if (Watch_Face != null && Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                ChangSizeBackground(Watch_Face.ScreenAOD.Background);

            if (ProgramSettings.Watch_Model == "T-Rex 2" /*ProgramSettings.Watch_Model == "GTR 4" || ProgramSettings.Watch_Model == "GTS 4"*/)
            {
                uCtrl_Animation_Elm.MotionAnimation = false;
                uCtrl_Animation_Elm.RotateAnimation = false;
            }
            else
            {
                uCtrl_Animation_Elm.MotionAnimation = true;
                uCtrl_Animation_Elm.RotateAnimation = true;
            }

            PreviewImage();
            JSON_Modified = true;
            FormText();

            //JSON_write();
            //PreviewImage();
        }

        private void ChangSizeBackground(Background background)
        {
            if(background.BackgroundColor != null)
            {
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                    case "T-Rex Ultra":
                        background.BackgroundColor.h = 454;
                        background.BackgroundColor.w = 454;
                        break;
                    case "GTR 3 Pro":
                        background.BackgroundColor.h = 480;
                        background.BackgroundColor.w = 480;
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        background.BackgroundColor.h = 450;
                        background.BackgroundColor.w = 390;
                        break;
                    case "GTR 4":
                        background.BackgroundColor.h = 466;
                        background.BackgroundColor.w = 466;
                        break;
                    case "Amazfit Band 7":
                        background.BackgroundColor.h = 368;
                        background.BackgroundColor.w = 194;
                        break;
                    case "GTS 4 mini":
                        background.BackgroundColor.h = 384;
                        background.BackgroundColor.w = 336;
                        break;
                    case "Falcon":
                    case "GTR mini":
                        background.BackgroundColor.h = 416;
                        background.BackgroundColor.w = 416;
                        break;
                }
            }
            if (background.BackgroundImage != null)
            {
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                    case "T-Rex Ultra":
                        background.BackgroundImage.h = 454;
                        background.BackgroundImage.w = 454;
                        break;
                    case "GTR 3 Pro":
                        background.BackgroundImage.h = 480;
                        background.BackgroundImage.w = 480;
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        background.BackgroundImage.h = 450;
                        background.BackgroundImage.w = 390;
                        break;
                    case "GTR 4":
                        background.BackgroundImage.h = 466;
                        background.BackgroundImage.w = 466;
                        break;
                    case "Amazfit Band 7":
                        background.BackgroundImage.h = 368;
                        background.BackgroundImage.w = 194;
                        break;
                    case "GTS 4 mini":
                        background.BackgroundImage.h = 384;
                        background.BackgroundImage.w = 336;
                        break;
                    case "Falcon":
                    case "GTR mini":
                        background.BackgroundImage.h = 416;
                        background.BackgroundImage.w = 416;
                        break;
                }
            }
        }

        private void button_pack_zip_Click(object sender, EventArgs e)
        {
            // сохранение если файл не сохранен
            if (SaveRequest() == DialogResult.Cancel) return;

            if (FullFileDir == null) return;
            string tempDir = Application.StartupPath + @"\Temp";
            string templatesFileDir = Application.StartupPath + @"\File_templates";

            string zipPath = FullFileDir + @"\" + Path.GetFileNameWithoutExtension(FileName) + ".zip";
            try
            {
                if (File.Exists(zipPath)) File.Delete(zipPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.FormStrings.Message_DontDelZip + Environment.NewLine + Environment.NewLine + ex.Message, 
                    Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
            if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
            Directory.CreateDirectory(tempDir);
            Directory.CreateDirectory(tempDir + @"\assets");
            Directory.CreateDirectory(tempDir + @"\watchface");

            string imagesFolder = FullFileDir + @"\assets";
            DirectoryInfo Folder;
            Folder = new DirectoryInfo(imagesFolder);
            //FileInfo[] Images;
            FileInfo[] Images = Folder.GetFiles("*.png");

            progressBar1.Value = 0;
            progressBar1.Maximum = Images.Length;
            progressBar1.Visible = true;
            bool fix_size = true;
            int fix_color = 1;
            if (comboBox_watch_model.Text == "Amazfit Band 7" || comboBox_watch_model.Text == "GTS 4 mini")
            {
                fix_size = false;
                fix_color = 2;
            }
            if (comboBox_watch_model.Text == "GTR mini")
            {
                fix_size = false;
                fix_color = 3;
            }
            foreach (FileInfo file in Images)
            {
                progressBar1.Value++;
                string fileNameFull = PngToTga(file.FullName, tempDir + @"\assets", fix_color, fix_size);
                if (fileNameFull != null) ImageFix(fileNameFull, fix_color);
            }

            imagesFolder = FullFileDir + @"\assets\animation";
            if (Directory.Exists(imagesFolder))
            {
                Folder = new DirectoryInfo(imagesFolder);
                Images = Folder.GetFiles("*.png");
                progressBar1.Maximum = progressBar1.Maximum + Images.Length;
                foreach (FileInfo file in Images)
                {
                    progressBar1.Value++;
                    string fileNameFull = PngToTga(file.FullName, tempDir + @"\assets\animation", fix_color, fix_size);
                    if (fileNameFull != null) ImageFix(fileNameFull, fix_color);
                }
            }

            App_WatchFace app = new App_WatchFace();
            app.app.appName = Path.GetFileNameWithoutExtension(FileName);
            app.i18n.enUS.appName = Path.GetFileNameWithoutExtension(FileName);
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null)
            {
                if (Watch_Face.WatchFace_Info.WatchFaceId > 999 && Watch_Face.WatchFace_Info.WatchFaceId < 10000000)
                {
                    app.app.appId = Watch_Face.WatchFace_Info.WatchFaceId;
                }
                if (Watch_Face.WatchFace_Info.Preview != null && Watch_Face.WatchFace_Info.Preview.Length > 0)
                {
                    app.app.icon = Watch_Face.WatchFace_Info.Preview + ".png";
                    app.app.cover.Add(Watch_Face.WatchFace_Info.Preview + ".png");
                    app.i18n.enUS.icon = Watch_Face.WatchFace_Info.Preview + ".png";
                }
            }
            if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Elements != null)
            {
                List<object> Elements = Watch_Face.ScreenNormal.Elements;
                //bool exists = Elements.Exists(el => el.GetType().Name == "ElementAnimation"); // проверяем чтотакой элемент есть
                //if (exists) app.module.watchface.hightCost = 1;
                ElementAnimation animation = (ElementAnimation)Watch_Face.ScreenNormal.Elements.Find(ea => ea.GetType().Name == "ElementAnimation");
                if (animation != null)
                {
                    if (animation.visible)
                    {
                        bool anim_exists = false;
                        if (animation.Frame_Animation_List != null && animation.Frame_Animation_List.visible) anim_exists = true;
                        if (animation.Motion_Animation_List != null && animation.Motion_Animation_List.visible) anim_exists = true;
                        if (animation.Rotate_Animation_List != null && animation.Rotate_Animation_List.visible) anim_exists = true;
                        if (anim_exists) app.module.watchface.hightCost = 1;
                    }
                }
            }
            if (Watch_Face.ScreenAOD != null) app.module.watchface.lockscreen = 1;
            if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
            {
                if (Watch_Face.ScreenNormal.Background.Editable_Background != null &&
                    Watch_Face.ScreenNormal.Background.Editable_Background.enable_edit_bg &&
                    Watch_Face.ScreenNormal.Background.Editable_Background.BackgroundList != null &&
                    Watch_Face.ScreenNormal.Background.Editable_Background.BackgroundList.Count > 0) app.module.watchface.editable = 1;
            }
            if (Watch_Face.Editable_Elements != null && Watch_Face.Editable_Elements.visible) app.module.watchface.editable = 1;
            if (Watch_Face.ElementEditablePointers != null && Watch_Face.ElementEditablePointers.visible && 
                Watch_Face.ElementEditablePointers.config != null && Watch_Face.ElementEditablePointers.config.Count > 0) app.module.watchface.editable = 1;
            switch (ProgramSettings.Watch_Model)
            {
                case "GTR 3 Pro":
                    app.platforms.Add(new Platform() { name = "Amazfit GTR 3 Pro", deviceSource = 229 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTR 3 Pro", deviceSource = 230 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTR 3 Pro", deviceSource = 6095106 });

                    app.designWidth = 480;
                    break;
                case "GTS 3":
                case "GTS 4":
                    app.platforms.Add(new Platform() { name = "Amazfit GTS 3", deviceSource = 224 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTS 3", deviceSource = 225 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTS 4", deviceSource = 7995648 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTS 4", deviceSource = 7995649 });

                    app.designWidth = 390;
                    break;
                case "GTR 4":
                    app.platforms.Add(new Platform() { name = "Amazfit GTR 4", deviceSource = 7930112 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTR 4", deviceSource = 7930113 });

                    app.designWidth = 466;
                    break;
                case "Amazfit Band 7":
                    app.platforms.Add(new Platform() { name = "Amazfit Band 7", deviceSource = 252 });
                    app.platforms.Add(new Platform() { name = "Amazfit Band 7", deviceSource = 253 });
                    app.platforms.Add(new Platform() { name = "Amazfit Band 7", deviceSource = 254 });

                    app.designWidth = 194;
                    break;
                case "GTS 4 mini":
                    app.platforms.Add(new Platform() { name = "Amazfit GTS 4 Mini", deviceSource = 246 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTS 4 Mini", deviceSource = 247 });

                    app.designWidth = 336;
                    break;
                case "Falcon":
                    app.platforms.Add(new Platform() { name = "Amazfit Falcon", deviceSource = 414 });
                    app.platforms.Add(new Platform() { name = "Amazfit Falcon", deviceSource = 415 });

                    app.designWidth = 416;
                    break;
                case "GTR mini":
                    app.platforms.Add(new Platform() { name = "Amazfit GTR Mini", deviceSource = 250 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTR Mini", deviceSource = 251 });

                    app.designWidth = 416;
                    break;

                default:
                    app.platforms.Add(new Platform() { name = "Amazfit GTR 3", deviceSource = 226 });
                    app.platforms.Add(new Platform() { name = "Amazfit GTR 3", deviceSource = 227 });
                    app.platforms.Add(new Platform() { name = "Amazfit T-Rex 2", deviceSource = 418 });
                    app.platforms.Add(new Platform() { name = "Amazfit T-Rex 2", deviceSource = 419 });
                    app.platforms.Add(new Platform() { name = "Amazfit T-Rex Ultra", deviceSource = 6553856 });
                    app.platforms.Add(new Platform() { name = "Amazfit T-Rex Ultra", deviceSource = 6553857 });

                    app.designWidth = 454;
                    break;
            }
            string appText = JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });

            File.WriteAllText(tempDir + @"\app.json", appText, Encoding.UTF8);
            File.Copy(templatesFileDir + @"\app.js", tempDir + @"\app.js");

            // преобразуем настройки в текстовый файл
            string variables = "";
            string items = "";
            //string widgetDelegate = "";

            JsonToJS(out variables, out items);


            string indexText = File.ReadAllText(templatesFileDir + @"\index.js");
            string versionText = "v " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            indexText = indexText.Replace("* Watch_Face_Editor tool v1.x", "* Watch_Face_Editor tool " + versionText);

            if (variables.Length>0) indexText = indexText.Replace("//Variable declaration section", variables);
            if (items.Length > 0) indexText = indexText.Replace("//Item description section", items);

            // удаляем слушателя для пульса
            int pos_destory = indexText.IndexOf("heart_rate.addEventListener");
            if (pos_destory > 0)
            {
                //pos_destory = indexText.IndexOf("console.log('index page.js on destory invoke')");
                //pos_destory = indexText.IndexOf("n.log(\"index page.js on destroy invoke\")");
                pos_destory = indexText.IndexOf("logger.log(\"index page.js on destroy invoke\")");
                if (pos_destory > 0)
                {
                    indexText = indexText.Insert(pos_destory, "heart_rate.removeEventListener(heart.event.CURRENT, hrCurrListener);"
                                + Environment.NewLine + TabInString(8)); 
                }
            }
            indexText = indexText.Replace("\r", "");

            File.WriteAllText(tempDir + @"\watchface\index.js", indexText, Encoding.UTF8);
            //link:
            // объединяем все в архив
            string startPath = tempDir;
            //string zipPath = FullFileDir + @"\" + Path.GetFileNameWithoutExtension(FileName) + ".zip";
            //if (File.Exists(zipPath)) File.Delete(zipPath);
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                zip.AddDirectory(startPath);
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.Save(zipPath);
            }

            // открываем файл если создали его
            if (File.Exists(zipPath))
            {
                if (ProgramSettings.Settings_Pack_Dialog)
                {
                    if (MessageBox.Show(Properties.FormStrings.Message_GoToFile_Text,
                    Properties.FormStrings.Message_GoToFile_Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + zipPath));
                    }
                }
                else if (ProgramSettings.Settings_Pack_GoToFile)
                {
                    Process.Start(new ProcessStartInfo("explorer.exe", " /select, " + zipPath));
                } 
            }

            //if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
#if !DEBUG
            if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
#endif
            progressBar1.Visible = false;
        }

        private void ImageFix(string fileNameFull, int fix_color)
        {
            if (File.Exists(fileNameFull))
            {
                try
                {
                    byte[] _streamBuffer;
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    string path = Path.GetDirectoryName(fileNameFull);
                    //fileName = Path.Combine(path, fileName);

                    //ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);

                    // читаем картинку в массив
                    using (var fileStream = File.OpenRead(fileNameFull))
                    {
                        _streamBuffer = new byte[fileStream.Length];
                        fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                        Header header = new Header(_streamBuffer);
                        ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());

                        int ColorMapCount = header.GetColorMapCount(); // количество цветов в карте
                        byte ColorMapEntrySize = header.GetColorMapEntrySize(); // битность цвета
                        byte ImageIDLength = header.GetImageIDLength(); // длина описания
                        ColorMap ColorMap = new ColorMap(_streamBuffer, ColorMapCount, ColorMapEntrySize, 18 + ImageIDLength);

                        int ColorMapLength = ColorMap._colorMap.Length;
                        Image_data imageData = new Image_data(_streamBuffer, 18 + ImageIDLength + ColorMapLength);

                        Footer footer = new Footer();

                        #region fix
                        header.SetImageIDLength(46);
                        imageDescription.SetSize(46, ImageWidth);
                        //imageDescription.SetSize(46, header.Width);

                        int colorMapCount = ColorMap.ColorMapCount;
                        //if (checkBox_Color256.Checked && !checkBox_32bit.Checked)
                        //{
                        //    colorMapCount = 256;
                        //    header.SetColorMapCount(colorMapCount);
                        //    if (!checkBox_32bit.Checked) ColorMap.SetColorCount(colorMapCount);
                        //}
                        bool argb_brga = true;
                        colorMapCount = 256;
                        header.SetColorMapCount(colorMapCount);
                        byte colorMapEntrySize = 32;

                        ColorMap.RestoreColor(colorMapList);
                        ColorMap.ColorsFix(argb_brga, colorMapCount, colorMapEntrySize, fix_color);
                        header.SetColorMapEntrySize(32);
                        #endregion

                        int newLength = 18 + header.GetImageIDLength() + ColorMap._colorMap.Length + imageData._imageData.Length;
                        //if (checkBox_Footer.Checked) newLength = newLength + footer._footer.Length;
                        byte[] newTGA = new byte[newLength];

                        header._header.CopyTo(newTGA, 0);
                        int offset = header._header.Length;

                        imageDescription._imageDescription.CopyTo(newTGA, offset);
                        offset = offset + imageDescription._imageDescription.Length;

                        ColorMap._colorMap.CopyTo(newTGA, offset);
                        offset = offset + ColorMap._colorMap.Length;

                        imageData._imageData.CopyTo(newTGA, offset);
                        offset = offset + imageData._imageData.Length;

                        //if (checkBox_Footer.Checked) footer._footer.CopyTo(newTGA, offset);

                        if (newTGA != null && newTGA.Length > 0)
                        {
                            string newFileName = Path.Combine(path, fileName + ".png");

                            using (var fileStreamTGA = File.OpenWrite(newFileName))
                            {
                                fileStreamTGA.Write(newTGA, 0, newTGA.Length);
                                fileStreamTGA.Flush();
                            }
                        }
                    }

                    try
                    {
                        File.Delete(fileNameFull);
                    }
                    catch (Exception)
                    {
                    }

                }
                catch (Exception exp)
                {
                    MessageBox.Show(Properties.FormStrings.Message_ImageFix_Error + Environment.NewLine + exp, 
                        Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button_RefreshPreview_Click(object sender, EventArgs e)
        {
            if (FileName == null || FullFileDir == null) return;
            if (Watch_Face == null || Watch_Face.WatchFace_Info == null || Watch_Face.WatchFace_Info.Preview == null)
            {
                button_CreatePreview_Click(null, null);
                return;
            }
            if (Watch_Face.WatchFace_Info.Preview != null && Watch_Face.WatchFace_Info.Preview.Length > 0)
            {
                string preview = FullFileDir + @"\assets\" + Watch_Face.WatchFace_Info.Preview + ".png";
                if (!File.Exists(preview))
                {
                    Watch_Face.WatchFace_Info.Preview = null;
                    button_CreatePreview_Click(null, null);
                    return;
                }
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                int PreviewHeight = 306;
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        PreviewHeight = 324;
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        PreviewHeight = 306;
                        break;
                    case "GTR 4":
                        bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        PreviewHeight = 314;
                        break;
                    case "Amazfit Band 7":
                        bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
                        PreviewHeight = 288;
                        break;
                    case "GTS 4 mini":
                        bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        PreviewHeight = 256;
                        break;
                    case "Falcon":
                    case "GTR mini":
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        PreviewHeight = 280;
                        break;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true, false, 
                    false, false, link, false, -1, false, 0);
                if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);

;
                Image loadedImage = null;
                using (FileStream stream = new FileStream(preview, FileMode.Open, FileAccess.Read))
                {
                    loadedImage = Image.FromStream(stream);
                }
                float scale = (float)PreviewHeight / bitmap.Height;
                if (loadedImage.Height != PreviewHeight)
                {
                    DialogResult ResultDialog = MessageBox.Show(Properties.FormStrings.Message_WarningPreview_Text1 +
                        Environment.NewLine + Properties.FormStrings.Message_WarningPreview_Text2,
                        Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (ResultDialog == DialogResult.Yes) scale = (float)loadedImage.Height / bitmap.Height;
                }
                bitmap = ResizeImage(bitmap, scale);
                bitmap.Save(preview, ImageFormat.Png);

                bitmap.Dispose();
                loadedImage.Dispose();

                LoadImage(Path.GetDirectoryName(preview) + @"\");
                HideAllElemenrOptions();
                ResetHighlightState("");
            }
        }

        private void button_CreatePreview_Click(object sender, EventArgs e)
        {
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null && Watch_Face.WatchFace_Info.Preview != null) return;
            if (FileName != null && FullFileDir != null) // проект уже сохранен
            {
                // формируем картинку для предпросмотра
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                int PreviewHeight = 306;
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        PreviewHeight = 324;
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        PreviewHeight = 306;
                        break;
                    case "GTR 4":
                        bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        PreviewHeight = 314;
                        break;
                    case "Amazfit Band 7":
                        bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
                        PreviewHeight = 288;
                        break;
                    case "GTS 4 mini":
                        bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        PreviewHeight = 256;
                        break;
                    case "Falcon":
                    case "GTR mini":
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        PreviewHeight = 280;
                        break;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true, false, 
                    false, false, link, false, -1, false, 0);
                if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);

                float scale = (float)PreviewHeight / bitmap.Height;
                bitmap = ResizeImage(bitmap, scale);
                //bitmap.Save(ListImagesFullName[i], ImageFormat.Png);

                // определяем имя файла для сохранения и сохраняем файл
                int i = 1;
                string NamePreview = "Preview.png";
                string PathPreview = FullFileDir + @"\assets\" + NamePreview;
                while (File.Exists(PathPreview) && i < 10)
                {
                    NamePreview = "Preview" + i.ToString() + ".png";
                    PathPreview = FullFileDir + @"\assets\" + NamePreview;
                    i ++;
                    if (i > 9)
                    {
                        MessageBox.Show(Properties.FormStrings.Message_Wrong_Preview_Exists,
                            Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                bitmap.Save(PathPreview, ImageFormat.Png);
                string fileNameOnly = Path.GetFileNameWithoutExtension(PathPreview);

                PreviewView = false;

                LoadImage(Path.GetDirectoryName(PathPreview) + @"\");

                if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                Watch_Face.WatchFace_Info.Preview = fileNameOnly;
                PreviewView = true;
                JSON_Modified = true;
                FormText();
                HideAllElemenrOptions();
                ResetHighlightState("");

                bitmap.Dispose();
                button_CreatePreview.Visible = false;
                button_RefreshPreview.Visible = true;
            }
        }

        private void button_unpack_zip_Click(object sender, EventArgs e)
        {

            // сохранение если файл не сохранен
            if (SaveRequest() == DialogResult.Cancel) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Binary File (*.bin)|*.bin";
            openFileDialog.Filter = Properties.FormStrings.FilterZip;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_Unpack;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullfilename = openFileDialog.FileName;
                Unpack_Zip(fullfilename);
            }
        }

        private void Unpack_Zip(string fullFileName)
        {
            if (!File.Exists(fullFileName)) return;
            string tempDir = Application.StartupPath + @"\Temp";
            //if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
            if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
            Directory.CreateDirectory(tempDir);
            string watchFacePath = Application.StartupPath + @"\Watch_face\";
            if (!Directory.Exists(watchFacePath)) Directory.CreateDirectory(watchFacePath);

            string projectName = Path.GetFileNameWithoutExtension(fullFileName);
            projectName = projectName.Replace(" ", "_");
            string projectPath = watchFacePath + projectName;
            // если файл существует
            if (Directory.Exists(projectPath))
            {
                string folderName = Path.GetFileNameWithoutExtension(projectPath);
                string path = Path.GetDirectoryName(projectPath);
                string newFullPath = projectPath;
                if (ProgramSettings.Settings_Unpack_Dialog)
                {
                    Logger.WriteLine("File.Exists");
                    FormFileExists f = new FormFileExists();
                    f.ShowDialog();
                    int dialogResult = f.Data;

                    switch (dialogResult)
                    {
                        case 0:
                            return;
                        //break;
                        case 1:
                            Logger.WriteLine("File.Copy");
                            newFullPath = Path.Combine(path, folderName);
                            //if (Directory.Exists(newFullPath)) Directory.Delete(newFullPath, true); 
                            if (Directory.Exists(newFullPath)) DeleteDirectory(newFullPath);
                            break;
                        case 2:
                            Logger.WriteLine("newFileName.Copy");
                            int count = 1;

                            while (Directory.Exists(newFullPath))
                            {
                                string tempFolderName = string.Format("{0}({1})", folderName, count++);
                                newFullPath = Path.Combine(path, tempFolderName);
                            }
                            break;
                    }
                }
                else if (ProgramSettings.Settings_Unpack_Save)
                {
                    Logger.WriteLine("newFileName.Copy");
                    int count = 1;

                    while (Directory.Exists(newFullPath))
                    {
                        string tempFolderName = string.Format("{0}({1})", folderName, count++);
                        newFullPath = Path.Combine(path, tempFolderName);
                    }
                }
                else if (ProgramSettings.Settings_Unpack_Replace)
                {
                    Logger.WriteLine("File.Copy");
                    newFullPath = Path.Combine(path, folderName);
                    //if (Directory.Exists(newFullPath)) Directory.Delete(newFullPath, true);
                    if (Directory.Exists(newFullPath)) DeleteDirectory(newFullPath);
                }
                projectPath = newFullPath;
            }
            //ZipFile.OpenRead(fullFileName);
            //ZipFile.ExtractToDirectory(fullFileName, tempDir);
            using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(fullFileName))
            {
                zip.ExtractAll(tempDir);
            }

            if (Directory.Exists(tempDir + @"\assets"))
            {
                progressBar1.Value = 0;
                progressBar1.Visible = true;

                List<string> allDirs = GetRecursDirectories(tempDir + @"\assets", 5, tempDir + @"\assets");
                Directory.CreateDirectory(projectPath);
                Directory.CreateDirectory(projectPath + @"\assets");
                foreach (string dirNames in allDirs)
                {
                    //Console.WriteLine(dirNames);
                    Directory.CreateDirectory(projectPath + @"\assets" + dirNames);
                }

                List<string> allFiles = GetRecursFiles(tempDir + @"\assets", "*.png", 5, tempDir + @"\assets");

                progressBar1.Maximum = allFiles.Count;
                int progress = 0;
                //bool fix_color = true;
                int fix_color = 1;
                if (comboBox_watch_model.Text == "Amazfit Band 7" || comboBox_watch_model.Text == "GTS 4 mini") fix_color = 2;
                if (comboBox_watch_model.Text == "GTR mini") fix_color = 3;
                //if (comboBox_watch_model.Text == "Amazfit Band 7" || comboBox_watch_model.Text == "GTS 4 mini") fix_color = false;
                foreach (string fileNames in allFiles)
                {
                    //Console.WriteLine(fileNames);
                    //TgaToPng(tempDir + @"\assets" + fileNames, projectPath + @"\assets" + fileNames, comboBox_watch_model.Text);
                    ImageAutoDetectReadFormat(tempDir + @"\assets" + fileNames, projectPath + @"\assets" + fileNames, fix_color);
                    progress++;
                    progressBar1.Value = progress;
                }

                // читаем данные из текста и преобразуем их в json
                string index_js = @"\watchface\index.js";
                if (File.Exists(tempDir + @"\app.json"))  // читаем путь к файлу с кодом циферблата
                {
                    string appText = File.ReadAllText(tempDir + @"\app.json");
                    try
                    {
                        App_WatchFace appJson = JsonConvert.DeserializeObject<App_WatchFace>(appText, new JsonSerializerSettings
                        {
                            DefaultValueHandling = DefaultValueHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        if (appJson != null && appJson.module != null)
                        {
                            if (appJson.module.watchface != null && appJson.module.watchface.path != null &&
                                appJson.module.watchface.path.Length > 2) index_js = @"\" + appJson.module.watchface.path + ".js";
                            index_js = index_js.Replace("/", @"\");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                ///////////////////////////
                JSToJson(tempDir + index_js); // создаем новый json файл циферблата
                if (Watch_Face != null && Watch_Face.ScreenNormal != null)
                {
                    if (File.Exists(tempDir + @"\app.json"))
                    {
                        string appText = File.ReadAllText(tempDir + @"\app.json");
                        try
                        {
                            App_WatchFace appJson = JsonConvert.DeserializeObject<App_WatchFace>(appText, new JsonSerializerSettings
                            {
                                DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                            if(appJson != null && appJson.app != null)
                            {
                                if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                                if (appJson.app.appId > 1000) Watch_Face.WatchFace_Info.WatchFaceId = appJson.app.appId;
                                else
                                {
                                    Random rnd = new Random();
                                    int ID = rnd.Next(1000, 10000000);
                                    Watch_Face.WatchFace_Info.WatchFaceId = ID;
                                }
                                if (appJson.app.icon != null && appJson.app.icon.Length > 3)
                                    Watch_Face.WatchFace_Info.Preview = Path.GetFileNameWithoutExtension(appJson.app.icon);

                                if (appJson.app.appName != null && appJson.app.appName.Length > 0)
                                    projectName = appJson.app.appName;

                                int width = 0;

                                if (Watch_Face.ScreenAOD != null && Watch_Face.ScreenAOD.Background != null)
                                {
                                    if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                                        width = Watch_Face.ScreenAOD.Background.BackgroundColor.w;
                                    if (Watch_Face.ScreenAOD.Background.BackgroundImage != null &&
                                        Watch_Face.ScreenAOD.Background.BackgroundImage.w != null)
                                        width = (int)Watch_Face.ScreenAOD.Background.BackgroundImage.w;
                                }

                                if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null)
                                {
                                    if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                                        width = Watch_Face.ScreenNormal.Background.BackgroundColor.w;
                                    if (Watch_Face.ScreenNormal.Background.BackgroundImage != null &&
                                        Watch_Face.ScreenNormal.Background.BackgroundImage.w != null)
                                        width = (int)Watch_Face.ScreenNormal.Background.BackgroundImage.w;
                                    if (Watch_Face.ScreenNormal.Background.Editable_Background != null)
                                        width = Watch_Face.ScreenNormal.Background.Editable_Background.w;

                                    if (Watch_Face.ScreenNormal.Background.Editable_Background != null &&
                                        Watch_Face.ScreenNormal.Background.Editable_Background.AOD_show)
                                    {
                                        if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                                        if (Watch_Face.ScreenAOD.Background == null) Watch_Face.ScreenAOD.Background = new Background();
                                    }
                                }

                                switch (width)
                                {
                                    case 454:
                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                        break;
                                    case 480:
                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";
                                        break;
                                    case 390:
                                        Watch_Face.WatchFace_Info.DeviceName = "GTS3";
                                        break;
                                    case 466:
                                        Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                                        break;
                                    case 194:
                                        Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";
                                        break;
                                    case 336:
                                        Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";
                                        break;
                                    case 416:
                                        Watch_Face.WatchFace_Info.DeviceName = "Falcon";
                                        break;

                                    default:
                                        Background background = Watch_Face.ScreenNormal.Background;
                                        if (background == null || 
                                            (background.BackgroundImage == null && background.Editable_Background == null))
                                        {
                                            if (background == null) background = new Background();
                                            if (background.BackgroundColor == null)
                                                background.BackgroundColor = new hmUI_widget_FILL_RECT();
                                            background.BackgroundColor.color = ColorToString(Color.Black);
                                            background.BackgroundColor.x = 0;
                                            background.BackgroundColor.y = 0;
                                            switch (ProgramSettings.Watch_Model)
                                            {
                                                case "GTR 3":
                                                    background.BackgroundColor.h = 454;
                                                    background.BackgroundColor.w = 454;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                                    break;
                                                case "T-Rex 2":
                                                    background.BackgroundColor.h = 454;
                                                    background.BackgroundColor.w = 454;
                                                    Watch_Face.WatchFace_Info.DeviceName = "T_Rex_2";
                                                    break;
                                                case "T-Rex Ultra":
                                                    background.BackgroundColor.h = 454;
                                                    background.BackgroundColor.w = 454;
                                                    Watch_Face.WatchFace_Info.DeviceName = "T_Rex_Ultra";
                                                    break;
                                                case "GTR 3 Pro":
                                                    background.BackgroundColor.h = 480;
                                                    background.BackgroundColor.w = 480;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";
                                                    break;
                                                case "GTS 3":
                                                    background.BackgroundColor.h = 450;
                                                    background.BackgroundColor.w = 390;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTS3";
                                                    break;
                                                case "GTR 4":
                                                    background.BackgroundColor.h = 466;
                                                    background.BackgroundColor.w = 466;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                                                    break;
                                                case "Amazfit Band 7":
                                                    background.BackgroundColor.h = 368;
                                                    background.BackgroundColor.w = 194;
                                                    Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";
                                                    break;
                                                case "GTS 4 mini":
                                                    background.BackgroundColor.h = 384;
                                                    background.BackgroundColor.w = 336;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";
                                                    break;
                                                case "Falcon":
                                                    background.BackgroundColor.h = 416;
                                                    background.BackgroundColor.w = 416;
                                                    Watch_Face.WatchFace_Info.DeviceName = "Falcon";
                                                    break;
                                                case "GTR mini":
                                                    background.BackgroundColor.h = 416;
                                                    background.BackgroundColor.w = 416;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTR_mini";
                                                    break;
                                                case "GTS 4":
                                                    background.BackgroundColor.h = 450;
                                                    background.BackgroundColor.w = 390;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTS4";
                                                    break;
                                                default:
                                                    background.BackgroundColor.h = 454;
                                                    background.BackgroundColor.w = 454;
                                                    Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                                    break;
                                            }
                                            background.BackgroundImage = null;
                                            Watch_Face.ScreenNormal.Background = background; 
                                        }
                                        else
                                        {
                                            if(background.Editable_Background != null && 
                                                background.Editable_Background.BackgroundList != null &&
                                                background.Editable_Background.BackgroundList.Count > 0)
                                            {
                                                int h = 454;
                                                int w = 454;
                                                switch (ProgramSettings.Watch_Model)
                                                {
                                                    case "GTR 3":
                                                        h = 454;
                                                        w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                                        break;
                                                    case "T-Rex 2":
                                                        h = 454;
                                                        w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_2";
                                                        break;
                                                    case "T-Rex Ultra":
                                                        h = 454;
                                                        w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_Ultra";
                                                        break;
                                                    case "GTR 3 Pro":
                                                        h = 480;
                                                        w = 480;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";
                                                        break;
                                                    case "GTS 3":
                                                        h = 450;
                                                        w = 390;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTS3";
                                                        break;
                                                    case "GTR 4":
                                                        h = 466;
                                                        w = 466;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                                                        break;
                                                    case "Amazfit Band 7":
                                                        h = 368;
                                                        w = 194;
                                                        Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";
                                                        break;
                                                    case "GTS 4 mini":
                                                        h = 384;
                                                        w = 336;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";
                                                        break;
                                                    case "Falcon":
                                                        h = 416;
                                                        w = 416;
                                                        Watch_Face.WatchFace_Info.DeviceName = "Falcon";
                                                        break;
                                                    case "GTR mini":
                                                        h = 416;
                                                        w = 416;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR_mini";
                                                        break;
                                                    case "GTS 4":
                                                        h = 450;
                                                        w = 390;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTS4";
                                                        break;
                                                    default:
                                                        h = 454;
                                                        w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                                        break;
                                                }
                                                background.Editable_Background.h = h;
                                                background.Editable_Background.w = w;
                                            }
                                            else if (background.BackgroundImage != null)
                                            {
                                                switch (ProgramSettings.Watch_Model)
                                                {
                                                    case "GTR 3":
                                                        background.BackgroundImage.h = 454;
                                                        background.BackgroundImage.w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                                        break;
                                                    case "T-Rex 2":
                                                        background.BackgroundImage.h = 454;
                                                        background.BackgroundImage.w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_2";
                                                        break;
                                                    case "T-Rex Ultra":
                                                        background.BackgroundImage.h = 454;
                                                        background.BackgroundImage.w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "T_Rex_Ultra";
                                                        break;
                                                    case "GTR 3 Pro":
                                                        background.BackgroundImage.h = 480;
                                                        background.BackgroundImage.w = 480;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";
                                                        break;
                                                    case "GTS 3":
                                                        background.BackgroundImage.h = 450;
                                                        background.BackgroundImage.w = 390;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTS3";
                                                        break;
                                                    case "GTR 4":
                                                        background.BackgroundImage.h = 466;
                                                        background.BackgroundImage.w = 466;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                                                        break;
                                                    case "Amazfit Band 7":
                                                        background.BackgroundImage.h = 368;
                                                        background.BackgroundImage.w = 194;
                                                        Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";
                                                        break;
                                                    case "GTS 4 mini":
                                                        background.BackgroundImage.h = 384;
                                                        background.BackgroundImage.w = 336;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";
                                                        break;
                                                    case "Falcon":
                                                        background.BackgroundImage.h = 416;
                                                        background.BackgroundImage.w = 416;
                                                        Watch_Face.WatchFace_Info.DeviceName = "Falcon";
                                                        break;
                                                    case "GTR mini":
                                                        background.BackgroundImage.h = 416;
                                                        background.BackgroundImage.w = 416;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR_mini";
                                                        break;
                                                    case "GTS 4":
                                                        background.BackgroundImage.h = 450;
                                                        background.BackgroundImage.w = 390;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTS4";
                                                        break;
                                                    default:
                                                        background.BackgroundImage.h = 454;
                                                        background.BackgroundImage.w = 454;
                                                        Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                }

                                // есло всетаки не определили фон и модель
                                Background backgroundEmpty = Watch_Face.ScreenNormal.Background;
                                if (backgroundEmpty == null ||
                                    (backgroundEmpty.BackgroundImage == null && backgroundEmpty.BackgroundColor == null &&
                                    backgroundEmpty.Editable_Background == null))
                                {
                                    if (backgroundEmpty == null) backgroundEmpty = new Background();
                                    if (backgroundEmpty.BackgroundColor == null)
                                        backgroundEmpty.BackgroundColor = new hmUI_widget_FILL_RECT();
                                    backgroundEmpty.BackgroundColor.color = ColorToString(Color.Black);
                                    backgroundEmpty.BackgroundColor.x = 0;
                                    backgroundEmpty.BackgroundColor.y = 0;
                                    switch (ProgramSettings.Watch_Model)
                                    {
                                        case "GTR 3":
                                            backgroundEmpty.BackgroundColor.h = 454;
                                            backgroundEmpty.BackgroundColor.w = 454;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                            break;
                                        case "T-Rex 2":
                                            backgroundEmpty.BackgroundColor.h = 454;
                                            backgroundEmpty.BackgroundColor.w = 454;
                                            Watch_Face.WatchFace_Info.DeviceName = "T_Rex_2";
                                            break;
                                        case "T-Rex Ultra":
                                            backgroundEmpty.BackgroundColor.h = 454;
                                            backgroundEmpty.BackgroundColor.w = 454;
                                            Watch_Face.WatchFace_Info.DeviceName = "T_Rex_Ultra";
                                            break;
                                        case "GTR 3 Pro":
                                            backgroundEmpty.BackgroundColor.h = 480;
                                            backgroundEmpty.BackgroundColor.w = 480;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";
                                            break;
                                        case "GTS 3":
                                            backgroundEmpty.BackgroundColor.h = 450;
                                            backgroundEmpty.BackgroundColor.w = 390;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTS3";
                                            break;
                                        case "GTR 4":
                                            backgroundEmpty.BackgroundColor.h = 466;
                                            backgroundEmpty.BackgroundColor.w = 466;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTR4";
                                            break;
                                        case "Amazfit Band 7":
                                            backgroundEmpty.BackgroundColor.h = 368;
                                            backgroundEmpty.BackgroundColor.w = 194;
                                            Watch_Face.WatchFace_Info.DeviceName = "Amazfit_Band_7";
                                            break;
                                        case "GTS 4 mini":
                                            backgroundEmpty.BackgroundColor.h = 384;
                                            backgroundEmpty.BackgroundColor.w = 336;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTS4_mini";
                                            break;
                                        case "Falcon":
                                            backgroundEmpty.BackgroundColor.h = 416;
                                            backgroundEmpty.BackgroundColor.w = 416;
                                            Watch_Face.WatchFace_Info.DeviceName = "Falcon";
                                            break;
                                        case "GTR mini":
                                            backgroundEmpty.BackgroundColor.h = 416;
                                            backgroundEmpty.BackgroundColor.w = 416;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTR_mini";
                                            break;
                                        case "GTS 4":
                                            backgroundEmpty.BackgroundColor.h = 450;
                                            backgroundEmpty.BackgroundColor.w = 390;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTS4";
                                            break;
                                        default:
                                            backgroundEmpty.BackgroundColor.h = 454;
                                            backgroundEmpty.BackgroundColor.w = 454;
                                            Watch_Face.WatchFace_Info.DeviceName = "GTR3";
                                            break;
                                    }
                                    Watch_Face.ScreenNormal.Background = backgroundEmpty;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    string Watch_Face_String = JsonConvert.SerializeObject(Watch_Face, Formatting.Indented, new JsonSerializerSettings
                    {
                        //DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    try
                    {
                        projectName = Regex.Replace(projectName, @"[^\w\.@-]", "-",
                                        RegexOptions.None, TimeSpan.FromSeconds(1.5));
                    }
                    catch (Exception)
                    {
                    }
                    string fullProjectName = Path.Combine(projectPath, projectName + ".json");
                    File.WriteAllText(fullProjectName, Watch_Face_String, Encoding.UTF8);

                    FileName = Path.GetFileName(fullProjectName);
                    FullFileDir = Path.GetDirectoryName(fullProjectName);
                    LoadJson(fullProjectName);
                }
                else MessageBox.Show(Properties.FormStrings.Message_ErrorReadJS, Properties.FormStrings.Message_Error_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                progressBar1.Visible = false;

                //FileName = Path.GetFileName(openFileDialog.FileName);
                FullFileDir = projectPath;
            }
#if !DEBUG
            if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
#endif
        }

        /*private void TgaToPng(string file)
        {
            try
            {
                //string fileNameFull = openFileDialog.FileName;
                string fileNameFull = file;
                string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                string path = Path.GetDirectoryName(fileNameFull);
                //fileName = Path.Combine(path, fileName);
                //using (FileStream stream = new FileStream(fileNameFull, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //{
                //    Image tempImg = Image.FromStream(stream);
                //}
                int RealWidth = -1;
                using (var fileStream = File.OpenRead(fileNameFull))
                {
                    byte[] _streamBuffer;
                    _streamBuffer = new byte[fileStream.Length];
                    fileStream.Read(_streamBuffer, 0, (int)fileStream.Length);

                    Header header = new Header(_streamBuffer);
                    ImageDescription imageDescription = new ImageDescription(_streamBuffer, header.GetImageIDLength());
                    RealWidth = imageDescription.GetRealWidth();
                }

                ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull, ImageMagick.MagickFormat.Tga);
                image.Format = ImageMagick.MagickFormat.Png32;
                if (RealWidth > 0 && RealWidth != image.Width)
                {
                    int height = image.Height;
                    image = (ImageMagick.MagickImage)image.Clone(RealWidth, height);
                }

                ImageMagick.IMagickImage Blue = image.Separate(ImageMagick.Channels.Blue).First();
                ImageMagick.IMagickImage Red = image.Separate(ImageMagick.Channels.Red).First();
                image.Composite(Red, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Blue);
                image.Composite(Blue, ImageMagick.CompositeOperator.Replace, ImageMagick.Channels.Red);

                //image.ColorType = ImageMagick.ColorType.Palette;
                //string newFileName = Path.Combine(path, fileName + ".png");
                //image.Write(targetFile);
                //Bitmap bitmap = image.ToBitmap();
                //panel1.BackgroundImage = bitmap;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine +
                    exp);
            }
        }*/

        /// <summary>Получаем список файлов в папке</summary>
        /// <param name="start_path">Начальная папка для просмотра</param>
        /// <param name="mask">Маска для поиска файлов</param>
        /// <param name="depth">Глубина просмотра подкаталогов</param>
        /// <param name="relative_path">Начальная папка? относительно которой будут возвращатся пути файлов</param>
        private List<string> GetRecursFiles(string start_path, string mask, int depth, string relative_path)
        {
            List<string> listFiles = new List<string>();
            if (depth < 0) return listFiles;
            depth--;
            try
            {
                string[] folders = Directory.GetDirectories(start_path);
                foreach (string folder in folders)
                {
                    //ls.Add("Папка: " + folder);
                    listFiles.AddRange(GetRecursFiles(folder, mask, depth, relative_path));
                }
                string[] files = Directory.GetFiles(start_path, mask);
                foreach (string fileName in files)
                {
                    if (relative_path.Length > 3) listFiles.Add(fileName.Replace(relative_path, ""));
                    else listFiles.Add(fileName);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return listFiles;
        }

        /// <summary>Получаем список файлов в папке</summary>
        /// <param name="start_path">Начальная папка для просмотра</param>
        /// <param name="depth">Глубина просмотра подкаталогов</param>
        /// <param name="relative_path">Начальная папка? относительно которой будут возвращатся пути файлов</param>
        private List<string> GetRecursDirectories(string start_path, int depth, string relative_path)
        {
            List<string> listFiles = new List<string>();
            if (depth < 0) return listFiles;
            depth--;
            try
            {
                string[] folders = Directory.GetDirectories(start_path);
                foreach (string folder in folders)
                {
                    if (relative_path.Length > 3) listFiles.Add(folder.Replace(relative_path, ""));
                    else listFiles.Add(folder);
                    listFiles.AddRange(GetRecursDirectories(folder, depth, relative_path));
                }
                //string[] files = Directory.GetFiles(start_path);
                //foreach (string fileName in files)
                //{
                //    if (relative_path) listFiles.Add(fileName.Replace(start_path, ""));
                //    else listFiles.Add(fileName);
                //}
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return listFiles;
        }

        /// <summary>Рекурсивно удаляем все файлы и подпапки к каталоге</summary>
        public static void DeleteDirectory(string target_dir)
        {
            foreach (string file in Directory.GetFiles(target_dir))
            {
                File.Delete(file);
            }

            foreach (string subDir in Directory.GetDirectories(target_dir))
            {
                DeleteDirectory(subDir);
            }

            Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) is not enough.
            Directory.Delete(target_dir);
        }

        private void uCtrl_AnalogTime_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAnalogTime analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAnalogTime());
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAnalogTime());
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }

            if (analogTime != null)
            {
                if (analogTime.Hour == null) analogTime.Hour = new hmUI_widget_IMG_POINTER();
                if (analogTime.Minute == null) analogTime.Minute = new hmUI_widget_IMG_POINTER();
                if (analogTime.Second == null) analogTime.Second = new hmUI_widget_IMG_POINTER();

                Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Hour")) analogTime.Hour.position = elementOptions["Hour"];
                if (elementOptions.ContainsKey("Minute")) analogTime.Minute.position = elementOptions["Minute"];
                if (elementOptions.ContainsKey("Second")) analogTime.Second.position = elementOptions["Second"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Hours":
                        analogTime.Hour.visible = checkBox.Checked;
                        break;
                    case "checkBox_Minutes":
                        analogTime.Minute.visible = checkBox.Checked;
                        break;
                    case "checkBox_Seconds":
                        analogTime.Second.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_AnalogTime_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_AnalogTime_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAnalogTime analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAnalogTime());
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAnalogTime());
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }

            if (analogTime != null)
            {
                if (analogTime.Hour == null) analogTime.Hour = new hmUI_widget_IMG_POINTER();
                if (analogTime.Minute == null) analogTime.Minute = new hmUI_widget_IMG_POINTER();
                if (analogTime.Second == null) analogTime.Second = new hmUI_widget_IMG_POINTER();

                //Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Hour")) analogTime.Hour.position = elementOptions["Hour"];
                if (elementOptions.ContainsKey("Minute")) analogTime.Minute.position = elementOptions["Minute"];
                if (elementOptions.ContainsKey("Second")) analogTime.Second.position = elementOptions["Second"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_AnalogTime_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementAnalogTime analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    analogTime = (ElementAnalogTime)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                }
            }
            if (analogTime != null)
            {
                analogTime.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_AnalogTimePro_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAnalogTimePro analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAnalogTimePro");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAnalogTimePro());
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAnalogTimePro");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAnalogTimePro());
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }

            if (analogTime != null)
            {
                if (analogTime.Hour == null) analogTime.Hour = new hmUI_widget_IMG_POINTER();
                if (analogTime.Minute == null) analogTime.Minute = new hmUI_widget_IMG_POINTER();
                if (analogTime.Second == null) analogTime.Second = new hmUI_widget_IMG_POINTER();
                if (analogTime.SmoothSecond == null) 
                {
                    analogTime.SmoothSecond = new Smooth_Second();
                    if (radioButton_ScreenIdle.Checked) analogTime.SmoothSecond.type = 2;
                }

                Dictionary<string, int> elementOptions = uCtrl_AnalogTimePro_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Hour")) analogTime.Hour.position = elementOptions["Hour"];
                if (elementOptions.ContainsKey("Minute")) analogTime.Minute.position = elementOptions["Minute"];
                if (elementOptions.ContainsKey("Second")) analogTime.Second.position = elementOptions["Second"];
                if (elementOptions.ContainsKey("SmoothSecond")) analogTime.SmoothSecond.position = elementOptions["SmoothSecond"];
                if (elementOptions.ContainsKey("Format_24hour")) analogTime.Format_24hour_position = elementOptions["Format_24hour"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Hours":
                        analogTime.Hour.visible = checkBox.Checked;
                        break;
                    case "checkBox_Minutes":
                        analogTime.Minute.visible = checkBox.Checked;
                        break;
                    case "checkBox_Seconds":
                        analogTime.Second.visible = checkBox.Checked;
                        break;
                    case "checkBox_SmoothSeconds":
                        analogTime.SmoothSecond.enable = checkBox.Checked;
                        break;
                    case "checkBox_Format_24hour":
                        analogTime.Format_24hour = checkBox.Checked;
                        break;
                }

            }

            uCtrl_AnalogTimePro_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_AnalogTimePro_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAnalogTimePro analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAnalogTimePro");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAnalogTimePro());
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAnalogTimePro");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAnalogTimePro());
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }

            if (analogTime != null)
            {
                if (analogTime.Hour == null) analogTime.Hour = new hmUI_widget_IMG_POINTER();
                if (analogTime.Minute == null) analogTime.Minute = new hmUI_widget_IMG_POINTER();
                if (analogTime.Second == null) analogTime.Second = new hmUI_widget_IMG_POINTER();
                if (analogTime.SmoothSecond == null) analogTime.SmoothSecond = new Smooth_Second();

                //Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Hour")) analogTime.Hour.position = elementOptions["Hour"];
                if (elementOptions.ContainsKey("Minute")) analogTime.Minute.position = elementOptions["Minute"];
                if (elementOptions.ContainsKey("Second")) analogTime.Second.position = elementOptions["Second"];
                if (elementOptions.ContainsKey("SmoothSecond")) analogTime.SmoothSecond.position = elementOptions["SmoothSecond"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_AnalogTimePro_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementAnalogTimePro analogTime = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    analogTime = (ElementAnalogTimePro)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnalogTimePro");
                }
            }
            if (analogTime != null)
            {
                analogTime.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableTimePointer_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementEditablePointers editablePointers = null;
            if (Watch_Face != null && Watch_Face.ElementEditablePointers != null) editablePointers = Watch_Face.ElementEditablePointers;
            
            if (editablePointers != null)
            {
                editablePointers.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_EditableElements_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            EditableElements editableElements = null;
            if (Watch_Face != null && Watch_Face.Editable_Elements != null) editableElements = Watch_Face.Editable_Elements;

            if (editableElements != null)
            {
                editableElements.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateDay_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementDateDay dateDay = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    dateDay = (ElementDateDay)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateDay = (ElementDateDay)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }
            if (dateDay != null)
            {
                dateDay.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateMonth_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementDateMonth dateMonth = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }
            if (dateMonth != null)
            {
                dateMonth.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateYear_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementDateYear dateYear = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    dateYear = (ElementDateYear)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateYear");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateYear = (ElementDateYear)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateYear");
                }
            }
            if (dateYear != null)
            {
                dateYear.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateWeek_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementDateWeek dateWeek = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }
            if (dateWeek != null)
            {
                dateWeek.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        #region SelectChanged

        private void uCtrl_DateDay_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_DateDay_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("DateDay");

            ElementDateDay dateDay = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    dateDay = (ElementDateDay)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateDay = (ElementDateDay)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }
            if (dateDay != null)
            {
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG_NUMBER img_number = null;

                switch (selectElement)
                {
                    case "Number":
                        if (uCtrl_DateDay_Elm.checkBox_Number.Checked)
                        {
                            img_number = dateDay.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, false);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_DateDay_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = dateDay.Pointer;
                            Read_ImgPointer_Options(img_pointer, true);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_DateMonth_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_DateMonth_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("DateMonth");

            ElementDateMonth dateMonth = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }
            if (dateMonth != null)
            {
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_LEVEL img_level = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_DateMonth_Elm.checkBox_Images.Checked)
                        {
                            img_level = dateMonth.Images;
                            Read_ImgLevel_Options(img_level, 12, false);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_DateMonth_Elm.checkBox_Number.Checked)
                        {
                            img_number = dateMonth.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, false);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_DateMonth_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = dateMonth.Pointer;
                            Read_ImgPointer_Options(img_pointer, true);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_DateYear_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            HideAllElemenrOptions();
            ResetHighlightState("DateYear");

            ElementDateYear dateYear = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    dateYear = (ElementDateYear)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateYear");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateYear = (ElementDateYear)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateYear");
                }
            }
            if (dateYear != null)
            {
                string selectedElement = uCtrl_DateMonth_Elm.selectedElement;
                hmUI_widget_IMG_NUMBER img_number = null;

                if (dateYear.Number == null) dateYear.Number = new hmUI_widget_IMG_NUMBER();
                img_number = dateYear.Number;
                Read_ImgNumber_Options(img_number, false, false, "", false, false, true, false);
                uCtrl_Text_Opt.Year = true;
                ShowElemenrOptions("Text");

            }
        }

        private void uCtrl_DateWeek_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_DateWeek_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("DateWeek");

            ElementDateWeek dateWeek = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }
            if (dateWeek != null)
            {
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG_LEVEL img_level = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_DateWeek_Elm.checkBox_Images.Checked)
                        {
                            img_level = dateWeek.Images;
                            Read_ImgLevel_Options(img_level, 7, false);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_DateWeek_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = dateWeek.Pointer;
                            Read_ImgPointer_Options(img_pointer, true);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Shortcuts_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            if (Watch_Face == null) return;
            string selectElement = uCtrl_Shortcuts_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Shortcuts");

            ElementShortcuts shortcuts = Watch_Face.Shortcuts;
            //if (radioButton_ScreenNormal.Checked)
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
            //        Watch_Face.ScreenNormal.Elements != null)
            //    {
            //        //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
            //        shortcuts = (ElementShortcuts)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}
            //else
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
            //        Watch_Face.ScreenAOD.Elements != null)
            //    {
            //        shortcuts = (ElementShortcuts)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}
            if (shortcuts != null)
            {
                hmUI_widget_IMG_CLICK img_click = null;

                switch (selectElement)
                {
                    case "Step":
                        if (uCtrl_Shortcuts_Elm.checkBox_Step.Checked)
                        {
                            img_click = shortcuts.Step;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Heart":
                        if (uCtrl_Shortcuts_Elm.checkBox_Heart.Checked)
                        {
                            img_click = shortcuts.Heart;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "SPO2":
                        if (uCtrl_Shortcuts_Elm.checkBox_SPO2.Checked)
                        {
                            img_click = shortcuts.SPO2;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "PAI":
                        if (uCtrl_Shortcuts_Elm.checkBox_PAI.Checked)
                        {
                            img_click = shortcuts.PAI;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Stress":
                        if (uCtrl_Shortcuts_Elm.checkBox_Stress.Checked)
                        {
                            img_click = shortcuts.Stress;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Weather":
                        if (uCtrl_Shortcuts_Elm.checkBox_Weather.Checked)
                        {
                            img_click = shortcuts.Weather;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Altimeter":
                        if (uCtrl_Shortcuts_Elm.checkBox_Altimeter.Checked)
                        {
                            img_click = shortcuts.Altimeter;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Sunrise":
                        if (uCtrl_Shortcuts_Elm.checkBox_Sunrise.Checked)
                        {
                            img_click = shortcuts.Sunrise;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Alarm":
                        if (uCtrl_Shortcuts_Elm.checkBox_Alarm.Checked)
                        {
                            img_click = shortcuts.Alarm;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Sleep":
                        if (uCtrl_Shortcuts_Elm.checkBox_Sleep.Checked)
                        {
                            img_click = shortcuts.Sleep;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Countdown":
                        if (uCtrl_Shortcuts_Elm.checkBox_Countdown.Checked)
                        {
                            img_click = shortcuts.Countdown;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Stopwatch":
                        if (uCtrl_Shortcuts_Elm.checkBox_Stopwatch.Checked)
                        {
                            img_click = shortcuts.Stopwatch;
                            Read_Shortcuts_Options(img_click);
                            ShowElemenrOptions("Shortcut");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Statuses_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Statuses_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Statuses");

            ElementStatuses statuses = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    statuses = (ElementStatuses)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    statuses = (ElementStatuses)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }
            if (statuses != null)
            {
                hmUI_widget_IMG_STATUS img_status = null;

                switch (selectElement)
                {
                    case "Alarm":
                        if (uCtrl_Statuses_Elm.checkBox_Alarm.Checked)
                        {
                            img_status = statuses.Alarm;
                            Read_Statuses_Options(img_status);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Bluetooth":
                        if (uCtrl_Statuses_Elm.checkBox_Bluetooth.Checked)
                        {
                            img_status = statuses.Bluetooth;
                            Read_Statuses_Options(img_status);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "DND":
                        if (uCtrl_Statuses_Elm.checkBox_DND.Checked)
                        {
                            img_status = statuses.DND;
                            Read_Statuses_Options(img_status);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Lock":
                        if (uCtrl_Statuses_Elm.checkBox_Lock.Checked)
                        {
                            img_status = statuses.Lock;
                            Read_Statuses_Options(img_status);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Animation_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Animation_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Animation");

            ElementAnimation animation = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    animation = (ElementAnimation)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    animation = (ElementAnimation)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }
            if (animation != null)
            {
                hmUI_widget_IMG_ANIM_List frame_animation = null;
                Motion_Animation_List motion_animation = null;
                Rotate_Animation_List rotate_animation = null;

                switch (selectElement)
                {
                    case "FrameAnimation":
                        if (uCtrl_Animation_Elm.checkBox_FrameAnimation.Checked)
                        {
                            frame_animation = animation.Frame_Animation_List;
                            Read_FrameAnimation_Options(frame_animation);
                            ShowElemenrOptions("FrameAnimation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "MotionAnimation":
                        if (uCtrl_Animation_Elm.checkBox_MotionAnimation.Checked)
                        {
                            motion_animation = animation.Motion_Animation_List;
                            Read_MotionAnimation_Options(motion_animation);
                            ShowElemenrOptions("MotionAnimation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "RotateAnimation":
                        if (uCtrl_Animation_Elm.checkBox_RotateAnimation.Checked)
                        {
                            rotate_animation = animation.Rotate_Animation_List;
                            Read_RotateAnimation_Options(rotate_animation);
                            ShowElemenrOptions("RotateAnimation");
                        }
                        else HideAllElemenrOptions();
                        break;


                }

            }
        }

        private void uCtrl_Steps_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Steps_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Steps");

            ElementSteps steps = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    steps = (ElementSteps)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    steps = (ElementSteps)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }
            if (steps != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Steps_Elm.checkBox_Images.Checked)
                        {
                            img_level = steps.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Steps_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = steps.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Steps_Elm.checkBox_Number.Checked)
                        {
                            img_number = steps.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_Steps_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = steps.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_Steps_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = steps.Text_circle;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Target":
                        if (uCtrl_Steps_Elm.checkBox_Number_Target.Checked)
                        {
                            img_number = steps.Number_Target;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation_Target":
                        if (uCtrl_Steps_Elm.checkBox_Text_rotation_Target.Checked)
                        {
                            text_rotation = steps.Text_rotation_Target;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle_Target":
                        if (uCtrl_Steps_Elm.checkBox_Text_circle_Target.Checked)
                        {
                            text_circle = steps.Text_circle_Target;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Steps_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = steps.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_Steps_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = steps.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_Steps_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = steps.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Steps_Elm.checkBox_Icon.Checked)
                        {
                            icon = steps.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Battery_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Battery_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Battery");

            ElementBattery battery = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    battery = (ElementBattery)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    battery = (ElementBattery)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }
            if (battery != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Battery_Elm.checkBox_Images.Checked)
                        {
                            img_level = battery.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Battery_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = battery.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Battery_Elm.checkBox_Number.Checked)
                        {
                            img_number = battery.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_Battery_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = battery.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_Battery_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = battery.Text_circle;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Battery_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = battery.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_Battery_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = battery.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_Battery_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = battery.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Battery_Elm.checkBox_Icon.Checked)
                        {
                            icon = battery.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Heart_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Heart_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Heart");

            ElementHeart heart = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    heart = (ElementHeart)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    heart = (ElementHeart)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }
            if (heart != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Heart_Elm.checkBox_Images.Checked)
                        {
                            img_level = heart.Images;
                            Read_ImgLevel_Options(img_level, 6, false);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Heart_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = heart.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 6, true);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Heart_Elm.checkBox_Number.Checked)
                        {
                            img_number = heart.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            uCtrl_Text_Opt.ImageError = true;
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_Heart_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = heart.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_Heart_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = heart.Text_circle;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Heart_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = heart.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_Heart_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = heart.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_Heart_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = heart.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Heart_Elm.checkBox_Icon.Checked)
                        {
                            icon = heart.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Calories_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Calories_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Calories");

            ElementCalories calories = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    calories = (ElementCalories)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    calories = (ElementCalories)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }
            if (calories != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Calories_Elm.checkBox_Images.Checked)
                        {
                            img_level = calories.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Calories_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = calories.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Calories_Elm.checkBox_Number.Checked)
                        {
                            img_number = calories.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_Calories_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = calories.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_Calories_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = calories.Text_circle;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Target":
                        if (uCtrl_Calories_Elm.checkBox_Number_Target.Checked)
                        {
                            img_number = calories.Number_Target;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation_Target":
                        if (uCtrl_Calories_Elm.checkBox_Text_rotation_Target.Checked)
                        {
                            text_rotation = calories.Text_rotation_Target;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle_Target":
                        if (uCtrl_Calories_Elm.checkBox_Text_circle_Target.Checked)
                        {
                            text_circle = calories.Text_circle_Target;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Calories_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = calories.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_Calories_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = calories.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_Calories_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = calories.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Calories_Elm.checkBox_Icon.Checked)
                        {
                            icon = calories.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_PAI_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_PAI_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("PAI");

            ElementPAI pai = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    pai = (ElementPAI)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    pai = (ElementPAI)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }
            if (pai != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_PAI_Elm.checkBox_Images.Checked)
                        {
                            img_level = pai.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_PAI_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = pai.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_PAI_Elm.checkBox_Number.Checked)
                        {
                            img_number = pai.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Target":
                        if (uCtrl_PAI_Elm.checkBox_Number_Target.Checked)
                        {
                            img_number = pai.Number_Target;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation_Target":
                        if (uCtrl_PAI_Elm.checkBox_Text_rotation_Target.Checked)
                        {
                            text_rotation = pai.Text_rotation_Target;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle_Target":
                        if (uCtrl_PAI_Elm.checkBox_Text_circle_Target.Checked)
                        {
                            text_circle = pai.Text_circle_Target;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_PAI_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = pai.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_PAI_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = pai.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_PAI_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = pai.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_PAI_Elm.checkBox_Icon.Checked)
                        {
                            icon = pai.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Distance_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Distance_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Distance");

            ElementDistance distance = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    distance = (ElementDistance)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    distance = (ElementDistance)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }
            if (distance != null)
            {
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Number":
                        if (uCtrl_Distance_Elm.checkBox_Number.Checked)
                        {
                            img_number = distance.Number;
                            Read_ImgNumber_Options(img_number, true, false, "", false, true, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_Distance_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = distance.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, true, false, false, false, true, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_Distance_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = distance.Text_circle;
                            Read_TextCircle_Options(text_circle, true, false, false, true, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Distance_Elm.checkBox_Icon.Checked)
                        {
                            icon = distance.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Stand_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Stand_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Stand");

            ElementStand stand = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    stand = (ElementStand)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    stand = (ElementStand)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }
            if (stand != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Stand_Elm.checkBox_Images.Checked)
                        {
                            img_level = stand.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Stand_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = stand.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Stand_Elm.checkBox_Number.Checked)
                        {
                            img_number = stand.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_Stand_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = stand.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_Stand_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = stand.Text_circle;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Target":
                        if (uCtrl_Stand_Elm.checkBox_Number_Target.Checked)
                        {
                            img_number = stand.Number_Target;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation_Target":
                        if (uCtrl_Stand_Elm.checkBox_Text_rotation_Target.Checked)
                        {
                            text_rotation = stand.Text_rotation_Target;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle_Target":
                        if (uCtrl_Stand_Elm.checkBox_Text_circle_Target.Checked)
                        {
                            text_circle = stand.Text_circle_Target;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Stand_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = stand.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_Stand_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = stand.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_Stand_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = stand.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Stand_Elm.checkBox_Icon.Checked)
                        {
                            icon = stand.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Activity_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Activity_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Activity");

            ElementActivity activity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    activity = (ElementActivity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    activity = (ElementActivity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }
            if (activity != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Activity_Elm.checkBox_Images.Checked)
                        {
                            img_level = activity.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Activity_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = activity.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Activity_Elm.checkBox_Number.Checked)
                        {
                            img_number = activity.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Target":
                        if (uCtrl_Activity_Elm.checkBox_Number_Target.Checked)
                        {
                            img_number = activity.Number_Target;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Activity_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = activity.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_Activity_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = activity.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_Activity_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = activity.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Activity_Elm.checkBox_Icon.Checked)
                        {
                            icon = activity.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_SpO2_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_SpO2_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("SpO2");

            ElementSpO2 SpO2 = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    SpO2 = (ElementSpO2)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    SpO2 = (ElementSpO2)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            if (SpO2 != null)
            {
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Number":
                        if (uCtrl_SpO2_Elm.checkBox_Number.Checked)
                        {
                            img_number = SpO2.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_SpO2_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = SpO2.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_SpO2_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = SpO2.Text_circle;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_SpO2_Elm.checkBox_Icon.Checked)
                        {
                            icon = SpO2.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }

            /*ResetHighlightState("SpO2");

            ElementSpO2 spo2 = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    spo2 = (ElementSpO2)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    spo2 = (ElementSpO2)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            if (spo2 != null)
            {
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;

                if (spo2.Number == null) spo2.Number = new hmUI_widget_IMG_NUMBER();
                img_number = spo2.Number;
                Read_ImgNumber_Options(img_number, false, false, "", true, false, true, true);
                ShowElemenrOptions("Text");

            }*/
        }

        private void uCtrl_Stress_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Stress_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Stress");

            ElementStress stress = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    stress = (ElementStress)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    stress = (ElementStress)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }
            if (stress != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Stress_Elm.checkBox_Images.Checked)
                        {
                            img_level = stress.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Stress_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = stress.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Stress_Elm.checkBox_Number.Checked)
                        {
                            img_number = stress.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Stress_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = stress.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Stress_Elm.checkBox_Icon.Checked)
                        {
                            icon = stress.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_FatBurning_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_FatBurning_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("FatBurning");

            ElementFatBurning fat_burning = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }
            if (fat_burning != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_NUMBER text_rotation = null;
                Text_Circle text_circle = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                Circle_Scale circle_scale = null;
                Linear_Scale linear_scale = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_FatBurning_Elm.checkBox_Images.Checked)
                        {
                            img_level = fat_burning.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_FatBurning_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = fat_burning.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_FatBurning_Elm.checkBox_Number.Checked)
                        {
                            img_number = fat_burning.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation":
                        if (uCtrl_FatBurning_Elm.checkBox_Text_rotation.Checked)
                        {
                            text_rotation = fat_burning.Text_rotation;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle":
                        if (uCtrl_FatBurning_Elm.checkBox_Text_circle.Checked)
                        {
                            text_circle = fat_burning.Text_circle;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Target":
                        if (uCtrl_FatBurning_Elm.checkBox_Number_Target.Checked)
                        {
                            img_number = fat_burning.Number_Target;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_rotation_Target":
                        if (uCtrl_FatBurning_Elm.checkBox_Text_rotation_Target.Checked)
                        {
                            text_rotation = fat_burning.Text_rotation_Target;
                            Read_ImgNumber_Rotate_Options(text_rotation, false, false, false, false, false, true);
                            ShowElemenrOptions("Text_rotation");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Text_circle_Target":
                        if (uCtrl_FatBurning_Elm.checkBox_Text_circle_Target.Checked)
                        {
                            text_circle = fat_burning.Text_circle_Target;
                            Read_TextCircle_Options(text_circle, false, false, false, false, true);
                            ShowElemenrOptions("Text_circle");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_FatBurning_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = fat_burning.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Circle_Scale":
                        if (uCtrl_FatBurning_Elm.checkBox_Circle_Scale.Checked)
                        {
                            circle_scale = fat_burning.Circle_Scale;
                            Read_CircleScale_Options(circle_scale);
                            ShowElemenrOptions("Circle_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Linear_Scale":
                        if (uCtrl_FatBurning_Elm.checkBox_Linear_Scale.Checked)
                        {
                            linear_scale = fat_burning.Linear_Scale;
                            Read_LinearScale_Options(linear_scale);
                            ShowElemenrOptions("Linear_Scale");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_FatBurning_Elm.checkBox_Icon.Checked)
                        {
                            icon = fat_burning.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }



        private void uCtrl_Weather_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Weather_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Weather");

            ElementWeather weather = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    weather = (ElementWeather)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    weather = (ElementWeather)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }
            if (weather != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_TEXT text = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Weather_Elm.checkBox_Images.Checked)
                        {
                            img_level = weather.Images;
                            Read_ImgLevel_Options(img_level, 29, false);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Weather_Elm.checkBox_Number.Checked)
                        {
                            img_number = weather.Number;
                            Read_ImgNumberWeather_Options(img_number, false, "", true, false);
                            ShowElemenrOptions("Text_Weather");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Min":
                        if (uCtrl_Weather_Elm.checkBox_Number_Min.Checked)
                        {
                            img_number = weather.Number_Min;
                            Read_ImgNumberWeather_Options(img_number, false, "", true, false);
                            ShowElemenrOptions("Text_Weather");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number_Max":
                        if (uCtrl_Weather_Elm.checkBox_Number_Max.Checked)
                        {
                            img_number = weather.Number_Max;
                            Read_ImgNumberWeather_Options(img_number, false, "", true, false);
                            ShowElemenrOptions("Text_Weather");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "CityName":
                        if (uCtrl_Weather_Elm.checkBox_Text_CityName.Checked)
                        {
                            text = weather.City_Name;
                            Read_Text_Options(text);
                            ShowElemenrOptions("SystemFont");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Weather_Elm.checkBox_Icon.Checked)
                        {
                            icon = weather.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_UVIndex_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_UVIndex_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("UVIndex");

            ElementUVIndex uv_index = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    uv_index = (ElementUVIndex)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    uv_index = (ElementUVIndex)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }
            if (uv_index != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_UVIndex_Elm.checkBox_Images.Checked)
                        {
                            img_level = uv_index.Images;
                            Read_ImgLevel_Options(img_level, 5, false);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_UVIndex_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = uv_index.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 5, true);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_UVIndex_Elm.checkBox_Number.Checked)
                        {
                            img_number = uv_index.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", true, false, false, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_UVIndex_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = uv_index.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_UVIndex_Elm.checkBox_Icon.Checked)
                        {
                            icon = uv_index.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Humidity_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Humidity_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Humidity");

            ElementHumidity humidity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    humidity = (ElementHumidity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    humidity = (ElementHumidity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }
            if (humidity != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Humidity_Elm.checkBox_Images.Checked)
                        {
                            img_level = humidity.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Humidity_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = humidity.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Humidity_Elm.checkBox_Number.Checked)
                        {
                            img_number = humidity.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, false, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Humidity_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = humidity.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Humidity_Elm.checkBox_Icon.Checked)
                        {
                            icon = humidity.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Altimeter_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Altimeter_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Altimeter");

            ElementAltimeter altimeter = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    altimeter = (ElementAltimeter)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    altimeter = (ElementAltimeter)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }
            if (altimeter != null)
            {
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Number":
                        if (uCtrl_Altimeter_Elm.checkBox_Number.Checked)
                        {
                            img_number = altimeter.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Altimeter_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = altimeter.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Altimeter_Elm.checkBox_Icon.Checked)
                        {
                            icon = altimeter.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Sunrise_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Sunrise_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Sunrise");

            ElementSunrise sunrise = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    sunrise = (ElementSunrise)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    sunrise = (ElementSunrise)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }
            if (sunrise != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Sunrise_Elm.checkBox_Images.Checked)
                        {
                            img_level = sunrise.Images;
                            Read_ImgLevel_Options(img_level, 2, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Sunrise_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = sunrise.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 2, true);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Sunrise":
                        if (uCtrl_Sunrise_Elm.checkBox_Sunrise.Checked)
                        {
                            img_number = sunrise.Sunrise;
                            Read_ImgNumber_Options(img_number, false, false, "", true, true, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Sunset":
                        if (uCtrl_Sunrise_Elm.checkBox_Sunset.Checked)
                        {
                            img_number = sunrise.Sunset;
                            Read_ImgNumber_Options(img_number, false, false, "", true, true, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Sunset_Sunrise":
                        if (uCtrl_Sunrise_Elm.checkBox_Sunset_Sunrise.Checked)
                        {
                            img_number = sunrise.Sunset_Sunrise;
                            Read_ImgNumber_Options(img_number, false, false, "", true, true, false, true, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Sunrise_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = sunrise.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Sunrise_Elm.checkBox_Icon.Checked)
                        {
                            icon = sunrise.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Wind_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_Wind_Elm.selectedElement;
            if (selectElement.Length == 0) HideAllElemenrOptions();
            ResetHighlightState("Wind");

            ElementWind wind = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    wind = (ElementWind)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    wind = (ElementWind)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }
            if (wind != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;
                hmUI_widget_IMG_PROGRESS img_prorgess = null;
                hmUI_widget_IMG_NUMBER img_number = null;
                hmUI_widget_IMG_POINTER img_pointer = null;
                hmUI_widget_IMG icon = null;

                switch (selectElement)
                {
                    case "Images":
                        if (uCtrl_Wind_Elm.checkBox_Images.Checked)
                        {
                            img_level = wind.Images;
                            Read_ImgLevel_Options(img_level, 10, true);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Segments":
                        if (uCtrl_Wind_Elm.checkBox_Segments.Checked)
                        {
                            img_prorgess = wind.Segments;
                            Read_ImgProrgess_Options(img_prorgess, 10, false);
                            ShowElemenrOptions("Segments");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Number":
                        if (uCtrl_Wind_Elm.checkBox_Number.Checked)
                        {
                            img_number = wind.Number;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, false, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Pointer":
                        if (uCtrl_Wind_Elm.checkBox_Pointer.Checked)
                        {
                            img_pointer = wind.Pointer;
                            Read_ImgPointer_Options(img_pointer, false);
                            ShowElemenrOptions("Pointer");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Direction":
                        if (uCtrl_Wind_Elm.checkBox_Direction.Checked)
                        {
                            img_level = wind.Direction;
                            Read_ImgLevel_Options(img_level, 8, false);
                            ShowElemenrOptions("Images");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Icon":
                        if (uCtrl_Wind_Elm.checkBox_Icon.Checked)
                        {
                            icon = wind.Icon;
                            Read_Icon_Options(icon);
                            ShowElemenrOptions("Icon");
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }
        }

        private void uCtrl_Moon_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("Moon");

            ElementMoon moon = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
                    moon = (ElementMoon)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementMoon");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    moon = (ElementMoon)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementMoon");
                }
            }
            if (moon != null)
            {
                hmUI_widget_IMG_LEVEL img_level = null;

                if (moon.Images == null) moon.Images = new hmUI_widget_IMG_LEVEL();
                img_level = moon.Images;
                Read_ImgLevel_Options(img_level, 30, true);
                ShowElemenrOptions("Images");

            }
        }

        private void uCtrl_Image_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("Image");

            ElementImage image = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    image = (ElementImage)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementImage");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    image = (ElementImage)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementImage");
                }
            }
            if (image != null)
            {
                hmUI_widget_IMG icon = null;

                icon = image.Icon;
                Read_Icon_Options(icon);
                ShowElemenrOptions("Icon");

            }
        }

        private void uCtrl_DisconnectAlert_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("DisconnectAlert");

            DisconnectAlert disconnectAlert = null;
            if (Watch_Face != null && Watch_Face.DisconnectAlert != null) disconnectAlert = Watch_Face.DisconnectAlert;

            if (disconnectAlert != null)
            {
                Read_DisconnectAlert_Options(disconnectAlert);
                ShowElemenrOptions("DisconnectAlert");

            }
        }

        private void uCtrl_RepeatingAlert_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("RepeatingAlert");

            RepeatAlert repeatingAlert = null;
            if (Watch_Face != null && Watch_Face.RepeatAlert != null) repeatingAlert = Watch_Face.RepeatAlert;

            if (repeatingAlert != null)
            {
                Read_RepeatingAlert_Options(repeatingAlert);
                ShowElemenrOptions("RepeatingAlert");

            }
        }

        private void uCtrl_TopImage_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            ResetHighlightState("TopImage");

            TopImage topImage = null;
            if (Watch_Face != null && Watch_Face.TopImage != null) topImage = Watch_Face.TopImage;

            if (topImage != null)
            {
                hmUI_widget_IMG icon = null;

                icon = topImage.Icon;
                Read_Icon_Options(icon);
                ShowElemenrOptions("Icon");

            }
        }

        #endregion

        private void uCtrl_DateDay_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDateDay dateDay = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDateDay");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDateDay());
                    dateDay = (ElementDateDay)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDateDay");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDateDay());
                    dateDay = (ElementDateDay)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }

            if (dateDay != null)
            {
                if (dateDay.Number == null) dateDay.Number = new hmUI_widget_IMG_NUMBER();
                if (dateDay.Pointer == null) dateDay.Pointer = new hmUI_widget_IMG_POINTER();

                Dictionary<string, int> elementOptions = uCtrl_DateDay_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Number")) dateDay.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) dateDay.Pointer.position = elementOptions["Pointer"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Pointer":
                        dateDay.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        dateDay.Number.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_DateDay_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateMonth_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDateMonth dateMonth = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDateMonth");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDateMonth());
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDateMonth");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDateMonth());
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }

            if (dateMonth != null)
            {
                if (dateMonth.Number == null) dateMonth.Number = new hmUI_widget_IMG_NUMBER();
                if (dateMonth.Pointer == null) dateMonth.Pointer = new hmUI_widget_IMG_POINTER();
                if (dateMonth.Images == null) dateMonth.Images = new hmUI_widget_IMG_LEVEL();

                Dictionary<string, int> elementOptions = uCtrl_DateMonth_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Number")) dateMonth.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) dateMonth.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Images")) dateMonth.Images.position = elementOptions["Images"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Pointer":
                        dateMonth.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        dateMonth.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Images":
                        dateMonth.Images.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_DateMonth_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateWeek_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDateWeek dateWeek = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDateWeek");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDateWeek());
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDateWeek");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDateWeek());
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }

            if (dateWeek != null)
            {
                if (dateWeek.Pointer == null) dateWeek.Pointer = new hmUI_widget_IMG_POINTER();
                if (dateWeek.Images == null) dateWeek.Images = new hmUI_widget_IMG_LEVEL();

                Dictionary<string, int> elementOptions = uCtrl_DateWeek_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Pointer")) dateWeek.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Images")) dateWeek.Images.position = elementOptions["Images"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Pointer":
                        dateWeek.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Images":
                        dateWeek.Images.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_DateWeek_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateDay_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDateDay dateDay = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDateDay");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDateDay());
                    dateDay = (ElementDateDay)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDateDay");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDateDay());
                    dateDay = (ElementDateDay)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateDay");
                }
            }

            if (dateDay != null)
            {
                if (dateDay.Number == null) dateDay.Number = new hmUI_widget_IMG_NUMBER();
                if (dateDay.Pointer == null) dateDay.Pointer = new hmUI_widget_IMG_POINTER();

                //Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Number")) dateDay.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) dateDay.Pointer.position = elementOptions["Pointer"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateMonth_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDateMonth dateMonth = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDateMonth");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDateMonth());
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDateMonth");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDateMonth());
                    dateMonth = (ElementDateMonth)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateMonth");
                }
            }

            if (dateMonth != null)
            {
                if (dateMonth.Number == null) dateMonth.Number = new hmUI_widget_IMG_NUMBER();
                if (dateMonth.Pointer == null) dateMonth.Pointer = new hmUI_widget_IMG_POINTER();
                if (dateMonth.Images == null) dateMonth.Images = new hmUI_widget_IMG_LEVEL();

                //Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Number")) dateMonth.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) dateMonth.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Images")) dateMonth.Images.position = elementOptions["Images"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DateWeek_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDateWeek dateWeek = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDateWeek");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDateWeek());
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDateWeek");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDateWeek());
                    dateWeek = (ElementDateWeek)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDateWeek");
                }
            }

            if (dateWeek != null)
            {
                if (dateWeek.Pointer == null) dateWeek.Pointer = new hmUI_widget_IMG_POINTER();
                if (dateWeek.Images == null) dateWeek.Images = new hmUI_widget_IMG_LEVEL();

                //Dictionary<string, int> elementOptions = uCtrl_AnalogTime_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Pointer")) dateWeek.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Images")) dateWeek.Images.position = elementOptions["Images"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void button_CopyAOD_Click(object sender, EventArgs e)
        {
            PreviewView = false;
            if (Watch_Face != null && Watch_Face.ScreenNormal != null) 
            {
                Watch_Face.ScreenAOD = new ScreenAOD();
                Background background = Watch_Face.ScreenNormal.Background;
                List<object> elements = Watch_Face.ScreenNormal.Elements;
                Watch_Face.ScreenAOD.Background = (Background)background.Clone();
                if (Watch_Face.ScreenNormal.Background != null && Watch_Face.ScreenNormal.Background.Editable_Background != null)
                    Watch_Face.ScreenNormal.Background.Editable_Background.AOD_show = true;
                //Watch_Face.ScreenAOD.Elements = elements;
                Watch_Face.ScreenAOD.Elements = new List<object>();
                foreach (Object element in elements)
                {
                    string type = element.GetType().Name;
                    switch (type)
                    {
                        #region ElementDigitalTime
                        case "ElementDigitalTime":
                            ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementDigitalTime)DigitalTime.Clone());
                            break;
                        #endregion

                        #region ElementAnalogTime
                        case "ElementAnalogTime":
                            ElementAnalogTime AnalogTime = (ElementAnalogTime)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementAnalogTime)AnalogTime.Clone());
                            break;
                        #endregion

                        #region ElementAnalogTimePro
                        case "ElementAnalogTimePro":
                            ElementAnalogTimePro AnalogTimePro = (ElementAnalogTimePro)element;
                            ElementAnalogTimePro AnalogTimePro_temp = (ElementAnalogTimePro)AnalogTimePro.Clone();
                            if (AnalogTimePro_temp.SmoothSecond != null) AnalogTimePro_temp.SmoothSecond.type = 2;
                            Watch_Face.ScreenAOD.Elements.Add(AnalogTimePro_temp);
                            break;
                        #endregion

                        #region ElementDateDay
                        case "ElementDateDay":
                            ElementDateDay DateDay = (ElementDateDay)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementDateDay)DateDay.Clone());
                            break;
                        #endregion

                        #region ElementDateMonth
                        case "ElementDateMonth":
                            ElementDateMonth DateMonth = (ElementDateMonth)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementDateMonth)DateMonth.Clone());
                            break;
                        #endregion

                        #region ElementDateYear
                        case "ElementDateYear":
                            ElementDateYear DateYear = (ElementDateYear)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementDateYear)DateYear.Clone());
                            break;
                        #endregion

                        #region ElementDateWeek
                        case "ElementDateWeek":
                            ElementDateWeek DateWeek = (ElementDateWeek)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementDateWeek)DateWeek.Clone());
                            break;
                        #endregion

                        #region ElementShortcuts
                        //case "ElementShortcuts":
                        //    ElementShortcuts shortcutsElement = (ElementShortcuts)element;
                        //    Watch_Face.ScreenAOD.Elements.Add((ElementShortcuts)shortcutsElement.Clone());
                        //    break;
                        #endregion

                        #region ElementStatuses
                        case "ElementStatuses":
                            ElementStatuses statusesElement = (ElementStatuses)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementStatuses)statusesElement.Clone());
                            break;
                        #endregion

                        #region ElementSteps
                        case "ElementSteps":
                            ElementSteps stepElement = (ElementSteps)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementSteps)stepElement.Clone());
                            break;
                        #endregion

                        #region ElementBattery
                        case "ElementBattery":
                            ElementBattery batteryElement = (ElementBattery)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementBattery)batteryElement.Clone());
                            break;
                        #endregion

                        #region ElementCalories
                        case "ElementCalories":
                            ElementCalories caloriesElement = (ElementCalories)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementCalories)caloriesElement.Clone());
                            break;
                        #endregion

                        #region ElementHeart
                        case "ElementHeart":
                            ElementHeart heartElement = (ElementHeart)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementHeart)heartElement.Clone());
                            break;
                        #endregion

                        #region ElementPAI
                        case "ElementPAI":
                            ElementPAI paiElement = (ElementPAI)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementPAI)paiElement.Clone());
                            break;
                        #endregion

                        #region ElementDistance
                        case "ElementDistance":
                            ElementDistance distanceElement = (ElementDistance)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementDistance)distanceElement.Clone());
                            break;
                        #endregion

                        #region ElementStand
                        case "ElementStand":
                            ElementStand standElement = (ElementStand)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementStand)standElement.Clone());
                            break;
                        #endregion

                        #region ElementActivity
                        case "ElementActivity":
                            ElementActivity activityElement = (ElementActivity)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementActivity)activityElement.Clone());
                            break;
                        #endregion

                        #region ElementSpO2
                        case "ElementSpO2":
                            ElementSpO2 spo2Element = (ElementSpO2)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementSpO2)spo2Element.Clone());
                            break;
                        #endregion

                        #region ElementStress
                        case "ElementStress":
                            ElementStress stressElement = (ElementStress)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementStress)stressElement.Clone());
                            break;
                        #endregion

                        #region ElementFatBurning
                        case "ElementFatBurning":
                            ElementFatBurning fatBurninElement = (ElementFatBurning)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementFatBurning)fatBurninElement.Clone());
                            break;
                        #endregion



                        #region ElementWeather
                        case "ElementWeather":
                            ElementWeather weatherElement = (ElementWeather)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementWeather)weatherElement.Clone());
                            break;
                        #endregion

                        #region ElementUVIndex
                        case "ElementUVIndex":
                            ElementUVIndex uv_indexElement = (ElementUVIndex)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementUVIndex)uv_indexElement.Clone());
                            break;
                        #endregion

                        #region ElementHumidity
                        case "ElementHumidity":
                            ElementHumidity humidityElement = (ElementHumidity)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementHumidity)humidityElement.Clone());
                            break;
                        #endregion

                        #region ElementAltimeter
                        case "ElementAltimeter":
                            ElementAltimeter altimeterElement = (ElementAltimeter)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementAltimeter)altimeterElement.Clone());
                            break;
                        #endregion

                        #region ElementSunrise
                        case "ElementSunrise":
                            ElementSunrise sunriseElement = (ElementSunrise)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementSunrise)sunriseElement.Clone());
                            break;
                        #endregion

                        #region ElementWind
                        case "ElementWind":
                            ElementWind windElement = (ElementWind)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementWind)windElement.Clone());
                            break;
                        #endregion

                        #region ElementMoon
                        case "ElementMoon":
                            ElementMoon moonElement = (ElementMoon)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementMoon)moonElement.Clone());
                            break;
                        #endregion

                        #region ElementImage
                        case "ElementImage":
                            ElementImage imageElement = (ElementImage)element;
                            Watch_Face.ScreenAOD.Elements.Add((ElementImage)imageElement.Clone());
                            break;
                        #endregion
                    }
                }

            }
            if (Watch_Face != null && Watch_Face.Editable_Elements != null) Watch_Face.Editable_Elements.AOD_show = true;
            if (Watch_Face != null && Watch_Face.TopImage != null) Watch_Face.TopImage.showInAOD = true;
            //if (Watch_Face != null && Watch_Face.ElementEditablePointers != null) Watch_Face.ElementEditablePointers.AOD_show = true;
            ShowElemetsWatchFace();
            JSON_Modified = true;
            PreviewView = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Shortcuts_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementShortcuts shortcuts = Watch_Face.Shortcuts;
            //if (radioButton_ScreenNormal.Checked)
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
            //        Watch_Face.ScreenNormal.Elements != null)
            //    {
            //        bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementShortcuts");
            //        //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
            //        if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementShortcuts());
            //        shortcuts = (ElementShortcuts)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}
            //else
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
            //        Watch_Face.ScreenAOD.Elements != null)
            //    {
            //        bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementShortcuts");
            //        //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
            //        if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementShortcuts());
            //        shortcuts = (ElementShortcuts)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}

            if (shortcuts != null)
            {
                if (shortcuts.Step == null) shortcuts.Step = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Heart == null) shortcuts.Heart = new hmUI_widget_IMG_CLICK();
                if (shortcuts.SPO2 == null) shortcuts.SPO2 = new hmUI_widget_IMG_CLICK();
                if (shortcuts.PAI == null) shortcuts.PAI = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Stress == null) shortcuts.Stress = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Weather == null) shortcuts.Weather = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Altimeter == null) shortcuts.Altimeter = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Sunrise == null) shortcuts.Sunrise = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Alarm == null) shortcuts.Alarm = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Sleep == null) shortcuts.Sleep = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Countdown == null) shortcuts.Countdown = new hmUI_widget_IMG_CLICK();
                if (shortcuts.Stopwatch == null) shortcuts.Stopwatch = new hmUI_widget_IMG_CLICK();

                if (elementOptions.ContainsKey("Step")) shortcuts.Step.position = elementOptions["Step"];
                if (elementOptions.ContainsKey("Heart")) shortcuts.Heart.position = elementOptions["Heart"];
                if (elementOptions.ContainsKey("SPO2")) shortcuts.SPO2.position = elementOptions["SPO2"];
                if (elementOptions.ContainsKey("PAI")) shortcuts.PAI.position = elementOptions["PAI"];
                if (elementOptions.ContainsKey("Stress")) shortcuts.Stress.position = elementOptions["Stress"];
                if (elementOptions.ContainsKey("Weather")) shortcuts.Weather.position = elementOptions["Weather"];
                if (elementOptions.ContainsKey("Altimeter")) shortcuts.Altimeter.position = elementOptions["Altimeter"];
                if (elementOptions.ContainsKey("Sunrise")) shortcuts.Sunrise.position = elementOptions["Sunrise"];
                if (elementOptions.ContainsKey("Alarm")) shortcuts.Alarm.position = elementOptions["Alarm"];
                if (elementOptions.ContainsKey("Sleep")) shortcuts.Sleep.position = elementOptions["Sleep"];
                if (elementOptions.ContainsKey("Countdown")) shortcuts.Countdown.position = elementOptions["Countdown"];
                if (elementOptions.ContainsKey("Stopwatch")) shortcuts.Stopwatch.position = elementOptions["Stopwatch"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Statuses_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementStatuses statuses = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementStatuses");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementStatuses());
                    statuses = (ElementStatuses)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementStatuses");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementStatuses());
                    statuses = (ElementStatuses)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }

            if (statuses != null)
            {
                if (statuses.Alarm == null) statuses.Alarm = new hmUI_widget_IMG_STATUS();
                if (statuses.Bluetooth == null) statuses.Bluetooth = new hmUI_widget_IMG_STATUS();
                if (statuses.DND == null) statuses.DND = new hmUI_widget_IMG_STATUS();
                if (statuses.Lock == null) statuses.Lock = new hmUI_widget_IMG_STATUS();

                if (elementOptions.ContainsKey("Alarm")) statuses.Alarm.position = elementOptions["Alarm"];
                if (elementOptions.ContainsKey("Bluetooth")) statuses.Bluetooth.position = elementOptions["Bluetooth"];
                if (elementOptions.ContainsKey("DND")) statuses.DND.position = elementOptions["DND"];
                if (elementOptions.ContainsKey("Lock")) statuses.Lock.position = elementOptions["Lock"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAnimation animation = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAnimation");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAnimation());
                    animation = (ElementAnimation)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAnimation");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAnimation());
                    animation = (ElementAnimation)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }

            if (animation != null)
            {
                if (animation.Frame_Animation_List == null) animation.Frame_Animation_List = new hmUI_widget_IMG_ANIM_List();
                if (animation.Motion_Animation_List == null) animation.Motion_Animation_List = new Motion_Animation_List();
                if (animation.Rotate_Animation_List == null) animation.Rotate_Animation_List = new Rotate_Animation_List();

                if (elementOptions.ContainsKey("FrameAnimation")) animation.Frame_Animation_List.position = elementOptions["FrameAnimation"];
                if (elementOptions.ContainsKey("MotionAnimation")) animation.Motion_Animation_List.position = elementOptions["MotionAnimation"];
                if (elementOptions.ContainsKey("RotateAnimation")) animation.Rotate_Animation_List.position = elementOptions["RotateAnimation"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }


        #region OptionsMoved
        private void uCtrl_Steps_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementSteps steps = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementSteps");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementSteps());
                    steps = (ElementSteps)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementSteps");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementSteps());
                    steps = (ElementSteps)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }

            if (steps != null)
            {
                if (steps.Images == null) steps.Images = new hmUI_widget_IMG_LEVEL();
                if (steps.Segments == null) steps.Segments = new hmUI_widget_IMG_PROGRESS();
                if (steps.Number == null) steps.Number = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_rotation == null) steps.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_circle == null) steps.Text_circle = new Text_Circle();
                if (steps.Number_Target == null) steps.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_rotation_Target == null) steps.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_circle_Target == null) steps.Text_circle_Target = new Text_Circle();
                if (steps.Pointer == null) steps.Pointer = new hmUI_widget_IMG_POINTER();
                if (steps.Circle_Scale == null) steps.Circle_Scale = new Circle_Scale();
                if (steps.Linear_Scale == null) steps.Linear_Scale = new Linear_Scale();
                if (steps.Icon == null) steps.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) steps.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) steps.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) steps.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) steps.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) steps.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) steps.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) steps.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) steps.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) steps.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) steps.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) steps.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) steps.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Battery_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementBattery battery = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementBattery");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementBattery());
                    battery = (ElementBattery)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementBattery");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementBattery());
                    battery = (ElementBattery)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }

            if (battery != null)
            {
                if (battery.Images == null) battery.Images = new hmUI_widget_IMG_LEVEL();
                if (battery.Segments == null) battery.Segments = new hmUI_widget_IMG_PROGRESS();
                if (battery.Number == null) battery.Number = new hmUI_widget_IMG_NUMBER();
                if (battery.Text_rotation == null) battery.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (battery.Text_circle == null) battery.Text_circle = new Text_Circle();
                if (battery.Pointer == null) battery.Pointer = new hmUI_widget_IMG_POINTER();
                if (battery.Circle_Scale == null) battery.Circle_Scale = new Circle_Scale();
                if (battery.Linear_Scale == null) battery.Linear_Scale = new Linear_Scale();
                if (battery.Icon == null) battery.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) battery.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) battery.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) battery.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) battery.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) battery.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Pointer")) battery.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) battery.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) battery.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) battery.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Heart_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementHeart heart = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementHeart");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementHeart());
                    heart = (ElementHeart)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementHeart");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementHeart());
                    heart = (ElementHeart)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }

            if (heart != null)
            {
                if (heart.Images == null) heart.Images = new hmUI_widget_IMG_LEVEL();
                if (heart.Segments == null) heart.Segments = new hmUI_widget_IMG_PROGRESS();
                if (heart.Number == null) heart.Number = new hmUI_widget_IMG_NUMBER();
                if (heart.Text_rotation == null) heart.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (heart.Text_circle == null) heart.Text_circle = new Text_Circle();
                if (heart.Pointer == null) heart.Pointer = new hmUI_widget_IMG_POINTER();
                if (heart.Circle_Scale == null) heart.Circle_Scale = new Circle_Scale();
                if (heart.Linear_Scale == null) heart.Linear_Scale = new Linear_Scale();
                if (heart.Icon == null) heart.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) heart.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) heart.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) heart.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) heart.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) heart.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Pointer")) heart.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) heart.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) heart.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) heart.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Calories_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementCalories calories = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementCalories");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementCalories());
                    calories = (ElementCalories)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementCalories");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementCalories());
                    calories = (ElementCalories)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }

            if (calories != null)
            {
                if (calories.Images == null) calories.Images = new hmUI_widget_IMG_LEVEL();
                if (calories.Segments == null) calories.Segments = new hmUI_widget_IMG_PROGRESS();
                if (calories.Number == null) calories.Number = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_rotation == null) calories.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_circle == null) calories.Text_circle = new Text_Circle();
                if (calories.Number_Target == null) calories.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_rotation_Target == null) calories.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_circle_Target == null) calories.Text_circle_Target = new Text_Circle();
                if (calories.Pointer == null) calories.Pointer = new hmUI_widget_IMG_POINTER();
                if (calories.Circle_Scale == null) calories.Circle_Scale = new Circle_Scale();
                if (calories.Linear_Scale == null) calories.Linear_Scale = new Linear_Scale();
                if (calories.Icon == null) calories.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) calories.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) calories.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) calories.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) calories.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) calories.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) calories.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) calories.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) calories.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) calories.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) calories.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) calories.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) calories.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_PAI_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementPAI pai = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementPAI");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementPAI());
                    pai = (ElementPAI)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementPAI");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementPAI());
                    pai = (ElementPAI)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }

            if (pai != null)
            {
                if (pai.Images == null) pai.Images = new hmUI_widget_IMG_LEVEL();
                if (pai.Segments == null) pai.Segments = new hmUI_widget_IMG_PROGRESS();
                if (pai.Number == null) pai.Number = new hmUI_widget_IMG_NUMBER();
                if (pai.Number_Target == null) pai.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (pai.Text_rotation_Target == null) pai.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (pai.Text_circle_Target == null) pai.Text_circle_Target = new Text_Circle();
                if (pai.Pointer == null) pai.Pointer = new hmUI_widget_IMG_POINTER();
                if (pai.Circle_Scale == null) pai.Circle_Scale = new Circle_Scale();
                if (pai.Linear_Scale == null) pai.Linear_Scale = new Linear_Scale();
                if (pai.Icon == null) pai.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) pai.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) pai.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) pai.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Number_Target")) pai.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) pai.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) pai.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) pai.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) pai.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) pai.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) pai.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Distance_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDistance distance = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDistance");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDistance());
                    distance = (ElementDistance)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDistance");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDistance());
                    distance = (ElementDistance)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }

            if (distance != null)
            {
                if (distance.Number == null) distance.Number = new hmUI_widget_IMG_NUMBER();
                if (distance.Text_rotation == null) distance.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (distance.Text_circle == null) distance.Text_circle = new Text_Circle();
                if (distance.Icon == null) distance.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Number")) distance.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) distance.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) distance.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Icon")) distance.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Stand_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementStand stand = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementStand");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementStand());
                    stand = (ElementStand)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementStand");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementStand());
                    stand = (ElementStand)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }

            if (stand != null)
            {
                if (stand.Images == null) stand.Images = new hmUI_widget_IMG_LEVEL();
                if (stand.Segments == null) stand.Segments = new hmUI_widget_IMG_PROGRESS();
                if (stand.Number == null) stand.Number = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_rotation == null) stand.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_circle == null) stand.Text_circle = new Text_Circle();
                if (stand.Number_Target == null) stand.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_rotation_Target == null) stand.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_circle_Target == null) stand.Text_circle_Target = new Text_Circle();
                if (stand.Pointer == null) stand.Pointer = new hmUI_widget_IMG_POINTER();
                if (stand.Circle_Scale == null) stand.Circle_Scale = new Circle_Scale();
                if (stand.Linear_Scale == null) stand.Linear_Scale = new Linear_Scale();
                if (stand.Icon == null) stand.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) stand.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) stand.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) stand.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) stand.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) stand.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) stand.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) stand.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) stand.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) stand.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) stand.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) stand.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) stand.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Activity_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementActivity activity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementActivity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementActivity());
                    activity = (ElementActivity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementActivity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementActivity());
                    activity = (ElementActivity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }

            if (activity != null)
            {
                if (activity.Images == null) activity.Images = new hmUI_widget_IMG_LEVEL();
                if (activity.Segments == null) activity.Segments = new hmUI_widget_IMG_PROGRESS();
                if (activity.Number == null) activity.Number = new hmUI_widget_IMG_NUMBER();
                if (activity.Number_Target == null) activity.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (activity.Pointer == null) activity.Pointer = new hmUI_widget_IMG_POINTER();
                if (activity.Circle_Scale == null) activity.Circle_Scale = new Circle_Scale();
                if (activity.Linear_Scale == null) activity.Linear_Scale = new Linear_Scale();
                if (activity.Icon == null) activity.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) activity.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) activity.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) activity.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Number_Target")) activity.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Pointer")) activity.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) activity.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) activity.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) activity.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_SpO2_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementSpO2 SpO2 = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementSpO2");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementSpO2());
                    SpO2 = (ElementSpO2)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementSpO2");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementSpO2());
                    SpO2 = (ElementSpO2)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }

            if (SpO2 != null)
            {
                if (SpO2.Number == null) SpO2.Number = new hmUI_widget_IMG_NUMBER();
                if (SpO2.Text_rotation == null) SpO2.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (SpO2.Text_circle == null) SpO2.Text_circle = new Text_Circle();
                if (SpO2.Icon == null) SpO2.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Number")) SpO2.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) SpO2.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) SpO2.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Icon")) SpO2.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Stress_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementStress stress = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementStress");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementStress());
                    stress = (ElementStress)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementStress");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementStress());
                    stress = (ElementStress)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }

            if (stress != null)
            {
                if (stress.Images == null) stress.Images = new hmUI_widget_IMG_LEVEL();
                if (stress.Segments == null) stress.Segments = new hmUI_widget_IMG_PROGRESS();
                if (stress.Number == null) stress.Number = new hmUI_widget_IMG_NUMBER();
                if (stress.Pointer == null) stress.Pointer = new hmUI_widget_IMG_POINTER();
                if (stress.Icon == null) stress.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) stress.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) stress.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) stress.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) stress.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) stress.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_FatBurning_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementFatBurning fat_burning = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementFatBurning");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementFatBurning());
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementFatBurning");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementFatBurning());
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }

            if (fat_burning != null)
            {
                if (fat_burning.Images == null) fat_burning.Images = new hmUI_widget_IMG_LEVEL();
                if (fat_burning.Segments == null) fat_burning.Segments = new hmUI_widget_IMG_PROGRESS();
                if (fat_burning.Number == null) fat_burning.Number = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_rotation == null) fat_burning.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_circle == null) fat_burning.Text_circle = new Text_Circle();
                if (fat_burning.Number_Target == null) fat_burning.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_rotation_Target == null) fat_burning.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_circle_Target == null) fat_burning.Text_circle_Target = new Text_Circle();
                if (fat_burning.Pointer == null) fat_burning.Pointer = new hmUI_widget_IMG_POINTER();
                if (fat_burning.Circle_Scale == null) fat_burning.Circle_Scale = new Circle_Scale();
                if (fat_burning.Linear_Scale == null) fat_burning.Linear_Scale = new Linear_Scale();
                if (fat_burning.Icon == null) fat_burning.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) fat_burning.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) fat_burning.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) fat_burning.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) fat_burning.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) fat_burning.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) fat_burning.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) fat_burning.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) fat_burning.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) fat_burning.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) fat_burning.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) fat_burning.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) fat_burning.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }



        private void uCtrl_Weather_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementWeather weather = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementWeather");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementWeather());
                    weather = (ElementWeather)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementWeather");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementWeather());
                    weather = (ElementWeather)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }

            if (weather != null)
            {
                if (weather.Images == null) weather.Images = new hmUI_widget_IMG_LEVEL();
                if (weather.Number == null) weather.Number = new hmUI_widget_IMG_NUMBER();
                if (weather.Number_Min == null) weather.Number_Min = new hmUI_widget_IMG_NUMBER();
                if (weather.Number_Max == null) weather.Number_Max = new hmUI_widget_IMG_NUMBER();
                if (weather.City_Name == null) weather.City_Name = new hmUI_widget_TEXT();
                if (weather.Icon == null) weather.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) weather.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Number")) weather.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Number_Min")) weather.Number_Min.position = elementOptions["Number_Min"];
                if (elementOptions.ContainsKey("Number_Max")) weather.Number_Max.position = elementOptions["Number_Max"];
                if (elementOptions.ContainsKey("CityName")) weather.City_Name.position = elementOptions["CityName"];
                if (elementOptions.ContainsKey("Icon")) weather.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_UVIndex_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementUVIndex uv_index = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementUVIndex");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementUVIndex());
                    uv_index = (ElementUVIndex)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementUVIndex");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementUVIndex());
                    uv_index = (ElementUVIndex)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }

            if (uv_index != null)
            {
                if (uv_index.Images == null) uv_index.Images = new hmUI_widget_IMG_LEVEL();
                if (uv_index.Segments == null) uv_index.Segments = new hmUI_widget_IMG_PROGRESS();
                if (uv_index.Number == null) uv_index.Number = new hmUI_widget_IMG_NUMBER();
                if (uv_index.Pointer == null) uv_index.Pointer = new hmUI_widget_IMG_POINTER();
                if (uv_index.Icon == null) uv_index.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) uv_index.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) uv_index.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) uv_index.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) uv_index.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) uv_index.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Humidity_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementHumidity humidity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementHumidity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementHumidity());
                    humidity = (ElementHumidity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementHumidity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementHumidity());
                    humidity = (ElementHumidity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }

            if (humidity != null)
            {
                if (humidity.Images == null) humidity.Images = new hmUI_widget_IMG_LEVEL();
                if (humidity.Segments == null) humidity.Segments = new hmUI_widget_IMG_PROGRESS();
                if (humidity.Number == null) humidity.Number = new hmUI_widget_IMG_NUMBER();
                if (humidity.Pointer == null) humidity.Pointer = new hmUI_widget_IMG_POINTER();
                if (humidity.Icon == null) humidity.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) humidity.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) humidity.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) humidity.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) humidity.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) humidity.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Altimeter_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAltimeter altimeter = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAltimeter");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAltimeter());
                    altimeter = (ElementAltimeter)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAltimeter");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAltimeter());
                    altimeter = (ElementAltimeter)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }

            if (altimeter != null)
            {
                if (altimeter.Number == null) altimeter.Number = new hmUI_widget_IMG_NUMBER();
                if (altimeter.Pointer == null) altimeter.Pointer = new hmUI_widget_IMG_POINTER();
                if (altimeter.Icon == null) altimeter.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Number")) altimeter.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) altimeter.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) altimeter.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Sunrise_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementSunrise sunrise = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementSunrise");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementSunrise());
                    sunrise = (ElementSunrise)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementSunrise");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementSunrise());
                    sunrise = (ElementSunrise)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }

            if (sunrise != null)
            {
                if (sunrise.Images == null) sunrise.Images = new hmUI_widget_IMG_LEVEL();
                if (sunrise.Segments == null) sunrise.Segments = new hmUI_widget_IMG_PROGRESS();
                if (sunrise.Sunrise == null) sunrise.Sunrise = new hmUI_widget_IMG_NUMBER();
                if (sunrise.Sunset == null) sunrise.Sunset = new hmUI_widget_IMG_NUMBER();
                if (sunrise.Pointer == null) sunrise.Pointer = new hmUI_widget_IMG_POINTER();
                if (sunrise.Icon == null) sunrise.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) sunrise.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) sunrise.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Sunrise")) sunrise.Sunrise.position = elementOptions["Sunrise"];
                if (elementOptions.ContainsKey("Sunset")) sunrise.Sunset.position = elementOptions["Sunset"];
                if (elementOptions.ContainsKey("Pointer")) sunrise.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) sunrise.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();

        }

        private void uCtrl_Wind_Elm_OptionsMoved(object sender, EventArgs eventArgs, Dictionary<string, int> elementOptions)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementWind wind = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementWind");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementWind());
                    wind = (ElementWind)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementWind");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementWind());
                    wind = (ElementWind)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }

            if (wind != null)
            {
                if (wind.Images == null) wind.Images = new hmUI_widget_IMG_LEVEL();
                if (wind.Segments == null) wind.Segments = new hmUI_widget_IMG_PROGRESS();
                if (wind.Number == null) wind.Number = new hmUI_widget_IMG_NUMBER();
                if (wind.Pointer == null) wind.Pointer = new hmUI_widget_IMG_POINTER();
                if (wind.Direction == null) wind.Direction = new hmUI_widget_IMG_LEVEL();
                if (wind.Icon == null) wind.Icon = new hmUI_widget_IMG();

                if (elementOptions.ContainsKey("Images")) wind.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) wind.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) wind.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) wind.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Direction")) wind.Direction.position = elementOptions["Direction"];
                if (elementOptions.ContainsKey("Icon")) wind.Icon.position = elementOptions["Icon"];

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        #endregion

        private void uCtrl_Shortcuts_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            if (Watch_Face == null) return;
            ElementShortcuts shortcuts = Watch_Face.Shortcuts;
            //if (radioButton_ScreenNormal.Checked)
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
            //        Watch_Face.ScreenNormal.Elements != null)
            //    {
            //        //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
            //        shortcuts = (ElementShortcuts)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}
            //else
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
            //        Watch_Face.ScreenAOD.Elements != null)
            //    {
            //        shortcuts = (ElementShortcuts)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}
            if (shortcuts != null)
            {
                shortcuts.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Statuses_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementStatuses statuses = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    statuses = (ElementStatuses)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    statuses = (ElementStatuses)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }
            if (statuses != null)
            {
                statuses.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementAnimation animation = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    animation = (ElementAnimation)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    animation = (ElementAnimation)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }
            if (animation != null)
            {
                animation.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        #region VisibleElementChanged

        private void uCtrl_Steps_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementSteps steps = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    steps = (ElementSteps)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    steps = (ElementSteps)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }
            if (steps != null)
            {
                steps.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Battery_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementBattery battery = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    battery = (ElementBattery)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    battery = (ElementBattery)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }
            if (battery != null)
            {
                battery.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Heart_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementHeart heart = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    heart = (ElementHeart)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    heart = (ElementHeart)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }
            if (heart != null)
            {
                heart.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Calories_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementCalories calories = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    calories = (ElementCalories)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    calories = (ElementCalories)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }
            if (calories != null)
            {
                calories.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_PAI_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementPAI pai = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    pai = (ElementPAI)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    pai = (ElementPAI)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }
            if (pai != null)
            {
                pai.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Distance_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementDistance distance = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    distance = (ElementDistance)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    distance = (ElementDistance)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }
            if (distance != null)
            {
                distance.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Stand_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementStand stand = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    stand = (ElementStand)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    stand = (ElementStand)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }
            if (stand != null)
            {
                stand.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Activity_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible, bool showCalories)
        {
            ElementActivity activity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    activity = (ElementActivity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    activity = (ElementActivity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }
            if (activity != null)
            {
                activity.visible = visible;
                activity.showCalories = showCalories;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_SpO2_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementSpO2 spo2 = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    spo2 = (ElementSpO2)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    spo2 = (ElementSpO2)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            if (spo2 != null)
            {
                spo2.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Stress_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementStress stress = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    stress = (ElementStress)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    stress = (ElementStress)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }
            if (stress != null)
            {
                stress.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_FatBurning_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementFatBurning fat_burning = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }
            if (fat_burning != null)
            {
                fat_burning.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }



        private void uCtrl_Weather_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementWeather weather = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    weather = (ElementWeather)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    weather = (ElementWeather)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }
            if (weather != null)
            {
                weather.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_UVIndex_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementUVIndex uv_index = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    uv_index = (ElementUVIndex)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    uv_index = (ElementUVIndex)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }
            if (uv_index != null)
            {
                uv_index.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Humidity_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementHumidity humidity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    humidity = (ElementHumidity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    humidity = (ElementHumidity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }
            if (humidity != null)
            {
                humidity.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Altimeter_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementAltimeter altimeter = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    altimeter = (ElementAltimeter)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    altimeter = (ElementAltimeter)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }
            if (altimeter != null)
            {
                altimeter.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Sunrise_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementSunrise sunrise = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    sunrise = (ElementSunrise)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    sunrise = (ElementSunrise)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }
            if (sunrise != null)
            {
                sunrise.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Wind_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementWind wind = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    wind = (ElementWind)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    wind = (ElementWind)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }
            if (wind != null)
            {
                wind.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Moon_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementMoon moon = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    moon = (ElementMoon)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementMoon");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    moon = (ElementMoon)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementMoon");
                }
            }
            if (moon != null)
            {
                moon.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Image_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            ElementImage image = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    //bool exists = Elements.Exists(e => e.GetType().Name == "ElementAnalogTime");
                    image = (ElementImage)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementImage");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    image = (ElementImage)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementImage");
                }
            }
            if (image != null)
            {
                image.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_DisconnectAlert_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            DisconnectAlert disconnectAlert = null;
            if (Watch_Face != null && Watch_Face.DisconnectAlert != null) disconnectAlert = Watch_Face.DisconnectAlert;

            if (disconnectAlert != null)
            {
                disconnectAlert.enable = visible;
            }

            JSON_Modified = true;
            //PreviewImage();
            FormText();
        }

        private void uCtrl_RepeatingAlert_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            RepeatAlert repeatAlert = null;
            if (Watch_Face != null && Watch_Face.RepeatAlert != null) repeatAlert = Watch_Face.RepeatAlert;

            if (repeatAlert != null)
            {
                repeatAlert.enable = visible;
            }

            JSON_Modified = true;
            //PreviewImage();
            FormText();
        }

        private void uCtrl_TopImage_Elm_VisibleElementChanged(object sender, EventArgs eventArgs, bool visible)
        {
            TopImage topImage = null;
            if (Watch_Face != null && Watch_Face.TopImage != null) topImage = Watch_Face.TopImage;

            if (topImage != null)
            {
                topImage.visible = visible;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        #endregion

        private void uCtrl_TopImage_Elm_ValueChanged(object sender, EventArgs eventArgs)
        {
            TopImage topImage = null;
            if (Watch_Face != null && Watch_Face.TopImage != null) topImage = Watch_Face.TopImage;

            if (topImage != null)
            {
                topImage.showInAOD = uCtrl_TopImage_Elm.checkBox_ShowInAOD.Checked;
            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Shortcuts_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementShortcuts statuses = Watch_Face.Shortcuts;
            //if (radioButton_ScreenNormal.Checked)
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
            //        Watch_Face.ScreenNormal.Elements != null)
            //    {
            //        bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementShortcuts");
            //        //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
            //        if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementShortcuts());
            //        statuses = (ElementShortcuts)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}
            //else
            //{
            //    if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
            //        Watch_Face.ScreenAOD.Elements != null)
            //    {
            //        bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementShortcuts");
            //        //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
            //        if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementShortcuts());
            //        statuses = (ElementShortcuts)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementShortcuts");
            //    }
            //}

            if (statuses != null)
            {
                if (statuses.Step == null) statuses.Step = new hmUI_widget_IMG_CLICK();
                if (statuses.Heart == null) statuses.Heart = new hmUI_widget_IMG_CLICK();
                if (statuses.SPO2 == null) statuses.SPO2 = new hmUI_widget_IMG_CLICK();
                if (statuses.PAI == null) statuses.PAI = new hmUI_widget_IMG_CLICK();
                if (statuses.Stress == null) statuses.Stress = new hmUI_widget_IMG_CLICK();
                if (statuses.Weather == null) statuses.Weather = new hmUI_widget_IMG_CLICK();
                if (statuses.Altimeter == null) statuses.Altimeter = new hmUI_widget_IMG_CLICK();
                if (statuses.Sunrise == null) statuses.Sunrise = new hmUI_widget_IMG_CLICK();
                if (statuses.Alarm == null) statuses.Alarm = new hmUI_widget_IMG_CLICK();
                if (statuses.Sleep == null) statuses.Sleep = new hmUI_widget_IMG_CLICK();
                if (statuses.Countdown == null) statuses.Countdown = new hmUI_widget_IMG_CLICK();
                if (statuses.Stopwatch == null) statuses.Stopwatch = new hmUI_widget_IMG_CLICK();

                Dictionary<string, int> elementOptions = uCtrl_Shortcuts_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Step")) statuses.Step.position = elementOptions["Step"];
                if (elementOptions.ContainsKey("Heart")) statuses.Heart.position = elementOptions["Heart"];
                if (elementOptions.ContainsKey("SPO2")) statuses.SPO2.position = elementOptions["SPO2"];
                if (elementOptions.ContainsKey("PAI")) statuses.PAI.position = elementOptions["PAI"];
                if (elementOptions.ContainsKey("Stress")) statuses.Stress.position = elementOptions["Stress"];
                if (elementOptions.ContainsKey("Weather")) statuses.Weather.position = elementOptions["Weather"];
                if (elementOptions.ContainsKey("Altimeter")) statuses.Altimeter.position = elementOptions["Altimeter"];
                if (elementOptions.ContainsKey("Sunrise")) statuses.Sunrise.position = elementOptions["Sunrise"];
                if (elementOptions.ContainsKey("Alarm")) statuses.Alarm.position = elementOptions["Alarm"];
                if (elementOptions.ContainsKey("Sleep")) statuses.Sleep.position = elementOptions["Sleep"];
                if (elementOptions.ContainsKey("Countdown")) statuses.Countdown.position = elementOptions["Countdown"];
                if (elementOptions.ContainsKey("Stopwatch")) statuses.Stopwatch.position = elementOptions["Stopwatch"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Step":
                        statuses.Step.visible = checkBox.Checked;
                        break;
                    case "checkBox_Heart":
                        statuses.Heart.visible = checkBox.Checked;
                        break;
                    case "checkBox_SPO2":
                        statuses.SPO2.visible = checkBox.Checked;
                        break;
                    case "checkBox_PAI":
                        statuses.PAI.visible = checkBox.Checked;
                        break;
                    case "checkBox_Stress":
                        statuses.Stress.visible = checkBox.Checked;
                        break;
                    case "checkBox_Weather":
                        statuses.Weather.visible = checkBox.Checked;
                        break;
                    case "checkBox_Altimeter":
                        statuses.Altimeter.visible = checkBox.Checked;
                        break;
                    case "checkBox_Sunrise":
                        statuses.Sunrise.visible = checkBox.Checked;
                        break;
                    case "checkBox_Alarm":
                        statuses.Alarm.visible = checkBox.Checked;
                        break;
                    case "checkBox_Sleep":
                        statuses.Sleep.visible = checkBox.Checked;
                        break;
                    case "checkBox_Countdown":
                        statuses.Countdown.visible = checkBox.Checked;
                        break;
                    case "checkBox_Stopwatch":
                        statuses.Stopwatch.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Shortcuts_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Statuses_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementStatuses statuses = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementStatuses");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementStatuses());
                    statuses = (ElementStatuses)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementStatuses");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementStatuses());
                    statuses = (ElementStatuses)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStatuses");
                }
            }

            if (statuses != null)
            {
                if (statuses.Alarm == null) statuses.Alarm = new hmUI_widget_IMG_STATUS();
                if (statuses.Bluetooth == null) statuses.Bluetooth = new hmUI_widget_IMG_STATUS();
                if (statuses.DND == null) statuses.DND = new hmUI_widget_IMG_STATUS();
                if (statuses.Lock == null) statuses.Lock = new hmUI_widget_IMG_STATUS();

                Dictionary<string, int> elementOptions = uCtrl_Statuses_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Alarm")) statuses.Alarm.position = elementOptions["Alarm"];
                if (elementOptions.ContainsKey("Bluetooth")) statuses.Bluetooth.position = elementOptions["Bluetooth"];
                if (elementOptions.ContainsKey("DND")) statuses.DND.position = elementOptions["DND"];
                if (elementOptions.ContainsKey("Lock")) statuses.Lock.position = elementOptions["Lock"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Alarm":
                        statuses.Alarm.visible = checkBox.Checked;
                        break;
                    case "checkBox_Bluetooth":
                        statuses.Bluetooth.visible = checkBox.Checked;
                        break;
                    case "checkBox_DND":
                        statuses.DND.visible = checkBox.Checked;
                        break;
                    case "checkBox_Lock":
                        statuses.Lock.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Statuses_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Animation_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAnimation animation = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAnimation");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAnimation());
                    animation = (ElementAnimation)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAnimation");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAnimation());
                    animation = (ElementAnimation)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAnimation");
                }
            }

            if (animation != null)
            {
                if (animation.Frame_Animation_List == null) animation.Frame_Animation_List = new hmUI_widget_IMG_ANIM_List();
                if (animation.Motion_Animation_List == null) animation.Motion_Animation_List = new Motion_Animation_List();
                if (animation.Rotate_Animation_List == null) animation.Rotate_Animation_List = new Rotate_Animation_List();

                Dictionary<string, int> elementOptions = uCtrl_Animation_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("FrameAnimation")) animation.Frame_Animation_List.position = elementOptions["FrameAnimation"];
                if (elementOptions.ContainsKey("MotionAnimation")) animation.Motion_Animation_List.position = elementOptions["MotionAnimation"];
                if (elementOptions.ContainsKey("RotateAnimation")) animation.Rotate_Animation_List.position = elementOptions["RotateAnimation"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_FrameAnimation":
                        animation.Frame_Animation_List.visible = checkBox.Checked;
                        break;
                    case "checkBox_MotionAnimation":
                        animation.Motion_Animation_List.visible = checkBox.Checked;
                        break;
                    case "checkBox_RotateAnimation":
                        animation.Rotate_Animation_List.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Animation_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        #region VisibleOptionsChanged
        private void uCtrl_Steps_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementSteps steps = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementSteps");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementSteps());
                    steps = (ElementSteps)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementSteps");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementSteps());
                    steps = (ElementSteps)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSteps");
                }
            }

            if (steps != null)
            {
                if (steps.Images == null) steps.Images = new hmUI_widget_IMG_LEVEL();
                if (steps.Segments == null) steps.Segments = new hmUI_widget_IMG_PROGRESS();
                if (steps.Number == null) steps.Number = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_rotation == null) steps.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_circle == null) steps.Text_circle = new Text_Circle();
                if (steps.Number_Target == null) steps.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_rotation_Target == null) steps.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (steps.Text_circle_Target == null) steps.Text_circle_Target = new Text_Circle();
                if (steps.Pointer == null) steps.Pointer = new hmUI_widget_IMG_POINTER();
                if (steps.Circle_Scale == null) steps.Circle_Scale = new Circle_Scale();
                if (steps.Linear_Scale == null) steps.Linear_Scale = new Linear_Scale();
                if (steps.Icon == null) steps.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Steps_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) steps.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) steps.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) steps.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) steps.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) steps.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) steps.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) steps.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) steps.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) steps.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) steps.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) steps.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) steps.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        steps.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        steps.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        steps.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        steps.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        steps.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Target":
                        steps.Number_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation_Target":
                        steps.Text_rotation_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle_Target":
                        steps.Text_circle_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        steps.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        steps.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        steps.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        steps.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Steps_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Battery_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementBattery battery = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementBattery");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementBattery());
                    battery = (ElementBattery)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementBattery");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementBattery());
                    battery = (ElementBattery)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementBattery");
                }
            }

            if (battery != null)
            {
                if (battery.Images == null) battery.Images = new hmUI_widget_IMG_LEVEL();
                if (battery.Segments == null) battery.Segments = new hmUI_widget_IMG_PROGRESS();
                if (battery.Number == null) battery.Number = new hmUI_widget_IMG_NUMBER();
                if (battery.Text_rotation == null) battery.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (battery.Text_circle == null) battery.Text_circle = new Text_Circle();
                if (battery.Pointer == null) battery.Pointer = new hmUI_widget_IMG_POINTER();
                if (battery.Circle_Scale == null) battery.Circle_Scale = new Circle_Scale();
                if (battery.Linear_Scale == null) battery.Linear_Scale = new Linear_Scale();
                if (battery.Icon == null) battery.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Battery_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) battery.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) battery.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) battery.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) battery.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) battery.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Pointer")) battery.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) battery.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) battery.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) battery.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        battery.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        battery.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        battery.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        battery.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        battery.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        battery.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        battery.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        battery.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        battery.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Battery_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Heart_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementHeart heart = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementHeart");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementHeart());
                    heart = (ElementHeart)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementHeart");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementHeart());
                    heart = (ElementHeart)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHeart");
                }
            }

            if (heart != null)
            {
                if (heart.Images == null) heart.Images = new hmUI_widget_IMG_LEVEL();
                if (heart.Segments == null) heart.Segments = new hmUI_widget_IMG_PROGRESS();
                if (heart.Number == null) heart.Number = new hmUI_widget_IMG_NUMBER();
                if (heart.Text_rotation == null) heart.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (heart.Text_circle == null) heart.Text_circle = new Text_Circle();
                if (heart.Pointer == null) heart.Pointer = new hmUI_widget_IMG_POINTER();
                if (heart.Circle_Scale == null) heart.Circle_Scale = new Circle_Scale();
                if (heart.Linear_Scale == null) heart.Linear_Scale = new Linear_Scale();
                if (heart.Icon == null) heart.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Heart_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) heart.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) heart.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) heart.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) heart.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) heart.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Pointer")) heart.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) heart.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) heart.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) heart.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        heart.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        heart.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        heart.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        heart.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        heart.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        heart.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        heart.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        heart.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        heart.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Heart_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Calories_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementCalories calories = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementCalories");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementCalories());
                    calories = (ElementCalories)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementCalories");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementCalories());
                    calories = (ElementCalories)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementCalories");
                }
            }

            if (calories != null)
            {
                if (calories.Images == null) calories.Images = new hmUI_widget_IMG_LEVEL();
                if (calories.Segments == null) calories.Segments = new hmUI_widget_IMG_PROGRESS();
                if (calories.Number == null) calories.Number = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_rotation == null) calories.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_circle == null) calories.Text_circle = new Text_Circle();
                if (calories.Number_Target == null) calories.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_rotation_Target == null) calories.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (calories.Text_circle_Target == null) calories.Text_circle_Target = new Text_Circle();
                if (calories.Pointer == null) calories.Pointer = new hmUI_widget_IMG_POINTER();
                if (calories.Circle_Scale == null) calories.Circle_Scale = new Circle_Scale();
                if (calories.Linear_Scale == null) calories.Linear_Scale = new Linear_Scale();
                if (calories.Icon == null) calories.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Calories_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) calories.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) calories.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) calories.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) calories.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) calories.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) calories.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) calories.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) calories.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) calories.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) calories.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) calories.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) calories.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        calories.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        calories.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        calories.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        calories.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        calories.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Target":
                        calories.Number_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation_Target":
                        calories.Text_rotation_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle_Target":
                        calories.Text_circle_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        calories.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        calories.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        calories.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        calories.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Calories_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_PAI_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementPAI pai = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementPAI");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementPAI());
                    pai = (ElementPAI)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementPAI");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementPAI());
                    pai = (ElementPAI)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementPAI");
                }
            }

            if (pai != null)
            {
                if (pai.Images == null) pai.Images = new hmUI_widget_IMG_LEVEL();
                if (pai.Segments == null) pai.Segments = new hmUI_widget_IMG_PROGRESS();
                if (pai.Number == null) pai.Number = new hmUI_widget_IMG_NUMBER();
                if (pai.Number_Target == null) pai.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (pai.Text_rotation_Target == null) pai.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (pai.Text_circle_Target == null) pai.Text_circle_Target = new Text_Circle();
                if (pai.Pointer == null) pai.Pointer = new hmUI_widget_IMG_POINTER();
                if (pai.Circle_Scale == null) pai.Circle_Scale = new Circle_Scale();
                if (pai.Linear_Scale == null) pai.Linear_Scale = new Linear_Scale();
                if (pai.Icon == null) pai.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_PAI_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) pai.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) pai.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) pai.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Number_Target")) pai.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) pai.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) pai.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) pai.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) pai.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) pai.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) pai.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        pai.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        pai.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        pai.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Target":
                        pai.Number_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation_Target":
                        pai.Text_rotation_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle_Target":
                        pai.Text_circle_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        pai.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        pai.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        pai.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        pai.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_PAI_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Distance_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementDistance distance = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementDistance");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementDistance());
                    distance = (ElementDistance)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementDistance");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementDistance());
                    distance = (ElementDistance)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementDistance");
                }
            }

            if (distance != null)
            {
                if (distance.Number == null) distance.Number = new hmUI_widget_IMG_NUMBER();
                if (distance.Text_rotation == null) distance.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (distance.Text_circle == null) distance.Text_circle = new Text_Circle();
                if (distance.Icon == null) distance.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Distance_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Number")) distance.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) distance.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) distance.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Icon")) distance.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Number":
                        distance.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        distance.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        distance.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        distance.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Distance_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Stand_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementStand stand = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementStand");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementStand());
                    stand = (ElementStand)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementStand");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementStand());
                    stand = (ElementStand)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStand");
                }
            }

            if (stand != null)
            {
                if (stand.Images == null) stand.Images = new hmUI_widget_IMG_LEVEL();
                if (stand.Segments == null) stand.Segments = new hmUI_widget_IMG_PROGRESS();
                if (stand.Number == null) stand.Number = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_rotation == null) stand.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_circle == null) stand.Text_circle = new Text_Circle();
                if (stand.Number_Target == null) stand.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_rotation_Target == null) stand.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (stand.Text_circle_Target == null) stand.Text_circle_Target = new Text_Circle();
                if (stand.Pointer == null) stand.Pointer = new hmUI_widget_IMG_POINTER();
                if (stand.Circle_Scale == null) stand.Circle_Scale = new Circle_Scale();
                if (stand.Linear_Scale == null) stand.Linear_Scale = new Linear_Scale();
                if (stand.Icon == null) stand.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Stand_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) stand.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) stand.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) stand.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) stand.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) stand.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) stand.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) stand.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) stand.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) stand.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) stand.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) stand.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) stand.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        stand.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        stand.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        stand.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        stand.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        stand.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Target":
                        stand.Number_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation_Target":
                        stand.Text_rotation_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle_Target":
                        stand.Text_circle_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        stand.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        stand.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        stand.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        stand.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Stand_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Activity_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementActivity activity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementActivity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementActivity());
                    activity = (ElementActivity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementActivity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementSteps());
                    activity = (ElementActivity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementActivity");
                }
            }

            if (activity != null)
            {
                if (activity.Images == null) activity.Images = new hmUI_widget_IMG_LEVEL();
                if (activity.Segments == null) activity.Segments = new hmUI_widget_IMG_PROGRESS();
                if (activity.Number == null) activity.Number = new hmUI_widget_IMG_NUMBER();
                if (activity.Number_Target == null) activity.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (activity.Pointer == null) activity.Pointer = new hmUI_widget_IMG_POINTER();
                if (activity.Circle_Scale == null) activity.Circle_Scale = new Circle_Scale();
                if (activity.Linear_Scale == null) activity.Linear_Scale = new Linear_Scale();
                if (activity.Icon == null) activity.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Activity_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) activity.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) activity.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) activity.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Number_Target")) activity.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Pointer")) activity.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) activity.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) activity.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) activity.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        activity.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        activity.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        activity.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Target":
                        activity.Number_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        activity.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        activity.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        activity.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        activity.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Activity_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_SpO2_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementSpO2 SpO2 = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementSpO2");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementSpO2());
                    SpO2 = (ElementSpO2)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementSpO2");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementSpO2());
                    SpO2 = (ElementSpO2)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSpO2");
                }
            }

            if (SpO2 != null)
            {
                if (SpO2.Number == null) SpO2.Number = new hmUI_widget_IMG_NUMBER();
                if (SpO2.Text_rotation == null) SpO2.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (SpO2.Text_circle == null) SpO2.Text_circle = new Text_Circle();
                if (SpO2.Icon == null) SpO2.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_SpO2_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Number")) SpO2.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) SpO2.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) SpO2.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Icon")) SpO2.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Number":
                        SpO2.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        SpO2.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        SpO2.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        SpO2.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_SpO2_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Stress_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementStress stress = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementStress");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementStress());
                    stress = (ElementStress)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementStress");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementStress());
                    stress = (ElementStress)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementStress");
                }
            }

            if (stress != null)
            {
                if (stress.Images == null) stress.Images = new hmUI_widget_IMG_LEVEL();
                if (stress.Segments == null) stress.Segments = new hmUI_widget_IMG_PROGRESS();
                if (stress.Number == null) stress.Number = new hmUI_widget_IMG_NUMBER();
                if (stress.Pointer == null) stress.Pointer = new hmUI_widget_IMG_POINTER();
                if (stress.Icon == null) stress.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Stress_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) stress.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) stress.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) stress.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) stress.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) stress.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        stress.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        stress.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        stress.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        stress.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        stress.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Stress_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_FatBurning_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementFatBurning fat_burning = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementFatBurning");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementFatBurning());
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementSteps");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementFatBurning());
                    fat_burning = (ElementFatBurning)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementFatBurning");
                }
            }

            if (fat_burning != null)
            {
                if (fat_burning.Images == null) fat_burning.Images = new hmUI_widget_IMG_LEVEL();
                if (fat_burning.Segments == null) fat_burning.Segments = new hmUI_widget_IMG_PROGRESS();
                if (fat_burning.Number == null) fat_burning.Number = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_rotation == null) fat_burning.Text_rotation = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_circle == null) fat_burning.Text_circle = new Text_Circle();
                if (fat_burning.Number_Target == null) fat_burning.Number_Target = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_rotation_Target == null) fat_burning.Text_rotation_Target = new hmUI_widget_IMG_NUMBER();
                if (fat_burning.Text_circle_Target == null) fat_burning.Text_circle_Target = new Text_Circle();
                if (fat_burning.Pointer == null) fat_burning.Pointer = new hmUI_widget_IMG_POINTER();
                if (fat_burning.Circle_Scale == null) fat_burning.Circle_Scale = new Circle_Scale();
                if (fat_burning.Linear_Scale == null) fat_burning.Linear_Scale = new Linear_Scale();
                if (fat_burning.Icon == null) fat_burning.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_FatBurning_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) fat_burning.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) fat_burning.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) fat_burning.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Text_rotation")) fat_burning.Text_rotation.position = elementOptions["Text_rotation"];
                if (elementOptions.ContainsKey("Text_circle")) fat_burning.Text_circle.position = elementOptions["Text_circle"];
                if (elementOptions.ContainsKey("Number_Target")) fat_burning.Number_Target.position = elementOptions["Number_Target"];
                if (elementOptions.ContainsKey("Text_rotation_Target")) fat_burning.Text_rotation_Target.position = elementOptions["Text_rotation_Target"];
                if (elementOptions.ContainsKey("Text_circle_Target")) fat_burning.Text_circle_Target.position = elementOptions["Text_circle_Target"];
                if (elementOptions.ContainsKey("Pointer")) fat_burning.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Circle_Scale")) fat_burning.Circle_Scale.position = elementOptions["Circle_Scale"];
                if (elementOptions.ContainsKey("Linear_Scale")) fat_burning.Linear_Scale.position = elementOptions["Linear_Scale"];
                if (elementOptions.ContainsKey("Icon")) fat_burning.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        fat_burning.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        fat_burning.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        fat_burning.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation":
                        fat_burning.Text_rotation.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle":
                        fat_burning.Text_circle.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Target":
                        fat_burning.Number_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_rotation_Target":
                        fat_burning.Text_rotation_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_circle_Target":
                        fat_burning.Text_circle_Target.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        fat_burning.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Circle_Scale":
                        fat_burning.Circle_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Linear_Scale":
                        fat_burning.Linear_Scale.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        fat_burning.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_FatBurning_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }



        private void uCtrl_Weather_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementWeather weather = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementWeather");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementWeather());
                    weather = (ElementWeather)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementWeather");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementWeather());
                    weather = (ElementWeather)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWeather");
                }
            }

            if (weather != null)
            {
                if (weather.Images == null) weather.Images = new hmUI_widget_IMG_LEVEL();
                if (weather.Number == null) weather.Number = new hmUI_widget_IMG_NUMBER();
                if (weather.Number_Min == null) weather.Number_Min = new hmUI_widget_IMG_NUMBER();
                if (weather.Number_Max == null) weather.Number_Max = new hmUI_widget_IMG_NUMBER();
                if (weather.City_Name == null) weather.City_Name = new hmUI_widget_TEXT();
                if (weather.Icon == null) weather.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Weather_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) weather.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Number")) weather.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Number_Min")) weather.Number_Min.position = elementOptions["Number_Min"];
                if (elementOptions.ContainsKey("Number_Max")) weather.Number_Max.position = elementOptions["Number_Max"];
                if (elementOptions.ContainsKey("CityName")) weather.City_Name.position = elementOptions["CityName"];
                if (elementOptions.ContainsKey("Icon")) weather.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        weather.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        weather.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Min":
                        weather.Number_Min.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number_Max":
                        weather.Number_Max.visible = checkBox.Checked;
                        break;
                    case "checkBox_Text_CityName":
                        weather.City_Name.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        weather.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Weather_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_UVIndex_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementUVIndex uv_index = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementUVIndex");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementUVIndex());
                    uv_index = (ElementUVIndex)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementUVIndex");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementUVIndex());
                    uv_index = (ElementUVIndex)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementUVIndex");
                }
            }

            if (uv_index != null)
            {
                if (uv_index.Images == null) uv_index.Images = new hmUI_widget_IMG_LEVEL();
                if (uv_index.Segments == null) uv_index.Segments = new hmUI_widget_IMG_PROGRESS();
                if (uv_index.Number == null) uv_index.Number = new hmUI_widget_IMG_NUMBER();
                if (uv_index.Pointer == null) uv_index.Pointer = new hmUI_widget_IMG_POINTER();
                if (uv_index.Icon == null) uv_index.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_UVIndex_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) uv_index.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) uv_index.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) uv_index.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) uv_index.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) uv_index.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        uv_index.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        uv_index.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        uv_index.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        uv_index.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        uv_index.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_UVIndex_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Humidity_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementHumidity humidity = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementHumidity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementHumidity());
                    humidity = (ElementHumidity)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementHumidity");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementHumidity());
                    humidity = (ElementHumidity)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementHumidity");
                }
            }

            if (humidity != null)
            {
                if (humidity.Images == null) humidity.Images = new hmUI_widget_IMG_LEVEL();
                if (humidity.Segments == null) humidity.Segments = new hmUI_widget_IMG_PROGRESS();
                if (humidity.Number == null) humidity.Number = new hmUI_widget_IMG_NUMBER();
                if (humidity.Pointer == null) humidity.Pointer = new hmUI_widget_IMG_POINTER();
                if (humidity.Icon == null) humidity.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Humidity_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) humidity.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) humidity.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) humidity.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) humidity.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) humidity.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        humidity.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        humidity.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        humidity.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        humidity.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        humidity.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Humidity_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Altimeter_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementAltimeter altimeter = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementAltimeter");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementAltimeter());
                    altimeter = (ElementAltimeter)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementAltimeter");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementAltimeter());
                    altimeter = (ElementAltimeter)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementAltimeter");
                }
            }

            if (altimeter != null)
            {
                if (altimeter.Number == null) altimeter.Number = new hmUI_widget_IMG_NUMBER();
                if (altimeter.Pointer == null) altimeter.Pointer = new hmUI_widget_IMG_POINTER();
                if (altimeter.Icon == null) altimeter.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Altimeter_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Number")) altimeter.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) altimeter.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) altimeter.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Number":
                        altimeter.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        altimeter.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        altimeter.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Altimeter_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Sunrise_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementSunrise sunrise = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementSunrise");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementSunrise());
                    sunrise = (ElementSunrise)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementSunrise");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementSunrise());
                    sunrise = (ElementSunrise)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementSunrise");
                }
            }

            if (sunrise != null)
            {
                if (sunrise.Images == null) sunrise.Images = new hmUI_widget_IMG_LEVEL();
                if (sunrise.Segments == null) sunrise.Segments = new hmUI_widget_IMG_PROGRESS();
                if (sunrise.Sunrise == null) sunrise.Sunrise = new hmUI_widget_IMG_NUMBER();
                if (sunrise.Sunset == null) sunrise.Sunset = new hmUI_widget_IMG_NUMBER();
                if (sunrise.Sunset_Sunrise == null) sunrise.Sunset_Sunrise = new hmUI_widget_IMG_NUMBER();
                if (sunrise.Pointer == null) sunrise.Pointer = new hmUI_widget_IMG_POINTER();
                if (sunrise.Icon == null) sunrise.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Sunrise_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) sunrise.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) sunrise.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Sunrise")) sunrise.Sunrise.position = elementOptions["Sunrise"];
                if (elementOptions.ContainsKey("Sunset")) sunrise.Sunset.position = elementOptions["Sunset"];
                if (elementOptions.ContainsKey("Sunset_Sunrise")) sunrise.Sunset_Sunrise.position = elementOptions["Sunset_Sunrise"];
                if (elementOptions.ContainsKey("Pointer")) sunrise.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Icon")) sunrise.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        sunrise.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        sunrise.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Sunrise":
                        sunrise.Sunrise.visible = checkBox.Checked;
                        break;
                    case "checkBox_Sunset":
                        sunrise.Sunset.visible = checkBox.Checked;
                        break;
                    case "checkBox_Sunset_Sunrise":
                        sunrise.Sunset_Sunrise.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        sunrise.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        sunrise.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Sunrise_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Wind_Elm_VisibleOptionsChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;

            ElementWind wind = null;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face != null && Watch_Face.ScreenNormal != null &&
                    Watch_Face.ScreenNormal.Elements != null)
                {
                    bool exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementWind");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenNormal.Elements.Add(new ElementWind());
                    wind = (ElementWind)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }
            else
            {
                if (Watch_Face != null && Watch_Face.ScreenAOD != null &&
                    Watch_Face.ScreenAOD.Elements != null)
                {
                    bool exists = Watch_Face.ScreenAOD.Elements.Exists(e => e.GetType().Name == "ElementWind");
                    //digitalTime = (ElementAnalogTime)Watch_Face.ScreenNormal.Elements.Find(e => e.GetType().Name == "ElementAnalogTime");
                    if (!exists) Watch_Face.ScreenAOD.Elements.Add(new ElementWind());
                    wind = (ElementWind)Watch_Face.ScreenAOD.Elements.Find(e => e.GetType().Name == "ElementWind");
                }
            }

            if (wind != null)
            {
                if (wind.Images == null) wind.Images = new hmUI_widget_IMG_LEVEL();
                if (wind.Segments == null) wind.Segments = new hmUI_widget_IMG_PROGRESS();
                if (wind.Number == null) wind.Number = new hmUI_widget_IMG_NUMBER();
                if (wind.Pointer == null) wind.Pointer = new hmUI_widget_IMG_POINTER();
                if (wind.Direction == null) wind.Direction = new hmUI_widget_IMG_LEVEL();
                if (wind.Icon == null) wind.Icon = new hmUI_widget_IMG();

                Dictionary<string, int> elementOptions = uCtrl_Wind_Elm.GetOptionsPosition();
                if (elementOptions.ContainsKey("Images")) wind.Images.position = elementOptions["Images"];
                if (elementOptions.ContainsKey("Segments")) wind.Segments.position = elementOptions["Segments"];
                if (elementOptions.ContainsKey("Number")) wind.Number.position = elementOptions["Number"];
                if (elementOptions.ContainsKey("Pointer")) wind.Pointer.position = elementOptions["Pointer"];
                if (elementOptions.ContainsKey("Direction")) wind.Direction.position = elementOptions["Direction"];
                if (elementOptions.ContainsKey("Icon")) wind.Icon.position = elementOptions["Icon"];

                CheckBox checkBox = (CheckBox)sender;
                string name = checkBox.Name;
                switch (name)
                {
                    case "checkBox_Images":
                        wind.Images.visible = checkBox.Checked;
                        break;
                    case "checkBox_Segments":
                        wind.Segments.visible = checkBox.Checked;
                        break;
                    case "checkBox_Number":
                        wind.Number.visible = checkBox.Checked;
                        break;
                    case "checkBox_Pointer":
                        wind.Pointer.visible = checkBox.Checked;
                        break;
                    case "checkBox_Direction":
                        wind.Direction.visible = checkBox.Checked;
                        break;
                    case "checkBox_Icon":
                        wind.Icon.visible = checkBox.Checked;
                        break;
                }

            }

            uCtrl_Wind_Elm_SelectChanged(sender, eventArgs);

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        #endregion

        private void button_SavePNG_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* SavePNG");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = FullFileDir;
            saveFileDialog.Filter = Properties.FormStrings.FilterPng;
            saveFileDialog.FileName = "Preview.png";
            //openFileDialog.Filter = "PNG Files: (*.png)|*.png";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = Properties.FormStrings.Dialog_Title_SavePNG;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        break;
                    case "GTR 4":
                        bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        break;
                    case "Amazfit Band 7":
                        bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
                        break;
                    case "GTS 4 mini":
                        bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        break;
                    case "Falcon":
                    case "GTR mini":
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true, false, 
                    false, false, link, false, -1, false, 0);
                if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
            }
            Logger.WriteLine("* SavePNG(end)");
        }

        private void button_SaveGIF_Click(object sender, EventArgs ea)
        {
            Logger.WriteLine("* SaveGIF");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = FullFileDir;
            saveFileDialog.Filter = Properties.FormStrings.FilterGif;
            saveFileDialog.FileName = "Preview.gif";
            //openFileDialog.Filter = "GIF Files: (*.gif)|*.gif";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = Properties.FormStrings.Dialog_Title_SaveGIF;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        break;
                    case "GTR 4":
                        bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        break;
                    case "Amazfit Band 7":
                        bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
                        break;
                    case "GTS 4 mini":
                        bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        break;
                    case "Falcon":
                    case "GTR mini":
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }
                Bitmap bitmapTemp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                Graphics gPanel = Graphics.FromImage(bitmap);
                bool save = false;
                Random rnd = new Random();
                PreviewView = false;
                int SetNumber = WatchFacePreviewSet.SetNumber;

                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    // основной экран
                    //WidgetDrawIndex = 0;
                    for (int i = 0; i < 13; i++)
                    {
                        save = false;
                        switch (i)
                        {
                            case 0:
                                //button_Set1.PerformClick();
                                SetPreferences(userCtrl_Set1);
                                save = true;
                                break;
                            case 1:
                                if (userCtrl_Set2.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set2);
                                    save = true;
                                }
                                break;
                            case 2:
                                if (userCtrl_Set3.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set3);
                                    save = true;
                                }
                                break;
                            case 3:
                                if (userCtrl_Set4.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set4);
                                    save = true;
                                }
                                break;
                            case 4:
                                if (userCtrl_Set5.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set5);
                                    save = true;
                                }
                                break;
                            case 5:
                                if (userCtrl_Set6.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set6);
                                    save = true;
                                }
                                break;
                            case 6:
                                if (userCtrl_Set7.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set7);
                                    save = true;
                                }
                                break;
                            case 7:
                                if (userCtrl_Set8.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set8);
                                    save = true;
                                }
                                break;
                            case 8:
                                if (userCtrl_Set9.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set9);
                                    save = true;
                                }
                                break;
                            case 9:
                                if (userCtrl_Set10.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set10);
                                    save = true;
                                }
                                break;
                            case 10:
                                if (userCtrl_Set11.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set11);
                                    save = true;
                                }
                                break;
                            case 11:
                                if (userCtrl_Set12.numericUpDown_Calories_Set.Value != 1234)
                                {
                                    SetPreferences(userCtrl_Set12);
                                    save = true;
                                }
                                break;
                        }

                        if (save)
                        {
                            bitmap = bitmapTemp;
                            gPanel = Graphics.FromImage(bitmap);
                            Logger.WriteLine("SaveGIF SetPreferences(" + i.ToString() + ")");

                            //int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                            int link = 0;
                            Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                                false, false, false, link, false, -1, false, 0);
                            //if (checkBox_crop.Checked) {
                            //    bitmap = ApplyMask(bitmap, mask);
                            //    gPanel = Graphics.FromImage(bitmap);
                            //}
                            if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                            else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                            // Add first image and set the animation delay to 100ms
                            MagickImage item = new MagickImage(bitmap);
                            //ExifProfile profile = item.GetExifProfile();
                            collection.Add(item);
                            //collection[collection.Count - 1].AnimationDelay = 100;
                            collection[collection.Count - 1].AnimationDelay = (int)(100 * numericUpDown_Gif_Speed.Value);
                            //WidgetDrawIndex++;
                        }
                    }

                    Logger.WriteLine("SaveGIF_Shortcuts");
                    bool Shortcuts_In_Gif = checkBox_Shortcuts_In_Gif.Checked;
                    //bool exists = false;
                    //if (Watch_Face != null && Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Elements != null) 
                    //exists = Watch_Face.ScreenNormal.Elements.Exists(e => e.GetType().Name == "ElementShortcuts"); // проверяем что такого элемента нет
                    //Logger.WriteLine("SaveGIF_Shortcuts");
                    // Shortcuts
                    if (Shortcuts_In_Gif && Watch_Face.Shortcuts != null && Watch_Face.Shortcuts.visible)
                    {
                        bitmap = bitmapTemp;
                        gPanel = Graphics.FromImage(bitmap);
                        //int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                        int link_AOD = 0;
                        Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                            false, false, false, link_AOD, Shortcuts_In_Gif, -1, false, 0);

                        if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                        else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                        // Add first image and set the animation delay to 100ms
                        MagickImage item_AOD = new MagickImage(bitmap);
                        //ExifProfile profile = item.GetExifProfile();
                        collection.Add(item_AOD);
                        //collection[collection.Count - 1].AnimationDelay = 100;
                        collection[collection.Count - 1].AnimationDelay = (int)(100 * numericUpDown_Gif_Speed.Value);
                    }

                    //WidgetDrawIndex = -1;
                    Logger.WriteLine("SaveGIF_AOD");
                    // AOD
                    if (Watch_Face.ScreenAOD != null && 
                        (Watch_Face.ScreenAOD.Background != null || Watch_Face.ScreenAOD.Elements != null))
                    {

                        bitmap = bitmapTemp;
                        gPanel = Graphics.FromImage(bitmap);
                        //int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                        int link_AOD = 1;
                        Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                            false, false, false, link_AOD, false, -1, false, 0);
                        //if (checkBox_crop.Checked)
                        //{
                        //    bitmap = ApplyMask(bitmap, mask);
                        //    gPanel = Graphics.FromImage(bitmap);
                        //}
                        if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                        else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                        // Add first image and set the animation delay to 100ms
                        MagickImage item_AOD = new MagickImage(bitmap);
                        //ExifProfile profile = item.GetExifProfile();
                        collection.Add(item_AOD);
                        //collection[collection.Count - 1].AnimationDelay = 100;
                        collection[collection.Count - 1].AnimationDelay = (int)(100 * numericUpDown_Gif_Speed.Value);


                        SetPreferences(userCtrl_Set1);
                        bitmap = bitmapTemp;
                        gPanel = Graphics.FromImage(bitmap);
                        Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                            false, false, false, link_AOD, false, -1, false, 0);
                        //if (checkBox_crop.Checked)
                        //{
                        //    bitmap = ApplyMask(bitmap, mask);
                        //    gPanel = Graphics.FromImage(bitmap);
                        //}
                        if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                        else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                        item_AOD = new MagickImage(bitmap);
                        //ExifProfile profile = item.GetExifProfile();
                        collection.Add(item_AOD);
                        //collection[collection.Count - 1].AnimationDelay = 100;
                        collection[collection.Count - 1].AnimationDelay = (int)(100 * numericUpDown_Gif_Speed.Value);
                    }


                    Logger.WriteLine("SaveGIF_Editable_Background");
                    // Editable_Background
                    if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Background != null &&
                        (Watch_Face.ScreenNormal.Background.Editable_Background != null &&
                        Watch_Face.ScreenNormal.Background.Editable_Background.enable_edit_bg &&
                        Watch_Face.ScreenNormal.Background.Editable_Background.BackgroundList.Count > 0))
                    {
                        int bg_index = Watch_Face.ScreenNormal.Background.Editable_Background.selected_background;
                        //int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                        int link_AOD = 0;
                        for (int index = 0; index < Watch_Face.ScreenNormal.Background.Editable_Background.BackgroundList.Count; index++)
                        {
                            bitmap = bitmapTemp;
                            gPanel = Graphics.FromImage(bitmap);
                            Watch_Face.ScreenNormal.Background.Editable_Background.selected_background = index;
                            Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                                            false, false, false, link_AOD, false, -1, true, 1);
                            if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                            else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                            // Add first image and set the animation delay to 100ms
                            MagickImage item_bg_edit = new MagickImage(bitmap);
                            //ExifProfile profile = item.GetExifProfile();
                            collection.Add(item_bg_edit);
                            //collection[collection.Count - 1].AnimationDelay = 100;
                            collection[collection.Count - 1].AnimationDelay = (int)(100 * numericUpDown_Gif_Speed.Value); 
                        }
                        Watch_Face.ScreenNormal.Background.Editable_Background.selected_background = bg_index;
                    }


                    Logger.WriteLine("SaveGIF_Editable_Pointers");
                    // Editable_Pointers
                    if (Watch_Face.ElementEditablePointers != null && 
                        Watch_Face.ElementEditablePointers.visible &&
                        Watch_Face.ElementEditablePointers.config != null &&
                        Watch_Face.ElementEditablePointers.config.Count > 0)
                    {
                        int p_index = Watch_Face.ElementEditablePointers.selected_pointers;
                        //int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                        int link_AOD = 0;
                        for (int index = 0; index < Watch_Face.ElementEditablePointers.config.Count; index++)
                        {
                            bitmap = bitmapTemp;
                            gPanel = Graphics.FromImage(bitmap);
                            Watch_Face.ElementEditablePointers.selected_pointers = index;
                            Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                                            false, false, false, link_AOD, false, -1, true, 3);
                            if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                            else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                            // Add first image and set the animation delay to 100ms
                            MagickImage item_bg_edit = new MagickImage(bitmap);
                            //ExifProfile profile = item.GetExifProfile();
                            collection.Add(item_bg_edit);
                            //collection[collection.Count - 1].AnimationDelay = 100;
                            collection[collection.Count - 1].AnimationDelay = (int)(100 * numericUpDown_Gif_Speed.Value);
                        }
                        Watch_Face.ElementEditablePointers.selected_pointers = p_index;
                    }

                    Logger.WriteLine("SaveGIF_Editable_Elements edit mode");
                    // Editable_Elements edit mode
                    if(Watch_Face.Editable_Elements != null &&
                        Watch_Face.Editable_Elements.visible  &&
                        Watch_Face.Editable_Elements.Watchface_edit_group != null &&
                        Watch_Face.Editable_Elements.Watchface_edit_group.Count > 0)
                    {
                        int zone_index = Watch_Face.Editable_Elements.selected_zone;
                        int[] e_index = new int[Watch_Face.Editable_Elements.Watchface_edit_group.Count];
                        for(int index = 0; index < Watch_Face.Editable_Elements.Watchface_edit_group.Count; index++)
                        {
                            e_index[index] = Watch_Face.Editable_Elements.Watchface_edit_group[index].selected_element;
                        }
                        //int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                        int link_AOD = 0;
                        for (int index = 0; index < Watch_Face.Editable_Elements.Watchface_edit_group.Count; index++) // поочередно выбираем все зоны
                        {
                            bitmap = bitmapTemp;
                            gPanel = Graphics.FromImage(bitmap);
                            Watch_Face.Editable_Elements.selected_zone = index;
                            WATCHFACE_EDIT_GROUP e_g = Watch_Face.Editable_Elements.Watchface_edit_group[index];
                            if (e_g.Elements != null && e_g.Elements.Count > 0)
                            {
                                // перебираем все зоны и меняем в них выбраные элементы
                                for (int group_index = 0; group_index < Watch_Face.Editable_Elements.Watchface_edit_group.Count; group_index++)
                                {
                                    WATCHFACE_EDIT_GROUP edit_group = Watch_Face.Editable_Elements.Watchface_edit_group[group_index];
                                    int element_index = index;
                                    while (element_index >= edit_group.Elements.Count)
                                    {
                                        element_index = element_index - edit_group.Elements.Count;
                                    }
                                    edit_group.selected_element = element_index;
                                }
                            }
                            Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                                            false, false, false, link_AOD, false, -1, true, 2);
                            if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                            else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                            // Add first image and set the animation delay to 100ms
                            MagickImage item_bg_edit = new MagickImage(bitmap);
                            //ExifProfile profile = item.GetExifProfile();
                            collection.Add(item_bg_edit);
                            //collection[collection.Count - 1].AnimationDelay = 100;
                            collection[collection.Count - 1].AnimationDelay = (int)(100 * numericUpDown_Gif_Speed.Value);
                        }
                        for (int index = 0; index < Watch_Face.Editable_Elements.Watchface_edit_group.Count; index++)
                        {
                            Watch_Face.Editable_Elements.Watchface_edit_group[index].selected_element = e_index[index];
                        }
                        Watch_Face.Editable_Elements.selected_zone = zone_index;
                    }


                    // Optionally reduce colors
                    QuantizeSettings settings = new QuantizeSettings();
                    //settings.Colors = 256;
                    //collection.Quantize(settings);

                    // Optionally optimize the images (images should have the same size).
                    collection.OptimizeTransparency();
                    //collection.Optimize();

                    // Save gif
                    collection.Write(saveFileDialog.FileName);
                }
                switch (SetNumber)
                {
                    case 1:
                        SetPreferences(userCtrl_Set1);
                        break;
                    case 2:
                        SetPreferences(userCtrl_Set2);
                        break;
                    case 3:
                        SetPreferences(userCtrl_Set3);
                        break;
                    case 4:
                        SetPreferences(userCtrl_Set4);
                        break;
                    case 5:
                        SetPreferences(userCtrl_Set5);
                        break;
                    case 6:
                        SetPreferences(userCtrl_Set6);
                        break;
                    case 7:
                        SetPreferences(userCtrl_Set7);
                        break;
                    case 8:
                        SetPreferences(userCtrl_Set8);
                        break;
                    case 9:
                        SetPreferences(userCtrl_Set9);
                        break;
                    case 10:
                        SetPreferences(userCtrl_Set10);
                        break;
                    case 11:
                        SetPreferences(userCtrl_Set11);
                        break;
                    case 12:
                        SetPreferences(userCtrl_Set12);
                        break;
                    default:
                        SetPreferences(userCtrl_Set12);
                        break;
                }
                PreviewView = true;
                mask.Dispose();
                bitmapTemp.Dispose();
                bitmap.Dispose();
            }
            Logger.WriteLine("* SaveGIF (end)");
        }

        private void button_SavePNG_shortcut_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* SavePNG_shortcut");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = FullFileDir;
            saveFileDialog.Filter = Properties.FormStrings.FilterPng;
            saveFileDialog.FileName = "Preview.png";
            //openFileDialog.Filter = "PNG Files: (*.png)|*.png";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = Properties.FormStrings.Dialog_Title_SavePNG;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                switch (ProgramSettings.Watch_Model)
                {
                    case "GTR 3 Pro":
                        bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png");
                        break;
                    case "GTS 3":
                    case "GTS 4":
                        bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                        break;
                    case "GTR 4":
                        bitmap = new Bitmap(Convert.ToInt32(466), Convert.ToInt32(466), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_4.png");
                        break;
                    case "Amazfit Band 7":
                        bitmap = new Bitmap(Convert.ToInt32(194), Convert.ToInt32(368), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_band_7.png");
                        break;
                    case "GTS 4 mini":
                        bitmap = new Bitmap(Convert.ToInt32(336), Convert.ToInt32(384), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_4_mini.png");
                        break;
                    case "Falcon":
                    case "GTR mini":
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                //int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                int link_AOD = 0;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, false, true,
                    false, false, false, link_AOD, true, -1, false, 0);
                if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
            }
            Logger.WriteLine("* SavePNG_shortcut(end)");
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\Settings.json"))
            {
                File.Delete(Application.StartupPath + @"\Settings.json");
                if (MessageBox.Show(Properties.FormStrings.Message_Restart_Text1 + Environment.NewLine +
                                Properties.FormStrings.Message_Restart_Text2, Properties.FormStrings.Message_Restart_Caption,
                                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Name == "tabPageConverting")
            {
                switch (comboBox_watch_model.Text)
                {
                    case "GTR 3":
                        comboBox_ConvertingInput_Model.Text = "454 (GTR 3)";
                        comboBox_ConvertingOutput_Model.Text = "480 (GTR 3 Pro)";
                        break;
                    case "T-Rex 2":
                        comboBox_ConvertingInput_Model.Text = "454 (T-Rex 2)";
                        comboBox_ConvertingOutput_Model.Text = "480 (GTR 3 Pro)";
                        break;
                    case "T-Rex Ultra":
                        comboBox_ConvertingInput_Model.Text = "454 (T-Rex Ultra)";
                        comboBox_ConvertingOutput_Model.Text = "480 (GTR 3 Pro)";
                        break;
                    case "GTR 3 Pro":
                        comboBox_ConvertingInput_Model.Text = "480 (GTR 3 Pro)";
                        comboBox_ConvertingOutput_Model.Text = "454 (GTR 3)";
                        break;
                    case "GTS 3":
                        comboBox_ConvertingInput_Model.Text = "390 (GTS 3)";
                        comboBox_ConvertingOutput_Model.Text = "336 (GTS 4 mini)";
                        break;
                    case "GTR 4":
                        comboBox_ConvertingInput_Model.Text = "466 (GTR 4)";
                        comboBox_ConvertingOutput_Model.Text = "454 (GTR 3)";
                        break;
                    case "GTS 4 mini":
                        comboBox_ConvertingInput_Model.Text = "336 (GTS 4 mini)";
                        comboBox_ConvertingOutput_Model.Text = "390 (GTS 3)";
                        break;
                    case "Falcon":
                        comboBox_ConvertingInput_Model.Text = "416 (Falcon)";
                        comboBox_ConvertingOutput_Model.Text = "454 (GTR 3)";
                        break;
                    case "GTR mini":
                        comboBox_ConvertingInput_Model.Text = "416 (GTR mini)";
                        comboBox_ConvertingOutput_Model.Text = "454 (GTR 3)";
                        break;
                    case "GTS 4":
                        comboBox_ConvertingInput_Model.Text = "390 (GTS 4)";
                        comboBox_ConvertingOutput_Model.Text = "336 (GTS 4 mini)";
                        break;
                    default:
                        comboBox_ConvertingInput_Model.SelectedIndex = 0;
                        comboBox_ConvertingOutput_Model.SelectedIndex = 0;
                        break;
                }
            }
            if (FileName != null && FullFileDir != null)
            {
                button_Converting.Enabled = true;
                label_ConvertingHelp.Visible = false;
            }
            else
            {
                button_Converting.Enabled = false;
                label_ConvertingHelp.Visible = true;
            }
        }

        private void comboBox_ConvertingInput_Model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ConvertingInput_Model.SelectedIndex == 0) numericUpDown_ConvertingInput_Custom.Enabled = true;
            else numericUpDown_ConvertingInput_Custom.Enabled = false;
            switch (comboBox_ConvertingInput_Model.Text)
            {
                case "336 (GTS 4 mini)":
                    numericUpDown_ConvertingInput_Custom.Value = 336;
                    break;
                case "390 (GTS 3)":
                    numericUpDown_ConvertingInput_Custom.Value = 390;
                    break;
                case "416 (Falcon)":
                    numericUpDown_ConvertingInput_Custom.Value = 416;
                    break;
                case "416 (GTR mini)":
                    numericUpDown_ConvertingInput_Custom.Value = 416;
                    break;
                case "454 (GTR 3)":
                    numericUpDown_ConvertingInput_Custom.Value = 454;
                    break;
                case "454 (T-Rex 2)":
                    numericUpDown_ConvertingInput_Custom.Value = 454;
                    break;
                case "454 (T-Rex Ultra)":
                    numericUpDown_ConvertingInput_Custom.Value = 454;
                    break;
                case "466 (GTR 4)":
                    numericUpDown_ConvertingInput_Custom.Value = 466;
                    break;
                case "480 (GTR 3 Pro)":
                    numericUpDown_ConvertingInput_Custom.Value = 480;
                    break;
                case "390 (GTS 4)":
                    numericUpDown_ConvertingInput_Custom.Value = 390;
                    break;
            }
        }

        private void comboBox_ConvertingOutput_Model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ConvertingOutput_Model.SelectedIndex == 0) numericUpDown_ConvertingOutput_Custom.Enabled = true;
            else numericUpDown_ConvertingOutput_Custom.Enabled = false;
            switch (comboBox_ConvertingOutput_Model.Text)
            {
                case "336 (GTS 4 mini)":
                    numericUpDown_ConvertingOutput_Custom.Value = 336;
                    break;
                case "390 (GTS 3)":
                    numericUpDown_ConvertingOutput_Custom.Value = 390;
                    break;
                case "416 (Falcon)":
                    numericUpDown_ConvertingOutput_Custom.Value = 416;
                    break;
                case "416 (GTR mini)":
                    numericUpDown_ConvertingOutput_Custom.Value = 416;
                    break;
                case "454 (GTR 3)":
                    numericUpDown_ConvertingOutput_Custom.Value = 454;
                    break;
                case "454 (T-Rex 2)":
                    numericUpDown_ConvertingOutput_Custom.Value = 454;
                    break;
                case "454 (T-Rex Ultra)":
                    numericUpDown_ConvertingOutput_Custom.Value = 454;
                    break;
                case "466 (GTR 4)":
                    numericUpDown_ConvertingOutput_Custom.Value = 466;
                    break;
                case "480 (GTR 3 Pro)":
                    numericUpDown_ConvertingOutput_Custom.Value = 480;
                    break;
                case "390 (GTS 4)":
                    numericUpDown_ConvertingOutput_Custom.Value = 390;
                    break;
            }
        }

        private void button_Converting_Click(object sender, EventArgs e)
        {
            if (FileName != null && FullFileDir != null)
            {
                // сохранение если файл не сохранен
                if (SaveRequest() == DialogResult.Cancel) return;

                string suffix = "_GTR_3";
                float scale = 1;
                string DeviceName = "GTR3";
                switch (comboBox_ConvertingOutput_Model.Text)
                {
                    case "336 (GTS 4 mini)":
                        suffix = "_GTS_4_mini";
                        DeviceName = "GTS4_mini";
                        break;
                    case "390 (GTS 3)":
                        suffix = "_GTS_3";
                        DeviceName = "GTS3";
                        break;
                    case "416 (Falcon)":
                        suffix = "_Falcon";
                        DeviceName = "Falcon";
                        break;
                    case "416 (GTR mini)":
                        suffix = "_GTR_mini";
                        DeviceName = "GTR_mini";
                        break;
                    case "454 (GTR 3)":
                        suffix = "_GTR_3";
                        DeviceName = "GTR3";
                        break;
                    case "454 (T-Rex 2)":
                        suffix = "_T_Rex_2";
                        DeviceName = "T_Rex_2";
                        break;
                    case "454 (T-Rex Ultra)":
                        suffix = "_T_Rex_Ultra";
                        DeviceName = "T_Rex_Ultra";
                        break;
                    case "466 (GTR 4)":
                        suffix = "_GTR_4";
                        DeviceName = "GTR4";
                        break;
                    case "480 (GTR 3 Pro)":
                        suffix = "_GTR_3_Pro";
                        DeviceName = "GTR3_Pro";
                        break;
                    case "390 (GTS 4)":
                        suffix = "_GTS_4";
                        DeviceName = "GTS4";
                        break;
                    default:
                        suffix = "_Custom_" + numericUpDown_ConvertingOutput_Custom.Value.ToString();
                        break;
                }

                scale = (float)(numericUpDown_ConvertingOutput_Custom.Value / numericUpDown_ConvertingInput_Custom.Value);

                string newFullDirName = FullFileDir + suffix;
                string newDirName = Path.GetFileName(newFullDirName);
                if (Directory.Exists(newFullDirName))
                {
                    //DialogResult dr = MessageBox.Show(Properties.FormStrings.Message_Save_JSON_Modified_Text1 +
                    //    Path.GetFileNameWithoutExtension(FileName) + Properties.FormStrings.Message_Save_JSON_Modified_Text2,
                    //    Properties.FormStrings.Message_Save_JSON_Modified_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    DialogResult dr = MessageBox.Show(Properties.FormStrings.Message_WarningConverting_Text1
                        + newDirName + Properties.FormStrings.Message_WarningConverting_Text2,
                        Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        Directory.Delete(newFullDirName, true);
                    }
                    else return;
                }

                // Масштабируем изображения
                Image loadedImage = null;
                Directory.CreateDirectory(newFullDirName);
                Directory.CreateDirectory(Path.Combine(newFullDirName, "assets"));
                foreach (string ImageFullName in ListImagesFullName)
                {
                    using (FileStream stream = new FileStream(ImageFullName, FileMode.Open, FileAccess.Read))
                    {
                        loadedImage = Image.FromStream(stream);
                    }
                    string fileName = Path.GetFileName(ImageFullName);
                    string newFullFileName = Path.Combine(newFullDirName, "assets", fileName);
                    Bitmap bitmap = ResizeImage(loadedImage, scale);

                    bitmap.Save(newFullFileName, ImageFormat.Png);
                }
                if (ListAnimImagesFullName.Count > 0)
                {
                    Directory.CreateDirectory(Path.Combine(newFullDirName, "assets", "animation"));
                    foreach (string ImageFullName in ListAnimImagesFullName)
                    {
                        using (FileStream stream = new FileStream(ImageFullName, FileMode.Open, FileAccess.Read))
                        {
                            loadedImage = Image.FromStream(stream);
                        }
                        string fileName = Path.GetFileName(ImageFullName);
                        string newFullFileName = Path.Combine(newFullDirName, "assets", "animation", fileName);
                        Bitmap bitmap = ResizeImage(loadedImage, scale);

                        bitmap.Save(newFullFileName, ImageFormat.Png);
                    } 
                }
                loadedImage = null;

                JSON_Scale(scale, DeviceName);

                string newFullFileNameJson = Path.Combine(newFullDirName,
                    Path.GetFileNameWithoutExtension(FileName) + suffix + ".json");
                string newJson = JsonConvert.SerializeObject(Watch_Face, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(newFullFileNameJson, newJson, Encoding.UTF8);

                FileName = Path.GetFileName(newFullFileNameJson);
                FullFileDir = Path.GetDirectoryName(newFullFileNameJson);

                LoadJson(newFullFileNameJson);

                MessageBox.Show(Properties.FormStrings.Message_ConvertingCompleted_Text,
                        Properties.FormStrings.Message_Warning_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show(Properties.FormStrings.Message_ConvertingCompleted_Text);
            }
        }

        private void linkLabel_py_amazfit_tools_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/SashaCX75/ImageToGTR3/releases/tag/v1.1");
        }

        private void удалитьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    DataGridView dataGridView = sourceControl as DataGridView;
                    try
                    {
                        int selectedRowCount = dataGridView.SelectedCells.Count;
                        if (selectedRowCount > 0)
                        {
                            string fileName = "";
                            DataGridViewSelectedCellCollection selectedCells = dataGridView.SelectedCells;
                            int startColumn = selectedCells[0].ColumnIndex;
                            foreach (DataGridViewCell cells in selectedCells)
                            {
                                int column = cells.ColumnIndex;
                                if (column == startColumn)
                                {
                                    int rowIndex = cells.RowIndex;
                                    if (dataGridView.Name == "dataGridView_AnimImagesList") fileName = ListAnimImagesFullName[rowIndex];
                                    else fileName = ListImagesFullName[rowIndex];
                                    if (File.Exists(fileName))
                                    {
                                        if (MessageBox.Show(Properties.FormStrings.Message_Delet_Text1 +
                                            Path.GetFileName(fileName) + Properties.FormStrings.Message_Delet_Text2,
                                            Properties.FormStrings.Message_Delet_Caption, MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                        {
                                            File.Delete(fileName);
                                        }
                                    }
                                }
                            }
                            if (dataGridView.Name == "dataGridView_AnimImagesList") fileName = Path.GetDirectoryName(fileName);
                            LoadImage(Path.GetDirectoryName(fileName) + @"\");
                            HideAllElemenrOptions();
                            if (dataGridView.Name == "dataGridView_AnimImagesList")
                            {
                                ResetHighlightState("Animation");
                                uCtrl_Animation_Elm_SelectChanged(sender, null);
                            }
                            else ResetHighlightState("");
                            PreviewImage();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }


        private void contextMenuStrip_RemoveImage_Opening(object sender, CancelEventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    DataGridView dataGridView = sourceControl as DataGridView;

                    if (dataGridView.CurrentCell == null) e.Cancel = true;
                    if (FullFileDir != null && Directory.Exists(FullFileDir + @"\assets\"))
                    {
                        contextMenuStrip_RemoveImage.Items[1].Enabled = true;
                    }
                    else
                    {
                        contextMenuStrip_RemoveImage.Items[1].Enabled = false;
                    }
                }
            }
        }

        private void dataGridView_ImagesList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView dataGridView = sender as DataGridView;
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    dataGridView.CurrentCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
        }

        private void radioButton_Settings_Open_Download_Your_File_CheckedChanged(object sender, EventArgs e)
        {
            textBox_PreviewStates_Path.Enabled = radioButton_Settings_Open_Download_Your_File.Checked;
            button_PreviewStates_PathGet.Enabled = radioButton_Settings_Open_Download_Your_File.Checked;
            if (Settings_Load) return;

            ProgramSettings.Settings_Open_Download_Your_File = radioButton_Settings_Open_Download_Your_File.Checked;

            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
        }

        private void button_PreviewStates_PathGet_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FullFileDir;
            openFileDialog.FileName = "Preview.States";
            openFileDialog.Filter = "PreviewStates file | *.States";
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_WatchSkin;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Logger.WriteLine("* WatchSkin_PathGet_Click");
                string fullfilename = openFileDialog.FileName;
                //if (fullfilename.IndexOf(Application.StartupPath) == 0)
                //    fullfilename = fullfilename.Remove(0, Application.StartupPath.Length);
                textBox_PreviewStates_Path.Text = fullfilename;
                ProgramSettings.PreviewStates_Path = fullfilename;

                string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText("Settings.json", JSON_String, Encoding.UTF8);

                Logger.WriteLine("* WatchSkin_PathGet_Click_END");
            }
        }

        private void обновитьСписокИзображенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FullFileDir != null && Directory.Exists(FullFileDir + @"\assets\")) LoadImage(FullFileDir + @"\assets\");
            HideAllElemenrOptions();
            ResetHighlightState("");
        }

        private void button_Add_Anim_Images_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* Add_Anim_Images");
            if (FullFileDir == null) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = FullFileDir;
            openFileDialog.FileName = FileName;
            openFileDialog.Filter = Properties.FormStrings.FilterPng;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_Dial_Settings;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //FileName = Path.GetFileName(openFileDialog.FileName);
                //FullFileDir = Path.GetDirectoryName(openFileDialog.FileName);

                Logger.WriteLine("* Add_Images_Click");
                string dirImgName = FullFileDir + @"\assets\";
                string dirName = FullFileDir + @"\assets\animation\";
                if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
                foreach (string fileFullName in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileName(fileFullName);
                    fileName = fileName.Replace(" ", "_");
                    if (fileName.IndexOf("_") < 0) fileName = "anim_" + fileName;
                    string newFileName = dirName + fileName;
                    if (File.Exists(newFileName))
                    {
                        DialogResult dialogResult = MessageBox.Show(Properties.FormStrings.Message_Warning_Image_Exist1
                            + fileName + Environment.NewLine + Properties.FormStrings.Message_Warning_Image_Exist2,
                            Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (dialogResult == DialogResult.Yes) File.Copy(fileFullName, newFileName, true); ;
                    }
                    else File.Copy(fileFullName, newFileName, true);
                }
                LoadImage(dirImgName);
            }
            Logger.WriteLine("* Add_Anim_Images (end)");
        }

        private void uCtrl_Animation_Elm_ShowAnimation(object sender, EventArgs eventArgs)
        {
            FormAnimation formAnimation = new FormAnimation(currentDPI);
            formAnimation.Owner = this;
            int interval = 10;
            int.TryParse(comboBox_Animation_Preview_Speed.Text, out interval);
            interval = 1000 / interval;
            formAnimation.timer1.Interval = interval;
            //switch (comboBox_Animation_Preview_Speed.SelectedIndex)
            //{
            //    case 0:
            //        formAnimation.timer1.Interval = 20;
            //        break;
            //    case 1:
            //        formAnimation.timer1.Interval = 25;
            //        break;
            //    case 2:
            //        formAnimation.timer1.Interval = 33;
            //        break;
            //    case 3:
            //        formAnimation.timer1.Interval = 50;
            //        break;
            //    case 4:
            //        formAnimation.timer1.Interval = 100;
            //        break;
            //}
            formAnimation.ShowDialog();
        }

        private void dataGridView_ImagesList_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                DataGridView dataGridView = sender as DataGridView;
                if (dataGridView != null)
                {
                    try
                    {
                        int selectedRowCount = dataGridView.SelectedCells.Count;
                        if (selectedRowCount > 0)
                        {
                            string fileName = "";
                            DataGridViewSelectedCellCollection selectedCells = dataGridView.SelectedCells;
                            int startColumn = selectedCells[0].ColumnIndex;
                            string delFileName = "";
                            foreach (DataGridViewCell cells in selectedCells)
                            {
                                int column = cells.ColumnIndex;
                                if (column == startColumn)
                                {
                                    int rowIndex = cells.RowIndex;
                                    if (dataGridView.Name == "dataGridView_AnimImagesList") fileName = ListAnimImagesFullName[rowIndex];
                                    else fileName = ListImagesFullName[rowIndex];
                                    delFileName += Environment.NewLine + Path.GetFileName(fileName) + ";";
                                }
                            }
                            if (MessageBox.Show(Properties.FormStrings.Message_Delet_Text11 +
                                            delFileName + Properties.FormStrings.Message_Delet_Text21,
                                            Properties.FormStrings.Message_Delet_Caption, MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                foreach (DataGridViewCell cells in selectedCells)
                                {
                                    int column = cells.ColumnIndex;
                                    if (column == startColumn)
                                    {
                                        int rowIndex = cells.RowIndex;
                                        if (dataGridView.Name == "dataGridView_AnimImagesList") fileName = ListAnimImagesFullName[rowIndex];
                                        else fileName = ListImagesFullName[rowIndex];
                                        if (File.Exists(fileName))
                                        {
                                            File.Delete(fileName);
                                        }
                                    }
                                } 
                            }
                            if (dataGridView.Name == "dataGridView_AnimImagesList") fileName = Path.GetDirectoryName(fileName);
                            LoadImage(Path.GetDirectoryName(fileName) + @"\");
                            HideAllElemenrOptions();
                            if (dataGridView.Name == "dataGridView_AnimImagesList") { 
                                ResetHighlightState("Animation");
                                uCtrl_Animation_Elm_SelectChanged(sender, null);
                            }
                            else ResetHighlightState("");
                            PreviewImage();
                        }
                    }
                    catch (Exception)
                    {
                    } 
                }
            }
        }

        private void uCtrl_EditableBackground_Opt_VisibleChanged(object sender, EventArgs e)
        {
            PreviewImage();
        }

        private void button_SaveAs_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* Project_SaveAs");
            // сохранение если файл не сохранен
            if (SaveRequest() == DialogResult.Cancel) return;

            //string subPath = Application.StartupPath + @"\Watch_face\";
            string subPath = Path.GetDirectoryName(FullFileDir);
            if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            dialog.InitialDirectory = subPath;
            dialog.EnsureValidNames = false;
            dialog.Title = Properties.FormStrings.Dialog_Title_Save_As;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok && FullFileDir != null)
            {
                Logger.WriteLine("* Project_SaveAs_Click");
                string fullfilename = Path.GetFileNameWithoutExtension(dialog.FileName);
                string assetsDir = Path.Combine(FullFileDir, "assets");
                fullfilename = Path.Combine(dialog.FileName, fullfilename) + ".json";
                string dirName = Path.GetDirectoryName(fullfilename) + @"\assets\";
                if (Directory.Exists(dirName))
                {
                    DialogResult dialogResult = MessageBox.Show(Properties.FormStrings.Message_Warning_Assets_Exist1 +
                        Environment.NewLine + Properties.FormStrings.Message_Warning_Assets_Exist2 + Environment.NewLine +
                        Properties.FormStrings.Message_Warning_Assets_Exist3, Properties.FormStrings.Message_Warning_Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No) return;
                }      
                if (Path.GetExtension(fullfilename) != ".json") fullfilename = fullfilename + ".json";
                FileName = Path.GetFileName(fullfilename);
                FullFileDir = Path.GetDirectoryName(fullfilename);
                if (Directory.Exists(assetsDir))
                {
                    string newAssetsDir = Path.Combine(FullFileDir, "assets");
                    if (!Directory.Exists(newAssetsDir)) Directory.CreateDirectory(newAssetsDir);

                    //Создать идентичное дерево каталогов
                    foreach (string dirPath in Directory.GetDirectories(assetsDir, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(assetsDir, newAssetsDir));

                    //Скопировать все файлы. И перезаписать(если такие существуют)
                    foreach (string newPath in Directory.GetFiles(assetsDir, "*.*", SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(assetsDir, newAssetsDir), true);
                }
                if (Watch_Face == null) Watch_Face = new WATCH_FACE();
                if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                Random rnd = new Random();
                int rndID = rnd.Next(1000, 10000000);
                Watch_Face.WatchFace_Info.WatchFaceId = rndID;

                //JSON_Modified = false;
                //FormText();
                save_JSON_File(fullfilename);
                LoadJson(fullfilename);
            }
            Logger.WriteLine("* Project_SaveAs (end)");
        }

        // Checking the version using >= enables forward compatibility.
        string CheckFor45PlusVersion(int releaseKey)
        {
            if (releaseKey >= 533320)
                return "4.8.1 or later";
            if (releaseKey >= 528040)
                return "4.8";
            if (releaseKey >= 461808)
                return "4.7.2";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if (releaseKey >= 379893)
                return "4.5.2";
            if (releaseKey >= 378675)
                return "4.5.1";
            if (releaseKey >= 378389)
                return "4.5";
            // This code should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }

    }
}

public static class MouseClickСoordinates
{
    //public static int X { get; set; }
    //public static int Y { get; set; }
    public static int X = -1;
    public static int Y = -1;
}

//public class WatchfaceID
//{
//    public int ID { get; set; }
//    public bool UseID { get; set; }
//}

static class Logger
{
    //----------------------------------------------------------
    // Статический метод записи строки в файл лога без переноса
    //----------------------------------------------------------
    public static void Write(string text)
    {
        try
        {
#if DEBUG
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Program log.txt", true))
            {
                sw.Write(text);
            }
#endif
        }
        catch (Exception)
        {
        }
    }

    //---------------------------------------------------------
    // Статический метод записи строки в файл лога с переносом
    //---------------------------------------------------------
    public static void WriteLine(string message)
    {
#if DEBUG
        try
        {
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Program log.txt", true))
            {
                sw.WriteLine(String.Format("{0,-23} {1}", DateTime.Now.ToString() + ":", message));
            }
        }
        catch (Exception)
        {
        }
#endif
        Console.WriteLine(String.Format("{0,-23} {1}", DateTime.Now.ToString() + ":", message));
    }
}

public class MyCustomComparer : IComparer<FileInfo>
{
    public int Compare(FileInfo x, FileInfo y)
    {
        // split filename
        //string[] parts1 = x.Name.Split('_');
        //string[] parts2 = y.Name.Split('_');
        string name1 = x.Name;
        string name2 = y.Name;
        int value1 = 0;
        int value2 = 0;
        name1 = Path.GetFileNameWithoutExtension(name1);
        name2 = Path.GetFileNameWithoutExtension(name2);
        if (Int32.TryParse(name1, out value1) && Int32.TryParse(name2, out value2))
        {
            if (value1 < value2) return -1;
            if (value1 > value2) return 1;
            if (value1 == value2) 
                return 0;
        }

        string[] parts1 = name1.Split(new char[] { '-', '_', '.' });
        string[] parts2 = name2.Split(new char[] { '-', '_', '.' });

        //// calculate how much leading zeros we need
        //int toPad1 = 10 - parts1[0].Length;
        //int toPad2 = 10 - parts2[0].Length;

        //if (toPad1 < 0) toPad1 = 0;
        //if (toPad2 < 0) toPad2 = 0;

        //// add the zeros, only for sorting
        //parts1[0] = parts1[0].Insert(0, new String('0', toPad1));
        //parts2[0] = parts2[0].Insert(0, new String('0', toPad2));

        for (int i = 0; i < parts1.Length; i++)
        {
            int ruselt;
            if (Int32.TryParse(parts1[i], out ruselt))
            {
                int toPad = 10 - parts1[i].Length;
                if (toPad < 0) toPad = 0;
                parts1[i] = parts1[i].Insert(0, new String('0', toPad)); 
            }
        }
        for (int i = 0; i < parts2.Length; i++)
        {
            int ruselt;
            if (Int32.TryParse(parts2[i], out ruselt))
            {
                int toPad = 10 - parts2[i].Length;
                if (toPad < 0) toPad = 0;
                parts2[i] = parts2[i].Insert(0, new String('0', toPad)); 
            }
        }

        // create the comparable string
        string toCompare1 = string.Join("", parts1);
        string toCompare2 = string.Join("", parts2);

        // compare
        int ret = toCompare1.CompareTo(toCompare2);
        Console.WriteLine("Compare1=" + toCompare1);
        Console.WriteLine("Compare2=" + toCompare2);
        Console.WriteLine("return=" + ret.ToString());
        Console.WriteLine(" ");

        return toCompare1.CompareTo(toCompare2);
    }
}
