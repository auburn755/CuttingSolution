namespace CutLib.InputClasses
{
    internal class CutSettings
    {
        public double SawWidth { get; set; } = 0;        //ширина линии реза, возможен 0 (а вдруг для раскроя стекла применять)
        public double MaxCutLength { get; set; } = 0;    // максимально возможная длина разреза, если 0, то длина реза не проверяется
        public int MaxSheetRotation { get; set; } = 0; // максимальное число поворотов листа при раскрое на полосы, если 0, то неограничено
        public double MinWasteLength { get; set; } = 0; // минимальный размер делового остатка, если 0, то деловые остатки не маркируются
    }
}
