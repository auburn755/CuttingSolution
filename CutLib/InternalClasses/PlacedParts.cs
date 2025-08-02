using CutLib.InputClasses;

namespace CutLib.InternalClasses
{
    // представляет список отдельных деталей одного типа или разных типов, размещенных в полосе
    internal class PlacedParts
    {
        private List <PlacedPart> placedParts = new List <PlacedPart> ();
        public void Add(Part part, bool isRotated)
        {
            PlacedPart placedPart = new PlacedPart ();
            placedPart.PartId = part.Id;
            placedPart.IsRotated = isRotated;
            part.Placed++;                          // отметить, что еще одна деталь размещена
            placedPart.InstanceId = part.Placed;    // присвоить данной размещенной детали дополнительный порядковый номер
        }
    }
}
