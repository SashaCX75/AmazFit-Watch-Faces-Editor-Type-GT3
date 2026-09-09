using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Watch_Face_Editor
{
    internal class SystemInfo
    {
        // =========================================================
        // Windows
        // =========================================================

        public static string GetWindowsVersion()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                {
                    if (key == null)
                        return "Unknown";

                    string productName =
                        key.GetValue("ProductName") as string;

                    string displayVersion =
                        key.GetValue("DisplayVersion") as string;

                    string currentBuild =
                        key.GetValue("CurrentBuild") as string;

                    // Определяем Windows 11 по номеру сборки.
                    // Windows 11 начинается с Build 22000.
                    int build;

                    bool isWindows11 =
                        int.TryParse(currentBuild, out build) &&
                        build >= 22000;

                    // Исправляем ProductName, если реальная система Windows 11
                    if (isWindows11 &&
                        !string.IsNullOrWhiteSpace(productName))
                    {
                        productName = productName.Replace(
                            "Windows 10",
                            "Windows 11");
                    }

                    if (string.IsNullOrWhiteSpace(productName))
                        productName = isWindows11
                            ? "Windows 11"
                            : "Windows";

                    string result = productName;

                    if (!string.IsNullOrWhiteSpace(displayVersion))
                    {
                        result += " " + displayVersion;
                    }

                    if (!string.IsNullOrWhiteSpace(currentBuild))
                    {
                        result += " (Build " + currentBuild + ")";
                    }

                    return result;
                }
            }
            catch
            {
                return "Unknown";
            }
        }


        // =========================================================
        // .NET Framework
        // =========================================================

        public static string GetNetFrameworkVersion()
        {
            try
            {
                List<int> releases = new List<int>();

                CheckNetFrameworkRegistry(
                    RegistryView.Registry64,
                    releases);

                CheckNetFrameworkRegistry(
                    RegistryView.Registry32,
                    releases);

                if (releases.Count == 0)
                    return "Not installed";

                int maxRelease = releases.Max();

                return GetNetFrameworkVersionName(maxRelease);
            }
            catch
            {
                return "Unknown";
            }
        }


        private static void CheckNetFrameworkRegistry(
            RegistryView view,
            List<int> releases)
        {
            try
            {
                using (RegistryKey baseKey =
                    RegistryKey.OpenBaseKey(
                        RegistryHive.LocalMachine,
                        view))
                {
                    using (RegistryKey key =
                        baseKey.OpenSubKey(
                            @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"))
                    {
                        if (key == null)
                            return;

                        object releaseValue =
                            key.GetValue("Release");

                        if (releaseValue == null)
                            return;

                        int release;

                        if (int.TryParse(
                            releaseValue.ToString(),
                            out release))
                        {
                            if (!releases.Contains(release))
                                releases.Add(release);
                        }
                    }
                }
            }
            catch
            {
                // Игнорируем недоступную ветку Registry32/Registry64
            }
        }


        private static string GetNetFrameworkVersionName(int release)
        {
            // .NET Framework 4.8.1
            if (release >= 533320)
                return "4.8.1";

            // .NET Framework 4.8
            if (release >= 528040)
                return "4.8";

            // .NET Framework 4.7.2
            if (release >= 461808)
                return "4.7.2";

            // .NET Framework 4.7.1
            if (release >= 461308)
                return "4.7.1";

            // .NET Framework 4.7
            if (release >= 460798)
                return "4.7";

            // .NET Framework 4.6.2
            if (release >= 394802)
                return "4.6.2";

            // .NET Framework 4.6.1
            if (release >= 394254)
                return "4.6.1";

            // .NET Framework 4.6
            if (release >= 393295)
                return "4.6";

            // .NET Framework 4.5.2
            if (release >= 379893)
                return "4.5.2";

            // .NET Framework 4.5.1
            if (release >= 378675)
                return "4.5.1";

            // .NET Framework 4.5
            if (release >= 378389)
                return "4.5";

            return "4.x";
        }


        // =========================================================
        // .NET / ASP.NET Core / Windows Desktop
        // =========================================================

        public static string GetDotNetVersion()
        {
            List<string> versions = GetRuntimes("Microsoft.NETCore.App");

            return FormatVersions(versions);
        }


        public static string GetAspNetCoreVersion()
        {
            List<string> versions = GetRuntimes("Microsoft.AspNetCore.App");

            return FormatVersions(versions);
        }


        public static string GetWindowsDesktopVersion()
        {
            List<string> versions = GetRuntimes("Microsoft.WindowsDesktop.App");

            return FormatVersions(versions);
        }


        private static List<string> GetRuntimes(string runtimeName)
        {
            List<string> result = new List<string>();

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "--list-runtimes",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    if (process == null)
                        return result;

                    string output = process.StandardOutput.ReadToEnd();

                    process.WaitForExit(5000);

                    using (StringReader reader = new StringReader(output))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!line.StartsWith(
                                runtimeName + " ",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            string version = line.Substring(runtimeName.Length).Trim();

                            // Убираем путь в квадратных скобках
                            int bracketIndex = version.IndexOf('[');

                            if (bracketIndex >= 0)
                            {
                                version = version.Substring(0, bracketIndex).Trim();
                            }

                            if (!string.IsNullOrWhiteSpace(version))
                                result.Add(version);
                        }
                    }
                }
            }
            catch
            {
                // dotnet может отсутствовать
            }

            return result
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(v => v, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }


        private static string FormatVersions(List<string> versions)
        {
            if (versions == null || versions.Count == 0)
                return "Not installed";

            return string.Join(", ", versions);
        }


        // =========================================================
        // Язык Windows
        // =========================================================

        public static string GetSystemLanguage()
        {
            try
            {
                return CultureInfo.InstalledUICulture.Name;
            }
            catch
            {
                return "Unknown";
            }
        }


        // =========================================================
        // Версия приложения
        // =========================================================

        public static string GetAppVersion()
        {
            try
            {
                Assembly assembly =
                    Assembly.GetEntryAssembly();

                if (assembly == null)
                    return "Unknown";

                AssemblyInformationalVersionAttribute informational =
                    assembly.GetCustomAttributes(
                        typeof(AssemblyInformationalVersionAttribute),
                        false)
                    .OfType<AssemblyInformationalVersionAttribute>()
                    .FirstOrDefault();

                if (informational != null &&
                    !string.IsNullOrWhiteSpace(
                        informational.InformationalVersion))
                {
                    return informational.InformationalVersion;
                }

                return assembly.GetName()
                    .Version
                    .ToString();
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
