using CutLib.OutputClasses;

using CutLib.InputClasses;
using CutLib.InternalClasses;
using System.Data.SqlTypes;

namespace CutLib
{
    public partial class CutEngine : ICutEngine
    {
        private SourceStocks sourceStocks = new SourceStocks();
        private Parts parts = new Parts();
        private CutSettings settings = new CutSettings();
        private SourceMaterial material = new SourceMaterial();
        public CutResult Execute()
        {
            Reset();                                                    
            Solver solver = new Solver(parts, sourceStocks, settings);
            CutTrees cutTrees = solver.Run();                           // строим деревья раскроя
            CutResult result = new CutResult();
            result.Material.Name = material.Name;                                 // передаем данные материала в результат раскроя
            foreach (Strip cutTree in cutTrees)                         // строим макеты каждого дерева раскроя и заносим их в результат
            {
                result.CuttingLayouts.Add(TranslateTreeToLayout(cutTree));
            }
            return result;
        }
        private void Reset()
        {
            if (parts.Count == 0 || !sourceStocks.HasStocks()) throw new CutLibNotDataForCuttingException("Детали или заготовки отсутствуют.");
            parts.PrepareForCutting();
            sourceStocks.PrepareForCutting();
        }
    }
}
