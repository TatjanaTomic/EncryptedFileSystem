using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedFileSystem
{
    static class Program
    {
        private static readonly string FS_PATH = Application.StartupPath + "\\" + "FileSystem";
        private static readonly string USERS_PATH = Application.StartupPath + "\\" + "Users";
        private static readonly string SHARED_PATH = Application.StartupPath + "\\" + "Shared";
        private static readonly string CERTS_PATH = Application.StartupPath + "\\" + "Certificates";
        private static readonly string TEMP_PATH = Application.StartupPath + "\\Temp";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MakeDirectories();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        private static void MakeDirectories()
        {
            if (!Directory.Exists(CERTS_PATH))
                throw new Exception("Za pokretanje aplikacije neophodno je da postoji okruženje za CA tijelo smješteno u direktorijum \"Certificates\"! ");

            if (!Directory.Exists(FS_PATH))
                Directory.CreateDirectory(FS_PATH);
            
            if (!Directory.Exists(USERS_PATH))
                Directory.CreateDirectory(USERS_PATH);

            if (!Directory.Exists(SHARED_PATH))
                Directory.CreateDirectory(SHARED_PATH);

            if (Directory.Exists(TEMP_PATH))
                Directory.Delete(TEMP_PATH, true);
            else
                Directory.CreateDirectory(TEMP_PATH);
        }
    }
}
