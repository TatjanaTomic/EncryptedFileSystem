using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedFileSystem.Controllers
{
    class FileSystemController
    {
        public static void DeleteFromFileSystem(string path, int isFileOdFolder)
        {
            if (isFileOdFolder == 0)
                File.Delete(path);
            else
                Directory.Delete(path, true);
        }
    }
}
