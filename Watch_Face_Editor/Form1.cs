using ControlLibrary;
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
        List<string> ListImages = new List<string>(); // перечень имен файлов с картинками
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
                    //ProgramSettings.language = "English";
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
            //Logger.WriteLine("Создали переменные");

            if (args.Length == 1)
            {
                string fileName = args[0].ToString();
                if ((File.Exists(fileName)) && (Path.GetExtension(fileName) == ".json"))
                {
                    Logger.WriteLine("args[0] - *.json");
                    StartFileNameJson = fileName;
                    //Logger.WriteLine("Программа запущена с аргументом: " + fileName);
                }
                if ((File.Exists(fileName)) && (Path.GetExtension(fileName) == ".zip"))
                {
                    Logger.WriteLine("args[0] - *.zip");
                    StartFileNameZip = fileName;
                    //Logger.WriteLine("Программа запущена с аргументом: " + fileName);
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


            //Logger.WriteLine("Form1_Load");

            PreviewView = false;

            comboBox_AddTime.SelectedIndex = 0;
            comboBox_AddDate.SelectedIndex = 0;
            comboBox_AddActivity.SelectedIndex = 0;
            comboBox_AddAir.SelectedIndex = 0;
            comboBox_AddSystem.SelectedIndex = 0;

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
            //label_version_help.Text =
            //    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
            //    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();

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


            //StartJsonPreview();
            SetPreferences(userCtrl_Set1);
            PreviewView = true;
            Logger.WriteLine("* Form1_Load (end)");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Logger.WriteLine("* Form1_Shown");
            ////Logger.WriteLine("Загружаем файл из значения аргумента " + StartFileNameJson);
            //if ((StartFileNameJson != null) && (StartFileNameJson.Length > 0))
            //{
            //    Logger.WriteLine("Загружаем Json файл из значения аргумента " + StartFileNameJson);
            //    LoadJsonAndImage(StartFileNameJson);
            //    StartFileNameJson = "";
            //}
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

            if (radioButton_GTR3.Checked) ProgramSettings.WatchSkin_GTR_3 = textBox_WatchSkin_Path.Text;
            if (radioButton_GTR3_Pro.Checked) ProgramSettings.WatchSkin_GTR_3_Pro = textBox_WatchSkin_Path.Text;


            
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            //PreviewImage();
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
            if (panel != null) panel.Tag = new object();
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Control UControl = (Control)sender;
            Panel panel = (Panel)UControl.Parent;
            if (panel != null && panel.Tag != null)
                panel.DoDragDrop(sender, DragDropEffects.Move);
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
            switch (optionsName)
            {
                case "Background":
                    userCtrl_Background_Options.Visible = true;
                    userCtrl_Background_Options.AOD = AOD;
                    uCtrl_Text_Opt.Visible = false;
                    break;
                case "Text":
                    userCtrl_Background_Options.Visible = false;
                    uCtrl_Text_Opt.Visible = true;
                    break;
            }
        }

        private void uCtrl_Background_Elm1_SelectChanged(object sender, EventArgs eventArgs)
        {
            uCtrl_DigitalTime_Elm1.ResetHighlightState();

            string preview = "";
            if (Watch_Face != null && Watch_Face.WatchFace_Info != null) preview = Watch_Face.WatchFace_Info.Preview;
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
            Read_Background_Options(background, preview);
            ShowElemenrOptions("Background");
        }

        private void uCtrl_DigitalTime_Elm1_SelectChanged(object sender, EventArgs eventArgs, string selectElement)
        {
            uCtrl_Background_Elm1.ResetHighlightState();
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
                string fullfilename = openFileDialog.FileName;
                string dirName = Path.GetDirectoryName(fullfilename) + @"\assets\";
                button_Add_Images.Enabled = true;

                string text = File.ReadAllText(fullfilename);
                Watch_Face = TextToJson(text);
                //try
                //{
                //    Watch_Face = JsonConvert.DeserializeObject<WATCH_FACE>(text, new JsonSerializerSettings
                //    {
                //        DefaultValueHandling = DefaultValueHandling.Ignore,
                //        NullValueHandling = NullValueHandling.Ignore
                //    });
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                //        Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
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

                LoadImage(dirName);
                ShowElemetsWatchFace();

                PreviewImage();

                FormText();
            }
            Logger.WriteLine("* JSON (end)");
        }

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
            Array.Sort(Images, new MyCustomComparer());
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
                if (radioButton_GTR3_Pro.Checked) Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";

                Watch_Face.ScreenNormal = new ScreenNormal();
                Watch_Face.ScreenNormal.Background = new Background();
                Watch_Face.ScreenNormal.Background.BackgroundColor = new hmUI_widget_FILL_RECT();
                if (radioButton_GTR3.Checked)
                {
                    Watch_Face.WatchFace_Info.DeviceName = "GTR3";

                    Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
                }
                else if (radioButton_GTR3_Pro.Checked)
                {
                    Watch_Face.WatchFace_Info.DeviceName = "GTR3_Pro";

                    Watch_Face.ScreenNormal.Background.BackgroundColor.show_level = "ONLY_NORMAL";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.color = "0xFF000000";
                    Watch_Face.ScreenNormal.Background.BackgroundColor.x = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.y = 0;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.h = 454;
                    Watch_Face.ScreenNormal.Background.BackgroundColor.w = 454;
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
                bitmap = new Bitmap(Convert.ToInt32(348), Convert.ToInt32(442), PixelFormat.Format32bppArgb);
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
                        bitmapPreviewResize = new Bitmap(Convert.ToInt32(348), Convert.ToInt32(442), PixelFormat.Format32bppArgb);
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
                bitmap = new Bitmap(Convert.ToInt32(348), Convert.ToInt32(442), PixelFormat.Format32bppArgb);
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
            if (comboBox_AddTime.SelectedIndex == 1) comboBox_AddDigitalTime();
            PreviewView = false;
            //if (comboBox_AddTime.SelectedIndex >= 0) MessageBox.Show(comboBox_AddTime.Text);
            comboBox_AddTime.Items.Insert(0, Properties.FormStrings.Elemet_Time);
            comboBox_AddTime.SelectedIndex = 0;
            ShowElemetsWatchFace();
            PreviewView = true;
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

        /// <summary>Добавляем цифровое время в циферблат</summary>
        private void comboBox_AddDigitalTime()
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
            Elements.Add(digitalTime);
        }

        /// <summary>Отображаем элемынты в соответствии с json файлом</summary>
        private void ShowElemetsWatchFace()
        {
            panel_UC_Background.Visible = false;
            panel_UC_DigitalTime.Visible = false;
            //int count = tableLayoutPanel_ElemetsWatchFace.RowCount;

            if (Watch_Face == null) return;
            if (radioButton_ScreenNormal.Checked)
            {
                if (Watch_Face.ScreenNormal == null) return;
                if (Watch_Face.ScreenNormal.Background != null) panel_UC_Background.Visible = true;
                if(Watch_Face.ScreenNormal.Elements!= null && Watch_Face.ScreenNormal.Elements.Count > 0)
                {
                    for (int i = 0; i < Watch_Face.ScreenNormal.Elements.Count; i++)
                    {
                        Object element = Watch_Face.ScreenNormal.Elements[i];
                        //string elementStr = element.ToString();
                        //string type = GetTypeFromSring(elementStr);
                        string type = element.GetType().Name;
                        switch (type)
                        {
                            case "ElementDigitalTime":
                                panel_UC_DigitalTime.Visible = true;
                                //SetElementPositionInGUI(type, count - i - 2);
                                SetElementPositionInGUI(type, i + 1);
                                ElementDigitalTime DigitalTime = (ElementDigitalTime)element;
                                break;
                        }
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
    }
}

public static class MouseСoordinates
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
        string[] parts1 = x.Name.Split('-');
        string[] parts2 = y.Name.Split('-');

        // calculate how much leading zeros we need
        int toPad1 = 10 - parts1[0].Length;
        int toPad2 = 10 - parts2[0].Length;

        if (toPad1 < 0) toPad1 = 0;
        if (toPad2 < 0) toPad2 = 0;

        // add the zeros, only for sorting
        parts1[0] = parts1[0].Insert(0, new String('0', toPad1));
        parts2[0] = parts2[0].Insert(0, new String('0', toPad2));

        // create the comparable string
        string toCompare1 = string.Join("", parts1);
        string toCompare2 = string.Join("", parts2);

        // compare
        return toCompare1.CompareTo(toCompare2);
    }
}
