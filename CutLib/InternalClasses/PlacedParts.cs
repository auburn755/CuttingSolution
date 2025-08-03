using CutLib.InputClasses;
using System.Collections;

namespace CutLib.InternalClasses
{
    // представляет список отдельных деталей одного или разных типов, размещенных в полосе
    internal class PlacedParts:IEnumerable<PlacedPart>
    {
        private List <PlacedPart> placedParts = new List <PlacedPart> ();

        public PlacedPart? this[int index]=>(index>=0 && index < placedParts.Count) ? placedParts[index]:null;
        public int Count => placedParts.Count;
        public void Add(Part part, bool isRotated)
        {
            PlacedPart placedPart = new PlacedPart ();
            placedPart.PartTypeId = part.TypeId;
            placedPart.PartTypeNum = part.TypeNum;
            placedPart.IsRotated = isRotated;
            part.Placed++;                          // отметить, что еще одна деталь размещена
            placedPart.InstanceNum = part.Placed;    // присвоить данной размещенной детали дополнительный порядковый номер
        }

        public IEnumerator<PlacedPart> GetEnumerator()
        {
            return placedParts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
