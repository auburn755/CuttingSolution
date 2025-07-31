namespace CuttingVisualizer.Classes
{
    public class Stocks
    {
        private readonly List<Stock> stocks;
        private int _maxStockHeight;
        private int _maxStockWidth;

        public Stocks()
        {
            //заполнить список для тестирования
            stocks = new List<Stock>();
            FillForTesting();
            UpdateMaxStockSizes();
        }

        public Stock? this[int index]
        {
            get
            {
                if (stocks.Count > 0)
                    if (index >= 0 && index < stocks.Count) return stocks[index]; else return null;
                else return null;
            }
        }

        public int Count { get { return stocks.Count; } }

        public int MaxStockHeight { get => _maxStockHeight; }

        public int MaxStockWidth { get => _maxStockWidth; }

        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            UpdateMaxStockSizes();
        }

        private void UpdateMaxStockSizes()
        {
            _maxStockHeight = (int)stocks.Max(s => s.Height);
            _maxStockWidth = (int)stocks.Max(s => s.Width);
        }
        private void FillForTesting()
        {
            stocks.Clear();
            Stock stock = new Stock(2750, 1830);
            stock.AddPart(new(800, 450, 10, 10));
            stock.AddPart(new(800, 450, 10, 814));
            stock.AddPart(new(800, 450, 10, 1618));
            stock.AddPart(new(800, 450, 464, 10));
            stocks.Add(stock);
            stock = new Stock(2750, 1830);
            stock.AddPart(new(400, 1150, 10, 10));
            stock.AddPart(new(400, 1150, 10, 414));
            stock.AddPart(new(400, 1150, 10, 818));
            stock.AddPart(new(400, 550, 1164, 10));
            stock.AddPart(new(400, 550, 1164, 414));
            stock.AddPart(new(400, 550, 1164, 818));
            stocks.Add(stock);
            stock = new Stock(1045, 1500);
            stock.AddPart(new(150, 1200, 10, 10));
            stock.AddPart(new(250, 850, 10, 164));
            stock.AddPart(new(200, 600, 10, 418));
            stocks.Add(stock);
        }

    }
}
