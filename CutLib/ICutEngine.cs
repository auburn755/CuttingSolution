using CutLib.DTO;
using CutLib.InputClasses;
using CutLib.InternalClasses;
using CutLib.OutputClasses;

namespace CutLib
{
    public interface ICutEngine
    {
        void AddParts(List<PartData> Parts, bool ListNumbering = true);
        void SetBaseStock(StockData BaseStock);
        void AddOffcuts(List<StockData> Offcuts);
        void SetMaterial(MaterialData Material);
        void SetCutSettings(CutSettingsData CutSettings);
        CutResult Execute();
    }
}
