using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Class
{
    internal class MspResponse
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public dynamic Response { get; set; }
    }
}
