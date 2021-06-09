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

        #region MyFiles

        public static void MakeDirectory(string relativePath, string name)
        {
            string absolutePath = FS_PATH + "\\" + relativePath + "\\" + name;

            if (Directory.Exists(absolutePath))
                throw new EfsException("Na izabranoj putanji postoji direktorijum sa unesenim nazivom! Unesite novi naziv.");
            else
                Directory.CreateDirectory(absolutePath);
        }

        public static void OpenFile(FileInfo selectedFile, string username)
        {
            Directory.Delete(TEMP_PATH, true);
            Directory.CreateDirectory(TEMP_PATH);

            User user = UserController.ReadUserInfo(username);
            if (selectedFile.Exists)
            {
                //pronađe se digitalni potpis izabrane datoteke
                FileInfo fileSignature = new FileInfo(selectedFile.FullName.Insert(selectedFile.FullName.LastIndexOf('.'), "#"));

                //svi fajlovi na fajl sistemu se čuvaju kriptovani pa je potrebno prvo ih dekriptovati
                var decryptedFile = DecryptFile(selectedFile, user.EncAlgorythm);
                var decryptedSignature = DecryptFile(fileSignature, user.EncAlgorythm);

                //provjeri se digitalni potpis
                if (!Verify(username, user.HashAlgorythm, decryptedFile, decryptedSignature))
                    throw new EfsException("Datoteka " + selectedFile.Name + " ne može da se otvori jer je narušen njen integritet!");
                else
                {
                    //preimenuje se dekriptovani fajl u naziv originalnog fajla zbog prikaza u odgovarajućem programu
                    decryptedFile.MoveTo(decryptedFile.DirectoryName + "\\" + selectedFile.Name);
                    //pokreće se proces koji otvara datoteku u podrazumijevanom programu
                    try
                    {
                        Process p = new Process
                        {
                            StartInfo = new ProcessStartInfo(decryptedFile.FullName)
                        };
                        p.Start();
                        p.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace + " : " + e.Message);
                    }
                }

                //čuva se eventualna nova verzija fajla, ako je nešto bilo promijenjeno (npr. iz notepada)
                //to će značiti da je narušen integritet fajla i fajl više neće biti upotrebljiv
                selectedFile.Delete();
                var newEncryptedFile = EncryptFile(decryptedFile, user.EncAlgorythm);
                newEncryptedFile.MoveTo(selectedFile.FullName);

                //brišu se dekriptovani fajlovi
                decryptedFile.Delete();
                decryptedSignature.Delete();
            }
        }

        public static void CreateNewFile(string username, string relativePath, string fileName, string content)
        {
            fileName = fileName.Replace(" ", "_").Replace("#", "-");
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

                //kreira se datoteka na privremenoj putanji i u nju se upiše tekst
                string tmpPath = TEMP_PATH + "\\" + fileName;
                File.WriteAllText(tmpPath, content);
                //digitalno se potpiše taj dokument i sačuva na istoj putanji
                var fileSignature = Digest(user.Name, user.HashAlgorythm, new FileInfo(tmpPath));

                //kriptuju se originalni fajl i digitalno potpisan korisnikovim simetričnim algoritmom
                var fileEnc = EncryptFile(new FileInfo(tmpPath), user.EncAlgorythm);
                var signatureEnc = EncryptFile(fileSignature, user.EncAlgorythm);

                //preimenuju se originalni fajl i digitalno potpisani da se izbaci "###" iz naziva
                fileEnc.MoveTo(absolutePath);
                signatureEnc.MoveTo(absolutePath.Insert(absolutePath.LastIndexOf("."), "#"));

                //brišu se nekriptovani fajlovi sa privremene putanje
                File.Delete(tmpPath);
                fileSignature.Delete();
            }
        }

        public static string GetFileContent(string username, FileInfo file)
        {
            string content;

            if (!file.Exists)
                throw new EfsException("Došlo je do greške prilikom otvaranja datoteke.");
            else
            {
                User user = UserController.ReadUserInfo(username);
                FileInfo fileSignature = new FileInfo(file.FullName.Insert(file.FullName.LastIndexOf('.'), "#"));

                var decryptedFile = DecryptFile(file, user.EncAlgorythm);
                var decryptedSignature = DecryptFile(fileSignature, user.EncAlgorythm);

                if (!Verify(user.Name, user.HashAlgorythm, decryptedFile, decryptedSignature))
                    throw new EfsException("Ne može se urediti datoteka " + file.Name + " jer je narušen njen integritet!");
                else
                    content = File.ReadAllText(decryptedFile.FullName);

                decryptedFile.Delete();
                decryptedSignature.Delete();
            }
            return content;
        }

        public static void EditFile(string username, string relativePath, string fileName, string content)
        {
            string absolutePath = FS_PATH + "\\" + relativePath + "\\" + fileName;
            FileInfo selectedFile = new FileInfo(absolutePath);
            FileInfo fileSignature = new FileInfo(absolutePath.Insert(absolutePath.LastIndexOf('.'), "#"));

            User user = UserController.ReadUserInfo(username);

            var decryptedFile = DecryptFile(selectedFile, user.EncAlgorythm);
            var decryptedSignature = DecryptFile(fileSignature, user.EncAlgorythm);

            File.WriteAllText(decryptedFile.FullName, content);
            var newSignature = Digest(user.Name, user.HashAlgorythm, decryptedFile);

            var fileEnc = EncryptFile(decryptedFile, user.EncAlgorythm);
            var newSignatureEnc = EncryptFile(newSignature, user.EncAlgorythm);

            selectedFile.Delete();
            fileSignature.Delete();
            decryptedFile.Delete();
            decryptedSignature.Delete();

            fileEnc.MoveTo(absolutePath);
            newSignatureEnc.MoveTo(absolutePath.Insert(absolutePath.LastIndexOf('.'), "#"));
        }

        public static void DeleteFromFileSystem(string path, Object o)
        {
            if (o.GetType() == typeof(FileInfo))
            {
                if (File.Exists(path))
                    File.Delete(path);

                string signature = path.Insert(path.LastIndexOf('.'), "#");
                if (File.Exists(signature))
                    File.Delete(signature);
            }
            else
                Directory.Delete(path, true);
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
                        newPath = GetNewPath(newPath);
                    
                    string tmpPath = TEMP_PATH + "\\" + selectedFile.Name.Replace(' ', '_').Replace('#', '_');

                    File.Copy(hostPath, tmpPath);
                    FileInfo newFile = new FileInfo(tmpPath);
                    var fileSignature = Digest(user.Name, user.HashAlgorythm, newFile);

                    var newFileEnc = EncryptFile(newFile, user.EncAlgorythm);
                    var fileSignatureEnc = EncryptFile(fileSignature, user.EncAlgorythm);

                    newFileEnc.MoveTo(newPath);
                    fileSignatureEnc.MoveTo(newPath.Insert(newPath.LastIndexOf('.'), "#"));

                    newFile.Delete();
                    fileSignature.Delete();                    
                }
            }
        }

        public static string DownloadFile(FileInfo selectedFile, string username)
        {
            User user = UserController.ReadUserInfo(username);
            string hostPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + selectedFile.Name;
            if(File.Exists(hostPath))
                hostPath = GetNewPath(hostPath);

            FileInfo fileSignature = new FileInfo(selectedFile.FullName.Insert(selectedFile.FullName.LastIndexOf('.'), "#"));

            var decryptedFile = DecryptFile(selectedFile, user.EncAlgorythm);
            var decryptedSignature = DecryptFile(fileSignature, user.EncAlgorythm);

            if (!Verify(username, user.HashAlgorythm, decryptedFile, decryptedSignature))
            {
                throw new EfsException("Datoteka " + selectedFile.Name + " ne može da se preuzme jer je narušen njen integritet!");
            }
            else
            {
                File.Copy(decryptedFile.FullName, hostPath);
            }

            decryptedFile.Delete();
            decryptedSignature.Delete();

            return hostPath;
        }

        #endregion MyFiles

        #region SharedFiles

        public static void ShareFile(string senderName, string receiverName, FileInfo file, string algorythm, string password)
        {
            if (!file.Exists)
                throw new EfsException("Greška prilikom otvaranja fajla!");
            else
            {
                User sender = UserController.ReadUserInfo(senderName);
                UserController.ValidateCertificate(receiverName);

                FileInfo fileSignature = new FileInfo(file.FullName.Insert(file.FullName.LastIndexOf('.'), "#"));

                //dekriptuju se fajl i digitalno potpisan fajl kako bi se mogli dalje koristiti
                var decryptedFile = DecryptFile(file, sender.EncAlgorythm);
                var decryptedSignature = DecryptFile(fileSignature, sender.EncAlgorythm);

                if (!Verify(sender.Name, sender.HashAlgorythm, decryptedFile, decryptedSignature))
                {
                    decryptedFile.Delete();
                    decryptedSignature.Delete();

                    throw new EfsException("Ne možete podijeliti datoteku jer je njen integritet narušen!");
                }
                else
                {
                    string destinationPath = SHARED_PATH + "\\" + receiverName;
                    if (!Directory.Exists(destinationPath))
                        Directory.CreateDirectory(destinationPath);

                    string sharedFilePath = destinationPath + "\\" + senderName + "_" + file.Name;
                    if (File.Exists(sharedFilePath))
                        sharedFilePath = GetNewPath(sharedFilePath);
                    var sharedFile = new FileInfo(sharedFilePath);

                    //kriptuju se originalni fajl i digitalno potpisan originalni fajl
                    //algoritmom i lozinkom koju je korisnik unio prilikom dijeljenja fajla
                    var fileEnc = EncryptFile(decryptedFile, algorythm, password);
                    var signatureEnc = EncryptFile(decryptedSignature, algorythm, password);

                    //zatim u fajl sa nazivom 'message_posiljalac_datum_vrijemeSlanja' upisuju se redom:
                    //naziv poslanog fajla, simetricni algoritam kojim je originalni fajl kriptovan i kljuc za enkripciju
                    //taj fajl se kriptuje javnim kljucem primaoca tako da ce ga samo primalac moci dekriptovati
                    string[] content = { sharedFile.Name, algorythm, password };
                    var encryptedInfoFile = EncryptInfoFile(senderName, receiverName, content);

                    //nakon sto se sve zavrsi, message file, enkriptovan originalni fajl i enkriptovan digitalno potpisan fajl
                    //se smjestaju u odredisni folder
                    encryptedInfoFile.MoveTo(destinationPath + "\\" + encryptedInfoFile.Name.Remove(0, 3));
                    fileEnc.MoveTo(sharedFilePath);
                    signatureEnc.MoveTo(sharedFilePath.Insert(sharedFilePath.LastIndexOf('.'), "#"));

                    decryptedFile.Delete();
                    decryptedSignature.Delete();
                }
            }
        }

        public static void DecryptSharedFile(string path, string algorythm, string key)
        {
            Directory.Delete(TEMP_PATH, true);
            Directory.CreateDirectory(TEMP_PATH);

            FileInfo file = new FileInfo(path);
            FileInfo fileSignature = new FileInfo(path.Insert(path.LastIndexOf("."), "#"));

            var decryptedFile = DecryptFile(file, algorythm, key);
            var decryptedSignature = DecryptFile(fileSignature, algorythm, key);

            string senderName = file.Name.Substring(0, file.Name.IndexOf('_'));
            string senderHashAlg = UserController.ReadUserInfo(senderName).HashAlgorythm;
            if (!Verify(senderName, senderHashAlg, decryptedFile, decryptedSignature))
            {
                throw new EfsException("Ne može se dekriptovati fajl!");
            }
            else
            {
                //preimenuje se dekriptovani fajl zbog prikaza korisiku
                decryptedFile.MoveTo(decryptedFile.DirectoryName + "\\" + file.Name);
                try
                {
                    Process p = new Process
                    {
                        StartInfo = new ProcessStartInfo(decryptedFile.FullName)
                    };
                    p.Start();
                    p.WaitForExit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace + " : " + e.Message);
                }
            }

            file.Delete();
            var newEncryptedFile = EncryptFile(decryptedFile, algorythm, key);
            newEncryptedFile.MoveTo(file.FullName);

            decryptedSignature.Delete();
            decryptedFile.Delete();
        }

        public static string ReadMessage(FileInfo infoFile, string username)
        {
            string content = null;
            if (infoFile.Exists)
            {
                string privateKey = CERTS_PATH + "\\private\\" + username + "_private.key";
                string tmpPath = TEMP_PATH + "\\tmp_" + DateTime.Now.ToString().Replace(' ', '_').Replace(':', '-') + ".txt";
                string command = "openssl rsautl -decrypt -in " + infoFile.FullName + " -out " + tmpPath + " -inkey " + privateKey;
                CommandPrompt.ExecuteCommand(command);

                content = File.ReadAllText(tmpPath);
                if (string.IsNullOrEmpty(content))
                    throw new EfsException("Nemate dozvolu za ovu operaciju!");

                File.Delete(tmpPath);
            }

            return content;
        }

        #endregion SharedFiles

        #region HelpMethods

        private static FileInfo EncryptFile(FileInfo file, string algorythm, string key = "criptography")
        {
            string encrypthedFile = file.FullName.Insert(file.FullName.LastIndexOf("\\") + 1, "###");
            string command = "openssl " + algorythm + " -in " + file.FullName + " -out " + encrypthedFile + " -nosalt -k " + key + " -base64";
            CommandPrompt.ExecuteCommand(command);
            return new FileInfo(encrypthedFile);
        }

        private static FileInfo DecryptFile(FileInfo file, string algorythm, string key = "criptography")
        {
            string decryptedFile = TEMP_PATH + "\\tmp_" + DateTime.Now.ToString().Replace(' ', '_').Replace(':', '-') + "_" + file.Name;
            string command = "openssl " + algorythm + " -d -in " + file.FullName + " -out " + decryptedFile + " -nosalt -k " + key + " -base64";
            CommandPrompt.ExecuteCommand(command);
            return new FileInfo(decryptedFile);

        }

        private static FileInfo EncryptInfoFile(string sender, string receiver, string[] content)
        {
            string infoFile = TEMP_PATH + "\\message_" + sender + "_" + DateTime.Now.ToString().Replace(' ', '_').Replace(':', '-') + ".txt";
            File.WriteAllLines(infoFile, content);

            string outInfoFile = infoFile.Insert(infoFile.LastIndexOf("\\") + 1, "###");
            string publicKey = CERTS_PATH + "\\public\\" + receiver + "_public.key";

            string command = "openssl rsautl -encrypt -in " + infoFile + " -out " + outInfoFile + " -inkey " + publicKey + " -pubin";
            CommandPrompt.ExecuteCommand(command);

            File.Delete(infoFile);

            return new FileInfo(outInfoFile);
        }

        private static bool Verify(string username, string hashAlgorythm, FileInfo file, FileInfo signature)
        {
            hashAlgorythm = GetHashAlgorythm(hashAlgorythm);
            string publicKey = CERTS_PATH + "\\public\\" + username + "_public.key";

            string command = "openssl dgst " + hashAlgorythm + " -verify " + publicKey + " -signature " + signature + " " + file.FullName;
            var result = CommandPrompt.ExecuteCommandWithResponse(command);

            return result.Trim().Equals("Verified OK");
        }

        private static bool CheckExtension(FileInfo file)
        {
            var extension = file.Extension;

            return (extension.Equals(".txt") || extension.Equals(".pdf") || extension.Equals(".docx") || extension.Equals(".png") || extension.Equals(".jpg") || extension.Equals(".jpeg"));
        }

        private static FileInfo Digest(string username, string hashAlgorythm, FileInfo inFile)
        {
            hashAlgorythm = GetHashAlgorythm(hashAlgorythm);
            string privateKey = CERTS_PATH + "\\private\\" + username + "_private.key";
            string outFile = inFile.FullName.Insert(inFile.FullName.LastIndexOf('.'), "#");

            string command = "openssl dgst " + hashAlgorythm + " -sign " + privateKey + " -out " + outFile + " " + inFile;
            CommandPrompt.ExecuteCommand(command);
            return new FileInfo(outFile);
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

        private static string GetNewPath(string path, string newPath = "", int number = 0)
        {
            number += 1;
            newPath = path.Insert(path.LastIndexOf('.'), "(" + number + ")" );

            if (File.Exists(newPath))
            {
                newPath = GetNewPath(path, newPath, number);
            }
            
            return newPath;
        }

        #endregion HelpMethods

    }
}
