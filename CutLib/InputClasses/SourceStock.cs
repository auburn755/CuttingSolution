using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class SourceStock
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public int Count { get; set; }
        public int Used {  get; set; }
        public Trim Trim { get; set; }
        public StripSize GetUsableSize()
        {
            return new StripSize(Height-Trim.Top-Trim.Bottom, Width-Trim.Left-Trim.Right);    
        }
    }
}
