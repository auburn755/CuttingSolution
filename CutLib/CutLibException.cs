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
    public class CutLibInvalidPartsException:CutLibException                    
    { 
        public CutLibInvalidPartsException(string message) : base(message) { }
    }
    public class CutLibInvalidStocksException : CutLibException
    {
        public CutLibInvalidStocksException(string message) : base(message) { }
    }
    public class CutLibInvalidSettingsException : CutLibException
    {
        public CutLibInvalidSettingsException(string message) : base(message) { }
    }
    public class CutLibInvalidMaterialException : CutLibException
    {
        public CutLibInvalidMaterialException(string message) : base(message) { }
    }
    public class CutLibNotDataForCuttingException : CutLibException
    {
        public CutLibNotDataForCuttingException(string message) : base(message) { }
    }
    public class CutLibInvalidIndexRangeException : CutLibException
    {
        public CutLibInvalidIndexRangeException(string message) : base(message) { }
    }
    public class CutLibCuttingNotCompletedException : CutLibException
    {
        public CutLibCuttingNotCompletedException(string message) : base(message) { }
    }
}
