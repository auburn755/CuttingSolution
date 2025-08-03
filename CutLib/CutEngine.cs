using CutLib.OutputClasses;

using CutLib.InputClasses;
using CutLib.InternalClasses;

namespace CutLib
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

    public class CutEngine : ICutEngine
    {
        private SourceStocks sourceStocks = new SourceStocks();
        private Parts parts = new Parts();
        private CutSettings СutSetting = new CutSettings();

        public void AddPart(double height, double width, int count, bool canRotate = true, string name = "")
        {
            parts.AddPart(height, width, count, canRotate, name);
            parts.Renumber();
        }

        public void AddStock(double height, double width, Trim trim, int count = 0)
        {   
            if (count == 0) sourceStocks.AddStock(height, width, trim); else sourceStocks.AddStock(width, height, trim, count);
        }
        public void SetMaxCutLength(double length)
        {
            СutSetting.MaxCutLength = length;
        }

        public void SetMaxSheetRotation(int count)
        {
            СutSetting.MaxSheetRotation = count;
        }

        public void SetMinWasteLength(double length)
        {
            СutSetting.MinWasteLength = length;
        }

        public void SetSawWidth(double width)
        {
            СutSetting.SawWidth = width;
        }        
        public CutResult Execute()
        {
            Solver solver =new Solver(parts, sourceStocks);
            CutTrees cutTrees = solver.Run();
            CutResult result = new CutResult() { Material = sourceStocks.Material };
            foreach (Strip rootStrip in cutTrees)
            {
                result.CuttingLayouts.Add(ConvertStripToLayout(rootStrip));
            }
            return result;
        }

        //транслировать дерево раскроя в координатный макет раскроя заготовки
        private CuttingLayout TranslateTreeToLayout(Strip rootStrip) 
        {
            CuttingLayout layout = new CuttingLayout();
            layout.SourseStock = rootStrip.SourceStock!;
            //координаты корневой полосы на заготовке - это величина подрезки слева и снизу
            double x = rootStrip.SourceStock!.Trim.Left;
            double y = rootStrip.SourceStock!.Trim.Bottom;
            TranslateStripToLayout(rootStrip, layout, x, y);
            return layout;
        }

        // транслировать полосу в координатный макет раскроя заготовки. полоса расположена по координатам x,y
        private void TranslateStripToLayout(Strip strip, CuttingLayout layout, double x, double y)
        {
            // есть размер полосы, есть начальные координаты полосы, есть направление полосы, значит можно вычислить CutLine
            // определяем линию реза полосы
            CutLineLayout cut;

        }
        





        private void AddPlacedPartsRecursive(Strip strip, CuttingLayout layout)
        {
            // здесь надо вычислить координаты полосы на заготовке
            foreach (PlacedPart placedPart in strip.PlacedParts)
            {
                layout.AddPart(new PlacedPartLayout
                {
                   //надо вычислить координаты детали на полосе, и транслировать в координаты заготовки
                   //заполнить остальные свойства
                });
            }
            if (strip.LeftStrip != null) AddPlacedPartsRecursive(strip.LeftStrip, layout);
            if (strip.RightStrip != null) AddPlacedPartsRecursive (strip.RightStrip, layout);
        }
    }
}
