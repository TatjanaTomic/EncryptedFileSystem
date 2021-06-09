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
                {
                    user.WriteInfo();
                }
                else
                {
                    throw new EfsException("Došlo je do greške prilikom rgistracije!");
                }

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
                    ValidateCertificate(username);

                    //Provjera lozinke
                    var passwordHash = Passwd(password, user.HashAlgorythm);
                    if (!passwordHash.Equals(user.Password))
                        throw new EfsException("Pogrešna lozinka!");
                    else
                        return user;
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

        public static void ValidateCertificate(string username)
        {
            string path = CERTS_PATH + "\\certs\\" + username + ".crt";

            if (!File.Exists(path))
                throw new EfsException("Nije pronađen digitalni sertifikat korisnika " + username + "!");
            else
            {
                string commandVerifyCrt = "openssl verify -trusted " + CERTS_PATH + "\\certs\\rootca.crt " + path;
                var verified = CommandPrompt.ExecuteCommandWithResponse(commandVerifyCrt);
                if (!verified.Contains("OK"))
                    throw new EfsException("Digitalni sertifikat korisnika " + username + " nije izdat od strane tijela kome se vjeruje!");

                string commandDates = "openssl x509 -in " + path + " -noout -dates";
                var dates = CommandPrompt.ExecuteCommandWithResponse(commandDates);
                if (!CheckDates(dates))
                    throw new EfsException("Digitalni sertifikat korisnika " + username + " trenutno nije važeći!");

                string commandSerialNumber = "openssl x509 -in " + path + " -noout -serial";
                var serialNumber = CommandPrompt.ExecuteCommandWithResponse(commandSerialNumber).Trim().Substring(7);
                if (IsRevoked(serialNumber))
                    throw new EfsException("Digitalni sertifikat korisnika " + username + " je trenutno povučen iz upotrebe!");
            }
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

        private static bool CheckDates(string dates)
        {
            string notBefore = dates.Split('\n')[0].Substring(10).Trim().Replace("  ", " ");
            string notAfter = dates.Split('\n')[1].Substring(9).Trim().Replace("  ", " ");

            var partsNotBefore = notBefore.Split(' ');
            var partsNotAfter = notAfter.Split(' ');

            var notBeforeTime = partsNotBefore[2].Split(':');
            var notAfterTime = partsNotAfter[2].Split(':');

            DateTime dateTimeNotBef = new DateTime(int.Parse(partsNotBefore[3]), GetMonth(partsNotBefore[0]), int.Parse(partsNotBefore[1]), int.Parse(notBeforeTime[0]), int.Parse(notBeforeTime[1]), int.Parse(notBeforeTime[2]));
            DateTime dateTimeNotAft = new DateTime(int.Parse(partsNotAfter[3]), GetMonth(partsNotAfter[0]), int.Parse(partsNotAfter[1]), int.Parse(notAfterTime[0]), int.Parse(notAfterTime[1]), int.Parse(notAfterTime[2]));

            return (dateTimeNotBef < DateTime.Now) && (dateTimeNotAft > DateTime.Now);
        }

        private static bool IsRevoked(string serial)
        {
            string indexPath = CERTS_PATH + "\\index.txt";
            var lines = File.ReadAllLines(indexPath);
            foreach (var line in lines)
                if (line.Contains("\t" + serial + "\t") && line.StartsWith("R\t"))
                    return true;

            return false;
        }

        private static int GetMonth(string month)
        {
            int monthNumber = 1;
            switch(month)
            {
                case "Jan":
                    monthNumber = 1;
                    break;
                case "Feb":
                    monthNumber = 2;
                    break;
                case "Mar":
                    monthNumber = 3;
                    break;
                case "Apr":
                    monthNumber = 4;
                    break;
                case "May":
                    monthNumber = 5;
                    break;
                case "Jun":
                    monthNumber = 6;
                    break;
                case "Jul":
                    monthNumber = 7;
                    break;
                case "Aug":
                    monthNumber = 8;
                    break;
                case "Sep":
                    monthNumber = 9;
                    break;
                case "Oct":
                    monthNumber = 10;
                    break;
                case "Nov":
                    monthNumber = 11;
                    break;
                case "Dec":
                    monthNumber = 12;
                    break;
            }

            return monthNumber;
        }

        #endregion
    }
}
