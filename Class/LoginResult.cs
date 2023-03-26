using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Class
{
    public class LoginResult
    {
        public string Status { get; set; }
        public string StatusDetails { get; set; }
        public ActorDetails Actor { get; set; }
        public PiggyBank PiggyBank { get; set; }
        public string Ticket { get; set; }
        public NebulaLoginStatus NebulaLoginStatus { get; set; }
    }
}
