using System.Collections;
using System.ComponentModel;

namespace CutLib.InputClasses
{
    // входящий список деталей
    internal class Parts:IEnumerable<Part>
    {
        private readonly List<Part> parts=new();

        public Part this[int index]
        {
            get
            {
                if (index < 0 || index >= parts.Count) throw new InvalidIndexRangeException("Неверный индекс Parts");
                else return parts[index];
            }
        }
        public void Add(Part part) { this.parts.Add(part); }
        public int Count => parts.Count;
        public void Reset()
        {
            foreach (Part part in parts) part.Placed = 0;
        }
        public bool HasUnplaced() => parts.Any(p => p.Placed < p.Count);

        public IEnumerator<Part> GetEnumerator()
        {
            return parts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
