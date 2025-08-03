using CutLib.InputClasses;

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
    public enum StripDirection
    {
        Vertical,
        Horizontal
    }

    internal class Strip
    {
        internal SourceStock? SourceStock { get; set; } = null; //ссылка на исходную заготовку, пусть будет только в rootStrip, а ниже по дереву null
        public StripSize Size {  get; set; }
        public Strip? LeftStrip { get; set; } = null;
        public Strip? RightStrip { get; set; } = null;
        public StripDirection Direction { get; set; }
        public PlacedParts? PlacedParts { get; set; } = null;
    }
}
