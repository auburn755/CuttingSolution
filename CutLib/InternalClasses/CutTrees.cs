using System.Collections;
using System.ComponentModel.Design;

namespace CutLib.InternalClasses
{
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
