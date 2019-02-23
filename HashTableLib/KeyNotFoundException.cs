using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableLib
{
    public class KeyNotFoundException : Exception
    {
        internal KeyNotFoundException(string message) : base(message)
        {

        }
    }
}
