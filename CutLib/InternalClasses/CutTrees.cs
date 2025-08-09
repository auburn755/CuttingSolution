using System.Collections;
using System.ComponentModel.Design;

namespace CutLib.InternalClasses
{
    // класс содержит результат раскроя в виде деревьев для каждой заготовки
    // дерево представляет объект Strip - корневая полоса исходной заготовки
    // в корневой полосе содержится ссылка на исходную заготовку
    internal class CutTrees : IEnumerable<Strip>
    {
        private List<Strip> cutTrees = new List<Strip>();
        public int Count
        {
            get { return cutTrees.Count; }
        }
        public Strip this[int index]
        {
            get
            { 
                if (index< 0 || index >= cutTrees.Count) throw new CutLibInvalidIndexRangeException("Неверный индекс для CutTrees");
                else return cutTrees[index];
            }
        }
         
        public void AddTree(Strip cutTree)
        {
            cutTrees.Add(cutTree);
        }
        public IEnumerator<Strip> GetEnumerator()
        {
            return cutTrees.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
