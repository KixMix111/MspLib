using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Class
{
    public class Clothes
    {
        public int ActorClothesRelId { get; set; }
        public int ActorId { get; set; }
        public int ClothesId { get; set; }
        public string Color { get; set; }
        public int IsWearing { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
