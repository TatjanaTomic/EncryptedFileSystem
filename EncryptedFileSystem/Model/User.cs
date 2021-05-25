using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptedFileSystem.Model
{
    public class User
    {
        private readonly string name;
        private readonly string password;
        private readonly int hashAlgorythm;
        private readonly string encAlgorythm;

        public User()
        {

        }

        public User(string name, string password, int hashAlgorythm, string encAlgorythm)
        {
            this.name = name;
            this.password = password;
            this.hashAlgorythm = hashAlgorythm;
            this.encAlgorythm = encAlgorythm; 
        }

        public string Name => name;

        public string Password => password;

        public int HashAlgorythm => hashAlgorythm;

        public string EncAlgorythm => encAlgorythm;

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Name == user.Name &&
                   Password == user.Password &&
                   HashAlgorythm == user.HashAlgorythm &&
                   EncAlgorythm == user.EncAlgorythm;
        }

        public override int GetHashCode()
        {
            int hashCode = -286654916;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + HashAlgorythm.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EncAlgorythm);
            return hashCode;
        }

        public void WriteInfo()
        {
            DirectoryInfo home = new DirectoryInfo(Application.StartupPath + "\\FileSystem" + "\\" + Name);
            try
            {
                if(!home.Exists)
                {
                    home.Create();
                }

                string[] lines = { Name, Password, HashAlgorythm.ToString(), EncAlgorythm };
                File.WriteAllLines(home.FullName + "\\info.txt", lines);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
