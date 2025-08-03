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
        public CutTrees Run()
        {
            parts.Renumber();
            stocks.Reset();
            while (parts.HasUnplaced())
            {
                var rootStrip=stocks.GetRootStrip();
                if (rootStrip == null) throw new Exception("Недостаточно заготовок для всех деталей");
                BuildBranches(rootStrip);
                cutTrees.AddRootStrip(rootStrip);
            }
            return new CutTrees();
        }
        private void BuildBranches(Strip rootStrip)
        {
            // тут будет сам рекурсивный алгоритм
        }
    }
}
