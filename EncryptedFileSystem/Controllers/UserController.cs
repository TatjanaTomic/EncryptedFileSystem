using EncryptedFileSystem.Exceptions;
using EncryptedFileSystem.Model;
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
        private static readonly string USERS_PATH = Application.StartupPath + "\\Users";

        public static void RegisterUser(string username, string password, string hashAlgorythm, string encAlgorythm)
        {
            if (CheckNameExists(username))
                throw new EfsException("Uneseno korisničko ime već postoji! Molimo Vas da unesete novo korisničko ime.");
            else
            {
                User user = new User(username, Passwd(password, hashAlgorythm), hashAlgorythm, encAlgorythm);

                //pokretanje shall skripte koja generiše privatni i javni ključ za datog korisnika
                string keysCommand = CERTS_PATH + "\\keysScript.sh" + " " + user.Name;
                CommandPrompt.ExecuteCommand(keysCommand);

                //pokretanje shall skripte koja izdaje zahtjev za sertifikat, a zatim potpisuje zahtjev i kreira sertifikat
                string certsCommand = CERTS_PATH + "\\certsScript.sh" + " " + user.Name;
                CommandPrompt.ExecuteCommand(certsCommand);

                if (File.Exists(CERTS_PATH + "\\private\\" + user.Name + "_private.key")
                    && File.Exists(CERTS_PATH + "\\certs\\" + user.Name + ".crt"))

                    user.WriteInfo();

            }
        }

        public static User LoginUser(string username, string password)
        {
            if (!CheckNameExists(username))
                throw new EfsException("Ne postoji korisnički nalog koji odgovara unesenom korisničkom imenu!");
            else
            {
                User user = ReadUserInfo(username);

                if (user != null)
                {
                    //TODO: Dodati provjeru validnosti sertifikata

                    //Provjera lozinke
                    var passwordHash = Passwd(password, user.HashAlgorythm);
                    if (passwordHash.Equals(user.Password))
                        return user;
                    else
                        throw new EfsException("Pogrešna lozinka!");
                }
                else
                    throw new EfsException("Došlo je do greške prilikom prijave na sistem!");
            }
        }

        public static User ReadUserInfo(string name)
        {
            User user = null;

            string decryptCommand = "openssl aria-256-ecb -d -in Users/" + name + "#.txt -out Users/" + name + ".txt -pbkdf2 -k kriptografija -nosalt -base64";
            CommandPrompt.ExecuteCommand(decryptCommand);

            try
            {
                var path = USERS_PATH + "\\" + name + ".txt";
                string[] lines = File.ReadAllLines(path);
                File.Delete(path);

                user = new User(lines[0], lines[1], lines[2], lines[3]);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + " : " + e.Message);
            }

            return user;
        }

        public static List<string> GetUsers()
        {
            DirectoryInfo root = new DirectoryInfo(FS_PATH);
            var homeDirs = root.GetDirectories();
            List<string> users = new List<string>();

            foreach (var home in homeDirs)
                users.Add(home.Name);

            return users;
        }

        #region

        private static bool CheckNameExists(string name)
        {
            DirectoryInfo root = new DirectoryInfo(FS_PATH);
            var homeDirs = root.GetDirectories();

            bool exists = false;
            foreach (var home in homeDirs)
                if (home.Name.Equals(name))
                    exists = true;

            return exists;
        }

        private static string Passwd(string password, string hashAlgorythm)
        {
            string command = "openssl passwd -" + hashAlgorythm + " -salt password " + password;
            return CommandPrompt.ExecuteCommandWithResponse(command).Trim();
        }

        #endregion
    }
}
