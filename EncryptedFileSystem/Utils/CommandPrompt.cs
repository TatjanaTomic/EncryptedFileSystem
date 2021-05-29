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
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }

        public static string ExecuteCommandWithResponse(String command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            //StringBuilder sb = new StringBuilder();
            //while (!process.HasExited)
            //{
            //    sb.Append(process.StandardOutput.ReadToEnd());
            //}
            
            process.WaitForExit();
            string result = process.StandardOutput.ReadToEnd();

            return result;
        }
    }
}
