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

    /* пример исключения
     * public class InvalidStockException : CutLibException
     *  {
     *  public InvalidStockException(string message) : base(message) { }
     *  }
     *  
     *  пример вызова исключения в библиотеке
     *  if (stock.Width > baseStock.Width || stock.Height > baseStock.Height)
     *  throw new InvalidStockException("Обрезок превышает базовый лист по размеру.");
    */
}
