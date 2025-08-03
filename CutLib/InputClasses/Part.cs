namespace CutLib.InputClasses
{   /*
     *  класс для хранения данных об одном типе деталей
     */
    internal class Part
    {
        public Guid TypeId { get; set; } = Guid.NewGuid();    // уникальный идентификатор типа детали
        public int TypeNum { get; set; } // порядковый номер типа детали во входящем списке, счет начинается от 1
        public string? Name { get; set; }   // название, или обозначение детали на чертежах
        public double Height { get; set; }
        public double Width { get; set; }
        public int Placed { get; set; } // сколько деталей уже размещено в раскрое
        public int Count { get; set; }
        public bool CanRotate { get; set; }

        // подготовка к повторному раскрою или при нумерации входящего списка деталей
        internal void Reset()
        {
            TypeNum = 0;
            Placed = 0;
        } 
    }
}
