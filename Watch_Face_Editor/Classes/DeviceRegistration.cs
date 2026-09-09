using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Watch_Face_Editor
{
    internal static class DeviceRegistration
    {
        private const string FunctionUrl =
            "https://vstpwwvyuiuzmhhapbqz.supabase.co/functions/v1/register-device";

        private const string SupabasePublishableKey =
            "sb_publishable_8tQtFQHp_1-BhqwCC0H8PQ_62BRfzI7";

        private static readonly string AppFolder =
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "Watch_Face_Editor_(ZeppOS)");

        private static readonly string DeviceIdFile =
            Path.Combine(AppFolder, "device_id.txt");


        /// <summary>
        /// Возвращает постоянный UUID компьютера.
        /// При первом запуске UUID создаётся и сохраняется.
        /// </summary>
        public static string GetDeviceId()
        {
            try
            {
                if (File.Exists(DeviceIdFile))
                {
                    string deviceId = File.ReadAllText(DeviceIdFile).Trim();

                    Guid guid;

                    if (Guid.TryParse(deviceId, out guid))
                    {
                        return guid.ToString();
                    }
                }

                Directory.CreateDirectory(AppFolder);

                string newDeviceId = Guid.NewGuid().ToString();

                File.WriteAllText(DeviceIdFile, newDeviceId);

                return newDeviceId;
            }
            catch
            {
                // Если файл невозможно создать/прочитать,
                // используем UUID только для текущего запуска.
                return Guid.NewGuid().ToString();
            }
        }


        /// <summary>
        /// Регистрирует компьютер в Supabase.
        /// Ошибка сети не должна мешать работе приложения.
        /// </summary>
        public static async Task<bool> RegisterAsync(string _appLanguage)
        {
            Logger.WriteLine("* Sending device info (start)");

            try
            {
                string deviceId = GetDeviceId();

                var systemInfo = await Task.Run(() =>
                {
                    return new
                    {
                        AppVersion = SystemInfo.GetAppVersion(),
                        AppLanguage = _appLanguage,
                        WindowsVersion = SystemInfo.GetWindowsVersion(),
                        SystemLanguage = SystemInfo.GetSystemLanguage(),
                        NetFrameworkVersion = SystemInfo.GetNetFrameworkVersion(),
                        NetVersion = SystemInfo.GetDotNetVersion(),
                        AspNetCoreVersion = SystemInfo.GetAspNetCoreVersion(),
                        WindowsDesktopVersion = SystemInfo.GetWindowsDesktopVersion()
                    };
                });

                string appVersion =
                    LimitLength(
                        systemInfo.AppVersion,
                        50,
                        "app_version");

                string appLanguage =
                    LimitLength(
                        systemInfo.AppLanguage,
                        50,
                        "app_language");

                string windowsVersion =
                    LimitLength(
                        systemInfo.WindowsVersion,
                        200,
                        "windows_version");

                string systemLanguage =
                    LimitLength(
                        systemInfo.SystemLanguage,
                        20,
                        "system_language");

                string netFrameworkVersion =
                    LimitLength(
                        systemInfo.NetFrameworkVersion,
                        150,
                        "net_framework_version");

                string netVersion =
                    LimitLength(
                        systemInfo.NetVersion,
                        150,
                        "net_version");

                string aspNetCoreVersion =
                    LimitLength(
                        systemInfo.AspNetCoreVersion,
                        150,
                        "aspnet_core_version");

                string windowsDesktopVersion =
                    LimitLength(
                        systemInfo.WindowsDesktopVersion,
                        150,
                        "windows_desktop_version");


                string json =
                    "{"
                    + "\"device_id\":\"" + EscapeJson(deviceId) + "\","
                    + "\"app_version\":\"" + EscapeJson(appVersion) + "\","
                    + "\"app_language\":\"" + EscapeJson(appLanguage) + "\","
                    + "\"windows_version\":\"" + EscapeJson(windowsVersion) + "\","
                    + "\"system_language\":\"" + EscapeJson(systemLanguage) + "\","
                    + "\"net_framework_version\":\"" + EscapeJson(netFrameworkVersion) + "\","
                    + "\"net_version\":\"" + EscapeJson(netVersion) + "\","
                    + "\"aspnet_core_version\":\"" + EscapeJson(aspNetCoreVersion) + "\","
                    + "\"windows_desktop_version\":\"" + EscapeJson(windowsDesktopVersion) + "\""
                    + "}";


                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);

                    client.DefaultRequestHeaders.Add(
                        "apikey",
                        SupabasePublishableKey);

                    using (var content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json"))
                    {
                        HttpResponseMessage response =
                            await client.PostAsync(
                                FunctionUrl,
                                content);

                        string responseText =
                            await response.Content.ReadAsStringAsync();

                        Logger.WriteLine(
                            "* Sending device info (Supabase HTTP: " +
                            response.StatusCode +
                            ")");

                        Logger.WriteLine(
                            "* Sending device info (Supabase response: " +
                            responseText +
                            ")");

                        if (!response.IsSuccessStatusCode)
                        {
                            Logger.WriteLine(
                                "* Sending device info (Supabase HTTP error)");

                            return false;
                        }

                        try
                        {
                            var serializer =
                                new JavaScriptSerializer();

                            var result =
                                serializer.Deserialize<Dictionary<string, object>>(
                                    responseText);

                            object successValue;

                            if (result.TryGetValue(
                                "success",
                                out successValue))
                            {
                                bool success;

                                if (bool.TryParse(
                                    successValue.ToString(),
                                    out success))
                                {
                                    Logger.WriteLine(
                                        "* Sending device info (success: " +
                                        success +
                                        ")");

                                    return success;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(
                                "* Sending device info (Invalid Supabase response: " +
                                ex +
                                ")");
                        }

                        return false;
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                Logger.WriteLine(
                    "* Sending device info (timeout): " + ex);

                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(
                    "* Sending device info (Supabase registration error: " +
                    ex +
                    ")");

                return false;
            }
        }

        private static string LimitLength(string value, int maxLength, string fieldName)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            if (value.Length <= maxLength)
                return value;

            Logger.WriteLine(
                "* Sending device info: field '" +
                fieldName +
                "' was truncated from " +
                value.Length +
                " to " +
                maxLength +
                " characters");

            return value.Substring(0, maxLength);
        }

        /// <summary>
        /// Язык интерфейса Windows.
        /// </summary>
        private static string GetSystemLanguage()
        {
            return System.Globalization.CultureInfo
                .InstalledUICulture
                .Name;
        }


        /// <summary>
        /// Минимальное экранирование строк для JSON.
        /// </summary>
        private static string EscapeJson(string value)
        {
            if (value == null)
                return "";

            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"");
        }
    }
}
