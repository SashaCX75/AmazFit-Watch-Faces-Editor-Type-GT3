using ControlLibrary;
using ImageMagick;
using Newtonsoft.Json;
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        WATCH_FACE Watch_Face;
        Watch_Face_Preview_Set WatchFacePreviewSet;
        List<string> ListImages = new List<string>(); // перечень имен файлов с картинками без раширений
        List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
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

            //SplashScreenStart();

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



                if ((ProgramSettings.language == null) || (ProgramSettings.language.Length < 2))
                {
                    string language = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                    //int language = System.Globalization.CultureInfo.CurrentCulture.LCID;
                    //ProgramSettings.language = "Русский";
                    ProgramSettings.language = "English";
                    Logger.WriteLine("language = " + language);
                    if (language == "ru")
                    {
                        ProgramSettings.language = "Русский";
                    }
                    if (language == "en")
                    {
                        ProgramSettings.language = "English";
                    }
                    if (language == "es")
                    {
                        ProgramSettings.language = "Español";
                    }
                    if (language == "fr")
                    {
                        ProgramSettings.language = "French";
                    }
                    if (language == "it")
                    {
                        ProgramSettings.language = "Italiano";
                    }
                    if (language == "zh")
                    {
                        ProgramSettings.language = "Chinese/简体中文";
                    }
                    if (language == "hu")
                    {
                        ProgramSettings.language = "Magyar";
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
            byte[] fontData = Properties.Resources.OpenSans_Regular;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.OpenSans_Regular.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.OpenSans_Regular.Length, IntPtr.Zero, ref dummy);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.WriteLine("* Form1_Load");
            // закрываем SplashScreen
            //IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "AmazFit WatchFace editor SplashScreen");
            //if (windowPtr != IntPtr.Zero)
            //{
            //    Logger.WriteLine("* SplashScreen_CLOSE");
            //    SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //}


            Logger.WriteLine("Form1_Load");

            PreviewView = false;

            comboBox_AddBackground.SelectedIndex = 0;
            comboBox_AddTime.SelectedIndex = 0;
            comboBox_AddDate.SelectedIndex = 0;
            comboBox_AddActivity.SelectedIndex = 0;
            comboBox_AddAir.SelectedIndex = 0;
            comboBox_AddSystem.SelectedIndex = 0;
            progressBar1.Width = (int)(650 * currentDPI);

            Logger.WriteLine("Set Model_Watch");
            if (ProgramSettings.Model_GTR3)
            {
                radioButton_GTR3.Checked = true;
                textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_3;
            }
            else if (ProgramSettings.Model_GTR3_Pro)
            {
                radioButton_GTR3_Pro.Checked = true;
                textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTR_3_Pro;
            }
            else if (ProgramSettings.Model_GTS3)
            {
                radioButton_GTS3.Checked = true;
                textBox_WatchSkin_Path.Text = ProgramSettings.WatchSkin_GTS_3;
            }
            checkBox_WatchSkin_Use.Checked = ProgramSettings.WatchSkin_Use;

            Logger.WriteLine("Set checkBox");
            checkBox_border.Checked = ProgramSettings.ShowBorder;
            checkBox_crop.Checked = ProgramSettings.Crop;
            checkBox_Show_Shortcuts.Checked = ProgramSettings.Show_Shortcuts;
            checkBox_CircleScaleImage.Checked = ProgramSettings.Show_CircleScale_Area;
            checkBox_center_marker.Checked = ProgramSettings.Shortcuts_Center_marker;
            checkBox_WidgetsArea.Checked = ProgramSettings.Show_Widgets_Area;

            //comboBox_Hour_alignment.SelectedIndex = 0;
            //comboBox_Minute_alignment.SelectedIndex = 0;
            //comboBox_Second_alignment.SelectedIndex = 0;

            //comboBox_Hour_alignment_AOD.SelectedIndex = 0;
            //comboBox_Minute_alignment_AOD.SelectedIndex = 0;

            //comboBox_Day_alignment.SelectedIndex = 0;
            //comboBox_Month_alignment.SelectedIndex = 0;
            //comboBox_Year_alignment.SelectedIndex = 0;

            //comboBox_Day_alignment_AOD.SelectedIndex = 0;
            //comboBox_Month_alignment_AOD.SelectedIndex = 0;
            //comboBox_Year_alignment_AOD.SelectedIndex = 0;

            //userControl_text_Distance.Collapsed = false;
            //userControl_text_Distance_AOD.Collapsed = false;

            //tabPage_Background.ImageIndex = 0;
            //tabPage_Time.ImageIndex = 1;
            //tabPage_Date.ImageIndex = 2;
            //tabPage_Activity.ImageIndex = 3;
            //tabPage_Air.ImageIndex = 4;
            //tabPage_System.ImageIndex = 5;

            //tabPage_Background_AOD.ImageIndex = 0;
            //tabPage_Time_AOD.ImageIndex = 1;
            //tabPage_Date_AOD.ImageIndex = 2;
            //tabPage_Activity_AOD.ImageIndex = 3;
            //tabPage_Air_AOD.ImageIndex = 4;
            //tabPage_System_AOD.ImageIndex = 5;

            //tabPage_WidgetsEdit.ImageIndex = 0;
            //tabPage_WidgetAdd.ImageIndex = 1;



            label_version.Text = "v " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();

            Logger.WriteLine("Set Settings");
            Settings_Load = true;
            radioButton_Settings_AfterUnpack_Dialog.Checked = ProgramSettings.Settings_AfterUnpack_Dialog;
            radioButton_Settings_AfterUnpack_DoNothing.Checked = ProgramSettings.Settings_AfterUnpack_DoNothing;
            radioButton_Settings_AfterUnpack_Download.Checked = ProgramSettings.Settings_AfterUnpack_Download;

            radioButton_Settings_Open_Dialog.Checked = ProgramSettings.Settings_Open_Dialog;
            radioButton_Settings_Open_DoNotning.Checked = ProgramSettings.Settings_Open_DoNotning;
            radioButton_Settings_Open_Download.Checked = ProgramSettings.Settings_Open_Download;

            radioButton_Settings_Pack_Dialog.Checked = ProgramSettings.Settings_Pack_Dialog;
            radioButton_Settings_Pack_DoNotning.Checked = ProgramSettings.Settings_Pack_DoNotning;
            radioButton_Settings_Pack_GoToFile.Checked = ProgramSettings.Settings_Pack_GoToFile;

            radioButton_Settings_Unpack_Dialog.Checked = ProgramSettings.Settings_Unpack_Dialog;
            radioButton_Settings_Unpack_Replace.Checked = ProgramSettings.Settings_Unpack_Replace;
            radioButton_Settings_Unpack_Save.Checked = ProgramSettings.Settings_Unpack_Save;
            numericUpDown_Gif_Speed.Value = (decimal)ProgramSettings.Gif_Speed;

            checkBox_Shortcuts_Area.Checked = ProgramSettings.Shortcuts_Area;
            checkBox_Shortcuts_Border.Checked = ProgramSettings.Shortcuts_Border;

            checkBox_ShowIn12hourFormat.Checked = ProgramSettings.ShowIn12hourFormat;
            checkBox_AllWidgetsInGif.Checked = ProgramSettings.DrawAllWidgets;

            if (ProgramSettings.language.Length > 1) comboBox_Language.Text = ProgramSettings.language;


            Settings_Load = false;
            JSON_Modified = false;


            StartJsonPreview();
            SetPreferences(userCtrl_Set1);
            PreviewView = true;
            Logger.WriteLine("* Form1_Load (end)");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Logger.WriteLine("* Form1_Shown");
            Logger.WriteLine("Загружаем файл из значения аргумента " + StartFileNameJson);
            if ((StartFileNameJson != null) && (StartFileNameJson.Length > 0))
            {
                Logger.WriteLine("Загружаем Json файл из значения аргумента " + StartFileNameJson);
                FileName = Path.GetFileName(StartFileNameJson);
                FullFileDir = Path.GetDirectoryName(StartFileNameJson);
                button_Add_Images.Enabled = true;
                LoadJson(StartFileNameJson);
                StartFileNameJson = "";
            }
            //else if ((StartFileNameBin != null) && (StartFileNameBin.Length > 0))
            //{
            //    Logger.WriteLine("Загружаем bin файл из значения аргумента " + StartFileNameBin);
            //    zip_unpack_bin(StartFileNameBin);
            //    StartFileNameBin = "";
            //}
            //JSON_Modified = false;
            //FormText();
            //Logger.WriteLine("Загрузили файл из значения аргумента " + StartFileNameJson);

            // изменяем размер панели для предпросмотра если она не влазит
            if (pictureBox_Preview.Top + pictureBox_Preview.Height > radioButton_GTR3.Top)
            {
                float newHeight = radioButton_GTR3.Top - pictureBox_Preview.Top;
                float scale = newHeight / pictureBox_Preview.Height;
                pictureBox_Preview.Size = new Size((int)(pictureBox_Preview.Width * scale), (int)(pictureBox_Preview.Height * scale));
            }

            userCtrl_Background_Options.AutoSize = true;
            uCtrl_Text_Opt.AutoSize = true;

            button_CreatePreview.Location = new Point(5, 563);
            Logger.WriteLine("* Form1_Shown(end)");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            Logger.WriteLine("* FormClosing");
#if !DEBUG
            SaveRequest();
#endif
        }

        private void SetLanguage()
        {
            Logger.WriteLine("* SetLanguage");
            if (ProgramSettings.language == "English")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            }
            else if (ProgramSettings.language == "Español")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
            }
            else if (ProgramSettings.language == "Português")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("pt");
            }
            else if (ProgramSettings.language == "Čeština")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("cs");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("cs");
            }
            else if (ProgramSettings.language == "Magyar")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hu");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("hu");
            }
            else if (ProgramSettings.language == "Slovenčina")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("sk");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("sk");
            }
            else if (ProgramSettings.language == "French")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("fr");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr");
            }
            else if (ProgramSettings.language == "Chinese/简体中文")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("zh");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh");
            }
            else if (ProgramSettings.language == "Italian" || ProgramSettings.language == "Italiano")
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("it");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("it");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru");
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

            ProgramSettings.ShowBorder = checkBox_border.Checked;
            ProgramSettings.Crop = checkBox_crop.Checked;
            ProgramSettings.Show_CircleScale_Area = checkBox_CircleScaleImage.Checked;
            ProgramSettings.Shortcuts_Center_marker = checkBox_center_marker.Checked;
            ProgramSettings.Show_Widgets_Area = checkBox_WidgetsArea.Checked;
            ProgramSettings.Show_Shortcuts = checkBox_Show_Shortcuts.Checked;

            ProgramSettings.language = comboBox_Language.Text;

            ProgramSettings.Model_GTR3 = radioButton_GTR3.Checked;
            ProgramSettings.Model_GTR3_Pro = radioButton_GTR3_Pro.Checked;
            ProgramSettings.Model_GTS3 = radioButton_GTS3.Checked;

            if (radioButton_GTR3.Checked) ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
            if (radioButton_GTR3_Pro.Checked) ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
            if (radioButton_GTS3.Checked) ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;



            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
        }

        private void comboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            if (radioButton_GTR3.Checked)
            {
                FormName = "GTR 3 watch face editor";
            }
            else if (radioButton_GTS3.Checked)
            {
                FormName = "GTS 3 watch face editor";
            }
            else if (radioButton_GTR3_Pro.Checked)
            {
                FormName = "GTR 3 Pro watch face editor";
            }
            else if (radioButton_GTS3.Checked)
            {
                FormName = "GTS 3 watch face editor";
            }

            if (FormNameSufix.Length == 0)
            {
                this.Text = FormName;
                button_OpenDir.Enabled = false;
            }
            else
            {
                if (JSON_Modified) FormNameSufix = FormNameSufix + "*";
                this.Text = FormName + " (" + FormNameSufix + ")";
                button_OpenDir.Enabled = true;
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

                if (radioButton_GTR3.Checked)
                {
                    ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
                }
                else if (radioButton_GTR3_Pro.Checked)
                {
                    ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
                }
                else if (radioButton_GTS3.Checked)
                {
                    ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;
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
            if (e.Data.GetDataPresent(typeof(UCtrl_Background_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(UCtrl_DigitalTime_Elm))) typeReturn = false;
            if (e.Data.GetDataPresent(typeof(Button))) typeReturn = false;
            if (typeReturn) return;

            e.Effect = e.AllowedEffect;
            Panel draggedPanel = (Panel)e.Data.GetData(typeof(Panel));
            if (draggedPanel == null)
            {
                if (e.Data.GetDataPresent(typeof(UCtrl_Background_Elm)))
                {
                    UCtrl_Background_Elm draggedUCtrl_Background_Elm = (UCtrl_Background_Elm)e.Data.GetData(typeof(UCtrl_Background_Elm));
                    if (draggedUCtrl_Background_Elm != null) draggedPanel = (Panel)draggedUCtrl_Background_Elm.Parent;
                }
                else if (e.Data.GetDataPresent(typeof(UCtrl_DigitalTime_Elm)))
                {
                    UCtrl_DigitalTime_Elm draggedUCtrl_Background_Elm = (UCtrl_DigitalTime_Elm)e.Data.GetData(typeof(UCtrl_DigitalTime_Elm));
                    if (draggedUCtrl_Background_Elm != null) draggedPanel = (Panel)draggedUCtrl_Background_Elm.Parent;
                }
                else if (e.Data.GetDataPresent(typeof(Button)))
                {
                    Button draggedUCtrl_Background_Elm = (Button)e.Data.GetData(typeof(Button));
                    if (draggedUCtrl_Background_Elm != null) draggedPanel = (Panel)draggedUCtrl_Background_Elm.Parent;
                }
            }
            if (draggedPanel == null) return;

            Point pt = tableLayoutPanel_ElemetsWatchFace.PointToClient(new Point(e.X, e.Y));
            Control control = tableLayoutPanel_ElemetsWatchFace.GetChildAtPoint(pt);


            if (control != null)
            {
                var pos = tableLayoutPanel_ElemetsWatchFace.GetPositionFromControl(control);
                var posOld = tableLayoutPanel_ElemetsWatchFace.GetPositionFromControl(draggedPanel);
                //tableLayoutPanel1.Controls.Add(draggedButton, pos.Column, pos.Row);

                if (pos != posOld)
                {
                    if (pt.Y < control.Location.Y + draggedPanel.Height * 0.9)
                    {
                        tableLayoutPanel_ElemetsWatchFace.SetRow(draggedPanel, pos.Row);
                        if (pos.Row < posOld.Row) tableLayoutPanel_ElemetsWatchFace.SetRow(control, pos.Row + 1);
                        else tableLayoutPanel_ElemetsWatchFace.SetRow(control, pos.Row - 1);
                    }
                }

                //if (pos != posOld && pos.Row < posOld.Row)
                //{
                //    if (pt.Y < control.Location.Y +control.Height * 0.4)
                //    {
                //        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                //        if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                //        else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                //    }
                //}
                //if (pos != posOld && pos.Row > posOld.Row)
                //{
                //    if (pt.Y > control.Location.Y + control.Height * 0.6)
                //    {
                //        tableLayoutPanel1.SetRow(draggedPanel, pos.Row);
                //        if (pos.Row < posOld.Row) tableLayoutPanel1.SetRow(control, pos.Row + 1);
                //        else tableLayoutPanel1.SetRow(control, pos.Row - 1);
                //    }
                //}
                draggedPanel.Tag = null;
            }
        }

        private void tableLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void ShowElemenrOptions(string optionsName)
        {
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
            }
        }

        /// <summary>Скрывает все панели с настройками элементов</summary>
        private void HideAllElemenrOptions()
        {
            userCtrl_Background_Options.Visible = false;
            uCtrl_Text_Opt.Visible = false;
        }

        private void uCtrl_Background_Elm_SelectChanged(object sender, EventArgs eventArgs)
        {
            uCtrl_DigitalTime_Elm.ResetHighlightState();

            string preview = "";
            int id = 0;
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null) 
            {
                preview = Watch_Face.WatchFace_Info.Preview;
                id = Watch_Face.WatchFace_Info.WatchFaceId;
            }
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
            Read_Background_Options(background, preview, id);
            ShowElemenrOptions("Background");
        }

        private void uCtrl_DigitalTime_Elm1_SelectChanged(object sender, EventArgs eventArgs)
        {
            string selectElement = uCtrl_DigitalTime_Elm.selectedElement;
            if(selectElement.Length == 0) HideAllElemenrOptions();
            uCtrl_Background_Elm.ResetHighlightState();

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
                string selectedElement = uCtrl_DigitalTime_Elm.selectedElement;
                hmUI_widget_IMG_NUMBER img_number = null;

                switch (selectedElement)
                {
                    case "Hour":
                        if (uCtrl_DigitalTime_Elm.checkBox_Hours.Checked)
                        {
                            img_number = digitalTime.Hour;
                            Read_ImgNumber_Options(img_number, false, false, "", false, false, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Minute":
                        if (uCtrl_DigitalTime_Elm.checkBox_Minutes.Checked)
                        {
                            img_number = digitalTime.Minute;
                            Read_ImgNumber_Options(img_number, false, true, Properties.FormStrings.FollowMinute, false, false, true);
                            ShowElemenrOptions("Text");
                        }
                        else HideAllElemenrOptions();
                        break;
                    case "Second":
                        if (uCtrl_DigitalTime_Elm.checkBox_Seconds.Checked)
                        {
                            img_number = digitalTime.Second;
                            Read_ImgNumber_Options(img_number, false, true, Properties.FormStrings.FollowSecond, false, false, true);
                            ShowElemenrOptions("Text"); 
                        }
                        else HideAllElemenrOptions();
                        break;
                }

            }

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void button_JSON_Click(object sender, EventArgs e)
        {
            Logger.WriteLine("* JSON");
            // сохранение если файл не сохранен
            SaveRequest();

            //string subPath = Application.StartupPath + @"\Watch_face\";
            //if (!Directory.Exists(subPath)) Directory.CreateDirectory(subPath);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FullFileDir;
            openFileDialog.FileName = FileName;
            openFileDialog.Filter = Properties.FormStrings.FilterJson;
            //openFileDialog.Filter = "Json files (*.json) | *.json";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_Open;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = Path.GetFileName(openFileDialog.FileName);
                FullFileDir = Path.GetDirectoryName(openFileDialog.FileName);

                Logger.WriteLine("* JSON_Click");
                string newFullName = openFileDialog.FileName;
                //string dirName = Path.GetDirectoryName(newFullName) + @"\assets\";
                button_Add_Images.Enabled = true;

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
            fileName = Path.Combine(dirName, "Preview.States");
            if (File.Exists(fileName))
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
            else StartJsonPreview();

            LoadImage(dirName);
            ShowElemetsWatchFace();
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null && Watch_Face.WatchFace_Info.DeviceName != null)
            {
                switch (Watch_Face.WatchFace_Info.DeviceName)
                {
                    case "GTR3":
                        radioButton_GTR3.Checked = true;
                        break;
                    case "GTR3_Pro":
                        radioButton_GTR3_Pro.Checked = true;
                        break;
                    case "GTS3":
                        radioButton_GTS3.Checked = true;
                        break;
                }
            }
            PreviewView = true;

            JSON_Modified = false;
            PreviewImage();
            FormText();
        }

        /// <summary>Загружаем файлы изображений в проект и в выпадающие списки</summary>
        /// <param name="dirName">Папка с изображениями</param>
        private void LoadImage(string dirName)
        {
            Logger.WriteLine("* LoadImage");
            if (!Directory.Exists(dirName)) return;

            dataGridView_ImagesList.Rows.Clear();
            ListImages.Clear();
            ListImagesFullName.Clear();

            DirectoryInfo Folder;
            Folder = new DirectoryInfo(dirName);
            //FileInfo[] Images;
            //Images = Folder.GetFiles("*.png").OrderBy(p => Path.GetFileNameWithoutExtension(p.Name)).ToArray();
            FileInfo[] Images = Folder.GetFiles("*.png");
            Images = FileInfoSort(Images);
            //Array.Sort(Images, new MyCustomComparer()); выдает ошибку
            Image loadedImage = null; 
            int count = 1;
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
            uCtrl_Text_Opt.ComboBoxAddItems(ListImages, ListImagesFullName);
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
            SaveRequest();

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
                if (radioButton_GTR3.Checked)
                {
                    Watch_Face.WatchFace_Info.DeviceName = "GTR3";

                    //Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                }
                else if (radioButton_GTR3_Pro.Checked)
                {
                    Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";

                    //Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 480;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 480;
                }
                else if (radioButton_GTS3.Checked)
                {
                    Watch_Face.WatchFace_Info.DeviceName = "GTS3";

                    //Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
                }

                string JSON_String = JsonConvert.SerializeObject(Watch_Face, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(fullfilename, JSON_String, Encoding.UTF8);
                button_Add_Images.Enabled = true;
                Directory.CreateDirectory(dirName);
                LoadImage(dirName);
                PreviewView = false;
                ShowElemetsWatchFace();
                PreviewView = true;

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
            openFileDialog.Title = Properties.FormStrings.Dialog_Title_Open;
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
                    saveFileDialog.Title = Properties.FormStrings.Dialog_Title_Open;
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
            Logger.WriteLine("* PreviewImage");
            if (!PreviewView) return;
            //Graphics gPanel = panel_Preview.CreateGraphics();
            //gPanel.Clear(panel_Preview.BackColor);
            float scale = 1.0f;
            //if (panel_Preview.Height < 300) scale = 0.5f;
            #region BackgroundImage
            Logger.WriteLine("BackgroundImage");
            Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
            if (radioButton_GTR3_Pro.Checked)
            {
                bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
            }
            else if (radioButton_GTS3.Checked)
            {
                bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
            }
            Graphics gPanel = Graphics.FromImage(bitmap);
            #endregion

            Logger.WriteLine("Preview_screen");
            int link = radioButton_ScreenNormal.Checked ? 0 : 1;
            Preview_screen(gPanel, scale, checkBox_crop.Checked, checkBox_WebW.Checked, checkBox_WebB.Checked,
                checkBox_border.Checked, checkBox_Show_Shortcuts.Checked, checkBox_Shortcuts_Area.Checked,
                checkBox_Shortcuts_Border.Checked, true, checkBox_CircleScaleImage.Checked,
                checkBox_center_marker.Checked, checkBox_WidgetsArea.Checked, link);
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
                    if (Form_Preview.Model_Wath.model_GTR3 != radioButton_GTR3.Checked)
                        Form_Preview.Model_Wath.model_GTR3 = radioButton_GTR3.Checked;
                    if (Form_Preview.Model_Wath.model_GTR3_Pro != radioButton_GTR3_Pro.Checked)
                        Form_Preview.Model_Wath.model_GTR3_Pro = radioButton_GTR3_Pro.Checked;
                    if (Form_Preview.Model_Wath.model_GTS3 != radioButton_GTS3.Checked)
                        Form_Preview.Model_Wath.model_GTS3 = radioButton_GTS3.Checked;
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
                    if (radioButton_GTR3_Pro.Checked)
                    {
                        bitmapPreviewResize = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                    }
                    if (radioButton_GTS3.Checked)
                    {
                        bitmapPreviewResize = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
                    }
                    Graphics gPanelPreviewResize = Graphics.FromImage(bitmapPreviewResize);
                    #endregion

                    int link_aod = radioButton_ScreenNormal.Checked ? 0 : 1;
                    Preview_screen(gPanelPreviewResize, 1, checkBox_crop.Checked,
                        checkBox_WebW.Checked, checkBox_WebB.Checked, checkBox_border.Checked,
                        checkBox_Show_Shortcuts.Checked, checkBox_Shortcuts_Area.Checked, checkBox_Shortcuts_Border.Checked, true,
                        checkBox_CircleScaleImage.Checked, checkBox_center_marker.Checked, checkBox_WidgetsArea.Checked, link_aod);
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
                    uCtrl_Text_Opt.SetMouseСoordinates(MouseClickСoordinates.X, MouseClickСoordinates.Y);

                };
            }

            if (Form_Preview.Model_Wath.model_GTR3 != radioButton_GTR3.Checked)
                Form_Preview.Model_Wath.model_GTR3 = radioButton_GTR3.Checked;
            if (Form_Preview.Model_Wath.model_GTR3_Pro != radioButton_GTR3_Pro.Checked)
                Form_Preview.Model_Wath.model_GTR3_Pro = radioButton_GTR3_Pro.Checked;
            if (Form_Preview.Model_Wath.model_GTS3 != radioButton_GTS3.Checked)
                Form_Preview.Model_Wath.model_GTS3 = radioButton_GTS3.Checked;
            formPreview.radioButton_CheckedChanged(sender, e);
            float scale = 1.0f;

            #region BackgroundImage 
            Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
            if (radioButton_GTR3_Pro.Checked)
            {
                bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
            }
            if (radioButton_GTS3.Checked)
            {
                bitmap = new Bitmap(Convert.ToInt32(390), Convert.ToInt32(450), PixelFormat.Format32bppArgb);
            }
            Graphics gPanel = Graphics.FromImage(bitmap);
            #endregion

            int link = radioButton_ScreenNormal.Checked ? 0 : 1;
            Preview_screen(gPanel, scale, checkBox_crop.Checked, checkBox_WebW.Checked, checkBox_WebB.Checked,
                checkBox_border.Checked, checkBox_Show_Shortcuts.Checked, checkBox_Shortcuts_Area.Checked,
                checkBox_Shortcuts_Border.Checked, true, checkBox_CircleScaleImage.Checked,
                checkBox_center_marker.Checked, checkBox_WidgetsArea.Checked, link);
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
            if (comboBox_AddTime.SelectedIndex == 1) AddDigitalTime();
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddTime.Items.Insert(0, Properties.FormStrings.Elemet_Time);
            comboBox_AddTime.SelectedIndex = 0;
            ShowElemetsWatchFace();
            PreviewView = true;
            JSON_Modified = true;
            FormText();
        }

        private void comboBox_AddDate_DropDownClosed(object sender, EventArgs e)
        {
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddDate.Items.Insert(0, Properties.FormStrings.Elemet_Date);
            comboBox_AddDate.SelectedIndex = 0;
            PreviewView = true;
        }

        private void comboBox_AddActivity_DropDownClosed(object sender, EventArgs e)
        {
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddActivity.Items.Insert(0, Properties.FormStrings.Elemet_Activity);
            comboBox_AddActivity.SelectedIndex = 0;
            PreviewView = true;
        }

        private void comboBox_AddAir_DropDownClosed(object sender, EventArgs e)
        {
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddAir.Items.Insert(0, Properties.FormStrings.Elemet_Air);
            comboBox_AddAir.SelectedIndex = 0;
            PreviewView = true;
        }

        private void comboBox_AddSystem_DropDownClosed(object sender, EventArgs e)
        {
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddSystem.Items.Insert(0, Properties.FormStrings.Elemet_System);
            comboBox_AddSystem.SelectedIndex = 0;
            PreviewView = true;
        }

        private void SaveRequest()
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
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show(Properties.FormStrings.Message_Save_new_JSON,
                        Properties.FormStrings.Message_Save_JSON_Modified_Caption,
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
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
                        saveFileDialog.Title = Properties.FormStrings.Dialog_Title_Open;
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
                        else return;
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
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
                if (radioButton_GTR3.Checked)
                {
                    //Watch_Face.WatchFace_Info.DeviceName = "GTR3";

                    //Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                }
                else if (radioButton_GTR3_Pro.Checked)
                {
                    //Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";

                    //Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 480;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 480;
                }
                else if (radioButton_GTS3.Checked)
                {
                    //Watch_Face.WatchFace_Info.DeviceName = "GTS3";

                    //Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 450;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 390;
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
                if (radioButton_GTR3.Checked)
                {
                    //Watch_Face.WatchFace_Info.DeviceName = "GTR3";

                    //Watch_Face.ScreenAOD.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.h = 454;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.w = 454;
                }
                else if (radioButton_GTR3_Pro.Checked)
                {
                    //Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";

                    //Watch_Face.ScreenAOD.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.h = 480;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.w = 480;
                }
                else if (radioButton_GTS3.Checked)
                {
                    //Watch_Face.WatchFace_Info.DeviceName = "GTS3";

                    //Watch_Face.ScreenAOD.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenAOD.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenAOD.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.h = 450;
                    Watch_Face.ScreenAOD.Background.BackgroundColor.w = 390;
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
            //if (!exists) Elements.Add(digitalTime);
            if (!exists) Elements.Insert(0, digitalTime);
            uCtrl_DigitalTime_Elm.SettingsClear();
        }

        /// <summary>Отображаем элемынты в соответствии с json файлом</summary>
        private void ShowElemetsWatchFace()
        {
            HideAllElemenrOptions();
            uCtrl_Background_Elm.ResetHighlightState();
            uCtrl_DigitalTime_Elm.ResetHighlightState();

            panel_UC_Background.Visible = false;
            panel_UC_DigitalTime.Visible = false;


            int count = tableLayoutPanel_ElemetsWatchFace.RowCount;

            if (Watch_Face == null) return;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) return;
                if (Watch_Face.ScreenNormal.Background != null) 
                {
                    uCtrl_Background_Elm.Visible_ShowDel(false);
                    panel_UC_Background.Visible = true;
                }
            }
            else
            {
                if (Watch_Face.ScreenAOD == null) return;
                if (Watch_Face.ScreenAOD.Background != null)
                {
                    uCtrl_Background_Elm.Visible_ShowDel(true);
                    uCtrl_Background_Elm.SetVisibilityElementStatus(Watch_Face.ScreenAOD.Background.visible);
                    panel_UC_Background.Visible = true;
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
                        case "ElementDigitalTime":
                            ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                            uCtrl_DigitalTime_Elm.SetVisibilityElementStatus(DigitalTime.visible);
                            Dictionary<int, string> elementOptions = new Dictionary<int, string>();
                            if (DigitalTime.Second != null)
                            {
                                uCtrl_DigitalTime_Elm.checkBox_Seconds.Checked = DigitalTime.Second.visible;
                                elementOptions.Add(DigitalTime.Second.position, "Second");
                            }
                            if (DigitalTime.Minute != null)
                            {
                                uCtrl_DigitalTime_Elm.checkBox_Minutes.Checked = DigitalTime.Minute.visible;
                                elementOptions.Add(DigitalTime.Minute.position, "Minute");
                            }
                            if (DigitalTime.Hour != null)
                            {
                                uCtrl_DigitalTime_Elm.checkBox_Hours.Checked = DigitalTime.Hour.visible;
                                elementOptions.Add(DigitalTime.Hour.position, "Hour");
                            }
                            if (DigitalTime.AmPm != null)
                            {
                                uCtrl_DigitalTime_Elm.checkBox_AmPm.Checked = DigitalTime.AmPm.visible;
                                elementOptions.Add(DigitalTime.AmPm.position, "AmPm");
                            }
                                
                            uCtrl_DigitalTime_Elm.SetOptionsPosition(elementOptions);

                            panel_UC_DigitalTime.Visible = true;
                            SetElementPositionInGUI(type, count - i - 2);
                            //SetElementPositionInGUI(type, i + 1);
                            break;
                    }
                }
            }
        }

        /// <summary>Перемещаем элемен в нужную позицию</summary>
        private void SetElementPositionInGUI(string type, int position)
        {
            if (position >= tableLayoutPanel_ElemetsWatchFace.RowCount || position < 0) return;
            Control panel = null;
            switch (type)
            {
                case "ElementDigitalTime":
                    panel = panel_UC_DigitalTime;
                    break;
            }
            if (panel == null) return;
            int realPos = tableLayoutPanel_ElemetsWatchFace.GetRow(panel);
            if (realPos == position) return;
            if (realPos < position)
            {
                for(int i= realPos;i< position; i++)
                {
                    Control panel2 = tableLayoutPanel_ElemetsWatchFace.GetControlFromPosition(0, i + 1);
                    if (panel2 == null) return;
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

            ProgramSettings.ShowBorder = checkBox_border.Checked;
            ProgramSettings.Crop = checkBox_crop.Checked;
            ProgramSettings.Show_CircleScale_Area = checkBox_CircleScaleImage.Checked;
            ProgramSettings.Shortcuts_Center_marker = checkBox_center_marker.Checked;
            ProgramSettings.Show_Widgets_Area = checkBox_WidgetsArea.Checked;
            ProgramSettings.Show_Shortcuts = checkBox_Show_Shortcuts.Checked;

            ProgramSettings.language = comboBox_Language.Text;

            ProgramSettings.Model_GTR3 = radioButton_GTR3.Checked;
            ProgramSettings.Model_GTR3_Pro = radioButton_GTR3_Pro.Checked;
            ProgramSettings.Model_GTS3 = radioButton_GTS3.Checked;

            if (radioButton_GTR3.Checked) ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
            if (radioButton_GTR3_Pro.Checked) ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
            if (radioButton_GTS3.Checked) ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;



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

            ProgramSettings.ShowBorder = checkBox_border.Checked;
            ProgramSettings.Crop = checkBox_crop.Checked;
            ProgramSettings.Show_CircleScale_Area = checkBox_CircleScaleImage.Checked;
            ProgramSettings.Shortcuts_Center_marker = checkBox_center_marker.Checked;
            ProgramSettings.Show_Widgets_Area = checkBox_WidgetsArea.Checked;
            ProgramSettings.Show_Shortcuts = checkBox_Show_Shortcuts.Checked;

            ProgramSettings.language = comboBox_Language.Text;

            ProgramSettings.Model_GTR3 = radioButton_GTR3.Checked;
            ProgramSettings.Model_GTR3_Pro = radioButton_GTR3_Pro.Checked;
            ProgramSettings.Model_GTS3 = radioButton_GTS3.Checked;

            if (radioButton_GTR3.Checked) ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
            if (radioButton_GTR3_Pro.Checked) ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;
            if (radioButton_GTS3.Checked) ProgramSettings.WatchSkin_GTS_3 = textBox_WatchSkin_Path.Text;



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

            uCtrl_DigitalTime_Elm1_SelectChanged(sender, eventArgs);

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

        private void uCtrl_DigitalTime_Elm_DelElement(object sender, EventArgs eventArgs)
        {
            List<object> Elements = new List<object>();
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

            bool exists = Elements.Exists(e => e.GetType().Name == "ElementDigitalTime");
            if (exists)
            {
                int index = Elements.FindIndex(e => e.GetType().Name == "ElementDigitalTime");
                Elements.RemoveAt(index);

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
            if (fileInfo.Length < 2) return fileInfo;
            for (int i = 0; i < fileInfo.Length - 1; i++)
            {
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
            return fileInfo;
        }

        public int FileInfoCompare(FileInfo fileInfo1, FileInfo fileInfo2)
        {
            // разделяем на блоки
            string name1 = fileInfo1.Name;
            string name2 = fileInfo2.Name;
            name1 = Path.GetFileNameWithoutExtension(name1);
            name2 = Path.GetFileNameWithoutExtension(name2);
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
            Console.WriteLine("Compare1=" + toCompare1);
            Console.WriteLine("Compare2=" + toCompare2);
            Console.WriteLine("return=" + ret.ToString());
            Console.WriteLine(" ");

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
            int calories = rnd.Next(0, 2500);
            int pulse = rnd.Next(45, 150);
            int distance = rnd.Next(0, 15000);
            int steps = rnd.Next(0, 15000);
            int goal = rnd.Next(0, 15000);
            int pai = rnd.Next(0, 150);
            int standUp = rnd.Next(0, 13);
            int stress = rnd.Next(0, 13);
            int fatBurning = rnd.Next(0, 13);
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
            int humidity = rnd.Next(0, 100);
            int UVindex = rnd.Next(0, 13);
            int windForce = rnd.Next(0, 13);

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

        // записываем параметры в JsonPreview
        private void button_JsonPreview_Write_Click(object sender, EventArgs e)
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
                    ps.Altitude = Air["Altitude"];
                    ps.AirPressure = Air["AirPressure"];


                    ps.Bluetooth = checkValue["Bluetooth"];
                    ps.Alarm = checkValue["Alarm"];
                    ps.Unlocked = checkValue["Lock"];
                    ps.DoNotDisturb = checkValue["DND"];

                    ps.ShowTemperature = checkValue["ShowTemperature"];

                    if (ps.Calories != 1234)
                    {
                        Array.Resize(ref objson, objson.Length + 1);
                        objson[count] = ps;
                        count++;
                    }
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

        private void button_JsonPreview_Read_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FullFileDir;
            //openFileDialog.Filter = Properties.FormStrings.FilterJson;
            openFileDialog.FileName = "Preview.States";
            openFileDialog.Filter = "PreviewStates file | *.States|Json files (*.json) | *.json";
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
                    ps.Altitude = Air["Altitude"];
                    ps.AirPressure = Air["AirPressure"];


                    ps.Bluetooth = checkValue["Bluetooth"];
                    ps.Alarm = checkValue["Alarm"];
                    ps.Unlocked = checkValue["Lock"];
                    ps.DoNotDisturb = checkValue["DND"];

                    ps.ShowTemperature = checkValue["ShowTemperature"];

                    if (ps.Calories != 1234)
                    {
                        Array.Resize(ref objson, objson.Length + 1);
                        objson[count] = ps;
                        count++;
                    }
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

        private void radioButton_Model_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && !radioButton.Checked) return;
            if (radioButton_GTR3.Checked)
            {
                pictureBox_Preview.Size = new Size((int)(230 * currentDPI), (int)(230 * currentDPI));
            }
            else if (radioButton_GTR3_Pro.Checked)
            {
                //pictureBox_Preview.Size = new Size((int)(243 * currentDPI), (int)(243 * currentDPI));
                pictureBox_Preview.Size = new Size((int)(230 * currentDPI), (int)(230 * currentDPI));
            }
            else if (radioButton_GTS3.Checked)
            {
                pictureBox_Preview.Size = new Size((int)(198 * currentDPI), (int)(228 * currentDPI));
            }

            // изменяем размер панели для предпросмотра если она не влазит
            if (pictureBox_Preview.Top + pictureBox_Preview.Height > radioButton_GTR3.Top)
            {
                float newHeight = radioButton_GTR3.Top - pictureBox_Preview.Top;
                float scale = newHeight / pictureBox_Preview.Height;
                pictureBox_Preview.Size = new Size((int)(pictureBox_Preview.Width * scale), (int)(pictureBox_Preview.Height * scale));
            }

            if ((formPreview != null) && (formPreview.Visible))
            {
                if (Form_Preview.Model_Wath.model_GTR3 != radioButton_GTR3.Checked)
                    Form_Preview.Model_Wath.model_GTR3 = radioButton_GTR3.Checked;
                if (Form_Preview.Model_Wath.model_GTR3_Pro != radioButton_GTR3_Pro.Checked)
                    Form_Preview.Model_Wath.model_GTR3_Pro = radioButton_GTR3_Pro.Checked;
                if (Form_Preview.Model_Wath.model_GTS3 != radioButton_GTS3.Checked)
                    Form_Preview.Model_Wath.model_GTS3 = radioButton_GTS3.Checked;
                formPreview.radioButton_CheckedChanged(sender, e);
            }

            if (Settings_Load) return;

            ProgramSettings.Model_GTR3 = radioButton_GTR3.Checked;
            ProgramSettings.Model_GTR3_Pro = radioButton_GTR3_Pro.Checked;
            ProgramSettings.Model_GTS3 = radioButton_GTS3.Checked;

            string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);

            PreviewImage();
            JSON_Modified = true;
            FormText();

            //JSON_write();
            //PreviewImage();
        }

        private void button_pack_zip_Click(object sender, EventArgs e)
        {
            // сохранение если файл не сохранен
            SaveRequest();

            if (FullFileDir == null) return;
            string tempDir = Application.StartupPath + @"\Temp";
            string templatesFileDir = Application.StartupPath + @"\File_templates";
            //goto link;
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
            foreach (FileInfo file in Images)
            {
                progressBar1.Value++;
                string fileNameFull = PngToTga(file.FullName, tempDir + @"\assets");
                if (fileNameFull != null) ImageFix(fileNameFull);
            }

            string appText = File.ReadAllText(templatesFileDir + @"\app.json");
            appText = appText.Replace("\"appName\": \"New_Project\"", 
                "\"appName\": \"" + Path.GetFileNameWithoutExtension(FileName) + "\"");
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null)
            {
                if (Watch_Face.WatchFace_Info.WatchFaceId > 999 && Watch_Face.WatchFace_Info.WatchFaceId < 10000000)
                {
                    appText = appText.Replace("\"appId\": 12345678",
                                    "\"appId\": " + Watch_Face.WatchFace_Info.WatchFaceId.ToString());
                }
                if (Watch_Face.WatchFace_Info.Preview != null && Watch_Face.WatchFace_Info.Preview.Length > 0)
                {
                    appText = appText.Replace("\"icon\": \"preview.png\"",
                                    "\"icon\": \"" + Watch_Face.WatchFace_Info.Preview + ".png\"");
                }
            }
            File.WriteAllText(tempDir + @"\app.json", appText, Encoding.UTF8);
            File.Copy(templatesFileDir + @"\app.js", tempDir + @"\app.js");

            string variables = "";
            string items = "";
            JsonToJS(out variables, out items);

            string indexText = File.ReadAllText(templatesFileDir + @"\index.js");
            string versionText = "v " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            indexText = indexText.Replace("* Watch_Face_Editor tool v1.x", "* Watch_Face_Editor tool " + versionText);

            if (variables.Length>0) indexText = indexText.Replace("//Variable declaration section", variables);
            if (items.Length > 0) indexText = indexText.Replace("//Item description section", items);
            indexText = indexText.Replace("\r", "");

            File.WriteAllText(tempDir + @"\watchface\index.js", indexText, Encoding.UTF8);
            //link:
            // объединяем все в архив
            string startPath = tempDir;
            string zipPath = FullFileDir + @"\" + Path.GetFileNameWithoutExtension(FileName) + ".zip";
            if (File.Exists(zipPath)) File.Delete(zipPath);
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
            //if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
            progressBar1.Visible = false;
        }

        /// <summary>Преобразуем Png в Tga</summary>
        private string PngToTga(string fileNameFull, string targetFolder)
        {
            if (File.Exists(fileNameFull))
            {
                colorMapList.Clear();
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(fileNameFull);
                    //string path = Path.GetDirectoryName(fileNameFull);
                    ImageMagick.MagickImage image = new ImageMagick.MagickImage(fileNameFull);
                    ImageMagick.MagickImage image_temp = new ImageMagick.MagickImage(fileNameFull);
                    ImageWidth = image.Width;
                    int newWidth = ImageWidth;
                    int newHeight = image.Height;
                    while (newWidth % 16 != 0)
                    {
                        newWidth++;
                    }

                    if (ImageWidth != newWidth)
                    {
                        Bitmap bitmap = image.ToBitmap();
                        Bitmap bitmapNew = new Bitmap(newWidth, newHeight);
                        Graphics gfx = Graphics.FromImage(bitmapNew);
                        gfx.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                        image = new ImageMagick.MagickImage(bitmapNew);
                        image_temp = new ImageMagick.MagickImage(bitmapNew);
                    }
                    image.ColorType = ImageMagick.ColorType.Palette;
                    if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                    {
                        image = image_temp;
                        ImageMagick.Pixel pixel = image.GetPixels().GetPixel(0, 0);
                        ushort[] p;
                        if (pixel[2] > 256)
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256), pixel[3] };
                            else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] - 256) };
                        }
                        else
                        {
                            if (pixel.Channels == 4) p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256), pixel[3] };
                            else p = new ushort[] { pixel[0], pixel[1], (ushort)(pixel[2] + 256) };
                        }
                        image.GetPixels().SetPixel(0, 0, p);
                        pixel = image.GetPixels().GetPixel(0, 0);
                        image.ColorType = ImageMagick.ColorType.Palette;
                        pixel = image.GetPixels().GetPixel(0, 0);
                        if (image.ColorSpace != ImageMagick.ColorSpace.sRGB)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_Image32bit +
                                Environment.NewLine + fileNameFull, Properties.FormStrings.Message_Warning_Caption,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                    }

                    for (int i = 0; i < image.ColormapSize; i++)
                    {

                        colorMapList.Add(image.GetColormap(i));
                    }
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }
                    string newFileName = Path.Combine(targetFolder, fileName + ".tga");
                    image.Write(newFileName, ImageMagick.MagickFormat.Tga);
                    return newFileName;

                }
                catch (Exception exp)
                {
                    MessageBox.Show(Properties.FormStrings.Message_Wrong_Original_Image + Environment.NewLine + exp);
                }
            }
            return null;
        }

        private void ImageFix(string fileNameFull)
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
                        ColorMap.ColorsFix(argb_brga, colorMapCount, colorMapEntrySize);
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
            if (Watch_Face == null || Watch_Face.WatchFace_Info == null || Watch_Face.WatchFace_Info.Preview == null) return;
            if (Watch_Face.WatchFace_Info.Preview != null && Watch_Face.WatchFace_Info.Preview.Length > 0)
            {
                string preview = FullFileDir + @"\assets\" + Watch_Face.WatchFace_Info.Preview + ".png";
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");
                int PreviewHeight = 306;
                if (radioButton_GTR3_Pro.Checked)
                {
                    bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                    mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png.png");
                    PreviewHeight = 324;
                }
                if (radioButton_GTS3.Checked)
                {
                    bitmap = new Bitmap(Convert.ToInt32(348), Convert.ToInt32(442), PixelFormat.Format32bppArgb);
                    mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                    PreviewHeight = 306;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, true, false, false, false, link);
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
                if (radioButton_GTR3_Pro.Checked)
                {
                    bitmap = new Bitmap(Convert.ToInt32(480), Convert.ToInt32(480), PixelFormat.Format32bppArgb);
                    mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3_pro.png.png");
                    PreviewHeight = 324;
                }
                if (radioButton_GTS3.Checked)
                {
                    bitmap = new Bitmap(Convert.ToInt32(348), Convert.ToInt32(442), PixelFormat.Format32bppArgb);
                    mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gts_3.png");
                    PreviewHeight = 306;
                }
                Graphics gPanel = Graphics.FromImage(bitmap);
                int link = radioButton_ScreenNormal.Checked ? 0 : 1;
                Preview_screen(gPanel, 1.0f, false, false, false, false, false, false, false, true, false, false, false, link);
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

                LoadImage(Path.GetDirectoryName(PathPreview));
                //ListImages.Add(fileNameOnly);
                //ListImagesFullName.Add(PathPreview);

                //// добавляем строки в таблицу
                ////Image PreviewImage = Image.FromHbitmap(bitmap.GetHbitmap());
                //Image PreviewImage = null;
                //using (FileStream stream = new FileStream(PathPreview, FileMode.Open, FileAccess.Read))
                //{
                //    PreviewImage = Image.FromStream(stream);
                //}
                //i = dataGridView_ImagesList.Rows.Count + 1;
                //var RowNew = new DataGridViewRow();
                //DataGridViewImageCellLayout ZoomType = DataGridViewImageCellLayout.Zoom;
                //if ((bitmap.Height < 45) && (bitmap.Width < 110))
                //    ZoomType = DataGridViewImageCellLayout.Normal;
                //RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = i.ToString() });
                //RowNew.Cells.Add(new DataGridViewTextBoxCell() { Value = fileNameOnly });
                //RowNew.Cells.Add(new DataGridViewImageCell()
                //{
                //    Value = PreviewImage,
                //    ImageLayout = ZoomType
                //});
                //RowNew.Height = 45;
                //dataGridView_ImagesList.Rows.Add(RowNew);

                if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                Watch_Face.WatchFace_Info.Preview = fileNameOnly;
                //userCtrl_Background_Options.ComboBoxAddItems(ListImages, ListImagesFullName);
                userCtrl_Background_Options.SetPreview(fileNameOnly);
                PreviewView = true;
                JSON_Modified = true;
                FormText();

                bitmap.Dispose();

            }
        }

        private void button_unpack_zip_Click(object sender, EventArgs e)
        {

            // сохранение если файл не сохранен
            SaveRequest();

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
                //string[] allfiles = Directory.GetFiles(tempDir + @"\assets", "*.png", SearchOption.AllDirectories);
                //foreach (string fileNames in allfiles)
                //{
                //    Console.WriteLine(fileNames);
                //}
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
                foreach (string fileNames in allFiles)
                {
                    //Console.WriteLine(fileNames);
                    TgaToPng(tempDir + @"\assets" + fileNames, projectPath + @"\assets" + fileNames);
                    progress++;
                    progressBar1.Value = progress;
                }

                JSToJson(tempDir + @"\watchface\index.js"); // создаем новый json файл циферблата
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
                                    Watch_Face.WatchFace_Info.Preview = appJson.app.icon;

                                if (appJson.app.appName != null && appJson.app.appName.Length > 0)
                                    projectName = appJson.app.appName;

                                int width = 0;

                                if (Watch_Face.ScreenNormal == null && Watch_Face.ScreenNormal.Background != null)
                                {
                                    if (Watch_Face.ScreenNormal.Background.BackgroundColor != null)
                                        width = Watch_Face.ScreenNormal.Background.BackgroundColor.w;
                                    if (Watch_Face.ScreenNormal.Background.BackgroundImage != null)
                                        width = Watch_Face.ScreenNormal.Background.BackgroundImage.w;
                                }

                                if (Watch_Face.ScreenAOD == null && Watch_Face.ScreenAOD.Background != null)
                                {
                                    if (Watch_Face.ScreenAOD.Background.BackgroundColor != null)
                                        width = Watch_Face.ScreenAOD.Background.BackgroundColor.w;
                                    if (Watch_Face.ScreenAOD.Background.BackgroundImage != null)
                                        width = Watch_Face.ScreenAOD.Background.BackgroundImage.w;
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
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                    string Watch_Face_String = JsonConvert.SerializeObject(Watch_Face, Formatting.Indented, new JsonSerializerSettings
                    {
                        //DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    });
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
        }

        private void TgaToPng(string file, string targetFile)
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
                image.Write(targetFile);
                //Bitmap bitmap = image.ToBitmap();
                //panel1.BackgroundImage = bitmap;
            }
            catch (Exception exp)
            {
                MessageBox.Show("Не верный формат исходного файла" + Environment.NewLine +
                    exp);
            }
        }

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
            //using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Program log.txt", true))
            //{
            //    sw.Write(text);
            //}
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
        try
        {
            //using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Program log.txt", true))
            //{
            //    sw.WriteLine(String.Format("{0,-23} {1}", DateTime.Now.ToString() + ":", message));
            //}
        }
        catch (Exception)
        {
        }
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
        name1 = Path.GetFileNameWithoutExtension(name1);
        name2 = Path.GetFileNameWithoutExtension(name2);

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
        for (int i = 0; i < parts1.Length; i++)
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
