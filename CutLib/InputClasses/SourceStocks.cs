using CutLib.InternalClasses;
using System.Collections;

namespace CutLib.InputClasses
{
    // надо сделать внутренний список Offcuts и не заморачиваться с общим списком с базовым листом
    // материал заменить на класс материал
    internal class SourceStocks
    {
        private List<SourceStock>? offcuts;
        private SourceStock? baseSourceStock;
        private int currentOffcutIndex = 0;                 // индекс текущего обрезка, выбранного для раскроя
        internal SourceMaterial? Material {  get; set; }
        public void SetBaseStock(double height, double width, Trim trim)
        {
                if (baseSourceStock == null) baseSourceStock = new SourceStock();
                baseSourceStock.Height = height;
                baseSourceStock.Width = width;
                baseSourceStock.Trim = trim;
        }
        public void AddOffcut(double height, double width, Trim trim, int count)
        {
            if (offcuts == null) offcuts = new List<SourceStock>();
            SourceStock offcut = new();
            offcut.Height = height;
            offcut.Width = width;
            offcut.Trim = trim;
            offcut.Count = count;
            offcuts.Add(offcut);
        }
        public void SetMaterial(string name)
        {
            if (Material == null) Material = new SourceMaterial();
            Material.Name = name;
        }
        public void PrepareForCutting()
        {
            if (offcuts is null && baseSourceStock is null) throw new CutLibInvalidStocksException("Объекты Stocks не заданы.");
            if (baseSourceStock != null) baseSourceStock.Used = 0;
            if (offcuts != null)
                foreach (SourceStock offcut in offcuts) offcut.Used = 0;
            currentOffcutIndex = 0;
        }
        public Strip? GetRootStrip()
        {
            if (baseSourceStock is null && offcuts is null) throw new CutLibInvalidStocksException("Объекты Stocks не заданы.");
            if (offcuts != null)
            {
                while (currentOffcutIndex < offcuts.Count)
                {
                    if (offcuts[currentOffcutIndex].Used < offcuts[currentOffcutIndex].Count)
                    {
                        offcuts[currentOffcutIndex].Used++;
                        Strip strip = new Strip();
                        strip.Size = offcuts[currentOffcutIndex].GetRootStripSize();
                        strip.SourceStock = offcuts[currentOffcutIndex];
                        return strip;
                    }
                    else
                    {
                        currentOffcutIndex++;
                        continue;
                    }
                }
            }
            if (baseSourceStock != null)
            {
                baseSourceStock.Used++;
                Strip strip = new Strip();
                strip.Size=baseSourceStock.GetRootStripSize();
                strip.SourceStock = baseSourceStock;
                return strip;
            }
            else
                return null;
        }
        // функция сравнения размера обрезка с базовым листом
        public bool IsSizeOffcutExceeded(double height, double width)
        {
            if (baseSourceStock != null)
            {
                if (height > baseSourceStock.Height || width > baseSourceStock.Width) return true; else return false;
            }
            else
            {
                return false;
            }
        }
    }
}
