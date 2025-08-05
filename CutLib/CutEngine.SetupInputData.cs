using CutLib.DTO;
namespace CutLib
{
    public partial class CutEngine
    {
        public void Parts(List<PartData> PartsData)
        {
            if (PartsData == null || PartsData.Count == 0) throw new InvalidPartsListException("Входящий список деталей пуст");
            foreach (var part in PartsData)
            {
                // передаем входящие детали во внутренний список Parts
            }
        }
    }
}
