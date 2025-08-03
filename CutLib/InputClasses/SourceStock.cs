using CutLib.InternalClasses;

namespace CutLib.InputClasses
{
    

    internal class SourceStock
    {
        public Guid Id=Guid.NewGuid();
        public double Height { get; set; }
        public double Width { get; set; }
        public int Count { get; set; }
        public int Used {  get; set; }
        public Trim Trim { get; set; }
        public bool CanRotate { get; set; }
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
