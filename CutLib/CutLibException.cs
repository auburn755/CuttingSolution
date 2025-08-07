using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib
{
    public class CutLibException : Exception
    {
        public CutLibException(string message) : base(message) { }
    }

    public class InvalidPartsListException:CutLibException
    { 
        public InvalidPartsListException(string message) : base(message) { }
    }

    public class InvalidOffcutsListException : CutLibException
    {
        public InvalidOffcutsListException(string message) : base(message) { }
    }
    public class InvalidIndexRangeException : CutLibException
    {
        public InvalidIndexRangeException(string message) : base(message) { }
    }
}
