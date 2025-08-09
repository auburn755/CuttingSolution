using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.OutputClasses
{
    // класс представляет линию реза на макете раскроя
    public class CutLineLayout
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double Length
        {
            get
            {
                if (X1 == X2) return (Y2 - Y1);     // если линия реза вертикальная
                if (Y1==Y2) return (X2 - X1);       // если линия реза горизонтальная
                return 0;
            }
        }
    }
}
