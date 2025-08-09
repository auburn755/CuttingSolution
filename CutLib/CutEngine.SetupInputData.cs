using CutLib.DTO;
using CutLib.InputClasses;
namespace CutLib
{
    // методы задания входящих данных
    public partial class CutEngine
    {
        /// <summary>
        /// Добавить список деталей.
        /// </summary>
        /// <param name="ListNumbering">Задает способ нумерации Part. true - детали нумеруются по порядку входящего списка; 
        /// false - используется PartData.Number</param>
        public void AddParts(List<PartData> Parts, bool ListNumbering = true)
        {
            if (Parts is null || Parts.Count == 0) throw new CutLibInvalidPartsException("Входящий список Parts = null или пуст.");
            Part part;
            int typeNum = 0;
            foreach (var inputPart in Parts)
            {
                if (inputPart.Height > 0 && inputPart.Width > 0 && inputPart.Count > 0)
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
                else throw new CutLibInvalidPartsException("Одна из деталей в Parts, имеет некорректные размеры или количество.");
            }
        }

        /// <summary>
        /// Задать основной лист материала
        /// </summary>
        public void SetBaseStock(StockData BaseStock)
        {
            if (BaseStock is null) throw new CutLibInvalidStocksException("BaseStock = null");
            if (BaseStock.Height > 0 && BaseStock.Width > 0)
            {
                sourceStocks.SetBaseStock(BaseStock.Height, BaseStock.Width, new Trim(BaseStock.TrimLeft, BaseStock.TrimRight, BaseStock.TrimTop, BaseStock.TrimBottom));
            }
            else throw new CutLibInvalidStocksException("Размеры базового листа некорректны.");
        }

        /// <summary>
        /// Добавить список обрезков
        /// </summary>
        public void AddOffcuts(List<StockData> Offcuts)
        {
            if (Offcuts is null || Offcuts.Count == 0) throw new CutLibInvalidStocksException("Входящий список Offcuts = null или пуст.");
            foreach (var Offcut in Offcuts)
            {
                if (Offcut.Height > 0 && Offcut.Width > 0 && Offcut.Count > 0)
                {
                    if (sourceStocks.IsSizeOffcutExceeded(Offcut.Height, Offcut.Width)) throw new CutLibInvalidStocksException("Размер обрезка превышает размер основного листа.");
                    sourceStocks.AddOffcut(Offcut.Height, Offcut.Width, new Trim(Offcut.TrimLeft, Offcut.TrimRight, Offcut.TrimTop, Offcut.TrimBottom), Offcut.Count);
                }
                else throw new CutLibInvalidStocksException("Один из обрезков в Offcuts, имеет некорректные размеры или количество.");
            }
        }

        /// <summary>
        /// Задать материал
        /// </summary>
        public void SetMaterial(MaterialData Material)
        {
            if (Material is null) throw new CutLibInvalidMaterialException("Material = null");
            material.Name = Material.Name; 
        }

        /// <summary>
        /// Задать параметры раскроя
        /// </summary>
        public void SetCutSettings(CutSettingsData CutSettings)
        {
            settings.SawWidth = CutSettings.SawWidth;
            settings.MaxCutLength = CutSettings.MaxCutLength;
            settings.MaxSheetRotation = CutSettings.MaxSheetRotation;
            settings.MinWasteLength = CutSettings.MinWasteLength;
        }
    }
}
