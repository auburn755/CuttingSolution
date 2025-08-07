using CutLib.InputClasses;

namespace CutLib.InternalClasses
{

    internal class Strip
    {
        internal SourceStock? SourceStock { get; set; } = null; //ссылка на исходную заготовку, пусть будет только в rootStrip, а ниже по дереву null
        public StripSize Size {  get; set; }
        public Strip? LeftStrip { get; set; } = null;
        public Strip? RightStrip { get; set; } = null;
        public CutDirection CutDirection { get; set; }
        public PlacedParts PlacedParts { get; set; } = new();
    }
}
