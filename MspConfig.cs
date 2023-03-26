using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib
{
    public class MspConfig
    {
        public Server Server { get; set; }
        public string Proxy { get; set; } = null;
        public bool UseSocket { get; set; } = false;
    }
}
