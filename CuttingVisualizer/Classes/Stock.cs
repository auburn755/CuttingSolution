namespace CuttingVisualizer.Classes
{
    public class Stock
    {
        private readonly List<Part> parts;
        public Stock(double height, double width)
        {
            parts = new List<Part>();
            Height = height;
            Width = width;
        }
        public Part? this[int index] => (index >= 0 && index < parts.Count) ? parts[index] : null;
        /*{
            get
            {
                if (parts.Count > 0)
                    if (index >= 0 && index < parts.Count) return parts[index]; else return null;
                else return null;
            }
        }*/
        public double Height { get; set; }
        public double Width { get; set; }
        public int Count { get { return parts.Count; } }
        public void AddPart(Part part) { parts.Add(part); }
    }
}
