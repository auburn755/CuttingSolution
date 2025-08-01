namespace CutLib.InternalClasses
{
    public struct StripSize
    {
        public double Height;
        public double Width;
        public StripSize()
        {
            Height = 0;
            Width = 0;
        }
        public StripSize(double height, double width)
        {
            Height = height;
            Width = width;
        }
    }

    internal class Strip
    {
        public StripSize Size {  get; set; }
    }
}
