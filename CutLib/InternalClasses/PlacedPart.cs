using CutLib.InputClasses;

namespace CutLib.InternalClasses
{
    // класс, представляющий одну размещенную деталь в полосе. По TypeId можно получить ссылку на исходный Part
    internal class PlacedPart
    {
        internal Guid PartTypeId { get; set; } // на всякий случай пусть будет
        public int PartTypeNum { get; set; } // порядковый номер типа детали во входящем списке
        public int InstanceNum { get; set; } // порядковый номер данной детали во входящем типе
        public bool IsRotated { get; set; } // как размещена деталь? в исходном виде - вертикально, или повёрнута горизонтально
    }
}
