using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CutLib.DTO
{
    public class PartData
    {
        public string Name { get; set; } = "";
        /// <summary>
        /// Номер детали для отчета или чертежей. Если не используется, детали будут нумероваться по порядку входящего списка.
        /// </summary>
        public int Number { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int Count { get; set; }
        /// <summary>
        /// true - поворот детали допускается
        /// </summary>
        public bool CanRotate { get; set; }
    }
}
