using CutLib.InputClasses;

namespace CutLib.InternalClasses
{
    // класс, представляющий одну размещенную деталь в полосе. 
    internal class PlacedPart
    {
        internal Part? Part { get; set; }            // ссылка на родительский тип детали
        public int InstanceNum { get; set; }        // порядковый номер данной детали в родительском типе
        public bool IsRotated { get; set; }         // как размещена деталь? в исходном виде - вертикально, или повёрнута горизонтально
    }
}
