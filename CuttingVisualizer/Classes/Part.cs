namespace CuttingVisualizer.Classes
{
    public class Part
    {
        public Part(double height, double width, double x, double y)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;
        }

        public double Height { get; set; }
        public double Width { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
