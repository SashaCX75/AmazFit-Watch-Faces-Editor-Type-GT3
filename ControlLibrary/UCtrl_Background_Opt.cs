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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ControlLibrary
{
    public partial class UCtrl_Background_Opt : UserControl
    {
        private bool setValue; // режим задания параметров
        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        private bool AODmode;
        private bool Editable_background_mode;
        public Object _Background;
        private long ID;
        private int[] CustomColors = { }; // пользовательские цвета

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
                button_EditInformation.Visible = !AODmode;
                label3.Visible = !AODmode;
                label1.Visible = !AODmode;
                label_ID.Visible = !AODmode;
                //radioButton_EditableBackground.Visible = !AODmode;
            }
        }
        [Description("Возможность включить редактируемый фон")]
        public bool Editable_background
        {
            get
            {
                return Editable_background_mode;
            }
            set
            {
                Editable_background_mode = value;
                radioButton_EditableBackground.Enabled = Editable_background_mode;
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

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event InformationChangedHandler InformationChanged;
        public delegate void InformationChangedHandler(object sender, EventArgs eventArgs);

        [Browsable(true)]
        [Description("Происходит при изменении пользовательских цветов")]
        public event CustomColorsChangedHandler CustomColorsChanged;
        public delegate void CustomColorsChangedHandler(int[] customColors);

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

        private void button_EditInformation_Click(object sender, EventArgs e)
        {
            if (InformationChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                InformationChanged(this, eventArgs);
            }
        }

        /// <summary>Получаем ID</summary>
        public long GetID()
        {
            return ID;
        }

        /// <summary>Устанавливаем ID</summary>
        public void SetID(long id)
        {
            if (id > 999 && id < 10000000)
            {
                ID = id;
                label_ID.Text = ID.ToString();
            }
        }

        private void radioButton_Background_image_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RadioButton radioButton = sender as System.Windows.Forms.RadioButton;
            if (!radioButton.Checked) return;
            label_EditableBackground_Hint.Visible = false;
            if (radioButton.Name == "radioButton_EditableBackground")
            {
                if(AODmode) label_EditableBackground_Hint.Visible = true;
            }
            bool i = radioButton_Background_image.Checked;
            bool c = radioButton_Background_color.Checked;
            comboBox_Background_image.Enabled = i;
            comboBox_Background_color.Enabled = c;

            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs);
            }
        }

        private void comboBox_Background_color_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            ComboBox comboBox_color = sender as ComboBox;
            colorDialog.Color = comboBox_color.BackColor;
            colorDialog.FullOpen = true;

            colorDialog.CustomColors = CustomColors;


            if (colorDialog.ShowDialog() == DialogResult.Cancel) return;

            // установка цвета формы
            comboBox_color.BackColor = colorDialog.Color;
            if (CustomColors != colorDialog.CustomColors)
            {
                CustomColors = colorDialog.CustomColors;

                if (CustomColorsChanged != null && !setValue)
                {
                    CustomColorsChanged(CustomColors);
                }
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

        /// <summary>Переключает отображение фона картинкой или цветом. 0 - картинка, 1 - фон, 2 - редактируемая фоновая картинка</summary>
        public void Switch_ImageType(int value)
        {
            //if (value == 0) radioButton_Background_image.Checked = true;
            //else radioButton_Background_color.Checked = true;
            switch (value)
            {
                case 0:
                    radioButton_Background_image.Checked = true;
                    break;
                case 1:
                    radioButton_Background_color.Checked = true;
                    break;
                case 2:
                    radioButton_EditableBackground.Checked = true;
                    break;
                default:
                    radioButton_Background_image.Checked = true;
                    break;
            }
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
            ComboBox cb = (ComboBox)sender;
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
        public void SettingsClear(int[] customColors)
        {
            setValue = true;
            CustomColors = customColors;

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

//public class Program_Settings
//{
//    public bool Settings_Unpack_Dialog = true;
//    public bool Settings_Unpack_Save = false;
//    public bool Settings_Unpack_Replace = false;

//    public bool Settings_Pack_Dialog = false;
//    public bool Settings_Pack_GoToFile = true;
//    public bool Settings_Pack_DoNotning = false;

//    public bool Settings_AfterUnpack_Dialog = false;
//    public bool Settings_AfterUnpack_Download = true;
//    public bool Settings_AfterUnpack_DoNothing = false;

//    public bool Settings_Open_Dialog = false;
//    public bool Settings_Open_Download = true;
//    public bool Settings_Open_DoNotning = false;
//    public bool Settings_Open_Download_Your_File = false;
//    public string PreviewStates_Path = "";

//    public string Watch_Model = "Balance 2";

//    public bool ShowBorder = false;
//    public bool Crop = true;
//    public bool Pointer_Center_marker = true;
//    public bool Show_Warnings = true;
//    public bool Show_Shortcuts = true;
//    public bool Show_Buttons = true;
//    public bool Show_CircleScale_Area = false;
//    public bool Show_Widgets_Area = true;

//    public bool Shortcuts_Area = true;
//    public bool Shortcuts_Border = true;
//    //public bool Shortcuts_Image = false;
//    public bool Shortcuts_In_Gif = true;

//    public bool Buttons_Area = true;
//    public bool Buttons_Border = true;
//    //public bool Buttons_Image = false;
//    public bool Buttons_In_Gif = true;

//    public bool Use_ARGB_encoding = false;
//    public bool ARGB_encoding_color = false;
//    public bool ARGB_encoding_forced = true;
//    public int ARGB_encoding_color_count = 255;

//    public float Scale = 1f;
//    public float Gif_Speed = 1f;
//    public int Animation_Preview_Speed = 4;

//    public bool DrawAllWidgets = false;

//    public bool ShowIn12hourFormat = true;
//    public bool CreateZPK = false;
//    public bool DelConfirm = false;
//    public bool AutoSave = false;
//    public int AutoSaveTime = 0;
//    public bool DevelopmentMode = false;

//    public int[] CustomColors = { };

//    public string language { get; set; }

//    public int Splitter_Pos = 0;

//    public bool WatchSkin_Use = false;

//    public string model_config = @"\model_config\configurations.json";

//    public string CacheFonts_light = "0123456789 _-.,:;`'%°\\\\/";
//    public string CacheFonts_full = "0123456789 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz " +
//            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя  ҐЄІЇґєії " + "_-.,:;`'%°\\\\/";
//}

public class LastColor
{
    public static Color? last_color { get; set; }
}
