using System.Collections;
using System.ComponentModel;

namespace CutLib.InputClasses
{
    // входящий список деталей
    internal class Parts:IEnumerable<Part>
    {
        private List<Part>? parts;

        public Part this[int index]
        {
            get
            {
                if (parts == null) throw new CutLibInvalidPartsException("Список Parts не задан.");
                if (index < 0 || index >= parts.Count) throw new CutLibInvalidIndexRangeException("Неверный индекс Part");
                else return parts[index];
            }
        }
        public void Add(Part part) 
        { 
            if (parts == null) parts=new List<Part>();
            parts.Add(part); 
        }
        public int Count
        {
            get
            {
                if (parts == null) throw new CutLibInvalidPartsException("Список Parts не задан.");
                return parts.Count;
            }
        }
        public void PrepareForCutting()
        {
            if (parts == null) throw new CutLibInvalidPartsException("Список Parts не задан.");
            foreach (Part part in parts) part.Placed = 0;
        }
        public bool HasUnplaced() 
        {
            if (parts == null) throw new CutLibInvalidPartsException("Список Parts не задан.");
            return parts.Any(p => p.Placed < p.Count);
        }

        public IEnumerator<Part> GetEnumerator()
        {
            if (parts == null) throw new CutLibInvalidPartsException("Список Parts не задан.");
            return parts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
