using CutLib.OutputClasses;

using CutLib.InputClasses;
using CutLib.InternalClasses;
using System.Data.SqlTypes;

namespace CutLib
{
    public partial class CutEngine
    {
        private SourceStocks sourceStocks = new SourceStocks();
        private Parts parts = new Parts();
        private CutSettings settings = new CutSettings();
        public CutResult Execute()
        {
            Solver solver =new Solver(parts, sourceStocks);
            CutTrees cutTrees = solver.Run();
            CutResult result = new CutResult() { Material = sourceStocks.Material };
            foreach (Strip rootStrip in cutTrees)
            {
                result.CuttingLayouts.Add(TranslateTreeToLayout(rootStrip));
            }
            return result;
        }
    }
}
