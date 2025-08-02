namespace CutLib.InternalClasses
{
    // класс, представляющий одну размещенную деталь в полосе
    internal class PlacedPart
    {
        public Guid PartId { get; set; }        // GUID типа детали
        public int PartTypeId { get; set; } // порядковый номер типа детали во входящем списке
        public int InstanceId { get; set; } // порядковый номер данной детали во входящем типе
        public bool IsRotated { get; set; } // как размещена деталь? в исходном виде - вертикально, или повёрнута горизонтально
    }
}
