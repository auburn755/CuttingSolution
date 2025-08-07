using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.DTO
{
    public class StockData
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public int Count { get; set; }
        public double TrimLeft { get; set; }
        public double TrimRight { get; set; }
        public double TrimTop { get; set; }
        public double TrimBottom { get; set; }
        public bool IsTexture { get; set; }
    }
}
