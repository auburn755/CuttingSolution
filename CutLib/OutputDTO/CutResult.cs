using CutLib.DTO;
using CutLib.InputClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.OutputClasses
{
    public class CutResult
    {
        public MaterialInfo Material { get; set; }= new MaterialInfo();
        public List<CuttingLayout> CuttingLayouts { get; set; } = new();
        public double TotalLengthAllCuts
        {
            get
            {
                double length = 0;
                foreach (CuttingLayout layout in CuttingLayouts) { length += layout.LengthAllCuts; }
                return length;
            }
        }
    }
}
