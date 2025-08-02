namespace CutLib.InputClasses
{
    internal class CutSetting
    {
        public double SawWidth { get; set; }        //ширина линии реза
        public double MaxCutLength { get; set; }    // максимально возможная длина разреза
        public int MaxSheetRotation { get; set; } = 0; // максимальное число поворотов листа при раскрое на полосы, если 0, то неограничено
        public double MinWasteLength { get; set; } = 0; // минимальный размер делового остатка
    }
}
