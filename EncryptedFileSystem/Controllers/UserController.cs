using EncryptedFileSystem.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedFileSystem.Controllers
{
    class UserController
    {
        private static readonly string CERTS_PATH = Application.StartupPath + "\\Certificates";
        private static readonly string FS_PATH = Application.StartupPath + "\\FileSystem";

        public static bool RegisterUser(Model.User user)
        {
            //prvo izvrsavam skriptu za generisanje para kljuceva
            string keysCommand = CERTS_PATH + "\\keysScript.sh" + " " + user.Name;
            CommandPrompt.ExecuteCommand(keysCommand);

            //Zatim izdam sertifikate
            string certsCommand = CERTS_PATH + "\\certsScript.sh" + " " + user.Name;
            CommandPrompt.ExecuteCommand(certsCommand);

            if (File.Exists(CERTS_PATH + "\\private\\" + user.Name + "_private.key")
                && File.Exists(CERTS_PATH + "\\certs\\" + user.Name + ".crt"))
            {
                //Upišem user-a u fajl
                user.WriteInfo();
                return true;
            }
            else
                return false;
        }

        public static bool CheckNameExists(string name)
        {
            DirectoryInfo root = new DirectoryInfo(Application.StartupPath + "\\FileSystem");
            var homeDirs = root.GetDirectories();

            bool exists = false;
            foreach (var home in homeDirs)
                if (home.Name.Equals(name))
                    exists = true;

            return exists;
        }
    }
}
