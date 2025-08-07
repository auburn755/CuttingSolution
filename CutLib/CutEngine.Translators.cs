using CutLib.InternalClasses;
using CutLib.OutputClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutLib
{
    // методы преобразования деревьев раскроя в координатные макеты раскроя
    public partial class CutEngine
    {
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
                CutLineLayout cut = new();    // новая линия реза
                int variant;            // запомним сюда вариант, как размещены детали на полосе (1-4)
                double nextX;
                double nextY;
                if (strip.PlacedParts.Count > 0)
                { // если детали в полосе были размещены
                    // вычисляем положение линии разреза и транслируем в координаты макета раскроя.
                    if (strip.CutDirection == CutDirection.Vertical)
                    {   //полоса разрезается вертикально, детали на ней размещены снизу вверх, по вертикали
                        if (!strip.PlacedParts[0]!.IsRotated)
                        {  // детали на полосе ориентированы вертикально (не повернуты)
                            // линия реза проходит от нижнего до верхнего края полосы, на расстоянии равным ширине детали от левого края полосы
                            cut.X1 = x + strip.PlacedParts[0]!.Part.Width;
                            cut.Y1 = y;
                            cut.X2 = cut.X1;
                            cut.Y2 = y + strip.Size.Height;
                            layout.AddCut(cut);
                            variant = 1;
                        }
                        else
                        {  // детали на полосе ориентированы горизонтально (повернуты)
                            // линия реза проходит от нижнего до верхнего края полосы, на расстоянии равным высоте детали от левого края полосы
                            cut.X1 = x + strip.PlacedParts[0]!.Part.Height;
                            cut.Y1 = y;
                            cut.X2 = cut.X1;
                            cut.Y2 = y + strip.Size.Height;
                            layout.AddCut(cut);
                            variant = 2;
                        }
                    }
                    else
                    {   //полоса разрезается горизонтально, детали на ней размещены слева направо, по горизонтали
                        if (!strip.PlacedParts[0]!.IsRotated)
                        {  // детали на полосе ориентированы вертикально (не повернуты)
                            // линия реза проходит от левого до правого края полосы, на расстоянии равным высоте детали от нижнего края полосы
                            cut.X1 = x;
                            cut.Y1 = y + strip.PlacedParts[0]!.Part.Height;
                            cut.X2 = x + strip.Size.Width;
                            cut.Y2 = cut.Y1;
                            layout.AddCut(cut);
                            variant = 3;
                        }
                        else
                        {  // детали на полосе ориентированы горизонтально (повернуты)
                            // линия реза проходит от левого до правого края полосы, на расстоянии равным ширине детали от нижнего края полоы
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
                    nextX = x;
                    nextY = y;
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
                                nextY = nextY + partLayout.Height;
                                layout.AddPart(partLayout);
                                // создаем макет линии реза, который отрезает эту деталь от полосы
                                cut = new CutLineLayout();
                                cut.X1 = nextX;
                                cut.Y1 = nextY;
                                cut.X2 = nextX + placedPart.Part.Width;
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
                                nextY = nextY + partLayout.Width;
                                RotatePartLayout(partLayout, placedPart.IsRotated);
                                layout.AddPart(partLayout);
                                // создаем макет линии реза, который отрезает эту деталь от полосы
                                cut = new CutLineLayout();
                                cut.X1 = nextX;
                                cut.Y1 = nextY;
                                cut.X2 = nextX + placedPart.Part.Height;
                                cut.Y2 = nextY;
                                layout.AddCut(cut);
                                // подготавливаем nextY для следующей детали
                                nextY = nextY + settings.SawWidth;
                                break;

                            case 3: // полоса горизонтально, детали вертикально Height x Width, координата nextX для линии реза увеличивается на part.Width,
                                    // а для следующей детали еще на SawWidth

                                // создаем макет детали и заносим в список макета раскроя
                                partLayout = new(placedPart.Part);
                                partLayout.InstanceNum = placedPart.InstanceNum;
                                partLayout.X = nextX;
                                partLayout.Y = nextY;
                                nextX = nextX + partLayout.Width;
                                layout.AddPart(partLayout);
                                // создаем макет линии реза, который отрезает эту деталь от полосы
                                cut = new CutLineLayout();
                                cut.X1 = nextX;
                                cut.Y1 = nextY;
                                cut.X2 = cut.X1;
                                cut.Y2 = nextY + placedPart.Part.Height;
                                layout.AddCut(cut);
                                // подготавливаем nextY для следующей детали
                                nextX = nextX + settings.SawWidth;
                                break;

                            case 4: // полоса горизонтально, детали горизонтально Width x Height, координата nextX для линии реза увеличивается на part.Height,
                                    // а для следующей детали еще на SawWidth

                                // создаем макет детали и заносим в список макета раскроя
                                partLayout = new(placedPart.Part);
                                partLayout.InstanceNum = placedPart.InstanceNum;
                                partLayout.X = nextX;
                                partLayout.Y = nextY;
                                nextX = nextX + partLayout.Height;
                                RotatePartLayout(partLayout, placedPart.IsRotated);
                                layout.AddPart(partLayout);
                                // создаем макет линии реза, который отрезает эту деталь от полосы
                                cut = new CutLineLayout();
                                cut.X1 = nextX;
                                cut.Y1 = nextY;
                                cut.X2 = cut.X1;
                                cut.Y2 = nextY + placedPart.Part.Width;
                                layout.AddCut(cut);
                                // подготавливаем nextY для следующей детали
                                nextX = nextX + settings.SawWidth;
                                break;

                            default:
                                break;
                        }
                    }

                    // теперь переходим к обработке следующего уровня дерева
                    // смотрим, по какому способу была распилена текущая полоса, вызываем рекурсию, подбирая координаты полос левой и правой ветвей
                    switch (variant)
                    {
                        case 1:
                            if (strip.LeftStrip != null) TranslateStripToLayout(strip.LeftStrip, layout, x, nextY);
                            if (strip.RightStrip != null) TranslateStripToLayout(strip.RightStrip, layout, x + strip.PlacedParts[0]!.Part.Width + settings.SawWidth, y);
                            break;

                        case 2:
                            if (strip.LeftStrip != null) TranslateStripToLayout(strip.LeftStrip, layout, x, nextY);
                            if (strip.RightStrip != null) TranslateStripToLayout(strip.RightStrip, layout, x + strip.PlacedParts[0]!.Part.Height + settings.SawWidth, y);
                            break;

                        case 3:
                            if (strip.LeftStrip != null) TranslateStripToLayout(strip.LeftStrip, layout, nextX, y);
                            if (strip.RightStrip != null) TranslateStripToLayout(strip.RightStrip, layout, x, y + strip.PlacedParts[0]!.Part.Height + settings.SawWidth);
                            break;

                        case 4:
                            if (strip.LeftStrip != null) TranslateStripToLayout(strip.LeftStrip, layout, nextX, y);
                            if (strip.RightStrip != null) TranslateStripToLayout(strip.RightStrip, layout, x, y + strip.PlacedParts[0]!.Part.Width + settings.SawWidth);
                            break;

                        default:
                            break;
                    }
                }
                else
                { // если детали в полосе не были размещены, значит это деловой остаток или отход. Детали и линии реза не "рисуем"

                    // пока не знаю что здесь сделать, может быть, если это остаток, его тоже рисовать, но тогда в PlacedPartLayout нужно дополнительное свойство
                    // сделать, деталь это или остаток рисоваться должен. но это потом
                }
            }
        }

        // если деталь была повернута в раскрое, то обменять высоту и ширину местами в макете детали
        private void RotatePartLayout(PlacedPartLayout part, bool isRotated)
        {
            if (isRotated)
            {
                double m = part.Height;
                part.Height = part.Width;
                part.Width = m;
            }
        }
    }
}
