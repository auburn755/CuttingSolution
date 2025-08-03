using System.Collections;

namespace CutLib.InternalClasses
{
    internal class CutTrees : IEnumerable<Strip>
    {
        private List<Strip> strips = new List<Strip>();
        public int Count
        { 
            get { return strips.Count; }
        }
        public Strip? this[int index]=>(index>=0 && index<strips.Count)?strips[index]:null;
        public void AddRootStrip(Strip rootStrip)
        {
            strips.Add(rootStrip);
        }
        public IEnumerator<Strip> GetEnumerator()
        {
            return strips.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
