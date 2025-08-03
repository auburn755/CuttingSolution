using CutLib.InputClasses;
using CutLib.InternalClasses;
using CutLib.OutputClasses;

namespace CutLib
{
    public interface ICutEngine
    {
        // добавление нового типа детали
        void AddPart(double height, double width, int count, bool canRotate = true, string name = "");
        // добавление новой заготовки. count - необязательный параметр, если опустить, то это целый лист
        void AddStock(double height, double width, Trim trim, int count = 0);

        void SetSawWidth(double width);
        void SetMaxCutLength(double length);
        void SetMaxSheetRotation(int count);
        void SetMinWasteLength(double length);
        CutResult Execute();
    }
}
