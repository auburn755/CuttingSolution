namespace CutLib.InputClasses
{
    // входящий список деталей
    internal class Parts
    {
        private List<Part> parts=new List<Part>();
        public void AddPart(int height, int width, int count, bool canRotate, string name="")
        {
            Part part=new Part();
            part.Height = height;
            part.Width = width;
            part.Count = count;
            part.CanRotate = canRotate;
            part.Name = name;
            part.Placed = 0;
            part.TypeId = 0;
            parts.Add(part);
        }
        public void Renumber()
        {
            for (int i=0; i<parts.Count; i++)
            {
                parts[i].Reset();
                parts[i].TypeId = i + 1;
            }
        }
    }
}
