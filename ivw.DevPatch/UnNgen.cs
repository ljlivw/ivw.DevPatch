using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ivw.DevPatch
{
    internal class UnNgen
    {
        private class Ngen
        {
            private readonly string _ngen;

            public Ngen()
            {
                _ngen = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "ngen.exe");
            }

            public string[] Display(string filterString = "")
            {
                string[] array = Process.Start(new ProcessStartInfo(_ngen, "display")
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }).StandardOutput.ReadToEnd().Split(new string[1]
                {
                    "\n"
                }, StringSplitOptions.RemoveEmptyEntries);
                List<string> list = new List<string>();
                string[] array2 = array;
                foreach (string text in array2)
                {
                    if (string.IsNullOrEmpty(filterString))
                    {
                        list.Add(text);
                    }
                    else if (text.Contains(filterString))
                    {
                        list.Add(text);
                    }
                }
                return list.ToArray();
            }

            public string Uninstall(string assembly)
            {
                assembly = assembly.Replace("\r", "");
                return Process.Start(new ProcessStartInfo(_ngen, $"uninstall \"{assembly}\"")
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }).StandardOutput.ReadToEnd();
            }

            public string ExecuteQueue()
            {
                return Process.Start(new ProcessStartInfo(_ngen, "executeQueuedItems")
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }).StandardOutput.ReadToEnd();
            }
        }

        public class ProgressArgs : EventArgs
        {
            public int PercentComplete
            {
                get;
                private set;
            }

            public string ProgressText
            {
                get;
                private set;
            }

            public bool ContiniousProgress
            {
                get;
                private set;
            }

            internal ProgressArgs(int percentComplete, string progressText)
            {
                PercentComplete = percentComplete;
                ProgressText = progressText;
                ContiniousProgress = false;
            }

            internal ProgressArgs(int percentComplete, string progressText, bool continiousProgress)
            {
                PercentComplete = percentComplete;
                ProgressText = progressText;
                ContiniousProgress = continiousProgress;
            }
        }

        public event EventHandler<ProgressArgs> ProgressChanged;

        protected virtual void OnProgressChanged(ProgressArgs e)
        {
            this.ProgressChanged?.Invoke(this, e);
        }

        public void DoJob()
        {
            Ngen ngen = new Ngen();
            OnProgressChanged(new ProgressArgs(1, "正在执行Ngen项目（可能需要几分钟）...", continiousProgress: true));
            ngen.ExecuteQueue();
            OnProgressChanged(new ProgressArgs(50, "正在搜索未生成的本机映像...", continiousProgress: true));
            string[] array = ngen.Display("DevExpress.");
            OnProgressChanged(new ProgressArgs(100, $"{array.Length} 找到本机映像. 准备卸载..."));
            Thread.Sleep(1000);
            int num = 0;
            string[] array2 = array;
            foreach (string assembly in array2)
            {
                num++;
                ngen.Uninstall(assembly);
                OnProgressChanged(new ProgressArgs(100 * num / array.Length, $"本机映像 {num} 的 {array.Length} 已卸载."));
            }
            OnProgressChanged(new ProgressArgs(100, "所有操作完成."));
        }
    }
}
