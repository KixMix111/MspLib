using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Class
{
    public class NebulaLoginStatus
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresDate { get; set; }
        public string ProfileId { get; set; }
    }
}
