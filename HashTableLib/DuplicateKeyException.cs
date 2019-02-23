using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableLib
{
    public class DuplicateKeyException : Exception
    {
        internal DuplicateKeyException(string message) : base(message)
        {

        }
    }
}
