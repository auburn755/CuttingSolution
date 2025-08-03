using CutLib.InputClasses;
using System.Collections;

namespace CutLib.OutputClasses
{
    // макет карты раскроя, содержит детали и линии разреза
    public class CuttingLayout
    {   
        internal SourceStock sourseStock { get; set; }
        public PlacedPartsLayouts Parts {  get; set; }= new PlacedPartsLayouts();
        public CutsLines Cuts { get; set; } = new CutsLines();
        public int CountParts
        {
            get { return Parts.parts.Count; }
        }
        public int CountCuts
        {
            get { return Cuts.cuts.Count; }
        }
        internal void AddPart(PlacedPartLayout part) => Parts.parts.Add(part);
        internal void AddCut(CutLineLayout cut)=>Cuts.cuts.Add(cut);
        public double StockWidth => sourseStock.Width;
        public double StockHeight=>sourseStock.Height;
    }
    public class PlacedPartsLayouts: IEnumerable<PlacedPartLayout>
    {
        internal List<PlacedPartLayout> parts = new();
        public PlacedPartLayout? this[int index]=>(index>=0 && index<parts.Count)?parts[index]:null;
        
        public IEnumerator<PlacedPartLayout> GetEnumerator()
        {
            return parts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class CutsLines:IEnumerable<CutLineLayout>
    {
        internal List<CutLineLayout> cuts = new();
        public CutLineLayout? this[int index]=>(index>=0 && index<cuts.Count)?cuts[index]:null;

        public IEnumerator<CutLineLayout> GetEnumerator()
        {
            return cuts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
