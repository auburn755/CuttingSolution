using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.DTO
{
    public class CutSettingsData
    {
        public double SawWidth { get; set; } = 0;
        public double MaxCutLength { get; set; } = 0;
        public int MaxSheetRotation { get; set; } = 0;
        public double MinWasteLength { get; set; } = 0;
    }
}
