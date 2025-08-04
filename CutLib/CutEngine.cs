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

    public class CutEngine
    {
        private SourceStocks sourceStocks = new SourceStocks();
        private Parts parts = new Parts();
        private CutSettings settings = new CutSettings();
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
            // определим положение линии разреза текущей полосы
            // четыре варианта: режем вертикально, деталь не повернута; режем вертикально, деталь повернута;
            //                  режем горизонтально, деталь не повернута; режем горизонтально, деталь повернута
            if (strip != null)
            {
                if (strip.PlacedParts.Count > 0)
                { // если детали в полосе были размещены
                    CutLineLayout cut=new();    // новая линия реза
                    int variant = 0;            // запомним вариант, как размещены детали на полосе (1-4)
                    // вычисляем положение линии разреза и транслируем в координаты макета раскроя.
                    if (strip.CutDirection == CutDirection.Vertical) 
                    {   //полоса разрезается вертикально, детали на ней размещены по вертикали
                        if (!strip.PlacedParts[0]!.IsRotated)
                        {  // детали на полосе ориентированы вертикально (не повернуты)
                            // линия реза проходит от нижнего до верхнего края полосы, на расстоянии от левого края равному ширине детали
                            cut.X1 = x + strip.PlacedParts[0]!.Part.Width;
                            cut.Y1 = y;
                            cut.X2 = cut.X1;
                            cut.Y2 = y + strip.Size.Height;
                            layout.AddCut(cut);
                            variant = 1;
                        }
                        else
                        {  // детали на полосе ориентированы горизонтально (повернуты)
                            // линия реза проходит от нижнего до верхнего края полосы, на расстоянии от левого края равному высоте детали
                            cut.X1 = x + strip.PlacedParts[0]!.Part.Height;
                            cut.Y1 = y;
                            cut.X2 = cut.X1;
                            cut.Y2=y + strip.Size.Height;
                            layout.AddCut(cut);
                            variant = 2;
                        }
                    }
                    else
                    {   //полоса разрезается горизонтально, детали на ней размещены по горизонтали
                        if (!strip.PlacedParts[0]!.IsRotated)
                        {  // детали на полосе ориентированы вертикально (не повернуты)
                            // линия реза проходит от левого до правого края полосы, на расстоянии от нижнего края равному высоте детали
                            cut.X1 = x;
                            cut.Y1 = y + strip.PlacedParts[0]!.Part.Height;
                            cut.X2 = x + strip.Size.Width;
                            cut.Y2 = cut.Y1;
                            layout.AddCut(cut);
                            variant = 3;
                        }
                        else
                        {  // детали на полосе ориентированы горизонтально (повернуты)
                            // линия реза проходит от левого до правого края полосы, на расстоянии от нижнего края равному ширине детали
                            cut.X1 = x;
                            cut.Y1 = y + strip.PlacedParts[0]!.Part.Width;
                            cut.X2 = x + strip.Size.Width;
                            cut.Y2 = cut.Y1;
                            layout.AddCut(cut);
                            variant = 4;
                        }
                    }

                    // перебираем размещенные детали и транслируем их координаты в координаты макета раскроя с учетом поворота детали
                    // и сразу создаем линию отреза детали от полосы и так же транлируем ее координаты в координаты макета раскроя
                    double nextX = x;
                    double nextY = y;
                    foreach (PlacedPart placedPart in strip.PlacedParts)
                    { // по какому из 4-х вариантов были размещены детали?
                        PlacedPartLayout partLayout;
                        switch (variant)
                        {
                            case 1: // полоса вертикально, детали вертикально Height x Width, координата nextY для линии реза увеличивается на part.Height,
                                    // а для следующей детали еще на SawWidth
                                
                                // создаем макет детали и заносим в список макета раскроя
                                partLayout = new(placedPart.Part);
                                partLayout.InstanceNum = placedPart.InstanceNum;
                                partLayout.X = nextX;
                                partLayout.Y = nextY;
                                nextY=nextY + partLayout.Height;
                                layout.AddPart(partLayout);
                                // создаем макет линии реза, который отрезает эту деталь от полосы
                                cut = new CutLineLayout();
                                cut.X1 = nextX;
                                cut.Y1 = nextY;
                                cut.X2= nextX+strip.Size.Width;
                                cut.Y2 = nextY;
                                layout.AddCut(cut);
                                // подготавливаем nextY для следующей детали
                                nextY = nextY + settings.SawWidth;
                            break;  

                            case 2: // полоса вертикально, детали горизонтально Width x Height, координата nextY для линии реза увеличивается на part.Width,
                                    // а для следующей детали еще на SawWidth

                                // создаем макет детали и заносим в список макета раскроя
                                partLayout = new(placedPart.Part);
                                partLayout.InstanceNum = placedPart.InstanceNum;
                                partLayout.X = nextX;
                                partLayout.Y = nextY;
                                nextY = nextY + partLayout.Height;
                                layout.AddPart(partLayout);
                                // создаем макет линии реза, который отрезает эту деталь от полосы
                                cut = new CutLineLayout();
                                cut.X1 = nextX;
                                cut.Y1 = nextY;
                                cut.X2 = nextX + strip.Size.Width;
                                cut.Y2 = nextY;
                                layout.AddCut(cut);
                                // подготавливаем nextY для следующей детали
                                nextY = nextY + settings.SawWidth;
                                break;

                            case 3: // полоса горизонтально, детали вертикально Height x Width, координата x увеличивается на part.Width+SawWidth

                            break;

                            case 4: // полоса горизонтально, детали горизонтально Width x Height, координата x увеличивается на part.Height+SawWidth

                            break;

                            default:
                            break;
                        }
                    }
                }
                else
                { // если детали в полосе не были размещены, значит это деловой остаток или отход. Детали и линии реза не "рисуем"

                }
            }

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
