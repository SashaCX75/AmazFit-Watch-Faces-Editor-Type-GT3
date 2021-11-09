using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        private WATCH_FACE TextToJson(string text)
        {
            WATCH_FACE Watch_Face_return = null;
            WATCH_FACE Watch_Face_temp = null;
            try
            {
                Watch_Face_temp = JsonConvert.DeserializeObject<WATCH_FACE>(text, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                    Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (Watch_Face_temp == null) return Watch_Face_return;
            
            Watch_Face_return = new WATCH_FACE();
            if (Watch_Face_temp.WatchFace_Info != null) Watch_Face_return.WatchFace_Info = Watch_Face_temp.WatchFace_Info;
            if (Watch_Face_temp.ScreenNormal != null)
            {
                Watch_Face_return.ScreenNormal = new ScreenNormal();
                if (Watch_Face_temp.ScreenNormal.Background != null)
                    Watch_Face_return.ScreenNormal.Background = Watch_Face_temp.ScreenNormal.Background;
                if (Watch_Face_temp.ScreenNormal.Elements != null)
                {
                    Watch_Face_return.ScreenNormal.Elements = new List<object>();
                    List<object> NewElements = Watch_Face_return.ScreenNormal.Elements;
                    foreach (object element in Watch_Face_temp.ScreenNormal.Elements)
                    {
                        string elementStr = element.ToString();
                        string type = GetTypeFromSring(elementStr);
                        switch (type)
                        {
                            case "DigitalTime":
                                ElementDigitalTime DigitalTime = null;
                                try
                                {
                                    DigitalTime = JsonConvert.DeserializeObject<ElementDigitalTime>(elementStr, new JsonSerializerSettings
                                    {
                                        //DefaultValueHandling = DefaultValueHandling.Ignore,
                                        NullValueHandling = NullValueHandling.Ignore
                                    });
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                        Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (DigitalTime != null) NewElements.Add(DigitalTime);
                                break;
                        }
                    }
                }
            }
            return Watch_Face_return;
        }

        /// <summary>Выделяем тип элемента из строки с параметрами</summary>
        private string GetTypeFromSring(string str)
        {
            string name = "elementName";
            string returnStr = "";
            int indexOf = str.IndexOf("elementName");
            if (indexOf >= 0)
            {
                str = str.Substring(indexOf + name.Length + 2);
                indexOf = str.IndexOf("\"");
                str = str.Substring(indexOf + 1);
                indexOf = str.IndexOf("\"");
                str = str.Substring(0, indexOf);
                returnStr = str;
            }
            return returnStr;
        }

        private void Read_Background_Options(Background background, string preview = "")
        {
            PreviewView = false;
            if (preview != null && preview.Length > 0) userCtrl_Background_Options.SetPreview(preview);
            if (background == null) return;
            if (background.BackgroundColor != null)
            {
                userCtrl_Background_Options.SetColorBackground(ColorRead(background.BackgroundColor.color));
                userCtrl_Background_Options.Switch_ImageColor(1);
            }
            if (background.BackgroundImage != null)
            {
                userCtrl_Background_Options.SetBackground(background.BackgroundImage.src);
                userCtrl_Background_Options.Switch_ImageColor(0);
            }
            userCtrl_Background_Options.Visible = background.visible;
            userCtrl_Background_Options._Background = background;

            PreviewView = true;
        }

        private void userCtrl_Background_Options_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            Background background = (Background)userCtrl_Background_Options._Background;
            string backgroundImg = userCtrl_Background_Options.GetBackground();
            string preview = userCtrl_Background_Options.GetPreview();
            if (preview.Length > 0)
            {
                if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                Watch_Face.WatchFace_Info.Preview = preview;
            }

            if (userCtrl_Background_Options.radioButton_Background_image.Checked)
            {
                if (backgroundImg.Length > 0)
                {
                    if (background == null) background = new Background();
                    if (background.BackgroundImage == null)
                        background.BackgroundImage = new hmUI_widget_IMG();
                    background.BackgroundImage.src = backgroundImg;
                    background.BackgroundImage.x = 0;
                    background.BackgroundImage.y = 0;
                    background.BackgroundImage.h = 454;
                    background.BackgroundImage.w = 454;
                    if (radioButton_GTR3_Pro.Checked)
                    {
                        background.BackgroundImage.h = 480;
                        background.BackgroundImage.w = 480;
                    }
                    background.BackgroundImage.show_level = "ONLY_NORMAL";
                }
            }
            else
            {
                if (background.BackgroundColor == null)
                    background.BackgroundColor = new hmUI_widget_FILL_RECT();
                background.BackgroundColor.color = ColorWrite(userCtrl_Background_Options.GetColorBackground());
                background.BackgroundColor.x = 0;
                background.BackgroundColor.y = 0;
                background.BackgroundColor.h = 454;
                background.BackgroundColor.w = 454;
                if (radioButton_GTR3_Pro.Checked)
                {
                    background.BackgroundColor.h = 480;
                    background.BackgroundColor.w = 480;
                }
            }
            background.visible = userCtrl_Background_Options.Visible;

            PreviewImage();
        }

        private Color ColorRead(string color)
        {
            if (color.Length == 18) color = color.Remove(2, 8);
            Color old_color = ColorTranslator.FromHtml(color);
            Color new_color = Color.FromArgb(255, old_color.R, old_color.G, old_color.B);
            return new_color;
        }

        private string ColorWrite(Color color)
        {
            Color new_color = Color.FromArgb(0, color.R, color.G, color.B);
            string colorStr = ColorTranslator.ToHtml(new_color);
            colorStr = colorStr.Replace("#", "0xFF");
            return colorStr;
        }
    }
}
