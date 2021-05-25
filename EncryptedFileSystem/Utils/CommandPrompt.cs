using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedFileSystem.Utils
{
    class CommandPrompt
    {
        public static void ExecuteCommand(String command)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo("cmd.exe", "/C " + command);
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            //StringBuilder q = new StringBuilder();
            //while (!process.HasExited)
            //{
            //    q.Append(process.StandardOutput.ReadToEnd());
            //}
            process.WaitForExit();
        }
    }
}
