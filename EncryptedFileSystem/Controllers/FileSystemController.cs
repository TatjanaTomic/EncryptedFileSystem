using EncryptedFileSystem.Exceptions;
using EncryptedFileSystem.Model;
using EncryptedFileSystem.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static readonly string SHARED_PATH = Application.StartupPath + "\\Shared";
        private static readonly string TEMP_PATH = Application.StartupPath + "\\Temp";

        public static void ShareFile(string senderName, string receiverName, FileInfo file, string algorythm, string password)
        {
            if (!file.Exists)
                throw new EfsException("Greška prilikom otvaranja fajla!");
            else
            {
                User sender = UserController.ReadUserInfo(senderName);

                if (!Verify(sender.Name, sender.HashAlgorythm, file))
                    throw new EfsException("Ne možete podijeliti datoteku jer je njen integritet narušen!");
                else
                {
                    //TODO: posto na mom fajl sistemu sve treba biti kriptovano mojim simetricnim algoritmom, PRVO-PRVO trebam dekriptovati svoje fajlove


                    //prvo se kriptuju originalni fajl i digitalno potpisan originalni fajl
                    var files = EncryptForSharing(file, algorythm, password);

                    //zatim u fajl sa nazivom 'posiljalac_datum_vrijemeSlanja' upisuju se redom:
                    //naziv poslanog fajla, simetricni algoritam kojim je originalni fajl kriptovan i kljuc za enkripciju
                    //taj fajl se kriptuje javnim kljucem primaoca tako da ce ga samo primalac moci dekriptovati
                    string sourcePath = file.FullName.Substring(0, file.FullName.LastIndexOf("\\") + 1);
                    string[] content = { file.Name, algorythm, password };
                    var encryptedInfoFile = EncryptInfoFile(sourcePath, senderName, receiverName, content);

                    //prethodni rezultati se smjestaju u shared folder, u folder sa nazivom primaoca
                    string destinationPath = SHARED_PATH + "\\" + receiverName;
                    if (!Directory.Exists(destinationPath))
                        Directory.CreateDirectory(destinationPath);

                    File.Move(encryptedInfoFile.FullName, destinationPath + "\\" + encryptedInfoFile.Name.Remove(0, 3));
                    File.Move(files[0].FullName, destinationPath + "\\" + senderName + "_" + files[0].Name.Remove(0, 3));
                    File.Move(files[1].FullName, destinationPath + "\\" + senderName + "_" + files[1].Name.Remove(0, 3));
                }
            }
        }

        private static FileInfo EncryptInfoFile(string sourcePath, string sender, string receiver, string[] content)
        {
            string infoFile = sourcePath + "message_" + sender + "_" + DateTime.Now.ToString().Replace(' ', '_').Replace(':', '-') + ".txt";
            File.WriteAllLines(infoFile, content);

            string outInfoFile = infoFile.Insert(infoFile.LastIndexOf("\\") + 1, "###");
            string publicKey = CERTS_PATH + "\\public\\" + receiver + "_public.key";

            string command = "openssl rsautl -encrypt -in " + infoFile + " -out " + outInfoFile + " -inkey " + publicKey + " -pubin";
            CommandPrompt.ExecuteCommand(command);

            File.Delete(infoFile);

            return new FileInfo(outInfoFile);
        }

        private static List<FileInfo> EncryptForSharing(FileInfo file, string algorythm, string key)
        {
            string outFile = file.FullName.Insert(file.FullName.LastIndexOf("\\") + 1, "###");
            string command = "openssl " + algorythm + " -in " + file.FullName + " -out " + outFile + " -nosalt -k " + key + " -base64";
            CommandPrompt.ExecuteCommand(command);
            FileInfo output1 = new FileInfo(outFile);

            string inFile2 = file.FullName.Insert(file.FullName.LastIndexOf('.'), "#"); //ovo je digitalni potpis izabranog fajla, trebam i njega proslijediti
            string outFile2 = inFile2.Insert(inFile2.LastIndexOf("\\") + 1, "###");
            string command2 = "openssl " + algorythm + " -in " + inFile2 + " -out " + outFile2 + " -nosalt -k " + key + " -base64";
            CommandPrompt.ExecuteCommand(command2);
            FileInfo output2= new FileInfo(outFile2);

            return new List<FileInfo>
            {
                output1,
                output2
            };
        }

        public static void DecryptSharedFile(string path, string algorythm, string key)
        {
            FileInfo file = new FileInfo(path);
            string decryptedFile = TEMP_PATH + "\\tmp_" + DateTime.Now.ToString().Replace(' ', '_').Replace(':', '-') + "_" + file.Name;
            string commandDecryptFile = "openssl " + algorythm + " -d -in " + file.FullName + " -out " + decryptedFile + " -nosalt -k " + key + " -base64";
            CommandPrompt.ExecuteCommand(commandDecryptFile);

            string fileSignature = path.Insert(path.LastIndexOf('.'), "#");
            string decryptedSignature = TEMP_PATH + "\\tmp_" + DateTime.Now.ToString().Replace(' ', '_').Replace(':', '-') + "_" + file.Name.Insert(file.Name.LastIndexOf('.'), "#");
            string commandDecryptSignature = "openssl " + algorythm + " -d -in " + fileSignature + " -out " + decryptedSignature + " -nosalt -k " + key + " -base64";
            CommandPrompt.ExecuteCommand(commandDecryptSignature);

            string senderName = file.Name.Substring(0, file.Name.IndexOf('_'));
            string senderHashAlg = GetHashAlgorythm(UserController.ReadUserInfo(senderName).HashAlgorythm);
            string senderPublicKey = CERTS_PATH + "\\public\\" + senderName + "_public.key";
            string commandVerify = "openssl dgst " + senderHashAlg + " -verify " + senderPublicKey + " -signature " + decryptedSignature + " " + decryptedFile;
            var result = CommandPrompt.ExecuteCommandWithResponse(commandVerify).Trim();

            if (!result.Equals("Verified OK"))
            {
                throw new EfsException("Ne može se dekriptovati fajl!");
            }
            else
            {
                Process.Start(decryptedFile);
            }

        }

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

        public static string ReadMessage(FileInfo infoFile, string username)
        {
            string content = null;
            if (infoFile.Exists)
            {
                string privateKey = CERTS_PATH + "\\private\\" + username + "_private.key";
                string tmpPath = TEMP_PATH + "\\tmp.txt";
                string command = "openssl rsautl -decrypt -in " + infoFile.FullName + " -out " + tmpPath + " -inkey " + privateKey;
                CommandPrompt.ExecuteCommand(command);
                
                content = File.ReadAllText(tmpPath);
                if (string.IsNullOrEmpty(content))
                    throw new EfsException("Nemate dozvolu za ovu operaciju!");

                File.Delete(tmpPath);
            }

            return content;
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
                User user = UserController.ReadUserInfo(username);
                FileInfo selectedFile = new FileInfo(hostPath);

                if (!CheckExtension(selectedFile))
                    throw new EfsException("Tip datoteke nije podržan!");
                else
                {
                    string newPath = efsPath + "\\" + selectedFile.Name.Replace(' ', '_').Replace('#', '_');
                    if (File.Exists(newPath))
                        throw new EfsException("Na izabranoj putanji postoji datoteka sa istim nazivom!");
                    else
                    {
                        File.Copy(hostPath, newPath);

                        Digest(user.Name, user.HashAlgorythm, newPath);
                    }
                    //TODO: Provjeri da li još šta ovdje treba
                }
            }                
        }

        public static void DeleteFromFileSystem(string path, Object o)
        {
            if (o.GetType() == typeof(FileInfo))
            {
                if(File.Exists(path))
                    File.Delete(path);
                
                string signature = path.Insert(path.LastIndexOf('.'), "#");
                if(File.Exists(signature))
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
