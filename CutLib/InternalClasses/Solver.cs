using CutLib.InputClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.InternalClasses
{
    internal class Solver
    {
        private Parts parts;
        private SourceStocks stocks;
        private CutTrees cutTrees = new();
        public Solver (Parts parts, SourceStocks stocks)
        {
            this.parts = parts;
            this.stocks = stocks;
        }
        public CutTrees Run()               // рассчитывает раскрой и строит деревья для каждой заготовки
        {
            parts.Reset();               // перенумеровать входящие детали и сбросить счетчики размещенных деталей
            stocks.Reset();                 // сбросить счетчики использованных заготовок
            while (parts.HasUnplaced())     // пока есть неразмещенные детали
            {
                var rootStrip=stocks.GetRootStrip();            // создаем новую корневую полосу для построения дерева раскроя 
                if (rootStrip == null) throw new Exception("Недостаточно заготовок для всех деталей");
                BuildBranches(rootStrip);                       // запускаем рекурсию для построения дерева раскроя, начиная с корневой полосы rootStrip
                cutTrees.AddTree(rootStrip);                    // сохраняем построенное дерево в список деревьев
            }                                                   // продолжаем цикл, пока не закончатся детали или обрезки, если нет базового листа
            return new CutTrees();
        }
        private void BuildBranches(Strip strip)
        {
            // тут должно быть:
            // strip.PlacedParts.Add(part, false); столько, сколько подходящих деталей
            // 
            // тут будет сам рекурсивный алгоритм
        }
    }
}
