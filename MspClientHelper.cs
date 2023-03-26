using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib
{
    internal class MspClientHelper
    {
        public static string ParseServer(Server server)
        {
            switch (server)
            {
                case Server.Danmark:
                    return "DK";
                case Server.Sverige:
                    return "SE";
                case Server.Norge:
                    return "NO";
                case Server.Suomi:
                    return "FI";
                case Server.Deutschland:
                    return "DE";
                case Server.USA:
                    return "US";
                case Server.UK:
                    return "GB";
                case Server.Canada:
                    return "CA";
                case Server.Australia:
                    return "AU";
                case Server.NewZealand:
                    return "NZ";
                case Server.Ireland:
                    return "IE";
                case Server.France:
                    return "FR";
                case Server.Polska:
                    return "PL";
                case Server.Nederland:
                    return "NL";
                case Server.España:
                    return "ES";
                case Server.Tûrkiye:
                    return "TR";
                default:
                    return "DK";
            }
        }
    }
}
