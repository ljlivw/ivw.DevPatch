using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ivw.DevPatch
{
    public class VsUnit
    {
        public static bool IsVisualStudioInstalled(VsVersion vsVersion)
        {
            string[] subKeyNames = Registry.ClassesRoot.GetSubKeyNames();
            switch (vsVersion)
            {
                case VsVersion.Vs2005:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.8.0") > 0;
                case VsVersion.Vs2008:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.9.0") > 0;
                case VsVersion.Vs2010:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.10.0") > 0;
                case VsVersion.Vs2012:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.11.0") > 0;
                case VsVersion.Vs2013:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.12.0") > 0;
                case VsVersion.Vs2015:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.14.0") > 0;
                case VsVersion.Vs2017:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.15.0") > 0;
                case VsVersion.Vs2019:
                    return Array.IndexOf(subKeyNames, "VisualStudio.DTE.16.0") > 0;
                default:
                    return false;
            }
        }
        public static bool InstallAddIn(VsVersion vsVersion)
        {
            switch (vsVersion)
            {
                case VsVersion.Vs2005:
                case VsVersion.Vs2008:
                case VsVersion.Vs2010:
                case VsVersion.Vs2012:
                case VsVersion.Vs2013:
                    return InstallAddIn(GetAddInstallPath(vsVersion), vsVersion);
                case VsVersion.Vs2015:
                case VsVersion.Vs2017:
                case VsVersion.Vs2019:
                    return InstallVsix(vsVersion);
                default:
                    throw new ArgumentOutOfRangeException("vsVer", vsVersion, null);
            }
        }
        public static bool UnInstallAddIn(VsVersion vsVersion)
        {
            switch (vsVersion)
            {
                case VsVersion.Vs2005:
                case VsVersion.Vs2008:
                case VsVersion.Vs2010:
                case VsVersion.Vs2012:
                case VsVersion.Vs2013:
                    return UnInstallAddIn(GetAddInstallPath(vsVersion), vsVersion);
                case VsVersion.Vs2015:
                case VsVersion.Vs2017:
                case VsVersion.Vs2019:
                    return InstallVsix(vsVersion);
                default:
                    throw new ArgumentOutOfRangeException("vsVer", vsVersion, null);
            }
        }

        public static bool InstallAddIn(string AddInstallPath, VsVersion vsVersion)
        {
            try
            {
                if (!Directory.Exists(AddInstallPath))
                {
                    Directory.CreateDirectory(AddInstallPath);
                }
                //FileStream fileStream = new FileStream(Path.Combine(AddInstallPath, "DevExpress.Patch.Vsa.AddIn"), FileMode.Create, FileAccess.Write);
                //fileStream.Write(Resources.AddInXml, 0, Resources.AddInXml.Length);
                //fileStream.Close();
                //FileStream fileStream2 = new FileStream(Path.Combine(AddInstallPath, "DevExpress.Patch.Vsa.dll"), FileMode.Create, FileAccess.Write);
                //fileStream2.Write(Resources.AddInDll, 0, Resources.AddInDll.Length);
                //fileStream2.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool UnInstallAddIn(string AddInstallPath, VsVersion vsVersion)
        {
            try
            {
                string path = "DevExpress.Patch.Vsa.AddIn";
                path = Path.Combine(AddInstallPath, path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                path = "DevExpress.Patch.Vsa.dll";
                path = Path.Combine(AddInstallPath, path);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        private static bool InstallVsix(VsVersion vsVersion)
        {
            if (!IsVsixSupported())
            {
                return false;
            }
            string text = null;
            switch (vsVersion)
            {
                case VsVersion.Vs2015:
                case VsVersion.Vs2017:
                case VsVersion.Vs2019:
                    text = "ac087982-c7c2-4312-b8ea-4a31e1add2c6";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("vsVer", vsVersion, null);
                case VsVersion.Vs2005:
                case VsVersion.Vs2008:
                case VsVersion.Vs2010:
                case VsVersion.Vs2012:
                case VsVersion.Vs2013:
                    break;
            }
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            return Process.Start(new ProcessStartInfo(GetVsixInstallerPath(), $"/quiet /admin /uninstall:{text}")
            {
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            })?.WaitForExit(300000) ?? false;
        }

        private static string GetAddInstallPath(VsVersion vsVersion) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft\\VisualStudio\\@vsVer@\\Addins").Replace("@vsVer@", VersionToVersionString(vsVersion));
        public static string VersionToVersionString(VsVersion vsVersion)
        {
            switch (vsVersion)
            {
                case VsVersion.Vs2005:
                    return "8.0";
                case VsVersion.Vs2008:
                    return "9.0";
                case VsVersion.Vs2010:
                    return "10.0";
                case VsVersion.Vs2012:
                    return "11.0";
                case VsVersion.Vs2013:
                    return "12.0";
                case VsVersion.Vs2015:
                    return "14.0";
                case VsVersion.Vs2017:
                    return "15.0";
                case VsVersion.Vs2019:
                    return "16.0";
                default:
                    return "";
            }
        }
        public static bool IsVsixSupported()
        {
            string vsixInstallerPath = GetVsixInstallerPath();
            if (!string.IsNullOrEmpty(vsixInstallerPath))
            {
                return File.Exists(vsixInstallerPath);
            }
            return false;
        }
        private static string GetVsixInstallerPath()
        {
            string format = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft Visual Studio @vsVer@\\Common7\\IDE\\VSIXInstaller.exe");
            string format2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft Visual Studio\\2019\\Community\\Common7\\IDE\\VSIXInstaller.exe");
            string format3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft Visual Studio\\2019\\Professional\\Common7\\IDE\\VSIXInstaller.exe");
            string format4 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Microsoft Visual Studio\\2019\\Enterprise\\Common7\\IDE\\VSIXInstaller.exe");
            string text = string.Format(format4, VersionToVersionString(VsVersion.Vs2019));
            if (File.Exists(text))
            {
                return text;
            }
            text = string.Format(format3, VersionToVersionString(VsVersion.Vs2017));
            if (File.Exists(text))
            {
                return text;
            }
            text = string.Format(format4, VersionToVersionString(VsVersion.Vs2017));
            if (File.Exists(text))
            {
                return text;
            }
            text = string.Format(format, VersionToVersionString(VsVersion.Vs2015));
            if (File.Exists(text))
            {
                return text;
            }
            text = string.Format(format, VersionToVersionString(VsVersion.Vs2013));
            if (File.Exists(text))
            {
                return text;
            }
            text = string.Format(format, VersionToVersionString(VsVersion.Vs2012));
            if (File.Exists(text))
            {
                return text;
            }
            text = string.Format(format, VersionToVersionString(VsVersion.Vs2010));
            if (File.Exists(text))
            {
                return text;
            }
            return string.Empty;
        }
    }
}
