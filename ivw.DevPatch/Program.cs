using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ivw.DevPatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在卸载以前的版本...");
            //RemoveFromGac(Resources.CommonLib_v60);
            //RemoveFromGac(Resources.CommonLib_v61);
            //RemoveFromGac(Resources.CommonLib_v62);
            //RemoveFromGac(Resources.CommonLib_v70);
            Console.WriteLine("正在卸载Vsix...");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2005))
            {
                VsUnit.UnInstallAddIn(VsVersion.Vs2005);
            }
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2008))
            {
                VsUnit.UnInstallAddIn(VsVersion.Vs2008);
            }
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2010))
            {
                VsUnit.UnInstallAddIn(VsVersion.Vs2010);
            }
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2013))
            {
                VsUnit.UnInstallAddIn(VsVersion.Vs2013);
            }
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2015))
            {
                VsUnit.UnInstallAddIn(VsVersion.Vs2015);
            }
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2017))
            {
                VsUnit.UnInstallAddIn(VsVersion.Vs2017);
            }
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2019))
            {
                VsUnit.UnInstallAddIn(VsVersion.Vs2019);
            }
            Console.WriteLine("卸载Vsix成功...");
            Console.WriteLine("正在卸载Licenses...");
            Licenses.RemoveLicenses();
            Console.WriteLine("卸载Licenses成功...");

            UnNgen unNgen = new UnNgen();
            unNgen.ProgressChanged += UnNgen_ProgressChanged;
            unNgen.DoJob();
            InstallAddIn();
            InstallLicenses();

            Console.WriteLine("破解成功，请按回车键结束...");
            Console.ReadKey();
        }

        private static void RemoveFromGac(byte[] lib)
        {
            string text = Path.Combine(Path.GetTempPath(), "DevExpress.Patch.Common.dll");
            using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(lib, 0, lib.Length);
                fileStream.Close();
            }
            new Publish().GacRemove(text);
            Thread.Sleep(1000);
            File.Delete(text);
        }

        private static void UnNgen_ProgressChanged(object sender, UnNgen.ProgressArgs e)
        {
            Console.WriteLine($"{e.ProgressText}【进度：{e.PercentComplete}】");
        }

        private static void InstallAddIn()
        {
            UnNgen.ProgressArgs userState = new UnNgen.ProgressArgs(10, "正在安装补丁...");
            string text = Path.Combine(Path.GetTempPath(), "DevExpress.Patch.Common.dll");
            using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
            {
                byte[] commonLib = Resources.DevExpress_Patch_Common;
                fileStream.Write(commonLib, 0, commonLib.Length);
                fileStream.Close();
            }
            new Publish().GacInstall(text);
            Thread.Sleep(1000);
            File.Delete(text);
            userState = new UnNgen.ProgressArgs(20, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2005))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2005);
            }
            userState = new UnNgen.ProgressArgs(30, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2008))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2008);
            }
            userState = new UnNgen.ProgressArgs(40, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2010))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2010);
            }
            userState = new UnNgen.ProgressArgs(50, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2012))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2012);
            }
            userState = new UnNgen.ProgressArgs(60, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2013))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2013);
            }
            userState = new UnNgen.ProgressArgs(70, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2015))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2015);
            }
            userState = new UnNgen.ProgressArgs(80, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2017))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2017);
            }
            userState = new UnNgen.ProgressArgs(90, "正在安装补丁...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            if (VsUnit.IsVisualStudioInstalled(VsVersion.Vs2019))
            {
                VsUnit.InstallAddIn(VsVersion.Vs2019);
            }
            userState = new UnNgen.ProgressArgs(100, "安装补丁完成...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
        }

        private static void InstallLicenses()
        {
            UnNgen.ProgressArgs userState = new UnNgen.ProgressArgs(1, "正在安装版本号...");
            Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            List<Version> list = new List<Version>();
            for (int i = 7; i <= 19; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    list.Add(new Version(i, j));
                }
            }
            for (int k = 0; k < list.Count; k++)
            {
                Licenses.InstallLicense(GetUserName(), list[k]);
                userState = new UnNgen.ProgressArgs(100 * k / list.Count, "正在安装版本号...");
                Console.WriteLine($"{userState.ProgressText}【进度：{userState.PercentComplete}】");
            }
        }
        private static string GetUserName() => "dimaster";
    }
}
