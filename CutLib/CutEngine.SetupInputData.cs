using CutLib.DTO;
using CutLib.InputClasses;
namespace CutLib
{
    public partial class CutEngine
    {
        /// <summary>
        /// Добавить список деталей. Если ListNumbering=true, то Part присваевается номер по порядку в списке, начиная с 1, 
        /// если ListNumbering=false, то берется входящий номер Part.Number.
        /// </summary>
        public void AddParts(List<PartData> PartsData, bool ListNumbering = true)
        {
            if (PartsData == null || PartsData.Count == 0) throw new InvalidPartsListException("Входящий список деталей пуст");
            Part part;
            int typeNum = 0;
            foreach (var inputPart in PartsData)
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
    }
}
