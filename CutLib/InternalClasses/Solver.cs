using CutLib.InputClasses;

namespace CutLib.InternalClasses
{
    // Решатель задачи раскроя. По входящим спискам Parts и SourceStocks, строит набор CutTrees - деревья раскроя.
    internal class Solver
    {
        private Parts parts;
        private SourceStocks stocks;
        private CutSettings settings;
        private CutTrees cutTrees = new();
        public Solver (Parts parts, SourceStocks stocks, CutSettings settings)
        {
            this.parts = parts;
            this.stocks = stocks;
            this.settings = settings;
        }
        public CutTrees Run()               // выполнить расчет
        {
            parts.Reset();                  // сбросить счетчики размещенных деталей
            stocks.Reset();                 // сбросить счетчики использованных заготовок
            while (parts.HasUnplaced())     // пока есть неразмещенные детали
            {
                var rootStrip=stocks.GetRootStrip();            // берем для раскроя очередную заготовку 
                if (rootStrip == null) throw new CuttingNotCompletedException("Не удалось разместить все детали на представленных заготовках."); // надо сделать, чтобы пользователю был представлен список неразмещенных деталей
                BuildBranches(rootStrip);                       // строим для заготовки дерево раскроя, начиная с ее корневой полосы rootStrip
                cutTrees.AddTree(rootStrip);                    // сохраняем построенное дерево в список деревьев
            }                                                   // продолжаем цикл, пока не закончатся детали или обрезки, если нет базового листа
            return cutTrees;
        }
        private void BuildBranches(Strip strip)
        {
            // тут будет сам рекурсивный алгоритм
        }
    }
}
