using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.InputClasses
{
    internal class SourceStocks
    {
        List<SourceStock> sourceStocks=new();
        public string? Material {  get; set; }
        public SourceStock? this[int index]=>(index >=0 && index < sourceStocks.Count) ? sourceStocks[index]:null;
        public bool AddStock(double height, double width, Trim trim)
        {
            if (!IsBaseStockExists())
            {
                SourceStock stock = new SourceStock();
                stock.Height = height;
                stock.Width = width;
                stock.Count = -1;
                stock.Used = 0;
                stock.Trim = trim;    
                sourceStocks.Add(stock);
                return true;
            }
            return false;
        }
        public bool AddStock(double height, double width, Trim trim, int count)
        {
            SourceStock stock = new SourceStock();
            stock.Height = height;
            stock.Width = width;
            stock.Count = count;
            stock.Used = 0;
            stock.Trim = trim;
            sourceStocks.Add(stock);
            return true;
        }
        private bool IsBaseStockExists()
        {
            foreach(SourceStock stock in sourceStocks) if (stock.Count==-1) return true;
            return false;
        }
        public bool RemoveStock(int index)
        {
            if(index>=0  && index<sourceStocks.Count)
            {
                sourceStocks.RemoveAt(index);
                return true;
            }
            else return false;
        }
        public StripSize GetNextStripSize()
        {
            /*
             * метод должен возвращать размер очередной заготовки для раскроя.
             * варианты наличия заготовок: 
             *          только базовый размер (т.е. целые листы);
             *          один базовый размер и обрезки, в этом случае отдаются сначала обрезки, а потом целые листы
             *          только обрезки.
             */
        }
    }
}
