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
        public SourceMaterial Material { get; set; }
        public List<CuttingLayout> CuttingLayouts { get; set; } = new();
    }
}
