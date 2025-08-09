using CutLib.InputClasses;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CutLib.OutputClasses
{
    // макет карты раскроя, содержит детали и линии разреза
    public class CuttingLayout
    {
        internal SourceStock? sourseStock { get; set; }      // ссылка на исходную заготовку
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
        public double StockWidth => sourseStock!.Width;
        public double StockHeight=>sourseStock!.Height;
        public double LengthAllCuts
        {
            get
            {
                double length=0;
                foreach (CutLineLayout cut in Cuts.cuts) { length += cut.Length; }
                return length;
            }
        }
    }
    public class PlacedPartsLayouts: IEnumerable<PlacedPartLayout>
    {
        internal List<PlacedPartLayout> parts = new();
        public PlacedPartLayout this[int index]
        {
            get
            {
                if (index < 0 || index >= parts.Count) throw new CutLibInvalidIndexRangeException("Неверный индекс Parts.");
                return parts[index];
            }
        }
        
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
        public CutLineLayout? this[int index]  //=>(index>=0 && index<cuts.Count)?cuts[index]:null;
        {
            get
            {
                if (index < 0 || index >= cuts.Count) throw new CutLibInvalidIndexRangeException("Неверный индекс Cuts.");
                return cuts[index];
            }
        }

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
