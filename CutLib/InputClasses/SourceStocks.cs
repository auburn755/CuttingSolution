using CutLib.DTO;
using CutLib.InternalClasses;
using System.Collections;

namespace CutLib.InputClasses
{
    // надо сделать внутренний список Offcuts и не заморачиваться с общим списком с базовым листом
    // материал заменить на класс материал
    internal class SourceStocks
    {
        private List<SourceStock>? offcuts;     // список обрезков для раскроя
        private SourceStock? baseSourceStock;   // базовый лист
        private int currentOffcutIndex = 0;                 // индекс текущего обрезка, выбранного для раскроя
        public void SetBaseStock(double height, double width, Trim trim)    // задать базовый размер листа
        {
                if (baseSourceStock == null) baseSourceStock = new SourceStock();
                baseSourceStock.Height = height;
                baseSourceStock.Width = width;
                baseSourceStock.Trim = trim;
        }
        public void AddOffcut(double height, double width, Trim trim, int count)    // добавить обрезок
        {
            if (offcuts == null) offcuts = new List<SourceStock>();
            SourceStock offcut = new();
            offcut.Height = height;
            offcut.Width = width;
            offcut.Trim = trim;
            offcut.Count = count;
            offcuts.Add(offcut);
        }
        
        public void PrepareForCutting()     // подготовиться к очередному раскрою - обнулить счетчики использованных заготовок
        {
            if (baseSourceStock != null) baseSourceStock.Used = 0;
            if (offcuts != null)
                foreach (SourceStock offcut in offcuts) offcut.Used = 0;
            currentOffcutIndex = 0;
        }

        public bool HasStocks()     // проверить, что исходные заготовки заданы
        {
            if (baseSourceStock != null) return true;

            else if (offcuts != null && offcuts.Count > 0) return true;

            return false;
        }
        public Strip? GetRootStrip()    // выдать очередную заготовку для раскроя. Null может быть, только если для раскроя заданы только обрезки и они закончились
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
        
        public bool IsSizeOffcutExceeded(double height, double width)   // сравнить размер обрезка height*width с размером базового листа
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
