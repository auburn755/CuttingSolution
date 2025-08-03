using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.OutputClasses
{
    public class CutResult
    {
        public string? Material { get; set; }
        public List<CuttingLayout> CuttingLayouts { get; set; } = new();
    }
}
