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
    class FileSystemController
    {
        private static readonly string CERTS_PATH = Application.StartupPath + "\\Certificates";
        private static readonly string FS_PATH = Application.StartupPath + "\\FileSystem";
        private static readonly string USERS_PATH = Application.StartupPath + "\\Users";
        private static readonly string SHARED_PATH = Application.StartupPath + "\\Shared";

        public static void OpenFile(FileInfo selectedFile, string username)
        {
            if(selectedFile.Exists)
            {
                User user = UserController.ReadUserInfo(username);

                if (!Verify(username, user.HashAlgorythm, selectedFile))
                    throw new EfsException("Datoteka " + selectedFile.Name + " ne može da se otvori jer je narušen njen integritet!");
                else
                {
                    System.Diagnostics.Process.Start(selectedFile.FullName);
                }
            }
        }

        public static void CreateNewFile(string username, string relativePath, string fileName, string content)
        {
            if (!fileName.EndsWith(".txt"))
                fileName += ".txt";
            string absolutePath = FS_PATH + "\\" + relativePath + "\\" + fileName;

            if (File.Exists(absolutePath))
            {
                throw new EfsException("Na izabranoj putanji postoji fajl sa unesenim nazivom. Unesite novi naziv.");
            }
            else
            {
                User user = UserController.ReadUserInfo(username);
                File.WriteAllText(absolutePath, content);

                Digest(user.Name, user.HashAlgorythm, absolutePath);
            }
        }

        public static void EditFile(string username, string relativePath, string fileName, string content)
        {
            string absolutePath = FS_PATH + "\\" + relativePath + "\\" + fileName;

            User user = UserController.ReadUserInfo(username);
            File.WriteAllText(absolutePath, content);

            Digest(user.Name, user.HashAlgorythm, absolutePath);
        }

        public static string GetFileContent(string username, FileInfo file)
        {
            User user = UserController.ReadUserInfo(username);
            string content;

            if (!file.Exists)
                throw new EfsException("Došlo je do greške prilikom otvaranja datoteke.");
            else if(!Verify(user.Name, user.HashAlgorythm, file))
            {
                throw new EfsException("Ne može se urediti datoteka " + file.Name + " jer je narušen njen integritet!");
            }
            else
            {
                content = File.ReadAllText(file.FullName);   
            }

            return content;
        }


        public static string DownloadFile(FileInfo selectedFile, string username)
        {
            //TODO: Nedovršeno
            User user = UserController.ReadUserInfo(username);
            string hostPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + selectedFile.Name;

            if (!Verify(username, user.HashAlgorythm, selectedFile))
                throw new EfsException("Datoteka " + selectedFile.Name + " ne može da se preuzme jer je narušen njen integritet!");
            else
            {
                File.Copy(selectedFile.FullName, hostPath);
            }

            return hostPath;
        }

        public static void DeleteFromShared()
        {
            throw new NotImplementedException();
        }

        public static void MakeDirectory(string username, string relativePath, string name)
        {
            string absolutePath = FS_PATH + "\\" + relativePath + "\\" + name;

            if (Directory.Exists(absolutePath))
                throw new EfsException("Već postoji direktorijum sa unesenim nazivom na izabranoj putanji!");
            else
                Directory.CreateDirectory(absolutePath);
        }

        private static bool Verify(string username, string hashAlgorythm, FileInfo file)
        {
            hashAlgorythm = GetHashAlgorythm(hashAlgorythm);
            string publicKey = CERTS_PATH + "\\public\\" + username + "_public.key";
            string signature = file.FullName.Insert(file.FullName.LastIndexOf('.'), "#");

            string command = "openssl dgst " + hashAlgorythm + " -verify " + publicKey + " -signature " + signature + " " + file.FullName;
            var result = CommandPrompt.ExecuteCommandWithResponse(command);

            return result.Trim().Equals("Verified OK");
        }

        public static void Upload(string username, string hostPath, string efsPath)
        {
            if (!File.Exists(hostPath))
                throw new EfsException("Došlo je do greške prilikom upload-a. Izaberite drugu datoteku.");
            else
            {
                //TODO: Dodati provjeru naziva, ne mogu se dodati datoteke sa istim nazivom

                User user = UserController.ReadUserInfo(username);
                FileInfo selectedFile = new FileInfo(hostPath);

                if (!CheckExtension(selectedFile))
                    throw new EfsException("Tip datoteke nije podržan!");
                else
                {
                    string newPath = efsPath + "\\" + selectedFile.Name.Replace(' ', '_').Replace('#', '_');
                    File.Copy(hostPath, newPath);

                    Digest(user.Name, user.HashAlgorythm, newPath);
                    //File.Delete(newPath);

                    //TODO: Provjeri da li još šta ovdje treba
                }
            }                
        }

        public static void DeleteFromFileSystem(string path, Object o)
        {
            if (o.GetType() == typeof(FileInfo))
            {
                File.Delete(path);
                string signature = path.Insert(path.LastIndexOf('.'), "#");
                File.Delete(signature);
            }
            else
                Directory.Delete(path, true);
        }

        private static bool CheckExtension(FileInfo file)
        {
            var extension = file.Extension;

            return (extension.Equals(".txt") || extension.Equals(".pdf") || extension.Equals(".docx") || extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg"));
        }

        private static void Digest(string username, string hashAlgorythm, string inFile)
        {
            hashAlgorythm = GetHashAlgorythm(hashAlgorythm);
            string privateKey = CERTS_PATH + "\\private\\" + username + "_private.key";
            string outFile = inFile.Insert(inFile.LastIndexOf('.'), "#");

            string command = "openssl dgst " + hashAlgorythm + " -sign " + privateKey + " -out " + outFile + " " + inFile;
            CommandPrompt.ExecuteCommand(command);
        }

        private static string GetHashAlgorythm(string hashAlgorythm)
        {
            switch (hashAlgorythm)
            {
                case "1":
                    hashAlgorythm = "-md5";
                    break;
                case "5":
                    hashAlgorythm = "-sha256";
                    break;
                case "6":
                    hashAlgorythm = "-sha512";
                    break;
            }

            return hashAlgorythm;
        }
    }
}
