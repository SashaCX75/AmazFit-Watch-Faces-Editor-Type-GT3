using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_Background_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        private bool AODmode;
        public Object _Background;
        private int ID;

        [Description("Отображается на экране AOD")]
        public bool AOD
        {
            get
            {
                return AODmode;
            }
            set
            {
                AODmode = value;
                comboBox_Preview_image.Visible = !AODmode;
                button_GenerateID.Visible = !AODmode;
                label3.Visible = !AODmode;
                label1.Visible = !AODmode;
            }
        }
        public UCtrl_Background_Opt()
        {
            InitializeComponent();
            setValue = false;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs);

        private void button_GenerateID_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            ID = rnd.Next(1000, 10000000);
            label_ID.Text = ID.ToString();

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        public void GenerateID()
        {
            Random rnd = new Random();
            ID = rnd.Next(1000, 10000000);
            label_ID.Text = ID.ToString();
        }

        /// <summary>Получаем ID</summary>
        public int GetID()
        {
            return ID;
        }

        /// <summary>Устанавливаем ID</summary>
        public void SetID(int id)
        {
            if (id > 999 && id < 10000000)
            {
                ID = id;
                label_ID.Text = ID.ToString();
            }
        }

        private void radioButton_Background_image_CheckedChanged(object sender, EventArgs e)
        {
            bool b = radioButton_Background_image.Checked;
            comboBox_Background_image.Enabled = b;
            comboBox_Background_color.Enabled = !b;

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void comboBox_Background_color_Click(object sender, EventArgs e)
        {
            Program_Settings ProgramSettings = new Program_Settings();
            ColorDialog colorDialog = new ColorDialog();
            ComboBox comboBox_color = sender as ComboBox;
            colorDialog.Color = comboBox_color.BackColor;
            colorDialog.FullOpen = true;

            // читаем пользовательские цвета из настроек
            if (File.Exists(Application.StartupPath + @"\Settings.json"))
            {
                ProgramSettings = JsonConvert.DeserializeObject<Program_Settings>
                            (File.ReadAllText(Application.StartupPath + @"\Settings.json"), new JsonSerializerSettings
                            {
                                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                                    NullValueHandling = NullValueHandling.Ignore
                            });
            }
            colorDialog.CustomColors = ProgramSettings.CustomColors;


            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета формы
            comboBox_color.BackColor = colorDialog.Color;
            if (ProgramSettings.CustomColors != colorDialog.CustomColors)
            {
                ProgramSettings.CustomColors = colorDialog.CustomColors;

                string JSON_String = JsonConvert.SerializeObject(ProgramSettings, Formatting.Indented, new JsonSerializerSettings
                {
                    //DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                File.WriteAllText(Application.StartupPath + @"\Settings.json", JSON_String, Encoding.UTF8);
            }

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        public void SetBackground(string value)
        {
            comboBox_Background_image.Text = value;
            if (comboBox_Background_image.SelectedIndex < 0) comboBox_Background_image.Text = "";
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetBackground()
        {
            if (comboBox_Background_image.SelectedIndex < 0) return "";
            return comboBox_Background_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int comboBoxGetSelectedIndexBackground()
        {
            return comboBox_Background_image.SelectedIndex;
        }

        public void SetColorBackground(Color color)
        {
            comboBox_Background_color.BackColor = color;
        }

        public Color GetColorBackground()
        {
            return comboBox_Background_color.BackColor;
        }

        public void SetPreview(string value)
        {
            comboBox_Preview_image.Text = value;
            if (comboBox_Preview_image.SelectedIndex < 0) comboBox_Preview_image.Text = "";
        }

        /// <summary>Возвращает номер выбранной картинки, в случае ошибки возвращает -1</summary>
        public string GetPreview()
        {
            if (comboBox_Preview_image.SelectedIndex < 0) return "";
            return comboBox_Preview_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexPreview()
        {
            return comboBox_Preview_image.SelectedIndex;
        }

        /// <summary>Переключает отображение фона картинкой или цветом. 0 - картинка, иначе цвет</summary>
        public void Switch_ImageColor(int value)
        {
            if (value == 0) radioButton_Background_image.Checked = true;
            else radioButton_Background_color.Checked = true;
        }

        #region Standard events
        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                ComboBox comboBox = sender as ComboBox;
                comboBox.Text = "";
                comboBox.SelectedIndex = -1;
                if (ValueChanged != null && !setValue)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs);
                }
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            //if (comboBox.Items.Count < 5) comboBox.DropDownHeight = comboBox.Items.Count * 35;
            //else comboBox.DropDownHeight = 106;
            float size = comboBox.Font.Size;
            Font myFont;
            FontFamily family = comboBox.Font.FontFamily;
            e.DrawBackground();
            int itemWidth = e.Bounds.Height;
            int itemHeight = e.Bounds.Height - 4;

            if (e.Index >= 0)
            {
                try
                {
                    using (FileStream stream = new FileStream(ListImagesFullName[e.Index], FileMode.Open, FileAccess.Read))
                    {
                        Image image = Image.FromStream(stream);
                        float scale = (float)itemWidth / image.Width;
                        if ((float)itemHeight / image.Height < scale) scale = (float)itemHeight / image.Height;
                        float itemWidthRec = image.Width * scale;
                        float itemHeightRec = image.Height * scale;
                        Rectangle rectangle = new Rectangle((int)(itemWidth - itemWidthRec) / 2 + 2,
                            e.Bounds.Top + (int)(itemHeight - itemHeightRec) / 2 + 2, (int)itemWidthRec, (int)itemHeightRec);
                        e.Graphics.DrawImage(image, rectangle);
                    }
                }
                catch { }
            }
            myFont = new Font(family, size);
            StringFormat lineAlignment = new StringFormat();;
            lineAlignment.LineAlignment = StringAlignment.Center;
            if (e.Index >= 0)
                e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), myFont, System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + itemWidth, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), lineAlignment);
            e.DrawFocusRectangle();
        }

        private void comboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 35;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        #endregion

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_Background_image.Items.Clear();
            comboBox_Preview_image.Items.Clear();

            comboBox_Background_image.Items.AddRange(ListImages.ToArray());
            comboBox_Preview_image.Items.AddRange(ListImages.ToArray());
            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0) 
            {
                comboBox_Background_image.DropDownHeight = 1;
                comboBox_Preview_image.DropDownHeight = 1;
            } 
            else if (count < 5)
            {
                //comboBox_Background_image.DropDownHeight = (int)(comboBox_Background_image.Height * 1.5f * count);
                comboBox_Background_image.DropDownHeight = 35 * count + 1;
                comboBox_Preview_image.DropDownHeight = 35 * count + 1;
            }
            else
            {
                //comboBox_Background_image.DropDownHeight = comboBox_Background_image.Height * 5;
                comboBox_Background_image.DropDownHeight = 106;
                comboBox_Preview_image.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            comboBox_Background_image.Text = null;
            comboBox_Preview_image.Text = null;
            radioButton_Background_color.Checked = true;
            ID = 0;
            label_ID.Text = "";

            setValue = false;
        }

        #endregion
    }
}

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

    public bool Model_GTR3 = true;
    public bool Model_GTR3_Pro = true;

    public bool ShowBorder = false;
    public bool Crop = true;
    public bool Show_Warnings = true;
    public bool Show_Shortcuts = true;
    public bool Shortcuts_Area = true;
    public bool Shortcuts_Border = true;
    public bool Shortcuts_Center_marker = true;
    public bool Show_CircleScale_Area = false;
    public bool Show_Widgets_Area = true;
    public float Scale = 1f;
    public float Gif_Speed = 1f;
    public bool DrawAllWidgets = false;

    public bool ShowIn12hourFormat = true;

    public int[] CustomColors = { };

    public string language { get; set; }

    public int Splitter_Pos = 0;

    public string WatchSkin_GTR_3 = @"Skin\WatchSkin_GTR_2e.json";
    public string WatchSkin_GTR_3_Pro = @"Skin\WatchSkin_GTR_2.json";
    public bool WatchSkin_Use = false;
}
