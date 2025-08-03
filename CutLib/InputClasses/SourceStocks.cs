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
        public void AddStock(double height, double width, Trim trim)
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
            }
        }

        //  добавление обрезка материала
        public void AddStock(double height, double width, Trim trim, int count)
        {
            SourceStock stock = new SourceStock();
            stock.Height = height;
            stock.Width = width;
            stock.Count = count;
            //stock.IsUnlimited = false;
            stock.Used = 0;
            stock.Trim = trim;
            offcuts.Add(stock);
        }

        //  удаление заготовки из списка обрезков по Id
        public void RemoveOffcut(Guid id)
        {
            var stock=offcuts.Find(x => x.Id == id);
            if (stock != null) offcuts.Remove(stock);
        }
        // удаление базового листа
        public void RemoveBase()=>baseStock=null;

        // подготовка к новому раскрою
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
                    strip.SourceStock=offcuts[currentOffcutIndex];
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
                strip.SourceStock = baseStock;
                return strip;
            }
            else
                return null;
        }
    }
}
