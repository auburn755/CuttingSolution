using CutLib.InternalClasses;

namespace CutLib.InputClasses
{
    internal class SourceStocks
    {
        private SourceStock? baseStock; //хранение базового листа
        private readonly List<SourceStock> offcuts=new();  //хранение обрезков
        private int currentOffcutIndex = 0;
        public string? Material {  get; set; }

        //  доступ к заготовкам по индексу. index == 0 - это основной лист, а index > 0 - обрезки 
        public SourceStock? this[int index]
        {
            get
            {
                if (index == 0) { if (baseStock == null) return null; else return baseStock; }
                if (index > 0 && index <= offcuts.Count)
                {
                    return offcuts[index - 1];
                }
                else return null;
            }
        }

        //  добавление основного листа материала
        public bool AddStock(double height, double width, Trim trim)
        {
            if (baseStock is null) 
            {   
                baseStock = new SourceStock();
                baseStock.Height = height;
                baseStock.Width = width;
                baseStock.Count = 0;
                //BaseStock.IsUnlimited = true;
                baseStock.Used = 0;
                baseStock.Trim = trim;    
                return true;
            }
            return false;
        }

        //  добавление обрезка материала
        public bool AddStock(double height, double width, Trim trim, int count)
        {
            SourceStock stock = new SourceStock();
            stock.Height = height;
            stock.Width = width;
            stock.Count = count;
            //stock.IsUnlimited = false;
            stock.Used = 0;
            stock.Trim = trim;
            offcuts.Add(stock);
            return true;
        }

        //  удаление заготовок по индексу. index == 0 - это основной лист, а index > 0 - обрезки
        public bool RemoveStock(int index)
        {
            if (index==0) { if (baseStock != null) { baseStock = null; return true; } else return false; }
            if (index>0 && index<=offcuts.Count)
            { offcuts.RemoveAt(index-1); return true; }
            else return false;
        }

        public void Reset()
        {
            if (baseStock !=null) baseStock.Used = 0;
            foreach (SourceStock stock in offcuts) stock.Used = 0;
        }
        public Strip? GetRootStrip()
        {
            while (currentOffcutIndex < offcuts.Count)
            {
                if (offcuts[currentOffcutIndex].Used < offcuts[currentOffcutIndex].Count)
                { 
                    offcuts[currentOffcutIndex].Used++;
                    Strip strip=new Strip();
                    strip.Size = offcuts[currentOffcutIndex].GetRootStripSize();
                    return strip;
                }
                else
                {
                    currentOffcutIndex++;
                    continue;
                }
            }
            if (baseStock != null)
            {
                baseStock.Used++;
                Strip strip = new Strip();
                strip.Size=baseStock.GetRootStripSize();
                return strip;
            }
            else
                return null;
        }
    }
}
