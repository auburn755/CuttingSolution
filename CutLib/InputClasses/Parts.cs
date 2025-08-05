namespace CutLib.InputClasses
{
    // входящий список деталей
    internal class Parts
    {
        private List<Part> parts=new List<Part>();
        public void AddPart(double height, double width, int count, bool canRotate, string name="")
        {
            Part part=new Part();
            part.Height = height;
            part.Width = width;
            part.Count = count;
            part.CanRotate = canRotate;
            part.Name = name;
            part.Placed = 0;
            part.TypeNum = 0;
            parts.Add(part);
        }
        public int Count => parts.Count;
        public void Renumber()
        {
            for (int i=0; i<parts.Count; i++)
            {
                parts[i].Reset();
                parts[i].TypeNum = i + 1;
            }
        }
        public bool HasUnplaced() => parts.Any(p => p.Placed < p.Count);

    }
}
