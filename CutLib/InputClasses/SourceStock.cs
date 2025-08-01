using CutLib.InternalClasses;

namespace CutLib.InputClasses
{
    public struct Trim
    {
        public double Left;
        public double Right;
        public double Top;
        public double Bottom;
        public Trim()
        {
            Left = 0;
            Right = 0;
            Top = 0;
            Bottom = 0;
        }
        public Trim(double left, double right, double top, double bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }
        public Trim(double trim)
        {
            Left = trim;
            Right = trim;
            Top = trim;
            Bottom = trim;
        }
    }

    public class SourceStock
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public int Count { get; set; }
        public int Used {  get; set; }
        public Trim Trim { get; set; }
        public bool IsUnlimited { get; set; }
        public StripSize GetRootStripSize()
        {
            return new StripSize(Height-Trim.Top-Trim.Bottom, Width-Trim.Left-Trim.Right);    
        }
        public override string ToString()
        {
            return $"Stock ({Width}×{Height}), usable: {GetRootStripSize().Width}×{GetRootStripSize().Height}, count: {Count}, used: {Used}";
        }
        public double Area => Height * Width;
        public double UsableArea => GetRootStripSize().Height * GetRootStripSize().Width;
    }
}
