using CutLib.InputClasses;

namespace CutLib.OutputClasses
{
    // класс представляет деталь, размещенную на макете раскроя
    public class PlacedPartLayout
    {
        internal PlacedPartLayout(Part part)
        {
            this.part = part;
            Name = part.Name;
            Width = part.Width;
            Height = part.Height;
            TypeNum = part.TypeNum;
        }
        internal Part part { get; set; }
        public string? Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int TypeNum { get; set; }
        public int InstanceNum { get; set; }
    }
}
