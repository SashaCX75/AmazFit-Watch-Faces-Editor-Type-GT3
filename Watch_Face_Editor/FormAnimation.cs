using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class FormAnimation : Form
    {
        private Bitmap SrcImg;
        float scalePreview = 1.0f;
        float currentDPI; // масштаб экрана
        float time_value_sec = 0;

        public FormAnimation(float cDPI)
        {
            InitializeComponent();
            //pictureBox_AnimatiomPreview.BackgroundImage = previewBackground;
            currentDPI = cDPI;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Graphics gPanel = pictureBox_AnimatiomPreview.CreateGraphics();
            SrcImg = new Bitmap(pictureBox_AnimatiomPreview.Width, pictureBox_AnimatiomPreview.Height);
            Graphics gPanel = Graphics.FromImage(SrcImg);
            gPanel.ScaleTransform(scalePreview, scalePreview, MatrixOrder.Prepend);

            Form1 form1 = this.Owner as Form1;//Получаем ссылку на первую форму
            int link = form1.radioButton_ScreenNormal.Checked ? 0 : 1;
            form1.Preview_screen(gPanel, 1, false, false, false, false, false, false, false, false, true, false,
                false, false, link, false, time_value_sec, false, 0);

            pictureBox_AnimatiomPreview.Image = SrcImg;
            time_value_sec = time_value_sec + timer1.Interval / 1000f;

            gPanel.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && !radioButton.Checked) return;
            Form1 form1 = this.Owner as Form1;//Получаем ссылку на первую форму
            pictureBox_AnimatiomPreview.BackgroundImageLayout = ImageLayout.Zoom;
            if (radioButton_normal.Checked)
            {
                pictureBox_AnimatiomPreview.BackgroundImageLayout = ImageLayout.None;
                switch (form1.comboBox_watch_model.Text)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_AnimatiomPreview.Size = new Size(456, 456);
                        this.Size = new Size((int)(456 + 20 * currentDPI), (int)(456 + 100 * currentDPI));
                        break;

                    case "GTR 3 Pro":
                        pictureBox_AnimatiomPreview.Size = new Size(482, 482);
                        this.Size = new Size((int)(482 + 20 * currentDPI), (int)(482 + 100 * currentDPI));
                        break;

                    case "GTS 3":
                    case "GTS 4":
                        pictureBox_AnimatiomPreview.Size = new Size(392, 452);
                        this.Size = new Size((int)(392 + 20 * currentDPI), (int)(452 + 100 * currentDPI));
                        break;

                    case "GTR 4":
                        pictureBox_AnimatiomPreview.Size = new Size(468, 468);
                        this.Size = new Size((int)(468 + 20 * currentDPI), (int)(468 + 100 * currentDPI));
                        break;

                    case "Amazfit Band 7":
                        pictureBox_AnimatiomPreview.Size = new Size(196, 370);
                        this.Size = new Size((int)(196 + 20 * currentDPI), (int)(370 + 100 * currentDPI));
                        break;

                    case "GTS 4 mini":
                        pictureBox_AnimatiomPreview.Size = new Size(338, 386);
                        this.Size = new Size((int)(338 + 20 * currentDPI), (int)(386 + 100 * currentDPI));
                        break;

                    case "Falcon":
                        pictureBox_AnimatiomPreview.Size = new Size(418, 418);
                        this.Size = new Size((int)(418 + 20 * currentDPI), (int)(418 + 100 * currentDPI));
                        break;
                }
                scalePreview = 1f;
            }

            if (radioButton_large.Checked)
            {
                switch (form1.comboBox_watch_model.Text)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_AnimatiomPreview.Size = new Size(683, 683);
                        this.Size = new Size((int)(683 + 20 * currentDPI), (int)(683 + 100 * currentDPI));
                        break;

                    case "GTR 3 Pro":
                        pictureBox_AnimatiomPreview.Size = new Size(722, 722);
                        this.Size = new Size((int)(722 + 20 * currentDPI), (int)(722 + 100 * currentDPI));
                        break;

                    case "GTS 3":
                    case "GTS 4":
                        pictureBox_AnimatiomPreview.Size = new Size(587, 677);
                        this.Size = new Size((int)(587 + 20 * currentDPI), (int)(677 + 100 * currentDPI));
                        break;

                    case "GTR 4":
                        pictureBox_AnimatiomPreview.Size = new Size(701, 701);
                        this.Size = new Size((int)(701 + 20 * currentDPI), (int)(701 + 100 * currentDPI));
                        break;

                    case "Amazfit Band 7":
                        pictureBox_AnimatiomPreview.Size = new Size(293, 554);
                        this.Size = new Size((int)(293 + 20 * currentDPI), (int)(554 + 100 * currentDPI));
                        break;

                    case "GTS 4 mini":
                        pictureBox_AnimatiomPreview.Size = new Size(506, 578);
                        this.Size = new Size((int)(506 + 20 * currentDPI), (int)(578 + 100 * currentDPI));
                        break;

                    case "Falcon":
                        pictureBox_AnimatiomPreview.Size = new Size(626, 626);
                        this.Size = new Size((int)(626 + 20 * currentDPI), (int)(626 + 100 * currentDPI));
                        break;
                }
                scalePreview = 1.5f;
            }

            if (radioButton_xlarge.Checked)
            {
                switch (form1.comboBox_watch_model.Text)
                {
                    case "GTR 3":
                    case "T-Rex 2":
                        pictureBox_AnimatiomPreview.Size = new Size(909, 909);
                        this.Size = new Size((int)(909 + 20 * currentDPI), (int)(909 + 100 * currentDPI));
                        break;

                    case "GTR 3 Pro":
                        pictureBox_AnimatiomPreview.Size = new Size(961, 961);
                        this.Size = new Size((int)(961 + 20 * currentDPI), (int)(961 + 100 * currentDPI));
                        break;

                    case "GTS 3":
                    case "GTS 4":
                        pictureBox_AnimatiomPreview.Size = new Size(781, 901);
                        this.Size = new Size((int)(781 + 20 * currentDPI), (int)(901 + 100 * currentDPI));
                        break;

                    case "GTR 4":
                        pictureBox_AnimatiomPreview.Size = new Size(933, 933);
                        this.Size = new Size((int)(933 + 20 * currentDPI), (int)(933 + 100 * currentDPI));
                        break;

                    case "Amazfit Band 7":
                        pictureBox_AnimatiomPreview.Size = new Size(389, 737);
                        this.Size = new Size((int)(389 + 20 * currentDPI), (int)(737 + 100 * currentDPI));
                        break;

                    case "GTS 4 mini":
                        pictureBox_AnimatiomPreview.Size = new Size(673, 769);
                        this.Size = new Size((int)(673 + 20 * currentDPI), (int)(769 + 100 * currentDPI));
                        break;

                    case "Falcon":
                        pictureBox_AnimatiomPreview.Size = new Size(833, 833);
                        this.Size = new Size((int)(833 + 20 * currentDPI), (int)(833 + 100 * currentDPI));
                        break;
                }
                scalePreview = 2f;
            }
            int width = button_SaveAnimation.Left + button_SaveAnimation.Width;
            if (this.Width < (int)(width + 20 * currentDPI)) this.Width = (int)(width + 20 * currentDPI);
        }

        private void button_SaveAnimation_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //openFileDialog.InitialDirectory = subPath;
            saveFileDialog.Filter = "GIF Files: (*.gif)|*.gif";
            saveFileDialog.FileName = "Preview.gif";
            //openFileDialog.Filter = "Binary File (*.bin)|*.bin";
            ////openFileDialog1.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = Properties.FormStrings.Dialog_Title_SaveGIF;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(Convert.ToInt32(454), Convert.ToInt32(454), PixelFormat.Format32bppArgb);
                Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\mask_gtr_3.png");

                Form1 form1 = this.Owner as Form1;//Получаем ссылку на первую форму
                switch (form1.comboBox_watch_model.Text)
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
                        bitmap = new Bitmap(Convert.ToInt32(416), Convert.ToInt32(416), PixelFormat.Format32bppArgb);
                        mask = new Bitmap(Application.StartupPath + @"\Mask\mask_falcon.png");
                        break;
                }
                Bitmap bitmapTemp = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                Graphics gPanel = Graphics.FromImage(bitmap);
                bool save = false;
                int set = 0;
                int setIndex = 0;
                float time_value_sec = 0;
                progressBar_SaveAnimation.Width = pictureBox_AnimatiomPreview.Width - 100;
                progressBar_SaveAnimation.Maximum = (int)numericUpDown_NumberOfFrames.Value;
                progressBar_SaveAnimation.Value = 0;
                progressBar_SaveAnimation.Visible = true;
                form1.PreviewView = false;
                timer1.Enabled = false;

                int SetNumber = form1.WatchFacePreviewSet.SetNumber;
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    for (int i = 0; i < numericUpDown_NumberOfFrames.Value; i++)
                    {
                        save = false;
                        while (!save)
                        {
                            switch (set)
                            {
                                case 0:
                                    //button_Set1.PerformClick();
                                    form1.SetPreferences(form1.userCtrl_Set1);
                                    save = true;
                                    break;
                                case 1:
                                    if (form1.userCtrl_Set2.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set2);
                                        save = true;
                                    }
                                    break;
                                case 2:
                                    if (form1.userCtrl_Set3.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set3);
                                        save = true;
                                    }
                                    break;
                                case 3:
                                    if (form1.userCtrl_Set4.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set4);
                                        save = true;
                                    }
                                    break;
                                case 4:
                                    if (form1.userCtrl_Set5.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set5);
                                        save = true;
                                    }
                                    break;
                                case 5:
                                    if (form1.userCtrl_Set6.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set6);
                                        save = true;
                                    }
                                    break;
                                case 6:
                                    if (form1.userCtrl_Set7.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set7);
                                        save = true;
                                    }
                                    break;
                                case 7:
                                    if (form1.userCtrl_Set8.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set8);
                                        save = true;
                                    }
                                    break;
                                case 8:
                                    if (form1.userCtrl_Set9.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set9);
                                        save = true;
                                    }
                                    break;
                                case 9:
                                    if (form1.userCtrl_Set10.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set10);
                                        save = true;
                                    }
                                    break;
                                case 10:
                                    if (form1.userCtrl_Set11.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set11);
                                        save = true;
                                    }
                                    break;
                                case 11:
                                    if (form1.userCtrl_Set12.numericUpDown_Calories_Set.Value != 1234)
                                    {
                                        form1.SetPreferences(form1.userCtrl_Set12);
                                        save = true;
                                    }
                                    break;
                            }
                            if (!save) set++;
                            if (set > 11) set = 0;
                        }

                        if (save)
                        {
                            bitmap = bitmapTemp;
                            gPanel = Graphics.FromImage(bitmap);

                            form1.Preview_screen(gPanel, 1, false, false, false, false, false, false, false, false, true, false,
                                false, false, 0, false, time_value_sec, false, 0);

                            if (form1.checkBox_WatchSkin_Use.Checked) bitmap = form1.ApplyWatchSkin(bitmap);
                            else if (form1.checkBox_crop.Checked) bitmap = form1.ApplyMask(bitmap, mask);

                            // Add first image and set the animation delay to 100ms
                            MagickImage item = new MagickImage(bitmap);
                            //ExifProfile profile = item.GetExifProfile();
                            collection.Add(item);
                            collection[collection.Count - 1].AnimationDelay = timer1.Interval / 10;

                            //collection[collection.Count - 1].AnimationDelay = 100;
                            //collection[collection.Count - 1].AnimationDelay = 10;


                        }

                        setIndex = setIndex + timer1.Interval;
                        //time_value_sec = time_value_sec + 0.1f;
                        time_value_sec = time_value_sec + timer1.Interval/1000f;
                        if (setIndex >= (1000 * form1.numericUpDown_Gif_Speed.Value))
                        {
                            setIndex = 0;
                            set++;
                            if (set > 11) set = 0;
                        }

                        progressBar_SaveAnimation.Value = i;
                        progressBar_SaveAnimation.Update();
                    }

                    progressBar_SaveAnimation.Visible = false;
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
                        form1.SetPreferences(form1.userCtrl_Set1);
                        break;
                    case 2:
                        form1.SetPreferences(form1.userCtrl_Set2);
                        break;
                    case 3:
                        form1.SetPreferences(form1.userCtrl_Set3);
                        break;
                    case 4:
                        form1.SetPreferences(form1.userCtrl_Set4);
                        break;
                    case 5:
                        form1.SetPreferences(form1.userCtrl_Set5);
                        break;
                    case 6:
                        form1.SetPreferences(form1.userCtrl_Set6);
                        break;
                    case 7:
                        form1.SetPreferences(form1.userCtrl_Set7);
                        break;
                    case 8:
                        form1.SetPreferences(form1.userCtrl_Set8);
                        break;
                    case 9:
                        form1.SetPreferences(form1.userCtrl_Set9);
                        break;
                    case 10:
                        form1.SetPreferences(form1.userCtrl_Set10);
                        break;
                    case 11:
                        form1.SetPreferences(form1.userCtrl_Set11);
                        break;
                    case 12:
                        form1.SetPreferences(form1.userCtrl_Set12);
                        break;
                    default:
                        form1.SetPreferences(form1.userCtrl_Set12);
                        break;
                }
                form1.PreviewView = true;
                timer1.Enabled = true;
                mask.Dispose();
            }
        }

        private void button_AnimationReset_Click(object sender, EventArgs e)
        {
            time_value_sec = 0;
        }

        private void FormAnimation_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            this.Dispose();
        }

        private void FormAnimation_Shown(object sender, EventArgs e)
        {
            radioButton_CheckedChanged(sender, e);
        }

        private void FormAnimation_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                radioButton_normal.Checked = true;
            }
        }
    }
}
