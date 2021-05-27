using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptedFileSystem.Exceptions
{
    public class EfsException : Exception
    {
        public EfsException()
        {

        }

        public EfsException(string message) : base(message)
        {

        }

    }
}
