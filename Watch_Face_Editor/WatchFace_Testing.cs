using ImageMagick;
using Newtonsoft.Json;
using Octokit;
using QRCoder;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SocketIOClient;
using SocketIOClient.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Automation;
using System.Windows.Forms;
using static Watch_Face_Editor.Form1;
using Application = System.Windows.Forms.Application;
using Device = SharpDX.Direct3D11.Device;
using DxgiResource = SharpDX.DXGI.Resource;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private SocketIO socket;
        private const int previewId = 1;
        private const string previewMethod = "ide.simulator.preview";
        IntPtr hWnd_ModelEmulator; // Дескриптор окна эмулятора

        private Rectangle captureArea = new Rectangle(0, 0, 480, 480);// Область захвата
        private CancellationTokenSource cts;
        private IScreenCapture capture;

        Bitmap currentFrame; // Временное хранение текущего кадра
        private readonly object frameLock = new object();

        public interface IScreenCapture
        {
            void Start(Rectangle area);
            void Stop();
            Bitmap GetFrame();
            bool IsSupported();
        }

        public struct UploadProgress
        {
            // Текстовое описание текущего действия (например, "Чтение файла...", "Отправка в сеть...")
            public string Stage { get; set; }

            // Процент выполнения (от 0 до 100)
            //public int Percentage { get; set; }
        }

        int EmulatorAreaPosX = 0;
        int EmulatorAreaPosY = 0;
        int EmulatorAreaWidth = 0;
        int EmulatorAreaHeight = 0;
        int EmulatorAreaOffsetX = 0;
        int EmulatorAreaOffsetY = 0;

        # region Файлообменники

        private async void btnUploadToLitterbox_Click(object sender, EventArgs e)
        {
            string zpkName = "";
            bool fromProject = true;
            if (ProjectDir == null || Watch_Face == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = Properties.FormStrings.FilterZpk;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Unpack;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    zpkName = openFileDialog.FileName;
                    NameForQR = Path.GetFileNameWithoutExtension(zpkName);
                    fromProject = false;
                }
            }
            else
            {
                string SaveFileName = Path.GetFileNameWithoutExtension(FileName);
                if (Watch_Face.WatchFace_Info?.WatchFaceName != null && Watch_Face.WatchFace_Info.WatchFaceName != "")
                    SaveFileName = Watch_Face.WatchFace_Info.WatchFaceName;
                zpkName = ProjectDir + @"\" + SaveFileName + ".zpk";
                string zipName = ProjectDir + @"\" + SaveFileName + ".zip";

                if (File.Exists(zpkName))
                {
                    DialogResult dr = MessageBox.Show(Properties.FormStrings.Message_Warning_OverwriteFile_Text,
                            Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        zipName = await CreateWatchFace(zipName);
                        if (File.Exists(zipName)) zpkName = CreateZPK(zipName);
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    zipName = await CreateWatchFace(zipName);
                    if (File.Exists(zipName)) zpkName = CreateZPK(zipName);
                }
            }
            progressBar1.Visible = false;

            if (File.Exists(zpkName))
            {
                resetQRCode();

                try
                {
                    //string url = UploadToLitterbox(zpkName);
                    string url = await UploadToLitterboxAsync(zpkName);
                    textBox_FileSharingURL.Text = url.Replace("https://", "").Replace("http://", "");

                    string correctedURL = CorrectUrl(url);
                    //CheckFileExists(correctedURL);

                    //textBox_LitterboxURL.Text = correctedURL;

                    Bitmap currentQRCode = GenerateQRCodeWithLogo(correctedURL);
                    pictureBoxQRCode.Image = currentQRCode;
                    pictureBoxQRCode.Visible = true;
                    button_saveQR.Enabled = true;
                    checkBox_AddPreviewQR.Enabled = fromProject;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Upload error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                }

                string tempDir = Application.StartupPath + @"\Temp";
                if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
            }
        }

        private async void btnUploadToFilePost_Click(object sender, EventArgs e)
        {
            string apiKey = textBox_FilePost_API_key.Text;
            bool isApiKeyValid = await CheckFilePostApiKeyAsync(apiKey);

            if (!isApiKeyValid)
            {
                MessageBox.Show(
                    Properties.FormStrings.Message_Warning_InvalidAPIKey_Text,
                    Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string zpkName = "";
            bool fromProject = true;
            if (ProjectDir == null || Watch_Face == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = Properties.FormStrings.FilterZpk;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Unpack;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    zpkName = openFileDialog.FileName;
                    NameForQR = Path.GetFileNameWithoutExtension(zpkName);
                    fromProject = false;
                }
            }
            else
            {
                string SaveFileName = Path.GetFileNameWithoutExtension(FileName);
                if (Watch_Face.WatchFace_Info?.WatchFaceName != null && Watch_Face.WatchFace_Info.WatchFaceName != "")
                    SaveFileName = Watch_Face.WatchFace_Info.WatchFaceName;
                zpkName = ProjectDir + @"\" + SaveFileName + ".zpk";
                string zipName = ProjectDir + @"\" + SaveFileName + ".zip";

                if (File.Exists(zpkName))
                {
                    DialogResult dr = MessageBox.Show(Properties.FormStrings.Message_Warning_OverwriteFile_Text,
                            Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        zipName = await CreateWatchFace(zipName);
                        if (File.Exists(zipName)) zpkName = CreateZPK(zipName);
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    zipName = await CreateWatchFace(zipName);
                    if (File.Exists(zipName)) zpkName = CreateZPK(zipName);
                }
            }
            progressBar1.Visible = false;

            if (File.Exists(zpkName))
            {
                resetQRCode();

                try
                {
                    //string url = UploadToLitterbox(zpkName);
                    string url = await UploadToFilePostAsync(zpkName, apiKey);
                    textBox_FileSharingURL.Text = url.Replace("https://", "").Replace("http://", "");

                    string correctedURL = CorrectUrl(url);
                    //CheckFileExists(correctedURL);

                    //textBox_LitterboxURL.Text = correctedURL;

                    Bitmap currentQRCode = GenerateQRCodeWithLogo(correctedURL);
                    pictureBoxQRCode.Image = currentQRCode;
                    pictureBoxQRCode.Visible = true;
                    button_saveQR.Enabled = true;
                    checkBox_AddPreviewQR.Enabled = fromProject;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Upload error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                }

                panel_FilePost_info.Visible = true;

                string tempDir = Application.StartupPath + @"\Temp";
                if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);

                try
                {
                    FilePostAccountInfo info = await GetFilePostAccountAsync(apiKey);

                    panel_FilePost_info.Visible = true;
                    if (info.email != null) label_FilePost_info_email.Text = "E-mail: " + info.email;

                    if (info.usage != null)
                    {
                        if (info.usage.uploads_this_month != 0 && info.usage.uploads_limit != 0)
                        {
                            string uploads_this_month = ": " + info.usage.uploads_this_month.ToString() + " / " +
                                info.usage.uploads_limit.ToString() + " (" +
                                (Math.Round(100f * info.usage.uploads_this_month / info.usage.uploads_limit, 1)).ToString() + "%)";
                            label_FilePost_info_uploads_month.Text = Properties.FormStrings.FilePost_info_uploads_month + uploads_this_month;
                        }

                    }
                }
                catch (Exception ex)
                {
                    label_FilePost_info_email.Text = "E-mail: -";
                    label_FilePost_info_uploads_month.Text = Properties.FormStrings.FilePost_info_uploads_month + ": -";
                    label_FilePost_info_storage_used.Text = Properties.FormStrings.FilePost_info_storage_used + ": -";
                    MessageBox.Show(
                        ex.Message,
                        "Get account info error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private async void btnUploadToGitHub_Click(object sender, EventArgs e)
        {
            string zpkName = "";
            bool fromProject = true;
            if (ProjectDir == null || Watch_Face == null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = Properties.FormStrings.FilterZpk;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;
                //openFileDialog.Title = Properties.FormStrings.Dialog_Title_Unpack;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    zpkName = openFileDialog.FileName;
                    NameForQR = Path.GetFileNameWithoutExtension(zpkName);
                    fromProject = false;
                }
            }
            else
            {
                string SaveFileName = Path.GetFileNameWithoutExtension(FileName);
                if (Watch_Face.WatchFace_Info?.WatchFaceName != null && Watch_Face.WatchFace_Info.WatchFaceName != "")
                    SaveFileName = Watch_Face.WatchFace_Info.WatchFaceName;
                zpkName = ProjectDir + @"\" + SaveFileName + ".zpk";
                string zipName = ProjectDir + @"\" + SaveFileName + ".zip";

                if (File.Exists(zpkName))
                {
                    DialogResult dr = MessageBox.Show(Properties.FormStrings.Message_Warning_OverwriteFile_Text,
                            Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        zipName = await CreateWatchFace(zipName);
                        if (File.Exists(zipName)) zpkName = CreateZPK(zipName);
                    }
                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else
                {
                    zipName = await CreateWatchFace(zipName);
                    if (File.Exists(zipName)) zpkName = CreateZPK(zipName);
                }
            }
            progressBar1.Visible = false;

            if (File.Exists(zpkName))
            {
                resetQRCode();

                try
                {
                    string token = textBox_GitHub_token.Text;
                    string owner = textBox_GitHub_owner.Text;
                    string repoName = textBox_GitHub_repoName.Text;
                    string branch = "main";
                    string localFilePath = zpkName;
                    string targetFilePath = textBox_GitHub_filePath.Text;
                    targetFilePath = targetFilePath.Trim('/');
                    targetFilePath += "/" + Path.GetFileName(localFilePath);
                    targetFilePath = targetFilePath.TrimStart('/');

                    // 1. Блокируем кнопку, чтобы пользователь не нажал её дважды во время загрузки
                    btnUploadToGitHub.Enabled = false;

                    // Сбрасываем прогресс-бар в ноль
                    progressBar1.Value = 0;
                    lblStatus.Text = Properties.FormStrings.GitHub_PreparingForLaunch;

                    // 2. Создаем обработчик прогресса.
                    // Благодаря магии IProgress<T>, этот код выполнится безопасно в UI-потоке.
                    var uiProgress = new Progress<UploadProgress>(progress =>
                    {
                        //progressBar1.Value = progress.Percentage;
                        lblStatus.Text = progress.Stage;
                    });

                    try
                    {
                        Console.WriteLine("Проверка учетных данных GitHub...");
                        // Шаг 1: Сначала проверяем токен и логин
                        lblStatus.Text = Properties.FormStrings.GitHub_CheckingToken;
                        await GitHubRepoChecker.ValidateCredentialsAsync(token, owner);
                        lblStatus.Text = Properties.FormStrings.GitHub_CheckingRepository;
                        await GitHubRepoChecker.CheckRepositoryExistsAsync(token, owner, repoName);

                        // Шаг 2: Если проверка прошла, загружаем архив
                        Console.WriteLine("Начало загрузки архива...");
                        string url = await GitHubArchiveUploader.UploadArchiveToGitHubAsync(
                            token, owner, repoName, branch, targetFilePath, localFilePath, 
                            Properties.FormStrings.GitHub_FileAdded + ": ",
                            checkBox_GitHub_AskConfirmation.Checked,
                            uiProgress
                        );

                        Console.WriteLine($"\nУспешно! Ссылка: {url}");

                        // Показываем результат
                        lblStatus.Text = Properties.FormStrings.GitHub_UploadSuccess;
                        //MessageBox.Show($"Файл успешно загружен!\nСсылка для скачивания:\n{url}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        string correctedURL = ConvertGitHubBlobToPagesUrl(url); // форматируем в правильный вид для GitHub
                        textBox_FileSharingURL.Text = correctedURL.Replace("https://", "").Replace("http://", "");
                        correctedURL = CorrectUrl(correctedURL); // Заменяем https на zpkd1

                        Bitmap currentQRCode = GenerateQRCodeWithLogo(correctedURL);
                        pictureBoxQRCode.Image = currentQRCode;
                        pictureBoxQRCode.Visible = true;
                        button_saveQR.Enabled = true;
                        checkBox_AddPreviewQR.Enabled = fromProject;

                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"\nОшибка авторизации: {ex.Message}");
                        MessageBox.Show(ex.Message, Properties.FormStrings.Message_AuthorisationError_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = Properties.FormStrings.Message_AuthorisationError_Caption;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"\nОшибка конфигурации: {ex.Message}");
                        MessageBox.Show(ex.Message, Properties.FormStrings.Message_ConfigurationError_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = Properties.FormStrings.Message_ConfigurationError_Caption;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nНепредвиденная ошибка: {ex.Message}");
                        MessageBox.Show(ex.Message, Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = Properties.FormStrings.Message_Error_Caption;
                    }
                    finally
                    {
                        // 4. В любом случае возвращаем кнопку в рабочее состояние
                        btnUploadToGitHub.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Upload error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                }

                string tempDir = Application.StartupPath + @"\Temp";
                if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
            }
        }

        private void pictureBox_GitHub_help_Click(object sender, EventArgs e)
        {
            label_GitHub_Instructions.Visible = !label_GitHub_Instructions.Visible;
        }

        private void resetQRCode()
        {
            pictureBoxQRCode.Visible = false;
            pictureBoxQRCode.Image = null;
            textBox_FileSharingURL.Text = "";
            //label_URL_status.Text = "❌";
            //label_URL_status.ForeColor = Color.Red;
            button_saveQR.Enabled = false;
            checkBox_AddPreviewQR.Enabled = false;
            lblStatus.Text = "";
        }

        public async Task<string> UploadToLitterboxAsync(string filePath)
        {
            string timeString = "1h";

            if (radioButton_12hours.Checked) timeString = "12h";
            else if (radioButton_1day.Checked) timeString = "24h";
            else if (radioButton_3days.Checked) timeString = "72h";
            NameValueCollection values = new NameValueCollection();

            values["reqtype"] = "fileupload";
            values["time"] = timeString;
            values["fileNameLength"] = "16";

            NameValueCollection headers = new NameValueCollection();

            headers["Accept"] = "text/plain, */*";
            headers["Origin"] = "https://litterbox.catbox.moe";
            headers["Referer"] = "https://litterbox.catbox.moe/";

            byte[] responseBytes =
                await UploadMultipartAsync(
                    "https://litterbox.catbox.moe/resources/internals/api.php",
                    filePath,
                    "fileToUpload",
                    values,
                    headers);

            string response = Encoding.UTF8.GetString(responseBytes).Trim();

            return response;
        }

        public async Task<string> UploadToFilePostAsync(string filePath, string apiKey)
        {
            NameValueCollection headers = new NameValueCollection();

            headers["X-API-Key"] = apiKey;

            byte[] responseBytes =
                await UploadMultipartAsync(
                    "https://filepost.dev/v1/upload",
                    filePath,
                    "file",
                    null,
                    headers);

            string response =
                Encoding.UTF8.GetString(responseBytes);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            dynamic json = serializer.Deserialize<dynamic>(response);

            if (json.ContainsKey("url")) return json["url"];

            throw new Exception("Invalid server response: " + response);
        }

        private async Task<byte[]> UploadMultipartAsync(
            string url,
            string filePath,
            string fileFieldName,
            NameValueCollection values,
            NameValueCollection headers = null)
        {
            using (WebClient client = new WebClient())
            {
                string boundary =
                    "---------------------------" +
                    DateTime.Now.Ticks.ToString("x");

                client.Headers.Add(
                    "Content-Type",
                    "multipart/form-data; boundary=" + boundary);

                client.Headers.Add(
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                // Дополнительные headers
                if (headers != null)
                {
                    foreach (string key in headers.AllKeys)
                    {
                        client.Headers[key] = headers[key];
                    }
                }

                // ProgressBar
                client.UploadProgressChanged += (s, e) =>
                {
                    int progress = 0;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = 100;
                    progressBar1.Value = 0;

                    if (e.TotalBytesToSend > 0)
                    {
                        progress = (int)(
                            (e.BytesSent * 100L) /
                            e.TotalBytesToSend);
                    }

                    if (progress < 0)
                        progress = 0;

                    if (progress > 100)
                        progress = 100;

                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke(new Action(() =>
                        {
                            progressBar1.Value = progress;
                        }));
                    }
                    else
                    {
                        progressBar1.Value = progress;
                    }
                };

                using (MemoryStream stream = new MemoryStream())
                {
                    // Текстовые поля
                    if (values != null)
                    {
                        foreach (string key in values.AllKeys)
                        {
                            byte[] part = Encoding.UTF8.GetBytes(
                                "--" + boundary + "\r\n" +
                                "Content-Disposition: form-data; name=\"" +
                                key + "\"\r\n\r\n" +
                                values[key] + "\r\n");

                            await stream.WriteAsync(part, 0, part.Length);
                        }
                    }

                    // Заголовок файла
                    string fileName = Path.GetFileName(filePath);

                    byte[] fileHeader = Encoding.UTF8.GetBytes(
                        "--" + boundary + "\r\n" +
                        "Content-Disposition: form-data; name=\"" +
                        fileFieldName +
                        "\"; filename=\"" +
                        fileName +
                        "\"\r\n" +
                        "Content-Type: application/octet-stream\r\n\r\n");

                    await stream.WriteAsync(
                        fileHeader,
                        0,
                        fileHeader.Length);

                    // Файл
                    using (FileStream fs = new FileStream(
                        filePath,
                        System.IO.FileMode.Open,
                        FileAccess.Read))
                    {
                        await fs.CopyToAsync(stream);
                    }

                    // Завершение multipart
                    byte[] trailer = Encoding.UTF8.GetBytes(
                        "\r\n--" + boundary + "--\r\n");

                    await stream.WriteAsync(
                        trailer,
                        0,
                        trailer.Length);

                    // Сброс progressbar
                    progressBar1.Value = 0;

                    // Upload async
                    return await client.UploadDataTaskAsync(
                        new Uri(url),
                        "POST",
                        stream.ToArray());
                }
            }
        }

        private string CorrectUrl(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // если начинается с https:// или http:// — заменяем на zpkd1://
            if (input.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                input.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                return input.Replace("https://", "zpkd1://")
                            .Replace("http://", "zpkd1://");
            }

            // если вообще не начинается ни с zpkd1://, ни с http(s)
            if (!input.StartsWith("zpkd1://", StringComparison.OrdinalIgnoreCase))
            {
                return "zpkd1://" + input;
            }

            return input;
        }

        public static string ConvertGitHubBlobToPagesUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            try
            {
                Uri uri = new Uri(url);
                string[] segments = uri.AbsolutePath.Trim('/').Split('/');

                if (segments.Length >= 5 &&
                    (segments[2] == "blob" || segments[2] == "tree") &&
                    segments[3] == "main")
                {
                    string user = segments[0];
                    string repo = segments[1];
                    string path = string.Join("/", segments.Skip(4));

                    // Проверка: если репозиторий называется UserName.github.io
                    if (repo.Equals($"{user}.github.io", StringComparison.OrdinalIgnoreCase))
                    {
                        return $"https://{user}.github.io/{path}";
                    }
                    else
                    {
                        return $"https://{user}.github.io/{repo}/{path}";
                    }
                }
            }
            catch
            {
                // Игнорируем ошибки и возвращаем исходную ссылку
            }

            return url;
        }

        private Bitmap GenerateQRCodeWithLogo(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrRaw = qrCode.GetGraphic(10, Color.Black, Color.White, GetLogo(), 20, 5, false);
                Bitmap qrCodeImage = AddQuietZone(qrRaw, quietZoneModules: 2, pixelsPerModule: 10);
                return qrCodeImage;
            }
        }
        private Bitmap AddQuietZone(Bitmap originalQr, int quietZoneModules, int pixelsPerModule)
        {
            int quietZonePixels = quietZoneModules * pixelsPerModule;

            int newSize = originalQr.Width + quietZonePixels * 2;
            Bitmap result = new Bitmap(newSize, newSize);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.White); // цвет тихой зоны
                g.DrawImage(originalQr, quietZonePixels, quietZonePixels);
            }

            return result;
        }

        private Bitmap GetLogo()
        {
            // Предполагается, что logo.png добавлен в ресурсы проекта (Properties > Resources)
            return Properties.Resources.logo_qr;
        }


        private Bitmap DrawTextWithOptions(Bitmap image, string text, string position, int fontSize, int textAreaHeight, Color backgroundColor)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new Bitmap(image); // Без текста — просто копия

            int padding = 10;
            int width = image.Width;
            int newHeight = image.Height + textAreaHeight;

            //Font font = new Font("Calibri", fontSize, FontStyle.Regular);
            Font font = GetSafeFont("Calibri", fontSize, FontStyle.Regular);
            Bitmap result = new Bitmap(width, newHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(backgroundColor);
                //g.Clear(Color.White);

                Rectangle textRect;
                Point imagePos;

                if (position.ToLower() == "top")
                {
                    // Текст сверху
                    textRect = new Rectangle(0, 0, width, textAreaHeight);
                    imagePos = new Point(0, textAreaHeight);
                }
                else
                {
                    // Текст снизу (по умолчанию)
                    textRect = new Rectangle(0, image.Height, width, textAreaHeight);
                    imagePos = new Point(0, 0);
                }

                // Отрисовка фона под текстом
                using (Brush bgBrush = new SolidBrush(backgroundColor))
                {
                    g.FillRectangle(bgBrush, textRect);
                }

                // Обрезка текста при необходимости
                string fittedText = FitTextToWidth(g, text, font, width - padding * 2);

                // Рисуем изображение
                g.DrawImage(image, imagePos);

                // Рисуем текст
                using (Brush textBrush = new SolidBrush(Color.Black))
                using (StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    g.DrawString(fittedText, font, textBrush, textRect, format);
                }
            }

            return result;
        }

        private Bitmap DrawTextWrapped(Bitmap image, string text, string position, int fontSize, Color backgroundColor, int minTextAreaHeight)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new Bitmap(image);

            Font font = GetSafeFont("Calibri", fontSize, FontStyle.Regular);
            int width = image.Width;
            int textPadding = 10;

            // Определяем высоту текста с переносами
            SizeF textSize;
            using (Bitmap dummyBmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(dummyBmp))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                RectangleF layout = new RectangleF(0, 0, width - textPadding * 2, float.MaxValue);
                textSize = g.MeasureString(text, font, layout.Size.ToSize(), new StringFormat());
            }

            int calculatedTextHeight = (int)Math.Ceiling(textSize.Height) + textPadding * 2;
            int textAreaHeight = Math.Max(calculatedTextHeight, minTextAreaHeight);

            int newHeight = image.Height + textAreaHeight;
            Bitmap result = new Bitmap(width, newHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //g.Clear(Color.White);
                g.Clear(backgroundColor);

                Rectangle textRect;
                Point imagePos;

                if (position.ToLower() == "top")
                {
                    textRect = new Rectangle(0, 0, width, textAreaHeight);
                    imagePos = new Point(0, textAreaHeight);
                }
                else
                {
                    textRect = new Rectangle(0, image.Height, width, textAreaHeight);
                    imagePos = new Point(0, 0);
                }

                // Фон текста
                using (Brush bgBrush = new SolidBrush(backgroundColor))
                {
                    g.FillRectangle(bgBrush, textRect);
                }

                // Рисуем изображение
                g.DrawImage(image, imagePos);

                // Рисуем текст с переносом
                using (Brush textBrush = new SolidBrush(Color.Black))
                using (StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.LineLimit
                })
                {
                    Rectangle paddedRect = new Rectangle(textRect.X + textPadding, textRect.Y + textPadding,
                                                         textRect.Width - textPadding * 2,
                                                         textRect.Height - textPadding * 2);
                    g.DrawString(text, font, textBrush, paddedRect, format);
                }
            }

            return result;
        }

        private Font GetSafeFont(string preferredFontName, float fontSize, FontStyle style, string fallbackFontName = "Segoe UI")
        {
            using (InstalledFontCollection fonts = new InstalledFontCollection())
            {
                bool hasPreferred = fonts.Families.Any(f => f.Name.Equals(preferredFontName, StringComparison.OrdinalIgnoreCase));

                string fontToUse = hasPreferred ? preferredFontName : fallbackFontName;
                return new Font(fontToUse, fontSize, style);
            }
        }

        private string FitTextToWidth(Graphics g, string text, Font font, int maxWidth)
        {
            const string ellipsis = "...";

            if (string.IsNullOrEmpty(text))
                return "";

            // Если весь текст помещается — возвращаем его
            if (g.MeasureString(text, font).Width <= maxWidth)
                return text;

            // Подрезаем посимвольно
            for (int i = text.Length - 1; i > 0; i--)
            {
                string cut = text.Substring(0, i).Trim() + ellipsis;
                if (g.MeasureString(cut, font).Width <= maxWidth)
                    return cut;
            }

            // Если даже один символ + "..." не влезает — возвращаем "..."
            return ellipsis;
        }

        private Bitmap ApplyRoundedCorners(Bitmap original, int cornerRadius)
        {
            Bitmap rounded = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(rounded))
            using (GraphicsPath path = RoundedRect(new Rectangle(0, 0, original.Width, original.Height), cornerRadius))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (Brush imageBrush = new TextureBrush(original))
                {
                    g.FillPath(imageBrush, path);
                }
            }

            return rounded;
        }

        private void DrawRoundedBorder(Graphics g, int width, int height, int cornerRadius, int borderWidth, Color borderColor)
        {
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                pen.Alignment = PenAlignment.Center;
                int d = cornerRadius * 2;

                Rectangle topLeftArc = new Rectangle(0, 0, d, d);
                Rectangle topRightArc = new Rectangle(width - d, 0, d, d);
                Rectangle bottomRightArc = new Rectangle(width - d, height - d, d, d);
                Rectangle bottomLeftArc = new Rectangle(0, height - d, d, d);

                // Углы
                g.DrawArc(pen, topLeftArc, 180, 90);
                g.DrawArc(pen, topRightArc, 270, 90);
                g.DrawArc(pen, bottomRightArc, 0, 90);
                g.DrawArc(pen, bottomLeftArc, 90, 90);

                // Прямые линии между углами
                g.DrawLine(pen, cornerRadius, 0, width - cornerRadius, 0);               // Верх
                g.DrawLine(pen, width, cornerRadius, width, height - cornerRadius);      // Право
                g.DrawLine(pen, width - cornerRadius, height, cornerRadius, height);     // Низ
                g.DrawLine(pen, 0, height - cornerRadius, 0, cornerRadius);              // Лево
            }
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            // 4 дуги
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }

        private Bitmap PadImageWithMargins(Bitmap original, int marginLeft, int marginTop, int marginRight, int marginBottom, Color backgroundColor)
        {
            int newWidth = original.Width + marginLeft + marginRight;
            int newHeight = original.Height + marginTop + marginBottom;

            Bitmap padded = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(padded))
            {
                g.Clear(backgroundColor);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.DrawImage(original, marginLeft, marginTop, original.Width, original.Height);
            }

            return padded;
        }

        private Bitmap AddPreviewWithMargins(Bitmap original, Bitmap preview, int margin, Color backgroundColor)
        {
            float previewScale = original.Height / (float)preview.Height;
            int newPreviewWidth = (int)Math.Round(preview.Width * previewScale);
            int newPreviewHeight = (int)Math.Round(preview.Height * previewScale);
            Bitmap scaledPreview = new Bitmap(newPreviewWidth, newPreviewHeight);
            using (Graphics g = Graphics.FromImage(scaledPreview))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(preview, 0, 0, newPreviewWidth, newPreviewHeight);
            }

            int newWidth = original.Width + margin + scaledPreview.Width;

            Bitmap padded = new Bitmap(newWidth, original.Height);
            using (Graphics g = Graphics.FromImage(padded))
            {
                g.Clear(backgroundColor);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.DrawImage(original, 0, 0, original.Width, original.Height);
                g.DrawImage(scaledPreview, original.Width + margin, 0, scaledPreview.Width, scaledPreview.Height);
            }

            return padded;
        }

        public static async Task<bool> UrlFileExistsAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    using (var response = await client.SendAsync(
                        new HttpRequestMessage(HttpMethod.Head, url)))
                    {
                        return response.IsSuccessStatusCode;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckFilePostApiKeyAsync(string apiKey)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(
                        "User-Agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                    client.Headers.Add("X-API-Key", apiKey);

                    byte[] responseBytes = await client.DownloadDataTaskAsync("https://filepost.dev/v1/account");

                    string response = Encoding.UTF8.GetString(responseBytes);

                    // если дошли сюда и получили JSON → ключ валиден
                    return true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse response)
                {
                    // стандартные ошибки авторизации
                    if (response.StatusCode == HttpStatusCode.Unauthorized ||
                        response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        return false;
                    }

                    // другие ошибки (500, network и т.д.)
                    throw;
                }

                throw;
            }
        }

        public async Task<FilePostAccountInfo> GetFilePostAccountAsync(string apiKey)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                client.Headers.Add("X-API-Key", apiKey);

                byte[] responseBytes = await client.DownloadDataTaskAsync("https://filepost.dev/v1/account");

                string json = Encoding.UTF8.GetString(responseBytes);
                Console.WriteLine("FilePost account info JSON: " + json);

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                return serializer.Deserialize<FilePostAccountInfo>(json);
            }
        }

        private void button_saveQR_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                string nameText = NameForQR;
                if (FileName != null) nameText = Path.GetFileNameWithoutExtension(FileName);
                if (Watch_Face != null && Watch_Face.WatchFace_Info != null)
                {
                    if (Watch_Face.WatchFace_Info.WatchFaceName != null && Watch_Face.WatchFace_Info.WatchFaceName != "")
                        nameText = Watch_Face.WatchFace_Info.WatchFaceName;
                    nameText = nameText.Trim();
                    if (Watch_Face.WatchFace_Info.WatchFaceVersion != 0) nameText += " v" + Watch_Face.WatchFace_Info.WatchFaceVersion.ToString();
                }
                string fileName = "QRCode.png";
                if (!string.IsNullOrWhiteSpace(nameText)) fileName = "QRCode " + nameText + ".png";
                fileName = fileName.Replace(' ', '_');

                saveDialog.Filter = Properties.FormStrings.FilterPng;
                saveDialog.Title = Properties.FormStrings.Dialog_Title_SaveQR;
                saveDialog.FileName = fileName;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    bool topText = false;
                    bool bottomText = false;

                    // Добавляем обводку QR-кода
                    Bitmap currentQRCode = pictureBoxQRCode.Image as Bitmap;
                    using (Graphics g = Graphics.FromImage(currentQRCode))
                    {
                        DrawRoundedBorder(g, currentQRCode.Width, currentQRCode.Height, cornerRadius: 15, borderWidth: 4, borderColor: Color.Black);
                    }
                    Bitmap roundedQRCode = ApplyRoundedCorners(currentQRCode, cornerRadius: 15);

                    // Добавляем предпросмотр
                    if (checkBox_AddPreviewQR.Checked)
                    {
                        if (Watch_Face?.WatchFace_Info?.Preview != null)
                        {
                            Bitmap preview = OpenFileStream(Watch_Face.WatchFace_Info.Preview);
                            if (preview != null) roundedQRCode = AddPreviewWithMargins(roundedQRCode, preview, margin: 20, backgroundColor: Color.FromArgb(255, 255, 224, 178));
                        }
                    }

                    //int textAreaHeight = 30;
                    //if (!string.IsNullOrWhiteSpace(nameText)) textAreaHeight = 25;
                    //Bitmap imageWithType = DrawTextWithOptions(
                    //    roundedQRCode,
                    //    text: type,
                    //    position: "top",
                    //    fontSize: 16,
                    //    textAreaHeight: textAreaHeight,
                    //    backgroundColor: Color.FromArgb(255, 255, 224, 178)
                    //);

                    // Имя и версия приложения
                    Bitmap imageWithName = DrawTextWithOptions(
                        roundedQRCode,
                        text: nameText,
                        position: "top",
                        fontSize: 24,
                        textAreaHeight: 40,
                        backgroundColor: Color.FromArgb(255, 255, 224, 178)
                    );
                    if (!string.IsNullOrWhiteSpace(nameText))
                        imageWithName = PadImageWithMargins(imageWithName, 0, 5, 0, 0, Color.FromArgb(255, 255, 224, 178));
                    if (imageWithName.Height > currentQRCode.Height) topText = true;

                    string modelsText = ProgramSettings.Watch_Model;
                    if (SelectedModel.screenType == "round") modelsText = "○ " + modelsText;
                    else if (SelectedModel.screenType == "square") modelsText = "▢ " + modelsText;
                    else if (SelectedModel.screenType == "bar") modelsText = "▯ " + modelsText;
                    modelsText += " (" + SelectedModel.background.w + "x" + SelectedModel.background.h + ")";
                    //Bitmap imageWithModels = DrawTextWrapped(
                    Bitmap imageWithModels = DrawTextWithOptions(
                            imageWithName,
                            text: modelsText,
                            position: "bottom",
                            fontSize: 24,
                            textAreaHeight: 40,
                            backgroundColor: Color.FromArgb(255, 255, 224, 178)
                        );
                    if (imageWithModels.Height > imageWithName.Height) bottomText = true;

                    if (imageWithModels.Height > currentQRCode.Height)
                    {
                        int marginLeft = 0;
                        int marginTop = 0;
                        int marginRight = 0;
                        int marginBottom = 0;

                        if (topText && bottomText)
                        {
                            marginLeft = 30;
                            marginRight = 30;
                        }
                        else if (topText && !bottomText)
                        {
                            marginLeft = 20;
                            marginRight = 20;
                            marginBottom = 20;
                        }
                        else if (!topText && bottomText)
                        {
                            marginLeft = 20;
                            marginRight = 20;
                            marginTop = 20;
                        }

                        Bitmap finalSize = PadImageWithMargins(imageWithModels, marginLeft, marginTop, marginRight, marginBottom, Color.FromArgb(255, 255, 224, 178));
                        // Добавляем обводку всего изображения
                        using (Graphics g = Graphics.FromImage(finalSize))
                        {
                            DrawRoundedBorder(g, finalSize.Width, finalSize.Height, cornerRadius: 30, borderWidth: 6, borderColor: Color.Black);
                        }
                        Bitmap finalSizeRounded = ApplyRoundedCorners(finalSize, cornerRadius: 30);
                        finalSizeRounded.Save(saveDialog.FileName, ImageFormat.Png);
                    }
                    else imageWithModels.Save(saveDialog.FileName, ImageFormat.Png);

                    if (File.Exists(saveDialog.FileName))
                    {
                        Process.Start("explorer.exe", $"/select,\"{saveDialog.FileName}\"");
                    }
                }
            }
        }

        private void textBox_FilePost_API_key_TextChanged(object sender, EventArgs e)
        {
            if (textBox_FilePost_API_key.Text.Length > 10) btnUploadToFilePost.Enabled = true;
            else btnUploadToFilePost.Enabled = false;
            if (Settings_Load) return;

            ProgramSettings.FilePost_API_key = SecretStorage.Encrypt(textBox_FilePost_API_key.Text);
            Save_Settings();
        }

        private void textBox_GitHub_TextChanged(object sender, EventArgs e)
        {
            if (textBox_GitHub_owner.Text.Length > 0 && textBox_GitHub_token.Text.Length > 0 &&
                    textBox_GitHub_repoName.Text.Length > 0) btnUploadToGitHub.Enabled = true;
            else btnUploadToGitHub.Enabled = false;
            if (Settings_Load) return;

            ProgramSettings.GitHub_owner = textBox_GitHub_owner.Text;
            ProgramSettings.GitHub_token = SecretStorage.Encrypt(textBox_GitHub_token.Text);
            ProgramSettings.GitHub_repoName = textBox_GitHub_repoName.Text;
            ProgramSettings.GitHub_filePath = textBox_GitHub_filePath.Text;
            ProgramSettings.GitHub_AskConfirmation = checkBox_GitHub_AskConfirmation.Checked;

            Save_Settings();
        }

        private void radioButton_FileSharing_CheckedChanged(object sender, EventArgs e)
        {
            //RadioButton radioButton = (RadioButton)sender;
            panel_Litterbox.Visible = radioButton_Litterbox.Checked;
            panel_FilePost.Visible = radioButton_FilePost.Checked;
            panel_GitHub.Visible = radioButton_GitHub.Checked;

            panel_FilePost.Location = panel_Litterbox.Location;
            panel_GitHub.Location = panel_Litterbox.Location;
            panel_FilePost_info.Visible = false;
        }

        private void panel_GitHub_VisibleChanged(object sender, EventArgs e)
        {
            if (panel_GitHub.Visible) 
            {
                if(textBox_GitHub_owner.Text.Length > 0 && textBox_GitHub_token.Text.Length > 0 &&
                    textBox_GitHub_repoName.Text.Length > 0 && textBox_GitHub_filePath.Text.Length > 0) btnUploadToGitHub.Enabled = true;
                else btnUploadToGitHub.Enabled = false;
            }
        }
        #endregion

        #region ZeppPlyer

        private void button_ZeppPlayerBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ZeppPlayer.exe|ZeppPlayer.exe";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "ZeppPlayer";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_ZeppPlayerPath.Text = openFileDialog.FileName;
                ProgramSettings.ZeppPlayerPath = openFileDialog.FileName;
                Save_Settings();
            }
        }

        private async void button_ZeppPlayerSend_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBox_ZeppPlayerPath.Text))
            {
                MessageBox.Show(Properties.FormStrings.Message_ZeppPlayerNotFound,
                    Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string projectPath = Path.GetDirectoryName(textBox_ZeppPlayerPath.Text) + @"\projects\";
            if (!Directory.Exists(projectPath)) Directory.CreateDirectory(projectPath);
            string projectName = Path.GetFileNameWithoutExtension(FileName);
            if (Watch_Face.WatchFace_Info?.WatchFaceName != null && Watch_Face.WatchFace_Info.WatchFaceName != "")
                projectName = Watch_Face.WatchFace_Info.WatchFaceName;
            projectPath += projectName;

            await CreateWatchFace_ZeppPlayer(projectPath, true);
            MessageBox.Show(Properties.FormStrings.Message_Simulator_PushSuccess_Text,
                Properties.FormStrings.Message_Infirmation_Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            string tempDir = Application.StartupPath + @"\Temp";
            if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
        }

        private void textBox_ZeppPlayerPath_TextChanged(object sender, EventArgs e)
        {
            button_ZeppPlayerSend.Enabled = File.Exists(textBox_ZeppPlayerPath.Text);
        }

        private void checkBox_ZeppPlayerCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (button_CaptureStop.Enabled) button_CaptureStop_Click(null, null);
            CheckBox checkBox = sender as CheckBox;

            panel_SimulatorCapture.Visible = checkBox.Checked;
            groupBox_simulator.Visible = !checkBox.Checked;

            //if (checkBox.Checked) radioButton_Litterbox.Checked = true;
            if (checkBox.Checked) label_GitHub_Instructions.Visible = false;
        }

        private void button_ZeppPlayer_UpdateScreenPos_Click(object sender, EventArgs e)
        {
            updateWindowsPos();
        }

        private void UpdateZeppPlayerPos()
        {
            var rect = UIAHelper.FindChromeElementRect(
                "ZeppPlayer - Google Chrome",
                "display");

            if (rect != null)
            {
                //Console.WriteLine($"X={rect.Value.X}");
                //Console.WriteLine($"Y={rect.Value.Y}");
                //Console.WriteLine($"W={rect.Value.Width}");
                //Console.WriteLine($"H={rect.Value.Height}");

                numericUpDown_CapturePosX.Value = rect.Value.X;
                numericUpDown_CapturePosY.Value = rect.Value.Y;
                numericUpDown_CaptureHeight.Value = rect.Value.Height;

                numericUpDown_CaptureOffsetX.Value = 0;
                numericUpDown_CaptureOffsetY.Value = 0;
            }
            else
            {
                MessageBox.Show(Properties.FormStrings.Message_ZeppPlayerTabNotFound1 + Environment.NewLine +
                    Properties.FormStrings.Message_ZeppPlayerTabNotFound2,
                    Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task CreateWatchFace_ZeppPlayer(string projectPath, bool isSimulator = false)
        {
            try
            {
                if (Directory.Exists(projectPath)) DeleteDirectory(projectPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.FormStrings.Message_DontDelZip + Environment.NewLine + Environment.NewLine + ex.Message,
                    Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string templatesFileDir = Application.StartupPath + @"\File_templates";

            //if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true);
            if (Directory.Exists(projectPath)) DeleteDirectory(projectPath);
            Directory.CreateDirectory(projectPath);
            Directory.CreateDirectory(projectPath + @"\assets");
            Directory.CreateDirectory(projectPath + @"\watchface");

            string imagesFolder = ProjectDir + @"\assets";
            DirectoryInfo Folder;
            Folder = new DirectoryInfo(imagesFolder);

            // читаем подпапки в папках
            List<string> allDirs = GetRecursDirectories(ProjectDir + @"\assets", 5, ProjectDir + @"\assets");
            foreach (string dirNames in allDirs)
            {
                //Console.WriteLine(dirNames);
                if (!Directory.Exists(projectPath + @"\assets" + dirNames)) Directory.CreateDirectory(projectPath + @"\assets" + dirNames);
            }
            List<string> allImagesFiles = GetRecursFiles(ProjectDir + @"\assets", "*.png", 5, ProjectDir + @"\assets");

            progressBar1.Value = 0;
            progressBar1.Maximum = allImagesFiles.Count;
            progressBar1.Visible = true;
            try
            {
                await Task.Run(() =>
                {
                    if (!isSimulator)
                    {
                        int fix_color = SelectedModel.colorScheme;
                        bool fix_size = SelectedModel.fixSize;

                        if (fix_color < 1 || fix_color > 3)
                        {
                            fix_color = 1;

                            Invoke(new Action(() =>
                            {
                                MessageBox.Show(
                                    Properties.FormStrings.Message_Wrong_ColorScheme + SelectedModel.name,
                                    Properties.FormStrings.Message_Warning_Caption,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }));
                        }

                        if (ProgramSettings.Use_ARGB_encoding)
                        {
                            DialogResult result = DialogResult.No;

                            Invoke(new Action(() =>
                            {
                                result = MessageBox.Show(
                                    Properties.FormStrings.Message_ARGB_Line1 + Environment.NewLine +
                                    Properties.FormStrings.Message_ARGB_Line2,
                                    Properties.FormStrings.Message_Warning_Caption,
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2);
                            }));

                            if (result != DialogResult.Yes)
                            {
                                ProgramSettings.Use_ARGB_encoding = false;

                                Invoke(new Action(() =>
                                {
                                    checkBox_Use_ARGB.Checked = false;
                                }));
                            }
                        }

                        foreach (string imgFileName in allImagesFiles)
                        {
                            string newDir = Path.GetDirectoryName(projectPath + @"\assets" + imgFileName);

                            ImageAutoDetectWriteFormat(
                                ProjectDir + @"\assets" + imgFileName,
                                newDir,
                                fix_size,
                                fix_color);

                            ValidateFileName(imgFileName);

                            Invoke(new Action(() =>
                            {
                                progressBar1.Value++;
                            }));
                        }
                    }
                    else
                    {
                        foreach (string imgFileName in allImagesFiles)
                        {
                            File.Copy(
                                ProjectDir + @"\assets" + imgFileName,
                                projectPath + @"\assets" + imgFileName,
                                true);

                            Invoke(new Action(() =>
                            {
                                progressBar1.Value++;
                            }));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error creating the watch face" + Environment.NewLine + Environment.NewLine + ex.Message,
                    Properties.FormStrings.Message_Error_Caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            App_WatchFace app = new App_WatchFace();
            app.app.appName = Path.GetFileNameWithoutExtension(FileName);
            app.i18n.enUS.appName = Path.GetFileNameWithoutExtension(FileName);
            if (Watch_Face.WatchFace_Info.WatchFaceName != null && Watch_Face.WatchFace_Info.WatchFaceName != "")
            {
                app.app.appName = Watch_Face.WatchFace_Info.WatchFaceName;
                app.i18n.enUS.appName = Watch_Face.WatchFace_Info.WatchFaceName;
            }
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
                app.app.version.code = Watch_Face.WatchFace_Info.WatchFaceVersion;
                app.app.version.name = Watch_Face.WatchFace_Info.WatchFaceVersion.ToString() + ".0.0";
            }
            if (Watch_Face.ScreenNormal != null && Watch_Face.ScreenNormal.Elements != null)
            {
                List<object> Elements = Watch_Face.ScreenNormal.Elements;
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
            if (Watch_Face.ScreenAOD != null)
            {
                if (Watch_Face.ScreenAOD.Elements != null && Watch_Face.ScreenAOD.Elements.Count > 0) app.module.watchface.lockscreen = 1;
                if (Watch_Face.ScreenAOD.Background != null && Watch_Face.ScreenAOD.Background.visible)
                {
                    if (Watch_Face.ScreenAOD.Background.BackgroundImage != null &&
                        Watch_Face.ScreenAOD.Background.BackgroundImage.src != null &&
                        Watch_Face.ScreenAOD.Background.BackgroundImage.src.Length > 0) app.module.watchface.lockscreen = 1;
                    if (Watch_Face.ScreenAOD.Background.BackgroundColor != null) app.module.watchface.lockscreen = 1;
                }
            }
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

            app.designWidth = SelectedModel.designWidth;
            foreach (int id in SelectedModel.deviceSource_ids)
            {
                app.platforms.Add(new Platform() { name = SelectedModel.name, deviceSource = id });
            }

            if (ProgramSettings.DevelopmentMode) app.packageInfo.mode = "development";

            int timeStamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            app.packageInfo.timeStamp = timeStamp;
            string appText = JsonConvert.SerializeObject(app, Formatting.Indented, new JsonSerializerSettings
            {
                //DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
            appText = appText.Replace("enUS", "en-US");
            File.WriteAllText(projectPath + @"\app.json", appText, new UTF8Encoding(false));

            File.Copy(templatesFileDir + @"\app.js", projectPath + @"\app.js");
            if (Directory.Exists(ProjectDir + @"\assets\fonts"))
            {
                List<string> allFontsFiles = GetRecursFiles(ProjectDir + @"\assets\fonts", "*", 5, ProjectDir + @"\assets");
                foreach (string fontFileName in allFontsFiles)
                {
                    ValidateFileName(fontFileName);
                }
                CopyDirectory(ProjectDir + @"\assets\fonts", projectPath + @"\assets\fonts", false);
            }

            // преобразуем настройки в текстовый файл
            string variables = "";
            string items = "";

            JsonToJS(out variables, out items);


            string indexText = File.ReadAllText(templatesFileDir + @"\index.js");
            string versionText = "v " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            indexText = indexText.Replace("* Watch_Face_Editor tool v*.*", "* Watch_Face_Editor tool " + versionText);

            if (variables.Length > 0) indexText = indexText.Replace("//Variable declaration section", variables);
            if (items.Length > 0) indexText = indexText.Replace("//Item description section", items);

            // удаляем слушателя для пульса
            int pos_destory = indexText.IndexOf("heart_rate.addEventListener");
            if (pos_destory > 0)
            {
                pos_destory = indexText.IndexOf("logger.log('index page.js on destroy invoke');");
                if (pos_destory > 0)
                {
                    indexText = indexText.Insert(pos_destory, "heart_rate.removeEventListener(heart.event.CURRENT, hrCurrListener);"
                                + Environment.NewLine + TabInString(8));
                }
            }

            // удаляем мировое время
            pos_destory = indexText.IndexOf("worldClock.init()");
            if (pos_destory > 0)
            {
                pos_destory = indexText.IndexOf("logger.log('index page.js on destroy invoke');");
                if (pos_destory > 0)
                {
                    indexText = indexText.Insert(pos_destory, "worldClock.uninit();"
                                + Environment.NewLine + TabInString(8));
                }
            }
            indexText = indexText.Replace("\r", "");

            File.WriteAllText(projectPath + @"\watchface\index.js", indexText, Encoding.UTF8);

            progressBar1.Visible = false;
        }

        #endregion

        #region Similator

        private void button_SimulatorConnect_Click(object sender, EventArgs e)
        {
            // Поиск окна симуляторапо заголовку
            IntPtr hWnd_Simulator = FindWindow(null, "Huami OS Simulator");
            if (hWnd_Simulator == IntPtr.Zero)
            {
                MessageBox.Show(Properties.FormStrings.Message_LaunchSimulator_Text, 
                    Properties.FormStrings.Message_Warning_Caption, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Поиск окна эмулятора по типу и заголовку
            hWnd_ModelEmulator = FindWindow("gdkWindowToplevel", "Zepp OS Simulator");
            if (hWnd_ModelEmulator == IntPtr.Zero) 
                hWnd_ModelEmulator = FindWindow("gdkWindowToplevel", "Zepp OS Simulator - Press Ctrl+Alt+G to release grab");
            if (hWnd_ModelEmulator == IntPtr.Zero) hWnd_ModelEmulator = FindWindow("gdkWindowToplevel", null);
            if (hWnd_ModelEmulator == IntPtr.Zero)
            {
                MessageBox.Show(Properties.FormStrings.Message_SimulatorRuning_Text1 + Environment.NewLine + 
                    Properties.FormStrings.Message_SimulatorRuning_Text2, 
                    Properties.FormStrings.Message_Warning_Caption, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WinApiHelper.ClientAreaInfo EmulatorArea = WinApiHelper.GetClientArea(hWnd_ModelEmulator);

            if (socket == null)
            {
                SimulatorInit();
                return;
            }
            else if (!socket.Connected)
            {
                MessageBox.Show(
                    Properties.FormStrings.Message_Simulator_StillConnecting_Text, 
                    Properties.FormStrings.Message_Warning_Caption, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private async void button_SimulatorDisconnect_Click(object sender, EventArgs e)
        {
            await SimulatorShutdownAsync();
        }

        private async void button_SimulatorSend_Click(object sender, EventArgs e)
        {
            if (ProjectDir == null || Watch_Face == null)
            {
                MessageBox.Show(
                    Properties.FormStrings.Message_OpenProject_Text, 
                    Properties.FormStrings.Message_Warning_Caption, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (socket == null || !socket.Connected)
            {
                MessageBox.Show(Properties.FormStrings.Message_Simulator_NotConnected_Text, 
                    Properties.FormStrings.Message_Warning_Caption, 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string zpkName = "";
            string SaveFileName = "TestInSimulator";
            //string tempDir = Application.StartupPath + @"\SimulatorTemp";
            string tempDir = Application.StartupPath + @"\Temp";
            zpkName = tempDir + @"\" + SaveFileName + ".zpk";
            string zipName = tempDir + @"\" + SaveFileName + ".zip";

            zipName = await CreateWatchFace(zipName, true);
            if (File.Exists(zipName)) zpkName = CreateZPK(zipName);

            progressBar1.Visible = false;

            if (File.Exists(zpkName) && Watch_Face != null && Watch_Face.WatchFace_Info != null)
            {
                byte[] zpkBuffer = ReadZpkFile(zpkName);
                int appId = 1234567;
                if (Watch_Face.WatchFace_Info.WatchFaceId != 0 ) appId = (int)Watch_Face.WatchFace_Info.WatchFaceId;
                string projectName = Path.GetFileNameWithoutExtension(FileName);
                if (Watch_Face.WatchFace_Info.WatchFaceName != null && Watch_Face.WatchFace_Info.WatchFaceName != "")
                    projectName = Watch_Face.WatchFace_Info.WatchFaceName;
                List<int> devSources = new List<int> {};
                foreach (int id in SelectedModel.deviceSource_ids)
                {
                    devSources.Add(id);
                }

                // Send the data to the simulator using the Upload method
                await Upload(zpkBuffer, projectName, "watch", appId, devSources);
                //MessageBox.Show("Push Sent!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (WindowHelper.WindowExists(hWnd_ModelEmulator))
                {
                    WindowHelper.BringToFront(hWnd_ModelEmulator);
                }

                if (Directory.Exists(tempDir)) DeleteDirectory(tempDir);
            }
        }

        private void checkBox_SimulatorCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (button_CaptureStop.Enabled) button_CaptureStop_Click(null, null);
            CheckBox checkBox = sender as CheckBox;

            panel_SimulatorCapture.Visible = checkBox.Checked;
            groupBox_ZeppPlayer.Visible = !checkBox.Checked;

            if (checkBox.Checked) label_GitHub_Instructions.Visible = false;
        }

        private void button_UpdateScreenPos_Click(object sender, EventArgs e)
        {
            updateWindowsPos();
        }

        private void updateWindowsPos()
        {
            if (checkBox_SimulatorCapture.Checked) updateSimulatorPos();
            else if (checkBox_ZeppPlayerCapture.Checked) UpdateZeppPlayerPos();
        }

        private void updateSimulatorPos()
        {
            //if (socket == null || !socket.Connected) return;
            // Поиск окна симуляторапо заголовку
            IntPtr hWnd_Simulator = FindWindow(null, "Huami OS Simulator");
            if (hWnd_Simulator == IntPtr.Zero)
            {
                MessageBox.Show(Properties.FormStrings.Message_LaunchSimulator_Text,
                    Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Поиск окна эмулятора по типу и заголовку
            hWnd_ModelEmulator = FindWindow("gdkWindowToplevel", "Zepp OS Simulator");
            if (hWnd_ModelEmulator == IntPtr.Zero)
                hWnd_ModelEmulator = FindWindow("gdkWindowToplevel", "Zepp OS Simulator - Press Ctrl+Alt+G to release grab");
            if (hWnd_ModelEmulator == IntPtr.Zero) hWnd_ModelEmulator = FindWindow("gdkWindowToplevel", null);
            if (hWnd_ModelEmulator == IntPtr.Zero)
            {
                MessageBox.Show(Properties.FormStrings.Message_SimulatorRuning_Text1 + Environment.NewLine +
                    Properties.FormStrings.Message_SimulatorRuning_Text2,
                    Properties.FormStrings.Message_Warning_Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (hWnd_ModelEmulator != IntPtr.Zero)
            {
                WinApiHelper.ClientAreaInfo EmulatorArea = WinApiHelper.GetClientArea(hWnd_ModelEmulator);
                EmulatorAreaPosX = EmulatorArea.WindowRect.Left + EmulatorArea.ClientOffset.X;
                EmulatorAreaPosY = EmulatorArea.WindowRect.Top + EmulatorArea.ClientOffset.Y;
                numericUpDown_CapturePosX.Value = EmulatorAreaPosX;
                numericUpDown_CapturePosY.Value = EmulatorAreaPosY;

                if (numericUpDown_CaptureHeight.Value <= 100 || 
                    (numericUpDown_CaptureOffsetX.Value == 0 && numericUpDown_CaptureOffsetY.Value == 0))
                {
                    numericUpDown_CaptureHeight.Value = SelectedModel.background.h;
                    EmulatorAreaOffsetY = EmulatorArea.Height - SelectedModel.background.h;
                    EmulatorAreaOffsetX = (EmulatorArea.Width - SelectedModel.background.w) / 2;
                    numericUpDown_CaptureOffsetX.Value = EmulatorAreaOffsetX;
                    numericUpDown_CaptureOffsetY.Value = EmulatorAreaOffsetY;
                }
                captureArea.X = EmulatorAreaPosX + EmulatorAreaOffsetX;
                captureArea.Y = EmulatorAreaPosY + EmulatorAreaOffsetY;
                captureArea.Width = EmulatorAreaWidth;
                captureArea.Height = EmulatorAreaHeight;
            }
        }

        private void numericUpDown_CapturePosX_ValueChanged(object sender, EventArgs e)
        {
            EmulatorAreaPosX = (int)numericUpDown_CapturePosX.Value;
            captureArea.X = EmulatorAreaPosX + EmulatorAreaOffsetX;
        }

        private void numericUpDown_CapturePosY_ValueChanged(object sender, EventArgs e)
        {
            EmulatorAreaPosY = (int)numericUpDown_CapturePosY.Value;
            captureArea.Y = EmulatorAreaPosY + EmulatorAreaOffsetY;
        }

        private void numericUpDown_CaptureOffsetX_ValueChanged(object sender, EventArgs e)
        {
            EmulatorAreaOffsetX = (int)numericUpDown_CaptureOffsetX.Value;
            captureArea.X = EmulatorAreaPosX + EmulatorAreaOffsetX;
        }

        private void numericUpDown_CaptureOffsetY_ValueChanged(object sender, EventArgs e)
        {
            EmulatorAreaOffsetY = (int)numericUpDown_CaptureOffsetY.Value;
            captureArea.Y = EmulatorAreaPosY + EmulatorAreaOffsetY;
        }

        private void numericUpDown_CaptureHeight_ValueChanged(object sender, EventArgs e)
        {
            EmulatorAreaWidth = (int)Math.Round(numericUpDown_CaptureHeight.Value / SelectedModel.background.h * SelectedModel.background.w);
            EmulatorAreaHeight = (int)numericUpDown_CaptureHeight.Value;
            label_CaptureWidth.Text = EmulatorAreaWidth.ToString();

            captureArea.Width = EmulatorAreaWidth;
            captureArea.Height = EmulatorAreaHeight;
        }

        private void resetCaptureArea()
        {
            numericUpDown_CaptureHeight.Value = 99;
            updateWindowsPos();
        }

        private void button_CaptureStart_Click(object sender, EventArgs e)
        {
            if (radioButtonDxgi.Checked)
            {
                var dxgi = new DxgiCapture();

                if (!dxgi.IsSupported())
                {
                    MessageBox.Show(Properties.FormStrings.Message_DXGI_error_Text);
                    capture = new BitBltCapture();
                    radioButtonBitBlt.Checked = true;
                }
                else
                {
                    capture = dxgi;
                }
            }
            else
            {
                capture = new BitBltCapture();
            }

            if (WindowHelper.WindowExists(hWnd_ModelEmulator))
            {
                WindowHelper.BringToFront(hWnd_ModelEmulator);
            }

            SwitchCaptureElements(false);
            if (progressBar_Capture.Visible) button_CaptureSaveGif.Enabled = false;

            StartCapture();
        }

        private void SwitchCaptureElements(bool enabled)
        {
            checkBox_SimulatorCapture.Enabled = enabled;
            button_UpdateScreenPos.Enabled = enabled;

            numericUpDown_CapturePosX.Enabled = enabled;
            numericUpDown_CapturePosY.Enabled = enabled;
            numericUpDown_CaptureHeight.Enabled = enabled;
            numericUpDown_CaptureOffsetX.Enabled = enabled;
            numericUpDown_CaptureOffsetY.Enabled = enabled;
            button_CaptureStart.Enabled = enabled;
            button_CaptureStop.Enabled = !enabled;
            radioButtonDxgi.Enabled = enabled;
            radioButtonBitBlt.Enabled = enabled;
            button_CaptureSaveGif.Enabled = !enabled;
        }

        private void button_CaptureStop_Click(object sender, EventArgs e)
        {
            Console.WriteLine("button_CaptureStop");
            cts?.Cancel();
            Console.WriteLine("cts?.Cancel();");
            capture?.Stop();
            Console.WriteLine("capture?.Stop();");
            SwitchCaptureElements(true);
        }

        private void button_CaptureSaveGif_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "GIF Files: (*.gif)|*.gif";
            saveFileDialog.FileName = "Preview.gif";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = Properties.FormStrings.Dialog_Title_SaveGIF;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                button_CaptureSaveGif.Enabled = false;
                if (WindowHelper.WindowExists(hWnd_ModelEmulator))
                {
                    WindowHelper.BringToFront(hWnd_ModelEmulator);
                }

                numericUpDown_CaptureFrameCount.Enabled = false;

                progressBar_Capture.Value = 0;
                progressBar_Capture.Maximum = (int)numericUpDown_CaptureFrameCount.Value;
                progressBar_Capture.Visible = true;
                int captureFrameCount = (int)numericUpDown_CaptureFrameCount.Value;
                button_CaptureBreakGif.Enabled = true;
                button_CaptureBreakGif.Visible = true;

                button_CapturePauseGif.Visible = true;
                button_CaptureResumeGif.Visible = true;

                Task.Run(async () =>
                {
                    int startDelay = (int)(Math.Max(100, 1000 / numericUpDown_CaptureFps.Value));
                    startDelay += 50;
                    await Task.Delay(startDelay); // 👈 небольшая пауза (UI успевает обновиться)
                    try
                    {
                        int delay = (int)(1000 / numericUpDown_CaptureFps.Value);
                        int frameCount = 0;
                        using (Bitmap mask = new Bitmap(Application.StartupPath + @"\Mask\" + SelectedModel.maskImage))
                        {
                            using (MagickImageCollection collection = new MagickImageCollection())
                            {
                                while (frameCount < numericUpDown_CaptureFrameCount.Value)
                                {
                                    if (button_CapturePauseGif.Enabled) // пауза
                                    {
                                        Bitmap bitmap = null;

                                        lock (frameLock)
                                        {
                                            bitmap = (Bitmap)currentFrame.Clone();
                                        }

                                        //if (checkBox_WatchSkin_Use.Checked) bitmap = ApplyWatchSkin(bitmap);
                                        //else if (checkBox_crop.Checked) bitmap = ApplyMask(bitmap, mask);
                                        Bitmap processed = bitmap;

                                        if (checkBox_WatchSkin_Use.Checked) processed = ApplyWatchSkin(bitmap);
                                        else if (checkBox_crop.Checked) processed = ApplyMask(bitmap, mask);

                                        if (!ReferenceEquals(processed, bitmap)) bitmap.Dispose();
                                        bitmap = processed;

                                        // Add first image and set the animation delay to 100ms
                                        //MagickImage item = new MagickImage(ImgConvert.CopyImageToByteArray(bitmap));
                                        //collection.Add(item);
                                        using (var item = new MagickImage(ImgConvert.CopyImageToByteArray(bitmap)))
                                        {
                                            collection.Add(item.Clone());
                                        }
                                        collection[collection.Count - 1].AnimationDelay = (int)(100 / numericUpDown_CaptureFps.Value);
                                        bitmap.Dispose();
                                        //item.Dispose();

                                        frameCount++;
                                            Invoke(new Action(() =>
                                            {
                                                progressBar_Capture.Value = frameCount;
                                            }));
                                    }
                                    await Task.Delay(delay);
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
                            } // using
                        } // using mask

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    Invoke(new Action(() =>
                    {
                        progressBar_Capture.Visible = false;
                        button_CaptureBreakGif.Visible = false;

                        button_CapturePauseGif.Visible = false;
                        button_CaptureResumeGif.Visible = false;

                        numericUpDown_CaptureFrameCount.Enabled = true;
                        numericUpDown_CaptureFrameCount.Value = captureFrameCount;

                        button_CaptureSaveGif.Enabled = button_CaptureStop.Enabled;
                    }));
                });
                //progressBar_Capture.Visible = false;

            }
        }

        private void button_CaptureBreakGif_Click(object sender, EventArgs e)
        {
            numericUpDown_CaptureFrameCount.Value = numericUpDown_CaptureFrameCount.Minimum;
            button_CaptureBreakGif.Enabled = false;

            button_CapturePauseGif.Enabled = true;
            button_CaptureResumeGif.Enabled = false;

        }

        private void button_CapturePauseGif_Click(object sender, EventArgs e)
        {
            button_CapturePauseGif.Enabled = false;
            button_CaptureResumeGif.Enabled = true;
        }

        private void button_CaptureResumeGif_Click(object sender, EventArgs e)
        {
            button_CapturePauseGif.Enabled = true; 
            button_CaptureResumeGif.Enabled = false;
        }

        private void StartCapture()
        {
            cts = new CancellationTokenSource();

            capture.Start(captureArea);

            Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    var frame = capture.GetFrame();
                    if (frame != null)
                    {
                        Bitmap uiCopy = null;

                        lock (frameLock)
                        {
                            currentFrame?.Dispose();
                            currentFrame = (Bitmap)frame.Clone();
                            if (checkBox_DrawCursor.Checked && currentFrame != null)
                            {
                                using (Graphics g = Graphics.FromImage(currentFrame))
                                {
                                    CursorHelper.DrawCursor(g, captureArea, checkBox_DrawCursor.CheckState);
                                }
                            }
                            uiCopy = (Bitmap)currentFrame.Clone();
                        }

                        BeginInvoke(new Action(() =>
                        {
                            var old = pictureBox_Capture.Image;
                            pictureBox_Capture.Image = uiCopy;
                            old?.Dispose();
                        }));
                    }

                    int delay = (int)(1000 / numericUpDown_CaptureFps.Value);
                    await Task.Delay(delay);
                }
            }, cts.Token);
        }


        public void SimulatorInit()
        {
            string url = "http://localhost:7650";
            if (socket != null) return;

            // Setup SocketIO options based on the simulator's requirements
            SocketIOOptions options = new SocketIOOptions
            {
                EIO = EngineIO.V4,
                Transport = TransportProtocol.WebSocket,
            };
            socket = new SocketIO(new Uri(url), options);

            // Socket on message listener
            socket.On("message", ctx =>
            {
                try
                {
                    // 1. Read json strings
                    string jsonPayload = ctx.GetValue<string>(0);
                    Console.WriteLine($"[DEBUG] Simulator payload: {jsonPayload}");
                    // 2. Parse JSON
                    var response = System.Text.Json.JsonSerializer.Deserialize<SimulatorResponse>(jsonPayload);
                    // 3. Process the result
                    if (response?.result?.success == true)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(Properties.FormStrings.Message_Simulator_PushSuccess_Text, 
                                Properties.FormStrings.Message_Infirmation_Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                    }
                    else
                    {
                        throw new Exception("Receives error message from the simulator");
                    }
                }
                catch (Exception ex)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        MessageBox.Show($"Error on processing message:\n{ex.Message}", Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
                return Task.CompletedTask;
            });

            // Socket on connect listener
            socket.OnConnected += (sender, e) =>
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    MessageBox.Show(Properties.FormStrings.Message_ConnectedSimulator_Text, 
                        Properties.FormStrings.Message_Infirmation_Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button_SimulatorSend.Enabled = true;
                    checkBox_SimulatorCapture.Enabled = true;
                    button_SimulatorConnect.Enabled = false;
                    button_SimulatorDisconnect.Enabled = true;
                    updateSimulatorPos();
                });
            };

            socket.OnDisconnected += (sender, e) =>
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    MessageBox.Show(Properties.FormStrings.Message_Simulator_Disconnected_Text,
                        Properties.FormStrings.Message_Warning_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    button_SimulatorSend.Enabled = false;
                    checkBox_SimulatorCapture.Checked = false;
                    checkBox_SimulatorCapture.Enabled = false;
                    button_SimulatorConnect.Enabled = true;
                    button_SimulatorDisconnect.Enabled = false;
                });
            };

            // Connect to the simulator asynchronously
            Task.Run(async () => {
                try
                {
                    await socket.ConnectAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connect Error: {ex.Message}");
                }
            });
        }

        public async Task SimulatorShutdownAsync()
        {
            if (socket == null)
                return;

            try
            {
                // 1. Отключаемся от сервера
                await socket.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnect error: {ex.Message}");
            }
            finally
            {
                try
                {
                    // 2. Освобождаем ресурсы
                    socket.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Dispose error: {ex.Message}");
                }

                socket = null;
            }
        }

        /// <summary>
        /// Read the ZPK file as a byte array to be sent to the simulator.
        /// </summary>
        public byte[] ReadZpkFile(string zpkPath)
        {
            string fullPath = Path.GetFullPath(zpkPath);
            if (!File.Exists(fullPath)) throw new FileNotFoundException($"ZPK file not found: {fullPath}");

            return File.ReadAllBytes(fullPath);
        }

        public async Task Upload(byte[] data, string projectName, string target, int appid, List<int> devices)
        {
            var dataArr = Array.ConvertAll(data, b => (int)b);

            // 1. Make a payload object that matches the simulator's expected structure
            var payload = new
            {
                jsonrpc = "2.0",
                method = previewMethod,
                @params = new
                {
                    target = target,
                    projectName = projectName,
                    appid = appid,
                    size = data.Length,
                    data = dataArr,
                    devices = devices
                },
                id = previewId
            };


            // 2. Serialize the payload to a JSON string using System.Text.Json
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null // Keep naming policy as the node.js version
            };
            string jsonMessage = System.Text.Json.JsonSerializer.Serialize(payload, options);

            Console.WriteLine($"[DEBUG] Send 'message' event!");

            // 4. Send the JSON string to the simulator use same method like the Node via Socket.IO
            await socket.EmitAsync("message", new[] { jsonMessage });
        }

        #endregion
    }

    # region Файлообменники
    public class FilePostAccountInfo
    {
        public string email { get; set; }

        public string plan { get; set; }

        public FilePostUsage usage { get; set; }
    }

    public class FilePostUsage
    {
        public int uploads_this_month { get; set; }

        public int uploads_limit { get; set; }

        public int max_file_size_mb { get; set; }
    }

    public class GitHubRepoChecker
    {
        /// <summary>
        /// Проверяет валидность токена и его соответствие указанному логину владельца.
        /// </summary>
        public static async Task<bool> ValidateCredentialsAsync(string token, string expectedOwner)
        {
            var client = new GitHubClient(new Octokit.ProductHeaderValue("GitHubAuthCheckerApp"));
            client.Credentials = new Credentials(token);

            try
            {
                // Запрашиваем данные текущего пользователя по токену
                var user = await client.User.Current();

                // Проверяем, совпадает ли владелец токена с ожидаемым логином (без учета регистра)
                if (!string.Equals(user.Login, expectedOwner, StringComparison.OrdinalIgnoreCase))
                {
                    //throw new ArgumentException($"Токен принадлежит пользователю '{user.Login}', но указан владелец '{expectedOwner}'.");
                    throw new ArgumentException(Properties.FormStrings.GitHub_TokenOwnerMismatch1 +" " + user.Login + ", " + Properties.FormStrings.GitHub_TokenOwnerMismatch2 + " " + expectedOwner);
                }

                Console.WriteLine($"Авторизация успешна! Токен принадлежит пользователю: {user.Login}");
                return true;
            }
            catch (AuthorizationException)
            {
                // Срабатывает, если токен невалидный, просрочен или отозван
                //throw new UnauthorizedAccessException("Переданный GitHub токен недействителен (ошибка 401 Unauthorized).");
                throw new UnauthorizedAccessException(Properties.FormStrings.GitHub_InvalidToken);
            }
            catch (Exception ex) when (!(ex is ArgumentException || ex is UnauthorizedAccessException))
            {
                // Прочие ошибки (например, проблемы с сетью)
                //throw new Exception($"Ошибка проверки авторизации: {ex.Message}", ex);
                throw new Exception(Properties.FormStrings.GitHub_AuthError + ": " + ex.Message + ", " + ex);
            }
        }
        
        /// <summary>
        /// Проверяет, существует ли указанный репозиторий и есть ли к нему доступ.
        /// </summary>
        public static async Task<bool> CheckRepositoryExistsAsync(string token, string owner, string repoName)
        {
            var client = new GitHubClient(new Octokit.ProductHeaderValue("GitHubRepoCheckerApp"));
            client.Credentials = new Credentials(token);

            try
            {
                // Пытаемся получить данные о репозитории
                var repository = await client.Repository.Get(owner, repoName);

                // Если код прошел сюда, значит репозиторий найден
                Console.WriteLine($"Репозиторий найден! Полное имя: {repository.FullName}");
                return true;
            }
            catch (NotFoundException)
            {
                // GitHub возвращает 404, если репозиторий приватный (и токен его не видит) или если его вообще нет
                //throw new Exception($"Репозиторий '{repoName}' у пользователя '{owner}' не найден или к нему нет доступа.");
                throw new Exception(Properties.FormStrings.GitHub_RepositoryNotFound1 + " " + repoName + " " + Properties.FormStrings.GitHub_RepositoryNotFound2);
            }
            catch (AuthorizationException)
            {
                throw new UnauthorizedAccessException(Properties.FormStrings.GitHub_InvalidToken);
            }
            catch (Exception ex) when (!(ex is NotFoundException || ex is UnauthorizedAccessException))
            {
                //throw new Exception($"Ошибка при проверке репозитория: {ex.Message}", ex);
                throw new Exception(Properties.FormStrings.GitHub_RepositoryCheckError + ": " + ex.Message + ". " + ex);
            }
        }
    }

    public class GitHubArchiveUploader
    {
        public static async Task<string> UploadArchiveToGitHubAsync(
            string token,
            string owner,
            string repoName,
            string branch,
            string targetFilePath,
            string localFilePath,
            string commitMessage,
            bool confirm_file_replacement,
            IProgress<UploadProgress> progress = null) // Добавили параметр прогресса
        {
            // Шаг 1: Чтение файла с диска
            progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_ReadFile/*, Percentage = 10*/ });
            byte[] fileBytes = File.ReadAllBytes(localFilePath);

            // Шаг 2: Кодирование в Base64 (для больших архивов это занимает время)
            progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_EncodeBase64/*, Percentage = 30*/ });
            string base64Content = Convert.ToBase64String(fileBytes);

            // Шаг 3: Авторизация клиента
            progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_ConnectingToAPI/*, Percentage = 50*/ });
            var client = new GitHubClient(new Octokit.ProductHeaderValue("GitHubArchiveUploaderApp"));
            client.Credentials = new Credentials(token);

            string currentFileSha = null;

            // Шаг 4: Проверка наличия старого файла на сервере
            progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_CheckExistingFile/*, Percentage = 60*/ });
            try
            {
                var existingFiles = await client.Repository.Content.GetAllContentsByRef(owner, repoName, targetFilePath, branch);
                if (existingFiles != null && existingFiles.Count > 0)
                {
                    currentFileSha = existingFiles[0].Sha;
                }
            }
            catch (NotFoundException)
            {
                // Файла нет, это нормально
            }

            // Шаг 5: Передача по сети (Самый долгий этап. Ставим 80% и ждем ответа сервера)
            if (currentFileSha == null)
            {
                progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_UploadFile/*, Percentage = 80*/ });
                //var createFileRequest = new CreateFileRequest(commitMessage, base64Content, branch, convertContentToBase64: true);
                var createFileRequest = new CreateFileRequest(commitMessage + Path.GetFileName(targetFilePath), base64Content, branch, convertContentToBase64: false);
                await client.Repository.Content.CreateFile(owner, repoName, targetFilePath, createFileRequest);
            }
            else
            {
                //progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_UpdateFile/*, Percentage = 80*/ });
                //var updateFileRequest = new UpdateFileRequest(commitMessage, base64Content, currentFileSha, branch, convertContentToBase64: false);
                //await client.Repository.Content.UpdateFile(owner, repoName, targetFilePath, updateFileRequest);

                if (!confirm_file_replacement)
                {
                    progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_UpdateFile/*, Percentage = 80*/ });
                    var updateFileRequest = new UpdateFileRequest(commitMessage + Path.GetFileName(targetFilePath), base64Content, currentFileSha, branch, convertContentToBase64: false);
                    await client.Repository.Content.UpdateFile(owner, repoName, targetFilePath, updateFileRequest);
                }
                else
                {
                    Logger.WriteLine("File.Exists");
                    FormFileExists f = new FormFileExists();
                    f.ShowDialog();
                    int dialogResult = f.Data;

                    switch (dialogResult)
                    {
                        case 0: // Отмена
                            throw new Exception(Properties.FormStrings.GitHub_UploadCanceled);
                        //break;
                        case 1: // заменить
                            progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_UpdateFile/*, Percentage = 80*/ });
                            var updateFileRequest = new UpdateFileRequest(commitMessage + Path.GetFileName(targetFilePath), base64Content, currentFileSha, branch, convertContentToBase64: false);
                            await client.Repository.Content.UpdateFile(owner, repoName, targetFilePath, updateFileRequest);

                            break;
                        case 2: // сохранить оба
                            Logger.WriteLine("newFileName.Copy");
                            int count = 1;
                            //string path = Path.GetDirectoryName(targetFilePath);
                            string path = "";
                            if (targetFilePath.Contains('/')) path = targetFilePath.Substring(0, targetFilePath.LastIndexOf('/'));
                            string fileName = Path.GetFileNameWithoutExtension(targetFilePath);
                            string extension = Path.GetExtension(targetFilePath);

                            while (currentFileSha != null)
                            {
                                currentFileSha = null;
                                targetFilePath = $"{path}/{fileName}({count++}){extension}".TrimStart('/'); // На случай, если path пустой.
                                //targetFilePath = Path.Combine(path, newFileName);
                                try
                                {
                                    var existingFiles = await client.Repository.Content.GetAllContentsByRef(owner, repoName, targetFilePath, branch);
                                    if (existingFiles != null && existingFiles.Count > 0)
                                    {
                                        currentFileSha = existingFiles[0].Sha;
                                    }
                                }
                                catch (NotFoundException)
                                {
                                    // Файла нет, это нормально
                                }
                            }
                            progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_UploadFile/*, Percentage = 80*/ });
                            var createFileRequest = new CreateFileRequest(commitMessage + Path.GetFileName(targetFilePath), base64Content, branch, convertContentToBase64: false);
                            await client.Repository.Content.CreateFile(owner, repoName, targetFilePath, createFileRequest);

                            break;
                    }
                }
            }

            // Готово!
            progress?.Report(new UploadProgress { Stage = Properties.FormStrings.GitHub_Stage_UploadFileCompleted/*, Percentage = 100*/ });

            //string downloadUrl = $"https://{owner}.github.io/{repoName}/{targetFilePath}";
            string downloadUrl = $"https://github.com/{owner}/{repoName}/tree/{branch}/{targetFilePath}";
            return downloadUrl;
        }
    }

    public static class SecretStorage
    {
        public static string Encrypt(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);

            byte[] encrypted = ProtectedData.Protect(
                data,
                null,
                DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string encryptedText)
        {
            try 
            {
                byte[] data = Convert.FromBase64String(encryptedText);

                byte[] decrypted = ProtectedData.Unprotect(
                    data,
                    null,
                    DataProtectionScope.CurrentUser);

                return Encoding.UTF8.GetString(decrypted);
            }
            catch
            {
                return "";
            }
        }
    }
    #endregion

    #region Zepplayer
    public static class UIAHelper
    {
        public static Rectangle? FindChromeElementRect(
            string windowName,
            string automationId)
        {
            try
            {
                // 1. Ищем окно Chrome
                var root = AutomationElement.RootElement;

                var windowCondition = new PropertyCondition(
                    AutomationElement.NameProperty,
                    windowName);

                var window = root.FindFirst(TreeScope.Children, windowCondition);

                if (window == null)
                    return null;

                // 2. Ищем элемент внутри окна по AutomationId
                var elementCondition = new PropertyCondition(
                    AutomationElement.AutomationIdProperty,
                    automationId);

                var element = window.FindFirst(TreeScope.Descendants, elementCondition);

                if (element == null)
                    return null;

                // 3. Берём координаты
                var rect = element.Current.BoundingRectangle;

                if (rect == System.Windows.Rect.Empty)
                    return null;

                // 4. Конвертация в System.Drawing.Rectangle
                return new Rectangle(
                    (int)rect.X,
                    (int)rect.Y,
                    (int)rect.Width,
                    (int)rect.Height);
            }
            catch
            {
                return null;
            }
        }
    }
    #endregion

    #region Simulator

    public static class WinApiHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        public struct ClientAreaInfo
        {
            public RECT WindowRect;     // всё окно (экранные координаты)
            public RECT ClientRect;     // клиентская область (экранные координаты)
            public POINT ClientOffset;  // смещение клиента внутри окна
            public int Width;
            public int Height;
        }

        public static ClientAreaInfo GetClientArea(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                throw new ArgumentException("Invalid handle");

            // 1. Окно (экранные координаты)
            GetWindowRect(hWnd, out RECT windowRect);

            // 2. Размер клиентской области (локальный)
            GetClientRect(hWnd, out RECT clientRectLocal);

            // 3. Конвертируем (0,0) клиента в экран
            POINT clientTopLeft = new POINT { X = 0, Y = 0 };
            ClientToScreen(hWnd, ref clientTopLeft);

            // 4. Конвертируем (W,H) клиента в экран
            POINT clientBottomRight = new POINT
            {
                X = clientRectLocal.Right,
                Y = clientRectLocal.Bottom
            };
            ClientToScreen(hWnd, ref clientBottomRight);

            // 5. Итоговый rect клиента в экранных координатах
            RECT clientRectScreen = new RECT
            {
                Left = clientTopLeft.X,
                Top = clientTopLeft.Y,
                Right = clientBottomRight.X,
                Bottom = clientBottomRight.Y
            };

            // 6. Смещение клиента внутри окна
            POINT offset = new POINT
            {
                X = clientTopLeft.X - windowRect.Left,
                Y = clientTopLeft.Y - windowRect.Top
            };

            return new ClientAreaInfo
            {
                WindowRect = windowRect,
                ClientRect = clientRectScreen,
                ClientOffset = offset,
                Width = clientRectScreen.Right - clientRectScreen.Left,
                Height = clientRectScreen.Bottom - clientRectScreen.Top
            };
        }
    }

    public class SimulatorResponse
    {
        public string jsonrpc { get; set; }
        public int id { get; set; }
        public SimulatorResult result { get; set; }
    }
    public class SimulatorResult
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class BitBltCapture : IScreenCapture
    {
        private Rectangle area;

        public bool IsSupported() => true;

        public void Start(Rectangle area)
        {
            this.area = area;
        }

        public void Stop() { }

        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdcDest, int x, int y, int w, int h,
            IntPtr hdcSrc, int x1, int y1, int rop);

        const int SRCCOPY = 0x00CC0020;

        public Bitmap GetFrame()
        {
            Bitmap bmp = new Bitmap(area.Width, area.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                IntPtr hdcDest = g.GetHdc();
                IntPtr hdcSrc = GetDC(IntPtr.Zero);

                BitBlt(hdcDest, 0, 0, area.Width, area.Height,
                    hdcSrc, area.Left, area.Top, SRCCOPY);

                ReleaseDC(IntPtr.Zero, hdcSrc);
                g.ReleaseHdc(hdcDest);
            }

            return bmp;
        }
    }

    //public class DxgiCapture : IScreenCapture
    //{
    //    private Device device;
    //    private OutputDuplication duplication;
    //    private bool initialized;
    //    private Rectangle captureArea;

    //    public bool IsSupported()
    //    {
    //        try
    //        {
    //            using (var d = new Device(SharpDX.Direct3D.DriverType.Hardware))
    //            {
    //                return true;
    //            }
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }

    //    public void Start(Rectangle area)
    //    {
    //        if (initialized) return;
    //        captureArea = area;
    //        captureArea = Rectangle.Intersect(captureArea, Screen.PrimaryScreen.Bounds); // ограничеваем размером экрана

    //        device = new Device(SharpDX.Direct3D.DriverType.Hardware);

    //        var dxgiDevice = device.QueryInterface<SharpDX.DXGI.Device>();
    //        var adapter = dxgiDevice.Adapter;
    //        var output = adapter.Outputs[0];
    //        var output1 = output.QueryInterface<Output1>();

    //        duplication = output1.DuplicateOutput(device);

    //        initialized = true;
    //    }

    //    public void Stop()
    //    {
    //        try
    //        {
    //            duplication?.ReleaseFrame();
    //        }
    //        catch
    //        {
    //        }

    //        duplication?.Dispose();
    //        duplication = null;

    //        device?.Dispose();
    //        device = null;

    //        initialized = false;
    //    }

    //    public Bitmap GetFrame()
    //    {
    //        SharpDX.DXGI.Resource screenResource = null;

    //        try
    //        {
    //            OutputDuplicateFrameInformation frameInfo;

    //            duplication.AcquireNextFrame(
    //                100,
    //                out frameInfo,
    //                out screenResource);

    //            using (var texture = screenResource.QueryInterface<Texture2D>())
    //            {
    //                var desc = texture.Description;

    //                // staging texture (CPU readable)
    //                var stagingDesc = new Texture2DDescription()
    //                {
    //                    CpuAccessFlags = CpuAccessFlags.Read,
    //                    BindFlags = BindFlags.None,
    //                    Format = desc.Format,
    //                    Width = desc.Width,
    //                    Height = desc.Height,
    //                    OptionFlags = ResourceOptionFlags.None,
    //                    MipLevels = 1,
    //                    ArraySize = 1,
    //                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
    //                    Usage = ResourceUsage.Staging
    //                };

    //                using (var stagingTex = new Texture2D(device, stagingDesc))
    //                {
    //                    // GPU -> CPU texture copy
    //                    device.ImmediateContext.CopyResource(texture, stagingTex);

    //                    // map CPU texture
    //                    var mapSource = device.ImmediateContext.MapSubresource(
    //                        stagingTex,
    //                        0,
    //                        SharpDX.Direct3D11.MapMode.Read,
    //                        SharpDX.Direct3D11.MapFlags.None);
    //                    Bitmap bmp = new Bitmap(
    //                        captureArea.Width,
    //                        captureArea.Height,
    //                        PixelFormat.Format32bppArgb);
    //                    Rectangle boundsRect = new Rectangle(
    //                        0,
    //                        0,
    //                        captureArea.Width,
    //                        captureArea.Height);

    //                    BitmapData mapDest = bmp.LockBits(
    //                        boundsRect,
    //                        ImageLockMode.WriteOnly,
    //                        bmp.PixelFormat);

    //                    IntPtr sourcePtr = mapSource.DataPointer;
    //                    IntPtr destPtr = mapDest.Scan0;

    //                    int sourcePitch = mapSource.RowPitch;
    //                    int destPitch = mapDest.Stride;

    //                    for (int y = 0; y < captureArea.Height; y++)
    //                    {
    //                        IntPtr src = sourcePtr
    //                            + (y + captureArea.Top) * sourcePitch
    //                            + captureArea.Left * 4;

    //                        IntPtr dst = destPtr
    //                            + y * destPitch;

    //                        Utilities.CopyMemory(
    //                            dst,
    //                            src,
    //                            captureArea.Width * 4);
    //                    }

    //                    bmp.UnlockBits(mapDest);

    //                    device.ImmediateContext.UnmapSubresource(stagingTex, 0);

    //                    return bmp;
    //                }
    //            }
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //        finally
    //        {
    //            screenResource?.Dispose();

    //            try
    //            {
    //                duplication.ReleaseFrame();
    //            }
    //            catch
    //            {
    //            }
    //        }
    //    }

    //}

    public class DxgiCapture : IScreenCapture
    {
        private Device device;
        private OutputDuplication duplication;

        private Texture2D stagingTexture;

        private Rectangle captureArea;
        private Rectangle monitorBounds;

        private bool initialized;

        // =========================================================
        // Проверка поддержки DXGI
        // =========================================================

        public bool IsSupported()
        {
            try
            {
                using (var d = new Device(
                    SharpDX.Direct3D.DriverType.Hardware))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // =========================================================
        // START
        // =========================================================

        public void Start(Rectangle area)
        {
            if (initialized) return;

            captureArea = Rectangle.Intersect(
                area,
                SystemInformation.VirtualScreen);

            if (captureArea.Width <= 0 || captureArea.Height <= 0)
                throw new Exception("Invalid capture area");

            Console.WriteLine("VirtualScreen = " + SystemInformation.VirtualScreen);
            Console.WriteLine("CaptureArea = " + captureArea);

            var factory = new Factory1();

            Output1 foundOutput = null;
            Adapter1 foundAdapter = null;

            // ============================
            // FIND MONITOR
            // ============================

            foreach (var adapter in factory.Adapters1)
            {
                device?.Dispose();
                device = new Device(adapter); // важно: device от adapter

                foreach (var output in adapter.Outputs)
                {
                    var desc = output.Description;

                    var rect = new Rectangle(
                        desc.DesktopBounds.Left,
                        desc.DesktopBounds.Top,
                        desc.DesktopBounds.Right - desc.DesktopBounds.Left,
                        desc.DesktopBounds.Bottom - desc.DesktopBounds.Top);

                    Console.WriteLine($"MONITOR: {rect}");

                    if (rect.IntersectsWith(captureArea))
                    {
                        monitorBounds = rect;
                        foundOutput = output.QueryInterface<Output1>();
                        foundAdapter = adapter;
                        break;
                    }
                }

                if (foundOutput != null)
                    break;
            }

            if (foundOutput == null) throw new Exception("Monitor not found");

            // ============================
            // DUPLICATION
            // ============================

            duplication = foundOutput.DuplicateOutput(device);

            // ============================
            // STAGING TEXTURE
            // ============================

            stagingTexture?.Dispose();

            stagingTexture = new Texture2D(
                device,
                new Texture2DDescription()
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = monitorBounds.Width,
                    Height = monitorBounds.Height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Staging
                });

            initialized = true;
        }

        // =========================================================
        // STOP
        // =========================================================

        public void Stop()
        {
            try
            {
                duplication?.ReleaseFrame();
            }
            catch
            {
            }

            stagingTexture?.Dispose();
            stagingTexture = null;

            duplication?.Dispose();
            duplication = null;

            device?.Dispose();
            device = null;

            initialized = false;
        }

        // =========================================================
        // GET FRAME
        // =========================================================

        public Bitmap GetFrame()
        {
            if (!initialized || duplication == null)
            {
                return null;
            }

            DxgiResource screenResource = null;

            bool frameAcquired = false;

            try
            {
                OutputDuplicateFrameInformation frameInfo;

                duplication.AcquireNextFrame(
                    100,
                    out frameInfo,
                    out screenResource);

                frameAcquired = true;

                using (var screenTexture =
                    screenResource.QueryInterface<Texture2D>())
                {
                    // =============================================
                    // GPU -> CPU
                    // =============================================

                    device.ImmediateContext.CopyResource(
                        screenTexture,
                        stagingTexture);

                    // =============================================
                    // MAP
                    // =============================================

                    var mapSource =
                        device.ImmediateContext.MapSubresource(
                            stagingTexture,
                            0,
                            SharpDX.Direct3D11.MapMode.Read,
                            SharpDX.Direct3D11.MapFlags.None);
                    int localX = captureArea.X - monitorBounds.X;
                    int localY = captureArea.Y - monitorBounds.Y;

                    try
                    {
                        // =========================================
                        // LOCAL COORDS INSIDE MONITOR
                        // =========================================

                        int localLeft = captureArea.Left - monitorBounds.Left;

                        int localTop = captureArea.Top - monitorBounds.Top;

                        // safety
                        if (localLeft < 0 || localTop < 0)
                        {
                            return null;
                        }

                        Bitmap bmp = new Bitmap(
                            captureArea.Width,
                            captureArea.Height,
                            PixelFormat.Format32bppArgb);

                        Rectangle rect = new Rectangle(
                            0,
                            0,
                            bmp.Width,
                            bmp.Height);

                        BitmapData bmpData =
                            bmp.LockBits(
                                rect,
                                ImageLockMode.WriteOnly,
                                bmp.PixelFormat);

                        try
                        {
                            IntPtr sourcePtr = mapSource.DataPointer;

                            IntPtr destPtr = bmpData.Scan0;

                            int sourcePitch = mapSource.RowPitch;

                            int destPitch = bmpData.Stride;

                            // =====================================
                            // COPY CROPPED AREA
                            // =====================================

                            for (int y = 0; y < captureArea.Height; y++)
                            {
                                IntPtr src = sourcePtr + (y + localY) * sourcePitch + localX * 4;

                                IntPtr dst = destPtr + y * destPitch;

                                Utilities.CopyMemory(dst, src, captureArea.Width * 4);
                            }
                        }
                        finally
                        {
                            bmp.UnlockBits(bmpData);
                        }

                        return bmp;
                    }
                    finally
                    {
                        device.ImmediateContext.UnmapSubresource(stagingTexture, 0);
                    }
                }
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine("DXGI ERROR: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return null;
            }
            finally
            {
                screenResource?.Dispose();
                if (frameAcquired)
                {
                    try
                    {
                        duplication.ReleaseFrame();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    public static class WindowHelper
    {
        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        private const int SW_RESTORE = 9;
        private const int SW_SHOW = 5;

        public static bool BringToFront(IntPtr hWnd)
        {
            // Проверяем что окно существует
            if (hWnd == IntPtr.Zero || !IsWindow(hWnd))
                return false;

            // Если окно свернуто — восстанавливаем
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
            }
            else
            {
                ShowWindow(hWnd, SW_SHOW);
            }

            // Поднимаем окно
            BringWindowToTop(hWnd);

            // Делаем активным
            return SetForegroundWindow(hWnd);
        }

        public static bool WindowExists(IntPtr hWnd)
        {
            return hWnd != IntPtr.Zero && IsWindow(hWnd);
        }
    }

    public static class CursorHelper
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private const int VK_LBUTTON = 0x01;
        private const int VK_RBUTTON = 0x02;
        private const int VK_MBUTTON = 0x04;

        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public POINT ptScreenPos;
        }

        const int CURSOR_SHOWING = 0x00000001;

        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        static extern bool DrawIcon(
            IntPtr hDC,
            int X,
            int Y,
            IntPtr hIcon);

        public static void DrawCursor(Graphics g, Rectangle captureArea, CheckState checkState)
        {
            bool leftPressed = (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0;

            bool rightPressed = (GetAsyncKeyState(VK_RBUTTON) & 0x8000) != 0;

            bool middlePressed = (GetAsyncKeyState(VK_MBUTTON) & 0x8000) != 0;

            CURSORINFO ci = new CURSORINFO();
            ci.cbSize = Marshal.SizeOf(ci);

            if (!GetCursorInfo(out ci)) return;

            if (ci.flags != CURSOR_SHOWING) return;

            // =========================================
            // Cursor position inside capture area
            // =========================================

            // Координаты курсора относительно области захвата
            int x = ci.ptScreenPos.X - captureArea.Left;
            int y = ci.ptScreenPos.Y - captureArea.Top;

            // =========================================
            // Draw mouse click effects FIRST
            // =========================================
            if (checkState == CheckState.Indeterminate)
            {
                using (Bitmap src = new Bitmap(Application.StartupPath + @"\Mask\shortcut_pointer.png"))
                {
                    g.DrawImage(src, x - 15, y - 9);
                }
            }

            if (leftPressed)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(170, Color.Red)))
                {
                    g.FillEllipse(
                        b,
                        x - 15,
                        y - 15,
                        30,
                        30);
                }
            }

            if (rightPressed)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(170, Color.DeepSkyBlue)))
                {
                    g.FillEllipse(
                        b,
                        x - 15,
                        y - 15,
                        30,
                        30);
                }
            }

            if (middlePressed)
            {
                using (Brush b = new SolidBrush(Color.FromArgb(170, Color.Green)))
                {
                    g.FillEllipse(
                        b,
                        x - 15,
                        y - 15,
                        30,
                        30);
                }
            }

            // =========================================
            // Draw cursor LAST
            // =========================================

            if (checkState == CheckState.Checked)
            {
                IntPtr hdc = g.GetHdc();

                try
                {
                    DrawIcon(hdc, x, y, ci.hCursor);
                }
                finally
                {
                    g.ReleaseHdc(hdc);
                }
            }
        }
    }

    #endregion
}
