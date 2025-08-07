using CutLib.DTO;
using CutLib.InputClasses;
namespace CutLib
{
    public partial class CutEngine
    {
        /// <summary>
        /// Добавить список деталей.
        /// </summary>
        /// <param name="ListNumbering">Задает способ нумерации Part. true - детали нумеруются по порядку входящего списка; 
        /// false - используется PartData.Number</param>
        public void AddParts(List<PartData> Parts, bool ListNumbering = true)
        {
            if (Parts == null || Parts.Count == 0) throw new InvalidPartsListException("Входящий список Parts пуст.");
            Part part;
            int typeNum = 0;
            foreach (var inputPart in Parts)
            {
                part = new Part();
                if (ListNumbering)
                {
                    typeNum++;
                    part.TypeNum = typeNum;
                }
                else
                {
                    part.TypeNum = inputPart.Number;
                }
                part.Name = inputPart.Name;
                part.Height = inputPart.Height;
                part.Width = inputPart.Width;
                part.Placed = 0;
                part.Count = inputPart.Count;
                part.CanRotate = inputPart.CanRotate;
                parts.Add(part);
            }
        }

        /// <summary>
        /// Задать основной лист материала
        /// </summary>
        public void SetBaseStock(StockData BaseStock)
        {
            sourceStocks.AddStock(BaseStock.Height, BaseStock.Width, new Trim(BaseStock.TrimLeft, BaseStock.TrimRight, BaseStock.TrimTop, BaseStock.TrimBottom));
        }

        /// <summary>
        /// Добавить список обрезков
        /// </summary>
        public void AddOffcuts(List<StockData> Offcuts)
        {
            if (Offcuts == null || Offcuts.Count == 0) throw new InvalidOffcutsListException("Входящий список Offcuts пуст.");
            foreach (var Offcut in Offcuts)
            {
                if (sourceStocks.IsSizeOffcutExceeded(Offcut.Height, Offcut.Width)) throw new InvalidOffcutsListException("Размер обрезка превышает размер листа.");
                sourceStocks.AddStock(Offcut.Height, Offcut.Width, new Trim(Offcut.TrimLeft, Offcut.TrimRight, Offcut.TrimTop, Offcut.TrimBottom), Offcut.Count);
            }
        }

        /// <summary>
        /// Задать материал
        /// </summary>
        public void SetMaterial(MaterialData Material)
        {
            sourceStocks.Material = Material.Name;
        }

        /// <summary>
        /// Задать параметры раскроя
        /// </summary>
        public void SetCutSettings(CutSettingsData CutSettings)
        {
            settings.SawWidth=CutSettings.SawWidth;
            settings.MaxCutLength=CutSettings.MaxCutLength;
            settings.MaxSheetRotation=CutSettings.MaxSheetRotation;
            settings.MinWasteLength=CutSettings.MinWasteLength;
        }
    }
}
