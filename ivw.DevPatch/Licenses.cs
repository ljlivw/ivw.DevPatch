using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ivw.DevPatch
{
    public class Licenses
    {
        public static bool InstallLicense(string userName, Version dxVersion)
        {
            try
            {
                byte[] array = new byte[20];
                string text = string.Format("{0},{1},{1},{2},1,1,1", $"{dxVersion.Major}{dxVersion.Minor}", 36733309396484113L, userName);
                Array.Resize(ref array, array.Length + text.Length);
                byte[] bytes = Encoding.Default.GetBytes(text);
                for (int i = 0; i < bytes.Length; i++)
                {
                    array[i + 20] = bytes[i];
                }
                text = Convert.ToBase64String(array);
                byte[] buffer = SHA1.Create().ComputeHash(array);
                RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
                rSACryptoServiceProvider.FromXmlString(Resources.PatchRsaKey);
                byte[] array2 = rSACryptoServiceProvider.SignData(buffer, SHA1.Create());
                byte[] array3 = new byte[100];
                for (int j = 0; j < 100; j++)
                {
                    array3[j] = array2[j];
                }
                text = Convert.ToBase64String(array3) + text;
                using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey("Licenses\\0378852d-d597-4a32-b6d9-680a16a3cda6"))
                {
                    registryKey.SetValue($"{dxVersion.Major}{dxVersion.Minor}", text);
                    registryKey.Close();
                }
                using (RegistryKey registryKey2 = Registry.ClassesRoot.CreateSubKey("Licenses\\6F0F8269-1516-44C6-BD30-0E90BE27871C"))
                {
                    registryKey2.SetValue($"{dxVersion.Major}{dxVersion.Minor}", 1309, RegistryValueKind.DWord);
                    registryKey2.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool RemoveLicenses()
        {
            try
            {
                RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey("Licenses\\0378852d-d597-4a32-b6d9-680a16a3cda6");
                string[] valueNames = registryKey.GetValueNames();
                foreach (string name in valueNames)
                {
                    registryKey.DeleteValue(name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"【RemoveLicenses】失败：{ex.ToString()}");
                return false;
            }
            return true;
        }
    }
}
