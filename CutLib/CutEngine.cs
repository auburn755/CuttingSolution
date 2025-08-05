using CutLib.OutputClasses;

using CutLib.InputClasses;
using CutLib.InternalClasses;
using System.Data.SqlTypes;

namespace CutLib
{
    public struct Trim
    {
        public double Left;
        public double Right;
        public double Top;
        public double Bottom;
        public Trim()
        {
            Left = 0;
            Right = 0;
            Top = 0;
            Bottom = 0;
        }
        public Trim(double left, double right, double top, double bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }
        public Trim(double trim)
        {
            Left = trim;
            Right = trim;
            Top = trim;
            Bottom = trim;
        }
    }

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
