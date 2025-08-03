using System.Collections;

namespace CutLib.InternalClasses
{
    internal class CutTrees : IEnumerable<Strip>
    {
        private List<Strip> cutTrees = new List<Strip>();
        public int Count
        { 
            get { return cutTrees.Count; }
        }
        public Strip? this[int index]=>(index>=0 && index<cutTrees.Count)?cutTrees[index]:null;
        public void AddTree(Strip rootStrip)
        {
            cutTrees.Add(rootStrip);
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
